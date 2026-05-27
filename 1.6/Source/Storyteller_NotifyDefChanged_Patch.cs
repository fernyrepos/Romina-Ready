using HarmonyLib;
using RimWorld;

namespace RominaReady
{
    [HarmonyPatch(typeof(Storyteller), nameof(Storyteller.Notify_DefChanged))]
    public static class Storyteller_NotifyDefChanged_Patch
    {
        public static void Postfix()
        {
            State.EnsureButtonState();
        }
    }
}