public class AccountChangePasswordMail : AccountPopupAdjuster
{
	private enum UI
	{
		IPT_PASSWORD,
		IPT_NEW_PASSWORD,
		IPT_CONFIRM_NEW_PASSWORD,
		BTN_OK,
		BTN_INVALID
	}

	public override void UpdateUI()
	{
		SetInput(UI.IPT_PASSWORD, string.Empty, 255, InputCallBack);
		SetInput(UI.IPT_NEW_PASSWORD, string.Empty, 255, InputCallBack);
		SetInput(UI.IPT_CONFIRM_NEW_PASSWORD, string.Empty, 255, InputCallBack);
	}

	private void InputCallBack()
	{
		bool flag = CheckRegistData();
		SetActive(UI.BTN_OK, flag);
		SetActive(UI.BTN_INVALID, !flag);
	}

	private bool CheckRegistData(bool is_send_event = false)
	{
		if (string.IsNullOrEmpty(GetInputText(UI.IPT_PASSWORD)))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("PASSWORD")
			});
			return false;
		}
		string inputText = GetInputText(UI.IPT_NEW_PASSWORD);
		string inputText2 = GetInputText(UI.IPT_CONFIRM_NEW_PASSWORD);
		if (string.IsNullOrEmpty(inputText))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("NEW_PASSWORD")
			});
			return false;
		}
		if (inputText.Length < 8)
		{
			CheckChangeEvent(is_send_event, "PASSWORD_TOO_SHORT");
			return false;
		}
		if (inputText != inputText2)
		{
			CheckChangeEvent(is_send_event, "CONFIRM_PASSWORD_NOT_MATCH");
			return false;
		}
		if (inputText.Length > 255)
		{
			CheckChangeEvent(is_send_event, "PASSWORD_TOO_LONG");
			return false;
		}
		return true;
	}

	private void CheckChangeEvent(bool is_send, string event_name = "", object event_data = null)
	{
		if (is_send)
		{
			GameSection.ChangeEvent(event_name, event_data);
		}
	}

	private void OnQuery_OK()
	{
		if (CheckRegistData(is_send_event: true))
		{
			string inputText = GetInputText(UI.IPT_PASSWORD);
			string inputText2 = GetInputText(UI.IPT_NEW_PASSWORD);
			string inputText3 = GetInputText(UI.IPT_CONFIRM_NEW_PASSWORD);
			GameSection.StayEvent();
			MonoBehaviourSingleton<AccountManager>.I.SendRegistChangePasswordRob(inputText, inputText2, inputText3, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success);
			});
		}
	}
}
