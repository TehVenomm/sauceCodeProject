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

	public NodeObject()
		: this()
	{
	}

	protected virtual bool triggerColliderIsRequired()
	{
		return true;
	}

	protected virtual void Awake()
	{
		_transform = this.get_transform();
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = GetCollider();
		if (_collider != null && triggerColliderIsRequired())
		{
			if (_rigidbody == null)
			{
				_rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
			}
			_rigidbody.set_isKinematic(true);
		}
	}

	protected Collider GetCollider()
	{
		Collider component = this.GetComponent<Collider>();
		if (component == null)
		{
			return null;
		}
		bool flag = triggerColliderIsRequired();
		if (component.get_isTrigger() == flag)
		{
			return component;
		}
		Collider[] components = this.get_gameObject().GetComponents<Collider>();
		Collider[] array = components;
		foreach (Collider val in array)
		{
			if (val.get_isTrigger() == flag)
			{
				return val;
			}
		}
		return null;
	}

	protected virtual void Start()
	{
		if (triggerColliderIsRequired())
		{
			stageObject = this.get_gameObject().GetComponentInParent<StageObject>();
		}
	}

	protected virtual void Update()
	{
		if (_collider != null && _collider.get_enabled())
		{
			timeCount += Time.get_deltaTime();
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!(_collider == null) && _collider.get_enabled() && !collider.get_isTrigger() && !(stageObject == null) && !(collider.get_gameObject() == this.get_gameObject()))
		{
			StageObject componentInParent = collider.get_gameObject().GetComponentInParent<StageObject>();
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
		if (_collider != null && _collider.get_isTrigger())
		{
			_collider.set_enabled(enable);
		}
		if (enable && init)
		{
			timeCount = 0f;
		}
	}
}
