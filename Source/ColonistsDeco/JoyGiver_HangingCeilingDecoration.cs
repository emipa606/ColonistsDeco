using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace ColonistsDeco;

internal class JoyGiver_HangingCeilingDecoration : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        IntVec3 ceilingLocation;
        var pawnMap = pawn.Map;

        if (pawn.WorkTypeIsDisabled(WorkTypeDefOf.Construction) || pawn.IsPrisoner || pawn.ownership.OwnedBed == null ||
            !pawn.TryGetComp<CompPawnDeco>().CanDecorate ||
            pawn.story.traits.HasTrait(TraitDefOf.Ascetic))
        {
            return null;
        }

        pawn.TryGetComp<CompPawnDeco>().ResetDecoCooldown();

        var tempCeilingLocations = pawn.ownership.OwnedBed.GetRoom().Cells;
        IList<IntVec3> ceilingLocations = new List<IntVec3>();

        foreach (var tempCeilingLocation in tempCeilingLocations)
        {
            if (tempCeilingLocation.IsValid && tempCeilingLocation.InBounds(pawnMap) &&
                !tempCeilingLocation.Filled(pawnMap) && tempCeilingLocation.GetThingList(pawnMap).Count == 0 &&
                tempCeilingLocation.Roofed(pawnMap))
            {
                ceilingLocations.Add(tempCeilingLocation);
            }
        }

        if (ceilingLocations.Count > 0)
        {
            ceilingLocation = ceilingLocations.RandomElement();
        }
        else
        {
            return null;
        }

        IList<Thing> thingsInRoom = pawn.ownership.OwnedBed.GetRoom().ContainedAndAdjacentThings;

        var ceilingDecorationAmount = 0;

        foreach (var thingInRoom in thingsInRoom)
        {
            if (Utility.IsCeilingDeco(thingInRoom))
            {
                ceilingDecorationAmount++;
            }
        }

        if (!pawn.CanReserveAndReach(ceilingLocation, PathEndMode.OnCell, Danger.None) ||
            ceilingDecorationAmount >= ColonistsDecoMain.Settings.ceilingDecorationLimit)
        {
            return null;
        }

        var job = JobMaker.MakeJob(def.jobDef, ceilingLocation);
        return job;
    }
}