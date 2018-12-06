package server

import (
	"context"

	pb "MessagingServer/grpc"
	"fmt"
	"google.golang.org/grpc"
	"io"
	"log"
	"net"
	"sync"
	"time"
)

type userTopicKey struct {
	username string
	topic    string
}

type brokerSub struct {
	ip    string
	port  int32
	topic string
}

type MessagingServer struct {
	mutex           *sync.Mutex
	userChannels    map[string]chan *pb.ChatMessage
	chatChannel     chan *pb.ChatMessage
	ip              string
	port            int
	peers           []*pb.ServiceResponse
	topicsUser      map[userTopicKey]bool
	brokerSubs      []brokerSub
	brokerSubsMutex *sync.Mutex
	health          float32
}

func NewMessagingServer(port int) MessagingServer {
	return MessagingServer{
		mutex:           &sync.Mutex{},
		userChannels:    make(map[string]chan *pb.ChatMessage),
		chatChannel:     make(chan *pb.ChatMessage),
		port:            port,
		topicsUser:      make(map[userTopicKey]bool),
		brokerSubsMutex: &sync.Mutex{},
	}
}

func (s *MessagingServer) Start() {
	go s.loopMessages()
	go s.registerNamingServer()

	// configure grpc
	listen, err := net.Listen("tcp", fmt.Sprintf(":%d", s.port))

	if err != nil {
		log.Fatal(err)
	}
	defer listen.Close()

	grpcServer := grpc.NewServer()
	pb.RegisterMessagingServer(grpcServer, s)

	log.Printf("Listening on port %v.\n", s.port)
	grpcServer.Serve(listen)
}

func (s *MessagingServer) loopMessages() {
	log.Printf("Starting looping to receive messages of users.\n")
	topicsUser := make(map[userTopicKey]bool)
	topicsCount := make(map[string]int)

	for {
		// receive message from a user
		chatMessage := <-s.chatChannel
		username := chatMessage.UserCredential.UserName

		if !chatMessage.External && chatMessage.GetControl() != nil {
			if chatMessage.GetControl().GetType() == pb.ControlMessageType_JOINED {
				log.Printf("User %s joined topic %s\n", username, chatMessage.Topic)
				topicsUser[userTopicKey{username, chatMessage.Topic}] = true

				if topicsCount[chatMessage.Topic] == 0 {
					req := &pb.SubscribeRequest{Ip: s.ip, Port: int32(s.port), Topic: chatMessage.Topic}
					s.floodingSubscription(req)
				}

				topicsCount[chatMessage.Topic]++
			}
		}

		if !chatMessage.External && chatMessage.GetText() != nil {
			if topicsUser[userTopicKey{username, chatMessage.Topic}] == false {
				// user trying to send message to topic he's not joined

				log.Printf("User %s is not joined in topic %s, ignoring message.\n", username, chatMessage.Topic)
				continue
			}
		}

		if chatMessage.External {
			log.Printf("External chat message\n")
		}
		log.Printf("Message received: %v\n", chatMessage)

		// forward the message to interested users
		s.mutex.Lock()
		for uname, userChannel := range s.userChannels {
			v := topicsUser[userTopicKey{uname, chatMessage.Topic}]
			if v {
				log.Printf("Send message from %s to %s\n", username, uname)
				userChannel <- chatMessage
			}
		}
		s.mutex.Unlock()

		if !chatMessage.External {
			// forward the message to interested broker
			for _, v := range s.brokerSubs {
				if v.topic == chatMessage.Topic {
					s.sendMessageToBroker(v, chatMessage)
				}
			}
		}

		// verify if it is a control message
		if !chatMessage.External && chatMessage.GetControl() != nil {
			if chatMessage.GetControl().GetType() == pb.ControlMessageType_QUITTED {
				log.Printf("User %s quitted topic %s\n", username, chatMessage.Topic)
				topicsUser[userTopicKey{username, chatMessage.Topic}] = false
				topicsCount[chatMessage.Topic]--
				if topicsCount[chatMessage.Topic] == 0 {
					req := &pb.SubscribeRequest{Ip: s.ip, Port: int32(s.port), Topic: chatMessage.Topic}
					s.floodingUnsubscription(req)
				}
			}
		}
	}
}

