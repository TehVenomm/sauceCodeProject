using UnityEngine;

public class AttackCannonBeam : MonoBehaviour
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
			fixedTime += Time.fixedDeltaTime;
		}

		protected override void OnTriggerEnter(Collider collider)
		{
			hitLayer = collider.gameObject.layer;
			if (((1 << hitLayer) & ignoreLayerMask) == 0)
			{
				if (hitLayer == 11)
				{
					hitEnemy = collider.gameObject.GetComponent<Enemy>();
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

	public void Initialize(InitParamCannonBeam initParam)
	{
		if (initParam.atkInfo == null)
		{
			return;
		}
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
				m_cachedTransform = base.transform;
				m_cachedTransform.parent = (MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform);
				Transform launchTrans = initParam.launchTrans;
				m_cachedTransform.position = launchTrans.position;
				m_cachedTransform.rotation = launchTrans.rotation;
				m_cachedTransform.localScale = data.timeStartScale;
				m_existTime = data.appearTime;
				m_hasExistTime = (data.appearTime > 0f);
				float radius = data.radius;
				float capsuleHeight = data.capsuleHeight;
				Vector3 hitOffset = data.hitOffset;
				int ignoreLayerMask = 20736;
				CannonBeamAttackObject cannonBeamAttackObject = new GameObject("CannonBeamAttackObject").AddComponent<CannonBeamAttackObject>();
				cannonBeamAttackObject.Initialize(m_attacker, m_cachedTransform, m_atkInfo, hitOffset, Vector3.zero, radius, capsuleHeight, 14);
				cannonBeamAttackObject.SetIgnoreLayerMask(ignoreLayerMask);
				m_attackObj = cannonBeamAttackObject;
				Transform transform = m_effectTrans = EffectManager.GetEffect(data.effectName, launchTrans);
			}
		}
	}

	private void Update()
	{
		if (m_hasExistTime)
		{
			m_existTimer += Time.deltaTime;
			if (m_existTimer > m_existTime)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void OnDestroy()
	{
		Transform parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
		if (m_effectTrans != null)
		{
			m_effectTrans.parent = parent;
			EffectManager.ReleaseEffect(m_effectTrans.gameObject);
		}
	}
}
