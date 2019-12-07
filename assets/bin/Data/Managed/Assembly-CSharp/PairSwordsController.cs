using System.Collections.Generic;
using UnityEngine;

public class PairSwordsController : IObserver, IWeaponController
{
	public enum CHARGE_STATE
	{
		NONE,
		LOOP,
		LASER_SHOT,
		LASER_LOOP,
		END
	}

	private static readonly Vector3 OFFSET_LASER_WAIT_EFFECT = new Vector3(0f, 1f, 1f);

	private static readonly int HASH_EFFECT_ON_WEAPON_FULL = Animator.StringToHash("Base Layer.LOOP1");

	private static readonly int HASH_EFFECT_ON_WEAPON_DEFAULT = Animator.StringToHash("Base Layer.LOOP2");

	private bool isCtrlActive;

	private CHARGE_STATE chargeState;

	private InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsInfo;

	private Player owner;

	private float timerForSpActionGaugeDecreaseAfterHit;

	private bool isExecLaserEnd;

	private bool isEventShotLaserExec;

	private bool isSetGaugePercentForLaser;

	private float gaugePercentForLaser;

	private int comboLvBySync;

	private List<AnimEventShot> bulletLaserList = new List<AnimEventShot>(3);

	private List<Transform> effectTransOnWeaponList = new List<Transform>(2);

	private List<Animator> effectAnimatorOnWeaponList = new List<Animator>(2);

	private Transform effectTransStartShotLaser;

	private bool hasJustAvoid;

	public Transform oracleSpEffect;

	public Transform oracleRushLoopEffect;

	public Transform oracleRushEffect;

	private const int kBurstNormalAttackId = 30;

	private const int kBurstAerialComboAttackId = 32;

	private const int kBurstCombineAttackId = 33;

	private const int kBurstCombineId = 34;

	private const int kBurstAerialSpecialId = 35;

	private const int kBurstGroundSpecialStartId = 36;

	private const int kBurstGroundSpecialEndId = 39;

	protected bool isCombineMode;

	protected Quaternion combineEffectRotation = Quaternion.identity;

	protected Vector3 combineEffectScale = new Vector3(1.5f, 1.5f, 1.5f);

	public const int kOracleNormalAttackId = 40;

	public const int kOracleNormal2AttackId = 41;

	public const int kOracleRushStartAttackId = 42;

	public const int kOracleRushAttackId = 43;

	public const int kOracleSpAttackId = 44;

	public const int kOracleAvoidAttackId = 45;

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

	private float spActionGaugeMax
	{
		get
		{
			if (owner == null)
			{
				return 0f;
			}
			return owner.spActionGaugeMax[owner.weaponIndex];
		}
		set
		{
			if (!(owner == null))
			{
				owner.spActionGaugeMax[owner.weaponIndex] = value;
			}
		}
	}

	private void SetChargeState(CHARGE_STATE chargeState)
	{
		this.chargeState = chargeState;
	}

	private void ResetTimerForSpActionGaugeDecreaseAfterHit()
	{
		timerForSpActionGaugeDecreaseAfterHit = 0f;
	}

	public void SetEventShotLaserExec()
	{
		isEventShotLaserExec = true;
	}

	public void SetGaugePercentForLaser()
	{
		if (!isSetGaugePercentForLaser)
		{
			gaugePercentForLaser = GetGaugeChargedPercent();
			isSetGaugePercentForLaser = true;
		}
	}

	public void AddBulletLaser(AnimEventShot bullet)
	{
		bulletLaserList.Add(bullet);
	}

