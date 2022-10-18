using Verse;

namespace SupremeMelee;

public class SupremeMeleeModSettings : ModSettings
{
    public static SupremeMeleeModSettings Instance;

    public bool animalSizeScaling = true;

    public float maximumParryChance = 0.95f;

    public float parryMagnitude = 2f;

    public bool verboseParryReadout;

    public SupremeMeleeModSettings()
    {
        Instance = this;
    }

    public float MinParryMagnitude => 1f / parryMagnitude;

    public float MaxParryMagnitude => parryMagnitude;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref verboseParryReadout, "verboseParryReadout");
        Scribe_Values.Look(ref parryMagnitude, "parryMagnitude", 2f);
        Scribe_Values.Look(ref maximumParryChance, "maximumParryChance", 0.95f);
        Scribe_Values.Look(ref animalSizeScaling, "animalSizeScaling", true);
    }
}