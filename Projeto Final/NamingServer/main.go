package main

import (
	"fmt"
	"github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/server"
	"log"
	"net"
	"os"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/grpc"
	"google.golang.org/grpc"
)

func main() {
	// get port from envvar
	port := os.Getenv("PORT")
	if port == "" {
		port = "7777"
	}

	listen, err := net.Listen("tcp", fmt.Sprintf(":%v", port))
	if err != nil {
		log.Fatalf("Falha na abertura de servidor TCP: %v.\n", err)
	}

	defer listen.Close()

	log.Printf("Listening on port %v.\n", port)

	namingServer, err := server.NewNamingServer()

	if err != nil {
		log.Fatal(err)
	}

	grpcServer := grpc.NewServer()
	pb.RegisterNamingServerServer(grpcServer, &namingServer)
	grpcServer.Serve(listen)
}
