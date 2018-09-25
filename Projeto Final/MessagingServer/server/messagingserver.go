package server

import (
	pb "github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/grpc"
)

type MessagingServer struct {
}

func NewMessagingServer() MessagingServer {
	return MessagingServer{}
}

func (s *MessagingServer) TalkAndListen(stream pb.MessagingServer_TalkAndListenServer) error {
	return nil
}
