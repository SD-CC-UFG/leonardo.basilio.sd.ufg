using System;
using Gtk;

namespace Client {
	
    class MainClass {
		
        public static void Main(string[] args) {
			
            Application.Init();
            
			GLib.ExceptionManager.UnhandledException += HandleException;

			var win = new FrmLogin();
            win.Show();

            Application.Run();

        }

		private static void HandleException(UnhandledExceptionEventArgs ex){

			var targetException = ex.ExceptionObject as System.Reflection.TargetInvocationException;

			if (targetException != null) {
                
				MessageBox.ShowError(null, targetException.InnerException.Message);
				 
			} else {

				MessageBox.ShowError(null, ex.ExceptionObject.ToString());

			}

		}

    }

}
