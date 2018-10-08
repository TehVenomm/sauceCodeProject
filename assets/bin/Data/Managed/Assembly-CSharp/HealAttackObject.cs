using System.Collections.Generic;
using UnityEngine;

public class HealAttackObject : IAttackCollider
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

	public HealAttackObject()
		: this()
	{
	}

	protected virtual string GetAttackInfoName()
	{
		return "sk_heal_atk";
	}

	public void Initialize(StageObject attacker, Transform parent, SkillInfo.SkillParam skillParam, Vector3 pos, Vector3 rot, float height, int attackLayer)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		_Initialize(attacker, parent, pos, rot, height, attackLayer, (float)skillParam.healHp, skillParam.tableData.skillRange);
	}

	public void Initialize(StageObject attacker, Transform parent, float healAtk, float radius)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		_Initialize(attacker, parent, Vector3.get_zero(), Vector3.get_zero(), 0f, 12, healAtk, radius);
	}

	private void _Initialize(StageObject attacker, Transform parent, Vector3 pos, Vector3 rot, float height, int attackLayer, float healAtk, float radius)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		m_player = (attacker as Player);
		this.get_gameObject().set_layer(attackLayer);
		AttackHitInfo attackHitInfo = m_player.FindAttackInfo(isDuplicate: this.isDuplicateAttackInfo, name: GetAttackInfoName(), fix_rate: true) as AttackHitInfo;
		attackHitInfo.atk.normal = healAtk;
		m_attacker = attacker;
		m_attackInfo = attackHitInfo;
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

	public void Destroy()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (m_rigidBody != null)
		{
			m_rigidBody.Sleep();
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

	protected virtual void Update()
	{
		m_timeCount += Time.get_deltaTime();
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
		if (m_rigidBody != null)
		{
			Object.Destroy(m_rigidBody);
			m_rigidBody = null;
		}
	}
}
