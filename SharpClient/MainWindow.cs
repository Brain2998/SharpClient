using System;
using Gtk;
using System.Net;
using System.Text.RegularExpressions;
using SharpClient;

public partial class MainWindow : Gtk.Window
{
	private bool Connected = false;
	private Connection connection;

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
			messageLog.Buffer.Text += value;
		}
	}

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
		connection.CloseConnection("0|202");
        Application.Quit();
        a.RetVal = true;
    }

	public void ConnectionFormChange(bool connected)
	{
		Connected = connected;
		serverIp.IsEditable = !connected;
		Nickname.IsEditable = !connected;
		Connect.Label= connected ? "Disconnect" : "Connect";
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
			Regex ipCheck = new Regex(@"(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})");
			if (Username != "")
			{
				if (ipCheck.IsMatch(ipAddress))
				{
					connection = new Connection(IPAddress.Parse(ipAddress), this);
					connection.StartConnection();
					if (connection.Established)
					    ConnectionFormChange(true);
				}
				else
				{
					Messages = "Invalid Server IP.\n";
				}
			}
			else
			{
				Messages = "Invalid nickname.\n";
			}
		}
	}
}
