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
        public static int weirds_users_suck = 30_000;
        public static async Task WTroutine()
        {
            var rng = new Random();
            if (!infestivalplaza)
            {
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
            foreach (var chan in _settings.Discordsettings.BotWTChannel)
            {
                var tosend = (ITextChannel)_client.GetChannel(chan);


                EmbedBuilder embed = new EmbedBuilder();
                try
                {
                    embed.ThumbnailUrl = thegift.IsShiny ? $"https://play.pokemonshowdown.com/sprites/ani-shiny/{((Species)thegift.Species).ToString().ToLower().Replace(" ", "")}.gif" : $"https://play.pokemonshowdown.com/sprites/ani/{((Species)thegift.Species).ToString().ToLower().Replace(" ", "")}.gif";
                }
                catch { }
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
                try
                {
                    await tosend.SendMessageAsync(embed: embed.Build());
                }
                catch (Exception ex) { await Log(ex.ToString()); }
            }
                await Task.Delay(10_000);
         
                for (int i = 5; i > 0; i--)
                {
                foreach (var chan in _settings.Discordsettings.BotWTChannel)
                {
                    var tosend = (ITextChannel)_client.GetChannel(chan);
                    await tosend.SendMessageAsync($"{i}");
                }
                    await Task.Delay(1000);
                }
            foreach (var chan in _settings.Discordsettings.BotWTChannel)
            {
                var tosend = (ITextChannel)_client.GetChannel(chan);
                await tosend.SendMessageAsync("wonder trade now");
            }
                ChangeStatus($"Wonder Trading: {(Species)thegift.Species}");
            
                var stop = new Stopwatch();
                await click(A, 3);
                
                if (BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != 0x41A8)
                {
                    while (BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != 0x3F2B && stop.ElapsedMilliseconds < weirds_users_suck)
                        await click(B, 1);
                    await click(B, 5);
                    return;
                }
                
                while (BitConverter.ToInt16(ntr.ReadBytes(screenoff, 2)) != 0x3F2B && stop.ElapsedMilliseconds < 30_000)
                    await Task.Delay(25);
                stop.Restart();
                while(stop.ElapsedMilliseconds < 60_000)
                {

                    await touch(155, 151, 1);
                }
                ChangeStatus("Wonder Trade Complete");
                while (!infestivalplaza)
                    await click(B, 2);
                await Task.Delay(5_000);

            


        }
    }
}
