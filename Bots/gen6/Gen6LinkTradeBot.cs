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

namespace _3DS_link_trade_bot
{
    public class Gen6LinkTradeBot
    {
        public static int friendindex = 0;
        public static async Task LinkTradeRoutine6()
        {
            ChangeStatus("starting a Link Trade");
            await tradeinfo.discordcontext.User.SendMessageAsync("starting your trade now, be prepared to accept the invite!");
            if (!isconnected6)
            {
                await touch(227, 15, 2);
                await touch(164, 130, 2);
                await click(A, 60);
            }

            var friends = getfriendlist6();
            friendindex = 0;
            for(int j = 0; j < friends.Count(); j++)
            {
                if (friends[j].Contains(tradeinfo.IGN))
                {
                    friendindex = j;
                    break;
                }
            }
            if(friendindex <= 5)
            {
                await touch(25 + (friendindex * 45), 74, 2);
            }
            else
            {
                int scrollcount = friendindex - 5;
                int drag = 0;
                while(scrollcount > 0)
                {
                    while (drag < 55)
                    {
                        var buttonarray2 = new byte[20];
                        var nokey2 = BitConverter.GetBytes((uint)NoKey);
                        nokey2.CopyTo(buttonarray2, 0);
                        nokey2 = BitConverter.GetBytes(gethexcoord(270 - drag, 74));
                        nokey2.CopyTo(buttonarray2, 4);
                        nokey2 = BitConverter.GetBytes(0x800800);
                        nokey2.CopyTo(buttonarray2, 8);
                        nokey2 = BitConverter.GetBytes(0x80800081);
                        nokey2.CopyTo(buttonarray2, 12);
                        nokey2 = BitConverter.GetBytes(0);
                        nokey2.CopyTo(buttonarray2, 16);
                        Connection.Send(buttonarray2);
                        drag++;
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
                    scrollcount--;
                }
                await touch(25 + (5 * 45), 74, 2);
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
