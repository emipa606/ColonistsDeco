using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ColonistsDeco;

internal class DecoModExtension : DefModExtension
{
    public DecoLocationType decoLocationType;
    public List<TechLevel> decoTechLevels = new List<TechLevel>();
}