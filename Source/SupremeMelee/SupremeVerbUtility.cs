using System.Linq;
using Verse;

namespace SupremeMelee
{
    // Token: 0x02000007 RID: 7
    public static class SupremeVerbUtility
    {
        // Token: 0x0600000D RID: 13 RVA: 0x00002958 File Offset: 0x00000B58
        public static bool IsDualWielding(this Pawn pawn)
        {
            Pawn_EquipmentTracker equipment;
            bool result;
            if ((equipment = pawn.equipment) != null)
            {
                result = equipment.AllEquipmentListForReading.Count(thing =>
                    thing.def.IsMeleeWeapon && thing.def.equipmentType == EquipmentType.Primary) >= 2;
            }
            else
            {
                result = false;
            }

            return result;
        }

        // Token: 0x0600000E RID: 14 RVA: 0x000029B4 File Offset: 0x00000BB4
        public static bool IsWieldingMeleeWeapons(this Pawn pawn)
        {
            Pawn_EquipmentTracker equipment;
            bool result;
            if ((equipment = pawn.equipment) != null)
            {
                result = equipment.AllEquipmentListForReading.Any(thing =>
                             thing.def.IsMeleeWeapon && thing.def.equipmentType == EquipmentType.Primary) ||
                         pawn.health.hediffSet.GetHediffsVerbs() != null;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}