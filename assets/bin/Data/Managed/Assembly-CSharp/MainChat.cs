// Decompiled with JetBrains decompiler
// Type: MainChat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class MainChat : UIBehaviour
{
  public const string EVENT_AGE_CONFIRM = "CHAT_AGE_CONFIRM";
  public static readonly string HEADER_BUTTON_PREFAB_PATH = "InternalUI/UI_Chat/ChatHeaderButton";
  public const int HEADER_BUTTON_COUNT = 4;
  private readonly Vector3[] HEADER_BUTTON_PORTRAIT_POS = new Vector3[4]
  {
    new Vector3(-217f, 33.4f, -0.0f),
    new Vector3(-60f, 33.4f, -0.0f),
    new Vector3(45f, 33.4f, -0.0f),
    new Vector3(209f, 33f, -0.0f)
  };
  private readonly Vector3[] HEADER_BUTTON_LANDSCAPE_POS = new Vector3[4]
  {
    new Vector3(-234.4f, 33.4f, -0.0f),
    new Vector3(-65f, 33.9f, -0.0f),
    new Vector3(50f, 33.4f, -0.0f),
    new Vector3(226.5f, 33f, -0.0f)
  };
  private static readonly string CHANNEL_FORMAT = "0000";
  private const int ITEM_COUNT_MAX = 30;
  private const int CHAT_ITEM_OFFSET = 22;
  private const int FORCE_SCROLL_LIMIT_PORTRAIT = 300;
  private const int FORCE_SCROLL_LIMIT_LANDSCAPE = 344;
  private const float SOFTNESS_HEIGHT = 10f;
  private const float SPRING_STRENGTH = 20f;
  private const float CHAT_WIDTH = 410f;
  private const float SCROLL_BAR_OFFSET = 48f;
  private const float SLIDER_OFFSET = 10f;
  private readonly Vector3 CHAT_ITEM_OFFSET_POS = new Vector3(-5f, 0.0f, 0.0f);
  private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0.0f);
  private readonly Vector3 ROOM_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0.0f);
  private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0.0f);
  private readonly Vector3 ROOM_SLIDER_CLOSE_POS = new Vector3(-180f, -150f, 0.0f);
  private const float ANCHOR_LEFT = 0.0f;
  private const float ANCHOR_CENTER = 0.5f;
  private const float ANCHOR_RIGHT = 1f;
  private const float ANCHOR_BOT = 0.0f;
  private const float ANCHOR_TOP = 1f;
  private static readonly Vector4 WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS = new Vector4(-240f, 240f, -427f, 427f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS = new Vector4(-240f, 240f, 71f, 390f);
  private static readonly Vector4 WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS = new Vector4(-15f, 500f, 0.0f, 0.0f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_LANDSCAPE_SETTINGS = new Vector4(-560f, -62f, 0.0f, 320f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS = new Vector4(-405f, 0.0f, 0.0f, 375f);
  private static readonly Vector4 UI_BTN_INPUT_CLOSE_DEFAULT_POS = new Vector4(-33f, 3f, -62f, -1f);
  private static readonly Vector4 UI_BTN_INPUT_CLOSE_LANDSCAPE_POS = new Vector4(-31f, 4f, 18f, 79f);
  private static readonly Vector4 UI_SPRITE_CHAT_BG_FRAME_DEFAULT_POS = new Vector4(23f, -31f, 313f, -53f);
  private static readonly Vector4 UI_SPRITE_CHAT_BG_FRAME_PERSONAL_POS = new Vector4(23f, -31f, 15f, -53f);
  private const int STAMP_COL_DEFAULT_COUNT = 5;
  private const int STAMP_COL_LANDSCAPE_COUNT = 4;
  private const int STAMP_ROW_DEFAULT_COUNT = 2;
  private const int STAMP_ROW_LANDSCAPE_COUNT = 3;
  private GameObject m_ChatAdvisaryItemPrefab;
  private GameObject m_ChatItemPrefab;
  private GameObject m_ChatStampListPrefab;
  private Vector3 SLIDER_OPEN_POS;
  private Vector3 SLIDER_CLOSE_POS;
  private MainChat.ChatItemListData[] m_DataList = new MainChat.ChatItemListData[Enum.GetNames(typeof (MainChat.CHAT_TYPE)).Length];
  private ChatMessageUserUIController m_msgUiCtrl;
  private UIScrollView m_ScrollView;
  private Transform m_ScrollViewTrans;
  private SpringPanel m_ScrollViewSpring;
  private UIWidget m_DummyDragScroll;
  private BoxCollider m_DragScrollCollider;
  private Transform m_DragScrollTrans;
  private UIInput m_Input;
  private ChatInputFrame m_InputFrame;
  private UIRect m_RootRect;
  public MainChat.HOME_TYPE HomeType;
  private List<int> m_StampIdListCanPost;
  private List<ChatHeaderButtonController> m_headerButtonList = new List<ChatHeaderButtonController>(4);
  private GameObject m_chatCloseButtonObj;
  private GameObject m_stampEditButtonObj;
  private GameObject m_favStampEditButtonObj;
  private UIWidget m_widgetTop;
  private UIWidget m_widgetTopHeader;
  private UISprite m_BackgroundInFrame;
  private UIPanel m_subHeaderPanel;
  private GameObject m_channelSelectSpriteButtonObject;
  private UILabel m_ChannelName;
  private GameObject m_showUserListButtonObject;
  private UIScrollView m_personalMsgScrollView;
  private UIGrid m_personalMsgGrid;
  private GameObject m_personalMsgGridObj;
  private UIWidget m_widgetBot;
  private UIWidget m_widgetChatRoot;
  private UIPanel m_scrollPanel;
  private UIGrid m_stampScrollGrid;
  private UISprite m_stampChatFrame;
  private UIPanel m_stampScrollPanel;
  private UIWidget m_spriteBgBlack;
  private UIWidget m_widgetInputCloseButton;
  private bool m_isShowFullChatView;
  private bool m_isPortrait;
  private Stack<System.Type> m_stateStack = new Stack<System.Type>();
  private ChatStateMachine<ChatState> m_stateMachine;
  private MainChat.PostRequestQueue[] m_PostRequetQueue = new MainChat.PostRequestQueue[Enum.GetNames(typeof (MainChat.CHAT_TYPE)).Length];
  private IEnumerator m_handlPostChatPorcess;
  private List<ChatItem> tmpList = new List<ChatItem>();
  private bool isFieldChat;
  private bool isMinimizable;
  private ChatUIFadeGroup logView;
  private ChatUIFadeGroup inputView;
  private ChatUIFadeGroup inputBG;
  private ChatUIFadeGroup channelSelect;
  private ChatUIFadeGroup chatOpenButton;
  private ChatUIFadeGroup bgBlack;
  private ChatUIFadeGroup sendBlockView;
  private ChatUIFadeGroup sendLimitView;
  private ChatUIFadeGroup sendLimitNoClanView;
  private ChatUIFadeGroup noConnectionView;
  private ChatUIFadeGroup sendLimitReloadView;
  private ChatChannelInputPanel channelInputPanel;
  private ChatStampFavoriteEdit stampFavoriteEdit;
  private ChatStampAll stampAll;
  private GuildChatAdvisoryItem chatAdvisoryItem;
  private bool isEnableOpenButton;
  public bool UseNoClanBlock;
  private bool isFirstSlectClanTab;
  private bool m_IsOnshotStampMode;
  private float tmpOffsetStart;
  private float dragTime;
  public bool IsDraging;
  private int m_LastPendingQueueCount;
  private List<UIBehaviour> m_Observers = new List<UIBehaviour>();

  public MainChat.ChatItemListData CurrentData => this.m_DataList[(int) this.currentChat];

  private UIScrollView ScrollView
  {
    get
    {
      return this.m_ScrollView ?? (this.m_ScrollView = ((Component) this.GetCtrl((Enum) MainChat.UI.SCR_CHAT)).GetComponent<UIScrollView>());
    }
  }

  private Transform ScrollViewTrans
  {
    get
    {
      return this.m_ScrollViewTrans ?? (this.m_ScrollViewTrans = ((Component) this.ScrollView).transform);
    }
  }

  private SpringPanel ScrollViewSpring
  {
    get
    {
      return this.m_ScrollViewSpring ?? (this.m_ScrollViewSpring = ((Component) this.GetCtrl((Enum) MainChat.UI.SCR_CHAT)).GetComponent<SpringPanel>());
    }
  }

  private UIWidget DummyDragScroll
  {
    get
    {
      return this.m_DummyDragScroll ?? (this.m_DummyDragScroll = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_DUMMY_DRAG_SCROLL)).GetComponent<UIWidget>());
    }
  }

  private BoxCollider DragScrollCollider
  {
    get
    {
      return this.m_DragScrollCollider ?? (this.m_DragScrollCollider = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_DUMMY_DRAG_SCROLL)).GetComponent<BoxCollider>());
    }
  }

  private Transform DragScrollTrans
  {
    get
    {
      return this.m_DragScrollTrans ?? (this.m_DragScrollTrans = ((Component) this.DragScrollCollider).transform);
    }
  }

  private UIInput Input
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Input, (Object) null))
        this.m_Input = ((Component) this.GetCtrl((Enum) MainChat.UI.IPT_POST)).GetComponent<UIInput>();
      return this.m_Input;
    }
  }

  private ChatInputFrame InputFrame
  {
    get
    {
      if (Object.op_Equality((Object) this.m_InputFrame, (Object) null))
        this.m_InputFrame = ((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_INPUT_FRAME)).GetComponent<ChatInputFrame>();
      return this.m_InputFrame;
    }
  }

  private UIRect RootRect
  {
    get
    {
      if (Object.op_Equality((Object) this.m_RootRect, (Object) null))
        this.m_RootRect = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_CHAT_ROOT)).GetComponent<UIRect>();
      return this.m_RootRect;
    }
  }

  public MainChat.CHAT_TYPE currentChat { get; private set; }

  public static bool splitLogView
  {
    get => true;
    set
    {
    }
  }

  private GameObject ChatCloseButtonObj
  {
    get
    {
      return this.m_chatCloseButtonObj ?? (this.m_chatCloseButtonObj = ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_INPUT_CLOSE)).gameObject);
    }
  }

  private GameObject StampEditButtonObj
  {
    get
    {
      return this.m_stampEditButtonObj ?? (this.m_stampEditButtonObj = ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_EDIT)).gameObject);
    }
  }

  private GameObject FavStampEditButtonObj
  {
    get
    {
      return this.m_favStampEditButtonObj ?? (this.m_favStampEditButtonObj = ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SHOW_ALL)).gameObject);
    }
  }

  private UIWidget WidgetTop
  {
    get
    {
      return this.m_widgetTop ?? (this.m_widgetTop = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_ANCHOR_TOP)).GetComponent<UIWidget>());
    }
  }

  private UIWidget WidgetTopHeader
  {
    get
    {
      return this.m_widgetTopHeader ?? (this.m_widgetTopHeader = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_HEADER_SPACE)).GetComponent<UIWidget>());
    }
  }

  private UISprite BackgroundInFrame
  {
    get
    {
      return this.m_BackgroundInFrame ?? (this.m_BackgroundInFrame = ((Component) this.GetCtrl((Enum) MainChat.UI.SPR_BG_IN_FRAME)).GetComponent<UISprite>());
    }
  }

  private UIPanel SubHeaderPanel
  {
    get
    {
      return this.m_subHeaderPanel ?? (this.m_subHeaderPanel = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_SUB_HEADER_SPACE)).GetComponent<UIPanel>());
    }
  }

  private GameObject ChannelSelectSpriteButtonObject
  {
    get
    {
      return this.m_channelSelectSpriteButtonObject ?? (this.m_channelSelectSpriteButtonObject = ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SPR_CHANNEL_SELECT)).gameObject);
    }
  }

  private UILabel ChannelName
  {
    get
    {
      return this.m_ChannelName ?? (this.m_ChannelName = ((Component) this.GetCtrl((Enum) MainChat.UI.LBL_CHANNEL_NAME)).GetComponent<UILabel>());
    }
  }

  private GameObject ShowUserListButtonObject
  {
    get
    {
      return this.m_showUserListButtonObject ?? (this.m_showUserListButtonObject = ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SHOW_USER_LIST)).gameObject);
    }
  }

  private UIScrollView PersonalMsgScrollView
  {
    get
    {
      return this.m_personalMsgScrollView ?? (this.m_personalMsgScrollView = ((Component) this.GetCtrl((Enum) MainChat.UI.SCR_PERSONAL_MSG_LIST_VIEW)).GetComponent<UIScrollView>());
    }
  }

  private UIGrid PersonalMsgGrid
  {
    get
    {
      return this.m_personalMsgGrid ?? (this.m_personalMsgGrid = ((Component) this.GetCtrl((Enum) MainChat.UI.GRD_PERSONAL_MSG_VIEW)).GetComponent<UIGrid>());
    }
  }

  private GameObject PersonalMsgGridObj
  {
    get
    {
      return this.m_personalMsgGridObj ?? (this.m_personalMsgGridObj = ((Component) this.PersonalMsgGrid).gameObject);
    }
  }

  private UIWidget WidgetBot
  {
    get
    {
      return this.m_widgetBot ?? (this.m_widgetBot = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_ANCHOR_BOTTOM)).GetComponent<UIWidget>());
    }
  }

  private UIWidget WidgetChatRoot
  {
    get
    {
      return this.m_widgetChatRoot ?? (this.m_widgetChatRoot = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_CHAT_ROOT)).GetComponent<UIWidget>());
    }
  }

  private UIPanel ScrollPanel
  {
    get
    {
      return this.m_scrollPanel ?? (this.m_scrollPanel = ((Component) this.GetCtrl((Enum) MainChat.UI.SCR_CHAT)).GetComponent<UIPanel>());
    }
  }

  private UIGrid StampScrollGrid
  {
    get
    {
      return this.m_stampScrollGrid ?? (this.m_stampScrollGrid = ((Component) this.GetCtrl((Enum) MainChat.UI.GRD_STAMP_LIST)).GetComponent<UIGrid>());
    }
  }

  private UISprite StampChatFrame
  {
    get
    {
      return this.m_stampChatFrame ?? (this.m_stampChatFrame = ((Component) this.GetCtrl((Enum) MainChat.UI.SPR_CHAT_FRAME)).GetComponent<UISprite>());
    }
  }

  private UIPanel StampScrollPanel
  {
    get
    {
      return this.m_stampScrollPanel ?? (this.m_stampScrollPanel = ((Component) this.GetCtrl((Enum) MainChat.UI.SCR_STAMP_LIST)).GetComponent<UIPanel>());
    }
  }

  private UIWidget SpriteBgBlack
  {
    get
    {
      return this.m_spriteBgBlack ?? (this.m_spriteBgBlack = ((Component) this.GetCtrl((Enum) MainChat.UI.SPR_BG_BLACK)).GetComponent<UIWidget>());
    }
  }

  private UIWidget WidgetInputCloseButton
  {
    get
    {
      return this.m_widgetInputCloseButton ?? (this.m_widgetInputCloseButton = ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_INPUT_CLOSE)).GetComponent<UIWidget>());
    }
  }

  private bool IsShowFullChatView => this.m_isShowFullChatView;

  private bool IsPortrait => this.m_isPortrait;

  private bool IsLandScapeFullViewMode => this.IsShowFullChatView && !this.IsPortrait;

  public ChatStateMachine<ChatState> StateMachine => this.m_stateMachine;

  private void OnEnable()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnDisable()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnScreenRotate(bool is_portrait)
  {
    this.m_isPortrait = is_portrait;
    this.InitStampList();
    this.SetBaseWidgetSettings();
    this.SetParent(MainChat.UI.BTN_HIDE_LOG, is_portrait ? MainChat.UI.OBJ_HIDE_LOG_P : MainChat.UI.OBJ_HIDE_LOG_L_2);
    this.SetHeaderButtonPosition(is_portrait);
    this.SetStampWindowSettings();
    this.SetChatBgFrameUI(this.currentChat);
    this.GetCtrl((Enum) MainChat.UI.OBJ_CHANNEL_INPUT).localScale = is_portrait ? Vector3.one : Vector3.op_Multiply(Vector3.one, 0.75f);
    this.WidgetChatRoot.bottomAnchor.Set(0.0f, is_portrait ? 72f : 0.0f);
    this.WidgetChatRoot.topAnchor.Set(1f, is_portrait ? -72f : -4f);
    this.SetScrollPanelUI(is_portrait, this.currentChat);
    float num = 1.17279065f;
    ((Component) this.SpriteBgBlack).transform.localScale = is_portrait ? Vector3.one : Vector3.op_Multiply(Vector3.one, num);
    if (this.logView.isOpened || this.logView.isOpening)
    {
      if (is_portrait)
      {
        this.inputBG.Close((System.Action) (() => { }));
      }
      else
      {
        this.inputBG.Open((System.Action) (() => { }));
        ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SHOW_LOG)).gameObject.SetActive(false);
      }
    }
    this.ScrollView.panel.widgetsAreStatic = false;
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.ScrollView.panel.widgetsAreStatic = true);
    this.UpdateCloseButtonPosition();
    this.UpdateAnchors();
  }

  private void SetStampWindowSettings()
  {
    int num1 = this.IsLandScapeFullViewMode ? 4 : 5;
    int num2 = this.IsLandScapeFullViewMode ? 3 : 2;
    this.StampScrollGrid.maxPerLine = num1;
    ((Behaviour) this.StampScrollGrid).enabled = true;
    float absolute = (float) ((double) this.StampScrollGrid.cellWidth * (double) num1 / 2.0);
    float cellHeight = this.StampScrollGrid.cellHeight;
    this.StampChatFrame.leftAnchor.Set(0.5f, -absolute);
    this.StampChatFrame.rightAnchor.Set(0.5f, absolute);
    this.StampChatFrame.topAnchor.Set(0.5f, cellHeight * 0.5f);
    this.StampChatFrame.bottomAnchor.Set(0.5f, (float) (-(double) this.StampScrollGrid.cellHeight * ((double) num2 - 0.5)));
  }

  private void SetBaseWidgetSettings()
  {
    Vector4 vector4_1 = this.IsLandScapeFullViewMode ? MainChat.WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS : MainChat.WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS;
    int num = this.IsLandScapeFullViewMode ? 1 : 0;
    this.WidgetTop.leftAnchor.Set(this.IsLandScapeFullViewMode ? 0.0f : 0.5f, vector4_1.x);
    this.WidgetTop.rightAnchor.Set(this.IsLandScapeFullViewMode ? 0.0f : 0.5f, vector4_1.y);
    this.WidgetTop.bottomAnchor.Set(this.IsLandScapeFullViewMode ? 0.0f : 0.5f, vector4_1.z);
    this.WidgetTop.topAnchor.Set(this.IsLandScapeFullViewMode ? 1f : 0.5f, vector4_1.w);
    Vector4 vector4_2 = this.IsPortrait ? MainChat.WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS : (this.IsShowFullChatView ? MainChat.WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS : MainChat.WIDGET_ANCHOR_BOT_LANDSCAPE_SETTINGS);
    this.WidgetBot.leftAnchor.Set(this.IsPortrait ? 0.5f : 1f, vector4_2.x);
    this.WidgetBot.rightAnchor.Set(this.IsPortrait ? 0.5f : 1f, vector4_2.y);
    this.WidgetBot.bottomAnchor.Set(0.0f, vector4_2.z);
    this.WidgetBot.topAnchor.Set(0.0f, vector4_2.w);
    if (!SpecialDeviceManager.HasSpecialDeviceInfo)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    if (!specialDeviceInfo.NeedModifyChatAnchor)
      return;
    if (SpecialDeviceManager.IsPortrait)
    {
      this.WidgetBot.leftAnchor.Set(0.5f, (float) specialDeviceInfo.ChatBottomAnchorPortrait.left);
      this.WidgetBot.rightAnchor.Set(0.5f, (float) specialDeviceInfo.ChatBottomAnchorPortrait.right);
      this.WidgetBot.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.ChatBottomAnchorPortrait.bottom);
      this.WidgetBot.topAnchor.Set(0.0f, (float) specialDeviceInfo.ChatBottomAnchorPortrait.top);
    }
    else
    {
      this.WidgetTop.leftAnchor.Set(0.0f, (float) specialDeviceInfo.ChatTopAnchorLandscape.left);
      this.WidgetTop.rightAnchor.Set(0.0f, (float) specialDeviceInfo.ChatTopAnchorLandscape.right);
      this.WidgetTop.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.ChatTopAnchorLandscape.bottom);
      this.WidgetTop.topAnchor.Set(1f, (float) specialDeviceInfo.ChatTopAnchorLandscape.top);
      if (this.IsShowFullChatView)
      {
        this.WidgetBot.leftAnchor.Set(1f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeFull.left);
        this.WidgetBot.rightAnchor.Set(1f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeFull.right);
        this.WidgetBot.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeFull.bottom);
        this.WidgetBot.topAnchor.Set(0.0f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeFull.top);
      }
      else
      {
        this.WidgetBot.leftAnchor.Set(1f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeSmall.left);
        this.WidgetBot.rightAnchor.Set(1f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeSmall.right);
        this.WidgetBot.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeSmall.bottom);
        this.WidgetBot.topAnchor.Set(0.0f, (float) specialDeviceInfo.ChatBottomAnchorLandscapeSmall.top);
      }
    }
  }

  private void SetScrollPanelUI(bool _isPortrait, MainChat.CHAT_TYPE _t)
  {
    switch (_t)
    {
      case MainChat.CHAT_TYPE.HOME:
        this.ScrollPanel.topAnchor.Set(1f, -129f);
        this.ScrollPanel.bottomAnchor.Set(0.0f, _isPortrait ? 316f : 22f);
        break;
      case MainChat.CHAT_TYPE.PERSONAL:
        this.ScrollPanel.topAnchor.Set(1f, -129f);
        this.ScrollPanel.bottomAnchor.Set(0.0f, _isPortrait ? 15f : 22f);
        break;
      default:
        this.ScrollPanel.topAnchor.Set(1f, -72f);
        this.ScrollPanel.bottomAnchor.Set(0.0f, _isPortrait ? 316f : 22f);
        break;
    }
    this.ScrollPanel.SetDirty();
  }

  private void SetChatBgFrameUI(MainChat.CHAT_TYPE _t)
  {
    if (Object.op_Equality((Object) this.BackgroundInFrame, (Object) null))
      return;
    Vector4 vector4 = (_t == MainChat.CHAT_TYPE.PERSONAL ? 0 : (!this.HasState(typeof (ChatState_PersonalTab)) ? 1 : 0)) == 0 || this.IsLandScapeFullViewMode ? MainChat.UI_SPRITE_CHAT_BG_FRAME_PERSONAL_POS : MainChat.UI_SPRITE_CHAT_BG_FRAME_DEFAULT_POS;
    this.BackgroundInFrame.leftAnchor.Set(0.0f, vector4.x);
    this.BackgroundInFrame.rightAnchor.Set(1f, vector4.y);
    this.BackgroundInFrame.bottomAnchor.Set(0.0f, vector4.z);
    this.BackgroundInFrame.topAnchor.Set(1f, vector4.w);
  }

  private void SetParent(MainChat.UI changeTarget, MainChat.UI parent)
  {
    Transform ctrl = this.GetCtrl((Enum) parent);
    this.SetParent(changeTarget, ctrl);
  }

  private void SetParent(MainChat.UI changeTarget, Transform parent)
  {
    Transform ctrl = this.GetCtrl((Enum) changeTarget);
    ctrl.parent = parent;
    ctrl.localPosition = Vector3.zero;
  }

  private void UpdateCloseButtonPosition()
  {
    UIWidget inputCloseButton = this.WidgetInputCloseButton;
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestAcceptRoom")
    {
      inputCloseButton.leftAnchor.Set(1f, MainChat.UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.x);
      inputCloseButton.rightAnchor.Set(1f, MainChat.UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.y);
      inputCloseButton.bottomAnchor.Set(0.0f, MainChat.UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.z);
      inputCloseButton.topAnchor.Set(0.0f, MainChat.UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.w);
    }
    else
    {
      inputCloseButton.leftAnchor.Set(1f, MainChat.UI_BTN_INPUT_CLOSE_DEFAULT_POS.x);
      inputCloseButton.rightAnchor.Set(1f, MainChat.UI_BTN_INPUT_CLOSE_DEFAULT_POS.y);
      inputCloseButton.bottomAnchor.Set(1f, MainChat.UI_BTN_INPUT_CLOSE_DEFAULT_POS.z);
      inputCloseButton.topAnchor.Set(1f, MainChat.UI_BTN_INPUT_CLOSE_DEFAULT_POS.w);
    }
  }

  private IEnumerator Start()
  {
    this.Initialize();
    this.HideOpenButton();
    this.HideDisplay();
    yield return (object) null;
    this.InitStateMachine();
    yield return (object) null;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_quest_chatitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatItem");
    LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
    LoadObject lo_chatAdvisaryItem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.InitChatDataList();
    for (int index = 0; index < this.m_PostRequetQueue.Length; ++index)
      this.m_PostRequetQueue[index] = new MainChat.PostRequestQueue();
    yield return (object) null;
    this.m_ChatItemPrefab = lo_quest_chatitem.loadedObject as GameObject;
    this.m_ChatStampListPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
    this.m_ChatAdvisaryItemPrefab = lo_chatAdvisaryItem.loadedObject as GameObject;
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    yield return (object) null;
    this.ChangeSliderPos(MainChat.CHAT_TYPE.HOME);
    this.SetSliderLimit();
    this.DummyDragScroll.width = 410;
    yield return (object) null;
    this.SetChannelName("0001");
    this.ResetStampIdList();
    this.Reset();
    this.HideAll();
    this.HideOpenButton();
    this.ScrollView.onStoppedMoving = new UIScrollView.OnDragNotification(this.onStoppedMovingScrollView);
    this.ScrollView.onDragStarted = new UIScrollView.OnDragNotification(this.onDragStartScrollView);
    this.ScrollView.onDragFinished = new UIScrollView.OnDragNotification(this.onDragEndScrollView);
    this.ScrollView.onPressStart = new UIScrollView.OnDragNotification(this.onPressStartScrollView);
    this.ScrollView.onPressEnd = new UIScrollView.OnDragNotification(this.onPressEndScrollView);
  }

  private void InitChatDataList()
  {
    this.m_DataList[0] = new MainChat.ChatItemListData(((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_HOME_ITEM_LIST_ROOT)).gameObject);
    this.m_DataList[1] = new MainChat.ChatItemListData(((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_ROOM_ITEM_LIST_ROOT)).gameObject);
    this.m_DataList[2] = new MainChat.ChatItemListData(((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_LOUNGE_ITEM_LIST_ROOT)).gameObject);
    this.m_DataList[3] = this.m_DataList[1];
    this.m_DataList[4] = (MainChat.ChatItemListData) null;
    this.m_DataList[5] = new MainChat.ChatItemListData(((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_CLAN_ITEM_LIST_ROOT)).gameObject);
  }

  private void ChangeSliderPos(MainChat.CHAT_TYPE type)
  {
    this.SLIDER_OPEN_POS = type == MainChat.CHAT_TYPE.HOME ? this.HOME_SLIDER_OPEN_POS : this.ROOM_SLIDER_OPEN_POS;
    this.SLIDER_CLOSE_POS = type == MainChat.CHAT_TYPE.HOME ? this.HOME_SLIDER_CLOSE_POS : this.ROOM_SLIDER_CLOSE_POS;
  }

  private void SetSliderLimit()
  {
    UIPanel component = ((Component) this.GetCtrl((Enum) MainChat.UI.WGT_SLIDE_LIMIT)).GetComponent<UIPanel>();
    component.topAnchor.absolute = (int) this.SLIDER_CLOSE_POS.y + 9;
    component.bottomAnchor.absolute = (int) this.SLIDER_OPEN_POS.y - 49;
  }

  private void Reset()
  {
    this.Input.value = "";
    this.InputFrame.Reset();
    this.UpdateAnchors();
    this.UpdateWindowSize();
  }

  public void SetChannelName(string name) => this.ChannelName.text = name;

  public bool IsOpeningWindow() => this.inputView.isOpened || this.inputView.isOpening;

  public void OnTouchPost()
  {
    if (!UserInfoManager.IsRegisterdAge() || !UserInfoManager.IsEnableCommunication())
      return;
    string message = this.Input.value;
    if (string.IsNullOrEmpty(message) || message.Trim().Length == 0)
      return;
    this.SendMessageAsMine(message);
    this.Input.value = "";
    this.OnInput();
  }

  public void OnInput()
  {
    this.InputFrame.FrameResize();
    this.SetActive((Enum) MainChat.UI.LBL_DEFAULT, string.IsNullOrEmpty(this.Input.value));
  }

  private void StartPostChatProcess() => this.m_handlPostChatPorcess = this.PostChatProcess();

  private void StopPostChatProcess() => this.m_handlPostChatPorcess = (IEnumerator) null;

  public int GetPendingQueueNum()
  {
    int pendingQueueNum = 0;
    if (this.m_PostRequetQueue == null)
    {
      pendingQueueNum = 0;
    }
    else
    {
      switch (this.HomeType)
      {
        case MainChat.HOME_TYPE.HOME_TOP:
          if (this.m_PostRequetQueue[0] != null)
          {
            pendingQueueNum = this.m_PostRequetQueue[0].Count;
            break;
          }
          break;
        case MainChat.HOME_TYPE.LOUNGE_TOP:
          if (this.m_PostRequetQueue[2] != null)
          {
            pendingQueueNum = this.m_PostRequetQueue[2].Count;
            break;
          }
          break;
      }
      if (this.hasRoomChat && this.m_PostRequetQueue[1] != null)
        pendingQueueNum += this.m_PostRequetQueue[1].Count;
    }
    if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      pendingQueueNum += MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
    return pendingQueueNum;
  }

  public int GetPendingQueueNumWithoutRoom()
  {
    int queueNumWithoutRoom = 0;
    if (this.m_PostRequetQueue != null)
    {
      if (this.hasLoungeChat)
      {
        if (this.m_PostRequetQueue[2] != null)
          queueNumWithoutRoom = this.m_PostRequetQueue[2].Count;
      }
      else if (this.m_PostRequetQueue[0] != null)
        queueNumWithoutRoom = this.m_PostRequetQueue[0].Count;
    }
    if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      queueNumWithoutRoom += MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
    return queueNumWithoutRoom;
  }

  private IEnumerator PostChatProcess()
  {
    bool bPolling = true;
    while (bPolling)
    {
      int queueArrayIndex = this.GetQueueArrayIndex(this.currentChat);
      if (this.m_PostRequetQueue[queueArrayIndex].Count > 0)
      {
        this.Post(this.m_PostRequetQueue[queueArrayIndex].Dequeue());
        yield return (object) null;
      }
      yield return (object) null;
    }
  }

  public int GetQueueArrayIndex(MainChat.CHAT_TYPE _t)
  {
    return _t == MainChat.CHAT_TYPE.ROOM || _t == MainChat.CHAT_TYPE.FIELD ? 1 : (int) _t;
  }

  public void SendStampAsMine(int stampId)
  {
    if (!this.CanIPostTheStamp(stampId))
      return;
    switch (this.currentChat)
    {
      case MainChat.CHAT_TYPE.HOME:
        if (MonoBehaviourSingleton<ChatManager>.I.homeChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.homeChat.SendStamp(stampId);
          break;
        }
        break;
      case MainChat.CHAT_TYPE.ROOM:
      case MainChat.CHAT_TYPE.FIELD:
        if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(stampId);
          break;
        }
        break;
      case MainChat.CHAT_TYPE.LOUNGE:
        if (MonoBehaviourSingleton<ChatManager>.I.loungeChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendStamp(stampId);
          break;
        }
        break;
      case MainChat.CHAT_TYPE.CLAN:
        if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.clanChat.SendStamp(stampId);
          break;
        }
        break;
    }
    this.UpdateSendBlock();
    if (!this.m_IsOnshotStampMode)
      return;
    this.HideAll();
  }

  public void SendMessageAsMine(string message)
  {
    switch (this.currentChat)
    {
      case MainChat.CHAT_TYPE.HOME:
        if (MonoBehaviourSingleton<ChatManager>.I.homeChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.homeChat.SendMessage(message);
          break;
        }
        break;
      case MainChat.CHAT_TYPE.ROOM:
      case MainChat.CHAT_TYPE.FIELD:
        if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.roomChat.SendMessage(message);
          break;
        }
        break;
      case MainChat.CHAT_TYPE.LOUNGE:
        if (MonoBehaviourSingleton<ChatManager>.I.loungeChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendMessage(message);
          break;
        }
        break;
      case MainChat.CHAT_TYPE.CLAN:
        if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null)
        {
          MonoBehaviourSingleton<ChatManager>.I.clanChat.SendMessage(message);
          break;
        }
        break;
    }
    this.UpdateSendBlock();
    if (!this.m_IsOnshotStampMode)
      return;
    this.HideAll();
  }

  private void Post(MainChat.ChatPostRequest request)
  {
    if (this.CurrentData == null)
      return;
    MainChat.ChatItemListData currentData = this.CurrentData;
    switch (request.Type)
    {
      case MainChat.ChatPostRequest.TYPE.Message:
        this.AddNextChatItem(request, currentData, (Action<ChatItem>) (chatItem => chatItem.Init(request.userId, request.userName, request.message, request.chatItemId)));
        SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
        break;
      case MainChat.ChatPostRequest.TYPE.Stamp:
        if (!this.IsValidStampId(request.stampId))
          break;
        StampTable.Data data = Singleton<StampTable>.I.GetData((uint) request.stampId);
        if (data == null)
          break;
        this.AddNextChatItem(request, currentData, (Action<ChatItem>) (chatItem => chatItem.Init(request.userId, request.userName, request.stampId, request.chatItemId)));
        if (data.hasSE)
        {
          SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
          break;
        }
        SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
        break;
      case MainChat.ChatPostRequest.TYPE.Notification:
        this.AddNextChatItem(request, currentData, (Action<ChatItem>) (chatItem => chatItem.Init(request.message, request.chatItemId, request.isWhiteColor)));
        SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
        break;
    }
  }

  private void AddNextChatItem(
    MainChat.ChatPostRequest request,
    MainChat.ChatItemListData data,
    Action<ChatItem> initializer)
  {
    if (Object.op_Equality((Object) this.m_ChatItemPrefab, (Object) null))
      return;
    if (!request.IsOldMessage)
    {
      this.addNextChatItem(data, initializer);
      this.StateMachine.CurrentState.OnShowMessageOnDisplay(request.chatItemId);
    }
    else
      this.addPrevChatItem(data, initializer);
  }

  private void addNextChatItem(MainChat.ChatItemListData data, Action<ChatItem> initializer)
  {
    if (data.itemList.Count > 0)
      data.currentTotalHeight += 22f;
    float y = 0.0f;
    ChatItem component;
    if (data.itemList.Count < 30)
    {
      component = ((Component) ResourceUtility.Realizes((Object) this.m_ChatItemPrefab, data.rootObject.transform, 5)).GetComponent<ChatItem>();
    }
    else
    {
      component = data.itemList[data.oldestItemIndex];
      ++data.oldestItemIndex;
      if (data.oldestItemIndex == 30)
        data.oldestItemIndex = 0;
      data.currentTotalHeight -= component.height + 22f;
      this.ScrollView.panel.widgetsAreStatic = false;
      y = component.height + 22f;
      data.MoveAll(y);
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.ScrollView.panel.widgetsAreStatic = true);
    }
    float currentTotalHeight = data.currentTotalHeight;
    ((Component) component).transform.localPosition = Vector3.op_Addition(this.CHAT_ITEM_OFFSET_POS, Vector3.op_Multiply(Vector3.down, currentTotalHeight));
    initializer(component);
    data.currentTotalHeight += component.height;
    this.UpdateDummyDragScroll();
    float num = this.IsPortrait ? 300f : 344f;
    if ((double) data.currentTotalHeight - (double) num < (double) this.ScrollViewTrans.localPosition.y)
    {
      float newHeight = (float) ((double) data.currentTotalHeight + (double) this.ScrollView.panel.baseClipRegion.y - (double) this.ScrollView.panel.baseClipRegion.w * 0.5 + ((double) this.ScrollViewTrans.localPosition.y + (double) this.ScrollView.panel.clipOffset.y)) + this.ScrollView.panel.clipSoftness.y;
      if (data.itemList.Count >= 30)
        this.ForceScroll((float) ((double) newHeight - (double) component.height - 22.0), false);
      this.ForceScroll(newHeight, true);
    }
    else if (data.itemList.Count >= 30 && (double) this.ScrollViewTrans.localPosition.y > (double) num)
      this.ForceScroll(this.ScrollViewTrans.localPosition.y - y, false);
    if (data.itemList.Count >= 30)
      return;
    data.itemList.Add(component);
  }

  private void addPrevChatItem(MainChat.ChatItemListData data, Action<ChatItem> initializer)
  {
    this.ScrollView.panel.widgetsAreStatic = false;
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.ScrollView.panel.widgetsAreStatic = true);
    ChatItem component;
    if (data.itemList.Count < 30)
    {
      component = ((Component) ResourceUtility.Realizes((Object) this.m_ChatItemPrefab, data.rootObject.transform, 5)).GetComponent<ChatItem>();
    }
    else
    {
      component = data.itemList[data.newestIndex];
      data.itemList.Remove(component);
    }
    initializer(component);
    this.tmpList.Clear();
    this.tmpList.Add(component);
    this.tmpList.AddRange((IEnumerable<ChatItem>) data.itemList);
    data.itemList.Clear();
    data.itemList.AddRange((IEnumerable<ChatItem>) this.tmpList);
    data.oldestItemIndex = 0;
    Vector3 vector3 = this.CHAT_ITEM_OFFSET_POS;
    data.currentTotalHeight = 0.0f;
    for (int index = 0; index < data.itemList.Count; ++index)
    {
      if (index > 0)
        data.currentTotalHeight += 22f;
      ChatItem chatItem = data.itemList[index];
      ((Component) chatItem).transform.localPosition = vector3;
      data.currentTotalHeight += chatItem.height;
      vector3 = Vector3.op_Addition(Vector3.op_Addition(vector3, Vector3.op_Multiply(Vector3.down, chatItem.height)), Vector3.op_Multiply(Vector3.down, 22f));
    }
    this.UpdateDummyDragScroll();
  }

  private void ForceScroll(float newHeight, bool useSpring)
  {
    this.ScrollView.DisableSpring();
    if (useSpring)
    {
      SpringPanel.Begin(((Component) this.ScrollView).gameObject, Vector3.op_Multiply(Vector3.up, newHeight), 20f);
    }
    else
    {
      Vector2 clipOffset = this.ScrollView.panel.clipOffset;
      float num = this.ScrollViewTrans.localPosition.y + clipOffset.y;
      this.ScrollViewTrans.localPosition = Vector3.op_Multiply(Vector3.up, newHeight);
      clipOffset.y = -newHeight + num;
      this.ScrollView.panel.clipOffset = clipOffset;
    }
  }

  public void SaveSlideOffset()
  {
    this.CurrentData.slideOffset = this.ScrollViewTrans.localPosition.y + this.ScrollView.panel.height;
  }

  private float CurrentTotalHeight
  {
    get => this.CurrentData == null ? 0.0f : this.CurrentData.currentTotalHeight;
  }

  public void UpdateWindowSize() => this.UpdateDummyDragScroll();

  private bool hasRoomChat => MonoBehaviourSingleton<ChatManager>.I.roomChat != null;

  private bool hasLoungeChat => MonoBehaviourSingleton<ChatManager>.I.loungeChat != null;

  private bool hasClanChat => MonoBehaviourSingleton<ChatManager>.I.clanChat != null;

  private void Initialize()
  {
    this.logView = this.CreateChatUIFadeGroup(MainChat.UI.WGT_ANCHOR_TOP);
    this.inputView = this.CreateChatUIFadeGroup(MainChat.UI.WGT_ANCHOR_BOTTOM);
    this.inputBG = this.CreateChatUIFadeGroup(MainChat.UI.SPR_BG_POST_FRAME);
    this.channelSelect = this.CreateChatUIFadeGroup(MainChat.UI.OBJ_CHANNEL_SELECT);
    this.chatOpenButton = this.CreateChatUIFadeGroup(MainChat.UI.BTN_OPEN);
    this.bgBlack = this.CreateChatUIFadeGroup(MainChat.UI.SPR_BG_BLACK);
    this.sendBlockView = this.CreateChatUIFadeGroup(MainChat.UI.OBJ_POST_BLOCK);
    this.sendLimitView = this.CreateChatUIFadeGroup(MainChat.UI.WGT_POST_LIMIT);
    this.sendLimitNoClanView = this.CreateChatUIFadeGroup(MainChat.UI.WGT_NO_CLAN);
    this.sendLimitReloadView = this.CreateChatUIFadeGroup(MainChat.UI.WGT_RELOAD);
    this.noConnectionView = this.CreateChatUIFadeGroup(MainChat.UI.WGT_NO_CONNECTION);
    this.GenerateHeaderButton();
    this.SetButtonEvent(MainChat.UI.BTN_SHOW_LOG, new EventDelegate(new EventDelegate.Callback(this.ShowFull)));
    this.SetButtonEvent(MainChat.UI.BTN_HIDE_LOG, new EventDelegate(new EventDelegate.Callback(this.ShowInputOnly)));
    this.SetButtonEvent(MainChat.UI.BTN_INPUT_CLOSE, new EventDelegate(new EventDelegate.Callback(this.HideFull)));
    this.SetButtonEvent(MainChat.UI.BTN_RECONNECT, new EventDelegate(new EventDelegate.Callback(this.ShowChannelSelect)));
    this.InitChannelSelect();
    this.InitChannelInput();
    this.SetButtonEvent(MainChat.UI.BTN_OPEN, new EventDelegate(new EventDelegate.Callback(this.ShowFullWithEdit)));
    this.SetLabelText((Enum) MainChat.UI.LBL_CHANNEL, StringTable.Get(STRING_CATEGORY.CHAT, 0U));
    this.SetLabelText((Enum) MainChat.UI.LBL_CHAT_HOME, StringTable.Get(STRING_CATEGORY.CHAT, 1U));
    this.SetLabelText((Enum) MainChat.UI.LBL_NO_CONNECTION, StringTable.Get(STRING_CATEGORY.CHAT, 4U));
    this.SetLabelText((Enum) MainChat.UI.LBL_POST_LIMIT, StringTable.Get(STRING_CATEGORY.CHAT, 5U));
    MonoBehaviourSingleton<ChatManager>.I.CreateHomeChat();
    MonoBehaviourSingleton<ChatManager>.I.homeChat.onReceiveText += new ChatRoom.OnReceiveText(this.OnReceiveHomeText);
    MonoBehaviourSingleton<ChatManager>.I.homeChat.onReceiveStamp += new ChatRoom.OnReceiveStamp(this.OnReceiveHomeStamp);
    MonoBehaviourSingleton<ChatManager>.I.homeChat.onJoin += new ChatRoom.OnJoin(this.OnJoinHomeChat);
    MonoBehaviourSingleton<ChatManager>.I.homeChat.onDisconnect += new ChatRoom.OnDisconnect(this.OnDisconnectHomeChat);
    MonoBehaviourSingleton<ChatManager>.I.OnCreateRoomChat += new System.Action(this.OnCreateRoomChat);
    MonoBehaviourSingleton<ChatManager>.I.OnDestroyRoomChat += new Action<ChatRoom>(this.OnDestroyRoomChat);
    MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += new Action<ChatRoom>(this.OnCreateLoungeChat);
    MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += new Action<ChatRoom>(this.OnDestroyLoungeChat);
    MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat += new Action<ChatRoom>(this.OnCreateClanChat);
    MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat += new Action<ChatRoom>(this.OnDestroyClanChat);
    MonoBehaviourSingleton<ChatManager>.I.CreateClanChat((IChatConnection) MonoBehaviourSingleton<ClanMatchingManager>.I.GetChatConnection());
    this.InputFrame.onChange += (System.Action) (() => this.OnInput());
    this.InputFrame.onSubmit += (System.Action) (() => this.OnTouchPost());
  }

  private void InitChannelSelect()
  {
    this.SetButtonEvent(MainChat.UI.BTN_SPR_CHANNEL_SELECT, new EventDelegate(new EventDelegate.Callback(this.ShowChannelSelect)));
    this.SetButtonEvent(MainChat.UI.BTN_CLOSE_CHANNEL_SELECT, new EventDelegate((EventDelegate.Callback) (() =>
    {
      SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
      this.CloseChannelSelect();
    })));
    this.SetButtonEvent(MainChat.UI.BTN_HOT, new EventDelegate(new EventDelegate.Callback(this.OnSelectHotChannel)));
    this.SetButtonEvent(MainChat.UI.BTN_QUIET, new EventDelegate(new EventDelegate.Callback(this.OnSelectQuietChannel)));
    this.SetButtonEvent(MainChat.UI.BTN_ANYWARE, new EventDelegate(new EventDelegate.Callback(this.OnSelectRecommendedChannel)));
    this.SetButtonEvent(MainChat.UI.BTN_NUMBER_INPUT, new EventDelegate(new EventDelegate.Callback(this.OnSelectInputChannelNumber)));
    this.SetButtonEvent(MainChat.UI.BTN_SHOW_ALL, new EventDelegate(new EventDelegate.Callback(this.OnSelectShowAll)));
    this.SetButtonEvent(MainChat.UI.BTN_EDIT, new EventDelegate(new EventDelegate.Callback(this.OnSelectEdit)));
  }

  private void InitChannelInput()
  {
    this.channelInputPanel = new ChatChannelInputPanel((ChatUITweenGroup) this.CreateChatUIFadeGroup(MainChat.UI.OBJ_CHANNEL_INPUT));
    this.channelInputPanel.SetNumLabels(this.GetLabel(MainChat.UI.LBL_INPUT_PASS_4), this.GetLabel(MainChat.UI.LBL_INPUT_PASS_3), this.GetLabel(MainChat.UI.LBL_INPUT_PASS_2), this.GetLabel(MainChat.UI.LBL_INPUT_PASS_1));
    this.channelInputPanel.SetNumButtons(this.GetButton(MainChat.UI.BTN_0), this.GetButton(MainChat.UI.BTN_1), this.GetButton(MainChat.UI.BTN_2), this.GetButton(MainChat.UI.BTN_3), this.GetButton(MainChat.UI.BTN_4), this.GetButton(MainChat.UI.BTN_5), this.GetButton(MainChat.UI.BTN_6), this.GetButton(MainChat.UI.BTN_7), this.GetButton(MainChat.UI.BTN_8), this.GetButton(MainChat.UI.BTN_9));
    this.channelInputPanel.SetOKButton(this.GetButton(MainChat.UI.BTN_NEXT));
    this.channelInputPanel.SetClearButton(this.GetButton(MainChat.UI.BTN_CLEAR));
    this.channelInputPanel.SetCloseButton(this.GetButton(MainChat.UI.BTN_CLOSE_CHANNEL_INPUT));
    this.channelInputPanel.SetOnOKDelegate(new Action<int>(this.OnInputChannel));
    this.channelInputPanel.SetOnCloseButtonDelegate((System.Action) (() => this.channelSelect.Open((System.Action) (() => { }))));
  }

  private UIButton GetButton(MainChat.UI elm)
  {
    Transform ctrl = this.GetCtrl((Enum) elm);
    if (Object.op_Implicit((Object) ctrl))
    {
      UIButton component = ((Component) ctrl).GetComponent<UIButton>();
      if (Object.op_Implicit((Object) component))
        return component;
    }
    return (UIButton) null;
  }

  private UILabel GetLabel(MainChat.UI elm)
  {
    Transform ctrl = this.GetCtrl((Enum) elm);
    if (Object.op_Implicit((Object) ctrl))
    {
      UILabel component = ((Component) ctrl).GetComponent<UILabel>();
      if (Object.op_Implicit((Object) component))
        return component;
    }
    return (UILabel) null;
  }

  private void SetButtonEvent(MainChat.UI elm, EventDelegate eventDelegate)
  {
    Transform ctrl = this.GetCtrl((Enum) elm);
    if (!Object.op_Implicit((Object) ctrl))
      return;
    UIButton component = ((Component) ctrl).GetComponent<UIButton>();
    if (!Object.op_Implicit((Object) component))
      return;
    component.onClick.Clear();
    component.onClick.Add(eventDelegate);
  }

  private void OnCreateRoomChat()
  {
    MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveText += new ChatRoom.OnReceiveText(this.OnReceiveRoomText);
    MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveStamp += new ChatRoom.OnReceiveStamp(this.OnReceiveRoomStamp);
    MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveNotification += new ChatRoom.OnReceiveNotification(this.OnReceiveRoomNotification);
  }

  private void OnDestroyRoomChat(ChatRoom roomChat)
  {
    roomChat.onReceiveText -= new ChatRoom.OnReceiveText(this.OnReceiveRoomText);
    roomChat.onReceiveStamp -= new ChatRoom.OnReceiveStamp(this.OnReceiveRoomStamp);
    MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveNotification -= new ChatRoom.OnReceiveNotification(this.OnReceiveRoomNotification);
    this.m_PostRequetQueue[1].Clear();
    this.m_DataList[1].Reset();
  }

  private void OnCreateLoungeChat(ChatRoom loungeChat)
  {
    loungeChat.onReceiveText += new ChatRoom.OnReceiveText(this.OnReceiveLoungeText);
    loungeChat.onReceiveStamp += new ChatRoom.OnReceiveStamp(this.OnReceiveLoungeStamp);
    loungeChat.onReceiveNotification += new ChatRoom.OnReceiveNotification(this.OnReceiveLoungeNotification);
    this.currentChat = MainChat.CHAT_TYPE.LOUNGE;
  }

  private void OnDestroyLoungeChat(ChatRoom loungeChat)
  {
    loungeChat.onReceiveText -= new ChatRoom.OnReceiveText(this.OnReceiveLoungeText);
    loungeChat.onReceiveStamp -= new ChatRoom.OnReceiveStamp(this.OnReceiveLoungeStamp);
    loungeChat.onReceiveNotification -= new ChatRoom.OnReceiveNotification(this.OnReceiveLoungeNotification);
    this.m_PostRequetQueue[2].Clear();
    this.m_DataList[2].Reset();
    this.currentChat = MainChat.CHAT_TYPE.HOME;
  }

  private void OnCreateClanChat(ChatRoom clanChat)
  {
    clanChat.onReceiveText += new ChatRoom.OnReceiveText(this.OnReceiveClanText);
    clanChat.onReceiveStamp += new ChatRoom.OnReceiveStamp(this.OnReceiveClanStamp);
    clanChat.onReceiveNotification += new ChatRoom.OnReceiveNotification(this.OnReceiveClanNotification);
    clanChat.onAfterSendUserMessage += new ChatRoom.OnAfterSendUserMessage(this.OnClanAfterSendUserMessage);
    this.currentChat = MainChat.CHAT_TYPE.CLAN;
  }

  private void OnDestroyClanChat(ChatRoom clanChat)
  {
    clanChat.onReceiveText -= new ChatRoom.OnReceiveText(this.OnReceiveClanText);
    clanChat.onReceiveStamp -= new ChatRoom.OnReceiveStamp(this.OnReceiveClanStamp);
    clanChat.onReceiveNotification -= new ChatRoom.OnReceiveNotification(this.OnReceiveClanNotification);
    clanChat.onAfterSendUserMessage -= new ChatRoom.OnAfterSendUserMessage(this.OnClanAfterSendUserMessage);
    this.m_PostRequetQueue[5].Clear();
    this.m_DataList[5].Reset();
    this.currentChat = MainChat.CHAT_TYPE.HOME;
  }

  private void OnJoinHomeChat(CHAT_ERROR_TYPE errorType)
  {
    if (errorType != CHAT_ERROR_TYPE.NO_ERROR)
      this.OnError(StringTable.Get(STRING_CATEGORY.CHAT_ERROR, 2U));
    this.UpdateSendBlock();
  }

  private void OnDisconnectHomeChat() => this.UpdateSendBlock();

  private bool IsAllowedUser(int userId)
  {
    return !MonoBehaviourSingleton<BlackListManager>.IsValid() || !MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(userId);
  }

  private void OnReceiveText(
    MainChat.CHAT_TYPE chatType,
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool IsOldMessage = false)
  {
    if (!this.IsAllowedUser(userId))
      return;
    this.m_PostRequetQueue[(int) chatType].Enqueue(new MainChat.ChatPostRequest(userId, userName, message, chatItemId, IsOldMessage));
  }

  private void OnReceiveStamp(
    MainChat.CHAT_TYPE chatType,
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool IsOldMessage = false)
  {
    if (!this.IsAllowedUser(userId))
      return;
    this.m_PostRequetQueue[(int) chatType].Enqueue(new MainChat.ChatPostRequest(userId, userName, stampId, chatItemId, IsOldMessage));
  }

  private void OnReceiveHomeText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveText(MainChat.CHAT_TYPE.HOME, userId, userName, message, chatItemId);
  }

  private void OnReceiveHomeStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveStamp(MainChat.CHAT_TYPE.HOME, userId, userName, stampId, chatItemId);
  }

  private void OnReceiveRoomText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveText(MainChat.CHAT_TYPE.ROOM, userId, userName, message, chatItemId);
  }

  private void OnReceiveRoomStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveStamp(MainChat.CHAT_TYPE.ROOM, userId, userName, stampId, chatItemId);
  }

  private void OnReceiveRoomNotification(string message, string chatItemId, bool isOldMessage = false)
  {
    this.m_PostRequetQueue[1].Enqueue(new MainChat.ChatPostRequest(message, chatItemId));
  }

  private void OnReceiveLoungeText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveText(MainChat.CHAT_TYPE.LOUNGE, userId, userName, message, chatItemId);
  }

  private void OnReceiveLoungeStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveStamp(MainChat.CHAT_TYPE.LOUNGE, userId, userName, stampId, chatItemId);
  }

  private void OnReceiveLoungeNotification(string message, string chatItemId, bool isOldMessage = false)
  {
    this.m_PostRequetQueue[2].Enqueue(new MainChat.ChatPostRequest(message, chatItemId));
  }

  private void OnReceiveClanText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveText(MainChat.CHAT_TYPE.CLAN, userId, userName, message, chatItemId, isOldMessage);
  }

  private void OnReceiveClanStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    this.OnReceiveStamp(MainChat.CHAT_TYPE.CLAN, userId, userName, stampId, chatItemId, isOldMessage);
  }

  private void OnReceiveClanNotification(string message, string chatItemId, bool isOldMessage = false)
  {
    this.m_PostRequetQueue[5].Enqueue(new MainChat.ChatPostRequest(message, chatItemId, isOldMessage, true));
  }

  private void OnClanAfterSendUserMessage()
  {
    if (this.CurrentData.itemList.Count > 0 && this.CurrentData.itemList.Count > this.CurrentData.newestIndex && Object.op_Inequality((Object) this.CurrentData.itemList[this.CurrentData.newestIndex], (Object) null) && ((Component) this.CurrentData.itemList[this.CurrentData.newestIndex]).gameObject.activeSelf)
    {
      if (!this.StateMachine.IsRun())
        return;
      this.StateMachine.CurrentState.OnDragAtBottom(this.CurrentData.itemList[this.CurrentData.newestIndex].chatItemId, 10f);
    }
    else
    {
      this.ResetCacheData(MainChat.CHAT_TYPE.CLAN);
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatResetCache();
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
    }
  }

  private ChatUIFadeGroup CreateChatUIFadeGroup(MainChat.UI elm)
  {
    Transform ctrl = this.GetCtrl((Enum) elm);
    UIRect root = (UIRect) null;
    if (Object.op_Implicit((Object) ctrl))
      root = ((Component) ctrl).GetComponent<UIRect>();
    ChatUIFadeGroup chatUiFadeGroup = new ChatUIFadeGroup(root);
    chatUiFadeGroup.Initialize();
    return chatUiFadeGroup;
  }

  public void SetRoomChatNameType(bool field)
  {
    this.isFieldChat = field;
    int index = 0;
    for (int count = this.m_headerButtonList.Count; index < count; ++index)
    {
      if (!Object.op_Equality((Object) this.m_headerButtonList[index], (Object) null) && (this.m_headerButtonList[index].MyChatType == MainChat.CHAT_TYPE.ROOM || this.m_headerButtonList[index].MyChatType == MainChat.CHAT_TYPE.FIELD))
        this.m_headerButtonList[index].InitUILabel(this.isFieldChat ? MainChat.CHAT_TYPE.FIELD : MainChat.CHAT_TYPE.ROOM);
    }
  }

  private void UpdateChannnelName()
  {
    this.UpdateChannnelName(MonoBehaviourSingleton<ChatManager>.I.currentChannel);
  }

  private void UpdateChannnelName(ChatChannel chatChannel)
  {
    int channel = -1;
    if (chatChannel != null)
      channel = chatChannel.channel;
    this.UpdateChannnelName(channel);
  }

  private void UpdateChannnelName(int channel)
  {
    if (channel > 0)
      this.SetChannelName(channel.ToString(MainChat.CHANNEL_FORMAT));
    else
      this.SetChannelName("未接続");
  }

  private void UpdateAdvisoryItem(bool is_portrait = true)
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.advisory != null && !GuildChatAdvisoryItem.HasReadHomeNew())
    {
      Vector3 vector3 = is_portrait ? new Vector3(0.0f, 280f, 0.0f) : new Vector3(0.0f, 215f, 0.0f);
      if (Object.op_Equality((Object) this.chatAdvisoryItem, (Object) null))
      {
        this.chatAdvisoryItem = ((Component) ResourceUtility.Realizes((Object) this.m_ChatAdvisaryItemPrefab, this.GetCtrl((Enum) MainChat.UI.WGT_CHAT_TOP), 5)).GetComponent<GuildChatAdvisoryItem>();
        ((Component) this.chatAdvisoryItem).transform.localPosition = vector3;
        this.chatAdvisoryItem.Init(MonoBehaviourSingleton<UserInfoManager>.I.advisory.title, MonoBehaviourSingleton<UserInfoManager>.I.advisory.content);
        this.SetButtonEvent(this.chatAdvisoryItem.close, new EventDelegate((EventDelegate.Callback) (() =>
        {
          GuildChatAdvisoryItem.SetReadHomeNew();
          if (!Object.op_Inequality((Object) this.chatAdvisoryItem, (Object) null))
            return;
          Object.DestroyImmediate((Object) ((Component) this.chatAdvisoryItem).gameObject);
          this.chatAdvisoryItem = (GuildChatAdvisoryItem) null;
        })));
      }
      else
      {
        ((Component) this.chatAdvisoryItem).gameObject.SetActive(true);
        ((Component) this.chatAdvisoryItem).transform.localPosition = vector3;
      }
    }
    else
    {
      if (!Object.op_Inequality((Object) this.chatAdvisoryItem, (Object) null))
        return;
      Object.DestroyImmediate((Object) ((Component) this.chatAdvisoryItem).gameObject);
      this.chatAdvisoryItem = (GuildChatAdvisoryItem) null;
    }
  }

  private bool ValidateBeforeShowUI()
  {
    if (UserInfoManager.IsRegisterdAge() || !MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge())
      return true;
    this.OnEvent("CHAT_AGE_CONFIRM", (object) null, (string) null);
    return false;
  }

  public void ShowFullWithEdit()
  {
    bool flag1 = this.GetCurrentStateType() == typeof (ChatState_HomeTab);
    bool flag2 = this.GetCurrentStateType() == typeof (ChatState_LoungeTab);
    bool flag3 = this.GetCurrentStateType() == typeof (ChatState_ClanTab);
    this.StampEditButtonObj.SetActive(flag1 | flag2 | flag3);
    this.FavStampEditButtonObj.SetActive(flag1 | flag2 | flag3);
    this.ShowFull();
  }

  public void ShowFull()
  {
    this.ChatCloseButtonObj.SetActive(true);
    SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
    if (!this.ValidateBeforeShowUI())
      return;
    this.InitChatTabState();
    this.m_IsOnshotStampMode = false;
    this.m_isShowFullChatView = true;
    this.UpdateSendBlock();
    this.UpdateChannnelName();
    this.UpdateCloseButtonPosition();
    this.UpdateAdvisoryItem(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.bgBlack.Open((System.Action) (() => { }));
    this.logView.Open((System.Action) (() => { }));
    this.inputView.Open((System.Action) (() => { }));
    this.inputBG.Close((System.Action) (() => { }));
    this.channelSelect.Close((System.Action) (() => { }));
    this.channelInputPanel.Close();
    this.chatOpenButton.Close((System.Action) (() => { }));
    this.StartPostChatProcess();
    this.NotifyObservers(MainChat.NOTIFY_FLAG.OPEN_WINDOW);
    ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_HIDE_LOG)).gameObject.SetActive(this.isMinimizable);
    if (MainChat.splitLogView)
    {
      bool is_portrait = MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
      if (!is_portrait)
      {
        this.inputBG.Open((System.Action) (() => { }));
        this.OnScreenRotate(is_portrait);
      }
      else
        this.InitStampList();
      ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SHOW_LOG)).gameObject.SetActive(is_portrait);
      ((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_HIDE_LOG_L_2)).gameObject.SetActive(true);
    }
    this.SpriteBgBlack.ResizeCollider();
    this.isFirstSlectClanTab = true;
  }

  public void ShowInputOnly()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
    if (!this.ValidateBeforeShowUI() || !UserInfoManager.IsFinishTutorial())
      return;
    this.ChatCloseButtonObj.SetActive(true);
    this.InitChatTabState();
    this.m_IsOnshotStampMode = true;
    this.m_isShowFullChatView = false;
    this.isMinimizable = true;
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.SetButtonEvent(MainChat.UI.BTN_HIDE_LOG, new EventDelegate(new EventDelegate.Callback(this.ShowInputOnly))));
    this.UpdateSendBlock();
    this.UpdateCloseButtonPosition();
    this.bgBlack.Close((System.Action) (() => { }));
    this.logView.Close((System.Action) (() => { }));
    this.inputView.Open((System.Action) (() => { }));
    this.inputBG.Open((System.Action) (() => { }));
    this.chatOpenButton.Close((System.Action) (() => { }));
    this.NotifyObservers(MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY);
    ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SHOW_LOG)).gameObject.SetActive(true);
    this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    if (MainChat.splitLogView)
      ((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_HIDE_LOG_L_2)).gameObject.SetActive(false);
    this.StampEditButtonObj.SetActive(false);
    this.FavStampEditButtonObj.SetActive(false);
    this.isFirstSlectClanTab = true;
  }

  public void ShowInputOnly_NotOneShot()
  {
    this.ShowInputOnly();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.SetButtonEvent(MainChat.UI.BTN_HIDE_LOG, new EventDelegate(new EventDelegate.Callback(this.ShowInputOnly_NotOneShot))));
    this.m_IsOnshotStampMode = false;
  }

  public void HideFull()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
    this.HideAll();
  }

  public void HideDisplay()
  {
    this.isMinimizable = false;
    this.logView.Close((System.Action) (() => { }));
    this.inputView.Close((System.Action) (() => { }));
    this.inputBG.Close((System.Action) (() => { }));
    this.bgBlack.Close((System.Action) (() => { }));
    if (this.isEnableOpenButton)
      this.chatOpenButton.Open((System.Action) (() => { }));
    this.StampEditButtonObj.SetActive(false);
    this.FavStampEditButtonObj.SetActive(false);
    this.ChatCloseButtonObj.SetActive(false);
    if (this.m_msgUiCtrl == null)
      return;
    this.m_msgUiCtrl.ClearList();
  }

  public void HideAll()
  {
    this.isMinimizable = false;
    this.logView.Close((System.Action) (() => { }));
    this.inputView.Close((System.Action) (() => { }));
    this.inputBG.Close((System.Action) (() => { }));
    this.bgBlack.Close((System.Action) (() => { }));
    this.CloseChannelInput();
    this.CloseChannelSelect();
    if (this.isEnableOpenButton)
      this.chatOpenButton.Open((System.Action) (() => { }));
    this.StopPostChatProcess();
    this.NotifyObservers(MainChat.NOTIFY_FLAG.CLOSE_WINDOW);
    this.StampEditButtonObj.SetActive(false);
    this.FavStampEditButtonObj.SetActive(false);
    this.ChatCloseButtonObj.SetActive(false);
    if (this.m_msgUiCtrl == null)
      return;
    this.m_msgUiCtrl.ClearList();
  }

  private void HideBottomUI() => this.SwitchBottomUIActivation(true);

  private void ShowBottomUI() => this.SwitchBottomUIActivation(false);

  private void SwitchBottomUIActivation(bool _isVisible)
  {
    if (((Component) this.WidgetBot).gameObject.activeSelf == _isVisible)
      return;
    ((Component) this.WidgetBot).gameObject.SetActive(_isVisible);
  }

  public void OnPressBackKey()
  {
    if (this.channelInputPanel.isOpened)
    {
      this.CloseChannelInput();
      this.channelSelect.Open((System.Action) (() => { }));
    }
    else if (this.channelSelect.isOpened)
      this.CloseChannelSelect();
    else if (Object.op_Inequality((Object) this.stampFavoriteEdit, (Object) null) && ((Component) this.stampFavoriteEdit).gameObject.activeSelf)
      this.stampFavoriteEdit.Close(false);
    else if (Object.op_Inequality((Object) this.stampAll, (Object) null) && ((Component) this.stampAll).gameObject.activeSelf)
    {
      this.stampAll.Close();
    }
    else
    {
      if (!(this.GetTopState() != typeof (ChatState_FollowerListView)))
        return;
      this.HideAll();
    }
  }

  public void ShowOpenButton()
  {
    if (!UserInfoManager.IsFinishTutorial())
      return;
    this.chatOpenButton.Open((System.Action) (() => { }));
    this.isEnableOpenButton = true;
    this.addObserver((UIBehaviour) this);
  }

  public void HideOpenButton()
  {
    this.chatOpenButton.Close((System.Action) (() => { }));
    this.isEnableOpenButton = false;
    this.RemoveObserver((UIBehaviour) this);
  }

  private bool IsNotConnected()
  {
    if (this.currentChat == MainChat.CHAT_TYPE.HOME)
    {
      if (!MonoBehaviourSingleton<ChatManager>.IsValid() || MonoBehaviourSingleton<ChatManager>.I.currentChannel == null || MonoBehaviourSingleton<ChatManager>.I.currentChannel.channel < 0 || MonoBehaviourSingleton<ChatManager>.I.homeChat == null || !MonoBehaviourSingleton<ChatManager>.I.homeChat.HasConnect)
        return true;
    }
    else if (this.currentChat == MainChat.CHAT_TYPE.ROOM)
    {
      if (MonoBehaviourSingleton<ChatManager>.I.roomChat == null || !MonoBehaviourSingleton<ChatManager>.I.roomChat.HasConnect)
        return true;
    }
    else if (this.currentChat == MainChat.CHAT_TYPE.LOUNGE && (MonoBehaviourSingleton<ChatManager>.I.loungeChat == null || !MonoBehaviourSingleton<ChatManager>.I.loungeChat.HasConnect))
      return true;
    return false;
  }

  private bool IsSendLimit()
  {
    try
    {
      if (!MonoBehaviourSingleton<ChatManager>.IsValid())
        return false;
      ChatManager i = MonoBehaviourSingleton<ChatManager>.I;
      if (this.IsNullObject((object) i))
        return false;
      if (this.currentChat == MainChat.CHAT_TYPE.HOME)
        return Object.op_Inequality((Object) i, (Object) null) && !i.homeChat.CanSendMessage();
      if (this.currentChat == MainChat.CHAT_TYPE.ROOM)
        return i.roomChat != null && !i.roomChat.CanSendMessage();
      if (this.currentChat == MainChat.CHAT_TYPE.LOUNGE)
        return i.loungeChat != null && !i.loungeChat.CanSendMessage();
      return this.currentChat == MainChat.CHAT_TYPE.CLAN && i.clanChat != null && !i.clanChat.CanSendMessage();
    }
    catch (Exception ex)
    {
      Log.Error(ex != null ? ex.Message : "Unhandled exception!!");
      return false;
    }
  }

  private bool IsNullObject(object targetObj)
  {
    return targetObj is Object ? !Object.op_Inequality((Object) targetObj, (Object) null) : targetObj == null;
  }

  public void UpdateSendBlock()
  {
    int num = this.IsNotConnected() ? 1 : 0;
    bool flag1 = num == 0 && this.IsSendLimit();
    bool flag2 = (num | (flag1 ? 1 : 0)) != 0 || this.UseNoClanBlock || this.IsDraging;
    if (num != 0)
      this.noConnectionView.Open((System.Action) (() => { }));
    else
      this.noConnectionView.Close((System.Action) (() => { }));
    if (flag1)
      this.sendLimitView.Open((System.Action) (() => { }));
    else
      this.sendLimitView.Close((System.Action) (() => { }));
    if (this.UseNoClanBlock)
      this.sendLimitNoClanView.Open((System.Action) (() => { }));
    else
      this.sendLimitNoClanView.Close((System.Action) (() => { }));
    if (this.IsDraging)
      this.sendLimitReloadView.Open((System.Action) (() => { }));
    else
      this.sendLimitReloadView.Close((System.Action) (() => { }));
    if (flag2)
      this.sendBlockView.Open((System.Action) (() => { }));
    else
      this.sendBlockView.Close((System.Action) (() => { }));
  }

  private void GenerateHeaderButton()
  {
    if (Object.op_Equality((Object) this.WidgetTopHeader, (Object) null))
      return;
    Transform transform = ((Component) this.WidgetTopHeader).transform;
    for (int index = 0; index < 4; ++index)
    {
      ChatHeaderButtonController component = ((Component) ResourceUtility.Realizes(Resources.Load(MainChat.HEADER_BUTTON_PREFAB_PATH), transform)).GetComponent<ChatHeaderButtonController>();
      if (!Object.op_Equality((Object) component, (Object) null))
      {
        component.Hide();
        this.m_headerButtonList.Add(component);
      }
    }
    this.SetHeaderButtonPosition(this.IsPortrait);
  }

  private void SetHeaderButtonPosition(bool _isPortrait)
  {
    if (this.m_headerButtonList == null || this.m_headerButtonList.Count < 1)
      return;
    Vector3[] vector3Array = _isPortrait ? this.HEADER_BUTTON_PORTRAIT_POS : this.HEADER_BUTTON_LANDSCAPE_POS;
    int index = 0;
    for (int count = this.m_headerButtonList.Count; index < count; ++index)
    {
      Transform transform = ((Component) this.m_headerButtonList[index]).transform;
      transform.localPosition = vector3Array[index];
      transform.localScale = Vector3.one;
    }
  }

  private void InitChatTabState()
  {
    int num1 = -1;
    int num2 = -1;
    int num3 = -1;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    int index1 = 0;
    this.m_headerButtonList[index1].Initialize(new ChatHeaderButtonController.InitParam()
    {
      ButtonIndex = index1,
      ChatType = MainChat.CHAT_TYPE.PERSONAL,
      OnSelectCallBack = new System.Action(this.OnSelectPersonalTab)
    });
    int index2 = index1 + 1;
    this.m_headerButtonList[index2].Initialize(new ChatHeaderButtonController.InitParam()
    {
      ButtonIndex = index2,
      ChatType = MainChat.CHAT_TYPE.CLAN,
      OnSelectCallBack = new System.Action(this.OnSelectClanTab)
    });
    int num7 = index2;
    int index3 = index2 + 1;
    if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      num4 = MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
    if (this.HomeType == MainChat.HOME_TYPE.HOME_TOP)
    {
      this.m_headerButtonList[index3].Initialize(new ChatHeaderButtonController.InitParam()
      {
        ButtonIndex = index3,
        ChatType = MainChat.CHAT_TYPE.HOME,
        OnSelectCallBack = new System.Action(this.OnSelectHomeTab)
      });
      num1 = index3;
      ++index3;
      if (this.m_PostRequetQueue[0] != null)
        num5 = this.m_PostRequetQueue[0].Count;
    }
    else if (this.HomeType == MainChat.HOME_TYPE.LOUNGE_TOP)
    {
      this.m_headerButtonList[index3].Initialize(new ChatHeaderButtonController.InitParam()
      {
        ButtonIndex = index3,
        ChatType = MainChat.CHAT_TYPE.LOUNGE,
        OnSelectCallBack = new System.Action(this.OnSelectLounge)
      });
      num2 = index3;
      ++index3;
      if (this.m_PostRequetQueue[2] != null)
        num6 = this.m_PostRequetQueue[2].Count;
    }
    if (this.hasRoomChat)
    {
      this.m_headerButtonList[index3].Initialize(new ChatHeaderButtonController.InitParam()
      {
        ButtonIndex = index3,
        ChatType = this.isFieldChat ? MainChat.CHAT_TYPE.FIELD : MainChat.CHAT_TYPE.ROOM,
        OnSelectCallBack = new System.Action(this.OnSelectRoomTab)
      });
      num3 = index3;
      ++index3;
    }
    for (int index4 = 0; index4 < this.m_headerButtonList.Count; ++index4)
    {
      if (index4 < index3)
        this.m_headerButtonList[index4].Show();
      else
        this.m_headerButtonList[index4].Hide();
    }
    int index5 = index3 - 1;
    if (num3 > 0)
      index5 = num3;
    else if (num4 > 0 && num7 > 0)
      index5 = num7;
    else if (num5 > 0 && num1 > 0)
      index5 = num1;
    else if (num6 > 0 && num2 > 0)
      index5 = num2;
    else if (num2 > 0)
      index5 = num2;
    else if (num7 > 0 && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      index5 = num7;
    else if (num1 > 0)
      index5 = num1;
    this.m_headerButtonList[index5].OnClick();
    if (this.m_msgUiCtrl != null)
      return;
    this.m_msgUiCtrl = new ChatMessageUserUIController(new ChatMessageUserUIController.InitParam()
    {
      ItemListParent = ((Component) this.PersonalMsgGrid).transform,
      ItemVisibleCount = Mathf.FloorToInt(this.PersonalMsgScrollView.panel.height / this.PersonalMsgGrid.cellHeight),
      OnClickItem = (System.Action) (() => this.PushNextState(typeof (ChatState_PersonalMsgView)))
    });
    ((Component) this.GetCtrl((Enum) MainChat.UI.BTN_SHOW_USER_LIST)).GetComponent<UIButton>().onClick.Add(new EventDelegate()
    {
      methodName = "OnClickShowUserListButton",
      target = (MonoBehaviour) this
    });
  }

  private void OnSelectHomeTab()
  {
    this.OnSelectHeaderTab(MainChat.CHAT_TYPE.HOME);
    this.PushNextExclusiveState(typeof (ChatState_HomeTab));
  }

  private void OnSelectRoomTab()
  {
    this.OnSelectHeaderTab(this.isFieldChat ? MainChat.CHAT_TYPE.FIELD : MainChat.CHAT_TYPE.ROOM);
    this.PushNextExclusiveState(this.isFieldChat ? typeof (ChatState_FieldTab) : typeof (ChatState_RoomTab));
  }

  private void OnSelectLounge()
  {
    this.OnSelectHeaderTab(MainChat.CHAT_TYPE.LOUNGE);
    this.PushNextExclusiveState(typeof (ChatState_LoungeTab));
  }

  private void OnSelectClanTab()
  {
    if (this.isFirstSlectClanTab)
    {
      this.m_DataList[5].Reset();
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatResetCache();
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
      this.isFirstSlectClanTab = false;
    }
    this.OnSelectHeaderTab(MainChat.CHAT_TYPE.CLAN);
    this.PushNextExclusiveState(typeof (ChatState_ClanTab));
  }

  public void ResetCacheData(MainChat.CHAT_TYPE chatType)
  {
    if (this.m_PostRequetQueue[(int) chatType] != null)
      this.m_PostRequetQueue[(int) chatType].Clear();
    if (this.m_DataList[(int) chatType] == null)
      return;
    this.m_DataList[(int) chatType].Reset();
  }

  private void OnSelectPersonalTab()
  {
    this.ResetOtherButtonSettings(MainChat.CHAT_TYPE.PERSONAL);
    this.PushNextExclusiveState(typeof (ChatState_PersonalTab));
    if (this.m_msgUiCtrl.IsConnecting)
      return;
    this.PersonalMsgScrollView.ResetPosition();
    this.StartCoroutine(this.m_msgUiCtrl.SendRequestMessagingPersonList((MonoBehaviour) this));
  }

  private void OnSelectHeaderTab(MainChat.CHAT_TYPE _t)
  {
    this.ResetOtherButtonSettings(_t);
    this.SaveSlideOffset();
    if (this.currentChat == _t)
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.ScrollView.SetDragAmount(1f, 1f, true));
    this.currentChat = _t;
    this.StateMachine.CurrentState.OnTapHeaderTab(_t);
    this.UpdateWindowSize();
  }

  private void ResetOtherButtonSettings(MainChat.CHAT_TYPE _t)
  {
    if (this.m_headerButtonList == null || this.m_headerButtonList.Count < 1)
      return;
    int index1 = 0;
    for (int count = this.m_headerButtonList.Count; index1 < count; ++index1)
    {
      if (this.m_headerButtonList[index1].MyChatType != _t)
        this.m_headerButtonList[index1].UnSelect();
    }
    bool flag1 = _t == MainChat.CHAT_TYPE.ROOM || _t == MainChat.CHAT_TYPE.FIELD;
    bool active = _t == MainChat.CHAT_TYPE.HOME;
    bool flag2 = _t == MainChat.CHAT_TYPE.LOUNGE;
    bool flag3 = _t == MainChat.CHAT_TYPE.PERSONAL;
    bool flag4 = _t == MainChat.CHAT_TYPE.CLAN;
    for (int index2 = 0; index2 < this.m_DataList.Length; ++index2)
    {
      if (this.m_DataList[index2] != null)
      {
        if (index2 == 1 || index2 == 3)
          this.m_DataList[index2].rootObject.SetActive(flag1);
        else
          this.m_DataList[index2].rootObject.SetActive((MainChat.CHAT_TYPE) index2 == _t);
      }
    }
    this.SwitchBottomUIActivation(!flag3);
    if (this.PersonalMsgGridObj.activeSelf != flag3)
      this.PersonalMsgGridObj.SetActive(flag3);
    ((Component) this.SubHeaderPanel).gameObject.SetActive(active | flag3);
    this.SetActiveChannelSelect(active);
    if (this.ShowUserListButtonObject.activeSelf != flag3)
      this.ShowUserListButtonObject.SetActive(flag3);
    this.SetScrollPanelUI(this.IsPortrait, _t);
    this.SetChatBgFrameUI(_t);
    bool flag5 = (active && !this.hasRoomChat) | flag2 | flag4;
    this.StampEditButtonObj.SetActive(flag5);
    this.FavStampEditButtonObj.SetActive(flag5);
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    if (!Object.op_Inequality((Object) this.InputFrame, (Object) null))
      return;
    this.InputFrame.UpdateAgeConfirm();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_INFO;
  }

  private void ShowChannelSelect()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
    this.SetChannelSelectBG();
    this.channelSelect.Open((System.Action) (() => { }));
  }

  private void CloseChannelSelect()
  {
    this.ResetChannelSelectBG();
    this.channelSelect.Close((System.Action) (() => { }));
  }

  private void OnSelectHotChannel()
  {
    this.OnSelectChannel(MonoBehaviourSingleton<ChatManager>.I.GetHotChannel());
  }

  private void OnSelectQuietChannel()
  {
    this.OnSelectChannel(MonoBehaviourSingleton<ChatManager>.I.GetColdChannel());
  }

  private void OnSelectRecommendedChannel()
  {
    this.OnSelectChannel(MonoBehaviourSingleton<ChatManager>.I.GetRecommendedChannel());
  }

  private void OnSelectInputChannelNumber()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
    this.ShowChannelInput();
  }

  private void OnSelectShowAll()
  {
    if (Object.op_Equality((Object) this.stampAll, (Object) null))
      this.stampAll = ((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_STAMP_ALL)).GetComponent<ChatStampAll>();
    this.stampAll.Open();
  }

  private void OnSelectEdit()
  {
    if (Object.op_Equality((Object) this.stampFavoriteEdit, (Object) null))
      this.stampFavoriteEdit = ((Component) this.GetCtrl((Enum) MainChat.UI.OBJ_STAMP_FAVORITE_EDIT)).GetComponent<ChatStampFavoriteEdit>();
    this.stampFavoriteEdit.Open();
  }

  private void ShowChannelInput()
  {
    this.channelSelect.Close((System.Action) (() => { }));
    this.channelInputPanel.Open();
  }

  private void CloseChannelInput() => this.channelInputPanel.Close();

  private void OnInputChannel(int number)
  {
    List<int> channels = MonoBehaviourSingleton<ChatManager>.I.GetChannels();
    int channel = 0;
    int index = 0;
    for (int count = channels.Count; index < count; ++index)
    {
      if (channels[index] == number)
        channel = channels[index];
    }
    if (channel > 0)
    {
      this.OnSelectChannel(channel);
      this.CloseChannelInput();
    }
    else
      this.OnError(StringTable.Format(STRING_CATEGORY.CHAT_ERROR, 0U, (object) number.ToString(MainChat.CHANNEL_FORMAT)));
  }

  private void OnSelectChannel(int channel)
  {
    SoundManager.PlaySystemSE(SoundID.UISE.OK);
    MonoBehaviourSingleton<ChatManager>.I.SelectChannel(channel);
    this.UpdateChannnelName(channel);
    this.CloseChannelSelect();
  }

  private void SetChannelSelectBG()
  {
    if (SpecialDeviceManager.HasSpecialDeviceInfo)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        this.SpriteBgBlack.width = (int) componentInChildren.ScreenWidthFull;
        this.SpriteBgBlack.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    this.SetParent(MainChat.UI.SPR_BG_BLACK, MainChat.UI.OBJ_CHANNEL_SELECT_BG);
    this.SpriteBgBlack.ParentHasChanged();
  }

  private void ResetChannelSelectBG()
  {
    if (SpecialDeviceManager.HasSpecialDeviceInfo)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        this.SpriteBgBlack.width = (int) componentInChildren.ScreenWidthFull;
        this.SpriteBgBlack.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    this.SetParent(MainChat.UI.SPR_BG_BLACK, this.GetCtrl((Enum) MainChat.UI.BTN_OPEN).parent);
    this.SpriteBgBlack.ParentHasChanged();
  }

  private void OnError(string message)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, message, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (s => { }), true);
  }

  public void onStoppedMovingScrollView()
  {
  }

  public void onPressStartScrollView() => this.tmpOffsetStart = this.ScrollView.panel.clipOffset.y;

  public void onPressEndScrollView()
  {
  }

  public void onDragStartScrollView()
  {
    this.dragTime = Time.time;
    this.IsDraging = true;
    this.UpdateSendBlock();
  }

  public void onDragEndScrollView()
  {
    this.IsDraging = false;
    this.UpdateSendBlock();
    if ((double) this.ScrollView.panel.clipOffset.y - (double) this.tmpOffsetStart < 0.0)
    {
      if (this.CurrentData.itemList.Count > 0)
      {
        if (this.CurrentData.itemList.Count <= this.CurrentData.newestIndex || !Object.op_Inequality((Object) this.CurrentData.itemList[this.CurrentData.newestIndex], (Object) null) || !((Component) this.CurrentData.itemList[this.CurrentData.newestIndex]).gameObject.activeSelf || !this.StateMachine.IsRun())
          return;
        this.StateMachine.CurrentState.OnDragAtBottom(this.CurrentData.itemList[this.CurrentData.newestIndex].chatItemId, Time.time - this.dragTime);
      }
      else
      {
        if (!this.StateMachine.IsRun())
          return;
        this.StateMachine.CurrentState.OnDragAtBottom("", Time.time - this.dragTime);
      }
    }
    else if (this.CurrentData.itemList.Count > 0)
    {
      if (this.CurrentData.itemList.Count <= this.CurrentData.oldestItemIndex || !Object.op_Inequality((Object) this.CurrentData.itemList[this.CurrentData.oldestItemIndex], (Object) null) || !((Component) this.CurrentData.itemList[this.CurrentData.oldestItemIndex]).gameObject.activeSelf || !this.StateMachine.IsRun())
        return;
      this.StateMachine.CurrentState.OnDragAtTop(this.CurrentData.itemList[this.CurrentData.oldestItemIndex].chatItemId, Time.time - this.dragTime);
    }
    else
    {
      if (!this.StateMachine.IsRun())
        return;
      this.StateMachine.CurrentState.OnDragAtTop("", Time.time - this.dragTime);
    }
  }

  private void InitStampList()
  {
    if (this.m_StampIdListCanPost == null)
      this.ResetStampIdList();
    this.UpdateStampList();
  }

  public void UpdateStampList()
  {
    this.SetGrid((Enum) MainChat.UI.GRD_STAMP_LIST, (string) null, this.m_StampIdListCanPost.Count + 10 + (this.IsLandScapeFullViewMode ? 2 : 0), true, new Func<int, Transform, Transform>(this.CreateStampItem), new Action<int, Transform, bool>(this.InitStampItem));
  }

  public void ResetStampIdList()
  {
    if (this.m_StampIdListCanPost == null)
      this.m_StampIdListCanPost = new List<int>();
    this.m_StampIdListCanPost.Clear();
    if (!Singleton<StampTable>.IsValid() || Singleton<StampTable>.I.table == null)
      return;
    Singleton<StampTable>.I.table.ForEach((Action<StampTable.Data>) (stamp_data =>
    {
      int id = (int) stamp_data.id;
      if (!this.CanIPostTheStamp(id))
        return;
      this.m_StampIdListCanPost.Add(id);
    }));
  }

  public void OnUpdateUnlockStampList()
  {
    this.m_StampIdListCanPost = (List<int>) null;
    this.InitStampList();
  }

  public bool IsValidStampId(int stampId)
  {
    return stampId >= 1 && Singleton<StampTable>.I.GetData((uint) stampId) != null;
  }

  public bool CanIPostTheStamp(int id)
  {
    if (!this.IsValidStampId(id))
      return false;
    if (Singleton<StampTable>.I.GetData((uint) id).type == STAMP_TYPE.COMMON)
      return true;
    return MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(id) && this.IsValidStampId(id);
  }

  private Transform CreateStampItem(int index, Transform parent)
  {
    Transform stampItem = ResourceUtility.Realizes((Object) this.m_ChatStampListPrefab, 5);
    stampItem.parent = parent;
    stampItem.localScale = Vector3.one;
    return stampItem;
  }

  private void InitStampItem(int index, Transform iTransform, bool isRecycle)
  {
    if (this.m_StampIdListCanPost == null)
      return;
    bool flag = false;
    int _stampId;
    if (index < 10)
      _stampId = MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index];
    else if (this.IsLandScapeFullViewMode)
    {
      if (index < 12)
      {
        _stampId = 1;
        flag = true;
      }
      else
        _stampId = this.m_StampIdListCanPost[index - 12];
    }
    else
      _stampId = this.m_StampIdListCanPost[index - 10];
    ChatStampListItem item = ((Component) iTransform).GetComponent<ChatStampListItem>();
    item.Init(_stampId);
    if (flag)
      item.SetAsDummy();
    if (isRecycle || flag)
      return;
    item.onButton += (System.Action) (() => MonoBehaviourSingleton<UIManager>.I.mainChat.SendStampAsMine(item.StampId));
  }

  private void UpdateDummyDragScroll()
  {
    this.DummyDragScroll.height = (double) this.ScrollView.panel.height <= (double) this.CurrentTotalHeight ? (int) ((double) this.CurrentTotalHeight - 20.0) : (int) ((double) this.ScrollView.panel.height - 20.0);
    this.DragScrollTrans.localPosition = new Vector3(this.ScrollView.panel.clipOffset.x, -this.CurrentTotalHeight, 0.0f);
    this.DragScrollCollider.size = new Vector3(this.ScrollView.panel.finalClipRegion.z, this.ScrollView.panel.finalClipRegion.w - this.ScrollView.panel.clipSoftness.y * 2f, 0.0f);
  }

  private void Update()
  {
    float deltaTime = Time.deltaTime;
    this.UpdateObserve();
    this.UpdateStateMachine(deltaTime);
    if (this.state != UIBehaviour.STATE.OPEN)
      return;
    if (this.m_handlPostChatPorcess != null && !this.m_handlPostChatPorcess.MoveNext())
      this.m_handlPostChatPorcess = (IEnumerator) null;
    if (this.sendBlockView.isOpened)
      this.UpdateSendBlock();
    bool isOpened = this.logView.isOpened;
    ((Collider) this.DragScrollCollider).enabled = isOpened;
    if (!isOpened)
      return;
    this.DragScrollCollider.center = Vector2.op_Implicit(new Vector2(this.ScrollView.panel.baseClipRegion.x, -(float) ((double) this.ScrollView.panel.baseClipRegion.w - (double) this.ScrollView.panel.baseClipRegion.y + (double) this.DragScrollTrans.localPosition.y - ((double) this.ScrollView.panel.finalClipRegion.w + (double) this.ScrollView.panel.clipOffset.y))));
  }

  private void LateUpdate()
  {
    if (this.logView == null || !this.logView.isOpened && !this.logView.isOpening)
      return;
    this.UpdateWidgetVisibility();
  }

  private void UpdateWidgetVisibility()
  {
    List<ChatItem> itemList = this.CurrentData.itemList;
    int index = 0;
    for (int count = itemList.Count; index < count; ++index)
    {
      UIWidget widget = itemList[index].widget;
      if (!this.IsVisible(itemList[index]))
        ((Component) widget).gameObject.SetActive(false);
      else if (!((Component) widget).gameObject.activeSelf)
        ((Component) widget).gameObject.SetActive(true);
    }
  }

  private bool IsVisible(ChatItem chatItem)
  {
    float y = ((Component) chatItem).transform.localPosition.y;
    float num1 = ((Component) chatItem).transform.localPosition.y - chatItem.height;
    Vector4 finalClipRegion = this.ScrollView.panel.finalClipRegion;
    float num2 = finalClipRegion.w * 0.5f;
    return (double) finalClipRegion.y + (double) num2 > (double) num1 && (double) finalClipRegion.y - (double) num2 < (double) y;
  }

  public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
  {
    if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) == (MainChat.NOTIFY_FLAG) 0)
      return;
    this.SetBadge((Enum) MainChat.UI.BTN_OPEN, this.GetPendingQueueNum(), (SpriteAlignment) 1, 7, -9);
  }

  private void UpdateObserve()
  {
    int pendingQueueNum = this.GetPendingQueueNum();
    if (this.m_LastPendingQueueCount != pendingQueueNum)
      this.NotifyObservers(MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE);
    this.m_LastPendingQueueCount = pendingQueueNum;
  }

  private void NotifyObservers(MainChat.NOTIFY_FLAG note)
  {
    if (this.m_Observers == null)
      return;
    this.m_Observers.ForEach((Action<UIBehaviour>) (x =>
    {
      if (!Object.op_Inequality((Object) x, (Object) null))
        return;
      x.OnModifyChat(note);
    }));
  }

  public void addObserver(UIBehaviour observer)
  {
    if (Object.op_Equality((Object) observer, (Object) null) || this.m_Observers.Contains(observer))
      return;
    this.m_Observers.Add(observer);
  }

  public void RemoveObserver(UIBehaviour observer)
  {
    if (!this.m_Observers.Contains(observer))
      return;
    this.m_Observers.Remove(observer);
  }

  protected override void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    base.OnDestroy();
    int index = 0;
    for (int length = this.m_DataList.Length; index < length; ++index)
    {
      if (this.m_DataList[index] != null)
        this.m_DataList[index].Reset();
    }
  }

  public void SetActiveChannelSelect(bool active)
  {
    if (active == this.ChannelSelectSpriteButtonObject.activeSelf)
      return;
    this.ChannelSelectSpriteButtonObject.SetActive(active);
  }

  private void InitStateMachine()
  {
    this.m_stateMachine = new ChatStateMachine<ChatState>();
    this.StateMachine.Initialize(this);
    this.StateMachine.AddListener(new ChatStateMachine<ChatState>.OnChangeStateType(this.OnChangeState));
    this.StateMachine.Start(typeof (ChatState_Init));
  }

  private void UpdateStateMachine(float _deltaTime)
  {
    if (this.StateMachine == null || !this.StateMachine.IsRun())
      return;
    this.StateMachine.Update(_deltaTime);
  }

  public void ExecCoroutine(IEnumerator _ienumarator) => this.StartCoroutine(_ienumarator);

  private void OnChangeState(System.Type currentType, System.Type prevType)
  {
  }

  public System.Type GetCurrentStateType()
  {
    return this.StateMachine != null ? this.StateMachine.CurrentStateType : (System.Type) null;
  }

  public System.Type GetPrevStateType()
  {
    return this.StateMachine != null ? this.StateMachine.PrevStateType : (System.Type) null;
  }

  public void PushNextState(System.Type _s)
  {
    System.Type currentStateType = this.GetCurrentStateType();
    if (currentStateType != (System.Type) null && currentStateType == _s)
      return;
    this.m_stateStack.Push(_s);
  }

  public void PushNextExclusiveState(System.Type _s)
  {
    System.Type currentStateType = this.GetCurrentStateType();
    if (currentStateType != (System.Type) null && currentStateType == _s)
      return;
    if (this.m_stateStack.Count > 0)
      this.PopState();
    this.PushNextState(_s);
  }

  public System.Type GetTopState()
  {
    return this.m_stateStack.Count <= 0 ? (System.Type) null : this.m_stateStack.Peek();
  }

  public System.Type PopState()
  {
    return this.m_stateStack.Count < 1 ? (System.Type) null : this.m_stateStack.Pop();
  }

  public bool HasState(System.Type _t) => this.m_stateStack.Contains(_t);

  public void OnClickShowUserListButton()
  {
    this.PushNextState(typeof (ChatState_FollowerListView));
  }

  [Flags]
  public enum NOTIFY_FLAG
  {
    OPEN_WINDOW = 1,
    CLOSE_WINDOW = 2,
    ARRIVED_MESSAGE = 4,
    OPEN_WINDOW_INPUT_ONLY = 8,
  }

  private enum UI
  {
    WGT_HEADER_SPACE,
    SPR_BG_OUT_FRAME,
    SPR_BG_IN_FRAME,
    WGT_SUB_HEADER_SPACE,
    LBL_CHANNEL_NAME,
    WGT_SLIDE_LIMIT,
    SCR_CHAT,
    WGT_DUMMY_DRAG_SCROLL,
    SCR_PERSONAL_MSG_LIST_VIEW,
    GRD_PERSONAL_MSG_VIEW,
    SPR_SCROLL_BAR_BACKGROUND,
    SPR_SCROLL_BAR_FOREGROUND,
    IPT_POST,
    BTN_CHANGE_TYPE,
    BTN_INPUT_CLOSE,
    BTN_EMOTION,
    OBJ_POST_FRAME,
    SPR_BG_POST_FRAME,
    OBJ_INPUT_FRAME,
    WGT_CHAT_ROOT,
    BTN_CHAT_HOME,
    BTN_CHAT_ROOM,
    LBL_CHAT_HOME,
    LBL_CHAT_ROOM,
    SPR_BG_CHAT_ROOM,
    OBJ_HOME_ITEM_LIST_ROOT,
    OBJ_ROOM_ITEM_LIST_ROOT,
    OBJ_LOUNGE_ITEM_LIST_ROOT,
    OBJ_CLAN_ITEM_LIST_ROOT,
    WGT_CHAT_TOP,
    LBL_DEFAULT,
    OBJ_SLIDE_PANEL,
    OBJ_POST_STAMP_FRAME,
    SPR_STAMP_LIST,
    SCR_STAMP_LIST,
    SPR_CHAT_FRAME,
    GRD_STAMP_LIST,
    BTN_STAMP_CLOSE,
    WGT_ANCHOR_BOTTOM,
    WGT_ANCHOR_TOP,
    BTN_SHOW_LOG,
    BTN_HIDE_LOG,
    OBJ_HIDE_LOG_P,
    OBJ_HIDE_LOG_L,
    OBJ_HIDE_LOG_L_2,
    SPR_BG_BLACK,
    OBJ_POST_BLOCK,
    SPR_POST_BLOCK_BG,
    WGT_NO_CONNECTION,
    BTN_RECONNECT,
    WGT_POST_LIMIT,
    WGT_NO_CLAN,
    WGT_RELOAD,
    LBL_NO_CONNECTION,
    LBL_POST_LIMIT,
    LBL_NO_CLAN,
    BTN_SPR_CHANNEL_SELECT,
    OBJ_CHANNEL_SELECT,
    LBL_CHANNEL,
    BTN_HOT,
    BTN_QUIET,
    BTN_ANYWARE,
    BTN_NUMBER_INPUT,
    BTN_CLOSE_CHANNEL_SELECT,
    OBJ_CHANNEL_SELECT_BG,
    OBJ_CHANNEL_INPUT,
    LBL_INPUT_PASS_1,
    LBL_INPUT_PASS_2,
    LBL_INPUT_PASS_3,
    LBL_INPUT_PASS_4,
    BTN_0,
    BTN_1,
    BTN_2,
    BTN_3,
    BTN_4,
    BTN_5,
    BTN_6,
    BTN_7,
    BTN_8,
    BTN_9,
    BTN_NEXT,
    BTN_CLEAR,
    BTN_CLOSE_CHANNEL_INPUT,
    BTN_OPEN,
    BTN_SHOW_ALL,
    BTN_EDIT,
    OBJ_STAMP_FAVORITE_EDIT,
    OBJ_STAMP_ALL,
    BTN_SHOW_USER_LIST,
  }

  public enum CHAT_TYPE
  {
    HOME,
    ROOM,
    LOUNGE,
    FIELD,
    PERSONAL,
    CLAN,
  }

  public enum HOME_TYPE
  {
    HOME_TOP,
    LOUNGE_TOP,
    CLAN_TOP,
  }

  public class ChatItemListData
  {
    public GameObject rootObject;
    public List<ChatItem> itemList;
    public float currentTotalHeight;
    public int oldestItemIndex;
    public float slideOffset;
    private const float DEFAULT_OFFSET = -26f;

    public int newestIndex
    {
      get
      {
        int newestIndex = this.oldestItemIndex - 1;
        if (newestIndex < 0)
          newestIndex = this.itemList.Count - 1;
        return newestIndex;
      }
    }

    public ChatItemListData(GameObject root)
    {
      this.rootObject = root;
      this.itemList = new List<ChatItem>();
      this.Init();
    }

    public void Init()
    {
      this.currentTotalHeight = 0.0f;
      this.oldestItemIndex = 0;
      this.slideOffset = -26f;
    }

    public void Reset()
    {
      int index = 0;
      for (int count = this.itemList.Count; index < count; ++index)
        Object.DestroyImmediate((Object) ((Component) this.itemList[index]).gameObject);
      this.itemList.Clear();
      this.Init();
    }

    public void MoveAll(float y)
    {
      Vector3 localPosition = ((Component) this.itemList[0]).transform.localPosition;
      int index = 0;
      for (int count = this.itemList.Count; index < count; ++index)
      {
        Transform transform = ((Component) this.itemList[index]).transform;
        localPosition.y = transform.localPosition.y + y;
        transform.localPosition = localPosition;
      }
    }
  }

  private class ChatPostRequest
  {
    public string chatItemId = "";
    public bool isWhiteColor;
    public bool IsOldMessage;

    public MainChat.ChatPostRequest.TYPE Type { get; private set; }

    public int userId { get; private set; }

    public string userName { get; private set; }

    public int stampId { get; private set; }

    public string message { get; private set; }

    public ChatPostRequest(
      int userId,
      string userName,
      string message,
      string chatItemId,
      bool IsOldMessage = false)
    {
      this.Type = MainChat.ChatPostRequest.TYPE.Message;
      this.userId = userId;
      this.userName = userName;
      this.message = message;
      this.chatItemId = chatItemId;
      this.IsOldMessage = IsOldMessage;
    }

    public ChatPostRequest(
      int userId,
      string userName,
      int stampId,
      string chatItemId,
      bool IsOldMessage = false)
    {
      this.Type = MainChat.ChatPostRequest.TYPE.Stamp;
      this.userId = userId;
      this.userName = userName;
      this.stampId = stampId;
      this.chatItemId = chatItemId;
      this.IsOldMessage = IsOldMessage;
    }

    public ChatPostRequest(
      string message,
      string chatItemId,
      bool IsOldMessage = false,
      bool isWhiteColor = false)
    {
      this.Type = MainChat.ChatPostRequest.TYPE.Notification;
      this.message = message;
      this.chatItemId = chatItemId;
      this.IsOldMessage = IsOldMessage;
      this.isWhiteColor = isWhiteColor;
    }

    public enum TYPE
    {
      Message,
      Stamp,
      Notification,
    }
  }

  private class PostRequestQueue
  {
    private static readonly int PENDING_MAX = 20;
    private Queue<MainChat.ChatPostRequest> queue = new Queue<MainChat.ChatPostRequest>();

    public bool HasOverFlowed { get; private set; }

    public int Count => this.queue.Count;

    public void Enqueue(MainChat.ChatPostRequest q)
    {
      this.queue.Enqueue(q);
      if (this.queue.Count <= MainChat.PostRequestQueue.PENDING_MAX)
        return;
      this.queue.Dequeue();
      this.HasOverFlowed = true;
    }

    public MainChat.ChatPostRequest Dequeue()
    {
      this.HasOverFlowed = false;
      return this.queue.Dequeue();
    }

    public void Clear()
    {
      this.queue.Clear();
      this.HasOverFlowed = false;
    }
  }
}
