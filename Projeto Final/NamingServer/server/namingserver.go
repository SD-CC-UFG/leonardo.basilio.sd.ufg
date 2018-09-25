package server

import (
	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/Chat_Grpc"
	"golang.org/x/net/context"

	"bufio"
	"errors"
	"log"
	"os"
	"strconv"
)

type service struct {
	ip   string
	port int32
	t    string
}

// tipo que implementa a interface NamingServer do Grpc
type NamingServer struct {
	services         map[string][]service
	nextServiceIndex map[string]int
}

func NewNamingServer() (NamingServer, error) {
	namingServer := NamingServer{services: make(map[string][]service),
		nextServiceIndex: make(map[string]int)}

	servicesFile, err := os.Open("services.conf")

	if err != nil {
		return NamingServer{}, err
	}

	defer servicesFile.Close()

	scanner := bufio.NewScanner(servicesFile)
	scanner.Split(bufio.ScanWords)

	for scanner.Scan() {
		serviceType := scanner.Text()

		scanner.Scan()
		ip := scanner.Text()

		scanner.Scan()
		port, err := strconv.Atoi(scanner.Text())

		if err != nil {
			return NamingServer{}, err
		}

		namingServer.services[serviceType] = append(namingServer.services[serviceType],
			service{ip: ip, port: int32(port), t: serviceType})
		namingServer.nextServiceIndex[serviceType] = 0
	}

	return namingServer, nil
}

func (s *NamingServer) GetServiceLocation(ctx context.Context, request *pb.ServiceRequest) (*pb.ServiceResponse, error) {
	var t string

	switch request.Name {
	case pb.ServiceRequest_AUTH:
		log.Printf("Authentication service required.\n")
		t = "A"

	case pb.ServiceRequest_MESSAGING:
		log.Printf("Chat Messaging service required.\n")
		t = "M"

	default:
		return nil, errors.New("Service do not exists.\n")
	}

	services, ok := s.services[t]
	if !ok {
		return nil, errors.New("Service unavailable.\n")
	}
	service := services[s.nextServiceIndex[t]]
	s.nextServiceIndex[t]++
	s.nextServiceIndex[t] %= len(s.services[t])

	return &pb.ServiceResponse{Ip: service.ip, Port: service.port}, nil

	return nil, errors.New("Service do not exists.\n")
}
