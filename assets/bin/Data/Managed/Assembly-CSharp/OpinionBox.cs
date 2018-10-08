public class OpinionBox : GameSection
{
	protected enum UI
	{
		IPT_TEXT,
		CLOSE
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_TEXT, string.Empty, 511, null);
	}

	protected virtual void OnQuery_SEND()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(GetInputValue(UI.IPT_TEXT), delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
