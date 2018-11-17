using System;
using System.Threading;
using System.Threading.Tasks;
using Chat.Grpc;
using Grpc.Core;

namespace Client {

    public partial class FrmMain : Gtk.Window {

        private enum ClientStatus {
            Disconnected = 0,
            Connected = 1
        }

        private UserCredential credential;
        private ClientStatus currentStatus;

        private AsyncDuplexStreamingCall<ChatMessage, ChatMessage> streamingCall;

        //======================================
        //======================================

        private Gtk.TextTag textTagError = new Gtk.TextTag(null) {
            Weight = Pango.Weight.Bold,
            Foreground = "red"
        };

        private Gtk.TextTag textTagSender = new Gtk.TextTag(null) {
            Weight = Pango.Weight.Bold,
            Foreground = "blue"
        };

        //======================================
        //======================================

        public FrmMain(UserCredential credential) : base(Gtk.WindowType.Toplevel) {

            this.Build();

            this.credential = credential;

            this.Focus = txtMessage;

            lblUserName.Text = credential.UserName + ":";

            //============================

            this.DeleteEvent += (o, args) => Gtk.Application.Quit();

            //============================

            txtRoom.Buffer.TagTable.Add(textTagError);
            txtRoom.Buffer.TagTable.Add(textTagSender);

            //============================

            this.StartChat();

        }

        private void StartChat() {

            this.SetStatus(ClientStatus.Disconnected);

            Task.Run(async () => {

                var messaging = await Services.GetMessaging();

                streamingCall = messaging.TalkAndListen();

                Gtk.Application.Invoke((sender, e) => this.SetStatus(ClientStatus.Connected));

                var cancellationSource = new CancellationTokenSource();
                var stream = streamingCall.ResponseStream;

                while (await stream.MoveNext(cancellationSource.Token)) {

                    Gtk.Application.Invoke((sender, e) => this.ProcessMessage(stream.Current));

                }

            });

        }

        private void SendUserMessage() {

            var messageString = txtMessage.Text.Trim();

            txtMessage.Text = "";

            if (!this.ParseCommand(messageString)) {

                this.SendChatMessage(new ChatMessage() {
                    Type = ChatMessageType.Text,
                    Text = new TextMessage() {
                        Text = messageString
                    },
                    UserCredential = credential
                });

            }

        }

        private void SendChatMessage(ChatMessage message) {

            if (this.IsConnected()) {

                streamingCall.RequestStream.WriteAsync(message);

            }

        }

        private bool ParseCommand(string message) {

            if (message.Length > 0 && message[0] == '/') {

                var words = message.Split(' ');

                switch (words[0].ToLower()) {

                    case "/join":

                        if (words.Length != 2) {

                            RaiseUserError("Sintaxe do comando: /join <canal>");

                        } else if (words[1][0] != '#') {

                            RaiseUserError("Nomes de canais devem começar com #");

                        } else if (words[1].Length < 2) {

                            RaiseUserError("Canal inválido");

                        } else {

                            JoinChannel(words[1].ToLower());

                        }

                        break;

                    default:

                        if (message.Length > 1 && message[1] == '/') return false;

                        RaiseUserError("Comando desconhecido.");

                        break;

                }

                return true;

            }

            return message == "";

        }

        private void ProcessMessage(ChatMessage message) {

            var messageTime = message.DateTime.ToShortTimeString();

            var buffer = txtRoom.Buffer;
            var iter = buffer.EndIter;

            switch (message.Type) {

                case ChatMessageType.Error:

                    buffer.InsertWithTags(ref iter, messageTime + " - " + message.Error.Text + "\n\n", textTagError);

                    break;

                case ChatMessageType.Text:

                    buffer.InsertWithTags(ref iter, messageTime + " - " + message.UserCredential.UserName + "\n", textTagSender);
                    buffer.Insert(ref iter, message.Text.Text + "\n\n");

                    break;

                default:

                    return;

            }

            txtRoom.ScrollToMark(buffer.InsertMark, 0, true, 0, 1);

        }

        protected void OnBtSendMessageClicked(object sender, EventArgs e) {
            SendUserMessage();
        }

        protected void OnTxtMessageKeyReleaseEvent(object o, Gtk.KeyReleaseEventArgs args) {
            if (args.Event.Key == Gdk.Key.Return) SendUserMessage();
        }

        private void RaiseUserError(string message) {

            ProcessMessage(new ChatMessage() {
                Type = ChatMessageType.Error,
                Error = new ErrorMessage() {
                    Text = message
                }
            });

        }

        private void SetStatus(ClientStatus status) {

            var message = "";

            this.currentStatus = status;

            switch (status) {

                case ClientStatus.Disconnected:

                    message = "Conectando...";
                    break;

                default:

                    imgLoading.Visible = false;
                    lblStatus.Text = "";

                    return;

            }

            imgLoading.Visible = true;
            lblStatus.Text = message;

        }

        private bool IsConnected() {

            if (this.currentStatus < ClientStatus.Connected) {

                RaiseUserError("Você não está conectado.");

                return false;

            }

            return true;

        }

        private void JoinChannel(string channelName) {

            this.SendChatMessage(new ChatMessage() {
                Type = ChatMessageType.Control,
                UserCredential = this.credential,
                Topic = channelName,
                Control = new ControlMessage() {
                    Type = ControlMessageType.Joined
                }
            });

        }

    }

}
