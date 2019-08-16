using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionMine : MonoBehaviour, IAttackCollider
{
	public enum Function
	{
		NONE,
		MAIN,
		DELETE
	}

	public class InitParamActionMine
	{
		public StageObject attacker;

		public AttackInfo atkInfo;

		public Vector3 position = Vector3.get_zero();

		public Quaternion rotation = Quaternion.get_identity();

		public int randomSeed;

		public int id;
	}

	public enum REACTION_TYPE
	{
		NONE,
		EXPLODE,
		REFLECT,
		DIRECTION_CHANGE
	}

	public class ReflectBulletCondition : MonoBehaviour
	{
		public int actionMineID = -1;

		public int reflectCount;

		public ReflectBulletCondition()
			: this()
		{
		}
	}

	public const string ANIM_STATE_END = "END";

	private float m_timeCount;

	private BulletData.BulletActionMine m_mineData;

	private CapsuleCollider m_capsule;

	private StageObject m_attacker;

	private AttackInfo m_atkInfo;

	private AttackHitChecker m_attackHitChecker;

	private AttackColliderProcessor m_colliderProcessor;

	private Transform m_cachedTransform;

	private GameObject m_effectObj;

	private string m_landHitEffectName = string.Empty;

	private float m_aliveTime;

	private int m_effectDeleteAnimHash;

	private Animator m_effectDeleteAnimator;

	private Function m_func;

	private int m_state;

	private bool m_isDeleted;

	private float m_unbreakableTimer;

	private float m_actionCoolTime;

	private float m_actionCoolTimer;

	private Random rand;

	public int objId;

	public REACTION_TYPE reactionType;

	public int ignoreLayerMask;

	public GameObject hitObject;

	public int hitLayer;

	public int reflectCount;

	private StageObject fromObject;

	public string AttackInfoName => m_atkInfo.name;

	public AttackActionMine()
		: this()
	{
	}

	public void ResetRandomSeed(int seed)
	{
		rand = new Random(seed);
	}

	public void Initialize(InitParamActionMine initParam)
	{
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		m_timeCount = 0f;
		this.get_gameObject().set_layer(31);
		m_atkInfo = initParam.atkInfo;
		AttackHitInfo attackHitInfo = m_atkInfo as AttackHitInfo;
		if (attackHitInfo != null)
		{
			attackHitInfo.enableIdentityCheck = false;
		}
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletBase data = bulletData.data;
		if (data == null)
		{
			return;
		}
		BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
		if (dataActionMine != null)
		{
			m_actionCoolTime = dataActionMine.actionCoolTime;
			m_attacker = initParam.attacker;
			m_landHitEffectName = data.landHiteffectName;
			m_aliveTime = data.appearTime;
			m_unbreakableTimer = dataActionMine.unbrakableTime;
			m_mineData = dataActionMine;
			m_isDeleted = false;
			m_cachedTransform = this.get_transform();
			m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
			m_cachedTransform.set_position(initParam.position);
			m_cachedTransform.set_rotation(initParam.rotation);
			m_cachedTransform.set_localScale(data.timeStartScale);
			Transform effect = EffectManager.GetEffect(data.effectName, this.get_transform());
			effect.set_localPosition(data.dispOffset);
			effect.set_localRotation(Quaternion.Euler(data.dispRotation));
			effect.set_localScale(Vector3.get_one());
			m_effectObj = effect.get_gameObject();
			m_effectDeleteAnimator = m_effectObj.GetComponent<Animator>();
			m_capsule = this.get_gameObject().AddComponent<CapsuleCollider>();
			m_capsule.set_direction(2);
			m_capsule.set_radius(data.radius);
			m_capsule.set_height(0f);
			m_capsule.set_enabled(true);
			m_capsule.set_center(Vector3.get_zero());
			m_capsule.set_isTrigger(true);
			Rigidbody val = this.get_gameObject().AddComponent<Rigidbody>();
			val.set_useGravity(false);
			int num = 0;
			if (dataActionMine.isIgnoreHitEnemyAttack)
			{
				num |= 0x2000;
			}
			if (dataActionMine.isIgnoreHitEnemyMove)
			{
				num |= 0x400;
			}
			if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
			{
				m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(m_atkInfo, m_attacker, m_capsule, this);
				m_attackHitChecker = m_attacker.ReferenceAttackHitChecker();
			}
			rand = new Random(initParam.randomSeed);
			objId = initParam.id;
			if (!string.IsNullOrEmpty(m_mineData.appearEffectName))
			{
				Vector3 position = m_cachedTransform.get_position();
				float x = position.x;
				Vector3 position2 = m_cachedTransform.get_position();
				Vector3 pos = default(Vector3);
				pos._002Ector(x, 0.1f, position2.z);
				EffectManager.OneShot(m_mineData.appearEffectName, pos, m_cachedTransform.get_rotation(), is_priority: true);
			}
			RequestMain();
		}
	}

	private void Update()
	{
		m_timeCount += Time.get_deltaTime();
		switch (m_func)
		{
		case Function.MAIN:
			FuncMain();
			break;
		case Function.DELETE:
			FuncDelete();
			break;
		}
	}

	private void RequestMain()
	{
		RequestFunction(Function.MAIN);
	}

	private void FuncMain()
	{
		if (m_isDeleted)
		{
			return;
		}
		m_unbreakableTimer -= Time.get_deltaTime();
		m_actionCoolTimer -= Time.get_deltaTime();
		if (m_timeCount >= m_aliveTime)
		{
			RequestDestroy();
		}
		else
		{
			if (m_mineData.actionType != BulletData.BulletActionMine.ACTION_TYPE.REFLECT)
			{
				return;
			}
			switch (reactionType)
			{
			case REACTION_TYPE.REFLECT:
				if (m_actionCoolTimer <= 0f)
				{
					RequestReflect();
				}
				break;
			case REACTION_TYPE.DIRECTION_CHANGE:
				if (m_actionCoolTimer <= 0f)
				{
					RequestReflect(isCreate: false);
				}
				break;
			case REACTION_TYPE.EXPLODE:
			{
				if (!(hitObject != null) || !(m_unbreakableTimer <= 0f))
				{
					break;
				}
				bool isExplode = false;
				if (!IsAvoidExplode(hitLayer) && IsLayerExplode(hitLayer))
				{
					isExplode = true;
				}
				if (m_attacker != null)
				{
					Enemy enemy = m_attacker as Enemy;
					if (enemy != null && enemy.enemySender != null)
					{
						enemy.ActDestroyActionMine(objId, isExplode);
						enemy.enemySender.OnDestroyActionMine(objId, isExplode);
					}
				}
				break;
			}
			}
			ResetHit();
		}
	}

	public void RequestDestroy(bool isExplode = true)
	{
		if (m_func == Function.DELETE || m_isDeleted)
		{
			return;
		}
		RequestFunction(Function.DELETE);
		if (isExplode)
		{
			CreateExplosion();
			ResetHit();
		}
		if (m_effectDeleteAnimator == null)
		{
			Destroy();
			return;
		}
		string text = (!isExplode) ? "END" : string.Empty;
		if (string.IsNullOrEmpty(text))
		{
			Destroy();
			return;
		}
		m_effectDeleteAnimHash = Animator.StringToHash(text);
		if (m_effectDeleteAnimator.HasState(0, m_effectDeleteAnimHash))
		{
			m_effectDeleteAnimator.Play(m_effectDeleteAnimHash, 0, 0f);
		}
	}

	private void Destroy()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (m_isDeleted)
		{
			return;
		}
		m_isDeleted = true;
		if (!string.IsNullOrEmpty(m_landHitEffectName))
		{
			Transform effect = EffectManager.GetEffect(m_landHitEffectName);
			if (effect != null)
			{
				effect.set_position(m_cachedTransform.get_position());
				effect.set_rotation(m_cachedTransform.get_rotation());
			}
		}
		Enemy enemy = m_attacker as Enemy;
		if (enemy != null)
		{
			enemy.OnDestroyActionMine(this);
		}
		Object.Destroy(this.get_gameObject());
	}

	private AnimEventShot CreateExplosion()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return null;
		}
		BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
		if (dataActionMine == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		if (dataActionMine.explodeBullet == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.get_rotation();
		Vector3 position = m_cachedTransform.get_position();
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataActionMine.explodeBullet, m_attacker, m_atkInfo, position, rotation);
		if (animEventShot == null)
		{
			return null;
		}
		return animEventShot;
	}

	private void RequestReflect(bool isCreate = true)
	{
		m_actionCoolTimer = m_actionCoolTime;
		if (m_func == Function.DELETE || m_isDeleted)
		{
			return;
		}
		if (isCreate)
		{
			Enemy enemy = m_attacker as Enemy;
			if (enemy != null && fromObject != null && fromObject is Self)
			{
				int num = rand.Next(-1073741824, 1073741823);
				enemy.ActResetActionMineRandom(num);
				if (enemy.enemySender != null)
				{
					enemy.enemySender.OnCreateReflectBullet(objId, num);
				}
				CreateReflectBullet();
			}
		}
		else
		{
			ChangeBulletDirection();
		}
	}

	public AnimEventShot CreateReflectBullet()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.playerList == null)
		{
			return null;
		}
		Quaternion rot = m_cachedTransform.get_rotation();
		if (IsGetTarget(m_cachedTransform.get_position(), ref rot))
		{
			AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(m_mineData.actionBullet, m_attacker, m_atkInfo, m_cachedTransform.get_position(), rot);
			if (animEventShot == null)
			{
				return null;
			}
			ReflectBulletCondition reflectBulletCondition = animEventShot.get_gameObject().AddComponent<ReflectBulletCondition>();
			reflectBulletCondition.actionMineID = objId;
			if (!string.IsNullOrEmpty(m_mineData.actionEffectName1))
			{
				EffectManager.OneShot(m_mineData.actionEffectName1, m_cachedTransform.get_position() + m_mineData.actionEffectOffset, rot);
			}
			if (!string.IsNullOrEmpty(m_mineData.actionEffectName2))
			{
				EffectManager.OneShot(m_mineData.actionEffectName2, m_cachedTransform.get_position() + m_mineData.actionEffectOffset, rot);
			}
			return animEventShot;
		}
		return null;
	}

	private void ChangeBulletDirection()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Quaternion rot = hitObject.get_transform().get_rotation();
		if (!IsGetTarget(hitObject.get_transform().get_position(), ref rot))
		{
			return;
		}
		BulletControllerBase component = hitObject.GetComponent<BulletControllerBase>();
		if (component != null)
		{
			component.Initialize(m_mineData.actionBullet, null, component._transform.get_position(), rot);
			if (!string.IsNullOrEmpty(m_mineData.actionEffectName2))
			{
				EffectManager.OneShot(m_mineData.actionEffectName2, component._transform.get_position() + m_mineData.actionEffectOffset, rot);
			}
		}
	}

	private bool IsGetTarget(Vector3 nowPos, ref Quaternion rot)
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		List<StageObject> list = new List<StageObject>(MonoBehaviourSingleton<StageObjectManager>.I.playerList);
		list.RemoveAll(delegate(StageObject obj)
		{
			Player player = obj as Player;
			if (player != null)
			{
				return player.hp <= 0;
			}
			return false;
		});
		list.Sort((StageObject a, StageObject b) => a.id - b.id);
		if (list.Count < 1)
		{
			return false;
		}
		Enemy enemy = m_attacker as Enemy;
		List<AttackActionMine> list2 = new List<AttackActionMine>(enemy.GetActionMineList());
		list2.Remove(this);
		list2.RemoveAll((AttackActionMine x) => x == null);
		if (list2 == null || list2.Count < 1)
		{
			GetTargetPlayer(list.ToArray(), nowPos, ref rot);
			return true;
		}
		float num = (float)rand.NextDouble();
		if (num < m_mineData.targettingPlayerRate)
		{
			GetTargetPlayer(list.ToArray(), nowPos, ref rot);
		}
		else
		{
			GetTargetMine(list2.ToArray(), nowPos, ref rot);
		}
		return true;
	}

	private void GetTargetPlayer(StageObject[] players, Vector3 nowPos, ref Quaternion rot)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		int num = rand.Next(0, players.Length);
		StageObject stageObject = players[num];
		Vector3 val = stageObject._position - nowPos;
		rot = Quaternion.LookRotation(val);
	}

	private void GetTargetMine(AttackActionMine[] mines, Vector3 nowPos, ref Quaternion rot)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		int num = rand.Next(0, mines.Length);
		AttackActionMine attackActionMine = mines[num];
		rot = Quaternion.LookRotation(attackActionMine.m_cachedTransform.get_position() - nowPos);
	}

	private void FuncDelete()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		switch (m_state)
		{
		case 1:
			if (m_effectDeleteAnimator == null)
			{
				SetState(3);
			}
			else
			{
				ForwardState();
			}
			break;
		case 2:
		{
			AnimatorStateInfo currentAnimatorStateInfo = m_effectDeleteAnimator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
			{
				ForwardState();
			}
			break;
		}
		case 3:
			Destroy();
			ForwardState();
			break;
		}
	}

	private void OnDestroy()
	{
		if (m_effectObj != null)
		{
			EffectManager.ReleaseEffect(m_effectObj);
			m_effectObj = null;
		}
	}

	private void RequestFunction(Function func)
	{
		m_func = func;
		SetState(1);
	}

	private bool IsAvoidExplode(int layer)
	{
		if (layer == 8)
		{
			Player component = hitObject.GetComponent<Player>();
			if (!(component == null) && component.isActSpecialAction && component.attackMode == Player.ATTACK_MODE.SPEAR)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsLayerExplode(int layer)
	{
		switch (layer)
		{
		case 8:
		case 9:
		case 13:
		case 15:
		case 17:
		case 18:
		case 21:
			return true;
		default:
			return false;
		}
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

	public void SetIgnoreLayerMask(int mask)
	{
		ignoreLayerMask = mask;
	}

	public void ResetHit()
	{
		hitObject = null;
		hitLayer = 0;
		reactionType = REACTION_TYPE.NONE;
		fromObject = null;
		m_capsule.set_enabled(true);
	}

	private void OnTriggerEnter(Collider collider)
	{
		hitObject = collider.get_gameObject();
		hitLayer = hitObject.get_layer();
		if (((1 << hitLayer) & ignoreLayerMask) > 0 || (hitLayer == 8 && hitObject.GetComponent<DangerRader>() != null))
		{
			return;
		}
		HealAttackObject component = collider.get_gameObject().GetComponent<HealAttackObject>();
		if (component != null)
		{
			return;
		}
		IAttackCollider component2 = hitObject.GetComponent<IAttackCollider>();
		if (component2 != null)
		{
			AttackInfo attackInfo = component2.GetAttackInfo();
			fromObject = component2.GetFromObject();
			if (attackInfo != null)
			{
				if (attackInfo.isSkillReference)
				{
					reactionType = REACTION_TYPE.REFLECT;
					collider.set_enabled(false);
					BulletObject component3 = collider.GetComponent<BulletObject>();
					if (component3 != null)
					{
						component3.OnDestroy();
					}
					return;
				}
				ReflectBulletCondition component4 = collider.GetComponent<ReflectBulletCondition>();
				if (component4 != null)
				{
					if (component4.actionMineID != objId)
					{
						reactionType = REACTION_TYPE.DIRECTION_CHANGE;
						component4.actionMineID = objId;
						component4.reflectCount++;
						reflectCount++;
					}
					return;
				}
			}
		}
		reactionType = REACTION_TYPE.EXPLODE;
		m_capsule.set_enabled(false);
	}
}
