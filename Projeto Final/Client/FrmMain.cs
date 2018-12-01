using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        private Dictionary<string, ChannelItem> channelsMap;
        private List<ChannelItem> channelsList;
        private ChannelItem currentChannel;
        private int currentChannelIndex = -1;
        private bool changingChannel = false;

        private AsyncDuplexStreamingCall<ChatMessage, ChatMessage> streamingCall;

        public FrmMain(UserCredential credential) : base(Gtk.WindowType.Toplevel) {

            this.Build();

            this.credential = credential;
            this.channelsMap = new Dictionary<string, ChannelItem>();
            this.channelsList = new List<ChannelItem>();

            this.Focus = txtMessage;

            lblUserName.Text = credential.UserName + ":";

            //============================

            this.DeleteEvent += (o, args) => Gtk.Application.Quit();

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

                    case "/quit":

                        if (words.Length != 1) {

                            RaiseUserError("/quit não possui parâmetros");

                        } else if (this.currentChannel == null) {

                            RaiseUserError("Você não entrou em nenhum canal");

                        } else {

                            QuitChannel(this.currentChannel.Topic);

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

            if (message.Type == ChatMessageType.Control) {

                switch (message.Control.Type) {

                    case ControlMessageType.Joined:

                        if (message.UserCredential.UserName == this.credential.UserName &&
                            !channelsMap.ContainsKey(message.Topic)) {

                            var item = new ChannelItem(message.Topic);

                            item.ButtonToggled += ChannelItem_ButtonToggled;

                            channelsMap.Add(message.Topic, item);
                            channelsList.Add(item);

                            channelsBox.PackEnd(item.Button);
                            channelsNotebook.AppendPage(item.Page, null);

                            this.GoToChannel(message.Topic);

                        }

                        break;

                    case ControlMessageType.Quitted:

                        if (message.UserCredential.UserName == this.credential.UserName &&
                            channelsMap.ContainsKey(message.Topic)) {

                            var item = channelsMap[message.Topic];

                            channelsMap.Remove(item.Topic);
                            channelsList.Remove(item);

                            channelsBox.Remove(item.Button);
                            channelsNotebook.RemovePage(channelsNotebook.CurrentPage);

                            if (item == this.currentChannel) {

                                this.currentChannel = null;

                                this.GoToChannel(this.currentChannelIndex - 1);

                            } else {

                                this.currentChannelIndex = channelsList.IndexOf(this.currentChannel);

                            }

                            return;

                        }

                        break;

                }

            }

            if (message.Topic == "") {

                this.currentChannel.AppendMessage(message);

            } else if (this.channelsMap.ContainsKey(message.Topic)) {

                this.channelsMap[message.Topic].AppendMessage(message);

            }

        }

        protected void OnBtSendMessageClicked(object sender, EventArgs e) {

            SendUserMessage();

        }

        protected void OnTxtMessageKeyReleaseEvent(object o, Gtk.KeyReleaseEventArgs args) {

            if (args.Event.Key == Gdk.Key.Return) SendUserMessage();

        }

        protected void OnTxtMessageKeyPressEvent(object o, Gtk.KeyPressEventArgs args) {

            if ((args.Event.Key == Gdk.Key.Tab || args.Event.Key == Gdk.Key.ISO_Left_Tab) &&
                args.Event.State.HasFlag(Gdk.ModifierType.ControlMask) &&
                this.currentChannel != null) {

                if (args.Event.Key == Gdk.Key.ISO_Left_Tab) {

                    this.GoToChannel(this.currentChannelIndex - 1, true);

                } else {

                    this.GoToChannel(this.currentChannelIndex + 1, true);

                }

                args.RetVal = true;

            }

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

            if (channelsMap.ContainsKey(channelName)) {

                this.GoToChannel(channelName);

            } else {

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

        private void QuitChannel(string channelName) {

            if (channelsMap.ContainsKey(channelName)) {

                this.SendChatMessage(new ChatMessage() {
                    Type = ChatMessageType.Control,
                    UserCredential = this.credential,
                    Topic = channelName,
                    Control = new ControlMessage() {
                        Type = ControlMessageType.Quitted
                    }
                });

            }

        }

        void ChannelItem_ButtonToggled(object sender, EventArgs e) {

            GoToChannel((sender as ChannelItem).Topic);

        }

        private void GoToChannel(int index, bool circular = false) {

            if (index < 0) {

                index = circular ? channelsList.Count - 1 : 0;

            } else if (index >= channelsList.Count) {

                index = circular ? 0 : channelsList.Count - 1;

            }

            if (index >= 0 && index < channelsList.Count) {

                this.GoToChannel(channelsList[index]);

            } else {

                this.GoToChannel(null as ChannelItem);

            }

        }

        private void GoToChannel(string topic) {

            this.GoToChannel(this.channelsMap[topic]);

        }

        private void GoToChannel(ChannelItem newChannel) {

            if (!this.changingChannel) {

                this.changingChannel = true;

                if (this.currentChannel != null) {

                    this.currentChannel.Button.Active = false;

                }

                if (newChannel != null) {

                    newChannel.Button.Active = true;

                    channelsNotebook.CurrentPage = Array.IndexOf(channelsNotebook.Children, newChannel.Page);

                    this.currentChannelIndex = channelsNotebook.CurrentPage - 1;

                } else {

                    channelsNotebook.CurrentPage = 0;

                    this.currentChannelIndex = -1;

                }

                this.currentChannel = newChannel;

                this.changingChannel = false;

                this.Focus = txtMessage;

            }

        }

    }

}
