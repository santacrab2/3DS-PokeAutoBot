using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration;

namespace _3DS_link_trade_bot
{
    
    public class Settings
    {
        protected const string Discord = nameof(Discord);
        protected const string BotsMainSettings = nameof(BotsMainSettings);
        protected const string Legality = nameof(Legality);
        protected const string RNG = nameof(RNG);
        [Category(BotsMainSettings), Description("GTS Distribution")]
        public bool GTSdistribution { get; set; } = false;
        [Category(BotsMainSettings), Description("GTS Page To Start on")]
        public int PokemonWanted { get; set; } = 1;

        [Category(BotsMainSettings), Description("WonderTrade Distribution")]
        public bool WonderTrade { get; set; } = false;
        [Category(BotsMainSettings), Description("Bot's Friend Code, include Dashes")]
        public string FriendCode { get; set; } = "0000-0000-0000";
        [Category(Discord)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Discordsettings Discordsettings { get; set; } = new();
        [Category(Legality)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LegalitySettings Legalitysettings { get; set; } = new();
        [Category(RNG)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RNGSettings RNGsettings { get; set; } = new();

    }
    public enum Mode
    {
        FlexTrade,
        FriendCodeOnly,
        GTSOnly,
        WTOnly,
        GTSWTOnly,

        EggRNGNonePID,
        EggRNGPID,
    }
}
