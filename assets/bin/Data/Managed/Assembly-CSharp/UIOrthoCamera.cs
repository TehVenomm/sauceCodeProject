using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
[RequireComponent(typeof(Camera))]
public class UIOrthoCamera
{
	private Camera mCam;

	private Transform mTrans;

	public UIOrthoCamera()
		: this()
	{
	}

	private void Start()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		mCam = this.GetComponent<Camera>();
		mTrans = this.get_transform();
		mCam.set_orthographic(true);
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Rect rect = mCam.get_rect();
		float num = rect.get_yMin() * (float)Screen.get_height();
		Rect rect2 = mCam.get_rect();
		float num2 = rect2.get_yMax() * (float)Screen.get_height();
		float num3 = (num2 - num) * 0.5f;
		Vector3 lossyScale = mTrans.get_lossyScale();
		float num4 = num3 * lossyScale.y;
		if (!Mathf.Approximately(mCam.get_orthographicSize(), num4))
		{
			mCam.set_orthographicSize(num4);
		}
	}
}
