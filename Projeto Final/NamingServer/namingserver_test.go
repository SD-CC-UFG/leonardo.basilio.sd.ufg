package main

import (
	"testing"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/Chat_Grpc"
	"golang.org/x/net/context"
	"google.golang.org/grpc"
)

func TestRequestAuthServer(t *testing.T) {
	t.Log("Test request for a AuthServer.")
	conn, err := grpc.Dial("localhost:7777", grpc.WithInsecure())
	if err != nil {
		t.Error(err)
	}
	defer conn.Close()

	client := pb.NewNamingServerClient(conn)

	request := &pb.ServiceRequest{Name: pb.ServiceRequest_AUTH}

	response, err := client.GetServiceLocation(context.Background(), request)

	if err != nil {
		t.Error(err)
	}

	t.Logf("Test successful. Response: %v.\n", response)
}

func TestRequestMessagingServer(t *testing.T) {
	t.Log("Test request for MessagingServer.")

	conn, err := grpc.Dial("localhost:7777", grpc.WithInsecure())
	if err != nil {
		t.Error(err)
	}
	defer conn.Close()

	client := pb.NewNamingServerClient(conn)

	request := &pb.ServiceRequest{Name: pb.ServiceRequest_MESSAGING}

	response, err := client.GetServiceLocation(context.Background(), request)

	if err != nil {
		t.Error(err)
	}

	t.Logf("Test successful. Response: %v.\n", response)
}

func TestRequestNotExistService(t *testing.T) {
	t.Log("Test request for a service that do not exists.")

	conn, err := grpc.Dial("localhost:7777", grpc.WithInsecure())
	if err != nil {
		t.Error(err)
	}
	defer conn.Close()

	client := pb.NewNamingServerClient(conn)

	_, err = client.GetServiceLocation(context.Background(),
		&pb.ServiceRequest{Name: 1000})

	if err == nil {
		t.Errorf("Request a service not existent and NamingServer do not return error.\n")
	}

	t.Logf("Test successful. Error returned: %v.\n", err)
}
