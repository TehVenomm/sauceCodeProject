using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
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
		if (onClick == null && selectOnClick != null)
		{
			onClick = selectOnClick.get_gameObject();
			selectOnClick = null;
			NGUITools.SetDirty(this);
		}
		if (onLeft == null && selectOnLeft != null)
		{
			onLeft = selectOnLeft.get_gameObject();
			selectOnLeft = null;
			NGUITools.SetDirty(this);
		}
		if (onRight == null && selectOnRight != null)
		{
			onRight = selectOnRight.get_gameObject();
			selectOnRight = null;
			NGUITools.SetDirty(this);
		}
		if (onUp == null && selectOnUp != null)
		{
			onUp = selectOnUp.get_gameObject();
			selectOnUp = null;
			NGUITools.SetDirty(this);
		}
		if (onDown == null && selectOnDown != null)
		{
			onDown = selectOnDown.get_gameObject();
			selectOnDown = null;
			NGUITools.SetDirty(this);
		}
	}
}
