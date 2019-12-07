using UnityEngine;

public class TwoHandSwordSoulController : IWeaponController
{
	public class InitParam
	{
		public Player Owner;
	}

	public enum eIaiState
	{
		None,
		In,
		Hide
	}

	public const int TWOHANDSWORD_SOUL_BASE_ATTACK_ID = 10;

	public const int TWOHANDSWORD_SOUL_FINISH_ATTACK_ID = 14;

	public const int TWOHANDSWORD_SOUL_SP_ATTACK_ID = 96;

	private Player m_owner;

	private InGameSettingsManager.Player.TwoHandSwordActionInfo m_actionInfo;

	private eIaiState iaiState;

	private float twoHandSwordBoostAttackSpeed;

	private Transform twoHandSwordsBoostLoopEffect;

	private Transform twoHandSwordsChargeMaxEffect;

	private float iaiCounter;

	public eIaiState IaiState => iaiState;

	public float TwoHandSwordBoostAttackSpeed => twoHandSwordBoostAttackSpeed;

	public void SetIaiState(eIaiState _state)
	{
		iaiState = _state;
	}

	public bool IsActiveIai()
	{
		return iaiState != eIaiState.None;
	}

	public void SetTwoHandSwordBoostAttackSpeed(float _speed)
	{
		twoHandSwordBoostAttackSpeed = _speed;
		if (m_actionInfo != null)
		{
			twoHandSwordBoostAttackSpeed = Mathf.Clamp(twoHandSwordBoostAttackSpeed, m_actionInfo.soulBoostMinAttackSpeed, m_actionInfo.soulBoostMaxAttackSpeed);
		}
	}

	public TwoHandSwordSoulController()
	{
		m_owner = null;
	}

	public void Init(Player _player)
	{
		m_owner = _player;
		m_actionInfo = _player.playerParameter.twoHandSwordActionInfo;
	}

	public void Init(InitParam _param)
	{
	}

	public void OnLoadComplete()
	{
	}

	public void Update()
	{
		switch (iaiState)
		{
		case eIaiState.In:
			iaiCounter += Time.deltaTime;
			if (iaiCounter >= m_actionInfo.soulIaiInSec)
			{
				m_owner.SetEnableNodeRenderer("", enable: false);
				m_owner.EventMoveEnd();
				m_owner.SetEnableEventMove(_isEnable: true);
				m_owner.SetEnableAddForce(_isEnable: false);
				m_owner.SetEventMoveVelocity(Vector3.forward * GetIaiMoveSpeed(m_owner.ChargeRate) * m_owner.buffParam.GetDistanceRateIai());
				m_owner.SetVelocity(Quaternion.LookRotation(m_owner.GetTransformForward()) * m_owner.EventMoveVelocity, Character.VELOCITY_TYPE.EVENT_MOVE);
				m_owner.SetEventMoveTimeCount(m_actionInfo.soulIaiMoveSec);
				iaiCounter = 0f;
				SetIaiState(eIaiState.Hide);
			}
			break;
		case eIaiState.Hide:
			iaiCounter += Time.deltaTime;
			if (iaiCounter >= m_actionInfo.soulIaiHideSec)
			{
				m_owner.SetEnableNodeRenderer("", enable: true);
				m_owner.SetNextTrigger();
				iaiCounter = 0f;
				SetIaiState(eIaiState.None);
			}
			break;
		}
	}

	public void OnEndAction()
	{
		if (m_owner != null)
		{
			m_owner.ReleaseEffect(ref twoHandSwordsChargeMaxEffect);
		}
		SetIaiState(eIaiState.None);
		iaiCounter = 0f;
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

	public void SetChargeRelease()
	{
		if (!(m_owner == null))
		{
			m_owner.ReleaseEffect(ref twoHandSwordsChargeMaxEffect);
			iaiCounter = 0f;
			SetIaiState(eIaiState.In);
			m_owner.SetNextTrigger();
		}
	}

	public bool GetIaiNormalDamageUp(ref float value, int attackID, float chargeRate)
	{
		value = 1f;
		if (!IsSpAttackId(attackID))
		{
			return false;
		}
		if (chargeRate >= 1f)
		{
			value = m_actionInfo.soulIaiNormalDamageUp[2];
		}
		else
		{
			value = m_actionInfo.soulIaiNormalDamageUp[0] + (m_actionInfo.soulIaiNormalDamageUp[1] - m_actionInfo.soulIaiNormalDamageUp[0]) * chargeRate;
		}
		return true;
	}

	public float GetIaiGaugeIncreaseValue(float chargeRate)
	{
		if (chargeRate >= 1f)
		{
			return m_actionInfo.soulIaiGaugeIncreaseValue[2];
		}
		return m_actionInfo.soulIaiGaugeIncreaseValue[0] + (m_actionInfo.soulIaiGaugeIncreaseValue[1] - m_actionInfo.soulIaiGaugeIncreaseValue[0]) * chargeRate;
	}

	private float GetIaiMoveSpeed(float chargeRate)
	{
		return m_actionInfo.soulIaiMoveSpeed[0] + (m_actionInfo.soulIaiMoveSpeed[1] - m_actionInfo.soulIaiMoveSpeed[0]) * chargeRate;
	}

	public bool IsComboFinishAttackId(int attackId)
	{
		if (attackId != 14)
		{
			return attackId == m_actionInfo.Soul_LongAttackFinishId;
		}
		return true;
	}

	public bool IsSpAttackId(int attackId)
	{
		if (attackId != 96)
		{
			return attackId == m_actionInfo.Soul_LongSpAttackId;
		}
		return true;
	}
}
