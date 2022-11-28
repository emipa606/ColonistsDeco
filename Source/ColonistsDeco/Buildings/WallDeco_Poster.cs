using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

internal class WallDeco_Poster : Building
{
    private Texture2D decoImage;
    private Texture2D inspectIcon;

    public override IEnumerable<Gizmo> GetGizmos()
    {
        decoImage = (Texture2D)Graphic.MatSouth.mainTexture;
        inspectIcon = ContentFinder<Texture2D>.Get("Icons/InspectIcon");

        if (inspectIcon == null || decoImage == null)
        {
            yield return null;
        }

        var item = new Command_Action
        {
            defaultLabel = "Deco.inspectposter".Translate(),
            defaultDesc = "Deco.inspectposterinfo".Translate(),
            icon = inspectIcon,
            action = openInspectWindow
        };

        yield return item;
    }

    private void openInspectWindow()
    {
        var decorationName = this.TryGetComp<CompDecoration>().decorationName;
        var decorationCreator = this.TryGetComp<CompDecoration>().decorationCreator;
        Find.WindowStack.Add(new Dialog_Inspect("Deco.hungby".Translate(decorationName, decorationCreator), decoImage));
    }
}