using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
[ExecuteInEditMode]
public class UIButtonKeys : UIKeyNavigation
{
	public UIButtonKeys selectOnClick;

	public UIButtonKeys selectOnUp;

	public UIButtonKeys selectOnDown;

	public UIButtonKeys selectOnLeft;

	public UIButtonKeys selectOnRight;

	protected override void OnEnable()
	{
		Upgrade();
		base.OnEnable();
	}

	public void Upgrade()
	{
		if ((Object)onClick == (Object)null && (Object)selectOnClick != (Object)null)
		{
			onClick = selectOnClick.gameObject;
			selectOnClick = null;
			NGUITools.SetDirty(this);
		}
		if ((Object)onLeft == (Object)null && (Object)selectOnLeft != (Object)null)
		{
			onLeft = selectOnLeft.gameObject;
			selectOnLeft = null;
			NGUITools.SetDirty(this);
		}
		if ((Object)onRight == (Object)null && (Object)selectOnRight != (Object)null)
		{
			onRight = selectOnRight.gameObject;
			selectOnRight = null;
			NGUITools.SetDirty(this);
		}
		if ((Object)onUp == (Object)null && (Object)selectOnUp != (Object)null)
		{
			onUp = selectOnUp.gameObject;
			selectOnUp = null;
			NGUITools.SetDirty(this);
		}
		if ((Object)onDown == (Object)null && (Object)selectOnDown != (Object)null)
		{
			onDown = selectOnDown.gameObject;
			selectOnDown = null;
			NGUITools.SetDirty(this);
		}
	}
}
