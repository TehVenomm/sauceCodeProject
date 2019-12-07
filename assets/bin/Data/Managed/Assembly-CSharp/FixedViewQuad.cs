using UnityEngine;

[ExecuteInEditMode]
public class FixedViewQuad : MonoBehaviour
{
	public Camera targetCamera;

	public float planeZ = 100f;

	private Transform _transform;

	private MeshRenderer _meshRenderer;

	private void Awake()
	{
		_transform = base.transform;
		_meshRenderer = GetComponent<MeshRenderer>();
		if (targetCamera == null)
		{
			targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
	}

	private void LateUpdate()
	{
		if (_meshRenderer != null)
		{
			_meshRenderer.enabled = (targetCamera != null);
		}
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		Vector3 vector = targetCamera.ScreenToWorldPoint(new Vector3(num, num2, planeZ));
		Vector3 position = (num < num2) ? new Vector3(num, -1f, planeZ) : new Vector3(-1f, num2, planeZ);
		float num3 = (targetCamera.ScreenToWorldPoint(position) - vector).magnitude * 2f;
		_transform.localScale = new Vector3(num3, num3, 1f);
		_transform.position = vector;
		_transform.rotation = targetCamera.transform.rotation;
	}
}
