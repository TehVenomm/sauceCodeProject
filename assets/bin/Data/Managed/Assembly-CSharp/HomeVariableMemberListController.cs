using System;
using System.Collections;
using UnityEngine;

public class HomeVariableMemberListController : UIBehaviour, IUpdatexecutor
{
	public class InitParam
	{
		public bool IsDisplayMutualFollower;

		public bool IsDisplayClanMember;

		public Action OnClick;

		public MainChat Mainchat;
	}

	private enum UI_CTRL_TYPE
	{
		UNDEFINED = -1,
		MUTUAL_FOLLOWER,
		CLAN_MEMBER,
		MAX
	}

	private enum UI
	{
		HEADER_TABS,
		MAIN_FRAME,
		SCR_LIST,
		GRD_LIST,
		WGT_BOT_BUTTONS,
		WGT_PAGE_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		BACK_BTN_ROOT,
		BTN_BACK
	}

	private const float ANCHOR_LEFT = 0f;

	private const float ANCHOR_CENTER = 0.5f;

	private const float ANCHOR_RIGHT = 1f;

	private const float ANCHOR_BOT = 0f;

	private const float ANCHOR_TOP = 1f;

	private static readonly Vector4 WIDGET_ANCHOR_MAIN_FRAME_DEFAULT_SETTINGS = new Vector4(16f, -16f, 210f, -104f);

	private static readonly Vector4 WIDGET_ANCHOR_MAIN_FRAME_SPLIT_LANDSCAPE_SETTINGS = new Vector4(180f, -180f, 80f, -20f);

	private static readonly Vector4 WIDGET_ANCHOR_PAGE_ROOT_DEFAULT_SETTINGS = new Vector4(-100f, 100f, 108f, 182f);

