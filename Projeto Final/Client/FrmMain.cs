using System;
using System.Threading;
using System.Threading.Tasks;
using Chat.Grpc;
using Grpc.Core;

namespace Client {

    public partial class FrmMain : Gtk.Window {

		private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private UserCredential credential;

		private AsyncDuplexStreamingCall<ChatMessage, ChatMessage> streamingCall;
        
        public FrmMain(UserCredential credential) : base(Gtk.WindowType.Toplevel) {
			
            this.Build();

			this.credential = credential;           

			this.Focus = txtMessage;
            
			lblUserName.Text = credential.UserName + ":";

            //============================

			this.DeleteEvent += (o, args) => Gtk.Application.Quit();

			//============================

			this.StartChat();

        }

		private void StartChat(){
        
			Task.Run(async () => {

				var messaging = await Services.GetMessaging();

                streamingCall = messaging.TalkAndListen();

				var cancellationSource = new CancellationTokenSource();
				var stream = streamingCall.ResponseStream;

                while(await stream.MoveNext(cancellationSource.Token)){

					ProcessIncomingMessage(stream.Current);

				}

			});

		}

		private void SendMessage() {

            var messageString = txtMessage.Text.Trim();

            if (messageString != "") {

				txtMessage.Text = "";

				streamingCall.RequestStream.WriteAsync(new ChatMessage() {
					DateTime = (uint) DateTime.Now.Subtract(UNIX_EPOCH).TotalSeconds,
                    Type = ChatMessage.Types.ChatMessageType.Text,
					Text = new TextMessage(){
						Text = messageString
					},
                    UserCredential = credential
				}).Wait();

            }

        }

		private void ProcessIncomingMessage(ChatMessage message){

			var messageDateTime = UNIX_EPOCH.AddSeconds(message.DateTime);

			Gtk.Application.Invoke((object sender, EventArgs e) => {

                var buffer = txtRoom.Buffer;
                var iter = buffer.EndIter;

                var tag = new Gtk.TextTag(null);
                tag.Weight = Pango.Weight.Bold;
                tag.Foreground = "blue";

                buffer.TagTable.Add(tag);
                buffer.InsertWithTags(ref iter, messageDateTime.ToShortTimeString() + " - " + message.UserCredential.UserName + "\n", tag);
				buffer.Insert(ref iter, message.Text.Text + "\n\n");

				txtRoom.ScrollToMark(buffer.InsertMark, 0, true, 0, 1);

            });
			
		}

		protected void OnBtSendMessageClicked(object sender, EventArgs e) {
			SendMessage();
		}

		protected void OnTxtMessageKeyReleaseEvent(object o, Gtk.KeyReleaseEventArgs args) {
			if (args.Event.Key == Gdk.Key.Return) SendMessage();
		}

	}

}
