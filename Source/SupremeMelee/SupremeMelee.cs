using Mlie;
using UnityEngine;
using Verse;

namespace SupremeMelee;

public class SupremeMelee : Mod
{
    private static string currentVersion;
    private string maxParryChanceBuffer;

    private string parryMagnitudeBuffer;

    public SupremeMelee(ModContentPack content) : base(content)
    {
        SupremeMeleeModSettings.Instance = GetSettings<SupremeMeleeModSettings>();
        if (SupremeMeleeModSettings.Instance == null)
        {
            return;
        }

        parryMagnitudeBuffer = SupremeMeleeModSettings.Instance.ParryMagnitude.ToString();
        maxParryChanceBuffer = SupremeMeleeModSettings.Instance.MaximumParryChance.ToString();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "SupremeMelee";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var num = 0;
        const float rowHeight = 48f;
        var inRect2 = new Rect(inRect);
        var rowRect = UIHelper.GetRowRect(inRect2, rowHeight, num);
        num++;
        Widgets.CheckboxLabeled(rowRect, "SupremeMelee_VerboseParryReadout".Translate(),
            ref SupremeMeleeModSettings.Instance.VerboseParryReadout);
        var rowRect2 = UIHelper.GetRowRect(inRect2, rowHeight, num);
        num++;
        Widgets.CheckboxLabeled(rowRect2, "SupremeMelee_AnimalSizeScaling".Translate(),
            ref SupremeMeleeModSettings.Instance.AnimalSizeScaling);
        var rowRect3 = UIHelper.GetRowRect(inRect2, rowHeight, num);
        num++;
        Widgets.TextFieldNumericLabeled(rowRect3,
            "SupremeMelee_ParryMagnitude".Translate(
                $"Min={SupremeMeleeModSettings.Instance.MinParryMagnitude}, Max={SupremeMeleeModSettings.Instance.MaxParryMagnitude}"),
            ref SupremeMeleeModSettings.Instance.ParryMagnitude, ref parryMagnitudeBuffer, 1f);
        var rowRect4 = UIHelper.GetRowRect(inRect2, rowHeight, num);
        Widgets.TextFieldNumericLabeled(rowRect4, "SupremeMelee_MaxParryChance".Translate(),
            ref SupremeMeleeModSettings.Instance.MaximumParryChance, ref maxParryChanceBuffer, 0.01f);

        if (currentVersion == null)
        {
            return;
        }

        var rowRect5 = UIHelper.GetRowRect(inRect2, rowHeight, num);
        GUI.contentColor = Color.gray;
        Widgets.Label(rowRect5, "CurrentModVersion_Label".Translate(currentVersion));
        GUI.contentColor = Color.white;
    }
}