using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                while (The_Q.Count == 0)
                {
                    await click(X, 1);
                    await click(X, 1);
                }
                tradeinfo = The_Q.Peek();
                The_Q.Dequeue();
                switch (tradeinfo.mode)
                {
                    case botmode.addfc: await FriendCodeRoutine(); continue;
                    case botmode.trade: await LinkTradeRoutine();continue;

                }
            }
        }

        public static void getfriendlist() 
        {
            var data = ntr.ReadBytes(Friendslistoffset, FriendListSize);
            FriendList list = new FriendList(data);
            for(int i = 0; i < FriendList.numofguests; i++)
            {
                var friend = list[i];
                guestlist.Add(friend.friendname);
           
            }

        }
        public static async Task LinkTradeRoutine()
        {
            if (!isconnected)
            {
                await touch(296, 221,3);
                await click(A,1);
                await click(A, 10);
                await click(A, 5);
                await click(A, 10);
                if (!isconnected)
                {
                    return;
                }
            }
            await touch(233, 119, 1);
            await touch(161, 83, 3);
            getfriendlist();
            await Task.Delay(5000);
            int downpresses = 0;
            for(int j =0;j< FriendList.numofguests; j++)
            {
                if (guestlist[j].Contains(tradeinfo.IGN))
                {
                    downpresses = j;
                    break;
                }
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


        }
        public static async Task FriendCodeRoutine()
        {
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
