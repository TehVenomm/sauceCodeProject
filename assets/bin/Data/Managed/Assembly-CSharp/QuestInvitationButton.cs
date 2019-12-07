public class QuestInvitationButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween(UI.OBJ_TWEEN, forward: true, null, is_input_block: false);
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