	public void GetEffectTransStartShotLaser()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && effectTransStartShotLaser == null)
		{
			effectTransStartShotLaser = EffectManager.GetEffect(pairSwordsInfo.Soul_EffectForWaitingLaser, owner._transform);
			effectTransStartShotLaser.localPosition = OFFSET_LASER_WAIT_EFFECT;
		}
	}

	public void Init(Player player)
	{
		owner = player;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			pairSwordsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
		}
	}

	public void OnLoadComplete()
	{
		if (!owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			isCtrlActive = false;
			return;
		}
		isCtrlActive = true;
		effectTransOnWeaponList.Clear();
		effectAnimatorOnWeaponList.Clear();
		int currentWeaponElement = owner.GetCurrentWeaponElement();
		string name = pairSwordsInfo.Soul_EffectsForWeapon[currentWeaponElement];
		Transform transform = Utility.Find(owner.FindNode("R_Wep"), name);
		Transform transform2 = Utility.Find(owner.FindNode("L_Wep"), name);
		effectTransOnWeaponList.Add(transform);
		effectTransOnWeaponList.Add(transform2);
		effectAnimatorOnWeaponList.Add(transform.GetComponent<Animator>());
		effectAnimatorOnWeaponList.Add(transform2.GetComponent<Animator>());
	}

	public void Update()
	{
		switch (chargeState)
		{
		case CHARGE_STATE.LOOP:
			if (!owner.IsCoopNone() && !owner.IsOriginal() && isExecLaserEnd)
			{
				SetChargeState(CHARGE_STATE.END);
			}
			break;
		case CHARGE_STATE.LASER_SHOT:
			if (isEventShotLaserExec)
			{
				SetChargeState(CHARGE_STATE.LASER_LOOP);
			}
			break;
		case CHARGE_STATE.LASER_LOOP:
			if (spActionGauge <= 0f)
			{
				owner.SetNextTrigger();
				SetChargeState(CHARGE_STATE.END);
			}
			break;
		case CHARGE_STATE.END:
			OnLaserEnd();
			isExecLaserEnd = false;
			SetChargeState(CHARGE_STATE.NONE);
			break;
		}
		timerForSpActionGaugeDecreaseAfterHit += Time.deltaTime;
		UpdateSpActionGauge();
		UpdateEffectOnWeapon();
	}

	private void UpdateSpActionGauge()
	{
		if (owner == null || !owner.CheckAttackMode(Player.ATTACK_MODE.PAIR_SWORDS))
		{
			return;
		}
		switch (owner.spAttackType)
		{
		case SP_ATTACK_TYPE.HEAT:
		{
			if (!owner.isBoostMode)
			{
				return;
			}
			float num2 = 1f + owner.GetSpGaugeDecreasingRate();
			owner.spActionGauge[owner.weaponIndex] -= pairSwordsInfo.boostGaugeDecreasePerSecond * num2 * Time.deltaTime;
			break;
		}
		case SP_ATTACK_TYPE.SOUL:
			if ((chargeState == CHARGE_STATE.LASER_SHOT || chargeState == CHARGE_STATE.LASER_LOOP) && isEventShotLaserExec)
			{
				spActionGauge -= pairSwordsInfo.Soul_GaugeDecreaseShootingLaserPerSecond * Time.deltaTime;
			}
			else if (timerForSpActionGaugeDecreaseAfterHit >= pairSwordsInfo.Soul_TimeForGaugeDecreaseAfterHit && !owner.IsStone() && (!IsComboLvMax() || !(timerForSpActionGaugeDecreaseAfterHit < pairSwordsInfo.Soul_TimeForGaugeDecreaseAfterHitOnComboLvMax)))
			{
				if (chargeState == CHARGE_STATE.LOOP || (chargeState == CHARGE_STATE.LASER_SHOT && !isEventShotLaserExec))
				{
					spActionGauge -= pairSwordsInfo.Soul_GaugeDecreaseWaitingLaserPerSecond * Time.deltaTime;
				}
				else
				{
					spActionGauge -= pairSwordsInfo.Soul_GaugeDecreasePerSecond * Time.deltaTime;
				}
			}
			break;
		case SP_ATTACK_TYPE.BURST:
		{
			if (!owner.isBoostMode)
			{
				return;
			}
			float num = 1f + owner.GetSpGaugeDecreasingRate();
			owner.spActionGauge[owner.weaponIndex] -= pairSwordsInfo.Burst_BoostGaugeDecreasePerSecond * num * Time.deltaTime;
			break;
		}
		case SP_ATTACK_TYPE.ORACLE:
			if (owner.isDead || owner.enabledRushAvoid || owner.actionID == (Character.ACTION_ID)49)
			{
				break;
			}
			if (owner.enableInputCharge)
			{
				spActionGauge -= pairSwordsInfo.Oracle_SpGaugeDecreasePerSecond * (1f + owner.GetSpGaugeDecreasingRate()) * Time.deltaTime;
				if (spActionGauge <= 0f)
				{
					owner.SetChargeRelease(1f);
				}
			}
			else
			{
				spActionGauge += pairSwordsInfo.Oracle_SpGaugeIncreasePerSecond * (1f + owner.GetSpGaugeDecreasingRate()) * Time.deltaTime;
			}
			break;
		}
		spActionGauge = Mathf.Max(0f, Mathf.Min(spActionGaugeMax, spActionGauge));
	}

	private void UpdateEffectOnWeapon()
	{
		if (effectAnimatorOnWeaponList.IsNullOrEmpty())
		{
			return;
		}
		for (int i = 0; i < effectAnimatorOnWeaponList.Count; i++)
		{
			if (effectAnimatorOnWeaponList[i] == null)
			{
				continue;
			}
			int fullPathHash = effectAnimatorOnWeaponList[i].GetCurrentAnimatorStateInfo(0).fullPathHash;
			if (IsComboLvMax())
			{
				if (fullPathHash != HASH_EFFECT_ON_WEAPON_FULL)
				{
					effectAnimatorOnWeaponList[i].Play(HASH_EFFECT_ON_WEAPON_FULL);
				}
				continue;
			}
			if (fullPathHash != HASH_EFFECT_ON_WEAPON_DEFAULT)
			{
				effectAnimatorOnWeaponList[i].Play(HASH_EFFECT_ON_WEAPON_DEFAULT);
			}
			Vector3 localScale = effectTransOnWeaponList[i].localScale;
			localScale.z = GetGaugeRate();
			effectTransOnWeaponList[i].localScale = localScale;
		}
	}

	public void OnStartCharge()
	{
		SetChargeState(CHARGE_STATE.LOOP);
	}

	public void OnLaserEnd(bool isPacket = false)
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && effectTransStartShotLaser != null)
		{
			EffectManager.ReleaseEffect(effectTransStartShotLaser.gameObject);
			effectTransStartShotLaser = null;
		}
		ClearLaserBullet();
		if (pairSwordsInfo.Soul_SeIds.Length >= 2)
		{
			SoundManager.StopLoopSE(pairSwordsInfo.Soul_SeIds[1], owner);
		}
		if (pairSwordsInfo.Soul_SeIds.Length >= 3 && pairSwordsInfo.Soul_SeIds[2] > 0)
		{
			SoundManager.PlayOneShotSE(pairSwordsInfo.Soul_SeIds[2], owner._position);
		}
		isEventShotLaserExec = false;
		isSetGaugePercentForLaser = false;
		gaugePercentForLaser = 0f;
		owner.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END);
		if (owner.playerSender != null)
		{
			owner.playerSender.OnPairSwordsLaserEnd();
		}
		if (isPacket)
		{
			owner.SetNextTrigger();
			isExecLaserEnd = true;
		}
	}

	private void ClearLaserBullet()
	{
		if (!bulletLaserList.IsNullOrEmpty())
		{
			for (int i = 0; i < bulletLaserList.Count; i++)
			{
				bulletLaserList[i].OnDestroy();
			}
			bulletLaserList.Clear();
		}
	}

	public void DecreaseSoulGaugeByDamage()
	{
		if (owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && !owner.IsInBarrier() && !owner.IsStone())
		{
			spActionGauge -= pairSwordsInfo.Soul_GaugeDecreaseByDamage;
			if (spActionGauge < 0f)
			{
				spActionGauge = 0f;
			}
		}
	}

	private float GetGaugeChargedPercent()
	{
		if (spActionGaugeMax <= 0f)
		{
			return 0f;
		}
		return Mathf.Clamp01(spActionGauge / spActionGaugeMax) * 100f;
	}

	private float GetGaugeRate()
	{
		if (spActionGaugeMax <= 0f)
		{
			return 0f;
		}
		return Mathf.Clamp01(spActionGauge / spActionGaugeMax);
	}

	public int GetComboLv()
	{
		float gaugeChargedPercent = GetGaugeChargedPercent();
		int result = 1;
		for (int i = 0; i < pairSwordsInfo.Soul_GaugePercentForComboLv.Length; i++)
		{
			if (gaugeChargedPercent >= pairSwordsInfo.Soul_GaugePercentForComboLv[i])
			{
				result = 1 + i;
			}
		}
		if (comboLvBySync > 0)
		{
			result = comboLvBySync;
			comboLvBySync = 0;
		}
		return result;
	}

	public void SetComboLv(int lv)
	{
		comboLvBySync = lv;
	}

	public float GetAttackSpeedUpRate()
	{
		switch (owner.spAttackType)
		{
		case SP_ATTACK_TYPE.HEAT:
			return GetHeatAttackSpeedUpRate();
		case SP_ATTACK_TYPE.SOUL:
			return GetSoulAttackSpeedUpRate();
		case SP_ATTACK_TYPE.BURST:
			return GetBurstAttackSpeedUpRate();
		default:
			return 0f;
		}
	}

	private float GetHeatAttackSpeedUpRate()
	{
		if (!owner.isBoostMode)
		{
			return 0f;
		}
		if (owner.attackID == 98)
		{
			return 0f;
		}
		return pairSwordsInfo.boostAttackAndMoveSpeedUpRate;
	}

	private float GetSoulAttackSpeedUpRate()
	{
		if (!owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			return 0f;
		}
		if (pairSwordsInfo.Soul_GaugePercentForComboLv.Length < pairSwordsInfo.Soul_NumOfComboLv)
		{
			return 0f;
		}
		if (pairSwordsInfo.Soul_AttackSpeedUpRatesByComboLv.Length < pairSwordsInfo.Soul_NumOfComboLv)
		{
			return 0f;
		}
		float gaugeChargedPercent = GetGaugeChargedPercent();
		float result = pairSwordsInfo.Soul_AttackSpeedUpRatesByComboLv[0];
		for (int i = 0; i < pairSwordsInfo.Soul_GaugePercentForComboLv.Length; i++)
		{
			if (gaugeChargedPercent >= pairSwordsInfo.Soul_GaugePercentForComboLv[i])
			{
				result = pairSwordsInfo.Soul_AttackSpeedUpRatesByComboLv[i];
			}
		}
		return result;
	}

	private float GetBurstAttackSpeedUpRate()
	{
		if (!owner.isBoostMode || !IsBurstGroundSpecialAttack())
		{
			return 0f;
		}
		return pairSwordsInfo.Burst_BoostAttackSpeedUpRate;
	}

	public bool GetElementDamageUpRate(ref float rate)
	{
		bool result = false;
		rate = 1f;
		if (owner.attackMode != Player.ATTACK_MODE.PAIR_SWORDS)
		{
			return result;
		}
		if (owner.spAttackType != SP_ATTACK_TYPE.BURST)
		{
			return result;
		}
		if (isCombineMode)
		{
			rate += pairSwordsInfo.Burst_CombineElementDamageUpRate;
			result = true;
		}
		if (owner.isBoostMode && IsBurstGroundSpecialAttack())
		{
			rate += pairSwordsInfo.Burst_BoostGroundSpElementDamageUpRate;
			result = true;
		}
		return result;
	}

	public float GetAtkRate()
	{
		if (!owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			return 1f;
		}
		if (pairSwordsInfo.Soul_NumOfComboLv <= 0)
		{
			return 1f;
		}
		if (pairSwordsInfo.Soul_GaugePercentForComboLv.Length < pairSwordsInfo.Soul_NumOfComboLv)
		{
			return 1f;
		}
		float gaugeChargedPercent = GetGaugeChargedPercent();
		float num = 1f;
		if (owner.attackID == pairSwordsInfo.Soul_SpLaserShotAttackId)
		{
			if (pairSwordsInfo.Soul_AtkRatesForLaserByComboLv.Length < pairSwordsInfo.Soul_NumOfComboLv)
			{
				return 1f;
			}
			if (isSetGaugePercentForLaser)
			{
				gaugeChargedPercent = gaugePercentForLaser;
			}
			else
			{
				gaugePercentForLaser = gaugeChargedPercent;
				isSetGaugePercentForLaser = true;
			}
			num = pairSwordsInfo.Soul_AtkRatesForLaserByComboLv[0];
			for (int i = 0; i < pairSwordsInfo.Soul_GaugePercentForComboLv.Length; i++)
			{
				if (gaugeChargedPercent >= pairSwordsInfo.Soul_GaugePercentForComboLv[i])
				{
					num = pairSwordsInfo.Soul_AtkRatesForLaserByComboLv[i];
				}
			}
			return num;
		}
		if (pairSwordsInfo.Soul_AtkRatesForBulletByComboLv.Length < pairSwordsInfo.Soul_NumOfComboLv)
		{
			return 1f;
		}
		num = pairSwordsInfo.Soul_AtkRatesForBulletByComboLv[0];
		for (int j = 0; j < pairSwordsInfo.Soul_GaugePercentForComboLv.Length; j++)
		{
			if (gaugeChargedPercent >= pairSwordsInfo.Soul_GaugePercentForComboLv[j])
			{
				num = pairSwordsInfo.Soul_AtkRatesForBulletByComboLv[j];
			}
		}
		return num;
	}

	private bool IsAbleToShotSoulLaser()
	{
		if (chargeState != CHARGE_STATE.LOOP)
		{
			return false;
		}
		return true;
	}

	public bool IsAbleToAlterSpAction()
	{
		if (owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
		{
			if (owner.isBoostMode)
			{
				return false;
			}
			if (!owner.IsSpActionGaugeHalfCharged())
			{
				return false;
			}
		}
		return true;
	}

	public bool IsComboLvMax()
	{
		if (GetComboLv() == pairSwordsInfo.Soul_NumOfComboLv)
		{
			return true;
		}
		return false;
	}

	public bool CheckContinueBoostMode()
	{
		switch (owner.spAttackType)
		{
		case SP_ATTACK_TYPE.HEAT:
			if (spActionGauge > 0f)
			{
				return true;
			}
			break;
		case SP_ATTACK_TYPE.BURST:
			if (!owner.IsCoopNone() && !owner.IsOriginal())
			{
				return true;
			}
			if (spActionGauge > 0f)
			{
				return true;
			}
			break;
		case SP_ATTACK_TYPE.SOUL:
			if (!owner.IsCoopNone() && !owner.IsOriginal())
			{
				return true;
			}
			if (IsComboLvMax())
			{
				return true;
			}
			break;
		}
		return false;
	}

	public void OnHit()
	{
		ResetTimerForSpActionGaugeDecreaseAfterHit();
	}

	public void OnEndAction()
	{
		hasJustAvoid = false;
		EndOracleSp();
		if (oracleRushLoopEffect != null)
		{
			if (owner is Self)
			{
				EffectManager.ReleaseEffect(oracleRushLoopEffect.gameObject);
			}
			else
			{
				EffectManager.ReleaseEffect(oracleRushLoopEffect.gameObject, isPlayEndAnimation: false, immediate: true);
			}
			oracleRushLoopEffect = null;
		}
		if (oracleRushEffect != null)
		{
			if (owner is Self)
			{
				EffectManager.ReleaseEffect(oracleRushEffect.gameObject);
			}
			else
			{
				EffectManager.ReleaseEffect(oracleRushEffect.gameObject, isPlayEndAnimation: false, immediate: true);
			}
			oracleRushEffect = null;
		}
	}

	public void OnActDead()
	{
		OnReaction();
	}

	public void OnActReaction()
	{
	}

	public void OnActAvoid()
	{
		OnAvoid();
	}

	public void OnActSkillAction()
	{
	}

	public void OnRelease()
	{
		if (isCtrlActive && !owner.isDead && (IsAbleToShotSoulLaser() || (owner.attackID == pairSwordsInfo.Soul_SpLaserWaitAttackId && chargeState == CHARGE_STATE.NONE)))
		{
			owner.ActAttack(pairSwordsInfo.Soul_SpLaserShotAttackId, send_packet: true, sync_immediately: true);
			SetChargeState(CHARGE_STATE.LASER_SHOT);
			if (owner.playerSender != null)
			{
				owner.playerSender.OnSyncSpActionGauge();
			}
		}
	}

	public void OnAvoid()
	{
		if (chargeState != 0)
		{
			SetChargeState(CHARGE_STATE.END);
		}
	}

	public void OnReaction()
	{
		if (chargeState != 0)
		{
			SetChargeState(CHARGE_STATE.END);
		}
	}

	public void OnActAttack(int id)
	{
	}

	public void OnBuffStart(BuffParam.BuffData data)
	{
	}

	public void OnBuffEnd(BuffParam.BUFFTYPE type)
	{
	}

	public void OnChangeWeapon()
	{
	}

	public void OnAttackedHitFix(AttackedHitStatusFix status)
	{
	}

	public float GetWalkSpeedUp(SP_ATTACK_TYPE _spAtkType)
	{
		switch (_spAtkType)
		{
		case SP_ATTACK_TYPE.HEAT:
			if (owner.isBoostMode)
			{
				return pairSwordsInfo.boostAttackAndMoveSpeedUpRate;
			}
			break;
		case SP_ATTACK_TYPE.BURST:
			return pairSwordsInfo.Burst_MoveSpeedUpRate;
		}
		return 0f;
	}

	public bool IsUpdateAerialCollider()
	{
		if (pairSwordsInfo == null)
		{
			return false;
		}
		return pairSwordsInfo.Burst_IsUpdateAerialCollider;
	}

	public void ResetCombineMode()
	{
		isCombineMode = false;
	}

	public bool IsCombineMode()
	{
		return isCombineMode;
	}

	public bool IsBurstGroundSpecialAttack()
	{
		if (owner.attackID >= 36)
		{
			return owner.attackID <= 39;
		}
		return false;
	}

	public int GetBurstAttackId()
	{
		if (!isCombineMode)
		{
			return 30;
		}
		return 33;
	}

	public bool IsOverrideHitEffect(ref EnemyHitTypeTable.TypeData _type, ref Vector3 _scale)
	{
		if (!owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST))
		{
			return false;
		}
		if (!isCombineMode)
		{
			return false;
		}
		if (pairSwordsInfo.Burst_CombineHitEffect.IsNullOrEmpty())
		{
			return false;
		}
		_type = new EnemyHitTypeTable.TypeData();
		for (int i = 0; i < pairSwordsInfo.Burst_CombineHitEffect.Length; i++)
		{
			_type.elementEffectNames[i] = pairSwordsInfo.Burst_CombineHitEffect[i];
		}
		_scale = pairSwordsInfo.Burst_CombineHitEffectScale;
		return true;
	}

	public bool ActBurstSpecialAction(ref bool effectFlag)
	{
		if (owner.attackID == 32)
		{
			owner.ActAttack(35, send_packet: true, sync_immediately: true);
			effectFlag = false;
			return true;
		}
		if (!isCombineMode)
		{
			owner.ActAttack(34);
			effectFlag = true;
			return true;
		}
		if (owner.actionID != Character.ACTION_ID.ATTACK || owner.attackID == 21 || owner.attackID == 34 || owner.attackID == 35)
		{
			owner.ActAttack(36, send_packet: true, sync_immediately: false, "", (owner.actionID != Character.ACTION_ID.ATTACK) ? "attack_36_start" : "");
			effectFlag = false;
			return true;
		}
		return false;
	}

	public void CombineBurst(bool isCombine)
	{
		if (isCombineMode != isCombine && owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST))
		{
			isCombineMode = isCombine;
			if (isCombineMode)
			{
				owner.loader.CombineBurstPairSword(isCombine: true, pairSwordsInfo.Burst_CombinePosition, Quaternion.Euler(pairSwordsInfo.Burst_CombineEuler));
				owner.CheckBurstPairSwordBoost();
			}
			else
			{
				owner.loader.CombineBurstPairSword(isCombine: false, Vector3.zero, Quaternion.identity);
			}
			SoundManager.PlayOneShotSE(10000042, owner._position);
			if (owner.loader != null && owner.loader.wepR != null)
			{
				EffectManager.OneShot("ef_btl_wsk3_twinsword_01_00", owner.loader.wepR.position, combineEffectRotation, combineEffectScale);
			}
			if (owner.playerSender != null)
			{
				owner.playerSender.OnSyncCombine(isCombineMode);
			}
		}
	}

	public void ActAerialAvoid(Vector3 inputVec)
	{
		Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
		Vector3 right = cameraTransform.right;
		Vector3 forward = cameraTransform.forward;
		forward.y = 0f;
		forward.Normalize();
		Vector3 worldPosition = owner._transform.position + (right * inputVec.x + forward * inputVec.y);
		owner._transform.LookAt(worldPosition);
	}

	public bool IsOracleAttackId(int id)
	{
		if (40 <= id)
		{
			return id <= 45;
		}
		return false;
	}

	public void EventShotOracleRush(AnimEventData.EventData data)
	{
		spActionGauge -= pairSwordsInfo.Oracle_SpGaugeRushDecrease * (1f + owner.GetSpGaugeDecreasingRate());
		if (owner is Self)
		{
			if (oracleRushLoopEffect != null)
			{
				EffectManager.ReleaseEffect(oracleRushLoopEffect.gameObject);
				oracleRushLoopEffect = null;
			}
			oracleRushLoopEffect = EffectManager.GetEffect("ef_btl_wsk4_twinsword_03", owner._transform);
			AttackInfo attackInfo = null;
			if (spActionGauge < 0f)
			{
				spActionGauge = 0f;
				attackInfo = owner.FindAttackInfo(data.stringArgs[0]);
			}
			else
			{
				attackInfo = owner.FindAttackInfo(data.stringArgs[2]);
			}
			if (attackInfo != null)
			{
				AnimEventShot animEventShot = AnimEventShot.Create(owner, data, attackInfo, Vector3.zero);
				oracleRushEffect = animEventShot.bulletEffect;
				if (oracleRushEffect != null)
				{
					oracleRushEffect.transform.localRotation = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
				}
			}
		}
		else
		{
			string effect_name = (!(spActionGauge < 0f)) ? $"ef_btl_wsk4_twinsword_01_{owner.GetCurrentWeaponElement():D2}" : "ef_btl_wsk4_twinsword_01";
			if (oracleRushEffect != null)
			{
				EffectManager.ReleaseEffect(oracleRushEffect.gameObject, isPlayEndAnimation: false, immediate: true);
				oracleRushEffect = null;
			}
			oracleRushEffect = EffectManager.GetEffect(effect_name);
			Vector3 point = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			oracleRushEffect.position = owner._transform.localToWorldMatrix.MultiplyPoint3x4(point);
			oracleRushEffect.rotation = owner._rotation * Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
		}
	}

	public void JustAvoid(bool force = false)
	{
		if (!(!hasJustAvoid | force))
		{
			return;
		}
		EffectManager.OneShot("ef_btl_wsk4_twinsword_04", owner._position, owner._rotation);
		float num = spActionGaugeMax * pairSwordsInfo.Oracle_JustAvoidGaugeIncreaseRate * (1f + owner.buffParam.GetGaugeIncreaseRate(SP_ATTACK_TYPE.ORACLE));
		spActionGauge = Mathf.Max(0f, Mathf.Min(spActionGaugeMax, spActionGauge + num));
		hasJustAvoid = true;
		Enemy enemy = owner.actionTarget as Enemy;
		if (enemy != null)
		{
			owner.SetHitStop(pairSwordsInfo.Oracle_JustAvoidMotionStopTime);
			enemy.SetHitStop(pairSwordsInfo.Oracle_JustAvoidMotionStopTime);
		}
		if (owner.IsCoopNone() || owner.IsOriginal())
		{
			Self self = owner as Self;
			if (self != null && MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				self.taskChecker.OnOraclePairSwords();
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnOraclePairSwords();
			}
		}
	}

	public void StartOracleSp(AnimEventData.EventData data)
	{
		owner.enabledOraclePairSwordsSP = true;
		owner.actionMoveRotateMaxSpeedRate = data.floatArgs[0];
		if (owner.moveRotateMaxSpeed == 0f)
		{
			owner.actionMoveRotateMaxSpeedRate = 1f;
		}
		if (oracleSpEffect == null)
		{
			oracleSpEffect = EffectManager.GetEffect($"ef_btl_wsk4_twinsword_05_{owner.GetCurrentWeaponElement():D2}", owner._transform);
		}
	}

	public void EndOracleSp()
	{
		owner.enabledOraclePairSwordsSP = false;
		owner.actionMoveRotateMaxSpeedRate = 1f;
		if (oracleSpEffect != null)
		{
			if (owner is Self)
			{
				EffectManager.ReleaseEffect(oracleSpEffect.gameObject);
			}
			else
			{
				EffectManager.ReleaseEffect(oracleSpEffect.gameObject, isPlayEndAnimation: false, immediate: true);
			}
			oracleSpEffect = null;
		}
	}
}
