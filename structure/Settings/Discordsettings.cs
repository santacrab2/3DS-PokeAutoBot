using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Discord;
using System.Configuration;

namespace _3DS_link_trade_bot
{
   
    public class Discordsettings 
    {
        protected const string Discord = nameof(Discord);
        public override string ToString() => "Discord Settings";
        
        [Category(Discord), Description("Discord Token")]
        public string token { get; set; } = "token";
        [Category(Discord), Description("The Channel(s) the bot will operate in.")]
        public List<ulong> BotTradeChannel { get; set; } = new();
        [Category(Discord), Description("The Channel(s) the bot will post the Wonder Trade Embed and Countdown too")]
        public List<ulong> BotWTChannel { get; set; } = new();


        [Category(Discord), Description("Toggle Who's That Pokemon Mode on/off")]
        public bool WhosThatPokemon { get; set; } = true;
        [Category(Discord), Description("The Channel(s) the bot will post the Wonder Trade Embed and Countdown too")]
        public ulong BotWTPChannel { get; set; } = 0;
    }
}
