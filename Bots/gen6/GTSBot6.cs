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

namespace _3DS_link_trade_bot.Bots.gen6
{
    public class GTSBot6
    {
        public static async Task GTSRoutine6()
        {
            if (!isconnected6)
            {
                await touch(227, 15, 2);
                await touch(164, 130, 2);
                await click(A, 60);
            }
            await click(Start, 1);
            await touch(87, 54, 1);
            await click(A, 15);
            await click(A, 1);
            ntr.WriteBytes(BitConverter.GetBytes(_settings.PokemonWanted), pokemonwantedoff);
            await touch(156, 180,1);
            while (!checkscreen(currentscreenoff, GTSScreenVal))
                await Task.Delay(500);

        }
    }
}
