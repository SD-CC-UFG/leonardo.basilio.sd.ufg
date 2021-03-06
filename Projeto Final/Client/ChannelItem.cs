﻿using Gtk;
using System;
using Chat.Grpc;

namespace Client {

    public class ChannelItem {

        public ChannelPage Page { get; }
        public Gtk.ToggleButton Button { get; }
        public string Topic { get; }

        public event EventHandler ButtonToggled;

        public ChannelItem(string topic) {

            this.Topic = topic;
            this.Page = new ChannelPage();
            this.Button = new Gtk.ToggleButton(topic);

            this.Button.Toggled += (sender, e) => ButtonToggled(this, e);

            this.Page.ShowAll();
            this.Button.ShowAll();

        }

        public void SetUnread(bool flag){

            var color = flag ? new Gdk.Color(0, 0, 255) : this.Button.Child.Style.Black;

            this.Button.Child.ModifyFg(StateType.Normal, color);
            this.Button.Child.ModifyFg(StateType.Prelight, color);

        }

        public void AppendMessage(ChatMessage message) {
            this.Page.AppendMessage(message);
        }

    }

}
