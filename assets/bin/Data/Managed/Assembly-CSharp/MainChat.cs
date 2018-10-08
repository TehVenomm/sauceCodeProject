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
		LBL_NO_CONNECTION,
		LBL_POST_LIMIT,
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

	private class ChatItemListData
	{
		private const float DEFAULT_OFFSET = -26f;

		public GameObject rootObject;

		public List<ChatItem> itemList;

		public float currentTotalHeight;

		public int oldestItemIndex;

		public float slideOffset;

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
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			Vector3 localPosition = itemList[0].get_transform().get_localPosition();
			int i = 0;
			for (int count = itemList.Count; i < count; i++)
			{
				Transform val = itemList[i].get_transform();
				Vector3 localPosition2 = val.get_localPosition();
				localPosition.y = localPosition2.y + y;
				val.set_localPosition(localPosition);
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

		public ChatPostRequest(int userId, string userName, string message)
		{
			Type = TYPE.Message;
			this.userId = userId;
			this.userName = userName;
			this.message = message;
		}

		public ChatPostRequest(int userId, string userName, int stampId)
		{
			Type = TYPE.Stamp;
			this.userId = userId;
			this.userName = userName;
			this.stampId = stampId;
		}

		public ChatPostRequest(string message)
		{
			Type = TYPE.Notification;
			this.message = message;
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

	public const int HEADER_BUTTON_COUNT = 3;

	private const int ITEM_COUNT_MAX = 30;

	private const int CHAT_ITEM_OFFSET = 22;

	private const int FORCE_SCROLL_LIMIT_PORTRAIT = 300;

	private const int FORCE_SCROLL_LIMIT_LANDSCAPE = 344;

	private const float SOFTNESS_HEIGHT = 10f;

	private const float SPRING_STRENGTH = 20f;

	private const float CHAT_WIDTH = 410f;

	private const float SCROLL_BAR_OFFSET = 48f;

	private const float SLIDER_OFFSET = 10f;

	private const float ANCHOR_LEFT = 0f;

	private const float ANCHOR_CENTER = 0.5f;

	private const float ANCHOR_RIGHT = 1f;

	private const float ANCHOR_BOT = 0f;

	private const float ANCHOR_TOP = 1f;

	private const int STAMP_COL_DEFAULT_COUNT = 5;

	private const int STAMP_COL_LANDSCAPE_COUNT = 4;

	private const int STAMP_ROW_DEFAULT_COUNT = 2;

	private const int STAMP_ROW_LANDSCAPE_COUNT = 3;

	public static readonly string HEADER_BUTTON_PREFAB_PATH = "InternalUI/UI_Chat/ChatHeaderButton";

	private readonly Vector3[] HEADER_BUTTON_PORTRAIT_POS = (Vector3[])new Vector3[3]
	{
		new Vector3(-217f, 33.4f, -0f),
		new Vector3(-3.8f, 33.4f, -0f),
		new Vector3(209f, 33.4f, -0f)
	};

	private readonly Vector3[] HEADER_BUTTON_LANDSCAPE_POS = (Vector3[])new Vector3[3]
	{
		new Vector3(-234.4f, 33.4f, -0f),
		new Vector3(-3.8f, 33.9f, -0f),
		new Vector3(226.5f, 33.4f, -0f)
	};

	private static readonly string CHANNEL_FORMAT = "0000";

	private readonly Vector3 CHAT_ITEM_OFFSET_POS = new Vector3(-5f, 0f, 0f);

	private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 ROOM_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0f);

	private readonly Vector3 ROOM_SLIDER_CLOSE_POS = new Vector3(-180f, -150f, 0f);

	private static readonly Vector4 WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS = new Vector4(-240f, 240f, -427f, 427f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS = new Vector4(-240f, 240f, 71f, 390f);

	private static readonly Vector4 WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS = new Vector4(-15f, 500f, 0f, 0f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_LANDSCAPE_SETTINGS = new Vector4(-560f, -62f, 0f, 320f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS = new Vector4(-405f, 0f, 0f, 375f);

	private static readonly Vector4 UI_BTN_INPUT_CLOSE_DEFAULT_POS = new Vector4(-33f, 3f, -62f, -1f);

	private static readonly Vector4 UI_BTN_INPUT_CLOSE_LANDSCAPE_POS = new Vector4(-31f, 4f, 18f, 79f);

	private static readonly Vector4 UI_SPRITE_CHAT_BG_FRAME_DEFAULT_POS = new Vector4(23f, -31f, 313f, -53f);

	private static readonly Vector4 UI_SPRITE_CHAT_BG_FRAME_PERSONAL_POS = new Vector4(23f, -31f, 15f, -53f);

	private GameObject m_ChatAdvisaryItemPrefab;

	private GameObject m_ChatItemPrefab;

	private GameObject m_ChatStampListPrefab;

	private Vector3 SLIDER_OPEN_POS;

	private Vector3 SLIDER_CLOSE_POS;

	private ChatItemListData[] m_DataList = new ChatItemListData[Enum.GetNames(typeof(CHAT_TYPE)).Length];

	private ChatMessageUserUIController m_msgUiCtrl;

	private UIScrollView m_ScrollView;

	private Transform m_ScrollViewTrans;

	private UIWidget m_DummyDragScroll;

	private BoxCollider m_DragScrollCollider;

	private Transform m_DragScrollTrans;

	private UIInput m_Input;

	private ChatInputFrame m_InputFrame;

	private UIRect m_RootRect;

	private List<int> m_StampIdListCanPost;

	private List<ChatHeaderButtonController> m_headerButtonList = new List<ChatHeaderButtonController>(3);

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

	private ChatUIFadeGroup noConnectionView;

	private ChatChannelInputPanel channelInputPanel;

	private ChatStampFavoriteEdit stampFavoriteEdit;

	private ChatStampAll stampAll;

	private GuildChatAdvisoryItem chatAdvisoryItem;

	private bool isEnableOpenButton;

	private bool m_IsOnshotStampMode;

	private int m_LastPendingQueueCount;

	private List<UIBehaviour> m_Observers = new List<UIBehaviour>();

	private ChatItemListData CurrentData => m_DataList[(int)currentChat];

	private UIScrollView ScrollView => m_ScrollView ?? (m_ScrollView = GetCtrl(UI.SCR_CHAT).GetComponent<UIScrollView>());

	private Transform ScrollViewTrans
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Expected O, but got Unknown
			//IL_001b: Expected O, but got Unknown
			object obj = m_ScrollViewTrans;
			if (obj == null)
			{
				Transform transform = ScrollView.get_transform();
				Transform val = transform;
				m_ScrollViewTrans = transform;
				obj = val;
			}
			return obj;
		}
	}

	private UIWidget DummyDragScroll => m_DummyDragScroll ?? (m_DummyDragScroll = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<UIWidget>());

	private BoxCollider DragScrollCollider => m_DragScrollCollider ?? (m_DragScrollCollider = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<BoxCollider>());

	private Transform DragScrollTrans
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Expected O, but got Unknown
			//IL_001b: Expected O, but got Unknown
			object obj = m_DragScrollTrans;
			if (obj == null)
			{
				Transform transform = DragScrollCollider.get_transform();
				Transform val = transform;
				m_DragScrollTrans = transform;
				obj = val;
			}
			return obj;
		}
	}

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

	private GameObject ChatCloseButtonObj
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Expected O, but got Unknown
			object obj = m_chatCloseButtonObj;
			if (obj == null)
			{
				GameObject gameObject = GetCtrl(UI.BTN_INPUT_CLOSE).get_gameObject();
				GameObject val = gameObject;
				m_chatCloseButtonObj = gameObject;
				obj = val;
			}
			return obj;
		}
	}

	private GameObject StampEditButtonObj
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Expected O, but got Unknown
			object obj = m_stampEditButtonObj;
			if (obj == null)
			{
				GameObject gameObject = GetCtrl(UI.BTN_EDIT).get_gameObject();
				GameObject val = gameObject;
				m_stampEditButtonObj = gameObject;
				obj = val;
			}
			return obj;
		}
	}

	private GameObject FavStampEditButtonObj
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Expected O, but got Unknown
			object obj = m_favStampEditButtonObj;
			if (obj == null)
			{
				GameObject gameObject = GetCtrl(UI.BTN_SHOW_ALL).get_gameObject();
				GameObject val = gameObject;
				m_favStampEditButtonObj = gameObject;
				obj = val;
			}
			return obj;
		}
	}

	private UIWidget WidgetTop => m_widgetTop ?? (m_widgetTop = GetCtrl(UI.WGT_ANCHOR_TOP).GetComponent<UIWidget>());

	private UIWidget WidgetTopHeader => m_widgetTopHeader ?? (m_widgetTopHeader = GetCtrl(UI.WGT_HEADER_SPACE).GetComponent<UIWidget>());

	private UISprite BackgroundInFrame => m_BackgroundInFrame ?? (m_BackgroundInFrame = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UISprite>());

	private UIPanel SubHeaderPanel => m_subHeaderPanel ?? (m_subHeaderPanel = GetCtrl(UI.WGT_SUB_HEADER_SPACE).GetComponent<UIPanel>());

	private GameObject ChannelSelectSpriteButtonObject
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Expected O, but got Unknown
			object obj = m_channelSelectSpriteButtonObject;
			if (obj == null)
			{
				GameObject gameObject = GetCtrl(UI.BTN_SPR_CHANNEL_SELECT).get_gameObject();
				GameObject val = gameObject;
				m_channelSelectSpriteButtonObject = gameObject;
				obj = val;
			}
			return obj;
		}
	}

	private UILabel ChannelName => m_ChannelName ?? (m_ChannelName = GetCtrl(UI.LBL_CHANNEL_NAME).GetComponent<UILabel>());

	private GameObject ShowUserListButtonObject
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Expected O, but got Unknown
			object obj = m_showUserListButtonObject;
			if (obj == null)
			{
				GameObject gameObject = GetCtrl(UI.BTN_SHOW_USER_LIST).get_gameObject();
				GameObject val = gameObject;
				m_showUserListButtonObject = gameObject;
				obj = val;
			}
			return obj;
		}
	}

	private UIScrollView PersonalMsgScrollView => m_personalMsgScrollView ?? (m_personalMsgScrollView = GetCtrl(UI.SCR_PERSONAL_MSG_LIST_VIEW).GetComponent<UIScrollView>());

	private UIGrid PersonalMsgGrid => m_personalMsgGrid ?? (m_personalMsgGrid = GetCtrl(UI.GRD_PERSONAL_MSG_VIEW).GetComponent<UIGrid>());

	private GameObject PersonalMsgGridObj
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Expected O, but got Unknown
			//IL_001b: Expected O, but got Unknown
			object obj = m_personalMsgGridObj;
			if (obj == null)
			{
				GameObject gameObject = PersonalMsgGrid.get_gameObject();
				GameObject val = gameObject;
				m_personalMsgGridObj = gameObject;
				obj = val;
			}
			return obj;
		}
	}

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

	private bool hasClanChat => MonoBehaviourSingleton<ChatManager>.I.ClanChat != null;

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

	private unsafe void OnScreenRotate(bool is_portrait)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected O, but got Unknown
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
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
				ChatUIFadeGroup chatUIFadeGroup = inputBG;
				if (_003C_003Ef__am_0024cache52 == null)
				{
					_003C_003Ef__am_0024cache52 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				chatUIFadeGroup.Close(_003C_003Ef__am_0024cache52);
			}
			else
			{
				ChatUIFadeGroup chatUIFadeGroup2 = inputBG;
				if (_003C_003Ef__am_0024cache53 == null)
				{
					_003C_003Ef__am_0024cache53 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				chatUIFadeGroup2.Open(_003C_003Ef__am_0024cache53);
				GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(false);
			}
		}
		ScrollView.panel.widgetsAreStatic = false;
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		InitStateMachine();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatItem", false);
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem", false);
		LoadObject lo_chatAdvisaryItem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem", false);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		InitChatDataList();
		for (int i = 0; i < m_PostRequetQueue.Length; i++)
		{
			m_PostRequetQueue[i] = new PostRequestQueue();
		}
		m_ChatItemPrefab = (lo_quest_chatitem.loadedObject as GameObject);
		m_ChatStampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		m_ChatAdvisaryItemPrefab = (lo_chatAdvisaryItem.loadedObject as GameObject);
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		ChangeSliderPos(CHAT_TYPE.HOME);
		SetSliderLimit();
		DummyDragScroll.width = 410;
		string channelName = "0001";
		SetChannelName(channelName);
		ResetStampIdList();
		Reset();
		HideAll();
		HideOpenButton();
	}

	private void InitChatDataList()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
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
		if (m_PostRequetQueue == null || m_PostRequetQueue[0] == null || m_PostRequetQueue[1] == null)
		{
			return 0;
		}
		int num = m_PostRequetQueue[0].Count;
		if (hasLoungeChat)
		{
			num = m_PostRequetQueue[2].Count;
		}
		if (hasRoomChat)
		{
			num += m_PostRequetQueue[1].Count;
		}
		return num;
	}

	public int GetPendingQueueNumWithoutRoom()
	{
		if (m_PostRequetQueue == null)
		{
			return 0;
		}
		if (hasLoungeChat)
		{
			if (m_PostRequetQueue[2] == null)
			{
				return 0;
			}
			return m_PostRequetQueue[2].Count;
		}
		if (m_PostRequetQueue[0] == null)
		{
			return 0;
		}
		return m_PostRequetQueue[0].Count;
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
				yield return (object)null;
			}
			yield return (object)null;
		}
	}

	public int GetQueueArrayIndex(CHAT_TYPE _t)
	{
		switch (_t)
		{
		case CHAT_TYPE.ROOM:
		case CHAT_TYPE.FIELD:
			return 1;
		default:
			return (int)_t;
		}
	}

	public void SendStampAsMine(int stampId)
	{
		if (CanIPostTheStamp(stampId))
		{
			switch (currentChat)
			{
			case CHAT_TYPE.HOME:
				MonoBehaviourSingleton<ChatManager>.I.homeChat.SendStamp(stampId);
				break;
			case CHAT_TYPE.ROOM:
			case CHAT_TYPE.FIELD:
				MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(stampId);
				break;
			case CHAT_TYPE.LOUNGE:
				MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendStamp(stampId);
				break;
			case CHAT_TYPE.CLAN:
				MonoBehaviourSingleton<ChatManager>.I.ClanChat.SendStamp(stampId);
				break;
			}
			UpdateSendBlock();
			if (m_IsOnshotStampMode)
			{
				HideAll();
			}
		}
	}

	public void SendMessageAsMine(string message)
	{
		switch (currentChat)
		{
		case CHAT_TYPE.HOME:
			MonoBehaviourSingleton<ChatManager>.I.homeChat.SendMessage(message);
			break;
		case CHAT_TYPE.ROOM:
		case CHAT_TYPE.FIELD:
			MonoBehaviourSingleton<ChatManager>.I.roomChat.SendMessage(message);
			break;
		case CHAT_TYPE.LOUNGE:
			MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendMessage(message);
			break;
		case CHAT_TYPE.CLAN:
			MonoBehaviourSingleton<ChatManager>.I.ClanChat.SendMessage(message);
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
		if (CurrentData != null)
		{
			ChatItemListData currentData = CurrentData;
			switch (request.Type)
			{
			case ChatPostRequest.TYPE.Message:
				Post(request.userId, request.userName, request.message, currentData);
				break;
			case ChatPostRequest.TYPE.Stamp:
				PostStamp(request.userId, request.userName, request.stampId, currentData);
				break;
			case ChatPostRequest.TYPE.Notification:
				PostNotification(request.message, currentData);
				break;
			}
		}
	}

	private void Post(int userId, string userName, string text, ChatItemListData data)
	{
		AddNextChatItem(data, delegate(ChatItem chatItem)
		{
			chatItem.Init(userId, userName, text);
		});
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP, 1f);
	}

	private void PostStamp(int userId, string userName, int stampId, ChatItemListData data)
	{
		if (IsValidStampId(stampId))
		{
			StampTable.Data data2 = Singleton<StampTable>.I.GetData((uint)stampId);
			if (data2 != null)
			{
				AddNextChatItem(data, delegate(ChatItem chatItem)
				{
					chatItem.Init(userId, userName, stampId);
				});
				if (data2.hasSE)
				{
					SoundManager.PlaySystemSE(SoundID.UISE.POPUP, 1f);
				}
				else
				{
					SoundManager.PlaySystemSE(SoundID.UISE.POPUP, 1f);
				}
			}
		}
	}

	private void PostNotification(string text, ChatItemListData data)
	{
		AddNextChatItem(data, delegate(ChatItem chatItem)
		{
			chatItem.Init(text);
		});
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP, 1f);
	}

	private unsafe void AddNextChatItem(ChatItemListData data, Action<ChatItem> initializer)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_ChatItemPrefab == null))
		{
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
				i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			float currentTotalHeight = data.currentTotalHeight;
			chatItem.get_transform().set_localPosition(CHAT_ITEM_OFFSET_POS + Vector3.get_down() * currentTotalHeight);
			initializer(chatItem);
			data.currentTotalHeight += chatItem.height;
			UpdateDummyDragScroll();
			float num2 = (float)((!IsPortrait) ? 344 : 300);
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
					ForceScroll(num7 - chatItem.height - 22f, false);
				}
				ForceScroll(num7, true);
			}
			else if (data.itemList.Count >= 30)
			{
				Vector3 localPosition3 = ScrollViewTrans.get_localPosition();
				if (localPosition3.y > num2)
				{
					Vector3 localPosition4 = ScrollViewTrans.get_localPosition();
					ForceScroll(localPosition4.y - num, false);
				}
			}
			if (data.itemList.Count < 30)
			{
				data.itemList.Add(chatItem);
			}
		}
	}

	private void ForceScroll(float newHeight, bool useSpring)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
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
		}
		else
		{
			Vector2 clipOffset = ScrollView.panel.clipOffset;
			Vector3 localPosition = ScrollViewTrans.get_localPosition();
			float num = localPosition.y + clipOffset.y;
			ScrollViewTrans.set_localPosition(Vector3.get_up() * newHeight);
			clipOffset.y = 0f - newHeight + num;
			ScrollView.panel.clipOffset = clipOffset;
		}
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

	private unsafe void Initialize()
	{
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		logView = CreateChatUIFadeGroup(UI.WGT_ANCHOR_TOP);
		inputView = CreateChatUIFadeGroup(UI.WGT_ANCHOR_BOTTOM);
		inputBG = CreateChatUIFadeGroup(UI.SPR_BG_POST_FRAME);
		channelSelect = CreateChatUIFadeGroup(UI.OBJ_CHANNEL_SELECT);
		chatOpenButton = CreateChatUIFadeGroup(UI.BTN_OPEN);
		bgBlack = CreateChatUIFadeGroup(UI.SPR_BG_BLACK);
		sendBlockView = CreateChatUIFadeGroup(UI.OBJ_POST_BLOCK);
		sendLimitView = CreateChatUIFadeGroup(UI.WGT_POST_LIMIT);
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
		MonoBehaviourSingleton<ChatManager>.I.OnCreateRoomChat += new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyRoomChat += OnDestroyRoomChat;
		MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += OnCreateLoungeChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += OnDestroyLoungeChat;
		ChatInputFrame inputFrame = InputFrame;
		inputFrame.onChange = Delegate.Combine((Delegate)inputFrame.onChange, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		ChatInputFrame inputFrame2 = InputFrame;
		inputFrame2.onSubmit = Delegate.Combine((Delegate)inputFrame2.onSubmit, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void InitChannelSelect()
	{
		SetButtonEvent(UI.BTN_SPR_CHANNEL_SELECT, new EventDelegate(ShowChannelSelect));
		SetButtonEvent(UI.BTN_CLOSE_CHANNEL_SELECT, new EventDelegate(delegate
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
			CloseChannelSelect();
		}));
		SetButtonEvent(UI.BTN_HOT, new EventDelegate(OnSelectHotChannel));
		SetButtonEvent(UI.BTN_QUIET, new EventDelegate(OnSelectQuietChannel));
		SetButtonEvent(UI.BTN_ANYWARE, new EventDelegate(OnSelectRecommendedChannel));
		SetButtonEvent(UI.BTN_NUMBER_INPUT, new EventDelegate(OnSelectInputChannelNumber));
		SetButtonEvent(UI.BTN_SHOW_ALL, new EventDelegate(OnSelectShowAll));
		SetButtonEvent(UI.BTN_EDIT, new EventDelegate(OnSelectEdit));
	}

	private unsafe void InitChannelInput()
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		ChatUIFadeGroup root = CreateChatUIFadeGroup(UI.OBJ_CHANNEL_INPUT);
		channelInputPanel = new ChatChannelInputPanel(root);
		channelInputPanel.SetNumLabels(GetLabel(UI.LBL_INPUT_PASS_4), GetLabel(UI.LBL_INPUT_PASS_3), GetLabel(UI.LBL_INPUT_PASS_2), GetLabel(UI.LBL_INPUT_PASS_1));
		channelInputPanel.SetNumButtons(GetButton(UI.BTN_0), GetButton(UI.BTN_1), GetButton(UI.BTN_2), GetButton(UI.BTN_3), GetButton(UI.BTN_4), GetButton(UI.BTN_5), GetButton(UI.BTN_6), GetButton(UI.BTN_7), GetButton(UI.BTN_8), GetButton(UI.BTN_9));
		channelInputPanel.SetOKButton(GetButton(UI.BTN_NEXT));
		channelInputPanel.SetClearButton(GetButton(UI.BTN_CLEAR));
		channelInputPanel.SetCloseButton(GetButton(UI.BTN_CLOSE_CHANNEL_INPUT));
		channelInputPanel.SetOnOKDelegate(OnInputChannel);
		channelInputPanel.SetOnCloseButtonDelegate(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private void OnReceiveText(CHAT_TYPE chatType, int userId, string userName, string message)
	{
		if (IsAllowedUser(userId))
		{
			m_PostRequetQueue[(int)chatType].Enqueue(new ChatPostRequest(userId, userName, message));
		}
	}

	private void OnReceiveStamp(CHAT_TYPE chatType, int userId, string userName, int stampId)
	{
		if (IsAllowedUser(userId))
		{
			m_PostRequetQueue[(int)chatType].Enqueue(new ChatPostRequest(userId, userName, stampId));
		}
	}

	private void OnReceiveHomeText(int userId, string userName, string message)
	{
		OnReceiveText(CHAT_TYPE.HOME, userId, userName, message);
	}

	private void OnReceiveHomeStamp(int userId, string userName, int stampId)
	{
		OnReceiveStamp(CHAT_TYPE.HOME, userId, userName, stampId);
	}

	private void OnReceiveRoomText(int userId, string userName, string message)
	{
		OnReceiveText(CHAT_TYPE.ROOM, userId, userName, message);
	}

	private void OnReceiveRoomStamp(int userId, string userName, int stampId)
	{
		OnReceiveStamp(CHAT_TYPE.ROOM, userId, userName, stampId);
	}

	private void OnReceiveRoomNotification(string message)
	{
		m_PostRequetQueue[1].Enqueue(new ChatPostRequest(message));
	}

	private void OnReceiveLoungeText(int userId, string userName, string message)
	{
		OnReceiveText(CHAT_TYPE.LOUNGE, userId, userName, message);
	}

	private void OnReceiveLoungeStamp(int userId, string userName, int stampId)
	{
		OnReceiveStamp(CHAT_TYPE.LOUNGE, userId, userName, stampId);
	}

	private void OnReceiveLoungeNotification(string message)
	{
		m_PostRequetQueue[2].Enqueue(new ChatPostRequest(message));
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
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
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
					//IL_001c: Unknown result type (might be due to invalid IL or missing references)
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
		bool active = GetCurrentStateType() == typeof(ChatState_HomeTab);
		StampEditButtonObj.SetActive(active);
		FavStampEditButtonObj.SetActive(active);
		ShowFull();
	}

	public unsafe void ShowFull()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		ChatCloseButtonObj.SetActive(true);
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
		if (ValidateBeforeShowUI())
		{
			InitChatTabState();
			m_IsOnshotStampMode = false;
			m_isShowFullChatView = true;
			UpdateSendBlock();
			UpdateChannnelName();
			UpdateCloseButtonPosition();
			UpdateAdvisoryItem(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
			ChatUIFadeGroup chatUIFadeGroup = bgBlack;
			if (_003C_003Ef__am_0024cache54 == null)
			{
				_003C_003Ef__am_0024cache54 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup.Open(_003C_003Ef__am_0024cache54);
			ChatUIFadeGroup chatUIFadeGroup2 = logView;
			if (_003C_003Ef__am_0024cache55 == null)
			{
				_003C_003Ef__am_0024cache55 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup2.Open(_003C_003Ef__am_0024cache55);
			ChatUIFadeGroup chatUIFadeGroup3 = inputView;
			if (_003C_003Ef__am_0024cache56 == null)
			{
				_003C_003Ef__am_0024cache56 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup3.Open(_003C_003Ef__am_0024cache56);
			ChatUIFadeGroup chatUIFadeGroup4 = inputBG;
			if (_003C_003Ef__am_0024cache57 == null)
			{
				_003C_003Ef__am_0024cache57 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup4.Close(_003C_003Ef__am_0024cache57);
			ChatUIFadeGroup chatUIFadeGroup5 = channelSelect;
			if (_003C_003Ef__am_0024cache58 == null)
			{
				_003C_003Ef__am_0024cache58 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup5.Close(_003C_003Ef__am_0024cache58);
			channelInputPanel.Close();
			ChatUIFadeGroup chatUIFadeGroup6 = chatOpenButton;
			if (_003C_003Ef__am_0024cache59 == null)
			{
				_003C_003Ef__am_0024cache59 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup6.Close(_003C_003Ef__am_0024cache59);
			StartPostChatProcess();
			NotifyObservers(NOTIFY_FLAG.OPEN_WINDOW);
			GetCtrl(UI.BTN_HIDE_LOG).get_gameObject().SetActive(isMinimizable);
			if (splitLogView)
			{
				bool flag = MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
				if (!flag)
				{
					ChatUIFadeGroup chatUIFadeGroup7 = inputBG;
					if (_003C_003Ef__am_0024cache5A == null)
					{
						_003C_003Ef__am_0024cache5A = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					chatUIFadeGroup7.Open(_003C_003Ef__am_0024cache5A);
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
		}
	}

	public unsafe void ShowInputOnly()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		ChatCloseButtonObj.SetActive(true);
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
		if (ValidateBeforeShowUI())
		{
			InitChatTabState();
			m_IsOnshotStampMode = true;
			m_isShowFullChatView = false;
			isMinimizable = true;
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			UpdateSendBlock();
			UpdateCloseButtonPosition();
			ChatUIFadeGroup chatUIFadeGroup = bgBlack;
			if (_003C_003Ef__am_0024cache5B == null)
			{
				_003C_003Ef__am_0024cache5B = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup.Close(_003C_003Ef__am_0024cache5B);
			ChatUIFadeGroup chatUIFadeGroup2 = logView;
			if (_003C_003Ef__am_0024cache5C == null)
			{
				_003C_003Ef__am_0024cache5C = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup2.Close(_003C_003Ef__am_0024cache5C);
			ChatUIFadeGroup chatUIFadeGroup3 = inputView;
			if (_003C_003Ef__am_0024cache5D == null)
			{
				_003C_003Ef__am_0024cache5D = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup3.Open(_003C_003Ef__am_0024cache5D);
			ChatUIFadeGroup chatUIFadeGroup4 = inputBG;
			if (_003C_003Ef__am_0024cache5E == null)
			{
				_003C_003Ef__am_0024cache5E = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup4.Open(_003C_003Ef__am_0024cache5E);
			ChatUIFadeGroup chatUIFadeGroup5 = chatOpenButton;
			if (_003C_003Ef__am_0024cache5F == null)
			{
				_003C_003Ef__am_0024cache5F = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup5.Close(_003C_003Ef__am_0024cache5F);
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
		}
	}

	public unsafe void ShowInputOnly_NotOneShot()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		ShowInputOnly();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		m_IsOnshotStampMode = false;
	}

	public void HideFull()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
		HideAll();
	}

	public unsafe void HideAll()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		isMinimizable = false;
		ChatUIFadeGroup chatUIFadeGroup = logView;
		if (_003C_003Ef__am_0024cache60 == null)
		{
			_003C_003Ef__am_0024cache60 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup.Close(_003C_003Ef__am_0024cache60);
		ChatUIFadeGroup chatUIFadeGroup2 = inputView;
		if (_003C_003Ef__am_0024cache61 == null)
		{
			_003C_003Ef__am_0024cache61 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup2.Close(_003C_003Ef__am_0024cache61);
		ChatUIFadeGroup chatUIFadeGroup3 = inputBG;
		if (_003C_003Ef__am_0024cache62 == null)
		{
			_003C_003Ef__am_0024cache62 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup3.Close(_003C_003Ef__am_0024cache62);
		ChatUIFadeGroup chatUIFadeGroup4 = bgBlack;
		if (_003C_003Ef__am_0024cache63 == null)
		{
			_003C_003Ef__am_0024cache63 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup4.Close(_003C_003Ef__am_0024cache63);
		CloseChannelInput();
		CloseChannelSelect();
		if (isEnableOpenButton)
		{
			ChatUIFadeGroup chatUIFadeGroup5 = chatOpenButton;
			if (_003C_003Ef__am_0024cache64 == null)
			{
				_003C_003Ef__am_0024cache64 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup5.Open(_003C_003Ef__am_0024cache64);
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
		SwitchBottomUIActivation(true);
	}

	private void ShowBottomUI()
	{
		SwitchBottomUIActivation(false);
	}

	private void SwitchBottomUIActivation(bool _isVisible)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (WidgetBot.get_gameObject().get_activeSelf() != _isVisible)
		{
			WidgetBot.get_gameObject().SetActive(_isVisible);
		}
	}

	public unsafe void OnPressBackKey()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (channelInputPanel.isOpened)
		{
			CloseChannelInput();
			ChatUIFadeGroup chatUIFadeGroup = channelSelect;
			if (_003C_003Ef__am_0024cache65 == null)
			{
				_003C_003Ef__am_0024cache65 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup.Open(_003C_003Ef__am_0024cache65);
		}
		else if (channelSelect.isOpened)
		{
			CloseChannelSelect();
		}
		else if (stampFavoriteEdit != null && stampFavoriteEdit.get_gameObject().get_activeSelf())
		{
			stampFavoriteEdit.Close(false);
		}
		else if (stampAll != null && stampAll.get_gameObject().get_activeSelf())
		{
			stampAll.Close();
		}
		else
		{
			HideAll();
		}
	}

	public unsafe void ShowOpenButton()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		ChatUIFadeGroup chatUIFadeGroup = chatOpenButton;
		if (_003C_003Ef__am_0024cache66 == null)
		{
			_003C_003Ef__am_0024cache66 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup.Open(_003C_003Ef__am_0024cache66);
		isEnableOpenButton = true;
		addObserver(this);
	}

	public unsafe void HideOpenButton()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		ChatUIFadeGroup chatUIFadeGroup = chatOpenButton;
		if (_003C_003Ef__am_0024cache67 == null)
		{
			_003C_003Ef__am_0024cache67 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup.Close(_003C_003Ef__am_0024cache67);
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
			if (MonoBehaviourSingleton<ChatManager>.IsValid())
			{
				ChatManager i = MonoBehaviourSingleton<ChatManager>.I;
				if (!IsNullObject(i))
				{
					if (currentChat != 0)
					{
						if (currentChat != CHAT_TYPE.ROOM)
						{
							return i.loungeChat != null && !i.loungeChat.CanSendMessage();
						}
						return i.roomChat != null && !i.roomChat.CanSendMessage();
					}
					return i != null && !i.homeChat.CanSendMessage();
				}
				return false;
			}
			return false;
			IL_00a8:
			bool result;
			return result;
		}
		catch (Exception ex)
		{
			Log.Error((ex == null) ? "Unhandled exception!!" : ex.Message);
			return false;
			IL_00d6:
			bool result;
			return result;
		}
	}

	private bool IsNullObject(object targetObj)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
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

	private unsafe void UpdateSendBlock()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		bool flag = IsNotConnected();
		bool flag2 = !flag && IsSendLimit();
		bool flag3 = flag || flag2;
		if (flag)
		{
			ChatUIFadeGroup chatUIFadeGroup = noConnectionView;
			if (_003C_003Ef__am_0024cache68 == null)
			{
				_003C_003Ef__am_0024cache68 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup.Open(_003C_003Ef__am_0024cache68);
		}
		else
		{
			ChatUIFadeGroup chatUIFadeGroup2 = noConnectionView;
			if (_003C_003Ef__am_0024cache69 == null)
			{
				_003C_003Ef__am_0024cache69 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup2.Close(_003C_003Ef__am_0024cache69);
		}
		if (flag2)
		{
			ChatUIFadeGroup chatUIFadeGroup3 = sendLimitView;
			if (_003C_003Ef__am_0024cache6A == null)
			{
				_003C_003Ef__am_0024cache6A = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup3.Open(_003C_003Ef__am_0024cache6A);
		}
		else
		{
			ChatUIFadeGroup chatUIFadeGroup4 = sendLimitView;
			if (_003C_003Ef__am_0024cache6B == null)
			{
				_003C_003Ef__am_0024cache6B = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup4.Close(_003C_003Ef__am_0024cache6B);
		}
		if (flag3)
		{
			ChatUIFadeGroup chatUIFadeGroup5 = sendBlockView;
			if (_003C_003Ef__am_0024cache6C == null)
			{
				_003C_003Ef__am_0024cache6C = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup5.Open(_003C_003Ef__am_0024cache6C);
		}
		else
		{
			ChatUIFadeGroup chatUIFadeGroup6 = sendBlockView;
			if (_003C_003Ef__am_0024cache6D == null)
			{
				_003C_003Ef__am_0024cache6D = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			chatUIFadeGroup6.Close(_003C_003Ef__am_0024cache6D);
		}
	}

	private void GenerateHeaderButton()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (!(WidgetTopHeader == null))
		{
			Transform parent = WidgetTopHeader.get_transform();
			for (int i = 0; i < 3; i++)
			{
				Transform val = ResourceUtility.Realizes(Resources.Load(HEADER_BUTTON_PREFAB_PATH), parent, -1);
				ChatHeaderButtonController component = val.GetComponent<ChatHeaderButtonController>();
				if (!(component == null))
				{
					component.Hide();
					m_headerButtonList.Add(component);
				}
			}
			SetHeaderButtonPosition(IsPortrait);
		}
	}

	private void SetHeaderButtonPosition(bool _isPortrait)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (m_headerButtonList != null && m_headerButtonList.Count >= 1)
		{
			Vector3[] array = (!_isPortrait) ? HEADER_BUTTON_LANDSCAPE_POS : HEADER_BUTTON_PORTRAIT_POS;
			int i = 0;
			for (int count = m_headerButtonList.Count; i < count; i++)
			{
				Transform val = m_headerButtonList[i].get_transform();
				val.set_localPosition(array[i]);
				val.set_localScale(Vector3.get_one());
			}
		}
	}

	private unsafe void InitChatTabState()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		int num = 3;
		if (!hasRoomChat)
		{
			num--;
		}
		for (int i = 0; i < 3; i++)
		{
			ChatHeaderButtonController.InitParam initParam = new ChatHeaderButtonController.InitParam();
			initParam.ButtonIndex = i;
			switch (i)
			{
			case 0:
				initParam.ChatType = CHAT_TYPE.PERSONAL;
				initParam.OnSelectCallBack = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				break;
			case 1:
				initParam.ChatType = (hasLoungeChat ? CHAT_TYPE.LOUNGE : CHAT_TYPE.HOME);
				initParam.OnSelectCallBack = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				break;
			case 2:
				initParam.ChatType = ((!isFieldChat) ? CHAT_TYPE.ROOM : CHAT_TYPE.FIELD);
				initParam.OnSelectCallBack = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				break;
			case 3:
				initParam.ChatType = CHAT_TYPE.CLAN;
				initParam.OnSelectCallBack = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				break;
			}
			m_headerButtonList[i].Initialize(initParam);
			if (i >= num)
			{
				m_headerButtonList[i].Hide();
			}
			else
			{
				m_headerButtonList[i].Show();
			}
		}
		if (hasRoomChat)
		{
			for (int j = 0; j < 3; j++)
			{
				if (m_headerButtonList[j].MyChatType == CHAT_TYPE.ROOM || m_headerButtonList[j].MyChatType == CHAT_TYPE.FIELD)
				{
					m_headerButtonList[j].OnClick();
				}
			}
		}
		else
		{
			for (int k = 0; k < 3; k++)
			{
				if (m_headerButtonList[k].MyChatType == CHAT_TYPE.HOME || m_headerButtonList[k].MyChatType == CHAT_TYPE.LOUNGE)
				{
					m_headerButtonList[k].OnClick();
				}
			}
		}
		if (m_msgUiCtrl == null)
		{
			ChatMessageUserUIController.InitParam initParam2 = new ChatMessageUserUIController.InitParam();
			initParam2.ItemListParent = PersonalMsgGrid.get_transform();
			initParam2.ItemVisibleCount = Mathf.FloorToInt(PersonalMsgScrollView.panel.height / PersonalMsgGrid.cellHeight);
			initParam2.OnClickItem = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			m_msgUiCtrl = new ChatMessageUserUIController(initParam2);
			EventDelegate eventDelegate = new EventDelegate();
			eventDelegate.methodName = "OnClickShowUserListButton";
			eventDelegate.target = this;
			GetCtrl(UI.BTN_SHOW_USER_LIST).GetComponent<UIButton>().onClick.Add(eventDelegate);
		}
	}

	private void OnSelectHomeTab()
	{
		if (hasLoungeChat)
		{
			OnSelectLounge();
		}
		else
		{
			OnSelectHeaderTab(CHAT_TYPE.HOME);
			PushNextExclusiveState(typeof(ChatState_HomeTab));
		}
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
		OnSelectHeaderTab(CHAT_TYPE.CLAN);
		PushNextExclusiveState(typeof(ChatState_ClanTab));
	}

	private void OnSelectPersonalTab()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		ResetOtherButtonSettings(CHAT_TYPE.PERSONAL);
		PushNextExclusiveState(typeof(ChatState_PersonalTab));
		if (!m_msgUiCtrl.IsConnecting)
		{
			PersonalMsgScrollView.ResetPosition();
			this.StartCoroutine(m_msgUiCtrl.SendRequestMessagingPersonList(this));
		}
	}

	private unsafe void OnSelectHeaderTab(CHAT_TYPE _t)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		ResetOtherButtonSettings(_t);
		SaveSlideOffset();
		currentChat = _t;
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		UpdateWindowSize();
	}

	private void ResetOtherButtonSettings(CHAT_TYPE _t)
	{
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		if (m_headerButtonList != null && m_headerButtonList.Count >= 1)
		{
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
			bool active2 = flag && !hasRoomChat;
			StampEditButtonObj.SetActive(active2);
			FavStampEditButtonObj.SetActive(active2);
		}
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

	private unsafe void ShowChannelSelect()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
		SetChannelSelectBG();
		ChatUIFadeGroup chatUIFadeGroup = channelSelect;
		if (_003C_003Ef__am_0024cache6E == null)
		{
			_003C_003Ef__am_0024cache6E = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup.Open(_003C_003Ef__am_0024cache6E);
	}

	private unsafe void CloseChannelSelect()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		ResetChannelSelectBG();
		ChatUIFadeGroup chatUIFadeGroup = channelSelect;
		if (_003C_003Ef__am_0024cache6F == null)
		{
			_003C_003Ef__am_0024cache6F = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup.Close(_003C_003Ef__am_0024cache6F);
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
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
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

	private unsafe void ShowChannelInput()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		ChatUIFadeGroup chatUIFadeGroup = channelSelect;
		if (_003C_003Ef__am_0024cache70 == null)
		{
			_003C_003Ef__am_0024cache70 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		chatUIFadeGroup.Close(_003C_003Ef__am_0024cache70);
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
		SoundManager.PlaySystemSE(SoundID.UISE.OK, 1f);
		MonoBehaviourSingleton<ChatManager>.I.SelectChannel(channel);
		UpdateChannnelName(channel);
		CloseChannelSelect();
	}

	private void SetChannelSelectBG()
	{
		SetParent(UI.SPR_BG_BLACK, UI.OBJ_CHANNEL_SELECT_BG);
		SpriteBgBlack.ParentHasChanged();
	}

	private void ResetChannelSelectBG()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		SetParent(UI.SPR_BG_BLACK, GetCtrl(UI.BTN_OPEN).get_parent());
		SpriteBgBlack.ParentHasChanged();
	}

	private void OnError(string message)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, message, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u), null, null, null), delegate
		{
		}, true, 0);
	}

	private void InitStampList()
	{
		if (m_StampIdListCanPost == null)
		{
			ResetStampIdList();
		}
		UpdateStampList();
	}

	public unsafe void UpdateStampList()
	{
		int item_num = m_StampIdListCanPost.Count + 10 + (IsLandScapeFullViewMode ? 2 : 0);
		SetGrid(create_item_func: new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), grid_ctrl_enum: UI.GRD_STAMP_LIST, item_prefab_name: null, item_num: item_num, reset: true, item_init_func: new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private unsafe void InitStampItem(int index, Transform iTransform, bool isRecycle)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		if (m_StampIdListCanPost != null)
		{
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
				_003CInitStampItem_003Ec__AnonStorey2D2 _003CInitStampItem_003Ec__AnonStorey2D;
				chatStampListItem.onButton = Delegate.Combine((Delegate)chatStampListItem.onButton, (Delegate)new Action((object)_003CInitStampItem_003Ec__AnonStorey2D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
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
		object dragScrollTrans = (object)DragScrollTrans;
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		dragScrollTrans.set_localPosition(new Vector3(clipOffset.x, 0f - CurrentTotalHeight, 0f));
		object dragScrollCollider = (object)DragScrollCollider;
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
				object dragScrollCollider = (object)DragScrollCollider;
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
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
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
			SetBadge((Enum)UI.BTN_OPEN, GetPendingQueueNum(), 1, 7, -9, false);
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
		if (!AppMain.isApplicationQuit)
		{
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
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
