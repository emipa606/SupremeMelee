using Verse;

namespace SupremeMelee;

public class SupremeMeleeModSettings : ModSettings
{
    public static SupremeMeleeModSettings Instance;

    public bool AnimalSizeScaling = true;

    public float MaximumParryChance = 0.95f;

    public float ParryMagnitude = 2f;

    public bool VerboseParryReadout;

    public SupremeMeleeModSettings()
    {
        Instance = this;
    }

    public float MinParryMagnitude => 1f / ParryMagnitude;

    public float MaxParryMagnitude => ParryMagnitude;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref VerboseParryReadout, "verboseParryReadout");
        Scribe_Values.Look(ref ParryMagnitude, "parryMagnitude", 2f);
        Scribe_Values.Look(ref MaximumParryChance, "maximumParryChance", 0.95f);
        Scribe_Values.Look(ref AnimalSizeScaling, "animalSizeScaling", true);
    }
}