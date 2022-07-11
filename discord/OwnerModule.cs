using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
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
    }
}
