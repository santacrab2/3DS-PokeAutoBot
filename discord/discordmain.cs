using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Net;
using static _3DS_link_trade_bot.Program;
using static _3DS_link_trade_bot.Form1;
using PKHeX.Core;

namespace _3DS_link_trade_bot
{
    public class discordmain
    {
        public static Discord.Interactions.IResult result;
        public static readonly WebClient webClient = new WebClient();
        public static DiscordSocketClient _client;
        //public static Settings Unisettings;

        public IServiceProvider _services;
  

      

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += ready;

            
            //var token = File.ReadAllText("token.txt");

            await _client.LoginAsync(TokenType.Bot, _settings.Discordsettings.token);
            await _client.StartAsync();
            // CommandHandler ch = new CommandHandler(_client, _commands);
            //await ch.InstallCommandsAsync();
            _client.MessageReceived += readpkfiles;
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private async Task ready()
        {
           
            var _interactionService = new InteractionService(_client);
            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            var gilds = _client.Guilds.ToArray();
            foreach(var gild in gilds)
                await _interactionService.RegisterCommandsToGuildAsync(gild.Id);
            
            _client.InteractionCreated += async interaction =>
            {

                var ctx = new SocketInteractionContext(_client, interaction);
                result = await _interactionService.ExecuteCommandAsync(ctx, null);
            };
            _client.SlashCommandExecuted += slashtask;
            _client.ButtonExecuted += handlebuttonpress;
        }
        public async Task handlebuttonpress(SocketMessageComponent arg)
        {

            if (arg.Data.CustomId == "wtpyes")
            {
                WTPSB.buttonpressed = true;
                WTPSB.tradepokemon = true;
                await arg.Message.DeleteAsync();
                return;
            }
            if (arg.Data.CustomId == "wtpno")
            {
                WTPSB.buttonpressed = true;
                WTPSB.tradepokemon = false;
                await arg.Message.DeleteAsync();
                return;
            }
        }
        public Task slashtask(SocketSlashCommand arg1)
        {

            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;

        }
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public static async Task<byte[]> DownloadFromUrlAsync(string url)
        {
            return await webClient.DownloadDataTaskAsync(url);
        }
        private async Task readpkfiles(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) 
                return;
            if(message.Attachments.Count > 0)
            {
                var attach = message.Attachments.FirstOrDefault();
                if (attach == default)
                    return;
                var att = Format.Sanitize(attach.Filename);
                if (!EntityDetection.IsSizePlausible(attach.Size))
                    return;

                var pokme = EntityFormat.GetFromBytes(await DownloadFromUrlAsync(attach.Url), EntityContext.Gen7);
                var newShowdown = new List<string>();
                var showdown = ShowdownParsing.GetShowdownText(pokme);
                foreach (var line in showdown.Split('\n'))
                    newShowdown.Add(line);

                if (pokme.IsEgg)
                    newShowdown.Add("\nPokémon is an egg");
                if (pokme.Ball > (int)Ball.None)
                    newShowdown.Insert(newShowdown.FindIndex(z => z.Contains("Nature")), $"Ball: {(Ball)pokme.Ball} Ball");
                if (pokme.IsShiny)
                {
                    var index = newShowdown.FindIndex(x => x.Contains("Shiny: Yes"));
                    if (pokme.ShinyXor == 0 || pokme.FatefulEncounter)
                        newShowdown[index] = "Shiny: Square\r";
                    else newShowdown[index] = "Shiny: Star\r";
                }
                var SID = NTR.game>2?string.Format("{0:0000}",pokme.TrainerSID7):string.Format("{0:00000}",pokme.SID);
                var TID = NTR.game>2?string.Format("{0:000000}",pokme.TrainerID7):string.Format("{0:00000}",pokme.TID);
                newShowdown.InsertRange(1, new string[] { $"OT: {pokme.OT_Name}", $"TID: {TID}", $"SID: {SID}", $"OTGender: {(Gender)pokme.OT_Gender}", $"Language: {(LanguageID)pokme.Language}" });
                await message.Channel.SendMessageAsync(Format.Code(string.Join("\n", newShowdown).TrimEnd()));
            }
        }
    }
}
