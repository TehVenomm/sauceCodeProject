using UnityEngine;

public class NodeObject : MonoBehaviour
{
	protected float timeCount;

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

	protected virtual bool triggerColliderIsRequired()
	{
		return true;
	}

	protected virtual void Awake()
	{
		_transform = base.transform;
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetCollider();
		if ((Object)_collider != (Object)null && triggerColliderIsRequired())
		{
			if ((Object)_rigidbody == (Object)null)
			{
				_rigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			_rigidbody.isKinematic = true;
		}
	}

	protected Collider GetCollider()
	{
		Collider component = GetComponent<Collider>();
		if ((Object)component == (Object)null)
		{
			return null;
		}
		bool flag = triggerColliderIsRequired();
		if (component.isTrigger == flag)
		{
			return component;
		}
		Collider[] components = base.gameObject.GetComponents<Collider>();
		Collider[] array = components;
		foreach (Collider collider in array)
		{
			if (collider.isTrigger == flag)
			{
				return collider;
			}
		}
		return null;
	}

	protected virtual void Start()
	{
		if (triggerColliderIsRequired())
		{
			stageObject = base.gameObject.GetComponentInParent<StageObject>();
		}
	}

	protected virtual void Update()
	{
		if ((Object)_collider != (Object)null && _collider.enabled)
		{
			timeCount += Time.deltaTime;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!((Object)_collider == (Object)null) && _collider.enabled && !collider.isTrigger && !((Object)stageObject == (Object)null) && !((Object)collider.gameObject == (Object)base.gameObject))
		{
			StageObject componentInParent = collider.gameObject.GetComponentInParent<StageObject>();
			if (!((Object)componentInParent == (Object)null) && !((Object)componentInParent == (Object)stageObject))
			{
				OnHitTrigger(collider, componentInParent);
			}
		}
	}

	protected virtual void OnHitTrigger(Collider to_collider, StageObject to_object)
	{
	}

	public void SetEnableTrigger(bool enable, bool init = true)
	{
		if ((Object)_collider != (Object)null && _collider.isTrigger)
		{
			_collider.enabled = enable;
		}
		if (enable && init)
		{
			timeCount = 0f;
		}
	}
}
