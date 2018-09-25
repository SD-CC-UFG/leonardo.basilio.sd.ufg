package main

import (
	"testing"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/Chat_Grpc"
	"golang.org/x/net/context"
	"google.golang.org/grpc"
)

// nesse prototipo se for requisitado AuthServer deve ser retornado
// ip: 200.0.13.102 port: 7777
// se for MessagingServer
// ip: 190.0.13.120 port: 8888

func TestRequestAuthServer(t *testing.T) {
	t.Log("Test request for a AuthServer.")
	conn, err := grpc.Dial("localhost:7777", grpc.WithInsecure())
	if err != nil {
		t.Error(err)
	}
	defer conn.Close()

	client := pb.NewNamingServerClient(conn)

	response, err := client.GetServiceLocation(context.Background(),
		&pb.ServiceRequest{Name: pb.ServiceRequest_AUTH})

	if err != nil {
		t.Error(err)
	}

	if response.Ip != "200.0.13.102" || response.Port != 7777 {
		t.Errorf("Response of naming server was incorrect.\n")
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

	response, err := client.GetServiceLocation(context.Background(),
		&pb.ServiceRequest{Name: pb.ServiceRequest_MESSAGING})

	if err != nil {
		t.Error(err)
	}

	if response.Ip != "190.0.13.120" || response.Port != 8888 {
		t.Errorf("Response of naming server was incorrect.\n")
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
