using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using PKHeX.Core;
using Discord.Interactions;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;
using static _3DS_link_trade_bot.LinkTradeBot;


namespace _3DS_link_trade_bot
{
    public class TradeModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("addfc","adds you to the bots friend list, dont forget to add the bot!")]
        public async Task addfc(string friendcode)
        {
            var tobequeued = new queuesystem() { discordcontext = Context,friendcode = friendcode,tradepokemon=EntityBlank.GetBlank(7),IGN ="",mode = botmode.addfc};
            The_Q.Enqueue(tobequeued);


        }

    }
}
