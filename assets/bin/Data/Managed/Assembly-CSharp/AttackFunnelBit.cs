using System.Collections.Generic;
using UnityEngine;

public class AttackFunnelBit : IBulletObservable
{
	public enum Function
	{
		None,
		Main,
		Search,
		Delete
	}

	public const float AIM_RAD_MIN = 0.00174532924f;

	public const string ANIM_STATE_BREAK = "BREAK";

	public const string ANIM_STATE_DISAPPEAR = "END";

	private BulletData.BulletFunnel m_funnelData;

	private StageObject m_attacker;

	private AttackInfo m_atkInfo;

	private Transform m_cachedTransform;

	private StageObject m_targetObject;

	private GameObject m_effectObj;

	private string m_landHitEffectName = string.Empty;

	private float m_aimAngleSpeed;

	private float m_moveSpeed;

	private float m_aliveTimer;

	private bool m_isDeleted;

	private Function m_func;

	private int m_state;

	private int m_effectDeleteAnimHash;

	private Animator m_effectAnimator;

	private float m_attackIntervalTimer;

	private AtkAttribute m_exAtk;

	private Player.ATTACK_MODE m_attackMode;

	private SkillInfo.SkillParam m_skillParam;

	private CapsuleCollider m_capsuleCollider;

	private float m_radius;

	private TargetPoint m_targetPoint;

	private int observedID;

	private List<IBulletObserver> bulletObserverList = new List<IBulletObserver>();

	public TargetPoint targetPoint => m_targetPoint;

	protected StageObject TargetObject => m_targetObject;

	public string AttackInfoName => m_atkInfo.name;

	public bool IsDeleted => m_isDeleted;

	public AttackFunnelBit()
		: this()
	{
	}

