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

	public TwoHandSwordSoulController()
	{
		m_owner = null;
	}

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
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		switch (iaiState)
		{
		case eIaiState.In:
			iaiCounter += Time.get_deltaTime();
			if (iaiCounter >= m_actionInfo.soulIaiInSec)
			{
				m_owner.SetEnableNodeRenderer(string.Empty, false);
				m_owner.EventMoveEnd();
				m_owner.SetEnableEventMove(true);
				m_owner.SetEnableAddForce(false);
				m_owner.SetEventMoveVelocity(Vector3.get_forward() * GetIaiMoveSpeed(m_owner.ChargeRate) * m_owner.buffParam.GetDistanceRateIai());
				m_owner.SetVelocity(Quaternion.LookRotation(m_owner.GetTransformForward()) * m_owner.EventMoveVelocity, Character.VELOCITY_TYPE.EVENT_MOVE);
				m_owner.SetEventMoveTimeCount(m_actionInfo.soulIaiMoveSec);
				iaiCounter = 0f;
				SetIaiState(eIaiState.Hide);
			}
			break;
		case eIaiState.Hide:
			iaiCounter += Time.get_deltaTime();
			if (iaiCounter >= m_actionInfo.soulIaiHideSec)
			{
				m_owner.SetEnableNodeRenderer(string.Empty, true);
				m_owner.SetNextTrigger(0);
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
			m_owner.ReleaseEffect(ref twoHandSwordsChargeMaxEffect, true);
		}
		SetIaiState(eIaiState.None);
		iaiCounter = 0f;
	}

	public void OnActDead()
	{
	}

	public void OnActAvoid()
	{
	}

	public void OnRelease()
	{
	}

	public void SetChargeRelease()
	{
		if (!(m_owner == null))
		{
			m_owner.ReleaseEffect(ref twoHandSwordsChargeMaxEffect, true);
			iaiCounter = 0f;
			SetIaiState(eIaiState.In);
			m_owner.SetNextTrigger(0);
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
		return attackId == 14 || attackId == m_actionInfo.Soul_LongAttackFinishId;
	}

	public bool IsSpAttackId(int attackId)
	{
		return attackId == 96 || attackId == m_actionInfo.Soul_LongSpAttackId;
	}
}
