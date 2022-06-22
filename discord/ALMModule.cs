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
    [EnabledInDm(false)]
    [DefaultMemberPermissions(GuildPermission.ViewChannel)]
    public class ALMModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("convert", "Makes you a pokemon file from showdown text")]
        public async Task convert(string PokemonText)
        {
            await DeferAsync();
            ShowdownSet set = TradeModule.ConvertToShowdown(PokemonText);
            var trainer = TrainerSettings.GetSavedTrainerData(7);
            if (NTR.game == 2 || NTR.game == 1)
                trainer = TrainerSettings.GetSavedTrainerData(6);
            var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
            var pkm = sav.GetLegalFromSet(set, out var res);
            int attempts = 0;
            while (!new LegalityAnalysis(pkm).Valid && attempts < 3)
            {
                trainer = TrainerSettings.GetSavedTrainerData(7);
                sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
                pkm = sav.GetLegalFromSet( set, out res);
                attempts++;
            }

            if (!new LegalityAnalysis(pkm).Valid || res.ToString() != "Regenerated")
            {
                var reason = $"I wasn't able to create a {(Species)set.Species} from that set.";
                var imsg = $"Oops! {reason}";

                imsg += $"\n{set.SetAnalysis(sav, pkm)}";
                await FollowupAsync(imsg, ephemeral: true).ConfigureAwait(false);
                return;
            }
            var tempfile = $"{Directory.GetCurrentDirectory()}//{pkm.FileName}";
            File.WriteAllBytes(tempfile, pkm.DecryptedBoxData);
            await FollowupWithFileAsync(tempfile, text:"Here is your legalized pk file");
            File.Delete(tempfile);
        }
    }
}
