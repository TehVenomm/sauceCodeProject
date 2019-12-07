using Network;

public class GuildSearchSettings : GameSection
{
	public enum UI
	{
		IPT_NAME,
		LBL_INPUT,
		LBL_DEFAULT
	}

	private string mSearchKeywork;

	public override void Initialize()
	{
		SetActive(UI.LBL_DEFAULT, string.IsNullOrEmpty(mSearchKeywork));
		SetInput(UI.IPT_NAME, mSearchKeywork, 16, OnChangeKeywork);
		base.Initialize();
	}

	private void OnQuery_SEARCH()
	{
		MonoBehaviourSingleton<GuildManager>.I.mSearchKeywork = mSearchKeywork;
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendSearch(delegate(bool is_success, Error err)
		{
			GameSection.ResumeEvent(is_success);
		}, saveSettings: true);
	}

	protected void OnChangeKeywork()
	{
		string inputValue = GetInputValue(UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", "");
		inputValue = inputValue.Replace("\u3000", "");
		SetActive(UI.LBL_DEFAULT, string.IsNullOrEmpty(inputValue));
		mSearchKeywork = inputValue;
	}
}