func (s *MessagingServer) registerNamingServer() {
	// register messaging service in naming service
	const namingServerAddr = "naming:7777"

	conn, err := grpc.Dial(namingServerAddr, grpc.WithInsecure())
	if err != nil {
		log.Fatal(err)
	}
	defer conn.Close()

	client := pb.NewNamingClient(conn)

	s.health = 100.0
	regReq := &pb.RegistrationRequest{Name: pb.ServiceType_MESSAGING, Port: int32(s.port), Health: s.health}
	regRes, err := client.RegisterService(context.Background(), regReq)

	if err != nil {
		log.Fatal(err)
	}

	if regRes.Success == false {
		log.Fatalf("Registration in naming server failed.\n")
	}

	log.Printf("Registration in naming server (%v)\n", regReq)

	s.ip = regRes.Ip
	s.peers = regRes.Peers
	log.Printf("Peers returned: %v\n", s.peers)

	// start goroutine to ping naming service periodically
	go func() {
		for {
			duration, _ := time.ParseDuration("2s")
			time.Sleep(duration) // sleep for 5 seconds

			conn, err := grpc.Dial(namingServerAddr, grpc.WithInsecure())
			if err != nil {
				log.Fatal(err)
			}

			client := pb.NewNamingClient(conn)

			pingReq := &pb.PingRequest{Name: pb.ServiceType_MESSAGING, Port: int32(s.port), Health: s.health}
			//log.Printf("Ping naming server (%v)", pingReq)
			pingRes, err := client.Ping(context.Background(), pingReq)

			if err != nil {
				log.Fatal(err)
			}

			if !pingRes.Success {
				log.Println("Error in ping naming server.")
			}

			if len(s.peers) < 1 {
				req := &pb.ServiceRequest{Name: pb.ServiceType_MESSAGING}
				res, err := client.GetServiceLocation(context.Background(), req)

				if err != nil {
					log.Printf("Error when trying to obtain a peer.\n")
				} else {
					if res.Ip != s.ip || int(res.Port) != s.port {
						s.peers = append(s.peers, res)
					}
				}
			}

			conn.Close()
		}
	}()
}

func (s *MessagingServer) addUser(username string) (ch chan *pb.ChatMessage) {
	if username != "" {
		s.mutex.Lock()
		s.userChannels[username] = make(chan *pb.ChatMessage)
		ch = s.userChannels[username]
		s.mutex.Unlock()
	} else {
		ch = nil
	}
	return
}

func (s *MessagingServer) closeUser(username string) {
	if username != "" {
		s.mutex.Lock()
		channel, ok := s.userChannels[username]
		if ok {
			close(channel)
		}
		delete(s.userChannels, username)
		s.mutex.Unlock()
	}
}

func (s *MessagingServer) getUserChannel(username string) (channel chan *pb.ChatMessage, ok bool) {
	if username == "" {
		return nil, false
	}
	channel, ok = s.userChannels[username]
	return
}

func (s *MessagingServer) sendMessagesUser(username string, channel chan *pb.ChatMessage, stream pb.Messaging_TalkAndListenServer) {
	log.Printf("Start loop to send messages to user %s\n", username)

	// envia mensagens do chat para o usuario
	for {
		log.Printf("Waiting for message for user %s\n", username)
		chatMessage, more := <-channel
		if !more {
			log.Printf("Username %s: channel closed.\n", username)
			break
		}
		log.Printf("Send message (%v) to %s.\n", chatMessage, username)
		if err := stream.Send(chatMessage); err != nil {
			log.Println(err)
			break
		}
	}
}

func (s *MessagingServer) sendMessageToBroker(subscription brokerSub, chatMessage *pb.ChatMessage) {
	conn, err := grpc.Dial(fmt.Sprintf("%s:%d", subscription.ip, subscription.port), grpc.WithInsecure())
	if err != nil {
		log.Fatal(err)
	}
	defer conn.Close()

	client := pb.NewMessagingClient(conn)

	log.Printf("Publish in broker (%s, %d) message of topic %s\n", subscription.ip, subscription.port, chatMessage.Topic)

	chatMessage.External = true
	_, err = client.Publish(context.Background(), chatMessage)
	if err != nil {
		log.Printf("Error on sending message to broker (%s, %d): %v\n", subscription.ip, subscription.port, err)
	}
}

