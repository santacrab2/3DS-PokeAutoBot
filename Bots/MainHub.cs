using System;
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


namespace _3DS_link_trade_bot
{
    public class MainHub
    {
        public static List<string> guestlist = new();
        public static queuesystem tradeinfo;
        public static CancellationTokenSource tradetoken = new CancellationTokenSource();
        public static Queue<queuesystem> The_Q = new Queue<queuesystem>();
        
        public static async void starttrades()
        {
            while (!tradetoken.IsCancellationRequested)
            {
                if(NTR.game == 3)
                {
                    GTSpagesizeoff = 0x32A6A1A4;
                    GTScurrentview = 0x305ea384;
                    GTSpagesizeoff = 0x32A6A1A4;
                    GTSblockoff = 0x32A6A7C4;
                    box1slot1 = 0x330D9838; 
                    screenoff = 0x00674802;
                    GTSDeposit = 0x32A6A180;
                    
                }
                    
                try {
                    //this is where it performs idling tasks
                    if (The_Q.Count == 0)
                    {
                        await click(X, 1);
                        await click(X, 1);
                        if (_settings.GTSdistribution == true)
                            await GTSBot.GTStrades();
                        await Task.Delay(5);
                        continue;


                    }
                    tradeinfo = The_Q.Dequeue();

                    switch (tradeinfo.mode)
                    {
                        case botmode.addfc: await FriendCodeRoutine(); continue;
                        case botmode.trade: await LinkTradeRoutine(); continue;

                    }
                }
                catch
                {
                    ChangeStatus("Bot Down");
                    foreach (var channel in _settings.Discordsettings.BotChannel)
                    {

                        var botchannelid = (ITextChannel)discordmain._client.GetChannelAsync(channel).Result;
                        await botchannelid.ModifyAsync(x => x.Name = botchannelid.Name.Replace("✅", "❌"));
                        await botchannelid.AddPermissionOverwriteAsync(botchannelid.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                        var offembed = new EmbedBuilder();
                        offembed.AddField($"{discordmain._client.CurrentUser.Username} Bot Announcement", "Gen 7 Link Trade Bot is Offline");
                        await botchannelid.SendMessageAsync(embed: offembed.Build());
                    }
                    tradetoken.Cancel();
                    
                   
                }
            }
            tradetoken = new();
        }

      


    }
}
