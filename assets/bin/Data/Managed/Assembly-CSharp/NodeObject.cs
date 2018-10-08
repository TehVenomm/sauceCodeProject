using UnityEngine;

public class NodeObject
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
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
