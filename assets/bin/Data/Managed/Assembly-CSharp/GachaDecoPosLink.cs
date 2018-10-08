using UnityEngine;

public class GachaDecoPosLink : MonoBehaviour
{
	public Transform target;

	public float limitX;

	private Transform _transform;

	private void Awake()
	{
		_transform = base.transform;
		if ((Object)target == (Object)null)
		{
			base.enabled = false;
		}
	}

	private void LateUpdate()
	{
		_transform.position = target.position;
		Vector3 localPosition = _transform.localPosition;
		if (localPosition.x > limitX)
		{
			localPosition.x = limitX;
			_transform.localPosition = localPosition;
		}
	}
}
