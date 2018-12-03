using System;

namespace AuthServer {
	
    class Program {

        static void Main(string[] args) {

            const int serverPort = 50001;

			var server = new Grpc.Core.Server() {
				
				Services = {
                    Chat.Grpc.Auth.BindService(new AuthServer("172.17.0.2"))
				},

				Ports = {                   
                    new Grpc.Core.ServerPort("0.0.0.0", serverPort, Grpc.Core.ServerCredentials.Insecure)
				}

			};

			server.Start();

            //=================================

            var naming = new Chat.Grpc.Naming.NamingClient(new Grpc.Core.Channel("localhost:7777", Grpc.Core.ChannelCredentials.Insecure));

            if (naming.RegisterService(new Chat.Grpc.RegistrationRequest() {
                Health = 100,
                Name = Chat.Grpc.ServiceType.Auth,
                Port = serverPort
            }).Success) {

                var exitEvent = new System.Threading.ManualResetEvent(false);
                var pingTimer = new System.Threading.Timer((state) => {

                    try {

                        if(naming.Ping(new Chat.Grpc.PingRequest() {
                            Health = 100,
                            Name = Chat.Grpc.ServiceType.Auth,
                            Port = serverPort
                        }).Success){
                            return;
                        }

                    }catch(Grpc.Core.RpcException){
                    }

                    exitEvent.Set();

                }, null, 3000, 3000);

                //========================================

                Console.CancelKeyPress += (sender, e) => exitEvent.Set();

                Console.WriteLine("Serviço de autenticação em execução...");

                exitEvent.WaitOne();

            }else{

                Console.WriteLine("O registro no servidor de nomes falhou.");

            }

			server.ShutdownAsync().Wait();

        }

    }

}
