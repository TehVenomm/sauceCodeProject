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
		SCR_CHAT,
		SPR_BG_OUT_FRAME,
		SPR_BG_IN_FRAME,
		WGT_SLIDE_LIMIT,
		WGT_DUMMY_DRAG_SCROLL,
		SPR_SCROLL_BAR_BACKGROUND,
		SPR_SCROLL_BAR_FOREGROUND,
		IPT_POST,
		BTN_CHANGE_TYPE,
		BTN_INPUT_CLOSE,
		BTN_EMOTION,
		OBJ_POST_FRAME,
		SPR_BG_POST_FRAME,
		LBL_CHANNEL_NAME,
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
		WGT_CHAT_TOP,
		LBL_DEFAULT,
		OBJ_SLIDE_PANEL,
		OBJ_POST_STAMP_FRAME,
		SPR_STAMP_LIST,
		SCR_STAMP_LIST,
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
		OBJ_STAMP_ALL
	}

	public enum CHAT_TYPE
	{
		HOME,
		ROOM,
		LOUNGE
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

	private enum BtnState
	{
		DISABLE,
		ON,
		OFF
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

	private class ChatTabButton
	{
		private UIButton button;

		private UILabel label;

		private Action onActivate;

		private Action onDeactivate;

		private readonly Color BASE_COLOR_ACTIVE = Color.get_white();

		private readonly Color BASE_COLOR_DEACTIVE = new Color(0.5f, 0.5f, 0.5f, 1f);

		private readonly Color OUTLINE_COLOR_ACTIVE = new Color(0f, 0.25f, 0.31f, 1f);

		private readonly Color OUTLINE_COLOR_DEACTIVE = new Color(0.12f, 0.12f, 0.12f, 1f);

		private readonly string SELECTED_SPRITE = "PartyBtn_on";

		private readonly string NOT_SELECTED_SPRITE = "PartyBtn_off";

		public ChatTabButton(UIButton button, UILabel label, Action onClick, Action onActivate, Action onDeactivate)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			this.button = button;
			this.label = label;
			this.onActivate = onActivate;
			this.onDeactivate = onDeactivate;
			button.onClick.Add(new EventDelegate(onClick.Invoke));
		}

		public void Activate()
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			button.normalSprite = NOT_SELECTED_SPRITE;
			button.SetState(UIButtonColor.State.Normal, true);
			label.color = BASE_COLOR_ACTIVE;
			label.effectColor = OUTLINE_COLOR_ACTIVE;
			if (onActivate != null)
			{
				onActivate();
			}
		}

		public void Select()
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			button.normalSprite = SELECTED_SPRITE;
			button.SetState(UIButtonColor.State.Normal, true);
			label.color = BASE_COLOR_ACTIVE;
			label.effectColor = OUTLINE_COLOR_ACTIVE;
		}

		public void Deactivate()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			button.SetState(UIButtonColor.State.Disabled, true);
			label.color = BASE_COLOR_DEACTIVE;
			label.effectColor = OUTLINE_COLOR_DEACTIVE;
			if (onDeactivate != null)
			{
				onDeactivate();
			}
		}

		public void Show()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			button.get_gameObject().SetActive(true);
		}

		public void Hide()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			button.get_gameObject().SetActive(false);
		}
	}

	public const string EVENT_AGE_CONFIRM = "CHAT_AGE_CONFIRM";

	private const int CHAT_ITEM_OFFSET = 22;

	private const int FORCE_SCROLL_LIMIT = 300;

	private const float SOFTNESS_HEIGHT = 10f;

	private const float SPRING_STRENGTH = 20f;

	private const float CHAT_WIDTH = 410f;

	private const float SCROLL_BAR_OFFSET = 48f;

	private const float SLIDER_OFFSET = 10f;

	private const int ITEM_COUNT_MAX = 30;

	private const float SLIDER_TRANSITION_TIME = 0.25f;

	private GameObject m_ChatItemPrefab;

	private GameObject m_ChatStampListPrefab;

	private GameObject m_ChatAdvisaryItemPrefab;

	private ChatItemListData[] m_DataList = new ChatItemListData[Enum.GetNames(typeof(CHAT_TYPE)).Length];

	private UIScrollView m_ScrollView;

	private Transform m_ScrollViewTrans;

	private UIWidget m_DummyDragScroll;

	private BoxCollider m_DragScrollCollider;

	private Transform m_DragScrollTrans;

	private UISprite m_BackgroundInFrame;

	private UIInput m_Input;

	private UILabel m_ChannelName;

	private ChatInputFrame m_InputFrame;

	private UIRect m_RootRect;

	private static readonly string CHANNEL_FORMAT = "0000";

	private Vector3 SLIDER_OPEN_POS;

	private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 ROOM_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private Vector3 SLIDER_CLOSE_POS;

	private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0f);

	private readonly Vector3 ROOM_SLIDER_CLOSE_POS = new Vector3(-180f, -150f, 0f);

	private bool m_IsPlayingChatWindowAnim;

	private List<int> m_StampIdListCanPost;

	private PostRequestQueue[] m_PostRequetQueue = new PostRequestQueue[Enum.GetNames(typeof(CHAT_TYPE)).Length];

	private IEnumerator m_handlPostChatPorcess;

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

	private ChatTabButton homeTabButton;

	private ChatTabButton roomTabButton;

	private ChatChannelInputPanel channelInputPanel;

	private ChatStampFavoriteEdit stampFavoriteEdit;

	private ChatStampAll stampAll;

	private GuildChatAdvisoryItem chatAdvisoryItem;

	private bool isEnableOpenButton;

	private string TAB_BG_NAME_ROOM = "ChatPartyTab1";

	private string TAB_BG_NAME_HOME = "ChatHomeTab1";

	private bool m_IsOnshotStampMode;

	private int m_LastPendingQueueCount;

	private List<UIBehaviour> m_Observers = new List<UIBehaviour>();

	private ChatItemListData CurrentData => m_DataList[(int)currentChat];

	private UIScrollView ScrollView
	{
		get
		{
			if (m_ScrollView == null)
			{
				m_ScrollView = GetCtrl(UI.SCR_CHAT).GetComponent<UIScrollView>();
			}
			return m_ScrollView;
		}
	}

	private Transform ScrollViewTrans
	{
		get
		{
			if (m_ScrollViewTrans == null)
			{
				m_ScrollViewTrans = GetCtrl(UI.SCR_CHAT);
			}
			return m_ScrollViewTrans;
		}
	}

	private UIWidget DummyDragScroll
	{
		get
		{
			if (m_DummyDragScroll == null)
			{
				m_DummyDragScroll = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<UIWidget>();
			}
			return m_DummyDragScroll;
		}
	}

	private BoxCollider DragScrollCollider
	{
		get
		{
			if (m_DragScrollCollider == null)
			{
				m_DragScrollCollider = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<BoxCollider>();
			}
			return m_DragScrollCollider;
		}
	}

	private Transform DragScrollTrans
	{
		get
		{
			if (m_DragScrollTrans == null)
			{
				m_DragScrollTrans = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL);
			}
			return m_DragScrollTrans;
		}
	}

	private UISprite BackgroundInFrame
	{
		get
		{
			if (m_BackgroundInFrame == null)
			{
				m_BackgroundInFrame = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UISprite>();
			}
			return m_BackgroundInFrame;
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

	private UILabel ChannelName
	{
		get
		{
			if (m_ChannelName == null)
			{
				m_ChannelName = GetCtrl(UI.LBL_CHANNEL_NAME).GetComponent<UILabel>();
			}
			return m_ChannelName;
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

	private float CurrentTotalHeight
	{
		get
		{
			float result = 0f;
			if (CurrentData != null)
			{
				result = CurrentData.currentTotalHeight;
			}
			return result;
		}
	}

	private bool hasRoomChat => MonoBehaviourSingleton<ChatManager>.I.roomChat != null;

	private bool hasLoungeChat => MonoBehaviourSingleton<ChatManager>.I.loungeChat != null;

	private void SetScrollBarHeight(int height)
	{
	}

	private void SetPostBtnState(BtnState btnState)
	{
	}

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
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		if (is_portrait)
		{
			UIWidget component = GetCtrl(UI.WGT_ANCHOR_BOTTOM).GetComponent<UIWidget>();
			component.leftAnchor.Set(0f, 0f);
			component.rightAnchor.Set(1f, 0f);
			component.bottomAnchor.Set(0f, 71f);
			component.topAnchor.Set(0f, 325f);
			UIWidget component2 = GetCtrl(UI.WGT_ANCHOR_TOP).GetComponent<UIWidget>();
			component2.leftAnchor.Set(0.5f, -240f);
			component2.rightAnchor.Set(0.5f, 240f);
			component2.bottomAnchor.Set(0.5f, -427f);
			component2.topAnchor.Set(0.5f, 427f);
			SetParent(UI.BTN_HIDE_LOG, UI.OBJ_HIDE_LOG_P);
			component2.get_transform().set_localScale(Vector3.get_one());
			GetCtrl(UI.OBJ_CHANNEL_INPUT).set_localScale(Vector3.get_one());
			UIWidget component3 = GetCtrl(UI.WGT_CHAT_ROOT).GetComponent<UIWidget>();
			component3.bottomAnchor.Set(0f, 72f);
			component3.topAnchor.Set(1f, -72f);
			UIPanel component4 = GetCtrl(UI.SCR_CHAT).GetComponent<UIPanel>();
			component4.bottomAnchor.Set(0f, 316f);
			UIWidget component5 = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UIWidget>();
			component5.bottomAnchor.Set(0f, 309f);
			GetCtrl(UI.SPR_BG_BLACK).set_localScale(Vector3.get_one());
			if (logView.isOpened || logView.isOpening)
			{
				inputBG.Close(delegate
				{
				});
			}
			GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(true);
		}
		else
		{
			UIWidget component6 = GetCtrl(UI.WGT_ANCHOR_BOTTOM).GetComponent<UIWidget>();
			component6.leftAnchor.Set(1f, -560f);
			component6.rightAnchor.Set(1f, -62f);
			component6.bottomAnchor.Set(0f, 0f);
			component6.topAnchor.Set(0f, 254f);
			UIWidget component7 = GetCtrl(UI.WGT_ANCHOR_TOP).GetComponent<UIWidget>();
			component7.leftAnchor.Set(1f, -552f);
			component7.rightAnchor.Set(1f, -72f);
			component7.bottomAnchor.Set(0f, 0f);
			component7.topAnchor.Set(1f, 0f);
			SetParent(UI.BTN_HIDE_LOG, UI.OBJ_HIDE_LOG_L);
			GetCtrl(UI.OBJ_CHANNEL_INPUT).set_localScale(new Vector3(0.75f, 0.75f, 0.75f));
			if (splitLogView)
			{
				float num = 0.86f;
				UIWidget component8 = base.collectUI.GetComponent<UIWidget>();
				float num2 = (float)component8.height * (1f - num) * 0.5f;
				component7.get_transform().set_localScale(new Vector3(num, num, num));
				component7.leftAnchor.Set(1f, -888f);
				component7.rightAnchor.Set(1f, -408f);
				component7.bottomAnchor.Set(0f, 0f - num2 - 4f);
				component7.topAnchor.Set(1f, num2 + 4f);
				UIWidget component9 = GetCtrl(UI.WGT_CHAT_ROOT).GetComponent<UIWidget>();
				component9.bottomAnchor.Set(0f, 0f);
				component9.topAnchor.Set(1f, -4f);
				UIPanel component10 = GetCtrl(UI.SCR_CHAT).GetComponent<UIPanel>();
				component10.bottomAnchor.Set(0f, 22f);
				component10.SetDirty();
				UIWidget component11 = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UIWidget>();
				component11.bottomAnchor.Set(0f, 13f);
				float num3 = 1.17279065f;
				GetCtrl(UI.SPR_BG_BLACK).set_localScale(new Vector3(num3, num3, num3));
				if (logView.isOpened || logView.isOpening)
				{
					inputBG.Open(delegate
					{
					});
					GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(false);
					component6.leftAnchor.Set(1f, -490f);
					component6.rightAnchor.Set(1f, 8f);
				}
				SetParent(UI.BTN_HIDE_LOG, UI.OBJ_HIDE_LOG_L_2);
			}
			else
			{
				UIWidget component12 = GetCtrl(UI.WGT_CHAT_ROOT).GetComponent<UIWidget>();
				component12.bottomAnchor.Set(0f, 0f);
				component12.topAnchor.Set(1f, -4f);
			}
		}
		ScrollView.panel.widgetsAreStatic = false;
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			ScrollView.panel.widgetsAreStatic = true;
		});
		UpdateCloseButtonPosition();
		UpdateAdvisoryItem(is_portrait);
		UpdateAnchors();
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
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.BTN_INPUT_CLOSE);
		if ((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestAcceptRoom")
		{
			ctrl.set_localPosition(new Vector3(231f, -218f));
		}
		else
		{
			ctrl.set_localPosition(new Vector3(231f, 24f));
		}
	}

	private IEnumerator Start()
	{
		Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatItem", false);
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem", false);
		LoadObject lo_chatAdvisaryItem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem", false);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		m_DataList[0] = new ChatItemListData(GetCtrl(UI.OBJ_HOME_ITEM_LIST_ROOT).get_gameObject());
		m_DataList[1] = new ChatItemListData(GetCtrl(UI.OBJ_ROOM_ITEM_LIST_ROOT).get_gameObject());
		m_DataList[2] = new ChatItemListData(GetCtrl(UI.OBJ_LOUNGE_ITEM_LIST_ROOT).get_gameObject());
		for (int i = 0; i < m_PostRequetQueue.Length; i++)
		{
			m_PostRequetQueue[i] = new PostRequestQueue();
		}
		m_ChatItemPrefab = (lo_quest_chatitem.loadedObject as GameObject);
		m_ChatStampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		m_ChatAdvisaryItemPrefab = (lo_chatAdvisaryItem.loadedObject as GameObject);
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
		m_IsPlayingChatWindowAnim = false;
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
		if (value.Length == 0)
		{
			SetPostBtnState(BtnState.OFF);
		}
		else
		{
			SetPostBtnState(BtnState.ON);
		}
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
			if (m_PostRequetQueue[(int)currentChat].Count > 0)
			{
				ChatPostRequest request = m_PostRequetQueue[(int)currentChat].Dequeue();
				Post(request);
				yield return (object)null;
			}
			yield return (object)null;
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
				MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(stampId);
				break;
			case CHAT_TYPE.LOUNGE:
				MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendStamp(stampId);
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
			MonoBehaviourSingleton<ChatManager>.I.roomChat.SendMessage(message);
			break;
		case CHAT_TYPE.LOUNGE:
			MonoBehaviourSingleton<ChatManager>.I.loungeChat.SendMessage(message);
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
		ChatItemListData data = m_DataList[(int)currentChat];
		switch (request.Type)
		{
		case ChatPostRequest.TYPE.Message:
			Post(request.userId, request.userName, request.message, data);
			break;
		case ChatPostRequest.TYPE.Stamp:
			PostStamp(request.userId, request.userName, request.stampId, data);
			break;
		case ChatPostRequest.TYPE.Notification:
			PostNotification(request.message, data);
			break;
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

	private void AddNextChatItem(ChatItemListData data, Action<ChatItem> initializer)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
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
				i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
				{
					ScrollView.panel.widgetsAreStatic = true;
				});
			}
			float currentTotalHeight = data.currentTotalHeight;
			chatItem.get_transform().set_localPosition(new Vector3(-15f, 0f - currentTotalHeight, 0f));
			initializer(chatItem);
			data.currentTotalHeight += chatItem.height;
			UpdateDummyDragScroll();
			float num2 = data.currentTotalHeight - 300f;
			Vector3 localPosition = ScrollViewTrans.get_localPosition();
			if (num2 < localPosition.y)
			{
				float currentTotalHeight2 = data.currentTotalHeight;
				Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
				float num3 = currentTotalHeight2 + baseClipRegion.y;
				Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
				float num4 = num3 - baseClipRegion2.w * 0.5f;
				Vector3 localPosition2 = ScrollViewTrans.get_localPosition();
				float y = localPosition2.y;
				Vector2 clipOffset = ScrollView.panel.clipOffset;
				float num5 = num4 + (y + clipOffset.y);
				Vector2 clipSoftness = ScrollView.panel.clipSoftness;
				float num6 = num5 + clipSoftness.y;
				if (data.itemList.Count >= 30)
				{
					ForceScroll(num6 - chatItem.height - 22f, false);
				}
				ForceScroll(num6, true);
			}
			else if (data.itemList.Count >= 30)
			{
				Vector3 localPosition3 = ScrollViewTrans.get_localPosition();
				if (localPosition3.y > 300f)
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		float num = 450f;
		float currentTotalHeight = CurrentTotalHeight;
		Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
		float num2 = currentTotalHeight + baseClipRegion.y;
		Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
		float num3 = num2 - baseClipRegion2.w * 0.5f;
		Vector3 localPosition = ScrollViewTrans.get_localPosition();
		float y = localPosition.y;
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		float num4 = num3 + (y + clipOffset.y);
		Vector2 clipSoftness = ScrollView.panel.clipSoftness;
		ForceScroll(num4 + clipSoftness.y, false);
		SetScrollBarHeight((int)(num - 48f));
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
		noConnectionView = CreateChatUIFadeGroup(UI.WGT_NO_CONNECTION);
		Action onClick = delegate
		{
			if (hasLoungeChat)
			{
				OnSelectLounge();
			}
			else
			{
				OnSelectHomeTab();
			}
		};
		homeTabButton = CreateChatTabButton(UI.BTN_CHAT_HOME, UI.LBL_CHAT_HOME, onClick, delegate
		{
		}, delegate
		{
		});
		roomTabButton = CreateChatTabButton(UI.BTN_CHAT_ROOM, UI.LBL_CHAT_ROOM, OnSelectRoomTab, delegate
		{
		}, delegate
		{
		});
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

	private ChatTabButton CreateChatTabButton(UI button_elm, UI label_elm, Action onClick, Action onActivate, Action onDeactivate)
	{
		UIButton component = GetCtrl(button_elm).GetComponent<UIButton>();
		UILabel component2 = GetCtrl(label_elm).GetComponent<UILabel>();
		return new ChatTabButton(component, component2, onClick, onActivate, onDeactivate);
	}

	public void SetRoomChatNameType(bool field)
	{
		SetLabelText((Enum)UI.LBL_CHAT_ROOM, StringTable.Get(STRING_CATEGORY.CHAT, (uint)((!field) ? 2 : 3)));
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
			SetChannelName(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 4u));
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
		SetActive((Enum)UI.BTN_SHOW_ALL, true);
		SetActive((Enum)UI.BTN_EDIT, true);
		ShowFull();
	}

	public void ShowFull()
	{
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
		if (ValidateBeforeShowUI())
		{
			if (hasRoomChat)
			{
				OnSelectRoomTab();
			}
			else if (hasLoungeChat)
			{
				OnSelectLounge();
			}
			else
			{
				OnSelectHomeTab();
			}
			m_IsOnshotStampMode = false;
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
			InitStampList();
			NotifyObservers(NOTIFY_FLAG.OPEN_WINDOW);
			GetCtrl(UI.BTN_HIDE_LOG).get_gameObject().SetActive(isMinimizable);
			if (splitLogView)
			{
				if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
				{
					inputBG.Open(delegate
					{
					});
					GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(false);
					UIWidget component = GetCtrl(UI.WGT_ANCHOR_BOTTOM).GetComponent<UIWidget>();
					component.leftAnchor.Set(1f, -490f);
					component.rightAnchor.Set(1f, 8f);
					component.UpdateAnchors();
				}
				else
				{
					GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(true);
				}
				GetCtrl(UI.OBJ_HIDE_LOG_L_2).get_gameObject().SetActive(true);
			}
			GetCtrl(UI.SPR_BG_BLACK).GetComponent<UIWidget>().ResizeCollider();
		}
	}

	public void ShowInputOnly()
	{
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
		if (ValidateBeforeShowUI())
		{
			if (hasRoomChat)
			{
				OnSelectRoomTab();
			}
			else if (hasLoungeChat)
			{
				OnSelectLounge();
			}
			else
			{
				OnSelectHomeTab();
			}
			m_IsOnshotStampMode = true;
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
			InitStampList();
			NotifyObservers(NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY);
			GetCtrl(UI.BTN_SHOW_LOG).get_gameObject().SetActive(true);
			if (splitLogView)
			{
				if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
				{
					UIWidget component = GetCtrl(UI.WGT_ANCHOR_BOTTOM).GetComponent<UIWidget>();
					component.leftAnchor.Set(1f, -560f);
					component.rightAnchor.Set(1f, -62f);
					component.UpdateAnchors();
				}
				GetCtrl(UI.OBJ_HIDE_LOG_L_2).get_gameObject().SetActive(false);
			}
			SetActive((Enum)UI.BTN_SHOW_ALL, false);
			SetActive((Enum)UI.BTN_EDIT, false);
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
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
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
	}

	public void OnPressBackKey()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
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

	private void UpdateSendBlock()
	{
		bool flag = IsNotConnected();
		bool flag2 = !flag && IsSendLimit();
		bool flag3 = flag || flag2;
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

	private void OnSelectHomeTab()
	{
		SaveSlideOffset();
		m_DataList[1].rootObject.SetActive(false);
		m_DataList[0].rootObject.SetActive(true);
		m_DataList[2].rootObject.SetActive(false);
		currentChat = CHAT_TYPE.HOME;
		SetTabState(hasRoomChat, false);
		SetLabelText((Enum)UI.LBL_CHAT_HOME, StringTable.Get(STRING_CATEGORY.CHAT, 1u));
		UpdateWindowSize();
	}

	private void OnSelectRoomTab()
	{
		SaveSlideOffset();
		m_DataList[1].rootObject.SetActive(true);
		m_DataList[0].rootObject.SetActive(false);
		m_DataList[2].rootObject.SetActive(false);
		currentChat = CHAT_TYPE.ROOM;
		string text = (!hasLoungeChat) ? StringTable.Get(STRING_CATEGORY.CHAT, 1u) : StringTable.Get(STRING_CATEGORY.CHAT, 7u);
		SetLabelText((Enum)UI.LBL_CHAT_HOME, text);
		SetTabState(true, true);
		UpdateWindowSize();
	}

	private void OnSelectLounge()
	{
		SaveSlideOffset();
		m_DataList[2].rootObject.SetActive(true);
		m_DataList[1].rootObject.SetActive(false);
		m_DataList[0].rootObject.SetActive(false);
		currentChat = CHAT_TYPE.LOUNGE;
		SetLabelText((Enum)UI.LBL_CHAT_HOME, StringTable.Get(STRING_CATEGORY.CHAT, 7u));
		SetTabState(hasRoomChat, false);
		UpdateWindowSize();
	}

	private void SetTabState(bool roomEnabled, bool roomSelected)
	{
		if (roomEnabled && roomSelected)
		{
			BackgroundInFrame.spriteName = TAB_BG_NAME_ROOM;
		}
		else
		{
			BackgroundInFrame.spriteName = TAB_BG_NAME_HOME;
		}
		SetHomeTabState(!roomSelected, true);
		SetRoomTabState(roomSelected, roomEnabled, true);
	}

	private void SetHomeTabState(bool selected, bool active = true)
	{
		if (selected)
		{
			homeTabButton.Select();
		}
		else if (active)
		{
			homeTabButton.Activate();
		}
		else
		{
			homeTabButton.Deactivate();
		}
	}

	private void SetRoomTabState(bool selected, bool visible, bool active = true)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (visible)
		{
			roomTabButton.Show();
			if (selected)
			{
				roomTabButton.Select();
				GetCtrl(UI.SPR_BG_CHAT_ROOM).get_gameObject().SetActive(selected);
			}
			else if (active)
			{
				roomTabButton.Activate();
				GetCtrl(UI.SPR_BG_CHAT_ROOM).get_gameObject().SetActive(!selected);
			}
			else
			{
				roomTabButton.Deactivate();
			}
		}
		else
		{
			roomTabButton.Hide();
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

	private void ShowChannelSelect()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
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
		SoundManager.PlaySystemSE(SoundID.UISE.OK, 1f);
		MonoBehaviourSingleton<ChatManager>.I.SelectChannel(channel);
		UpdateChannnelName(channel);
		CloseChannelSelect();
	}

	private void SetChannelSelectBG()
	{
		SetParent(UI.SPR_BG_BLACK, UI.OBJ_CHANNEL_SELECT_BG);
		GetCtrl(UI.SPR_BG_BLACK).GetComponent<UIWidget>().ParentHasChanged();
	}

	private void ResetChannelSelectBG()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		SetParent(UI.SPR_BG_BLACK, GetCtrl(UI.BTN_OPEN).get_parent());
		GetCtrl(UI.SPR_BG_BLACK).GetComponent<UIWidget>().ParentHasChanged();
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

	public void UpdateStampList()
	{
		int item_num = m_StampIdListCanPost.Count + 10;
		SetGrid(create_item_func: CreateStampItem, grid_ctrl_enum: UI.GRD_STAMP_LIST, item_prefab_name: null, item_num: item_num, reset: true, item_init_func: InitStampItem);
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
		if (m_StampIdListCanPost != null)
		{
			int stampId = (index >= 10) ? m_StampIdListCanPost[index - 10] : MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index];
			ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
			item.Init(stampId);
			if (!isRecycle)
			{
				ChatStampListItem chatStampListItem = item;
				chatStampListItem.onButton = (Action)Delegate.Combine(chatStampListItem.onButton, (Action)delegate
				{
					MonoBehaviourSingleton<UIManager>.I.mainChat.SendStampAsMine(item.StampId);
				});
			}
		}
	}

	private IEnumerator ChatWindowAnim(Vector3 toPos)
	{
		if (!m_IsPlayingChatWindowAnim)
		{
			m_IsPlayingChatWindowAnim = true;
			SaveSlideOffset();
			float startTime = Time.get_time();
			float now = startTime;
			float endTime = startTime + 0.25f;
			while (now < endTime)
			{
				now = Time.get_time();
				UpdateWindowSize();
				yield return (object)null;
			}
			UpdateWindowSize();
			m_IsPlayingChatWindowAnim = false;
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
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		UpdateObserve();
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
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		UIPanel panel = ScrollView.panel;
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
				m_DataList[i].Reset();
			}
		}
	}

	public void SetActiveChannelSelect(bool active)
	{
		SetActive((Enum)UI.BTN_SPR_CHANNEL_SELECT, active);
	}
}
