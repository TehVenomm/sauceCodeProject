using UnityEngine;

public class BlackMarketIngameButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween(UI.OBJ_TWEEN, true, null, false, 0);
		base.OnOpen();
	}

	public void SetDisableButton(bool flag)
	{
		UIButton componentInChildren = GetComponentInChildren<UIButton>();
		if ((Object)componentInChildren != (Object)null)
		{
			componentInChildren.isEnabled = !flag;
		}
	}
}
