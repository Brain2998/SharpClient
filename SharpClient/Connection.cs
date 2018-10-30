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
				SendMessage("0|"+ChatForm.Username);
				string serverResponse = clientReader.ReadLine();
                switch (serverResponse)
                {
                    case "0|100":
                        ChatForm.ShowMessage("Connected successfully!");
						isConnected = true;
						Messaging = new Thread(ReceiveMessages);
                        Messaging.Start();
                        break;
					case "0|102":
                        ChatForm.ShowMessage("Invalid nickname.");
						break;
                    case "0|103":
                        ChatForm.ShowMessage("Nickname is already used.");
                        break;
					case "0|101":
                    default:
                        ChatForm.ShowMessage("Unknown registration error.");
                        break;
                }                
            }
			catch
            {
                ChatForm.ShowMessage("Can not connect to specified server.");
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
                    switch (serverMessage.Substring(0, 2))
                    {
                        case "0|":
                            CloseConnection(serverMessage);
                            break;
                        case "1|":
                            int stickerIndex = serverMessage.IndexOf("2|");
                            if (stickerIndex>0)
                            {
                                ChatForm.ShowMessage(serverMessage.Substring(2, stickerIndex-2));
                                ChatForm.ShowSticker(serverMessage.Substring(stickerIndex + 2));
                            }
                            else
                            {
                                ChatForm.ShowMessage(serverMessage.Substring(2));
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ChatForm.ShowMessage("RecieveMessage: " + e.Message);
				if (isConnected)
				{
					CloseConnection("0|200");
				}
            }
        }

		public void SendMessage(string Message)
		{
			try
			{
				if (Message.Length > 0)
				{
                    clientWriter.WriteLine(Message);
                    clientWriter.Flush();
				}
			}
			catch (Exception e)
			{
                ChatForm.ShowMessage("Send message: " + e.Message);
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
                        ChatForm.ShowMessage("Server is down.");
						ChatForm.ConnectionFormChange(false);
						break;
					case "0|202":
                        ChatForm.ShowMessage("Disconnected by user request.");
						SendMessage(Reason);
						break;
                    case "0|203":
                        ChatForm.ShowMessage("Disconnected by server.");
                        ChatForm.ConnectionFormChange(false);
                        break;
                    case "0|200":
					default:
                        ChatForm.ShowMessage("Unknown error during communication.");
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
                ChatForm.ShowMessage("CloseConnection: " + e.Message);
            }
		}      
    }
}
