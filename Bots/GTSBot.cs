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
    public class GTSBot
    {
        public static List<string> KnownGTSBreakers = new List<string> { "funkygamer26", "chloegarcia", "volcano.“do”", "33012888", "edou", "moon.", "unknown.yt", "japan.kebuju", "はちゆきおし", "あああ" , "あか", "adventrsnivy", "noxii",""," ", "doudou#6666", "doudou#9999", "zeraoratv=yt", "sun." }; 
        public static int tradeindex;
        public static int gtspagesize;
        
        public static async Task GTStrades()
        {
            if (!infestivalplaza)
            {
                await click(X, 1);
                await touch(229, 171, 10);

            }
            if (!isconnected)
            {
                ChangeStatus("connecting to the internet");
                await touch(296, 221, 5);
                await click(A, 2);
                await click(A, 30);
                await click(A, 5);
                await click(A, 20);
                if (!isconnected)
                {
                    return;
                }
            }
            ChangeStatus("starting GTS distribution task");
            await touch(235, 130, 1);
            await touch(161, 121, 10);
            ntr.WriteBytes(BitConverter.GetBytes(_settings.PokemonWanted), GTSDeposit);
            await touch(165, 95, 1);
            await touch(154, 187, 5);
            if(BitConverter.ToUInt32(ntr.ReadBytes(screenoff,0x04)) == 0x40F5)
            {
                ChangeStatus("no pokemon found");
                _settings.PokemonWanted++;
                if (_settings.PokemonWanted > 800)
                    _settings.PokemonWanted = 1;
                for (int i = 0; i < 3; i++)
                    await click(B, 2);
                await click(A, 10);
                return;
            }
            gtspagesize = (int)BitConverter.ToUInt32(ntr.ReadBytes(GTSpagesizeoff, 0x04));
            var pkm = GetGTSPoke();

            if(pkm == null)
            {
                ChangeStatus("no legal request found");
                _settings.PokemonWanted++;
                if (_settings.PokemonWanted > 800)
                    _settings.PokemonWanted = 1;
                for (int i = 0; i < 3; i++)
                    await click(B, 1);
                await click(A, 5);
                return;
            }
            ChangeStatus("gts trading");
            await Gen7LinkTradeBot.injection(pkm);
            ntr.WriteBytes(BitConverter.GetBytes(tradeindex), GTScurrentview);
            await click(A, 5);
            for (int i = 0; i < 3; i++)
                await click(A, 1);
            var stop = new Stopwatch();
            stop.Restart();
            while(BitConverter.ToInt16(ntr.ReadBytes(screenoff,2))!= 0x3F2B && stop.ElapsedMilliseconds < 120_000)
                await click(A, 5);
            if (stop.ElapsedMilliseconds > 120_000)
            {
                stop.Restart();
                while(BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != 0x3F2B && stop.ElapsedMilliseconds < 60_000)
                    await click(B, 2);
            }
            await click(B, 1);
            await click(A, 5);
            return;


        }
        public static PKM GetGTSPoke()
        {
            PKM pkm = null; 
            GTSPage gtspage = new GTSPage(ntr.ReadBytes(GTSblockoff, 0x6400));
            for (int i = gtspagesize; i>=0; i--)
            {
                try
                {
                    var entry = gtspage[i];
                    if (KnownGTSBreakers.Contains(entry.trainername.ToLower()))
                    {
                        continue;
                    }

                    var sav = SaveUtil.GetBlankSAV(GameVersion.UM, "piplup.net");
                    pkm = sav.GetLegalFromSet(new ShowdownSet($"Piplup.net({(Species)entry.RequestedPoke})\nLevel: {(entry.levelindex < 10 ? (entry.levelindex * 10) - 1 : 99)}\nShiny: Yes\nBall: Dive"), out _);
                    pkm.OT_Name = "piplup.net";
                    pkm.Gender = entry.genderindex == 2 ? 1 : 0;
                    if (!new LegalityAnalysis(pkm).Valid)
                    {
                        pkm = null;
                        continue;
                    }
                    else
                    {
                        Log($"Trading Trainer:{entry.trainername.ToLower()}");
                        tradeindex = i;
                        break;
                    }
                }
                catch { pkm = null; continue; }

            }
            return pkm;
        }
    }
}
