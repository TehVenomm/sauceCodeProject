using System;

public class BlackMarketIngameButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween((Enum)UI.OBJ_TWEEN, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		base.OnOpen();
	}

	public void SetDisableButton(bool flag)
	{
		UIButton componentInChildren = this.GetComponentInChildren<UIButton>();
		if (componentInChildren != null)
		{
			componentInChildren.isEnabled = !flag;
		}
	}
}
