using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize
{
	public UIWidget target;

	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	public int minWidth = 100;

	public int minHeight = 100;

	public int maxWidth = 100000;

	public int maxHeight = 100000;

	public bool updateAnchors;

	private Plane mPlane;

	private Vector3 mRayPos;

	private Vector3 mLocalPos;

	private int mWidth;

	private int mHeight;

	private bool mDragging;

	public UIDragResize()
		: this()
	{
	}

	private void OnDragStart()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (target != null)
		{
			Vector3[] worldCorners = target.worldCorners;
			mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
			Ray currentRay = UICamera.currentRay;
			float num = default(float);
			if (mPlane.Raycast(currentRay, ref num))
			{
				mRayPos = currentRay.GetPoint(num);
				mLocalPos = target.cachedTransform.get_localPosition();
				mWidth = target.width;
				mHeight = target.height;
				mDragging = true;
			}
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if (mDragging && target != null)
		{
			Ray currentRay = UICamera.currentRay;
			float num = default(float);
			if (mPlane.Raycast(currentRay, ref num))
			{
				Transform cachedTransform = target.cachedTransform;
				cachedTransform.set_localPosition(mLocalPos);
				target.width = mWidth;
				target.height = mHeight;
				Vector3 val = currentRay.GetPoint(num) - mRayPos;
				cachedTransform.set_position(cachedTransform.get_position() + val);
				Vector3 val2 = Quaternion.Inverse(cachedTransform.get_localRotation()) * (cachedTransform.get_localPosition() - mLocalPos);
				cachedTransform.set_localPosition(mLocalPos);
				NGUIMath.ResizeWidget(target, pivot, val2.x, val2.y, minWidth, minHeight, maxWidth, maxHeight);
				if (updateAnchors)
				{
					target.BroadcastMessage("UpdateAnchors");
				}
			}
		}
	}

	private void OnDragEnd()
	{
		mDragging = false;
	}
}
