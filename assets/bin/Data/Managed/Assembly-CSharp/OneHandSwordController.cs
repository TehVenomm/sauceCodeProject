using System.Collections.Generic;
using UnityEngine;

public class OneHandSwordController : IWeaponController
{
	private static readonly int ORACLE_ATTACK_ID_START = 23;

	private static readonly int ORACLE_ATTACK_ID_END = 30;

	private InGameSettingsManager.Player.OneHandSwordActionInfo ohsInfo;

	private Player owner;

	private bool isCtrlActive;

	private List<Transform> oracleDragonEffects = new List<Transform>();

	private List<Animator> oracleDragonEffectAnimators = new List<Animator>();

	private Transform oracleProtection;

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

	private SnatchController snatchCtrl => owner.snatchCtrl;

	public void Init(Player player)
	{
		if (!(player == null))
		{
			owner = player;
			ohsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
		}
	}

	public void OnLoadComplete()
	{
		if (!owner.CheckAttackMode(Player.ATTACK_MODE.ONE_HAND_SWORD))
		{
			isCtrlActive = false;
		}
		else
		{
			isCtrlActive = true;
		}
	}

	public void Update()
	{
		UpdateSpActionGauge();
	}

	public void OnEndAction()
	{
	}

	public void OnActDead()
	{
	}

	public void OnActReaction()
	{
	}

	public void OnActAvoid()
	{
	}

	public void OnActSkillAction()
	{
	}

	public void OnRelease()
	{
		if (owner.enableCounterAttack)
		{
			switch (owner.spAttackType)
			{
			case SP_ATTACK_TYPE.SOUL:
			case SP_ATTACK_TYPE.BURST:
				break;
			case SP_ATTACK_TYPE.NONE:
				owner.ActAttack(owner.playerParameter.specialActionInfo.spAttackID, send_packet: true, sync_immediately: true, string.Empty, string.Empty);
				IncrementCounterAttackCount();
				owner.isActOneHandSwordCounter = true;
				break;
			case SP_ATTACK_TYPE.HEAT:
				owner.ActAttack(ohsInfo.heatOHSInfo.counterAttackId, send_packet: true, sync_immediately: true, string.Empty, string.Empty);
				IncrementCounterAttackCount();
				owner.isActOneHandSwordCounter = true;
				break;
			}
		}
	}

	public void OnActAttack(int id)
	{
		if (owner.spAttackType != SP_ATTACK_TYPE.ORACLE)
		{
			return;
		}
		string text = "attack_" + id;
		int num = Animator.StringToHash(text);
		for (int i = 0; i < oracleDragonEffectAnimators.Count; i++)
		{
			Animator val = oracleDragonEffectAnimators[i];
			if (val.HasState(0, num))
			{
				val.Play(text);
			}
		}
	}

	public void OnBuffStart(BuffParam.BuffData data)
	{
		if (data.type == BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION && oracleProtection == null)
		{
			oracleProtection = EffectManager.GetEffect("ef_btl_wsk4_sword_dragon_veil", owner._transform);
		}
	}

	public void OnBuffEnd(BuffParam.BUFFTYPE type)
	{
		if (type == BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION && oracleProtection != null)
		{
			EffectManager.ReleaseEffect(oracleProtection.get_gameObject());
			oracleProtection = null;
		}
	}

