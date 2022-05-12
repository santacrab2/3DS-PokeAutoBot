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

namespace _3DS_link_trade_bot
{
    public class discordmain
    {
        public static Discord.Interactions.IResult result;
        public static readonly WebClient webClient = new WebClient();
        public static DiscordSocketClient _client;
        public static Settings Unisettings;

        public IServiceProvider _services;
        public discordmain(Settings settings)
        {
            Unisettings = settings;
        }

      

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += ready;

            
            //var token = File.ReadAllText("token.txt");

            await _client.LoginAsync(TokenType.Bot, Unisettings.Discordsettings.token);
            await _client.StartAsync();
           // CommandHandler ch = new CommandHandler(_client, _commands);
            //await ch.InstallCommandsAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private async Task ready()
        {
           
            var _interactionService = new InteractionService(_client);
            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            await _interactionService.RegisterCommandsToGuildAsync(872587205787394119);
            _client.InteractionCreated += async interaction =>
            {

                var ctx = new SocketInteractionContext(_client, interaction);
                result = await _interactionService.ExecuteCommandAsync(ctx, null);
            };
            _client.SlashCommandExecuted += slashtask;
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
    }
}
