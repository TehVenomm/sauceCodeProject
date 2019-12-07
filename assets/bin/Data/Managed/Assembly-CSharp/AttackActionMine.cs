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

		public Vector3 position = Vector3.zero;

		public Quaternion rotation = Quaternion.identity;

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

	private System.Random rand;

	public int objId;

	public REACTION_TYPE reactionType;

	public int ignoreLayerMask;

	public GameObject hitObject;

	public int hitLayer;

	public int reflectCount;

	private StageObject fromObject;

	public string AttackInfoName => m_atkInfo.name;

	public void ResetRandomSeed(int seed)
	{
		rand = new System.Random(seed);
	}

	public void Initialize(InitParamActionMine initParam)
	{
		m_timeCount = 0f;
		base.gameObject.layer = 31;
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
			m_cachedTransform = base.transform;
			m_cachedTransform.parent = (MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform);
			m_cachedTransform.position = initParam.position;
			m_cachedTransform.rotation = initParam.rotation;
			m_cachedTransform.localScale = data.timeStartScale;
			Transform effect = EffectManager.GetEffect(data.effectName, base.transform);
			if (effect != null)
			{
				effect.localPosition = data.dispOffset;
				effect.localRotation = Quaternion.Euler(data.dispRotation);
				effect.localScale = Vector3.one;
				m_effectObj = effect.gameObject;
				m_effectDeleteAnimator = m_effectObj.GetComponent<Animator>();
			}
			m_capsule = base.gameObject.AddComponent<CapsuleCollider>();
			m_capsule.direction = 2;
			m_capsule.radius = data.radius;
			m_capsule.height = 0f;
			m_capsule.enabled = true;
			m_capsule.center = Vector3.zero;
			m_capsule.isTrigger = true;
			base.gameObject.AddComponent<Rigidbody>().useGravity = false;
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
			rand = new System.Random(initParam.randomSeed);
			objId = initParam.id;
			if (!string.IsNullOrEmpty(m_mineData.appearEffectName))
			{
				EffectManager.OneShot(pos: new Vector3(m_cachedTransform.position.x, 0.1f, m_cachedTransform.position.z), effect_name: m_mineData.appearEffectName, rot: m_cachedTransform.rotation, is_priority: true);
			}
			RequestMain();
		}
	}

	private void Update()
	{
		m_timeCount += Time.deltaTime;
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
		m_unbreakableTimer -= Time.deltaTime;
		m_actionCoolTimer -= Time.deltaTime;
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
		string text = isExplode ? string.Empty : "END";
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
				effect.position = m_cachedTransform.position;
				effect.rotation = m_cachedTransform.rotation;
			}
		}
		Enemy enemy = m_attacker as Enemy;
		if (enemy != null)
		{
			enemy.OnDestroyActionMine(this);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private AnimEventShot CreateExplosion()
	{
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
		Quaternion rotation = m_cachedTransform.rotation;
		Vector3 position = m_cachedTransform.position;
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
		if (m_atkInfo.bulletData == null)
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
		Quaternion rot = m_cachedTransform.rotation;
		if (IsGetTarget(m_cachedTransform.position, ref rot))
		{
			AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(m_mineData.actionBullet, m_attacker, m_atkInfo, m_cachedTransform.position, rot);
			if (animEventShot == null)
			{
				return null;
			}
			animEventShot.gameObject.AddComponent<ReflectBulletCondition>().actionMineID = objId;
			if (!string.IsNullOrEmpty(m_mineData.actionEffectName1))
			{
				EffectManager.OneShot(m_mineData.actionEffectName1, m_cachedTransform.position + m_mineData.actionEffectOffset, rot);
			}
			if (!string.IsNullOrEmpty(m_mineData.actionEffectName2))
			{
				EffectManager.OneShot(m_mineData.actionEffectName2, m_cachedTransform.position + m_mineData.actionEffectOffset, rot);
			}
			return animEventShot;
		}
		return null;
	}

	private void ChangeBulletDirection()
	{
		Quaternion rot = hitObject.transform.rotation;
		if (!IsGetTarget(hitObject.transform.position, ref rot))
		{
			return;
		}
		BulletControllerBase component = hitObject.GetComponent<BulletControllerBase>();
		if (component != null)
		{
			component.Initialize(m_mineData.actionBullet, null, component._transform.position, rot);
			if (!string.IsNullOrEmpty(m_mineData.actionEffectName2))
			{
				EffectManager.OneShot(m_mineData.actionEffectName2, component._transform.position + m_mineData.actionEffectOffset, rot);
			}
		}
	}

	private bool IsGetTarget(Vector3 nowPos, ref Quaternion rot)
	{
		List<StageObject> list = new List<StageObject>(MonoBehaviourSingleton<StageObjectManager>.I.playerList);
		list.RemoveAll(delegate(StageObject obj)
		{
			Player player = obj as Player;
			return player != null && player.hp <= 0;
		});
		list.Sort((StageObject a, StageObject b) => a.id - b.id);
		if (list.Count < 1)
		{
			return false;
		}
		List<AttackActionMine> list2 = new List<AttackActionMine>((m_attacker as Enemy).GetActionMineList());
		list2.Remove(this);
		list2.RemoveAll((AttackActionMine x) => x == null);
		if (list2 == null || list2.Count < 1)
		{
			GetTargetPlayer(list.ToArray(), nowPos, ref rot);
			return true;
		}
		if ((float)rand.NextDouble() < m_mineData.targettingPlayerRate)
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
		int num = rand.Next(0, players.Length);
		Vector3 forward = players[num]._position - nowPos;
		rot = Quaternion.LookRotation(forward);
	}

	private void GetTargetMine(AttackActionMine[] mines, Vector3 nowPos, ref Quaternion rot)
	{
		int num = rand.Next(0, mines.Length);
		AttackActionMine attackActionMine = mines[num];
		rot = Quaternion.LookRotation(attackActionMine.m_cachedTransform.position - nowPos);
	}

	private void FuncDelete()
	{
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
			if (m_effectDeleteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				ForwardState();
			}
			break;
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
		Vector3 result = from_collider.bounds.center;
		Character character = m_attacker as Character;
		if (character != null && character.rootNode != null)
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
		if (info.attackType == AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK && to_object is Enemy && (to_object as Enemy).healDamageRate <= 0f)
		{
			return false;
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
		m_capsule.enabled = true;
	}

	private void OnTriggerEnter(Collider collider)
	{
		hitObject = collider.gameObject;
		hitLayer = hitObject.layer;
		if (((1 << hitLayer) & ignoreLayerMask) > 0 || (hitLayer == 8 && hitObject.GetComponent<DangerRader>() != null) || collider.gameObject.GetComponent<HealAttackObject>() != null)
		{
			return;
		}
		IAttackCollider component = hitObject.GetComponent<IAttackCollider>();
		if (component != null)
		{
			AttackInfo attackInfo = component.GetAttackInfo();
			fromObject = component.GetFromObject();
			if (attackInfo != null)
			{
				if (attackInfo.isSkillReference)
				{
					reactionType = REACTION_TYPE.REFLECT;
					collider.enabled = false;
					BulletObject component2 = collider.GetComponent<BulletObject>();
					if (component2 != null)
					{
						component2.OnDestroy();
					}
					return;
				}
				ReflectBulletCondition component3 = collider.GetComponent<ReflectBulletCondition>();
				if (component3 != null)
				{
					if (component3.actionMineID != objId)
					{
						reactionType = REACTION_TYPE.DIRECTION_CHANGE;
						component3.actionMineID = objId;
						component3.reflectCount++;
						reflectCount++;
					}
					return;
				}
			}
		}
		reactionType = REACTION_TYPE.EXPLODE;
		m_capsule.enabled = false;
	}
}
