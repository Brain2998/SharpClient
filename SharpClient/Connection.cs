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
		private IPAddress ipAddress;
		private static MainWindow ChatForm;
		private Thread Messaging;
		public bool isConnected = false;

		public Connection(IPAddress address, MainWindow form)
        {
			ChatForm = form;
			ipAddress = address;
        }

        public void StartConnection()
		{
			tcpServer = new TcpClient();

            try
            {
				tcpServer.ConnectAsync(ipAddress, 1986).Wait(1000);
                clientWriter = new StreamWriter(tcpServer.GetStream());
                clientReader = new StreamReader(tcpServer.GetStream());
                clientWriter.WriteLine(ChatForm.Username);
                clientWriter.Flush();
				string serverResponse = clientReader.ReadLine();
                switch (serverResponse)
                {
                    case "0|100":
                        ChatForm.Messages = "Connected successfully!\n";
						isConnected = true;
						Messaging = new Thread(ReceiveMessages);
                        Messaging.Start();
                        break;
					case "0|102":
						ChatForm.Messages = "Invalid nickname.\n";
						break;
                    case "0|103":
                        ChatForm.Messages = "Nickname is already used.\n";
                        break;
					case "0|101":
                    default:
                        ChatForm.Messages = "Unknown registration error.\n";
                        break;
                }                
            }
			catch
            {
                ChatForm.Messages = "Can not connect to specified server.\n";
            }
		}

		private void ReceiveMessages()
        {
            string serverMessage;
            try
            {
                while (isConnected)
                {
					if (!tcpServer.GetStream().DataAvailable)
					{
						Thread.Sleep(200);
						continue;
					}
                    serverMessage = clientReader.ReadLine();
                    if (serverMessage.StartsWith("0|"))
                    {
                        CloseConnection(serverMessage);
                    }
                }
            }
            catch (Exception e)
            {
                ChatForm.Messages = "RecieveMessage: " + e.Message + "\n";
				if (isConnected)
				{
					CloseConnection("0|200");
				}
            }
        }
        
        public void CloseConnection(string Reason)
		{
			isConnected = false;
			try
			{
				switch (Reason)
				{
					case "0|201":
						ChatForm.Messages = "Server is down.\n";
						ChatForm.ConnectionFormChange(false);
						break;
					case "0|202":
						ChatForm.Messages = "Disconnected by user request.\n";
						clientWriter.WriteLine(Reason);
						clientWriter.Flush();
						break;
					case "0|200":
					default:
						ChatForm.Messages = "Unknown error during communication.\n";
						break;
				}
				isConnected = false;
				Messaging.Join();
				tcpServer.Close();
				clientReader.Close();
				clientWriter.Close();
			}
			catch (Exception e)
            {
				ChatForm.Messages = "CloseConnection: " + e.Message+ "\n";
            }
		}      
    }
}
