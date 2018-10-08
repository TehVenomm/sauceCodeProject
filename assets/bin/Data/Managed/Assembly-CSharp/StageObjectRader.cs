using System.Collections.Generic;
using UnityEngine;

public class StageObjectRader
{
	public class CatchStageObject
	{
		public StageObject obj;

		public Collider collider;

		public BulletObject bullet;
	}

	public List<CatchStageObject> objects
	{
		get;
		protected set;
	}

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

	public StageObject stageObject
	{
		get;
		protected set;
	}

	public StageObjectRader()
		: this()
	{
	}

	protected virtual void Awake()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		objects = new List<CatchStageObject>();
		_transform = this.get_transform();
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = this.GetComponent<Collider>();
		if (_collider == null)
		{
			SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
			val.set_center(new Vector3(0f, 0f, 0f));
			_collider = val;
		}
		if (_collider != null)
		{
			_collider.set_isTrigger(true);
			if (_rigidbody == null)
			{
				_rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
			}
			_rigidbody.set_isKinematic(true);
		}
	}

	protected virtual void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		stageObject = this.get_gameObject().GetComponentInParent<StageObject>();
	}

	public void SetRadius(float radius)
	{
		SphereCollider val = _collider as SphereCollider;
		if (val != null)
		{
			val.set_radius(radius);
		}
	}

	public CatchStageObject Find(Collider collider)
	{
		RemoveFromDestroyedCollider();
		return objects.Find((CatchStageObject o) => o.collider == collider);
	}

	public Enemy FindEnemy()
	{
		RemoveFromDestroyedCollider();
		CatchStageObject catchStageObject = objects.Find((CatchStageObject o) => o.obj is Enemy && o.collider != null && o.bullet == null);
		return (catchStageObject == null) ? null : (catchStageObject.obj as Enemy);
	}

	public BulletObject FindEnemyBullet()
	{
		RemoveFromDestroyedCollider();
		return objects.Find((CatchStageObject o) => o.obj is Enemy && o.collider != null && o.bullet != null)?.bullet;
	}

	protected virtual void RemoveFromDestroyedCollider()
	{
		objects.RemoveAll((CatchStageObject o) => o.collider == null);
	}

	protected virtual void Add(StageObject obj, Collider collider, BulletObject bullet)
	{
		CatchStageObject catchStageObject = Find(collider);
		if (catchStageObject == null)
		{
			CatchStageObject catchStageObject2 = new CatchStageObject();
			catchStageObject2.obj = obj;
			catchStageObject2.collider = collider;
			catchStageObject2.bullet = bullet;
			catchStageObject = catchStageObject2;
			objects.Add(catchStageObject);
		}
	}

	protected virtual void Remove(Collider collider)
	{
		CatchStageObject catchStageObject = Find(collider);
		if (catchStageObject != null)
		{
			objects.Remove(catchStageObject);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (!(_collider == null) && _collider.get_enabled() && !(this.stageObject == null) && !(collider.get_gameObject() == this.get_gameObject()))
		{
			StageObject stageObject = null;
			BulletObject component = collider.get_gameObject().GetComponent<BulletObject>();
			if (component != null)
			{
				stageObject = component.stageObject;
			}
			else
			{
				if (collider.get_isTrigger())
				{
					return;
				}
				stageObject = collider.get_gameObject().GetComponentInParent<StageObject>();
			}
			if (!(stageObject == null) && !(stageObject == this.stageObject))
			{
				Add(stageObject, collider, component);
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!(collider.get_gameObject() == this.get_gameObject()))
		{
			StageObject componentInParent = collider.get_gameObject().GetComponentInParent<StageObject>();
			if (!(componentInParent == null))
			{
				Remove(collider);
			}
		}
	}
}
