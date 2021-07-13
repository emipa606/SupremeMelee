using UnityEngine;
using Verse;

namespace SupremeMelee
{
    // Token: 0x02000004 RID: 4
    public class SupremeMelee : Mod
    {
        // Token: 0x04000006 RID: 6
        private string maxParryChanceBuffer;

        // Token: 0x04000005 RID: 5
        private string parryMagnitudeBuffer;

        // Token: 0x06000003 RID: 3 RVA: 0x0000262C File Offset: 0x0000082C
        public SupremeMelee(ModContentPack content) : base(content)
        {
            SupremeMeleeModSettings.Instance = GetSettings<SupremeMeleeModSettings>();
            if (SupremeMeleeModSettings.Instance == null)
            {
                return;
            }

            parryMagnitudeBuffer = SupremeMeleeModSettings.Instance.parryMagnitude.ToString();
            maxParryChanceBuffer = SupremeMeleeModSettings.Instance.maximumParryChance.ToString();
        }

        // Token: 0x06000004 RID: 4 RVA: 0x0000268C File Offset: 0x0000088C
        public override string SettingsCategory()
        {
            return "SupremeMelee";
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000026A4 File Offset: 0x000008A4
        public override void DoSettingsWindowContents(Rect inRect)
        {
            var num = 0;
            var rowHeight = 48f;
            var inRect2 = new Rect(inRect);
            var rowRect = UIHelper.GetRowRect(inRect2, rowHeight, num);
            num++;
            Widgets.CheckboxLabeled(rowRect, "SupremeMelee_VerboseParryReadout".Translate(),
                ref SupremeMeleeModSettings.Instance.verboseParryReadout);
            var rowRect2 = UIHelper.GetRowRect(inRect2, rowHeight, num);
            num++;
            Widgets.CheckboxLabeled(rowRect2, "SupremeMelee_AnimalSizeScaling".Translate(),
                ref SupremeMeleeModSettings.Instance.animalSizeScaling);
            var rowRect3 = UIHelper.GetRowRect(inRect2, rowHeight, num);
            num++;
            Widgets.TextFieldNumericLabeled(rowRect3,
                "SupremeMelee_ParryMagnitude".Translate(
                    $"Min={SupremeMeleeModSettings.Instance.MinParryMagnitude}, Max={SupremeMeleeModSettings.Instance.MaxParryMagnitude}"),
                ref SupremeMeleeModSettings.Instance.parryMagnitude, ref parryMagnitudeBuffer, 1f);
            var rowRect4 = UIHelper.GetRowRect(inRect2, rowHeight, num);
            Widgets.TextFieldNumericLabeled(rowRect4, "SupremeMelee_MaxParryChance".Translate(),
                ref SupremeMeleeModSettings.Instance.maximumParryChance, ref maxParryChanceBuffer, 0.01f);
        }
    }
}