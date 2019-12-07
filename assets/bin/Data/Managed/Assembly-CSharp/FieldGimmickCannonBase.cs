using UnityEngine;

public class FieldGimmickCannonBase : FieldGimmickObject, IFieldGimmickCannon, IFieldGimmickObject
{
	public enum STATE
	{
		NONE,
		STANDBY,
		ROTATE,
		READY,
		COOLTIME,
		DISABLE
	}

	protected const string NAME_NODE_BASE = "CMN_cannon01_Origin/Move/Root/base/rot";

	protected const string NAME_NODE_CANNON = "CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot";

	protected const string NAME_ANIM_SHOT_REACTION = "Reaction";

	protected const float ROTATE_RAD_PER_FRAME = 0.17453292f;

	private const float RADIUS_TARGET_CANNON = 2f;

	private const float TIME_DEFAULT_COOL_TIME = 2f;

	public const int SE_ID_TURN_TO_TARGET = 10000079;

	protected Transform m_baseTrans;

	protected Transform m_cannonTrans;

	protected float m_coolTime;

	private float m_coolTimeCounter;

	protected float m_rotateTime;

	protected float m_rotateTimeCounter;

	private STATE m_state;

	protected Player m_owner;

	private Enemy m_boss;

	protected Quaternion m_rotStart = Quaternion.identity;

	protected Quaternion m_rotEnd = Quaternion.identity;

	private Transform m_targetEffect;

	protected Animator _animator
	{
		get;
		set;
	}

	protected Transform _transform
	{
		get;
		set;
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		m_coolTime = pointData.value1;
		_animator = base.gameObject.GetComponentInChildren<Animator>();
	}

	public bool IsUsing()
	{
		return m_owner != null;
	}

	public bool IsAbleToUse()
	{
		if (m_owner == null)
		{
			return m_state == STATE.NONE;
		}
		return false;
	}

	public bool IsAbleToShot()
	{
		if (IsUsing())
		{
			return m_state == STATE.READY;
		}
		return false;
	}

	public bool IsCooling()
	{
		return m_state == STATE.COOLTIME;
	}

	protected bool IsRemainCoolTime()
	{
		return m_coolTimeCounter > 0f;
	}

	protected virtual bool IsReadyForShot()
	{
		return m_state == STATE.READY;
	}

	public virtual bool IsAimCamera()
	{
		return true;
	}

