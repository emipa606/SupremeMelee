using Verse;

namespace SupremeMelee;

public static class SupremeVerbUtility
{
    public static bool IsDualWielding(this Pawn pawn)
    {
        Pawn_EquipmentTracker equipment;
        if ((equipment = pawn.equipment) != null)
        {
            return equipment.AllEquipmentListForReading.Count(thing =>
                thing.def.IsMeleeWeapon && thing.def.equipmentType == EquipmentType.Primary) >= 2;
        }

        return false;
    }

    public static bool IsWieldingMeleeWeapons(this Pawn pawn)
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