using UnityEngine;

public class AttackCannonBeam
{
	public class InitParamCannonBeam
	{
		public StageObject attacker;

		public AttackInfo atkInfo;

		public Transform launchTrans;
	}

	public class CannonBeamAttackObject : AttackColliderObject
	{
		public int ignoreLayerMask;

		private float fixedTime;

		public bool isHit
		{
			get;
			private set;
		}

		public int hitLayer
		{
			get;
			private set;
		}

		public Enemy hitEnemy
		{
			get;
			private set;
		}

		public void SetIgnoreLayerMask(int mask)
		{
			ignoreLayerMask = mask;
		}

		public override float GetTime()
		{
			return fixedTime;
		}

		private void FixedUpdate()
		{
			fixedTime += Time.get_fixedDeltaTime();
		}

		protected override void OnTriggerEnter(Collider collider)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			hitLayer = collider.get_gameObject().get_layer();
			if (((1 << hitLayer) & ignoreLayerMask) == 0)
			{
				if (hitLayer == 11)
				{
					hitEnemy = collider.get_gameObject().GetComponent<Enemy>();
				}
				isHit = true;
				base.OnTriggerEnter(collider);
				DeactivateOwnCollider();
				Destroy();
			}
		}
	}

	private StageObject m_attacker;

	private AttackInfo m_atkInfo;

	private Transform m_cachedTransform;

	private bool m_hasExistTime;

	private float m_existTimer;

	private float m_existTime;

	private Transform m_effectTrans;

	private GameObject m_effectObj;

	private CannonBeamAttackObject m_attackObj;

	public AttackCannonBeam()
		: this()
	{
	}

	public void Initialize(InitParamCannonBeam initParam)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		if (initParam.atkInfo != null)
		{
			m_atkInfo = initParam.atkInfo;
			AttackHitInfo attackHitInfo = m_atkInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				attackHitInfo.enableIdentityCheck = false;
			}
			BulletData bulletData = m_atkInfo.bulletData;
			if (!(bulletData == null))
			{
				BulletData.BulletBase data = bulletData.data;
				if (data != null)
				{
					m_attacker = initParam.attacker;
					m_cachedTransform = this.get_transform();
					m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
					Transform launchTrans = initParam.launchTrans;
					m_cachedTransform.set_position(launchTrans.get_position());
					m_cachedTransform.set_rotation(launchTrans.get_rotation());
					m_cachedTransform.set_localScale(data.timeStartScale);
					m_existTime = data.appearTime;
					m_hasExistTime = (data.appearTime > 0f);
					float radius = data.radius;
					float capsuleHeight = data.capsuleHeight;
					Vector3 hitOffset = data.hitOffset;
					int ignoreLayerMask = 20736;
					GameObject val = new GameObject("CannonBeamAttackObject");
					CannonBeamAttackObject cannonBeamAttackObject = val.AddComponent<CannonBeamAttackObject>();
					cannonBeamAttackObject.Initialize(m_attacker, m_cachedTransform, m_atkInfo, hitOffset, Vector3.get_zero(), radius, capsuleHeight, 14);
					cannonBeamAttackObject.SetIgnoreLayerMask(ignoreLayerMask);
					m_attackObj = cannonBeamAttackObject;
					Transform val2 = m_effectTrans = EffectManager.GetEffect(data.effectName, launchTrans);
				}
			}
		}
	}

	private void Update()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (m_hasExistTime)
		{
			m_existTimer += Time.get_deltaTime();
			if (m_existTimer > m_existTime)
			{
				Object.Destroy(this.get_gameObject());
			}
		}
	}

	private void OnDestroy()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		Transform val = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
		if (m_effectTrans != null)
		{
			m_effectTrans.set_parent(val);
			EffectManager.ReleaseEffect(m_effectTrans.get_gameObject(), true, false);
		}
	}
}
