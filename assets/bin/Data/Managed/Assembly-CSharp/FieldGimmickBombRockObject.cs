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
				m_modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, GetTransform(), -1);
			}
		}
	}

	public void RequestDestroy()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			Transform effect = EffectManager.GetEffect("ef_btl_enemy_explosion_01_02", base._transform);
			effect.set_localScale(new Vector3(0.8f, 0.8f, 0.8f));
		}
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.PlayOneShotSE(30000102, _position);
		}
		m_modelTrans.get_gameObject().SetActive(false);
		GameObject val = new GameObject("BombRockAttackToEnemy");
		BombRockAttackObject bombRockAttackObject = val.AddComponent<BombRockAttackObject>();
		AttackInfo attackInfo = MonoBehaviourSingleton<StageObjectManager>.I.self.GetAttackInfos().Find((AttackInfo info) => info.name == "bombrock");
		if (attackInfo != null)
		{
			AttackHitInfo attackHitInfo = attackInfo as AttackHitInfo;
			attackHitInfo.atk.normal = m_attackRate;
			bombRockAttackObject.Initialize(MonoBehaviourSingleton<StageObjectManager>.I.self, base._transform, attackHitInfo, Vector3.get_zero(), Vector3.get_zero(), 2f, 1f, 14);
		}
		GameObject val2 = new GameObject("BombRockAttackToSelf");
		BombRockAttackObject bombRockAttackObject2 = val2.AddComponent<BombRockAttackObject>();
		AttackHitInfo attackHitInfo2 = new AttackHitInfo();
		attackHitInfo2.attackType = AttackHitInfo.ATTACK_TYPE.BOMBROCK;
		attackHitInfo2.toPlayer.reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW;
		attackHitInfo2.toPlayer.reactionBlowForce = 100f;
		attackHitInfo2.toPlayer.reactionBlowAngle = 20f;
		bombRockAttackObject2.Initialize(this, base._transform, attackHitInfo2, Vector3.get_zero(), Vector3.get_zero(), 2f, 1f, 15);
		if (!object.ReferenceEquals(m_effectBase, null))
		{
			Object.Destroy(m_effectBase.get_gameObject());
			m_effectBase = null;
		}
		this.StartCoroutine(ProcessDestroy());
	}

	private IEnumerator ProcessDestroy()
	{
		yield return (object)new WaitForSeconds(0.7f);
		Object.Destroy(this.get_gameObject());
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

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		Utility.SetLayerWithChildren(this.get_transform(), 18);
		if (object.ReferenceEquals(m_sphereCollider, null))
		{
			m_sphereCollider = this.get_gameObject().AddComponent<SphereCollider>();
			m_sphereCollider.set_center(new Vector3(0f, 0f, 0f));
			m_sphereCollider.set_radius(1.2f);
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
		m_sphereCollider.set_enabled(false);
		RequestDestroy();
	}
}
