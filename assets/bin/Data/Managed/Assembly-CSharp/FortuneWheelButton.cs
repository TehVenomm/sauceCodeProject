using System;

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
		PlayTween((Enum)UI.OBJ_TWEEN, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
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
