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
		SetInput(UI.IPT_AD, "", 4);
		SetInput(UI.IPT_MONTH, "", 2);
	}

	private void OnQuery_OK()
	{
		string inputValue = GetInputValue(UI.IPT_AD);
		string inputValue2 = GetInputValue(UI.IPT_MONTH);
		if (inputValue.Length == 4 && inputValue2.Length >= 1 && int.TryParse(inputValue, out int result) && int.TryParse(inputValue2, out int result2) && result > 1800 && result2 >= 1 && result2 <= 12)
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
