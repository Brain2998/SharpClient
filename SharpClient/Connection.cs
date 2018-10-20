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
		public bool Established=false;

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
                        Established = true;
						Messaging = new Thread(ReceiveMessages);
                        Messaging.Start();
                        break;
					case "0|102":
						ChatForm.Messages = "Invalid nickname.\n";
						break;
                    case "0|103":
                        ChatForm.Messages = "Nickname is already used.\n";
                        break;
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
        
        public void CloseConnection(string Reason)
		{
			switch (Reason)
            {
				case "0|201":
                    ChatForm.Messages = "Server is down.\n";
                    ChatForm.ConnectionFormChange(false);
                    break;
                case "0|202":
                    ChatForm.Messages = "Disconnected by user request\n";
					clientWriter.WriteLine(Reason);
                    clientWriter.Flush();
                    break;
                default:
					ChatForm.Messages = "Unknown error during communication. Disconncted\n";
                    break;
            }
			Messaging.Abort();
			tcpServer.Close();         
			clientReader.Close();
			clientWriter.Close();
		}

		private void ReceiveMessages()
		{
			string serverMessage;

  		    //while (tcpServer.GetStream().DataAvailable)
			while (tcpServer.Connected)
			{
				if (tcpServer.GetStream().DataAvailable)
				{
					serverMessage = clientReader.ReadLine();
					if (serverMessage.StartsWith("0|"))
					{
						if (serverMessage == "0|201")
						{
							CloseConnection("0|201");
						}
					}
				}
			}
		}
    }
}
