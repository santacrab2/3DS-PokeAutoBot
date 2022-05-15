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

namespace _3DS_link_trade_bot
{
    public class WTBot
    {
        public static async Task WTroutine()
        {
            var rng = new Random();
            if (!infestivalplaza)
            {
                await click(X, 2);
                await touch(229, 171, 10);

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
                if (!isconnected)
                {
                    return;
                }
            }
            ChangeStatus("starting WT distribution task");
            await touch(235, 130, 1);
            await touch(165, 165, 5);
            var thegift = wtmons[rng.Next(wtmons.Count())];
            await Gen7LinkTradeBot.injection(thegift);
            await click(A, 5);
            for (int i = 0; i < 2; i++)
                await click(A, 1);
            foreach(var chan in _settings.Discordsettings.BotWTChannel)
            {
               var tosend = (ITextChannel) _client.GetChannel(chan);
                EmbedBuilder embed = new EmbedBuilder();
                embed.ThumbnailUrl = thegift.IsShiny ? $"https://play.pokemonshowdown.com/sprites/ani-shiny/{((Species)thegift.Species).ToString().ToLower().Replace(" ", "")}.gif" : $"https://play.pokemonshowdown.com/sprites/ani/{((Species)thegift.Species).ToString().ToLower().Replace(" ", "")}.gif";
                var newShowdown = new List<string>();
                thegift.ClearNickname();
                var showdown = ShowdownParsing.GetShowdownText(thegift);
                foreach (var line in showdown.Split('\n'))
                    newShowdown.Add(line);

                if (thegift.IsEgg)
                    newShowdown.Add("\nPokémon is an egg");
                if (thegift.Ball > (int)Ball.None)
                    newShowdown.Insert(newShowdown.FindIndex(z => z.Contains("Nature")), $"Ball: {(Ball)thegift.Ball} Ball");
                if (thegift.IsShiny)
                {
                    var index = newShowdown.FindIndex(x => x.Contains("Shiny: Yes"));
                    if (thegift.ShinyXor == 0 || thegift.FatefulEncounter)
                        newShowdown[index] = "Shiny: Square\r";
                    else newShowdown[index] = "Shiny: Star\r";
                }

                
                embed.AddField("Wonder trading in 15 seconds", Format.Code(string.Join("\n", newShowdown).TrimEnd()));
                await tosend.SendMessageAsync(embed: embed.Build());
                await Task.Delay(15_000);
                await tosend.SendMessageAsync("wonder trade now");
                await click(A, 1);
                ChangeStatus($"Wonder Trading: {(Species)thegift.Species}");
                var stop = new Stopwatch();
                while (BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != 0x3F2B && stop.ElapsedMilliseconds < 30_000)
                    await Task.Delay(25);
                await Task.Delay(60_000);
                while(!infestivalplaza)
                    await click(B, 2);


            }


        }
    }
}
