using UnityEngine;

public class FieldGimmickCannonObject : FieldGimmickObject, IFieldGimmickCannon, IFieldGimmickObject
{
	private enum STATE
	{
		NONE,
		STANDBY,
		ROTATE,
		READY,
		COOLTIME,
		DISABLE
	}

	private const string NAME_FIELD_GIMMICK_CANNON = "FieldGimmickCannon";

	private const string NAME_NODE_BASE = "CMN_cannon01_Origin/Move/Root/base/rot";

	private const string NAME_NODE_CANNON = "CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot";

	private const string NAME_ANIM_SHOT_REACTION = "Reaction";

	private const float ROTATE_RAD_PER_FRAME = 0.17453292f;

	private const float RADIUS_TARGET_CANNON = 2f;

	private const float SQR_RADIUS_TARGET_CANNON = 4f;

	private const float TIME_DEFAULT_COOL_TIME = 2f;

	public const int SE_ID_TURN_TO_TARGET = 10000079;

	private Transform m_baseTrans;

	private Transform m_cannonTrans;

	private Player m_owner;

	private Enemy m_boss;

	private Transform m_targetEffect;

	private STATE m_state = STATE.DISABLE;

	private float m_rotateTime;

	private float m_rotateFinishTime;

	private Quaternion m_rotStart = Quaternion.identity;

	private Quaternion m_rotEnd = Quaternion.identity;

	private BallisticLineRenderer ballisticLineRenderer;

	private float m_coolTime;

	private float m_coolFinishTime;

	private Animator _animator
	{
		get;
		set;
	}

	private Transform _transform
	{
		get;
		set;
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		m_coolFinishTime = pointData.value1;
		m_baseTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
		m_cannonTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
		m_baseTrans.LookAt(Vector3.zero);
		_animator = base.gameObject.GetComponentInChildren<Animator>();
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
		}
		if (ballisticLineRenderer == null)
		{
			ballisticLineRenderer = base.gameObject.AddComponent<BallisticLineRenderer>();
		}
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

	public bool IsAimCamera()
	{
		return true;
	}

	public override float GetTargetRadius()
	{
		return 2f;
	}

	public override float GetTargetSqrRadius()
	{
		return 4f;
	}

	private void SetState(STATE state)
	{
		m_state = state;
	}

	public void OnLeave()
	{
		m_owner = null;
		ballisticLineRenderer.SetVisible(enabled: false);
		SetState(STATE.NONE);
	}

	public void OnBoard(Player player)
	{
		m_owner = player;
		AttackInfo attackHitInfo = GetAttackHitInfo(player.GetCurrentWeaponElement());
		if (attackHitInfo != null)
		{
			ballisticLineRenderer.SetBulletData(attackHitInfo.bulletData);
		}
		SetState(STATE.STANDBY);
	}

	public void SetStateReady()
	{
		if (m_owner is Self)
		{
			ballisticLineRenderer.SetVisible(enabled: true);
		}
		SetState(STATE.READY);
	}

	public void SetStateRotate()
	{
		SetState(STATE.ROTATE);
	}

	public void SetStateCooltime()
	{
		m_coolTime = 2f;
		if (m_coolFinishTime > 0f)
		{
			m_coolTime = m_coolFinishTime;
		}
		SetState(STATE.COOLTIME);
	}

	private bool IsReadyForShot()
	{
		return m_state == STATE.READY;
	}

	private bool IsRemainCooltime()
	{
		return m_coolTime > 0f;
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
			if (m_boss == null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				m_boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			}
			if (!m_boss.IsValidShield())
			{
				SetState(STATE.DISABLE);
			}
			break;
		case STATE.STANDBY:
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && !(MonoBehaviourSingleton<StageObjectManager>.I.boss == null))
			{
				Vector3 position = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
				position.y = 0f;
				Vector3 normalized = (position - _transform.position).normalized;
				float num = Vector3.Dot(m_baseTrans.forward, normalized);
				m_rotateTime = 0f;
				m_rotateFinishTime = Mathf.Acos(num) / 0.17453292f * (1f / (float)Application.targetFrameRate);
				m_rotStart = Quaternion.LookRotation(m_baseTrans.forward);
				m_rotEnd = Quaternion.LookRotation(normalized);
				if (num >= 1f)
				{
					SetStateReady();
					break;
				}
				SetStateRotate();
				SoundManager.PlayOneShotSE(10000079, _transform.position);
			}
			break;
		case STATE.ROTATE:
		{
			m_rotateTime += Time.deltaTime;
			if (m_rotateFinishTime <= 0f)
			{
				SetStateReady();
				break;
			}
			float num2 = Mathf.Clamp(m_rotateTime / m_rotateFinishTime, 0f, 1f);
			if (num2 >= 1f)
			{
				SetStateReady();
			}
			else
			{
				m_baseTrans.localRotation = Quaternion.Lerp(m_rotStart, m_rotEnd, num2);
			}
			break;
		}
		case STATE.READY:
			if (IsRemainCooltime())
			{
				SetState(STATE.COOLTIME);
			}
			break;
		case STATE.COOLTIME:
			if (m_coolTime <= 0f)
			{
				SetStateReady();
			}
			break;
		case STATE.DISABLE:
			if (m_boss == null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				m_boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			}
			if (m_boss.IsValidShield())
			{
				OnLeave();
			}
			break;
		}
		if (m_coolTime >= 0f)
		{
			m_coolTime -= Time.deltaTime;
		}
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

	public override void UpdateTargetMarker(bool isNear)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (((boss != null && boss.IsValidShield() && !IsUsing()) & isNear) && self != null && self.IsChangeableAction((Character.ACTION_ID)31))
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

	private void UpdateCannonAngle()
	{
		if (!(m_cannonTrans == null) && !(m_owner == null))
		{
			Self self = m_owner as Self;
			if (!(self == null))
			{
				m_cannonTrans.localRotation = Quaternion.Euler(self.GetCannonShotEuler());
				ballisticLineRenderer.UpdateLine(m_cannonTrans.position, m_cannonTrans.rotation * Vector3.forward);
			}
		}
	}

	private void UpdateCannonRotation()
	{
		if (!(m_baseTrans == null) && !(m_owner == null))
		{
			m_baseTrans.localRotation = m_owner._rigidbody.rotation;
		}
	}

	private AttackInfo GetAttackHitInfo(int weaponElement)
	{
		string attackInfoName = "cannonball_";
		switch (weaponElement)
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

	public void Shot()
	{
		if (IsReadyForShot())
		{
			if (_animator != null)
			{
				_animator.Play("Reaction", 0, 0f);
			}
			AttackInfo attackHitInfo = GetAttackHitInfo(m_owner.GetCurrentWeaponElement());
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
				SetStateCooltime();
			}
		}
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

	public Vector3 GetPosition()
	{
		return _transform.position;
	}

	public void ApplyCannonVector(Vector3 cannonVec)
	{
		Vector3 forward = cannonVec;
		forward.y = 0f;
		m_baseTrans.rotation = Quaternion.LookRotation(forward);
		m_cannonTrans.rotation = Quaternion.LookRotation(cannonVec);
	}

	public override string GetObjectName()
	{
		return "FieldGimmickCannon";
	}
}
