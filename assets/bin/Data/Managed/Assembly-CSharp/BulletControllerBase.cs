using UnityEngine;

public class BulletControllerBase : MonoBehaviour
{
	protected BulletObject bulletObject;

	protected StageObject fromObject;

	protected StageObject targetObject;

	public Transform _transform
	{
		get;
		protected set;
	}

	public Vector3 _position
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

	public void RegisterBulletObject(BulletObject b)
	{
		bulletObject = b;
	}

	public virtual void RegisterFromObject(StageObject obj)
	{
		fromObject = obj;
	}

	public virtual void RegisterTargetObject(StageObject obj)
	{
		targetObject = obj;
	}

	protected virtual void Awake()
	{
		timeCount = 0f;
		bulletSkillInfoParam = null;
		_transform = base.transform;
		_position = base.transform.position;
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
	}

	public virtual void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		speed = bullet.data.speed;
		initialVelocity = bullet.data.speed;
		bulletSkillInfoParam = skillParam;
		Vector3 forward = Vector3.forward;
		forward = rot * forward;
		forward *= speed;
		_transform.position = pos;
		_transform.LookAt(pos + forward);
		_rigidbody.velocity = forward;
	}

	public virtual void PostInitialize()
	{
	}

	public virtual void DestroyBulletObject()
	{
	}

	public virtual void Update()
	{
		timeCount += Time.deltaTime;
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
