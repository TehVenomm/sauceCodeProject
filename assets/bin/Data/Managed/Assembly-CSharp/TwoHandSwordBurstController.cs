using UnityEngine;

public class TwoHandSwordBurstController : IWeaponController
{
	public class InitParam
	{
		public InGameSettingsManager.Player.TwoHandSwordActionInfo ActionInfo;

		public Player Owner;

		public int MaxBulletCount = 6;

		public int[] CurrentRestBullets;

		public bool IsNeedFullBullet = true;
	}

	public const int MAX_BURST_BULLET_COUNT = 6;

	private Player m_owner;

	private InGameSettingsManager.Player.BurstTwoHandSwordActionInfo m_burstActionInfo;

	private int[] m_currentRestBulletCount;

	private int[] m_currentMaxBulletCount;

	private bool m_isReadyForShoot;

	private bool m_isShootingNow;

	private bool m_isReloadingNow;

	private bool m_isHit3rdComboAttack;

	private bool m_isHitAvoidAttack;

	private bool m_isEnableChangeReloadMotionSpeed;

	private bool m_isEnableTransitionFromAvoidAtk;

	public int FirstReloadActionAtkID => (m_burstActionInfo != null) ? m_burstActionInfo.FirstReloadActionAttackID : 0;

	public int[] GetAllCurrentRestBulletCount => m_currentRestBulletCount;

	public int CurrentRestBulletCount
	{
		get
		{
			if (m_owner == null || m_currentRestBulletCount == null || m_owner.weaponIndex < 0 || m_owner.weaponIndex >= m_currentRestBulletCount.Length || m_currentRestBulletCount.Length < 0)
			{
				return 0;
			}
			return m_currentRestBulletCount[m_owner.weaponIndex];
		}
	}

	public int CurrentMaxBulletCount
	{
		get
		{
			if (m_owner == null || m_currentMaxBulletCount == null || m_owner.weaponIndex < 0 || m_owner.weaponIndex >= m_currentMaxBulletCount.Length || m_currentMaxBulletCount.Length < 0)
			{
				return 0;
			}
			return m_currentMaxBulletCount[m_owner.weaponIndex];
		}
	}

	public bool IsReadyForShoot => m_isReadyForShoot;

	public bool IsShootingNow => m_isShootingNow;

	public bool IsReloadingNow => m_isReloadingNow;

	public bool IsHit3rdComboAttack => m_isHit3rdComboAttack;

	public bool IsHitAvoidAttack => m_isHitAvoidAttack;

	public bool IsEnableChangeReloadMotionSpeed => m_isEnableChangeReloadMotionSpeed;

	public bool IsEnableTransitionFromAvoidAtk => m_isEnableTransitionFromAvoidAtk;

	public TwoHandSwordBurstController()
	{
		m_owner = null;
		m_burstActionInfo = null;
		m_currentRestBulletCount = new int[3];
		m_currentMaxBulletCount = new int[3];
		for (int i = 0; i < 3; i++)
		{
			m_currentRestBulletCount[i] = 0;
			m_currentMaxBulletCount[i] = 0;
		}
	}

	public void SetCurrentRestBulletCount(int value)
	{
		if (value >= 0 && !(m_owner == null) && m_currentRestBulletCount != null && m_owner.weaponIndex >= 0 && m_owner.weaponIndex < m_currentRestBulletCount.Length && m_currentRestBulletCount.Length >= 0)
		{
			m_currentRestBulletCount[m_owner.weaponIndex] = value;
		}
	}

	public void SetReadyForShoot(bool _isReady)
	{
		m_isReadyForShoot = _isReady;
	}

	public void SetStartShooting(bool _isReady)
	{
		m_isShootingNow = _isReady;
	}

	public void SetStartReloading(bool _isReadyForReload)
	{
		m_isReloadingNow = _isReadyForReload;
	}

	public void Set3rdComboAttackHitFlag(bool _isHit)
	{
		m_isHit3rdComboAttack = _isHit;
	}

	public void SetIsHitAvoidAttack(bool _isHit)
	{
		m_isHitAvoidAttack = _isHit;
	}

	public void SetChangeReloadMotionSpeedFlag(bool _isEnable)
	{
		m_isEnableChangeReloadMotionSpeed = _isEnable;
	}

