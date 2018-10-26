using System;
using Gtk;
using System.Net;
using SharpClient;

public partial class MainWindow : Gtk.Window
{
	private bool Connected = false;
	private Connection connection;
	private IPAddress address;

	public string ipAddress
	{
		get
		{			
			return serverIp.Text;
		}
		set
		{
			serverIp.Text = value;
		}
	}

	public string Username
	{
		get
		{
			return Nickname.Text;
		}
		set
		{
			Nickname.Text = value;
		}
	}

	public string Messages
	{
		get
		{
			return messageLog.Buffer.Text;
		}
		set
		{
			messageLog.Buffer.Text += value+"\n";
		}
	}

    public string MessageToSend
	{
		get
		{
			return Message.Buffer.Text;
		}
		set
		{
			Message.Buffer.Text = value;
		}
	}

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
		if (Connected)
		{
			connection.CloseConnection("0|202");
		}
        Application.Quit();
        a.RetVal = true;
    }

	public void ConnectionFormChange(bool connected)
	{
		Connected = connected;
		serverIp.IsEditable = !connected;
		Nickname.IsEditable = !connected;      
		Connect.Label= connected ? "Disconnect" : "Connect";
		Message.Editable = connected;
	}

	protected void OnConnectClicked(object sender, EventArgs e)
	{
		if (Connected)
		{
			connection.CloseConnection("0|202");
			ConnectionFormChange(false);
		}
		else
		{
			if (Username != "")
			{
				if (IPAddress.TryParse(ipAddress, out address))
				{
					connection = new Connection(address, this);
					connection.StartConnection();
					if (connection.isConnected)
					    ConnectionFormChange(true);
				}
				else
				{
					Messages = "Invalid Server IP.";
				}
			}
			else
			{
				Messages = "Invalid nickname.";
			}
		}
	}

    private void SendMessage()
	{
		if (Connected)
		{
			if (MessageToSend.Length > 0)
			{
				connection.SendMessage(MessageToSend);
				MessageToSend = "";
			}
		}
	}

	protected void OnSendMessageButtonClicked(object sender, EventArgs e)
	{
		SendMessage();
	}   

	[GLib.ConnectBefore]
	protected void OnMessageKeyPressEvent(object o, KeyPressEventArgs args)
	{
		if (args.Event.Key==Gdk.Key.Return)
		{
			SendMessage();
		}
	}
}
