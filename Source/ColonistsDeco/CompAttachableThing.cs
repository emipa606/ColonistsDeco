using System.Collections.Generic;
using Verse;

namespace ColonistsDeco;

internal class CompAttachableThing : ThingComp
{
    public readonly List<Thing> attachedThings = [];

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

    public override void PostDeSpawn(Map map)
    {
        base.PostDeSpawn(map);
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

    public void RemoveAttachment(Thing attachment)
    {
        attachedThings.Remove(attachment);
    }
}