using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildMessage : GameSection
{
	[Serializable]
	public class ChatPostRequest
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

		public string uuId
		{
			get;
			private set;
		}

		public int chatId
		{
			get;
			private set;
		}

		public int receiveId
		{
			get;
			private set;
		}

		public int senderId
		{
			get;
			private set;
		}

		public string senderName
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

		public ChatPostRequest(string uuID, int cid, int rId, int sId, string sName, string message)
		{
			uuId = uuID;
			chatId = cid;
			Type = TYPE.Message;
			receiveId = rId;
			senderId = sId;
			senderName = sName;
			this.message = message;
		}

		public ChatPostRequest(string uuID, int cid, int rId, int sId, string sName, int stampId)
		{
			uuId = uuID;
			chatId = cid;
			Type = TYPE.Stamp;
			receiveId = rId;
			senderId = sId;
			senderName = sName;
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

	private class ChatItemListData
	{
		private const float DEFAULT_OFFSET = -26f;

		public GameObject rootObject;

		public List<GuildChatItem> itemList;

		public float currentTotalHeight;

		public int oldestItemIndex;

		public float slideOffset;

		public ChatItemListData(GameObject root)
		{
			rootObject = root;
			itemList = new List<GuildChatItem>();
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

	private class ChatTabData
	{
		public Transform tran;

		public FriendCharaInfo info;
	}

	private enum UI
	{
		OBJ_CHAT_PANEL,
		OBJ_DONATE_PANEL,
		WGT_DUMMY_DRAG_SCROLL,
		BTN_CLOSE,
		BTN_DONATE,
		BTN_MEMBER,
		BTN_CHAT,
		WGT_CHAT_ROOT,
		WGT_CHAT_TOP,
		SCR_CHAT,
		OBJ_CLAN_ITEM_LIST_ROOT,
		OBJ_MEMBER_ITEM_LIST_ROOT,
		BTN_TAB_GUILD,
		SCR_TAB_CHAT,
		GRD_TAB_CHAT,
		LBL_TAB_NAME,
		SPR_TAB_HIGHLIGHT,
		BTN_TAB_CLOSE,
		OBJ_STAMP_DOWN,
		OBJ_STAMP_UP,
		BTN_STAMP_UP,
		BTN_STAMP_DOWN,
		SCR_STAMP_LIST,
		GRD_STAMP_LIST,
		OBJ_POST_BLOCK,
		BTN_RECONNECT,
		IPT_POST,
		OBJ_CHAT_INPUT,
		LBL_CONNECTION_STATUS,
		WGT_DONATE_ROOT,
		SCR_DONATE,
		GRD_DONATE,
		LBL_DONATE_NUM,
		LBL_DONATE_MAX,
		BTN_DONATE_CHAT,
		PNL_MATERIAL_INFO,
		LBL_NO_DONATE,
		LBL_USER_NAME,
		OBJ_TARGET,
		OBJ_OWNER,
		LBL_CHAT_MESSAGE,
		LBL_MATERIAL_NAME,
		SLD_PROGRESS,
		OBJ_FULL,
		OBJ_NORMAL,
		OBJ_MATERIAL_ICON,
		LBL_QUATITY,
		BTN_GIFT,
		BTN_ASK,
		BTN_GUILD_SETTING,
		TEX_MODEL,
		SPR_BADGE,
		SPR_ICON_NEW
	}

	private enum CHAT_TYPE
	{
		CLAN,
		MEMBER
	}

	public enum VIEW_TYPE
	{
		CHAT,
		DONATE
	}

	public enum ClanStatusType
	{
		DISBAND = 1,
		DONATION,
		PIN,
		KICK
	}

	public enum ClanDonationStatus
	{
		NEW = 1,
		DELETE,
		UPDATE
	}

	public const string EVENT_AGE_CONFIRM = "CHAT_AGE_CONFIRM";

	private const int CHAT_ITEM_OFFSET = 22;

	private const int FORCE_SCROLL_LIMIT = 32;

	private const float SOFTNESS_HEIGHT = 10f;

	private const float SPRING_STRENGTH = 20f;

	private const float CHAT_WIDTH = 410f;

	private const int ITEM_COUNT_MAX = 30;

	private static readonly string STAMP_SYMBOL_BEGIN = "[STMP]";

	private TouchScreenKeyboard m_Keyboard;

	private CHAT_TYPE _chatType;

	private VIEW_TYPE _viewType;

	private GameObject m_ChatPinItemPrefab;

	private GameObject m_ChatItemPrefab;

	private GameObject m_ChatStampListPrefab;

	private GameObject m_ChatAdvisaryItemPrefab;

	private GameObject m_DonatePinItemPrefab;

	private ChatItemListData[] m_DataList = new ChatItemListData[Enum.GetNames(typeof(CHAT_TYPE)).Length];

	private PostRequestQueue[] m_PostRequestQueue = new PostRequestQueue[Enum.GetNames(typeof(CHAT_TYPE)).Length];

	private Dictionary<int, int> _updatedTime = new Dictionary<int, int>();

	private UIScrollView m_ScrollView;

	private Transform m_ScrollViewTrans;

	private UIWidget m_DummyDragScroll;

	private BoxCollider m_DragScrollCollider;

	private Transform m_DragScrollTrans;

	private UIRect m_RootRect;

	private UIInput m_Input;

	private List<int> m_StampIdListCanPost;

	private List<ChatTabData> m_ChatTabListData = new List<ChatTabData>();

	private List<ChatPostRequest> m_ChatLogListData = new List<ChatPostRequest>();

	private Dictionary<string, List<ChatPostRequest>> m_MemberLogs = new Dictionary<string, List<ChatPostRequest>>();

	private Transform[] m_ObjRoot = (Transform[])new Transform[Enum.GetNames(typeof(VIEW_TYPE)).Length];

	private Vector4 baseClipRegion;

	private Vector4 currentClipRegion;

	private bool need_update_pin;

	private bool need_update_donate;

	private bool need_update_donate_later;

	private ClanChatLogMessageData pinMessage;

	private CharaInfo senerInfo;

	private GuildChatPinItem chatPinItem;

	private DonateInfo pinDonate;

	private GuildDonatePinItem donatePinItem;

	private Vector4 baseDonateClipRegion;

	private GuildChatAdvisoryItem chatAdvisoryItem;

	private ClanAdvisaryData _advisaryData;

	public override string overrideBackKeyEvent => "CLOSE";

	private ChatItemListData CurrentData => m_DataList[(int)_chatType];

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

	public override void Initialize()
	{
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveText += OnReceiveClanText;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveStamp += OnReceiveClanStamp;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceivePrivateText += OnReceiveClanPrivateText;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceivePrivateStamp += OnReceiveClanPrivateStamp;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveNotification += OnReceiveClanNotification;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveUpdateStatus += OnReceiveUpdateStatus;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onJoin += OnJoinClanChat;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onDisconnect += OnDisconnectClanChat;
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onLeave += OnLeaveClanChat;
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		_chatType = ((MonoBehaviourSingleton<GuildManager>.I.talkUser != null) ? CHAT_TYPE.MEMBER : CHAT_TYPE.CLAN);
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatItem", false);
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem", false);
		LoadObject lo_quest_chatpinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatPinItem", false);
		LoadObject lo_chatAdvisaryItem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem", false);
		LoadObject lo_quest_donatepinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildDonatePinItem", false);
		bool finish_chat_log = false;
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatLog(new Action<bool, GuildChatModel>((object)/*Error near IL_00e4: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		bool finish_donate_list = false;
		MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator65)/*Error near IL_0101: stateMachine*/)._003Cfinish_donate_list_003E__7 = true;
		});
		bool finish_log_member = true;
		if (_chatType == CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
		{
			finish_log_member = false;
			MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, new Action<bool, GuildPrivateChatModel>((object)/*Error near IL_0154: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		while (!finish_chat_log || !finish_donate_list || !finish_log_member)
		{
			yield return (object)null;
		}
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		m_DataList[0] = new ChatItemListData(GetCtrl(UI.OBJ_CLAN_ITEM_LIST_ROOT).get_gameObject());
		m_DataList[1] = new ChatItemListData(GetCtrl(UI.OBJ_MEMBER_ITEM_LIST_ROOT).get_gameObject());
		m_ObjRoot[0] = GetCtrl(UI.OBJ_CHAT_PANEL);
		m_ObjRoot[1] = GetCtrl(UI.OBJ_DONATE_PANEL);
		for (int i = 0; i < m_PostRequestQueue.Length; i++)
		{
			m_PostRequestQueue[i] = new PostRequestQueue();
		}
		m_ChatItemPrefab = (lo_quest_chatitem.loadedObject as GameObject);
		m_ChatStampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		m_ChatPinItemPrefab = (lo_quest_chatpinitem.loadedObject as GameObject);
		m_ChatAdvisaryItemPrefab = (lo_chatAdvisaryItem.loadedObject as GameObject);
		m_DonatePinItemPrefab = (lo_quest_donatepinitem.loadedObject as GameObject);
		DummyDragScroll.width = 410;
		InitStampList();
		SetActive((Enum)UI.OBJ_STAMP_UP, false);
		SetActive((Enum)UI.OBJ_STAMP_DOWN, true);
		object event_data = GameSection.GetEventData();
		if (event_data != null && event_data is VIEW_TYPE)
		{
			_viewType = (VIEW_TYPE)(int)event_data;
		}
		bool waitToGetMember = true;
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, new Action<bool, GuildMemberListModel>((object)/*Error near IL_03c6: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (waitToGetMember)
		{
			yield return (object)null;
		}
		UIPanel chatPanel = ScrollView.get_gameObject().GetComponent<UIPanel>();
		baseClipRegion = chatPanel.baseClipRegion;
		currentClipRegion = chatPanel.baseClipRegion;
		if (_chatType == CHAT_TYPE.CLAN)
		{
			if (_advisaryData != null)
			{
				this.StartCoroutine(AddAdvisary());
			}
			if (pinMessage != null)
			{
				this.StartCoroutine(AddChatPinMsg());
			}
		}
		base.Initialize();
	}

	public override void InitializeReopen()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (_viewType == VIEW_TYPE.CHAT)
		{
			this.StartCoroutine(DoInitializeReopen());
		}
		else
		{
			base.InitializeReopen();
		}
	}

	private unsafe IEnumerator DoInitializeReopen()
	{
		_chatType = ((MonoBehaviourSingleton<GuildManager>.I.talkUser != null) ? CHAT_TYPE.MEMBER : CHAT_TYPE.CLAN);
		bool finish_log_member = true;
		if (_chatType == CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
		{
			finish_log_member = false;
			MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, new Action<bool, GuildPrivateChatModel>((object)/*Error near IL_0084: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		while (!finish_log_member)
		{
			yield return (object)null;
		}
		base.InitializeReopen();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveText -= OnReceiveClanText;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveStamp -= OnReceiveClanStamp;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceivePrivateText -= OnReceiveClanPrivateText;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceivePrivateStamp -= OnReceiveClanPrivateStamp;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveNotification -= OnReceiveClanNotification;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveUpdateStatus -= OnReceiveUpdateStatus;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onJoin -= OnJoinClanChat;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onDisconnect -= OnDisconnectClanChat;
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onLeave -= OnLeaveClanChat;
		}
	}

	private void Update()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (base.isInitialized)
		{
			Vector4 val = ScrollView.panel.baseClipRegion;
			float w = val.w;
			Vector4 val2 = ScrollView.panel.baseClipRegion;
			float num = w - val2.y;
			Vector3 localPosition = DragScrollTrans.get_localPosition();
			float num2 = num + localPosition.y;
			Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
			float w2 = finalClipRegion.w;
			Vector2 clipOffset = ScrollView.panel.clipOffset;
			float num3 = num2 - (w2 + clipOffset.y);
			object dragScrollCollider = (object)DragScrollCollider;
			Vector4 val3 = ScrollView.panel.baseClipRegion;
			dragScrollCollider.set_center(Vector2.op_Implicit(new Vector2(val3.x, 0f - num3)));
		}
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_CHAT_PANEL, _viewType == VIEW_TYPE.CHAT);
		SetActive((Enum)UI.OBJ_DONATE_PANEL, _viewType == VIEW_TYPE.DONATE);
		if (_viewType == VIEW_TYPE.CHAT)
		{
			UpdateChat();
		}
		else
		{
			SetActive((Enum)UI.LBL_NO_DONATE, false);
			if (!GameSceneEvent.IsStay())
			{
				GameSceneEvent.Stay();
			}
			MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate
			{
				UpdateDonate();
				GameSection.ResumeEvent(true, null);
			});
		}
		UpdateClanBadge();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus && _viewType == VIEW_TYPE.DONATE)
		{
			this.StopCoroutine(ShowDisableState());
			if (!GameSceneEvent.IsStay())
			{
				GameSceneEvent.Stay();
			}
			MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate
			{
				UpdateDonate();
				GameSection.ResumeEvent(true, null);
			});
		}
	}

	private void UpdateClanBadge()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guilMemberList != null)
		{
			bool is_visible = MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters != null && MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters.Count > 0;
			SetActive(FindCtrl(base._transform, UI.BTN_MEMBER), UI.SPR_BADGE, is_visible);
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD_SETTING), UI.SPR_BADGE, is_visible);
		}
		else
		{
			SetActive(FindCtrl(base._transform, UI.BTN_MEMBER), UI.SPR_BADGE, false);
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD_SETTING), UI.SPR_BADGE, false);
		}
	}

	protected virtual void LateUpdate()
	{
		ClanUpdateStatus();
		if (IsValidDispatchEventInUpdate() && !GameSaveData.instance.isShowChatOfferBanner && MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId)
		{
			DispatchEvent("BANNER_CLANCHATOFFER", null);
			GameSaveData.instance.isShowChatOfferBanner = true;
		}
	}

	private bool IsValidDispatchEventInUpdate()
	{
		if (!HomeSelfCharacter.CTRL)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage())
		{
			return false;
		}
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			return false;
		}
		return true;
	}

	private void UpdateChat()
	{
		bool hasConnect = MonoBehaviourSingleton<ChatManager>.I.clanChat.HasConnect;
		SetActive((Enum)UI.OBJ_POST_BLOCK, !hasConnect);
		SetActive((Enum)UI.BTN_CHAT, !hasConnect);
		SetActive((Enum)UI.OBJ_CHAT_INPUT, hasConnect);
		SetLabelText((Enum)UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_DISCONNECT"));
		SetButtonEvent((Enum)UI.BTN_CHAT, new EventDelegate(delegate
		{
			SetActive((Enum)UI.OBJ_STAMP_UP, true);
			SetActive((Enum)UI.OBJ_STAMP_DOWN, false);
		}));
		SetButtonEvent((Enum)UI.BTN_RECONNECT, new EventDelegate(delegate
		{
			SetLabelText((Enum)UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_CONNECTING"));
			if (!MonoBehaviourSingleton<ChatManager>.I.clanChat.IsConnecting)
			{
				MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(MonoBehaviourSingleton<GuildManager>.I.guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate(bool success)
				{
					if (success)
					{
						SetActive((Enum)UI.OBJ_POST_BLOCK, false);
						SetActive((Enum)UI.BTN_CHAT, false);
						SetActive((Enum)UI.OBJ_CHAT_INPUT, true);
					}
				});
			}
		}));
		SetButtonEvent((Enum)UI.BTN_TAB_GUILD, new EventDelegate(delegate
		{
			if (_chatType != 0)
			{
				MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();
				_chatType = CHAT_TYPE.CLAN;
				RefreshUI();
			}
		}));
		SetButtonEvent((Enum)UI.BTN_STAMP_UP, new EventDelegate(delegate
		{
			SetActive((Enum)UI.OBJ_STAMP_UP, true);
			SetActive((Enum)UI.OBJ_STAMP_DOWN, false);
		}));
		SetButtonEvent((Enum)UI.BTN_STAMP_DOWN, new EventDelegate(delegate
		{
			SetActive((Enum)UI.OBJ_STAMP_UP, false);
			SetActive((Enum)UI.OBJ_STAMP_DOWN, true);
		}));
		SetInputSubmitEvent((Enum)UI.IPT_POST, new EventDelegate(delegate
		{
			OnTouchPost();
			SetInputValue((Enum)UI.IPT_POST, string.Empty);
		}));
		UpdateChatLog();
		UpdateTabChat();
		UpdateCurrentData();
		UpdateAdvisoryItem();
		UpdateChatPin();
	}

	private void UpdateChatLog()
	{
		m_DataList[(int)_chatType].Reset();
		if (_chatType == CHAT_TYPE.MEMBER)
		{
			if (MonoBehaviourSingleton<GuildManager>.I.talkUser == null)
			{
				MonoBehaviourSingleton<GuildManager>.I.UpdateTalkUser();
			}
			if (MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
			{
				List<ChatPostRequest> memberChatLog = GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
				if (memberChatLog != null)
				{
					for (int i = 0; i < memberChatLog.Count; i++)
					{
						Post(memberChatLog[i]);
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < m_ChatLogListData.Count; j++)
			{
				Post(m_ChatLogListData[j]);
			}
		}
	}

	private void UpdateCurrentData()
	{
		SetActive((Enum)UI.OBJ_MEMBER_ITEM_LIST_ROOT, _chatType == CHAT_TYPE.MEMBER);
		SetActive((Enum)UI.OBJ_CLAN_ITEM_LIST_ROOT, _chatType == CHAT_TYPE.CLAN);
	}

	private unsafe void UpdateTabChat()
	{
		m_ChatTabListData.Clear();
		SetGrid(UI.GRD_TAB_CHAT, "GuildMessageTabListItem", MonoBehaviourSingleton<GuildManager>.I.talkUsers.Count, true, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		ScrollViewResetPosition((Enum)UI.SCR_TAB_CHAT);
	}

	private void AddClanChatLog(List<ClanChatLogMessageData> datas)
	{
		m_ChatLogListData.Clear();
		int num = (datas.Count <= 1000) ? datas.Count : 1000;
		for (int i = 0; i < num; i++)
		{
			ClanChatLogMessageData clanChatLogMessageData = datas[i];
			string[] array = clanChatLogMessageData.message.Split(':');
			string sName = array[0];
			string text = array[1];
			if (clanChatLogMessageData.fromUserId == 0)
			{
				m_ChatLogListData.Add(new ChatPostRequest(text));
			}
			else if (text.Contains(STAMP_SYMBOL_BEGIN))
			{
				string s = text.Substring(STAMP_SYMBOL_BEGIN.Length, 8);
				int result = -1;
				int.TryParse(s, out result);
				m_ChatLogListData.Add(new ChatPostRequest(clanChatLogMessageData.uuid, clanChatLogMessageData.id, clanChatLogMessageData.toUserId, clanChatLogMessageData.fromUserId, sName, result));
			}
			else
			{
				m_ChatLogListData.Add(new ChatPostRequest(clanChatLogMessageData.uuid, clanChatLogMessageData.id, clanChatLogMessageData.toUserId, clanChatLogMessageData.fromUserId, sName, text));
			}
		}
	}

	private void ResetMemberChatLog(int userId)
	{
		GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, userId)?.Clear();
	}

	private List<ChatPostRequest> GetMemberChatLog(int receiveId, int senderId)
	{
		string key = $"{receiveId}_{senderId}";
		string key2 = $"{senderId}_{receiveId}";
		if (m_MemberLogs.ContainsKey(key))
		{
			return m_MemberLogs[key];
		}
		if (m_MemberLogs.ContainsKey(key2))
		{
			return m_MemberLogs[key2];
		}
		return null;
	}

	private void AddMemberChatLog(int userId, List<ClanChatLogMessageData> datas)
	{
		List<ChatPostRequest> list = GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, userId);
		if (list == null)
		{
			list = new List<ChatPostRequest>();
			m_MemberLogs.Add($"{MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id}_{userId}", list);
		}
		list.Clear();
		for (int i = 0; i < datas.Count; i++)
		{
			ClanChatLogMessageData clanChatLogMessageData = datas[i];
			string[] array = clanChatLogMessageData.message.Split(':');
			string sName = array[0];
			string text = array[1];
			if (text.Contains(STAMP_SYMBOL_BEGIN))
			{
				string s = text.Substring(STAMP_SYMBOL_BEGIN.Length, 8);
				int result = -1;
				int.TryParse(s, out result);
				list.Add(new ChatPostRequest(clanChatLogMessageData.uuid, clanChatLogMessageData.id, clanChatLogMessageData.toUserId, clanChatLogMessageData.fromUserId, sName, result));
			}
			else
			{
				list.Add(new ChatPostRequest(clanChatLogMessageData.uuid, clanChatLogMessageData.id, clanChatLogMessageData.toUserId, clanChatLogMessageData.fromUserId, sName, text));
			}
		}
	}

	private void AddMemberChatLog(string uuId, int chatId, int receiveId, int senderId, string senderName, string message)
	{
		List<ChatPostRequest> list = GetMemberChatLog(receiveId, senderId);
		if (list == null)
		{
			list = new List<ChatPostRequest>();
			m_MemberLogs.Add($"{receiveId}_{senderId}", list);
		}
		list.Add(new ChatPostRequest(uuId, chatId, receiveId, senderId, senderName, message));
		int userId = (receiveId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? receiveId : senderId;
		if (MonoBehaviourSingleton<GuildManager>.I.AddTalkUser(userId) && _viewType == VIEW_TYPE.CHAT)
		{
			UpdateTabChat();
		}
	}

	private void AddMemberChatLog(string uuId, int chatId, int receiveId, int senderId, string senderName, int stampId)
	{
		List<ChatPostRequest> list = GetMemberChatLog(receiveId, senderId);
		if (list == null)
		{
			list = new List<ChatPostRequest>();
			m_MemberLogs.Add($"{receiveId}_{senderId}", list);
		}
		list.Add(new ChatPostRequest(uuId, chatId, receiveId, senderId, senderName, stampId));
		int userId = (receiveId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? receiveId : senderId;
		if (MonoBehaviourSingleton<GuildManager>.I.AddTalkUser(userId) && _viewType == VIEW_TYPE.CHAT)
		{
			UpdateTabChat();
		}
	}

	private void OnQuery_CLOSE_TAB()
	{
		FriendCharaInfo friendCharaInfo = GameSection.GetEventData() as FriendCharaInfo;
		GameSection.StayEvent();
		if (MonoBehaviourSingleton<GuildManager>.I.talkUser != null && friendCharaInfo.userId == MonoBehaviourSingleton<GuildManager>.I.talkUser.userId)
		{
			MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();
			MonoBehaviourSingleton<GuildManager>.I.RemoveTalkUser(friendCharaInfo);
			MonoBehaviourSingleton<GuildManager>.I.UpdateTalkUser();
		}
		else
		{
			MonoBehaviourSingleton<GuildManager>.I.RemoveTalkUser(friendCharaInfo);
		}
		_chatType = ((MonoBehaviourSingleton<GuildManager>.I.talkUser != null) ? CHAT_TYPE.MEMBER : CHAT_TYPE.CLAN);
		GameSection.ResumeEvent(false, null);
		RefreshUI();
	}

	private unsafe void OnQuery_TAB()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		FriendCharaInfo friendCharaInfo = MonoBehaviourSingleton<GuildManager>.I.talkUsers[index];
		if (MonoBehaviourSingleton<GuildManager>.I.talkUser != null && friendCharaInfo.userId == MonoBehaviourSingleton<GuildManager>.I.talkUser.userId)
		{
			GameSection.ResumeEvent(false, null);
		}
		else
		{
			MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(friendCharaInfo);
			_chatType = CHAT_TYPE.MEMBER;
			List<ChatPostRequest> memberChatLog = GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
			if (memberChatLog != null && memberChatLog.Count > 0)
			{
				GameSection.ResumeEvent(false, null);
				RefreshUI();
			}
			else
			{
				MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, new Action<bool, GuildPrivateChatModel>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}

	private void OnReceiveClanText(ClanChatLogMessageData clanChatMsgData)
	{
		if (IsAllowedUser(clanChatMsgData.fromUserId))
		{
			ChatPostRequest chatPostRequest = new ChatPostRequest(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.message);
			m_ChatLogListData.Add(chatPostRequest);
			if (_chatType == CHAT_TYPE.CLAN)
			{
				Post(chatPostRequest);
			}
		}
	}

	private void OnReceiveClanStamp(ClanChatLogMessageData clanChatMsgData)
	{
		if (IsAllowedUser(clanChatMsgData.fromUserId))
		{
			ChatPostRequest chatPostRequest = new ChatPostRequest(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.stampId);
			m_ChatLogListData.Add(chatPostRequest);
			if (_chatType == CHAT_TYPE.CLAN)
			{
				Post(chatPostRequest);
			}
		}
	}

	private void OnReceiveClanPrivateText(ClanChatLogMessageData clanChatMsgData)
	{
		if (IsAllowedUser(clanChatMsgData.fromUserId))
		{
			AddMemberChatLog(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.message);
			if (_chatType == CHAT_TYPE.MEMBER)
			{
				RefreshUI();
			}
			else
			{
				foreach (ChatTabData chatTabListDatum in m_ChatTabListData)
				{
					if (chatTabListDatum.info.userId == clanChatMsgData.fromUserId)
					{
						SetBadge(chatTabListDatum.tran, -1, 1, 5, 5, false);
						break;
					}
				}
			}
		}
	}

	private void OnReceiveClanPrivateStamp(ClanChatLogMessageData clanChatMsgData)
	{
		if (IsAllowedUser(clanChatMsgData.fromUserId))
		{
			AddMemberChatLog(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.stampId);
			if (_chatType == CHAT_TYPE.MEMBER)
			{
				RefreshUI();
			}
			else
			{
				foreach (ChatTabData chatTabListDatum in m_ChatTabListData)
				{
					if (chatTabListDatum.info.userId == clanChatMsgData.fromUserId)
					{
						SetBadge(chatTabListDatum.tran, -1, 1, 5, 5, false);
						break;
					}
				}
			}
		}
	}

	private void OnReceiveClanNotification(string message)
	{
		ChatPostRequest chatPostRequest = new ChatPostRequest(message);
		m_ChatLogListData.Add(chatPostRequest);
		if (_chatType == CHAT_TYPE.CLAN)
		{
			Post(chatPostRequest);
		}
	}

	private void OnJoinClanChat(CHAT_ERROR_TYPE errorType, string userId)
	{
		if (errorType != 0)
		{
			OnError(StringTable.Get(STRING_CATEGORY.CHAT_ERROR, 2u));
		}
		else
		{
			int result = -9999;
			int.TryParse(userId, out result);
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == result)
			{
				SetActive((Enum)UI.OBJ_POST_BLOCK, false);
				SetActive((Enum)UI.BTN_CHAT, false);
				SetActive((Enum)UI.OBJ_CHAT_INPUT, true);
			}
		}
	}

	private void OnDisconnectClanChat()
	{
		SetLabelText((Enum)UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_DISCONNECT"));
		SetActive((Enum)UI.OBJ_POST_BLOCK, true);
		SetActive((Enum)UI.BTN_CHAT, true);
		SetActive((Enum)UI.OBJ_CHAT_INPUT, false);
	}

	private void OnLeaveClanChat(CHAT_ERROR_TYPE errorType, string userId)
	{
		SetLabelText((Enum)UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_DISCONNECT"));
		SetActive((Enum)UI.OBJ_POST_BLOCK, true);
		SetActive((Enum)UI.BTN_CHAT, true);
		SetActive((Enum)UI.OBJ_CHAT_INPUT, false);
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
		int count = m_StampIdListCanPost.Count;
		SetGrid(create_item_func: new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), grid_ctrl_enum: UI.GRD_STAMP_LIST, item_prefab_name: null, item_num: count, reset: true, item_init_func: new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		base.SetEnabled<UIScrollView>((Enum)UI.SCR_STAMP_LIST, true);
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
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		if (m_StampIdListCanPost != null)
		{
			int stampId = m_StampIdListCanPost[index];
			ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
			item.Init(stampId);
			if (!isRecycle)
			{
				ChatStampListItem chatStampListItem = item;
				_003CInitStampItem_003Ec__AnonStorey34E _003CInitStampItem_003Ec__AnonStorey34E;
				chatStampListItem.onButton = Delegate.Combine((Delegate)chatStampListItem.onButton, (Delegate)new Action((object)_003CInitStampItem_003Ec__AnonStorey34E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}

	public void OnTouchPost()
	{
		if (UserInfoManager.IsRegisterdAge() && UserInfoManager.IsEnableCommunication())
		{
			string value = Input.value;
			if (!string.IsNullOrEmpty(value) && value.Trim().Length != 0)
			{
				SendMessageAsMine(value);
			}
		}
	}

	public void SendMessageAsMine(string message)
	{
		if (_chatType == CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
		{
			MonoBehaviourSingleton<ChatManager>.I.clanChat.SendPrivateMessage(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId.ToString(), message);
		}
		else
		{
			MonoBehaviourSingleton<ChatManager>.I.clanChat.SendMessage(message);
		}
	}

	public void SendStampAsMine(int stampId)
	{
		if (CanIPostTheStamp(stampId))
		{
			SetActive((Enum)UI.OBJ_STAMP_UP, false);
			SetActive((Enum)UI.OBJ_STAMP_DOWN, true);
			if (_chatType == CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.clanChat.SendPrivateStamp(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId.ToString(), stampId);
			}
			else
			{
				MonoBehaviourSingleton<ChatManager>.I.clanChat.SendStamp(stampId);
			}
		}
	}

	private void Post(ChatPostRequest request)
	{
		ChatItemListData data = m_DataList[(int)_chatType];
		switch (request.Type)
		{
		case ChatPostRequest.TYPE.Message:
			Post(request.uuId, request.chatId, request.senderId, request.senderName, request.message, data);
			break;
		case ChatPostRequest.TYPE.Stamp:
			PostStamp(request.senderId, request.senderName, request.stampId, data);
			break;
		case ChatPostRequest.TYPE.Notification:
			PostNotification(request.message, data);
			break;
		}
	}

	private void Post(string uuid, int chatId, int userId, string userName, string text, ChatItemListData data)
	{
		AddNextChatItem(data, delegate(GuildChatItem chatItem)
		{
			chatItem.Init(uuid, chatId, userId, userName, text);
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
				AddNextChatItem(data, delegate(GuildChatItem chatItem)
				{
					chatItem.Init(string.Empty, 0, userId, userName, stampId);
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
		AddNextChatItem(data, delegate(GuildChatItem chatItem)
		{
			chatItem.Init(text);
		});
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP, 1f);
	}

	private unsafe void AddNextChatItem(ChatItemListData data, Action<GuildChatItem> initializer)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_ChatItemPrefab == null))
		{
			if (data.itemList.Count > 0)
			{
				data.currentTotalHeight += 22f;
			}
			GuildChatItem guildChatItem = null;
			if (data.itemList.Count < 30)
			{
				guildChatItem = ResourceUtility.Realizes(m_ChatItemPrefab, data.rootObject.get_transform(), 5).GetComponent<GuildChatItem>();
			}
			else
			{
				guildChatItem = data.itemList[data.oldestItemIndex];
				data.oldestItemIndex++;
				if (data.oldestItemIndex == 30)
				{
					data.oldestItemIndex = 0;
				}
				data.currentTotalHeight -= guildChatItem.height + 22f;
				ScrollView.panel.widgetsAreStatic = false;
				data.MoveAll(guildChatItem.height + 22f);
				AppMain i = MonoBehaviourSingleton<AppMain>.I;
				i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			float currentTotalHeight = data.currentTotalHeight;
			guildChatItem.get_transform().set_localPosition(new Vector3(-15f, 0f - currentTotalHeight, 0f));
			initializer(guildChatItem);
			data.currentTotalHeight += guildChatItem.height;
			UpdateDummyDragScroll();
			float currentTotalHeight2 = data.currentTotalHeight;
			Vector4 val = ScrollView.panel.baseClipRegion;
			float num = currentTotalHeight2 + val.y;
			Vector4 val2 = ScrollView.panel.baseClipRegion;
			float num2 = num - val2.w * 0.5f;
			Vector3 localPosition = ScrollViewTrans.get_localPosition();
			float y = localPosition.y;
			Vector2 clipOffset = ScrollView.panel.clipOffset;
			float num3 = num2 + (y + clipOffset.y);
			Vector2 clipSoftness = ScrollView.panel.clipSoftness;
			float num4 = num3 + clipSoftness.y;
			if (data.itemList.Count >= 30)
			{
				ForceScroll(num4 - guildChatItem.height - 22f, false);
			}
			ForceScroll(num4, true);
			if (data.itemList.Count < 30)
			{
				data.itemList.Add(guildChatItem);
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

	private bool IsAllowedUser(int userId)
	{
		if (MonoBehaviourSingleton<BlackListManager>.IsValid())
		{
			return !MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(userId);
		}
		return true;
	}

	private unsafe void UpdateDonate()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		pinDonate = MonoBehaviourSingleton<GuildManager>.I.pinDonate;
		if (pinDonate != null)
		{
			this.StartCoroutine(AddDonatePin(pinDonate));
		}
		SetButtonEvent((Enum)UI.BTN_DONATE_CHAT, new EventDelegate(delegate
		{
			_viewType = VIEW_TYPE.CHAT;
			RefreshUI();
		}));
		List<DonateInfo> donate_list = MonoBehaviourSingleton<GuildManager>.I.donateList;
		_003CUpdateDonate_003Ec__AnonStorey352 _003CUpdateDonate_003Ec__AnonStorey;
		SetGrid(UI.GRD_DONATE, "GuildMessageDonateListItem", donate_list.Count, true, new Action<int, Transform, bool>((object)_003CUpdateDonate_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetActive((Enum)UI.LBL_NO_DONATE, donate_list.Count == 0);
	}

	private void OnQuery_DONATE()
	{
		_viewType = VIEW_TYPE.DONATE;
		RefreshUI();
	}

	private void OnCloseDialog_GuildDonateSendDialog()
	{
		RefreshUI();
	}

	private void OnQuery_RETURN()
	{
		_viewType = VIEW_TYPE.CHAT;
		RefreshUI();
	}

	private void SetItemIcon(Transform holder, ItemSortData data, Transform parent_scroll, int event_data)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int icon_id = -1;
		int num = -1;
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
			num = data.GetNum();
			if (num == 1)
			{
				num = -1;
			}
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		}
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(data.GetTableID());
			enemy_icon_id = itemData.enemyIconID;
		}
		ItemIcon itemIcon = null;
		if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = data.GetIconType();
			itemIconCreateParam.icon_id = data.GetIconID();
			itemIconCreateParam.rarity = data.GetRarity();
			itemIconCreateParam.parent = holder;
			itemIconCreateParam.element = data.GetIconElement();
			itemIconCreateParam.magi_enable_equip_type = data.GetIconMagiEnableType();
			itemIconCreateParam.num = data.GetNum();
			itemIconCreateParam.enemy_icon_id = enemy_icon_id;
			itemIconCreateParam.questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST;
			itemIcon = ItemIcon.Create(itemIconCreateParam);
		}
		else
		{
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, -1, "DROP", event_data, is_new, -1, false, null, false, enemy_icon_id, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		}
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
	}

	private void OnQuery_DROP()
	{
		int index = (int)GameSection.GetEventData();
		DonateInfo donateInfo = MonoBehaviourSingleton<GuildManager>.I.donateList[index];
		uint itemId = (uint)donateInfo.itemId;
		ItemSortData itemSortData = new ItemSortData();
		ItemInfo itemInfo = new ItemInfo();
		itemInfo.uniqueID = 0uL;
		itemInfo.tableID = itemId;
		itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
		itemInfo.num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemId);
		itemSortData.SetItem(itemInfo);
		GameSection.SetEventData(new object[2]
		{
			itemSortData,
			donateInfo.itemNum
		});
	}

	private void OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
	{
		if (clanUpdateStatusData.type != 1)
		{
			if (clanUpdateStatusData.type == 2)
			{
				if (clanUpdateStatusData.status == 2)
				{
					need_update_donate_later = true;
				}
				else
				{
					need_update_donate = true;
				}
			}
			else if (clanUpdateStatusData.type == 3)
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId)
				{
					need_update_pin = true;
				}
			}
			else if (clanUpdateStatusData.type != 4)
			{
				return;
			}
		}
	}

	private void ClanUpdateStatus()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (base.isInitialized)
		{
			if (need_update_pin)
			{
				RefreshClanPinData();
			}
			if (need_update_donate && _viewType == VIEW_TYPE.DONATE)
			{
				need_update_donate = false;
				RefreshUI();
			}
			else if (need_update_donate_later && _viewType == VIEW_TYPE.DONATE)
			{
				need_update_donate_later = false;
				this.StartCoroutine(ShowDisableState());
			}
		}
	}

	private IEnumerator ShowDisableState()
	{
		yield return (object)new WaitForSeconds(3f);
		RefreshUI();
	}

	private unsafe void RefreshClanPinData()
	{
		need_update_pin = false;
		bool stayEvent = false;
		if (!GameSceneEvent.IsStay())
		{
			stayEvent = true;
			GameSceneEvent.Stay();
		}
		_003CRefreshClanPinData_003Ec__AnonStorey354 _003CRefreshClanPinData_003Ec__AnonStorey;
		MonoBehaviourSingleton<GuildManager>.I.GetAllPinData(new Action<bool, GuildGetPinModel>((object)_003CRefreshClanPinData_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnReceiveClanChatUnPin()
	{
		RemovePinMsg();
	}

	private void UpdateChatPin()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		if (chatPinItem == null)
		{
			if (_chatType == CHAT_TYPE.CLAN && pinMessage != null)
			{
				this.StartCoroutine(AddChatPinMsg());
			}
		}
		else if (_chatType == CHAT_TYPE.MEMBER && chatPinItem.get_gameObject().get_activeSelf())
		{
			ScrollView.panel.baseClipRegion = baseClipRegion;
			chatPinItem.get_gameObject().SetActive(false);
		}
		else if (_chatType == CHAT_TYPE.CLAN && !chatPinItem.get_gameObject().get_activeSelf())
		{
			ScrollView.panel.baseClipRegion = currentClipRegion;
			chatPinItem.get_gameObject().SetActive(true);
		}
	}

	private void OnQuery_UNPIN()
	{
		chatPinItem.HideUnPinButton();
		DispatchEvent("UNPIN_MSG", "Are you sure you want to unpin this message?");
	}

	private unsafe void OnQuery_GuildUnPinMessageDialog_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatUnPin(new Action<bool, GuildChatUnPinModel>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void RemovePinMsg()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (!(chatPinItem == null))
		{
			ClearRenderModel(chatPinItem.get_transform(), UI.TEX_MODEL);
			Object.DestroyImmediate(chatPinItem.get_gameObject());
			chatPinItem = null;
			pinMessage = null;
			CalculateBaseClipScrollView();
		}
	}

	private void OnQuery_GuildUnPinMessageDialog_NO()
	{
		chatPinItem.HideUnPinButton();
	}

	private void OnQuery_PIN_MSG()
	{
		pinMessage = (GameSection.GetEventData() as ClanChatLogMessageData);
		if (chatPinItem != null)
		{
			DispatchEvent("REPLACE_PIN_MSG", "You already have a pinned message. Replace with this?");
		}
		else
		{
			SendPinMsg();
		}
	}

	private void OnQuery_GuildReplacePinMessageDialog_YES()
	{
		SendPinMsg();
	}

	private void OnQuery_GuildReplacePinMessageDialog_NO()
	{
		HidePinButton();
	}

	private unsafe void SendPinMsg()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatPin(pinMessage.fromUserId, pinMessage.id, pinMessage.uuid, pinMessage.type, pinMessage.message, new Action<bool, GuildChatPinModel>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private IEnumerator AddChatPinMsg()
	{
		if (senerInfo != null && !(m_ChatPinItemPrefab == null))
		{
			if (chatPinItem == null)
			{
				chatPinItem = ResourceUtility.Realizes(m_ChatPinItemPrefab, RootRect.get_transform(), 5).GetComponent<GuildChatPinItem>();
				chatPinItem.get_transform().set_localPosition(new Vector3(0f, 370f, 0f));
			}
			yield return (object)null;
			if (pinMessage.type == 0)
			{
				chatPinItem.ShowPinMsg(senerInfo.name, pinMessage.message);
			}
			else if (pinMessage.type == 1)
			{
				chatPinItem.ShowPinStamp(senerInfo.name, pinMessage.stampId);
			}
			CalculateBaseClipScrollView();
			ClearRenderModel(chatPinItem.get_transform(), UI.TEX_MODEL);
			yield return (object)null;
			SetRenderPlayerModel(chatPinItem.get_transform(), UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(senerInfo, false, true, false, true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), true, null);
		}
	}

	private void OnQuery_HIDE_PIN_BTN()
	{
		int result = 0;
		string s = GameSection.GetEventData() as string;
		int.TryParse(s, out result);
		int count = CurrentData.itemList.Count;
		for (int i = 0; i < count; i++)
		{
			if (CurrentData.itemList[i].msgId != result)
			{
				CurrentData.itemList[i].HidePinButton();
			}
		}
	}

	private void HidePinButton()
	{
		int count = CurrentData.itemList.Count;
		for (int i = 0; i < count; i++)
		{
			CurrentData.itemList[i].HidePinButton();
		}
	}

	private IEnumerator AddDonatePin(DonateInfo info)
	{
		if (!(m_DonatePinItemPrefab == null))
		{
			bool isFirstPin = donatePinItem == null;
			if (donatePinItem == null)
			{
				donatePinItem = ResourceUtility.Realizes(m_DonatePinItemPrefab, GetCtrl(UI.WGT_DONATE_ROOT).get_transform(), 5).GetComponent<GuildDonatePinItem>();
				donatePinItem.get_transform().set_localPosition(new Vector3(0f, 375f, 0f));
			}
			UpdateDonatePinUI(info);
			donatePinItem.ShowPin(info);
			UIPanel donatePanel = GetCtrl(UI.SCR_DONATE).GetComponent<UIScrollView>().get_gameObject().GetComponent<UIPanel>();
			int h = donatePinItem.GetBaseHeight + 30;
			if (isFirstPin)
			{
				baseDonateClipRegion = donatePanel.baseClipRegion;
			}
			donatePanel.baseClipRegion = new Vector4(baseDonateClipRegion.x, baseDonateClipRegion.y - (float)h / 2f, baseDonateClipRegion.z, baseDonateClipRegion.w - (float)h);
		}
		yield break;
	}

	private void UpdateDonatePinUI(DonateInfo info)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		Transform root = donatePinItem.get_transform();
		SetActive(root, UI.OBJ_TARGET, info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		SetActive(root, UI.OBJ_OWNER, info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		Transform val = (info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? FindCtrl(root, UI.OBJ_OWNER) : FindCtrl(root, UI.OBJ_TARGET);
		SetLabelText(root, UI.LBL_CHAT_MESSAGE, info.msg);
		bool flag = info.itemNum >= info.quantity;
		SetActive(val, UI.OBJ_FULL, flag);
		SetActive(val, UI.OBJ_NORMAL, !flag);
		SetSliderValue(val, UI.SLD_PROGRESS, (float)info.itemNum / (float)info.quantity);
		SetLabelText(val, UI.LBL_CHAT_MESSAGE, info.msg);
		SetLabelText(val, UI.LBL_USER_NAME, info.nickName);
		SetLabelText(val, UI.LBL_MATERIAL_NAME, info.materialName);
		int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1, false);
		SetLabelText(val, UI.LBL_QUATITY, itemNum);
		SetLabelText(val, UI.LBL_DONATE_NUM, info.itemNum);
		SetLabelText(val, UI.LBL_DONATE_MAX, info.quantity);
		if (info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			SetButtonEvent(val, UI.BTN_ASK, new EventDelegate(delegate
			{
				DispatchEvent("ASK", info);
			}));
		}
		else
		{
			int itemNum2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1, false);
			if (!flag && itemNum2 > 0 && info.itemNum < info.quantity)
			{
				SetButtonEvent(val, UI.BTN_GIFT, new EventDelegate(delegate
				{
					DispatchEvent("SEND", info);
				}));
			}
			else
			{
				SetButtonEnabled(val, UI.BTN_GIFT, false);
			}
		}
		Item item = new Item();
		item.uniqId = "0";
		item.itemId = info.itemId;
		item.num = info.itemNum;
		ItemInfo item2 = ItemInfo.CreateItemInfo(item);
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item2);
		SetItemIcon(FindCtrl(val, UI.OBJ_MATERIAL_ICON), itemSortData, FindCtrl(GetCtrl(UI.OBJ_DONATE_PANEL), UI.PNL_MATERIAL_INFO), 0);
	}

	private void OnQuery_PIN_DONATE()
	{
		pinDonate = (GameSection.GetEventData() as DonateInfo);
		if (donatePinItem != null)
		{
			DispatchEvent("REPLACE_PIN_DONATE", "This will replace your current pinned donate");
		}
		else
		{
			SendPinDonate(pinDonate);
		}
	}

	private void OnQuery_GuildReplacePinDonateDialog_YES()
	{
		SendPinDonate(pinDonate);
	}

	private void OnQuery_GuildReplacePinDonateDialog_NO()
	{
	}

	private unsafe void OnQuery_GuildUnPinDonateDialog_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatUnPin(new Action<bool, GuildChatUnPinModel>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnQuery_GuildUnPinDonateDialog_NO()
	{
	}

	private void OnQuery_UNPIN_DONATE_BTN()
	{
		DispatchEvent("UNPIN_DONATE", "Are you sure?");
	}

	private unsafe void SendPinDonate(DonateInfo info)
	{
		if (info != null && !(info.expired <= 0.0))
		{
			GameSection.StayEvent();
			_003CSendPinDonate_003Ec__AnonStorey356 _003CSendPinDonate_003Ec__AnonStorey;
			MonoBehaviourSingleton<GuildManager>.I.SendClanChatPin(0, info.id, string.Empty, 2, string.Empty, new Action<bool, GuildChatPinModel>((object)_003CSendPinDonate_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void UpdateAdvisoryItem()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (chatAdvisoryItem == null && _chatType == CHAT_TYPE.CLAN && _advisaryData != null)
		{
			this.StartCoroutine(AddAdvisary());
		}
	}

	private IEnumerator AddAdvisary()
	{
		if (_advisaryData != null && !(m_ChatAdvisaryItemPrefab == null) && !GuildChatAdvisoryItem.HasReadNew())
		{
			if (chatAdvisoryItem == null)
			{
				chatAdvisoryItem = ResourceUtility.Realizes(m_ChatAdvisaryItemPrefab, GetCtrl(UI.WGT_CHAT_TOP), 5).GetComponent<GuildChatAdvisoryItem>();
				chatAdvisoryItem.get_transform().set_localPosition(new Vector3(0f, 370f, 0f));
			}
			yield return (object)null;
			chatAdvisoryItem.Init(_advisaryData.title, _advisaryData.content);
			SetButtonEvent(chatAdvisoryItem.close, new EventDelegate(delegate
			{
				//IL_0026: Unknown result type (might be due to invalid IL or missing references)
				GuildChatAdvisoryItem.SetReadNew();
				if (((_003CAddAdvisary_003Ec__Iterator6A)/*Error near IL_0120: stateMachine*/)._003C_003Ef__this.chatAdvisoryItem != null)
				{
					Object.DestroyImmediate(((_003CAddAdvisary_003Ec__Iterator6A)/*Error near IL_0120: stateMachine*/)._003C_003Ef__this.chatAdvisoryItem.get_gameObject());
					((_003CAddAdvisary_003Ec__Iterator6A)/*Error near IL_0120: stateMachine*/)._003C_003Ef__this.chatAdvisoryItem = null;
				}
			}));
		}
	}

	private void CalculateBaseClipScrollView()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = default(Vector4);
		val._002Ector(baseClipRegion.x, baseClipRegion.y, baseClipRegion.z, baseClipRegion.w);
		if (chatPinItem != null && chatPinItem.get_gameObject().get_activeSelf())
		{
			float num = (float)chatPinItem.GetHeight - 15f;
			val._002Ector(val.x, val.y - num / 2f, val.z, val.w - num);
		}
		ScrollView.panel.baseClipRegion = val;
	}
}
