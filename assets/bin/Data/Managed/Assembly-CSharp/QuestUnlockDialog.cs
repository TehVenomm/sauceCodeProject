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
		PlayTween((Enum)UI.OBJ_UNLOCK_PORTAL_ROOT, true, (EventDelegate.Callback)null, true, 0);
	}
}
