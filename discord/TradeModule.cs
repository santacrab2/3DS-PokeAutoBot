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
using System.Configuration;
using Discord.WebSocket;

namespace _3DS_link_trade_bot
{
    [EnabledInDm(false)]
    [DefaultMemberPermissions(GuildPermission.ViewChannel)]
    public class TradeModule : InteractionModuleBase<SocketInteractionContext>
    {
        
       
       

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
                if(set == null)
                {
                    await FollowupAsync("Your text was in an incorrect format. Please go read <id:guide> while you are muted for the next hour. Consider using /simpletrade once you are unmuted! <:happypip:872674980222107649> \nIf you feel this is in error DM the bot to appeal and santacrab will review.");
                    var user = (SocketGuildUser)Context.Interaction.User;
                    await user.SetTimeOutAsync(TimeSpan.FromHours(1));
                    return;
                }
                var sav = NTR.game switch
                {
                    4 or 3 => SaveUtil.GetBlankSAV(EntityContext.Gen7, "Pip"),

                    2 or 1 => SaveUtil.GetBlankSAV(EntityContext.Gen6, "Pip"),

                };
                RegenTemplate rset = new(set, sav.Generation);
                
                var pkm = sav.GetLegalFromSet(rset).Created;
                if (pkm.Generation < 6)
                    pkm.CurrentHandler = 1;
                if (pkm is PK7 pk7)
                {
                    pk7.SetDefaultRegionOrigins(pk7.Language);
                    pkm = pk7;
                }
                if(pkm is PK6 pk6)
                {
                    pk6.SetDefaultRegionOrigins(pk6.Language);
                    pkm = pk6;
                }

