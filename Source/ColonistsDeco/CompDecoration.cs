using Verse;

namespace ColonistsDeco;

public class CompDecoration : ThingComp
{
    public string decoratorCreator;
    public CompProperties_Decoration Props => (CompProperties_Decoration)props;

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref decoratorCreator, "decoratorCreator", "Unknown".Translate());
    }
}