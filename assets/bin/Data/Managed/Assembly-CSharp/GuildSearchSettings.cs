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

	private unsafe void OnQuery_SEARCH()
	{
		MonoBehaviourSingleton<GuildManager>.I.mSearchKeywork = mSearchKeywork;
		GameSection.StayEvent();
		GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
		if (_003C_003Ef__am_0024cache1 == null)
		{
			_003C_003Ef__am_0024cache1 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendSearch(_003C_003Ef__am_0024cache1, true);
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
