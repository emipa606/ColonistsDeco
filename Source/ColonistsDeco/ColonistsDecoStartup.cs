using Verse;

namespace ColonistsDeco;

[StaticConstructorOnStartup]
internal static class ColonistsDecoStartup
{
    static ColonistsDecoStartup()
    {
        Utility.LoadDefs();
    }
}