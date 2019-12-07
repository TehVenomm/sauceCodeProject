public class FortuneWheelButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN,
		TIME_COUNTDOWN_TXT,
		SPR_NOTE_UPDATE
	}

	protected override void OnOpen()
	{
		PlayTween(UI.OBJ_TWEEN, forward: true, null, is_input_block: false);
		base.OnOpen();
	}

	public void Show(bool isShow)
	{
		if (isShow)
		{
			Open();
		}
		else
		{
			Close();
		}
	}
}
