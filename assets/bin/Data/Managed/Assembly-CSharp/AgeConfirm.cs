using System;
using System.Collections.Generic;

public class AgeConfirm : GameSection
{
	private enum UI
	{
		IPT_AD,
		IPT_MONTH
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_AD, string.Empty, 4);
		SetInput(UI.IPT_MONTH, string.Empty, 2);
	}

	private void OnQuery_OK()
	{
		string inputValue = GetInputValue((Enum)UI.IPT_AD);
		string inputValue2 = GetInputValue((Enum)UI.IPT_MONTH);
		int result2;
		int result;
		if (inputValue.Length == 4 && inputValue2.Length >= 1 && int.TryParse(inputValue, out result) && int.TryParse(inputValue2, out result2) && result > 1800 && result2 >= 1 && result2 <= 12)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<UserInfoManager>.I.SendBirthday(result, result2, 1, delegate(bool is_success)
			{
				if (!is_success)
				{
					GameSection.ChangeStayEvent("ERROR");
				}
				GameSection.ResumeEvent(is_resume: true);
			});
		}
		else
		{
			GameSection.ChangeEvent("ERROR");
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		if (historyList.Count > 1)
		{
			int count = historyList.Count;
			GameSection.ChangeEvent("WARNING_" + historyList[count - 2].sceneName.ToUpper());
		}
	}
}
