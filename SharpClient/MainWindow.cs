using System;
using Gtk;
using System.Net;
using SharpClient;

public partial class MainWindow : Gtk.Window
{
	public bool Connected = false;
    public Connection connection;
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

	public void ShowMessage(string message)
	{
        messageBox.PackStart(new TextMessage(message), false, false, 0);
        messageBox.ShowAll();
	}

    public void ShowSticker(string sticker)
    {
        messageBox.PackStart(new Sticker(PixStickers.Stickers[sticker]), false, false, 0);
        messageBox.ShowAll();
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
                    ShowMessage("Invalid Server IP.");
				}
			}
			else
			{
                ShowMessage("Invalid nickname.");
			}
		}
	}

    private void SendMessage()
	{
		if (Connected)
		{
			if (MessageToSend.Length > 0)
			{
				connection.SendMessage("1|"+MessageToSend);
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

    protected void OnSendStickerButtonClicked(object sender, EventArgs e)
    {
        try
        {
            StickerWindow stickerWindow = new StickerWindow("Select sticker", this);
        }
        catch (Exception err)
        {
            ShowMessage("StickerButton: " + err.Message+" "+err.InnerException);
        }
    }
}