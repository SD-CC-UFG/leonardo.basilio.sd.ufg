using System;
using System.Threading.Tasks;
using Chat.Grpc;
using Grpc.Core;

namespace Client {

	public partial class FrmLogin : Gtk.Window {

		private bool mainWindowShown = false;

		public FrmLogin() : base(Gtk.WindowType.Toplevel) {

			this.Build();

			this.Destroyed += (o, args) => { if (!this.mainWindowShown) Gtk.Application.Quit(); };

		}

		protected void OnTxtKeyReleaseEvent(object o, Gtk.KeyReleaseEventArgs args) {

			if (args.Event.Key == Gdk.Key.Return) {

				OnBtSignInClicked(o, args);

			} else if (args.Event.Key == Gdk.Key.Escape) {

				this.Destroy();

			}

		}

		protected void OnBtQuitClicked(object sender, EventArgs e) {
			this.Destroy();
		}

		protected void OnBtSignUpClicked(object sender, EventArgs e) {

			this.ProcessLogin(async (auth) => {

				return await auth.SignUpAsync(new UserLogin() {
					UserName = txtUserName.Text,
					Password = txtPassword.Text
				});

			});

		}

		protected void OnBtSignInClicked(object sender, EventArgs e) {

			this.ProcessLogin(async (auth) => {

				return await auth.AuthenticateAsync(new UserLogin() {
					UserName = txtUserName.Text,
					Password = txtPassword.Text
				});

			});

		}

		private void ProcessLogin(Func<Auth.AuthClient, Task<UserCredential>> handler) {

			this.LockWindow(true);

			Task.Run(async () => {

				try {

					var auth = await Services.GetAuthentication();
					var credential = await handler(auth);

					if (credential.Error != "") {

						throw new ArgumentException(credential.Error);

					}

					Gtk.Application.Invoke((sender, e) => this.ShowMain(credential));

				} catch (Exception ex) {

					Gtk.Application.Invoke((sender, e) => {

						MessageBox.ShowError(this, ex);

						this.LockWindow(false);

					});

				}

			});

		}

		private void ShowMain(UserCredential credential) {

			var main = new FrmMain(credential);

			main.Show();

			this.mainWindowShown = true;

			this.Destroy();

		}

		private void LockWindow(bool flag) {

			FrForm.Sensitive = !flag;

		}

	}

}
