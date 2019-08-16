using UnityEngine;

public class CircleShadow : MonoBehaviour
{
	private Transform _transform;

	private Transform animTransform;

	public CircleShadow()
		: this()
	{
	}

	private void Awake()
	{
		_transform = this.get_transform();
	}

	private void LateUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = _transform.get_position();
		if (animTransform != null)
		{
			position = animTransform.get_position();
		}
		position.y = 0.005f;
		_transform.set_position(position);
	}

	public void setAnimTransform(Transform target)
	{
		animTransform = target;
	}
}
