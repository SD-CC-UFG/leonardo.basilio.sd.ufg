package main

import (
	"fmt"
	"github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/server"
	"log"
	"net"
	"os"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"google.golang.org/grpc"
)

func main() {
	// get the port from env var
	port := os.Getenv("PORT")
	if port == "" {
		port = "8888"
	}

	listen, err := net.Listen("tcp", fmt.Sprintf(":%s", port))

	if err != nil {
		log.Fatal(err)
	}

	defer listen.Close()

	grpcServer := grpc.NewServer()

	messagingServer := server.NewMessagingServer()
	messagingServer.StartLoop()

	pb.RegisterMessagingServer(grpcServer, &messagingServer)

	log.Printf("Listening on port %v.\n", port)

	grpcServer.Serve(listen)
}
