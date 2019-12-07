using Network;

public class LoungeSearchSettings : LoungeConditionSettings
{
	public new enum UI
	{
		POP_TARGET_MIN_LEVEL,
		POP_TARGET_MAX_LEVEL,
		LBL_TARGET_MIN_LEVEL,
		LBL_TARGET_MAX_LEVEL,
		POP_TARGET_CAPACITY,
		LBL_TARGET_CAPACITY,
		POP_TARGET_LABEL,
		LBL_TARGET_LABEL,
		POP_TARGET_LOCK,
		LBL_TARGET_LOCK,
		IPT_NAME,
		OBJ_CREATE,
		OBJ_CHANGE,
		OBJ_SEARCH,
		LBL_DEFAULT
	}

	public class SearchRequestParam
	{
		public int order;

		public LOUNGE_LABEL label
		{
			get;
			private set;
		}

		public string loungeName
		{
			get;
			private set;
		}

		public SearchRequestParam()
		{
			order = 0;
			label = LOUNGE_LABEL.NONE;
			loungeName = "";
		}

		public SearchRequestParam(int order, LOUNGE_LABEL label, string loungeName)
		{
			this.order = order;
			this.label = label;
			this.loungeName = loungeName;
		}

		public void SetLabel(LOUNGE_LABEL label)
		{
			this.label = label;
		}

		public void SetLoungeName(string name)
		{
			loungeName = name;
		}
	}

	private SearchRequestParam searchRequest = new SearchRequestParam();

	public override void Initialize()
	{
		CopyLoungeSearchRequestParam();
		labels = StringTable.GetAllInCategory(STRING_CATEGORY.LOUNGE_LABEL);
		if (labels.Length > (int)searchRequest.label)
		{
			labelIndex = (int)searchRequest.label;
		}
		SetActive(UI.LBL_DEFAULT, string.IsNullOrEmpty(searchRequest.loungeName));
		SetInput(UI.IPT_NAME, searchRequest.loungeName, 16, OnChangeLoungeName);
		GameSection.SetEventData(false);
		InitializeBase();
	}

	private void CopyLoungeSearchRequestParam()
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SetLoungeSearchRequestFromPrefs();
		SearchRequestParam searchRequestParam = MonoBehaviourSingleton<LoungeMatchingManager>.I.searchRequest;
		searchRequest = new SearchRequestParam(searchRequestParam.order, searchRequestParam.label, searchRequestParam.loungeName);
	}

	public override void UpdateUI()
	{
		UpdateLabel();
	}

	protected override void SetParamLabel(LOUNGE_LABEL label)
	{
		searchRequest.SetLabel(label);
	}

	protected override void OnChangeLoungeName()
	{
		string inputValue = GetInputValue(UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", "");
		inputValue = inputValue.Replace("\u3000", "");
		SetActive(UI.LBL_DEFAULT, string.IsNullOrEmpty(inputValue));
		searchRequest.SetLoungeName(inputValue);
	}

	private void OnQuery_SEARCH()
	{
		searchRequest.order = 1;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SetSearchRequest(searchRequest);
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearch(delegate(bool is_success, Error err)
		{
			GameSection.ResumeEvent(is_success);
		}, saveSettings: true);
	}

	private void OnQuery_MATCHING()
	{
		searchRequest.order = 1;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SetSearchRequest(searchRequest);
		RequestRandomMatching();
	}

	private void OnQuery_LoungeSearchMatchingFailed_YES()
	{
		RequestRandomMatching();
	}

	private void RequestRandomMatching()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearchRandomMatching(delegate(bool is_success, Error err)
		{
			if (!is_success)
			{
				GameSection.ChangeStayEvent("NOT_FOUND_MATCHING_LOUNGE");
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}
}
