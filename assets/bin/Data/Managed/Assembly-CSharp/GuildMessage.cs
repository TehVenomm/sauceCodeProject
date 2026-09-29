// Decompiled with JetBrains decompiler
// Type: GuildMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildMessage : GameSection
{
  private static readonly string STAMP_SYMBOL_BEGIN = "[STMP]";
  public const string EVENT_AGE_CONFIRM = "CHAT_AGE_CONFIRM";
  private TouchScreenKeyboard m_Keyboard;
  private GuildMessage.CHAT_TYPE _chatType;
  private GuildMessage.VIEW_TYPE _viewType;
  private GameObject m_ChatPinItemPrefab;
  private GameObject m_ChatItemPrefab;
  private GameObject m_ChatStampListPrefab;
  private GameObject m_ChatAdvisaryItemPrefab;
  private GameObject m_DonatePinItemPrefab;
  private GuildMessage.ChatItemListData[] m_DataList = new GuildMessage.ChatItemListData[Enum.GetNames(typeof (GuildMessage.CHAT_TYPE)).Length];
  private GuildMessage.PostRequestQueue[] m_PostRequestQueue = new GuildMessage.PostRequestQueue[Enum.GetNames(typeof (GuildMessage.CHAT_TYPE)).Length];
  private Dictionary<int, int> _updatedTime = new Dictionary<int, int>();
  private UIScrollView m_ScrollView;
  private Transform m_ScrollViewTrans;
  private UIWidget m_DummyDragScroll;
  private BoxCollider m_DragScrollCollider;
  private Transform m_DragScrollTrans;
  private UIRect m_RootRect;
  private UIInput m_Input;
  private const int CHAT_ITEM_OFFSET = 22;
  private const int FORCE_SCROLL_LIMIT = 32 /*0x20*/;
  private const float SOFTNESS_HEIGHT = 10f;
  private const float SPRING_STRENGTH = 20f;
  private const float CHAT_WIDTH = 410f;
  private const int ITEM_COUNT_MAX = 30;
  private List<int> m_StampIdListCanPost;
  private List<GuildMessage.ChatTabData> m_ChatTabListData = new List<GuildMessage.ChatTabData>();
  private List<GuildMessage.ChatPostRequest> m_ChatLogListData = new List<GuildMessage.ChatPostRequest>();
  private Dictionary<string, List<GuildMessage.ChatPostRequest>> m_MemberLogs = new Dictionary<string, List<GuildMessage.ChatPostRequest>>();
  private Transform[] m_ObjRoot = new Transform[Enum.GetNames(typeof (GuildMessage.VIEW_TYPE)).Length];
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

  private GuildMessage.ChatItemListData CurrentData => this.m_DataList[(int) this._chatType];

  private float CurrentTotalHeight
  {
    get
    {
      float currentTotalHeight = 0.0f;
      if (this.CurrentData != null)
        currentTotalHeight = this.CurrentData.currentTotalHeight;
      return currentTotalHeight;
    }
  }

  private UIScrollView ScrollView
  {
    get
    {
      if (Object.op_Equality((Object) this.m_ScrollView, (Object) null))
        this.m_ScrollView = ((Component) this.GetCtrl((Enum) GuildMessage.UI.SCR_CHAT)).GetComponent<UIScrollView>();
      return this.m_ScrollView;
    }
  }

  private Transform ScrollViewTrans
  {
    get
    {
      if (Object.op_Equality((Object) this.m_ScrollViewTrans, (Object) null))
        this.m_ScrollViewTrans = this.GetCtrl((Enum) GuildMessage.UI.SCR_CHAT);
      return this.m_ScrollViewTrans;
    }
  }

  private UIWidget DummyDragScroll
  {
    get
    {
      if (Object.op_Equality((Object) this.m_DummyDragScroll, (Object) null))
        this.m_DummyDragScroll = ((Component) this.GetCtrl((Enum) GuildMessage.UI.WGT_DUMMY_DRAG_SCROLL)).GetComponent<UIWidget>();
      return this.m_DummyDragScroll;
    }
  }

  private BoxCollider DragScrollCollider
  {
    get
    {
      if (Object.op_Equality((Object) this.m_DragScrollCollider, (Object) null))
        this.m_DragScrollCollider = ((Component) this.GetCtrl((Enum) GuildMessage.UI.WGT_DUMMY_DRAG_SCROLL)).GetComponent<BoxCollider>();
      return this.m_DragScrollCollider;
    }
  }

  private Transform DragScrollTrans
  {
    get
    {
      if (Object.op_Equality((Object) this.m_DragScrollTrans, (Object) null))
        this.m_DragScrollTrans = this.GetCtrl((Enum) GuildMessage.UI.WGT_DUMMY_DRAG_SCROLL);
      return this.m_DragScrollTrans;
    }
  }

  private UIRect RootRect
  {
    get
    {
      if (Object.op_Equality((Object) this.m_RootRect, (Object) null))
        this.m_RootRect = ((Component) this.GetCtrl((Enum) GuildMessage.UI.WGT_CHAT_ROOT)).GetComponent<UIRect>();
      return this.m_RootRect;
    }
  }

  private UIInput Input
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Input, (Object) null))
        this.m_Input = ((Component) this.GetCtrl((Enum) GuildMessage.UI.IPT_POST)).GetComponent<UIInput>();
      return this.m_Input;
    }
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this._chatType = MonoBehaviourSingleton<GuildManager>.I.talkUser != null ? GuildMessage.CHAT_TYPE.MEMBER : GuildMessage.CHAT_TYPE.CLAN;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatItem");
    LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
    LoadObject lo_quest_chatpinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatPinItem");
    LoadObject lo_chatAdvisaryItem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildChatAdvisoryItem");
    LoadObject lo_quest_donatepinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "GuildDonatePinItem");
    bool finish_chat_log = false;
    MonoBehaviourSingleton<GuildManager>.I.SendClanChatLog((Action<bool, GuildChatModel>) ((success, ret) =>
    {
      finish_chat_log = true;
      this.AddClanChatLog(ret.result.array);
      if (ret.result.pin != null)
      {
        this.pinMessage = new ClanChatLogMessageData();
        this.pinMessage.fromUserId = ret.result.pin.fromUserId;
        this.pinMessage.id = ret.result.pin.id;
        this.pinMessage.type = ret.result.pin.type;
        this.pinMessage.message = ret.result.pin.message;
        this.pinMessage.uuid = ret.result.pin.uuid;
        if (this.pinMessage.type == 1)
          this.pinMessage.stampId = int.Parse(ret.result.pin.message);
        this.senerInfo = ret.result.pin.charInfo;
      }
      if (ret.result.advisory == null)
        return;
      this._advisaryData = ret.result.advisory;
    }));
    bool finish_donate_list = false;
    MonoBehaviourSingleton<GuildManager>.I.SendDonateList((Action<bool>) (success => finish_donate_list = true));
    bool finish_log_member = true;
    if (this._chatType == GuildMessage.CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
    {
      finish_log_member = false;
      MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, (Action<bool, GuildPrivateChatModel>) ((success, ret) =>
      {
        this.AddMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, ret.result.array);
        finish_log_member = true;
      }));
    }
    while (!finish_chat_log || !finish_donate_list || !finish_log_member)
      yield return (object) null;
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    this.m_DataList[0] = new GuildMessage.ChatItemListData(((Component) this.GetCtrl((Enum) GuildMessage.UI.OBJ_CLAN_ITEM_LIST_ROOT)).gameObject);
    this.m_DataList[1] = new GuildMessage.ChatItemListData(((Component) this.GetCtrl((Enum) GuildMessage.UI.OBJ_MEMBER_ITEM_LIST_ROOT)).gameObject);
    this.m_ObjRoot[0] = this.GetCtrl((Enum) GuildMessage.UI.OBJ_CHAT_PANEL);
    this.m_ObjRoot[1] = this.GetCtrl((Enum) GuildMessage.UI.OBJ_DONATE_PANEL);
    for (int index = 0; index < this.m_PostRequestQueue.Length; ++index)
      this.m_PostRequestQueue[index] = new GuildMessage.PostRequestQueue();
    this.m_ChatItemPrefab = lo_quest_chatitem.loadedObject as GameObject;
    this.m_ChatStampListPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
    this.m_ChatPinItemPrefab = lo_quest_chatpinitem.loadedObject as GameObject;
    this.m_ChatAdvisaryItemPrefab = lo_chatAdvisaryItem.loadedObject as GameObject;
    this.m_DonatePinItemPrefab = lo_quest_donatepinitem.loadedObject as GameObject;
    this.DummyDragScroll.width = 410;
    this.InitStampList();
    this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_UP, false);
    this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_DOWN, true);
    object eventData = GameSection.GetEventData();
    if (eventData != null && eventData is GuildMessage.VIEW_TYPE viewType)
      this._viewType = viewType;
    bool waitToGetMember = true;
    MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool, GuildMemberListModel>) ((success, ret) => waitToGetMember = false));
    while (waitToGetMember)
      yield return (object) null;
    UIPanel component = ((Component) this.ScrollView).gameObject.GetComponent<UIPanel>();
    this.baseClipRegion = component.baseClipRegion;
    this.currentClipRegion = component.baseClipRegion;
    if (this._chatType == GuildMessage.CHAT_TYPE.CLAN)
    {
      if (this._advisaryData != null)
        this.StartCoroutine(this.AddAdvisary());
      if (this.pinMessage != null)
        this.StartCoroutine(this.AddChatPinMsg());
    }
    base.Initialize();
  }

  public override void InitializeReopen()
  {
    if (this._viewType == GuildMessage.VIEW_TYPE.CHAT)
      this.StartCoroutine(this.DoInitializeReopen());
    else
      base.InitializeReopen();
  }

  private IEnumerator DoInitializeReopen()
  {
    this._chatType = MonoBehaviourSingleton<GuildManager>.I.talkUser != null ? GuildMessage.CHAT_TYPE.MEMBER : GuildMessage.CHAT_TYPE.CLAN;
    bool finish_log_member = true;
    if (this._chatType == GuildMessage.CHAT_TYPE.MEMBER && MonoBehaviourSingleton<GuildManager>.I.talkUser != null)
    {
      finish_log_member = false;
      MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, (Action<bool, GuildPrivateChatModel>) ((success, ret) =>
      {
        this.ResetMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
        this.AddMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, ret.result.array);
        finish_log_member = true;
      }));
    }
    while (!finish_log_member)
      yield return (object) null;
    base.InitializeReopen();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    MonoBehaviourSingleton<ChatManager>.IsValid();
  }

  private void Update()
  {
    if (!this.isInitialized)
      return;
    this.DragScrollCollider.center = Vector2.op_Implicit(new Vector2(this.ScrollView.panel.baseClipRegion.x, -(float) ((double) this.ScrollView.panel.baseClipRegion.w - (double) this.ScrollView.panel.baseClipRegion.y + (double) this.DragScrollTrans.localPosition.y - ((double) this.ScrollView.panel.finalClipRegion.w + (double) this.ScrollView.panel.clipOffset.y))));
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) GuildMessage.UI.OBJ_CHAT_PANEL, this._viewType == GuildMessage.VIEW_TYPE.CHAT);
    this.SetActive((Enum) GuildMessage.UI.OBJ_DONATE_PANEL, this._viewType == GuildMessage.VIEW_TYPE.DONATE);
    if (this._viewType == GuildMessage.VIEW_TYPE.CHAT)
    {
      this.UpdateChat();
    }
    else
    {
      this.SetActive((Enum) GuildMessage.UI.LBL_NO_DONATE, false);
      if (!GameSceneEvent.IsStay())
        GameSceneEvent.Stay();
      MonoBehaviourSingleton<GuildManager>.I.SendDonateList((Action<bool>) (success =>
      {
        this.UpdateDonate();
        GameSection.ResumeEvent(true);
      }));
    }
    this.UpdateClanBadge();
  }

  private void OnApplicationFocus(bool hasFocus)
  {
    if (!hasFocus || this._viewType != GuildMessage.VIEW_TYPE.DONATE)
      return;
    this.StopCoroutine(this.ShowDisableState());
    if (!GameSceneEvent.IsStay())
      GameSceneEvent.Stay();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateList((Action<bool>) (success =>
    {
      this.UpdateDonate();
      GameSection.ResumeEvent(true);
    }));
  }

  private void UpdateClanBadge()
  {
    if (MonoBehaviourSingleton<GuildManager>.I.guilMemberList != null)
    {
      bool is_visible = MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters != null && MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters.Count > 0;
      this.SetActive(this.FindCtrl(this._transform, (Enum) GuildMessage.UI.BTN_MEMBER), (Enum) GuildMessage.UI.SPR_BADGE, is_visible);
      this.SetActive(this.FindCtrl(this._transform, (Enum) GuildMessage.UI.BTN_GUILD_SETTING), (Enum) GuildMessage.UI.SPR_BADGE, is_visible);
    }
    else
    {
      this.SetActive(this.FindCtrl(this._transform, (Enum) GuildMessage.UI.BTN_MEMBER), (Enum) GuildMessage.UI.SPR_BADGE, false);
      this.SetActive(this.FindCtrl(this._transform, (Enum) GuildMessage.UI.BTN_GUILD_SETTING), (Enum) GuildMessage.UI.SPR_BADGE, false);
    }
  }

  protected virtual void LateUpdate()
  {
    this.ClanUpdateStatus();
    if (!this.IsValidDispatchEventInUpdate() || GameSaveData.instance.isShowChatOfferBanner || MonoBehaviourSingleton<GuildManager>.I.guildData == null || MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId)
      return;
    this.DispatchEvent("BANNER_CLANCHATOFFER");
    GameSaveData.instance.isShowChatOfferBanner = true;
  }

  private bool IsValidDispatchEventInUpdate()
  {
    return HomeSelfCharacter.CTRL && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible();
  }

  private void UpdateChat()
  {
    bool is_visible = false;
    this.SetActive((Enum) GuildMessage.UI.OBJ_POST_BLOCK, !is_visible);
    this.SetActive((Enum) GuildMessage.UI.BTN_CHAT, !is_visible);
    this.SetActive((Enum) GuildMessage.UI.OBJ_CHAT_INPUT, is_visible);
    this.SetLabelText((Enum) GuildMessage.UI.LBL_CONNECTION_STATUS, this.sectionData.GetText("TEXT_DISCONNECT"));
    this.SetButtonEvent((Enum) GuildMessage.UI.BTN_CHAT, new EventDelegate((EventDelegate.Callback) (() =>
    {
      this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_UP, true);
      this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_DOWN, false);
    })));
    this.SetButtonEvent((Enum) GuildMessage.UI.BTN_RECONNECT, new EventDelegate((EventDelegate.Callback) (() => this.SetLabelText((Enum) GuildMessage.UI.LBL_CONNECTION_STATUS, this.sectionData.GetText("TEXT_CONNECTING")))));
    this.SetButtonEvent((Enum) GuildMessage.UI.BTN_TAB_GUILD, new EventDelegate((EventDelegate.Callback) (() =>
    {
      if (this._chatType == GuildMessage.CHAT_TYPE.CLAN)
        return;
      MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();
      this._chatType = GuildMessage.CHAT_TYPE.CLAN;
      this.RefreshUI();
    })));
    this.SetButtonEvent((Enum) GuildMessage.UI.BTN_STAMP_UP, new EventDelegate((EventDelegate.Callback) (() =>
    {
      this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_UP, true);
      this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_DOWN, false);
    })));
    this.SetButtonEvent((Enum) GuildMessage.UI.BTN_STAMP_DOWN, new EventDelegate((EventDelegate.Callback) (() =>
    {
      this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_UP, false);
      this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_DOWN, true);
    })));
    this.SetInputSubmitEvent((Enum) GuildMessage.UI.IPT_POST, new EventDelegate((EventDelegate.Callback) (() =>
    {
      this.OnTouchPost();
      this.SetInputValue((Enum) GuildMessage.UI.IPT_POST, string.Empty);
    })));
    this.UpdateChatLog();
    this.UpdateTabChat();
    this.UpdateCurrentData();
    this.UpdateAdvisoryItem();
    this.UpdateChatPin();
  }

  private void UpdateChatLog()
  {
    this.m_DataList[(int) this._chatType].Reset();
    if (this._chatType == GuildMessage.CHAT_TYPE.MEMBER)
    {
      if (MonoBehaviourSingleton<GuildManager>.I.talkUser == null)
        MonoBehaviourSingleton<GuildManager>.I.UpdateTalkUser();
      if (MonoBehaviourSingleton<GuildManager>.I.talkUser == null)
        return;
      List<GuildMessage.ChatPostRequest> memberChatLog = this.GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
      if (memberChatLog == null)
        return;
      for (int index = 0; index < memberChatLog.Count; ++index)
        this.Post(memberChatLog[index]);
    }
    else
    {
      for (int index = 0; index < this.m_ChatLogListData.Count; ++index)
        this.Post(this.m_ChatLogListData[index]);
    }
  }

  private void UpdateCurrentData()
  {
    this.SetActive((Enum) GuildMessage.UI.OBJ_MEMBER_ITEM_LIST_ROOT, this._chatType == GuildMessage.CHAT_TYPE.MEMBER);
    this.SetActive((Enum) GuildMessage.UI.OBJ_CLAN_ITEM_LIST_ROOT, this._chatType == GuildMessage.CHAT_TYPE.CLAN);
  }

  private void UpdateTabChat()
  {
    this.m_ChatTabListData.Clear();
    this.SetGrid((Enum) GuildMessage.UI.GRD_TAB_CHAT, "GuildMessageTabListItem", MonoBehaviourSingleton<GuildManager>.I.talkUsers.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      FriendCharaInfo talkUser = MonoBehaviourSingleton<GuildManager>.I.talkUsers[i];
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == talkUser.userId)
        return;
      this.SetLabelText(t, (Enum) GuildMessage.UI.LBL_TAB_NAME, talkUser.name);
      this.SetActive(t, (Enum) GuildMessage.UI.SPR_TAB_HIGHLIGHT, MonoBehaviourSingleton<GuildManager>.I.talkUser != null && MonoBehaviourSingleton<GuildManager>.I.talkUser.userId == talkUser.userId);
      this.SetEvent(t, (Enum) GuildMessage.UI.BTN_TAB_CLOSE, "CLOSE_TAB", (object) talkUser);
      this.m_ChatTabListData.Add(new GuildMessage.ChatTabData()
      {
        tran = t,
        info = talkUser
      });
      this.SetEvent(t, "TAB", i);
    }));
    this.ScrollViewResetPosition((Enum) GuildMessage.UI.SCR_TAB_CHAT);
  }

  private void AddClanChatLog(List<ClanChatLogMessageData> datas)
  {
    this.m_ChatLogListData.Clear();
    int num = datas.Count > 1000 ? 1000 : datas.Count;
    for (int index = 0; index < num; ++index)
    {
      ClanChatLogMessageData data = datas[index];
      string[] strArray = data.message.Split(':');
      string sName = strArray[0];
      string message = strArray[1];
      if (data.fromUserId == 0)
        this.m_ChatLogListData.Add(new GuildMessage.ChatPostRequest(message));
      else if (message.Contains(GuildMessage.STAMP_SYMBOL_BEGIN))
      {
        string s = message.Substring(GuildMessage.STAMP_SYMBOL_BEGIN.Length, 8);
        int stampId = -1;
        ref int local = ref stampId;
        int.TryParse(s, out local);
        this.m_ChatLogListData.Add(new GuildMessage.ChatPostRequest(data.uuid, data.id, data.toUserId, data.fromUserId, sName, stampId));
      }
      else
        this.m_ChatLogListData.Add(new GuildMessage.ChatPostRequest(data.uuid, data.id, data.toUserId, data.fromUserId, sName, message));
    }
  }

  private void ResetMemberChatLog(int userId)
  {
    this.GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, userId)?.Clear();
  }

  private List<GuildMessage.ChatPostRequest> GetMemberChatLog(int receiveId, int senderId)
  {
    string key1 = $"{receiveId}_{senderId}";
    string key2 = $"{senderId}_{receiveId}";
    if (this.m_MemberLogs.ContainsKey(key1))
      return this.m_MemberLogs[key1];
    return this.m_MemberLogs.ContainsKey(key2) ? this.m_MemberLogs[key2] : (List<GuildMessage.ChatPostRequest>) null;
  }

  private void AddMemberChatLog(int userId, List<ClanChatLogMessageData> datas)
  {
    List<GuildMessage.ChatPostRequest> chatPostRequestList = this.GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, userId);
    if (chatPostRequestList == null)
    {
      chatPostRequestList = new List<GuildMessage.ChatPostRequest>();
      this.m_MemberLogs.Add($"{MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id}_{userId}", chatPostRequestList);
    }
    chatPostRequestList.Clear();
    for (int index = 0; index < datas.Count; ++index)
    {
      ClanChatLogMessageData data = datas[index];
      string[] strArray = data.message.Split(':');
      string sName = strArray[0];
      string message = strArray[1];
      if (message.Contains(GuildMessage.STAMP_SYMBOL_BEGIN))
      {
        string s = message.Substring(GuildMessage.STAMP_SYMBOL_BEGIN.Length, 8);
        int stampId = -1;
        ref int local = ref stampId;
        int.TryParse(s, out local);
        chatPostRequestList.Add(new GuildMessage.ChatPostRequest(data.uuid, data.id, data.toUserId, data.fromUserId, sName, stampId));
      }
      else
        chatPostRequestList.Add(new GuildMessage.ChatPostRequest(data.uuid, data.id, data.toUserId, data.fromUserId, sName, message));
    }
  }

  private void AddMemberChatLog(
    string uuId,
    int chatId,
    int receiveId,
    int senderId,
    string senderName,
    string message)
  {
    List<GuildMessage.ChatPostRequest> chatPostRequestList = this.GetMemberChatLog(receiveId, senderId);
    if (chatPostRequestList == null)
    {
      chatPostRequestList = new List<GuildMessage.ChatPostRequest>();
      this.m_MemberLogs.Add($"{receiveId}_{senderId}", chatPostRequestList);
    }
    chatPostRequestList.Add(new GuildMessage.ChatPostRequest(uuId, chatId, receiveId, senderId, senderName, message));
    if (!MonoBehaviourSingleton<GuildManager>.I.AddTalkUser(receiveId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id ? senderId : receiveId) || this._viewType != GuildMessage.VIEW_TYPE.CHAT)
      return;
    this.UpdateTabChat();
  }

  private void AddMemberChatLog(
    string uuId,
    int chatId,
    int receiveId,
    int senderId,
    string senderName,
    int stampId)
  {
    List<GuildMessage.ChatPostRequest> chatPostRequestList = this.GetMemberChatLog(receiveId, senderId);
    if (chatPostRequestList == null)
    {
      chatPostRequestList = new List<GuildMessage.ChatPostRequest>();
      this.m_MemberLogs.Add($"{receiveId}_{senderId}", chatPostRequestList);
    }
    chatPostRequestList.Add(new GuildMessage.ChatPostRequest(uuId, chatId, receiveId, senderId, senderName, stampId));
    if (!MonoBehaviourSingleton<GuildManager>.I.AddTalkUser(receiveId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id ? senderId : receiveId) || this._viewType != GuildMessage.VIEW_TYPE.CHAT)
      return;
    this.UpdateTabChat();
  }

  private void OnQuery_CLOSE_TAB()
  {
    FriendCharaInfo eventData = GameSection.GetEventData() as FriendCharaInfo;
    GameSection.StayEvent();
    if (MonoBehaviourSingleton<GuildManager>.I.talkUser != null && eventData.userId == MonoBehaviourSingleton<GuildManager>.I.talkUser.userId)
    {
      MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();
      MonoBehaviourSingleton<GuildManager>.I.RemoveTalkUser(eventData);
      MonoBehaviourSingleton<GuildManager>.I.UpdateTalkUser();
    }
    else
      MonoBehaviourSingleton<GuildManager>.I.RemoveTalkUser(eventData);
    this._chatType = MonoBehaviourSingleton<GuildManager>.I.talkUser != null ? GuildMessage.CHAT_TYPE.MEMBER : GuildMessage.CHAT_TYPE.CLAN;
    GameSection.ResumeEvent(false);
    this.RefreshUI();
  }

  private void OnQuery_TAB()
  {
    int eventData = (int) GameSection.GetEventData();
    GameSection.StayEvent();
    FriendCharaInfo talkUser = MonoBehaviourSingleton<GuildManager>.I.talkUsers[eventData];
    if (MonoBehaviourSingleton<GuildManager>.I.talkUser != null && talkUser.userId == MonoBehaviourSingleton<GuildManager>.I.talkUser.userId)
    {
      GameSection.ResumeEvent(false);
    }
    else
    {
      MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(talkUser);
      this._chatType = GuildMessage.CHAT_TYPE.MEMBER;
      List<GuildMessage.ChatPostRequest> memberChatLog = this.GetMemberChatLog(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<GuildManager>.I.talkUser.userId);
      if (memberChatLog != null && memberChatLog.Count > 0)
      {
        GameSection.ResumeEvent(false);
        this.RefreshUI();
      }
      else
        MonoBehaviourSingleton<GuildManager>.I.SendPrivateClanChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, (Action<bool, GuildPrivateChatModel>) ((success, ret) =>
        {
          this.AddMemberChatLog(MonoBehaviourSingleton<GuildManager>.I.talkUser.userId, ret.result.array);
          GameSection.ResumeEvent(false);
          this.RefreshUI();
        }));
    }
  }

  private void OnReceiveClanText(ClanChatLogMessageData clanChatMsgData)
  {
    if (!this.IsAllowedUser(clanChatMsgData.fromUserId))
      return;
    GuildMessage.ChatPostRequest request = new GuildMessage.ChatPostRequest(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.message);
    this.m_ChatLogListData.Add(request);
    if (this._chatType != GuildMessage.CHAT_TYPE.CLAN)
      return;
    this.Post(request);
  }

  private void OnReceiveClanStamp(ClanChatLogMessageData clanChatMsgData)
  {
    if (!this.IsAllowedUser(clanChatMsgData.fromUserId))
      return;
    GuildMessage.ChatPostRequest request = new GuildMessage.ChatPostRequest(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.stampId);
    this.m_ChatLogListData.Add(request);
    if (this._chatType != GuildMessage.CHAT_TYPE.CLAN)
      return;
    this.Post(request);
  }

  private void OnReceiveClanPrivateText(ClanChatLogMessageData clanChatMsgData)
  {
    if (!this.IsAllowedUser(clanChatMsgData.fromUserId))
      return;
    this.AddMemberChatLog(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.message);
    if (this._chatType == GuildMessage.CHAT_TYPE.MEMBER)
    {
      this.RefreshUI();
    }
    else
    {
      foreach (GuildMessage.ChatTabData chatTabData in this.m_ChatTabListData)
      {
        if (chatTabData.info.userId == clanChatMsgData.fromUserId)
        {
          this.SetBadge(chatTabData.tran, -1, (SpriteAlignment) 1);
          break;
        }
      }
    }
  }

  private void OnReceiveClanPrivateStamp(ClanChatLogMessageData clanChatMsgData)
  {
    if (!this.IsAllowedUser(clanChatMsgData.fromUserId))
      return;
    this.AddMemberChatLog(clanChatMsgData.uuid, clanChatMsgData.id, clanChatMsgData.toUserId, clanChatMsgData.fromUserId, clanChatMsgData.senderName, clanChatMsgData.stampId);
    if (this._chatType == GuildMessage.CHAT_TYPE.MEMBER)
    {
      this.RefreshUI();
    }
    else
    {
      foreach (GuildMessage.ChatTabData chatTabData in this.m_ChatTabListData)
      {
        if (chatTabData.info.userId == clanChatMsgData.fromUserId)
        {
          this.SetBadge(chatTabData.tran, -1, (SpriteAlignment) 1);
          break;
        }
      }
    }
  }

  private void OnReceiveClanNotification(string message)
  {
    GuildMessage.ChatPostRequest request = new GuildMessage.ChatPostRequest(message);
    this.m_ChatLogListData.Add(request);
    if (this._chatType != GuildMessage.CHAT_TYPE.CLAN)
      return;
    this.Post(request);
  }

  private void OnJoinClanChat(CHAT_ERROR_TYPE errorType, string userId)
  {
    if (errorType != CHAT_ERROR_TYPE.NO_ERROR)
    {
      this.OnError(StringTable.Get(STRING_CATEGORY.CHAT_ERROR, 2U));
    }
    else
    {
      int result = -9999;
      int.TryParse(userId, out result);
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != result)
        return;
      this.SetActive((Enum) GuildMessage.UI.OBJ_POST_BLOCK, false);
      this.SetActive((Enum) GuildMessage.UI.BTN_CHAT, false);
      this.SetActive((Enum) GuildMessage.UI.OBJ_CHAT_INPUT, true);
    }
  }

  private void OnDisconnectClanChat()
  {
    this.SetLabelText((Enum) GuildMessage.UI.LBL_CONNECTION_STATUS, this.sectionData.GetText("TEXT_DISCONNECT"));
    this.SetActive((Enum) GuildMessage.UI.OBJ_POST_BLOCK, true);
    this.SetActive((Enum) GuildMessage.UI.BTN_CHAT, true);
    this.SetActive((Enum) GuildMessage.UI.OBJ_CHAT_INPUT, false);
  }

  private void OnLeaveClanChat(CHAT_ERROR_TYPE errorType, string userId)
  {
    this.SetLabelText((Enum) GuildMessage.UI.LBL_CONNECTION_STATUS, this.sectionData.GetText("TEXT_DISCONNECT"));
    this.SetActive((Enum) GuildMessage.UI.OBJ_POST_BLOCK, true);
    this.SetActive((Enum) GuildMessage.UI.BTN_CHAT, true);
    this.SetActive((Enum) GuildMessage.UI.OBJ_CHAT_INPUT, false);
  }

  private void OnError(string message)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, message, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (s => { }), true);
  }

  private void InitStampList()
  {
    if (this.m_StampIdListCanPost == null)
      this.ResetStampIdList();
    this.UpdateStampList();
  }

  public void UpdateStampList()
  {
    this.SetGrid((Enum) GuildMessage.UI.GRD_STAMP_LIST, (string) null, this.m_StampIdListCanPost.Count, true, new Func<int, Transform, Transform>(this.CreateStampItem), new Action<int, Transform, bool>(this.InitStampItem));
    this.SetEnabled<UIScrollView>((Enum) GuildMessage.UI.SCR_STAMP_LIST, true);
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
    int _stampId = this.m_StampIdListCanPost[index];
    ChatStampListItem item = ((Component) iTransform).GetComponent<ChatStampListItem>();
    item.Init(_stampId);
    if (isRecycle)
      return;
    item.onButton += (System.Action) (() => this.SendStampAsMine(item.StampId));
  }

  public void OnTouchPost()
  {
    if (!UserInfoManager.IsRegisterdAge() || !UserInfoManager.IsEnableCommunication())
      return;
    string message = this.Input.value;
    if (string.IsNullOrEmpty(message) || message.Trim().Length == 0)
      return;
    this.SendMessageAsMine(message);
  }

  public void SendMessageAsMine(string message)
  {
  }

  public void SendStampAsMine(int stampId)
  {
    if (!this.CanIPostTheStamp(stampId))
      return;
    this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_UP, false);
    this.SetActive((Enum) GuildMessage.UI.OBJ_STAMP_DOWN, true);
  }

  private void Post(GuildMessage.ChatPostRequest request)
  {
    GuildMessage.ChatItemListData data = this.m_DataList[(int) this._chatType];
    switch (request.Type)
    {
      case GuildMessage.ChatPostRequest.TYPE.Message:
        this.Post(request.uuId, request.chatId, request.senderId, request.senderName, request.message, data);
        break;
      case GuildMessage.ChatPostRequest.TYPE.Stamp:
        this.PostStamp(request.senderId, request.senderName, request.stampId, data);
        break;
      case GuildMessage.ChatPostRequest.TYPE.Notification:
        this.PostNotification(request.message, data);
        break;
    }
  }

  private void Post(
    string uuid,
    int chatId,
    int userId,
    string userName,
    string text,
    GuildMessage.ChatItemListData data)
  {
    this.AddNextChatItem(data, (Action<GuildChatItem>) (chatItem => chatItem.Init(uuid, chatId, userId, userName, text)));
    SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
  }

  private void PostStamp(
    int userId,
    string userName,
    int stampId,
    GuildMessage.ChatItemListData data)
  {
    if (!this.IsValidStampId(stampId))
      return;
    StampTable.Data data1 = Singleton<StampTable>.I.GetData((uint) stampId);
    if (data1 == null)
      return;
    this.AddNextChatItem(data, (Action<GuildChatItem>) (chatItem => chatItem.Init(string.Empty, 0, userId, userName, stampId)));
    if (data1.hasSE)
      SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
    else
      SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
  }

  private void PostNotification(string text, GuildMessage.ChatItemListData data)
  {
    this.AddNextChatItem(data, (Action<GuildChatItem>) (chatItem => chatItem.Init(text)));
    SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
  }

  private void AddNextChatItem(
    GuildMessage.ChatItemListData data,
    Action<GuildChatItem> initializer)
  {
    if (Object.op_Equality((Object) this.m_ChatItemPrefab, (Object) null))
      return;
    if (data.itemList.Count > 0)
      data.currentTotalHeight += 22f;
    GuildChatItem component;
    if (data.itemList.Count < 30)
    {
      component = ((Component) ResourceUtility.Realizes((Object) this.m_ChatItemPrefab, data.rootObject.transform, 5)).GetComponent<GuildChatItem>();
    }
    else
    {
      component = data.itemList[data.oldestItemIndex];
      ++data.oldestItemIndex;
      if (data.oldestItemIndex == 30)
        data.oldestItemIndex = 0;
      data.currentTotalHeight -= component.height + 22f;
      this.ScrollView.panel.widgetsAreStatic = false;
      data.MoveAll(component.height + 22f);
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.ScrollView.panel.widgetsAreStatic = true);
    }
    float currentTotalHeight = data.currentTotalHeight;
    ((Component) component).transform.localPosition = new Vector3(-15f, -currentTotalHeight, 0.0f);
    initializer(component);
    data.currentTotalHeight += component.height;
    this.UpdateDummyDragScroll();
    float newHeight = (float) ((double) data.currentTotalHeight + (double) this.ScrollView.panel.baseClipRegion.y - (double) this.ScrollView.panel.baseClipRegion.w * 0.5 + ((double) this.ScrollViewTrans.localPosition.y + (double) this.ScrollView.panel.clipOffset.y)) + this.ScrollView.panel.clipSoftness.y;
    if (data.itemList.Count >= 30)
      this.ForceScroll((float) ((double) newHeight - (double) component.height - 22.0), false);
    this.ForceScroll(newHeight, true);
    if (data.itemList.Count >= 30)
      return;
    data.itemList.Add(component);
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

  private void UpdateDummyDragScroll()
  {
    this.DummyDragScroll.height = (double) this.ScrollView.panel.height <= (double) this.CurrentTotalHeight ? (int) ((double) this.CurrentTotalHeight - 20.0) : (int) ((double) this.ScrollView.panel.height - 20.0);
    this.DragScrollTrans.localPosition = new Vector3(this.ScrollView.panel.clipOffset.x, -this.CurrentTotalHeight, 0.0f);
    this.DragScrollCollider.size = new Vector3(this.ScrollView.panel.finalClipRegion.z, this.ScrollView.panel.finalClipRegion.w - this.ScrollView.panel.clipSoftness.y * 2f, 0.0f);
  }

  private bool IsAllowedUser(int userId)
  {
    return !MonoBehaviourSingleton<BlackListManager>.IsValid() || !MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(userId);
  }

  private void UpdateDonate()
  {
    this.pinDonate = MonoBehaviourSingleton<GuildManager>.I.pinDonate;
    if (this.pinDonate != null)
      this.StartCoroutine(this.AddDonatePin(this.pinDonate));
    this.SetButtonEvent((Enum) GuildMessage.UI.BTN_DONATE_CHAT, new EventDelegate((EventDelegate.Callback) (() =>
    {
      this._viewType = GuildMessage.VIEW_TYPE.CHAT;
      this.RefreshUI();
    })));
    List<DonateInfo> donate_list = MonoBehaviourSingleton<GuildManager>.I.donateList;
    this.SetGrid((Enum) GuildMessage.UI.GRD_DONATE, "GuildMessageDonateListItem", donate_list.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      DonateInfo info = donate_list[i];
      ((Component) t).GetComponent<GuildMessageDonateListItem>().SetDonateInfo(info);
      this.SetActive(t, (Enum) GuildMessage.UI.OBJ_TARGET, info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
      this.SetActive(t, (Enum) GuildMessage.UI.OBJ_OWNER, info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
      Transform transform = info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id ? this.FindCtrl(t, (Enum) GuildMessage.UI.OBJ_TARGET) : this.FindCtrl(t, (Enum) GuildMessage.UI.OBJ_OWNER);
      this.SetLabelText(t, (Enum) GuildMessage.UI.LBL_CHAT_MESSAGE, info.msg);
      bool is_visible = info.itemNum >= info.quantity;
      this.SetActive(transform, (Enum) GuildMessage.UI.OBJ_FULL, is_visible);
      this.SetActive(transform, (Enum) GuildMessage.UI.OBJ_NORMAL, !is_visible);
      this.SetSliderValue(transform, (Enum) GuildMessage.UI.SLD_PROGRESS, (float) info.itemNum / (float) info.quantity);
      this.SetLabelText(transform, (Enum) GuildMessage.UI.LBL_CHAT_MESSAGE, info.msg);
      this.SetLabelText(transform, (Enum) GuildMessage.UI.LBL_USER_NAME, info.nickName);
      this.SetLabelText(transform, (Enum) GuildMessage.UI.LBL_MATERIAL_NAME, info.materialName);
      int itemNum1 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) info.itemId), 1);
      this.SetLabelText(transform, (Enum) GuildMessage.UI.LBL_QUATITY, (object) itemNum1);
      this.SetLabelText(transform, (Enum) GuildMessage.UI.LBL_DONATE_NUM, (object) info.itemNum);
      this.SetLabelText(transform, (Enum) GuildMessage.UI.LBL_DONATE_MAX, (object) info.quantity);
      if (info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      {
        if (!is_visible)
          this.SetButtonEvent(transform, (Enum) GuildMessage.UI.BTN_ASK, new EventDelegate((EventDelegate.Callback) (() => this.DispatchEvent("ASK", (object) info))));
        else
          this.SetButtonEnabled(transform, (Enum) GuildMessage.UI.BTN_ASK, false);
      }
      else
      {
        int itemNum2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) info.itemId), 1);
        if (!is_visible && itemNum2 > 0 && info.itemNum < info.quantity)
          this.SetButtonEvent(transform, (Enum) GuildMessage.UI.BTN_GIFT, new EventDelegate((EventDelegate.Callback) (() => this.DispatchEvent("SEND", (object) info))));
        else
          this.SetButtonEnabled(transform, (Enum) GuildMessage.UI.BTN_GIFT, false);
      }
      ItemInfo itemInfo = ItemInfo.CreateItemInfo(new Network.Item()
      {
        uniqId = "0",
        itemId = info.itemId,
        num = info.itemNum
      });
      ItemSortData data = new ItemSortData();
      data.SetItem((object) itemInfo);
      this.SetItemIcon(this.FindCtrl(transform, (Enum) GuildMessage.UI.OBJ_MATERIAL_ICON), data, this.FindCtrl(this.GetCtrl((Enum) GuildMessage.UI.OBJ_DONATE_PANEL), (Enum) GuildMessage.UI.PNL_MATERIAL_INFO), i);
    }));
    this.SetActive((Enum) GuildMessage.UI.LBL_NO_DONATE, donate_list.Count == 0);
  }

  private void OnQuery_DONATE()
  {
    this._viewType = GuildMessage.VIEW_TYPE.DONATE;
    this.RefreshUI();
  }

  private void OnCloseDialog_GuildDonateSendDialog() => this.RefreshUI();

  private void OnQuery_RETURN()
  {
    this._viewType = GuildMessage.VIEW_TYPE.CHAT;
    this.RefreshUI();
  }

  private void SetItemIcon(
    Transform holder,
    ItemSortData data,
    Transform parent_scroll,
    int event_data)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
    int icon_id = -1;
    if (data != null)
    {
      itemIconType = data.GetIconType();
      icon_id = data.GetIconID();
      rarity = new RARITY_TYPE?(data.GetRarity());
      element = data.GetIconElement();
      magi_enable_icon_type = data.GetIconMagiEnableType();
      data.GetNum();
    }
    bool is_new = false;
    switch (itemIconType)
    {
      case ITEM_ICON_TYPE.NONE:
        int enemy_icon_id = 0;
        if (itemIconType == ITEM_ICON_TYPE.ITEM)
          enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
        ItemIcon itemIcon;
        if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
          itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
          {
            icon_type = data.GetIconType(),
            icon_id = data.GetIconID(),
            rarity = new RARITY_TYPE?(data.GetRarity()),
            parent = holder,
            element = data.GetIconElement(),
            magi_enable_equip_type = data.GetIconMagiEnableType(),
            num = data.GetNum(),
            enemy_icon_id = enemy_icon_id,
            questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
          });
        else
          itemIcon = ItemIcon.Create(itemIconType, icon_id, rarity, holder, element, magi_enable_icon_type, event_name: "DROP", event_data: event_data, is_new: is_new, enemy_icon_id: enemy_icon_id);
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
        break;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (data.GetUniqID() != 0UL)
        {
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(itemIconType, data.GetUniqID());
          goto case ITEM_ICON_TYPE.NONE;
        }
        goto case ITEM_ICON_TYPE.NONE;
      default:
        is_new = true;
        goto case ITEM_ICON_TYPE.NONE;
    }
  }

  private void OnQuery_DROP()
  {
    DonateInfo donate = MonoBehaviourSingleton<GuildManager>.I.donateList[(int) GameSection.GetEventData()];
    uint itemId = (uint) donate.itemId;
    ItemSortData itemSortData = new ItemSortData();
    ItemInfo itemInfo = new ItemInfo();
    itemInfo.uniqueID = 0UL;
    itemInfo.tableID = itemId;
    itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
    itemInfo.num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemId);
    itemSortData.SetItem((object) itemInfo);
    GameSection.SetEventData((object) new object[2]
    {
      (object) itemSortData,
      (object) donate.itemNum
    });
  }

  private void OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
  {
    if (clanUpdateStatusData.type == 1)
      return;
    if (clanUpdateStatusData.type == 2)
    {
      if (clanUpdateStatusData.status == 2)
        this.need_update_donate_later = true;
      else
        this.need_update_donate = true;
    }
    else if (clanUpdateStatusData.type == 3)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId)
        return;
      this.need_update_pin = true;
    }
    else
    {
      int type = clanUpdateStatusData.type;
    }
  }

  private void ClanUpdateStatus()
  {
    if (!this.isInitialized)
      return;
    if (this.need_update_pin)
      this.RefreshClanPinData();
    if (this.need_update_donate && this._viewType == GuildMessage.VIEW_TYPE.DONATE)
    {
      this.need_update_donate = false;
      this.RefreshUI();
    }
    else
    {
      if (!this.need_update_donate_later || this._viewType != GuildMessage.VIEW_TYPE.DONATE)
        return;
      this.need_update_donate_later = false;
      this.StartCoroutine(this.ShowDisableState());
    }
  }

  private IEnumerator ShowDisableState()
  {
    yield return (object) new WaitForSeconds(3f);
    this.RefreshUI();
  }

  private void RefreshClanPinData()
  {
    this.need_update_pin = false;
    bool stayEvent = false;
    if (!GameSceneEvent.IsStay())
    {
      stayEvent = true;
      GameSceneEvent.Stay();
    }
    MonoBehaviourSingleton<GuildManager>.I.GetAllPinData((Action<bool, GuildGetPinModel>) ((success, ret) =>
    {
      if (success)
      {
        if (!string.IsNullOrEmpty(ret.result.message))
        {
          if (ret.result.type != 2)
          {
            this.pinMessage = new ClanChatLogMessageData();
            this.pinMessage.fromUserId = ret.result.fromUserId;
            this.pinMessage.id = ret.result.id;
            this.pinMessage.type = ret.result.type;
            this.pinMessage.message = ret.result.message;
            this.pinMessage.uuid = ret.result.uuid;
            if (this.pinMessage.type == 1)
              this.pinMessage.stampId = int.Parse(ret.result.message);
            this.senerInfo = ret.result.charInfo;
            this.StartCoroutine(this.AddChatPinMsg());
          }
          else if (this._viewType == GuildMessage.VIEW_TYPE.DONATE)
            this.RefreshUI();
        }
        else
          this.RemovePinMsg();
      }
      if (!stayEvent)
        return;
      GameSceneEvent.Resume();
    }));
  }

  private void OnReceiveClanChatUnPin() => this.RemovePinMsg();

  private void UpdateChatPin()
  {
    if (Object.op_Equality((Object) this.chatPinItem, (Object) null))
    {
      if (this._chatType != GuildMessage.CHAT_TYPE.CLAN || this.pinMessage == null)
        return;
      this.StartCoroutine(this.AddChatPinMsg());
    }
    else if (this._chatType == GuildMessage.CHAT_TYPE.MEMBER && ((Component) this.chatPinItem).gameObject.activeSelf)
    {
      this.ScrollView.panel.baseClipRegion = this.baseClipRegion;
      ((Component) this.chatPinItem).gameObject.SetActive(false);
    }
    else
    {
      if (this._chatType != GuildMessage.CHAT_TYPE.CLAN || ((Component) this.chatPinItem).gameObject.activeSelf)
        return;
      this.ScrollView.panel.baseClipRegion = this.currentClipRegion;
      ((Component) this.chatPinItem).gameObject.SetActive(true);
    }
  }

  private void OnQuery_UNPIN()
  {
    this.chatPinItem.HideUnPinButton();
    this.DispatchEvent("UNPIN_MSG", (object) "Are you sure you want to unpin this message?");
  }

  private void OnQuery_GuildUnPinMessageDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendClanChatUnPin((Action<bool, GuildChatUnPinModel>) ((success, ret) =>
    {
      if (success)
        this.RemovePinMsg();
      GameSection.ResumeEvent(success);
    }));
  }

  private void RemovePinMsg()
  {
    if (Object.op_Equality((Object) this.chatPinItem, (Object) null))
      return;
    this.ClearRenderModel(((Component) this.chatPinItem).transform, (Enum) GuildMessage.UI.TEX_MODEL);
    Object.DestroyImmediate((Object) ((Component) this.chatPinItem).gameObject);
    this.chatPinItem = (GuildChatPinItem) null;
    this.pinMessage = (ClanChatLogMessageData) null;
    this.CalculateBaseClipScrollView();
  }

  private void OnQuery_GuildUnPinMessageDialog_NO() => this.chatPinItem.HideUnPinButton();

  private void OnQuery_PIN_MSG()
  {
    this.pinMessage = GameSection.GetEventData() as ClanChatLogMessageData;
    if (Object.op_Inequality((Object) this.chatPinItem, (Object) null))
      this.DispatchEvent("REPLACE_PIN_MSG", (object) "You already have a pinned message. Replace with this?");
    else
      this.SendPinMsg();
  }

  private void OnQuery_GuildReplacePinMessageDialog_YES() => this.SendPinMsg();

  private void OnQuery_GuildReplacePinMessageDialog_NO() => this.HidePinButton();

  private void SendPinMsg()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendClanChatPin(this.pinMessage.fromUserId, this.pinMessage.id, this.pinMessage.uuid, this.pinMessage.type, this.pinMessage.message, (Action<bool, GuildChatPinModel>) ((success, ret) =>
    {
      if (success)
      {
        this.HidePinButton();
        this.senerInfo = ret.result.charInfo;
        this.StartCoroutine(this.AddChatPinMsg());
      }
      GameSection.ResumeEvent(success);
    }));
  }

  private IEnumerator AddChatPinMsg()
  {
    if (this.senerInfo != null && !Object.op_Equality((Object) this.m_ChatPinItemPrefab, (Object) null))
    {
      if (Object.op_Equality((Object) this.chatPinItem, (Object) null))
      {
        this.chatPinItem = ((Component) ResourceUtility.Realizes((Object) this.m_ChatPinItemPrefab, ((Component) this.RootRect).transform, 5)).GetComponent<GuildChatPinItem>();
        ((Component) this.chatPinItem).transform.localPosition = new Vector3(0.0f, 370f, 0.0f);
      }
      yield return (object) null;
      if (this.pinMessage.type == 0)
        this.chatPinItem.ShowPinMsg(this.senerInfo.name, this.pinMessage.message);
      else if (this.pinMessage.type == 1)
        this.chatPinItem.ShowPinStamp(this.senerInfo.name, this.pinMessage.stampId);
      this.CalculateBaseClipScrollView();
      this.ClearRenderModel(((Component) this.chatPinItem).transform, (Enum) GuildMessage.UI.TEX_MODEL);
      yield return (object) null;
      this.SetRenderPlayerModel(((Component) this.chatPinItem).transform, (Enum) GuildMessage.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(this.senerInfo, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    }
  }

  private void OnQuery_HIDE_PIN_BTN()
  {
    int result = 0;
    int.TryParse(GameSection.GetEventData() as string, out result);
    int count = this.CurrentData.itemList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.CurrentData.itemList[index].msgId != result)
        this.CurrentData.itemList[index].HidePinButton();
    }
  }

  private void HidePinButton()
  {
    int count = this.CurrentData.itemList.Count;
    for (int index = 0; index < count; ++index)
      this.CurrentData.itemList[index].HidePinButton();
  }

  private IEnumerator AddDonatePin(DonateInfo info)
  {
    if (!Object.op_Equality((Object) this.m_DonatePinItemPrefab, (Object) null))
    {
      int num1 = Object.op_Equality((Object) this.donatePinItem, (Object) null) ? 1 : 0;
      if (Object.op_Equality((Object) this.donatePinItem, (Object) null))
      {
        this.donatePinItem = ((Component) ResourceUtility.Realizes((Object) this.m_DonatePinItemPrefab, ((Component) this.GetCtrl((Enum) GuildMessage.UI.WGT_DONATE_ROOT)).transform, 5)).GetComponent<GuildDonatePinItem>();
        ((Component) this.donatePinItem).transform.localPosition = new Vector3(0.0f, 375f, 0.0f);
      }
      this.UpdateDonatePinUI(info);
      this.donatePinItem.ShowPin(info);
      UIPanel component = ((Component) ((Component) this.GetCtrl((Enum) GuildMessage.UI.SCR_DONATE)).GetComponent<UIScrollView>()).gameObject.GetComponent<UIPanel>();
      int num2 = this.donatePinItem.GetBaseHeight + 30;
      if (num1 != 0)
        this.baseDonateClipRegion = component.baseClipRegion;
      component.baseClipRegion = new Vector4(this.baseDonateClipRegion.x, this.baseDonateClipRegion.y - (float) num2 / 2f, this.baseDonateClipRegion.z, this.baseDonateClipRegion.w - (float) num2);
      yield break;
    }
  }

  private void UpdateDonatePinUI(DonateInfo info)
  {
    Transform transform1 = ((Component) this.donatePinItem).transform;
    this.SetActive(transform1, (Enum) GuildMessage.UI.OBJ_TARGET, info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    this.SetActive(transform1, (Enum) GuildMessage.UI.OBJ_OWNER, info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    Transform transform2 = info.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id ? this.FindCtrl(transform1, (Enum) GuildMessage.UI.OBJ_TARGET) : this.FindCtrl(transform1, (Enum) GuildMessage.UI.OBJ_OWNER);
    this.SetLabelText(transform1, (Enum) GuildMessage.UI.LBL_CHAT_MESSAGE, info.msg);
    bool is_visible = info.itemNum >= info.quantity;
    this.SetActive(transform2, (Enum) GuildMessage.UI.OBJ_FULL, is_visible);
    this.SetActive(transform2, (Enum) GuildMessage.UI.OBJ_NORMAL, !is_visible);
    this.SetSliderValue(transform2, (Enum) GuildMessage.UI.SLD_PROGRESS, (float) info.itemNum / (float) info.quantity);
    this.SetLabelText(transform2, (Enum) GuildMessage.UI.LBL_CHAT_MESSAGE, info.msg);
    this.SetLabelText(transform2, (Enum) GuildMessage.UI.LBL_USER_NAME, info.nickName);
    this.SetLabelText(transform2, (Enum) GuildMessage.UI.LBL_MATERIAL_NAME, info.materialName);
    int itemNum1 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) info.itemId), 1);
    this.SetLabelText(transform2, (Enum) GuildMessage.UI.LBL_QUATITY, (object) itemNum1);
    this.SetLabelText(transform2, (Enum) GuildMessage.UI.LBL_DONATE_NUM, (object) info.itemNum);
    this.SetLabelText(transform2, (Enum) GuildMessage.UI.LBL_DONATE_MAX, (object) info.quantity);
    if (info.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      this.SetButtonEvent(transform2, (Enum) GuildMessage.UI.BTN_ASK, new EventDelegate((EventDelegate.Callback) (() => this.DispatchEvent("ASK", (object) info))));
    }
    else
    {
      int itemNum2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) info.itemId), 1);
      if (!is_visible && itemNum2 > 0 && info.itemNum < info.quantity)
        this.SetButtonEvent(transform2, (Enum) GuildMessage.UI.BTN_GIFT, new EventDelegate((EventDelegate.Callback) (() => this.DispatchEvent("SEND", (object) info))));
      else
        this.SetButtonEnabled(transform2, (Enum) GuildMessage.UI.BTN_GIFT, false);
    }
    ItemInfo itemInfo = ItemInfo.CreateItemInfo(new Network.Item()
    {
      uniqId = "0",
      itemId = info.itemId,
      num = info.itemNum
    });
    ItemSortData data = new ItemSortData();
    data.SetItem((object) itemInfo);
    this.SetItemIcon(this.FindCtrl(transform2, (Enum) GuildMessage.UI.OBJ_MATERIAL_ICON), data, this.FindCtrl(this.GetCtrl((Enum) GuildMessage.UI.OBJ_DONATE_PANEL), (Enum) GuildMessage.UI.PNL_MATERIAL_INFO), 0);
  }

  private void OnQuery_PIN_DONATE()
  {
    this.pinDonate = GameSection.GetEventData() as DonateInfo;
    if (Object.op_Inequality((Object) this.donatePinItem, (Object) null))
      this.DispatchEvent("REPLACE_PIN_DONATE", (object) "This will replace your current pinned donate");
    else
      this.SendPinDonate(this.pinDonate);
  }

  private void OnQuery_GuildReplacePinDonateDialog_YES() => this.SendPinDonate(this.pinDonate);

  private void OnQuery_GuildReplacePinDonateDialog_NO()
  {
  }

  private void OnQuery_GuildUnPinDonateDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendClanChatUnPin((Action<bool, GuildChatUnPinModel>) ((success, ret) =>
    {
      if (success)
      {
        ((Component) ((Component) this.GetCtrl((Enum) GuildMessage.UI.SCR_DONATE)).GetComponent<UIScrollView>()).gameObject.GetComponent<UIPanel>().baseClipRegion = this.baseDonateClipRegion;
        Object.DestroyImmediate((Object) ((Component) this.donatePinItem).gameObject);
        this.donatePinItem = (GuildDonatePinItem) null;
      }
      GameSection.ResumeEvent(success);
    }));
  }

  private void OnQuery_GuildUnPinDonateDialog_NO()
  {
  }

  private void OnQuery_UNPIN_DONATE_BTN()
  {
    this.DispatchEvent("UNPIN_DONATE", (object) "Are you sure?");
  }

  private void SendPinDonate(DonateInfo info)
  {
    if (info == null || info.expired <= 0.0)
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendClanChatPin(0, info.id, "", 2, "", (Action<bool, GuildChatPinModel>) ((success, ret) =>
    {
      if (success)
      {
        MonoBehaviourSingleton<GuildManager>.I.pinDonate = info;
        this.StartCoroutine(this.AddDonatePin(info));
      }
      GameSection.ResumeEvent(success);
    }));
  }

  private void UpdateAdvisoryItem()
  {
    if (!Object.op_Equality((Object) this.chatAdvisoryItem, (Object) null) || this._chatType != GuildMessage.CHAT_TYPE.CLAN || this._advisaryData == null)
      return;
    this.StartCoroutine(this.AddAdvisary());
  }

  private IEnumerator AddAdvisary()
  {
    if (this._advisaryData != null && !Object.op_Equality((Object) this.m_ChatAdvisaryItemPrefab, (Object) null) && !GuildChatAdvisoryItem.HasReadNew())
    {
      if (Object.op_Equality((Object) this.chatAdvisoryItem, (Object) null))
      {
        this.chatAdvisoryItem = ((Component) ResourceUtility.Realizes((Object) this.m_ChatAdvisaryItemPrefab, this.GetCtrl((Enum) GuildMessage.UI.WGT_CHAT_TOP), 5)).GetComponent<GuildChatAdvisoryItem>();
        ((Component) this.chatAdvisoryItem).transform.localPosition = new Vector3(0.0f, 370f, 0.0f);
      }
      yield return (object) null;
      this.chatAdvisoryItem.Init(this._advisaryData.title, this._advisaryData.content);
      this.SetButtonEvent(this.chatAdvisoryItem.close, new EventDelegate((EventDelegate.Callback) (() =>
      {
        GuildChatAdvisoryItem.SetReadNew();
        if (!Object.op_Inequality((Object) this.chatAdvisoryItem, (Object) null))
          return;
        Object.DestroyImmediate((Object) ((Component) this.chatAdvisoryItem).gameObject);
        this.chatAdvisoryItem = (GuildChatAdvisoryItem) null;
      })));
    }
  }

  private void CalculateBaseClipScrollView()
  {
    Vector4 vector4;
    // ISSUE: explicit constructor call
    ((Vector4) ref vector4).\u002Ector(this.baseClipRegion.x, this.baseClipRegion.y, this.baseClipRegion.z, this.baseClipRegion.w);
    if (Object.op_Inequality((Object) this.chatPinItem, (Object) null) && ((Component) this.chatPinItem).gameObject.activeSelf)
    {
      float num = (float) this.chatPinItem.GetHeight - 15f;
      // ISSUE: explicit constructor call
      ((Vector4) ref vector4).\u002Ector(vector4.x, vector4.y - num / 2f, vector4.z, vector4.w - num);
    }
    this.ScrollView.panel.baseClipRegion = vector4;
  }

  [Serializable]
  public class ChatPostRequest
  {
    public GuildMessage.ChatPostRequest.TYPE Type { get; private set; }

    public string uuId { get; private set; }

    public int chatId { get; private set; }

    public int receiveId { get; private set; }

    public int senderId { get; private set; }

    public string senderName { get; private set; }

    public int stampId { get; private set; }

    public string message { get; private set; }

    public ChatPostRequest(string uuID, int cid, int rId, int sId, string sName, string message)
    {
      this.uuId = uuID;
      this.chatId = cid;
      this.Type = GuildMessage.ChatPostRequest.TYPE.Message;
      this.receiveId = rId;
      this.senderId = sId;
      this.senderName = sName;
      this.message = message;
    }

    public ChatPostRequest(string uuID, int cid, int rId, int sId, string sName, int stampId)
    {
      this.uuId = uuID;
      this.chatId = cid;
      this.Type = GuildMessage.ChatPostRequest.TYPE.Stamp;
      this.receiveId = rId;
      this.senderId = sId;
      this.senderName = sName;
      this.stampId = stampId;
    }

    public ChatPostRequest(string message)
    {
      this.Type = GuildMessage.ChatPostRequest.TYPE.Notification;
      this.message = message;
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
    private Queue<GuildMessage.ChatPostRequest> queue = new Queue<GuildMessage.ChatPostRequest>();

    public bool HasOverFlowed { get; private set; }

    public int Count => this.queue.Count;

    public void Enqueue(GuildMessage.ChatPostRequest q)
    {
      this.queue.Enqueue(q);
      if (this.queue.Count <= GuildMessage.PostRequestQueue.PENDING_MAX)
        return;
      this.queue.Dequeue();
      this.HasOverFlowed = true;
    }

    public GuildMessage.ChatPostRequest Dequeue()
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
      this.rootObject = root;
      this.itemList = new List<GuildChatItem>();
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
    SPR_ICON_NEW,
  }

  private enum CHAT_TYPE
  {
    CLAN,
    MEMBER,
  }

  public enum VIEW_TYPE
  {
    CHAT,
    DONATE,
  }

  public enum ClanStatusType
  {
    DISBAND = 1,
    DONATION = 2,
    PIN = 3,
    KICK = 4,
  }

  public enum ClanDonationStatus
  {
    NEW = 1,
    DELETE = 2,
    UPDATE = 3,
  }
}
