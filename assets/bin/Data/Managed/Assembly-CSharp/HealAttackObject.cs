using System.Collections.Generic;
using UnityEngine;

public class HealAttackObject : MonoBehaviour, IAttackCollider
{
	private Player m_player;

	private StageObject m_attacker;

	private CapsuleCollider m_capsule;

	private Rigidbody m_rigidBody;

	protected AttackInfo m_attackInfo;

	protected AttackColliderProcessor m_colliderProcessor;

	protected AttackHitChecker m_attackHitChecker;

	protected float m_timeCount;

	protected virtual bool isDuplicateAttackInfo => false;

	public int UniqueID
	{
		get;
		set;
	}

	protected virtual string GetAttackInfoName()
	{
		return "sk_heal_atk";
	}

	public void Initialize(StageObject attacker, Transform parent, SkillInfo.SkillParam skillParam, Vector3 pos, Vector3 rot, float height, int attackLayer)
	{
		_Initialize(attacker, parent, pos, rot, height, attackLayer, (float)skillParam.healHp, skillParam.tableData.skillRange);
	}

	public void Initialize(StageObject attacker, Transform parent, float healAtk, float radius)
	{
		_Initialize(attacker, parent, Vector3.zero, Vector3.zero, 0f, 12, healAtk, radius);
	}

	private void _Initialize(StageObject attacker, Transform parent, Vector3 pos, Vector3 rot, float height, int attackLayer, float healAtk, float radius)
	{
		m_player = (attacker as Player);
		base.gameObject.layer = attackLayer;
		AttackHitInfo attackHitInfo = m_player.FindAttackInfo(isDuplicate: this.isDuplicateAttackInfo, name: GetAttackInfoName(), fix_rate: true) as AttackHitInfo;
		attackHitInfo.atk.normal = healAtk;
		m_attacker = attacker;
		m_attackInfo = attackHitInfo;
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
			m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_attackInfo, attacker, m_capsule, this, Player.ATTACK_MODE.NONE, null);
			m_attackHitChecker = attacker.ReferenceAttackHitChecker();
		}
	}

	public void Destroy()
	{
		if ((Object)m_rigidBody != (Object)null)
		{
			m_rigidBody.Sleep();
		}
		Object.Destroy(base.gameObject);
	}

	protected void Awake()
	{
		m_capsule = base.gameObject.AddComponent<CapsuleCollider>();
		m_rigidBody = base.gameObject.AddComponent<Rigidbody>();
		m_rigidBody.useGravity = false;
	}

	protected virtual void Update()
	{
		m_timeCount += Time.deltaTime;
		if (!m_player.isActSkillAction)
		{
			Destroy();
		}
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
		if ((Object)m_capsule != (Object)null)
		{
			m_capsule.enabled = true;
		}
	}

	protected void DeactivateOwnCollider()
	{
		if ((Object)m_capsule != (Object)null)
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
		if ((Object)character != (Object)null && (Object)character.rootNode != (Object)null)
		{
			result = character.rootNode.position;
		}
		return result;
	}

	public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		if (m_attackHitChecker != null && !m_attackHitChecker.CheckHitAttack(info, to_collider, to_object))
		{
			return false;
		}
		if (info.attackType == AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK && to_object is Enemy)
		{
			Enemy enemy = to_object as Enemy;
			if (enemy.healDamageRate <= 0f)
			{
				return false;
			}
		}
		return true;
	}

	public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
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
		if ((Object)m_rigidBody != (Object)null)
		{
			Object.Destroy(m_rigidBody);
			m_rigidBody = null;
		}
	}
}
