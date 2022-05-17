using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using PKHeX.Core;

namespace _3DS_link_trade_bot
{
    public class queuesystem
    {
        public SocketInteractionContext discordcontext { get; set; }
        
        public PKM tradepokemon { get; set; }
        public string IGN { get; set; }
       public string friendcode { get; set; }
        public botmode mode { get; set; }
        
    }
    public enum botmode
    {
        addfc,
        trade,
        dump
    }
}
