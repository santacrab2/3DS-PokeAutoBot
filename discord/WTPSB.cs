using PKHeX.Core;
using PKHeX.Core.AutoMod;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using static PKHeX.Core.Species;
using System.Reflection;

namespace _3DS_link_trade_bot
{
   
    public class WTPSB : InteractionModuleBase<SocketInteractionContext> 
    {
        public static bool buttonpressed = false;
        public static bool tradepokemon = false;
        public static CancellationTokenSource WTPsource = new();
        public static GameVersion Game =  GameVersion.USUM;
        public static string guess = "";
        public static SocketUser usr;
        public static string ign;
        public static int randspecies;
        public static SocketInteractionContext con;
        [SlashCommand("wtpstart","owner only")]
        [RequireOwner]
        public async Task WhoseThatPokemon()
        {
            ITextChannel wtpchannel = (ITextChannel)Context.Channel;
            await wtpchannel.ModifyAsync(newname => newname.Name = wtpchannel.Name.Replace("❌", "✅"));
            await RespondAsync("\"who's that pokemon\" mode started!",ephemeral:true); 
            await wtpchannel.AddPermissionOverwriteAsync(wtpchannel.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Allow));
            while (!WTPsource.IsCancellationRequested)
            {
                Stopwatch sw = new();
                sw.Restart();
                Random random = new Random();
                var code = random.Next(99999999);
                var Dex = GetPokedex();
                randspecies = Dex[random.Next(Dex.Length)];
                EmbedBuilder embed = new EmbedBuilder();
                embed.Title = "Who's That Pokemon";
                embed.AddField(new EmbedFieldBuilder { Name = "instructions", Value = "Type /guess <pokemon name> to guess the name of the pokemon displayed and you get that pokemon in your actual game!" });
                if (randspecies < 891)
                    embed.ImageUrl = $"https://logoassetsgame.s3.us-east-2.amazonaws.com/wtp/pokemon/{randspecies}q.png";
                else
                    embed.ImageUrl = $"https://raw.githubusercontent.com/santacrab2/SysBot.NET/RNGstuff/finalimages/{randspecies}q.png";
                await wtpchannel.SendMessageAsync(embed: embed.Build());
                while (guess.ToLower() != ((Species)randspecies).ToString().ToLower() && sw.ElapsedMilliseconds / 1000 < 600)
                {
                    await Task.Delay(25);
                }
               
                var entry = File.ReadAllLines("deps/DexFlavor.txt")[randspecies];
                embed = new EmbedBuilder().WithFooter(entry);
                embed.Title = $"It's {(Species)randspecies}";
                embed.AddField(new EmbedFieldBuilder { Name = "instructions", Value = $"Type /guess <pokemon name> to guess the name of the pokemon displayed and you get that pokemon in your actual game!" });
                if (randspecies < 891)
                    embed.ImageUrl = $"https://logoassetsgame.s3.us-east-2.amazonaws.com/wtp/pokemon/{randspecies}a.png";
                else
                    embed.ImageUrl = $"https://raw.githubusercontent.com/santacrab2/SysBot.NET/RNGstuff/finalimages/{randspecies}a.png";
                await wtpchannel.SendMessageAsync(embed: embed.Build());
      
                if (guess.ToLower() == ((Species)randspecies).ToString().ToLower())
                {
                    var compmessage = new ComponentBuilder().WithButton("Yes","wtpyes",ButtonStyle.Success).WithButton("No","wtpno",ButtonStyle.Danger);
                    var embedmes = new EmbedBuilder();
                    embedmes.AddField("Receive Pokemon?", $"Would you like to receive {(Species)randspecies} in your game?");
                    await ReplyAsync(embed: embedmes.Build(), components: compmessage.Build());
                    
                    while (!buttonpressed)
                    {
                        await Task.Delay(25);
                    }
                    if (tradepokemon)
                    {
                        var set = new ShowdownSet($"{(Species)randspecies}\nShiny: Yes");
                        var template = new PK7();
                        var sav = TrainerSettings.GetSavedTrainerData(7);
                        var pk = sav.GetLegalFromTemplate(template,set, out var result);
                        pk = pk.Legalize();
                        if (!new LegalityAnalysis(pk).Valid)
                        {
                            set = new ShowdownSet(((Species)randspecies).ToString());
                            template = new PK7();
                            sav = TrainerSettings.GetSavedTrainerData(7);
                            pk = sav.GetLegalFromTemplate(template, set, out result);
                            pk = pk.Legalize();
                        }
                        pk.Ball = BallApplicator.ApplyBallLegalByColor(pk);
                        int[] sugmov = MoveSetApplicator.GetMoveSet(pk, true);
                        pk.SetMoves(sugmov);
                        int natue = random.Next(24);
                        pk.Nature = natue;
                        EffortValues.SetRandom(pk.EVs, 8);

                        try { await Context.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await FollowupAsync("enable private messages from users on the server to be queued"); return; }
                        var tobequeued = new queuesystem() { discordcontext = con, friendcode = "", IGN = ign, tradepokemon = pk, mode = botmode.trade };
                        MainHub.The_Q.Enqueue(tobequeued);
                        await FollowupAsync($"{Context.User.Username} - Added to the queue. Current Position: {MainHub.The_Q.Count()}. Receiving: {(Species)pk.Species}");

                    }
                    usr = null;
                    guess = "";
                    tradepokemon = false;
                    buttonpressed = false;
                }
                usr = null;
                guess = "";
            }
            WTPsource = new();
        }
        [EnabledInDm(false)]
        [DefaultMemberPermissions(GuildPermission.ViewChannel)]
        [SlashCommand("guess","guess the pokemon")]
       
