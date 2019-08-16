public class ClanUnregisteredDialogBase : GameSection
{
	private void OnQuery_CREATE()
	{
	}

	private void OnQuery_SEARCH()
	{
	}

	private void OnQuery_FOLLOWER_LIST()
	{
	}

	private void OnQuery_INVITED_LIST()
	{
	}

	private void OnQuery_HELP()
	{
		GameSection.SetEventData(WebViewManager.Clan);
	}
}
