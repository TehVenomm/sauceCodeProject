public class SmithGrowSkillSelectMaterialItemNum : GameSection
{
	protected enum UI
	{
		LBL_SELECT_NUM,
		LBL_SELECT_PRICE,
		BTN_SELECT_NUM_MINUS,
		BTN_SELECT_NUM_PLUS,
		SLD_SELECT_NUM,
		SPR_SELECT_FRAME,
		STR_TITLE_U,
		STR_TITLE_D,
		STR_SELECT_NUM,
		LBL_CAPTION
	}

	protected SortCompareData m_data;

	private int m_nowEmpty;

	private int m_maxNum;

	private int m_nowSelect;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		m_data = (array[0] as SortCompareData);
		m_nowEmpty = (int)array[1];
		m_nowSelect = (int)array[2];
		if (m_nowEmpty + m_nowSelect >= m_data.GetNum())
		{
			m_maxNum = m_data.GetNum();
		}
		else
		{
			m_maxNum = m_nowEmpty + m_nowSelect;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		string key = "TEXT_SELECT";
		SetLabelText(UI.LBL_CAPTION, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_U, base.sectionData.GetText(key));
		SetLabelText(UI.STR_TITLE_D, base.sectionData.GetText(key));
		string key2 = "TEXT_SELECT_NUM";
		SetLabelText(UI.STR_SELECT_NUM, base.sectionData.GetText(key2));
		SetProgressInt(UI.SLD_SELECT_NUM, m_nowSelect, 0, m_maxNum, OnChagenSlider);
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt(UI.SLD_SELECT_NUM);
		SetLabelText(UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", progressInt));
	}

	private void OnQuery_SELECT_NUM_MINUS()
	{
		SetProgressInt(UI.SLD_SELECT_NUM, GetProgressInt(UI.SLD_SELECT_NUM) - 1, -1, -1, null);
	}

	private void OnQuery_SELECT_NUM_PLUS()
	{
		SetProgressInt(UI.SLD_SELECT_NUM, GetProgressInt(UI.SLD_SELECT_NUM) + 1, -1, -1, null);
	}

	protected int GetSliderNum()
	{
		return GetProgressInt(UI.SLD_SELECT_NUM);
	}

	private void OnQuery_SELECT()
	{
		GameSection.SetEventData(new object[2]
		{
			m_data,
			GetSliderNum()
		});
		GameSection.BackSection();
	}
}
