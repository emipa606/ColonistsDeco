using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

internal class ColonistsDecoModSettings : ModSettings
{
    public int ceilingDecorationLimit = 2;

    public int defaultDecoCooldown = 60000;
    public int wallDecorationLimit = 2;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref wallDecorationLimit, "wallDecorationLimit", 3);
        Scribe_Values.Look(ref ceilingDecorationLimit, "ceilingDecorationLimit", 2);
        Scribe_Values.Look(ref defaultDecoCooldown, "defaultDecoCooldown", 60000);
    }

    public void DoWindowContents(Rect canvas)
    {
        var listingStandard = new Listing_Standard
        {
            ColumnWidth = canvas.width
        };
        listingStandard.Begin(canvas);
        listingStandard.Slider(ref wallDecorationLimit, 1, 25, () => "Deco.walllimit".Translate(wallDecorationLimit),
            1f);
        listingStandard.Gap(32f);
        listingStandard.Slider(ref ceilingDecorationLimit, 1, 25,
            () => "Deco.ceilinglimit".Translate(ceilingDecorationLimit), 1f);
        listingStandard.Gap(32f);
        listingStandard.Slider(ref defaultDecoCooldown, 0, 240000,
            () => "Deco.cooldown".Translate(defaultDecoCooldown.ToStringTicksToPeriod()), 100f);
        if (Current.ProgramState == ProgramState.Playing)
        {
            listingStandard.Gap(32f);
            if (listingStandard.ButtonText("Deco.remove".Translate()))
            {
                Find.WindowStack.Add(
                    new Dialog_Confirm("Deco.removeconfirm".Translate(), removeDecos));
            }
        }

        listingStandard.Gap(32f);
        if (listingStandard.ButtonText("Deco.reset".Translate()))
        {
            Find.WindowStack.Add(new Dialog_Confirm("Deco.resetconfirm".Translate(), resetSettings));
        }

        if (ColonistsDecoMain.CurrentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("Deco.currentModVersion".Translate(ColonistsDecoMain.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }

    private void resetSettings()
    {
        wallDecorationLimit = 2;
        defaultDecoCooldown = 60000;
    }

    private static void removeDecos()
    {
        var allThings = new List<Thing>();

        foreach (var c in Current.Game.CurrentMap.AllCells)
        {
            foreach (var t in Current.Game.CurrentMap.thingGrid.ThingsListAtFast(c))
            {
                allThings.Add(t);
            }
        }

        foreach (var t in allThings)
        {
            if (Utility.IsWallDeco(t) || Utility.IsCeilingDeco(t) || Utility.IsBedsideDeco(t))
            {
                t.Destroy();
            }
        }
    }
}