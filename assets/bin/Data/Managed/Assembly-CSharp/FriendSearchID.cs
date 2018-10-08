using Network;
using System;

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

	private unsafe void OnQuery_OK()
	{
		GameSection.SetEventData(null);
		string input_text = GetInputValue((Enum)UI.IPT_TEXT);
		GameSection.StayEvent();
		_003COnQuery_OK_003Ec__AnonStorey311 _003COnQuery_OK_003Ec__AnonStorey;
		MonoBehaviourSingleton<FriendManager>.I.SendSearchID(input_text, new Action<bool, FriendSearchResult>((object)_003COnQuery_OK_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
