using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace SharpClient
{
    public class Connection
    {
		private TcpClient tcpServer;
        private StreamReader clientReader;
        private StreamWriter clientWriter;

		public Connection(IPAddress address, string Nickname)
        {
			tcpServer = new TcpClient();
			tcpServer.Connect(address, 1986);
			clientWriter = new StreamWriter(tcpServer.GetStream());
            clientWriter.WriteLine(Nickname);
            clientWriter.Flush();
        }
        public void CloseConnection()
		{
			tcpServer.Close();
		}
    }
}
