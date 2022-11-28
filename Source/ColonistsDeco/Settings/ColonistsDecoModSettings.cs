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
        var listing_Standard = new Listing_Standard
        {
            ColumnWidth = canvas.width
        };
        listing_Standard.Begin(canvas);
        listing_Standard.Slider(ref wallDecorationLimit, 1, 25, () => "Deco.walllimit".Translate(wallDecorationLimit),
            1f);
        listing_Standard.Gap(32f);
        listing_Standard.Slider(ref ceilingDecorationLimit, 1, 25,
            () => "Deco.ceilinglimit".Translate(ceilingDecorationLimit), 1f);
        listing_Standard.Gap(32f);
        listing_Standard.Slider(ref defaultDecoCooldown, 0, 240000,
            () => "Deco.cooldown".Translate(defaultDecoCooldown.ToStringTicksToPeriod()), 100f);
        if (Current.ProgramState == ProgramState.Playing)
        {
            listing_Standard.Gap(32f);
            if (listing_Standard.ButtonText("Deco.remove".Translate()))
            {
                Find.WindowStack.Add(
                    new Dialog_Confirm("Deco.removeconfirm".Translate(), RemoveDecos));
            }
        }

        listing_Standard.Gap(32f);
        if (listing_Standard.ButtonText("Deco.reset".Translate()))
        {
            Find.WindowStack.Add(new Dialog_Confirm("Deco.resetconfirm".Translate(), ResetSettings));
        }

        if (ColonistsDecoMain.currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("Deco.currentModVersion".Translate(ColonistsDecoMain.currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }

    private void ResetSettings()
    {
        wallDecorationLimit = 2;
        defaultDecoCooldown = 60000;
    }

    private void RemoveDecos()
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