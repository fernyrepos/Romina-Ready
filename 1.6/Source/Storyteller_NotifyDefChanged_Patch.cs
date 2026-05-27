using HarmonyLib;
using RimWorld;
using Verse;

namespace RominaReady
{
    [HarmonyPatch(typeof(Storyteller), nameof(Storyteller.Notify_DefChanged))]
    public static class Storyteller_NotifyDefChanged_Patch
    {
        public static void Postfix()
        {
            if (Find.Storyteller?.def == DefsOf.RR_RominaReady)
                State.isReady = false;
            State.EnsureButtonState();
        }
    }
}