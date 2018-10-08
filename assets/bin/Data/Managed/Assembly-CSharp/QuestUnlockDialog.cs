public class QuestUnlockDialog : GameSection
{
	protected enum UI
	{
		OBJ_UNLOCK_PORTAL_ROOT
	}

	public override void Initialize()
	{
		base.Initialize();
		PlayTween(UI.OBJ_UNLOCK_PORTAL_ROOT, true, null, true, 0);
	}
}
