using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace _3DS_link_trade_bot
{
    public class Settings
    {
        protected const string Discord = nameof(Discord);
        protected const string MainSettings = nameof(MainSettings);
        [Category(MainSettings), Description("GTS Distribution")]
        public bool GTSdistribution { get; set; } = false;
        [Category(MainSettings), Description("GTS Page To Start on")]
        public int PokemonWanted { get; set; } = 1;
        [Category(MainSettings), Description("Bot's Friend Code, include Dashes")]
        public string FriendCode { get; set; } = "0000-0000-0000";
        [Category(Discord)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Discordsettings Discordsettings { get; set; } = new();
    }
}
