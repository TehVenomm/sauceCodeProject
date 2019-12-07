using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InGameCameraCuller : MonoBehaviourSingleton<InGameCameraCuller>
{
	private static readonly Plane[] _planes = new Plane[6];

	private Camera TargetCamera;

	private Matrix4x4 CamMatrix;

	public bool IsUpdated
	{
		get;
		private set;
	}

	private void Start()
	{
		TargetCamera = GetComponent<Camera>();
	}

	private void Update()
	{
		IsUpdated = false;
		if (base.transform.hasChanged)
		{
			CamMatrix = TargetCamera.projectionMatrix * TargetCamera.worldToCameraMatrix;
			SetFrustumPlanes(CamMatrix);
			base.transform.hasChanged = false;
			IsUpdated = true;
		}
	}

	public void SetFrustumPlanes(Matrix4x4 worldProjectionMatrix)
	{
		GeometryUtility.CalculateFrustumPlanes(worldProjectionMatrix, _planes);
	}

	public bool IsVisible(Bounds bound)
	{
		return GeometryUtility.TestPlanesAABB(_planes, bound);
	}
}
