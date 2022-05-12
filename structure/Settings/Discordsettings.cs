using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Discord;

namespace _3DS_link_trade_bot
{
    public class Discordsettings 
    {
        protected const string Discord = nameof(Discord);
        public override string ToString() => "Discord Settings";
   
        [Category(Discord), Description("Discord Token")]
        public string token { get; set; } = "token";
        [Category(Discord), Description("The Channel(s) the bot will operate in.")]
        public ulong[] BotChannel { get; set; }
    }
}
