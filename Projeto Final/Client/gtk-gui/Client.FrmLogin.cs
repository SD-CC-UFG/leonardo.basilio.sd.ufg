
// This file has been generated by the GUI designer. Do not modify.
namespace Client
{
	public partial class FrmLogin
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.Frame FrForm;

		private global::Gtk.Alignment GtkAlignment;

		private global::Gtk.VBox vbox2;

		private global::Gtk.Label label1;

		private global::Gtk.Entry txtUserName;

		private global::Gtk.Label label2;

		private global::Gtk.Entry txtPassword;

		private global::Gtk.Button btSignIn;

		private global::Gtk.Button btSignUp;

		private global::Gtk.Button btQuit;

		private global::Gtk.Label GtkLabel3;

		private global::Gtk.Statusbar statusbar1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Client.FrmLogin
			this.WidthRequest = 250;
			this.Name = "Client.FrmLogin";
			this.Title = global::Mono.Unix.Catalog.GetString("Login");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.Resizable = false;
			// Container child Client.FrmLogin.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.FrForm = new global::Gtk.Frame();
			this.FrForm.Name = "FrForm";
			this.FrForm.ShadowType = ((global::Gtk.ShadowType)(2));
			this.FrForm.BorderWidth = ((uint)(10));
			// Container child FrForm.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			this.GtkAlignment.TopPadding = ((uint)(12));
			this.GtkAlignment.RightPadding = ((uint)(12));
			this.GtkAlignment.BottomPadding = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Usuário:");
			this.vbox2.Add(this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.txtUserName = new global::Gtk.Entry();
			this.txtUserName.CanFocus = true;
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.IsEditable = true;
			this.txtUserName.InvisibleChar = '•';
			this.vbox2.Add(this.txtUserName);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.txtUserName]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 0F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Senha:");
			this.vbox2.Add(this.label2);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.label2]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.txtPassword = new global::Gtk.Entry();
			this.txtPassword.CanFocus = true;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.IsEditable = true;
			this.txtPassword.Visibility = false;
			this.txtPassword.InvisibleChar = '•';
			this.vbox2.Add(this.txtPassword);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.txtPassword]));
			w4.Position = 3;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.btSignIn = new global::Gtk.Button();
			this.btSignIn.CanDefault = true;
			this.btSignIn.CanFocus = true;
			this.btSignIn.Name = "btSignIn";
			this.btSignIn.UseUnderline = true;
			this.btSignIn.Label = global::Mono.Unix.Catalog.GetString("Entrar");
			this.vbox2.Add(this.btSignIn);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.btSignIn]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.btSignUp = new global::Gtk.Button();
			this.btSignUp.CanFocus = true;
			this.btSignUp.Name = "btSignUp";
			this.btSignUp.UseUnderline = true;
			this.btSignUp.Label = global::Mono.Unix.Catalog.GetString("Cadastrar-se");
			this.vbox2.Add(this.btSignUp);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.btSignUp]));
			w6.Position = 5;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.btQuit = new global::Gtk.Button();
			this.btQuit.CanFocus = true;
			this.btQuit.Name = "btQuit";
			this.btQuit.UseUnderline = true;
			this.btQuit.Label = global::Mono.Unix.Catalog.GetString("Sair");
			this.vbox2.Add(this.btQuit);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.btQuit]));
			w7.Position = 6;
			w7.Expand = false;
			w7.Fill = false;
			this.GtkAlignment.Add(this.vbox2);
			this.FrForm.Add(this.GtkAlignment);
			this.GtkLabel3 = new global::Gtk.Label();
			this.GtkLabel3.Name = "GtkLabel3";
			this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Login</b>");
			this.GtkLabel3.UseMarkup = true;
			this.FrForm.LabelWidget = this.GtkLabel3;
			this.vbox1.Add(this.FrForm);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.FrForm]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.statusbar1 = new global::Gtk.Statusbar();
			this.statusbar1.Name = "statusbar1";
			this.statusbar1.Spacing = 6;
			this.vbox1.Add(this.statusbar1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 300;
			this.DefaultHeight = 301;
			this.Show();
			this.txtUserName.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler(this.OnTxtKeyReleaseEvent);
			this.txtPassword.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler(this.OnTxtKeyReleaseEvent);
			this.btSignIn.Clicked += new global::System.EventHandler(this.OnBtSignInClicked);
			this.btSignUp.Clicked += new global::System.EventHandler(this.OnBtSignUpClicked);
			this.btQuit.Clicked += new global::System.EventHandler(this.OnBtQuitClicked);
		}
	}
}
