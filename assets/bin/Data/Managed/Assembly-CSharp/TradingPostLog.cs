public class TradingPostLog : GameSection
{
	private void OnQuery_HELP()
	{
		GameSection.ChangeEvent("[BACK]");
		RequestEvent("HELP");
	}

	private void OnQuery_HISTORY()
	{
		GameSection.ChangeEvent("[BACK]");
		RequestEvent("HISTORY");
	}

	private void OnQuery_ACTIVE()
	{
		GameSection.ChangeEvent("[BACK]");
		RequestEvent("ACTIVE");
	}

	private void OnQuery_INVENTORY()
	{
		RequestEvent("TO_STORAGE");
	}
}
