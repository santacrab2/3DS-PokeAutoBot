using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Discord;
using PKHeX.Core;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using static _3DS_link_trade_bot.MainHub;
using static _3DS_link_trade_bot.Form1;
using static _3DS_link_trade_bot.RAM;
using static _3DS_link_trade_bot.discordmain;
using static _3DS_link_trade_bot.NTRClient;
namespace _3DS_link_trade_bot
{
    public class WTBot
    {
        public static int weirds_users_suck = 30_000;
        public static async Task WTroutine()
        {
            var rng = new Random();
     
            if (!infestivalplaza)
            {
                ChangeStatus("entering festival plaza");
                await click(X, 1);
                await touch(229, 171, 10);
                await touch(296, 221, 5);
                await click(B, 10);

            }
            if (!isconnected)
            {
                ChangeStatus("connecting to the internet");
                await touch(296, 221, 5);
                await click(A, 2);
                //start and wait for connection
                await click(A, 30);
                await click(A, 5);
                //start and wait for checking status
                await click(A, 20);
                
            }
            if (userinvitedbot)
                await click(X, 1);
            ChangeStatus("starting WT distribution task");
            await touch(235, 130, 1);
            await touch(165, 165, 5);
            var thegift = wtmons[rng.Next(wtmons.Count())];
            await Gen7LinkTradeBot.injection(thegift);
            await click(A, 5);
            for (int i = 0; i < 2; i++)
                await click(A, 1);
 
         
            ChangeStatus($"Wonder Trading: {(Species)thegift.Species}");
           
            var stop = new Stopwatch();
                await click(A, 3);
                stop.Restart();
                if (BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != 0x41A8)
                {
                    while (BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != start_seekscreen && stop.ElapsedMilliseconds < weirds_users_suck)
                        await click(B, 1);
                    await click(B, 5);
                    return;
                }
            var receivingpkm = EntityFormat.GetFromBytes(ntr.ReadBytes(WTReceivingPokemon, 232));
          
            while (stop.ElapsedMilliseconds < 90_000)
            {
                if(receivingpkm != null)
                {
                    if (receivingpkm.ChecksumValid)
                        break;
                }
                receivingpkm = EntityFormat.GetFromBytes(ntr.ReadBytes(WTReceivingPokemon, 232));
             
                await Task.Delay(1000);
            }
            if (receivingpkm == null || !receivingpkm.ChecksumValid)
            {
                ChangeStatus($"No Match found, exiting Wonder Trade");
               
                while (!infestivalplaza && stop.ElapsedMilliseconds < 60_000)
                    await click(B, 2);
                await Task.Delay(5_000);
                return;
            }
            var matchedtrainerbytes = ntr.ReadBytes(WTTrainerMatch, 24);
            var matchedtrainer = Encoding.Unicode.GetString(matchedtrainerbytes).Trim('\0');
            stop.Restart();
            while(matchedtrainer == "１２３４５６"&&stop.ElapsedMilliseconds < 30_000)
            {
                matchedtrainerbytes = ntr.ReadBytes(WTTrainerMatch, 24);
                matchedtrainer = Encoding.Unicode.GetString(matchedtrainerbytes).Trim('\0');
            }
            matchedtrainer = Encoding.Unicode.GetString(matchedtrainerbytes).Trim('\0').Trim('６').Trim('５').Trim('４').Trim('\0');
            ChangeStatus($"WT Match Found: Trainer: {matchedtrainer} Incoming: {(Species)receivingpkm.Species}");
           
            
            stop.Restart();
                while(BitConverter.ToInt16(ntr.ReadBytes(WTReceivingPokemon, 2)) != 0&&stop.ElapsedMilliseconds < 60_000)
                {

                    await touch(155, 151, 1);
                }
                ChangeStatus("Wonder Trade Complete");
            stop.Restart();
                while (!infestivalplaza&&stop.ElapsedMilliseconds < 60_000)
                    await click(B, 2);
                await Task.Delay(5_000);

            


        }
    }
}
