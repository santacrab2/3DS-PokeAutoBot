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
        [DefaultMemberPermissions(GuildPermission.BanMembers)]
        [SlashCommand("wtpstart","owner only")]
        [RequireOwner]
        public async Task WhoseThatPokemon()
        {
            await DeferAsync(ephemeral: true);
            ITextChannel wtpchannel = (ITextChannel)Context.Channel;
            await wtpchannel.ModifyAsync(newname => newname.Name = wtpchannel.Name.Replace("❌", "✅"));
            await FollowupAsync("\"who's that pokemon\" mode started!",ephemeral:true); 
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
                embed.AddField(new EmbedFieldBuilder { Name = "instructions", Value = "1. add your friendcode to the bots 3ds with /addfc command(only once)\n2. Type /guess pokemon to guess. Include your in game name if you plan to receive it in a trade!" });
                if (randspecies < 891)
                    embed.ImageUrl = $"https://logoassetsgame.s3.us-east-2.amazonaws.com/wtp/pokemon/{randspecies}q.png";
                else
                    embed.ImageUrl = $"https://raw.githubusercontent.com/santacrab2/SysBot.NET/RNGstuff/finalimages/{randspecies}q.png";
                await wtpchannel.SendMessageAsync(embed: embed.Build());
                while (guess.ToLower() != SpeciesName.GetSpeciesName(randspecies,2).ToLower() && sw.ElapsedMilliseconds / 1000 < 600)
                {
                    await Task.Delay(25);
                }
               
                var entry = File.ReadAllLines("deps/DexFlavor.txt")[randspecies];
                embed = new EmbedBuilder().WithFooter(entry);
                embed.Title = $"It's {SpeciesName.GetSpeciesName(randspecies, 2)}";
                embed.AddField(new EmbedFieldBuilder { Name = "instructions", Value = $"Type /guess <pokemon name> to guess the name of the pokemon displayed and you get that pokemon in your actual game!" });
                if (randspecies < 891)
                    embed.ImageUrl = $"https://logoassetsgame.s3.us-east-2.amazonaws.com/wtp/pokemon/{randspecies}a.png";
                else
                    embed.ImageUrl = $"https://raw.githubusercontent.com/santacrab2/SysBot.NET/RNGstuff/finalimages/{randspecies}a.png";
                await wtpchannel.SendMessageAsync(embed: embed.Build());
      
                if (guess.ToLower() == SpeciesName.GetSpeciesName(randspecies, 2).ToLower())
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
                        var set = new ShowdownSet($"{SpeciesName.GetSpeciesNameGeneration(randspecies,2,6)}\nShiny: Yes");
                     
                        var trainer = TrainerSettings.GetSavedTrainerData(7);
                        if (NTR.game <3)
                            trainer = TrainerSettings.GetSavedTrainerData(6);
                        var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
                        var pk = sav.GetLegalFromSet(set, out var result);
                        pk=pk.Legalize();
                        if (pk is PK7 pkk)
                        {
                            pkk.SetDefaultRegionOrigins();
                            pk = pkk;
                        }

                        if (!new LegalityAnalysis(pk).Valid)
                        {
                            set = new ShowdownSet(SpeciesName.GetSpeciesNameGeneration(randspecies,2,6));
                            
                            trainer = TrainerSettings.GetSavedTrainerData(7);
                            if (NTR.game <3)
                                trainer = TrainerSettings.GetSavedTrainerData(6);
                            sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
                            pk = sav.GetLegalFromSet( set, out result);
                            pk.Legalize();
                            if (pk is PK7 pk7)
                            {
                                pk7.SetDefaultRegionOrigins();
                                pk = pk7;
                            }
                        }
                        pk.Ball = BallApplicator.ApplyBallLegalByColor(pk);
                        int[] sugmov = MoveSetApplicator.GetMoveSet(pk, true);
                        pk.SetMoves(sugmov);
                        int natue = random.Next(24);
                        pk.Nature = natue;
                        

                        try { await con.User.SendMessageAsync("I have added you to the queue. I will message you here when the trade starts"); } catch { await con.Interaction.FollowupAsync("enable private messages from users on the server to be queued"); return; }
                        var tobequeued = new queuesystem() { discordcontext = con, friendcode = "", IGN = ign, tradepokemon = pk, mode = botmode.trade };
                        MainHub.The_Q.Enqueue(tobequeued);
                        await con.Interaction.FollowupAsync($"{usr.Username} - Added to the queue. Current Position: {MainHub.The_Q.Count()}. Receiving: {(Species)pk.Species}");

                    }
                    usr = null;
                    guess = "";
                    con = null;
                    tradepokemon = false;
                    buttonpressed = false;
                }
                con = null;
                usr = null;
                guess = "";
            }
            await wtpchannel.ModifyAsync(newname => newname.Name = wtpchannel.Name.Replace("✅","❌"));
            await wtpchannel.AddPermissionOverwriteAsync(wtpchannel.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
            WTPsource = new();
        }
        [EnabledInDm(false)]
        [DefaultMemberPermissions(GuildPermission.ViewChannel)]
        [SlashCommand("guess","guess the pokemon")]
       
        public async Task WTPguess([Summary("pokemon")]string userguess, [Summary(description: "In Game Trainer Name, if you plan to receive your guess in trade")] string TrainerName)
        {
            await DeferAsync();
            if (userguess.ToLower() == SpeciesName.GetSpeciesName(randspecies, 2).ToLower())
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
        [DefaultMemberPermissions(GuildPermission.BanMembers)]
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
                if (NTR.game >2)
                {
                    var entry = PersonalTable.USUM.GetFormEntry(i, 0);


                    if (entry is PersonalInfoSM { IsPresentInGame: false } )
                        continue;
                }else
                {
                    var entry = PersonalTable.AO.GetFormEntry(i, 0);
                    if (entry is PersonalInfoORAS { IsPresentInGame: false })
                        continue;
                }    
                var species = SpeciesName.GetSpeciesNameGeneration(i, 2, 6);
                var set = new ShowdownSet($"{species}{(i == (int)NidoranF ? "-F" : i == (int)NidoranM ? "-M" : "")}");
                
                var trainer = TrainerSettings.GetSavedTrainerData(7);
                if (NTR.game <3)
                    trainer = TrainerSettings.GetSavedTrainerData(6);
                var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
                _ = sav.GetLegalFromSet(set, out var result);

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
