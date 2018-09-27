using System;

namespace Client {
	
    internal static class MessageBox {
        
		public static void ShowError(Gtk.Window parent, string message){

			var dialog = new Gtk.MessageDialog(parent, 
			                                   Gtk.DialogFlags.Modal, 
			                                   Gtk.MessageType.Error, 
			                                   Gtk.ButtonsType.Ok, 
			                                   message);

			dialog.Run();

			dialog.Destroy();

		}

		public static void ShowInfo(Gtk.Window parent, string message) {

            var dialog = new Gtk.MessageDialog(parent,
                                               Gtk.DialogFlags.Modal,
                                               Gtk.MessageType.Info,
                                               Gtk.ButtonsType.Ok,
                                               message);

            dialog.Run();

            dialog.Destroy();

        }

    }

}
