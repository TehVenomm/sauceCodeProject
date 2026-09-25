// Decompiled with JetBrains decompiler
// Type: FriendMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendMessage : GameSection
{
  private const int CHAT_ITEM_OFFSET = 22;
  private const float SOFTNESS_HEIGHT = 10f;
  private const float SPRING_STRENGTH = 20f;
  private const float CHAT_WIDTH = 410f;
  private const float SCROLL_BAR_OFFSET = 48f;
  private const float LOADPREV_SCROLLY_THRESHOLD = 30f;
  private string talkerName = string.Empty;
  private int nowPage;
  private int loadedPage;
  private GameObject chatItemPrefab;
  private GameObject chatStampListPrefab;
  private FriendMessage.MessageItemListData itemListData;
  private List<FriendMessageData> postMessageList = new List<FriendMessageData>();
  private Queue<FriendMessage.PostMessageData> messageQueue = new Queue<FriendMessage.PostMessageData>();
  private List<int> m_StampIdListCanPost;
  private UIScrollView m_ScrollView;
  private Transform m_ScrollViewTrans;
  private UIWidget m_DummyDragScroll;
  private BoxCollider m_DragScrollCollider;
  private Transform m_DragScrollTrans;
  private UISprite m_BackgroundInFrame;
  private UIInput m_Input;
  private ChatInputFrame m_InputFrame;
  private readonly float IntervalSendGetNoRead = 5f;
  private float interval;
  private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0.0f);
  private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0.0f);
  private bool updateStampList;

  private float CurrentTotalHeight
  {
    get
    {
      float currentTotalHeight = 0.0f;
      if (this.itemListData != null)
        currentTotalHeight = this.itemListData.currentTotalHeight;
      return currentTotalHeight;
    }
  }

  private float BasePosY
  {
    get
    {
      float basePosY = 0.0f;
      if (this.itemListData != null)
        basePosY = this.itemListData.basePosY;
      return basePosY;
    }
  }

  private UIScrollView ScrollView
  {
    get
    {
      if (Object.op_Equality((Object) this.m_ScrollView, (Object) null))
        this.m_ScrollView = ((Component) this.GetCtrl((Enum) FriendMessage.UI.SCR_CHAT)).GetComponent<UIScrollView>();
      return this.m_ScrollView;
    }
  }

  private Transform ScrollViewTrans
  {
    get
    {
      if (Object.op_Equality((Object) this.m_ScrollViewTrans, (Object) null))
        this.m_ScrollViewTrans = this.GetCtrl((Enum) FriendMessage.UI.SCR_CHAT);
      return this.m_ScrollViewTrans;
    }
  }

  private UIWidget DummyDragScroll
  {
    get
    {
      if (Object.op_Equality((Object) this.m_DummyDragScroll, (Object) null))
        this.m_DummyDragScroll = ((Component) this.GetCtrl((Enum) FriendMessage.UI.WGT_DUMMY_DRAG_SCROLL)).GetComponent<UIWidget>();
      return this.m_DummyDragScroll;
    }
  }

  private BoxCollider DragScrollCollider
  {
    get
    {
      if (Object.op_Equality((Object) this.m_DragScrollCollider, (Object) null))
        this.m_DragScrollCollider = ((Component) this.GetCtrl((Enum) FriendMessage.UI.WGT_DUMMY_DRAG_SCROLL)).GetComponent<BoxCollider>();
      return this.m_DragScrollCollider;
    }
  }

  private Transform DragScrollTrans
  {
    get
    {
      if (Object.op_Equality((Object) this.m_DragScrollTrans, (Object) null))
        this.m_DragScrollTrans = this.GetCtrl((Enum) FriendMessage.UI.WGT_DUMMY_DRAG_SCROLL);
      return this.m_DragScrollTrans;
    }
  }

  private UISprite BackgroundInFrame
  {
    get
    {
      if (Object.op_Equality((Object) this.m_BackgroundInFrame, (Object) null))
        this.m_BackgroundInFrame = ((Component) this.GetCtrl((Enum) FriendMessage.UI.SPR_BG_IN_FRAME)).GetComponent<UISprite>();
      return this.m_BackgroundInFrame;
    }
  }

  private UIInput Input
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Input, (Object) null))
        this.m_Input = ((Component) this.GetCtrl((Enum) FriendMessage.UI.IPT_POST)).GetComponent<UIInput>();
      return this.m_Input;
    }
  }

  private ChatInputFrame InputFrame
  {
    get
    {
      if (Object.op_Equality((Object) this.m_InputFrame, (Object) null))
        this.m_InputFrame = ((Component) this.GetCtrl((Enum) FriendMessage.UI.OBJ_INPUT_FRAME)).GetComponent<ChatInputFrame>();
      return this.m_InputFrame;
    }
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_quest_chatitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatItem");
    LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
    this.talkerName = this.GetTalkerName(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId);
    this.SetLabelText((Enum) FriendMessage.UI.Title_U, this.talkerName);
    this.SetLabelText((Enum) FriendMessage.UI.Title_D, this.talkerName);
    this.nowPage = 0;
    this.loadedPage = -1;
    this.itemListData = new FriendMessage.MessageItemListData(((Component) this.GetCtrl((Enum) FriendMessage.UI.OBJ_ROOM_ITEM_LIST_ROOT)).gameObject);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.chatItemPrefab = lo_quest_chatitem.loadedObject as GameObject;
    this.chatStampListPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
    this.SetSliderLimit();
    this.DummyDragScroll.width = 410;
    this.ResetStampIdList();
    this.Reset();
    this.postMessageList.Clear();
    this.ScrollView.onDragFinished += new UIScrollView.OnDragNotification(this.OnDragFinished);
    this.InputFrame.onChange += (System.Action) (() => this.OnInput());
    this.InputFrame.onSubmit += (System.Action) (() => this.OnTouchPost());
    if (MonoBehaviourSingleton<FriendManager>.I.talkUser.userId == 0)
    {
      ((Component) this.GetCtrl((Enum) FriendMessage.UI.SCR_STAMP_LIST)).gameObject.SetActive(false);
      ((Component) this.GetCtrl((Enum) FriendMessage.UI.OBJ_POST_FRAME)).gameObject.SetActive(false);
    }
    else
    {
      this.updateStampList = true;
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += new System.Action(this.InitStampList);
    }
    base.Initialize();
  }

  private void Update()
  {
    if (!this.isInitialized)
      return;
    this.DragScrollCollider.center = Vector2.op_Implicit(new Vector2(this.ScrollView.panel.baseClipRegion.x, -(float) ((double) this.ScrollView.panel.baseClipRegion.w - (double) this.ScrollView.panel.baseClipRegion.y + (double) this.DragScrollTrans.localPosition.y - ((double) this.ScrollView.panel.finalClipRegion.w + (double) this.ScrollView.panel.clipOffset.y))));
    this.interval += Time.deltaTime;
    if ((double) this.IntervalSendGetNoRead <= (double) this.interval)
    {
      this.interval = 0.0f;
      this.DispatchEvent("SEND_GET_NOREAD_MESSAGE");
    }
    List<FriendMessageData> messageDetailList = MonoBehaviourSingleton<FriendManager>.I.messageDetailList;
    if (messageDetailList.Count > this.postMessageList.Count)
    {
      HashSet<FriendMessageData> friendMessageDataSet = new HashSet<FriendMessageData>((IEnumerable<FriendMessageData>) messageDetailList);
      friendMessageDataSet.ExceptWith((IEnumerable<FriendMessageData>) this.postMessageList);
      FriendMessageData[] friendMessageDataArray = new FriendMessageData[friendMessageDataSet.Count];
      friendMessageDataSet.CopyTo(friendMessageDataArray);
      long num = 0;
      if (0 < this.postMessageList.Count)
        num = this.postMessageList[0].lid;
      for (int index = friendMessageDataArray.Length - 1; index >= 0; --index)
      {
        if (num > friendMessageDataArray[0].lid)
          this.messageQueue.Enqueue(new FriendMessage.PostMessageData(friendMessageDataArray[index], true, false));
      }
      for (int index = 0; index < friendMessageDataArray.Length; ++index)
      {
        if (num <= friendMessageDataArray[0].lid)
        {
          bool _forceScroll = index == friendMessageDataArray.Length - 1;
          this.messageQueue.Enqueue(new FriendMessage.PostMessageData(friendMessageDataArray[index], false, _forceScroll));
        }
      }
      this.postMessageList.AddRange((IEnumerable<FriendMessageData>) friendMessageDataArray);
      this.postMessageList.Sort((Comparison<FriendMessageData>) ((l, r) => l.lid.CompareTo(r.lid)));
    }
    if (0 >= this.messageQueue.Count)
      return;
    FriendMessage.PostMessageData postMessageData = this.messageQueue.Dequeue();
    this.PostUI(postMessageData.message, postMessageData.topPosition, postMessageData.forceScroll);
    if (0 <= this.loadedPage || this.messageQueue.Count != 0)
      return;
    this.loadedPage = 0;
  }

  public override void Exit()
  {
    if (MonoBehaviourSingleton<FriendManager>.IsValid())
      MonoBehaviourSingleton<FriendManager>.I.ResetUser();
    base.Exit();
  }

  private string GetTalkerName(int user_id)
  {
    return MonoBehaviourSingleton<FriendManager>.I.talkUser.userId == user_id ? MonoBehaviourSingleton<FriendManager>.I.talkUser.name : string.Empty;
  }

  private void OnQuery_SEND_GET_MESSAGE_DETAIL()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId, this.nowPage, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      if (!is_success)
        return;
      this.ScrollView.DisableSpring();
      this.RefreshUI();
      this.loadedPage = this.nowPage;
    }));
  }

  private void OnQuery_SEND_GET_NOREAD_MESSAGE()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetNoreadMessage((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_SEND()
  {
    string eventData = GameSection.GetEventData() as string;
    string message = !string.IsNullOrEmpty(eventData) ? eventData.Replace("\n", "") : "";
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId, message, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_SEND_STAMP()
  {
    int num = 1;
    object eventData = GameSection.GetEventData();
    if (eventData != null)
      num = (int) eventData;
    string message = "[STAMP]" + num.ToString();
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId, message, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void PostUI(FriendMessageData data, bool topPosition, bool forceScroll)
  {
    if (data.message.StartsWith("[STAMP]"))
    {
      string s = data.message.Replace("[STAMP]", "");
      this.PostUIStamp(data.fromUserId, int.Parse(s), topPosition, forceScroll);
    }
    else
      this.PostUIMessage(data.fromUserId, data.message, topPosition, forceScroll);
  }

  private void PostUIMessage(int userId, string message, bool topPosition, bool forceScroll)
  {
    this.AddNextChatItem((Action<ChatItem>) (chatItem => chatItem.Init(userId, this.talkerName, message)), topPosition, forceScroll);
  }

  private void PostUIStamp(int userId, int stampId, bool topPosition, bool forceScroll)
  {
    if (!this.IsValidStampId(stampId) || Singleton<StampTable>.I.GetData((uint) stampId) == null)
      return;
    this.AddNextChatItem((Action<ChatItem>) (chatItem => chatItem.Init(userId, this.talkerName, stampId)), topPosition, forceScroll);
  }

  private void AddNextChatItem(Action<ChatItem> initializer, bool topPosition, bool forceScroll)
  {
    if (Object.op_Equality((Object) this.chatItemPrefab, (Object) null))
      return;
    FriendMessage.MessageItemListData itemListData = this.itemListData;
    ChatItem component = ((Component) ResourceUtility.Realizes((Object) this.chatItemPrefab, itemListData.rootObject.transform, 5)).GetComponent<ChatItem>();
    if (topPosition)
    {
      initializer(component);
      float num = component.height + 22f;
      itemListData.basePosY += num;
      ((Component) component).transform.localPosition = new Vector3(-15f, itemListData.basePosY, 0.0f);
      itemListData.currentTotalHeight += num;
    }
    else
    {
      if (itemListData.itemList.Count > 0)
        itemListData.currentTotalHeight += 22f;
      float num = itemListData.currentTotalHeight - itemListData.basePosY;
      ((Component) component).transform.localPosition = new Vector3(-15f, -num, 0.0f);
      initializer(component);
      itemListData.currentTotalHeight += component.height;
    }
    this.UpdateDummyDragScroll();
    if (!topPosition & forceScroll)
      this.ForceScroll((float) ((double) itemListData.currentTotalHeight + (double) this.ScrollView.panel.baseClipRegion.y - (double) this.ScrollView.panel.baseClipRegion.w * 0.5 + ((double) this.ScrollViewTrans.localPosition.y + (double) this.ScrollView.panel.clipOffset.y)) + this.ScrollView.panel.clipSoftness.y - itemListData.basePosY, true);
    if (topPosition)
      itemListData.itemList.Insert(0, component);
    else
      itemListData.itemList.Add(component);
  }

  private void UpdateDummyDragScroll()
  {
    this.DummyDragScroll.height = (double) this.ScrollView.panel.height <= (double) this.CurrentTotalHeight ? (int) ((double) this.CurrentTotalHeight - 20.0) : (int) ((double) this.ScrollView.panel.height - 20.0);
    this.DragScrollTrans.localPosition = new Vector3(this.ScrollView.panel.clipOffset.x, this.BasePosY - this.CurrentTotalHeight, 0.0f);
    this.DragScrollCollider.size = new Vector3(this.ScrollView.panel.finalClipRegion.z, this.ScrollView.panel.finalClipRegion.w - this.ScrollView.panel.clipSoftness.y * 2f, 0.0f);
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

  private void SetSliderLimit()
  {
    UIPanel component = ((Component) this.GetCtrl((Enum) FriendMessage.UI.WGT_SLIDE_LIMIT)).GetComponent<UIPanel>();
    component.topAnchor.absolute = (int) this.HOME_SLIDER_OPEN_POS.y + 9;
    component.bottomAnchor.absolute = (int) this.HOME_SLIDER_CLOSE_POS.y - 49;
  }

  private void Reset()
  {
    this.Input.value = "";
    this.InputFrame.Reset();
    this.UpdateAnchors();
    this.UpdateWindowSize();
  }

  public void UpdateWindowSize()
  {
    this.ForceScroll((float) ((double) this.CurrentTotalHeight + (double) this.ScrollView.panel.baseClipRegion.y - (double) this.ScrollView.panel.baseClipRegion.w * 0.5 + ((double) this.ScrollViewTrans.localPosition.y + (double) this.ScrollView.panel.clipOffset.y)) + this.ScrollView.panel.clipSoftness.y, false);
    this.UpdateDummyDragScroll();
  }

  private void OnDragFinished()
  {
    if (MonoBehaviourSingleton<FriendManager>.I.messagePageMax - 1 <= this.nowPage || this.nowPage != this.loadedPage || (double) this.CurrentTotalHeight < (double) this.ScrollView.panel.height)
      return;
    Bounds bounds = this.ScrollView.bounds;
    if (30.0 > (double) this.ScrollView.panel.CalculateConstrainOffset(Vector2.op_Implicit(((Bounds) ref bounds).min), Vector2.op_Implicit(((Bounds) ref bounds).max)).y)
      return;
    ++this.nowPage;
    this.DispatchEvent("SEND_GET_MESSAGE_DETAIL");
  }

  private void InitStampList()
  {
    if (this.m_StampIdListCanPost == null)
      this.ResetStampIdList();
    if (!this.updateStampList)
      return;
    this.SetGrid((Enum) FriendMessage.UI.GRD_STAMP_LIST, (string) null, this.m_StampIdListCanPost.Count, true, new Func<int, Transform, Transform>(this.CreateStampItem), new Action<int, Transform, bool>(this.InitStampItem));
    this.updateStampList = false;
  }

  public void ResetStampIdList()
  {
    if (this.m_StampIdListCanPost == null)
      this.m_StampIdListCanPost = new List<int>();
    this.m_StampIdListCanPost.Clear();
    if (!Singleton<StampTable>.IsValid())
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
    return this.IsValidStampId(id) && (Singleton<StampTable>.I.GetData((uint) id).type == STAMP_TYPE.COMMON || MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(id));
  }

  private Transform CreateStampItem(int index, Transform parent)
  {
    Transform stampItem = ResourceUtility.Realizes((Object) this.chatStampListPrefab, 5);
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
    item.onButton += (System.Action) (() => this.DispatchEvent("SEND_STAMP", (object) item.StampId));
  }

  public void OnTouchPost()
  {
    string event_data = this.Input.value;
    if (event_data.Length == 0)
      return;
    this.DispatchEvent("SEND", (object) event_data);
    this.Input.value = "";
    this.InputFrame.FrameResize();
  }

  public void OnInput()
  {
    this.InputFrame.FrameResize();
    this.SetActive((Enum) FriendMessage.UI.LBL_DEFAULT, string.IsNullOrEmpty(this.Input.value));
  }

  private enum UI
  {
    SCR_CHAT,
    SPR_BG_CHAT,
    SPR_BG_IN_FRAME,
    Title_U,
    Title_D,
    OBJ_ROOM_ITEM_LIST_ROOT,
    WGT_DUMMY_DRAG_SCROLL,
    WGT_SLIDE_LIMIT,
    IPT_POST,
    OBJ_INPUT_FRAME,
    WGT_ANCHOR_BOTTOM,
    WGT_ANCHOR_TOP,
    WGT_CHAT_ROOT,
    OBJ_POST_FRAME,
    LBL_DEFAULT,
    SCR_STAMP_LIST,
    GRD_STAMP_LIST,
  }

  public class MessageItemListData
  {
    public GameObject rootObject;
    public List<ChatItem> itemList;
    public float currentTotalHeight;
    public int oldestItemIndex;
    public float slideOffset;
    public float basePosY;
    private const float DEFAULT_OFFSET = -26f;

    public MessageItemListData(GameObject root)
    {
      this.rootObject = root;
      this.itemList = new List<ChatItem>();
      this.Init();
    }

    public void Init()
    {
      this.currentTotalHeight = 0.0f;
      this.oldestItemIndex = 0;
      this.basePosY = 0.0f;
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

  public class PostMessageData
  {
    public FriendMessageData message;
    public bool topPosition;
    public bool forceScroll;

    public PostMessageData(FriendMessageData _message, bool _topPosition, bool _forceScroll)
    {
      this.message = _message;
      this.topPosition = _topPosition;
      this.forceScroll = _forceScroll;
    }
  }
}
