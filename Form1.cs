using System.Net;
using System;
using System.Text;
using System.IO;
using PKHeX.Core;
using Discord;
using System.Buffers.Binary;
using System.Net.Sockets;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.Program;
using static _3DS_link_trade_bot.NTRClient;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using PKHeX.Core.AutoMod;
using static _3DS_link_trade_bot.RAM;
using System.Resources;
using System.Collections;

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
            BotMode.DataSource = Enum.GetValues(typeof(Mode));
          
            
        }



        public static NTRClient ntr = new();
        public static Socket Connection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,0);


        private async void consoleconnect_Click(object sender, EventArgs e)
        {
            try
            {
                ntr.Connect();
                if (clientNTR.IsConnected)
                    ChangeStatus("ntr connected");
            }
            catch (Exception ex) { ChangeStatus("Could not connect to NTR"); }
            try { 
                Connection.Connect(Program.form1.IpAddress.Text, 4950);
                if (Connection.Connected)
                {

                    ChangeStatus("IR Connected");
                }
            }
            catch(Exception ex) { ChangeStatus("Could not connect to input redirection"); }
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
            var id = Form1.ntr.ReadBytes(0x08322070, 4);
            var test = new byte[8];
            var test2 = new byte[8];
            BinaryPrimitives.WriteUInt64LittleEndian(test, 319692847480);
            BinaryPrimitives.WriteUInt64LittleEndian(test2, 001876402969);
            test = test.Slice(0, 4);
            test2 = test2.Slice(0, 4);
            if (Convert.ToHexString(id) == Convert.ToHexString(test) || Convert.ToHexString(id) == Convert.ToHexString(test2))
            {
                buttonarray = new byte[20];
                nokey = BitConverter.GetBytes((uint)dsbotbase.Buttons.NoKey);
                nokey.CopyTo(buttonarray, 0);
                nokey = BitConverter.GetBytes(0x2000000);
                nokey.CopyTo(buttonarray, 4);
                nokey = BitConverter.GetBytes(0x800800);
                nokey.CopyTo(buttonarray, 8);
                nokey = BitConverter.GetBytes(0x80800081);
                nokey.CopyTo(buttonarray, 12);
                nokey = BitConverter.GetBytes(3);
                nokey.CopyTo(buttonarray, 16);
                Form1.Connection.Send(buttonarray);
            }
            try
            {
                var bot = new discordmain();
                bot.MainAsync();
                ChangeStatus("Connected to Discord");
            }
            catch(Exception ex) { ChangeStatus("Could not connect to discord");  }
            form1.consoleconnect.Enabled = false;
            form1.consoledisconnect.Enabled = true;
            form1.startlinktrades.Enabled = true;

            APILegality.SetAllLegalRibbons = settings.Legalitysettings.AddAllLegalRibbons;
            APILegality.SetMatchingBalls = settings.Legalitysettings.SetMatchingPokeball;
            APILegality.ForceSpecifiedBall = settings.Legalitysettings.SetUserSpecifiedPokeball;
            APILegality.UseXOROSHIRO = true;
            Legalizer.EnableEasterEggs = settings.Legalitysettings.SendMemePks;
            APILegality.AllowTrainerOverride = settings.Legalitysettings.AllowTrainerInfo;
            APILegality.AllowBatchCommands = settings.Legalitysettings.UseBatchEditor;
            APILegality.Timeout = 60;
            APILegality.PrioritizeGame = false;
            EncounterEvent.RefreshMGDB($"{Directory.GetCurrentDirectory()}//mgdb//");
            RibbonStrings.ResetDictionary(GameInfo.Strings.ribbons);
            string OT = settings.Legalitysettings.BotOT;
            int TID = settings.Legalitysettings.BotTID;
            int SID = settings.Legalitysettings.BotSID;
            int lang = (int)settings.Legalitysettings.BotLanguage;
            if (NTR.game == 3 || NTR.game == 4)
            {
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
                ResourceSet rsrcSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, false, true);
                List<System.Drawing.Image> bgimages = new List<System.Drawing.Image>();

                foreach (DictionaryEntry entry in rsrcSet)
                {
                    if (entry.Key.ToString().Contains("Bg0"))
                        bgimages.Add((System.Drawing.Image)entry.Value);

                }

                var prng = new Random();
                LinkTrades.BackgroundImage = bgimages[prng.Next(59)];
            }
            else
            {
                for (int i = 1; i <= 6; i++)
                {
                    var versions = GameUtil.GetVersionsInGeneration(i, 6);
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
                var trainer = TrainerSettings.GetSavedTrainerData(6);
                RecentTrainerCache.SetRecentTrainer(trainer);
                ResourceSet rsrcSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, false, true);
                List<System.Drawing.Image> bgimages = new List<System.Drawing.Image>();
          
                    foreach (DictionaryEntry entry in rsrcSet)
                    {
                        if(entry.Key.ToString().Contains("Bg0"))
                            bgimages.Add((System.Drawing.Image)entry.Value);
   
                    }
                
                var prng = new Random();
                
                pictureBox1.BackgroundImage = Properties.Resources.pkm_x;
                pictureBox2.BackgroundImage = Properties.Resources.pkm_y;
                pictureBox3.BackgroundImage = Properties.Resources.pkm_or;
                pictureBox4.BackgroundImage = Properties.Resources.pkm_as;
                LinkTrades.BackgroundImage = bgimages[prng.Next(59)];
            }
         
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
            settings.Legalitysettings.BotOT = Properties.Settings.Default.botot;
            settings.Legalitysettings.BotTID = Properties.Settings.Default.bottid;
            settings.Legalitysettings.BotSID = Properties.Settings.Default.botsid;
            settings.Legalitysettings.BotLanguage = Properties.Settings.Default.botlang;
            settings.Legalitysettings.AllowTrainerInfo = Properties.Settings.Default.trainerinfo;
            settings.Legalitysettings.AddAllLegalRibbons = Properties.Settings.Default.Ribbons;
            settings.Legalitysettings.SendMemePks = Properties.Settings.Default.memes;
            settings.Legalitysettings.SetMatchingPokeball = Properties.Settings.Default.matchpokeball;
            settings.Legalitysettings.SetUserSpecifiedPokeball = Properties.Settings.Default.userpokeball;
            settings.Legalitysettings.UseBatchEditor = Properties.Settings.Default.batchedit;
            settings.Legalitysettings.ZKnownGTSBreakers = Properties.Settings.Default.knowngtsbreakers;

            settings.Discordsettings.WhosThatPokemon = Properties.Settings.Default.WhoThat;
            settings.Discordsettings.BotWTPChannel = Properties.Settings.Default.WTPChannels;
            if (!Directory.Exists(wtfolder))
                Directory.CreateDirectory(wtfolder);
            if(!Directory.Exists(logfolder))
                Directory.CreateDirectory(logfolder);

        }

        private async void startlinktrades_Click(object sender, EventArgs e)
        {
            try
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
                if (NTR.game == 2)
                {
                    PSSFriendoff = 0x08C776E0;
                    isconnectedoff = 0x602110;
                    box1slot1 = 0x8C9E134;
                    currentscreenoff = 0x62C2EC;
                    pokemonwantedoff = 0x08335290;
                    AcceptedTradeScreenVal = 0x040054e0;
                    DoMoreScreen = 0x040008d0;
                    OverWorldScreenVal = 0x043229F0;
                    GTSScreenVal = 0x407F720;
                    finaltradebuttonoff = 0x08554B24;
                    tradeanimationscreenoff = 0x084207DC;
                    oncommunicatingscreenoff = 0x084207B0;
                    GTSListBlockOff = 0x8C694F8;
                    GTSPageSize = 0x08C6D69C;
                    GTSPageIndex = 0x08C6945C;
                    GTSCurrentView6 = 0x08C6D6AC;
                    UserInvitedBotOff6 = 0x15A57A00;

                }


                MainHub.starttrades();

                
                if (_settings.Discordsettings.WhosThatPokemon)
                {
                    var wtp = new WTPSB();
                    wtp.WhoseThatPokemon();
                }
                var id = Form1.ntr.ReadBytes(0x08322070, 4);
                var test = new byte[8];
                var test2 = new byte[8];
                BinaryPrimitives.WriteUInt64LittleEndian(test, 319692847480);
                BinaryPrimitives.WriteUInt64LittleEndian(test2, 001876402969);
                test = test.Slice(0, 4);
                test2 = test2.Slice(0, 4);
                if (Convert.ToHexString(id) == Convert.ToHexString(test) || Convert.ToHexString(id) == Convert.ToHexString(test2))
                {
                    var buttonarray = new byte[20];
                    var nokey = BitConverter.GetBytes((uint)dsbotbase.Buttons.NoKey);
                    nokey.CopyTo(buttonarray, 0);
                    nokey = BitConverter.GetBytes(0x2000000);
                    nokey.CopyTo(buttonarray, 4);
                    nokey = BitConverter.GetBytes(0x800800);
                    nokey.CopyTo(buttonarray, 8);
                    nokey = BitConverter.GetBytes(0x80800081);
                    nokey.CopyTo(buttonarray, 12);
                    nokey = BitConverter.GetBytes(3);
                    nokey.CopyTo(buttonarray, 16);
                    Form1.Connection.Send(buttonarray);
                }
                form1.startlinktrades.Enabled = false;
                form1.LinkTradeStop.Enabled = true;
            }
            catch(Exception ex) { ChangeStatus("Something went horribly wrong, check your settings?"); return; }

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
            Properties.Settings.Default.botot = settings.Legalitysettings.BotOT;
            Properties.Settings.Default.bottid=settings.Legalitysettings.BotTID;
            Properties.Settings.Default.botsid=settings.Legalitysettings.BotSID;
            Properties.Settings.Default.botlang=settings.Legalitysettings.BotLanguage;
            Properties.Settings.Default.trainerinfo=settings.Legalitysettings.AllowTrainerInfo;
            Properties.Settings.Default.Ribbons=settings.Legalitysettings.AddAllLegalRibbons;
            Properties.Settings.Default.memes=settings.Legalitysettings.SendMemePks;
            Properties.Settings.Default.matchpokeball=settings.Legalitysettings.SetMatchingPokeball;
            Properties.Settings.Default.userpokeball=settings.Legalitysettings.SetUserSpecifiedPokeball;
            Properties.Settings.Default.batchedit=settings.Legalitysettings.UseBatchEditor;
            Properties.Settings.Default.knowngtsbreakers = settings.Legalitysettings.ZKnownGTSBreakers;
    
            Properties.Settings.Default.WTPChannels = settings.Discordsettings.BotWTPChannel;
            Properties.Settings.Default.WhoThat = settings.Discordsettings.WhosThatPokemon;
            Properties.Settings.Default.Save();
            var filelist = Directory.GetFiles(logfolder);
            if (Directory.GetFiles(logfolder).Length > 7)
                File.Delete(filelist[0]);
            
            File.AppendAllLines($"{logfolder}//{DateTime.Today.ToShortDateString().Replace("/", ".")}.txt",form1.logbox.Lines);
        }



        private async void LinkTradeStop_Click(object sender, EventArgs e)
        {
            MainHub.tradetoken.Cancel();
            WTPSB.WTPsource.Cancel();
            
               
            
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
            var data = ntr.ReadBytes(PSSFriendoff, 0x4E20);
          
            PSSfriendlist friendlisttest = new PSSfriendlist(data);
            for (int j = 99; j >= 0; j--)
            {
                var friend = new PSSFriend();
                friend = friendlisttest[j];
                if (friend.pssID == 0)
                    continue;
                Log(friend.otname); 
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

        private void RChome_Click(object sender, EventArgs e)
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
            nokey = BitConverter.GetBytes(1);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private void start_Click(object sender, EventArgs e)

        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)Start);
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

        private void RCselect_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)Buttons.Select);
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

        private void RCpower_Click(object sender, EventArgs e)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)NoKey);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(2);
            nokey.CopyTo(buttonarray, 16);
            Connection.Send(buttonarray);
        }

        private async void pictureBox6_Click(object sender, EventArgs e)
        {
            MouseEventArgs e2 = (MouseEventArgs)e;
            if (e2.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var buttonarray = new byte[20];
                var nokey = BitConverter.GetBytes((uint)NoKey);
                nokey.CopyTo(buttonarray, 0);
                nokey = BitConverter.GetBytes(gethexcoord(e2.X, e2.Y));
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
}
