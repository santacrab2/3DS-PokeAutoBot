using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using PKHeX.Core;
using PKHeX.Core.AutoMod;
using Discord.Interactions;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using static _3DS_link_trade_bot.MainHub;
using static _3DS_link_trade_bot.Form1;

namespace _3DS_link_trade_bot
{
    [EnabledInDm(false)]
    [DefaultMemberPermissions(GuildPermission.ViewChannel)]
    public class FriendCodeModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("addfc", "adds you to the bots friend list, dont forget to add the bot!")]
        public async Task addfc(string friendcode)
        {
            await DeferAsync();
            if (The_Q.Count != 0)
            {
                if (The_Q.Any(z => z.discordcontext.User == Context.User))
                {
                    await FollowupAsync("you are already in queue", ephemeral: true);
                    return;
                }
            }
            try { await Context.User.SendMessageAsync($"I Have added you to the Friend Code queue. I will message you here when I am adding you. My FC is {_settings.FriendCode} Please add me."); } catch { await FollowupAsync("enable private messages from users on the server to be queued"); return; }
            friendcode = friendcode.Replace("-", "").Replace(" ", "");
            var tobequeued = new queuesystem() { discordcontext = Context, friendcode = friendcode, tradepokemon = EntityBlank.GetBlank(7), IGN = "", mode = botmode.addfc };
            The_Q.Enqueue(tobequeued);
            await FollowupAsync($"Added {Context.User.Username} to the Friend Code queue.");


        }
    }
}
