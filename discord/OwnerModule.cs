using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using PKHeX.Core;
using static _3DS_link_trade_bot.Form1;

namespace _3DS_link_trade_bot
{
    [DefaultMemberPermissions(GuildPermission.BanMembers)]
    [RequireOwner]
    public class OwnerModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("addtradechannel", "add this channel to the Trade channel List")]
       
        public async Task AddChannel()
        {

            _settings.Discordsettings.BotTradeChannel.Add(Context.Channel.Id);

            await RespondAsync($"I have added the channel **{Context.Channel.Name}** with the id {Context.Channel.Id} for you {Context.User.Username}", ephemeral: true);
        }
        [SlashCommand("addwtchannel", "add this channel to the WT channel List")]

        public async Task AddWTChannel()
        {

            _settings.Discordsettings.BotWTChannel.Add(Context.Channel.Id);

            await RespondAsync($"I have added the channel **{Context.Channel.Name}** with the id {Context.Channel.Id} for you {Context.User.Username}", ephemeral: true);
        }
        [SlashCommand("queueclear","clears the queue entirely, owner command")]
        public async Task clearqueue()
        {
            MainHub.The_Q.Clear();
            await RespondAsync("the queue has been cleared", ephemeral:true);
        }
        [SlashCommand("clearfriendlist","clears your 3ds friend list for however many friends you state starting at whichever index you state")]
        public async Task clearf(int index, int friendstoremove)
        {
            await DeferAsync(ephemeral:true);
            await FollowupAsync("Starting Friend Deletion Routine...", ephemeral: true);
            await MainHub.clearfriendlist(index, friendstoremove);
            await FollowupAsync($"Friend Deletion Routine Complete! I have Deleted {friendstoremove} friends",ephemeral:true);
        }

        [SlashCommand("rules", "posts the rule")]
        [DefaultMemberPermissions(GuildPermission.BanMembers)]
        public async Task rulewarn(int rule)
        {
            await DeferAsync();
            var embed = new EmbedBuilder();
           embed.Title = $"Rule {rule}";
            embed.AddField(ruletitles[rule], rulebody[rule]);
            await FollowupAsync(embed: embed.Build());
        }

        [SlashCommand("kickwarn", "posts the rule and kicks the user, bans on second warn")]
        [DefaultMemberPermissions(GuildPermission.BanMembers)]
        public async Task rulekick(SocketGuildUser user, int rule)
        {
            await DeferAsync();
            var embed = new EmbedBuilder();
            embed.Title = $"Rule {rule}";
            embed.AddField(ruletitles[rule], rulebody[rule]);
            await FollowupAsync($"@{user.Username}",embed: embed.Build());

            try
            {
                await user.SendMessageAsync("You have been kicked for breaking the rule below. You may rejoin with https://www.piplup.net. Please re-read the server rules.", embed: embed.Build());
            }
            catch { }
               
            await user.KickAsync($"Rule {rule}. {ruletitles[rule]}");
            
            
        }

        [SlashCommand("banhammer", "bans the user")]
        [DefaultMemberPermissions(GuildPermission.BanMembers)]
        public async Task TavernBan(SocketGuildUser user,int rule,bool DeleteMessages = false)
        {
            await DeferAsync();
            var embed = new EmbedBuilder();
            embed.Title = $"Rule {rule}";
            embed.AddField(ruletitles[rule], rulebody[rule]);
            if (DeleteMessages)
            {
               foreach(IMessageChannel channel in Context.Guild.Channels)
                {
                    var usermessages = (await AsyncEnumerableExtensions.FlattenAsync(channel.GetMessagesAsync())).Where(z=>z.Author.Id==Context.User.Id);
                    foreach(IMessage message in usermessages)
                    {
                       await message.DeleteAsync();
                    }
                }
            }
            await user.SendMessageAsync("You have been banned. Reason: see below. There is no appeal, have a good one! :)", embed: embed.Build());
            await user.BanAsync(reason: $"Rule {rule}. {ruletitles[rule]}");
            await Context.Channel.SendMessageAsync($"{Context.User.Username} has been banned for Rule {rule}. {ruletitles[rule]}");

        }
        public string[] ruletitles = new string[]
        {
            "No Bots eTa WeN",
            "No Spam",
            "Keep your conduct and conversation civil",
            "NSFW content of any kind is strictly prohibited.",
            "No Charging Real Money for Pokemon",
            "No Slandering the Server",
            "Evading the rules, breaking TOS, seeking loopholes, or attempting to stay \"borderline\" within the rules is considered the same as breaking the rules.",
            "Do not send unsolicited DMs or Friend Requests to other users.",
            "It is prohibited to discuss, request, or promote methods to defy intended game mechanics.",
            "No Advertising",
            "No Genning for others",
            "Do not bring any outside drama here.",
            "Read the guides/help",
            "Staying in this server means you agree to my tyranny.",
            "No Excuses",
        };

        public string[] rulebody = new string[]
        {
            "The bots are up when they are up. It is a free service. Therefore there is no schedule and I run them when I want to. Asking when in a chat will get you kicked/banned. You can head to ⁠<#1081994925635289131> to see how to request a bot to be turned on sooner.",
            "Do not Spam the same message across multiple channels. Do not post in an incorrect channel just because one seems more active.",
            "This is a general rule that covers \"Be nice\" and \"Stay on-topic\".\r\n- Avoid inflammatory topics and heated arguments. Harassment and hateful speech are obviously not allowed.\r\n- Swearing is permitted as long as it is not meant to insult and belittle another user.\r\n- If someone has specific requests about how they are treated, respect their wishes.",
            "This includes gore, other \"shock\" content, or off-topic lewd conversations. In general, if it is inappropriate to talk about it at work, it's not allowed here.",
            "No charging real money for pokemon. We will never charge, neither should you! If you are found to be selling pokemon from this server's services, you will be put on the naughty list.",
            "No slandering the server or the services. If we feel you have ill intent towards the server or the people that are here you will be put on the naughty list.",
            "We understand that it is not possible for the rules to cover every case of misbehavior. In such cases, moderators have discretion to decide whether a user is in violation or intentionally toeing the line.",
            "Make sure the other user is all right with it before you send a private message or friend request. Use the public help channels to request help. To report an issue to moderators in private, send a private message to them.",
            "This includes asking for help with shiny-locked Pokémon or other impossible/illegal Pokémon to trade to others. You will be warned once, and ignoring the warning will result in being put on the naughty list",
            "Statuses may passively link to a stream or server as long as they are not paid services. Any other ads are prohibited.",
            "This means you can NOT be the middle man for genning, meaning you trade bot, then trade other person. You can help people who are struggling with it, and send them pkhex files in public chat.\r\n​\r\nThis is to avoid people bypassing bans. If you are found to be helping someone bypass a ban, you will also be put on the naughty list.",
            "This includes any arguments between users on other sites or other servers. If you pursue a user here to continue harassment after they have made it clear that it is unwanted, you will be banned.",
            "The vast majority of questions are answered in the how-to-guide channel, reading the pins in the help/bot channels, or reading the description on the slash commands.\r\n​\r\nYou are expected to have made an attempt to solve the problem yourself before asking for human help.\r\n​\r\nEnsure that you are in the correct channel before asking your question, and provide as much information as possible. Do not tag random users to answer your question or hop from channel to channel. Users who are uncooperative may be kicked or banned from the server.",
            "You have no right or obligation to stay here.",
            "There are no excuses for not having read these rules. It is a requirement to participate in this server. If you try to use an excuse such as a disability as the reason you did not read the rules, you will be banned immediately. There are accessiblity tools all over the internet and many ways for a person to follow these rules while having a disability, if you want to participate here you have to put in an effort."

        };
       
    }
}
