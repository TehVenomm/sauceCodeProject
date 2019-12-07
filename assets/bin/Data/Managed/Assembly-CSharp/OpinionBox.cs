public class OpinionBox : GameSection
{
	protected enum UI
	{
		IPT_TEXT,
		CLOSE
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_TEXT, string.Empty, 511);
	}

	protected virtual void OnQuery_SEND()
	{
		GameSection.StayEvent();
		string text = GetInputValue(UI.IPT_TEXT);
		if (text.IsNullOrWhiteSpace())
		{
			text = text.Trim();
		}
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Replace("\n", "\\n");
		}
		MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(text, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}
}
