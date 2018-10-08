using System;

public class GuildDonateInvitationButton : UIBehaviour
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
}
