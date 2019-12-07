public class ProfileEditComment : ConfigName
{
	private enum UI
	{
		IPT_TEXT,
		BTN_OK,
		SPR_TITLE_CHANGE_NAME,
		SPR_TITLE_CHANGE_COMMENT,
		SPR_TITLE_SEARCH_NAME,
		SPR_TITLE_SEARCH_ID
	}

	public override void Initialize()
	{
		sectionType = SECTION_TYPE.CHANGE_COMMENT;
		base.Initialize();
	}

	protected override void SetBeforeText()
	{
		before_text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment;
		inputMaxLength = 30;
	}

	private void OnQuery_OK()
	{
		string text = GetInputValue(UI.IPT_TEXT);
		int num = text.IndexOf("\n");
		if (num != -1)
		{
			int num2 = text.IndexOf("\n", num + 1);
			if (num2 != -1)
			{
				text = text.Remove(num2, text.Length - num2);
				SetInputValue(UI.IPT_TEXT, text);
			}
		}
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendEditComment(text, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}
}
