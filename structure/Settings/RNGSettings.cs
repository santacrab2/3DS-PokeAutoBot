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
    public class RNGSettings
    {
        protected const string RNG = nameof(RNG);
        public override string ToString() => "RNG Settings";
        [Category(RNG), Description("The Amount of Frames to Skip")]
        public int frames { get; set; } = 0;
    }
}