	public virtual void Initialize(StageObject attacker, AttackInfo atkInfo, StageObject targetObj, Transform launchTrans, Vector3 offsetPos, Quaternion offsetRot)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		m_attacker = attacker;
		m_atkInfo = atkInfo;
		AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
		if (attackHitInfo != null)
		{
			attackHitInfo.enableIdentityCheck = false;
		}
		BulletData bulletData = atkInfo.bulletData;
		m_landHitEffectName = bulletData.data.landHiteffectName;
		m_aliveTimer = bulletData.data.appearTime;
		m_moveSpeed = bulletData.data.speed;
		SetColliderByRadius(bulletData.data.radius);
		BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
		m_aimAngleSpeed = dataFunnel.lookAtAngle * 0.0174532924f;
		m_funnelData = dataFunnel;
		m_isDeleted = false;
		m_cachedTransform = this.get_transform();
		m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
		m_cachedTransform.set_position(launchTrans.get_position() + launchTrans.get_rotation() * offsetPos);
		m_cachedTransform.set_rotation(launchTrans.get_rotation() * offsetRot);
		m_cachedTransform.set_localScale(bulletData.data.timeStartScale);
		Transform effect = EffectManager.GetEffect(bulletData.data.effectName, this.get_transform());
		effect.set_localPosition(bulletData.data.dispOffset);
		effect.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
		effect.set_localScale(Vector3.get_one());
		m_effectObj = effect.get_gameObject();
		m_effectAnimator = m_effectObj.GetComponent<Animator>();
		RegisterObserver();
		if (targetObj != null)
		{
			RequestMain(targetObj);
		}
		else
		{
			RequestSearch();
		}
	}

	private void SetColliderByRadius(float _radius)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		if (!(_radius <= 0f))
		{
			m_radius = _radius;
			m_capsuleCollider = this.get_gameObject().AddComponent<CapsuleCollider>();
			m_capsuleCollider.set_center(new Vector3(0f, 0f, 0f));
			m_capsuleCollider.set_direction(2);
			m_capsuleCollider.set_isTrigger(true);
			m_capsuleCollider.set_radius(m_radius);
			m_capsuleCollider.set_height(m_radius * 2f);
			Utility.SetLayerWithChildren(this.get_transform(), 31);
			SetTargetPoint();
		}
	}

	private void SetTargetPoint()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		m_targetPoint = this.get_gameObject().AddComponent<TargetPoint>();
		m_targetPoint.isAimEnable = false;
		m_targetPoint.isTargetEnable = false;
	}

	private void Update()
	{
		switch (m_func)
		{
		case Function.Main:
			FuncMain();
			break;
		case Function.Search:
			FuncSearch();
			break;
		case Function.Delete:
			FuncDelete();
			break;
		}
	}

	private void RequestMain(StageObject targetObj)
	{
		m_targetObject = targetObj;
		RequestFunction(Function.Main);
	}

	private void FuncMain()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDeleted)
		{
			m_aliveTimer -= Time.get_deltaTime();
			if (m_aliveTimer <= 0f)
			{
				RequestDestroy(false);
			}
			else if (!(m_targetObject == null))
			{
				switch (m_state)
				{
				case 1:
				{
					Vector3 position = m_targetObject.get_transform().get_position();
					position.y = GetFloatingHeight();
					LookAtTarget(position);
					Vector3 forward = m_cachedTransform.get_forward();
					Vector3 val = m_cachedTransform.get_position() + forward * (m_moveSpeed * Time.get_deltaTime());
					m_cachedTransform.set_position(val);
					if (Vector3.Distance(position, val) <= GetAttackStartRange())
					{
						m_attackIntervalTimer = m_funnelData.attackInterval;
						ForwardState();
					}
					break;
				}
				case 2:
					RotateAroundTarget();
					LookAtTarget(m_targetObject.get_transform().get_position());
					m_attackIntervalTimer -= Time.get_deltaTime();
					if (!(m_attackIntervalTimer > 0f))
					{
						m_attackIntervalTimer = m_funnelData.attackInterval;
						if (CheckTargetDead())
						{
							RequestDestroy(false);
						}
						else
						{
							CreateBullet();
						}
					}
					break;
				}
			}
			else
			{
				RequestDestroy(false);
			}
		}
	}

	private void RotateAroundTarget()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		BulletData.BulletFunnel funnelData = m_funnelData;
		float floatingHeight = GetFloatingHeight();
		Vector3 position = m_targetObject.get_transform().get_position();
		position.y = floatingHeight;
		Vector3 position2 = m_cachedTransform.get_position();
		position2.y = floatingHeight;
		Vector3 val = position2 - position;
		val.Normalize();
		val *= GetAttackStartRange();
		val = Quaternion.AngleAxis(funnelData.rotateAngle, Vector3.get_up()) * val;
		Vector3 val2 = position + val - m_cachedTransform.get_position();
		m_cachedTransform.set_position(position2 + val2 * Time.get_deltaTime());
	}

	private void LookAtTarget(Vector3 targetPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		Vector3 forward = m_cachedTransform.get_forward();
		Vector3 position = m_cachedTransform.get_position();
		Vector3 val = targetPos - position;
		val.Normalize();
		float num = m_aimAngleSpeed * Time.get_deltaTime();
		float num2 = Vector3.Dot(forward, val);
		float num3 = Mathf.Acos(num2);
		if (num2 < 1f && num < num3 && num3 >= 0.00174532924f)
		{
			float num4 = num / num3;
			Quaternion rotation;
			if (num4 >= 1f)
			{
				rotation = Quaternion.LookRotation(val);
			}
			else
			{
				Quaternion val2 = Quaternion.LookRotation(forward);
				Quaternion val3 = Quaternion.LookRotation(val);
				rotation = Quaternion.Slerp(val2, val3, num4);
			}
			m_cachedTransform.set_rotation(rotation);
		}
		else
		{
			m_cachedTransform.set_rotation(Quaternion.LookRotation(val));
		}
	}

	private AnimEventShot CreateBullet()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return null;
		}
		BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
		if (dataFunnel == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.get_rotation();
		Vector3 pos = m_cachedTransform.get_position() + rotation * m_funnelData.offsetPosition;
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataFunnel.bitBullet, m_attacker, m_atkInfo, pos, rotation, m_exAtk, m_attackMode, m_skillParam);
		if (animEventShot == null)
		{
			Log.Error("Failed to create AnimEventShot for Funnel!!");
			return null;
		}
		return animEventShot;
	}

	private void RequestSearch()
	{
		RequestFunction(Function.Search);
	}

	private void FuncSearch()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDeleted)
		{
			m_aliveTimer -= Time.get_deltaTime();
			if (m_aliveTimer <= 0f)
			{
				RequestDestroy(false);
			}
			else
			{
				int state = m_state;
				if (state == 1)
				{
					Transform cachedTransform = m_cachedTransform;
					Vector3 position = cachedTransform.get_position() + cachedTransform.get_forward() * (m_moveSpeed * Time.get_deltaTime());
					cachedTransform.set_position(position);
					float searchRange = m_funnelData.searchRange;
					if (!(searchRange <= 0f) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
					{
						Vector2 zero = Vector2.get_zero();
						zero.x = position.x;
						zero.y = position.z;
						StageObject stageObject = SearchNearestTarget(zero, searchRange);
						if (stageObject != null)
						{
							RequestMain(stageObject);
						}
					}
				}
			}
		}
	}

	public void RequestDestroy(bool isPlayFallEffect = true)
	{
		if (m_func != Function.Delete && !IsDeleted)
		{
			RequestFunction(Function.Delete);
			if (m_effectAnimator == null)
			{
				Destroy();
			}
			else
			{
				m_effectDeleteAnimHash = Animator.StringToHash((!isPlayFallEffect) ? "END" : "BREAK");
				if (m_effectAnimator.HasState(0, m_effectDeleteAnimHash))
				{
					m_effectAnimator.Play(m_effectDeleteAnimHash, 0, 0f);
					m_effectAnimator.Update(0f);
				}
				else
				{
					Debug.LogWarning((object)"Not found delete animation!!");
					Destroy();
				}
			}
		}
	}

	private void FuncDelete()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		switch (m_state)
		{
		case 1:
			if (m_effectAnimator == null)
			{
				ForwardState();
			}
			else
			{
				AnimatorStateInfo currentAnimatorStateInfo = m_effectAnimator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
				{
					ForwardState();
				}
			}
			break;
		case 2:
			Destroy();
			ForwardState();
			break;
		}
	}

	private void Destroy()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDeleted)
		{
			m_isDeleted = true;
			if (!string.IsNullOrEmpty(m_landHitEffectName))
			{
				Transform effect = EffectManager.GetEffect(m_landHitEffectName, null);
				if (effect != null)
				{
					effect.set_position(m_cachedTransform.get_position());
					effect.set_rotation(m_cachedTransform.get_rotation());
				}
			}
			if (m_attacker != null)
			{
				Enemy enemy = m_attacker as Enemy;
				if (enemy != null)
				{
					enemy.OnDestroyFunnel(this);
				}
				m_attacker = null;
			}
			NotifyDestroy();
			Object.Destroy(this.get_gameObject());
		}
	}

	private void OnDestroy()
	{
		if (m_effectObj != null)
		{
			EffectManager.ReleaseEffect(m_effectObj, true, false);
			m_effectObj = null;
		}
	}

	protected virtual bool CheckTargetDead()
	{
		Player player = m_targetObject as Player;
		return player == null || player.isDead;
	}

	protected virtual StageObject SearchNearestTarget(Vector2 bulletPos, float searchRadius)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		float num = 3.40282347E+38f;
		float radius = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.radius;
		StageObject result = null;
		int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count;
		for (int i = 0; i < count; i++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[i] as Player;
			if (!player.isDead)
			{
				float num2 = Vector2.Distance(player.positionXZ, bulletPos);
				if (num2 <= radius + searchRadius && num2 <= num)
				{
					num = num2;
					result = player;
				}
			}
		}
		return result;
	}

	protected virtual float GetAttackStartRange()
	{
		return m_funnelData.attackRange;
	}

	protected virtual float GetFloatingHeight()
	{
		return m_funnelData.floatingHeight;
	}

	protected void SetAttackMode(Player.ATTACK_MODE attackMode)
	{
		m_attackMode = attackMode;
	}

	protected void SetExAtk(AtkAttribute atk)
	{
		m_exAtk = atk;
	}

	protected void SetSkillParam(SkillInfo.SkillParam param)
	{
		m_skillParam = param;
	}

	private void RequestFunction(Function func)
	{
		m_func = func;
		SetState(1);
	}

	private void SetState(int state)
	{
		m_state = state;
	}

	private void ForwardState()
	{
		m_state++;
	}

	private void BackState()
	{
		if (m_state > 0)
		{
			m_state--;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (!m_isDeleted && collider.get_gameObject().get_layer() == 14)
		{
			NotifyBroken(true);
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (!m_isDeleted && collider.get_gameObject().get_layer() == 14)
		{
			NotifyBroken(true);
		}
	}

	public int GetObservedID()
	{
		return observedID;
	}

	public void SetObservedID(int id)
	{
		observedID = id;
	}

	public void RegisterObserver()
	{
		if (!bulletObserverList.Contains(m_attacker))
		{
			bulletObserverList.Add(m_attacker);
			SetObservedID(m_attacker.GetObservedID());
			m_attacker.RegisterObservable(this);
		}
	}

	public void NotifyBroken(bool isSendOnlyOriginal = true)
	{
		for (int i = 0; i < bulletObserverList.Count; i++)
		{
			bulletObserverList[i].OnBreak(observedID, isSendOnlyOriginal);
		}
	}

	public void NotifyDestroy()
	{
		for (int i = 0; i < bulletObserverList.Count; i++)
		{
			bulletObserverList[i].OnBulletDestroy(observedID);
		}
	}

	public void ForceBreak()
	{
		RequestDestroy(true);
	}
}
