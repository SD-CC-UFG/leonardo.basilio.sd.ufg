package server

import (
	"context"

	"fmt"
	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"google.golang.org/grpc"
	"io"
	"log"
	"math/rand"
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
	mutex        *sync.Mutex
	userChannels map[string]chan *pb.ChatMessage
	chatChannel  chan *pb.ChatMessage
	ip           string
	port         int
	peers        []*pb.ServiceResponse
	topicsUser   map[userTopicKey]bool
	brokerSubs   []brokerSub
}

func NewMessagingServer(port int) MessagingServer {
	return MessagingServer{
		mutex:        &sync.Mutex{},
		userChannels: make(map[string]chan *pb.ChatMessage),
		chatChannel:  make(chan *pb.ChatMessage),
		port:         port,
		topicsUser:   make(map[userTopicKey]bool),
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

	for {
		// receive message from a user
		chatMessage := <-s.chatChannel
		username := chatMessage.UserCredential.UserName

		if chatMessage.GetControl() != nil {
			if chatMessage.GetControl().GetType() == pb.ControlMessageType_JOINED {
				s.topicsUser[userTopicKey{username, chatMessage.Topic}] = true
			}
		}

		if chatMessage.GetText() != nil {
			if s.topicsUser[userTopicKey{username, chatMessage.Topic}] == false {
				// user trying to send message to topic he's not joined

				log.Printf("User %s is not joined in topic %s, ignoring message.\n", username, chatMessage.Topic)
				continue
			}
		}

		log.Printf("Message received: %v\n", chatMessage)

		// forward the message to interested users
		s.mutex.Lock()
		for username, userChannel := range s.userChannels {
			v, ok := s.topicsUser[userTopicKey{username, chatMessage.Topic}]
			if ok && v {
				userChannel <- chatMessage
			}
		}
		s.mutex.Unlock()

		// forward the message to interested broker
		for _, v := range s.brokerSubs {
			if v.topic == chatMessage.Topic {
				s.sendMessageToBroker(v, chatMessage)
			}
		}

		// verify if it is a control message
		if chatMessage.GetControl() != nil {
			if chatMessage.GetControl().GetType() == pb.ControlMessageType_QUITTED {
				s.topicsUser[userTopicKey{username, chatMessage.Topic}] = false
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

	var seed = time.Now().UTC().UnixNano()
	rand.Seed(seed)

	health := rand.Float32()
	regReq := &pb.RegistrationRequest{Name: pb.ServiceType_MESSAGING, Port: int32(s.port), Health: health}
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

	// start goroutine to ping naming service periodically
	go func() {
		for {
			duration, _ := time.ParseDuration("5s")
			time.Sleep(duration) // sleep for 5 seconds

			conn, err := grpc.Dial(namingServerAddr, grpc.WithInsecure())
			if err != nil {
				log.Fatal(err)
			}

			client := pb.NewNamingClient(conn)

			// log.Print("Ping naming server.")
			health := rand.Float32()
			pingReq := &pb.PingRequest{Name: pb.ServiceType_MESSAGING, Port: int32(s.port), Health: health}
			pingRes, err := client.Ping(context.Background(), pingReq)

			if err != nil {
				log.Fatal(err)
			}

			if !pingRes.Success {
				log.Fatalf("Error in ping naming server.\n")
			}

			conn.Close()
		}
	}()
}

func (s *MessagingServer) addUser(username string) {
	if username != "" {
		s.mutex.Lock()
		s.userChannels[username] = make(chan *pb.ChatMessage)
		s.mutex.Unlock()
	}
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
	s.mutex.Lock()
	channel, ok = s.userChannels[username]
	s.mutex.Unlock()
	return
}

func (s *MessagingServer) sendMessagesUser(username string, stream pb.Messaging_TalkAndListenServer) {
	channel, ok := s.getUserChannel(username)

	if !ok {
		log.Printf("Error: request send messages to user that do not exist (user %s)\n", username)
		return
	}

	// envia mensagens do chat para o usuario
	for {
		chatMessage, more := <-channel
		if !more {
			log.Printf("Username %s: channel closed.\n", username)
			break
		}
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

	log.Printf("Publish in broker (%s, %d) message of topic %s\n", subscription.ip, subscription.port,
		chatMessage.Topic)

	_, err = client.Publish(context.Background(), chatMessage)
	if err != nil {
		log.Printf("Error on sending message to broker (%s, %d): %v\n", subscription.ip, subscription.port, err)
		return
	}
}

func (s *MessagingServer) TalkAndListen(stream pb.Messaging_TalkAndListenServer) error {
	var username string

	for {
		in, err := stream.Recv()

		if err != nil {
			// close channel of user in case of EOF or any error
			// with channel closed the goroutine that send messages
			// to this user can exit

			s.closeUser(username)

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
			s.addUser(username)
			go s.sendMessagesUser(username, stream)
		}

		// send to main channel, where messages are processed
		s.chatChannel <- in
	}

	return nil
}

func (s *MessagingServer) Subscribe(ctx context.Context, request *pb.SubscribeRequest) (*pb.SubscribeResponse, error) {
	// store topic and interested broker
	for _, v := range s.brokerSubs {
		if v.ip == request.Ip && v.port == request.Port && v.topic == request.Topic {
			// subscription already stored, ignore
			return &pb.SubscribeResponse{}, nil
		}
	}

	s.brokerSubs = append(s.brokerSubs, brokerSub{ip: request.Ip, port: request.Port, topic: request.Topic})

	return &pb.SubscribeResponse{}, nil
}

func (s *MessagingServer) Unsubscribe(ctx context.Context, request *pb.SubscribeRequest) (*pb.SubscribeResponse, error) {
	// remove the broker from list of interested in this topic
	for i, v := range s.brokerSubs {
		if v.ip == request.Ip && v.port == request.Port && v.topic == request.Topic {
			s.brokerSubs[i] = s.brokerSubs[len(s.brokerSubs)-1] // assign last element to pos i
			s.brokerSubs = s.brokerSubs[:len(s.brokerSubs)-1]   // truncate slice, removing last pos
			break
		}
	}

	return &pb.SubscribeResponse{}, nil
}

func (s *MessagingServer) Publish(ctx context.Context, chatMessage *pb.ChatMessage) (*pb.PublishResponse, error) {
	// verify the users interested in the topic of this message
	// send message to this users' channels

	s.chatChannel <- chatMessage
	return nil, nil
}
