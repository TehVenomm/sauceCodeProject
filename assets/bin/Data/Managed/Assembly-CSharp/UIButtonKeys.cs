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
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
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
