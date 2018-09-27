package main

import (
	"io"
	"testing"
	"time"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"golang.org/x/net/context"
	"google.golang.org/grpc"
)

func createClientConnTest(username string, parallel bool) func(t *testing.T) {
	return func(t *testing.T) {
		if parallel {
			t.Parallel()
		}
		conn, err := grpc.Dial("localhost:7777", grpc.WithInsecure())
		if err != nil {
			t.Error(err)
		}
		defer conn.Close()

		client := pb.NewMessagingServerClient(conn)

		stream, err := client.TalkAndListen(context.Background())

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

		for i := 0; i < 3; i++ {
			chatMessage := &pb.ChatMessage{}
			chatMessage.UserCredential = &pb.UserCredential{UserName: username}
			chatMessage.DateTime = uint32(time.Now().Unix())
			chatMessage.Text = &pb.TextMessage{Text: "Ola a todos!"}

			if err := stream.Send(chatMessage); err != nil {
				t.Fatalf("Failed to send a message: %v", err)
			}

			d, _ := time.ParseDuration("3s")
			time.Sleep(d)
		}
		stream.CloseSend()
		<-waitc
	}
}

func TestClientConn(t *testing.T) {
	t.Run("Test 1", createClientConnTest("leandro", true))
}

func TestTwoClientConn(t *testing.T) {
	t.Run("Test 1", createClientConnTest("leandro", true))
	t.Run("Test 2", createClientConnTest("joao", true))
}
