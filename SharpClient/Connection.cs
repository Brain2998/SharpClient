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
		private static MainWindow ChatForm;
		private Thread Messaging;
		public bool Established=false;

		public Connection(IPAddress address, MainWindow form)
        {
			ChatForm = form;
			tcpServer = new TcpClient();
            try         
			{
				tcpServer.ConnectAsync(address, 1986).Wait(1000);
				clientWriter = new StreamWriter(tcpServer.GetStream());
				clientReader = new StreamReader(tcpServer.GetStream());
				clientWriter.WriteLine(ChatForm.Username);
				clientWriter.Flush();
				//Waiting for registration response
				while (!tcpServer.GetStream().DataAvailable)
					Thread.Sleep(50);
				string Response = clientReader.ReadLine();
				switch (Response)
				{
					case "0":
						ChatForm.Messages = "Connected successfully!\n";
						Established = true;
						break;
					case "2":
						ChatForm.Messages = "Nickname is already used.\n";
						break;
					default:
						ChatForm.Messages = "Connection error.\n";
						break;
				}
				Messaging = new Thread(ReceiveMessages);
				Messaging.Start();
			}
            catch
			{
				ChatForm.Messages = "Can not connect to specified server.\n";
			}
        }
        
        public void CloseConnection()
		{
			clientWriter.WriteLine("3");
			clientWriter.Flush();
			tcpServer.Close();
			ChatForm.Messages = "Disconnected by user request\n";
			clientReader.Close();
			clientWriter.Close();
			Messaging.Abort();
		}

		private void ReceiveMessages()
		{
			string Message;
			while (tcpServer.Connected)
			{
				if (tcpServer.GetStream().DataAvailable)
				{
					Message=clientReader.ReadLine();
					if (Message=="4")
					{
						ChatForm.Messages = "Server is down";
					}
				}
			}
		}
    }
}
