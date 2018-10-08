using UnityEngine;

public class UIScrollOutSideObject
{
	private Transform target;

	private Transform _transform;

	public bool enaleUpdate
	{
		get;
		private set;
	}

	public UIScrollOutSideObject()
		: this()
	{
	}

	public void SetActive(bool is_active)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		enaleUpdate = is_active;
		this.get_gameObject().SetActive(is_active);
	}

	public void SetTargetTransform(Transform _t)
	{
		target = _t;
		enaleUpdate = true;
	}

	public void OnDestroy()
	{
		_transform = null;
		target = null;
	}

	private void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
	}

	private void LateUpdate()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (enaleUpdate && _transform != null && target != null)
		{
			this.get_transform().set_position(target.get_position());
		}
	}
}
