using HarmonyLib;
using UnityEngine;
using Verse;

namespace RominaReady
{
    public class RominaReadySettings : ModSettings
    {
        public bool allowSmallThreats;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref allowSmallThreats, "allowSmallThreats", false);
        }
    }

    public class RominaReadyMod : Mod
    {
        public static RominaReadySettings settings;

        public RominaReadyMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<RominaReadySettings>();
            new Harmony("ferny.RominaReady").PatchAll();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("RR_AllowSmallThreats".Translate(), ref settings.allowSmallThreats);
            listing.End();
        }

        public override string SettingsCategory() => Content.Name;
    }
}
