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
		MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(GetInputValue((Enum)UI.IPT_TEXT), delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
