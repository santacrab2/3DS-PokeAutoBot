using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Interactions;
using Discord;
using static _3DS_link_trade_bot.MainHub;

namespace _3DS_link_trade_bot
{
    [EnabledInDm(true)]
    [DefaultMemberPermissions(GuildPermission.ViewChannel)]
    public class queuemodule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("queuelist","Displays the queue")]
        public async Task queuelist()
        {
            var embed = new EmbedBuilder().WithTitle("Queue");
            if (The_Q.Count == 0)
               await RespondAsync("Queue is Empty");
            else
            {
                StringBuilder sb = new StringBuilder();
                int i = 1;
                foreach(var item in The_Q)
                {
                    sb.AppendLine($"{i}.{item.discordcontext.User.Username}");
                    i++;
                }
                embed.AddField("Users in Queue",sb.ToString());
                await RespondAsync(embed: embed.Build());
            }

        }
    }
}
