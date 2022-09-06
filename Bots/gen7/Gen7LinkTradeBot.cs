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
using static _3DS_link_trade_bot.MainHub;

namespace _3DS_link_trade_bot
{
    public class Gen7LinkTradeBot
    {
        public static async Task LinkTradeRoutine()
        {
            
            ChangeStatus("starting a Link Trade");
            await tradeinfo.discordcontext.User.SendMessageAsync("starting your trade now, be prepared to accept the invite!");
            if (IsSoftBanned)
            {
                ChangeStatus("softban detected, restarting game");
                await PressPower(1);
                await presshome(10);
                await click(A, 15);
                await click(A, 7);
            }
            if (!infestivalplaza)
            {
                ChangeStatus("entering festival plaza");
                await click(X, 1);
                await touch(229, 171, 10);
                await touch(296, 221, 5);
                await click(B, 10);

            }
            if (!isconnected)
            {
                ChangeStatus("connecting to the internet");
                await touch(296, 221, 5);
                await click(A, 2);
                await click(A, 20);
                await click(A, 5);
                await click(A, 15);
               
            }
            if (userinvitedbot)
                await click(X, 1);
            await injection(tradeinfo.tradepokemon);
            await touch(233, 119, 1);
            await touch(161, 83, 3);
            guestlist = getfriendlist();
            await Task.Delay(5000);
            int downpresses = 50;
            ChangeStatus("reading friend list");
            for (int j = 0; j < FriendList.numofguests; j++)
            {
                if (guestlist[j].Contains(tradeinfo.IGN))
                {
                    downpresses = j;
                    break;
                }

            }
            if (downpresses == 50)
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} not found");
                await tradeinfo.discordcontext.User.SendMessageAsync("I could not find you on the guest list, Please refresh your internet connection and try again!");
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
            while (stop.ElapsedMilliseconds <= 60_000 && ntr.ReadBytes(FailedTradeoff, 1)[0] != 0xFF && !failedtrade)
                await Task.Delay(25);
            if (failedtrade)
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} failed to accept the trade offer");
                await tradeinfo.discordcontext.User.SendMessageAsync("something went wrong, please try again!");
                await click(B, 2);
                await click(B, 2);
                return;
            }

            await Task.Delay(5_000);
            await click(A, 1);
            await click(A, 1);
            stop.Restart();
            while (ntr.ReadBytes(finalofferscreenoff, 1)[0] != 0x4F)
            {
                if (stop.ElapsedMilliseconds > 30_000)
                {
                    for (int i = 0; i < 3; i++)
                        await click(B, 2);
                    await click(A, 5);
                    return;
                }
                await Task.Delay(25);
            }
            ChangeStatus("link trading");
            await click(A, 10);
            stop.Restart();
            while ((!onboxscreen && !infestivalplaza))
            {
                await Task.Delay(500);
                if (tradeevolution)
                    break;
            }
            if (tradeevolution)
            {
                while (!onboxscreen && !infestivalplaza)
                    await click(A, 2);
            }
            var tradedpkbytes = ntr.ReadBytes(box1slot1, 232);
            var tradedpk = new PK7(tradedpkbytes);
            if (SearchUtil.HashByDetails(tradedpk) == SearchUtil.HashByDetails(tradeinfo.tradepokemon))
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} did not complete the trade");
                await tradeinfo.discordcontext.User.SendMessageAsync("Something went wrong, please try again");

            }
            else
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} completed the trade");
                var temp = $"{Directory.GetCurrentDirectory()}/{tradedpk.Species}.pk7";
                File.WriteAllBytes(temp, tradedpk.DecryptedBoxData);
                await tradeinfo.discordcontext.User.SendFileAsync(temp, "Here is the pokemon you traded me");
                File.Delete(temp);
            }
            ChangeStatus("Link Trade Complete");
            await click(B, 1);
            await click(A, 10);
            return;

        }
        public static async Task injection(PKM pk, bool party1slot1=false)
        {
            if (party1slot1)
            {
                pk.ResetPartyStats();
                ntr.WriteBytes(pk.EncryptedPartyData, Party1Slot1);
            }
            else
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
            ChangeStatus($"Adding {tradeinfo.discordcontext.User.Username} with friend code: {tradeinfo.friendcode}");
            await tradeinfo.discordcontext.User.SendMessageAsync("adding you to the friends list now!");
            await presshome(5);

            await touch(120, 10, 1);

            await touch(120, 10, 6);
            await click(A, 2);
            await touch(160, 10, 3);

            await touch(160, 180, 5);

            await enterfriendcode(tradeinfo.friendcode);
            await Task.Delay(2000);
            await touch(240, 230, 10);

            await touch(180, 100, 1);

            await touch(240, 230, 3);

            await touch(240, 200, 5);

            await presshome(10);

            await presshome(5);
            await click(A, 5);
            await click(A, 1);
      

        }
        public static async Task DumpRoutine()
        {
            ChangeStatus("starting a Link Trade");
            await tradeinfo.discordcontext.User.SendMessageAsync("starting your dump trade now, be prepared to accept the invite! You have 30 seconds to get the files you need before I quit.");
            if (!infestivalplaza)
            {
                await click(X, 1);
                await touch(229, 171, 10);
                await touch(296, 221, 10);
                await click(B, 10);

            }
            while (!isconnected)
            {
                ChangeStatus("connecting to the internet");
                await touch(296, 221, 5);
                await click(A, 2);
                await click(A, 30);
                await click(A, 5);
                await click(A, 20);

            }
            await touch(233, 119, 1);
            await touch(161, 83, 3);
            guestlist = getfriendlist();
            await Task.Delay(5000);
            int downpresses = 50;
            ChangeStatus("reading friend list");
            for (int j = 0; j < FriendList.numofguests; j++)
            {
                if (guestlist[j].Contains(tradeinfo.IGN))
                {
                    downpresses = j;
                    break;
                }

            }
            if (downpresses == 50)
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} not found");
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
            while (stop.ElapsedMilliseconds <= 30_000 && ntr.ReadBytes(FailedTradeoff, 1)[0] != 0xFF && !failedtrade)
                await Task.Delay(25);
            if (failedtrade)
            {
                await click(B, 2);
                await click(B, 2);
            }

            await Task.Delay(5_000);
            var initialpokebytes = ntr.ReadBytes(OfferedPokemonoff, 232);
            var econtext = EntityContext.Gen6;
            if (NTR.game > 2)
                econtext = EntityContext.Gen7;
            var initpoke = EntityFormat.GetFromBytes(initialpokebytes,econtext);
            var temppath = $"{Path.GetTempPath()}//{initpoke.FileName}";
            File.WriteAllBytes(temppath, initpoke.DecryptedBoxData);
            await tradeinfo.discordcontext.User.SendFileAsync(temppath, "Here is the pokemon you showed me.");
            File.Delete(temppath);
            stop.Restart();
            while(stop.ElapsedMilliseconds < 30_000)
            {
                var newpokebytes = ntr.ReadBytes(OfferedPokemonoff, 232);
                var newpoke = EntityFormat.GetFromBytes(newpokebytes);
                if(SearchUtil.HashByDetails(newpoke) != SearchUtil.HashByDetails(initpoke))
                {
                    temppath = $"{Path.GetTempPath()}//{newpoke.FileName}";
                    File.WriteAllBytes(temppath, newpoke.DecryptedBoxData);
                    await tradeinfo.discordcontext.User.SendFileAsync(temppath, "Here is the pokemon you showed me.");
                    File.Delete(temppath);
                    initpoke = newpoke;
                }
            }
            await click(B, 1);
            await click(A, 10);
            return;

        }
    }
}
