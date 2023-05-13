using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ColonistsDeco;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    private static readonly Type patchType = typeof(HarmonyPatches);

    static HarmonyPatches()
    {
        var harmony = new Harmony("rimworld.iemanddieabeheet.colonistsdeco");

        harmony.Patch(AccessTools.Method(typeof(BeautyUtility), nameof(BeautyUtility.CellBeauty)),
            postfix: new HarmonyMethod(patchType, nameof(CellBeautyPostfix)));
    }

    public static void CellBeautyPostfix(ref float __result, IntVec3 c, Map map)
    {
        var cells = GenAdjFast.AdjacentCellsCardinal(c);
        foreach (var cell in cells)
        {
            if (!cell.InBounds(map))
            {
                continue;
            }

            foreach (var thing in cell.GetThingList(map))
            {
                if (Utility.wallDecoDefs.Contains(thing.def) && thing.Position + thing.Rotation.FacingCell == c)
                {
                    __result += thing.GetStatValue(StatDefOf.Beauty);
                }
            }
        }
    }
}