using Network;
using System;

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
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(mSearchKeywork));
		SetInput((Enum)UI.IPT_NAME, mSearchKeywork, 16, (EventDelegate.Callback)OnChangeKeywork);
		base.Initialize();
	}

	private void OnQuery_SEARCH()
	{
		MonoBehaviourSingleton<GuildManager>.I.mSearchKeywork = mSearchKeywork;
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendSearch(delegate(bool is_success, Error err)
		{
			GameSection.ResumeEvent(is_success, null);
		}, true);
	}

	protected void OnChangeKeywork()
	{
		string inputValue = GetInputValue((Enum)UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", string.Empty);
		inputValue = inputValue.Replace("\u3000", string.Empty);
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(inputValue));
		mSearchKeywork = inputValue;
	}
}
