package main

import (
	"fmt"
	"github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/server"
	"log"
	"net"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"google.golang.org/grpc"
)

func main() {
	const port = 8888
	listen, err := net.Listen("tcp", fmt.Sprintf(":%d", port))

	if err != nil {
		log.Fatal(err)
	}

	defer listen.Close()

	grpcServer := grpc.NewServer()

	messagingServer := server.NewMessagingServer()
	messagingServer.StartLoop()

	pb.RegisterMessagingServerServer(grpcServer, &messagingServer)

	grpcServer.Serve(listen)
}
