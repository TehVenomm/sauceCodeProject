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
		inputMaxLength = 14;
	}

	private unsafe void OnQuery_OK()
	{
		GameSection.SetEventData(null);
		string input_text = GetInputValue((Enum)UI.IPT_TEXT);
		GameSection.StayEvent();
		_003COnQuery_OK_003Ec__AnonStorey32F _003COnQuery_OK_003Ec__AnonStorey32F;
		MonoBehaviourSingleton<FriendManager>.I.SendSearchName(input_text, page, new Action<bool, FriendSearchResult>((object)_003COnQuery_OK_003Ec__AnonStorey32F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
