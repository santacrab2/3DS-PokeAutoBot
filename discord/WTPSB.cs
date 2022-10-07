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
using static _3DS_link_trade_bot.Form1;

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
        public static ushort randspecies;
        public static SocketInteractionContext con;
     
        public async Task WhoseThatPokemon()
        {

            ITextChannel wtpchannel = (ITextChannel) await discordmain._client.GetChannelAsync(_settings.Discordsettings.BotWTPChannel);
  
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
                    await wtpchannel.SendMessageAsync(embed: embedmes.Build(), components: compmessage.Build());
                    
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
                        ushort[] sugmov = MoveSetApplicator.GetMoveSet(pk, true);
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
            
            WTPsource = new();
        }
        [EnabledInDm(false)]
        [DefaultMemberPermissions(GuildPermission.ViewChannel)]
        [SlashCommand("guess","guess the pokemon")]
       
        public async Task WTPguess([Summary("pokemon")]string userguess, [Summary(description: "In Game Trainer Name, if you plan to receive your guess in trade")] string TrainerName)
        {
            await DeferAsync();
            if (MainHub.The_Q.Count != 0)
            {
                if (MainHub.The_Q.Any(z => z.discordcontext.User == Context.User))
                {
                    await FollowupAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
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
            
        }

        private ushort[] GetPokedex()
        {
            List<ushort> dex = new();
            var dexcount = 807;
            if (NTR.game < 3)
                dexcount = 721;
            for (ushort i = 1; i <= dexcount; i++)
            {
               
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
