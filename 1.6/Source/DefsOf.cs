using RimWorld;

namespace RominaReady
{
    [DefOf]
    public static class DefsOf
    {
        public static StorytellerDef RR_RominaReady;
        public static IncidentCategoryDef DiseaseAnimal;

        static DefsOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DefsOf));
        }
    }
}
