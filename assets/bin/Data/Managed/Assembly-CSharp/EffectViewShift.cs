using UnityEngine;

public class EffectViewShift : MonoBehaviour
{
	public float shiftValue = -0.25f;

	public Transform targetCamera;

	private Transform _transform;

	private Vector3 defaultLocalPos;

	public EffectViewShift()
		: this()
	{
	}

	private void Start()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		defaultLocalPos = _transform.get_localPosition();
	}

	private void LateUpdate()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		if (targetCamera == null)
		{
			UpdateTargetCamera();
			if (targetCamera == null)
			{
				return;
			}
		}
		_transform.set_localPosition(defaultLocalPos);
		Vector3 position = _transform.get_position();
		Transform transform = _transform;
		Vector3 val = position - targetCamera.get_position();
		transform.set_position(val.get_normalized() * shiftValue + position);
	}

	private void UpdateTargetCamera()
	{
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			if (AppMain.isInitialized)
			{
				targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			}
		}
		else if (targetCamera == null && Camera.get_main() != null)
		{
			targetCamera = Camera.get_main().get_transform();
		}
	}
}
