using System;

public class QuestInvitationButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween((Enum)UI.OBJ_TWEEN, true, (EventDelegate.Callback)null, false, 0);
		MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(true);
		base.OnOpen();
	}

	protected override void OnClose()
	{
		if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
		{
			MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(false);
		}
		base.OnClose();
	}
}
