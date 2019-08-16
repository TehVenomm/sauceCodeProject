using UnityEngine;

public class CameraPosLink : MonoBehaviour
{
	public Camera targetCamera;

	public bool y0;

	public float cameraOffsetZ;

	private Transform _transform;

	public CameraPosLink()
		: this()
	{
	}

	private void Start()
	{
		if (targetCamera == null)
		{
			targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
		_transform = this.get_transform();
	}

	private void LateUpdate()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetCamera == null))
		{
			Vector3 val = targetCamera.get_transform().get_position();
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.target != null)
			{
				Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.target.get_transform().get_position();
				Vector3 val2 = position - val;
				val2.Normalize();
				val2 *= cameraOffsetZ;
				val += val2;
			}
			if (y0)
			{
				val.y = 0f;
			}
			_transform.set_position(val);
		}
	}
}
