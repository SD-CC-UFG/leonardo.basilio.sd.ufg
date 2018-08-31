using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gtk;

public partial class MainWindow : Gtk.Window {

    private TcpClient socket;

	private Queue<byte[]> outputQueue = new Queue<byte[]>();
    private object outputQueueLock = new object();
	private ManualResetEvent outputQueueEvent = new ManualResetEvent(false);

	public MainWindow() : base(Gtk.WindowType.Toplevel) {
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnBtConectarClicked(object sender, EventArgs e) {

		if(socket == null){          

            if(txtUsuario.Text.Trim() == ""){

				MostrarErro("Informe o nome do usuário.");

			}else if(txtServidor.Text.Trim() == ""){

				MostrarErro("Informe o endereço do servidor.");

			}else{

				Conectar();

			}

		}else{

			Desconectar();

		}


	}

	protected void OnBtEnviarClicked(object sender, EventArgs e) {
		EnviarMensagem();
	}

	protected void OnTxtMsgKeyReleaseEvent(object o, KeyReleaseEventArgs args) {
		if(args.Event.Key == Gdk.Key.Return) EnviarMensagem();
	}

	private void EnviarMensagem() {

		if (txtMsg.Text.Trim() != "") {

			EnviarMensagem(txtMsg.Text);

			txtMsg.Text = "";

		}

	}

	private void EnviarMensagem(string mensagem){

		if (socket == null) return;

		var message = txtUsuario.Text + "\n" + mensagem;       

        lock (outputQueueLock) {

            outputQueue.Enqueue(System.Text.Encoding.UTF8.GetBytes(message));

			outputQueueEvent.Set();

        }

    }

	private void EnviarMensagensNaFila() {

        var writer = new BinaryWriter(socket.GetStream());

        while (true) {

			outputQueueEvent.WaitOne();

			if (socket == null) break;

            byte[] message;

            lock (outputQueueLock) {

				if (outputQueue.Count == 0) {

					outputQueueEvent.Reset();

					continue;

				}

                message = outputQueue.Dequeue();

            }

            try {

                writer.Write(message.Length);
                writer.Write(message);

            } catch (IOException) {

				Desconectar();
               
                break;

            }
   
		}

    }

	private void LerMensagens() {

        var reader = new BinaryReader(socket.GetStream());

        byte[] messageBuffer;

        while (true) {

            try {

                messageBuffer = reader.ReadBytes(reader.ReadInt32());

            } catch (IOException) {

				Desconectar();

                return;

            }

            var message = System.Text.Encoding.UTF8.GetString(messageBuffer);
			var lines = message.Split(new char[] { '\n' }, 2);

			Application.Invoke((object sender, EventArgs e) => {

				var buffer = txtLista.Buffer;
                var iter = buffer.EndIter;

				var tag = new TextTag(null);
				tag.Weight = Pango.Weight.Bold;
				tag.Foreground = "blue";

				buffer.TagTable.Add(tag);
				buffer.InsertWithTags(ref iter, DateTime.Now.ToShortTimeString() + " - " + lines[0] + "\n", tag);
				buffer.Insert(ref iter, lines[1] + "\n\n");

				txtLista.ScrollToMark(buffer.InsertMark, 0, true, 0, 1);

			});

        }

    }

	private void Conectar(){
		
		socket = new TcpClient();

        try {

            socket.Connect(txtServidor.Text, 8888);

        } catch (SocketException ex) {

			MostrarErro(ex);

            return;

        }

        txtUsuario.Sensitive = false;
        txtServidor.Sensitive = false;

        txtMsg.Sensitive = true;
        btEnviar.Sensitive = true;

        btConectar.Label = "Desconectar";

		outputQueueEvent.Reset();

		Task.Run(new System.Action(LerMensagens));
		Task.Run(new System.Action(EnviarMensagensNaFila));

		EnviarMensagem("");

	}

	private void Desconectar(){

		if (socket != null) {
         
            lock (outputQueueLock) {
                outputQueue.Clear();
            }

			try {
				socket.Close();
			} catch {
			}

			socket = null;

			outputQueueEvent.Set();

			Application.Invoke((object sender, EventArgs e) => {

				txtUsuario.Sensitive = true;
				txtServidor.Sensitive = true;

				txtMsg.Sensitive = false;
				btEnviar.Sensitive = false;

				btConectar.Label = "Conectar";

			});

		}

	}

	private void MostrarErro(Exception ex) {
		MostrarErro(ex.Message);
	}

	private void MostrarErro(string mensagem){

		var msg = new MessageDialog(this, 0, MessageType.Error, ButtonsType.Ok, mensagem);

        msg.Run();

        msg.Destroy();

	}

}
