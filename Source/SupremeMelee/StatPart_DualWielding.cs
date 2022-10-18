using RimWorld;
using Verse;

namespace SupremeMelee;

public class StatPart_DualWielding : StatPart
{
    private readonly float dualWieldingFactor = 0.6f;

    public override string ExplanationPart(StatRequest req)
    {
        string result;
        if (req.Thing is Pawn pawn && pawn.IsDualWielding())
        {
            result = "SupremeMelee_DualWieldingFactor".Translate() + ": x" + dualWieldingFactor.ToStringPercent();
        }
        else
        {
            result = null;
        }

        return result;
    }

    public override void TransformValue(StatRequest req, ref float val)
    {
        if (req.Thing is Pawn pawn && pawn.IsDualWielding())
        {
            val *= dualWieldingFactor;
        }
    }
}