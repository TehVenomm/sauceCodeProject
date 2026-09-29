// Decompiled with JetBrains decompiler
// Type: FriendMessageUIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendMessageUIController : UIBehaviour
{
  private const string PREFAB_NAME_CHAT_ITEM = "ChatItem";
  private const string PREFAB_NAME_CHAT_STAMP_ITEM = "ChatStampListItem";
  private const int STAMP_COL_DEFAULT_COUNT = 5;
  private const int STAMP_COL_LANDSCAPE_COUNT = 4;
  private const float ANCHOR_LEFT = 0.0f;
  private const float ANCHOR_CENTER = 0.5f;
  private const float ANCHOR_RIGHT = 1f;
  private const float ANCHOR_BOT = 0.0f;
  private const float ANCHOR_TOP = 1f;
  private static readonly Vector4 WIDGET_ANCHOR_BG_IMG_DEFAULT_SETTINGS = new Vector4(0.0f, 0.0f, 72f, -72f);
  private static readonly Vector4 WIDGET_ANCHOR_BG_IMG_SPLIT_LANDSCAPE_SETTINGS = new Vector4(0.0f, 0.0f, 15f, -15f);
  private static readonly Vector4 WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS = new Vector4(0.0f, 0.0f, -540f, -120f);
  private static readonly Vector4 WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS = new Vector4(0.0f, -390f, 40f, -80f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS = new Vector4(0.0f, 0.0f, 82f, -540f);
  private static readonly Vector4 WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS = new Vector4(465f, 0.0f, 40f, -70f);
  private static readonly Vector4 UI_BTN_INPUT_CLOSE_DEFAULT_POS = new Vector4(10f, 46f, -2f, 59f);
  private static readonly Vector4 UI_BTN_INPUT_CLOSE_LANDSCAPE_POS = new Vector4(0.0f, 36f, 2f, 63f);
  private const int CHAT_ITEM_OFFSET = 22;
  private const float SOFTNESS_HEIGHT = 10f;
  private const float SPRING_STRENGTH = 20f;
  private const float CHAT_WIDTH = 410f;
  private const float SCROLL_BAR_OFFSET = 48f;
  private const float LOADPREV_SCROLLY_THRESHOLD = 30f;
  private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0.0f);
  private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0.0f);
  [SerializeField]
  private UILabel m_titleUpper;
  [SerializeField]
  private UILabel m_titleLower;
  [SerializeField]
  private GameObject m_defaultLabelObject;
  [SerializeField]
  private GameObject m_stampListRoot;
  [SerializeField]
  private GameObject m_postFrameObject;
  [SerializeField]
  private GameObject m_talkRootObject;
  [SerializeField]
  private UIPanel m_slideLimit;
  [SerializeField]
  private UIScrollView m_ScrollView;
  [SerializeField]
  private UIWidget m_DummyDragScroll;
  [SerializeField]
  private BoxCollider m_DragScrollCollider;
  [SerializeField]
  private ChatInputFrame m_InputFrame;
  [SerializeField]
  private UIInput m_Input;
  private Transform m_scrollViewTrans;
  private Transform m_dragScrollTrans;
  private List<FriendMessageData> postMessageList = new List<FriendMessageData>();
  private Queue<FriendMessage.PostMessageData> messageQueue = new Queue<FriendMessage.PostMessageData>();
  private int m_nowPage;
  private int m_loadedPage = -1;
  private FriendMessage.MessageItemListData itemListData;
  private GameObject chatItemPrefab;
  private GameObject chatStampListPrefab;
  private List<int> m_StampIdListCanPost;
  private bool isInitialized;
  private bool updateStampList;
  private MainChat m_manager;
  private UIWidget m_widgetBackGroundImg;
  private UIWidget m_widgetTop;
  private UIWidget m_widgetBot;
  private UIGrid m_gridStamp;
  private readonly float IntervalSendGetNoRead = 5f;
  private float interval;

  private Transform ScrollViewTrans
  {
    get
    {
      return this.m_scrollViewTrans ?? (this.m_scrollViewTrans = ((Component) this.m_ScrollView).transform);
    }
  }

  private Transform DragScrollTrans
  {
    get
    {
      return this.m_dragScrollTrans ?? (this.m_dragScrollTrans = ((Component) this.m_DragScrollCollider).transform);
    }
  }

  public FriendMessageUserListModel.MessageUserInfo talkUser { private set; get; }

  private float CurrentTotalHeight
  {
    get => this.itemListData == null ? 0.0f : this.itemListData.currentTotalHeight;
  }

  private float BasePosY => this.itemListData == null ? 0.0f : this.itemListData.basePosY;

  private UIWidget WidgetBackGroundImg
  {
    get
    {
      return this.m_widgetBackGroundImg ?? (this.m_widgetBackGroundImg = ((Component) this.GetCtrl((Enum) FriendMessageUIController.UI.SPR_BG)).GetComponent<UIWidget>());
    }
  }

  private UIWidget WidgetTop
  {
    get
    {
      return this.m_widgetTop ?? (this.m_widgetTop = ((Component) this.GetCtrl((Enum) FriendMessageUIController.UI.WGT_ANCHOR_TOP)).GetComponent<UIWidget>());
    }
  }

  private UIWidget WidgetBot
  {
    get
    {
      return this.m_widgetBot ?? (this.m_widgetBot = ((Component) this.GetCtrl((Enum) FriendMessageUIController.UI.WGT_ANCHOR_BOTTOM)).GetComponent<UIWidget>());
    }
  }

  private UIGrid GridStamp
  {
    get
    {
      return this.m_gridStamp ?? (this.m_gridStamp = ((Component) this.GetCtrl((Enum) FriendMessageUIController.UI.GRD_STAMP_LIST)).GetComponent<UIGrid>());
    }
  }

  public void Initialize(MainChat _manager)
  {
    this.m_manager = _manager;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    this.InitUI();
    this.CreateCtrlsArray(typeof (FriendMessageUIController.UI));
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
    {
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
      this.OnScreenRotateAsInit(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    }
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_quest_chatitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatItem");
    LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
    this.talkUser = MonoBehaviourSingleton<FriendManager>.I.talkUser;
    if (this.talkUser == null)
    {
      this.OnClickCloseButton();
    }
    else
    {
      string name = this.talkUser.name;
      this.SetLebelText(this.m_titleUpper, name);
      this.SetLebelText(this.m_titleLower, name);
      this.m_nowPage = 0;
      this.m_loadedPage = -1;
      this.itemListData = new FriendMessage.MessageItemListData(this.m_talkRootObject);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.chatItemPrefab = lo_quest_chatitem.loadedObject as GameObject;
      this.chatStampListPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
      this.SetSliderLimit();
      this.m_DummyDragScroll.width = 410;
      this.ResetStampIdList();
      this.Reset();
      this.postMessageList.Clear();
      this.m_ScrollView.onDragFinished += new UIScrollView.OnDragNotification(this.OnDragFinished);
      this.m_InputFrame.onChange += (System.Action) (() => this.OnInput());
      this.m_InputFrame.onSubmit += (System.Action) (() => this.OnTouchPost());
      if (this.talkUser.userId == 0)
      {
        this.m_stampListRoot.SetActive(false);
        this.m_postFrameObject.SetActive(false);
      }
      else
      {
        this.updateStampList = true;
        MonoBehaviourSingleton<AppMain>.I.onDelayCall += new System.Action(this.InitStampList);
      }
      this.isInitialized = true;
    }
  }

  private void InitStampList()
  {
    if (this.m_StampIdListCanPost == null)
      this.ResetStampIdList();
    if (!this.updateStampList)
      return;
    this.SetGrid((Enum) FriendMessageUIController.UI.GRD_STAMP_LIST, (string) null, this.m_StampIdListCanPost.Count, true, new Func<int, Transform, Transform>(this.CreateStampItem), new Action<int, Transform, bool>(this.InitStampItem));
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
    if (Object.op_Equality((Object) this.chatStampListPrefab, (Object) null))
      return (Transform) null;
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
    item.onButton += (System.Action) (() => this.SendStamp(item.StampId));
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
      this.ForceScroll((float) ((double) itemListData.currentTotalHeight + (double) this.m_ScrollView.panel.baseClipRegion.y - (double) this.m_ScrollView.panel.baseClipRegion.w * 0.5 + ((double) this.ScrollViewTrans.localPosition.y + (double) this.m_ScrollView.panel.clipOffset.y)) + this.m_ScrollView.panel.clipSoftness.y - itemListData.basePosY, true);
    if (topPosition)
      itemListData.itemList.Insert(0, component);
    else
      itemListData.itemList.Add(component);
  }

  private void SetSliderLimit()
  {
    if (Object.op_Equality((Object) this.m_slideLimit, (Object) null))
      return;
    this.m_slideLimit.topAnchor.absolute = (int) this.HOME_SLIDER_OPEN_POS.y + 9;
    this.m_slideLimit.bottomAnchor.absolute = (int) this.HOME_SLIDER_CLOSE_POS.y - 49;
  }

  private void SetLebelText(UILabel _ui, string _text)
  {
    if (Object.op_Equality((Object) _ui, (Object) null))
      return;
    _ui.text = _text;
  }

  private void Update()
  {
    if (!this.isInitialized)
      return;
    this.m_DragScrollCollider.center = Vector2.op_Implicit(new Vector2(this.m_ScrollView.panel.baseClipRegion.x, -(float) ((double) this.m_ScrollView.panel.baseClipRegion.w - (double) this.m_ScrollView.panel.baseClipRegion.y + (double) this.DragScrollTrans.localPosition.y - ((double) this.m_ScrollView.panel.finalClipRegion.w + (double) this.m_ScrollView.panel.clipOffset.y))));
    this.interval += Time.deltaTime;
    if ((double) this.IntervalSendGetNoRead <= (double) this.interval)
    {
      this.interval = 0.0f;
      this.SendGetNoReadMessage();
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
    if (0 <= this.m_loadedPage || this.messageQueue.Count != 0)
      return;
    this.m_loadedPage = 0;
  }

  private void Reset()
  {
    this.m_Input.value = "";
    this.m_InputFrame.Reset();
    this.UpdateAnchors();
    this.UpdateWindowSize();
  }

  public void UpdateWindowSize()
  {
    this.ForceScroll((float) ((double) this.CurrentTotalHeight + (double) this.m_ScrollView.panel.baseClipRegion.y - (double) this.m_ScrollView.panel.baseClipRegion.w * 0.5 + ((double) this.ScrollViewTrans.localPosition.y + (double) this.m_ScrollView.panel.clipOffset.y)) + this.m_ScrollView.panel.clipSoftness.y, false);
    this.UpdateDummyDragScroll();
  }

  private void UpdateDummyDragScroll()
  {
    this.m_DummyDragScroll.height = (double) this.m_ScrollView.panel.height <= (double) this.CurrentTotalHeight ? (int) ((double) this.CurrentTotalHeight - 20.0) : (int) ((double) this.m_ScrollView.panel.height - 20.0);
    this.DragScrollTrans.localPosition = new Vector3(this.m_ScrollView.panel.clipOffset.x, this.BasePosY - this.CurrentTotalHeight, 0.0f);
    this.m_DragScrollCollider.size = new Vector3(this.m_ScrollView.panel.finalClipRegion.z, this.m_ScrollView.panel.finalClipRegion.w - this.m_ScrollView.panel.clipSoftness.y * 2f, 0.0f);
  }

  private void ForceScroll(float newHeight, bool useSpring)
  {
    this.m_ScrollView.DisableSpring();
    if (useSpring)
    {
      SpringPanel.Begin(((Component) this.m_ScrollView).gameObject, Vector3.op_Multiply(Vector3.up, newHeight), 20f);
    }
    else
    {
      Vector2 clipOffset = this.m_ScrollView.panel.clipOffset;
      float num = this.ScrollViewTrans.localPosition.y + clipOffset.y;
      this.ScrollViewTrans.localPosition = Vector3.op_Multiply(Vector3.up, newHeight);
      clipOffset.y = -newHeight + num;
      this.m_ScrollView.panel.clipOffset = clipOffset;
    }
  }

  private void OnDragFinished()
  {
    if (MonoBehaviourSingleton<FriendManager>.I.messagePageMax - 1 <= this.m_nowPage || this.m_nowPage != this.m_loadedPage || (double) this.CurrentTotalHeight < (double) this.m_ScrollView.panel.height)
      return;
    Bounds bounds = this.m_ScrollView.bounds;
    if (30.0 > (double) this.m_ScrollView.panel.CalculateConstrainOffset(Vector2.op_Implicit(((Bounds) ref bounds).min), Vector2.op_Implicit(((Bounds) ref bounds).max)).y)
      return;
    ++this.m_nowPage;
    this.SendGetMessageDetail();
  }

  public void OnTouchPost()
  {
    string _text = this.m_Input.value;
    if (_text.Length == 0)
      return;
    this.SendText(_text);
    this.m_Input.value = "";
    this.m_InputFrame.FrameResize();
  }

  public void OnInput()
  {
    this.m_InputFrame.FrameResize();
    bool flag = string.IsNullOrEmpty(this.m_Input.value);
    if (this.m_defaultLabelObject.activeSelf == flag)
      return;
    this.m_defaultLabelObject.SetActive(flag);
  }

  private void OnScreenRotate(bool _isPortrait)
  {
    this.SetCommonScreenSettings(_isPortrait);
    this.UpdateAnchors();
    this.updateStampList = true;
    this.InitStampList();
  }

  private void OnScreenRotateAsInit(bool _isPortrait)
  {
    this.SetCommonScreenSettings(_isPortrait);
    this.UpdateAnchors();
  }

  private void SetCommonScreenSettings(bool _isPortrait)
  {
    Vector4 vector4_1 = _isPortrait ? FriendMessageUIController.WIDGET_ANCHOR_BG_IMG_DEFAULT_SETTINGS : FriendMessageUIController.WIDGET_ANCHOR_BG_IMG_SPLIT_LANDSCAPE_SETTINGS;
    this.WidgetBackGroundImg.leftAnchor.Set(0.0f, vector4_1.x);
    this.WidgetBackGroundImg.rightAnchor.Set(1f, vector4_1.y);
    this.WidgetBackGroundImg.bottomAnchor.Set(_isPortrait ? 0.0f : 0.0f, vector4_1.z);
    this.WidgetBackGroundImg.topAnchor.Set(1f, vector4_1.w);
    Vector4 vector4_2 = _isPortrait ? FriendMessageUIController.WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS : FriendMessageUIController.WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS;
    this.WidgetTop.leftAnchor.Set(0.0f, vector4_2.x);
    this.WidgetTop.rightAnchor.Set(1f, vector4_2.y);
    this.WidgetTop.bottomAnchor.Set(_isPortrait ? 1f : 0.0f, vector4_2.z);
    this.WidgetTop.topAnchor.Set(1f, vector4_2.w);
    Vector4 vector4_3 = _isPortrait ? FriendMessageUIController.WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS : FriendMessageUIController.WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS;
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyChatAnchor)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      if (!SpecialDeviceManager.IsPortrait)
        vector4_3 = specialDeviceInfo.FriendMessage_WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS;
    }
    this.WidgetBot.leftAnchor.Set(0.0f, vector4_3.x);
    this.WidgetBot.rightAnchor.Set(1f, vector4_3.y);
    this.WidgetBot.bottomAnchor.Set(0.0f, vector4_3.z);
    this.WidgetBot.topAnchor.Set(1f, vector4_3.w);
    this.GridStamp.maxPerLine = _isPortrait ? 5 : 4;
    ((Behaviour) this.GridStamp).enabled = true;
  }

  private void SendGetMessageDetail()
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(this.talkUser.userId, this.m_nowPage, true, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.m_ScrollView.DisableSpring();
      this.RefreshUI();
      this.m_loadedPage = this.m_nowPage;
    }));
  }

  private void SendGetNoReadMessage()
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetNoreadMessage(true, (Action<bool>) (is_success => { }));
  }

  private void SendStamp(int _stampId)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(this.talkUser.userId, "[STAMP]" + _stampId.ToString(), true, (Action<bool>) (is_success => { }));
  }

  private void SendText(string _text)
  {
    string message = _text;
    if (!string.IsNullOrEmpty(message))
      message = message.Replace("\n", "");
    MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(this.talkUser.userId, message, true, (Action<bool>) (is_success => { }));
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
    if (!string.IsNullOrEmpty(message))
      message = message.Replace("\n", "");
    this.AddNextChatItem((Action<ChatItem>) (chatItem => chatItem.Init(userId, this.talkUser.name, message)), topPosition, forceScroll);
  }

  private void PostUIStamp(int userId, int stampId, bool topPosition, bool forceScroll)
  {
    if (!this.IsValidStampId(stampId) || Singleton<StampTable>.I.GetData((uint) stampId) == null)
      return;
    this.AddNextChatItem((Action<ChatItem>) (chatItem => chatItem.Init(userId, this.talkUser.name, stampId)), topPosition, forceScroll);
  }

  public void OnClickCloseButton()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.m_manager.PopState();
  }

  private enum UI
  {
    SPR_BG,
    WGT_ANCHOR_TOP,
    WGT_ANCHOR_BOTTOM,
    GRD_STAMP_LIST,
  }
}
