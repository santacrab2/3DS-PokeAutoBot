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
    public class WTBot6
    {
        public static async Task WTRoutine6()
        {
            var rng = new Random();
            if (!isconnected6)
            {
                ChangeStatus("connecting to the internet");
                await touch(227, 15, 2);
                await touch(164, 130, 2);
                await click(A, 60);
            }
            if (userinvitedbot6)
                await touch(178, 213, 1);
            ChangeStatus("Starting WonderTrade");
            var thegift = wtmons[rng.Next(wtmons.Count())];
            await Gen7LinkTradeBot.injection(thegift);
            while (!checkscreen(currentscreenoff, MenuScreenVal))
                await click(Start, 1);
            await click(L, 1);
            await touch(230, 120, 3);
            await click(A, 5);
            await touch(157, 117, 5);
            for (int i = 0; i < 2; i++)
                await click(A,1);
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
            stop.Restart();
            var receivingpkm = EntityFormat.GetFromBytes(ntr.ReadBytes(WTReceivingPokemon6, 260));

            while (stop.ElapsedMilliseconds < 90_000)
            {
                if (receivingpkm != null && new LegalityAnalysis(receivingpkm).Valid)
                {
                    if (receivingpkm.ChecksumValid)
                        break;
                }
                receivingpkm = EntityFormat.GetFromBytes(ntr.ReadBytes(WTReceivingPokemon6, 260));

                await Task.Delay(1000);
            }
            if (receivingpkm == null || !new LegalityAnalysis(receivingpkm).Valid)
            {
                ChangeStatus($"No Match found, exiting Wonder Trade");
                foreach (var chan in _settings.Discordsettings.BotWTChannel)
                {
                    var tosend = (ITextChannel)_client.GetChannel(chan);
                    await tosend.SendMessageAsync(Format.Code("No Match found for Wondertrade..."));
                }
                await click(A, 1);
                await click(A, 1);
                while (!checkscreen(currentscreenoff, OverWorldScreenVal))
                    await click(B, 1);
                await Task.Delay(1_000);
                return;
            }
            var matchedtrainerbytes = ntr.ReadBytes(WTTrainerMatch6, 24);
   
            var matchedtrainer = Encoding.Unicode.GetString(matchedtrainerbytes).Trim('\0');
            ChangeStatus($"WT Match Found: Trainer: {matchedtrainer} Incoming: {(Species)receivingpkm.Species}");
            foreach (var chan in _settings.Discordsettings.BotWTChannel)
            {
                var tosend = (ITextChannel)_client.GetChannel(chan);

                EmbedBuilder embed = new EmbedBuilder();
                try
                {
                    embed.ThumbnailUrl = receivingpkm.IsShiny ? $"https://play.pokemonshowdown.com/sprites/ani-shiny/{((Species)receivingpkm.Species).ToString().ToLower().Replace(" ", "")}.gif" : $"https://play.pokemonshowdown.com/sprites/ani/{((Species)receivingpkm.Species).ToString().ToLower().Replace(" ", "")}.gif";
                }
                catch { }
                var newShowdown = new List<string>();

                var showdown = ShowdownParsing.GetShowdownText(receivingpkm);
                foreach (var line in showdown.Split('\n'))
                    newShowdown.Add(line);

                if (receivingpkm.IsEgg)
                    newShowdown.Add("\nPokémon is an egg");
                if (receivingpkm.Ball > (int)Ball.None)
                    newShowdown.Insert(newShowdown.FindIndex(z => z.Contains("Nature")), $"Ball: {(Ball)receivingpkm.Ball} Ball");
                if (receivingpkm.IsShiny)
                {
                    var index = newShowdown.FindIndex(x => x.Contains("Shiny: Yes"));
                    if (receivingpkm.ShinyXor == 0 || receivingpkm.FatefulEncounter)
                        newShowdown[index] = "Shiny: Square\r";
                    else newShowdown[index] = "Shiny: Star\r";
                }
                embed.AddField($"WT Match Found!\nTrainer: {matchedtrainer}", Format.Code(string.Join("\n", newShowdown).TrimEnd()));
                try
                {
                    await tosend.SendMessageAsync(embed: embed.Build());
                }
                catch (Exception ex) { await Log(ex.ToString()); }
            }
            while (!checkscreen(currentscreenoff, OverWorldScreenVal))
                await click(B, 1);
            await Task.Delay(1_000);
        }
    }
}
