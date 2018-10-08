using UnityEngine;

public class BulletControllerBase
{
	protected BulletObject bulletObject;

	public Transform _transform
	{
		get;
		protected set;
	}

	public Rigidbody _rigidbody
	{
		get;
		protected set;
	}

	public Collider _collider
	{
		get;
		protected set;
	}

	protected float speed
	{
		get;
		private set;
	}

	protected float initialVelocity
	{
		get;
		private set;
	}

	public float timeCount
	{
		get;
		protected set;
	}

	public SkillInfo.SkillParam bulletSkillInfoParam
	{
		get;
		protected set;
	}

	public BulletControllerBase()
		: this()
	{
	}

	public void RegisterBulletObject(BulletObject b)
	{
		bulletObject = b;
	}

	protected virtual void Awake()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		timeCount = 0f;
		bulletSkillInfoParam = null;
		_transform = this.get_transform();
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = this.GetComponent<Collider>();
	}

	public virtual void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		speed = bullet.data.speed;
		initialVelocity = bullet.data.speed;
		bulletSkillInfoParam = skillParam;
		Vector3 forward = Vector3.get_forward();
		forward = rot * forward;
		forward *= speed;
		_transform.set_position(pos);
		_transform.LookAt(pos + forward);
		_rigidbody.set_velocity(forward);
	}

	public virtual void Update()
	{
		timeCount += Time.get_deltaTime();
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void OnHit(Collider collider)
	{
	}

	public virtual void OnHitStay(Collider collider)
	{
	}

	public virtual void OnLandHit()
	{
	}

	public virtual void OnShot()
	{
	}

	public virtual bool IsHit(Collider collider)
	{
		return true;
	}

	public virtual bool IsBreak(Collider collider)
	{
		return false;
	}

	protected void SetVelocity(float v)
	{
		speed = v;
	}
}
