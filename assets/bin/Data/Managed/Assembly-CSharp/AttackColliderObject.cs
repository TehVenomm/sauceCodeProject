using System.Collections.Generic;
using UnityEngine;

public class AttackColliderObject : IAttackCollider
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

	public AttackColliderObject()
		: this()
	{
	}

	public void Initialize(StageObject attacker, Transform parent, AttackInfo atkInfo, Vector3 pos, Vector3 rot, float radius, float height, int attackLayer)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().set_layer(attackLayer);
		m_attacker = attacker;
		m_attackInfo = atkInfo;
		Transform val = this.get_transform();
		val.set_parent(parent);
		val.set_localEulerAngles(rot);
		val.set_localPosition(val.get_localRotation() * pos);
		val.set_localScale(Vector3.get_one());
		m_capsule.set_direction(2);
		m_capsule.set_radius(radius);
		m_capsule.set_height(height);
		m_capsule.set_enabled(true);
		m_capsule.set_center(new Vector3(0f, 0f, height * 0.5f));
		m_capsule.set_isTrigger(true);
		m_timeCount = 0f;
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_attackInfo, attacker, m_capsule, this, Player.ATTACK_MODE.NONE, null);
			m_attackHitChecker = attacker.ReferenceAttackHitChecker();
		}
	}

	public void InitializeForExAtkCollider(StageObject attacker, Transform parent, AttackInfo atkInfo, Vector3 pos, Vector3 rot, float radius, float height, int attackLayer)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().set_layer(attackLayer);
		m_attacker = attacker;
		m_attackInfo = atkInfo;
		Transform val = this.get_transform();
		val.set_parent(parent);
		val.set_localEulerAngles(rot);
		val.set_localPosition(pos);
		val.set_localScale(Vector3.get_one());
		m_capsule.set_direction(2);
		m_capsule.set_radius(radius);
		m_capsule.set_height(height);
		m_capsule.set_enabled(true);
		m_capsule.set_center(new Vector3(0f, 0f, height * 0.5f));
		m_capsule.set_isTrigger(true);
		m_timeCount = 0f;
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_attackInfo, attacker, m_capsule, this, Player.ATTACK_MODE.NONE, null);
			m_attackHitChecker = attacker.ReferenceAttackHitChecker();
		}
	}

	public virtual void Destroy()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (m_rigidBody != null)
		{
			m_rigidBody.Sleep();
		}
		if (m_attackInfo != null)
		{
			BulletData bulletData = m_attackInfo.bulletData;
			if (bulletData != null && !string.IsNullOrEmpty(bulletData.data.landHiteffectName))
			{
				Transform effect = EffectManager.GetEffect(bulletData.data.landHiteffectName, null);
				if (effect != null)
				{
					effect.set_position(this.get_transform().get_position());
					effect.set_rotation(this.get_transform().get_rotation());
				}
			}
		}
		Object.Destroy(this.get_gameObject());
	}

	protected void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		m_capsule = this.get_gameObject().AddComponent<CapsuleCollider>();
		m_rigidBody = this.get_gameObject().AddComponent<Rigidbody>();
		m_rigidBody.set_useGravity(false);
	}

	private void Update()
	{
		m_timeCount += Time.get_deltaTime();
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
			m_capsule.set_enabled(true);
		}
	}

	protected void DeactivateOwnCollider()
	{
		if (m_capsule != null)
		{
			m_capsule.set_enabled(false);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Bounds bounds = from_collider.get_bounds();
		Vector3 result = bounds.get_center();
		Character character = m_attacker as Character;
		if (character != null && character.rootNode != null)
		{
			result = character.rootNode.get_position();
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
