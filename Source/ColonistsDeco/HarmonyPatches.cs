using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ColonistsDeco;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    static HarmonyPatches()
    {
        new Harmony("rimworld.iemanddieabeheet.colonistsdeco").PatchAll(Assembly.GetExecutingAssembly());
    }
}

[HarmonyPatch(typeof(BeautyUtility), nameof(BeautyUtility.CellBeauty))]
public static class BeautyUtility_CellBeauty
{
    public static void Postfix(ref float __result, IntVec3 c, Map map)
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