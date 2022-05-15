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
            this.tabs = new System.Windows.Forms.TabControl();
            this.LinkTrades = new System.Windows.Forms.TabPage();
            this.LinkTradeStop = new System.Windows.Forms.Button();
            this.startlinktrades = new System.Windows.Forms.Button();
            this.Discord = new System.Windows.Forms.TabPage();
            this.discordconnect = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Logs = new System.Windows.Forms.TabPage();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleconnect = new System.Windows.Forms.Button();
            this.consoledisconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusbox = new System.Windows.Forms.RichTextBox();
            this.tabs.SuspendLayout();
            this.LinkTrades.SuspendLayout();
            this.Discord.SuspendLayout();
            this.Settings.SuspendLayout();
            this.Logs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.LinkTrades);
            this.tabs.Controls.Add(this.Discord);
            this.tabs.Controls.Add(this.Settings);
            this.tabs.Controls.Add(this.Logs);
            this.tabs.Location = new System.Drawing.Point(0, 43);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(431, 303);
            this.tabs.TabIndex = 0;
            // 
            // LinkTrades
            // 
            this.LinkTrades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LinkTrades.Controls.Add(this.LinkTradeStop);
            this.LinkTrades.Controls.Add(this.startlinktrades);
            this.LinkTrades.Location = new System.Drawing.Point(4, 24);
            this.LinkTrades.Name = "LinkTrades";
            this.LinkTrades.Padding = new System.Windows.Forms.Padding(3);
            this.LinkTrades.Size = new System.Drawing.Size(423, 275);
            this.LinkTrades.TabIndex = 0;
            this.LinkTrades.Text = "Link Trades";
            // 
            // LinkTradeStop
            // 
            this.LinkTradeStop.Location = new System.Drawing.Point(112, 24);
            this.LinkTradeStop.Name = "LinkTradeStop";
            this.LinkTradeStop.Size = new System.Drawing.Size(75, 23);
            this.LinkTradeStop.TabIndex = 2;
            this.LinkTradeStop.Text = "Stop";
            this.LinkTradeStop.UseVisualStyleBackColor = true;
            this.LinkTradeStop.Click += new System.EventHandler(this.LinkTradeStop_Click);
            // 
            // startlinktrades
            // 
            this.startlinktrades.Location = new System.Drawing.Point(31, 24);
            this.startlinktrades.Name = "startlinktrades";
            this.startlinktrades.Size = new System.Drawing.Size(75, 23);
            this.startlinktrades.TabIndex = 0;
            this.startlinktrades.Text = "Start";
            this.startlinktrades.UseVisualStyleBackColor = true;
            this.startlinktrades.Click += new System.EventHandler(this.startlinktrades_Click);
            // 
            // Discord
            // 
            this.Discord.BackColor = System.Drawing.SystemColors.Control;
            this.Discord.Controls.Add(this.discordconnect);
            this.Discord.Location = new System.Drawing.Point(4, 24);
            this.Discord.Name = "Discord";
            this.Discord.Padding = new System.Windows.Forms.Padding(3);
            this.Discord.Size = new System.Drawing.Size(423, 275);
            this.Discord.TabIndex = 2;
            this.Discord.Text = "ChatServices";
            // 
            // discordconnect
            // 
            this.discordconnect.Location = new System.Drawing.Point(8, 6);
            this.discordconnect.Name = "discordconnect";
            this.discordconnect.Size = new System.Drawing.Size(142, 23);
            this.discordconnect.TabIndex = 2;
            this.discordconnect.Text = "Connect to Discord";
            this.discordconnect.UseVisualStyleBackColor = true;
            this.discordconnect.Click += new System.EventHandler(this.discordconnect_Click);
            // 
            // Settings
            // 
            this.Settings.BackColor = System.Drawing.SystemColors.Control;
            this.Settings.Controls.Add(this.propertyGrid1);
            this.Settings.Location = new System.Drawing.Point(4, 24);
            this.Settings.Name = "Settings";
            this.Settings.Padding = new System.Windows.Forms.Padding(3);
            this.Settings.Size = new System.Drawing.Size(423, 275);
            this.Settings.TabIndex = 3;
            this.Settings.Text = "Settings";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(8, 6);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(409, 263);
            this.propertyGrid1.TabIndex = 0;
            // 
            // Logs
            // 
            this.Logs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Logs.Controls.Add(this.logbox);
            this.Logs.Location = new System.Drawing.Point(4, 24);
            this.Logs.Name = "Logs";
            this.Logs.Padding = new System.Windows.Forms.Padding(3);
            this.Logs.Size = new System.Drawing.Size(423, 275);
            this.Logs.TabIndex = 1;
            this.Logs.Text = "Logs";
            // 
            // logbox
            // 
            this.logbox.Location = new System.Drawing.Point(8, 6);
            this.logbox.Name = "logbox";
            this.logbox.ReadOnly = true;
            this.logbox.Size = new System.Drawing.Size(409, 263);
            this.logbox.TabIndex = 0;
            this.logbox.Text = "";
            // 
            // IpAddress
            // 
            this.IpAddress.Location = new System.Drawing.Point(32, 12);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(100, 23);
            this.IpAddress.TabIndex = 1;
            this.IpAddress.Text = "192.168.1.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP:";
            // 
            // consoleconnect
            // 
            this.consoleconnect.Location = new System.Drawing.Point(148, 12);
            this.consoleconnect.Name = "consoleconnect";
            this.consoleconnect.Size = new System.Drawing.Size(75, 23);
            this.consoleconnect.TabIndex = 3;
            this.consoleconnect.Text = "Connect";
            this.consoleconnect.UseVisualStyleBackColor = true;
            this.consoleconnect.Click += new System.EventHandler(this.consoleconnect_Click);
            // 
            // consoledisconnect
            // 
            this.consoledisconnect.Location = new System.Drawing.Point(229, 12);
            this.consoledisconnect.Name = "consoledisconnect";
            this.consoledisconnect.Size = new System.Drawing.Size(75, 23);
            this.consoledisconnect.TabIndex = 4;
            this.consoledisconnect.Text = "Disconnect";
            this.consoledisconnect.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status:";
            // 
            // statusbox
            // 
            this.statusbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusbox.Location = new System.Drawing.Point(54, 349);
            this.statusbox.Multiline = false;
            this.statusbox.Name = "statusbox";
            this.statusbox.ReadOnly = true;
            this.statusbox.Size = new System.Drawing.Size(377, 18);
            this.statusbox.TabIndex = 6;
            this.statusbox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 373);
            this.Controls.Add(this.statusbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.consoledisconnect);
            this.Controls.Add(this.consoleconnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IpAddress);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabs.ResumeLayout(false);
            this.LinkTrades.ResumeLayout(false);
            this.Discord.ResumeLayout(false);
            this.Settings.ResumeLayout(false);
            this.Logs.ResumeLayout(false);
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
        private Button startlinktrades;
        private TabPage Discord;
        private Button discordconnect;
        private Button LinkTradeStop;
        private TabPage Settings;
        private PropertyGrid propertyGrid1;
    }
}