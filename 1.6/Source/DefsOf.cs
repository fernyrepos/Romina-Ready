using RimWorld;

namespace RominaReady
{
    [DefOf]
    public static class DefsOf
    {
        public static StorytellerDef RR_RominaReady;

        static DefsOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DefsOf));
        }
    }
}
