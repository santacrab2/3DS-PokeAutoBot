using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using static _3DS_link_trade_bot.dsbotbase;
using static _3DS_link_trade_bot.dsbotbase.Buttons;


namespace _3DS_link_trade_bot
{
    public class TradeModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("addfc","adds you to the bots friend list, dont forget to add the bot!")]
        public async Task addfc(string friendcode)
        {
            presshome();
            await Task.Delay(2000);
            touch(120, 10);
            await Task.Delay(1000);
            touch(120, 10);
            await Task.Delay(5000);
            touch(160, 10);
            await Task.Delay(3000);
            touch(160, 180);
            await Task.Delay(5000);
            await enterfriendcode(friendcode);
            await Task.Delay(2000);
            touch(240, 230);
            await Task.Delay(10000);
            touch(180, 100);
            await Task.Delay(1000);
            touch(240, 230);
            await Task.Delay(3000);
            touch(240, 200);
            await Task.Delay(5000);
            click(X);
            await Task.Delay(5000);
            presshome();


        }

    }
}
