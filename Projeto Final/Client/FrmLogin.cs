using System;
using Chat.Grpc;

namespace Client {

	public partial class FrmLogin : Gtk.Window {

		public FrmLogin() : base(Gtk.WindowType.Toplevel) {
			
			this.Build();
            
		}
  
		protected void OnBtQuitClicked(object sender, EventArgs e) {

			Gtk.Application.Quit();

		}

		protected void OnBtSignUpClicked(object sender, EventArgs e) {

			var auth = Services.GetAuthentication();

			var credential = auth.SignUp(new UserLogin() {
                UserName = txtUserName.Text,
                Password = txtPassword.Text
			});
         
			if(credential.Error != ""){

				MessageBox.ShowError(this, credential.Error);

			}else{

				this.ShowMain(credential);

			}

		}

		protected void OnBtSignInClicked(object sender, EventArgs e) {

			var auth = Services.GetAuthentication();

			var credential = auth.Authenticate(new UserLogin() {
                UserName = txtUserName.Text,
                Password = txtPassword.Text
            });

            if (credential.Error != "") {

                MessageBox.ShowError(this, credential.Error);

            } else {

                this.ShowMain(credential);

            }

		}

		private void ShowMain(UserCredential credential){

			var main = new FrmMain(credential);

			main.Show();

			this.Destroy();

		}
      
		protected void OnTxtKeyReleaseEvent(object o, Gtk.KeyReleaseEventArgs args) {
			if (args.Event.Key == Gdk.Key.Return) OnBtSignUpClicked(o, args);
		}
  
	}

}
