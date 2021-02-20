using Verse;

namespace SupremeMelee
{
    // Token: 0x02000005 RID: 5
    public class SupremeMeleeModSettings : ModSettings
    {
        // Token: 0x04000007 RID: 7
        public static SupremeMeleeModSettings Instance;

        // Token: 0x0400000B RID: 11
        public bool animalSizeScaling = true;

        // Token: 0x0400000A RID: 10
        public float maximumParryChance = 0.95f;

        // Token: 0x04000009 RID: 9
        public float parryMagnitude = 2f;

        // Token: 0x04000008 RID: 8
        public bool verboseParryReadout;

        // Token: 0x06000008 RID: 8 RVA: 0x00002814 File Offset: 0x00000A14
        public SupremeMeleeModSettings()
        {
            Instance = this;
        }

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000006 RID: 6 RVA: 0x000027DC File Offset: 0x000009DC
        public float MinParryMagnitude => 1f / parryMagnitude;

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000007 RID: 7 RVA: 0x000027FC File Offset: 0x000009FC
        public float MaxParryMagnitude => parryMagnitude;

        // Token: 0x06000009 RID: 9 RVA: 0x00002848 File Offset: 0x00000A48
        public override void ExposeData()
        {
            Scribe_Values.Look(ref verboseParryReadout, "verboseParryReadout");
            Scribe_Values.Look(ref parryMagnitude, "parryMagnitude", 2f);
            Scribe_Values.Look(ref maximumParryChance, "maximumParryChance", 0.95f);
            Scribe_Values.Look(ref animalSizeScaling, "animalSizeScaling", true);
        }
    }
}