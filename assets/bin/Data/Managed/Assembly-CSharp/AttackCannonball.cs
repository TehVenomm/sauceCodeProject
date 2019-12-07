using UnityEngine;

public class AttackCannonball : MonoBehaviour
{
	public class InitParamCannonball
	{
		public StageObject attacker;

		public AttackInfo atkInfo;

		public Transform launchTrans;

		public Vector3 offsetPos = Vector3.zero;

		public Quaternion offsetRot = Quaternion.identity;

		public Quaternion shotRotation = Quaternion.identity;
	}

	public const string EFFECT_NAME_HIT_BASIS = "ef_btl_magibullet_landing_01";

	public const string EFFECT_NAME_HIT_CRITICAL = "ef_btl_magibullet_landing_02";

	public const string EFFECT_NAME_HIT_NOSHIELD = "ef_btl_magibullet_landing_03";

	public const string EFFECT_NAME_SHOT = "ef_btl_magibullet_shot_01";

	public const string EFFECT_NAME_BREAK_SHIELD = "ef_btl_goldbird_aura_01_01";

	public const string EFFECT_NAME_GUIDE_TAP = "ef_btl_cannon_tap";

	private StageObject m_attacker;

	private AttackInfo m_atkInfo;

	private Transform m_cachedTransform;

	private GameObject m_effectObj;

	private CannonballAttackObject m_cannonballAttackObj;

	private Rigidbody cannonballRigidbody;

	private Transform cannonballTransform;

	private float gravityStartTime;

	private float gravityRate;

	private bool hasExistTime;

	private float existTimer;

	private float existTime;

	public void Initialize(InitParamCannonball initParam)
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
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletBase data = bulletData.data;
		if (data == null)
		{
			return;
		}
		BulletData.BulletCannonball dataCannonball = bulletData.dataCannonball;
		if (dataCannonball != null)
		{
			m_attacker = initParam.attacker;
			m_cachedTransform = base.transform;
			m_cachedTransform.parent = (MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform);
			Transform launchTrans = initParam.launchTrans;
			m_cachedTransform.position = launchTrans.position + launchTrans.rotation * initParam.offsetPos;
			m_cachedTransform.rotation = launchTrans.rotation * initParam.offsetRot;
			m_cachedTransform.localScale = data.timeStartScale;
			existTime = data.appearTime;
			hasExistTime = (data.appearTime > 0f);
			float radius = data.radius;
			float height = 0f;
			Vector3 hitOffset = data.hitOffset;
			int ignoreLayerMask = 20736;
			GameObject gameObject = new GameObject("CannonballAttackObject");
			CannonballAttackObject cannonballAttackObject = gameObject.AddComponent<CannonballAttackObject>();
			cannonballAttackObject.Initialize(m_attacker, m_cachedTransform, m_atkInfo, hitOffset, Vector3.zero, radius, height, 14);
			cannonballAttackObject.SetIgnoreLayerMask(ignoreLayerMask);
			cannonballAttackObject.SetOwner(this);
			m_cannonballAttackObj = cannonballAttackObject;
			gravityStartTime = dataCannonball.gravityStartTime;
			gravityRate = dataCannonball.gravityRate;
			cannonballRigidbody = gameObject.GetComponent<Rigidbody>();
			cannonballTransform = gameObject.transform;
			float speed = data.speed;
			cannonballRigidbody.velocity = initParam.shotRotation * Vector3.forward * speed;
			Transform effect = EffectManager.GetEffect(data.effectName, cannonballAttackObject.transform);
			if (effect != null)
			{
				effect.localPosition = data.dispOffset;
				effect.localRotation = Quaternion.Euler(data.dispRotation);
				effect.localScale = Vector3.one;
				m_effectObj = effect.gameObject;
			}
		}
	}

	private void FixedUpdate()
	{
		if (gravityStartTime >= 0f && m_cannonballAttackObj.GetTime() >= gravityStartTime)
		{
			cannonballRigidbody.AddForce(Physics.gravity * gravityRate, ForceMode.Acceleration);
			if (cannonballRigidbody.velocity != Vector3.zero)
			{
				cannonballTransform.forward = cannonballRigidbody.velocity;
			}
		}
	}

	private void Update()
	{
		if (hasExistTime)
		{
			existTimer += Time.deltaTime;
			if (existTimer > existTime)
			{
				Object.Destroy(base.gameObject);
			}
		}
		if (cannonballTransform.position.y <= -1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		if (!(m_effectObj == null))
		{
			EffectManager.ReleaseEffect(m_effectObj);
			m_effectObj = null;
		}
	}

	public void OnHit()
	{
		Object.Destroy(base.gameObject);
	}
}
