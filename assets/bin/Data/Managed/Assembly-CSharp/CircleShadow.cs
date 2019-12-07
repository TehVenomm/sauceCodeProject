using UnityEngine;

public class CircleShadow : MonoBehaviour
{
	private Transform _transform;

	private Transform animTransform;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void LateUpdate()
	{
		Vector3 position = _transform.position;
		if (animTransform != null)
		{
			position = animTransform.position;
		}
		position.y = 0.005f;
		_transform.position = position;
	}

	public void setAnimTransform(Transform target)
	{
		animTransform = target;
	}
}
