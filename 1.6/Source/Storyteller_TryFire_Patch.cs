using HarmonyLib;
using RimWorld;
using Verse;

namespace RominaReady
{
    [HarmonyPatch(typeof(Storyteller), nameof(Storyteller.TryFire))]
    public static class Storyteller_TryFire_Patch
    {
        private static bool ShouldSetThreatFired(FiringIncident fi)
        {
            return fi.def.category == IncidentCategoryDefOf.ThreatBig
                || fi.def.category == IncidentCategoryDefOf.DiseaseHuman
                || fi.def.category == DefsOf.DiseaseAnimal
                || fi.def.category == IncidentCategoryDefOf.DeepDrillInfestation
                || (!RominaReadyMod.settings.allowSmallThreats && fi.def.category == IncidentCategoryDefOf.ThreatSmall);
        }

        public static bool Prefix(FiringIncident fi, ref bool __result, out bool __state)
        {
            __state = false;
            if (Find.Storyteller?.def != DefsOf.RR_RominaReady)
                return true;
            if (State.isReady)
                return true;
            if (ShouldSetThreatFired(fi))
            {
                __result = false;
                __state = true;
                return false;
            }
            return true;
        }

        public static void Postfix(FiringIncident fi, bool __result, bool __state)
        {
            if (__state || !__result)
                return;
            if (Find.Storyteller?.def != DefsOf.RR_RominaReady)
                return;
            if (ShouldSetThreatFired(fi))
            {
                State.SetThreatFired();
            }
        }
    }
}
