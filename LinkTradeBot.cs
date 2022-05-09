using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;


namespace _3DS_link_trade_bot
{
    public class LinkTradeBot
    {
        public static queuesystem tradeinfo;
        public static CancellationTokenSource tradetoken = new CancellationTokenSource();
        public static Queue<queuesystem> The_Q = new Queue<queuesystem>();
        public static async void starttrades()
        {
            while (!tradetoken.IsCancellationRequested)
            {
                while (The_Q.Count == 0)
                {
                    await Task.Delay(25);
                }
                tradeinfo = The_Q.Peek();
                The_Q.Dequeue();
                switch (tradeinfo.mode)
                {
                    case botmode.addfc: await FriendCodeRoutine(); return;
                }
            }
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
