using System.Collections.Generic;
using UnityEngine;

public class AttackFunnelBit : MonoBehaviour, IBulletObservable
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

	public virtual void Initialize(StageObject attacker, AttackInfo atkInfo, StageObject targetObj, Transform launchTrans, Vector3 offsetPos, Quaternion offsetRot)
	{
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
		m_cachedTransform = base.transform;
		m_cachedTransform.parent = ((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
		m_cachedTransform.position = launchTrans.position + launchTrans.rotation * offsetPos;
		m_cachedTransform.rotation = launchTrans.rotation * offsetRot;
		m_cachedTransform.localScale = bulletData.data.timeStartScale;
		Transform effect = EffectManager.GetEffect(bulletData.data.effectName, base.transform);
		effect.localPosition = bulletData.data.dispOffset;
		effect.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
		effect.localScale = Vector3.one;
		m_effectObj = effect.gameObject;
		m_effectAnimator = m_effectObj.GetComponent<Animator>();
		RegisterObserver();
		if ((Object)targetObj != (Object)null)
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
		if (!(_radius <= 0f))
		{
			m_radius = _radius;
			m_capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			m_capsuleCollider.center = new Vector3(0f, 0f, 0f);
			m_capsuleCollider.direction = 2;
			m_capsuleCollider.isTrigger = true;
			m_capsuleCollider.radius = m_radius;
			m_capsuleCollider.height = m_radius * 2f;
			Utility.SetLayerWithChildren(base.transform, 31);
			SetTargetPoint();
		}
	}

	private void SetTargetPoint()
	{
		m_targetPoint = base.gameObject.AddComponent<TargetPoint>();
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
		if (!IsDeleted)
		{
			m_aliveTimer -= Time.deltaTime;
			if (m_aliveTimer <= 0f)
			{
				RequestDestroy(false);
			}
			else if (!((Object)m_targetObject == (Object)null))
			{
				switch (m_state)
				{
				case 1:
				{
					Vector3 position = m_targetObject.transform.position;
					position.y = GetFloatingHeight();
					LookAtTarget(position);
					Vector3 forward = m_cachedTransform.forward;
					Vector3 vector = m_cachedTransform.position + forward * (m_moveSpeed * Time.deltaTime);
					m_cachedTransform.position = vector;
					if (Vector3.Distance(position, vector) <= GetAttackStartRange())
					{
						m_attackIntervalTimer = m_funnelData.attackInterval;
						ForwardState();
					}
					break;
				}
				case 2:
					RotateAroundTarget();
					LookAtTarget(m_targetObject.transform.position);
					m_attackIntervalTimer -= Time.deltaTime;
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
		BulletData.BulletFunnel funnelData = m_funnelData;
		float floatingHeight = GetFloatingHeight();
		Vector3 position = m_targetObject.transform.position;
		position.y = floatingHeight;
		Vector3 position2 = m_cachedTransform.position;
		position2.y = floatingHeight;
		Vector3 vector = position2 - position;
		vector.Normalize();
		vector *= GetAttackStartRange();
		vector = Quaternion.AngleAxis(funnelData.rotateAngle, Vector3.up) * vector;
		Vector3 a = position + vector - m_cachedTransform.position;
		m_cachedTransform.position = position2 + a * Time.deltaTime;
	}

	private void LookAtTarget(Vector3 targetPos)
	{
		Vector3 forward = m_cachedTransform.forward;
		Vector3 position = m_cachedTransform.position;
		Vector3 vector = targetPos - position;
		vector.Normalize();
		float num = m_aimAngleSpeed * Time.deltaTime;
		float num2 = Vector3.Dot(forward, vector);
		float num3 = Mathf.Acos(num2);
		if (num2 < 1f && num < num3 && num3 >= 0.00174532924f)
		{
			float num4 = num / num3;
			Quaternion rotation;
			if (num4 >= 1f)
			{
				rotation = Quaternion.LookRotation(vector);
			}
			else
			{
				Quaternion a = Quaternion.LookRotation(forward);
				Quaternion b = Quaternion.LookRotation(vector);
				rotation = Quaternion.Slerp(a, b, num4);
			}
			m_cachedTransform.rotation = rotation;
		}
		else
		{
			m_cachedTransform.rotation = Quaternion.LookRotation(vector);
		}
	}

	private AnimEventShot CreateBullet()
	{
		BulletData bulletData = m_atkInfo.bulletData;
		if ((Object)bulletData == (Object)null)
		{
			return null;
		}
		BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
		if (dataFunnel == null)
		{
			return null;
		}
		if ((Object)m_attacker == (Object)null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.rotation;
		Vector3 pos = m_cachedTransform.position + rotation * m_funnelData.offsetPosition;
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataFunnel.bitBullet, m_attacker, m_atkInfo, pos, rotation, m_exAtk, m_attackMode, m_skillParam);
		if ((Object)animEventShot == (Object)null)
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
		if (!IsDeleted)
		{
			m_aliveTimer -= Time.deltaTime;
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
					Vector3 vector = cachedTransform.position += cachedTransform.forward * (m_moveSpeed * Time.deltaTime);
					float searchRange = m_funnelData.searchRange;
					if (!(searchRange <= 0f) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
					{
						Vector2 zero = Vector2.zero;
						zero.x = vector.x;
						zero.y = vector.z;
						StageObject stageObject = SearchNearestTarget(zero, searchRange);
						if ((Object)stageObject != (Object)null)
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
			if ((Object)m_effectAnimator == (Object)null)
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
					Debug.LogWarning("Not found delete animation!!");
					Destroy();
				}
			}
		}
	}

	private void FuncDelete()
	{
		switch (m_state)
		{
		case 1:
			if ((Object)m_effectAnimator == (Object)null)
			{
				ForwardState();
			}
			else if (m_effectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				ForwardState();
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
		if (!IsDeleted)
		{
			m_isDeleted = true;
			if (!string.IsNullOrEmpty(m_landHitEffectName))
			{
				Transform effect = EffectManager.GetEffect(m_landHitEffectName, null);
				if ((Object)effect != (Object)null)
				{
					effect.position = m_cachedTransform.position;
					effect.rotation = m_cachedTransform.rotation;
				}
			}
			if ((Object)m_attacker != (Object)null)
			{
				Enemy enemy = m_attacker as Enemy;
				if ((Object)enemy != (Object)null)
				{
					enemy.OnDestroyFunnel(this);
				}
				m_attacker = null;
			}
			NotifyDestroy();
			Object.Destroy(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		if ((Object)m_effectObj != (Object)null)
		{
			EffectManager.ReleaseEffect(m_effectObj, true, false);
			m_effectObj = null;
		}
	}

	protected virtual bool CheckTargetDead()
	{
		Player player = m_targetObject as Player;
		return (Object)player == (Object)null || player.isDead;
	}

	protected virtual StageObject SearchNearestTarget(Vector2 bulletPos, float searchRadius)
	{
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
		if (!m_isDeleted && collider.gameObject.layer == 14)
		{
			NotifyBroken(true);
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		if (!m_isDeleted && collider.gameObject.layer == 14)
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
