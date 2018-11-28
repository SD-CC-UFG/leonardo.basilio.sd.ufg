package server

import (
	"context"
	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"io"
	"log"
	"sync"
)

type MessagingServer struct {
	mutex        *sync.Mutex
	userChannels map[string]chan *pb.ChatMessage
	chatChannel  chan *pb.ChatMessage
}

func NewMessagingServer() MessagingServer {
	return MessagingServer{
		mutex:        &sync.Mutex{},
		userChannels: make(map[string]chan *pb.ChatMessage),
		chatChannel:  make(chan *pb.ChatMessage),
	}
}

func (s *MessagingServer) StartLoop() {
	log.Printf("Starting looping to receive messages of users.\n")

	go func() {
		for {
			// recebe mensagem enviada por algum usuario
			chatMessage := <-s.chatChannel

			log.Printf("Message received: %v\n", chatMessage)

			s.mutex.Lock()
			// percorre todos os channels de usuarios ativos
			// e envia a mensagem
			for _, userChannel := range s.userChannels {
				userChannel <- chatMessage
			}
			s.mutex.Unlock()
		}
	}()
}

func (s *MessagingServer) sendMessages(username string, stream pb.Messaging_TalkAndListenServer) {
	log.Printf("Create channel to user %s.\n", username)

	channel := make(chan *pb.ChatMessage)

	s.mutex.Lock()
	s.userChannels[username] = channel
	s.mutex.Unlock()

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

	s.mutex.Lock()
	delete(s.userChannels, username)
	s.mutex.Unlock()
}

func (s *MessagingServer) TalkAndListen(stream pb.Messaging_TalkAndListenServer) error {
	var username string

	in, err := stream.Recv()

	if err != nil {
		log.Println(err)
		return err
	}

	if in.UserCredential != nil {
		if in.Type != pb.ChatMessageType_CONTROL || in.GetControl().Type != pb.ControlMessageType_JOINED {
			log.Printf("First message is not a control message of type JOINED.\n")
			return nil
		}
		username = in.UserCredential.UserName
		go s.sendMessages(username, stream)
	} else {
		log.Printf("User %s disconnect by: without credential.\n", username)
		return nil
	}

	for {
		in, err := stream.Recv()
		if err == io.EOF || err != nil {
			// em caso de erro (ou EOF), fecha o channel do usuario
			// liberando go routine que envia mensagens chat -> usuario
			s.mutex.Lock()
			channel, ok := s.userChannels[username]
			if ok {
				close(channel)
			}
			s.mutex.Unlock()

			if err != io.EOF {
				log.Println(err)
				return err
			} else {
				return nil
			}
		}

		// envia mensagem do usuario para o chat
		s.chatChannel <- in
	}

	return nil
}

func (s *MessagingServer) Subscribe(ctx context.Context, request *pb.SubscribeRequest) (*pb.SubscribeResponse, error) {
	// store topic and interested broker
	return nil, nil
}

func (s *MessagingServer) Unsubscribe(ctx context.Context, request *pb.SubscribeRequest) (*pb.SubscribeResponse, error) {
	// remove the broker from list of interested in this topic
	return nil, nil
}

func (s *MessagingServer) Publish(ctx context.Context, chatMessage *pb.ChatMessage) (*pb.PublishResponse, error) {
	// verify the users interested in the topic of this message
	// send message to this users' channels
	return nil, nil
}
