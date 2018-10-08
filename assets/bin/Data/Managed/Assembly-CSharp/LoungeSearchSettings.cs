using Network;
using System;

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
			loungeName = string.Empty;
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
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(searchRequest.loungeName));
		SetInput((Enum)UI.IPT_NAME, searchRequest.loungeName, 16, (EventDelegate.Callback)((LoungeConditionSettings)this).OnChangeLoungeName);
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
		string inputValue = GetInputValue((Enum)UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", string.Empty);
		inputValue = inputValue.Replace("\u3000", string.Empty);
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(inputValue));
		searchRequest.SetLoungeName(inputValue);
	}

	private unsafe void OnQuery_SEARCH()
	{
		searchRequest.order = 1;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SetSearchRequest(searchRequest);
		GameSection.StayEvent();
		LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
		if (_003C_003Ef__am_0024cache1 == null)
		{
			_003C_003Ef__am_0024cache1 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendSearch(_003C_003Ef__am_0024cache1, true);
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

	private unsafe void RequestRandomMatching()
	{
		GameSection.StayEvent();
		LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendSearchRandomMatching(_003C_003Ef__am_0024cache2);
	}
}
