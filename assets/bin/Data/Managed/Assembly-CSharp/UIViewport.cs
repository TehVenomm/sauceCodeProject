using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	public Camera sourceCamera;

	public Transform topLeft;

	public Transform bottomRight;

	public float fullSize = 1f;

	private Camera mCam;

	public UIViewport()
		: this()
	{
	}

	private void Start()
	{
		mCam = this.GetComponent<Camera>();
		if (sourceCamera == null)
		{
			sourceCamera = Camera.get_main();
		}
	}

	private void LateUpdate()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		if (!(topLeft != null) || !(bottomRight != null))
		{
			return;
		}
		if (topLeft.get_gameObject().get_activeInHierarchy())
		{
			Vector3 val = sourceCamera.WorldToScreenPoint(topLeft.get_position());
			Vector3 val2 = sourceCamera.WorldToScreenPoint(bottomRight.get_position());
			Rect val3 = default(Rect);
			val3._002Ector(val.x / (float)Screen.get_width(), val2.y / (float)Screen.get_height(), (val2.x - val.x) / (float)Screen.get_width(), (val.y - val2.y) / (float)Screen.get_height());
			float num = fullSize * val3.get_height();
			if (val3 != mCam.get_rect())
			{
				mCam.set_rect(val3);
			}
			if (mCam.get_orthographicSize() != num)
			{
				mCam.set_orthographicSize(num);
			}
			mCam.set_enabled(true);
		}
		else
		{
			mCam.set_enabled(false);
		}
	}
}
