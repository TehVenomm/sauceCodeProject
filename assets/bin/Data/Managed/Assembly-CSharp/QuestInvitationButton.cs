using System;

public class QuestInvitationButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween((Enum)UI.OBJ_TWEEN, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
		{
			MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(isOpen: true);
		}
		base.OnOpen();
	}

	protected override void OnClose()
	{
		if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
		{
			MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(isOpen: false);
		}
		base.OnClose();
	}
}
