using UnityEngine;

public class AttackCannonball : MonoBehaviour
{
	public class InitParamCannonball
	{
		public StageObject attacker;

		public AttackInfo atkInfo;

		public Transform launchTrans;

		public Vector3 offsetPos = Vector3.get_zero();

		public Quaternion offsetRot = Quaternion.get_identity();

		public Quaternion shotRotation = Quaternion.get_identity();
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

	public AttackCannonball()
		: this()
	{
	}

	public void Initialize(InitParamCannonball initParam)
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
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
		if (data != null)
		{
			BulletData.BulletCannonball dataCannonball = bulletData.dataCannonball;
			if (dataCannonball != null)
			{
				m_attacker = initParam.attacker;
				m_cachedTransform = this.get_transform();
				m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
				Transform launchTrans = initParam.launchTrans;
				m_cachedTransform.set_position(launchTrans.get_position() + launchTrans.get_rotation() * initParam.offsetPos);
				m_cachedTransform.set_rotation(launchTrans.get_rotation() * initParam.offsetRot);
				m_cachedTransform.set_localScale(data.timeStartScale);
				existTime = data.appearTime;
				hasExistTime = (data.appearTime > 0f);
				float radius = data.radius;
				float height = 0f;
				Vector3 hitOffset = data.hitOffset;
				int ignoreLayerMask = 20736;
				GameObject val = new GameObject("CannonballAttackObject");
				CannonballAttackObject cannonballAttackObject = val.AddComponent<CannonballAttackObject>();
				cannonballAttackObject.Initialize(m_attacker, m_cachedTransform, m_atkInfo, hitOffset, Vector3.get_zero(), radius, height, 14);
				cannonballAttackObject.SetIgnoreLayerMask(ignoreLayerMask);
				cannonballAttackObject.SetOwner(this);
				m_cannonballAttackObj = cannonballAttackObject;
				gravityStartTime = dataCannonball.gravityStartTime;
				gravityRate = dataCannonball.gravityRate;
				cannonballRigidbody = val.GetComponent<Rigidbody>();
				cannonballTransform = val.get_transform();
				float speed = data.speed;
				cannonballRigidbody.set_velocity(initParam.shotRotation * Vector3.get_forward() * speed);
				Transform effect = EffectManager.GetEffect(data.effectName, cannonballAttackObject.get_transform());
				effect.set_localPosition(data.dispOffset);
				effect.set_localRotation(Quaternion.Euler(data.dispRotation));
				effect.set_localScale(Vector3.get_one());
				m_effectObj = effect.get_gameObject();
			}
		}
	}

	private void FixedUpdate()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (gravityStartTime >= 0f && m_cannonballAttackObj.GetTime() >= gravityStartTime)
		{
			cannonballRigidbody.AddForce(Physics.get_gravity() * gravityRate, 5);
			if (cannonballRigidbody.get_velocity() != Vector3.get_zero())
			{
				cannonballTransform.set_forward(cannonballRigidbody.get_velocity());
			}
		}
	}

	private void Update()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (hasExistTime)
		{
			existTimer += Time.get_deltaTime();
			if (existTimer > existTime)
			{
				Object.Destroy(this.get_gameObject());
			}
		}
		Vector3 position = cannonballTransform.get_position();
		if (position.y <= -1f)
		{
			Object.Destroy(this.get_gameObject());
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
		Object.Destroy(this.get_gameObject());
	}
}
