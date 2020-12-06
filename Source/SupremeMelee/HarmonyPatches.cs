using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace SupremeMelee
{
	// Token: 0x02000003 RID: 3
	[StaticConstructorOnStartup]
	public static class HarmonyPatches
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		static HarmonyPatches()
		{
			var harmony = new Harmony("chjees.suprememelee");
			Type typeFromHandle = typeof(Pawn);
			harmony.Patch(typeFromHandle.GetMethod("PreApplyDamage"), new HarmonyMethod(typeof(HarmonyPatches).GetMethod("Patch_Pawn_PreApplyDamage")), null, null, null);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
		public static bool Patch_Pawn_PreApplyDamage(ref Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;
            if (dinfo.Instigator == null)
            {
                return true;
            }
            Pawn pawn = __instance;
            if (pawn.Downed || pawn.InBed() || pawn.IsBurning() || pawn.stances.stunner.Stunned || (!pawn.Drafted && !(pawn.stances.curStance is Stance_Busy) && !(pawn.stances.curStance is Stance_Warmup)))
            {
                return true;
            }
            if (dinfo.WeaponBodyPartGroup != null || (dinfo.Weapon != null && dinfo.Weapon.IsMeleeWeapon))
            {
                if (pawn.IsWieldingMeleeWeapons())
                {
                    var statValue = pawn.GetStatValue(SupremeMeleeStatDefOf.SupremeMelee_MeleeParryMeleeChance, true);
                    if (statValue > 0f)
                    {
                        var num = (float)pawn.skills.GetSkill(SkillDefOf.Melee).Level;
                        var num2 = 10f;
                        if (dinfo.Instigator is Pawn pawn2)
                        {
                            if (pawn2.skills == null)
                            {
                                var animalSizeScaling = SupremeMeleeModSettings.Instance.animalSizeScaling;
                                if (animalSizeScaling)
                                {
                                    var num3 = pawn2.BodySize / pawn.BodySize;
                                    num2 = Mathf.Clamp(10f * num3, 10f, 25f);
                                }
                            }
                            else
                            {
                                num2 = pawn2.skills.GetSkill(SkillDefOf.Melee).Level;
                            }
                        }
                        var num4 = Mathf.Clamp(num / num2, SupremeMeleeModSettings.Instance.MinParryMagnitude, SupremeMeleeModSettings.Instance.MaxParryMagnitude);
                        var num5 = Math.Min(statValue * num4, SupremeMeleeModSettings.Instance.maximumParryChance);
                        if (Rand.Chance(num5) || (pawn.IsDualWielding() && Rand.Chance(num5)))
                        {
                            SoundDefOf.Crunch.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map, false));
                            Vector3 loc = pawn.TrueCenter() + (Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle).RotatedBy(180f) * 0.5f);
                            var scale = Mathf.Min(10f, 2f + (dinfo.Amount / 10f));
                            MoteMaker.MakeStaticMote(loc, pawn.Map, ThingDefOf.Mote_ExplosionFlash, scale);
                            var verboseParryReadout = SupremeMeleeModSettings.Instance.verboseParryReadout;
                            if (verboseParryReadout)
                            {
                                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "SupremeMelee_TextMote_Parry".Translate(string.Format("{0}={1}*{2}={3}M/{4}M ", new object[]
                                {
                                            num5.ToStringPercent(),
                                            statValue,
                                            num4,
                                            num,
                                            num2
                                })), 3.9f);
                            }
                            else
                            {
                                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "SupremeMelee_TextMote_Parry".Translate(num5.ToStringPercent()), 1.9f);
                            }
                            absorbed = true;
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (dinfo.Weapon == null || !dinfo.Weapon.IsRangedWeapon)
                {
                    return true;
                }
                if (pawn.IsWieldingMeleeWeapons())
                {
                    var statValue2 = pawn.GetStatValue(SupremeMeleeStatDefOf.SupremeMelee_MeleeParryProjectileChance, true);
                    if (statValue2 > 0f)
                    {
                        var num6 = (float)pawn.skills.GetSkill(SkillDefOf.Melee).Level;
                        var num7 = 10f;
                        if (dinfo.Instigator is Pawn pawn3 && pawn3.skills != null)
                        {
                            num7 = pawn3.skills.GetSkill(SkillDefOf.Shooting).Level;
                        }
                        var num8 = Mathf.Clamp(num6 / num7, SupremeMeleeModSettings.Instance.MinParryMagnitude, SupremeMeleeModSettings.Instance.MaxParryMagnitude);
                        var num9 = Math.Min(statValue2 * num8, SupremeMeleeModSettings.Instance.maximumParryChance);
                        if (Rand.Chance(num9) || (pawn.IsDualWielding() && Rand.Chance(num9)))
                        {
                            SoundDefOf.BulletImpact_Ground.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map, false));
                            Vector3 loc2 = pawn.TrueCenter() + (Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle).RotatedBy(180f) * 0.5f);
                            var scale2 = Mathf.Min(10f, 2f + (dinfo.Amount / 10f));
                            MoteMaker.MakeStaticMote(loc2, pawn.Map, ThingDefOf.Mote_ExplosionFlash, scale2);
                            var verboseParryReadout2 = SupremeMeleeModSettings.Instance.verboseParryReadout;
                            if (verboseParryReadout2)
                            {
                                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "SupremeMelee_TextMote_Parry".Translate(string.Format("{0}={1}*{2}={3}M/{4}S ", new object[]
                                {
                                                num9.ToStringPercent(),
                                                statValue2,
                                                num8,
                                                num6,
                                                num7
                                })), 3.9f);
                            }
                            else
                            {
                                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "SupremeMelee_TextMote_Parry".Translate(num9.ToStringPercent()), 1.9f);
                            }
                            absorbed = true;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // Token: 0x04000003 RID: 3
        public const float maxParryingChance = 0.95f;
	}
}
