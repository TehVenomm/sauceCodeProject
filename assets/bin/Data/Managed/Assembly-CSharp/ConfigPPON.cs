using Network;

public class ConfigPPON : GameSection
{
	private enum UI
	{
		IPT_PW,
		IPT_PW_CONFIRM,
		BTN_OK
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_PW, "", 4, OnInputChange);
		SetInput(UI.IPT_PW_CONFIRM, "", 4, OnInputChange);
	}

	private void OnInputChange()
	{
		SetButtonEnabled(UI.BTN_OK, GetInputValue(UI.IPT_PW).Length == 4 && GetInputValue(UI.IPT_PW_CONFIRM).Length == 4);
	}

	private void OnQuery_OK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendParentalPassword(GetInputValue(UI.IPT_PW), GetInputValue(UI.IPT_PW_CONFIRM), delegate(Error ret)
		{
			if (ret != 0)
			{
				GameSection.ChangeStayEvent("ERROR", new object[1]
				{
					(int)ret
				});
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}
}
