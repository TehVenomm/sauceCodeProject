using UnityEngine;

public class TwoHandSwordController : IObserver, IWeaponController
{
	public class InitParam
	{
		public Player Owner;

		public TwoHandSwordBurstController.InitParam BurstInitParam;
	}

	public const int TWOHANDSWORD_AVOID_ATTACKID = 20;

	public const float THS_HEAT_GAUGE_MAX = 999f;

	public const float THS_HEAT_GAUGE_UNIT = 333f;

	public const int TWOHANDSWORD_NORMAL_SP_ATTACK_ID = 97;

	public const int TWOHANDSWORD_RUSHATTACK_ID = 98;

	public const int TWOHANDSWORD_HEAT_COMBO1_ID = 89;

	public const int TWOHANDSWORD_HEAT_COMBO2_ID = 88;

	private const int INVALID_CTRL_INDEX = -1;

	public const int MAX_BURST_BULLET_COUNT = 6;

	private Player m_owner;

	private InGameSettingsManager.Player.TwoHandSwordActionInfo m_actionInfo;

	private IWeaponController[] m_thsCtrls = new IWeaponController[4];

	private TwoHandSwordBurstController m_thsNormalCtrl;

	private TwoHandSwordBurstController m_thsHeatCtrl;

	private TwoHandSwordSoulController m_thsSoulCtrl;

	private TwoHandSwordBurstController m_thsBurstCtrl;

	public float TwoHandSwordBoostAttackSpeed => (m_thsSoulCtrl != null) ? m_thsSoulCtrl.TwoHandSwordBoostAttackSpeed : 0f;

	public int[] GetAllCurrentRestBulletCount => (m_thsBurstCtrl != null) ? m_thsBurstCtrl.GetAllCurrentRestBulletCount : null;

	public int FirstReloadActionAtkID => (m_thsBurstCtrl != null) ? m_thsBurstCtrl.FirstReloadActionAtkID : (-1);

	public int CurrentRestBulletCount => (m_thsBurstCtrl != null) ? m_thsBurstCtrl.CurrentRestBulletCount : 0;

	public int CurrentMaxBulletCount => (m_thsBurstCtrl != null) ? m_thsBurstCtrl.CurrentMaxBulletCount : 0;

	public bool IsReadyForShoot => m_thsBurstCtrl != null && m_thsBurstCtrl.IsReadyForShoot;

	public bool IsShootingNow => m_thsBurstCtrl != null && m_thsBurstCtrl.IsShootingNow;

	public bool IsReloadingNow => m_thsBurstCtrl != null && m_thsBurstCtrl.IsReloadingNow;

	public bool IsHit3rdComboAttack => m_thsBurstCtrl != null && m_thsBurstCtrl.IsHit3rdComboAttack;

	public bool IsHitAvoidAttack => m_thsBurstCtrl != null && m_thsBurstCtrl.IsHitAvoidAttack;

	public bool IsEnableChangeReloadMotionSpeed => m_thsBurstCtrl != null && m_thsBurstCtrl.IsEnableChangeReloadMotionSpeed;

	public bool IsEnableTransitionFromAvoidAtk => m_thsBurstCtrl != null && m_thsBurstCtrl.IsEnableTransitionFromAvoidAtk;

	public TwoHandSwordController()
	{
		m_owner = null;
		m_actionInfo = null;
		m_thsSoulCtrl = new TwoHandSwordSoulController();
		m_thsCtrls[2] = m_thsSoulCtrl;
		m_thsBurstCtrl = new TwoHandSwordBurstController();
		m_thsCtrls[3] = m_thsBurstCtrl;
	}

	public TwoHandSwordController(InitParam _param)
	{
		InitAppend(_param);
	}

	public void Init(Player _player)
	{
		m_owner = _player;
		for (int i = 0; i < m_thsCtrls.Length; i++)
		{
			if (m_thsCtrls[i] != null)
			{
				m_thsCtrls[i].Init(_player);
			}
		}
		if (m_owner != null && m_owner.playerParameter != null)
		{
			m_actionInfo = m_owner.playerParameter.twoHandSwordActionInfo;
		}
	}

	public void InitAppend(InitParam _param)
	{
		Init(_param.Owner);
		m_thsBurstCtrl.Init(_param.BurstInitParam);
	}

	public void OnHit()
	{
	}

	public void OnLoadComplete()
	{
	}

	public void Update()
	{
		int validControllerIndex = GetValidControllerIndex();
		if (validControllerIndex >= 0)
		{
			m_thsCtrls[validControllerIndex].Update();
		}
	}

	public void OnEndAction()
	{
		int validControllerIndex = GetValidControllerIndex();
		if (validControllerIndex >= 0)
		{
			m_thsCtrls[validControllerIndex].OnEndAction();
		}
	}

	public void OnActDead()
	{
	}

