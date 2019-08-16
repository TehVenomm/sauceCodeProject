using UnityEngine;

public class RotateObject : MonoBehaviour
{
	[SerializeField]
	private Vector3 rotateSpeed;

	private Transform _transform;

	public RotateObject()
		: this()
	{
	}

	private void Awake()
	{
		_transform = this.get_transform();
	}

	private void Update()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (!(_transform == null))
		{
			_transform.Rotate(rotateSpeed * Time.get_deltaTime());
		}
	}
}
