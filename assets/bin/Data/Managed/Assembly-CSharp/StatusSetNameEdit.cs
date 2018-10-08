public class StatusSetNameEdit : ConfigName
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

	private int setNo;

	private EquipSetInfo info;

	public override void Initialize()
	{
		setNo = (int)(GameSection.GetEventData() as object[])[0];
		info = ((GameSection.GetEventData() as object[])[1] as EquipSetInfo);
		base.Initialize();
	}

	protected override void SetBeforeText()
	{
		before_text = info.name;
		inputMaxLength = 13;
	}

	private void OnQuery_OK()
	{
		GameSection.SetEventData(null);
		string input_text = GetInputValue(UI.IPT_TEXT);
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.SendEquipSetName(input_text, setNo, delegate(bool is_success)
		{
			if (is_success)
			{
				info.ChangeName(input_text);
			}
			GameSection.ChangeStayEvent("OK", new object[3]
			{
				is_success,
				setNo,
				input_text
			});
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
