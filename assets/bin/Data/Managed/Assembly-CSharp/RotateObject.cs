using UnityEngine;

public class RotateObject : MonoBehaviour
{
	[SerializeField]
	private Vector3 rotateSpeed = Vector3.zero;

	private Transform _transform;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void Update()
	{
		if (!(_transform == null))
		{
			_transform.Rotate(rotateSpeed * Time.deltaTime);
		}
	}
}
