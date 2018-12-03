package main

import (
	"github.com/sd-cc-ufg/leonardo.basilio.sd.ufg/ProjetoFinal/MessagingServer/server"

	"os"
	"strconv"
)

func main() {
	// get the port from env var
	port, err := strconv.Atoi(os.Getenv("PORT"))
	if err != nil || port < 0 || port > 65000 {
		port = 8888
	}

	messagingServer := server.NewMessagingServer(port)
	messagingServer.Start()

}
