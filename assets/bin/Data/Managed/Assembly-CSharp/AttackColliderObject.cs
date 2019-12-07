using System.Collections.Generic;
using UnityEngine;

public class AttackColliderObject : MonoBehaviour, IAttackCollider
{
	private StageObject m_attacker;

	private CapsuleCollider m_capsule;

	private Rigidbody m_rigidBody;

	private AttackInfo m_attackInfo;

	private AttackColliderProcessor m_colliderProcessor;

	protected AttackHitChecker m_attackHitChecker;

	private float m_timeCount;

	public int UniqueID
	{
		get;
		set;
	}

	public void Initialize(StageObject attacker, Transform parent, AttackInfo atkInfo, Vector3 pos, Vector3 rot, float radius, float height, int attackLayer)
	{
		base.gameObject.layer = attackLayer;
		m_attacker = attacker;
		m_attackInfo = atkInfo;
		Transform transform = base.transform;
		transform.parent = parent;
		transform.localEulerAngles = rot;
		transform.localPosition = transform.localRotation * pos;
		transform.localScale = Vector3.one;
		m_capsule.direction = 2;
		m_capsule.radius = radius;
		m_capsule.height = height;
		m_capsule.enabled = true;
		m_capsule.center = new Vector3(0f, 0f, height * 0.5f);
		m_capsule.isTrigger = true;
		m_timeCount = 0f;
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_attackInfo, attacker, m_capsule, this);
			m_attackHitChecker = attacker.ReferenceAttackHitChecker();
		}
	}

	public void Initialize(StageObject attacker, Transform parent, AttackInfo atkInfo, Vector3 pos, Vector3 rot, float radius, float height, int direction, Vector3 center, int attackLayer)
	{
		base.gameObject.layer = attackLayer;
		m_attacker = attacker;
		m_attackInfo = atkInfo;
		Transform transform = base.transform;
		transform.parent = parent;
		transform.localEulerAngles = rot;
		transform.localPosition = pos;
		transform.localScale = Vector3.one;
		m_capsule.direction = direction;
		m_capsule.radius = radius;
		m_capsule.height = height;
		m_capsule.enabled = true;
		m_capsule.center = center;
		m_capsule.isTrigger = true;
		m_timeCount = 0f;
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_attackInfo, attacker, m_capsule, this);
			m_attackHitChecker = attacker.ReferenceAttackHitChecker();
		}
	}

	public void InitializeForExAtkCollider(StageObject attacker, Transform parent, AttackInfo atkInfo, Vector3 pos, Vector3 rot, float radius, float height, int attackLayer)
	{
		base.gameObject.layer = attackLayer;
		m_attacker = attacker;
		m_attackInfo = atkInfo;
		Transform transform = base.transform;
		transform.parent = parent;
		transform.localEulerAngles = rot;
		transform.localPosition = pos;
		transform.localScale = Vector3.one;
		m_capsule.direction = 2;
		m_capsule.radius = radius;
		m_capsule.height = height;
		m_capsule.enabled = true;
		m_capsule.center = new Vector3(0f, 0f, height * 0.5f);
		m_capsule.isTrigger = true;
		m_timeCount = 0f;
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_attackInfo, attacker, m_capsule, this);
			m_attackHitChecker = attacker.ReferenceAttackHitChecker();
		}
		if (m_colliderProcessor != null && m_attackInfo != null)
		{
			AttackHitInfo attackHitInfo = m_attackInfo as AttackHitInfo;
			if (attackHitInfo != null && attackHitInfo.isValidTriggerStay)
			{
				m_colliderProcessor.ValidTriggerStay();
				m_colliderProcessor.ValidMultiHitInterval();
			}
		}
	}

	public virtual void Destroy()
	{
		if (m_rigidBody != null)
		{
			m_rigidBody.Sleep();
		}
		if (m_attackInfo != null)
		{
			BulletData bulletData = m_attackInfo.bulletData;
			if (bulletData != null && !string.IsNullOrEmpty(bulletData.data.landHiteffectName))
			{
				Transform effect = EffectManager.GetEffect(bulletData.data.landHiteffectName);
				if (effect != null)
				{
					effect.position = base.transform.position;
					effect.rotation = base.transform.rotation;
				}
			}
		}
		Object.Destroy(base.gameObject);
	}

	protected void Awake()
	{
		m_capsule = base.gameObject.AddComponent<CapsuleCollider>();
		m_rigidBody = base.gameObject.GetComponent<Rigidbody>();
		if (m_rigidBody == null)
		{
			m_rigidBody = base.gameObject.AddComponent<Rigidbody>();
		}
		m_rigidBody.useGravity = false;
	}

	private void Update()
	{
		m_timeCount += Time.deltaTime;
	}

	protected virtual void OnTriggerEnter(Collider collider)
	{
		if (m_colliderProcessor != null)
		{
			m_colliderProcessor.OnTriggerEnter(collider);
		}
	}

	protected virtual void OnTriggerStay(Collider collider)
	{
		if (m_colliderProcessor != null)
		{
			m_colliderProcessor.OnTriggerStay(collider);
		}
	}

	protected virtual void OnTriggerExit(Collider collider)
	{
		if (m_colliderProcessor != null)
		{
			m_colliderProcessor.OnTriggerExit(collider);
		}
	}

	protected void ActivateOwnCollider()
	{
		if (m_capsule != null)
		{
			m_capsule.enabled = true;
		}
	}

	protected void DeactivateOwnCollider()
	{
		if (m_capsule != null)
		{
			m_capsule.enabled = false;
		}
	}

	public void ValidTriggerStay()
	{
		if (m_colliderProcessor != null)
		{
			m_colliderProcessor.ValidTriggerStay();
		}
	}

	public virtual void OnHitTrigger(Collider to_collider, StageObject to_object)
	{
	}

	public virtual float GetTime()
	{
		return m_timeCount;
	}

	public virtual bool IsEnable()
	{
		return true;
	}

	public virtual void SortHitStackList(List<AttackHitColliderProcessor.HitResult> stack_list)
	{
	}

	public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
	{
		Vector3 result = from_collider.bounds.center;
		Character character = m_attacker as Character;
		if (character != null && character.rootNode != null)
		{
			result = character.rootNode.position;
		}
		return result;
	}

	public virtual bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		if (m_attackHitChecker != null && !m_attackHitChecker.CheckHitAttack(info, to_collider, to_object))
		{
			return false;
		}
		return true;
	}

	public virtual void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		if (m_attackHitChecker != null)
		{
			m_attackHitChecker.OnHitAttack(info, hit_param);
		}
	}

	public AttackInfo GetAttackInfo()
	{
		return m_colliderProcessor.attackInfo;
	}

	public StageObject GetFromObject()
	{
		return m_colliderProcessor.fromObject;
	}

	public void DetachRigidbody()
	{
		if (m_rigidBody != null)
		{
			Object.Destroy(m_rigidBody);
			m_rigidBody = null;
		}
	}
}
