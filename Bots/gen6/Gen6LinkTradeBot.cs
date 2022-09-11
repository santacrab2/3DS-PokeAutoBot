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
using static _3DS_link_trade_bot.MainHub;
using static _3DS_link_trade_bot.Gen7LinkTradeBot;
namespace _3DS_link_trade_bot
{
    public class Gen6LinkTradeBot
    {
        public static int friendindex = 0;
        public static async Task LinkTradeRoutine6()
        {
            
            ChangeStatus($"starting a Link Trade with {tradeinfo.discordcontext.User.Username}");
            await tradeinfo.discordcontext.User.SendMessageAsync("starting your trade now, be prepared to accept the invite!");
       
            if (!isconnected6)
            {
                await touch(227, 15, 2);
                await touch(164, 130, 2);
                await click(A, 60);
            }
            if (userinvitedbot6)
                await touch(178, 213, 1);
            await injection(tradeinfo.tradepokemon);
            var trainersearch = "";
            var trainersearch2 = "";
            
            var friendindex = 0;
            while(friendindex < 6)
            {
                await touch(25 + (friendindex * 45), 74, 5);
                trainersearch = Encoding.Unicode.GetString(ntr.ReadBytes(SelectedFriendoff, 24)).Trim('\0');
                trainersearch2 = Encoding.Unicode.GetString(ntr.ReadBytes(secondSelectedFriendoff, 24)).Trim('\0');

                if (trainersearch.Contains(tradeinfo.IGN) || trainersearch2.Contains(tradeinfo.IGN))
                    break;
                await click(B, 1);
                friendindex++;
            }
            if (friendindex == 6)
            {
                await tradeinfo.discordcontext.User.SendMessageAsync("Could not find you on my friend list, refresh your internet connection in game and  try again!");
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} not found");
                
                return;
            }
            await touch(214, 114, 1);
            await touch(156, 157, 2);
            await click(A, 1);
            var stop = new Stopwatch();
            stop.Restart();
            var msgsent = false;
            ChangeStatus("waiting for trade partner to accept invite");
            while(!checkscreen(currentscreenoff,AcceptedTradeScreenVal) && stop.ElapsedMilliseconds < 35_000)
            {
                await Task.Delay(500);
                if(stop.ElapsedMilliseconds > 20_000 && !msgsent)
                {
                    await tradeinfo.discordcontext.User.SendMessageAsync("Accept the Trade invite or the game will cancel it in 10 seconds");
                    msgsent = true;
                }
            }
            if(stop.ElapsedMilliseconds > 35_000)
            {
                await tradeinfo.discordcontext.User.SendMessageAsync("You did not accept the trade invite in time, please try again.");
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} did not accept the trade invite in time");
                await click(B, 2);
                await click(B, 1);
                return;
            }
            ChangeStatus($"{tradeinfo.discordcontext.User.Username} accepted the trade, waiting for box screen");
            stop.Restart();
            while (checkscreen(currentscreenoff, AcceptedTradeScreenVal) && stop.ElapsedMilliseconds < 60_000)
                await Task.Delay(1000);
            
            ChangeStatus($"offering {(Species)tradeinfo.tradepokemon.Species} for trade");
      
            stop.Restart();
            await Task.Delay(10_000);
            await DpadClick(DpadRIGHT, 2);
            await DpadClick(DpadRIGHT, 2);
            while (!ontradeanimationscreen && stop.ElapsedMilliseconds < 60_000)
                await click(A,1);
            ChangeStatus("watching trade animation");
            stop.Restart();
            while ((ontradeanimationscreen || oncommunicatingscreen)&& stop.ElapsedMilliseconds<180_000)
                await Task.Delay(1000);
            await Task.Delay(10000);
            ChangeStatus("exiting trade");
            await click(B, 1);
            await click(A, 1);
            ChangeStatus("waiting for trade partner to exit");
            while (!checkscreen(currentscreenoff, DoMoreScreen)&&stop.ElapsedMilliseconds<180_000)
                await Task.Delay(1000);
            await touch(165, 170, 2);
            stop.Restart();
            while (!checkscreen(currentscreenoff, OverWorldScreenVal) && stop.ElapsedMilliseconds<60_000)
                await Task.Delay(1000);
            var receivedpkm = EntityFormat.GetFromBytes(ntr.ReadBytes(box1slot1, 260), EntityContext.Gen6);
            if(SearchUtil.HashByDetails(tradeinfo.tradepokemon) != SearchUtil.HashByDetails(receivedpkm))
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} completed the trade");
                var temp = $"{Directory.GetCurrentDirectory()}/{receivedpkm.Species}.pk6";
                File.WriteAllBytes(temp, receivedpkm.DecryptedBoxData);
                await tradeinfo.discordcontext.User.SendFileAsync(temp, "Here is the pokemon you traded me");
                File.Delete(temp);
            }
            else
            {
                ChangeStatus($"{tradeinfo.discordcontext.User.Username} did not complete the trade");
                await tradeinfo.discordcontext.User.SendMessageAsync("Something went wrong, please try again");
            }
            return;
        }
       
        public static List<string> getfriendlist6()
        {
           
            var the_l = new List<string>();
            var data = ntr.ReadBytes(PSSFriendoff, PSSDataSize);
            PSSfriendlist list = new PSSfriendlist(data);
            for (int i = 99; i >= 0; i--)
            {
                var friend = new PSSFriend();
                friend = list[i];
                if (friend.pssID == 0)
                    continue;
                the_l.Add(friend.otname);
               
            }
            return the_l;

        }
    }
}
