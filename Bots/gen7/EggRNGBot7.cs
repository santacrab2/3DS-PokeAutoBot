using Discord;
using PKHeX.Core;
using PKHeX.Core.AutoMod;
using PKHeX.Core.Searching;
using System.Diagnostics;
using static _3DS_link_trade_bot.Form1;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using static _3DS_link_trade_bot.RAM;
using static _3DS_link_trade_bot.discordmain;
using System.Text;

namespace _3DS_link_trade_bot
{
    public class EggRNGBot7
    {
        public static async Task EggRNGNonePID7Routine()
        {
            int countdown = 0;
            while (countdown != _settings.RNGsettings.frames)
            {
                ntr.WriteBytes(BitConverter.GetBytes(0x1), HasEggOff);
                await click(A, 2);
                for (int i = 0; i < 10; i++)
                    await click(B, 1);


                countdown++;

            }
            ntr.WriteBytes(BitConverter.GetBytes(0x1), HasEggOff);
        }
    }
}
