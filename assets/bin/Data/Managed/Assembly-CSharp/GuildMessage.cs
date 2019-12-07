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
		public GameObject rootObject;

		public List<GuildChatItem> itemList;

		public float currentTotalHeight;

		public int oldestItemIndex;

		public float slideOffset;

		private const float DEFAULT_OFFSET = -26f;

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

	private static readonly string STAMP_SYMBOL_BEGIN = "[STMP]";

	public const string EVENT_AGE_CONFIRM = "CHAT_AGE_CONFIRM";

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

	private const int CHAT_ITEM_OFFSET = 22;

	private const int FORCE_SCROLL_LIMIT = 32;

	private const float SOFTNESS_HEIGHT = 10f;

	private const float SPRING_STRENGTH = 20f;

	private const float CHAT_WIDTH = 410f;

	private const int ITEM_COUNT_MAX = 30;

	private List<int> m_StampIdListCanPost;

	private List<ChatTabData> m_ChatTabListData = new List<ChatTabData>();

	private List<ChatPostRequest> m_ChatLogListData = new List<ChatPostRequest>();

	private Dictionary<string, List<ChatPostRequest>> m_MemberLogs = new Dictionary<string, List<ChatPostRequest>>();

	private Transform[] m_ObjRoot = new Transform[Enum.GetNames(typeof(VIEW_TYPE)).Length];

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
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		_chatType = ((MonoBehaviourSingleton<GuildManager>.I.talkUser != null) ? CHAT_TYPE.MEMBER : CHAT_TYPE.CLAN);
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatItem");
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
		LoadObject lo_quest_chatpinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatPinItem");
		LoadObject lo_chatAdvisaryItem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem");
		LoadObject lo_quest_donatepinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildDonatePinItem");
		bool finish_chat_log = false;
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatLog(delegate(bool success, GuildChatModel ret)
		{
			finish_chat_log = true;
			AddClanChatLog(ret.result.array);
			if (ret.result.pin != null)
			{
				pinMessage = new ClanChatLogMessageData();
				pinMessage.fromUserId = ret.result.pin.fromUserId;
				pinMessage.id = ret.result.pin.id;
				pinMessage.type = ret.result.pin.type;
				pinMessage.message = ret.result.pin.message;
				pinMessage.uuid = ret.result.pin.uuid;
				if (pinMessage.type == 1)
				{
					pinMessage.stampId = int.Parse(ret.result.pin.message);
				}
				senerInfo = ret.result.pin.charInfo;
			}
			if (ret.result.advisory != null)
			{
				_advisaryData = ret.result.advisory;
			}
		});
		bool finish_donate_list = false;
		MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate
		{
			finish_donate_list = true;
		});
		bool finish_log_member = true;
		if (_chatType == CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
		{
			finish_log_member = false;
			MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, delegate(bool success, GuildPrivateChatModel ret)
			{
				AddMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, ret.result.array);
				finish_log_member = true;
			});
		}
		while (!finish_chat_log || !finish_donate_list || !finish_log_member)
		{
			yield return null;
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		m_DataList[0] = new ChatItemListData(GetCtrl(UI.OBJ_CLAN_ITEM_LIST_ROOT).gameObject);
		m_DataList[1] = new ChatItemListData(GetCtrl(UI.OBJ_MEMBER_ITEM_LIST_ROOT).gameObject);
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
		SetActive(UI.OBJ_STAMP_UP, is_visible: false);
		SetActive(UI.OBJ_STAMP_DOWN, is_visible: true);
		object eventData = GameSection.GetEventData();
		if (eventData != null && eventData is VIEW_TYPE)
		{
			_viewType = (VIEW_TYPE)eventData;
		}
		bool waitToGetMember = true;
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate
		{
			waitToGetMember = false;
		});
		while (waitToGetMember)
		{
			yield return null;
		}
		UIPanel component = ScrollView.gameObject.GetComponent<UIPanel>();
		baseClipRegion = component.baseClipRegion;
		currentClipRegion = component.baseClipRegion;
		if (_chatType == CHAT_TYPE.CLAN)
		{
			if (_advisaryData != null)
			{
				StartCoroutine(AddAdvisary());
			}
			if (pinMessage != null)
			{
				StartCoroutine(AddChatPinMsg());
			}
		}
		base.Initialize();
	}

	public override void InitializeReopen()
	{
		if (_viewType == VIEW_TYPE.CHAT)
		{
			StartCoroutine(DoInitializeReopen());
		}
		else
		{
			base.InitializeReopen();
		}
	}

	private IEnumerator DoInitializeReopen()
	{
		_chatType = ((MonoBehaviourSingleton<GuildManager>.I.talkUser != null) ? CHAT_TYPE.MEMBER : CHAT_TYPE.CLAN);
		bool finish_log_member = true;
		if (_chatType == CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
		{
			finish_log_member = false;
			MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, delegate(bool success, GuildPrivateChatModel ret)
			{
				ResetMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
				AddMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, ret.result.array);
				finish_log_member = true;
			});
		}
		while (!finish_log_member)
		{
			yield return null;
		}
		base.InitializeReopen();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		MonoBehaviourSingleton<ChatManager>.IsValid();
	}

	private void Update()
	{
		if (base.isInitialized)
		{
			float num = ScrollView.panel.baseClipRegion.w - ScrollView.panel.baseClipRegion.y + DragScrollTrans.localPosition.y - (ScrollView.panel.finalClipRegion.w + ScrollView.panel.clipOffset.y);
			DragScrollCollider.center = new Vector2(ScrollView.panel.baseClipRegion.x, 0f - num);
		}
	}

	public override void UpdateUI()
	{
		SetActive(UI.OBJ_CHAT_PANEL, _viewType == VIEW_TYPE.CHAT);
		SetActive(UI.OBJ_DONATE_PANEL, _viewType == VIEW_TYPE.DONATE);
		if (_viewType == VIEW_TYPE.CHAT)
		{
			UpdateChat();
		}
		else
		{
			SetActive(UI.LBL_NO_DONATE, is_visible: false);
			if (!GameSceneEvent.IsStay())
			{
				GameSceneEvent.Stay();
			}
			MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate
			{
				UpdateDonate();
				GameSection.ResumeEvent(is_resume: true);
			});
		}
		UpdateClanBadge();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus && _viewType == VIEW_TYPE.DONATE)
		{
			StopCoroutine(ShowDisableState());
			if (!GameSceneEvent.IsStay())
			{
				GameSceneEvent.Stay();
			}
			MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate
			{
				UpdateDonate();
				GameSection.ResumeEvent(is_resume: true);
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
			SetActive(FindCtrl(base._transform, UI.BTN_MEMBER), UI.SPR_BADGE, is_visible: false);
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD_SETTING), UI.SPR_BADGE, is_visible: false);
		}
	}

	protected virtual void LateUpdate()
	{
		ClanUpdateStatus();
		if (IsValidDispatchEventInUpdate() && !GameSaveData.instance.isShowChatOfferBanner && MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId)
		{
			DispatchEvent("BANNER_CLANCHATOFFER");
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
		bool flag = false;
		SetActive(UI.OBJ_POST_BLOCK, !flag);
		SetActive(UI.BTN_CHAT, !flag);
		SetActive(UI.OBJ_CHAT_INPUT, flag);
		SetLabelText(UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_DISCONNECT"));
		SetButtonEvent(UI.BTN_CHAT, new EventDelegate(delegate
		{
			SetActive(UI.OBJ_STAMP_UP, is_visible: true);
			SetActive(UI.OBJ_STAMP_DOWN, is_visible: false);
		}));
		SetButtonEvent(UI.BTN_RECONNECT, new EventDelegate(delegate
		{
			SetLabelText(UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_CONNECTING"));
		}));
		SetButtonEvent(UI.BTN_TAB_GUILD, new EventDelegate(delegate
		{
			if (_chatType != 0)
			{
				MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();
				_chatType = CHAT_TYPE.CLAN;
				RefreshUI();
			}
		}));
		SetButtonEvent(UI.BTN_STAMP_UP, new EventDelegate(delegate
		{
			SetActive(UI.OBJ_STAMP_UP, is_visible: true);
			SetActive(UI.OBJ_STAMP_DOWN, is_visible: false);
		}));
		SetButtonEvent(UI.BTN_STAMP_DOWN, new EventDelegate(delegate
		{
			SetActive(UI.OBJ_STAMP_UP, is_visible: false);
			SetActive(UI.OBJ_STAMP_DOWN, is_visible: true);
		}));
		SetInputSubmitEvent(UI.IPT_POST, new EventDelegate(delegate
		{
			OnTouchPost();
			SetInputValue(UI.IPT_POST, string.Empty);
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
			if (MonoBehaviourSingleton<GuildManager>.I.talkUser == null)
			{
				return;
			}
			List<ChatPostRequest> memberChatLog = GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
			if (memberChatLog != null)
			{
				for (int i = 0; i < memberChatLog.Count; i++)
				{
					Post(memberChatLog[i]);
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
		SetActive(UI.OBJ_MEMBER_ITEM_LIST_ROOT, _chatType == CHAT_TYPE.MEMBER);
		SetActive(UI.OBJ_CLAN_ITEM_LIST_ROOT, _chatType == CHAT_TYPE.CLAN);
	}

	private void UpdateTabChat()
	{
		m_ChatTabListData.Clear();
		SetGrid(UI.GRD_TAB_CHAT, "GuildMessageTabListItem", MonoBehaviourSingleton<GuildManager>.I.talkUsers.Count, reset: true, delegate(int i, Transform t, bool b)
		{
			FriendCharaInfo friendCharaInfo = MonoBehaviourSingleton<GuildManager>.I.talkUsers[i];
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != friendCharaInfo.userId)
			{
				SetLabelText(t, UI.LBL_TAB_NAME, friendCharaInfo.name);
				SetActive(t, UI.SPR_TAB_HIGHLIGHT, MonoBehaviourSingleton<GuildManager>.I.talkUser != null && MonoBehaviourSingleton<GuildManager>.I.talkUser.userId == friendCharaInfo.userId);
				SetEvent(t, UI.BTN_TAB_CLOSE, "CLOSE_TAB", friendCharaInfo);
				m_ChatTabListData.Add(new ChatTabData
				{
					tran = t,
					info = friendCharaInfo
				});
				SetEvent(t, "TAB", i);
			}
		});
		ScrollViewResetPosition(UI.SCR_TAB_CHAT);
	}

	private void AddClanChatLog(List<ClanChatLogMessageData> datas)
	{
		m_ChatLogListData.Clear();
		int num = (datas.Count > 1000) ? 1000 : datas.Count;
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
		int userId = (receiveId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? senderId : receiveId;
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
		int userId = (receiveId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? senderId : receiveId;
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
		GameSection.ResumeEvent(is_resume: false);
		RefreshUI();
	}

	private void OnQuery_TAB()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		FriendCharaInfo friendCharaInfo = MonoBehaviourSingleton<GuildManager>.I.talkUsers[index];
		if (MonoBehaviourSingleton<GuildManager>.I.talkUser != null && friendCharaInfo.userId == MonoBehaviourSingleton<GuildManager>.I.talkUser.userId)
		{
			GameSection.ResumeEvent(is_resume: false);
			return;
		}
		MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(friendCharaInfo);
		_chatType = CHAT_TYPE.MEMBER;
		List<ChatPostRequest> memberChatLog = GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
		if (memberChatLog != null && memberChatLog.Count > 0)
		{
			GameSection.ResumeEvent(is_resume: false);
			RefreshUI();
		}
		else
		{
			MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, delegate(bool success, GuildPrivateChatModel ret)
			{
				AddMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, ret.result.array);
				GameSection.ResumeEvent(is_resume: false);
				RefreshUI();
			});
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
						SetBadge(chatTabListDatum.tran, -1, SpriteAlignment.TopLeft);
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
						SetBadge(chatTabListDatum.tran, -1, SpriteAlignment.TopLeft);
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
			return;
		}
		int result = -9999;
		int.TryParse(userId, out result);
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == result)
		{
			SetActive(UI.OBJ_POST_BLOCK, is_visible: false);
			SetActive(UI.BTN_CHAT, is_visible: false);
			SetActive(UI.OBJ_CHAT_INPUT, is_visible: true);
		}
	}

	private void OnDisconnectClanChat()
	{
		SetLabelText(UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_DISCONNECT"));
		SetActive(UI.OBJ_POST_BLOCK, is_visible: true);
		SetActive(UI.BTN_CHAT, is_visible: true);
		SetActive(UI.OBJ_CHAT_INPUT, is_visible: false);
	}

	private void OnLeaveClanChat(CHAT_ERROR_TYPE errorType, string userId)
	{
		SetLabelText(UI.LBL_CONNECTION_STATUS, base.sectionData.GetText("TEXT_DISCONNECT"));
		SetActive(UI.OBJ_POST_BLOCK, is_visible: true);
		SetActive(UI.BTN_CHAT, is_visible: true);
		SetActive(UI.OBJ_CHAT_INPUT, is_visible: false);
	}

	private void OnError(string message)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, message, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u)), delegate
		{
		}, error: true);
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
		int count = m_StampIdListCanPost.Count;
		SetGrid(UI.GRD_STAMP_LIST, null, count, reset: true, CreateStampItem, InitStampItem);
		SetEnabled<UIScrollView>(UI.SCR_STAMP_LIST, is_enabled: true);
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
		if (m_StampIdListCanPost != null)
		{
			int stampId = m_StampIdListCanPost[index];
			ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
			item.Init(stampId);
			if (!isRecycle)
			{
				ChatStampListItem chatStampListItem = item;
				chatStampListItem.onButton = (Action)Delegate.Combine(chatStampListItem.onButton, (Action)delegate
				{
					SendStampAsMine(item.StampId);
				});
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
	}

	public void SendStampAsMine(int stampId)
	{
		if (CanIPostTheStamp(stampId))
		{
			SetActive(UI.OBJ_STAMP_UP, is_visible: false);
			SetActive(UI.OBJ_STAMP_DOWN, is_visible: true);
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
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
	}

	private void PostStamp(int userId, string userName, int stampId, ChatItemListData data)
	{
		if (!IsValidStampId(stampId))
		{
			return;
		}
		StampTable.Data data2 = Singleton<StampTable>.I.GetData((uint)stampId);
		if (data2 != null)
		{
			AddNextChatItem(data, delegate(GuildChatItem chatItem)
			{
				chatItem.Init(string.Empty, 0, userId, userName, stampId);
			});
			if (data2.hasSE)
			{
				SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
			}
			else
			{
				SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
			}
		}
	}

	private void PostNotification(string text, ChatItemListData data)
	{
		AddNextChatItem(data, delegate(GuildChatItem chatItem)
		{
			chatItem.Init(text);
		});
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
	}

	private void AddNextChatItem(ChatItemListData data, Action<GuildChatItem> initializer)
	{
		if (m_ChatItemPrefab == null)
		{
			return;
		}
		if (data.itemList.Count > 0)
		{
			data.currentTotalHeight += 22f;
		}
		GuildChatItem guildChatItem = null;
		if (data.itemList.Count < 30)
		{
			guildChatItem = ResourceUtility.Realizes(m_ChatItemPrefab, data.rootObject.transform, 5).GetComponent<GuildChatItem>();
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
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
			{
				ScrollView.panel.widgetsAreStatic = true;
			});
		}
		float currentTotalHeight = data.currentTotalHeight;
		guildChatItem.transform.localPosition = new Vector3(-15f, 0f - currentTotalHeight, 0f);
		initializer(guildChatItem);
		data.currentTotalHeight += guildChatItem.height;
		UpdateDummyDragScroll();
		float num = data.currentTotalHeight + ScrollView.panel.baseClipRegion.y - ScrollView.panel.baseClipRegion.w * 0.5f + (ScrollViewTrans.localPosition.y + ScrollView.panel.clipOffset.y) + ScrollView.panel.clipSoftness.y;
		if (data.itemList.Count >= 30)
		{
			ForceScroll(num - guildChatItem.height - 22f, useSpring: false);
		}
		ForceScroll(num, useSpring: true);
		if (data.itemList.Count < 30)
		{
			data.itemList.Add(guildChatItem);
		}
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

	private bool IsAllowedUser(int userId)
	{
		if (MonoBehaviourSingleton<BlackListManager>.IsValid())
		{
			return !MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(userId);
		}
		return true;
	}

	private void UpdateDonate()
	{
		pinDonate = MonoBehaviourSingleton<GuildManager>.I.pinDonate;
		if (pinDonate != null)
		{
			StartCoroutine(AddDonatePin(pinDonate));
		}
		SetButtonEvent(UI.BTN_DONATE_CHAT, new EventDelegate(delegate
		{
			_viewType = VIEW_TYPE.CHAT;
			RefreshUI();
		}));
		List<DonateInfo> donate_list = MonoBehaviourSingleton<GuildManager>.I.donateList;
		SetGrid(UI.GRD_DONATE, "GuildMessageDonateListItem", donate_list.Count, reset: true, delegate(int i, Transform t, bool b)
		{
			DonateInfo info = donate_list[i];
			t.GetComponent<GuildMessageDonateListItem>().SetDonateInfo(info);
			SetActive(t, UI.OBJ_TARGET, info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			SetActive(t, UI.OBJ_OWNER, info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			Transform transform = (info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? FindCtrl(t, UI.OBJ_TARGET) : FindCtrl(t, UI.OBJ_OWNER);
			SetLabelText(t, UI.LBL_CHAT_MESSAGE, info.msg);
			bool flag = info.itemNum >= info.quantity;
			SetActive(transform, UI.OBJ_FULL, flag);
			SetActive(transform, UI.OBJ_NORMAL, !flag);
			SetSliderValue(transform, UI.SLD_PROGRESS, (float)info.itemNum / (float)info.quantity);
			SetLabelText(transform, UI.LBL_CHAT_MESSAGE, info.msg);
			SetLabelText(transform, UI.LBL_USER_NAME, info.nickName);
			SetLabelText(transform, UI.LBL_MATERIAL_NAME, info.materialName);
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1);
			SetLabelText(transform, UI.LBL_QUATITY, itemNum);
			SetLabelText(transform, UI.LBL_DONATE_NUM, info.itemNum);
			SetLabelText(transform, UI.LBL_DONATE_MAX, info.quantity);
			if (info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				if (!flag)
				{
					SetButtonEvent(transform, UI.BTN_ASK, new EventDelegate(delegate
					{
						DispatchEvent("ASK", info);
					}));
				}
				else
				{
					SetButtonEnabled(transform, UI.BTN_ASK, is_enabled: false);
				}
			}
			else
			{
				int itemNum2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1);
				if (!flag && itemNum2 > 0 && info.itemNum < info.quantity)
				{
					SetButtonEvent(transform, UI.BTN_GIFT, new EventDelegate(delegate
					{
						DispatchEvent("SEND", info);
					}));
				}
				else
				{
					SetButtonEnabled(transform, UI.BTN_GIFT, is_enabled: false);
				}
			}
			ItemInfo item = ItemInfo.CreateItemInfo(new Item
			{
				uniqId = "0",
				itemId = info.itemId,
				num = info.itemNum
			});
			ItemSortData itemSortData = new ItemSortData();
			itemSortData.SetItem(item);
			SetItemIcon(FindCtrl(transform, UI.OBJ_MATERIAL_ICON), itemSortData, FindCtrl(GetCtrl(UI.OBJ_DONATE_PANEL), UI.PNL_MATERIAL_INFO), i);
		});
		SetActive(UI.LBL_NO_DONATE, donate_list.Count == 0);
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
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
			data.GetNum();
			_ = 1;
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
			if (data.GetUniqID() != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
		}
		ItemIcon itemIcon = null;
		itemIcon = ((data.GetIconType() != ITEM_ICON_TYPE.QUEST_ITEM) ? ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, -1, "DROP", event_data, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id) : ItemIcon.Create(new ItemIcon.ItemIconCreateParam
		{
			icon_type = data.GetIconType(),
			icon_id = data.GetIconID(),
			rarity = data.GetRarity(),
			parent = holder,
			element = data.GetIconElement(),
			magi_enable_equip_type = data.GetIconMagiEnableType(),
			num = data.GetNum(),
			enemy_icon_id = enemy_icon_id,
			questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
		}));
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
		if (clanUpdateStatusData.type == 1)
		{
			return;
		}
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
		else
		{
			_ = clanUpdateStatusData.type;
			_ = 4;
		}
	}

	private void ClanUpdateStatus()
	{
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
				StartCoroutine(ShowDisableState());
			}
		}
	}

	private IEnumerator ShowDisableState()
	{
		yield return new WaitForSeconds(3f);
		RefreshUI();
	}

	private void RefreshClanPinData()
	{
		need_update_pin = false;
		bool stayEvent = false;
		if (!GameSceneEvent.IsStay())
		{
			stayEvent = true;
			GameSceneEvent.Stay();
		}
		MonoBehaviourSingleton<GuildManager>.I.GetAllPinData(delegate(bool success, GuildGetPinModel ret)
		{
			if (success)
			{
				if (!string.IsNullOrEmpty(ret.result.message))
				{
					if (ret.result.type != 2)
					{
						pinMessage = new ClanChatLogMessageData();
						pinMessage.fromUserId = ret.result.fromUserId;
						pinMessage.id = ret.result.id;
						pinMessage.type = ret.result.type;
						pinMessage.message = ret.result.message;
						pinMessage.uuid = ret.result.uuid;
						if (pinMessage.type == 1)
						{
							pinMessage.stampId = int.Parse(ret.result.message);
						}
						senerInfo = ret.result.charInfo;
						StartCoroutine(AddChatPinMsg());
					}
					else if (_viewType == VIEW_TYPE.DONATE)
					{
						RefreshUI();
					}
				}
				else
				{
					RemovePinMsg();
				}
			}
			if (stayEvent)
			{
				GameSceneEvent.Resume();
			}
		});
	}

	private void OnReceiveClanChatUnPin()
	{
		RemovePinMsg();
	}

	private void UpdateChatPin()
	{
		if (chatPinItem == null)
		{
			if (_chatType == CHAT_TYPE.CLAN && pinMessage != null)
			{
				StartCoroutine(AddChatPinMsg());
			}
		}
		else if (_chatType == CHAT_TYPE.MEMBER && chatPinItem.gameObject.activeSelf)
		{
			ScrollView.panel.baseClipRegion = baseClipRegion;
			chatPinItem.gameObject.SetActive(value: false);
		}
		else if (_chatType == CHAT_TYPE.CLAN && !chatPinItem.gameObject.activeSelf)
		{
			ScrollView.panel.baseClipRegion = currentClipRegion;
			chatPinItem.gameObject.SetActive(value: true);
		}
	}

	private void OnQuery_UNPIN()
	{
		chatPinItem.HideUnPinButton();
		DispatchEvent("UNPIN_MSG", "Are you sure you want to unpin this message?");
	}

	private void OnQuery_GuildUnPinMessageDialog_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatUnPin(delegate(bool success, GuildChatUnPinModel ret)
		{
			if (success)
			{
				RemovePinMsg();
			}
			GameSection.ResumeEvent(success);
		});
	}

	private void RemovePinMsg()
	{
		if (!(chatPinItem == null))
		{
			ClearRenderModel(chatPinItem.transform, UI.TEX_MODEL);
			UnityEngine.Object.DestroyImmediate(chatPinItem.gameObject);
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

	private void SendPinMsg()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatPin(pinMessage.fromUserId, pinMessage.id, pinMessage.uuid, pinMessage.type, pinMessage.message, delegate(bool success, GuildChatPinModel ret)
		{
			if (success)
			{
				HidePinButton();
				senerInfo = ret.result.charInfo;
				StartCoroutine(AddChatPinMsg());
			}
			GameSection.ResumeEvent(success);
		});
	}

	private IEnumerator AddChatPinMsg()
	{
		if (senerInfo != null && !(m_ChatPinItemPrefab == null))
		{
			if (chatPinItem == null)
			{
				chatPinItem = ResourceUtility.Realizes(m_ChatPinItemPrefab, RootRect.transform, 5).GetComponent<GuildChatPinItem>();
				chatPinItem.transform.localPosition = new Vector3(0f, 370f, 0f);
			}
			yield return null;
			if (pinMessage.type == 0)
			{
				chatPinItem.ShowPinMsg(senerInfo.name, pinMessage.message);
			}
			else if (pinMessage.type == 1)
			{
				chatPinItem.ShowPinStamp(senerInfo.name, pinMessage.stampId);
			}
			CalculateBaseClipScrollView();
			ClearRenderModel(chatPinItem.transform, UI.TEX_MODEL);
			yield return null;
			SetRenderPlayerModel(chatPinItem.transform, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(senerInfo, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
		}
	}

	private void OnQuery_HIDE_PIN_BTN()
	{
		int result = 0;
		int.TryParse(GameSection.GetEventData() as string, out result);
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
			bool num = donatePinItem == null;
			if (donatePinItem == null)
			{
				donatePinItem = ResourceUtility.Realizes(m_DonatePinItemPrefab, GetCtrl(UI.WGT_DONATE_ROOT).transform, 5).GetComponent<GuildDonatePinItem>();
				donatePinItem.transform.localPosition = new Vector3(0f, 375f, 0f);
			}
			UpdateDonatePinUI(info);
			donatePinItem.ShowPin(info);
			UIPanel component = GetCtrl(UI.SCR_DONATE).GetComponent<UIScrollView>().gameObject.GetComponent<UIPanel>();
			int num2 = donatePinItem.GetBaseHeight + 30;
			if (num)
			{
				baseDonateClipRegion = component.baseClipRegion;
			}
			component.baseClipRegion = new Vector4(baseDonateClipRegion.x, baseDonateClipRegion.y - (float)num2 / 2f, baseDonateClipRegion.z, baseDonateClipRegion.w - (float)num2);
		}
		yield break;
	}

	private void UpdateDonatePinUI(DonateInfo info)
	{
		Transform transform = donatePinItem.transform;
		SetActive(transform, UI.OBJ_TARGET, info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		SetActive(transform, UI.OBJ_OWNER, info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		Transform transform2 = (info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id) ? FindCtrl(transform, UI.OBJ_TARGET) : FindCtrl(transform, UI.OBJ_OWNER);
		SetLabelText(transform, UI.LBL_CHAT_MESSAGE, info.msg);
		bool flag = info.itemNum >= info.quantity;
		SetActive(transform2, UI.OBJ_FULL, flag);
		SetActive(transform2, UI.OBJ_NORMAL, !flag);
		SetSliderValue(transform2, UI.SLD_PROGRESS, (float)info.itemNum / (float)info.quantity);
		SetLabelText(transform2, UI.LBL_CHAT_MESSAGE, info.msg);
		SetLabelText(transform2, UI.LBL_USER_NAME, info.nickName);
		SetLabelText(transform2, UI.LBL_MATERIAL_NAME, info.materialName);
		int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1);
		SetLabelText(transform2, UI.LBL_QUATITY, itemNum);
		SetLabelText(transform2, UI.LBL_DONATE_NUM, info.itemNum);
		SetLabelText(transform2, UI.LBL_DONATE_MAX, info.quantity);
		if (info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			SetButtonEvent(transform2, UI.BTN_ASK, new EventDelegate(delegate
			{
				DispatchEvent("ASK", info);
			}));
		}
		else
		{
			int itemNum2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1);
			if (!flag && itemNum2 > 0 && info.itemNum < info.quantity)
			{
				SetButtonEvent(transform2, UI.BTN_GIFT, new EventDelegate(delegate
				{
					DispatchEvent("SEND", info);
				}));
			}
			else
			{
				SetButtonEnabled(transform2, UI.BTN_GIFT, is_enabled: false);
			}
		}
		ItemInfo item = ItemInfo.CreateItemInfo(new Item
		{
			uniqId = "0",
			itemId = info.itemId,
			num = info.itemNum
		});
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item);
		SetItemIcon(FindCtrl(transform2, UI.OBJ_MATERIAL_ICON), itemSortData, FindCtrl(GetCtrl(UI.OBJ_DONATE_PANEL), UI.PNL_MATERIAL_INFO), 0);
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

	private void OnQuery_GuildUnPinDonateDialog_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendClanChatUnPin(delegate(bool success, GuildChatUnPinModel ret)
		{
			if (success)
			{
				GetCtrl(UI.SCR_DONATE).GetComponent<UIScrollView>().gameObject.GetComponent<UIPanel>().baseClipRegion = baseDonateClipRegion;
				UnityEngine.Object.DestroyImmediate(donatePinItem.gameObject);
				donatePinItem = null;
			}
			GameSection.ResumeEvent(success);
		});
	}

	private void OnQuery_GuildUnPinDonateDialog_NO()
	{
	}

	private void OnQuery_UNPIN_DONATE_BTN()
	{
		DispatchEvent("UNPIN_DONATE", "Are you sure?");
	}

	private void SendPinDonate(DonateInfo info)
	{
		if (info != null && !(info.expired <= 0.0))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<GuildManager>.I.SendClanChatPin(0, info.id, "", 2, "", delegate(bool success, GuildChatPinModel ret)
			{
				if (success)
				{
					MonoBehaviourSingleton<GuildManager>.I.pinDonate = info;
					StartCoroutine(AddDonatePin(info));
				}
				GameSection.ResumeEvent(success);
			});
		}
	}

	private void UpdateAdvisoryItem()
	{
		if (chatAdvisoryItem == null && _chatType == CHAT_TYPE.CLAN && _advisaryData != null)
		{
			StartCoroutine(AddAdvisary());
		}
	}

	private IEnumerator AddAdvisary()
	{
		if (_advisaryData != null && !(m_ChatAdvisaryItemPrefab == null) && !GuildChatAdvisoryItem.HasReadNew())
		{
			if (chatAdvisoryItem == null)
			{
				chatAdvisoryItem = ResourceUtility.Realizes(m_ChatAdvisaryItemPrefab, GetCtrl(UI.WGT_CHAT_TOP), 5).GetComponent<GuildChatAdvisoryItem>();
				chatAdvisoryItem.transform.localPosition = new Vector3(0f, 370f, 0f);
			}
			yield return null;
			chatAdvisoryItem.Init(_advisaryData.title, _advisaryData.content);
			SetButtonEvent(chatAdvisoryItem.close, new EventDelegate(delegate
			{
				GuildChatAdvisoryItem.SetReadNew();
				if (chatAdvisoryItem != null)
				{
					UnityEngine.Object.DestroyImmediate(chatAdvisoryItem.gameObject);
					chatAdvisoryItem = null;
				}
			}));
		}
	}

	private void CalculateBaseClipScrollView()
	{
		Vector4 vector = new Vector4(baseClipRegion.x, baseClipRegion.y, baseClipRegion.z, baseClipRegion.w);
		if (chatPinItem != null && chatPinItem.gameObject.activeSelf)
		{
			float num = (float)chatPinItem.GetHeight - 15f;
			vector = new Vector4(vector.x, vector.y - num / 2f, vector.z, vector.w - num);
		}
		ScrollView.panel.baseClipRegion = vector;
	}
}
