using System;
using Chat.Grpc;

namespace Client {

    [System.ComponentModel.ToolboxItem(true)]
    public partial class ChannelPage : Gtk.Bin {

        private Gtk.TextTag textTagError = new Gtk.TextTag(null) {
            Weight = Pango.Weight.Bold,
            Foreground = "red"
        };

        private Gtk.TextTag textTagSender = new Gtk.TextTag(null) {
            Weight = Pango.Weight.Bold,
            Foreground = "blue"
        };

        private Gtk.TextTag textTagInfo = new Gtk.TextTag(null) {
            Weight = Pango.Weight.Bold,
            Foreground = "green"
        };

        //========================================

        public ChannelPage() {

            this.Build();

            txtRoom.Buffer.TagTable.Add(textTagError);
            txtRoom.Buffer.TagTable.Add(textTagSender);
            txtRoom.Buffer.TagTable.Add(textTagInfo);

        }

        public void AppendMessage(ChatMessage message) {

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

                case ChatMessageType.Control:

                    switch (message.Control.Type) {

                        case ControlMessageType.Joined:

                            buffer.InsertWithTags(ref iter, messageTime + " - " + message.UserCredential.UserName + " entrou no canal " + message.Topic + "\n\n", textTagInfo);

                            break;

                        case ControlMessageType.Quitted:

                            buffer.InsertWithTags(ref iter, messageTime + " - " + message.UserCredential.UserName + " saiu do canal " + message.Topic + "\n\n", textTagInfo);

                            break;

                    }

                    break;

                default:

                    return;

            }

            txtRoom.ScrollToMark(buffer.InsertMark, 0, true, 0, 1);

        }

    }

}
