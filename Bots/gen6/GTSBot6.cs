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
    public class GTSBot6
    {
        public static int gtspagesize = 0;
        public static string LastGTSTrainer = "";
        public static uint tradeindex = 0;
        public static async Task GTSRoutine6()
        {
        
            if (!isconnected6)
            {
                ChangeStatus("connecting to the internet");
                await touch(227, 15, 2);
                await touch(164, 130, 2);
                await click(A, 60);
            }
            if (userinvitedbot6)
                await touch(178, 213, 1);
            ChangeStatus("Entering GTS");
            await click(Start, 1);
            await click(R, 1);
            await touch(87, 54, 3);
            await click(A, 15);
            await click(A, 1);
            ChangeStatus("selecting the page to search");
            ntr.WriteBytes(BitConverter.GetBytes(_settings.PokemonWanted), pokemonwantedoff);
            await Task.Delay(1000);
            await touch(156, 180,1);
            while (!checkscreen(currentscreenoff, GTSScreenVal))
                await Task.Delay(500);
            ChangeStatus("searching the GTS list");
            var tosend = GetGTSPoke();
            await Task.Delay(1000);
            ChangeStatus($"sending: {(Species)tosend.Species} to: {LastGTSTrainer}");
            await Gen7LinkTradeBot.injection(tosend);
            await click(Y, 5);
            ntr.WriteBytes(BitConverter.GetBytes(tradeindex),GTSCurrentView6);
            await Task.Delay(1000);
           
            await click(B, 3);
           
            await click(A, 5);
            while (checkscreen(currentscreenoff, BoxScreenVal) || checkscreen(currentscreenoff, AcceptScreenVal))
                await click(A, 1);
            await Task.Delay(5000);
            while (!checkscreen(currentscreenoff, OverWorldScreenVal))
                await click(B, 1);
            

        }
        public static PKM GetGTSPoke()
        {
            PKM pkm = null;
            GTSPage6 gtspage = new GTSPage6(ntr.ReadBytes(GTSListBlockOff, GTSPage6.GTSBlocksize6));
            //gtspagesize = BitConverter.ToInt32(ntr.ReadBytes(GTSpagesizeoff, 2));
            for (int i = 99; i >= 0; i--)
            {
                try
                {
                    var entry = gtspage[i];

                    if (_settings.Legalitysettings.ZKnownGTSBreakers.Contains(entry.trainername.ToLower()))
                    {
                        continue;
                    }
                    
                    if (entry.RequestedPoke == 0)
                        continue;
                    var trainer = TrainerSettings.GetSavedTrainerData(6);
                    var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);

                    pkm = sav.GetLegalFromSet(new ShowdownSet($"Piplup.net({(Species)entry.RequestedPoke})\nLevel: {(entry.RequestLevel < 10 ? (entry.RequestLevel * 10) - 1 : 99)}\nShiny: Yes\nBall: Dive"), out var res);
                    pkm.OT_Name = entry.trainername;
                    pkm.Gender = entry.RequestedGender == 2 ? 1 : 0;
                    if (!new LegalityAnalysis(pkm).Valid || pkm.FatefulEncounter)
                    {
                        pkm = null;
                        continue;
                    }
                    else
                    {
                        ChangeStatus($"Trading Trainer:{entry.trainername.ToLower()}");
                        LastGTSTrainer = entry.trainername;
                        tradeindex = (uint)i;
                        break;
                    }
                }
                catch { pkm = null; continue; }

            }
            return pkm;
        }
    }
}
