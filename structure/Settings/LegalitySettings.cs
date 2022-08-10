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
        [Category(Legality), Description("known GTS hackers, bot will update the list if it finds any new ones automatically")]
        public List<string> ZKnownGTSBreakers { get; set; } = new List<string> { "funkygamer26", "chloegarcia", "volcano.“do”", "33012888", "moon.", "unknown.yt", "japan.kebuju", "はちゆきおし", "あああ", "あか", "adventrsnivy", "noxii", "", " ", "doudou#6666", "doudou#9999", "zeraoratv=yt", "sun.", "leonmaxi.tv", "rayky", "kewl", "not toxi", "serkan", "arceus", "flecheringyt", "quit the gts", "0", "meza", "ｇｔｒ　すき", "div yt", " stefan ", "or", "ゅうた", "ゲンそんちょ", "iamnotsnivy", "シュウ", "ｇａｇａｇａ", "lycanroc.tv", "jpan.kebj", "サン", "shinygtr", "t0xic", "fallen", "kebujugamer", "hackplayer", "masterabra", "shinygtr.com", "5aul", "darkraitytbe", "volcan", "pikamain", "jpn.kebju", "adventrpika", "sander", "lenny", "チヒロ", "ledge", "かいと", "リオン", "eveana", "lucas", "ｇｔｒ　すき", "div yt", " stefan ", "or", "ゅうた", "edou780", "ili@n", "forg1vnyt", "forgivn", "♥daisy♥", "???", "@ 『stefan』 @", "CViD 19", "ChrisHaxYT", "Zanky#4367", "StayMadKek", "darkstar yt", "　　　", "nextforg1vn", "#nico strat", "                            ", "    ■    ■    ■    ■    ■    ■", "cuckflict", "    ", "Pl4t#4069", "", "sabo", "yt=z3r40r4", "advtrshαdσω", "        shαdσω", "bldontm", "shadow", "jony", "⑥❻⑥❻⑥❻⑥❻⑥❻⑥", "ｖ_", "dr.jhnsn", "iv_t#００⑧", "①❶②❷③❸④❹⑤❺⑥", "" };
    }
}
