public class GuildRequestContinue : GameSection
{
	private enum UI
	{
		MESSAGE
	}

	public override void UpdateUI()
	{
		string text = GameSection.GetEventData() as string;
		SetLabelText(UI.MESSAGE, text);
		base.UpdateUI();
	}

	private void OnQuery_YES()
	{
		if (GameSection.CheckCrystal(MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem().crystalNum))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestExtend(delegate(bool questCompleteData)
			{
				GameSection.ResumeEvent(questCompleteData);
				GameSection.SetEventData(questCompleteData);
			});
		}
	}

	private void OnQuery_NO()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestRetire(delegate(bool questCompleteData)
		{
			GameSection.ResumeEvent(questCompleteData);
			GameSection.SetEventData(questCompleteData);
		});
	}
}
