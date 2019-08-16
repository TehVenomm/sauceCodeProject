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
				Object.DestroyImmediate(itemList[i].get_gameObject());
			}
			itemList.Clear();
			Init();
		}

		public void MoveAll(float y)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			Vector3 localPosition = itemList[0].get_transform().get_localPosition();
			int i = 0;
			for (int count = itemList.Count; i < count; i++)
			{
				Transform transform = itemList[i].get_transform();
				Vector3 localPosition2 = transform.get_localPosition();
				localPosition.y = localPosition2.y + y;
				transform.set_localPosition(localPosition);
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

		public string chatItemId = string.Empty;

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

	private readonly Vector3[] HEADER_BUTTON_PORTRAIT_POS = (Vector3[])new Vector3[4]
	{
		new Vector3(-217f, 33.4f, -0f),
		new Vector3(-60f, 33.4f, -0f),
		new Vector3(45f, 33.4f, -0f),
		new Vector3(209f, 33f, -0f)
	};

	private readonly Vector3[] HEADER_BUTTON_LANDSCAPE_POS = (Vector3[])new Vector3[4]
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

	private Transform ScrollViewTrans => m_ScrollViewTrans ?? (m_ScrollViewTrans = ScrollView.get_transform());

	private SpringPanel ScrollViewSpring => m_ScrollViewSpring ?? (m_ScrollViewSpring = GetCtrl(UI.SCR_CHAT).GetComponent<SpringPanel>());

	private UIWidget DummyDragScroll => m_DummyDragScroll ?? (m_DummyDragScroll = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<UIWidget>());

	private BoxCollider DragScrollCollider => m_DragScrollCollider ?? (m_DragScrollCollider = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<BoxCollider>());

	private Transform DragScrollTrans => m_DragScrollTrans ?? (m_DragScrollTrans = DragScrollCollider.get_transform());

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

	private GameObject ChatCloseButtonObj => m_chatCloseButtonObj ?? (m_chatCloseButtonObj = GetCtrl(UI.BTN_INPUT_CLOSE).get_gameObject());

	private GameObject StampEditButtonObj => m_stampEditButtonObj ?? (m_stampEditButtonObj = GetCtrl(UI.BTN_EDIT).get_gameObject());

	private GameObject FavStampEditButtonObj => m_favStampEditButtonObj ?? (m_favStampEditButtonObj = GetCtrl(UI.BTN_SHOW_ALL).get_gameObject());

	private UIWidget WidgetTop => m_widgetTop ?? (m_widgetTop = GetCtrl(UI.WGT_ANCHOR_TOP).GetComponent<UIWidget>());

	private UIWidget WidgetTopHeader => m_widgetTopHeader ?? (m_widgetTopHeader = GetCtrl(UI.WGT_HEADER_SPACE).GetComponent<UIWidget>());

	private UISprite BackgroundInFrame => m_BackgroundInFrame ?? (m_BackgroundInFrame = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UISprite>());

	private UIPanel SubHeaderPanel => m_subHeaderPanel ?? (m_subHeaderPanel = GetCtrl(UI.WGT_SUB_HEADER_SPACE).GetComponent<UIPanel>());

	private GameObject ChannelSelectSpriteButtonObject => m_channelSelectSpriteButtonObject ?? (m_channelSelectSpriteButtonObject = GetCtrl(UI.BTN_SPR_CHANNEL_SELECT).get_gameObject());

	private UILabel ChannelName => m_ChannelName ?? (m_ChannelName = GetCtrl(UI.LBL_CHANNEL_NAME).GetComponent<UILabel>());

	private GameObject ShowUserListButtonObject => m_showUserListButtonObject ?? (m_showUserListButtonObject = GetCtrl(UI.BTN_SHOW_USER_LIST).get_gameObject());

	private UIScrollView PersonalMsgScrollView => m_personalMsgScrollView ?? (m_personalMsgScrollView = GetCtrl(UI.SCR_PERSONAL_MSG_LIST_VIEW).GetComponent<UIScrollView>());

	private UIGrid PersonalMsgGrid => m_personalMsgGrid ?? (m_personalMsgGrid = GetCtrl(UI.GRD_PERSONAL_MSG_VIEW).GetComponent<UIGrid>());

	private GameObject PersonalMsgGridObj => m_personalMsgGridObj ?? (m_personalMsgGridObj = PersonalMsgGrid.get_gameObject());

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

	private bool IsLandScapeFullViewMode => IsShowFullChatView && !IsPortrait;

	public ChatStateMachine<ChatState> StateMachine => m_stateMachine;

	private float CurrentTotalHeight => (CurrentData == null) ? 0f : CurrentData.currentTotalHeight;

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
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		m_isPortrait = is_portrait;
		InitStampList();
		SetBaseWidgetSettings();
		SetParent(UI.BTN_HIDE_LOG, (!is_portrait) ? UI.OBJ_HIDE_LOG_L_2 : UI.OBJ_HIDE_LOG_P);
		SetHeaderButtonPosition(is_portrait);
		SetStampWindowSettings();
		SetChatBgFrameUI(currentChat);
		GetCtrl(UI.OBJ_CHANNEL_INPUT).set_localScale((!is_portrait) ? (Vector3.get_one() * 0.75f) : Vector3.get_one());
		WidgetChatRoot.bottomAnchor.Set(0f, (!is_portrait) ? 0f : 72f);
		WidgetChatRoot.topAnchor.Set(1f, (!is_portrait) ? (-4f) : (-72f));
		SetScrollPanelUI(is_portrait, currentChat);
		float num = 1.17279065f;
		SpriteBgBlack.get_transform().set_localScale((!is_portrait) ? (Vector3.get_one() * num) : Vector3.get_one());
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
				GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(false);
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
		int num = (!IsLandScapeFullViewMode) ? 5 : 4;
		int num2 = (!IsLandScapeFullViewMode) ? 2 : 3;
		StampScrollGrid.maxPerLine = num;
		StampScrollGrid.set_enabled(true);
		float num3 = StampScrollGrid.cellWidth * (float)num / 2f;
		float cellHeight = StampScrollGrid.cellHeight;
		StampChatFrame.leftAnchor.Set(0.5f, 0f - num3);
		StampChatFrame.rightAnchor.Set(0.5f, num3);
		StampChatFrame.topAnchor.Set(0.5f, cellHeight * 0.5f);
		StampChatFrame.bottomAnchor.Set(0.5f, (0f - StampScrollGrid.cellHeight) * ((float)num2 - 0.5f));
	}

	private void SetBaseWidgetSettings()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = (!IsLandScapeFullViewMode) ? WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS : WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS;
		float num = (!IsLandScapeFullViewMode) ? 0.5f : 0f;
		WidgetTop.leftAnchor.Set((!IsLandScapeFullViewMode) ? 0.5f : 0f, val.x);
		WidgetTop.rightAnchor.Set((!IsLandScapeFullViewMode) ? 0.5f : 0f, val.y);
		WidgetTop.bottomAnchor.Set((!IsLandScapeFullViewMode) ? 0.5f : 0f, val.z);
		WidgetTop.topAnchor.Set((!IsLandScapeFullViewMode) ? 0.5f : 1f, val.w);
		val = (IsPortrait ? WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS : ((!IsShowFullChatView) ? WIDGET_ANCHOR_BOT_LANDSCAPE_SETTINGS : WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS));
		WidgetBot.leftAnchor.Set((!IsPortrait) ? 1f : 0.5f, val.x);
		WidgetBot.rightAnchor.Set((!IsPortrait) ? 1f : 0.5f, val.y);
		WidgetBot.bottomAnchor.Set(0f, val.z);
		WidgetBot.topAnchor.Set(0f, val.w);
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
			ScrollPanel.bottomAnchor.Set(0f, (!_isPortrait) ? 22f : 316f);
			break;
		case CHAT_TYPE.PERSONAL:
			ScrollPanel.topAnchor.Set(1f, -129f);
			ScrollPanel.bottomAnchor.Set(0f, (!_isPortrait) ? 22f : 15f);
			break;
		default:
			ScrollPanel.topAnchor.Set(1f, -72f);
			ScrollPanel.bottomAnchor.Set(0f, (!_isPortrait) ? 22f : 316f);
			break;
		}
		ScrollPanel.SetDirty();
	}

	private void SetChatBgFrameUI(CHAT_TYPE _t)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (!(BackgroundInFrame == null))
		{
			Vector4 val = (_t == CHAT_TYPE.PERSONAL || HasState(typeof(ChatState_PersonalTab)) || IsLandScapeFullViewMode) ? UI_SPRITE_CHAT_BG_FRAME_PERSONAL_POS : UI_SPRITE_CHAT_BG_FRAME_DEFAULT_POS;
			BackgroundInFrame.leftAnchor.Set(0f, val.x);
			BackgroundInFrame.rightAnchor.Set(1f, val.y);
			BackgroundInFrame.bottomAnchor.Set(0f, val.z);
			BackgroundInFrame.topAnchor.Set(1f, val.w);
		}
	}

	private void SetParent(UI changeTarget, UI parent)
	{
		Transform ctrl = GetCtrl(parent);
		SetParent(changeTarget, ctrl);
	}

	private void SetParent(UI changeTarget, Transform parent)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(changeTarget);
		ctrl.set_parent(parent);
		ctrl.set_localPosition(Vector3.get_zero());
	}

	private void UpdateCloseButtonPosition()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		UIWidget widgetInputCloseButton = WidgetInputCloseButton;
		if ((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestAcceptRoom")
		{
			UIRect.AnchorPoint leftAnchor = widgetInputCloseButton.leftAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_LANDSCAPE_POS = UI_BTN_INPUT_CLOSE_LANDSCAPE_POS;
			leftAnchor.Set(1f, uI_BTN_INPUT_CLOSE_LANDSCAPE_POS.x);
			UIRect.AnchorPoint rightAnchor = widgetInputCloseButton.rightAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_LANDSCAPE_POS2 = UI_BTN_INPUT_CLOSE_LANDSCAPE_POS;
			rightAnchor.Set(1f, uI_BTN_INPUT_CLOSE_LANDSCAPE_POS2.y);
			UIRect.AnchorPoint bottomAnchor = widgetInputCloseButton.bottomAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_LANDSCAPE_POS3 = UI_BTN_INPUT_CLOSE_LANDSCAPE_POS;
			bottomAnchor.Set(0f, uI_BTN_INPUT_CLOSE_LANDSCAPE_POS3.z);
			UIRect.AnchorPoint topAnchor = widgetInputCloseButton.topAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_LANDSCAPE_POS4 = UI_BTN_INPUT_CLOSE_LANDSCAPE_POS;
			topAnchor.Set(0f, uI_BTN_INPUT_CLOSE_LANDSCAPE_POS4.w);
		}
		else
		{
			UIRect.AnchorPoint leftAnchor2 = widgetInputCloseButton.leftAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_DEFAULT_POS = UI_BTN_INPUT_CLOSE_DEFAULT_POS;
			leftAnchor2.Set(1f, uI_BTN_INPUT_CLOSE_DEFAULT_POS.x);
			UIRect.AnchorPoint rightAnchor2 = widgetInputCloseButton.rightAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_DEFAULT_POS2 = UI_BTN_INPUT_CLOSE_DEFAULT_POS;
			rightAnchor2.Set(1f, uI_BTN_INPUT_CLOSE_DEFAULT_POS2.y);
			UIRect.AnchorPoint bottomAnchor2 = widgetInputCloseButton.bottomAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_DEFAULT_POS3 = UI_BTN_INPUT_CLOSE_DEFAULT_POS;
			bottomAnchor2.Set(1f, uI_BTN_INPUT_CLOSE_DEFAULT_POS3.z);
			UIRect.AnchorPoint topAnchor2 = widgetInputCloseButton.topAnchor;
			Vector4 uI_BTN_INPUT_CLOSE_DEFAULT_POS4 = UI_BTN_INPUT_CLOSE_DEFAULT_POS;
			topAnchor2.Set(1f, uI_BTN_INPUT_CLOSE_DEFAULT_POS4.w);
		}
	}

	private IEnumerator Start()
	{
		Initialize();
		yield return null;
		InitStateMachine();
		yield return null;
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatItem");
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
		LoadObject lo_chatAdvisaryItem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem");
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
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
		m_DataList[0] = new ChatItemListData(GetCtrl(UI.OBJ_HOME_ITEM_LIST_ROOT).get_gameObject());
		m_DataList[1] = new ChatItemListData(GetCtrl(UI.OBJ_ROOM_ITEM_LIST_ROOT).get_gameObject());
		m_DataList[2] = new ChatItemListData(GetCtrl(UI.OBJ_LOUNGE_ITEM_LIST_ROOT).get_gameObject());
		m_DataList[3] = m_DataList[1];
		m_DataList[4] = null;
		m_DataList[5] = new ChatItemListData(GetCtrl(UI.OBJ_CLAN_ITEM_LIST_ROOT).get_gameObject());
	}

	private void ChangeSliderPos(CHAT_TYPE type)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		SLIDER_OPEN_POS = ((type != 0) ? ROOM_SLIDER_OPEN_POS : HOME_SLIDER_OPEN_POS);
		SLIDER_CLOSE_POS = ((type != 0) ? ROOM_SLIDER_CLOSE_POS : HOME_SLIDER_CLOSE_POS);
	}

	private void SetSliderLimit()
	{
		UIPanel component = GetCtrl(UI.WGT_SLIDE_LIMIT).GetComponent<UIPanel>();
		component.topAnchor.absolute = (int)SLIDER_CLOSE_POS.y + 9;
		component.bottomAnchor.absolute = (int)SLIDER_OPEN_POS.y - 49;
	}

	private void Reset()
	{
		Input.value = string.Empty;
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
		return inputView.isOpened || inputView.isOpening;
	}

	public void OnTouchPost()
	{
		if (UserInfoManager.IsRegisterdAge() && UserInfoManager.IsEnableCommunication())
		{
			string value = Input.value;
			if (!string.IsNullOrEmpty(value) && value.Trim().Length != 0)
			{
				SendMessageAsMine(value);
				Input.value = string.Empty;
				OnInput();
			}
		}
	}

	public void OnInput()
	{
		InputFrame.FrameResize();
		string value = Input.value;
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(value));
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
			int queueIndex = GetQueueArrayIndex(currentChat);
			if (m_PostRequetQueue[queueIndex].Count > 0)
			{
				ChatPostRequest request = m_PostRequetQueue[queueIndex].Dequeue();
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
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		if (data.itemList.Count > 0)
		{
			data.currentTotalHeight += 22f;
		}
		float num = 0f;
		ChatItem chatItem = null;
		if (data.itemList.Count < 30)
		{
			chatItem = ResourceUtility.Realizes(m_ChatItemPrefab, data.rootObject.get_transform(), 5).GetComponent<ChatItem>();
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
		chatItem.get_transform().set_localPosition(CHAT_ITEM_OFFSET_POS + Vector3.get_down() * currentTotalHeight);
		initializer(chatItem);
		data.currentTotalHeight += chatItem.height;
		UpdateDummyDragScroll();
		float num2 = (!IsPortrait) ? 344 : 300;
		float num3 = data.currentTotalHeight - num2;
		Vector3 localPosition = ScrollViewTrans.get_localPosition();
		if (num3 < localPosition.y)
		{
			float currentTotalHeight2 = data.currentTotalHeight;
			Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
			float num4 = currentTotalHeight2 + baseClipRegion.y;
			Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
			float num5 = num4 - baseClipRegion2.w * 0.5f;
			Vector3 localPosition2 = ScrollViewTrans.get_localPosition();
			float y = localPosition2.y;
			Vector2 clipOffset = ScrollView.panel.clipOffset;
			float num6 = num5 + (y + clipOffset.y);
			Vector2 clipSoftness = ScrollView.panel.clipSoftness;
			float num7 = num6 + clipSoftness.y;
			if (data.itemList.Count >= 30)
			{
				ForceScroll(num7 - chatItem.height - 22f, useSpring: false);
			}
			ForceScroll(num7, useSpring: true);
		}
		else if (data.itemList.Count >= 30)
		{
			Vector3 localPosition3 = ScrollViewTrans.get_localPosition();
			if (localPosition3.y > num2)
			{
				Vector3 localPosition4 = ScrollViewTrans.get_localPosition();
				ForceScroll(localPosition4.y - num, useSpring: false);
			}
		}
		if (data.itemList.Count < 30)
		{
			data.itemList.Add(chatItem);
		}
	}

	private void addPrevChatItem(ChatItemListData data, Action<ChatItem> initializer)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		ScrollView.panel.widgetsAreStatic = false;
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			ScrollView.panel.widgetsAreStatic = true;
		});
		ChatItem chatItem = null;
		if (data.itemList.Count < 30)
		{
			chatItem = ResourceUtility.Realizes(m_ChatItemPrefab, data.rootObject.get_transform(), 5).GetComponent<ChatItem>();
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
		Vector3 val = CHAT_ITEM_OFFSET_POS;
		data.currentTotalHeight = 0f;
		for (int j = 0; j < data.itemList.Count; j++)
		{
			if (j > 0)
			{
				data.currentTotalHeight += 22f;
			}
			ChatItem chatItem2 = data.itemList[j];
			chatItem2.get_transform().set_localPosition(val);
			data.currentTotalHeight += chatItem2.height;
			val += Vector3.get_down() * chatItem2.height;
			val += Vector3.get_down() * 22f;
		}
		UpdateDummyDragScroll();
	}

	private void ForceScroll(float newHeight, bool useSpring)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		ScrollView.DisableSpring();
		if (useSpring)
		{
			SpringPanel.Begin(ScrollView.get_gameObject(), Vector3.get_up() * newHeight, 20f);
			return;
		}
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		Vector3 localPosition = ScrollViewTrans.get_localPosition();
		float num = localPosition.y + clipOffset.y;
		ScrollViewTrans.set_localPosition(Vector3.get_up() * newHeight);
		clipOffset.y = 0f - newHeight + num;
		ScrollView.panel.clipOffset = clipOffset;
	}

	public void SaveSlideOffset()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		ChatItemListData currentData = CurrentData;
		Vector3 localPosition = ScrollViewTrans.get_localPosition();
		currentData.slideOffset = localPosition.y + ScrollView.panel.height;
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
		SetLabelText((Enum)UI.LBL_CHANNEL, StringTable.Get(STRING_CATEGORY.CHAT, 0u));
		SetLabelText((Enum)UI.LBL_CHAT_HOME, StringTable.Get(STRING_CATEGORY.CHAT, 1u));
		SetLabelText((Enum)UI.LBL_NO_CONNECTION, StringTable.Get(STRING_CATEGORY.CHAT, 4u));
		SetLabelText((Enum)UI.LBL_POST_LIMIT, StringTable.Get(STRING_CATEGORY.CHAT, 5u));
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
		if (Object.op_Implicit(ctrl))
		{
			UIButton component = ctrl.GetComponent<UIButton>();
			if (Object.op_Implicit(component))
			{
				return component;
			}
		}
		return null;
	}

	private UILabel GetLabel(UI elm)
	{
		Transform ctrl = GetCtrl(elm);
		if (Object.op_Implicit(ctrl))
		{
			UILabel component = ctrl.GetComponent<UILabel>();
			if (Object.op_Implicit(component))
			{
				return component;
			}
		}
		return null;
	}

	private void SetButtonEvent(UI elm, EventDelegate eventDelegate)
	{
		Transform ctrl = GetCtrl(elm);
		if (Object.op_Implicit(ctrl))
		{
			UIButton component = ctrl.GetComponent<UIButton>();
			if (Object.op_Implicit(component))
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
		if (CurrentData.itemList.Count > 0 && CurrentData.itemList.Count > CurrentData.newestIndex && CurrentData.itemList[CurrentData.newestIndex] != null && CurrentData.itemList[CurrentData.newestIndex].get_gameObject().get_activeSelf())
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
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, string.Empty);
		}
	}

	private ChatUIFadeGroup CreateChatUIFadeGroup(UI elm)
	{
		Transform ctrl = GetCtrl(elm);
		UIRect root = null;
		if (Object.op_Implicit(ctrl))
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<UserInfoManager>.I.advisory != null && !GuildChatAdvisoryItem.HasReadHomeNew())
		{
			Vector3 localPosition = (!is_portrait) ? new Vector3(0f, 215f, 0f) : new Vector3(0f, 280f, 0f);
			if (chatAdvisoryItem == null)
			{
				chatAdvisoryItem = ResourceUtility.Realizes(m_ChatAdvisaryItemPrefab, GetCtrl(UI.WGT_CHAT_TOP), 5).GetComponent<GuildChatAdvisoryItem>();
				chatAdvisoryItem.get_transform().set_localPosition(localPosition);
				chatAdvisoryItem.Init(MonoBehaviourSingleton<UserInfoManager>.I.advisory.title, MonoBehaviourSingleton<UserInfoManager>.I.advisory.content);
				SetButtonEvent(chatAdvisoryItem.close, new EventDelegate(delegate
				{
					GuildChatAdvisoryItem.SetReadHomeNew();
					if (chatAdvisoryItem != null)
					{
						Object.DestroyImmediate(chatAdvisoryItem.get_gameObject());
						chatAdvisoryItem = null;
					}
				}));
			}
			else
			{
				chatAdvisoryItem.get_gameObject().SetActive(true);
				chatAdvisoryItem.get_transform().set_localPosition(localPosition);
			}
		}
		else if (chatAdvisoryItem != null)
		{
			Object.DestroyImmediate(chatAdvisoryItem.get_gameObject());
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
		StampEditButtonObj.SetActive(flag || flag2 || flag3);
		FavStampEditButtonObj.SetActive(flag || flag2 || flag3);
		ShowFull();
	}

	public void ShowFull()
	{
		ChatCloseButtonObj.SetActive(true);
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
		GetCtrl(UI.BTN_HIDE_LOG).get_gameObject().SetActive(isMinimizable);
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
			GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(flag);
			GetCtrl(UI.OBJ_HIDE_LOG_L_2).get_gameObject().SetActive(true);
		}
		SpriteBgBlack.ResizeCollider();
		isFirstSlectClanTab = true;
	}

	public void ShowInputOnly()
	{
		ChatCloseButtonObj.SetActive(true);
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
			GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(true);
			bool is_portrait = MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
			OnScreenRotate(is_portrait);
			if (splitLogView)
			{
				GetCtrl(UI.OBJ_HIDE_LOG_L_2).get_gameObject().SetActive(false);
			}
			StampEditButtonObj.SetActive(false);
			FavStampEditButtonObj.SetActive(false);
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
		StampEditButtonObj.SetActive(false);
		FavStampEditButtonObj.SetActive(false);
		ChatCloseButtonObj.SetActive(false);
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
		if (WidgetBot.get_gameObject().get_activeSelf() != _isVisible)
		{
			WidgetBot.get_gameObject().SetActive(_isVisible);
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
		else if (stampFavoriteEdit != null && stampFavoriteEdit.get_gameObject().get_activeSelf())
		{
			stampFavoriteEdit.Close(update: false);
		}
		else if (stampAll != null && stampAll.get_gameObject().get_activeSelf())
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
			Log.Error((ex == null) ? "Unhandled exception!!" : ex.Message);
			return false;
		}
	}

	private bool IsNullObject(object targetObj)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		if (targetObj is Object)
		{
			if (targetObj != null)
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
		bool flag = IsNotConnected();
		bool flag2 = !flag && IsSendLimit();
		bool flag3 = flag || flag2 || UseNoClanBlock || IsDraging;
		if (flag)
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
		if (flag2)
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
		if (flag3)
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
		Transform transform = WidgetTopHeader.get_transform();
		for (int i = 0; i < 4; i++)
		{
			Transform val = ResourceUtility.Realizes(Resources.Load(HEADER_BUTTON_PREFAB_PATH), transform);
			ChatHeaderButtonController component = val.GetComponent<ChatHeaderButtonController>();
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (m_headerButtonList != null && m_headerButtonList.Count >= 1)
		{
			Vector3[] array = (!_isPortrait) ? HEADER_BUTTON_LANDSCAPE_POS : HEADER_BUTTON_PORTRAIT_POS;
			int i = 0;
			for (int count = m_headerButtonList.Count; i < count; i++)
			{
				Transform transform = m_headerButtonList[i].get_transform();
				transform.set_localPosition(array[i]);
				transform.set_localScale(Vector3.get_one());
			}
		}
	}

	private void InitChatTabState()
	{
		int num = -1;
		int num2 = -1;
		int num3 = -1;
		int num4 = -1;
		int num5 = -1;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		ChatHeaderButtonController.InitParam initParam = new ChatHeaderButtonController.InitParam();
		initParam.ButtonIndex = num9;
		initParam.ChatType = CHAT_TYPE.PERSONAL;
		initParam.OnSelectCallBack = OnSelectPersonalTab;
		m_headerButtonList[num9].Initialize(initParam);
		num = num9;
		num9++;
		ChatHeaderButtonController.InitParam initParam2 = new ChatHeaderButtonController.InitParam();
		initParam2.ButtonIndex = num9;
		initParam2.ChatType = CHAT_TYPE.CLAN;
		initParam2.OnSelectCallBack = OnSelectClanTab;
		m_headerButtonList[num9].Initialize(initParam2);
		num2 = num9;
		num9++;
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			num6 = MonoBehaviourSingleton<ClanMatchingManager>.I.UnreadMessageCount;
		}
		if (HomeType == HOME_TYPE.HOME_TOP)
		{
			ChatHeaderButtonController.InitParam initParam3 = new ChatHeaderButtonController.InitParam();
			initParam3.ButtonIndex = num9;
			initParam3.ChatType = CHAT_TYPE.HOME;
			initParam3.OnSelectCallBack = OnSelectHomeTab;
			m_headerButtonList[num9].Initialize(initParam3);
			num3 = num9;
			num9++;
			if (m_PostRequetQueue[0] != null)
			{
				num7 = m_PostRequetQueue[0].Count;
			}
		}
		else if (HomeType == HOME_TYPE.LOUNGE_TOP)
		{
			ChatHeaderButtonController.InitParam initParam4 = new ChatHeaderButtonController.InitParam();
			initParam4.ButtonIndex = num9;
			initParam4.ChatType = CHAT_TYPE.LOUNGE;
			initParam4.OnSelectCallBack = OnSelectLounge;
			m_headerButtonList[num9].Initialize(initParam4);
			num4 = num9;
			num9++;
			if (m_PostRequetQueue[2] != null)
			{
				num8 = m_PostRequetQueue[2].Count;
			}
		}
		if (hasRoomChat)
		{
			ChatHeaderButtonController.InitParam initParam5 = new ChatHeaderButtonController.InitParam();
			initParam5.ButtonIndex = num9;
			initParam5.ChatType = ((!isFieldChat) ? CHAT_TYPE.ROOM : CHAT_TYPE.FIELD);
			initParam5.OnSelectCallBack = OnSelectRoomTab;
			m_headerButtonList[num9].Initialize(initParam5);
			num5 = num9;
			num9++;
		}
		for (int i = 0; i < m_headerButtonList.Count; i++)
		{
			if (i < num9)
			{
				m_headerButtonList[i].Show();
			}
			else
			{
				m_headerButtonList[i].Hide();
			}
		}
		int index = num9 - 1;
		if (num5 > 0)
		{
			index = num5;
		}
		else if (num6 > 0 && num2 > 0)
		{
			index = num2;
		}
		else if (num7 > 0 && num3 > 0)
		{
			index = num3;
		}
		else if (num8 > 0 && num4 > 0)
		{
			index = num4;
		}
		else if (num4 > 0)
		{
			index = num4;
		}
		else if (num2 > 0 && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			index = num2;
		}
		else if (num3 > 0)
		{
			index = num3;
		}
		m_headerButtonList[index].OnClick();
		if (m_msgUiCtrl == null)
		{
			ChatMessageUserUIController.InitParam initParam6 = new ChatMessageUserUIController.InitParam();
			initParam6.ItemListParent = PersonalMsgGrid.get_transform();
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
		PushNextExclusiveState((!isFieldChat) ? typeof(ChatState_RoomTab) : typeof(ChatState_FieldTab));
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
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, string.Empty);
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
			this.StartCoroutine(m_msgUiCtrl.SendRequestMessagingPersonList(this));
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
		if (PersonalMsgGridObj.get_activeSelf() != flag3)
		{
			PersonalMsgGridObj.SetActive(flag3);
		}
		SubHeaderPanel.get_gameObject().SetActive(flag || flag3);
		SetActiveChannelSelect(flag);
		if (ShowUserListButtonObject.get_activeSelf() != flag3)
		{
			ShowUserListButtonObject.SetActive(flag3);
		}
		SetScrollPanelUI(IsPortrait, _t);
		SetChatBgFrameUI(_t);
		bool active2 = (flag && !hasRoomChat) || flag2 || flag4;
		StampEditButtonObj.SetActive(active2);
		FavStampEditButtonObj.SetActive(active2);
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
			UIVirtualScreen componentInChildren = this.GetComponentInChildren<UIVirtualScreen>();
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
			UIVirtualScreen componentInChildren = this.GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren != null)
			{
				SpriteBgBlack.width = (int)componentInChildren.ScreenWidthFull;
				SpriteBgBlack.height = (int)componentInChildren.ScreenHeightFull;
			}
		}
		SetParent(UI.SPR_BG_BLACK, GetCtrl(UI.BTN_OPEN).get_parent());
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		tmpOffsetStart = clipOffset.y;
	}

	public void onPressEndScrollView()
	{
	}

	public void onDragStartScrollView()
	{
		dragTime = Time.get_time();
		IsDraging = true;
		UpdateSendBlock();
	}

	public void onDragEndScrollView()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		IsDraging = false;
		UpdateSendBlock();
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		if (clipOffset.y - tmpOffsetStart < 0f)
		{
			if (CurrentData.itemList.Count > 0)
			{
				if (CurrentData.itemList.Count > CurrentData.newestIndex && CurrentData.itemList[CurrentData.newestIndex] != null && CurrentData.itemList[CurrentData.newestIndex].get_gameObject().get_activeSelf() && StateMachine.IsRun())
				{
					StateMachine.CurrentState.OnDragAtBottom(CurrentData.itemList[CurrentData.newestIndex].chatItemId, Time.get_time() - dragTime);
				}
			}
			else if (StateMachine.IsRun())
			{
				StateMachine.CurrentState.OnDragAtBottom(string.Empty, Time.get_time() - dragTime);
			}
		}
		else if (CurrentData.itemList.Count > 0)
		{
			if (CurrentData.itemList.Count > CurrentData.oldestItemIndex && CurrentData.itemList[CurrentData.oldestItemIndex] != null && CurrentData.itemList[CurrentData.oldestItemIndex].get_gameObject().get_activeSelf() && StateMachine.IsRun())
			{
				StateMachine.CurrentState.OnDragAtTop(CurrentData.itemList[CurrentData.oldestItemIndex].chatItemId, Time.get_time() - dragTime);
			}
		}
		else if (StateMachine.IsRun())
		{
			StateMachine.CurrentState.OnDragAtTop(string.Empty, Time.get_time() - dragTime);
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
		StampTable.Data data = Singleton<StampTable>.I.GetData((uint)stampId);
		if (data == null)
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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(m_ChatStampListPrefab, 5);
		val.set_parent(parent);
		val.set_localScale(Vector3.get_one());
		return val;
	}

	private void InitStampItem(int index, Transform iTransform, bool isRecycle)
	{
		if (m_StampIdListCanPost == null)
		{
			return;
		}
		bool flag = false;
		int stampId;
		if (index < 10)
		{
			stampId = MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index];
		}
		else if (IsLandScapeFullViewMode)
		{
			if (index < 12)
			{
				stampId = 1;
				flag = true;
			}
			else
			{
				stampId = m_StampIdListCanPost[index - 12];
			}
		}
		else
		{
			stampId = m_StampIdListCanPost[index - 10];
		}
		ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
		item.Init(stampId);
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
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		if (ScrollView.panel.height > CurrentTotalHeight)
		{
			DummyDragScroll.height = (int)(ScrollView.panel.height - 20f);
		}
		else
		{
			DummyDragScroll.height = (int)(CurrentTotalHeight - 20f);
		}
		Transform dragScrollTrans = DragScrollTrans;
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		dragScrollTrans.set_localPosition(new Vector3(clipOffset.x, 0f - CurrentTotalHeight, 0f));
		BoxCollider dragScrollCollider = DragScrollCollider;
		Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
		float z = finalClipRegion.z;
		Vector4 finalClipRegion2 = ScrollView.panel.finalClipRegion;
		float w = finalClipRegion2.w;
		Vector2 clipSoftness = ScrollView.panel.clipSoftness;
		dragScrollCollider.set_size(new Vector3(z, w - clipSoftness.y * 2f, 0f));
	}

	private void Update()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = Time.get_deltaTime();
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
			DragScrollCollider.set_enabled(isOpened);
			if (isOpened)
			{
				Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
				float w = baseClipRegion.w;
				Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
				float num = w - baseClipRegion2.y;
				Vector3 localPosition = DragScrollTrans.get_localPosition();
				float num2 = num + localPosition.y;
				Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
				float w2 = finalClipRegion.w;
				Vector2 clipOffset = ScrollView.panel.clipOffset;
				float num3 = num2 - (w2 + clipOffset.y);
				BoxCollider dragScrollCollider = DragScrollCollider;
				Vector4 baseClipRegion3 = ScrollView.panel.baseClipRegion;
				dragScrollCollider.set_center(Vector2.op_Implicit(new Vector2(baseClipRegion3.x, 0f - num3)));
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
			ChatItem chatItem = itemList[i];
			UIWidget widget = chatItem.widget;
			if (!IsVisible(itemList[i]))
			{
				widget.get_gameObject().SetActive(false);
			}
			else if (!widget.get_gameObject().get_activeSelf())
			{
				widget.get_gameObject().SetActive(true);
			}
		}
	}

	private bool IsVisible(ChatItem chatItem)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = chatItem.get_transform().get_localPosition();
		float y = localPosition.y;
		Vector3 localPosition2 = chatItem.get_transform().get_localPosition();
		float num = localPosition2.y - chatItem.height;
		Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
		float num2 = finalClipRegion.w * 0.5f;
		return finalClipRegion.y + num2 > num && finalClipRegion.y - num2 < y;
	}

	public override void OnModifyChat(NOTIFY_FLAG flag)
	{
		if ((flag & NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
		{
			SetBadge((Enum)UI.BTN_OPEN, GetPendingQueueNum(), 1, 7, -9, is_scale_normalize: false);
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
		if (active != ChannelSelectSpriteButtonObject.get_activeSelf())
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
		this.StartCoroutine(_ienumarator);
	}

	private void OnChangeState(Type currentType, Type prevType)
	{
	}

	public Type GetCurrentStateType()
	{
		return (StateMachine != null) ? StateMachine.CurrentStateType : null;
	}

	public Type GetPrevStateType()
	{
		return (StateMachine != null) ? StateMachine.PrevStateType : null;
	}

	public void PushNextState(Type _s)
	{
		Type currentStateType = GetCurrentStateType();
		if (currentStateType == null || currentStateType != _s)
		{
			m_stateStack.Push(_s);
		}
	}

	public void PushNextExclusiveState(Type _s)
	{
		Type currentStateType = GetCurrentStateType();
		if (currentStateType == null || currentStateType != _s)
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
		return (m_stateStack.Count <= 0) ? null : m_stateStack.Peek();
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
