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
        public static async void starttrades()
        {
            presshome();
            await Task.Delay(2000);
            touch(120, 10);
            await Task.Delay(1000);
            touch(120, 10);
            await Task.Delay(5000);
            touch(160, 10);
            await Task.Delay(3000);
            touch(160, 180);
            await Task.Delay(5000);
            string fc = "367562863092";
            enterfriendcode(fc);
        }
    }
}
