using System;
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
        public async Task addfc([Summary(description:"No Dashes!!")]string friendcode)
        {
            try { await Context.User.SendMessageAsync($"I Have added you to the Friend Code queue. I will message you here when I add you. My Fc is {Program.form1.botfc.Text}"); } catch { await RespondAsync("enable private messages from users on the server to be queued"); return; }
            var tobequeued = new queuesystem() { discordcontext = Context,friendcode = friendcode,tradepokemon=EntityBlank.GetBlank(7),IGN ="",mode = botmode.addfc};
            The_Q.Enqueue(tobequeued);
            await RespondAsync($"Added {Context.User.Username} to the Friend Code queue.");


        }

        [SlashCommand("trade", "trades you a pokemon over link trade in 3ds games")]
        public async Task trade(string TrainerName, string PokemonText = " ", Attachment pk7 = null)
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
                await RespondAsync("I have received your command");
          

                ShowdownSet set = ConvertToShowdown(PokemonText);
                var sav = SaveUtil.GetBlankSAV(GameVersion.UM, "Piplup");
                var pkm = sav.GetLegalFromSet(set, out var res);
               

                if (!new LegalityAnalysis(pkm).Valid || res.ToString() != "Regenerated")
                {
                    var reason =  $"I wasn't able to create a {(Species)set.Species} from that set.";
                    var imsg = $"Oops! {reason}";
                 
                        imsg += $"\n{set.SetAnalysis(sav,pkm)}";
                    await FollowupAsync(imsg, ephemeral: true).ConfigureAwait(false);
                    return;
                }
                try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued");return; }
               
                await FollowupAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count()+1}. Receiving: {(Species)pkm.Species}");
                queuesystem tobequeued;
                tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, mode = botmode.trade, tradepokemon = pkm };
                The_Q.Enqueue(tobequeued);
               
            }
            if (pk7 != null)
            {
                await RespondAsync("I have received your command");
                if (!EntityDetection.IsSizePlausible(pk7.Size))
                {
                    await FollowupAsync("this is not a pk file", ephemeral:true);
                    return;
                }
                var buffer = await discordmain.DownloadFromUrlAsync(pk7.Url);
                var pkm = new PK7(buffer);
                if(!new LegalityAnalysis(pkm).Valid)
                {
                    var sav = SaveUtil.GetBlankSAV(GameVersion.US, "santacrab");
                    ShowdownSet set = new(pkm);
                    await FollowupAsync($"This File is illegal. Heres why: {set.SetAnalysis(sav, pkm)}");
                }
                try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued"); return; }
                var tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, tradepokemon = pkm, mode = botmode.trade };
                The_Q.Enqueue(tobequeued);
                await FollowupAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count() + 1}. Receiving: {(Species)pkm.Species}");
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