                if (ItemStorage7USUM.GetCrystalKey((ushort)pkm.HeldItem, out var key))
                    pkm.HeldItem = 0;
                if (!new LegalityAnalysis(pkm).Valid || FormInfo.IsFusedForm(pkm.Species,pkm.Form,pkm.Format))
                {
                    var reason = FormInfo.IsFusedForm(pkm.Species,pkm.Form,pkm.Format)?"You can not trade Fused Pokemon because you won't have the originals when they de-fuse and your save file will crash": $"I wasn't able to create a {(Species)set.Species} from that set.";
                    var imsg = $"Oops! {reason}";
                 
                        imsg += $"\n{set.SetAnalysis(sav,pkm)}";
                    if (pkm.Species == 0)
                        imsg += $"\n\nI did not detect any pokemon species, check your spelling and text format. See <id:guide> for more info.";
                    await FollowupAsync(imsg).ConfigureAwait(false);
                    return;
                }
                try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued");return; }
                queuesystem tobequeued;
                tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, mode = botmode.trade, tradepokemon = pkm };
                The_Q.Enqueue(tobequeued);
                await FollowupAsync($"{Context.User.Mention} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pkm.Species}");
                return;
               
            }
            if (pk7orpk6 != null)
            {

                if (!EntityDetection.IsSizePlausible(pk7orpk6.Size) )
                {
                    await FollowupAsync("this is not a pk file", ephemeral: true);
                    return;
                }
                if(NTR.game < 3)
                {
                    if (!pk7orpk6.Filename.EndsWith(".pk6"))
                    {
                        await FollowupAsync("this is not a pk6 file, pk6 is required for the generation 6 bot. Please use text if you are unsure how to obtain this file type.");
                        return;
                    }
                }
                else
                {
                    if (!pk7orpk6.Filename.EndsWith(".pk7"))
                    {
                        await FollowupAsync("this is not a pk7 file, pk7 is required for the generation 7 bot. Please use text if you are unsure how to obtain this file type.");
                        return;
                    }
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
                    await FollowupAsync($"{Context.User.Mention} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pkm.Species}");
                    return;
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
                    await FollowupAsync($"{Context.User.Mention} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pkm.Species}");
                    return;
                }
            }
            await FollowupAsync("You did not include any pokemon information, Please make sure the command boxes are filled out. See <id:guide> for instructions and examples");
        }
        public static List<simpletradeobject> simpletradecache = new();
        [SlashCommand("simpletrade", "helps you build a pokemon with a simple form")]

        public async Task DumbassTrade(string TrainerName)
        {
            await DeferAsync(ephemeral: true);
            if (The_Q.Count != 0)
            {
                if (The_Q.Any(z => z.discordcontext.User == Context.User))
                {
                    await FollowupAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
            var cache = new simpletradeobject();
            cache.user = Context.User;
            if (simpletradecache.Find(z => z.user == cache.user) != null)
                simpletradecache.Remove(simpletradecache.Find(z => z.user == cache.user));
            simpletradecache.Add(cache);
            var sav = NTR.game switch
            {
                4 or 3 => SaveUtil.GetBlankSAV(EntityContext.Gen7, "Pip"),

                2 or 1 => SaveUtil.GetBlankSAV(EntityContext.Gen6, "Pip"),

            };
            var pk = EntityBlank.GetBlank(sav);
            var datasource = new FilteredGameDataSource(sav, GameInfo.Sources);
            cache.currenttype = "species";
            cache.opti = datasource.Species.Select(z => z.Text).ToArray();

            var component = compo(cache.currenttype, cache.page=0, cache.opti);
            await FollowupAsync("Choose", components: component, ephemeral: true);
            while (!cache.responded)
                await Task.Delay(250);
            cache.responded = false;
            var set = new ShowdownSet(cache.response.Data.Values.First());
            cache.opti = FormConverter.GetFormList(pk.Species, GameInfo.Strings.types, GameInfo.Strings.forms, GameInfo.GenderSymbolUnicode, pk.Context);
            if (cache.opti.Length > 1)
            {
                var tempspec = cache.response.Data.Values.First();
                cache.currenttype = "Form";
                cache.opti = FormConverter.GetFormList(pk.Species, GameInfo.Strings.types, GameInfo.Strings.forms, GameInfo.GenderSymbolUnicode, pk.Context);
                await Context.Interaction.ModifyOriginalResponseAsync(z => z.Components = compo(cache.currenttype, cache.page=0, cache.opti));
                while (!cache.responded)
                    await Task.Delay(250);
                cache.responded = false;
                var tempspecform = $"{tempspec}-{cache.response.Data.Values.First()}";
                set = new ShowdownSet(tempspecform);
            }
            cache.currenttype = "Shiny";
            cache.opti = new string[] { "Yes", "No" };
            await Context.Interaction.ModifyOriginalResponseAsync(z => z.Components = compo(cache.currenttype, cache.page=0, cache.opti));
            while (!cache.responded)
                await Task.Delay(250);
            cache.responded = false;
            set= new ShowdownSet($"{set.Text}\nShiny: {cache.response.Data.Values.First()}");
            pk = sav.GetLegalFromSet(set).Created;
            if (!pk.PersonalInfo.Genderless)
            {
                cache.currenttype = "Gender";
                cache.opti = new string[] { "Male ♂", "Female ♀" };
                await Context.Interaction.ModifyOriginalResponseAsync(z => z.Components = compo(cache.currenttype, cache.page = 0, cache.opti));
                while (!cache.responded)
                    await Task.Delay(250);
                cache.responded = false;
                pk.Gender = cache.response.Data.Values.First() == "Male ♂" ? 0 : 1;
            }
            cache.currenttype = "Item";
            cache.opti = datasource.Items.Select(z => z.Text).ToArray();
            await Context.Interaction.ModifyOriginalResponseAsync(z => z.Components = compo(cache.currenttype, cache.page = 0, cache.opti));
            while (!cache.responded)
                await Task.Delay(250);
            cache.responded = false;
            var item = datasource.Items.Where(z => z.Text == cache.response.Data.Values.First()).First();
            pk.ApplyHeldItem(item != null ? item.Value : 0, sav.Context);
            cache.currenttype = "Level";
            cache.opti = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100" };
            await Context.Interaction.ModifyOriginalResponseAsync(z => z.Components = compo(cache.currenttype, cache.page = 0, cache.opti));
            while (!cache.responded)
                await Task.Delay(250);
            cache.responded = false;
            pk.CurrentLevel = int.Parse(cache.response.Data.Values.First());
            cache.currenttype = "Ball";
            List<string> balllist = BallApplicator.GetLegalBalls(pk).Select(z => z.ToString()).ToList();
            balllist.Insert(0, "Any");
            cache.opti = balllist.ToArray();
            await Context.Interaction.ModifyOriginalResponseAsync(z => z.Components = compo(cache.currenttype, cache.page = 0, cache.opti));
            while (!cache.responded)
                await Task.Delay(250);
            cache.responded = false;
            if (cache.response.Data.Values.First() != "Any")
            {
                var ball = BallApplicator.GetLegalBalls(pk).Where(z => z.ToString() == cache.response.Data.Values.First()).First();
                pk.Ball = (int)ball;
            }
            
            simpletradecache.Remove(cache);
            sav.Legalize(pk);
            if (pk.Generation < 6)
                pk.CurrentHandler = 1;
            if (pk is PK7 pk7)
            {
                pk7.SetDefaultRegionOrigins(pk.Language);
                pk = pk7;
            }
            if (pk is PK6 pk6)
            {
                pk6.SetDefaultRegionOrigins(pk.Language);
                pk = pk6;
            }

            if (ItemStorage7USUM.GetCrystalKey((ushort)pk.HeldItem, out var key))
                pk.HeldItem = 0;
            if (!new LegalityAnalysis(pk).Valid || FormInfo.IsFusedForm(pk.Species, pk.Form, pk.Format))
            {
                var reason = FormInfo.IsFusedForm(pk.Species, pk.Form, pk.Format) ? "You can not trade Fused Pokemon because you won't have the originals when they de-fuse and your save file will crash" : $"I wasn't able to create a {(Species)set.Species} from that set.";
                var imsg = $"Oops! {reason}";

                imsg += $"\n{new ShowdownSet(pk).SetAnalysis(sav, pk)}";
                
                await FollowupAsync(imsg).ConfigureAwait(false);
                return;
            }
            try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued"); return; }
            queuesystem tobequeued;
            tobequeued = new queuesystem() { discordcontext = Context, friendcode = "", IGN = TrainerName, mode = botmode.trade, tradepokemon = pk };
            The_Q.Enqueue(tobequeued);
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} : {TrainerName} - Added to the queue. Current Position: {The_Q.Count()}. Receiving: {(Species)pk.Species}");
           
            return;

        }
        public static SelectMenuBuilder GetSelectMenu(string type, int page, string[] options)
        {
            var returnMenu = new SelectMenuBuilder().WithCustomId(type).WithPlaceholder($"Select a {type}");
            if (options.Length > 25)
            {
                var newoptions = options.Skip(page*25).Take(25);
                foreach(var option in newoptions)
                {
                    returnMenu.AddOption(option, option);
                }
            }
            else
            {
                foreach (var option in options)
                    returnMenu.AddOption(option, option);
            }
            return returnMenu;

        }
        public static MessageComponent compo(string type, int page, string[] options)
        {


            var nextbutton = new ActionRowBuilder().WithButton("Next 25", "next");
            var previousbutton = new ActionRowBuilder().WithButton("Previous 25", "prev");
            var select = new ComponentBuilder().WithSelectMenu(GetSelectMenu(type, page, options), 0);
            select.AddRow(nextbutton);
            select.AddRow(previousbutton);



            return select.Build();
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
            await FollowupAsync($"{Context.User.Mention} - added to the dump queue. Current Position: {The_Q.Count()}.");
        }
        [SlashCommand("hi","hi")]
        public  async Task some()
        {
           
                await DeferAsync(ephemeral:true);

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

            var setsplit = finalset.Split("\r\n");
            var TheShow = new ShowdownSet(finalset);

            if (TheShow.Species == 0)
            {


                var langID = (LanguageID)0;

                if (setsplit[0].IndexOf("(") > -1 && setsplit[0].IndexOf(")") - 2 != setsplit[0].IndexOf("("))
                {
                    var spec = setsplit[0][(setsplit[0].IndexOf("(") + 1)..setsplit[0].IndexOf(")")];
                    var nick = setsplit[0][..setsplit[0].IndexOf("(")];
                    ushort specid = 0;

                    for (int i = 1; i < 11; i++)
                    {
                        SpeciesName.TryGetSpecies(spec, i, out specid);
                        if (specid > 0)
                        {
                            langID = (LanguageID)i;
                            break;
                        }
                    }
                    if (specid == 0)
                        return null;
                    var engspec = SpeciesName.GetSpeciesNameGeneration((ushort)specid, 2, 9);
                    setsplit[0] = nick + $"({engspec})";

                    if (!finalset.Contains("Language:"))
                    {
                        var langs = Enum.GetNames(typeof(LanguageID));
                        if (setsplit.Length > 2)
                        {
                            var setlist = setsplit.ToList();
                            setlist.Insert(1, $"Language: {langs[(int)langID]}");
                            setsplit = setlist.ToArray();
                        }
                        else
                            setsplit = setsplit.Append($"Language: {langs[(int)langID]}").ToArray();

                    }
                    var result = new StringBuilder();
                    foreach (var item in setsplit)
                    {
                        result.Append(item + "\r\n");
                    }
                    finalset = result.ToString();
                    TheShow = new ShowdownSet(finalset);
                }
                else
                {
                    if (setsplit[0].IndexOf("(") > -1)
                    {
                        var spec = setsplit[0][..(setsplit[0].IndexOf("(") - 1)];
                        ushort specid = 0;

                        for (int i = 1; i < 11; i++)
                        {
                            SpeciesName.TryGetSpecies(spec, i, out specid);
                            if (specid > 0)
                            {
                                langID = (LanguageID)i;
                                break;
                            }
                        }
                        if (specid == 0)
                            return null;
                        var engspec = SpeciesName.GetSpeciesNameGeneration((ushort)specid, 2, 9);
                        setsplit[0] = setsplit[0].Replace(spec, "");
                        setsplit[0] = setsplit[0].Insert(0, engspec);
                        if (!finalset.Contains("Language:"))
                        {
                            var langs = Enum.GetNames(typeof(LanguageID));
                            if (setsplit.Length > 2)
                            {
                                var setlist = setsplit.ToList();
                                setlist.Insert(1, $"Language: {langs[(int)langID]}");
                                setsplit = setlist.ToArray();
                            }
                            else
                                setsplit = setsplit.Append($"Language: {langs[(int)langID]}").ToArray();

                        }
                        var result = new StringBuilder();
                        foreach (var item in setsplit)
                        {
                            result.Append(item + "\r\n");
                        }
                        finalset = result.ToString();
                        TheShow = new ShowdownSet(finalset);
                    }
                    else if (setsplit[0].Contains("@"))
                    {
                        var spec = setsplit[0][0..(setsplit[0].IndexOf("@") - 1)];
                        ushort specid = 0;

                        for (int i = 1; i < 11; i++)
                        {
                            SpeciesName.TryGetSpecies(spec, i,out specid);
                            if (specid >0)
                            {
                                langID = (LanguageID)i;
                                break;
                            }

                        }
                        if (specid ==0)
                            return null;
                        var engspec = SpeciesName.GetSpeciesNameGeneration((ushort)specid, 2, 9);
                        setsplit[0] = setsplit[0].Replace(spec, "");
                        setsplit[0] = setsplit[0].Insert(0, engspec);
                        if (!finalset.Contains("Language:"))
                        {
                            var langs = Enum.GetNames(typeof(LanguageID));
                            if (setsplit.Length > 2)
                            {
                                var setlist = setsplit.ToList();
                                setlist.Insert(1, $"Language: {langs[(int)langID]}");
                                setsplit = setlist.ToArray();
                            }
                            else
                                setsplit = setsplit.Append($"Language: {langs[(int)langID]}").ToArray();

                        }

                        var result = new StringBuilder();
                        foreach (var item in setsplit)
                        {
                            result.Append(item + "\r\n");
                        }
                        finalset = result.ToString();
                        TheShow = new ShowdownSet(finalset);
                    }
                    else
                    {
                        var spec = setsplit[0].Trim();

                        ushort specid = 0;

                        for (int i = 1; i < 11; i++)
                        {
                            SpeciesName.TryGetSpecies(spec, i, out specid);
                            if (specid > 0)
                            {
                                langID = (LanguageID)i;
                                break;
                            }
                        }
                        if (specid == 0)
                            return null;
                        var engspec = SpeciesName.GetSpeciesName((ushort)specid, (int)LanguageID.English);

                        setsplit[0] = engspec;
                        if (!finalset.Contains("Language:"))
                        {
                            var langs = Enum.GetNames(typeof(LanguageID));
                            if (setsplit.Length > 2)
                            {
                                var setlist = setsplit.ToList();
                                setlist.Insert(1, $"Language: {langs[(int)langID]}");
                                setsplit = setlist.ToArray();
                            }
                            else
                            {
                                setsplit = setsplit.Append($"Language: {langs[(int)langID]}").ToArray();
                            }

                        }
                        var result = new StringBuilder();
                        foreach (var item in setsplit)
                        {
                            result.Append(item + "\r\n");
                        }
                        finalset = result.ToString();
                        TheShow = new ShowdownSet(finalset);
                    }
                }


            }

            foreach (var line in setsplit)
            {
                if (TheShow.Nickname != String.Empty)
                    continue;
                if (!char.IsUpper(line[0]))
                {
                    if (line[0] != '-')
                        return null;
                }
            }

            return TheShow;
        }

        private static readonly string[] splittables =
        {
            "Ability:", "EVs:", "IVs:", "Shiny:", "Gigantamax:", "Ball:", "- ", "Level:",
            "Happiness:", "Language:", "OT:", "OTGender:", "TID:", "SID:", "Alpha:",
            "Adamant Nature", "Bashful Nature", "Brave Nature", "Bold Nature", "Calm Nature",
            "Careful Nature", "Docile Nature", "Gentle Nature", "Hardy Nature", "Hasty Nature",
            "Impish Nature", "Jolly Nature", "Lax Nature", "Lonely Nature", "Mild Nature",
            "Modest Nature", "Naive Nature", "Naughty Nature", "Quiet Nature", "Quirky Nature",
            "Rash Nature", "Relaxed Nature", "Sassy Nature", "Serious Nature", "Timid Nature", "Tera Type:"
        };
        public static string GetFormattedShowdownText(PKM pkm)
        {
            var newShowdown = new List<string>();
            var showdown = ShowdownParsing.GetShowdownText(pkm);
            foreach (var line in showdown.Split('\n'))
                newShowdown.Add($"\n{line}");

            int index = newShowdown.FindIndex(z => z.Contains("Nature"));
            if (pkm.Ball > (int)Ball.None && index != -1)
                newShowdown.Insert(newShowdown.FindIndex(z => z.Contains("Nature")), $"\nBall: {(Ball)pkm.Ball} Ball");

            index = newShowdown.FindIndex(x => x.Contains("Shiny: Yes"));
            if (pkm is PK8 && pkm.IsShiny && index != -1)
            {
                if (pkm.ShinyXor == 0 || pkm.FatefulEncounter)
                    newShowdown[index] = "\nShiny: Square\r";
                else newShowdown[index] = "\nShiny: Star\r";
            }

            var extra = new string[] { $"\nOT: {pkm.OT_Name}", $"\nTID: {pkm.DisplayTID:000000}", $"\nSID: {pkm.DisplaySID:0000}", $"\nOTGender: {(Gender)pkm.OT_Gender}", $"\nLanguage: {(LanguageID)pkm.Language}", $"{(pkm.IsEgg ? "\nIsEgg: Yes" : "")}" };
            newShowdown.InsertRange(1, extra);
            return Format.Code(string.Join("", newShowdown).Trim());
        }
    }
}
public class simpletradeobject
{
    public int page { get; set; } = 0;
    public  string currenttype { get; set; } = "Species";
    public  string[] opti { get; set; } = Array.Empty<string>();
    public SocketMessageComponent response { get; set; }
    public bool responded { get; set; } = false;
    public SocketUser user { get; set; }
    public simpletradeobject()
    {

    }
    
}