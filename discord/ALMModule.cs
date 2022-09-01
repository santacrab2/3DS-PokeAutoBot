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
            RegenTemplate rset = new(set);
            var trainer = TrainerSettings.GetSavedTrainerData(GameVersion.USUM,7);
            if (NTR.game <3)
                trainer = TrainerSettings.GetSavedTrainerData(GameVersion.ORAS,6);
            var sav = SaveUtil.GetBlankSAV((GameVersion)trainer.Game, trainer.OT);
          
            var pkm = sav.GetLegalFromSet(rset, out var res);
           
           
            
            if (pkm is PK7 pk7)
            {
                pk7.SetDefaultRegionOrigins();
                pkm = pk7;
            }
               
            
         
            var correctfile = (NTR.game > 2 && pkm is PK7) ? true : pkm is PK6 ? true : false;

            if (!new LegalityAnalysis(pkm).Valid || !correctfile)
            {
                var reason = $"I wasn't able to create a {(Species)set.Species} from that set.";
                var imsg = $"Oops! {reason}";
                var tempfile2 = $"{Directory.GetCurrentDirectory()}//{pkm.FileName}";
                File.WriteAllBytes(tempfile2, pkm.DecryptedBoxData);
                imsg += $"\n{set.SetAnalysis(sav, pkm)} Attempted Save: {sav.Version}";
            
                await FollowupWithFileAsync(tempfile2,text:imsg, ephemeral: true).ConfigureAwait(false);
                File.Delete(tempfile2);
                return;
            }
            var tempfile = $"{Directory.GetCurrentDirectory()}//{pkm.FileName}";
            File.WriteAllBytes(tempfile, pkm.DecryptedBoxData);
            await FollowupWithFileAsync(tempfile, text:"Here is your legalized pk file");
            File.Delete(tempfile);
        }
    }
}
