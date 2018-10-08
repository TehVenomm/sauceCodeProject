using System;

public class QuestInvitationInGameButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween((Enum)UI.OBJ_TWEEN, true, (EventDelegate.Callback)null, false, 0);
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
