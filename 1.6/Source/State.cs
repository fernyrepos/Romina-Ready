using Verse;

namespace RominaReady
{
    public static class State
    {
        public static bool isReady = false;
        public static bool hasHadFirstThreat = false;
        private static Window_ReadyButton window;
        public static void Clear()
        {
            isReady = false;
            hasHadFirstThreat = false;
            window = null;
        }

        public static void SetThreatFired()
        {
            if (!isReady)
                return;
            isReady = false;
            hasHadFirstThreat = true;
            EnsureButtonState();
        }

        public static void OnButtonClicked()
        {
            isReady = true;
            window = null;
        }

        public static void ClearWindowReference(Window_ReadyButton expected)
        {
            if (window == expected)
                window = null;
        }

        public static void EnsureButtonState()
        {
            if (Find.Storyteller?.def != DefsOf.RR_RominaReady)
            {
                isReady = true;
                return;
            }
            if (isReady || window != null)
                return;
            window = new Window_ReadyButton();
            Find.WindowStack.Add(window);
        }

        public static void ExposeData()
        {
            Scribe_Values.Look(ref isReady, "RR_isReady", false);
            Scribe_Values.Look(ref hasHadFirstThreat, "RR_hasHadFirstThreat", false);
        }
    }
}
