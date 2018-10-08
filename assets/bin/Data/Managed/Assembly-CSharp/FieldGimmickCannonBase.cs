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

	protected Quaternion m_rotStart = Quaternion.get_identity();

	protected Quaternion m_rotEnd = Quaternion.get_identity();

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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(pointData);
		m_coolTime = pointData.value1;
		_animator = this.get_gameObject().GetComponentInChildren<Animator>();
	}

	public bool IsUsing()
	{
		return m_owner != null;
	}

	public bool IsAbleToUse()
	{
		return m_owner == null && m_state == STATE.NONE;
	}

	public bool IsAbleToShot()
	{
		return IsUsing() && m_state == STATE.READY;
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		_transform = this.get_transform();
		Utility.SetLayerWithChildren(this.get_transform(), 19);
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
		if (state == STATE.READY || state == STATE.COOLTIME)
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
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_boss == null) && !(m_baseTrans == null))
		{
			Vector3 position = m_boss._position;
			position.y = 0f;
			Vector3 val = position - _transform.get_position();
			Vector3 normalized = val.get_normalized();
			Vector3 forward = m_baseTrans.get_forward();
			float num = Vector3.Dot(forward, normalized);
			m_rotateTimeCounter = 0f;
			m_rotateTime = Mathf.Acos(num) / 0.17453292f * (1f / (float)Application.get_targetFrameRate());
			m_rotStart = m_baseTrans.get_localRotation();
			m_rotEnd = m_rotStart * Quaternion.FromToRotation(forward, normalized);
			if (num >= 1f)
			{
				SetState(STATE.READY);
			}
			else
			{
				SetState(STATE.ROTATE);
				SoundManager.PlayOneShotSE(10000079, _transform.get_position());
			}
		}
	}

	protected virtual void UpdateStateRotate()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		m_rotateTimeCounter += Time.get_deltaTime();
		if (m_rotateTime <= 0f)
		{
			SetState(STATE.READY);
		}
		else
		{
			float num = Mathf.Clamp01(m_rotateTimeCounter / m_rotateTime);
			if (num >= 1f)
			{
				SetState(STATE.READY);
			}
			else
			{
				m_baseTrans.set_localRotation(Quaternion.Lerp(m_rotStart, m_rotEnd, num));
			}
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
			m_coolTimeCounter -= Time.get_deltaTime();
		}
	}

	private void UpdateCannonRotation()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_baseTrans == null) && !(m_owner == null))
		{
			m_baseTrans.set_localRotation(m_owner._rigidbody.get_rotation() * Quaternion.Inverse(_transform.get_rotation()));
		}
	}

	private void UpdateCannonAngle()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_baseTrans == null) && !(m_owner == null))
		{
			Self self = m_owner as Self;
			if (!(self == null))
			{
				m_cannonTrans.set_localRotation(Quaternion.Euler(self.GetCannonShotEuler()));
			}
		}
	}

	public new void UpdateTargetMarker(bool isNear)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null && !IsUsing() && isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)30))
		{
			if (m_targetEffect == null && !string.IsNullOrEmpty(ResourceName.GetFieldGimmickCannonTargetEffect()))
			{
				m_targetEffect = EffectManager.GetEffect(ResourceName.GetFieldGimmickCannonTargetEffect(), _transform);
			}
			if (m_targetEffect != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - _transform.get_position();
				Vector3 pos = val.get_normalized() + Vector3.get_up() + _transform.get_position();
				m_targetEffect.Set(pos, rotation);
			}
		}
		else if (m_targetEffect != null)
		{
			EffectManager.ReleaseEffect(m_targetEffect.get_gameObject(), true, false);
		}
	}

	public virtual void Shot()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
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
				initParamCannonball.offsetPos = Vector3.get_zero();
				initParamCannonball.offsetRot = Quaternion.get_identity();
				initParamCannonball.shotRotation = m_cannonTrans.get_rotation();
				GameObject val = new GameObject("AttackCannonball");
				AttackCannonball attackCannonball = val.AddComponent<AttackCannonball>();
				attackCannonball.Initialize(initParamCannonball);
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return _transform.get_position();
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (m_baseTrans == null)
		{
			return Vector3.get_forward();
		}
		return m_baseTrans.get_forward();
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
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = cannonVec;
		val.y = 0f;
		m_baseTrans.set_rotation(Quaternion.LookRotation(val));
		m_cannonTrans.set_rotation(Quaternion.LookRotation(cannonVec));
	}
}
