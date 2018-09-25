package server

import (
	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/NamingServer/grpc"
	"golang.org/x/net/context"

	"bufio"
	"errors"
	"os"
	"strconv"
	"sync"
)

type ServiceType pb.ServiceRequest_ServiceType

type service struct {
	ip   string
	port int32
}

// tipo que implementa a interface NamingServer do Grpc
type NamingServer struct {
	services         map[ServiceType][]service
	nextServiceIndex map[ServiceType]int
	mutex            *sync.Mutex
}

func NewNamingServer() (NamingServer, error) {
	namingServer := NamingServer{services: make(map[ServiceType][]service),
		nextServiceIndex: make(map[ServiceType]int), mutex: &sync.Mutex{}}

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

		idx := ServiceType(pb.ServiceRequest_ServiceType_value[serviceType])
		namingServer.services[idx] = append(namingServer.services[idx],
			service{ip: ip, port: int32(port)})
		namingServer.nextServiceIndex[idx] = 0
	}

	return namingServer, nil
}

func (s *NamingServer) GetServiceLocation(ctx context.Context, request *pb.ServiceRequest) (*pb.ServiceResponse, error) {
	t := ServiceType(request.Name)
	services, ok := s.services[t]

	if !ok {
		return nil, errors.New("Service unavailable.\n")
	}
	service := services[s.nextServiceIndex[t]]

	s.mutex.Lock()
	s.nextServiceIndex[t]++
	s.nextServiceIndex[t] %= len(s.services[t])
	s.mutex.Unlock()

	return &pb.ServiceResponse{Ip: service.ip, Port: service.port}, nil

	return nil, errors.New("Service do not exists.\n")
}
