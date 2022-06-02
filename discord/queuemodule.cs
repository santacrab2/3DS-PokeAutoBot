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
            await DeferAsync();
            var embed = new EmbedBuilder().WithTitle("Queue");
            if (The_Q.Count == 0)
               await FollowupAsync("Queue is Empty");
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
                await FollowupAsync(embed: embed.Build());
            }

        }
        [SlashCommand("leavequeue","removes you from queue")]
        public async Task qc()
        {
            await DeferAsync();
            var qlist = The_Q.ToList();
            var index = qlist.FindIndex(z => z.discordcontext.User.Id == Context.User.Id);
            if(index == -1)
            {
                await FollowupAsync("you are not in the queue");
            }
            qlist.RemoveAt(index);
            var newqu = new Queue<queuesystem>();
            foreach (var item in qlist)
                newqu.Enqueue(item);
            The_Q = newqu;
            await FollowupAsync("removed you from the queue");
        }
    }
}
