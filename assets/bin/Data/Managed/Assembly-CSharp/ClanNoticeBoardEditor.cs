using System;

public class ClanNoticeBoardEditor : GameSection
{
	protected enum UI
	{
		IPT_TEXT
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_TEXT, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body, 256);
	}

	protected virtual void OnQuery_SEND()
	{
		GameSection.StayEvent();
		string text = GetInputValue((Enum)UI.IPT_TEXT);
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Replace("\n", "\\n");
		}
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestEditNoticeBoard(text, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}
}
