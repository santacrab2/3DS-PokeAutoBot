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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LinkTrades = new System.Windows.Forms.TabPage();
            this.startlinktrades = new System.Windows.Forms.Button();
            this.Discord = new System.Windows.Forms.TabPage();
            this.discordconnect = new System.Windows.Forms.Button();
            this.discordtoken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Logs = new System.Windows.Forms.TabPage();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleconnect = new System.Windows.Forms.Button();
            this.consoledisconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusbox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.LinkTrades.SuspendLayout();
            this.Discord.SuspendLayout();
            this.Logs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.LinkTrades);
            this.tabControl1.Controls.Add(this.Discord);
            this.tabControl1.Controls.Add(this.Logs);
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 303);
            this.tabControl1.TabIndex = 0;
            // 
            // LinkTrades
            // 
            this.LinkTrades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LinkTrades.Controls.Add(this.button1);
            this.LinkTrades.Controls.Add(this.startlinktrades);
            this.LinkTrades.Location = new System.Drawing.Point(4, 24);
            this.LinkTrades.Name = "LinkTrades";
            this.LinkTrades.Padding = new System.Windows.Forms.Padding(3);
            this.LinkTrades.Size = new System.Drawing.Size(423, 275);
            this.LinkTrades.TabIndex = 0;
            this.LinkTrades.Text = "Link Trades";
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
            this.Discord.Controls.Add(this.discordtoken);
            this.Discord.Controls.Add(this.label3);
            this.Discord.Location = new System.Drawing.Point(4, 24);
            this.Discord.Name = "Discord";
            this.Discord.Padding = new System.Windows.Forms.Padding(3);
            this.Discord.Size = new System.Drawing.Size(423, 275);
            this.Discord.TabIndex = 2;
            this.Discord.Text = "Discord";
            // 
            // discordconnect
            // 
            this.discordconnect.Location = new System.Drawing.Point(28, 66);
            this.discordconnect.Name = "discordconnect";
            this.discordconnect.Size = new System.Drawing.Size(142, 23);
            this.discordconnect.TabIndex = 2;
            this.discordconnect.Text = "Connect to Discord";
            this.discordconnect.UseVisualStyleBackColor = true;
            this.discordconnect.Click += new System.EventHandler(this.discordconnect_Click);
            // 
            // discordtoken
            // 
            this.discordtoken.Location = new System.Drawing.Point(50, 14);
            this.discordtoken.Name = "discordtoken";
            this.discordtoken.Size = new System.Drawing.Size(350, 23);
            this.discordtoken.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Token:";
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
            this.statusbox.Size = new System.Drawing.Size(120, 18);
            this.statusbox.TabIndex = 6;
            this.statusbox.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(327, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.LinkTrades.ResumeLayout(false);
            this.Discord.ResumeLayout(false);
            this.Discord.PerformLayout();
            this.Logs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
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
        public TextBox discordtoken;
        private Label label3;
        private Button button1;
    }
}