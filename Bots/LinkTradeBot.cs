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


namespace _3DS_link_trade_bot
{
    public class LinkTradeBot
    {
        public static List<string> guestlist = new();
        public static queuesystem tradeinfo;
        public static CancellationTokenSource tradetoken = new CancellationTokenSource();
        public static Queue<queuesystem> The_Q = new Queue<queuesystem>();
        
        public static async void starttrades()
        {
            while (!tradetoken.IsCancellationRequested)
            {
                //this is where it performs idling tasks
                if(The_Q.Count == 0)
                {
                    await click(X, 1);
                    await click(X, 1);
                    if(_settings.GTSdistribution == true)
                        await GTSBot.GTStrades();
                    await Task.Delay(5);
                    continue;
                }
                tradeinfo = The_Q.Peek();
                The_Q.Dequeue();
                switch (tradeinfo.mode)
                {
                    case botmode.addfc: await FriendCodeRoutine(); continue;
                    case botmode.trade: await LinkTradeRoutine();continue;

                }
            }
            tradetoken = new();
        }

        public static async Task LinkTradeRoutine()
        {
            ChangeStatus("starting a Link Trade");
            await tradeinfo.discordcontext.User.SendMessageAsync("starting your trade now, be prepared to accept the invite!");
            if (!isconnected)
            {
                ChangeStatus("connecting to the internet");
                await touch(296, 221,3);
                await click(A,1);
                await click(A, 20);
                await click(A, 15);
                await click(A, 20);
                if (!isconnected)
                {
                    return;
                }
            }
            await injection(tradeinfo.tradepokemon);
            await touch(233, 119, 1);
            await touch(161, 83, 3);
            guestlist = getfriendlist();
            await Task.Delay(5000);
            int downpresses = 50;
            ChangeStatus("reading friend list");
            for(int j =0;j< FriendList.numofguests; j++)
            {
                if (guestlist[j].Contains(tradeinfo.IGN))
                {
                    downpresses = j;
                    break;
                }

            }
            if (downpresses ==50)
            {
                ChangeStatus("user not found");
                await tradeinfo.discordcontext.User.SendMessageAsync("I could not find you on the trade list, Please refresh your internet connection and try again!");
                await click(B, 1);
                await click(B, 5);
                return;
            }
            for (int f = 0; f < downpresses; f++)
                await DpadClick(DpadDOWN, 1);
            await click(A, 1);
            await click(A, 1);
            Stopwatch stop = new();
            stop.Restart();
            while (stop.ElapsedMilliseconds <= 30_000 && ntr.ReadBytes(FailedTradeoff, 1)[0] != 0xFF&&!failedtrade)
                await Task.Delay(25);
            if (failedtrade)
            {
                await click(B, 2);
                await click(B, 2);
            }
               
            await Task.Delay(5_000);
            await click(A, 1);
            await click(A, 1);
            stop.Restart();
            while(ntr.ReadBytes(finalofferscreenoff,1)[0] != 0x4F)
            {
                if(stop.ElapsedMilliseconds > 30_000)
                {
                    for(int i = 0; i < 3; i++)
                        await click(B, 2);
                    await click(A, 3);
                    return;
                }
                await Task.Delay(25);
            }
            ChangeStatus("link trading");
            await click(A, 10);
            //stop.Restart();
            while(!onboxscreen)
            {
                await click(A, 5);
            }
            var tradedpkbytes = ntr.ReadBytes(box1slot1, 232);
            var tradedpk = new PK7(tradedpkbytes);
            if (SearchUtil.HashByDetails(tradedpk) == SearchUtil.HashByDetails(tradeinfo.tradepokemon))
            {
                ChangeStatus("user did not complete the trade");
                await tradeinfo.discordcontext.User.SendMessageAsync("Something went wrong, please try again");
               
            }
            else
            {
                ChangeStatus("user completed the trade");
                var temp = $"{Directory.GetCurrentDirectory()}/{tradedpk.FileName}";
                File.WriteAllBytes(temp, tradedpk.DecryptedBoxData);
                await tradeinfo.discordcontext.User.SendFileAsync(temp, "Here is the pokemon you traded me");
                File.Delete(temp);
            }
            ChangeStatus("Link Trade Complete");
            await click(B, 1);
            await click(A, 1);
            return;

        }
        public static async Task injection(PKM pk)
        {
            ntr.WriteBytes(pk.EncryptedBoxData, box1slot1);
        }
        public static List<string> getfriendlist()
        {
            var the_l = new List<string>();
            var data = ntr.ReadBytes(Friendslistoffset, FriendListSize);
            FriendList list = new FriendList(data);
            for (int i = 0; i < FriendList.numofguests; i++)
            {
                var friend = list[i];
                the_l.Add(friend.friendname);
                
            }
            return the_l;

        }
        public static async Task FriendCodeRoutine()
        {
            await tradeinfo.discordcontext.User.SendMessageAsync("adding you to the friends list now!");
            await presshome(2);

            await touch(120, 10,1);

            await touch(120, 10,5);

            await touch(160, 10,3);

            await touch(160, 180,5);
          
            await enterfriendcode(tradeinfo.friendcode);
            await Task.Delay(2000);
            await touch(240, 230,10);

            await touch(180, 100,1);

            await touch(240, 230,3);

            await touch(240, 200,5);

            await click(X,5);

            await presshome(1);
           
        }


    }
}
