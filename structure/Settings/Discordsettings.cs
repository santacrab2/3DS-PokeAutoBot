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

        [Category(Discord), Description("The ID of the Role to Ping when the Bot turns on")]
        public ulong PingRoleID { get; set; }
        [Category(Discord), Description("the message to send when announcing it is turning on")]
        public string PingMessage { get; set; } = "Gen 7 Link Trade Bot is Online";

        [Category(Discord), Description("Toggle Who's That Pokemon Mode on/off")]
        public bool WhosThatPokemon { get; set; } = true;
        [Category(Discord), Description("The Channel(s) the bot will post the Wonder Trade Embed and Countdown too")]
        public ulong BotWTPChannel { get; set; } = 0;
        [Category(Discord), Description("send an announcement message to discord when the bot turns on/off or crashes")]
        public bool SendStatusMessage { get; set; } = false;
    }
}
