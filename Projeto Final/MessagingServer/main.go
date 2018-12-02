package main

import (
	"fmt"
	"github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/server"
	"log"
	"math/rand"
	"net"
	"os"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
	"google.golang.org/grpc"
	"strconv"
)

func main() {
	// get the port from env var
	port, err := strconv.Atoi(os.Getenv("PORT"))
	if err != nil || port < 0 || port > 65000 {
		// choose a port random
		port = rand.Intn(65001)
	}

	listen, err := net.Listen("tcp", fmt.Sprintf(":%d", port))

	if err != nil {
		log.Fatal(err)
	}

	defer listen.Close()

	grpcServer := grpc.NewServer()

	messagingServer := server.NewMessagingServer(port)
	messagingServer.StartLoop()

	pb.RegisterMessagingServer(grpcServer, &messagingServer)

	log.Printf("Listening on port %v.\n", port)

	grpcServer.Serve(listen)
}
