using System.Net;
using System;
using System.Text;
using System.IO;
using PKHeX.Core;
using Discord;
using System.Net.Sockets;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.Program;
using static _3DS_link_trade_bot.NTRClient;
using static _3DS_link_trade_bot.dsbotbase.Buttons;


namespace _3DS_link_trade_bot
{
    public partial class Form1 : Form
    {
   

        public Form1()
        {
            InitializeComponent();
        }



        public static NTRClient ntr = new();
        public static Socket Connection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,0);
        public static Socket Connection2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);

        private void consoleconnect_Click(object sender, EventArgs e)
        {
            ntr.Connect();
            if (clientNTR.IsConnected)
                ChangeStatus("ntr connected");
            Connection.Connect(Program.form1.IpAddress.Text, 4950);
            if (Connection.Connected)
            {
                
                Log("IR Connected");
            }
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes(0xFFF);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);

        }
        public static void ChangeStatus(string text)
        {
            Program.form1.statusbox.Text = text;
            
        }
        public static void Log(string text)
        {
            form1.logbox.AppendText(text + "\n");
        }

        private void logbox_TextChanged(object sender, EventArgs e)
        {

        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        
            ntr.Disconnect();
            Application.Exit();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            form1.IpAddress.Text = Properties.Settings.Default.IpAddress;
            form1.discordtoken.Text = Properties.Settings.Default.discordtoken;
            form1.botchannel.Text = Properties.Settings.Default.botchannel;
            form1.botfc.Text = Properties.Settings.Default.botfc;
        }

        private void startlinktrades_Click(object sender, EventArgs e)
        {

            LinkTradeBot.starttrades();
         
            var botchannels = form1.botchannel.Text.Split(',',StringSplitOptions.RemoveEmptyEntries);
            foreach (var channel in botchannels)
            {
                var id = ulong.Parse(channel);
                var botchannelid = (ITextChannel)discordmain._client.GetChannelAsync(id).Result;
                botchannelid.ModifyAsync(x => x.Name = botchannelid.Name.Replace("❌", "✅"));
                botchannelid.AddPermissionOverwriteAsync(botchannelid.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Allow));
                var offembed = new EmbedBuilder();
                offembed.AddField($"{discordmain._client.CurrentUser.Username} Bot Announcement", "Gen 7 Link Trade Bot is Online");
                botchannelid.SendMessageAsync("<@&898900914348372058>", embed: offembed.Build());
            }
        

            
        }

        private void discordconnect_Click(object sender, EventArgs e)
        {
            var bot = new discordmain();
            bot.MainAsync();
            ChangeStatus("Connected to Discord");
            Log("Connected to Discord");
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IpAddress = form1.IpAddress.Text;
            Properties.Settings.Default.discordtoken = form1.discordtoken.Text;
            Properties.Settings.Default.botchannel = form1.botchannel.Text;
            Properties.Settings.Default.botfc = form1.botfc.Text;   
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LinkTradeBot.LinkTradeRoutine();
        }

        private void LinkTradeStop_Click(object sender, EventArgs e)
        {
            LinkTradeBot.tradetoken.Cancel();
            var botchannels = form1.botchannel.Text.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var channel in botchannels)
            {
                var id = ulong.Parse(channel);
                var botchannelid = (ITextChannel)discordmain._client.GetChannelAsync(id).Result;
                botchannelid.ModifyAsync(x => x.Name = botchannelid.Name.Replace("✅", "❌"));
                botchannelid.AddPermissionOverwriteAsync(botchannelid.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                var offembed = new EmbedBuilder();
                offembed.AddField($"{discordmain._client.CurrentUser.Username} Bot Announcement", "Gen 7 Link Trade Bot is Offline");
                botchannelid.SendMessageAsync(embed: offembed.Build());
            }
       
         
        }
    }
}