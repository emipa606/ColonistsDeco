using Mlie;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

internal class ColonistsDecoMain : Mod
{
    public static ColonistsDecoModSettings Settings;
    public static string currentVersion;

    public ColonistsDecoMain(ModContentPack content)
        : base(content)
    {
        Settings = GetSettings<ColonistsDecoModSettings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(ModLister.GetActiveModWithIdentifier("Mlie.ColonistsDeco"));
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Colonists' Deco";
    }
}