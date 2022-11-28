//Thank you Helldragger!

using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace ColonistsDeco;

internal class JoyGiver_HangingWallDecoration : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        var pawnMap = pawn.Map;

        if (pawn.WorkTypeIsDisabled(WorkTypeDefOf.Construction) || pawn.IsPrisoner || pawn.ownership.OwnedBed == null ||
            !pawn.TryGetComp<CompPawnDeco>().CanDecorate ||
            pawn.story.traits.HasTrait(TraitDefOf.Ascetic))
        {
            return null;
        }

        pawn.TryGetComp<CompPawnDeco>().ResetDecoCooldown();

        IList<IntVec3> wallLocations = pawn.ownership.OwnedBed.GetRoom().BorderCells.ToList();

        IList<Thing> wallThingList = new List<Thing>();

        foreach (var wallLocation in wallLocations)
        {
            if (!wallLocation.IsValid || !wallLocation.InBounds(pawnMap))
            {
                continue;
            }

            IList<Thing> wallTempThingList = wallLocation.GetThingList(pawnMap);
            foreach (var wallThing in wallTempThingList)
            {
                if (Utility.IsWall(wallThing))
                {
                    wallThingList.Add(wallThing);
                }
            }
        }

        IList<Thing> thingsInRoom = pawn.ownership.OwnedBed.GetRoom().ContainedAndAdjacentThings;

        var wallDecoAmount = 0;

        foreach (var thingInRoom in thingsInRoom)
        {
            if (Utility.IsWallDeco(thingInRoom))
            {
                wallDecoAmount++;
            }
        }

        var wall = wallThingList.RandomElement();
        var randomPlacePos = IntVec3.Invalid;
        var i = 0;
        while (i < 4)
        {
            var intVec = wall.Position + GenAdj.CardinalDirections[i];
            var region = (wall.Position + GenAdj.CardinalDirections[i]).GetRegion(pawnMap);
            if (region != null && region.Room == pawn.ownership.OwnedBed.GetRoom())
            {
                randomPlacePos = intVec;
            }

            var num = i + 1;
            i = num;
        }

        if (pawn.CanReserveAndReach(randomPlacePos, PathEndMode.OnCell, Danger.None) && pawn.CanReserve(wall) &&
            !randomPlacePos.IsValid || wall == null || wall.def == new ThingDef() ||
            wallDecoAmount >= ColonistsDecoMain.Settings.wallDecorationLimit)
        {
            return null;
        }

        var job = JobMaker.MakeJob(def.jobDef, randomPlacePos, wall);
        return job;
    }
}