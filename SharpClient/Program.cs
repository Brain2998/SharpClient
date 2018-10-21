﻿using System;
using Gtk;

namespace SharpClient
{
    class MainClass
    {
		private static MainWindow win;
        public static void Main(string[] args)
        {
			try
			{
				Application.Init();
				win = new MainWindow();
				win.Show();
				Application.Run();
			}
			catch (Exception err)
            {
				win.Messages = "Main: " + err.Message + "\n";
            }
        }
    }
}
