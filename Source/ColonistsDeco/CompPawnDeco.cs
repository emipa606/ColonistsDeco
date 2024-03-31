using System;
using Verse;

namespace ColonistsDeco;

internal class CompPawnDeco : ThingComp
{
    private int decoCooldown = ColonistsDecoMain.Settings.defaultDecoCooldown;

    public int DecoCooldown => decoCooldown;

    public bool CanDecorate => decoCooldown == 0;

    public void ResetDecoCooldown()
    {
        var random = new Random();
        if (ColonistsDecoMain.Settings.defaultDecoCooldown != 0)
        {
            decoCooldown = ColonistsDecoMain.Settings.defaultDecoCooldown + random.Next(30000);
        }
        else
        {
            decoCooldown = 0;
        }
    }

    public void RemoveDecoCooldown()
    {
        decoCooldown = 0;
    }

    public override void CompTick()
    {
        base.CompTick();

        if (decoCooldown != 0)
        {
            decoCooldown--;
        }
    }

    public override void PostExposeData()
    {
        base.PostExposeData();

        Scribe_Values.Look(ref decoCooldown, "decoCooldown", ColonistsDecoMain.Settings.defaultDecoCooldown);
    }
}