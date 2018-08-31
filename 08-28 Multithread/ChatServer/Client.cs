using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ChatServer {

    class Client {
        
        public string username { get; private set; }

        private TcpClient socket;        

        private Queue<byte[]> outputQueue = new Queue<byte[]>();
		private object outputQueueLock = new object();
        private ManualResetEvent outputQueueEvent = new ManualResetEvent(false);

        private bool reading = false;

        public delegate void NewMessageHandler(Client sender, string text);
        public event NewMessageHandler NewMessage;

        public delegate void SessionClosedHandler(Client sender);
        public event SessionClosedHandler SessionClosed;

        public Client(TcpClient socket){
            
            this.socket = socket;

        }
    
        public void BeginRead(){

            if(!reading){
            
                reading = true;

                Task.Run(new Action(ReadMessages));
				Task.Run(new Action(SendQueueMessages));

            }

        }

        private void ReadMessages(){

            var reader = new BinaryReader(socket.GetStream());

            byte[] messageBuffer;

            while(true){
                
                try {

                    messageBuffer = reader.ReadBytes(reader.ReadInt32());

                }catch(IOException){

                    Disconnect();

                    return;

                }

                var message = System.Text.Encoding.UTF8.GetString(messageBuffer);
				var lines = message.Split(new char[] { '\n' }, 2);
                                    
                this.username = lines[0];

                if(lines[1] != ""){
                    
                    NewMessage(this, lines[1]);

                }else{
                    
                    NewMessage(this, ">> ENTROU NA SALA");

                }

            }

        }

        public void SendMessage(Client sender, string text){

            if(socket == null) return;

            var message = sender.username + "\n" + text;

            lock(outputQueueLock){

                outputQueue.Enqueue(System.Text.Encoding.UTF8.GetBytes(message));

				outputQueueEvent.Set();

            }
            
        }

        private void SendQueueMessages(){

            var writer = new BinaryWriter(socket.GetStream());

			while (true) {

				outputQueueEvent.WaitOne();

				if (socket == null) break;

				byte[] message;

				lock (outputQueueLock) {

					if (outputQueue.Count == 0){

						outputQueueEvent.Reset();

						continue;

					}

					message = outputQueue.Dequeue();

				}

				try {

					writer.Write(message.Length);
					writer.Write(message);

				} catch (IOException) {

					Disconnect();

					break;

				}

			}

        }

        private void Disconnect(){

            try {                
                socket.Close();
            }catch{                
            }

            socket = null;

			outputQueueEvent.Set();

            if(this.username != ""){

                NewMessage(this, "<< SAIU DA SALA");

                SessionClosed(this);

            }

        }

    }

}