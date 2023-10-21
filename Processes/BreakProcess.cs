using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethMod.Processes
{
    public class BreakProcess : CustomProcess
    {
        // The unique internal name of this process
        public override string UniqueNameID => "BreakProcess";

        // The "default" appliance of this process (e.g., counter for "chop", hob for "cook")
        // When a dish requires this process, this is the appliance that will spawn at the beginning of a run
        public override GameDataObject BasicEnablingAppliance => Mod.Counter/* reference to your default appliance */;

        // Whether or not the process can be obfuscated, such as through the "Blindfolded Chefs" card. This is normally set to `true`
        public override bool CanObfuscateProgress => true;

        // The localization information for this process. This must be set for at least one language. 
        public override List<(Locale, ProcessInfo)> InfoList => new List<(Locale, ProcessInfo)>()
    {
        // Note: for testing, I recommend setting "my_custom_icon" to "knead" for now until we add the custom icon (step 5)
        (Locale.English, LocalisationUtils.CreateProcessInfo("Break", "<sprite name=\"break_0\">"))
    };

    }
}
