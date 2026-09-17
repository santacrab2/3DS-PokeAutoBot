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
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace _3DS_link_trade_bot
{
    public class discordmain
    {
        public static Discord.Interactions.IResult result;
        public static readonly HttpClient httpClient = new();
        public static DiscordSocketClient _client;
        //public static Settings Unisettings;

        public IServiceProvider _services;
  

      

        public async Task MainAsync()
        {
           
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += ready;

            // Source - https://stackoverflow.com/a/56706459
            // Posted by Raphtaliyah
            // Retrieved 2026-09-06, License - CC BY-SA 4.0
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1");

            //var token = File.ReadAllText("token.txt");

            await _client.LoginAsync(TokenType.Bot, _settings.Discordsettings.token);
            await _client.StartAsync();
            // CommandHandler ch = new CommandHandler(_client, _commands);
            //await ch.InstallCommandsAsync();
            _client.MessageReceived += readpkfiles;
            Task.Run(PostSerebii);
            Task.Run(PostPKHEX);
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private async Task ready()
        {
            ChangeStatus("discord Ready Event triggered");
           
            var _interactionService = new InteractionService(_client);
            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            var gilds = _client.Guilds.ToArray();
            foreach (var gild in gilds)
            {

                await _interactionService.RegisterCommandsToGuildAsync(gild.Id, true);
                if (_settings.old3ds)
                {
                    ChangeStatus("old 3ds detected removing commands");
                    if ((Mode)form1.BotMode.SelectedItem == Mode.FriendCodeOnly)
                    {
                        ChangeStatus("Removing Trade Module Commands for Friend Code Mode");
                        var commands = await gild.GetApplicationCommandsAsync();
                        foreach (var command in commands)
                        {
                            if (command.Name == "dump")
                                await command.DeleteAsync();
                            else if (command.Name == "trade")
                                await command.DeleteAsync();
                            else if (command.Name == "guess")
                                await command.DeleteAsync();
                        }

                    }
                    if ((Mode)form1.BotMode.SelectedItem == Mode.FlexTrade)
                    {
                        ChangeStatus("Removing Friend Code command for Flex Trade");
                        var commands = await gild.GetApplicationCommandsAsync();
                        foreach(var command in commands)
                        {
                            if (command.Name == "addfc")
                                await command.DeleteAsync();
                        }
                    }

                    
                }
            }
            
            _client.InteractionCreated += async interaction =>
            {

                var ctx = new SocketInteractionContext(_client, interaction);
                result = await _interactionService.ExecuteCommandAsync(ctx, null);
            };
            _client.SlashCommandExecuted += slashtask;
            _client.ButtonExecuted += handlebuttonpress;
            _client.SelectMenuExecuted += MenuHandler;
        }
        public async Task MenuHandler(SocketMessageComponent arg)
        {
            var currentcache = TradeModule.simpletradecache.Find(z => z.user == arg.User);
            currentcache.response = arg;
            currentcache.responded = true;
            await arg.RespondAsync();
        }
        public async Task handlebuttonpress(SocketMessageComponent arg)
        {
            var currentcache = TradeModule.simpletradecache.Find(z => z.user == arg.User);
            var lastpage = currentcache.opti.Length % 25 == 0 ? (currentcache.opti.Length / 25) - 1 : currentcache.opti.Length / 25;
            switch (arg.Data.CustomId)
            {
                
                case "next":
                    if (currentcache.page < lastpage)
                    {
                        currentcache.page++;
                            
                        await arg.UpdateAsync(z => z.Components = TradeModule.compo(currentcache.currenttype, currentcache.page, currentcache.opti)); 
                    }
                    else
                    {
                        currentcache.page= 0;
                        await arg.UpdateAsync(z => z.Components = TradeModule.compo(currentcache.currenttype, currentcache.page, currentcache.opti));
                    }
                    break;
                case "prev":
                    if (currentcache.page > 0)
                    {
                        currentcache.page--;
                        await arg.UpdateAsync(z => z.Components = TradeModule.compo(currentcache.currenttype, currentcache.page, currentcache.opti));
                    }
                    else if (currentcache.opti.Length > 25)
                    {
                        if (currentcache.opti.Length % 25 == 0)
                            currentcache.page = (currentcache.opti.Length / 25) - 1;
                        else
                            currentcache.page = (currentcache.opti.Length / 25);
                        await arg.UpdateAsync(z => z.Components = TradeModule.compo(currentcache.currenttype, currentcache.page, currentcache.opti));
                    }
                    break;

            }
        }
        public Task slashtask(SocketSlashCommand arg1)
        {

            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        arg1.FollowupAsync("some kind of unmet precondition error? What does that even mean?");
                        break;
                    case InteractionCommandError.UnknownCommand:
                        arg1.FollowupAsync("That's not a command?! Watcha doin?");
                        break;
                    case InteractionCommandError.BadArgs:
                        arg1.FollowupAsync("You Did not fill in all of the required parameters or Your Format is incorrect, pay attention to what you are doing and try again!");
                        break;
                    case InteractionCommandError.Exception:
                        arg1.FollowupAsync("Exception thrown, try again?");
                        break;
                    case InteractionCommandError.Unsuccessful:
                        arg1.FollowupAsync("The command was just simply unsuccessful");
                        break;
                    default:
                        arg1.FollowupAsync("This error could literally be anything, this is the default message!");
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
            return await httpClient.GetByteArrayAsync(url);
        }
        private async Task readpkfiles(SocketMessage messageParam)
        {
            
            var message = messageParam as SocketUserMessage;
            if(message.Channel is SocketDMChannel && !message.Author.IsBot)
            {
               var logs= (ITextChannel)await _client.GetChannelAsync(872613946471899196);
               await logs.SendMessageAsync($"DM Log: User: {message.Author.Username} ID: {message.Author.Id} Message: {message.Content}");
            }
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
              
                newShowdown.InsertRange(1, new string[] { $"OT: {pokme.OT_Name}", $"TID: {pokme.TrainerTID7}", $"SID: {pokme.TrainerSID7}", $"OTGender: {(Gender)pokme.OT_Gender}", $"Language: {(LanguageID)pokme.Language}" });
                await message.Channel.SendMessageAsync(Format.Code(string.Join("\n", newShowdown).TrimEnd()));
            }
        }
        public static async void PostSerebii()
        {
            while (true)
            {
                var serebiiposts = await httpClient.GetAsync("https://public.api.bsky.app/xrpc/app.bsky.feed.getAuthorFeed?actor=serebii.bsky.social");
                if (serebiiposts.StatusCode != HttpStatusCode.OK)
                {
                    ChangeStatus("Error fetching Serebii posts: " + serebiiposts.StatusCode);
                    await Task.Delay(60000); // Wait for 60 seconds before retrying
                    continue;
                }
                var serebiicontent = await serebiiposts.Content.ReadAsStringAsync();
                var j = JsonDocument.Parse(serebiicontent);
                var latesturi = j.RootElement.GetProperty("feed").EnumerateArray().FirstOrDefault().GetProperty("post").GetProperty("uri").GetString();
                if (latesturi is not null && latesturi != _settings.SerebiiUri)
                {
                    int i = 0;
                    var urinicycle = j.RootElement.GetProperty("feed").EnumerateArray().Select(z => z.GetProperty("post").GetProperty("uri").GetString()).ToList();
                    while (urinicycle[i] != _settings.SerebiiUri)
                    {

                        var editeduri = urinicycle[i].Replace("at://", "");
                        var urlparts = editeduri.Split('/');
                        ITextChannel serebiichannel = (ITextChannel)await _client.GetChannelAsync(872613836723744768);
                        await serebiichannel.SendMessageAsync("https://bsky.app/profile/serebii.bsky.social/post/" + urlparts[2]);
                        i++;
                    }
                    _settings.SerebiiUri = latesturi;
                }
                await Task.Delay(60000); // Check every 60 seconds
            }
        }
        public static async void PostPKHEX()
        {
            while (true)
            {
                var pkhexcommits = await httpClient.GetAsync("https://api.github.com/repos/kwsch/PKHeX/commits");
                if (pkhexcommits.StatusCode != HttpStatusCode.OK)
                {
                    ChangeStatus("Error fetching PKHeX commits: " + pkhexcommits.StatusCode);
                    await Task.Delay(60000); // Wait for 60 seconds before retrying
                    continue;
                }
                var pkhexcommitcontent = await pkhexcommits.Content.ReadAsStringAsync();
                var prettycontent = JsonDocument.Parse(pkhexcommitcontent);
                var latestsha = prettycontent.RootElement.EnumerateArray().FirstOrDefault().GetProperty("sha").GetString();

                if (latestsha != _settings.pkhexsha)
                {
                    int i = 0;
                    var shacycle = prettycontent.RootElement.EnumerateArray().Select(z => z.GetProperty("sha").GetString()).ToList();
                    while (shacycle[i] != _settings.pkhexsha)
                    {
                        var commitsha = shacycle[i];
                        var commiturl = $"https://github.com/kwsch/PKHeX/commit/{commitsha}";
                        ITextChannel pkhexchannel = (ITextChannel)await _client.GetChannelAsync(1549531305941925898);
                        await pkhexchannel.SendMessageAsync(commiturl);
                        i++;
                    }
                    _settings.pkhexsha = latestsha;
                    await Task.Delay(360000);
                }

            }
        }
    }

}
