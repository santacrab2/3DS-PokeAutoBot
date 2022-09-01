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
using static _3DS_link_trade_bot.MainHub;
using static _3DS_link_trade_bot.Form1;

namespace _3DS_link_trade_bot
{
    [EnabledInDm(false)]
    [DefaultMemberPermissions(GuildPermission.ViewChannel)]
    public class TradeModule : InteractionModuleBase<SocketInteractionContext>
    {
        
       
        [SlashCommand("addfc","adds you to the bots friend list, dont forget to add the bot!")]
        public async Task addfc([Summary(description:"No Dashes!!")]string friendcode)
        {
            await DeferAsync();
            if (The_Q.Count != 0)
            {
                if (The_Q.Any(z => z.discordcontext.User == Context.User))
                {
                    await FollowupAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
            try { await Context.User.SendMessageAsync($"I Have added you to the Friend Code queue. I will message you here when I am adding you. My FC is {_settings.FriendCode} Please add me."); } catch { await RespondAsync("enable private messages from users on the server to be queued"); return; }
            friendcode = friendcode.Replace("-", "").Replace(" ","");
            var tobequeued = new queuesystem() { discordcontext = Context,friendcode = friendcode,tradepokemon=EntityBlank.GetBlank(7),IGN ="",mode = botmode.addfc};
            The_Q.Enqueue(tobequeued);
            await FollowupAsync($"Added {Context.User.Username} to the Friend Code queue.");
       

        }

        [SlashCommand("trade", "trades you a pokemon over link trade in 3ds games")]
        public async Task trade(string TrainerName, string PokemonText = " ", Attachment pk7orpk6 = null)
        {
            await DeferAsync();
           
            if (The_Q.Count != 0)
            {
                if (The_Q.Any(z => z.discordcontext.User == Context.User))
                {
                    await FollowupAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
            if (PokemonText != " ")
            {
               
          

                ShowdownSet set = ConvertToShowdown(PokemonText);
                RegenTemplate rset = new(set);
                var trainer = TrainerSettings.GetSavedTrainerData(GameVersion.USUM,7);
                if (NTR.game <3)
                    trainer = TrainerSettings.GetSavedTrainerData(GameVersion.ORAS, 6);
                var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
               
                var pkm = sav.GetLegalFromSet(rset, out var res);
                
                
                if (pkm is PK7 pk7)
                {
                    pk7.SetDefaultRegionOrigins();
                    pkm = pk7;
                }


                if (Legal.ZCrystalDictionary.ContainsValue((ushort)pkm.HeldItem))
                    pkm.HeldItem = 0;
                if (!new LegalityAnalysis(pkm).Valid || FormInfo.IsFusedForm(pkm.Species,pkm.Form,pkm.Format))
                {
                    var reason = FormInfo.IsFusedForm(pkm.Species,pkm.Form,pkm.Format)?"You can not trade Fused Pokemon because you won't have the originals when they de-fuse and your save file will crash": $"I wasn't able to create a {(Species)set.Species} from that set.";
                    var imsg = $"Oops! {reason}";
                 
                        imsg += $"\n{set.SetAnalysis(sav,pkm)}";
                    await FollowupAsync(imsg).ConfigureAwait(false);
                    return;
                }
                try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued");return; }
                queuesystem tobequeued;
                tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, mode = botmode.trade, tradepokemon = pkm };
                The_Q.Enqueue(tobequeued);
                await FollowupAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pkm.Species}");
                
               
            }
            if (pk7orpk6 != null)
            {

                if (!EntityDetection.IsSizePlausible(pk7orpk6.Size))
                {
                    await FollowupAsync("this is not a pk file", ephemeral: true);
                    return;
                }
                var buffer = await discordmain.DownloadFromUrlAsync(pk7orpk6.Url);

                if (NTR.game >2)
                {
                    var pkm = EntityFormat.GetFromBytes(buffer, EntityContext.Gen7);

                    if (!new LegalityAnalysis(pkm).Valid)
                    {
                        SaveFile sav;
                        if (NTR.game >2)
                            sav = SaveUtil.GetBlankSAV(GameVersion.UM, "Piplup");
                        else
                            sav = SaveUtil.GetBlankSAV(GameVersion.OR, "piplup");
                        ShowdownSet set = new(pkm);
                        await FollowupAsync($"This File is illegal. Heres why: {set.SetAnalysis(sav, pkm)}");
                        return;
                    }
                    try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued"); return; }
                    var tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, tradepokemon = pkm, mode = botmode.trade };
                    The_Q.Enqueue(tobequeued);
                    await FollowupAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pkm.Species}");
                }
                else
                {
                    var pkm = EntityFormat.GetFromBytes(buffer, EntityContext.Gen6);

                    if (!new LegalityAnalysis(pkm).Valid)
                    {
                        SaveFile sav;
                        if (NTR.game>2)
                            sav = SaveUtil.GetBlankSAV(GameVersion.UM, "Piplup");
                        else
                            sav = SaveUtil.GetBlankSAV(GameVersion.OR, "piplup");
                        ShowdownSet set = new(pkm);
                        await FollowupAsync($"This File is illegal. Heres why: {set.SetAnalysis(sav, pkm)}");
                        return;
                    }
                    try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued"); return; }
                    var tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, tradepokemon = pkm, mode = botmode.trade };
                    The_Q.Enqueue(tobequeued);
                    await FollowupAsync($"{Context.User.Username} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pkm.Species}");
                }
            }
        }

        [SlashCommand("dump","reads pokemon in your box that you show the bot and sends you pk files of them without trading")]
        public async Task dump(string TrainerName)
        {
            DeferAsync();
            if (The_Q.Count != 0)
            {
                if (The_Q.Any(z => z.discordcontext.User == Context.User))
                {
                    await FollowupAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
            The_Q.Enqueue(new queuesystem { discordcontext = Context, IGN = TrainerName, friendcode = "", tradepokemon = null, mode = botmode.dump });
            await FollowupAsync($"{Context.User.Username} - added to the dump queue. Current Position: {The_Q.Count()}.");
        }
        [SlashCommand("hi","hi")]
        public  async Task some()
        {
           
                await DeferAsync();
                await FollowupAsync("hi", ephemeral: true);
           
            
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
