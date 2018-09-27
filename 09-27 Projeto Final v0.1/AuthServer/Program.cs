using System;

namespace AuthServer {
	
    class Program {
		
        static void Main(string[] args) {
			
			var server = new Grpc.Core.Server() {
				
				Services = {
					Chat.Grpc.AuthServer.BindService(new AuthServer("172.17.0.2"))
				},

				Ports = {                   
                    new Grpc.Core.ServerPort("0.0.0.0", 50001, Grpc.Core.ServerCredentials.Insecure)
				}

			};

			server.Start();

			//=================================

			var exitEvent = new System.Threading.ManualResetEvent(false);

			Console.CancelKeyPress += (sender, e) => exitEvent.Set();

			exitEvent.WaitOne();

			server.ShutdownAsync().Wait();

        }

    }

}
