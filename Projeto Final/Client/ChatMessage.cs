using System;

namespace Chat.Grpc {

    public partial class ChatMessage {

        private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime DateTime {
            get {
                return UNIX_EPOCH.AddSeconds(this.Timestamp);
            }
        }

        partial void OnConstruction() {

            this.Timestamp = (uint)System.DateTime.Now.Subtract(UNIX_EPOCH).TotalSeconds;

        }

    }

}
