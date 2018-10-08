using UnityEngine;

public class UIScrollOutSideObject : MonoBehaviour
{
	private Transform target;

	private Transform _transform;

	public bool enaleUpdate
	{
		get;
		private set;
	}

	public void SetActive(bool is_active)
	{
		enaleUpdate = is_active;
		base.gameObject.SetActive(is_active);
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
		_transform = base.transform;
	}

	private void LateUpdate()
	{
		if (enaleUpdate && (Object)_transform != (Object)null && (Object)target != (Object)null)
		{
			base.transform.position = target.position;
		}
	}
}
