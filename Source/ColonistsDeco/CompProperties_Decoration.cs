using Verse;

namespace ColonistsDeco;

public class CompProperties_Decoration : CompProperties
{
    public string decorationName;
    public string decoratorCreator;

    public CompProperties_Decoration()
    {
        compClass = typeof(CompDecoration);
    }
}