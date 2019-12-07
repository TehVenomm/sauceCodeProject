public class HomeNPCTalk : GameSection
{
	private enum UI
	{
		BTN_BACK
	}

	public int npcID
	{
		get;
		private set;
	}

	public override void Initialize()
	{
		npcID = (int)GameSection.GetEventData();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetFullScreenButton(UI.BTN_BACK);
	}
}
