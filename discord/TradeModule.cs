﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using PKHeX.Core;
using PKHeX.Core.AutoMod;
using Discord.Interactions;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using static _3DS_link_trade_bot.LinkTradeBot;


namespace _3DS_link_trade_bot
{
    public class TradeModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("addfc","adds you to the bots friend list, dont forget to add the bot!")]
        public async Task addfc(string friendcode)
        {
            var tobequeued = new queuesystem() { discordcontext = Context,friendcode = friendcode,tradepokemon=EntityBlank.GetBlank(7),IGN ="",mode = botmode.addfc};
            The_Q.Enqueue(tobequeued);


        }

        [SlashCommand("trade", "trades you a pokemon over link trade in 3ds games")]
        public async Task trade(string TrainerName, string PokemonText = " ", Attachment PK7orPK6 = null)
        {
            var channelcheck = Program.form1.botchannel.Text.Split(',');
            if (!channelcheck.Contains(Context.Channel.Id.ToString()))
            {
                await RespondAsync("You can not use this command in this channel", ephemeral: true);
                return;
            }
            if (The_Q.Count != 0)
            {
                if (The_Q.Any(z => z.IGN == TrainerName))
                {
                    await RespondAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
            if (PokemonText != " ")
            {
                Legalizer.EnableEasterEggs = false;
                APILegality.SetAllLegalRibbons = false;
                APILegality.SetMatchingBalls = true;
                APILegality.ForceSpecifiedBall = true;
                APILegality.UseXOROSHIRO = true;
                APILegality.UseTrainerData = true;
                APILegality.AllowTrainerOverride = true;
                APILegality.AllowBatchCommands = true;
                APILegality.PrioritizeGame = true;
                APILegality.Timeout = 30;
                APILegality.PrioritizeGameVersion = GameVersion.USUM;
                // Reload Database & Ribbon Index
                EncounterEvent.RefreshMGDB($"{Directory.GetCurrentDirectory()}//mgdb//");
                RibbonStrings.ResetDictionary(GameInfo.Strings.ribbons);

                ShowdownSet set = ConvertToShowdown(PokemonText);
                var sav = SaveUtil.GetBlankSAV(GameVersion.UM, "Piplup");
                var pkm = sav.GetLegalFromSet(set, out _);
               

                if (!new LegalityAnalysis(pkm).Valid)
                {
                    var reason =  $"I wasn't able to create a {(Species)set.Species} from that set.";
                    var imsg = $"Oops! {reason}";
                 
                        imsg += $"\n{set.SetAnalysis(sav,pkm)}";
                    await RespondAsync(imsg, ephemeral: true).ConfigureAwait(false);
                    return;
                }
                try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await RespondAsync("enable private messages from users on the server to be queued");return; }
                await RespondAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count()+1}. Receiving: {(Species)pkm.Species}");
                queuesystem tobequeued;
                tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, mode = botmode.trade, tradepokemon = pkm };
                The_Q.Enqueue(tobequeued);
               
            }
            if (PK7orPK6 != null)
            {
                if (!EntityDetection.IsSizePlausible(PK7orPK6.Size))
                {
                    await RespondAsync("this is not a pk file", ephemeral:true);
                    return;
                }
                var buffer = await discordmain.DownloadFromUrlAsync(PK7orPK6.Url);
                var pkm = new PK7(buffer);
                if(!new LegalityAnalysis(pkm).Valid)
                {
                    var sav = SaveUtil.GetBlankSAV(GameVersion.US, "santacrab");
                    ShowdownSet set = new(pkm);
                    await RespondAsync($"This File is illegal. Heres why: {set.SetAnalysis(sav, pkm)}");
                }
                try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await RespondAsync("enable private messages from users on the server to be queued"); return; }
                var tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, tradepokemon = pkm, mode = botmode.trade };
                The_Q.Enqueue(tobequeued);
                await RespondAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count() + 1}. Receiving: {(Species)pkm.Species}");
            }
        }
        [SlashCommand("hi","hi")]
        public  async Task some()
        {
            await RespondAsync("hi", ephemeral: true);
        }
        public static ShowdownSet? ConvertToShowdown(string setstring)
        {
            // LiveStreams remove new lines, so we are left with a single line set
            var restorenick = string.Empty;

            var nickIndex = setstring.LastIndexOf(')');
            if (nickIndex > -1)
            {
                restorenick = setstring[..(nickIndex + 1)];
                if (restorenick.TrimStart().StartsWith("("))
                    return null;
                setstring = setstring[(nickIndex + 1)..];
            }

            foreach (string i in splittables)
            {
                if (setstring.Contains(i))
                    setstring = setstring.Replace(i, $"\r\n{i}");
            }

            var finalset = restorenick + setstring;
            return new ShowdownSet(finalset);
        }

        private static readonly string[] splittables =
        {
            "Ability:", "EVs:", "IVs:", "Shiny:", "Gigantamax:", "Ball:", "- ", "Level:",
            "Happiness:", "Language:", "OT:", "OTGender:", "TID:", "SID:", "Alpha:",
            "Adamant Nature", "Bashful Nature", "Brave Nature", "Bold Nature", "Calm Nature",
            "Careful Nature", "Docile Nature", "Gentle Nature", "Hardy Nature", "Hasty Nature",
            "Impish Nature", "Jolly Nature", "Lax Nature", "Lonely Nature", "Mild Nature",
            "Modest Nature", "Naive Nature", "Naughty Nature", "Quiet Nature", "Quirky Nature",
            "Rash Nature", "Relaxed Nature", "Sassy Nature", "Serious Nature", "Timid Nature",
        };

    }
}