	public void OnActAvoid()
	{
		int validControllerIndex = GetValidControllerIndex();
		if (validControllerIndex >= 0)
		{
			m_thsCtrls[validControllerIndex].OnActAvoid();
		}
	}

	public void OnRelease()
	{
		int validControllerIndex = GetValidControllerIndex();
		if (validControllerIndex >= 0)
		{
			m_thsCtrls[validControllerIndex].OnRelease();
		}
	}

	private int GetValidControllerIndex()
	{
		if (m_owner == null || m_owner.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD)
		{
			return -1;
		}
		int spAttackType = (int)m_owner.spAttackType;
		if (m_thsCtrls == null || spAttackType < 0 || m_thsCtrls.Length <= spAttackType || m_thsCtrls[spAttackType] == null)
		{
			return -1;
		}
		return spAttackType;
	}

	public bool GetNormalAttackId(SP_ATTACK_TYPE _spAtktype, EXTRA_ATTACK_TYPE _exAtkType, ref int _attackId, ref string _motionLayerName)
	{
		if (m_owner == null || m_actionInfo == null)
		{
			return false;
		}
		switch (_spAtktype)
		{
		case SP_ATTACK_TYPE.SOUL:
			if (_exAtkType == EXTRA_ATTACK_TYPE.LONG)
			{
				_attackId = m_owner.playerParameter.twoHandSwordActionInfo.Soul_LongAttackId;
			}
			else
			{
				_attackId = 10;
			}
			break;
		case SP_ATTACK_TYPE.BURST:
			_attackId = m_actionInfo.burstTHSInfo.BaseAtkId;
			_motionLayerName = m_owner.GetMotionLayerName(Player.ATTACK_MODE.TWO_HAND_SWORD, _spAtktype, _attackId);
			break;
		}
		return true;
	}

	public bool GetSpActionInfo(SP_ATTACK_TYPE _spAtkType, EXTRA_ATTACK_TYPE _exType, ref int _attackId, ref string _motionLayerName)
	{
		if (m_owner == null)
		{
			return false;
		}
		switch (_spAtkType)
		{
		case SP_ATTACK_TYPE.NONE:
			_attackId = 97;
			break;
		case SP_ATTACK_TYPE.HEAT:
			_attackId = 98;
			break;
		case SP_ATTACK_TYPE.SOUL:
			if (_exType == EXTRA_ATTACK_TYPE.LONG)
			{
				_attackId = m_owner.playerParameter.twoHandSwordActionInfo.Soul_LongSpAttackId;
			}
			else
			{
				_attackId = 96;
			}
			break;
		case SP_ATTACK_TYPE.BURST:
			if (m_actionInfo != null)
			{
				_attackId = m_actionInfo.burstTHSInfo.ReadyForShotID;
				_motionLayerName = m_owner.GetMotionLayerName(Player.ATTACK_MODE.TWO_HAND_SWORD, _spAtkType, _attackId);
			}
			break;
		}
		return true;
	}

