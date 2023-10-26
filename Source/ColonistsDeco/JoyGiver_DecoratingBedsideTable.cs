using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace ColonistsDeco;

internal class JoyGiver_DecoratingBedsideTable : JoyGiver
{
    public override Job TryGiveJob(Pawn pawn)
    {
        var pawnMap = pawn.Map;

        if (pawn.WorkTypeIsDisabled(WorkTypeDefOf.Construction) || pawn.IsPrisoner || pawn.ownership.OwnedBed == null)
        {
            return null;
        }

        var pawnRoom = pawn.ownership.OwnedBed.GetRoom();

        if (pawnRoom == null || pawnRoom.PsychologicallyOutdoors)
        {
            return null;
        }

        IList<Thing> bedsideTables = pawnRoom.ContainedThingsList(Utility.bedsideTables).ToList();

        for (var i = bedsideTables.Count - 1; i >= 0; i--)
        {
            IList<Thing> ThingList = bedsideTables[i].Position.GetThingList(pawnMap);
            if (ThingList.Any(Utility.IsBedsideDeco))
            {
                bedsideTables.RemoveAt(i);
            }
        }

        if (bedsideTables.NullOrEmpty())
        {
            return null;
        }

        var bedsideTable = bedsideTables.RandomElement();

        var emptyThing = new Thing();
        if (bedsideTable == null || bedsideTable.def == emptyThing.def)
        {
            return null;
        }

        var job = JobMaker.MakeJob(def.jobDef, bedsideTable);
        return job;
    }
}