	public void SetEnableTransitionFromAvoidAtkFlag(bool _isEnable)
	{
		m_isEnableTransitionFromAvoidAtk = _isEnable;
	}

	public void Init(Player _player)
	{
		m_owner = _player;
	}

	public void Init(InitParam _param)
	{
		if (_param.Owner != null)
		{
			m_owner = _param.Owner;
		}
		m_burstActionInfo = _param.ActionInfo.burstTHSInfo;
		for (int i = 0; i < 3; i++)
		{
			if (!IsSetCurrentBulletCountAsFull(_param, i))
			{
				m_currentRestBulletCount[i] = _param.CurrentRestBullets[i];
			}
			else
			{
				m_currentRestBulletCount[i] = _param.MaxBulletCount;
			}
			m_currentMaxBulletCount[i] = _param.MaxBulletCount;
		}
	}

	private bool IsSetCurrentBulletCountAsFull(InitParam _param, int index)
	{
		if (_param.CurrentRestBullets == null || index < 0 || _param.CurrentRestBullets.Length <= index)
		{
			return true;
		}
		if (_param.IsNeedFullBullet && m_currentMaxBulletCount[index] < _param.MaxBulletCount)
		{
			return true;
		}
		return false;
	}

	public void OnLoadComplete()
	{
	}

	public void Update()
	{
	}

