using System;
using Gtk;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SharpClient
{
    public static class PixStickers
    {
        private static string location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static Dictionary<string, Gdk.Pixbuf> Stickers = new Dictionary<string, Gdk.Pixbuf>
        {
            ["Kappa"] = new Gdk.Pixbuf(location+"/Stickers/Kappa.png"),
            ["BibleThump"] = new Gdk.Pixbuf(location+"/Stickers/BibleThump.png"),
            ["NotLikeThis"] = new Gdk.Pixbuf(location + "/Stickers/NotLikeThis.png"),
            ["HeyGuys"] = new Gdk.Pixbuf(location + "/Stickers/HeyGuys.png")
        };
    }

    public class TextMessage : Gtk.Label
    {
        public TextMessage(string Message)
        {
            Xalign = 0;
            Yalign = 0;
            LabelProp = Message;
        }
    }

    public class Sticker : Gtk.Image
    {
        public string StickerName;

        public Sticker(Gdk.Pixbuf sticker)
        {
            Xalign = 0;
            Yalign = 0;
            Pixbuf = sticker;
        }

        public Sticker(Gdk.Pixbuf sticker, string name)
        {
            Xalign = 0;
            Yalign = 0;
            Pixbuf = sticker;
            StickerName = name;
        }
    }

    public class StickerWindow : Window
    {
        private static MainWindow ChatForm;
        private List<EventBox> events = new List<EventBox>();
        private Table stickerTable = new Table(2, 2, true);

        public StickerWindow(string title, MainWindow form) : base(title)
        {
            Title = title;
            ChatForm = form;

            for (int i = 0; i < stickerTable.NColumns; ++i)
            {
                for (int j = 0; j < stickerTable.NRows; ++j)
                {
                    events.Add(new EventBox());
                    events[events.Count - 1].ButtonReleaseEvent += OnStickerClicked;
                    stickerTable.Attach(events[events.Count - 1],(uint)i, (uint)i +1, (uint)j, (uint)j +1);
                }
            }
            Sticker Kappa = new Sticker(PixStickers.Stickers["Kappa"], "Kappa");
            events[0].Add(Kappa);
            Sticker BibleThump = new Sticker(PixStickers.Stickers["BibleThump"], "BibleThump");
            events[1].Add(BibleThump);
            Sticker NotLikeThis = new Sticker(PixStickers.Stickers["NotLikeThis"], "NotLikeThis");
            events[2].Add(NotLikeThis);
            Sticker HeyGuys = new Sticker(PixStickers.Stickers["HeyGuys"], "HeyGuys");
            events[3].Add(HeyGuys);
            Add(stickerTable);
            ShowAll();
        }

        static void OnStickerClicked(object obj, ButtonReleaseEventArgs args)
        {
            if (args.Event.Button == 1)
            {
                EventBox box = (EventBox)obj;
                Sticker sticker = (Sticker)box.Child;
                if (ChatForm.Connected)
                {
                    ChatForm.connection.SendMessage("2|" + sticker.StickerName);
                }
                box.Parent.Parent.Hide();
            }
        }
    }
}
