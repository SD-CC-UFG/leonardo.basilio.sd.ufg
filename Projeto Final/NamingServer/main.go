package main

import (
	"fmt"
	"github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/server"
	"log"
	"net"

	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/Chat_Grpc"
	"google.golang.org/grpc"
)

const (
	port = 7777
)

func main() {
	listen, err := net.Listen("tcp", fmt.Sprintf(":%d", port))
	if err != nil {
		log.Fatalf("Falha na abertura de servidor TCP.\n")
	}

	namingServer, err := server.NewNamingServer()

	if err != nil {
		log.Fatal(err)
	}

	grpcServer := grpc.NewServer()
	pb.RegisterNamingServerServer(grpcServer, &namingServer)
	grpcServer.Serve(listen)
}
