using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChat : UIBehaviour
{
	[Flags]
	public enum NOTIFY_FLAG
	{
		OPEN_WINDOW = 0x1,
		CLOSE_WINDOW = 0x2,
		ARRIVED_MESSAGE = 0x4,
		OPEN_WINDOW_INPUT_ONLY = 0x8
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
		BTN_SHOW_USER_LIST
	}

	public enum CHAT_TYPE
	{
		HOME,
		ROOM,
		LOUNGE,
		FIELD,
		PERSONAL,
		CLAN
	}

	public enum HOME_TYPE
	{
		HOME_TOP,
		LOUNGE_TOP,
		CLAN_TOP
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
				int num = oldestItemIndex - 1;
				if (num < 0)
				{
					num = itemList.Count - 1;
				}
				return num;
			}
		}

		public ChatItemListData(GameObject root)
		{
			rootObject = root;
			itemList = new List<ChatItem>();
			Init();
		}

		public void Init()
		{
			currentTotalHeight = 0f;
			oldestItemIndex = 0;
			slideOffset = -26f;
		}

		public void Reset()
		{
			int i = 0;
			for (int count = itemList.Count; i < count; i++)
			{
				UnityEngine.Object.DestroyImmediate(itemList[i].gameObject);
			}
			itemList.Clear();
			Init();
		}

		public void MoveAll(float y)
		{
			Vector3 localPosition = itemList[0].transform.localPosition;
			int i = 0;
			for (int count = itemList.Count; i < count; i++)
			{
				Transform transform = itemList[i].transform;
				localPosition.y = transform.localPosition.y + y;
				transform.localPosition = localPosition;
			}
		}
	}

	private class ChatPostRequest
	{
		public enum TYPE
		{
			Message,
			Stamp,
			Notification
		}

		public string chatItemId = "";

		public bool isWhiteColor;

		public bool IsOldMessage;

		public TYPE Type
		{
			get;
			private set;
		}

		public int userId
		{
			get;
			private set;
		}

		public string userName
		{
			get;
			private set;
		}

		public int stampId
		{
			get;
			private set;
		}

		public string message
		{
			get;
			private set;
		}

		public ChatPostRequest(int userId, string userName, string message, string chatItemId, bool IsOldMessage = false)
		{
			Type = TYPE.Message;
			this.userId = userId;
			this.userName = userName;
			this.message = message;
			this.chatItemId = chatItemId;
			this.IsOldMessage = IsOldMessage;
		}

		public ChatPostRequest(int userId, string userName, int stampId, string chatItemId, bool IsOldMessage = false)
		{
			Type = TYPE.Stamp;
			this.userId = userId;
			this.userName = userName;
			this.stampId = stampId;
			this.chatItemId = chatItemId;
			this.IsOldMessage = IsOldMessage;
		}

		public ChatPostRequest(string message, string chatItemId, bool IsOldMessage = false, bool isWhiteColor = false)
		{
			Type = TYPE.Notification;
			this.message = message;
			this.chatItemId = chatItemId;
			this.IsOldMessage = IsOldMessage;
			this.isWhiteColor = isWhiteColor;
		}
	}

	private class PostRequestQueue
	{
		private static readonly int PENDING_MAX = 20;

		private Queue<ChatPostRequest> queue = new Queue<ChatPostRequest>();

		public bool HasOverFlowed
		{
			get;
			private set;
		}

		public int Count => queue.Count;

		public void Enqueue(ChatPostRequest q)
		{
			queue.Enqueue(q);
			if (queue.Count > PENDING_MAX)
			{
				queue.Dequeue();
				HasOverFlowed = true;
			}
		}

		public ChatPostRequest Dequeue()
		{
			HasOverFlowed = false;
			return queue.Dequeue();
		}

		public void Clear()
		{
			queue.Clear();
			HasOverFlowed = false;
		}
	}

	public const string EVENT_AGE_CONFIRM = "CHAT_AGE_CONFIRM";

	public static readonly string HEADER_BUTTON_PREFAB_PATH = "InternalUI/UI_Chat/ChatHeaderButton";

	public const int HEADER_BUTTON_COUNT = 4;

	private readonly Vector3[] HEADER_BUTTON_PORTRAIT_POS = new Vector3[4]
	{
		new Vector3(-217f, 33.4f, -0f),
		new Vector3(-60f, 33.4f, -0f),
		new Vector3(45f, 33.4f, -0f),
		new Vector3(209f, 33f, -0f)
	};

	private readonly Vector3[] HEADER_BUTTON_LANDSCAPE_POS = new Vector3[4]
	{
		new Vector3(-234.4f, 33.4f, -0f),
		new Vector3(-65f, 33.9f, -0f),
		new Vector3(50f, 33.4f, -0f),
		new Vector3(226.5f, 33f, -0f)
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

	private readonly Vector3 CHAT_ITEM_OFFSET_POS = new Vector3(-5f, 0f, 0f);

	private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 ROOM_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0f);

	private readonly Vector3 ROOM_SLIDER_CLOSE_POS = new Vector3(-180f, -150f, 0f);

	private const float ANCHOR_LEFT = 0f;

	private const float ANCHOR_CENTER = 0.5f;

	private const float ANCHOR_RIGHT = 1f;

	private const float ANCHOR_BOT = 0f;

	private const float ANCHOR_TOP = 1f;

	private static readonly Vector4 WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS = new Vector4(-240f, 240f, -427f, 427f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS = new Vector4(-240f, 240f, 71f, 390f);

	private static readonly Vector4 WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS = new Vector4(-15f, 500f, 0f, 0f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_LANDSCAPE_SETTINGS = new Vector4(-560f, -62f, 0f, 320f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS = new Vector4(-405f, 0f, 0f, 375f);

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

	private ChatItemListData[] m_DataList = new ChatItemListData[Enum.GetNames(typeof(CHAT_TYPE)).Length];

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

	public HOME_TYPE HomeType;

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

	private Stack<Type> m_stateStack = new Stack<Type>();

	private ChatStateMachine<ChatState> m_stateMachine;

	private PostRequestQueue[] m_PostRequetQueue = new PostRequestQueue[Enum.GetNames(typeof(CHAT_TYPE)).Length];

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

	public ChatItemListData CurrentData => m_DataList[(int)currentChat];

	private UIScrollView ScrollView => m_ScrollView ?? (m_ScrollView = GetCtrl(UI.SCR_CHAT).GetComponent<UIScrollView>());

	private Transform ScrollViewTrans => m_ScrollViewTrans ?? (m_ScrollViewTrans = ScrollView.transform);

	private SpringPanel ScrollViewSpring => m_ScrollViewSpring ?? (m_ScrollViewSpring = GetCtrl(UI.SCR_CHAT).GetComponent<SpringPanel>());

	private UIWidget DummyDragScroll => m_DummyDragScroll ?? (m_DummyDragScroll = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<UIWidget>());

	private BoxCollider DragScrollCollider => m_DragScrollCollider ?? (m_DragScrollCollider = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<BoxCollider>());

	private Transform DragScrollTrans => m_DragScrollTrans ?? (m_DragScrollTrans = DragScrollCollider.transform);

	private UIInput Input
	{
		get
		{
			if (m_Input == null)
			{
				m_Input = GetCtrl(UI.IPT_POST).GetComponent<UIInput>();
			}
			return m_Input;
		}
	}

	private ChatInputFrame InputFrame
	{
		get
		{
			if (m_InputFrame == null)
			{
				m_InputFrame = GetCtrl(UI.OBJ_INPUT_FRAME).GetComponent<ChatInputFrame>();
			}
			return m_InputFrame;
		}
	}

	private UIRect RootRect
	{
		get
		{
			if (m_RootRect == null)
			{
				m_RootRect = GetCtrl(UI.WGT_CHAT_ROOT).GetComponent<UIRect>();
			}
			return m_RootRect;
		}
	}

	public CHAT_TYPE currentChat
	{
		get;
		private set;
	}

	public static bool splitLogView
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	private GameObject ChatCloseButtonObj => m_chatCloseButtonObj ?? (m_chatCloseButtonObj = GetCtrl(UI.BTN_INPUT_CLOSE).gameObject);

	private GameObject StampEditButtonObj => m_stampEditButtonObj ?? (m_stampEditButtonObj = GetCtrl(UI.BTN_EDIT).gameObject);

	private GameObject FavStampEditButtonObj => m_favStampEditButtonObj ?? (m_favStampEditButtonObj = GetCtrl(UI.BTN_SHOW_ALL).gameObject);

	private UIWidget WidgetTop => m_widgetTop ?? (m_widgetTop = GetCtrl(UI.WGT_ANCHOR_TOP).GetComponent<UIWidget>());

	private UIWidget WidgetTopHeader => m_widgetTopHeader ?? (m_widgetTopHeader = GetCtrl(UI.WGT_HEADER_SPACE).GetComponent<UIWidget>());

	private UISprite BackgroundInFrame => m_BackgroundInFrame ?? (m_BackgroundInFrame = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UISprite>());

	private UIPanel SubHeaderPanel => m_subHeaderPanel ?? (m_subHeaderPanel = GetCtrl(UI.WGT_SUB_HEADER_SPACE).GetComponent<UIPanel>());

	private GameObject ChannelSelectSpriteButtonObject => m_channelSelectSpriteButtonObject ?? (m_channelSelectSpriteButtonObject = GetCtrl(UI.BTN_SPR_CHANNEL_SELECT).gameObject);

	private UILabel ChannelName => m_ChannelName ?? (m_ChannelName = GetCtrl(UI.LBL_CHANNEL_NAME).GetComponent<UILabel>());

	private GameObject ShowUserListButtonObject => m_showUserListButtonObject ?? (m_showUserListButtonObject = GetCtrl(UI.BTN_SHOW_USER_LIST).gameObject);

	private UIScrollView PersonalMsgScrollView => m_personalMsgScrollView ?? (m_personalMsgScrollView = GetCtrl(UI.SCR_PERSONAL_MSG_LIST_VIEW).GetComponent<UIScrollView>());

	private UIGrid PersonalMsgGrid => m_personalMsgGrid ?? (m_personalMsgGrid = GetCtrl(UI.GRD_PERSONAL_MSG_VIEW).GetComponent<UIGrid>());

	private GameObject PersonalMsgGridObj => m_personalMsgGridObj ?? (m_personalMsgGridObj = PersonalMsgGrid.gameObject);

	private UIWidget WidgetBot => m_widgetBot ?? (m_widgetBot = GetCtrl(UI.WGT_ANCHOR_BOTTOM).GetComponent<UIWidget>());

	private UIWidget WidgetChatRoot => m_widgetChatRoot ?? (m_widgetChatRoot = GetCtrl(UI.WGT_CHAT_ROOT).GetComponent<UIWidget>());

	private UIPanel ScrollPanel => m_scrollPanel ?? (m_scrollPanel = GetCtrl(UI.SCR_CHAT).GetComponent<UIPanel>());

	private UIGrid StampScrollGrid => m_stampScrollGrid ?? (m_stampScrollGrid = GetCtrl(UI.GRD_STAMP_LIST).GetComponent<UIGrid>());

	private UISprite StampChatFrame => m_stampChatFrame ?? (m_stampChatFrame = GetCtrl(UI.SPR_CHAT_FRAME).GetComponent<UISprite>());

	private UIPanel StampScrollPanel => m_stampScrollPanel ?? (m_stampScrollPanel = GetCtrl(UI.SCR_STAMP_LIST).GetComponent<UIPanel>());

	private UIWidget SpriteBgBlack => m_spriteBgBlack ?? (m_spriteBgBlack = GetCtrl(UI.SPR_BG_BLACK).GetComponent<UIWidget>());

	private UIWidget WidgetInputCloseButton => m_widgetInputCloseButton ?? (m_widgetInputCloseButton = GetCtrl(UI.BTN_INPUT_CLOSE).GetComponent<UIWidget>());

	private bool IsShowFullChatView => m_isShowFullChatView;

	private bool IsPortrait => m_isPortrait;

	private bool IsLandScapeFullViewMode
	{
		get
		{
			if (IsShowFullChatView)
			{
				return !IsPortrait;
			}
			return false;
		}
	}

	public ChatStateMachine<ChatState> StateMachine => m_stateMachine;

	private float CurrentTotalHeight
	{
		get
		{
			if (CurrentData == null)
			{
				return 0f;
			}
			return CurrentData.currentTotalHeight;
		}
	}

	private bool hasRoomChat => MonoBehaviourSingleton<ChatManager>.I.roomChat != null;

	private bool hasLoungeChat => MonoBehaviourSingleton<ChatManager>.I.loungeChat != null;

	private bool hasClanChat => MonoBehaviourSingleton<ChatManager>.I.clanChat != null;

	private void OnEnable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
	}

	private void OnDisable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		m_isPortrait = is_portrait;
		InitStampList();
		SetBaseWidgetSettings();
		SetParent(UI.BTN_HIDE_LOG, is_portrait ? UI.OBJ_HIDE_LOG_P : UI.OBJ_HIDE_LOG_L_2);
		SetHeaderButtonPosition(is_portrait);
		SetStampWindowSettings();
		SetChatBgFrameUI(currentChat);
		GetCtrl(UI.OBJ_CHANNEL_INPUT).localScale = (is_portrait ? Vector3.one : (Vector3.one * 0.75f));
		WidgetChatRoot.bottomAnchor.Set(0f, is_portrait ? 72f : 0f);
		WidgetChatRoot.topAnchor.Set(1f, is_portrait ? (-72f) : (-4f));
		SetScrollPanelUI(is_portrait, currentChat);
		float d = 1.17279065f;
		SpriteBgBlack.transform.localScale = (is_portrait ? Vector3.one : (Vector3.one * d));
		if (logView.isOpened || logView.isOpening)
		{
			if (is_portrait)
			{
				inputBG.Close(delegate
				{
				});
			}
			else
			{
				inputBG.Open(delegate
				{
				});
				GetCtrl(UI.BTN_SHOW_LOG).gameObject.SetActive(value: false);
			}
		}
		ScrollView.panel.widgetsAreStatic = false;
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			ScrollView.panel.widgetsAreStatic = true;
		});
		UpdateCloseButtonPosition();
		UpdateAnchors();
	}

	private void SetStampWindowSettings()
	{
		int num = IsLandScapeFullViewMode ? 4 : 5;
		int num2 = IsLandScapeFullViewMode ? 3 : 2;
		StampScrollGrid.maxPerLine = num;
		StampScrollGrid.enabled = true;
		float num3 = StampScrollGrid.cellWidth * (float)num / 2f;
		float cellHeight = StampScrollGrid.cellHeight;
		StampChatFrame.leftAnchor.Set(0.5f, 0f - num3);
		StampChatFrame.rightAnchor.Set(0.5f, num3);
		StampChatFrame.topAnchor.Set(0.5f, cellHeight * 0.5f);
		StampChatFrame.bottomAnchor.Set(0.5f, (0f - StampScrollGrid.cellHeight) * ((float)num2 - 0.5f));
	}

	private void SetBaseWidgetSettings()
	{
		Vector4 vector = IsLandScapeFullViewMode ? WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS : WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS;
		_ = IsLandScapeFullViewMode;
		WidgetTop.leftAnchor.Set(IsLandScapeFullViewMode ? 0f : 0.5f, vector.x);
		WidgetTop.rightAnchor.Set(IsLandScapeFullViewMode ? 0f : 0.5f, vector.y);
		WidgetTop.bottomAnchor.Set(IsLandScapeFullViewMode ? 0f : 0.5f, vector.z);
		WidgetTop.topAnchor.Set(IsLandScapeFullViewMode ? 1f : 0.5f, vector.w);
		vector = (IsPortrait ? WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS : (IsShowFullChatView ? WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS : WIDGET_ANCHOR_BOT_LANDSCAPE_SETTINGS));
		WidgetBot.leftAnchor.Set(IsPortrait ? 0.5f : 1f, vector.x);
		WidgetBot.rightAnchor.Set(IsPortrait ? 0.5f : 1f, vector.y);
		WidgetBot.bottomAnchor.Set(0f, vector.z);
		WidgetBot.topAnchor.Set(0f, vector.w);
		if (!SpecialDeviceManager.HasSpecialDeviceInfo)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		if (!specialDeviceInfo.NeedModifyChatAnchor)
		{
			return;
		}
		if (SpecialDeviceManager.IsPortrait)
		{
			WidgetBot.leftAnchor.Set(0.5f, specialDeviceInfo.ChatBottomAnchorPortrait.left);
			WidgetBot.rightAnchor.Set(0.5f, specialDeviceInfo.ChatBottomAnchorPortrait.right);
			WidgetBot.bottomAnchor.Set(0f, specialDeviceInfo.ChatBottomAnchorPortrait.bottom);
			WidgetBot.topAnchor.Set(0f, specialDeviceInfo.ChatBottomAnchorPortrait.top);
			return;
		}
		WidgetTop.leftAnchor.Set(0f, specialDeviceInfo.ChatTopAnchorLandscape.left);
		WidgetTop.rightAnchor.Set(0f, specialDeviceInfo.ChatTopAnchorLandscape.right);
		WidgetTop.bottomAnchor.Set(0f, specialDeviceInfo.ChatTopAnchorLandscape.bottom);
		WidgetTop.topAnchor.Set(1f, specialDeviceInfo.ChatTopAnchorLandscape.top);
		if (IsShowFullChatView)
		{
			WidgetBot.leftAnchor.Set(1f, specialDeviceInfo.ChatBottomAnchorLandscapeFull.left);
			WidgetBot.rightAnchor.Set(1f, specialDeviceInfo.ChatBottomAnchorLandscapeFull.right);
			WidgetBot.bottomAnchor.Set(0f, specialDeviceInfo.ChatBottomAnchorLandscapeFull.bottom);
			WidgetBot.topAnchor.Set(0f, specialDeviceInfo.ChatBottomAnchorLandscapeFull.top);
		}
		else
		{
			WidgetBot.leftAnchor.Set(1f, specialDeviceInfo.ChatBottomAnchorLandscapeSmall.left);
			WidgetBot.rightAnchor.Set(1f, specialDeviceInfo.ChatBottomAnchorLandscapeSmall.right);
			WidgetBot.bottomAnchor.Set(0f, specialDeviceInfo.ChatBottomAnchorLandscapeSmall.bottom);
			WidgetBot.topAnchor.Set(0f, specialDeviceInfo.ChatBottomAnchorLandscapeSmall.top);
		}
	}

	private void SetScrollPanelUI(bool _isPortrait, CHAT_TYPE _t)
	{
		switch (_t)
		{
		case CHAT_TYPE.HOME:
			ScrollPanel.topAnchor.Set(1f, -129f);
			ScrollPanel.bottomAnchor.Set(0f, _isPortrait ? 316f : 22f);
			break;
		case CHAT_TYPE.PERSONAL:
			ScrollPanel.topAnchor.Set(1f, -129f);
			ScrollPanel.bottomAnchor.Set(0f, _isPortrait ? 15f : 22f);
			break;
		default:
			ScrollPanel.topAnchor.Set(1f, -72f);
			ScrollPanel.bottomAnchor.Set(0f, _isPortrait ? 316f : 22f);
			break;
		}
		ScrollPanel.SetDirty();
	}

	private void SetChatBgFrameUI(CHAT_TYPE _t)
	{
		if (!(BackgroundInFrame == null))
		{
			Vector4 vector = (_t != CHAT_TYPE.PERSONAL && !HasState(typeof(ChatState_PersonalTab)) && !IsLandScapeFullViewMode) ? UI_SPRITE_CHAT_BG_FRAME_DEFAULT_POS : UI_SPRITE_CHAT_BG_FRAME_PERSONAL_POS;
			BackgroundInFrame.leftAnchor.Set(0f, vector.x);
			BackgroundInFrame.rightAnchor.Set(1f, vector.y);
			BackgroundInFrame.bottomAnchor.Set(0f, vector.z);
			BackgroundInFrame.topAnchor.Set(1f, vector.w);
		}
	}

	private void SetParent(UI changeTarget, UI parent)
	{
		Transform ctrl = GetCtrl(parent);
		SetParent(changeTarget, ctrl);
	}

	private void SetParent(UI changeTarget, Transform parent)
	{
		Transform ctrl = GetCtrl(changeTarget);
		ctrl.parent = parent;
		ctrl.localPosition = Vector3.zero;
	}

	private void UpdateCloseButtonPosition()
	{
		UIWidget widgetInputCloseButton = WidgetInputCloseButton;
		if ((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestAcceptRoom")
		{
			widgetInputCloseButton.leftAnchor.Set(1f, UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.x);
			widgetInputCloseButton.rightAnchor.Set(1f, UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.y);
			widgetInputCloseButton.bottomAnchor.Set(0f, UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.z);
			widgetInputCloseButton.topAnchor.Set(0f, UI_BTN_INPUT_CLOSE_LANDSCAPE_POS.w);
		}
		else
		{
			widgetInputCloseButton.leftAnchor.Set(1f, UI_BTN_INPUT_CLOSE_DEFAULT_POS.x);
			widgetInputCloseButton.rightAnchor.Set(1f, UI_BTN_INPUT_CLOSE_DEFAULT_POS.y);
			widgetInputCloseButton.bottomAnchor.Set(1f, UI_BTN_INPUT_CLOSE_DEFAULT_POS.z);
			widgetInputCloseButton.topAnchor.Set(1f, UI_BTN_INPUT_CLOSE_DEFAULT_POS.w);
		}
	}

	private IEnumerator Start()
	{
		Initialize();
		yield return null;
		InitStateMachine();
		yield return null;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatItem");
		LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
		LoadObject lo_chatAdvisaryItem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		InitChatDataList();
		for (int i = 0; i < m_PostRequetQueue.Length; i++)
		{
			m_PostRequetQueue[i] = new PostRequestQueue();
		}
		yield return null;
		m_ChatItemPrefab = (lo_quest_chatitem.loadedObject as GameObject);
		m_ChatStampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		m_ChatAdvisaryItemPrefab = (lo_chatAdvisaryItem.loadedObject as GameObject);
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		yield return null;
		ChangeSliderPos(CHAT_TYPE.HOME);
		SetSliderLimit();
		DummyDragScroll.width = 410;
		yield return null;
		string channelName = "0001";
		SetChannelName(channelName);
		ResetStampIdList();
		Reset();
		HideAll();
		HideOpenButton();
		ScrollView.onStoppedMoving = onStoppedMovingScrollView;
		ScrollView.onDragStarted = onDragStartScrollView;
		ScrollView.onDragFinished = onDragEndScrollView;
		ScrollView.onPressStart = onPressStartScrollView;
		ScrollView.onPressEnd = onPressEndScrollView;
	}

	private void InitChatDataList()
	{
		m_DataList[0] = new ChatItemListData(GetCtrl(UI.OBJ_HOME_ITEM_LIST_ROOT).gameObject);
		m_DataList[1] = new ChatItemListData(GetCtrl(UI.OBJ_ROOM_ITEM_LIST_ROOT).gameObject);
		m_DataList[2] = new ChatItemListData(GetCtrl(UI.OBJ_LOUNGE_ITEM_LIST_ROOT).gameObject);
		m_DataList[3] = m_DataList[1];
		m_DataList[4] = null;
		m_DataList[5] = new ChatItemListData(GetCtrl(UI.OBJ_CLAN_ITEM_LIST_ROOT).gameObject);
	}

	private void ChangeSliderPos(CHAT_TYPE type)
	{
		SLIDER_OPEN_POS = ((type == CHAT_TYPE.HOME) ? HOME_SLIDER_OPEN_POS : ROOM_SLIDER_OPEN_POS);
		SLIDER_CLOSE_POS = ((type == CHAT_TYPE.HOME) ? HOME_SLIDER_CLOSE_POS : ROOM_SLIDER_CLOSE_POS);
	}

	private void SetSliderLimit()
	{
		UIPanel component = GetCtrl(UI.WGT_SLIDE_LIMIT).GetComponent<UIPanel>();
		component.topAnchor.absolute = (int)SLIDER_CLOSE_POS.y + 9;
		component.bottomAnchor.absolute = (int)SLIDER_OPEN_POS.y - 49;
	}

	private void Reset()
	{
		Input.value = "";
		InputFrame.Reset();
		UpdateAnchors();
		UpdateWindowSize();
	}

	public void SetChannelName(string name)
	{
		ChannelName.text = name;
	}

	public bool IsOpeningWindow()
	{
		if (!inputView.isOpened)
		{
			return inputView.isOpening;
		}
		return true;
	}

	public void OnTouchPost()
	{
		if (UserInfoManager.IsRegisterdAge() && UserInfoManager.IsEnableCommunication())
		{
			string value = Input.value;
			if (!string.IsNullOrEmpty(value) && value.Trim().Length != 0)
			{
				SendMessageAsMine(value);
				Input.value = "";
				OnInput();
			}
		}
	}

	public void OnInput()
	{
		InputFrame.FrameResize();
		string value = Input.value;
		SetActive(UI.LBL_DEFAULT, string.IsNullOrEmpty(value));
	}

	private void StartPostChatProcess()
	{
		m_handlPostChatPorcess = PostChatProcess();
	}

	private void StopPostChatProcess()
	{
		m_handlPostChatPorcess = null;
	}

	public int GetPendingQueueNum()
	{
		int num = 0;
		if (m_PostRequetQueue == null)
		{
			num = 0;
		}
		else
		{
			switch (HomeType)
			{
			case HOME_TYPE.HOME_TOP:
				if (m_PostRequetQueue[0] != null)
				{
					num = m_PostRequetQueue[0].Count;
				}
				break;
			case HOME_TYPE.LOUNGE_TOP:
				if (m_PostRequetQueue[2] != null)
				{
					num = m_PostRequetQueue[2].Count;
				}
				break;
			}
			if (hasRoomChat && m_PostRequetQueue[1] != null)
			{
				num += m_PostRequetQueue[1].Count;
			}
		}
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			num += MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
		}
		return num;
	}

	public int GetPendingQueueNumWithoutRoom()
	{
		int num = 0;
		if (m_PostRequetQueue != null)
		{
			if (hasLoungeChat)
			{
				if (m_PostRequetQueue[2] != null)
				{
					num = m_PostRequetQueue[2].Count;
				}
			}
			else if (m_PostRequetQueue[0] != null)
			{
				num = m_PostRequetQueue[0].Count;
			}
		}
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			num += MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
		}
		return num;
	}

	private IEnumerator PostChatProcess()
	{
		bool bPolling = true;
		while (bPolling)
		{
			int queueArrayIndex = GetQueueArrayIndex(currentChat);
			if (m_PostRequetQueue[queueArrayIndex].Count > 0)
			{
				ChatPostRequest request = m_PostRequetQueue[queueArrayIndex].Dequeue();
				Post(request);
				yield return null;
			}
			yield return null;
		}
	}

	public int GetQueueArrayIndex(CHAT_TYPE _t)
	{
		if (_t == CHAT_TYPE.ROOM || _t == CHAT_TYPE.FIELD)
		{
			return 1;
		}
		return (int)_t;
	}

	public void SendStampAsMine(int stampId)
	{
		if (!CanIPostTheStamp(stampId))
		{
			return;
		}
		switch (currentChat)
		{
		case CHAT_TYPE.HOME:
			if (MonoBehaviourSingleton<ChatManager>.I.homeChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.homeChat.SendStamp(stampId);
			}
			break;
		case CHAT_TYPE.ROOM:
		case CHAT_TYPE.FIELD:
			if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(stampId);
			}
			break;
		case CHAT_TYPE.LOUNGE:
			if (MonoBehaviourSingleton<ChatManager>.I.loungeChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendStamp(stampId);
			}
			break;
		case CHAT_TYPE.CLAN:
			if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.clanChat.SendStamp(stampId);
			}
			break;
		}
		UpdateSendBlock();
		if (m_IsOnshotStampMode)
		{
			HideAll();
		}
	}

	public void SendMessageAsMine(string message)
	{
		switch (currentChat)
		{
		case CHAT_TYPE.HOME:
			if (MonoBehaviourSingleton<ChatManager>.I.homeChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.homeChat.SendMessage(message);
			}
			break;
		case CHAT_TYPE.ROOM:
		case CHAT_TYPE.FIELD:
			if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.roomChat.SendMessage(message);
			}
			break;
		case CHAT_TYPE.LOUNGE:
			if (MonoBehaviourSingleton<ChatManager>.I.loungeChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendMessage(message);
			}
			break;
		case CHAT_TYPE.CLAN:
			if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.clanChat.SendMessage(message);
			}
			break;
		}
		UpdateSendBlock();
		if (m_IsOnshotStampMode)
		{
			HideAll();
		}
	}

	private void Post(ChatPostRequest request)
	{
		if (CurrentData == null)
		{
			return;
		}
		ChatItemListData currentData = CurrentData;
		switch (request.Type)
		{
		case ChatPostRequest.TYPE.Message:
			AddNextChatItem(request, currentData, delegate(ChatItem chatItem)
			{
				chatItem.Init(request.userId, request.userName, request.message, request.chatItemId);
			});
			SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
			break;
		case ChatPostRequest.TYPE.Stamp:
		{
			if (!IsValidStampId(request.stampId))
			{
				break;
			}
			StampTable.Data data = Singleton<StampTable>.I.GetData((uint)request.stampId);
			if (data != null)
			{
				AddNextChatItem(request, currentData, delegate(ChatItem chatItem)
				{
					chatItem.Init(request.userId, request.userName, request.stampId, request.chatItemId);
				});
				if (data.hasSE)
				{
					SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
				}
				else
				{
					SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
				}
			}
			break;
		}
		case ChatPostRequest.TYPE.Notification:
			AddNextChatItem(request, currentData, delegate(ChatItem chatItem)
			{
				chatItem.Init(request.message, request.chatItemId, request.isWhiteColor);
			});
			SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
			break;
		}
	}

	private void AddNextChatItem(ChatPostRequest request, ChatItemListData data, Action<ChatItem> initializer)
	{
		if (!(m_ChatItemPrefab == null))
		{
			if (!request.IsOldMessage)
			{
				addNextChatItem(data, initializer);
				StateMachine.CurrentState.OnShowMessageOnDisplay(request.chatItemId);
			}
			else
			{
				addPrevChatItem(data, initializer);
			}
		}
	}

	private void addNextChatItem(ChatItemListData data, Action<ChatItem> initializer)
	{
		if (data.itemList.Count > 0)
		{
			data.currentTotalHeight += 22f;
		}
		float num = 0f;
		ChatItem chatItem = null;
		if (data.itemList.Count < 30)
		{
			chatItem = ResourceUtility.Realizes(m_ChatItemPrefab, data.rootObject.transform, 5).GetComponent<ChatItem>();
		}
		else
		{
			chatItem = data.itemList[data.oldestItemIndex];
			data.oldestItemIndex++;
			if (data.oldestItemIndex == 30)
			{
				data.oldestItemIndex = 0;
			}
			data.currentTotalHeight -= chatItem.height + 22f;
			ScrollView.panel.widgetsAreStatic = false;
			num = chatItem.height + 22f;
			data.MoveAll(num);
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
			{
				ScrollView.panel.widgetsAreStatic = true;
			});
		}
		float currentTotalHeight = data.currentTotalHeight;
		chatItem.transform.localPosition = CHAT_ITEM_OFFSET_POS + Vector3.down * currentTotalHeight;
		initializer(chatItem);
		data.currentTotalHeight += chatItem.height;
		UpdateDummyDragScroll();
		float num2 = IsPortrait ? 300 : 344;
		if (data.currentTotalHeight - num2 < ScrollViewTrans.localPosition.y)
		{
			float num3 = data.currentTotalHeight + ScrollView.panel.baseClipRegion.y - ScrollView.panel.baseClipRegion.w * 0.5f + (ScrollViewTrans.localPosition.y + ScrollView.panel.clipOffset.y) + ScrollView.panel.clipSoftness.y;
			if (data.itemList.Count >= 30)
			{
				ForceScroll(num3 - chatItem.height - 22f, useSpring: false);
			}
			ForceScroll(num3, useSpring: true);
		}
		else if (data.itemList.Count >= 30 && ScrollViewTrans.localPosition.y > num2)
		{
			ForceScroll(ScrollViewTrans.localPosition.y - num, useSpring: false);
		}
		if (data.itemList.Count < 30)
		{
			data.itemList.Add(chatItem);
		}
	}

	private void addPrevChatItem(ChatItemListData data, Action<ChatItem> initializer)
	{
		ScrollView.panel.widgetsAreStatic = false;
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			ScrollView.panel.widgetsAreStatic = true;
		});
		ChatItem chatItem = null;
		if (data.itemList.Count < 30)
		{
			chatItem = ResourceUtility.Realizes(m_ChatItemPrefab, data.rootObject.transform, 5).GetComponent<ChatItem>();
		}
		else
		{
			chatItem = data.itemList[data.newestIndex];
			data.itemList.Remove(chatItem);
		}
		initializer(chatItem);
		tmpList.Clear();
		tmpList.Add(chatItem);
		tmpList.AddRange(data.itemList);
		data.itemList.Clear();
		data.itemList.AddRange(tmpList);
		data.oldestItemIndex = 0;
		Vector3 cHAT_ITEM_OFFSET_POS = CHAT_ITEM_OFFSET_POS;
		data.currentTotalHeight = 0f;
		for (int j = 0; j < data.itemList.Count; j++)
		{
			if (j > 0)
			{
				data.currentTotalHeight += 22f;
			}
			ChatItem chatItem2 = data.itemList[j];
			chatItem2.transform.localPosition = cHAT_ITEM_OFFSET_POS;
			data.currentTotalHeight += chatItem2.height;
			cHAT_ITEM_OFFSET_POS += Vector3.down * chatItem2.height;
			cHAT_ITEM_OFFSET_POS += Vector3.down * 22f;
		}
		UpdateDummyDragScroll();
	}

	private void ForceScroll(float newHeight, bool useSpring)
	{
		ScrollView.DisableSpring();
		if (useSpring)
		{
			SpringPanel.Begin(ScrollView.gameObject, Vector3.up * newHeight, 20f);
			return;
		}
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		float num = ScrollViewTrans.localPosition.y + clipOffset.y;
		ScrollViewTrans.localPosition = Vector3.up * newHeight;
		clipOffset.y = 0f - newHeight + num;
		ScrollView.panel.clipOffset = clipOffset;
	}

	public void SaveSlideOffset()
	{
		CurrentData.slideOffset = ScrollViewTrans.localPosition.y + ScrollView.panel.height;
	}

	public void UpdateWindowSize()
	{
		UpdateDummyDragScroll();
	}

	private void Initialize()
	{
		logView = CreateChatUIFadeGroup(UI.WGT_ANCHOR_TOP);
		inputView = CreateChatUIFadeGroup(UI.WGT_ANCHOR_BOTTOM);
		inputBG = CreateChatUIFadeGroup(UI.SPR_BG_POST_FRAME);
		channelSelect = CreateChatUIFadeGroup(UI.OBJ_CHANNEL_SELECT);
		chatOpenButton = CreateChatUIFadeGroup(UI.BTN_OPEN);
		bgBlack = CreateChatUIFadeGroup(UI.SPR_BG_BLACK);
		sendBlockView = CreateChatUIFadeGroup(UI.OBJ_POST_BLOCK);
		sendLimitView = CreateChatUIFadeGroup(UI.WGT_POST_LIMIT);
		sendLimitNoClanView = CreateChatUIFadeGroup(UI.WGT_NO_CLAN);
		sendLimitReloadView = CreateChatUIFadeGroup(UI.WGT_RELOAD);
		noConnectionView = CreateChatUIFadeGroup(UI.WGT_NO_CONNECTION);
		GenerateHeaderButton();
		SetButtonEvent(UI.BTN_SHOW_LOG, new EventDelegate(ShowFull));
		SetButtonEvent(UI.BTN_HIDE_LOG, new EventDelegate(ShowInputOnly));
		SetButtonEvent(UI.BTN_INPUT_CLOSE, new EventDelegate(HideFull));
		SetButtonEvent(UI.BTN_RECONNECT, new EventDelegate(ShowChannelSelect));
		InitChannelSelect();
		InitChannelInput();
		SetButtonEvent(UI.BTN_OPEN, new EventDelegate(ShowFullWithEdit));
		SetLabelText(UI.LBL_CHANNEL, StringTable.Get(STRING_CATEGORY.CHAT, 0u));
		SetLabelText(UI.LBL_CHAT_HOME, StringTable.Get(STRING_CATEGORY.CHAT, 1u));
		SetLabelText(UI.LBL_NO_CONNECTION, StringTable.Get(STRING_CATEGORY.CHAT, 4u));
		SetLabelText(UI.LBL_POST_LIMIT, StringTable.Get(STRING_CATEGORY.CHAT, 5u));
		MonoBehaviourSingleton<ChatManager>.I.CreateHomeChat();
		MonoBehaviourSingleton<ChatManager>.I.homeChat.onReceiveText += OnReceiveHomeText;
		MonoBehaviourSingleton<ChatManager>.I.homeChat.onReceiveStamp += OnReceiveHomeStamp;
		MonoBehaviourSingleton<ChatManager>.I.homeChat.onJoin += OnJoinHomeChat;
		MonoBehaviourSingleton<ChatManager>.I.homeChat.onDisconnect += OnDisconnectHomeChat;
		MonoBehaviourSingleton<ChatManager>.I.OnCreateRoomChat += OnCreateRoomChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyRoomChat += OnDestroyRoomChat;
		MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += OnCreateLoungeChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += OnDestroyLoungeChat;
		MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat += OnCreateClanChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat += OnDestroyClanChat;
		MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(MonoBehaviourSingleton<ClanMatchingManager>.I.GetChatConnection());
		ChatInputFrame inputFrame = InputFrame;
		inputFrame.onChange = (Action)Delegate.Combine(inputFrame.onChange, (Action)delegate
		{
			OnInput();
		});
		ChatInputFrame inputFrame2 = InputFrame;
		inputFrame2.onSubmit = (Action)Delegate.Combine(inputFrame2.onSubmit, (Action)delegate
		{
			OnTouchPost();
		});
	}

	private void InitChannelSelect()
	{
		SetButtonEvent(UI.BTN_SPR_CHANNEL_SELECT, new EventDelegate(ShowChannelSelect));
		SetButtonEvent(UI.BTN_CLOSE_CHANNEL_SELECT, new EventDelegate(delegate
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
			CloseChannelSelect();
		}));
		SetButtonEvent(UI.BTN_HOT, new EventDelegate(OnSelectHotChannel));
		SetButtonEvent(UI.BTN_QUIET, new EventDelegate(OnSelectQuietChannel));
		SetButtonEvent(UI.BTN_ANYWARE, new EventDelegate(OnSelectRecommendedChannel));
		SetButtonEvent(UI.BTN_NUMBER_INPUT, new EventDelegate(OnSelectInputChannelNumber));
		SetButtonEvent(UI.BTN_SHOW_ALL, new EventDelegate(OnSelectShowAll));
		SetButtonEvent(UI.BTN_EDIT, new EventDelegate(OnSelectEdit));
	}

	private void InitChannelInput()
	{
		ChatUIFadeGroup root = CreateChatUIFadeGroup(UI.OBJ_CHANNEL_INPUT);
		channelInputPanel = new ChatChannelInputPanel(root);
		channelInputPanel.SetNumLabels(GetLabel(UI.LBL_INPUT_PASS_4), GetLabel(UI.LBL_INPUT_PASS_3), GetLabel(UI.LBL_INPUT_PASS_2), GetLabel(UI.LBL_INPUT_PASS_1));
		channelInputPanel.SetNumButtons(GetButton(UI.BTN_0), GetButton(UI.BTN_1), GetButton(UI.BTN_2), GetButton(UI.BTN_3), GetButton(UI.BTN_4), GetButton(UI.BTN_5), GetButton(UI.BTN_6), GetButton(UI.BTN_7), GetButton(UI.BTN_8), GetButton(UI.BTN_9));
		channelInputPanel.SetOKButton(GetButton(UI.BTN_NEXT));
		channelInputPanel.SetClearButton(GetButton(UI.BTN_CLEAR));
		channelInputPanel.SetCloseButton(GetButton(UI.BTN_CLOSE_CHANNEL_INPUT));
		channelInputPanel.SetOnOKDelegate(OnInputChannel);
		channelInputPanel.SetOnCloseButtonDelegate(delegate
		{
			channelSelect.Open(delegate
			{
			});
		});
	}

	private UIButton GetButton(UI elm)
	{
		Transform ctrl = GetCtrl(elm);
		if ((bool)ctrl)
		{
			UIButton component = ctrl.GetComponent<UIButton>();
			if ((bool)component)
			{
				return component;
			}
		}
		return null;
	}

	private UILabel GetLabel(UI elm)
	{
		Transform ctrl = GetCtrl(elm);
		if ((bool)ctrl)
		{
			UILabel component = ctrl.GetComponent<UILabel>();
			if ((bool)component)
			{
				return component;
			}
		}
		return null;
	}

	private void SetButtonEvent(UI elm, EventDelegate eventDelegate)
	{
		Transform ctrl = GetCtrl(elm);
		if ((bool)ctrl)
		{
			UIButton component = ctrl.GetComponent<UIButton>();
			if ((bool)component)
			{
				component.onClick.Clear();
				component.onClick.Add(eventDelegate);
			}
		}
	}

	private void OnCreateRoomChat()
	{
		MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveText += OnReceiveRoomText;
		MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveStamp += OnReceiveRoomStamp;
		MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveNotification += OnReceiveRoomNotification;
	}

	private void OnDestroyRoomChat(ChatRoom roomChat)
	{
		roomChat.onReceiveText -= OnReceiveRoomText;
		roomChat.onReceiveStamp -= OnReceiveRoomStamp;
		MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveNotification -= OnReceiveRoomNotification;
		m_PostRequetQueue[1].Clear();
		m_DataList[1].Reset();
	}

	private void OnCreateLoungeChat(ChatRoom loungeChat)
	{
		loungeChat.onReceiveText += OnReceiveLoungeText;
		loungeChat.onReceiveStamp += OnReceiveLoungeStamp;
		loungeChat.onReceiveNotification += OnReceiveLoungeNotification;
		currentChat = CHAT_TYPE.LOUNGE;
	}

	private void OnDestroyLoungeChat(ChatRoom loungeChat)
	{
		loungeChat.onReceiveText -= OnReceiveLoungeText;
		loungeChat.onReceiveStamp -= OnReceiveLoungeStamp;
		loungeChat.onReceiveNotification -= OnReceiveLoungeNotification;
		m_PostRequetQueue[2].Clear();
		m_DataList[2].Reset();
		currentChat = CHAT_TYPE.HOME;
	}

	private void OnCreateClanChat(ChatRoom clanChat)
	{
		clanChat.onReceiveText += OnReceiveClanText;
		clanChat.onReceiveStamp += OnReceiveClanStamp;
		clanChat.onReceiveNotification += OnReceiveClanNotification;
		clanChat.onAfterSendUserMessage += OnClanAfterSendUserMessage;
		currentChat = CHAT_TYPE.CLAN;
	}

	private void OnDestroyClanChat(ChatRoom clanChat)
	{
		clanChat.onReceiveText -= OnReceiveClanText;
		clanChat.onReceiveStamp -= OnReceiveClanStamp;
		clanChat.onReceiveNotification -= OnReceiveClanNotification;
		clanChat.onAfterSendUserMessage -= OnClanAfterSendUserMessage;
		m_PostRequetQueue[5].Clear();
		m_DataList[5].Reset();
		currentChat = CHAT_TYPE.HOME;
	}

	private void OnJoinHomeChat(CHAT_ERROR_TYPE errorType)
	{
		if (errorType != 0)
		{
			OnError(StringTable.Get(STRING_CATEGORY.CHAT_ERROR, 2u));
		}
		UpdateSendBlock();
	}

	private void OnDisconnectHomeChat()
	{
		UpdateSendBlock();
	}

	private bool IsAllowedUser(int userId)
	{
		if (MonoBehaviourSingleton<BlackListManager>.IsValid())
		{
			return !MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(userId);
		}
		return true;
	}

	private void OnReceiveText(CHAT_TYPE chatType, int userId, string userName, string message, string chatItemId, bool IsOldMessage = false)
	{
		if (IsAllowedUser(userId))
		{
			m_PostRequetQueue[(int)chatType].Enqueue(new ChatPostRequest(userId, userName, message, chatItemId, IsOldMessage));
		}
	}

	private void OnReceiveStamp(CHAT_TYPE chatType, int userId, string userName, int stampId, string chatItemId, bool IsOldMessage = false)
	{
		if (IsAllowedUser(userId))
		{
			m_PostRequetQueue[(int)chatType].Enqueue(new ChatPostRequest(userId, userName, stampId, chatItemId, IsOldMessage));
		}
	}

	private void OnReceiveHomeText(int userId, string userName, string message, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveText(CHAT_TYPE.HOME, userId, userName, message, chatItemId);
	}

	private void OnReceiveHomeStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveStamp(CHAT_TYPE.HOME, userId, userName, stampId, chatItemId);
	}

	private void OnReceiveRoomText(int userId, string userName, string message, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveText(CHAT_TYPE.ROOM, userId, userName, message, chatItemId);
	}

	private void OnReceiveRoomStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveStamp(CHAT_TYPE.ROOM, userId, userName, stampId, chatItemId);
	}

	private void OnReceiveRoomNotification(string message, string chatItemId, bool isOldMessage = false)
	{
		m_PostRequetQueue[1].Enqueue(new ChatPostRequest(message, chatItemId));
	}

	private void OnReceiveLoungeText(int userId, string userName, string message, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveText(CHAT_TYPE.LOUNGE, userId, userName, message, chatItemId);
	}

	private void OnReceiveLoungeStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveStamp(CHAT_TYPE.LOUNGE, userId, userName, stampId, chatItemId);
	}

	private void OnReceiveLoungeNotification(string message, string chatItemId, bool isOldMessage = false)
	{
		m_PostRequetQueue[2].Enqueue(new ChatPostRequest(message, chatItemId));
	}

	private void OnReceiveClanText(int userId, string userName, string message, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveText(CHAT_TYPE.CLAN, userId, userName, message, chatItemId, isOldMessage);
	}

	private void OnReceiveClanStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false)
	{
		OnReceiveStamp(CHAT_TYPE.CLAN, userId, userName, stampId, chatItemId, isOldMessage);
	}

	private void OnReceiveClanNotification(string message, string chatItemId, bool isOldMessage = false)
	{
		m_PostRequetQueue[5].Enqueue(new ChatPostRequest(message, chatItemId, isOldMessage, isWhiteColor: true));
	}

	private void OnClanAfterSendUserMessage()
	{
		if (CurrentData.itemList.Count > 0 && CurrentData.itemList.Count > CurrentData.newestIndex && CurrentData.itemList[CurrentData.newestIndex] != null && CurrentData.itemList[CurrentData.newestIndex].gameObject.activeSelf)
		{
			if (StateMachine.IsRun())
			{
				StateMachine.CurrentState.OnDragAtBottom(CurrentData.itemList[CurrentData.newestIndex].chatItemId, 10f);
			}
		}
		else
		{
			ResetCacheData(CHAT_TYPE.CLAN);
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatResetCache();
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
		}
	}

	private ChatUIFadeGroup CreateChatUIFadeGroup(UI elm)
	{
		Transform ctrl = GetCtrl(elm);
		UIRect root = null;
		if ((bool)ctrl)
		{
			root = ctrl.GetComponent<UIRect>();
		}
		ChatUIFadeGroup chatUIFadeGroup = new ChatUIFadeGroup(root);
		chatUIFadeGroup.Initialize();
		return chatUIFadeGroup;
	}

	public void SetRoomChatNameType(bool field)
	{
		isFieldChat = field;
		int i = 0;
		for (int count = m_headerButtonList.Count; i < count; i++)
		{
			if (!(m_headerButtonList[i] == null) && (m_headerButtonList[i].MyChatType == CHAT_TYPE.ROOM || m_headerButtonList[i].MyChatType == CHAT_TYPE.FIELD))
			{
				m_headerButtonList[i].InitUILabel((!isFieldChat) ? CHAT_TYPE.ROOM : CHAT_TYPE.FIELD);
			}
		}
	}

	private void UpdateChannnelName()
	{
		UpdateChannnelName(MonoBehaviourSingleton<ChatManager>.I.currentChannel);
	}

	private void UpdateChannnelName(ChatChannel chatChannel)
	{
		int channel = -1;
		if (chatChannel != null)
		{
			channel = chatChannel.channel;
		}
		UpdateChannnelName(channel);
	}

	private void UpdateChannnelName(int channel)
	{
		if (channel > 0)
		{
			SetChannelName(channel.ToString(CHANNEL_FORMAT));
		}
		else
		{
			SetChannelName("未接続");
		}
	}

	private void UpdateAdvisoryItem(bool is_portrait = true)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.advisory != null && !GuildChatAdvisoryItem.HasReadHomeNew())
		{
			Vector3 localPosition = is_portrait ? new Vector3(0f, 280f, 0f) : new Vector3(0f, 215f, 0f);
			if (chatAdvisoryItem == null)
			{
				chatAdvisoryItem = ResourceUtility.Realizes(m_ChatAdvisaryItemPrefab, GetCtrl(UI.WGT_CHAT_TOP), 5).GetComponent<GuildChatAdvisoryItem>();
				chatAdvisoryItem.transform.localPosition = localPosition;
				chatAdvisoryItem.Init(MonoBehaviourSingleton<UserInfoManager>.I.advisory.title, MonoBehaviourSingleton<UserInfoManager>.I.advisory.content);
				SetButtonEvent(chatAdvisoryItem.close, new EventDelegate(delegate
				{
					GuildChatAdvisoryItem.SetReadHomeNew();
					if (chatAdvisoryItem != null)
					{
						UnityEngine.Object.DestroyImmediate(chatAdvisoryItem.gameObject);
						chatAdvisoryItem = null;
					}
				}));
			}
			else
			{
				chatAdvisoryItem.gameObject.SetActive(value: true);
				chatAdvisoryItem.transform.localPosition = localPosition;
			}
		}
		else if (chatAdvisoryItem != null)
		{
			UnityEngine.Object.DestroyImmediate(chatAdvisoryItem.gameObject);
			chatAdvisoryItem = null;
		}
	}

	private bool ValidateBeforeShowUI()
	{
		if (UserInfoManager.IsRegisterdAge())
		{
			return true;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge())
		{
			return true;
		}
		OnEvent("CHAT_AGE_CONFIRM", null, null);
		return false;
	}

	public void ShowFullWithEdit()
	{
		bool flag = GetCurrentStateType() == typeof(ChatState_HomeTab);
		bool flag2 = GetCurrentStateType() == typeof(ChatState_LoungeTab);
		bool flag3 = GetCurrentStateType() == typeof(ChatState_ClanTab);
		StampEditButtonObj.SetActive(flag | flag2 | flag3);
		FavStampEditButtonObj.SetActive(flag | flag2 | flag3);
		ShowFull();
	}

	public void ShowFull()
	{
		ChatCloseButtonObj.SetActive(value: true);
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
		if (!ValidateBeforeShowUI())
		{
			return;
		}
		InitChatTabState();
		m_IsOnshotStampMode = false;
		m_isShowFullChatView = true;
		UpdateSendBlock();
		UpdateChannnelName();
		UpdateCloseButtonPosition();
		UpdateAdvisoryItem(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		bgBlack.Open(delegate
		{
		});
		logView.Open(delegate
		{
		});
		inputView.Open(delegate
		{
		});
		inputBG.Close(delegate
		{
		});
		channelSelect.Close(delegate
		{
		});
		channelInputPanel.Close();
		chatOpenButton.Close(delegate
		{
		});
		StartPostChatProcess();
		NotifyObservers(NOTIFY_FLAG.OPEN_WINDOW);
		GetCtrl(UI.BTN_HIDE_LOG).gameObject.SetActive(isMinimizable);
		if (splitLogView)
		{
			bool flag = MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
			if (!flag)
			{
				inputBG.Open(delegate
				{
				});
				OnScreenRotate(flag);
			}
			else
			{
				InitStampList();
			}
			GetCtrl(UI.BTN_SHOW_LOG).gameObject.SetActive(flag);
			GetCtrl(UI.OBJ_HIDE_LOG_L_2).gameObject.SetActive(value: true);
		}
		SpriteBgBlack.ResizeCollider();
		isFirstSlectClanTab = true;
	}

	public void ShowInputOnly()
	{
		ChatCloseButtonObj.SetActive(value: true);
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
		if (ValidateBeforeShowUI())
		{
			InitChatTabState();
			m_IsOnshotStampMode = true;
			m_isShowFullChatView = false;
			isMinimizable = true;
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
			{
				SetButtonEvent(UI.BTN_HIDE_LOG, new EventDelegate(ShowInputOnly));
			});
			UpdateSendBlock();
			UpdateCloseButtonPosition();
			bgBlack.Close(delegate
			{
			});
			logView.Close(delegate
			{
			});
			inputView.Open(delegate
			{
			});
			inputBG.Open(delegate
			{
			});
			chatOpenButton.Close(delegate
			{
			});
			NotifyObservers(NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY);
			GetCtrl(UI.BTN_SHOW_LOG).gameObject.SetActive(value: true);
			bool is_portrait = MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
			OnScreenRotate(is_portrait);
			if (splitLogView)
			{
				GetCtrl(UI.OBJ_HIDE_LOG_L_2).gameObject.SetActive(value: false);
			}
			StampEditButtonObj.SetActive(value: false);
			FavStampEditButtonObj.SetActive(value: false);
			isFirstSlectClanTab = true;
		}
	}

	public void ShowInputOnly_NotOneShot()
	{
		ShowInputOnly();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			SetButtonEvent(UI.BTN_HIDE_LOG, new EventDelegate(ShowInputOnly_NotOneShot));
		});
		m_IsOnshotStampMode = false;
	}

	public void HideFull()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
		HideAll();
	}

	public void HideAll()
	{
		isMinimizable = false;
		logView.Close(delegate
		{
		});
		inputView.Close(delegate
		{
		});
		inputBG.Close(delegate
		{
		});
		bgBlack.Close(delegate
		{
		});
		CloseChannelInput();
		CloseChannelSelect();
		if (isEnableOpenButton)
		{
			chatOpenButton.Open(delegate
			{
			});
		}
		StopPostChatProcess();
		NotifyObservers(NOTIFY_FLAG.CLOSE_WINDOW);
		StampEditButtonObj.SetActive(value: false);
		FavStampEditButtonObj.SetActive(value: false);
		ChatCloseButtonObj.SetActive(value: false);
		if (m_msgUiCtrl != null)
		{
			m_msgUiCtrl.ClearList();
		}
	}

	private void HideBottomUI()
	{
		SwitchBottomUIActivation(_isVisible: true);
	}

	private void ShowBottomUI()
	{
		SwitchBottomUIActivation(_isVisible: false);
	}

	private void SwitchBottomUIActivation(bool _isVisible)
	{
		if (WidgetBot.gameObject.activeSelf != _isVisible)
		{
			WidgetBot.gameObject.SetActive(_isVisible);
		}
	}

	public void OnPressBackKey()
	{
		if (channelInputPanel.isOpened)
		{
			CloseChannelInput();
			channelSelect.Open(delegate
			{
			});
		}
		else if (channelSelect.isOpened)
		{
			CloseChannelSelect();
		}
		else if (stampFavoriteEdit != null && stampFavoriteEdit.gameObject.activeSelf)
		{
			stampFavoriteEdit.Close(update: false);
		}
		else if (stampAll != null && stampAll.gameObject.activeSelf)
		{
			stampAll.Close();
		}
		else if (GetTopState() != typeof(ChatState_FollowerListView))
		{
			HideAll();
		}
	}

	public void ShowOpenButton()
	{
		chatOpenButton.Open(delegate
		{
		});
		isEnableOpenButton = true;
		addObserver(this);
	}

	public void HideOpenButton()
	{
		chatOpenButton.Close(delegate
		{
		});
		isEnableOpenButton = false;
		RemoveObserver(this);
	}

	private bool IsNotConnected()
	{
		if (currentChat == CHAT_TYPE.HOME)
		{
			if (!MonoBehaviourSingleton<ChatManager>.IsValid() || MonoBehaviourSingleton<ChatManager>.I.currentChannel == null || MonoBehaviourSingleton<ChatManager>.I.currentChannel.channel < 0 || MonoBehaviourSingleton<ChatManager>.I.homeChat == null || !MonoBehaviourSingleton<ChatManager>.I.homeChat.HasConnect)
			{
				return true;
			}
		}
		else if (currentChat == CHAT_TYPE.ROOM)
		{
			if (MonoBehaviourSingleton<ChatManager>.I.roomChat == null || !MonoBehaviourSingleton<ChatManager>.I.roomChat.HasConnect)
			{
				return true;
			}
		}
		else if (currentChat == CHAT_TYPE.LOUNGE && (MonoBehaviourSingleton<ChatManager>.I.loungeChat == null || !MonoBehaviourSingleton<ChatManager>.I.loungeChat.HasConnect))
		{
			return true;
		}
		return false;
	}

	private bool IsSendLimit()
	{
		try
		{
			if (!MonoBehaviourSingleton<ChatManager>.IsValid())
			{
				return false;
			}
			ChatManager i = MonoBehaviourSingleton<ChatManager>.I;
			if (IsNullObject(i))
			{
				return false;
			}
			if (currentChat == CHAT_TYPE.HOME)
			{
				return i != null && !i.homeChat.CanSendMessage();
			}
			if (currentChat == CHAT_TYPE.ROOM)
			{
				return i.roomChat != null && !i.roomChat.CanSendMessage();
			}
			if (currentChat == CHAT_TYPE.LOUNGE)
			{
				return i.loungeChat != null && !i.loungeChat.CanSendMessage();
			}
			if (currentChat == CHAT_TYPE.CLAN)
			{
				return i.clanChat != null && !i.clanChat.CanSendMessage();
			}
			return false;
		}
		catch (Exception ex)
		{
			Log.Error((ex != null) ? ex.Message : "Unhandled exception!!");
			return false;
		}
	}

	private bool IsNullObject(object targetObj)
	{
		if (targetObj is UnityEngine.Object)
		{
			if ((UnityEngine.Object)targetObj != null)
			{
				return false;
			}
			return true;
		}
		if (targetObj != null)
		{
			return false;
		}
		return true;
	}

	public void UpdateSendBlock()
	{
		bool num = IsNotConnected();
		bool flag = !num && IsSendLimit();
		bool flag2 = (num | flag) || UseNoClanBlock || IsDraging;
		if (num)
		{
			noConnectionView.Open(delegate
			{
			});
		}
		else
		{
			noConnectionView.Close(delegate
			{
			});
		}
		if (flag)
		{
			sendLimitView.Open(delegate
			{
			});
		}
		else
		{
			sendLimitView.Close(delegate
			{
			});
		}
		if (UseNoClanBlock)
		{
			sendLimitNoClanView.Open(delegate
			{
			});
		}
		else
		{
			sendLimitNoClanView.Close(delegate
			{
			});
		}
		if (IsDraging)
		{
			sendLimitReloadView.Open(delegate
			{
			});
		}
		else
		{
			sendLimitReloadView.Close(delegate
			{
			});
		}
		if (flag2)
		{
			sendBlockView.Open(delegate
			{
			});
		}
		else
		{
			sendBlockView.Close(delegate
			{
			});
		}
	}

	private void GenerateHeaderButton()
	{
		if (WidgetTopHeader == null)
		{
			return;
		}
		Transform transform = WidgetTopHeader.transform;
		for (int i = 0; i < 4; i++)
		{
			ChatHeaderButtonController component = ResourceUtility.Realizes(Resources.Load(HEADER_BUTTON_PREFAB_PATH), transform).GetComponent<ChatHeaderButtonController>();
			if (!(component == null))
			{
				component.Hide();
				m_headerButtonList.Add(component);
			}
		}
		SetHeaderButtonPosition(IsPortrait);
	}

	private void SetHeaderButtonPosition(bool _isPortrait)
	{
		if (m_headerButtonList != null && m_headerButtonList.Count >= 1)
		{
			Vector3[] array = _isPortrait ? HEADER_BUTTON_PORTRAIT_POS : HEADER_BUTTON_LANDSCAPE_POS;
			int i = 0;
			for (int count = m_headerButtonList.Count; i < count; i++)
			{
				Transform transform = m_headerButtonList[i].transform;
				transform.localPosition = array[i];
				transform.localScale = Vector3.one;
			}
		}
	}

	private void InitChatTabState()
	{
		int num = -1;
		int num2 = -1;
		int num3 = -1;
		int num4 = -1;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		ChatHeaderButtonController.InitParam initParam = new ChatHeaderButtonController.InitParam();
		initParam.ButtonIndex = num8;
		initParam.ChatType = CHAT_TYPE.PERSONAL;
		initParam.OnSelectCallBack = OnSelectPersonalTab;
		m_headerButtonList[num8].Initialize(initParam);
		num8++;
		ChatHeaderButtonController.InitParam initParam2 = new ChatHeaderButtonController.InitParam();
		initParam2.ButtonIndex = num8;
		initParam2.ChatType = CHAT_TYPE.CLAN;
		initParam2.OnSelectCallBack = OnSelectClanTab;
		m_headerButtonList[num8].Initialize(initParam2);
		num = num8;
		num8++;
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			num5 = MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
		}
		if (HomeType == HOME_TYPE.HOME_TOP)
		{
			ChatHeaderButtonController.InitParam initParam3 = new ChatHeaderButtonController.InitParam();
			initParam3.ButtonIndex = num8;
			initParam3.ChatType = CHAT_TYPE.HOME;
			initParam3.OnSelectCallBack = OnSelectHomeTab;
			m_headerButtonList[num8].Initialize(initParam3);
			num2 = num8;
			num8++;
			if (m_PostRequetQueue[0] != null)
			{
				num6 = m_PostRequetQueue[0].Count;
			}
		}
		else if (HomeType == HOME_TYPE.LOUNGE_TOP)
		{
			ChatHeaderButtonController.InitParam initParam4 = new ChatHeaderButtonController.InitParam();
			initParam4.ButtonIndex = num8;
			initParam4.ChatType = CHAT_TYPE.LOUNGE;
			initParam4.OnSelectCallBack = OnSelectLounge;
			m_headerButtonList[num8].Initialize(initParam4);
			num3 = num8;
			num8++;
			if (m_PostRequetQueue[2] != null)
			{
				num7 = m_PostRequetQueue[2].Count;
			}
		}
		if (hasRoomChat)
		{
			ChatHeaderButtonController.InitParam initParam5 = new ChatHeaderButtonController.InitParam();
			initParam5.ButtonIndex = num8;
			initParam5.ChatType = ((!isFieldChat) ? CHAT_TYPE.ROOM : CHAT_TYPE.FIELD);
			initParam5.OnSelectCallBack = OnSelectRoomTab;
			m_headerButtonList[num8].Initialize(initParam5);
			num4 = num8;
			num8++;
		}
		for (int i = 0; i < m_headerButtonList.Count; i++)
		{
			if (i < num8)
			{
				m_headerButtonList[i].Show();
			}
			else
			{
				m_headerButtonList[i].Hide();
			}
		}
		int index = num8 - 1;
		if (num4 > 0)
		{
			index = num4;
		}
		else if (num5 > 0 && num > 0)
		{
			index = num;
		}
		else if (num6 > 0 && num2 > 0)
		{
			index = num2;
		}
		else if (num7 > 0 && num3 > 0)
		{
			index = num3;
		}
		else if (num3 > 0)
		{
			index = num3;
		}
		else if (num > 0 && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			index = num;
		}
		else if (num2 > 0)
		{
			index = num2;
		}
		m_headerButtonList[index].OnClick();
		if (m_msgUiCtrl == null)
		{
			ChatMessageUserUIController.InitParam initParam6 = new ChatMessageUserUIController.InitParam();
			initParam6.ItemListParent = PersonalMsgGrid.transform;
			initParam6.ItemVisibleCount = Mathf.FloorToInt(PersonalMsgScrollView.panel.height / PersonalMsgGrid.cellHeight);
			initParam6.OnClickItem = delegate
			{
				PushNextState(typeof(ChatState_PersonalMsgView));
			};
			m_msgUiCtrl = new ChatMessageUserUIController(initParam6);
			EventDelegate eventDelegate = new EventDelegate();
			eventDelegate.methodName = "OnClickShowUserListButton";
			eventDelegate.target = this;
			GetCtrl(UI.BTN_SHOW_USER_LIST).GetComponent<UIButton>().onClick.Add(eventDelegate);
		}
	}

	private void OnSelectHomeTab()
	{
		OnSelectHeaderTab(CHAT_TYPE.HOME);
		PushNextExclusiveState(typeof(ChatState_HomeTab));
	}

	private void OnSelectRoomTab()
	{
		OnSelectHeaderTab((!isFieldChat) ? CHAT_TYPE.ROOM : CHAT_TYPE.FIELD);
		PushNextExclusiveState(isFieldChat ? typeof(ChatState_FieldTab) : typeof(ChatState_RoomTab));
	}

	private void OnSelectLounge()
	{
		OnSelectHeaderTab(CHAT_TYPE.LOUNGE);
		PushNextExclusiveState(typeof(ChatState_LoungeTab));
	}

	private void OnSelectClanTab()
	{
		if (isFirstSlectClanTab)
		{
			m_DataList[5].Reset();
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatResetCache();
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
			isFirstSlectClanTab = false;
		}
		OnSelectHeaderTab(CHAT_TYPE.CLAN);
		PushNextExclusiveState(typeof(ChatState_ClanTab));
	}

	public void ResetCacheData(CHAT_TYPE chatType)
	{
		if (m_PostRequetQueue[(int)chatType] != null)
		{
			m_PostRequetQueue[(int)chatType].Clear();
		}
		if (m_DataList[(int)chatType] != null)
		{
			m_DataList[(int)chatType].Reset();
		}
	}

	private void OnSelectPersonalTab()
	{
		ResetOtherButtonSettings(CHAT_TYPE.PERSONAL);
		PushNextExclusiveState(typeof(ChatState_PersonalTab));
		if (!m_msgUiCtrl.IsConnecting)
		{
			PersonalMsgScrollView.ResetPosition();
			StartCoroutine(m_msgUiCtrl.SendRequestMessagingPersonList(this));
		}
	}

	private void OnSelectHeaderTab(CHAT_TYPE _t)
	{
		ResetOtherButtonSettings(_t);
		SaveSlideOffset();
		if (currentChat == _t)
		{
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
			{
				ScrollView.SetDragAmount(1f, 1f, updateScrollbars: true);
			});
		}
		currentChat = _t;
		StateMachine.CurrentState.OnTapHeaderTab(_t);
		UpdateWindowSize();
	}

	private void ResetOtherButtonSettings(CHAT_TYPE _t)
	{
		if (m_headerButtonList == null || m_headerButtonList.Count < 1)
		{
			return;
		}
		int i = 0;
		for (int count = m_headerButtonList.Count; i < count; i++)
		{
			if (m_headerButtonList[i].MyChatType != _t)
			{
				m_headerButtonList[i].UnSelect();
			}
		}
		bool active = _t == CHAT_TYPE.ROOM || _t == CHAT_TYPE.FIELD;
		bool flag = _t == CHAT_TYPE.HOME;
		bool flag2 = _t == CHAT_TYPE.LOUNGE;
		bool flag3 = _t == CHAT_TYPE.PERSONAL;
		bool flag4 = _t == CHAT_TYPE.CLAN;
		for (int j = 0; j < m_DataList.Length; j++)
		{
			if (m_DataList[j] != null)
			{
				if (j == 1 || j == 3)
				{
					m_DataList[j].rootObject.SetActive(active);
				}
				else
				{
					m_DataList[j].rootObject.SetActive(j == (int)_t);
				}
			}
		}
		SwitchBottomUIActivation(!flag3);
		if (PersonalMsgGridObj.activeSelf != flag3)
		{
			PersonalMsgGridObj.SetActive(flag3);
		}
		SubHeaderPanel.gameObject.SetActive(flag | flag3);
		SetActiveChannelSelect(flag);
		if (ShowUserListButtonObject.activeSelf != flag3)
		{
			ShowUserListButtonObject.SetActive(flag3);
		}
		SetScrollPanelUI(IsPortrait, _t);
		SetChatBgFrameUI(_t);
		bool flag5 = flag && !hasRoomChat;
		flag5 = (flag5 | flag2 | flag4);
		StampEditButtonObj.SetActive(flag5);
		FavStampEditButtonObj.SetActive(flag5);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (InputFrame != null)
		{
			InputFrame.UpdateAgeConfirm();
		}
	}

	protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return GameSection.NOTIFY_FLAG.UPDATE_USER_INFO;
	}

	private void ShowChannelSelect()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
		SetChannelSelectBG();
		channelSelect.Open(delegate
		{
		});
	}

	private void CloseChannelSelect()
	{
		ResetChannelSelectBG();
		channelSelect.Close(delegate
		{
		});
	}

	private void OnSelectHotChannel()
	{
		int hotChannel = MonoBehaviourSingleton<ChatManager>.I.GetHotChannel();
		OnSelectChannel(hotChannel);
	}

	private void OnSelectQuietChannel()
	{
		int coldChannel = MonoBehaviourSingleton<ChatManager>.I.GetColdChannel();
		OnSelectChannel(coldChannel);
	}

	private void OnSelectRecommendedChannel()
	{
		int recommendedChannel = MonoBehaviourSingleton<ChatManager>.I.GetRecommendedChannel();
		OnSelectChannel(recommendedChannel);
	}

	private void OnSelectInputChannelNumber()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
		ShowChannelInput();
	}

	private void OnSelectShowAll()
	{
		if (stampAll == null)
		{
			stampAll = GetCtrl(UI.OBJ_STAMP_ALL).GetComponent<ChatStampAll>();
		}
		stampAll.Open();
	}

	private void OnSelectEdit()
	{
		if (stampFavoriteEdit == null)
		{
			stampFavoriteEdit = GetCtrl(UI.OBJ_STAMP_FAVORITE_EDIT).GetComponent<ChatStampFavoriteEdit>();
		}
		stampFavoriteEdit.Open();
	}

	private void ShowChannelInput()
	{
		channelSelect.Close(delegate
		{
		});
		channelInputPanel.Open();
	}

	private void CloseChannelInput()
	{
		channelInputPanel.Close();
	}

	private void OnInputChannel(int number)
	{
		List<int> channels = MonoBehaviourSingleton<ChatManager>.I.GetChannels();
		int num = 0;
		int i = 0;
		for (int count = channels.Count; i < count; i++)
		{
			if (channels[i] == number)
			{
				num = channels[i];
			}
		}
		if (num > 0)
		{
			OnSelectChannel(num);
			CloseChannelInput();
		}
		else
		{
			OnError(StringTable.Format(STRING_CATEGORY.CHAT_ERROR, 0u, number.ToString(CHANNEL_FORMAT)));
		}
	}

	private void OnSelectChannel(int channel)
	{
		SoundManager.PlaySystemSE(SoundID.UISE.OK);
		MonoBehaviourSingleton<ChatManager>.I.SelectChannel(channel);
		UpdateChannnelName(channel);
		CloseChannelSelect();
	}

	private void SetChannelSelectBG()
	{
		if (SpecialDeviceManager.HasSpecialDeviceInfo)
		{
			UIVirtualScreen componentInChildren = GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren != null)
			{
				SpriteBgBlack.width = (int)componentInChildren.ScreenWidthFull;
				SpriteBgBlack.height = (int)componentInChildren.ScreenHeightFull;
			}
		}
		SetParent(UI.SPR_BG_BLACK, UI.OBJ_CHANNEL_SELECT_BG);
		SpriteBgBlack.ParentHasChanged();
	}

	private void ResetChannelSelectBG()
	{
		if (SpecialDeviceManager.HasSpecialDeviceInfo)
		{
			UIVirtualScreen componentInChildren = GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren != null)
			{
				SpriteBgBlack.width = (int)componentInChildren.ScreenWidthFull;
				SpriteBgBlack.height = (int)componentInChildren.ScreenHeightFull;
			}
		}
		SetParent(UI.SPR_BG_BLACK, GetCtrl(UI.BTN_OPEN).parent);
		SpriteBgBlack.ParentHasChanged();
	}

	private void OnError(string message)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, message, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u)), delegate
		{
		}, error: true);
	}

	public void onStoppedMovingScrollView()
	{
	}

	public void onPressStartScrollView()
	{
		tmpOffsetStart = ScrollView.panel.clipOffset.y;
	}

	public void onPressEndScrollView()
	{
	}

	public void onDragStartScrollView()
	{
		dragTime = Time.time;
		IsDraging = true;
		UpdateSendBlock();
	}

	public void onDragEndScrollView()
	{
		IsDraging = false;
		UpdateSendBlock();
		if (ScrollView.panel.clipOffset.y - tmpOffsetStart < 0f)
		{
			if (CurrentData.itemList.Count > 0)
			{
				if (CurrentData.itemList.Count > CurrentData.newestIndex && CurrentData.itemList[CurrentData.newestIndex] != null && CurrentData.itemList[CurrentData.newestIndex].gameObject.activeSelf && StateMachine.IsRun())
				{
					StateMachine.CurrentState.OnDragAtBottom(CurrentData.itemList[CurrentData.newestIndex].chatItemId, Time.time - dragTime);
				}
			}
			else if (StateMachine.IsRun())
			{
				StateMachine.CurrentState.OnDragAtBottom("", Time.time - dragTime);
			}
		}
		else if (CurrentData.itemList.Count > 0)
		{
			if (CurrentData.itemList.Count > CurrentData.oldestItemIndex && CurrentData.itemList[CurrentData.oldestItemIndex] != null && CurrentData.itemList[CurrentData.oldestItemIndex].gameObject.activeSelf && StateMachine.IsRun())
			{
				StateMachine.CurrentState.OnDragAtTop(CurrentData.itemList[CurrentData.oldestItemIndex].chatItemId, Time.time - dragTime);
			}
		}
		else if (StateMachine.IsRun())
		{
			StateMachine.CurrentState.OnDragAtTop("", Time.time - dragTime);
		}
	}

	private void InitStampList()
	{
		if (m_StampIdListCanPost == null)
		{
			ResetStampIdList();
		}
		UpdateStampList();
	}

	public void UpdateStampList()
	{
		int item_num = m_StampIdListCanPost.Count + 10 + (IsLandScapeFullViewMode ? 2 : 0);
		SetGrid(UI.GRD_STAMP_LIST, null, item_num, reset: true, CreateStampItem, InitStampItem);
	}

	public void ResetStampIdList()
	{
		if (m_StampIdListCanPost == null)
		{
			m_StampIdListCanPost = new List<int>();
		}
		m_StampIdListCanPost.Clear();
		if (Singleton<StampTable>.IsValid() && Singleton<StampTable>.I.table != null)
		{
			Singleton<StampTable>.I.table.ForEach(delegate(StampTable.Data stamp_data)
			{
				int id = (int)stamp_data.id;
				if (CanIPostTheStamp(id))
				{
					m_StampIdListCanPost.Add(id);
				}
			});
		}
	}

	public void OnUpdateUnlockStampList()
	{
		m_StampIdListCanPost = null;
		InitStampList();
	}

	public bool IsValidStampId(int stampId)
	{
		if (stampId < 1)
		{
			return false;
		}
		if (Singleton<StampTable>.I.GetData((uint)stampId) == null)
		{
			return false;
		}
		return true;
	}

	public bool CanIPostTheStamp(int id)
	{
		if (!IsValidStampId(id))
		{
			return false;
		}
		if (Singleton<StampTable>.I.GetData((uint)id).type == STAMP_TYPE.COMMON)
		{
			return true;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(id))
		{
			return false;
		}
		return IsValidStampId(id);
	}

	private Transform CreateStampItem(int index, Transform parent)
	{
		Transform transform = ResourceUtility.Realizes(m_ChatStampListPrefab, 5);
		transform.parent = parent;
		transform.localScale = Vector3.one;
		return transform;
	}

	private void InitStampItem(int index, Transform iTransform, bool isRecycle)
	{
		if (m_StampIdListCanPost == null)
		{
			return;
		}
		bool flag = false;
		int num = index;
		if (index < 10)
		{
			num = MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index];
		}
		else if (IsLandScapeFullViewMode)
		{
			if (index < 12)
			{
				num = 1;
				flag = true;
			}
			else
			{
				num = m_StampIdListCanPost[index - 12];
			}
		}
		else
		{
			num = m_StampIdListCanPost[index - 10];
		}
		ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
		item.Init(num);
		if (flag)
		{
			item.SetAsDummy();
		}
		if (!isRecycle && !flag)
		{
			ChatStampListItem chatStampListItem = item;
			chatStampListItem.onButton = (Action)Delegate.Combine(chatStampListItem.onButton, (Action)delegate
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.SendStampAsMine(item.StampId);
			});
		}
	}

	private void UpdateDummyDragScroll()
	{
		if (ScrollView.panel.height > CurrentTotalHeight)
		{
			DummyDragScroll.height = (int)(ScrollView.panel.height - 20f);
		}
		else
		{
			DummyDragScroll.height = (int)(CurrentTotalHeight - 20f);
		}
		DragScrollTrans.localPosition = new Vector3(ScrollView.panel.clipOffset.x, 0f - CurrentTotalHeight, 0f);
		DragScrollCollider.size = new Vector3(ScrollView.panel.finalClipRegion.z, ScrollView.panel.finalClipRegion.w - ScrollView.panel.clipSoftness.y * 2f, 0f);
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		UpdateObserve();
		UpdateStateMachine(deltaTime);
		if (base.state == STATE.OPEN)
		{
			if (m_handlPostChatPorcess != null && !m_handlPostChatPorcess.MoveNext())
			{
				m_handlPostChatPorcess = null;
			}
			if (sendBlockView.isOpened)
			{
				UpdateSendBlock();
			}
			bool isOpened = logView.isOpened;
			DragScrollCollider.enabled = isOpened;
			if (isOpened)
			{
				float num = ScrollView.panel.baseClipRegion.w - ScrollView.panel.baseClipRegion.y + DragScrollTrans.localPosition.y - (ScrollView.panel.finalClipRegion.w + ScrollView.panel.clipOffset.y);
				DragScrollCollider.center = new Vector2(ScrollView.panel.baseClipRegion.x, 0f - num);
			}
		}
	}

	private void LateUpdate()
	{
		if (logView != null && (logView.isOpened || logView.isOpening))
		{
			UpdateWidgetVisibility();
		}
	}

	private void UpdateWidgetVisibility()
	{
		List<ChatItem> itemList = CurrentData.itemList;
		int i = 0;
		for (int count = itemList.Count; i < count; i++)
		{
			UIWidget widget = itemList[i].widget;
			if (!IsVisible(itemList[i]))
			{
				widget.gameObject.SetActive(value: false);
			}
			else if (!widget.gameObject.activeSelf)
			{
				widget.gameObject.SetActive(value: true);
			}
		}
	}

	private bool IsVisible(ChatItem chatItem)
	{
		float y = chatItem.transform.localPosition.y;
		float num = chatItem.transform.localPosition.y - chatItem.height;
		Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
		float num2 = finalClipRegion.w * 0.5f;
		if (finalClipRegion.y + num2 > num)
		{
			return finalClipRegion.y - num2 < y;
		}
		return false;
	}

	public override void OnModifyChat(NOTIFY_FLAG flag)
	{
		if ((flag & NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
		{
			SetBadge(UI.BTN_OPEN, GetPendingQueueNum(), SpriteAlignment.TopLeft, 7, -9);
		}
	}

	private void UpdateObserve()
	{
		int pendingQueueNum = GetPendingQueueNum();
		if (m_LastPendingQueueCount != pendingQueueNum)
		{
			NotifyObservers(NOTIFY_FLAG.ARRIVED_MESSAGE);
		}
		m_LastPendingQueueCount = pendingQueueNum;
	}

	private void NotifyObservers(NOTIFY_FLAG note)
	{
		if (m_Observers != null)
		{
			m_Observers.ForEach(delegate(UIBehaviour x)
			{
				if (x != null)
				{
					x.OnModifyChat(note);
				}
			});
		}
	}

	public void addObserver(UIBehaviour observer)
	{
		if (!(observer == null) && !m_Observers.Contains(observer))
		{
			m_Observers.Add(observer);
		}
	}

	public void RemoveObserver(UIBehaviour observer)
	{
		if (m_Observers.Contains(observer))
		{
			m_Observers.Remove(observer);
		}
	}

	protected override void OnDestroy()
	{
		if (AppMain.isApplicationQuit)
		{
			return;
		}
		base.OnDestroy();
		int i = 0;
		for (int num = m_DataList.Length; i < num; i++)
		{
			if (m_DataList[i] != null)
			{
				m_DataList[i].Reset();
			}
		}
	}

	public void SetActiveChannelSelect(bool active)
	{
		if (active != ChannelSelectSpriteButtonObject.activeSelf)
		{
			ChannelSelectSpriteButtonObject.SetActive(active);
		}
	}

	private void InitStateMachine()
	{
		m_stateMachine = new ChatStateMachine<ChatState>();
		StateMachine.Initialize(this);
		StateMachine.AddListener(OnChangeState);
		StateMachine.Start(typeof(ChatState_Init));
	}

	private void UpdateStateMachine(float _deltaTime)
	{
		if (StateMachine != null && StateMachine.IsRun())
		{
			StateMachine.Update(_deltaTime);
		}
	}

	public void ExecCoroutine(IEnumerator _ienumarator)
	{
		StartCoroutine(_ienumarator);
	}

	private void OnChangeState(Type currentType, Type prevType)
	{
	}

	public Type GetCurrentStateType()
	{
		if (StateMachine != null)
		{
			return StateMachine.CurrentStateType;
		}
		return null;
	}

	public Type GetPrevStateType()
	{
		if (StateMachine != null)
		{
			return StateMachine.PrevStateType;
		}
		return null;
	}

	public void PushNextState(Type _s)
	{
		Type currentStateType = GetCurrentStateType();
		if (!(currentStateType != null) || !(currentStateType == _s))
		{
			m_stateStack.Push(_s);
		}
	}

	public void PushNextExclusiveState(Type _s)
	{
		Type currentStateType = GetCurrentStateType();
		if (!(currentStateType != null) || !(currentStateType == _s))
		{
			if (m_stateStack.Count > 0)
			{
				PopState();
			}
			PushNextState(_s);
		}
	}

	public Type GetTopState()
	{
		if (m_stateStack.Count <= 0)
		{
			return null;
		}
		return m_stateStack.Peek();
	}

	public Type PopState()
	{
		if (m_stateStack.Count < 1)
		{
			return null;
		}
		return m_stateStack.Pop();
	}

	public bool HasState(Type _t)
	{
		return m_stateStack.Contains(_t);
	}

	public void OnClickShowUserListButton()
	{
		PushNextState(typeof(ChatState_FollowerListView));
	}
}
