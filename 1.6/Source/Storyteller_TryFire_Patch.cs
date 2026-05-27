using HarmonyLib;
using RimWorld;
using Verse;

namespace RominaReady
{
    [HarmonyPatch(typeof(Storyteller), nameof(Storyteller.TryFire))]
    public static class Storyteller_TryFire_Patch
    {
        public static bool Prefix(FiringIncident fi, ref bool __result)
        {
            if (Find.Storyteller?.def != DefsOf.RR_RominaReady)
                return true;
            if (State.isReady)
                return true;
            if (fi.def.category == IncidentCategoryDefOf.ThreatBig)
            {
                __result = false;
                return false;
            }
            if (!RominaReadyMod.settings.allowSmallThreats && fi.def.category == IncidentCategoryDefOf.ThreatSmall)
            {
                __result = false;
                return false;
            }
            return true;
        }

        public static void Postfix(FiringIncident fi, bool __result)
        {
            if (!__result)
                return;
            if (Find.Storyteller?.def != DefsOf.RR_RominaReady)
                return;
            if (fi.def.category == IncidentCategoryDefOf.ThreatBig)
            {
                State.SetThreatFired();
                return;
            }
            if (!RominaReadyMod.settings.allowSmallThreats && fi.def.category == IncidentCategoryDefOf.ThreatSmall)
                State.SetThreatFired();
        }
    }
}
