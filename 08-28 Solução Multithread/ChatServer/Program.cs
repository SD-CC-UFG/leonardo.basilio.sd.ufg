using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ChatServer {
    
    class Program {

        static LinkedList<Client> clientList = new LinkedList<Client>();
        static Object clientListLock = new Object();

        static void Main(string[] args){
            
            var server = new TcpListener(IPAddress.Any, 8888);

			server.Start();

            while(true){

                var client = new Client(server.AcceptTcpClient());

                lock(clientListLock){

                    clientList.AddLast(client);

                }

                client.NewMessage += HandleNewMessage;
                client.SessionClosed += HandleSessionClosed;

                client.BeginRead();

            }

        }

        static void HandleNewMessage(Client sender, string text){
			
            lock(clientListLock){
				
                foreach(Client client in clientList){

                    client.SendMessage(sender, text);

                }

            }

        }

        static void HandleSessionClosed(Client sender){

            lock(clientListLock){
				
                clientList.Remove(sender);

            }

        }

    }

}
