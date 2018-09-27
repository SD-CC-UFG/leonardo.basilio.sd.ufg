package main

import (
	"io"
	"testing"
	"time"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"golang.org/x/net/context"
	"google.golang.org/grpc"
)

func TestClientConn(t *testing.T) {
	conn, err := grpc.Dial("localhost:7777", grpc.WithInsecure())
	if err != nil {
		t.Error(err)
	}
	defer conn.Close()

	client := pb.NewMessagingServerClient(conn)

	stream, err := client.TalkAndListen(context.Background())
	go func() {
		for {
			in, err := stream.Recv()
			if err == io.EOF {
				// read done.
				return
			}
			if err != nil {
				t.Fatalf("Failed to receive a note : %v", err)
			}

			t.Logf("Message received from server: %v", in)
		}
	}()

	for i := 0; i < 10; i++ {
		chatMessage := &pb.ChatMessage{}
		chatMessage.UserCredential = &pb.UserCredential{UserName: "leandro"}
		chatMessage.DateTime = uint32(time.Now().Unix())
		chatMessage.Text = &pb.TextMessage{Text: "Ola a todos!"}

		if err := stream.Send(chatMessage); err != nil {
			t.Fatalf("Failed to send a message: %v", err)
		}

		d, _ := time.ParseDuration("5s")
		time.Sleep(d)
	}
	stream.CloseSend()
}