	private static readonly Vector4 WIDGET_ANCHOR_PAGE_ROOT_LANDSCAPE_SETTINGS = new Vector4(-100f, 100f, 10f, 80f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_BTN_ROOT_DEFAULT_SETTINGS = new Vector4(0f, 0f, 120f, 200f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_BTN_ROOT_LANDSCAPE_SETTINGS = new Vector4(0f, 0f, 5f, 70f);

	[SerializeField]
	private UILabel m_titleText_Top;

	[SerializeField]
	private UILabel m_titleText_Bot;

	[SerializeField]
	private UIButton[] m_uiCtrlSwitchTabs = new UIButton[2];

	private ScrollItemListControllerBase[] m_uiCtrlArray = new ScrollItemListControllerBase[2];

	private UI_CTRL_TYPE m_currentTabType = UI_CTRL_TYPE.UNDEFINED;

	private bool m_isLoadingObject;

	private MainChat m_mainChat;

	private Action m_onClick;

	private UIPanel m_rootPanel;

	private GameObject m_tabRootObject;

	private UIPanel m_scrollViewPanel;

	private UIScrollView m_scrollView;

	private UIGrid m_uiGrid;

	private UILabel m_currentPageLabel;

	private UILabel m_maxPageLabel;

	private UIPanel m_mainFramePanel;

	private UIWidget m_widgetBotButtonsRoot;

	private int m_visibleItemCount;

	private ScrollItemListControllerBase CurrentCtrl => (m_currentTabType == UI_CTRL_TYPE.UNDEFINED) ? null : m_uiCtrlArray[(int)m_currentTabType];

	public bool IsLoadingObject => m_isLoadingObject;

	protected UIPanel RootPanel => m_rootPanel ?? (m_rootPanel = base.transform.GetChild(0).GetComponent<UIPanel>());

	protected GameObject TabRootObject => m_tabRootObject ?? (m_tabRootObject = GetCtrl(UI.HEADER_TABS).gameObject);

	protected UIPanel ScrollViewPanel => m_scrollViewPanel ?? (m_scrollViewPanel = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>());

	protected UIScrollView ScrollView => m_scrollView ?? (m_scrollView = GetCtrl(UI.SCR_LIST).GetComponent<UIScrollView>());

	protected UIGrid UiGrid => m_uiGrid ?? (m_uiGrid = GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>());

	protected UILabel CurrentPageLabel => m_currentPageLabel ?? (m_currentPageLabel = GetCtrl(UI.LBL_NOW).GetComponent<UILabel>());

	protected UILabel MaxPageLabel => m_maxPageLabel ?? (m_maxPageLabel = GetCtrl(UI.LBL_MAX).GetComponent<UILabel>());

	protected UIPanel MainFramePanel => m_mainFramePanel ?? (m_mainFramePanel = GetCtrl(UI.MAIN_FRAME).GetComponent<UIPanel>());

	protected UIWidget WidgetBotButtonsRoot => m_widgetBotButtonsRoot ?? (m_widgetBotButtonsRoot = GetCtrl(UI.WGT_BOT_BUTTONS).GetComponent<UIWidget>());

	public void Initialize(InitParam _param)
	{
		StartCoroutine(InitCoroutine(_param));
	}

	private IEnumerator InitCoroutine(InitParam _param)
	{
		InitUI();
		UIPanel[] panels = GetComponentsInChildren<UIPanel>(true);
		for (int j = 0; j < panels.Length; j++)
		{
			panels[j].depth += 6200;
		}
		SetRootAlpha(0f);
		HideTabRootObj();
		if (_param == null)
		{
			OnClickBackButton();
		}
		else
		{
			m_mainChat = _param.Mainchat;
			m_onClick = _param.OnClick;
			int tabCount = CreateMemberListUI(_param);
			if (tabCount >= 1)
			{
				yield return (object)RegisterRequiredResourcesData();
				for (int i = 0; i < m_uiCtrlArray.Length; i++)
				{
					if (m_uiCtrlArray[i] != null)
					{
						m_currentTabType = (UI_CTRL_TYPE)i;
						break;
					}
				}
				if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
				{
					MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
					OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
				}
				SwitchUICtrl(m_currentTabType);
			}
		}
	}

	private int CreateMemberListUI(InitParam _param)
	{
		int num = 0;
		if (_param.IsDisplayMutualFollower)
		{
			num++;
			HomeMutualFollowerListUIController.InitParam initParam = new HomeMutualFollowerListUIController.InitParam();
			initParam.ListCtrl = this;
			initParam.CoroutineExecutor = this;
			initParam.MaxPageNumber = 1;
			initParam.OnCompleteAllItemLoading = OnCompleteAllItemLoading;
			m_uiCtrlArray[0] = new HomeMutualFollowerListUIController(initParam);
		}
		if (_param.IsDisplayClanMember)
		{
			num++;
			ClanMemberListUIController.InitParam initParam2 = new ClanMemberListUIController.InitParam();
			initParam2.CoroutineExecutor = this;
			initParam2.MaxPageNumber = 1;
			initParam2.OnCompleteAllItemLoading = OnCompleteAllItemLoading;
			m_uiCtrlArray[0] = new ClanMemberListUIController(initParam2);
		}
		return num;
	}

	private IEnumerator RegisterRequiredResourcesData()
	{
		if (m_uiCtrlArray != null && m_uiCtrlArray.Length >= 1 && !IsLoadingObject)
		{
			m_isLoadingObject = true;
			LoadingQueue load_queue = new LoadingQueue(this);
			for (int i = 0; i < m_uiCtrlArray.Length; i++)
			{
				if (m_uiCtrlArray[i] != null)
				{
					LoadObject loadEventObj = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, m_uiCtrlArray[i].GetItemPrefabName());
					if (load_queue.IsLoading())
					{
						yield return (object)load_queue.Wait();
					}
					AddPrefab(loadEventObj.loadedObject as GameObject, loadEventObj.PopInstantiatedGameObject());
				}
			}
			m_isLoadingObject = false;
		}
	}

	private void OnCompleteAllItemLoading(int _loadCompleteCount)
	{
		int num = Mathf.Min(CurrentCtrl.GetItemListDataCount(), m_visibleItemCount);
		if (_loadCompleteCount >= num)
		{
			SetRootAlpha(1f);
		}
	}

	public void UpdateAllUI(Action _action)
	{
		_action?.Invoke();
	}

	public void InvokeCoroutine(IEnumerator _enumerator)
	{
		if (_enumerator != null)
		{
			StartCoroutine(_enumerator);
		}
	}

	private void SetRootAlpha(float _value)
	{
		if (!((UnityEngine.Object)RootPanel == (UnityEngine.Object)null))
		{
			float alpha = Mathf.Clamp01(_value);
			MainFramePanel.alpha = alpha;
			WidgetBotButtonsRoot.alpha = alpha;
		}
	}

	public void ShowMutualFollowerListUI()
	{
		SwitchUICtrl(UI_CTRL_TYPE.MUTUAL_FOLLOWER);
	}

	public void ShowClanMemberListUI()
	{
		SwitchUICtrl(UI_CTRL_TYPE.CLAN_MEMBER);
	}

	private void SwitchUICtrl(UI_CTRL_TYPE _type)
	{
		if (m_uiCtrlArray != null && m_uiCtrlArray.Length >= 1 && _type >= UI_CTRL_TYPE.MUTUAL_FOLLOWER && m_uiCtrlArray.Length > (int)_type && m_uiCtrlArray[(int)_type] != null)
		{
			m_uiCtrlArray[(int)_type].SetInitPageInfo();
			SetTitleText(m_uiCtrlArray[(int)_type].GetChatTitle());
		}
	}

	private void ShowTabRootObj()
	{
		SwitchTabRootObjActivation(true);
	}

	private void HideTabRootObj()
	{
		SwitchTabRootObjActivation(false);
	}

	private void SwitchTabRootObjActivation(bool _isActivate)
	{
		if (!((UnityEngine.Object)TabRootObject == (UnityEngine.Object)null) && TabRootObject.activeSelf != _isActivate)
		{
			TabRootObject.SetActive(_isActivate);
		}
	}

	public void SetCurrentPageNum(int _pageNum)
	{
		if (_pageNum >= 0)
		{
			CurrentPageLabel.text = _pageNum.ToString();
		}
	}

	public void SetMaxPageNum(int _pageNum)
	{
		if (_pageNum >= 0)
		{
			MaxPageLabel.text = _pageNum.ToString();
		}
	}

	private void SetTitleText(string _text)
	{
		if ((UnityEngine.Object)m_titleText_Top != (UnityEngine.Object)null)
		{
			m_titleText_Top.text = _text;
		}
		if ((UnityEngine.Object)m_titleText_Bot != (UnityEngine.Object)null)
		{
			m_titleText_Bot.text = _text;
		}
	}

	public override void UpdateUI()
	{
		int itemListDataCount = m_uiCtrlArray[(int)m_currentTabType].GetItemListDataCount();
		if (itemListDataCount == 0)
		{
			UiGrid.gameObject.SetActive(false);
			SetActive(UI.OBJ_ACTIVE_ROOT, false);
			SetActive(UI.OBJ_INACTIVE_ROOT, true);
			SetLabelText(UI.LBL_NOW, "0");
			SetLabelText(UI.LBL_MAX, "0");
			OnCompleteAllItemLoading(itemListDataCount);
		}
		else
		{
			UiGrid.gameObject.SetActive(true);
			bool flag = m_uiCtrlArray[(int)m_currentTabType].MaxPageNum > 1;
			SetActive(UI.OBJ_ACTIVE_ROOT, flag);
			SetActive(UI.OBJ_INACTIVE_ROOT, !flag);
			UpdateDynamicList();
		}
	}

	protected void UpdateDynamicList()
	{
		int itemListDataCount = m_uiCtrlArray[(int)m_currentTabType].GetItemListDataCount();
		if (GameDefine.ACTIVE_DEGREE)
		{
			UiGrid.cellHeight = (float)GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
		}
		m_visibleItemCount = Mathf.FloorToInt(ScrollViewPanel.height / UiGrid.cellHeight);
		object grid_ctrl_enum = UI.GRD_LIST;
		string itemPrefabName = m_uiCtrlArray[(int)m_currentTabType].GetItemPrefabName();
		int item_num = itemListDataCount;
		ScrollItemListControllerBase obj = m_uiCtrlArray[(int)m_currentTabType];
		SetDynamicList((Enum)grid_ctrl_enum, itemPrefabName, item_num, false, null, null, obj.SetListItem);
	}

	private void OnScreenRotate(bool _isPortrait)
	{
		Vector4 vector = (!_isPortrait) ? WIDGET_ANCHOR_MAIN_FRAME_SPLIT_LANDSCAPE_SETTINGS : WIDGET_ANCHOR_MAIN_FRAME_DEFAULT_SETTINGS;
		MainFramePanel.leftAnchor.Set(0f, vector.x);
		MainFramePanel.rightAnchor.Set(1f, vector.y);
		MainFramePanel.bottomAnchor.Set(0f, vector.z);
		MainFramePanel.topAnchor.Set(1f, vector.w);
		vector = ((!_isPortrait) ? WIDGET_ANCHOR_BOT_BTN_ROOT_LANDSCAPE_SETTINGS : WIDGET_ANCHOR_BOT_BTN_ROOT_DEFAULT_SETTINGS);
		WidgetBotButtonsRoot.leftAnchor.Set(0f, vector.x);
		WidgetBotButtonsRoot.rightAnchor.Set(1f, vector.y);
		WidgetBotButtonsRoot.bottomAnchor.Set(0f, vector.z);
		WidgetBotButtonsRoot.topAnchor.Set(0f, vector.w);
	}

	public void OnClickBackButton()
	{
		if (!IsConnetingNetwork() && (UnityEngine.Object)m_mainChat != (UnityEngine.Object)null)
		{
			m_mainChat.PopState();
		}
	}

	public void OnClickItem()
	{
		if (!IsConnetingNetwork() && (UnityEngine.Object)m_mainChat != (UnityEngine.Object)null)
		{
			m_mainChat.PopState();
			m_mainChat.PushNextState(typeof(ChatState_PersonalMsgView));
		}
	}

	public void OnClickNextPage()
	{
		if (CurrentCtrl != null && !IsConnetingNetwork())
		{
			CurrentCtrl.MoveOnNextPage();
		}
	}

	public void OnClickPrevPage()
	{
		if (CurrentCtrl != null && !IsConnetingNetwork())
		{
			CurrentCtrl.MoveOnPrevPage();
		}
	}

	public void OnClickSwitchInfoButton()
	{
	}

	public bool IsConnetingNetwork()
	{
		return CurrentCtrl != null && CurrentCtrl.IsRequestNextPageInfo;
	}

	public bool IsInitializing()
	{
		return IsConnetingNetwork() || IsLoadingObject;
	}
}
