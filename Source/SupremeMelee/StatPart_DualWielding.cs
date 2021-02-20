using RimWorld;
using Verse;

namespace SupremeMelee
{
    // Token: 0x02000006 RID: 6
    public class StatPart_DualWielding : StatPart
    {
        // Token: 0x0400000C RID: 12
        private readonly float dualWieldingFactor = 0.6f;

        // Token: 0x0600000A RID: 10 RVA: 0x000028AC File Offset: 0x00000AAC
        public override string ExplanationPart(StatRequest req)
        {
            var flag = req.Thing is Pawn pawn && pawn.IsDualWielding();
            string result;
            if (flag)
            {
                result = "SupremeMelee_DualWieldingFactor".Translate() + ": x" + dualWieldingFactor.ToStringPercent();
            }
            else
            {
                result = null;
            }

            return result;
        }

        // Token: 0x0600000B RID: 11 RVA: 0x0000290C File Offset: 0x00000B0C
        public override void TransformValue(StatRequest req, ref float val)
        {
            var flag = req.Thing is Pawn pawn && pawn.IsDualWielding();
            if (flag)
            {
                val *= dualWieldingFactor;
            }
        }
    }
}