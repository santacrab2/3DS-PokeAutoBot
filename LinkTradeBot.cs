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
            for(int i = 0; i < fc.Length; i++)
            {
                numpadx numberkeyx = numpadx.one;
                numbpady numberkeyy = numbpady.one;
                switch (fc[i])
                {
                    case '1': numberkeyx = numpadx.one; break;
                    case '2':numberkeyx = numpadx.two; break;
                    case '3': numberkeyx = numpadx.three; break;
                    case '4': numberkeyx= numpadx.four; break;
                    case '5':numberkeyx = numpadx.five;break;
                    case '6':numberkeyx = numpadx.six;break;
                    case '7':numberkeyx = numpadx.seven;break;
                    case '8': numberkeyx = numpadx.eight;break;
                    case '9':numberkeyx = numpadx.nine;break;
                    case '0': numberkeyx = numpadx.zero;break;
                    default: break;

                }
                switch (fc[i])
                {
                    case '1': numberkeyy = numbpady.one; break;
                    case '2': numberkeyy = numbpady.two; break;
                    case '3': numberkeyy = numbpady.three; break;
                    case '4': numberkeyy = numbpady.four; break;
                    case '5': numberkeyy = numbpady.five; break;
                    case '6': numberkeyy = numbpady.six; break;
                    case '7': numberkeyy = numbpady.seven; break;
                    case '8': numberkeyy = numbpady.eight; break;
                    case '9': numberkeyy = numbpady.nine; break;
                    case '0': numberkeyy = numbpady.zero; break;
                    default: break;

                }
                touch((decimal)(int)numberkeyx, (decimal)(int)numberkeyy);
                await Task.Delay(800);
            }
        }
    }
}
