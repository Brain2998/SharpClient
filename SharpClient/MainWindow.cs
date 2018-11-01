using System;
using Gtk;
using System.Net;
using SharpClient;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public partial class MainWindow : Gtk.Window
{
    public bool Connected = false;
    public Connection connection;
    private IPAddress address;
    private Adjustment adjustment = new Adjustment(0xffffffff, 0, 0xffffffff, 0, 0, 0);
    private Regex messageRegex = new Regex("[\t\r\n]");

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



    public async void ShowMessage(string message)
    {
        messageBox.PackStart(new TextMessage(message), false, false, 0);
        messageBox.ShowAll();
        await PutTaskDelay();
        ScrollDown();
    }

    public async void ShowSticker(string sticker)
    {
        messageBox.PackStart(new Sticker(PixStickers.Stickers[sticker]), false, false, 0);
        messageBox.ShowAll();
        await PutTaskDelay();
        ScrollDown();
    }

    private void ScrollDown()
    {
        Adjustment chatAdjustment = chatWindow.Vadjustment;
        chatWindow.Vadjustment.Value = chatAdjustment.Upper - chatAdjustment.PageSize;
    }

    async Task PutTaskDelay()
    {
        await Task.Delay(100);
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
        GLib.ExceptionManager.UnhandledException += logException;
    }

    void logException(GLib.UnhandledExceptionArgs args)
    {
        args.ExitApplication = false;
        ShowMessage("GlobalException: " + args.ExceptionObject.ToString());
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
		if (Connected)
		{
			connection.CloseConnection("0|202");
		}
        Environment.Exit(0);
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
                if (MessageToSend.Length < 300)
                {
                    connection.SendMessage("1|" + messageRegex.Replace(MessageToSend, " "));
                }
                else 
                {
                    ShowMessage("Message too long.");
                }
			}
		}
	}

	protected void OnSendMessageButtonClicked(object sender, EventArgs e)
	{
		SendMessage();
        MessageToSend = "";
    }   

	[GLib.ConnectBefore]
	protected void OnMessageKeyPressEvent(object o, KeyPressEventArgs args)
	{
		if (args.Event.Key==Gdk.Key.Return || args.Event.Key == Gdk.Key.KP_Enter)
		{
			SendMessage();
		}
	}

    protected void OnMessageKeyReleaseEvent(object o, KeyReleaseEventArgs args)
    {
        if (args.Event.Key == Gdk.Key.Return || args.Event.Key==Gdk.Key.KP_Enter)
        {
            MessageToSend = "";
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