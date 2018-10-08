using Network;
using System;

public class GuildRequestContinue : GameSection
{
	private enum UI
	{
		MESSAGE
	}

	public override void UpdateUI()
	{
		string text = GameSection.GetEventData() as string;
		SetLabelText((Enum)UI.MESSAGE, text);
		base.UpdateUI();
	}

	private void OnQuery_YES()
	{
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		if (GameSection.CheckCrystal(selectedItem.crystalNum, 0, true))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestExtend(delegate(bool questCompleteData)
			{
				GameSection.ResumeEvent(true, null, false);
				GameSection.SetEventData(questCompleteData);
			});
		}
	}

	private void OnQuery_NO()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestRetire(delegate(bool questCompleteData)
		{
			GameSection.ResumeEvent(true, null, false);
			GameSection.SetEventData(questCompleteData);
		});
	}
}
