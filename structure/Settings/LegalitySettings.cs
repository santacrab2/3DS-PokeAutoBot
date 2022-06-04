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
    public class LegalitySettings
    {
        protected const string Legality = nameof(Legality);
        public override string ToString() => "Legality Settings";
        [Category(Legality), Description("Bots OT name, must be less than 8 characters for past generations to work")]
        public string BotOT { get; set; } = "pip";
        [Category(Legality), Description("Bots 5 digit TID, Not the 6 digit Trainer ID 7")]
        public int BotTID { get; set; } = 42069;
        [Category(Legality), Description("Bots 5 digit SID, Not the 4 digit SID7")]
        public int BotSID { get; set; } = 42069;
        [Category(Legality), Description("Bots Game Language")]
        public PKHeX.Core.LanguageID BotLanguage = PKHeX.Core.LanguageID.English;
        [Category(Legality), Description("add all Legal Ribbons to showdown set trades")]
        public bool AddAllLegalRibbons { get; set; } = false;
        [Category(Legality), Description("Set Match by color Pokeballs if not Specified in set")]
        public bool SetMatchingPokeball { get; set; } = true;
        [Category(Legality), Description("Apply Pokeballs specified in showdown sets")]
        public bool SetUserSpecifiedPokeball { get; set; } = true;
        [Category(Legality), Description("Send Meme pk files when users submit an illegal set")]
        public bool SendMemePks { get; set; } = false;
        [Category(Legality), Description("Allow OT: TID: SID: in showdownsets")]
        public bool AllowTrainerInfo { get; set; } = true;
        [Category(Legality), Description("Allow Batch Editor commands in showdown sets")]
        public bool UseBatchEditor { get; set; } = true;

    }
}
