using UnityEngine;

public class CameraPosLink : MonoBehaviour
{
	public Camera targetCamera;

	public bool y0;

	private Transform _transform;

	private void Start()
	{
		if ((Object)targetCamera == (Object)null)
		{
			targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
		_transform = base.transform;
	}

	private void LateUpdate()
	{
		if (!((Object)targetCamera == (Object)null))
		{
			Vector3 position = targetCamera.transform.position;
			if (y0)
			{
				position.y = 0f;
			}
			_transform.position = position;
		}
	}
}
