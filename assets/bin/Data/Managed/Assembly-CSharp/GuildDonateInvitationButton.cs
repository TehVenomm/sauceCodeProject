using System;

public class GuildDonateInvitationButton : UIBehaviour
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
}
