using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ColonistsDeco;

internal class DecoModExtension : DefModExtension
{
    public readonly List<TechLevel> decoTechLevels = [];
    public DecoLocationType decoLocationType;
}