	public virtual void OnBoard(Player player)
	{
		m_owner = player;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			m_boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		}
		SetState(STATE.STANDBY);
	}

	public virtual void OnLeave()
	{
		m_owner = null;
		SetState(STATE.NONE);
	}

	protected override void Awake()
	{
		_transform = base.transform;
		Utility.SetLayerWithChildren(base.transform, 19);
	}

	private void Update()
	{
		switch (m_state)
		{
		case STATE.NONE:
			UpdateStateNone();
			break;
		case STATE.STANDBY:
			UpdateStateStandBy();
			break;
		case STATE.ROTATE:
			UpdateStateRotate();
			break;
		case STATE.READY:
			UpdateStateReady();
			break;
		case STATE.COOLTIME:
			UpdateStateCooltime();
			break;
		case STATE.DISABLE:
			UpdateStateDisable();
			break;
		}
		DecreaseCoolTime();
	}

	private void LateUpdate()
	{
		STATE state = m_state;
		if ((uint)(state - 3) <= 1u)
		{
			UpdateCannonRotation();
			UpdateCannonAngle();
		}
	}

	protected virtual void UpdateStateNone()
	{
	}

	protected virtual void UpdateStateStandBy()
	{
		if (!(m_boss == null) && !(m_baseTrans == null))
		{
			Vector3 position = m_boss._position;
			position.y = 0f;
			Vector3 normalized = (position - _transform.position).normalized;
			Vector3 forward = m_baseTrans.forward;
			float num = Vector3.Dot(forward, normalized);
			m_rotateTimeCounter = 0f;
			m_rotateTime = Mathf.Acos(num) / 0.17453292f * (1f / (float)Application.targetFrameRate);
			m_rotStart = m_baseTrans.localRotation;
			m_rotEnd = m_rotStart * Quaternion.FromToRotation(forward, normalized);
			if (num >= 1f)
			{
				SetState(STATE.READY);
				return;
			}
			SetState(STATE.ROTATE);
			SoundManager.PlayOneShotSE(10000079, _transform.position);
		}
	}

	protected virtual void UpdateStateRotate()
	{
		m_rotateTimeCounter += Time.deltaTime;
		if (m_rotateTime <= 0f)
		{
			SetState(STATE.READY);
			return;
		}
		float num = Mathf.Clamp01(m_rotateTimeCounter / m_rotateTime);
		if (num >= 1f)
		{
			SetState(STATE.READY);
		}
		else
		{
			m_baseTrans.localRotation = Quaternion.Lerp(m_rotStart, m_rotEnd, num);
		}
	}

	protected virtual void UpdateStateReady()
	{
		if (IsRemainCoolTime())
		{
			SetState(STATE.COOLTIME);
		}
	}

	protected virtual void UpdateStateCooltime()
	{
		if (m_coolTimeCounter <= 0f)
		{
			SetState(STATE.READY);
		}
	}

	protected virtual void UpdateStateDisable()
	{
	}

	protected void StartCoolTime()
	{
		m_coolTimeCounter = 2f;
		if (m_coolTime > 0f)
		{
			m_coolTimeCounter = m_coolTime;
		}
	}

	private void DecreaseCoolTime()
	{
		if (m_coolTimeCounter >= 0f)
		{
			m_coolTimeCounter -= Time.deltaTime;
		}
	}

	private void UpdateCannonRotation()
	{
		if (!(m_baseTrans == null) && !(m_owner == null))
		{
			m_baseTrans.localRotation = m_owner._rigidbody.rotation * Quaternion.Inverse(_transform.rotation);
		}
	}

	private void UpdateCannonAngle()
	{
		if (!(m_baseTrans == null) && !(m_owner == null))
		{
			Self self = m_owner as Self;
			if (!(self == null))
			{
				m_cannonTrans.localRotation = Quaternion.Euler(self.GetCannonShotEuler());
			}
		}
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (((MonoBehaviourSingleton<StageObjectManager>.I.boss != null && !IsUsing()) & isNear) && self != null && self.IsChangeableAction((Character.ACTION_ID)31))
		{
			if (m_targetEffect == null && !string.IsNullOrEmpty(ResourceName.GetFieldGimmickCannonTargetEffect()))
			{
				m_targetEffect = EffectManager.GetEffect(ResourceName.GetFieldGimmickCannonTargetEffect(), _transform);
			}
			if (m_targetEffect != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized + Vector3.up + _transform.position;
				m_targetEffect.Set(pos, rotation);
			}
		}
		else if (m_targetEffect != null)
		{
			EffectManager.ReleaseEffect(m_targetEffect.gameObject);
		}
	}

	public virtual void Shot()
	{
		if (IsReadyForShot())
		{
			if (_animator != null)
			{
				_animator.Play("Reaction", 0, 0f);
			}
			AttackInfo attackHitInfo = GetAttackHitInfo();
			if (attackHitInfo != null)
			{
				AttackCannonball.InitParamCannonball initParamCannonball = new AttackCannonball.InitParamCannonball();
				initParamCannonball.attacker = m_owner;
				initParamCannonball.atkInfo = attackHitInfo;
				initParamCannonball.launchTrans = m_cannonTrans;
				initParamCannonball.offsetPos = Vector3.zero;
				initParamCannonball.offsetRot = Quaternion.identity;
				initParamCannonball.shotRotation = m_cannonTrans.rotation;
				new GameObject("AttackCannonball").AddComponent<AttackCannonball>().Initialize(initParamCannonball);
				EffectManager.GetEffect("ef_btl_magibullet_shot_01", m_cannonTrans);
				StartCoolTime();
				SetState(STATE.COOLTIME);
			}
		}
	}

	public void SetState(STATE state)
	{
		m_state = state;
	}

	public Vector3 GetPosition()
	{
		return _transform.position;
	}

	public Transform GetCannonTransform()
	{
		return m_cannonTrans;
	}

	public Transform GetBaseTransform()
	{
		return m_baseTrans;
	}

	public Vector3 GetBaseTransformForward()
	{
		if (m_baseTrans == null)
		{
			return Vector3.forward;
		}
		return m_baseTrans.forward;
	}

	protected virtual AttackInfo GetAttackHitInfo()
	{
		int num = 6;
		if (m_owner != null)
		{
			num = m_owner.GetCurrentWeaponElement();
		}
		string attackInfoName = "cannonball_";
		switch (num)
		{
		case 0:
			attackInfoName += "fire";
			break;
		case 1:
			attackInfoName += "water";
			break;
		case 2:
			attackInfoName += "thunder";
			break;
		case 3:
			attackInfoName += "soil";
			break;
		case 4:
			attackInfoName += "light";
			break;
		case 5:
			attackInfoName += "dark";
			break;
		default:
			attackInfoName += "normal";
			break;
		}
		AttackInfo attackInfo = m_owner.GetAttackInfos().Find((AttackInfo info) => info.name == attackInfoName);
		if (attackInfo == null)
		{
			Log.Error(LOG.INGAME, "Not found cannon attackInfo. name = " + attackInfoName);
			return null;
		}
		return attackInfo;
	}

	public virtual void ApplyCannonVector(Vector3 cannonVec)
	{
		Vector3 forward = cannonVec;
		forward.y = 0f;
		m_baseTrans.rotation = Quaternion.LookRotation(forward);
		m_cannonTrans.rotation = Quaternion.LookRotation(cannonVec);
	}
}
