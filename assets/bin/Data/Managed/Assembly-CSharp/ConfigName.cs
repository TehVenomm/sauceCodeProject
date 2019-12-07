public class ConfigName : GameSection
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

	protected enum SECTION_TYPE
	{
		CHANGE_NAME,
		CHANGE_COMMENT,
		SEARCH_NAME,
		SEARCH_ID
	}

	protected SECTION_TYPE sectionType;

	protected string before_text;

	protected int inputMaxLength;

	protected virtual void SetBeforeText()
	{
		before_text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
		inputMaxLength = 14;
	}

	public override void Initialize()
	{
		SetBeforeText();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive(UI.SPR_TITLE_CHANGE_NAME, sectionType == SECTION_TYPE.CHANGE_NAME);
		SetActive(UI.SPR_TITLE_CHANGE_COMMENT, sectionType == SECTION_TYPE.CHANGE_COMMENT);
		SetActive(UI.SPR_TITLE_SEARCH_NAME, sectionType == SECTION_TYPE.SEARCH_NAME);
		SetActive(UI.SPR_TITLE_SEARCH_ID, sectionType == SECTION_TYPE.SEARCH_ID);
		SetInput(UI.IPT_TEXT, before_text, inputMaxLength, UpdateButton);
	}

	private void UpdateButton()
	{
		string inputValue = GetInputValue(UI.IPT_TEXT);
		SetButtonEnabled(UI.BTN_OK, inputValue != before_text && inputValue.Length > 0);
	}

	private void OnQuery_OK()
	{
		string inputValue = GetInputValue(UI.IPT_TEXT);
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendChangeName(inputValue, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}
}
