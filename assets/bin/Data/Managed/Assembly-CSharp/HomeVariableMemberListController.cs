// Decompiled with JetBrains decompiler
// Type: HomeVariableMemberListController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeVariableMemberListController : UIBehaviour, IUpdatexecutor
{
  private const float ANCHOR_LEFT = 0.0f;
  private const float ANCHOR_CENTER = 0.5f;
  private const float ANCHOR_RIGHT = 1f;
  private const float ANCHOR_BOT = 0.0f;
  private const float ANCHOR_TOP = 1f;
  private static readonly Vector4 WIDGET_ANCHOR_MAIN_FRAME_DEFAULT_SETTINGS = new Vector4(16f, -16f, 210f, -104f);
  private static readonly Vector4 WIDGET_ANCHOR_MAIN_FRAME_SPLIT_LANDSCAPE_SETTINGS = new Vector4(180f, -180f, 80f, -20f);
  private static readonly Vector4 WIDGET_ANCHOR_PAGE_ROOT_DEFAULT_SETTINGS = new Vector4(-100f, 100f, 108f, 182f);
  private static readonly Vector4 WIDGET_ANCHOR_PAGE_ROOT_LANDSCAPE_SETTINGS = new Vector4(-100f, 100f, 10f, 80f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_BTN_ROOT_DEFAULT_SETTINGS = new Vector4(0.0f, 0.0f, 120f, 200f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_BTN_ROOT_LANDSCAPE_SETTINGS = new Vector4(0.0f, 0.0f, 5f, 70f);
  [SerializeField]
  private UILabel m_titleText_Top;
  [SerializeField]
  private UILabel m_titleText_Bot;
  [SerializeField]
  private UIButton[] m_uiCtrlSwitchTabs = new UIButton[2];
  private ScrollItemListControllerBase[] m_uiCtrlArray = new ScrollItemListControllerBase[2];
  private HomeVariableMemberListController.UI_CTRL_TYPE m_currentTabType = HomeVariableMemberListController.UI_CTRL_TYPE.UNDEFINED;
  private bool m_isLoadingObject;
  private MainChat m_mainChat;
  private System.Action m_onClick;
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

  private ScrollItemListControllerBase CurrentCtrl
  {
    get
    {
      return this.m_currentTabType == HomeVariableMemberListController.UI_CTRL_TYPE.UNDEFINED ? (ScrollItemListControllerBase) null : this.m_uiCtrlArray[(int) this.m_currentTabType];
    }
  }

  public bool IsLoadingObject => this.m_isLoadingObject;

  protected UIPanel RootPanel
  {
    get
    {
      return this.m_rootPanel ?? (this.m_rootPanel = ((Component) ((Component) this).transform.GetChild(0)).GetComponent<UIPanel>());
    }
  }

  protected GameObject TabRootObject
  {
    get
    {
      return this.m_tabRootObject ?? (this.m_tabRootObject = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.HEADER_TABS)).gameObject);
    }
  }

  protected UIPanel ScrollViewPanel
  {
    get
    {
      return this.m_scrollViewPanel ?? (this.m_scrollViewPanel = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.SCR_LIST)).GetComponent<UIPanel>());
    }
  }

  protected UIScrollView ScrollView
  {
    get
    {
      return this.m_scrollView ?? (this.m_scrollView = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.SCR_LIST)).GetComponent<UIScrollView>());
    }
  }

  protected UIGrid UiGrid
  {
    get
    {
      return this.m_uiGrid ?? (this.m_uiGrid = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.GRD_LIST)).GetComponent<UIGrid>());
    }
  }

  protected UILabel CurrentPageLabel
  {
    get
    {
      return this.m_currentPageLabel ?? (this.m_currentPageLabel = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.LBL_NOW)).GetComponent<UILabel>());
    }
  }

  protected UILabel MaxPageLabel
  {
    get
    {
      return this.m_maxPageLabel ?? (this.m_maxPageLabel = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.LBL_MAX)).GetComponent<UILabel>());
    }
  }

  protected UIPanel MainFramePanel
  {
    get
    {
      return this.m_mainFramePanel ?? (this.m_mainFramePanel = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.MAIN_FRAME)).GetComponent<UIPanel>());
    }
  }

  protected UIWidget WidgetBotButtonsRoot
  {
    get
    {
      return this.m_widgetBotButtonsRoot ?? (this.m_widgetBotButtonsRoot = ((Component) this.GetCtrl((Enum) HomeVariableMemberListController.UI.WGT_BOT_BUTTONS)).GetComponent<UIWidget>());
    }
  }

  public void Initialize(HomeVariableMemberListController.InitParam _param)
  {
    this.StartCoroutine(this.InitCoroutine(_param));
  }

  private IEnumerator InitCoroutine(HomeVariableMemberListController.InitParam _param)
  {
    this.InitUI();
    foreach (UIPanel componentsInChild in ((Component) this).GetComponentsInChildren<UIPanel>(true))
      componentsInChild.depth += 6200;
    this.SetRootAlpha(0.0f);
    this.HideTabRootObj();
    if (_param == null)
    {
      this.OnClickBackButton();
    }
    else
    {
      this.m_mainChat = _param.Mainchat;
      this.m_onClick = _param.OnClick;
      if (this.CreateMemberListUI(_param) >= 1)
      {
        yield return (object) this.RegisterRequiredResourcesData();
        for (int index = 0; index < this.m_uiCtrlArray.Length; ++index)
        {
          if (this.m_uiCtrlArray[index] != null)
          {
            this.m_currentTabType = (HomeVariableMemberListController.UI_CTRL_TYPE) index;
            break;
          }
        }
        if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
        {
          MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
          this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
        }
        this.SwitchUICtrl(this.m_currentTabType);
      }
    }
  }

  private int CreateMemberListUI(HomeVariableMemberListController.InitParam _param)
  {
    int memberListUi = 0;
    if (_param.IsDisplayMutualFollower)
    {
      ++memberListUi;
      HomeMutualFollowerListUIController.InitParam _initParam = new HomeMutualFollowerListUIController.InitParam();
      _initParam.ListCtrl = this;
      _initParam.CoroutineExecutor = (IUpdatexecutor) this;
      _initParam.MaxPageNumber = 1;
      _initParam.OnCompleteAllItemLoading = new Action<int>(this.OnCompleteAllItemLoading);
      this.m_uiCtrlArray[0] = (ScrollItemListControllerBase) new HomeMutualFollowerListUIController(_initParam);
    }
    if (_param.IsDisplayClanMember)
    {
      ++memberListUi;
      ClanMemberListUIController.InitParam _initParam = new ClanMemberListUIController.InitParam();
      _initParam.CoroutineExecutor = (IUpdatexecutor) this;
      _initParam.MaxPageNumber = 1;
      _initParam.OnCompleteAllItemLoading = new Action<int>(this.OnCompleteAllItemLoading);
      this.m_uiCtrlArray[0] = (ScrollItemListControllerBase) new ClanMemberListUIController(_initParam);
    }
    return memberListUi;
  }

  private IEnumerator RegisterRequiredResourcesData()
  {
    if (this.m_uiCtrlArray != null && this.m_uiCtrlArray.Length >= 1 && !this.IsLoadingObject)
    {
      this.m_isLoadingObject = true;
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
      for (int i = 0; i < this.m_uiCtrlArray.Length; ++i)
      {
        if (this.m_uiCtrlArray[i] != null)
        {
          LoadObject loadEventObj = (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, this.m_uiCtrlArray[i].GetItemPrefabName());
          if (load_queue.IsLoading())
            yield return (object) load_queue.Wait();
          this.AddPrefab(loadEventObj.loadedObject as GameObject, loadEventObj.PopInstantiatedGameObject());
          loadEventObj = (LoadObject) null;
        }
      }
      this.m_isLoadingObject = false;
    }
  }

  private void OnCompleteAllItemLoading(int _loadCompleteCount)
  {
    int num = Mathf.Min(this.CurrentCtrl.GetItemListDataCount(), this.m_visibleItemCount);
    if (_loadCompleteCount < num)
      return;
    this.SetRootAlpha(1f);
  }

  public void UpdateAllUI(System.Action _action)
  {
    if (_action == null)
      return;
    _action();
  }

  private void Update() => this.ProcessBackKey();

  public void InvokeCoroutine(IEnumerator _enumerator)
  {
    if (_enumerator == null)
      return;
    this.StartCoroutine(_enumerator);
  }

  private void SetRootAlpha(float _value)
  {
    if (Object.op_Equality((Object) this.RootPanel, (Object) null))
      return;
    float num = Mathf.Clamp01(_value);
    this.MainFramePanel.alpha = num;
    this.WidgetBotButtonsRoot.alpha = num;
  }

  public void ShowMutualFollowerListUI()
  {
    this.SwitchUICtrl(HomeVariableMemberListController.UI_CTRL_TYPE.MUTUAL_FOLLOWER);
  }

  public void ShowClanMemberListUI()
  {
    this.SwitchUICtrl(HomeVariableMemberListController.UI_CTRL_TYPE.CLAN_MEMBER);
  }

  private void SwitchUICtrl(
    HomeVariableMemberListController.UI_CTRL_TYPE _type)
  {
    int index = (int) _type;
    if (this.m_uiCtrlArray == null || this.m_uiCtrlArray.Length < 1 || index < 0 || this.m_uiCtrlArray.Length <= index || this.m_uiCtrlArray[index] == null)
      return;
    this.m_uiCtrlArray[index].SetInitPageInfo();
    this.SetTitleText(this.m_uiCtrlArray[index].GetChatTitle());
  }

  private void ShowTabRootObj() => this.SwitchTabRootObjActivation(true);

  private void HideTabRootObj() => this.SwitchTabRootObjActivation(false);

  private void SwitchTabRootObjActivation(bool _isActivate)
  {
    if (Object.op_Equality((Object) this.TabRootObject, (Object) null) || this.TabRootObject.activeSelf == _isActivate)
      return;
    this.TabRootObject.SetActive(_isActivate);
  }

  public void SetCurrentPageNum(int _pageNum)
  {
    if (_pageNum < 0)
      return;
    this.CurrentPageLabel.text = _pageNum.ToString();
  }

  public void SetMaxPageNum(int _pageNum)
  {
    if (_pageNum < 0)
      return;
    this.MaxPageLabel.text = _pageNum.ToString();
  }

  private void SetTitleText(string _text)
  {
    if (Object.op_Inequality((Object) this.m_titleText_Top, (Object) null))
      this.m_titleText_Top.text = _text;
    if (!Object.op_Inequality((Object) this.m_titleText_Bot, (Object) null))
      return;
    this.m_titleText_Bot.text = _text;
  }

  public override void UpdateUI()
  {
    int itemListDataCount = this.m_uiCtrlArray[(int) this.m_currentTabType].GetItemListDataCount();
    if (itemListDataCount == 0)
    {
      ((Component) this.UiGrid).gameObject.SetActive(false);
      this.SetActive((Enum) HomeVariableMemberListController.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) HomeVariableMemberListController.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) HomeVariableMemberListController.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) HomeVariableMemberListController.UI.LBL_MAX, "0");
      this.OnCompleteAllItemLoading(itemListDataCount);
    }
    else
    {
      ((Component) this.UiGrid).gameObject.SetActive(true);
      bool is_visible = this.m_uiCtrlArray[(int) this.m_currentTabType].MaxPageNum > 1;
      this.SetActive((Enum) HomeVariableMemberListController.UI.OBJ_ACTIVE_ROOT, is_visible);
      this.SetActive((Enum) HomeVariableMemberListController.UI.OBJ_INACTIVE_ROOT, !is_visible);
      this.UpdateDynamicList();
    }
  }

  protected void UpdateDynamicList()
  {
    int itemListDataCount = this.m_uiCtrlArray[(int) this.m_currentTabType].GetItemListDataCount();
    if (GameDefine.ACTIVE_DEGREE)
      this.UiGrid.cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.m_visibleItemCount = Mathf.FloorToInt(this.ScrollViewPanel.height / this.UiGrid.cellHeight);
    this.SetDynamicList((Enum) HomeVariableMemberListController.UI.GRD_LIST, this.m_uiCtrlArray[(int) this.m_currentTabType].GetItemPrefabName(), itemListDataCount, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, new Action<int, Transform, bool>(this.m_uiCtrlArray[(int) this.m_currentTabType].SetListItem));
  }

  private void OnScreenRotate(bool _isPortrait)
  {
    Vector4 vector4_1 = _isPortrait ? HomeVariableMemberListController.WIDGET_ANCHOR_MAIN_FRAME_DEFAULT_SETTINGS : HomeVariableMemberListController.WIDGET_ANCHOR_MAIN_FRAME_SPLIT_LANDSCAPE_SETTINGS;
    this.MainFramePanel.leftAnchor.Set(0.0f, vector4_1.x);
    this.MainFramePanel.rightAnchor.Set(1f, vector4_1.y);
    this.MainFramePanel.bottomAnchor.Set(0.0f, vector4_1.z);
    this.MainFramePanel.topAnchor.Set(1f, vector4_1.w);
    Vector4 vector4_2 = _isPortrait ? HomeVariableMemberListController.WIDGET_ANCHOR_BOT_BTN_ROOT_DEFAULT_SETTINGS : HomeVariableMemberListController.WIDGET_ANCHOR_BOT_BTN_ROOT_LANDSCAPE_SETTINGS;
    this.WidgetBotButtonsRoot.leftAnchor.Set(0.0f, vector4_2.x);
    this.WidgetBotButtonsRoot.rightAnchor.Set(1f, vector4_2.y);
    this.WidgetBotButtonsRoot.bottomAnchor.Set(0.0f, vector4_2.z);
    this.WidgetBotButtonsRoot.topAnchor.Set(0.0f, vector4_2.w);
  }

  public void ProcessBackKey()
  {
    if (!Input.GetKeyUp((KeyCode) 27))
      return;
    this.OnClickBackButton();
  }

  public void OnClickBackButton()
  {
    if (this.IsConnetingNetwork() || !Object.op_Inequality((Object) this.m_mainChat, (Object) null))
      return;
    this.m_mainChat.PopState();
  }

  public void OnClickItem()
  {
    if (this.IsConnetingNetwork() || !Object.op_Inequality((Object) this.m_mainChat, (Object) null))
      return;
    this.m_mainChat.PopState();
    this.m_mainChat.PushNextState(typeof (ChatState_PersonalMsgView));
  }

  public void OnClickNextPage()
  {
    if (this.CurrentCtrl == null || this.IsConnetingNetwork())
      return;
    this.CurrentCtrl.MoveOnNextPage();
  }

  public void OnClickPrevPage()
  {
    if (this.CurrentCtrl == null || this.IsConnetingNetwork())
      return;
    this.CurrentCtrl.MoveOnPrevPage();
  }

  public void OnClickSwitchInfoButton()
  {
  }

  public bool IsConnetingNetwork()
  {
    return this.CurrentCtrl != null && this.CurrentCtrl.IsRequestNextPageInfo;
  }

  public bool IsInitializing() => this.IsConnetingNetwork() || this.IsLoadingObject;

  public class InitParam
  {
    public bool IsDisplayMutualFollower;
    public bool IsDisplayClanMember;
    public System.Action OnClick;
    public MainChat Mainchat;
  }

  private enum UI_CTRL_TYPE
  {
    UNDEFINED = -1, // 0xFFFFFFFF
    MUTUAL_FOLLOWER = 0,
    CLAN_MEMBER = 1,
    MAX = 2,
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
    BTN_BACK,
  }
}
