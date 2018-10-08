using System;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : IWeaponController
{
	private InGameSettingsManager.Player.SpearActionInfo spearInfo;

	private Player owner;

	private bool isCtrlActive;

	private bool isSoulSacrificedHp;

	private bool isSoulHealedHp;

	private bool isSoulNarrowEscaped;

	private bool isBladeEffectContinue;

	private float chargeRate;

	private Transform bladeEffectTrans;

	private Character.HealData healData;

	private float spActionGauge
	{
		get
		{
			if ((UnityEngine.Object)owner == (UnityEngine.Object)null)
			{
				return 0f;
			}
			return owner.spActionGauge[owner.weaponIndex];
		}
		set
		{
			if (!((UnityEngine.Object)owner == (UnityEngine.Object)null))
			{
				owner.spActionGauge[owner.weaponIndex] = value;
			}
		}
	}

	public void Init(Player player)
	{
		if (!((UnityEngine.Object)player == (UnityEngine.Object)null) && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			owner = player;
			spearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
			healData = new Character.HealData(0, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>
			{
				80
			});
		}
	}

	public void OnLoadComplete()
	{
		if (!owner.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
		{
			isCtrlActive = false;
		}
		else
		{
			isCtrlActive = true;
		}
	}

	public void OnActDead()
	{
		if (isCtrlActive)
		{
			isSoulNarrowEscaped = false;
		}
	}

	public void OnEndAction()
	{
		if (isCtrlActive)
		{
			isSoulSacrificedHp = false;
			isSoulHealedHp = false;
			if (!isBladeEffectContinue)
			{
				ClearBladeEffect();
				ClearStoredChargeRate();
			}
			isBladeEffectContinue = false;
		}
	}

	public void OnActAvoid()
	{
	}

	public void OnRelease()
	{
	}

	public void Update()
	{
		if (isCtrlActive)
		{
			UpdateSpActionGauge();
		}
	}

	private void UpdateSpActionGauge()
	{
		if (isCtrlActive && !((UnityEngine.Object)owner == (UnityEngine.Object)null) && owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && owner.isBoostMode)
		{
			float num = 0f;
			num = ((!owner.enableInputCharge) ? (spearInfo.Soul_BoostModeGaugeDecreasePerSecond * Time.deltaTime) : (spearInfo.Soul_BoostModeGaugeDecreasePerSecondOnSpActionCharging * Time.deltaTime));
			float num2 = 1f + owner.buffParam.GetGaugeDecreaseRate();
			spActionGauge -= num * num2;
			if (spActionGauge <= 0f)
			{
				spActionGauge = 0f;
			}
		}
	}

	public void SacrificeHPBySoulAttackHit()
	{
		if (isCtrlActive && owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && !owner.isBoostMode && !isSoulSacrificedHp && (owner.IsCoopNone() || owner.IsOriginal()))
		{
			int num = Mathf.FloorToInt((float)(owner.hpMax * GetSoulSpearSacrificeHpPercentByAttackID()) * 0.01f);
			if (num > 0)
			{
				SacrificedHp(num, false);
			}
		}
	}

	public void SacrificedHp(int sacrificedHP, bool isPacket = false)
	{
		int num = owner.hp - sacrificedHP;
		if (num <= 0 && (owner.attackID != spearInfo.Soul_AttackId || !isSoulNarrowEscaped))
		{
			num = 1;
			isSoulNarrowEscaped = true;
		}
		owner.hp = num;
		isSoulSacrificedHp = true;
		if (!isPacket)
		{
			if (MonoBehaviourSingleton<UIDamageManager>.IsValid() && owner is Self)
			{
				MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage(owner, sacrificedHP, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
			}
			if ((UnityEngine.Object)owner.playerSender != (UnityEngine.Object)null)
			{
				owner.playerSender.OnSacrificedHp(sacrificedHP);
			}
			if (owner.hp <= 0)
			{
				if (owner.buffParam.IsNarrowEscape())
				{
					owner.buffParam.UseNarrowEscape();
					owner.hp = 1;
				}
				else
				{
					owner.healHp = 0;
					owner.ActDead(true, false);
				}
			}
		}
	}

	private int GetSoulSpearSacrificeHpPercentByAttackID()
	{
		int[] soul_AttackIdsForSacrifice = spearInfo.Soul_AttackIdsForSacrifice;
		int[] soul_SacrificeHPPercents = spearInfo.Soul_SacrificeHPPercents;
		if (soul_AttackIdsForSacrifice.IsNullOrEmpty() || soul_SacrificeHPPercents.IsNullOrEmpty())
		{
			return 0;
		}
		if (soul_AttackIdsForSacrifice.Length != soul_SacrificeHPPercents.Length)
		{
			return 0;
		}
		int num = Array.IndexOf(soul_AttackIdsForSacrifice, owner.attackID);
		if (num < 0)
		{
			return 0;
		}
		return soul_SacrificeHPPercents[num];
	}

	public void HealHPBySoulSpAttackHit(SP_ATTACK_TYPE type)
	{
		if (isCtrlActive && owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && type == SP_ATTACK_TYPE.SOUL && !isSoulHealedHp && (owner.IsCoopNone() || owner.IsOriginal()))
		{
			int soulSpearHealHpPercentByChargeRate = GetSoulSpearHealHpPercentByChargeRate();
			if (soulSpearHealHpPercentByChargeRate > 0)
			{
				isSoulHealedHp = true;
				healData.healHp = soulSpearHealHpPercentByChargeRate;
				owner.OnHealReceive(healData);
			}
		}
	}

	public void StoreChargeRate(float chargeRate)
	{
		this.chargeRate = chargeRate;
	}

	private void ClearStoredChargeRate()
	{
		chargeRate = 0f;
	}

	private int GetSoulSpearHealHpPercentByChargeRate()
	{
		if (spearInfo.Soul_HealHPPercents.IsNullOrEmpty())
		{
			return 0;
		}
		if (spearInfo.Soul_HealHPPercents.Length < 3)
		{
			return 0;
		}
		if (chargeRate >= 1f)
		{
			return Mathf.FloorToInt((float)(owner.hpMax * spearInfo.Soul_HealHPPercents[2]) * 0.01f);
		}
		return Mathf.FloorToInt((float)(owner.hpMax * (spearInfo.Soul_HealHPPercents[0] + Mathf.FloorToInt((float)(spearInfo.Soul_HealHPPercents[1] - spearInfo.Soul_HealHPPercents[0]) * chargeRate))) * 0.01f);
	}

	public bool GetBoostDamageUpRate(ref float value)
	{
		value = 1f;
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (owner.actionID != Character.ACTION_ID.ATTACK)
		{
			return false;
		}
		if (!owner.isBoostMode)
		{
			return false;
		}
		value = spearInfo.Soul_BoostElementDamageRate;
		return true;
	}

	public bool IsSoulBoostMode()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (!owner.isBoostMode)
		{
			return false;
		}
		return true;
	}

	public void ExecBladeEffect()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			bladeEffectTrans = EffectManager.GetEffect("ef_btl_wsk2_spear_02_02", owner.FindNode("R_Wep"));
		}
	}

	public void ContinueBladeEffect()
	{
		isBladeEffectContinue = true;
	}

	public void MakeInvincible()
	{
		owner.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
	}

	public bool IsInputedSpAttackContinue()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		return owner.inputComboFlag && owner.inputComboID == spearInfo.Soul_SpAttackContinueId;
	}

	public void ClearBladeEffect()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && !((UnityEngine.Object)bladeEffectTrans == (UnityEngine.Object)null))
		{
			EffectManager.ReleaseEffect(bladeEffectTrans.gameObject, true, false);
			bladeEffectTrans = null;
		}
	}
}
