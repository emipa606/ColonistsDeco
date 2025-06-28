using Mlie;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

internal class ColonistsDecoMain : Mod
{
    public static ColonistsDecoModSettings Settings;
    public static string CurrentVersion;

    public ColonistsDecoMain(ModContentPack content)
        : base(content)
    {
        Settings = GetSettings<ColonistsDecoModSettings>();
        CurrentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
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