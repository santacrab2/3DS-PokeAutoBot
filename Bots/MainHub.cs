﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using PKHeX.Core;
using PKHeX.Core.AutoMod;
using PKHeX.Core.Searching;
using System.Diagnostics;
using static _3DS_link_trade_bot.Form1;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using static _3DS_link_trade_bot.RAM;
using static _3DS_link_trade_bot.Gen7LinkTradeBot;
using static _3DS_link_trade_bot.Gen6LinkTradeBot;

namespace _3DS_link_trade_bot
{
    public class MainHub
    {
        public static List<PKM> wtmons = new();
        public static List<string> guestlist = new();
        public static queuesystem tradeinfo;
        public static CancellationTokenSource tradetoken = new CancellationTokenSource();
        public static Queue<queuesystem> The_Q = new Queue<queuesystem>();

        public static async void starttrades()
        {
            await Log("loading WT mons");
            var gamecontext = EntityContext.Gen6;
            if (NTR.game > 2)
                gamecontext = EntityContext.Gen7;
            var wtfiles = Directory.GetFiles(wtfolder);
            if (wtfiles.Length > 0)
            {
                foreach (var wtfile in wtfiles)
                {
                    var wtpkm = EntityFormat.GetFromBytes(File.ReadAllBytes(wtfile),gamecontext);
                    if (!new LegalityAnalysis(wtpkm).Valid || wtpkm.FatefulEncounter == true)
                    {
                        await Log($"{wtfile} not added due to being illegal or untradeable");
                        continue;
                    }

                    wtmons.Add(wtpkm);
                }
            }
            if (wtmons.Count() == 0)
                await Log("No Wonder Trade Pokemon Loaded");
            switch ((Mode)Program.form1.BotMode.SelectedItem)
            {
                case Mode.FlexTrade: await FlexTradeRoutine(); break;
                case Mode.GTSOnly:
                    while (!tradetoken.IsCancellationRequested)
                    {
                        if (NTR.game == 1 || NTR.game == 2)
                        {
                            await GTSBot6.GTSRoutine6();
                           


                        }
                        else
                        {
                            await GTSBot.GTStrades();
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                _settings.Legalitysettings.ZKnownGTSBreakers.Add(GTSBot.LastGTSTrainer.ToLower());

                                await resetgame();
                            }
                        }
                    } break;
                case Mode.WTOnly:
                    while (!tradetoken.IsCancellationRequested)
                    {
                        if (NTR.game == 1 || NTR.game == 2)
                        {
                            await WTBot6.WTRoutine6();
                           
                        }
                        else
                        {
                            await WTBot.WTroutine();
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                await resetgame();
                            }
                        }
                    } break;
                case Mode.GTSWTOnly:
                    while (!tradetoken.IsCancellationRequested)
                    {
                        if (NTR.game == 1 || NTR.game == 2)
                        {
                            await GTSBot6.GTSRoutine6();
                            

                        }
                        else
                        {
                            await GTSBot.GTStrades();
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                _settings.Legalitysettings.ZKnownGTSBreakers.Add(GTSBot.LastGTSTrainer.ToLower());

                                await resetgame();
                            }
                        }
                        if (NTR.game == 1 || NTR.game == 2)
                        {
                            await WTBot6.WTRoutine6();
                            
                        }
                        else
                        {
                            await WTBot.WTroutine();
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                await resetgame();
                            }
                        }
                    } break;
                case Mode.EggRNGNonePID:
                
                        await EggRNGBot7.EggRNGNonePID7Routine();
                     break;

            } 
            ChangeStatus("Bot Stopped");
            tradetoken = new();
            Program.form1.startlinktrades.Enabled = true;
            Program.form1.LinkTradeStop.Enabled = false;
        }

        public static async Task FlexTradeRoutine()
        {
            while (!tradetoken.IsCancellationRequested)
            {
                try
                {
                    //this is where it performs idling tasks
                    if (The_Q.Count == 0)
                    {
                        if (NTR.game == 3 || NTR.game == 4)
                        {
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                await resetgame();
                            }
                            if (_settings.GTSdistribution == true)
                                await GTSBot.GTStrades();
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                _settings.Legalitysettings.ZKnownGTSBreakers.Add(GTSBot.LastGTSTrainer.ToLower());

                                await resetgame();
                            }
                            if (_settings.WonderTrade == true)
                                await WTBot.WTroutine();
                            if (IsSoftBanned)
                            {
                                ChangeStatus("softban detected, restarting game");
                                await resetgame();
                            }
                        }
                        else
                        {
                           
                            if (_settings.GTSdistribution == true)
                                 await GTSBot6.GTSRoutine6();
                         
                            if (_settings.WonderTrade == true)
                                await WTBot6.WTRoutine6();
                        
                        }
                        await Task.Delay(1000);
                        continue;


                    }
                    tradeinfo = The_Q.Dequeue();
                    if (NTR.game == 1 || NTR.game == 2)
                    {
                        switch (tradeinfo.mode)
                        {
                            case botmode.addfc: await FriendCodeRoutine(); continue;
                            case botmode.trade: await LinkTradeRoutine6(); continue;
                            case botmode.dump: continue;

                        }
                    }
                    else
                    {
                        switch (tradeinfo.mode)
                        {
                            case botmode.addfc: await FriendCodeRoutine(); continue;
                            case botmode.trade: await LinkTradeRoutine(); continue;
                            case botmode.dump: await DumpRoutine(); continue;

                        }
                    }
                }
                catch (Exception ex)
                {
                    await Log(ex.ToString());
                    ChangeStatus("Bot DTF");
                    if (_settings.Discordsettings.SendStatusMessage)
                    {
                        foreach (var channel in _settings.Discordsettings.BotTradeChannel)
                        {

                            var botchannelid = (ITextChannel)discordmain._client.GetChannelAsync(channel).Result;
                            await botchannelid.ModifyAsync(x => x.Name = botchannelid.Name.Replace("✅", "❌"));
                            await botchannelid.AddPermissionOverwriteAsync(botchannelid.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                            var offembed = new EmbedBuilder();
                            offembed.AddField($"{discordmain._client.CurrentUser.Username} Bot Announcement", "Link Trade Bot is Offline");
                            await botchannelid.SendMessageAsync(embed: offembed.Build());
                        }
                    }
                    WTPSB.WTPsource.Cancel();
                    tradetoken.Cancel();
                }
            }
        }
      public static async Task resetgame()
        {
            await PressPower(5);
            await presshome(10);
            await click(A, 15);
            await click(A, 7);
            await click(A, 7);
            ntr.Disconnect();
            await Task.Delay(2000);
            ntr.Connect();
            await Task.Delay(5000);
            if (NTR.game==1 || NTR.game == 2)
            {
                await click(A, 5);
                await click(A, 5);
                await touch(120, 54, 2);
            }
        }


    }
}
