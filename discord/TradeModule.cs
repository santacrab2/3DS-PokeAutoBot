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

        [SlashCommand("trade", "trades you a pokemon over link trade in 3ds games")]
        public async Task trade(string TrainerName, string PokemonText = "", Attachment PK7orPK6 = null)
        {
            queuesystem tobequeued;
            tobequeued = new queuesystem() { discordcontext = Context,friendcode = "",IGN=TrainerName,mode = botmode.trade, tradepokemon = EntityBlank.GetBlank(7) };
            The_Q.Enqueue(tobequeued);
        }

    }
}
