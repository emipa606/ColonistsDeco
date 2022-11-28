using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

internal class CeilingDeco_Default : Building
{
    private Texture2D decoImage;
    private Texture2D inspectIcon;

    public override IEnumerable<Gizmo> GetGizmos()
    {
        decoImage = (Texture2D)Graphic.ExtractInnerGraphicFor(this).MatSouth.mainTexture;
        inspectIcon = ContentFinder<Texture2D>.Get("Icons/InspectIcon");

        if (inspectIcon == null || decoImage == null)
        {
            yield return null;
        }

        var item = new Command_Action
        {
            defaultLabel = "Deco.inspect".Translate(),
            defaultDesc = "Deco.inspectinfo".Translate(),
            icon = inspectIcon,
            action = openInspectWindow
        };

        yield return item;
    }

    private void openInspectWindow()
    {
        var decorationName = this.TryGetComp<CompDecoration>().decorationName;
        var decorationCreator = this.TryGetComp<CompDecoration>().decorationCreator;
        Find.WindowStack.Add(new Dialog_Inspect("Deco.hungby".Translate(decorationName, decorationCreator),
            decoImage));
    }
}