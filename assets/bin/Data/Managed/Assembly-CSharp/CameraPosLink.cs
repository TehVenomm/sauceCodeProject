using UnityEngine;

public class CameraPosLink
{
	public Camera targetCamera;

	public bool y0;

	private Transform _transform;

	public CameraPosLink()
		: this()
	{
	}

	private void Start()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		if (targetCamera == null)
		{
			targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
		_transform = this.get_transform();
	}

	private void LateUpdate()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetCamera == null))
		{
			Vector3 position = targetCamera.get_transform().get_position();
			if (y0)
			{
				position.y = 0f;
			}
			_transform.set_position(position);
		}
	}
}
