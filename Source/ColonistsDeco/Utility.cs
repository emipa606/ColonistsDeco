using System.Collections.Generic;
using System.Text.RegularExpressions;
using RimWorld;
using Verse;

namespace ColonistsDeco;

internal class Utility
{
    private static readonly Dictionary<ThingDef, List<TechLevel>> thingTechProgression = new();

    private static readonly Dictionary<ThingDef, (List<TechLevel>, DecoLocationType)> decoDictionary = new();

    private static readonly List<ThingDef> ceilingDecoDefs = [];

    public static readonly List<ThingDef> wallDecoDefs = [];

    public static readonly List<ThingDef> bedsideDecoDefs = [];

    private static readonly List<int> wallHashes = [];

    private static readonly List<int> ceilingDecoHashes = [];

    private static readonly List<int> wallDecoHashes = [];

    private static readonly List<int> bedsideDecoHashes = [];

    private static readonly List<ResearchProjectDef> researchProjectDefs = [];

    public static readonly List<ThingDef> bedsideTables = [];

    public static void LoadDefs()
    {
        var attachableThingComp = new CompProperties_AttachableThing();
        var pawnDecoThingComp = new CompProperties_PawnDeco();


        foreach (var currentDef in DefDatabase<ThingDef>.AllDefs)
        {
            if (currentDef.HasModExtension<DecoModExtension>())
            {
                thingTechProgression.Add(currentDef, currentDef.GetModExtension<DecoModExtension>().decoTechLevels);
                decoDictionary.Add(currentDef,
                    (currentDef.GetModExtension<DecoModExtension>().decoTechLevels,
                        currentDef.GetModExtension<DecoModExtension>().decoLocationType));

                switch (currentDef.GetModExtension<DecoModExtension>().decoLocationType)
                {
                    case DecoLocationType.Wall:
                        wallDecoDefs.Add(currentDef);
                        wallDecoHashes.Add(currentDef.GetHashCode());
                        break;
                    case DecoLocationType.Bedside:
                        bedsideDecoDefs.Add(currentDef);
                        bedsideDecoHashes.Add(currentDef.GetHashCode());
                        break;
                    case DecoLocationType.Ceiling:
                        ceilingDecoDefs.Add(currentDef);
                        ceilingDecoHashes.Add(currentDef.GetHashCode());
                        break;
                }
            }

            switch (currentDef.defName)
            {
                case "Wall":
                case var val when new Regex("(Smoothed)+").IsMatch(val):
                    currentDef.comps.Add(attachableThingComp);
                    wallHashes.Add(currentDef.GetHashCode());
                    break;
                case "EndTable":
                    currentDef.comps.Add(attachableThingComp);
                    bedsideTables.Add(currentDef);
                    break;
                case "Human":
                    currentDef.comps.Add(pawnDecoThingComp);
                    break;
                default:
                    var comp = currentDef.GetCompProperties<CompProperties_Facility>();
                    if (comp is { mustBePlacedAdjacentCardinalToBedHead: true } &&
                        currentDef.label.ToLower().Contains("table") && !currentDef.label.ToLower().Contains("lamp"))
                    {
                        bedsideTables.Add(currentDef);
                        currentDef.comps.Add(attachableThingComp);
                    }

                    break;
            }
        }

        Log.Message(
            $"[ColonistsDeco]: Found {bedsideTables.Count} types of bedside tables: {string.Join(", ", bedsideTables)}");

        foreach (var researchDef in DefDatabase<ResearchProjectDef>.AllDefs)
        {
            if (researchDef.tab == DefDatabase<ResearchTabDef>.GetNamed("Decorations"))
            {
                researchProjectDefs.Add(researchDef);
            }
        }
    }

    public static bool IsCeilingDeco(Thing thing)
    {
        return ceilingDecoHashes.Contains(thing.def.GetHashCode());
    }

    public static bool IsWallDeco(Thing thing)
    {
        return wallDecoHashes.Contains(thing.def.GetHashCode());
    }

    public static bool IsBedsideDeco(Thing thing)
    {
        return bedsideDecoHashes.Contains(thing.def.GetHashCode());
    }

    public static bool IsWall(Thing thing)
    {
        return wallHashes.Any(h => h == thing.def.GetHashCode());
    }

    public static List<ThingDef> GetDecoList(DecoLocationType decoLocationType)
    {
        var decoList = new List<ThingDef>();
        var locationDecoList = new List<ThingDef>();
        var maxTechLevel = TechLevel.Neolithic;

        foreach (var researchProjectDef in researchProjectDefs)
        {
            if (researchProjectDef.IsFinished)
            {
                maxTechLevel = researchProjectDef.techLevel;
            }
        }

        foreach (var deco in decoDictionary.Keys)
        {
            if (!decoDictionary.TryGetValue(deco, out var decoTuple))
            {
                continue;
            }

            if (decoTuple.Item1.Any(t => t == maxTechLevel) && decoTuple.Item2 == decoLocationType)
            {
                decoList.Add(deco);
            }
            else if (decoTuple.Item2 == decoLocationType)
            {
                locationDecoList.Add(deco);
            }
        }

        return decoList.Count > 0 ? decoList : locationDecoList;
    }
}