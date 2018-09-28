using System;
using Gtk;

namespace Client {
	
    class MainClass {
		
        public static void Main(string[] args) {
			
            Application.Init();
            
			GLib.ExceptionManager.UnhandledException += (ex) => MessageBox.ShowError(null, ex.ExceptionObject as Exception);

			var win = new FrmLogin();
            win.Show();

            Application.Run();

        }

    }

}
