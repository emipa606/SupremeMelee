using Verse;

namespace SupremeMelee;

public static class SupremeVerbUtility
{
    extension(Pawn pawn)
    {
        public bool IsDualWielding()
        {
            Pawn_EquipmentTracker equipment;
            if ((equipment = pawn.equipment) != null)
            {
                return equipment.AllEquipmentListForReading.Count(thing =>
                    thing.def.IsMeleeWeapon && thing.def.equipmentType == EquipmentType.Primary) >= 2;
            }

            return false;
        }

        public bool IsWieldingMeleeWeapons()
        {
            Pawn_EquipmentTracker equipment;
            if ((equipment = pawn.equipment) != null)
            {
                return equipment.AllEquipmentListForReading.Any(thing =>
                           thing.def.IsMeleeWeapon && thing.def.equipmentType == EquipmentType.Primary) ||
                       pawn.health.hediffSet.GetHediffsVerbs() != null;
            }

            return false;
        }
    }
}