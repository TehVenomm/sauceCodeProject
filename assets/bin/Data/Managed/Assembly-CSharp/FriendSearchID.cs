using Network;

public class FriendSearchID : ConfigName
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
		sectionType = SECTION_TYPE.SEARCH_ID;
		base.Initialize();
	}

	protected override void SetBeforeText()
	{
		before_text = string.Empty;
		inputMaxLength = 99;
	}

	private void OnQuery_OK()
	{
		GameSection.SetEventData(null);
		string input_text = GetInputValue(UI.IPT_TEXT);
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendSearchID(input_text, delegate(bool is_success, FriendSearchResult recv_data)
		{
			if (is_success)
			{
				GameSection.ChangeStayEvent("OK", new object[4]
				{
					true,
					0,
					recv_data,
					input_text
				});
			}
			else
			{
				GameSection.ChangeStayEvent("OK", new object[4]
				{
					true,
					0,
					new FriendSearchResult(),
					input_text
				});
			}
			GameSection.ResumeEvent(true, null);
		});
	}
}
