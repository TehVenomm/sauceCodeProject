using System.Collections.Generic;
using UnityEngine;

public class StageObjectRader : MonoBehaviour
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

	protected virtual void Awake()
	{
		objects = new List<CatchStageObject>();
		_transform = base.transform;
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
		if ((Object)_collider == (Object)null)
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.center = new Vector3(0f, 0f, 0f);
			_collider = sphereCollider;
		}
		if ((Object)_collider != (Object)null)
		{
			_collider.isTrigger = true;
			if ((Object)_rigidbody == (Object)null)
			{
				_rigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			_rigidbody.isKinematic = true;
		}
	}

	protected virtual void Start()
	{
		stageObject = base.gameObject.GetComponentInParent<StageObject>();
	}

	public void SetRadius(float radius)
	{
		SphereCollider sphereCollider = _collider as SphereCollider;
		if ((Object)sphereCollider != (Object)null)
		{
			sphereCollider.radius = radius;
		}
	}

	public CatchStageObject Find(Collider collider)
	{
		RemoveFromDestroyedCollider();
		return objects.Find((CatchStageObject o) => (Object)o.collider == (Object)collider);
	}

	public Enemy FindEnemy()
	{
		RemoveFromDestroyedCollider();
		CatchStageObject catchStageObject = objects.Find((CatchStageObject o) => o.obj is Enemy && (Object)o.collider != (Object)null && (Object)o.bullet == (Object)null);
		return (catchStageObject == null) ? null : (catchStageObject.obj as Enemy);
	}

	public BulletObject FindEnemyBullet()
	{
		RemoveFromDestroyedCollider();
		return objects.Find((CatchStageObject o) => o.obj is Enemy && (Object)o.collider != (Object)null && (Object)o.bullet != (Object)null)?.bullet;
	}

	protected virtual void RemoveFromDestroyedCollider()
	{
		objects.RemoveAll((CatchStageObject o) => (Object)o.collider == (Object)null);
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
		if (!((Object)_collider == (Object)null) && _collider.enabled && !((Object)this.stageObject == (Object)null) && !((Object)collider.gameObject == (Object)base.gameObject))
		{
			StageObject stageObject = null;
			BulletObject component = collider.gameObject.GetComponent<BulletObject>();
			if ((Object)component != (Object)null)
			{
				stageObject = component.stageObject;
			}
			else
			{
				if (collider.isTrigger)
				{
					return;
				}
				stageObject = collider.gameObject.GetComponentInParent<StageObject>();
			}
			if (!((Object)stageObject == (Object)null) && !((Object)stageObject == (Object)this.stageObject))
			{
				Add(stageObject, collider, component);
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (!((Object)collider.gameObject == (Object)base.gameObject))
		{
			StageObject componentInParent = collider.gameObject.GetComponentInParent<StageObject>();
			if (!((Object)componentInParent == (Object)null))
			{
				Remove(collider);
			}
		}
	}
}