        public async Task WTPguess([Summary("pokemon")]string userguess, [Summary(description: "In Game Trainer Name, if you plan to receive your guess in trade")] string TrainerName="")
        {
            await DeferAsync();
            if (userguess.ToLower() == ((Species)randspecies).ToString().ToLower())
            {
                await FollowupAsync($"{Context.User.Username} You are correct! It's {userguess}");
                guess = userguess;
                usr = Context.User;
                con = Context;
                ign = TrainerName;
            }
            else
                await FollowupAsync($"{Context.User.Username} You are incorrect. It is not {userguess}");
        }
        [SlashCommand("wtpcancel","owner only")]
        [RequireOwner]
        public async Task wtpcancel()
        {
            WTPsource.Cancel();
            await RespondAsync("\"Who's That Pokemon\" mode stopped.",ephemeral:true);
            ITextChannel wtpchannel = (ITextChannel)Context.Channel;
            await wtpchannel.ModifyAsync(newname => newname.Name = wtpchannel.Name.Replace("✅","❌"));
            await wtpchannel.AddPermissionOverwriteAsync(wtpchannel.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
        }

        private int[] GetPokedex()
        {
            List<int> dex = new();
            for (int i = 1; i <= 907; i++)
            {
                var entry = PersonalTable.USUM.GetFormEntry(i,0);
                if (entry is PersonalInfoSM { IsPresentInGame: false})
                    continue;

                var species = SpeciesName.GetSpeciesNameGeneration(i, 2, 8);
                var set = new ShowdownSet($"{species}{(i == (int)NidoranF ? "-F" : i == (int)NidoranM ? "-M" : "")}");
                var template = new PK7();
                var sav = TrainerSettings.GetSavedTrainerData(7);
                _ = sav.GetLegalFromTemplate(template, set, out var result);

                if (result == LegalizationResult.Regenerated)
                    dex.Add(i);
            }
            return dex.ToArray();
        }
        private async Task HandleMessageAsync(SocketMessage arg)
        {
            if (arg is not SocketUserMessage msg)
                return;
            guess = msg.ToString();
        }
    }
}
