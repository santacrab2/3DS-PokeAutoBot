namespace _3DS_link_trade_bot
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabs = new System.Windows.Forms.TabControl();
            this.LinkTrades = new System.Windows.Forms.TabPage();
            this.LinkTradeStop = new System.Windows.Forms.Button();
            this.startlinktrades = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Logs = new System.Windows.Forms.TabPage();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.RemoteControl = new System.Windows.Forms.TabPage();
            this.touchscreen = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RCpower = new System.Windows.Forms.Button();
            this.RCselect = new System.Windows.Forms.Button();
            this.RCstart = new System.Windows.Forms.Button();
            this.RChome = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.RCa = new System.Windows.Forms.Button();
            this.RCb = new System.Windows.Forms.Button();
            this.RCx = new System.Windows.Forms.Button();
            this.RCy = new System.Windows.Forms.Button();
            this.RCright = new System.Windows.Forms.Button();
            this.RCdown = new System.Windows.Forms.Button();
            this.RCleft = new System.Windows.Forms.Button();
            this.RCup = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleconnect = new System.Windows.Forms.Button();
            this.consoledisconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusbox = new System.Windows.Forms.RichTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.BotMode = new System.Windows.Forms.ComboBox();
            this.tabs.SuspendLayout();
            this.LinkTrades.SuspendLayout();
            this.Settings.SuspendLayout();
            this.Logs.SuspendLayout();
            this.RemoteControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.touchscreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.LinkTrades);
            this.tabs.Controls.Add(this.Settings);
            this.tabs.Controls.Add(this.Logs);
            this.tabs.Controls.Add(this.RemoteControl);
            this.tabs.Location = new System.Drawing.Point(0, 65);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(538, 312);
            this.tabs.TabIndex = 0;
            // 
            // LinkTrades
            // 
            this.LinkTrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.LinkTrades.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.Bg000;
            this.LinkTrades.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LinkTrades.Controls.Add(this.LinkTradeStop);
            this.LinkTrades.Controls.Add(this.startlinktrades);
            this.LinkTrades.Location = new System.Drawing.Point(4, 24);
            this.LinkTrades.Name = "LinkTrades";
            this.LinkTrades.Padding = new System.Windows.Forms.Padding(3);
            this.LinkTrades.Size = new System.Drawing.Size(530, 284);
            this.LinkTrades.TabIndex = 0;
            this.LinkTrades.Text = "Bots Home";
            // 
            // LinkTradeStop
            // 
            this.LinkTradeStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LinkTradeStop.Enabled = false;
            this.LinkTradeStop.ForeColor = System.Drawing.Color.White;
            this.LinkTradeStop.Location = new System.Drawing.Point(281, 255);
            this.LinkTradeStop.Name = "LinkTradeStop";
            this.LinkTradeStop.Size = new System.Drawing.Size(75, 23);
            this.LinkTradeStop.TabIndex = 2;
            this.LinkTradeStop.Text = "Stop";
            this.LinkTradeStop.UseVisualStyleBackColor = false;
            this.LinkTradeStop.Click += new System.EventHandler(this.LinkTradeStop_Click);
            // 
            // startlinktrades
            // 
            this.startlinktrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.startlinktrades.Enabled = false;
            this.startlinktrades.ForeColor = System.Drawing.Color.White;
            this.startlinktrades.Location = new System.Drawing.Point(187, 255);
            this.startlinktrades.Name = "startlinktrades";
            this.startlinktrades.Size = new System.Drawing.Size(75, 23);
            this.startlinktrades.TabIndex = 0;
            this.startlinktrades.Text = "Start";
            this.startlinktrades.UseVisualStyleBackColor = false;
            this.startlinktrades.Click += new System.EventHandler(this.startlinktrades_Click);
            // 
            // Settings
            // 
            this.Settings.BackColor = System.Drawing.SystemColors.Control;
            this.Settings.Controls.Add(this.propertyGrid1);
            this.Settings.Location = new System.Drawing.Point(4, 24);
            this.Settings.Name = "Settings";
            this.Settings.Padding = new System.Windows.Forms.Padding(3);
            this.Settings.Size = new System.Drawing.Size(530, 284);
            this.Settings.TabIndex = 3;
            this.Settings.Text = "Settings";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.propertyGrid1.CategoryForeColor = System.Drawing.Color.White;
            this.propertyGrid1.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.propertyGrid1.CommandsBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.propertyGrid1.CommandsLinkColor = System.Drawing.Color.Gray;
            this.propertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.propertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.propertyGrid1.HelpForeColor = System.Drawing.Color.White;
            this.propertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.propertyGrid1.Location = new System.Drawing.Point(8, 6);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedItemWithFocusBackColor = System.Drawing.SystemColors.ControlText;
            this.propertyGrid1.Size = new System.Drawing.Size(519, 275);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.propertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // Logs
            // 
            this.Logs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Logs.Controls.Add(this.logbox);
            this.Logs.Location = new System.Drawing.Point(4, 24);
            this.Logs.Name = "Logs";
            this.Logs.Padding = new System.Windows.Forms.Padding(3);
            this.Logs.Size = new System.Drawing.Size(530, 284);
            this.Logs.TabIndex = 1;
            this.Logs.Text = "Logs";
            // 
            // logbox
            // 
            this.logbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.logbox.ForeColor = System.Drawing.Color.White;
            this.logbox.Location = new System.Drawing.Point(8, 6);
            this.logbox.Name = "logbox";
            this.logbox.ReadOnly = true;
            this.logbox.Size = new System.Drawing.Size(519, 275);
            this.logbox.TabIndex = 0;
            this.logbox.Text = "";
            // 
            // RemoteControl
            // 
            this.RemoteControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RemoteControl.Controls.Add(this.touchscreen);
            this.RemoteControl.Controls.Add(this.label4);
            this.RemoteControl.Controls.Add(this.label3);
            this.RemoteControl.Controls.Add(this.RCpower);
            this.RemoteControl.Controls.Add(this.RCselect);
            this.RemoteControl.Controls.Add(this.RCstart);
            this.RemoteControl.Controls.Add(this.RChome);
            this.RemoteControl.Controls.Add(this.pictureBox5);
            this.RemoteControl.Controls.Add(this.RCa);
            this.RemoteControl.Controls.Add(this.RCb);
            this.RemoteControl.Controls.Add(this.RCx);
            this.RemoteControl.Controls.Add(this.RCy);
            this.RemoteControl.Controls.Add(this.RCright);
            this.RemoteControl.Controls.Add(this.RCdown);
            this.RemoteControl.Controls.Add(this.RCleft);
            this.RemoteControl.Controls.Add(this.RCup);
            this.RemoteControl.Location = new System.Drawing.Point(4, 24);
            this.RemoteControl.Name = "RemoteControl";
            this.RemoteControl.Padding = new System.Windows.Forms.Padding(3);
            this.RemoteControl.Size = new System.Drawing.Size(530, 284);
            this.RemoteControl.TabIndex = 4;
            this.RemoteControl.Text = "Remote Control";
            // 
            // touchscreen
            // 
            this.touchscreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("touchscreen.BackgroundImage")));
            this.touchscreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.touchscreen.InitialImage = global::_3DS_link_trade_bot.Properties.Resources.touchscreen;
            this.touchscreen.Location = new System.Drawing.Point(104, 10);
            this.touchscreen.Name = "touchscreen";
            this.touchscreen.Size = new System.Drawing.Size(320, 240);
            this.touchscreen.TabIndex = 15;
            this.touchscreen.TabStop = false;
            this.touchscreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox6_Click);
            this.touchscreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(467, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "SELECT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(467, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "START";
            // 
            // RCpower
            // 
            this.RCpower.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCpower.BackgroundImage")));
            this.RCpower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCpower.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCpower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCpower.Location = new System.Drawing.Point(498, 249);
            this.RCpower.Name = "RCpower";
            this.RCpower.Size = new System.Drawing.Size(29, 29);
            this.RCpower.TabIndex = 12;
            this.RCpower.UseVisualStyleBackColor = true;
            this.RCpower.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCpower_Click);
            this.RCpower.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCselect
            // 
            this.RCselect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCselect.BackgroundImage")));
            this.RCselect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCselect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCselect.Location = new System.Drawing.Point(440, 214);
            this.RCselect.Name = "RCselect";
            this.RCselect.Size = new System.Drawing.Size(21, 22);
            this.RCselect.TabIndex = 11;
            this.RCselect.UseVisualStyleBackColor = true;
            this.RCselect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCselect_Click);
            this.RCselect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCstart
            // 
            this.RCstart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCstart.BackgroundImage")));
            this.RCstart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCstart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCstart.ForeColor = System.Drawing.Color.Transparent;
            this.RCstart.Location = new System.Drawing.Point(440, 187);
            this.RCstart.Name = "RCstart";
            this.RCstart.Size = new System.Drawing.Size(21, 21);
            this.RCstart.TabIndex = 10;
            this.RCstart.UseVisualStyleBackColor = true;
            this.RCstart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.start_Click);
            this.RCstart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RChome
            // 
            this.RChome.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RChome.BackgroundImage")));
            this.RChome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RChome.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RChome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RChome.Location = new System.Drawing.Point(243, 255);
            this.RChome.Name = "RChome";
            this.RChome.Size = new System.Drawing.Size(41, 23);
            this.RChome.TabIndex = 9;
            this.RChome.UseVisualStyleBackColor = true;
            this.RChome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RChome_Click);
            this.RChome.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.Center;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(37, 136);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 31);
            this.pictureBox5.TabIndex = 8;
            this.pictureBox5.TabStop = false;
            // 
            // RCa
            // 
            this.RCa.BackColor = System.Drawing.Color.Transparent;
            this.RCa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCa.BackgroundImage")));
            this.RCa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCa.Location = new System.Drawing.Point(482, 78);
            this.RCa.Name = "RCa";
            this.RCa.Size = new System.Drawing.Size(30, 31);
            this.RCa.TabIndex = 7;
            this.RCa.UseVisualStyleBackColor = false;
            this.RCa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCa_Click);
            this.RCa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCb
            // 
            this.RCb.BackColor = System.Drawing.Color.Transparent;
            this.RCb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCb.BackgroundImage")));
            this.RCb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCb.Location = new System.Drawing.Point(458, 102);
            this.RCb.Name = "RCb";
            this.RCb.Size = new System.Drawing.Size(31, 31);
            this.RCb.TabIndex = 6;
            this.RCb.UseVisualStyleBackColor = false;
            this.RCb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCb_Click);
            this.RCb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCx
            // 
            this.RCx.BackColor = System.Drawing.Color.Transparent;
            this.RCx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCx.BackgroundImage")));
            this.RCx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCx.Location = new System.Drawing.Point(458, 53);
            this.RCx.Name = "RCx";
            this.RCx.Size = new System.Drawing.Size(31, 31);
            this.RCx.TabIndex = 5;
            this.RCx.UseVisualStyleBackColor = false;
            this.RCx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCx_Click);
            this.RCx.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCy
            // 
            this.RCy.BackColor = System.Drawing.Color.Transparent;
            this.RCy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RCy.BackgroundImage")));
            this.RCy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCy.Location = new System.Drawing.Point(430, 78);
            this.RCy.Name = "RCy";
            this.RCy.Size = new System.Drawing.Size(31, 31);
            this.RCy.TabIndex = 4;
            this.RCy.UseVisualStyleBackColor = false;
            this.RCy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCy_Click);
            this.RCy.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCright
            // 
            this.RCright.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.Right;
            this.RCright.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCright.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCright.Location = new System.Drawing.Point(64, 136);
            this.RCright.Name = "RCright";
            this.RCright.Size = new System.Drawing.Size(32, 31);
            this.RCright.TabIndex = 3;
            this.RCright.Text = "➡";
            this.RCright.UseVisualStyleBackColor = true;
            this.RCright.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCright_Click);
            this.RCright.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCdown
            // 
            this.RCdown.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.Down;
            this.RCdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCdown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCdown.Location = new System.Drawing.Point(37, 162);
            this.RCdown.Name = "RCdown";
            this.RCdown.Size = new System.Drawing.Size(32, 31);
            this.RCdown.TabIndex = 2;
            this.RCdown.Text = "⬇";
            this.RCdown.UseVisualStyleBackColor = true;
            this.RCdown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCdown_Click);
            this.RCdown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCleft
            // 
            this.RCleft.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.Left;
            this.RCleft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCleft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCleft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCleft.Location = new System.Drawing.Point(8, 136);
            this.RCleft.Name = "RCleft";
            this.RCleft.Size = new System.Drawing.Size(32, 31);
            this.RCleft.TabIndex = 1;
            this.RCleft.Text = "⬅";
            this.RCleft.UseVisualStyleBackColor = true;
            this.RCleft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCleft_Click);
            this.RCleft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCup
            // 
            this.RCup.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.Up;
            this.RCup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RCup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.RCup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.RCup.Location = new System.Drawing.Point(37, 113);
            this.RCup.Name = "RCup";
            this.RCup.Size = new System.Drawing.Size(31, 31);
            this.RCup.TabIndex = 0;
            this.RCup.Text = "⬆";
            this.RCup.UseVisualStyleBackColor = true;
            this.RCup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCup_Click);
            this.RCup.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.pkm_s;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(310, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 47);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // IpAddress
            // 
            this.IpAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.IpAddress.ForeColor = System.Drawing.Color.White;
            this.IpAddress.Location = new System.Drawing.Point(32, 7);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(100, 23);
            this.IpAddress.TabIndex = 1;
            this.IpAddress.Text = "192.168.1.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP:";
            // 
            // consoleconnect
            // 
            this.consoleconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.consoleconnect.Location = new System.Drawing.Point(138, 7);
            this.consoleconnect.Name = "consoleconnect";
            this.consoleconnect.Size = new System.Drawing.Size(75, 23);
            this.consoleconnect.TabIndex = 3;
            this.consoleconnect.Text = "Connect";
            this.consoleconnect.UseVisualStyleBackColor = false;
            this.consoleconnect.Click += new System.EventHandler(this.consoleconnect_Click);
            // 
            // consoledisconnect
            // 
            this.consoledisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.consoledisconnect.Enabled = false;
            this.consoledisconnect.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.consoledisconnect.Location = new System.Drawing.Point(213, 7);
            this.consoledisconnect.Name = "consoledisconnect";
            this.consoledisconnect.Size = new System.Drawing.Size(75, 23);
            this.consoledisconnect.TabIndex = 4;
            this.consoledisconnect.Text = "Disconnect";
            this.consoledisconnect.UseVisualStyleBackColor = false;
            this.consoledisconnect.Click += new System.EventHandler(this.consoledisconnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status:";
            // 
            // statusbox
            // 
            this.statusbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusbox.ForeColor = System.Drawing.Color.White;
            this.statusbox.Location = new System.Drawing.Point(50, 383);
            this.statusbox.Multiline = false;
            this.statusbox.Name = "statusbox";
            this.statusbox.ReadOnly = true;
            this.statusbox.Size = new System.Drawing.Size(488, 18);
            this.statusbox.TabIndex = 6;
            this.statusbox.Text = "";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.pkm_m;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(369, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 47);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.pkm_us;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(425, 36);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 47);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::_3DS_link_trade_bot.Properties.Resources.pkm_um;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(481, 36);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 47);
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // BotMode
            // 
            this.BotMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BotMode.ForeColor = System.Drawing.Color.White;
            this.BotMode.FormattingEnabled = true;
            this.BotMode.Location = new System.Drawing.Point(294, 6);
            this.BotMode.Name = "BotMode";
            this.BotMode.Size = new System.Drawing.Size(98, 23);
            this.BotMode.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(550, 404);
            this.Controls.Add(this.BotMode);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.statusbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.consoledisconnect);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.consoleconnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IpAddress);
            this.Controls.Add(this.tabs);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "3DS Link Trade Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabs.ResumeLayout(false);
            this.LinkTrades.ResumeLayout(false);
            this.Settings.ResumeLayout(false);
            this.Logs.ResumeLayout(false);
            this.RemoteControl.ResumeLayout(false);
            this.RemoteControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.touchscreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabs;
        private TabPage Logs;
        private TabPage LinkTrades;
        private Label label1;
        private Button consoleconnect;
        private Button consoledisconnect;
        private Label label2;
        public TextBox IpAddress;
        public RichTextBox statusbox;
        public RichTextBox logbox;
        private TabPage Settings;
        private PropertyGrid propertyGrid1;
        public Button startlinktrades;
        public Button LinkTradeStop;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private TabPage RemoteControl;
        public Button RCup;
        public Button RCleft;
        public Button RCdown;
        public Button RCright;
        public Button RCa;
        public Button RCb;
        public Button RCx;
        public Button RCy;
        private PictureBox pictureBox5;
        public Button RChome;
        public Button RCselect;
        public Button RCstart;
        private Label label4;
        private Label label3;
        public Button RCpower;
        private PictureBox touchscreen;
        public ComboBox BotMode;
    }
}
