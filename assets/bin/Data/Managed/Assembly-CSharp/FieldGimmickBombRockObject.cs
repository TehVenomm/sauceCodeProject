using System.Collections;
using UnityEngine;

public class FieldGimmickBombRockObject : StageObject, IFieldGimmickObject
{
	public const string EFFECT_NAME_BOMBROCK_EXPLOSION = "ef_btl_enemy_explosion_01_02";

	public const string EFFECT_NAME_BOMBROCK_BASE = "ef_btl_bg_bombrock_01";

	private const string NAME_ATTACK_HIT_INFO = "bombrock";

	private const float TIME_REMAIN_DESTROY = 0.7f;

	private const float RADIUS_ATTACK_COLLIDER = 2f;

	private const float HEIGHT_ATTACK_COLLIDER = 1f;

	public const int SE_EXPLOSION = 30000102;

	private int m_id;

	private FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;

	private Transform m_modelTrans;

	private Transform m_effectBase;

	private SphereCollider m_sphereCollider;

	private float m_attackRate;

	public void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		m_id = (int)pointData.pointID;
		m_gimmickType = pointData.gimmickType;
		m_attackRate = pointData.value1;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get((uint)m_gimmickType);
			if (loadObject != null)
			{
				m_modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, GetTransform());
			}
		}
	}

	public void RequestDestroy()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			EffectManager.GetEffect("ef_btl_enemy_explosion_01_02", base._transform).localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.PlayOneShotSE(30000102, _position);
		}
		m_modelTrans.gameObject.SetActive(value: false);
		BombRockAttackObject bombRockAttackObject = new GameObject("BombRockAttackToEnemy").AddComponent<BombRockAttackObject>();
		AttackInfo attackInfo = MonoBehaviourSingleton<StageObjectManager>.I.self.GetAttackInfos().Find((AttackInfo info) => info.name == "bombrock");
		if (attackInfo != null)
		{
			AttackHitInfo attackHitInfo = attackInfo as AttackHitInfo;
			attackHitInfo.atk.normal = m_attackRate;
			bombRockAttackObject.Initialize(MonoBehaviourSingleton<StageObjectManager>.I.self, base._transform, attackHitInfo, Vector3.zero, Vector3.zero, 2f, 1f, 14);
		}
		new GameObject("BombRockAttackToSelf").AddComponent<BombRockAttackObject>().Initialize(atkInfo: new AttackHitInfo
		{
			attackType = AttackHitInfo.ATTACK_TYPE.BOMBROCK,
			toPlayer = 
			{
				reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW,
				reactionBlowForce = 100f,
				reactionBlowAngle = 20f
			}
		}, attacker: this, parent: base._transform, pos: Vector3.zero, rot: Vector3.zero, radius: 2f, height: 1f, attackLayer: 15);
		if ((object)m_effectBase != null)
		{
			Object.Destroy(m_effectBase.gameObject);
			m_effectBase = null;
		}
		StartCoroutine(ProcessDestroy());
	}

	private IEnumerator ProcessDestroy()
	{
		yield return new WaitForSeconds(0.7f);
		Object.Destroy(base.gameObject);
	}

	public int GetId()
	{
		return m_id;
	}

	public Transform GetTransform()
	{
		return base._transform;
	}

	public string GetObjectName()
	{
		return "BombRock";
	}

	public void SetTransform(Transform trans)
	{
		m_modelTrans = trans;
	}

	public float GetTargetRadius()
	{
		return 0f;
	}

	public float GetTargetSqrRadius()
	{
		return 0f;
	}

	public void UpdateTargetMarker(bool isNear)
	{
	}

	public bool IsSearchableNearest()
	{
		return true;
	}

	protected override void Awake()
	{
		base.Awake();
		Utility.SetLayerWithChildren(base.transform, 18);
		if ((object)m_sphereCollider == null)
		{
			m_sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			m_sphereCollider.center = new Vector3(0f, 0f, 0f);
			m_sphereCollider.radius = 1.2f;
		}
	}

	protected override void Start()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			m_effectBase = EffectManager.GetEffect("ef_btl_bg_bombrock_01", base._transform);
		}
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (!(from_object is Enemy))
		{
			return false;
		}
		return base.IsValidAttackedHit(from_object);
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		base.OnAttackedHitFix(status);
		m_sphereCollider.enabled = false;
		RequestDestroy();
	}
}
