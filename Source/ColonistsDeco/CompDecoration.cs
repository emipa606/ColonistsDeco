using Verse;

namespace ColonistsDeco;

public class CompDecoration : ThingComp
{
    public string decorationCreator;

    public string decorationName;
    public CompProperties_Decoration Props => (CompProperties_Decoration)props;

    public override void Initialize(CompProperties props)
    {
        base.Initialize(props);

        decorationName = Props.decorationName;
        decorationCreator = "Unknown".Translate();
    }
}