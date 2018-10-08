using System;

public class OpinionBox : GameSection
{
	protected enum UI
	{
		IPT_TEXT,
		CLOSE
	}

	public override void UpdateUI()
	{
		SetInput((Enum)UI.IPT_TEXT, string.Empty, 511, (EventDelegate.Callback)null);
	}

	protected virtual void OnQuery_SEND()
	{
		GameSection.StayEvent();
		string text = GetInputValue((Enum)UI.IPT_TEXT);
		if (text.IsNullOrWhiteSpace())
		{
			text = text.Trim();
		}
		MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(text, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null, false);
		});
	}
}
