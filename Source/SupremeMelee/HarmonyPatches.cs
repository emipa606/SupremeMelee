using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace SupremeMelee;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    public const float maxParryingChance = 0.95f;

    static HarmonyPatches()
    {
        var harmony = new Harmony("chjees.suprememelee");
        var typeFromHandle = typeof(Pawn);
        harmony.Patch(typeFromHandle.GetMethod("PreApplyDamage"),
            new HarmonyMethod(typeof(HarmonyPatches).GetMethod("Patch_Pawn_PreApplyDamage")));
    }

    public static bool Patch_Pawn_PreApplyDamage(ref Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
    {
        absorbed = false;
        try
        {
            if (dinfo.Instigator == null)
            {
                return true;
            }

            var pawn = __instance;
            if (pawn.Downed || pawn.InBed() || pawn.IsBurning() || (pawn.stances?.stunner?.Stunned ?? false) ||
                !pawn.Drafted &&
                pawn.stances?.curStance is not Stance_Busy && pawn.stances?.curStance is not Stance_Warmup)
            {
                return true;
            }

            if (dinfo.WeaponBodyPartGroup != null || dinfo.Weapon is { IsMeleeWeapon: true })
            {
                if (!pawn.IsWieldingMeleeWeapons())
                {
                    return true;
                }

                if (SupremeMeleeStatDefOf.SupremeMelee_MeleeParryMeleeChance.Worker.IsDisabledFor(pawn))
                {
                    return true;
                }

                var meleeParryMeleeChance =
                    pawn.GetStatValue(SupremeMeleeStatDefOf.SupremeMelee_MeleeParryMeleeChance);
                if (!(meleeParryMeleeChance > 0f))
                {
                    return true;
                }

                var defenderMeleeSkill = pawn.skills?.GetSkill(SkillDefOf.Melee)?.Level ?? 0f;
                var attackerMeleeSkillBalanced = 10f;
                if (dinfo.Instigator is Pawn instigator)
                {
                    if (instigator.skills == null)
                    {
                        var animalSizeScaling = SupremeMeleeModSettings.Instance.animalSizeScaling;
                        if (animalSizeScaling)
                        {
                            var bodySizeDifference = instigator.BodySize / pawn.BodySize;
                            attackerMeleeSkillBalanced = Mathf.Clamp(10f * bodySizeDifference, 10f, 25f);
                        }
                    }
                    else
                    {
                        attackerMeleeSkillBalanced = instigator.skills.GetSkill(SkillDefOf.Melee)?.Level ?? 0;
                    }
                }

                var meleeSkillDifference = Mathf.Clamp(defenderMeleeSkill / attackerMeleeSkillBalanced,
                    SupremeMeleeModSettings.Instance.MinParryMagnitude,
                    SupremeMeleeModSettings.Instance.MaxParryMagnitude);
                var effectiveParryChance = Math.Min(meleeParryMeleeChance * meleeSkillDifference,
                    SupremeMeleeModSettings.Instance.maximumParryChance);
                if (!Rand.Chance(effectiveParryChance) &&
                    (!pawn.IsDualWielding() || !Rand.Chance(effectiveParryChance)))
                {
                    return true;
                }

                if (pawn.Map != null)
                {
                    SoundDefOf.Crunch?.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
                    var loc = pawn.TrueCenter() +
                              (Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle).RotatedBy(180f) * 0.5f);
                    var scale = Mathf.Min(10f, 2f + (dinfo.Amount / 10f));
                    FleckMaker.Static(loc, pawn.Map, FleckDefOf.ExplosionFlash, scale);
                    var verboseParryReadout = SupremeMeleeModSettings.Instance.verboseParryReadout;
                    if (verboseParryReadout)
                    {
                        MoteMaker.ThrowText(pawn.DrawPos, pawn.Map,
                            "SupremeMelee_TextMote_Parry".Translate(
                                $"{effectiveParryChance.ToStringPercent()}={meleeParryMeleeChance}*{meleeSkillDifference}={defenderMeleeSkill}M/{attackerMeleeSkillBalanced}M "),
                            3.9f);
                    }
                    else
                    {
                        MoteMaker.ThrowText(pawn.DrawPos, pawn.Map,
                            "SupremeMelee_TextMote_Parry".Translate(effectiveParryChance.ToStringPercent()),
                            1.9f);
                    }
                }


                absorbed = true;
                return false;
            }

            if (dinfo.Weapon == null || !dinfo.Weapon.IsRangedWeapon)
            {
                return true;
            }

            if (!pawn.IsWieldingMeleeWeapons())
            {
                return true;
            }

            var meleeParryProjectileChance =
                pawn.GetStatValue(SupremeMeleeStatDefOf.SupremeMelee_MeleeParryProjectileChance);
            if (!(meleeParryProjectileChance > 0f))
            {
                return true;
            }

            var num6 = pawn.skills?.GetSkill(SkillDefOf.Melee)?.Level ?? 0;
            var num7 = 10f;
            if (dinfo.Instigator is Pawn { skills: { } } pawn3)
            {
                num7 = pawn3.skills.GetSkill(SkillDefOf.Shooting)?.Level ?? 0;
            }

            var num8 = Mathf.Clamp(num6 / num7, SupremeMeleeModSettings.Instance.MinParryMagnitude,
                SupremeMeleeModSettings.Instance.MaxParryMagnitude);
            var num9 = Math.Min(meleeParryProjectileChance * num8,
                SupremeMeleeModSettings.Instance.maximumParryChance);
            if (!Rand.Chance(num9) && (!pawn.IsDualWielding() || !Rand.Chance(num9)))
            {
                return true;
            }

            if (pawn.Map != null)
            {
                SoundDefOf.BulletImpact_Ground?.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
                var loc2 = pawn.TrueCenter() +
                           (Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle).RotatedBy(180f) * 0.5f);
                var scale2 = Mathf.Min(10f, 2f + (dinfo.Amount / 10f));
                FleckMaker.Static(loc2, pawn.Map, FleckDefOf.ExplosionFlash, scale2);
                var verboseParryReadout2 = SupremeMeleeModSettings.Instance.verboseParryReadout;
                if (verboseParryReadout2)
                {
                    MoteMaker.ThrowText(pawn.DrawPos, pawn.Map,
                        "SupremeMelee_TextMote_Parry".Translate(
                            $"{num9.ToStringPercent()}={meleeParryProjectileChance}*{num8}={num6}M/{num7}S "),
                        3.9f);
                }
                else
                {
                    MoteMaker.ThrowText(pawn.DrawPos, pawn.Map,
                        "SupremeMelee_TextMote_Parry".Translate(num9.ToStringPercent()), 1.9f);
                }
            }


            absorbed = true;
            return false;
        }
        catch (Exception ex)
        {
            Log.Error($"Error in Supreme Melee: {ex}");
        }

        return false;
    }
}