	public void OnReloadEffect(BuffParam.BUFFTYPE type)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (type == BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION)
		{
			if (oracleProtection != null)
			{
				EffectManager.ReleaseEffect(oracleProtection.get_gameObject());
			}
			EffectManager.OneShot("ef_btl_wsk4_sword_dragon_veil_re", owner._transform.get_position(), owner._transform.get_rotation(), Vector3.get_one(), is_priority: false, delegate(Transform effect)
			{
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0028: Unknown result type (might be due to invalid IL or missing references)
				effect.set_parent(owner._transform);
				effect.set_localPosition(Vector3.get_zero());
				effect.set_localRotation(Quaternion.get_identity());
				effect.set_localScale(Vector3.get_one());
			});
			oracleProtection = EffectManager.GetEffect("ef_btl_wsk4_sword_dragon_veil", owner._transform);
		}
	}

	public void OnChangeWeapon()
	{
		if (owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION))
		{
			owner.OnBuffEnd(BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION, sync: true);
		}
	}

	public void OnAttackedHitFix(AttackedHitStatusFix status)
	{
	}

	private void UpdateSpActionGauge()
	{
		if (!isCtrlActive || owner == null)
		{
			return;
		}
		float num = 0f;
		switch (owner.spAttackType)
		{
		default:
			return;
		case SP_ATTACK_TYPE.SOUL:
			if (!owner.isBoostMode)
			{
				return;
			}
			num = ((snatchCtrl.state != SnatchController.STATE.SNATCH) ? (ohsInfo.Soul_BoostGaugeDecreasePerSecond * Time.get_deltaTime()) : (ohsInfo.Soul_BoostSnatchGaugeDecreasePerSecond * Time.get_deltaTime()));
			break;
		case SP_ATTACK_TYPE.BURST:
			if (!owner.isBoostMode)
			{
				return;
			}
			num = 1000f / ohsInfo.burstOHSInfo.GaugeTime * Time.get_deltaTime();
			break;
		case SP_ATTACK_TYPE.ORACLE:
			if (!owner.isBoostMode)
			{
				return;
			}
			num = 1000f / ohsInfo.oracleOHSInfo.spGaugeDecreasingValue * Time.get_deltaTime();
			break;
		}
		float num2 = 1f + owner.GetSpGaugeDecreasingRate();
		spActionGauge -= num * num2;
		if (spActionGauge <= 0f)
		{
			spActionGauge = 0f;
		}
	}

	private void IncrementCounterAttackCount()
	{
		if ((owner.IsCoopNone() || owner.IsOriginal()) && owner.buffParam.IncrementCounterConditionAbility())
		{
			EffectManager.GetEffect("ef_btl_ab_charge_01", owner._transform);
		}
	}

	public bool GetNormalAttackId(SP_ATTACK_TYPE _spAtkType, EXTRA_ATTACK_TYPE _exAtkType, ref int _attackId, ref string _motionLayerName)
	{
		if (owner == null || ohsInfo == null)
		{
			return false;
		}
		switch (_spAtkType)
		{
		case SP_ATTACK_TYPE.SOUL:
			_attackId = ohsInfo.soulOHSInfo.BaseAtkId;
			break;
		case SP_ATTACK_TYPE.BURST:
			_attackId = ohsInfo.burstOHSInfo.BaseAtkId;
			_motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.ONE_HAND_SWORD, _spAtkType, _attackId);
			break;
		case SP_ATTACK_TYPE.ORACLE:
			_attackId = ohsInfo.oracleOHSInfo.comboAttackId;
			_motionLayerName = owner.GetMotionLayerName(Player.ATTACK_MODE.ONE_HAND_SWORD, _spAtkType, _attackId);
			break;
		}
		return true;
	}

	public bool CheckActAttackCombo(int attackId)
	{
		if (!owner.CheckSpAttackType(SP_ATTACK_TYPE.HEAT))
		{
			return false;
		}
		if (!owner.isBoostMode)
		{
			return false;
		}
		if (attackId != ohsInfo.heatOHSInfo.revengeStrikeAttackId)
		{
			return false;
		}
		spActionGauge = 0f;
		return true;
	}

	public static bool IsOracleAttackId(int id)
	{
		return id >= ORACLE_ATTACK_ID_START && id <= ORACLE_ATTACK_ID_END;
	}

	public void OnStartOracleBoost()
	{
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.type = BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION;
		int oracleOhsProtectionDoubleProbability = owner.buffParam.GetOracleOhsProtectionDoubleProbability();
		int num = Random.Range(0, 100);
		buffData.value = ((num >= oracleOhsProtectionDoubleProbability) ? 1 : 2);
		buffData.time = -1f;
		owner.OnBuffStart(buffData);
		InGameSettingsManager.Player.OracleOneHandSwordActionInfo.DragonEffect[] dragonEffects = ohsInfo.oracleOHSInfo.dragonEffects;
		for (int i = 0; i < dragonEffects.Length; i++)
		{
			Transform val = owner.FindNode(dragonEffects[i].link);
			if (val == null)
			{
				continue;
			}
			Transform effect = EffectManager.GetEffect(dragonEffects[i].GetEffectName(owner.GetNowWeaponElement()), val);
			if (!(effect == null))
			{
				oracleDragonEffects.Add(effect);
				Animator component = effect.GetComponent<Animator>();
				if (component != null)
				{
					oracleDragonEffectAnimators.Add(component);
				}
			}
		}
	}

	public void OnEndOracleBoost()
	{
		for (int i = 0; i < oracleDragonEffects.Count; i++)
		{
			EffectManager.ReleaseEffect(oracleDragonEffects[i].get_gameObject());
		}
		oracleDragonEffects.Clear();
		oracleDragonEffectAnimators.Clear();
	}
}