	public void OnEndAction()
	{
		SetReadyForShoot(_isReady: false);
		SetStartReloading(_isReadyForReload: false);
		SetStartShooting(_isReady: false);
		Set3rdComboAttackHitFlag(_isHit: false);
		SetIsHitAvoidAttack(_isHit: false);
		SetChangeReloadMotionSpeedFlag(_isEnable: false);
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
		int num = -1;
		string motionLayerName = string.Empty;
		if (IsReadyForShoot)
		{
			SetReadyForShoot(_isReady: false);
			if (IsRequiredReloadAction())
			{
				num = m_burstActionInfo.FirstReloadActionAttackID;
				motionLayerName = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, num);
			}
			else if (IsEnableShootAction())
			{
				num = m_burstActionInfo.FirstShotAttackID;
				motionLayerName = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, num);
				SetStartShooting(_isReady: true);
			}
		}
		if (IsReloadingNow && IsEnableReloadAction())
		{
			num = m_burstActionInfo.NextReloadActionAttackID;
			motionLayerName = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, num);
		}
		if (num != -1)
		{
			m_owner.ActAttack(num, send_packet: true, sync_immediately: true, motionLayerName, string.Empty);
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

	public bool GetActAttackComboParam(ref int _attackId, ref string _motionLayerName)
	{
		return true;
	}

	public bool GetBurstShotNormalDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
	{
		if (m_burstActionInfo == null)
		{
			return false;
		}
		switch (_type)
		{
		case AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT:
			_value = m_burstActionInfo.SingleShotBaseDmgRate;
			return true;
		case AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST:
			_value = m_burstActionInfo.FullBurstBaseDmgRate;
			return true;
		default:
			return false;
		}
	}

	public bool GetBurstShotElementDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
	{
		if (m_burstActionInfo == null)
		{
			return false;
		}
		switch (_type)
		{
		case AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT:
			_value = m_burstActionInfo.SingleShotElementDmgRate;
			return true;
		case AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST:
			_value = m_burstActionInfo.FullBurstElementDmgRate;
			return true;
		default:
			return false;
		}
	}

	public bool GetComboAttackParam(ref int _attackId, ref string _motionLayerName)
	{
		if (_attackId == m_burstActionInfo.NextShotAttackID)
		{
			if (!IsEnableShootAction())
			{
				return false;
			}
			_attackId = m_burstActionInfo.NextShotAttackID;
			_motionLayerName = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, _attackId);
		}
		return true;
	}

	public bool IsEnableChangeActionByLongTap()
	{
		if (m_owner == null)
		{
			return false;
		}
		if (IsEnableComboShotFromAvoidAttack())
		{
			int firstShotAttackID = m_burstActionInfo.FirstShotAttackID;
			string motionLayerName = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, firstShotAttackID);
			Player owner = m_owner;
			int id = firstShotAttackID;
			string motionLayerName2 = motionLayerName;
			owner.ActAttack(id, send_packet: true, sync_immediately: false, motionLayerName2, string.Empty);
			return true;
		}
		if (IsEnableShootFullBurst())
		{
			int fullBurstAttackID = m_burstActionInfo.FullBurstAttackID;
			string motionLayerName3 = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, fullBurstAttackID);
			Player owner2 = m_owner;
			int id = fullBurstAttackID;
			string motionLayerName2 = motionLayerName3;
			owner2.ActAttack(id, send_packet: true, sync_immediately: false, motionLayerName2, string.Empty);
			return true;
		}
		return false;
	}

	private bool IsEnableComboShotFromAvoidAttack()
	{
		if (!IsHitAvoidAttack)
		{
			return false;
		}
		if (!IsEnableTransitionFromAvoidAtk)
		{
			return false;
		}
		if (!IsEnableShootAction())
		{
			return false;
		}
		return true;
	}

	private bool IsEnableShootFullBurst()
	{
		if (!IsHit3rdComboAttack)
		{
			return false;
		}
		if (!IsEnableShootAction())
		{
			return false;
		}
		return true;
	}

	public bool IsRequiredReloadAction()
	{
		if (m_owner == null || m_owner.spAttackType != SP_ATTACK_TYPE.BURST)
		{
			return false;
		}
		return CurrentRestBulletCount <= 0;
	}

	public bool IsEnableReloadAction()
	{
		if (m_owner == null || m_owner.spAttackType != SP_ATTACK_TYPE.BURST)
		{
			return false;
		}
		if (CurrentMaxBulletCount <= 0)
		{
			return false;
		}
		return CurrentRestBulletCount < CurrentMaxBulletCount;
	}

	public bool IsEnableShootAction()
	{
		if (m_owner == null || m_owner.spAttackType != SP_ATTACK_TYPE.BURST)
		{
			return false;
		}
		if (CurrentMaxBulletCount <= 0)
		{
			return false;
		}
		return CurrentRestBulletCount > 0;
	}

	public bool DoShootAction()
	{
		if (!IsEnableShootAction())
		{
			return false;
		}
		return ConsumeBullet();
	}

	private bool ConsumeBullet()
	{
		if (CurrentRestBulletCount < 1)
		{
			return false;
		}
		SetCurrentRestBulletCount(CurrentRestBulletCount - 1);
		return true;
	}

	public bool DoReloadAction()
	{
		if (!IsEnableReloadAction() || !IsReloadingNow)
		{
			return false;
		}
		return ReloadBullet();
	}

	public bool TryFirstReloadAction()
	{
		if (m_owner == null)
		{
			return false;
		}
		SelfController selfController = m_owner.controller as SelfController;
		if (selfController == null)
		{
			return false;
		}
		selfController.OnReserveBurstReloadMotion();
		return true;
	}

	public bool IsChangebleReloadAction()
	{
		if (m_owner.isDead || m_owner.IsChangingWeapon || (!m_owner.IsPlayingMotion(2) && m_owner.actionID != (Character.ACTION_ID)26))
		{
			return false;
		}
		if (!IsEnableReloadAction() || IsReloadingNow)
		{
			return false;
		}
		return true;
	}

	public bool CheckEnableNextReloadAction()
	{
		if (IsEnableReloadAction())
		{
			int nextReloadActionAttackID = m_burstActionInfo.NextReloadActionAttackID;
			string motionLayerName = m_owner.GetMotionLayerName(m_owner.attackMode, m_owner.spAttackType, nextReloadActionAttackID);
			m_owner.ActAttack(nextReloadActionAttackID, send_packet: true, sync_immediately: true, motionLayerName, string.Empty);
			return true;
		}
		SetStartReloading(_isReadyForReload: false);
		return false;
	}

	private bool ReloadBullet()
	{
		if (CurrentRestBulletCount >= CurrentMaxBulletCount)
		{
			return false;
		}
		SetCurrentRestBulletCount(CurrentRestBulletCount + 1);
		return true;
	}

	public bool DoFullBurst()
	{
		if (CurrentRestBulletCount < 1)
		{
			return false;
		}
		SetCurrentRestBulletCount(0);
		return true;
	}

	public float GetAtkRate(Enemy _enemy, Player _player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = _enemy.get_transform().get_position() - _player.get_transform().get_position();
		float magnitude = val.get_magnitude();
		return m_burstActionInfo.GetDistanceAttenuationRatio(magnitude);
	}
}
