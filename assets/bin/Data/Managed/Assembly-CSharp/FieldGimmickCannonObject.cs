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

	private Quaternion m_rotStart = Quaternion.get_identity();

	private Quaternion m_rotEnd = Quaternion.get_identity();

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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(pointData);
		m_coolFinishTime = pointData.value1;
		m_baseTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
		m_cannonTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
		m_baseTrans.LookAt(Vector3.get_zero());
		_animator = this.get_gameObject().GetComponentInChildren<Animator>();
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
		}
		if (ballisticLineRenderer == null)
		{
			ballisticLineRenderer = this.get_gameObject().AddComponent<BallisticLineRenderer>();
		}
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
		ballisticLineRenderer.SetVisible(false);
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
			ballisticLineRenderer.SetVisible(true);
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		_transform = this.get_transform();
		Utility.SetLayerWithChildren(this.get_transform(), 19);
	}

	private void Update()
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
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
				Vector3 val = position - _transform.get_position();
				Vector3 normalized = val.get_normalized();
				float num = Vector3.Dot(m_baseTrans.get_forward(), normalized);
				m_rotateTime = 0f;
				m_rotateFinishTime = Mathf.Acos(num) / 0.17453292f * (1f / (float)Application.get_targetFrameRate());
				m_rotStart = Quaternion.LookRotation(m_baseTrans.get_forward());
				m_rotEnd = Quaternion.LookRotation(normalized);
				if (num >= 1f)
				{
					SetStateReady();
				}
				else
				{
					SetStateRotate();
					SoundManager.PlayOneShotSE(10000079, _transform.get_position());
				}
			}
			break;
		case STATE.ROTATE:
			m_rotateTime += Time.get_deltaTime();
			if (m_rotateFinishTime <= 0f)
			{
				SetStateReady();
			}
			else
			{
				float num2 = Mathf.Clamp(m_rotateTime / m_rotateFinishTime, 0f, 1f);
				if (num2 >= 1f)
				{
					SetStateReady();
				}
				else
				{
					m_baseTrans.set_localRotation(Quaternion.Lerp(m_rotStart, m_rotEnd, num2));
				}
			}
			break;
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
			m_coolTime -= Time.get_deltaTime();
		}
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

	public new void UpdateTargetMarker(bool isNear)
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null && boss.IsValidShield() && !IsUsing() && isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)30))
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

	private void UpdateCannonAngle()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_cannonTrans == null) && !(m_owner == null))
		{
			Self self = m_owner as Self;
			if (!(self == null))
			{
				m_cannonTrans.set_localRotation(Quaternion.Euler(self.GetCannonShotEuler()));
				ballisticLineRenderer.UpdateLine(m_cannonTrans.get_position(), m_cannonTrans.get_rotation() * Vector3.get_forward());
			}
		}
	}

	private void UpdateCannonRotation()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_baseTrans == null) && !(m_owner == null))
		{
			m_baseTrans.set_localRotation(m_owner._rigidbody.get_rotation());
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
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
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
				initParamCannonball.offsetPos = Vector3.get_zero();
				initParamCannonball.offsetRot = Quaternion.get_identity();
				initParamCannonball.shotRotation = m_cannonTrans.get_rotation();
				GameObject val = new GameObject("AttackCannonball");
				AttackCannonball attackCannonball = val.AddComponent<AttackCannonball>();
				attackCannonball.Initialize(initParamCannonball);
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (m_baseTrans == null)
		{
			return Vector3.get_forward();
		}
		return m_baseTrans.get_forward();
	}

	public Vector3 GetPosition()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return _transform.get_position();
	}

	public void ApplyCannonVector(Vector3 cannonVec)
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

	public override string GetObjectName()
	{
		return "FieldGimmickCannon";
	}
}
