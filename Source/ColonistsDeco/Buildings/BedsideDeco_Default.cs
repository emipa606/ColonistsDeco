using RimWorld;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

internal class BedsideDeco_Default : Building
{
    private float randomNum1 = Rand.Range(-.05f, .05f);
    private float randomNum2 = Rand.Range(-.05f, .05f);

    public override void Print(SectionLayer layer)
    {
        var center = this.TrueCenter();
        Rand.PushState();
        Rand.Seed = Position.GetHashCode();
        center.y = def.Altitude;
        center.x += randomNum1;
        center.z += randomNum2;
        Rand.PopState();

        Vector2 size;
        bool flipped;
        if (Graphic.ShouldDrawRotated)
        {
            size = Graphic.drawSize;
            flipped = false;
        }
        else
        {
            size = Rotation.IsHorizontal ? Graphic.drawSize.Rotated() : Graphic.drawSize;
            flipped = Rotation == Rot4.West && Graphic.WestFlipped || Rotation == Rot4.East && Graphic.EastFlipped;
        }

        var material = Graphic.MatAt(Rotation, this);
        Graphic.TryGetTextureAtlasReplacementInfo(material, def.category.ToAtlasGroup(), flipped, true, out material,
            out var uvs, out var vertexColor);
        Printer_Plane.PrintPlane(layer, center, size, material, 0f, flipped, uvs,
            [vertexColor, vertexColor, vertexColor, vertexColor]);
        Graphic.ShadowGraphic?.Print(layer, this, 0f);
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref randomNum1, "randomNum1");
        Scribe_Values.Look(ref randomNum2, "randomNum2");
    }
}