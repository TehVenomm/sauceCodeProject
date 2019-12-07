public class ItemDetailSkillSimpleDialog : ItemDetailSkillDialog
{
	public class InitParam
	{
		public object EventDataForParentInitialization;

		public object EventDataForPrevSectionInit;

		public InitParam()
		{
			EventDataForParentInitialization = null;
			EventDataForPrevSectionInit = null;
		}

		public InitParam(object _eventDataForParent, object _eventDataForPrevSection)
		{
			EventDataForParentInitialization = _eventDataForParent;
			EventDataForPrevSectionInit = _eventDataForPrevSection;
		}
	}

	private InitParam m_initParam;

	public override void Initialize()
	{
		InitParam initParam = m_initParam = (GameSection.GetEventData() as InitParam);
		if (m_initParam != null)
		{
			GameSection.SetEventData(m_initParam.EventDataForParentInitialization);
		}
		base.Initialize();
		ForceInvisibleUIButtons();
	}

	private void ForceInvisibleUIButtons()
	{
		SetActive(UI.BTN_CHANGE, is_visible: false);
		SetActive(UI.BTN_GROW, is_visible: false);
		SetActive(UI.BTN_SELL, is_visible: false);
	}

	private void OnQuery_SECTION_BACK()
	{
		if (m_initParam != null)
		{
			GameSection.SetEventData(m_initParam.EventDataForPrevSectionInit);
		}
	}
}
