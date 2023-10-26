using System.Collections.Generic;
using System.Text.RegularExpressions;
using RimWorld;
using Verse;

namespace ColonistsDeco;

internal class Utility
{
    public static ThingDef wallDef;

    public static ThingDef tornDef;

    public static Dictionary<ThingDef, List<TechLevel>> thingTechProgression =
        new Dictionary<ThingDef, List<TechLevel>>();

    public static Dictionary<ThingDef, (List<TechLevel>, DecoLocationType)> decoDictionary =
        new Dictionary<ThingDef, (List<TechLevel>, DecoLocationType)>();

    public static List<ThingDef> ceilingDecoDefs = new List<ThingDef>();

    public static List<ThingDef> wallDecoDefs = new List<ThingDef>();

    public static List<ThingDef> bedsideDecoDefs = new List<ThingDef>();

    public static List<int> wallHashes = new List<int>();

    public static List<int> ceilingDecoHashes = new List<int>();

    public static List<int> wallDecoHashes = new List<int>();

    public static List<int> bedsideDecoHashes = new List<int>();

    public static List<ResearchProjectDef> researchProjectDefs = new List<ResearchProjectDef>();

    public static List<ThingDef> bedsideTables = new List<ThingDef>();

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

                        if (currentDef.defName == "DECOPosterTorn")
                        {
                            tornDef = currentDef;
                        }

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
                    wallDef = currentDef;
                    currentDef.comps.Add(attachableThingComp);
                    wallHashes.Add(currentDef.GetHashCode());
                    break;
                case var val when new Regex("(Smoothed)+").IsMatch(val):
                    wallDef = currentDef;
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

    public static bool ResearchLevelHasDecos(ResearchProjectDef researchLevel, DecoLocationType decoLocationType)
    {
        var count = 0;

        foreach (var deco in decoDictionary.Keys)
        {
            if (!decoDictionary.TryGetValue(deco, out var decoTuple))
            {
                continue;
            }

            if (decoTuple.Item1.Any(t => t == researchLevel.techLevel) && decoTuple.Item2 == decoLocationType)
            {
                count++;
            }
        }

        return count > 0;
    }

    public static ResearchProjectDef GetHighestResearchedLevel()
    {
        var rd = new ResearchProjectDef();

        foreach (var researchProjectDef in researchProjectDefs)
        {
            if (researchProjectDef.IsFinished)
            {
                rd = researchProjectDef;
            }
        }

        return rd;
    }
}