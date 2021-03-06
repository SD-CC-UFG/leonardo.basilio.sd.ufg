
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox1;

	private global::Gtk.HBox hbox1;

	private global::Gtk.Frame frame1;

	private global::Gtk.Alignment GtkAlignment;

	private global::Gtk.Entry txtUsuario;

	private global::Gtk.Label GtkLabel;

	private global::Gtk.Frame frame2;

	private global::Gtk.Alignment GtkAlignment1;

	private global::Gtk.Entry txtServidor;

	private global::Gtk.Label GtkLabel1;

	private global::Gtk.Frame frame6;

	private global::Gtk.Alignment GtkAlignment2;

	private global::Gtk.Button btConectar;

	private global::Gtk.Label GtkLabel3;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView txtLista;

	private global::Gtk.HBox hbox2;

	private global::Gtk.Entry txtMsg;

	private global::Gtk.Button btEnviar;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Chat");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		this.vbox1.BorderWidth = ((uint)(10));
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.frame1 = new global::Gtk.Frame();
		this.frame1.Name = "frame1";
		this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frame1.Gtk.Container+ContainerChild
		this.GtkAlignment = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment.Name = "GtkAlignment";
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		this.txtUsuario = new global::Gtk.Entry();
		this.txtUsuario.CanFocus = true;
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.Text = global::Mono.Unix.Catalog.GetString("cliente");
		this.txtUsuario.IsEditable = true;
		this.txtUsuario.InvisibleChar = '•';
		this.GtkAlignment.Add(this.txtUsuario);
		this.frame1.Add(this.GtkAlignment);
		this.GtkLabel = new global::Gtk.Label();
		this.GtkLabel.Name = "GtkLabel";
		this.GtkLabel.LabelProp = global::Mono.Unix.Catalog.GetString("Usuário:");
		this.GtkLabel.UseMarkup = true;
		this.frame1.LabelWidget = this.GtkLabel;
		this.hbox1.Add(this.frame1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.frame1]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.frame2 = new global::Gtk.Frame();
		this.frame2.Name = "frame2";
		this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frame2.Gtk.Container+ContainerChild
		this.GtkAlignment1 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment1.Name = "GtkAlignment1";
		// Container child GtkAlignment1.Gtk.Container+ContainerChild
		this.txtServidor = new global::Gtk.Entry();
		this.txtServidor.CanFocus = true;
		this.txtServidor.Name = "txtServidor";
		this.txtServidor.Text = global::Mono.Unix.Catalog.GetString("127.0.0.1");
		this.txtServidor.IsEditable = true;
		this.txtServidor.InvisibleChar = '•';
		this.GtkAlignment1.Add(this.txtServidor);
		this.frame2.Add(this.GtkAlignment1);
		this.GtkLabel1 = new global::Gtk.Label();
		this.GtkLabel1.Name = "GtkLabel1";
		this.GtkLabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Servidor:");
		this.GtkLabel1.UseMarkup = true;
		this.frame2.LabelWidget = this.GtkLabel1;
		this.hbox1.Add(this.frame2);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.frame2]));
		w6.Position = 1;
		w6.Expand = false;
		w6.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.frame6 = new global::Gtk.Frame();
		this.frame6.Name = "frame6";
		this.frame6.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frame6.Gtk.Container+ContainerChild
		this.GtkAlignment2 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment2.Name = "GtkAlignment2";
		// Container child GtkAlignment2.Gtk.Container+ContainerChild
		this.btConectar = new global::Gtk.Button();
		this.btConectar.CanFocus = true;
		this.btConectar.Name = "btConectar";
		this.btConectar.UseUnderline = true;
		this.btConectar.Label = global::Mono.Unix.Catalog.GetString("Conectar");
		this.GtkAlignment2.Add(this.btConectar);
		this.frame6.Add(this.GtkAlignment2);
		this.GtkLabel3 = new global::Gtk.Label();
		this.GtkLabel3.Name = "GtkLabel3";
		this.GtkLabel3.UseMarkup = true;
		this.frame6.LabelWidget = this.GtkLabel3;
		this.hbox1.Add(this.frame6);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.frame6]));
		w9.Position = 2;
		w9.Expand = false;
		w9.Fill = false;
		this.vbox1.Add(this.hbox1);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
		w10.Position = 0;
		w10.Expand = false;
		w10.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.txtLista = new global::Gtk.TextView();
		this.txtLista.CanFocus = true;
		this.txtLista.Name = "txtLista";
		this.txtLista.Editable = false;
		this.GtkScrolledWindow.Add(this.txtLista);
		this.vbox1.Add(this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
		w12.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox2 = new global::Gtk.HBox();
		this.hbox2.Name = "hbox2";
		this.hbox2.Spacing = 6;
		// Container child hbox2.Gtk.Box+BoxChild
		this.txtMsg = new global::Gtk.Entry();
		this.txtMsg.Sensitive = false;
		this.txtMsg.CanFocus = true;
		this.txtMsg.Name = "txtMsg";
		this.txtMsg.IsEditable = true;
		this.txtMsg.InvisibleChar = '•';
		this.hbox2.Add(this.txtMsg);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.txtMsg]));
		w13.Position = 0;
		// Container child hbox2.Gtk.Box+BoxChild
		this.btEnviar = new global::Gtk.Button();
		this.btEnviar.Sensitive = false;
		this.btEnviar.CanFocus = true;
		this.btEnviar.Name = "btEnviar";
		this.btEnviar.UseUnderline = true;
		this.btEnviar.Label = global::Mono.Unix.Catalog.GetString("Enviar");
		this.hbox2.Add(this.btEnviar);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.btEnviar]));
		w14.Position = 1;
		w14.Expand = false;
		w14.Fill = false;
		this.vbox1.Add(this.hbox2);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
		w15.Position = 2;
		w15.Expand = false;
		w15.Fill = false;
		this.Add(this.vbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 588;
		this.DefaultHeight = 395;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.btConectar.Clicked += new global::System.EventHandler(this.OnBtConectarClicked);
		this.txtMsg.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler(this.OnTxtMsgKeyReleaseEvent);
		this.btEnviar.Clicked += new global::System.EventHandler(this.OnBtEnviarClicked);
	}
}
