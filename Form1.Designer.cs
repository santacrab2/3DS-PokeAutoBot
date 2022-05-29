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
            this.button1 = new System.Windows.Forms.Button();
            this.LinkTradeStop = new System.Windows.Forms.Button();
            this.startlinktrades = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Logs = new System.Windows.Forms.TabPage();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.RemoteControl = new System.Windows.Forms.TabPage();
            this.RCa = new System.Windows.Forms.Button();
            this.RCb = new System.Windows.Forms.Button();
            this.RCx = new System.Windows.Forms.Button();
            this.RCy = new System.Windows.Forms.Button();
            this.RCright = new System.Windows.Forms.Button();
            this.RCdown = new System.Windows.Forms.Button();
            this.RCleft = new System.Windows.Forms.Button();
            this.RCup = new System.Windows.Forms.Button();
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleconnect = new System.Windows.Forms.Button();
            this.consoledisconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusbox = new System.Windows.Forms.RichTextBox();
            this.tabs.SuspendLayout();
            this.LinkTrades.SuspendLayout();
            this.Settings.SuspendLayout();
            this.Logs.SuspendLayout();
            this.RemoteControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.LinkTrades);
            this.tabs.Controls.Add(this.Settings);
            this.tabs.Controls.Add(this.Logs);
            this.tabs.Controls.Add(this.RemoteControl);
            this.tabs.Location = new System.Drawing.Point(0, 43);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(431, 303);
            this.tabs.TabIndex = 0;
            // 
            // LinkTrades
            // 
            this.LinkTrades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LinkTrades.Controls.Add(this.button1);
            this.LinkTrades.Controls.Add(this.LinkTradeStop);
            this.LinkTrades.Controls.Add(this.startlinktrades);
            this.LinkTrades.Location = new System.Drawing.Point(4, 24);
            this.LinkTrades.Name = "LinkTrades";
            this.LinkTrades.Padding = new System.Windows.Forms.Padding(3);
            this.LinkTrades.Size = new System.Drawing.Size(423, 275);
            this.LinkTrades.TabIndex = 0;
            this.LinkTrades.Text = "Link Trades";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LinkTradeStop
            // 
            this.LinkTradeStop.Enabled = false;
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
            this.startlinktrades.Enabled = false;
            this.startlinktrades.Location = new System.Drawing.Point(31, 24);
            this.startlinktrades.Name = "startlinktrades";
            this.startlinktrades.Size = new System.Drawing.Size(75, 23);
            this.startlinktrades.TabIndex = 0;
            this.startlinktrades.Text = "Start";
            this.startlinktrades.UseVisualStyleBackColor = true;
            this.startlinktrades.Click += new System.EventHandler(this.startlinktrades_Click);
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
            // RemoteControl
            // 
            this.RemoteControl.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this.RemoteControl.Size = new System.Drawing.Size(423, 275);
            this.RemoteControl.TabIndex = 4;
            this.RemoteControl.Text = "Remote Control";
            // 
            // RCa
            // 
            this.RCa.ForeColor = System.Drawing.Color.Red;
            this.RCa.Location = new System.Drawing.Point(344, 76);
            this.RCa.Name = "RCa";
            this.RCa.Size = new System.Drawing.Size(29, 23);
            this.RCa.TabIndex = 7;
            this.RCa.Text = "A";
            this.RCa.UseVisualStyleBackColor = true;
            this.RCa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCa_Click);
            this.RCa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCb
            // 
            this.RCb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.RCb.Location = new System.Drawing.Point(303, 101);
            this.RCb.Name = "RCb";
            this.RCb.Size = new System.Drawing.Size(35, 23);
            this.RCb.TabIndex = 6;
            this.RCb.Text = "B";
            this.RCb.UseVisualStyleBackColor = true;
            this.RCb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCb_Click);
            this.RCb.MouseUp +=new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCx
            // 
            this.RCx.ForeColor = System.Drawing.Color.Teal;
            this.RCx.Location = new System.Drawing.Point(306, 54);
            this.RCx.Name = "RCx";
            this.RCx.Size = new System.Drawing.Size(32, 23);
            this.RCx.TabIndex = 5;
            this.RCx.Text = "X";
            this.RCx.UseVisualStyleBackColor = true;
            this.RCx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCx_Click);
            this.RCx.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCy
            // 
            this.RCy.ForeColor = System.Drawing.Color.Green;
            this.RCy.Location = new System.Drawing.Point(272, 76);
            this.RCy.Name = "RCy";
            this.RCy.Size = new System.Drawing.Size(28, 23);
            this.RCy.TabIndex = 4;
            this.RCy.Text = "Y";
            this.RCy.UseVisualStyleBackColor = true;
            this.RCy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCy_Click);
            this.RCy.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCright
            // 
            this.RCright.Location = new System.Drawing.Point(78, 149);
            this.RCright.Name = "RCright";
            this.RCright.Size = new System.Drawing.Size(32, 23);
            this.RCright.TabIndex = 3;
            this.RCright.Text = "➡";
            this.RCright.UseVisualStyleBackColor = true;
            this.RCright.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCright_Click);
            this.RCright.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCdown
            // 
            this.RCdown.Location = new System.Drawing.Point(50, 178);
            this.RCdown.Name = "RCdown";
            this.RCdown.Size = new System.Drawing.Size(24, 27);
            this.RCdown.TabIndex = 2;
            this.RCdown.Text = "⬇";
            this.RCdown.UseVisualStyleBackColor = true;
            this.RCdown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCdown_Click);
            this.RCdown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCleft
            // 
            this.RCleft.Location = new System.Drawing.Point(18, 149);
            this.RCleft.Name = "RCleft";
            this.RCleft.Size = new System.Drawing.Size(26, 23);
            this.RCleft.TabIndex = 1;
            this.RCleft.Text = "⬅";
            this.RCleft.UseVisualStyleBackColor = true;
            this.RCleft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCleft_Click);
            this.RCleft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
            // 
            // RCup
            // 
            this.RCup.Location = new System.Drawing.Point(50, 117);
            this.RCup.Name = "RCup";
            this.RCup.Size = new System.Drawing.Size(23, 27);
            this.RCup.TabIndex = 0;
            this.RCup.Text = "⬆";
            this.RCup.UseVisualStyleBackColor = true;
            this.RCup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RCup_Click);
            this.RCup.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RC_Release);
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
            this.consoledisconnect.Enabled = false;
            this.consoledisconnect.Location = new System.Drawing.Point(229, 12);
            this.consoledisconnect.Name = "consoledisconnect";
            this.consoledisconnect.Size = new System.Drawing.Size(75, 23);
            this.consoledisconnect.TabIndex = 4;
            this.consoledisconnect.Text = "Disconnect";
            this.consoledisconnect.UseVisualStyleBackColor = true;
            this.consoledisconnect.Click += new System.EventHandler(this.consoledisconnect_Click);
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
        private Button button1;
        private TabPage RemoteControl;
        public Button RCup;
        public Button RCleft;
        public Button RCdown;
        public Button RCright;
        public Button RCa;
        public Button RCb;
        public Button RCx;
        public Button RCy;
    }
}