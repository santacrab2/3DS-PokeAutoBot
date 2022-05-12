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
        [Category(MainSettings), Description("Distribution")]
        public bool distribution { get; set; } = false;
        [Category(MainSettings), Description("await time ")]
        public int awaittime { get; set; } = 1;
        [Category(Discord)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Discordsettings Discordsettings { get; set; } = new();
    }
}
