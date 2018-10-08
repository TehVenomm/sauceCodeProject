public class TradingPostLog : GameSection
{
	private void OnQuery_HELP()
	{
		GameSection.ChangeEvent("[BACK]", null);
		RequestEvent("HELP", null);
	}

	private void OnQuery_HISTORY()
	{
		GameSection.ChangeEvent("[BACK]", null);
		RequestEvent("HISTORY", null);
	}

	private void OnQuery_ACTIVE()
	{
		GameSection.ChangeEvent("[BACK]", null);
		RequestEvent("ACTIVE", null);
	}

	private void OnQuery_INVENTORY()
	{
		RequestEvent("TO_STORAGE", null);
	}
}
