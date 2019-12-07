using UnityEngine;

public class EffectViewShift : MonoBehaviour
{
	public float shiftValue = -0.25f;

	public Transform targetCamera;

	private Transform _transform;

	private Vector3 defaultLocalPos;

	private void Start()
	{
		_transform = base.transform;
		defaultLocalPos = _transform.localPosition;
	}

	private void LateUpdate()
	{
		if (targetCamera == null)
		{
			UpdateTargetCamera();
			if (targetCamera == null)
			{
				return;
			}
		}
		_transform.localPosition = defaultLocalPos;
		Vector3 position = _transform.position;
		_transform.position = (position - targetCamera.position).normalized * shiftValue + position;
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
		else if (targetCamera == null && Camera.main != null)
		{
			targetCamera = Camera.main.transform;
		}
	}
}
