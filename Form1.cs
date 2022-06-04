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
using PKHeX.Core.AutoMod;
using static _3DS_link_trade_bot.RAM;




namespace _3DS_link_trade_bot
{
    public partial class Form1 : Form
    {
        public static string wtfolder = $"{Directory.GetCurrentDirectory()}//WTFiles";
        public static string logfolder = $"{Directory.GetCurrentDirectory()}//logs";
        public Settings settings = new();
        public static Settings _settings;
        public Form1()
        {
            

            InitializeComponent();
            propertyGrid1.SelectedObject = settings;
          
            
        }



        public static NTRClient ntr = new();
        public static Socket Connection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,0);
      

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
            Connection.Send(buttonarray);
            _settings = settings;
         
            try {
                var bot = new discordmain();
                bot.MainAsync();
                ChangeStatus("Connected to Discord");
            }
            catch { ChangeStatus("Could not connect to discord"); }
            form1.consoleconnect.Enabled = false;
            form1.consoledisconnect.Enabled = true;
            form1.startlinktrades.Enabled = true;
        }
        public static void ChangeStatus(string text)
        {
            if(form1.logbox.Lines.Count() > 100)
            {
                var filelist = Directory.GetFiles(logfolder);
                if (Directory.GetFiles(logfolder).Length > 7)
                    File.Delete(filelist[0]);
                File.AppendAllLines($"{logfolder}//{DateTime.Today.ToShortDateString().Replace("/", ".")}.txt", form1.logbox.Lines);
                form1.logbox.Clear();
            }
            
            form1.statusbox.Text = text;
            form1.logbox.AppendText($"{DateTime.Now.ToLongTimeString()}: {text}\n");
            
        }
        public static async Task Log(string text)
        {
            if (form1.logbox.Lines.Count() > 100)
            {
                var filelist = Directory.GetFiles(logfolder);
                if (Directory.GetFiles(logfolder).Length > 7)
                    File.Delete(filelist[0]);
                File.AppendAllLines($"{logfolder}//{DateTime.Today.ToShortDateString().Replace("/", ".")}.txt", form1.logbox.Lines);
                form1.logbox.Clear();
            }
            form1.logbox.AppendText($"{DateTime.Now.ToLongTimeString()}: {text}\n");
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
            settings.Discordsettings.token = Properties.Settings.Default.discordtoken;
            settings.Discordsettings.BotTradeChannel = Properties.Settings.Default.botchannels;
            settings.FriendCode = Properties.Settings.Default.botfc;
            settings.PokemonWanted = Properties.Settings.Default.Pokemonwanted;
            settings.Discordsettings.BotWTChannel = Properties.Settings.Default.wtchannels;
            settings.GTSdistribution = Properties.Settings.Default.gtsbool;
            settings.WonderTrade = Properties.Settings.Default.wtbool;
            
            if (!Directory.Exists(wtfolder))
                Directory.CreateDirectory(wtfolder);
            if(!Directory.Exists(logfolder))
                Directory.CreateDirectory(logfolder);
            APILegality.SetAllLegalRibbons = false;
            APILegality.SetMatchingBalls = true;
            APILegality.ForceSpecifiedBall = true;
            APILegality.UseXOROSHIRO = true;
            Legalizer.EnableEasterEggs = false;
            APILegality.AllowTrainerOverride = true;
            APILegality.AllowBatchCommands = true;
            APILegality.Timeout = 30;
            APILegality.PrioritizeGame = false;
            EncounterEvent.RefreshMGDB($"{Directory.GetCurrentDirectory()}//mgdb//");
            RibbonStrings.ResetDictionary(GameInfo.Strings.ribbons);
            string OT = "pip";
            int TID = 54654;
            int SID = 45636;
            int lang = 2;
            for (int i = 1; i <= 7; i++)
            {
                var versions = GameUtil.GetVersionsInGeneration(i, 7);
                foreach (var v in versions)
                {
                    var fallback = new SimpleTrainerInfo(v)
                    {
                        Language = lang,
                        TID = TID,
                        SID = SID,
                        OT = OT,
                    };
                    var exist = TrainerSettings.GetSavedTrainerData(v, i, fallback);
                    if (exist is SimpleTrainerInfo) // not anything from files; this assumes ALM returns SimpleTrainerInfo for non-user-provided fake templates.
                        TrainerSettings.Register(fallback);
                }
            }

            var trainer = TrainerSettings.GetSavedTrainerData(7);
            RecentTrainerCache.SetRecentTrainer(trainer);
        }

        private void startlinktrades_Click(object sender, EventArgs e)
        {
            if (NTR.game == 3)
            {
                GTSpagesizeoff = 0x32A6A1A4;
                GTScurrentview = 0x305ea384;
                GTSpagesizeoff = 0x32A6A1A4;
                GTSblockoff = 0x32A6A7C4;
                box1slot1 = 0x330D9838;
                screenoff = 0x00674802;
                GTSDeposit = 0x32A6A180;
                Friendslistoffset = 0x30010F94;
                isconnectedoff = 0x318635CE;
                FailedTradeoff = 0x300FE0A0;
                OfferedPokemonoff = 0x006754CC;
                finalofferscreenoff = 0x307F7982;
                festscreenoff = 0x31883B7C;
                festscreendisplayed = 0xC8;
                tradevolutionscreenoff = 0x3002310C;
            }
            
           
            
          

            MainHub.starttrades();
         
            
            foreach (var channel in settings.Discordsettings.BotTradeChannel)
            {
                
                var botchannelid = (ITextChannel)discordmain._client.GetChannelAsync(channel).Result;
                if (botchannelid.Name.Contains("❌"))
                {
                    botchannelid.ModifyAsync(x => x.Name = botchannelid.Name.Replace("❌", "✅"));
                    botchannelid.AddPermissionOverwriteAsync(botchannelid.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Allow));
                    var offembed = new EmbedBuilder();
                    offembed.AddField($"{discordmain._client.CurrentUser.Username} Bot Announcement", "Gen 7 Link Trade Bot is Online");
                    botchannelid.SendMessageAsync("<@&898900914348372058>", embed: offembed.Build());
                }
            }
            form1.startlinktrades.Enabled = false;
            form1.LinkTradeStop.Enabled = true;

            
        }

        private void discordconnect_Click(object sender, EventArgs e)
        {
            var bot = new discordmain();
            bot.MainAsync();
            ChangeStatus("Connected to Discord");
           
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IpAddress = form1.IpAddress.Text;
            Properties.Settings.Default.discordtoken = settings.Discordsettings.token;
            Properties.Settings.Default.botchannels = settings.Discordsettings.BotTradeChannel;
            Properties.Settings.Default.botfc = settings.FriendCode;
            Properties.Settings.Default.Pokemonwanted = settings.PokemonWanted;
            Properties.Settings.Default.wtchannels = settings.Discordsettings.BotWTChannel;
            Properties.Settings.Default.gtsbool = settings.GTSdistribution;
            Properties.Settings.Default.wtbool = settings.WonderTrade;
            Properties.Settings.Default.Save();
            var filelist = Directory.GetFiles(logfolder);
            if (Directory.GetFiles(logfolder).Length > 7)
                File.Delete(filelist[0]);
            
            File.AppendAllLines($"{logfolder}//{DateTime.Today.ToShortDateString().Replace("/", ".")}.txt",form1.logbox.Lines);
        }



        private void LinkTradeStop_Click(object sender, EventArgs e)
        {
            MainHub.tradetoken.Cancel();
            
            foreach (var channel in settings.Discordsettings.BotTradeChannel)
            {
                
                var botchannelid = (ITextChannel)discordmain._client.GetChannelAsync(channel).Result;
                botchannelid.ModifyAsync(x => x.Name = botchannelid.Name.Replace("✅", "❌"));
                botchannelid.AddPermissionOverwriteAsync(botchannelid.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                var offembed = new EmbedBuilder();
                offembed.AddField($"{discordmain._client.CurrentUser.Username} Bot Announcement", "Gen 7 Link Trade Bot is Offline");
                botchannelid.SendMessageAsync(embed: offembed.Build());
            }
            form1.startlinktrades.Enabled = true;
            form1.LinkTradeStop.Enabled = false;
         
        }



        private void consoledisconnect_Click(object sender, EventArgs e)
        {
            ntr.Disconnect();
            form1.consoledisconnect.Enabled = false;
            form1.startlinktrades.Enabled = false;
            form1.LinkTradeStop.Enabled = false;
            form1.consoleconnect.Enabled = true;
        }
        
         private void button1_Click(object sender, EventArgs e)
        {
            //this reads the PSS in gen 6 and gives me a list of the trainer names, this is basically the start of gen 6!!
            ulong blocksize = 0x4e30;
            ulong off = 0x08C6FFDC;
            var data = ntr.ReadBytes(off, 0x4E20);
            for (int i = 0; i < 2; i++)
            {
                PSSfriendlist friendlisttest = new PSSfriendlist(data);
                for (int j = 0; j < 100; j++)
                {
                    var friend = friendlisttest[j];
                    if (friend.pssID == 0)
                        break;
                    Log(friend.otname);
                }
                off += blocksize;
                data = ntr.ReadBytes(off, 0x4E20);
            }
        }

        private async void RCup_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)DpadUP);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }
        private async void RC_Release(object sender, EventArgs e)
        {
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
            Connection.Send(buttonarray);


        }

        private void RCleft_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)DpadLEFT);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void RCdown_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)DpadDOWN);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void RCright_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)DpadRIGHT);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void RCx_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)X);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void RCy_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)Y);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void RCb_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)B);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void RCa_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)A);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

  
    }
}
