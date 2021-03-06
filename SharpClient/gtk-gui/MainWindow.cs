
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Table table1;

	private global::Gtk.ScrolledWindow chatWindow;

	private global::Gtk.VBox messageBox;

	private global::Gtk.Button Connect;

	private global::Gtk.ScrolledWindow GtkScrolledWindow1;

	private global::Gtk.TextView Message;

	private global::Gtk.Entry Nickname;

	private global::Gtk.Label nicknameLabel;

	private global::Gtk.Button SendMessageButton;

	private global::Gtk.Button sendStickerButton;

	private global::Gtk.Entry serverIp;

	private global::Gtk.Label serverLabel;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("SharpClient");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.table1 = new global::Gtk.Table(((uint)(10)), ((uint)(4)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.chatWindow = new global::Gtk.ScrolledWindow();
		this.chatWindow.CanFocus = true;
		this.chatWindow.Name = "chatWindow";
		this.chatWindow.VscrollbarPolicy = ((global::Gtk.PolicyType)(0));
		this.chatWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
		this.chatWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child chatWindow.Gtk.Container+ContainerChild
		global::Gtk.Viewport w1 = new global::Gtk.Viewport();
		w1.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child GtkViewport.Gtk.Container+ContainerChild
		this.messageBox = new global::Gtk.VBox();
		this.messageBox.Name = "messageBox";
		this.messageBox.Spacing = 6;
		w1.Add(this.messageBox);
		this.chatWindow.Add(w1);
		this.table1.Add(this.chatWindow);
		global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.chatWindow]));
		w4.TopAttach = ((uint)(2));
		w4.BottomAttach = ((uint)(8));
		w4.RightAttach = ((uint)(4));
		w4.XOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.Connect = new global::Gtk.Button();
		this.Connect.CanFocus = true;
		this.Connect.Name = "Connect";
		this.Connect.UseUnderline = true;
		this.Connect.Label = global::Mono.Unix.Catalog.GetString("Connect");
		this.table1.Add(this.Connect);
		global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.Connect]));
		w5.BottomAttach = ((uint)(2));
		w5.LeftAttach = ((uint)(3));
		w5.RightAttach = ((uint)(4));
		// Container child table1.Gtk.Table+TableChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.Message = new global::Gtk.TextView();
		this.Message.CanFocus = true;
		this.Message.Name = "Message";
		this.Message.Editable = false;
		this.Message.WrapMode = ((global::Gtk.WrapMode)(3));
		this.GtkScrolledWindow1.Add(this.Message);
		this.table1.Add(this.GtkScrolledWindow1);
		global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow1]));
		w7.TopAttach = ((uint)(8));
		w7.BottomAttach = ((uint)(10));
		w7.RightAttach = ((uint)(3));
		w7.XOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.Nickname = new global::Gtk.Entry();
		this.Nickname.CanFocus = true;
		this.Nickname.Name = "Nickname";
		this.Nickname.IsEditable = true;
		this.Nickname.InvisibleChar = '•';
		this.table1.Add(this.Nickname);
		global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.Nickname]));
		w8.TopAttach = ((uint)(1));
		w8.BottomAttach = ((uint)(2));
		w8.LeftAttach = ((uint)(1));
		w8.RightAttach = ((uint)(3));
		// Container child table1.Gtk.Table+TableChild
		this.nicknameLabel = new global::Gtk.Label();
		this.nicknameLabel.Name = "nicknameLabel";
		this.nicknameLabel.Xalign = 1F;
		this.nicknameLabel.LabelProp = global::Mono.Unix.Catalog.GetString("Nickname:");
		this.table1.Add(this.nicknameLabel);
		global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.nicknameLabel]));
		w9.TopAttach = ((uint)(1));
		w9.BottomAttach = ((uint)(2));
		w9.XOptions = ((global::Gtk.AttachOptions)(4));
		w9.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.SendMessageButton = new global::Gtk.Button();
		this.SendMessageButton.CanFocus = true;
		this.SendMessageButton.Name = "SendMessageButton";
		this.SendMessageButton.UseUnderline = true;
		this.SendMessageButton.Label = global::Mono.Unix.Catalog.GetString("Send");
		this.table1.Add(this.SendMessageButton);
		global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.SendMessageButton]));
		w10.TopAttach = ((uint)(8));
		w10.BottomAttach = ((uint)(9));
		w10.LeftAttach = ((uint)(3));
		w10.RightAttach = ((uint)(4));
		// Container child table1.Gtk.Table+TableChild
		this.sendStickerButton = new global::Gtk.Button();
		this.sendStickerButton.CanFocus = true;
		this.sendStickerButton.Name = "sendStickerButton";
		this.sendStickerButton.UseUnderline = true;
		this.sendStickerButton.Label = global::Mono.Unix.Catalog.GetString("Select sticker");
		this.table1.Add(this.sendStickerButton);
		global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.sendStickerButton]));
		w11.TopAttach = ((uint)(9));
		w11.BottomAttach = ((uint)(10));
		w11.LeftAttach = ((uint)(3));
		w11.RightAttach = ((uint)(4));
		w11.XOptions = ((global::Gtk.AttachOptions)(4));
		w11.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.serverIp = new global::Gtk.Entry();
		this.serverIp.CanFocus = true;
		this.serverIp.Name = "serverIp";
		this.serverIp.IsEditable = true;
		this.serverIp.InvisibleChar = '•';
		this.table1.Add(this.serverIp);
		global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.serverIp]));
		w12.LeftAttach = ((uint)(1));
		w12.RightAttach = ((uint)(3));
		// Container child table1.Gtk.Table+TableChild
		this.serverLabel = new global::Gtk.Label();
		this.serverLabel.Name = "serverLabel";
		this.serverLabel.Xalign = 1F;
		this.serverLabel.LabelProp = global::Mono.Unix.Catalog.GetString("Server IP:");
		this.table1.Add(this.serverLabel);
		global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.serverLabel]));
		w13.XOptions = ((global::Gtk.AttachOptions)(4));
		w13.YOptions = ((global::Gtk.AttachOptions)(4));
		this.Add(this.table1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 426;
		this.DefaultHeight = 424;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.sendStickerButton.Clicked += new global::System.EventHandler(this.OnSendStickerButtonClicked);
		this.SendMessageButton.Clicked += new global::System.EventHandler(this.OnSendMessageButtonClicked);
		this.Message.KeyPressEvent += new global::Gtk.KeyPressEventHandler(this.OnMessageKeyPressEvent);
		this.Message.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler(this.OnMessageKeyReleaseEvent);
		this.Connect.Clicked += new global::System.EventHandler(this.OnConnectClicked);
	}
}
