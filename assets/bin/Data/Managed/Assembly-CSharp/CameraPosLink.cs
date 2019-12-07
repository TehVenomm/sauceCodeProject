using UnityEngine;

public class CameraPosLink : MonoBehaviour
{
	public Camera targetCamera;

	public bool y0;

	public float cameraOffsetZ;

	private Transform _transform;

	private void Start()
	{
		if (targetCamera == null)
		{
			targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
		_transform = base.transform;
	}

	private void LateUpdate()
	{
		if (!(targetCamera == null))
		{
			Vector3 position = targetCamera.transform.position;
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.target != null)
			{
				Vector3 vector = MonoBehaviourSingleton<InGameCameraManager>.I.target.transform.position - position;
				vector.Normalize();
				vector *= cameraOffsetZ;
				position += vector;
			}
			if (y0)
			{
				position.y = 0f;
			}
			_transform.position = position;
		}
	}
}
