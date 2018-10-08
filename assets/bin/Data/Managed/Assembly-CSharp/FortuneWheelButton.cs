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
		PlayTween((Enum)UI.OBJ_TWEEN, true, (EventDelegate.Callback)null, false, 0);
		base.OnOpen();
	}

	public void Show(bool isShow)
	{
		if (isShow)
		{
			Open(UITransition.TYPE.OPEN);
		}
		else
		{
			Close(UITransition.TYPE.CLOSE);
		}
	}
}
