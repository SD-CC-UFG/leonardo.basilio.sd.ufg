package main

import (
	"fmt"
	"io"
	"testing"
	"time"

	pb "MessagingServer/grpc"
	"golang.org/x/net/context"
	"google.golang.org/grpc"
)

func createClientConnTest(username string, topic string, parallel bool) func(t *testing.T) {
	return func(t *testing.T) {
		if parallel {
			t.Parallel()
		}
		conn, err := grpc.Dial("localhost:8888", grpc.WithInsecure())
		if err != nil {
			t.Error(err)
		}
		defer conn.Close()

		client := pb.NewMessagingClient(conn)

		stream, err := client.TalkAndListen(context.Background())

		if err != nil {
			t.Error(err)
		}

		waitc := make(chan struct{})
		go func() {
			for {
				in, err := stream.Recv()
				if err == io.EOF {
					// read done.
					close(waitc)
					return
				}
				if err != nil {
					t.Fatalf("Failed to receive a note : %v", err)
				}

				t.Logf("Message received from server: %v", in)
			}
		}()

		const n = 5
		for i := 0; i < n; i++ {
			chatMessage := &pb.ChatMessage{}
			chatMessage.UserCredential = &pb.UserCredential{UserName: username}
			chatMessage.Timestamp = uint32(time.Now().Unix())
			chatMessage.Topic = topic
			if i == 0 {
				chatMessage.Type = pb.ChatMessageType_CONTROL
				chatMessage.Payload = &pb.ChatMessage_Control{Control: &pb.ControlMessage{Type: pb.ControlMessageType_JOINED}}
			} else if i == n-1 {
				chatMessage.Type = pb.ChatMessageType_CONTROL
				chatMessage.Payload = &pb.ChatMessage_Control{Control: &pb.ControlMessage{Type: pb.ControlMessageType_QUITTED}}
			} else {
				chatMessage.Type = pb.ChatMessageType_TEXT
				chatMessage.Payload = &pb.ChatMessage_Text{Text: &pb.TextMessage{Text: "Ola a todos!"}}
			}

			if err := stream.Send(chatMessage); err != nil {
				t.Fatalf("Failed to send a message: %v", err)
			}

			d, _ := time.ParseDuration("1s")
			time.Sleep(d)
		}

		stream.CloseSend()
		<-waitc
	}
}

func TestClientConn(t *testing.T) {
	t.Run("Test 1", createClientConnTest("leandro", "#cpp", true))
}

func TestNClientConn(t *testing.T) {
	const N = 30
	for i := 0; i < N; i++ {
		t.Run(fmt.Sprintf("Test %d", i),
			createClientConnTest(fmt.Sprintf("user_%0d", i), "#csharp", true))
	}
	for i := 0; i < N; i++ {
		t.Run(fmt.Sprintf("Test %d", N+i),
			createClientConnTest(fmt.Sprintf("user_%0d", N+i), "#golang", true))
	}
}
