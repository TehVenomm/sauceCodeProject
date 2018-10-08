using UnityEngine;

[AddComponentMenu("NGUI/UI/Viewport Camera")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class UIViewport : MonoBehaviour
{
	public Camera sourceCamera;

	public Transform topLeft;

	public Transform bottomRight;

	public float fullSize = 1f;

	private Camera mCam;

	private void Start()
	{
		mCam = GetComponent<Camera>();
		if ((Object)sourceCamera == (Object)null)
		{
			sourceCamera = Camera.main;
		}
	}

	private void LateUpdate()
	{
		if ((Object)topLeft != (Object)null && (Object)bottomRight != (Object)null)
		{
			if (topLeft.gameObject.activeInHierarchy)
			{
				Vector3 vector = sourceCamera.WorldToScreenPoint(topLeft.position);
				Vector3 vector2 = sourceCamera.WorldToScreenPoint(bottomRight.position);
				Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
				float num = fullSize * rect.height;
				if (rect != mCam.rect)
				{
					mCam.rect = rect;
				}
				if (mCam.orthographicSize != num)
				{
					mCam.orthographicSize = num;
				}
				mCam.enabled = true;
			}
			else
			{
				mCam.enabled = false;
			}
		}
	}
}
