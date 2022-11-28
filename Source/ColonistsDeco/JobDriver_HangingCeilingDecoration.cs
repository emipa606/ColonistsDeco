using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace ColonistsDeco;

internal class JobDriver_HangingCeilingDecoration : JobDriver
{
    protected const int BaseWorkAmount = 100;
    private float workLeft = 100f;
    protected LocalTargetInfo placeInfo => job.GetTarget(TargetIndex.A);

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return pawn.Reserve(placeInfo, job, 1, -1, null, errorOnFailed);
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        yield return Toils_Reserve.Reserve(TargetIndex.A);

        yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);

        var hangCeilingDeco = new Toil
        {
            handlingFacing = true,
            initAction = delegate { workLeft = 100f; },
            tickAction = delegate
            {
                pawn.skills?.Learn(SkillDefOf.Construction, 0.085f);

                var statValue = pawn.GetStatValueForPawn(StatDefOf.ConstructionSpeed, pawn);
                workLeft -= statValue * 1.7f;
                if (!(workLeft <= 0f))
                {
                    return;
                }

                var ceilingDecos = Utility.GetDecoList(DecoLocationType.Ceiling);

                IList<Thing> thingsInRoom = pawn.ownership.OwnedBed.GetRoom().ContainedAndAdjacentThings;

                var possibleCeilingDecos = new List<ThingDef>(ceilingDecos);
                foreach (var thingInRoom in thingsInRoom)
                {
                    if (Utility.IsCeilingDeco(thingInRoom))
                    {
                        possibleCeilingDecos.Remove(thingInRoom.def);
                    }
                }

                var ceilingDeco = possibleCeilingDecos.Count > 0
                    ? possibleCeilingDecos.RandomElement()
                    : ceilingDecos.RandomElement();

                var thing = ThingMaker.MakeThing(ceilingDeco);
                thing.SetFactionDirect(pawn.Faction);
                var compDecoration = thing.TryGetComp<CompDecoration>();
                if (compDecoration != null)
                {
                    compDecoration.decorationCreator = pawn.Name.ToStringShort;
                }

                GenSpawn.Spawn(thing, placeInfo.Cell, Map, Rot4.North);
                ReadyForNextToil();
            },
            defaultCompleteMode = ToilCompleteMode.Never
        };

        hangCeilingDeco.WithProgressBar(TargetIndex.A, () => (BaseWorkAmount - workLeft) / BaseWorkAmount, true);

        yield return hangCeilingDeco;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref workLeft, "workLeft");
    }
}