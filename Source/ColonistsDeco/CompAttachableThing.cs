using System.Collections.Generic;
using Verse;

namespace ColonistsDeco;

internal class CompAttachableThing : ThingComp
{
    private readonly List<Thing> attachedThings = [];

    public override void CompTick()
    {
        base.CompTick();
        {
            if (attachedThings == null)
            {
                return;
            }

            foreach (var thing in attachedThings)
            {
                thing.Position = parent.Position;
            }
        }
    }

    public override void PostDeSpawn(Map map, DestroyMode mode = DestroyMode.Vanish)
    {
        base.PostDeSpawn(map, mode);
        if (attachedThings.Count <= 0)
        {
            return;
        }

        foreach (var attachedThing in attachedThings)
        {
            if (attachedThing.Spawned)
            {
                attachedThing.Destroy(DestroyMode.Deconstruct);
            }
        }

        attachedThings.Clear();
    }

    public void AddAttachment(Thing attachment)
    {
        attachedThings.Add(attachment);
    }
}