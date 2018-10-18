using System;
using Gtk;
using System.IO;
using System.Net;
using System.Net.Sockets;

public partial class MainWindow : Gtk.Window
{
	private StreamWriter Writer;
	private StreamReader Reader;
	private TcpClient tcpServer;
	private IPAddress ipAddress;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

	protected void OnConnectClicked(object sender, EventArgs e)
	{
		ipAddress = IPAddress.Parse(serverIp.Text);
		tcpServer = new TcpClient();
		tcpServer.Connect(ipAddress, 1986);
		Writer = new StreamWriter(tcpServer.GetStream());
		Writer.WriteLine(Nickname.Text);
		Writer.Flush();
	}
}
