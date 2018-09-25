using System;
using Chat.Grpc;
using Grpc.Core;

namespace Client {

	public partial class FrmLogin : Gtk.Window {

		public FrmLogin() : base(Gtk.WindowType.Toplevel) {
			
			this.Build();
            
		}

		protected AuthServer.AuthServerClient GetAuthServer(){

			var authServer = new AuthServer.AuthServerClient(
				new Channel("127.0.0.1", 50001, ChannelCredentials.Insecure)
			);

            return authServer;

		}

		protected void OnBtQuitClicked(object sender, EventArgs e) {

			Gtk.Application.Quit();

		}

		protected void OnBtSignUpClicked(object sender, EventArgs e) {

			var auth = this.GetAuthServer();

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

			var auth = this.GetAuthServer();

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

			MessageBox.ShowInfo(this, "OK!");

		}

	}

}
