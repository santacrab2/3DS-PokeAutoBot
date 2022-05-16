using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core.AutoMod;
using PKHeX.Core;
using Discord;
using Discord.Interactions;

namespace _3DS_link_trade_bot
{
    public class ALMModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("convert", "Makes you a pokemon file from showdown text")]
        public async Task convert(string PokemonText)
        {
            ShowdownSet set = TradeModule.ConvertToShowdown(PokemonText);
            var sav = SaveUtil.GetBlankSAV(GameVersion.UM, "Piplup");
            var pkm = sav.GetLegalFromSet(set, out var res);


            if (!new LegalityAnalysis(pkm).Valid || res.ToString() != "Regenerated")
            {
                var reason = $"I wasn't able to create a {(Species)set.Species} from that set.";
                var imsg = $"Oops! {reason}";

                imsg += $"\n{set.SetAnalysis(sav, pkm)}";
                await RespondAsync(imsg, ephemeral: true).ConfigureAwait(false);
                return;
            }
            var tempfile = $"{Directory.GetCurrentDirectory()}//{pkm.FileName}";
            File.WriteAllBytes(tempfile, pkm.DecryptedBoxData);
            await RespondWithFileAsync(tempfile, text:"Here is your legalized pk file");
            File.Delete(tempfile);
        }
    }
}
