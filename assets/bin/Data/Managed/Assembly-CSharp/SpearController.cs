using System;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : IWeaponController
{
	private static readonly int defaultMaxStock = 8;

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

	private Transform wepNode;

	private Transform spinNode;

	private Transform spinEffectTrans;

	private Transform spinGroundEffectTrans;

	private float spinTimer;

	private bool enableSpin;

	private bool isHitAttack;

	private bool isBarrierBulletDelete;

	private string spinEffectName = string.Empty;

	private string throwGroundEffectName = string.Empty;

	private string spinThrowGroundEffectName = string.Empty;

	private EffectCtrl spinEffectCtrl;

	private int EFFECT_STATE_NAME_HASH_LOOP_1;

	private int EFFECT_STATE_NAME_HASH_LOOP_2;

	private bool isPlayingSpinSE;

	private bool isPlayingSpinMaxSpeedSE;

	private BulletObject oracleSpBullet;

	private BulletControllerOracleSpearSp oracleSpController;

	private Transform oracleGuardEffect;

	private int consumedStockCounts;

	private bool freeToUseStock;

	public int[] stockedCounts = new int[3];

	public bool FullStocked => StockedCount >= MaxStockCount;

	public int MaxStockCount => defaultMaxStock;

	public bool OracleSpCharged
	{
		get
		{
			if (oracleSpController != null)
			{
				return oracleSpController.Charged;
			}
			return false;
		}
	}

	public bool InOracleSpLoop => oracleSpController != null;

	public float StockedRate
	{
		get
		{
			float result = 0f;
			if (MaxStockCount > 0)
			{
				result = ((consumedStockCounts <= 0) ? ((float)StockedCount / (float)MaxStockCount) : ((float)consumedStockCounts / (float)MaxStockCount));
			}
			return result;
		}
	}

	public int StockedCount
	{
		get
		{
			return stockedCounts[owner.weaponIndex];
		}
		set
		{
			stockedCounts[owner.weaponIndex] = value;
		}
	}

	public bool Guarding
	{
		get;
		set;
	}

	private float spActionGauge
	{
		get
		{
			if (owner == null)
			{
				return 0f;
			}
			return owner.spActionGauge[owner.weaponIndex];
		}
		set
		{
			if (!(owner == null))
			{
				owner.spActionGauge[owner.weaponIndex] = value;
			}
		}
	}

	public void Init(Player player)
	{
		if (!(player == null) && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			owner = player;
			spearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
			healData = new Character.HealData(0, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>
			{
				80
			});
			EFFECT_STATE_NAME_HASH_LOOP_1 = Animator.StringToHash(GameDefine.EFFECT_STATE_NAME_LOOP_1);
			EFFECT_STATE_NAME_HASH_LOOP_2 = Animator.StringToHash(GameDefine.EFFECT_STATE_NAME_LOOP_2);
			for (int i = 0; i < 3; i++)
			{
				stockedCounts[i] = -1;
			}
		}
	}

	public bool IsGuard()
	{
		if (owner != null)
		{
			if (Guarding)
			{
				return !owner.disableGuard;
			}
			return false;
		}
		return Guarding;
	}

	public void OnLoadComplete()
	{
		if (!owner.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
		{
			isCtrlActive = false;
			return;
		}
		isCtrlActive = true;
		wepNode = owner.FindNode(GameDefine.PLAYER_WEAPON_PARENT_NODE_RIGHT);
		spinEffectName = string.Empty;
		throwGroundEffectName = string.Empty;
		spinThrowGroundEffectName = string.Empty;
		switch (owner.spAttackType)
		{
		case SP_ATTACK_TYPE.BURST:
		{
			spinNode = owner.FindNode("weaponR");
			int nowWeaponElement = (int)owner.GetNowWeaponElement();
			if (nowWeaponElement < spearInfo.burstSpearInfo.spinEffectNames.Length)
			{
				spinEffectName = spearInfo.burstSpearInfo.spinEffectNames[nowWeaponElement];
			}
			if (nowWeaponElement < spearInfo.burstSpearInfo.spinThrowGroundEffectNames.Length)
			{
				spinThrowGroundEffectName = spearInfo.burstSpearInfo.spinThrowGroundEffectNames[nowWeaponElement];
			}
			throwGroundEffectName = spearInfo.burstSpearInfo.throwGroundEffectName;
			break;
		}
		case SP_ATTACK_TYPE.ORACLE:
			if (StockedCount < 0)
			{
				StockedCount = owner.buffParam.passive.stockAddInit;
			}
			break;
		}
	}

	public void OnActDead()
	{
		if (isCtrlActive)
		{
			isSoulNarrowEscaped = false;
		}
	}

	public void OnActReaction()
	{
		OnWeaponActionEnd();
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
			isHitAttack = false;
			GuardOff();
			DestroyOracleSp();
		}
	}

	public void OnActAvoid()
	{
		OnWeaponActionEnd();
	}

	public void OnActSkillAction()
	{
		OnWeaponActionEnd();
	}

	public void OnRelease()
	{
	}

	public void OnActAttack(int id)
	{
	}

	public void OnBuffStart(BuffParam.BuffData data)
	{
	}

	public void OnBuffEnd(BuffParam.BUFFTYPE type)
	{
		if (type == BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS)
		{
			consumedStockCounts = 0;
		}
	}

	public void OnChangeWeapon()
	{
		OnWeaponActionEnd();
		if (owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
		{
			owner.OnBuffEnd(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS, sync: true);
		}
	}

	public void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		if ((owner.IsCoopNone() || owner.IsOriginal()) && IsGuard())
		{
			if (owner._CheckJustGuardSec())
			{
				GuardJust();
			}
			else
			{
				GuardOn(isPlayEnd: true);
			}
		}
	}

	public void Update()
	{
		if (isCtrlActive)
		{
			UpdateSpActionGauge();
			UpdateBurstSpin();
			UpdateOracleSp();
		}
	}

	private void UpdateSpActionGauge()
	{
		if (!isCtrlActive || owner == null || !owner.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
		{
			return;
		}
		switch (owner.spAttackType)
		{
		case SP_ATTACK_TYPE.SOUL:
			if (owner.isBoostMode)
			{
				float num4 = 0f;
				num4 = ((!owner.enableInputCharge) ? (spearInfo.Soul_BoostModeGaugeDecreasePerSecond * Time.deltaTime) : (spearInfo.Soul_BoostModeGaugeDecreasePerSecondOnSpActionCharging * Time.deltaTime));
				float num5 = 1f + owner.GetSpGaugeDecreasingRate();
				spActionGauge -= num4 * num5;
				if (spActionGauge <= 0f)
				{
					spActionGauge = 0f;
				}
			}
			break;
		case SP_ATTACK_TYPE.BURST:
			if (owner.isActSpecialAction)
			{
				float num = 1f + owner.GetSpGaugeDecreasingRate();
				spActionGauge -= spearInfo.burstSpearInfo.gaugeDecreaseOnSpAttackPerSecond * Time.deltaTime * num;
				if (spActionGauge <= 0f)
				{
					spActionGauge = 0f;
					owner.SetNextTrigger();
				}
			}
			else if (enableSpin)
			{
				float num2 = 1f + owner.GetSpGaugeDecreasingRate();
				spActionGauge -= spearInfo.burstSpearInfo.gaugeDecreaseOnSpinPerSecond * Time.deltaTime * num2;
				if (spActionGauge <= 0f)
				{
					spActionGauge = 0f;
					OnWeaponActionEnd();
				}
			}
			else
			{
				float num3 = 1f + owner.buffParam.GetGaugeIncreaseRate(SP_ATTACK_TYPE.BURST);
				spActionGauge += spearInfo.burstSpearInfo.gaugeIncreasePerSecond * Time.deltaTime * num3;
				if (spActionGauge >= owner.CurrentWeaponSpActionGaugeMax)
				{
					spActionGauge = owner.CurrentWeaponSpActionGaugeMax;
				}
			}
			break;
		case SP_ATTACK_TYPE.ORACLE:
			UpdateOracleStock();
			break;
		}
	}

	private void UpdateBurstSpin()
	{
		if (spinNode == null || !enableSpin)
		{
			return;
		}
		float spinRate = GetSpinRate();
		float zAngle = Mathf.Lerp(spearInfo.burstSpearInfo.spinSpeedMin, spearInfo.burstSpearInfo.spinSpeedMax, spinRate);
		spinNode.Rotate(0f, 0f, zAngle);
		spinTimer += Time.deltaTime;
		if (spinRate >= 1f)
		{
			if (!spinEffectCtrl.IsCurrentState(EFFECT_STATE_NAME_HASH_LOOP_2))
			{
				spinEffectCtrl.Play(EFFECT_STATE_NAME_HASH_LOOP_2);
			}
			if (spearInfo.burstSpearInfo.spinSeId > 0 && isPlayingSpinSE)
			{
				SoundManager.StopLoopSE(spearInfo.burstSpearInfo.spinSeId, owner);
				isPlayingSpinSE = false;
			}
			if (spearInfo.burstSpearInfo.spinMaxSpeedSeId > 0 && !isPlayingSpinMaxSpeedSE)
			{
				SoundManager.PlayLoopSE(spearInfo.burstSpearInfo.spinMaxSpeedSeId, owner);
				isPlayingSpinMaxSpeedSE = true;
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
				SacrificedHp(num);
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
		if (isPacket)
		{
			return;
		}
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid() && owner is Self)
		{
			MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage(owner, sacrificedHP, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
		}
		if (owner.playerSender != null)
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
				owner.ActDead(force_sync: true);
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
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (owner.actionID != Character.ACTION_ID.ATTACK)
		{
			return false;
		}
		switch (owner.spAttackType)
		{
		case SP_ATTACK_TYPE.SOUL:
			if (!owner.isBoostMode)
			{
				return false;
			}
			value = spearInfo.Soul_BoostElementDamageRate;
			break;
		case SP_ATTACK_TYPE.BURST:
			if (!enableSpin)
			{
				return false;
			}
			if (GetSpinRate() >= 1f)
			{
				value = spearInfo.burstSpearInfo.spinElementDamageRateMax;
			}
			else
			{
				value = spearInfo.burstSpearInfo.spinElementDamageRate;
			}
			break;
		default:
			return false;
		}
		return true;
	}

	public bool GetSpinRate(ref float rate)
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		float num = spearInfo.burstSpearInfo.spinTimeToMaxSpeed - spearInfo.burstSpearInfo.spinTimeToMaxSpeed * owner.buffParam.GetBurstSpearSpinTimeRate();
		if (num <= 0f)
		{
			rate = 1f;
		}
		else
		{
			rate = Mathf.Clamp01(spinTimer / num);
		}
		return true;
	}

	private float GetSpinRate()
	{
		float num = spearInfo.burstSpearInfo.spinTimeToMaxSpeed - spearInfo.burstSpearInfo.spinTimeToMaxSpeed * owner.buffParam.GetBurstSpearSpinTimeRate();
		if (num <= 0f)
		{
			return 1f;
		}
		return Mathf.Clamp01(spinTimer / num);
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

	public bool IsBurstSpin()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		return enableSpin;
	}

	public bool IsSpecialActionHit()
	{
		return IsBurstSpin();
	}

	public void ExecBladeEffect()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			bladeEffectTrans = EffectManager.GetEffect("ef_btl_wsk2_spear_02_02", wepNode);
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

	public void EnableHitFlag()
	{
		if (!isHitAttack && owner.attackID != spearInfo.burstSpearInfo.hitComboAttackId)
		{
			isHitAttack = true;
		}
	}

	public bool IsInputedSpAttackContinue()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (owner.inputComboFlag)
		{
			return owner.inputComboID == spearInfo.Soul_SpAttackContinueId;
		}
		return false;
	}

	public bool IsEnableBurstCombo()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		if (!isHitAttack)
		{
			return false;
		}
		return true;
	}

	private void ClearBladeEffect()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && !(bladeEffectTrans == null))
		{
			EffectManager.ReleaseEffect(bladeEffectTrans.gameObject);
			bladeEffectTrans = null;
		}
	}

	private void ClearSpinEffect()
	{
		if (!(spinEffectTrans == null))
		{
			EffectManager.ReleaseEffect(spinEffectTrans.gameObject);
			spinEffectTrans = null;
		}
	}

	private void ClearSpinGroundEffect()
	{
		if (!(spinGroundEffectTrans == null))
		{
			EffectManager.ReleaseEffect(ref spinGroundEffectTrans);
		}
	}

	private void ClearSpinSE()
	{
		if (spearInfo.burstSpearInfo.spinSeId > 0)
		{
			SoundManager.StopLoopSE(spearInfo.burstSpearInfo.spinSeId, owner);
		}
		if (spearInfo.burstSpearInfo.spinMaxSpeedSeId > 0)
		{
			SoundManager.StopLoopSE(spearInfo.burstSpearInfo.spinMaxSpeedSeId, owner);
		}
		isPlayingSpinSE = false;
		isPlayingSpinMaxSpeedSE = false;
	}

	public bool GetNormalAttackId(SP_ATTACK_TYPE _spAtkType, EXTRA_ATTACK_TYPE _exAtkType, ref int _attackId, ref string _motionLayerName)
	{
		if (owner == null || spearInfo == null)
		{
			return false;
		}
		switch (_spAtkType)
		{
		case SP_ATTACK_TYPE.SOUL:
			_attackId = spearInfo.Soul_AttackId;
			break;
		case SP_ATTACK_TYPE.BURST:
			_attackId = spearInfo.burstSpearInfo.baseAtkId;
			_motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
			break;
		case SP_ATTACK_TYPE.ORACLE:
			_attackId = spearInfo.oracle.comboAttackId;
			_motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
			break;
		}
		return true;
	}

	public bool GetSpActionInfo(SP_ATTACK_TYPE _spAtkType, EXTRA_ATTACK_TYPE _exAtkType, ref int _attackId, ref string _motionLayerName)
	{
		if (owner == null || spearInfo == null)
		{
			return false;
		}
		switch (_spAtkType)
		{
		case SP_ATTACK_TYPE.NONE:
			_attackId = spearInfo.rushLoopAttackID;
			break;
		case SP_ATTACK_TYPE.HEAT:
			_attackId = spearInfo.Heat_SpAttackId;
			break;
		case SP_ATTACK_TYPE.SOUL:
			_attackId = spearInfo.Soul_SpAttackId;
			break;
		case SP_ATTACK_TYPE.BURST:
			_attackId = spearInfo.burstSpearInfo.spAtkId;
			_motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
			break;
		case SP_ATTACK_TYPE.ORACLE:
			_attackId = spearInfo.oracle.guardAttackId;
			_motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
			break;
		}
		return true;
	}

	public float GetWalkSpeedUp(SP_ATTACK_TYPE _spAtkType)
	{
		switch (_spAtkType)
		{
		case SP_ATTACK_TYPE.NONE:
			return 0f;
		case SP_ATTACK_TYPE.HEAT:
			return spearInfo.heatWalkSpeed;
		case SP_ATTACK_TYPE.SOUL:
			return spearInfo.Soul_WalkSpeedUpRate;
		case SP_ATTACK_TYPE.BURST:
			return 0f;
		default:
			return 0f;
		}
	}

	public float GetAvoidUp(SP_ATTACK_TYPE _spAtkType)
	{
		if (_spAtkType != SP_ATTACK_TYPE.HEAT && _spAtkType == SP_ATTACK_TYPE.BURST)
		{
			return spearInfo.burstSpearInfo.avoidSpeedUpRate;
		}
		return 0f;
	}

	public bool TryOverrideHitEffect(ref EnemyHitTypeTable.TypeData retTypeData, ref Vector3 scale)
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		if (spearInfo.burstSpearInfo.spinElementHitEffectNames.IsNullOrEmpty())
		{
			return false;
		}
		if (!IsBurstSpin())
		{
			return false;
		}
		retTypeData = new EnemyHitTypeTable.TypeData();
		int i = 0;
		for (int num = spearInfo.burstSpearInfo.spinElementHitEffectNames.Length; i < num; i++)
		{
			retTypeData.elementEffectNames[i] = spearInfo.burstSpearInfo.spinElementHitEffectNames[i];
		}
		scale = spearInfo.burstSpearInfo.spinElementHitEffectScale;
		return true;
	}

	public void ActAttackBurstCombo()
	{
		if (isCtrlActive && owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			int hitComboAttackId = spearInfo.burstSpearInfo.hitComboAttackId;
			string motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.BURST, hitComboAttackId);
			owner.ActAttack(hitComboAttackId, send_packet: true, sync_immediately: false, motionLayerName);
		}
	}

	public void OnWeaponActionStart()
	{
		if (!isCtrlActive || !owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return;
		}
		enableSpin = true;
		if (!(spinEffectTrans == null))
		{
			return;
		}
		spinEffectTrans = EffectManager.GetEffect(spinEffectName, wepNode);
		if (spinEffectTrans != null)
		{
			spinEffectCtrl = spinEffectTrans.GetComponent<EffectCtrl>();
			if (spinEffectCtrl != null)
			{
				spinEffectCtrl.Play(EFFECT_STATE_NAME_HASH_LOOP_1);
			}
		}
		if (spearInfo.burstSpearInfo.spinSeId > 0 && !isPlayingSpinSE)
		{
			SoundManager.PlayLoopSE(spearInfo.burstSpearInfo.spinSeId, owner);
			isPlayingSpinSE = true;
		}
	}

	public void OnWeaponActionEnd()
	{
		if (isCtrlActive)
		{
			if (spinNode != null)
			{
				spinNode.localRotation = InGameUtility.QUATERNION_IDENTITY;
			}
			enableSpin = false;
			spinTimer = 0f;
			ClearSpinEffect();
			ClearSpinSE();
		}
	}

	public bool CheckConditionTrigger()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		if (!enableSpin)
		{
			return false;
		}
		if ((owner.IsOriginal() || owner.IsCoopNone()) && owner.attackHitCount <= 0)
		{
			return false;
		}
		return true;
	}

	public bool CheckConditionTrigger2()
	{
		if (!isCtrlActive)
		{
			return false;
		}
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		if (!enableSpin)
		{
			return true;
		}
		return false;
	}

	public void OnFixPositionWeaponR_ON(Vector3 pos)
	{
		if (isCtrlActive && owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
		{
			Vector3 vector = new Vector3(pos.x, 0f, pos.z);
			EffectManager.OneShot(throwGroundEffectName, vector, Quaternion.identity);
			if (enableSpin)
			{
				spinGroundEffectTrans = EffectManager.GetEffect(spinThrowGroundEffectName);
				spinGroundEffectTrans.position = vector;
			}
		}
	}

	public void OnFixPositionWeaponR_OFF()
	{
		ClearSpinGroundEffect();
	}

	public bool IsBarrierBulletDelete()
	{
		return isBarrierBulletDelete;
	}

	public void EnableBarrierBulletDelete()
	{
		isBarrierBulletDelete = true;
	}

	public void DisableBarrierBulletDelete()
	{
		isBarrierBulletDelete = false;
	}

	public static bool IsOracleAttackId(int attackId)
	{
		InGameSettingsManager.Player.SpearActionInfo.Oracle oracle = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.oracle;
		if (attackId >= oracle.comboAttackId)
		{
			return attackId <= oracle.reservedAttackId;
		}
		return false;
	}

	public bool CanConsumeOracleStock(int count = 0)
	{
		if (StockedCount <= 0 || StockedCount < count)
		{
			return false;
		}
		return true;
	}

	public void ConsumeOracleStock(int count = 0)
	{
		if (CanConsumeOracleStock(count) && !LotFreeToUseStock())
		{
			if (count > 0)
			{
				stockedCounts[owner.weaponIndex] = Mathf.Max(0, stockedCounts[owner.weaponIndex] - count);
			}
			else
			{
				stockedCounts[owner.weaponIndex] = 0;
			}
		}
	}

	private bool LotFreeToUseStock()
	{
		float freeStockProbability = owner.buffParam.passive.freeStockProbability;
		if (freeStockProbability > 0f)
		{
			return UnityEngine.Random.Range(0f, 1f) <= freeStockProbability;
		}
		return false;
	}

	public void EventShotOracleSp(AnimEventData.EventData data)
	{
		if (oracleSpBullet != null)
		{
			DestroyOracleSp();
		}
		AttackInfo attackInfo = owner.FindAttackInfo(data.stringArgs[0]);
		if (attackInfo != null)
		{
			oracleSpBullet = AnimEventShot.Create(owner, data, attackInfo, Vector3.zero);
			if (oracleSpBullet != null)
			{
				oracleSpController = oracleSpBullet.GetComponent<BulletControllerOracleSpearSp>();
			}
		}
	}

	public void DestroyOracleSp()
	{
		if (oracleSpBullet != null)
		{
			oracleSpBullet.OnDestroy();
			oracleSpBullet = null;
			oracleSpController = null;
		}
	}

	public void UpdateOracleSp()
	{
		if (oracleSpController != null && owner.GetChargingRate() >= 1f && !oracleSpController.Charged && (owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS) || CanConsumeOracleStock(1)))
		{
			if (!owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
			{
				ConsumeOracleStock(1);
			}
			oracleSpController.UpdateChargedEffect();
		}
	}

	public void GuardOn(bool isPlayEnd = false)
	{
		if (!Guarding)
		{
			Guarding = true;
			owner._StartGuard();
		}
		if (oracleGuardEffect != null)
		{
			EffectManager.ReleaseEffect(oracleGuardEffect.gameObject, isPlayEnd, !isPlayEnd);
			oracleGuardEffect = null;
		}
		oracleGuardEffect = EffectManager.GetEffect("ef_btl_wsk4_spear_guard", owner._transform);
	}

	public void GuardOff()
	{
		Guarding = false;
		owner._EndGuard();
		if (oracleGuardEffect != null)
		{
			Animator component = oracleGuardEffect.GetComponent<Animator>();
			if (component != null)
			{
				component.Play("END_cancel");
			}
		}
	}

	public void GuardJust()
	{
		if (oracleGuardEffect != null)
		{
			Animator component = oracleGuardEffect.GetComponent<Animator>();
			if (component != null)
			{
				component.Play("END_just");
				SoundManager.PlayOneShotSE(10000042, owner._position);
			}
		}
	}

	public void StartOracleGutsMode()
	{
		if (!owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS) && CanConsumeOracleStock())
		{
			consumedStockCounts = StockedCount;
			ConsumeOracleStock();
			owner.FinishBoostMode();
			owner.DoHealType(HEAL_TYPE.ALL_BADSTATUS);
			SoundManager.PlayOneShotSE(spearInfo.oracle.gutsSE, owner._position);
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS;
			buffData.time = spearInfo.oracle.gutsBaseTime;
			buffData.time += spearInfo.oracle.gutsTimePerStock * (float)consumedStockCounts;
			buffData.time *= 1f + owner.buffParam.passive.gutsTimeRateUp;
			buffData.value = 999;
			owner.OnBuffStart(buffData);
		}
	}

	public float GetOracleElementDamageRate(AttackHitInfo info)
	{
		float num = 1f;
		if (info.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP)
		{
			num = ((!OracleSpCharged) ? (num * spearInfo.oracle.spElementDamageRate) : (num * spearInfo.oracle.chargedSpElementDamageRate));
		}
		else if (info.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP_CHARGED)
		{
			num *= spearInfo.oracle.chargedSpElementDamageRate;
		}
		else
		{
			num = Mathf.Lerp(num, spearInfo.oracle.elementDamageRateFullStocked, StockedRate);
			if (owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
			{
				num *= spearInfo.oracle.elementDamageRateWhileGuts;
			}
		}
		return num;
	}

	public float GetOracleAttackSpeedRate()
	{
		float num = Mathf.Lerp(1f, spearInfo.oracle.attackSpeedRateFullStocked, StockedRate);
		if (owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
		{
			num *= spearInfo.oracle.attackSpeedRateWhileGuts;
		}
		return num;
	}

	public float GetOracleSpChargeTimeRate(float rate)
	{
		return Mathf.Lerp(rate, rate * spearInfo.oracle.spChargeTimeRateFullStocked, StockedRate);
	}

	private void UpdateOracleStock()
	{
		if (owner.IsSpActionGaugeFullCharged() && stockedCounts[owner.weaponIndex] < MaxStockCount)
		{
			OnUpdateOracleStock();
		}
	}

	public void OnUpdateOracleStock()
	{
		stockedCounts[owner.weaponIndex]++;
		spActionGauge = 0f;
		SoundManager.PlayOneShotUISE(40000359);
		Transform effect = EffectManager.GetEffect("ef_btl_wsk4_spear_stock", owner._transform);
		if (effect != null)
		{
			EffectManager.ReleaseEffect(effect.gameObject);
		}
		if (owner.IsCoopNone() || owner.IsOriginal())
		{
			Self self = owner as Self;
			if (self != null && MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				self.taskChecker.OnOracleSpear();
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnOracleSpear();
			}
		}
		if (owner.playerSender != null)
		{
			owner.playerSender.OnUpdateOracleSpearStock();
		}
	}
}
