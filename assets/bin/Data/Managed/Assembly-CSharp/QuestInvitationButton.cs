public class QuestInvitationButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween(UI.OBJ_TWEEN, true, null, false, 0);
		if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
		{
			MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(true);
		}
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
