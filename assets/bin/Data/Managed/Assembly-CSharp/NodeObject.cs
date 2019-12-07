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
		if (_collider != null && triggerColliderIsRequired())
		{
			if (_rigidbody == null)
			{
				_rigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			_rigidbody.isKinematic = true;
		}
	}

	protected Collider GetCollider()
	{
		Collider component = GetComponent<Collider>();
		if (component == null)
		{
			return null;
		}
		bool flag = triggerColliderIsRequired();
		if (component.isTrigger == flag)
		{
			return component;
		}
		Collider[] components = base.gameObject.GetComponents<Collider>();
		foreach (Collider collider in components)
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
		if (_collider != null && _collider.enabled)
		{
			timeCount += Time.deltaTime;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!(_collider == null) && _collider.enabled && !collider.isTrigger && !(stageObject == null) && !(collider.gameObject == base.gameObject))
		{
			StageObject componentInParent = collider.gameObject.GetComponentInParent<StageObject>();
			if (!(componentInParent == null) && !(componentInParent == stageObject))
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
		if (_collider != null && _collider.isTrigger)
		{
			_collider.enabled = enable;
		}
		if (enable && init)
		{
			timeCount = 0f;
		}
	}
}