	public bool GetSpGaugeIncreaseValue(AttackHitInfo.ATTACK_TYPE _atkType, int _attackId, float _attackRate, SoulEnergyController _soulEnergyCtrl, Vector3 _hitPosition, float _chargeRate, ref float _gaugeMax, ref float _increaseValue)
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (m_owner == null)
		{
			return false;
		}
		SP_ATTACK_TYPE spAttackType = m_owner.spAttackType;
		if (spAttackType != SP_ATTACK_TYPE.HEAT && spAttackType != SP_ATTACK_TYPE.SOUL)
		{
			return false;
		}
		if (m_actionInfo == null)
		{
			return false;
		}
		if (m_owner.spAttackType == SP_ATTACK_TYPE.HEAT)
		{
			if (_atkType == AttackHitInfo.ATTACK_TYPE.THS_HEAT_COMBO)
			{
				return false;
			}
			_gaugeMax = 999f;
			_increaseValue = m_actionInfo.heatGaugeIncreaseBase * _attackRate;
			return true;
		}
		if (spAttackType == SP_ATTACK_TYPE.SOUL)
		{
			if (object.ReferenceEquals(_soulEnergyCtrl, null))
			{
				return false;
			}
			SoulEnergy soulEnergy = null;
			if (IsSoulComboFinishAttackId(_attackId))
			{
				soulEnergy = _soulEnergyCtrl.Get(m_actionInfo.soulComboGaugeIncreaseValue);
			}
			else
			{
				if (!IsSoulSpAttackId(_attackId))
				{
					return false;
				}
				soulEnergy = _soulEnergyCtrl.Get(GetIaiGaugeIncreaseValue(_chargeRate));
			}
			if (!object.ReferenceEquals(soulEnergy, null))
			{
				if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIPlayerStatus>.I.DirectionSoulGauge(soulEnergy, _hitPosition);
				}
				if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIEnduranceStatus>.I.DirectionSoulGauge(soulEnergy, _hitPosition);
				}
			}
			return false;
		}
		return true;
	}

	public bool GetActAttackComboParam(ref int _attackId, ref string _motionLayerName)
	{
		SP_ATTACK_TYPE spAttackType = m_owner.spAttackType;
		if (spAttackType == SP_ATTACK_TYPE.BURST)
		{
			return m_thsBurstCtrl.GetComboAttackParam(ref _attackId, ref _motionLayerName);
		}
		return true;
	}

	public float GetAtkRate(Enemy _enemy, Player _player)
	{
		if (_enemy == null || _player == null)
		{
			return 1f;
		}
		if (m_owner.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD)
		{
			return 1f;
		}
		SP_ATTACK_TYPE spAttackType = _player.spAttackType;
		if (spAttackType == SP_ATTACK_TYPE.BURST)
		{
			return m_thsBurstCtrl.GetAtkRate(_enemy, _player);
		}
		return 1f;
	}

	public bool IsTwoHandSwordSpAttackContinueTimeOut(float _timer)
	{
		if (m_actionInfo == null)
		{
			return true;
		}
		return _timer > m_actionInfo.timeSpAttackContinueInput;
	}

	public void SetTwoHandSwordBoostAttackSpeed(float _speed)
	{
		if (m_thsSoulCtrl != null)
		{
			m_thsSoulCtrl.SetTwoHandSwordBoostAttackSpeed(_speed);
		}
	}

	public void SetSoulChargeRelease()
	{
		if (m_thsSoulCtrl != null)
		{
			m_thsSoulCtrl.SetChargeRelease();
		}
	}

	public bool GetIaiNormalDamageUp(ref float value, int attackID, float chargeRate)
	{
		if (m_thsSoulCtrl == null)
		{
			return false;
		}
		return m_thsSoulCtrl.GetIaiNormalDamageUp(ref value, attackID, chargeRate);
	}

	public float GetIaiGaugeIncreaseValue(float chargeRate)
	{
		if (m_thsSoulCtrl == null)
		{
			return 0f;
		}
		return m_thsSoulCtrl.GetIaiGaugeIncreaseValue(chargeRate);
	}

	public bool IsActiveIai()
	{
		if (m_thsSoulCtrl == null)
		{
			return false;
		}
		return m_thsSoulCtrl.IsActiveIai();
	}

	public bool IsSoulComboFinishAttackId(int attackId)
	{
		if (m_thsSoulCtrl == null)
		{
			return false;
		}
		return m_thsSoulCtrl.IsComboFinishAttackId(attackId);
	}

	public bool IsSoulSpAttackId(int attackId)
	{
		if (m_thsSoulCtrl == null)
		{
			return false;
		}
		return m_thsSoulCtrl.IsSpAttackId(attackId);
	}

	public void SetReadyForShoot(bool _isReady)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.SetReadyForShoot(_isReady);
		}
	}

	public void SetStartShooting(bool _isReady)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.SetStartShooting(_isReady);
		}
	}

	public void SetStartReloading(bool _isReadyForReload)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.SetStartReloading(_isReadyForReload);
		}
	}

	public void Set3rdComboAttackHitFlag(bool _isHit)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.Set3rdComboAttackHitFlag(_isHit);
		}
	}

	public void SetIsHitAvoidAttack(bool _isHit)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.SetIsHitAvoidAttack(_isHit);
		}
	}

	public void SetChangeReloadMotionSpeedFlag(bool _isEnable)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.SetChangeReloadMotionSpeedFlag(_isEnable);
		}
	}

	public void SetEnableTransitionFromAvoidAtkFlag(bool _isEnable)
	{
		if (m_thsBurstCtrl != null)
		{
			m_thsBurstCtrl.SetEnableTransitionFromAvoidAtkFlag(_isEnable);
		}
	}

	public bool GetBurstShotNormalDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.GetBurstShotNormalDamageUpRate(_type, ref _value);
	}

	public bool GetBurstShotElementDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.GetBurstShotElementDamageUpRate(_type, ref _value);
	}

	public bool IsEnableChangeActionByLongTap()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.IsEnableChangeActionByLongTap();
	}

	public bool IsRequiredReloadAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.IsRequiredReloadAction();
	}

	public bool IsEnableReloadAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.IsEnableReloadAction();
	}

	public bool IsEnableShootAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.IsEnableShootAction();
	}

	public bool DoShootAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.DoShootAction();
	}

	public bool DoReloadAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.DoReloadAction();
	}

	public bool TryFirstReloadAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.TryFirstReloadAction();
	}

	public bool IsChangebleReloadAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.IsChangebleReloadAction();
	}

	public bool CheckEnableNextReloadAction()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.CheckEnableNextReloadAction();
	}

	public bool DoFullBurst()
	{
		if (m_thsBurstCtrl == null)
		{
			return false;
		}
		return m_thsBurstCtrl.DoFullBurst();
	}
}
