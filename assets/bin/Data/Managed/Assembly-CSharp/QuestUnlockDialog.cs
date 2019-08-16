using System;

public class QuestUnlockDialog : GameSection
{
	protected enum UI
	{
		OBJ_UNLOCK_PORTAL_ROOT
	}

	public override void Initialize()
	{
		base.Initialize();
		PlayTween((Enum)UI.OBJ_UNLOCK_PORTAL_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: true, 0);
	}
}