func (s *MessagingServer) floodingSubscription(req *pb.SubscribeRequest) {
	for _, v := range s.peers {
		conn, err := grpc.Dial(fmt.Sprintf("%s:%d", v.Ip, v.Port), grpc.WithInsecure())
		if err != nil {
			log.Fatal(err)
		}
		defer conn.Close()

		client := pb.NewMessagingClient(conn)

		log.Printf("Send subscription in topic %s to broker (%s, %d)", req.Topic, v.Ip, v.Port)
		_, err = client.Subscribe(context.Background(), req)
		if err != nil {
			log.Printf("Error on sending to peer (%s, %d)\n", v.Ip, v.Port)
			continue
		}
	}
}

func (s *MessagingServer) floodingUnsubscription(req *pb.SubscribeRequest) {
	for _, v := range s.peers {
		conn, err := grpc.Dial(fmt.Sprintf("%s:%d", v.Ip, v.Port), grpc.WithInsecure())
		if err != nil {
			log.Fatal(err)
		}
		defer conn.Close()

		client := pb.NewMessagingClient(conn)

		log.Printf("Send unsubscription in topic %s to broker (%s, %d)", req.Topic, v.Ip, v.Port)
		_, err = client.Unsubscribe(context.Background(), req)
		if err != nil {
			log.Printf("Error on sending to peer (%s, %d)\n", v.Ip, v.Port)
			continue
		}

	}
}

func (s *MessagingServer) TalkAndListen(stream pb.Messaging_TalkAndListenServer) error {
	var username string
	s.health--

	for {
		in, err := stream.Recv()

		if err != nil {
			// close channel of user in case of EOF or any error
			// with channel closed the goroutine that send messages
			// to this user can exit

			s.closeUser(username)
			s.health++

			if err != io.EOF {
				log.Println(err)
				return err
			} else {
				return nil
			}
		}

		// verify user credentials here
		if in.UserCredential == nil {
			log.Printf("User %s disconnect by: without credential.\n", username)
			s.closeUser(username)
			return nil
		}

		if username == "" {
			username = in.UserCredential.UserName
			channel := s.addUser(username)
			go s.sendMessagesUser(username, channel, stream)
		}

		in.External = false
		// send to main channel, where messages are processed
		s.chatChannel <- in
	}

	return nil
}

func (s *MessagingServer) Subscribe(ctx context.Context, request *pb.SubscribeRequest) (*pb.SubscribeResponse, error) {
	if request.Ip == s.ip && request.Port == int32(s.port) {
		return &pb.SubscribeResponse{}, nil
	}

	// store topic and interested broker
	for _, v := range s.brokerSubs {
		if v.ip == request.Ip && v.port == request.Port && v.topic == request.Topic {
			// subscription already stored, ignore
			log.Printf("Subscription already stored\n")
			return &pb.SubscribeResponse{}, nil
		}
	}

	s.brokerSubsMutex.Lock()
	s.brokerSubs = append(s.brokerSubs, brokerSub{ip: request.Ip, port: request.Port, topic: request.Topic})
	s.brokerSubsMutex.Unlock()

	go s.floodingSubscription(request)

	return &pb.SubscribeResponse{}, nil
}

func (s *MessagingServer) Unsubscribe(ctx context.Context, request *pb.SubscribeRequest) (*pb.SubscribeResponse, error) {
	if request.Ip == s.ip && request.Port == int32(s.port) {
		return &pb.SubscribeResponse{}, nil
	}

	// remove the broker from list of interested in this topic
	for i, v := range s.brokerSubs {
		if v.ip == request.Ip && v.port == request.Port && v.topic == request.Topic {
			s.brokerSubsMutex.Lock()
			s.brokerSubs[i] = s.brokerSubs[len(s.brokerSubs)-1] // assign last element to pos i
			s.brokerSubs = s.brokerSubs[:len(s.brokerSubs)-1]   // truncate slice, removing last pos
			s.brokerSubsMutex.Unlock()

			go s.floodingUnsubscription(request)

			break
		}
	}

	return &pb.SubscribeResponse{}, nil
}

func (s *MessagingServer) Publish(ctx context.Context, chatMessage *pb.ChatMessage) (*pb.PublishResponse, error) {
	// verify the users interested in the topic of this message
	// send message to this users' channels

	chatMessage.External = true
	s.chatChannel <- chatMessage
	return nil, nil
}
