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
using static System.Buffers.Binary.BinaryPrimitives;

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
            while(!checkscreen(currentscreenoff,MenuScreenVal))
                await click(Start, 1);
            await click(R, 1);
            await touch(87, 54, 3);
            await click(A, 15);
            await click(A, 1);
            ChangeStatus("selecting the page to search");
            ntr.WriteBytes(BitConverter.GetBytes(_settings.PokemonWanted), pokemonwantedoff);
            await Task.Delay(1000);
            await touch(156, 180,1);
            var stop = new Stopwatch();
            stop.Restart();
            while (!checkscreen(currentscreenoff, GTSScreenVal) && stop.ElapsedMilliseconds < 60_000)
                await Task.Delay(500);
            ChangeStatus("searching the GTS list");
            var tosend = GetGTSPoke();
            await Task.Delay(2000);
            if(tosend == null)
            {
                ChangeStatus("no legal match found");
                if (ReadUInt16LittleEndian(ntr.ReadBytes(GTSPageSize, 2)) == 0)
                    await click(A, 1);
                while (!checkscreen(currentscreenoff, OverWorldScreenVal))
                    await click(B, 1);
                
                _settings.PokemonWanted++;
                if (_settings.PokemonWanted > 718)
                    _settings.PokemonWanted = 1;
                return;
            }
            await Task.Delay(1000);
            ChangeStatus($"sending: {(Species)tosend.Species} to: {LastGTSTrainer}");
            await Gen7LinkTradeBot.injection(tosend);
            await click(Y, 3);
            ntr.WriteBytes(BitConverter.GetBytes(tradeindex),GTSCurrentView6);
            await Task.Delay(1000);
           
            await click(B, 3);
           
            await click(A, 5);
           for(int i = 0; i < 5; i++)
                await click(A, 1);
            await Task.Delay(5000);
            
            stop.Restart();
            while (!checkscreen(currentscreenoff, OverWorldScreenVal) && stop.ElapsedMilliseconds < 90_000)
                await click(B, 1);
            

        }
        public static PKM GetGTSPoke()
        {
            
            PKM pkm = null;
            GTSPage6 gtspage = new GTSPage6(ntr.ReadBytes(GTSListBlockOff, GTSPage6.GTSBlocksize6));
            
            for (int i = 99; i >= 0; i--)
            {
                try
                {
                    var entry = gtspage[i];
                    bool isafuckhead = false;
                    foreach(string fuckhead in _settings.Legalitysettings.ZKnownGTSBreakers)
                    {
                        if (fuckhead.ToLower().Contains(entry.trainername.ToLower()))
                        {
                            isafuckhead = true;
                            break;
                        }

                         
                    }
                    if (isafuckhead)
                        continue;
                    
                    if (entry.RequestedPoke == 0 || entry.RequestedPoke == 263)
                        continue;
                    var trainer = TrainerSettings.GetSavedTrainerData(6);
                    var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);

                    pkm = sav.GetLegalFromSet(new ShowdownSet($"{SpeciesName.GetSpeciesNameGeneration(entry.RequestedPoke,2,6)}\nLevel: {(entry.RequestLevel > 0 ? (entry.RequestLevel * 10) - 1 : 99)}\nShiny: Yes"), out var res);
                    if((entry.GTSmsg.ToLower().Contains("no")||entry.GTSmsg.ToLower().Contains("not")) && entry.GTSmsg.Contains("shiny"))
                        pkm = sav.GetLegalFromSet(new ShowdownSet($"{SpeciesName.GetSpeciesNameGeneration(entry.RequestedPoke,2,6)}\nLevel: {(entry.RequestLevel > 0 ? (entry.RequestLevel * 10) - 1 : 99)}"), out res);
                    pkm = pkm.Legalize();
                    pkm.OT_Name = entry.trainername;
                    pkm.Gender = entry.RequestedGender == 2 ? 1 : 0;
                   
                    if (!new LegalityAnalysis(pkm).Valid || pkm.FatefulEncounter)
                    {
                        pkm = null;
                        continue;
                    }
                    else
                    {
                        
                 
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
