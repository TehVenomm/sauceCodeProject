using Network;
using System;

public class FriendSearchName : ConfigName
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

	private int page;

	public override void Initialize()
	{
		page = (int)GameSection.GetEventData();
		sectionType = SECTION_TYPE.SEARCH_NAME;
		base.Initialize();
	}

	protected override void SetBeforeText()
	{
		before_text = string.Empty;
		inputMaxLength = 12;
	}

	private void OnQuery_OK()
	{
		GameSection.SetEventData(null);
		string input_text = GetInputValue((Enum)UI.IPT_TEXT);
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendSearchName(input_text, page, delegate(bool is_success, FriendSearchResult recv_data)
		{
			GameSection.ChangeStayEvent("OK", new object[4]
			{
				is_success,
				page,
				recv_data,
				input_text
			});
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
