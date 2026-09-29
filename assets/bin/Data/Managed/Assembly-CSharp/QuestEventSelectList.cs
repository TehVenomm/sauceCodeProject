// Decompiled with JetBrains decompiler
// Type: QuestEventSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestEventSelectList : QuestSpecialSelect
{
  protected Network.EventData eventData;
  protected List<DeliveryTable.DeliveryData> clearedDeliveries;
  protected List<QuestEventSelectList.Story> stories = new List<QuestEventSelectList.Story>();
  protected UIBehaviour.LabelWidthLimitter title;
  protected UIBehaviour.LabelWidthLimitter titleEffect;
  protected Transform mapItem;
  protected bool isResetUI;
  protected int nowPage = 1;
  protected int pageMax = 1;

  protected virtual bool showStory => true;

  protected virtual bool showMap => true;

  public override void Initialize()
  {
    this.eventData = GameSection.GetEventData() as Network.EventData;
    this.SkipTween((Enum) QuestEventSelectList.UI.SPR_DELIVERY_BTN_SELECTED);
    this.SetActive((Enum) QuestEventSelectList.UI.OBJ_DELIVERY_ROOT, true);
    int width = this.GetWidth((Enum) QuestEventSelectList.UI.WGT_LOCATION_NAME_LIMIT);
    this.title = new UIBehaviour.LabelWidthLimitter(((Component) this.GetCtrl((Enum) QuestEventSelectList.UI.LBL_LOCATION_NAME)).GetComponent<UILabel>(), width, false);
    this.titleEffect = new UIBehaviour.LabelWidthLimitter(((Component) this.GetCtrl((Enum) QuestEventSelectList.UI.LBL_LOCATION_NAME_EFFECT)).GetComponent<UILabel>(), width, true);
    base.Initialize();
  }

  protected override IEnumerator DoInitialize()
  {
    string eventBg = ResourceName.GetEventBG(this.eventData.bannerId);
    Hash128 hash128 = new Hash128();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null))
      hash128 = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(eventBg));
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null) || ((Hash128) ref hash128).isValid)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_bg = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_BG, eventBg);
      LoadObject lo_item = (LoadObject) null;
      if (this.ShouldShowEventMapButton())
        lo_item = loadingQueue.Load(RESOURCE_CATEGORY.QUEST_ITEM, "QEM_10000000");
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.SetTexture((Enum) QuestEventSelectList.UI.TEX_EVENT_BG, (Texture) (lo_bg.loadedObject as Texture2D));
      if (lo_item != null && Object.op_Inequality((Object) null, lo_item.loadedObject))
        this.mapItem = (lo_item.loadedObject as GameObject).transform;
      lo_bg = (LoadObject) null;
      lo_item = (LoadObject) null;
    }
    this.GetDeliveryList();
    this.EndInitialize();
  }

  public override void Exit() => base.Exit();

  protected override void OnOpen()
  {
    this.StartCoroutine(this.InitScroll());
    base.OnOpen();
  }

  protected override void RemoveRecommend()
  {
  }

  private IEnumerator InitScroll()
  {
    UIScrollView scroll = this.GetComponent<UIScrollView>((Enum) QuestEventSelectList.UI.SCR_DELIVERY_QUEST);
    while (this.state != UIBehaviour.STATE.OPEN)
      yield return (object) null;
    ((Behaviour) scroll).enabled = scroll.shouldMoveVertically;
    this.RepositionTable();
  }

  protected virtual bool ShouldShowEventMapButton()
  {
    return this.showMap && Array.Find<RegionTable.Data>(Singleton<RegionTable>.I.GetData(), (Predicate<RegionTable.Data>) (o => o.eventId == this.eventData.eventId)) != null;
  }

  protected override bool IsVisibleDelivery(Delivery delivery, DeliveryTable.DeliveryData tableData)
  {
    return this.eventData != null && tableData.IsEvent() && tableData.eventID == this.eventData.eventId;
  }

  protected void RepositionTable()
  {
    UITable component = this.GetComponent<UITable>((Enum) QuestEventSelectList.UI.TBL_DELIVERY_QUEST);
    if (!Object.op_Implicit((Object) component))
      return;
    component.Reposition();
    List<Transform> childList = component.GetChildList();
    int index = 0;
    for (int count = childList.Count; index < count; ++index)
    {
      Vector3 localPosition = childList[index].localPosition;
      localPosition.x = 0.0f;
      childList[index].localPosition = localPosition;
    }
  }

  protected bool HasChapterStory() => this.GetChapterId() > 0;

  protected int GetChapterId()
  {
    if (!MonoBehaviourSingleton<TheaterModeTable>.IsValid() || this.stories == null || this.stories.Count < 1)
      return -1;
    int chapterId = -1;
    MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterData((Action<TheaterModeTable.TheaterModeData>) (data =>
    {
      int index = 0;
      for (int count = this.stories.Count; index < count; ++index)
      {
        if (data.script_id == this.stories[index].id)
          chapterId = data.chapter_id;
      }
    }));
    return chapterId;
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) QuestEventSelectList.UI.LBL_LOCATION_NAME, this.eventData.name);
    this.SetLabelText((Enum) QuestEventSelectList.UI.LBL_LOCATION_NAME_EFFECT, this.eventData.name);
    this.SetActive((Enum) QuestEventSelectList.UI.BTN_INFO, !string.IsNullOrEmpty(this.eventData.linkName));
    this.title.Update();
    this.titleEffect.Update();
    this.stories.Clear();
    if (this.eventData.prologueStoryId > 0)
      this.stories.Add(new QuestEventSelectList.Story(this.eventData.prologueStoryId, this.eventData.prologueTitle));
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
      this.SetActive((Enum) QuestEventSelectList.UI.BTN_FISHING_RECORD, this.eventData.subButtonType == 1);
    this.clearedDeliveries = this.CreateClearedDliveryList();
    this.UpdateList();
    this.UpdateAnchors();
    this.isResetUI = false;
  }

  protected virtual List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
  {
    List<DeliveryTable.DeliveryData> clearedDliveryList = new List<DeliveryTable.DeliveryData>();
    List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
    int index = 0;
    for (int count = clearStatusDelivery.Count; index < count; ++index)
    {
      ClearStatusDelivery d = clearStatusDelivery[index];
      if (d.deliveryStatus == 3)
      {
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) d.deliveryId);
        if (deliveryTableData.eventID == this.eventData.eventId && !Array.Exists<Delivery>(this.deliveryInfo, (Predicate<Delivery>) (x => x.dId == d.deliveryId)))
        {
          clearedDliveryList.Add(deliveryTableData);
          if (deliveryTableData.clearEventID > 0U)
          {
            string title = deliveryTableData.clearEventTitle;
            if (string.IsNullOrEmpty(title))
              title = deliveryTableData.name;
            this.stories.Add(new QuestEventSelectList.Story((int) deliveryTableData.clearEventID, title));
          }
        }
      }
    }
    return clearedDliveryList;
  }

  protected void UpdateList()
  {
    if (Object.op_Implicit((Object) this.GetCtrl((Enum) QuestEventSelectList.UI.TBL_DELIVERY_QUEST)))
      this.UpdateTable();
    else
      this.UpdateGrid();
  }

  protected virtual void UpdateTable()
  {
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    List<QuestEventSelectList.ShowDeliveryData> showDeliveryDataList = new List<QuestEventSelectList.ShowDeliveryData>();
    if (this.deliveryInfo != null)
    {
      for (int i = 0; i < this.deliveryInfo.Length; ++i)
      {
        QuestEventSelectList.ShowDeliveryData showDeliveryData = new QuestEventSelectList.ShowDeliveryData(i, false, this.deliveryInfo[i]);
        showDeliveryDataList.Add(showDeliveryData);
      }
    }
    if (this.clearedDeliveries != null)
    {
      for (int index = 0; index < this.clearedDeliveries.Count; ++index)
      {
        QuestEventSelectList.ShowDeliveryData showDeliveryData = new QuestEventSelectList.ShowDeliveryData(index, true, this.clearedDeliveries[index]);
        showDeliveryDataList.Add(showDeliveryData);
      }
    }
    this.pageMax = 1 + (showDeliveryDataList.Count - 1) / 10;
    bool is_visible = this.pageMax > 1;
    this.SetActive((Enum) QuestEventSelectList.UI.OBJ_ACTIVE_ROOT, is_visible);
    this.SetActive((Enum) QuestEventSelectList.UI.OBJ_INACTIVE_ROOT, !is_visible);
    this.SetLabelText((Enum) QuestEventSelectList.UI.LBL_MAX, this.pageMax.ToString());
    this.SetLabelText((Enum) QuestEventSelectList.UI.LBL_NOW, this.nowPage.ToString());
    QuestEventSelectList.ShowDeliveryData[] showList = this.GetPagingList<QuestEventSelectList.ShowDeliveryData>(showDeliveryDataList.ToArray(), 10, this.nowPage);
    int length = showList.Length;
    if (this.showStory)
      length += num1 + this.stories.Count;
    if (length == 0)
    {
      this.SetActive((Enum) QuestEventSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestEventSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestEventSelectList.UI.TBL_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestEventSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestEventSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestEventSelectList.UI.TBL_DELIVERY_QUEST, true);
      bool flag = false;
      if (this.ShouldShowEventMapButton())
      {
        flag = true;
        ++length;
      }
      int questStartIndex = 0;
      if (flag)
        ++questStartIndex;
      int borderIndex = questStartIndex + showList.Length;
      int storyStartIndex = borderIndex;
      if (this.stories.Count > 0)
        ++storyStartIndex;
      Transform ctrl = this.GetCtrl((Enum) QuestEventSelectList.UI.TBL_DELIVERY_QUEST);
      if (Object.op_Implicit((Object) ctrl))
      {
        int num2 = 0;
        for (int childCount = ctrl.childCount; num2 < childCount; ++num2)
        {
          Transform child = ctrl.GetChild(0);
          child.parent = (Transform) null;
          Object.Destroy((Object) ((Component) child).gameObject);
        }
      }
      bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
      this.SetTable((Enum) QuestEventSelectList.UI.TBL_DELIVERY_QUEST, "", length, this.isResetUI, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        if (i < storyStartIndex)
          return i < borderIndex ? (i < questStartIndex ? (!Object.op_Inequality((Object) null, (Object) this.mapItem) ? this.Realizes("QuestEventBorderItem", parent) : ResourceUtility.Realizes((Object) ((Component) this.mapItem).gameObject, parent)) : this.Realizes("QuestRequestItem", parent)) : this.Realizes("QuestEventBorderItem", parent);
        return !this.HasChapterStory() || i == storyStartIndex || !isRenewalFlag ? this.Realizes("QuestEventStoryItem", parent) : (Transform) null;
      }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        if (Object.op_Equality((Object) t, (Object) null))
          return;
        this.SetActive(t, true);
        if (i >= storyStartIndex)
        {
          this.InitStory(i - storyStartIndex, t);
        }
        else
        {
          if (i >= borderIndex && i < storyStartIndex)
            return;
          if (i >= questStartIndex && i < borderIndex)
          {
            this.InitDelivery(showList[i - questStartIndex], t);
            this.ChangeDeliveryFrameSprite(t);
          }
          else
          {
            if (i >= questStartIndex)
              return;
            this.InitMap(t);
          }
        }
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestEventSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
      this.RepositionTable();
    }
  }

  protected virtual void ChangeDeliveryFrameSprite(Transform parent)
  {
  }

  protected virtual void UpdateGrid()
  {
    List<QuestEventSelectList.ShowDeliveryData> showDeliveryDataList = new List<QuestEventSelectList.ShowDeliveryData>();
    if (this.deliveryInfo != null)
    {
      for (int i = 0; i < this.deliveryInfo.Length; ++i)
      {
        QuestEventSelectList.ShowDeliveryData showDeliveryData = new QuestEventSelectList.ShowDeliveryData(i, false, this.deliveryInfo[i]);
        showDeliveryDataList.Add(showDeliveryData);
      }
    }
    if (this.clearedDeliveries != null)
    {
      for (int index = 0; index < this.clearedDeliveries.Count; ++index)
      {
        QuestEventSelectList.ShowDeliveryData showDeliveryData = new QuestEventSelectList.ShowDeliveryData(index, true, this.clearedDeliveries[index]);
        showDeliveryDataList.Add(showDeliveryData);
      }
    }
    this.pageMax = 1 + (showDeliveryDataList.Count - 1) / 10;
    bool is_visible = this.pageMax > 1;
    this.SetActive((Enum) QuestEventSelectList.UI.OBJ_ACTIVE_ROOT, is_visible);
    this.SetActive((Enum) QuestEventSelectList.UI.OBJ_INACTIVE_ROOT, !is_visible);
    this.SetLabelText((Enum) QuestEventSelectList.UI.LBL_MAX, this.pageMax.ToString());
    this.SetLabelText((Enum) QuestEventSelectList.UI.LBL_NOW, this.nowPage.ToString());
    QuestEventSelectList.ShowDeliveryData[] showList = this.GetPagingList<QuestEventSelectList.ShowDeliveryData>(showDeliveryDataList.ToArray(), 10, this.nowPage);
    if (showDeliveryDataList.Count == 0)
    {
      this.SetActive((Enum) QuestEventSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestEventSelectList.UI.GRD_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestEventSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestEventSelectList.UI.GRD_DELIVERY_QUEST, true);
      this.SetDynamicList((Enum) QuestEventSelectList.UI.GRD_DELIVERY_QUEST, "QuestRequestItem", showList.Length, this.isResetUI, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetActive(t, true);
        if (showList[i].data.subType == DELIVERY_SUB_TYPE.ASSIGNED_EQUIPMENT)
          this.SetEvent(t, "COMPLETED_ASSIGNED_EQUIPMENT", showList[i].index);
        else if (!showList[i].isCompleted)
          this.SetEvent(t, "SELECT_DELIVERY", showList[i].index);
        else
          this.SetEvent(t, "SELECT_COMPLETED_DELIVERY", showList[i].index);
        this.SetupDeliveryListItem(t, showList[i].data);
        if (!showList[i].isCompleted)
          return;
        this.SetActive(t, (Enum) QuestEventSelectList.UI.OBJ_REQUEST_COMPLETED, true);
      }));
    }
  }

  protected void InitDelivery(QuestEventSelectList.ShowDeliveryData showData, Transform t)
  {
    if (showData.isCompleted)
    {
      bool flag = false;
      if (showData.data != null)
      {
        QuestTable.QuestTableData questData = showData.data.GetQuestData();
        if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
          flag = true;
      }
      if (flag)
        this.SetEvent(t, "SELECT_COMPLETED_DELIVERY_HAPPEN", showData.index);
      else if (showData.data.subType == DELIVERY_SUB_TYPE.ASSIGNED_EQUIPMENT)
        this.SetEvent(t, "COMPLETED_ASSIGNED_EQUIPMENT", showData.index);
      else
        this.SetEvent(t, "SELECT_COMPLETED_DELIVERY", showData.index);
      this.UpdateCompletedDeliveryUI(t);
      this.SetupDeliveryListItem(t, showData.data);
      this.SetActive(t, (Enum) QuestEventSelectList.UI.OBJ_REQUEST_COMPLETED, true);
      this.SetCompletedHaveCount(t, showData.data);
    }
    else
    {
      if (showData.data != null && showData.data.subType == DELIVERY_SUB_TYPE.READ_STORY)
        this.SetEvent(t, "READ_STORY", showData.index);
      else if (showData.data != null && showData.data.subType == DELIVERY_SUB_TYPE.ASSIGNED_EQUIPMENT)
        this.SetEvent(t, "ASSIGNED_EQUIPMENT", showData.index);
      else
        this.SetEvent(t, "SELECT_DELIVERY", showData.index);
      this.SetupDeliveryListItem(t, showData.data);
    }
  }

  protected virtual void UpdateCompletedDeliveryUI(Transform parent)
  {
  }

  private void OnQuery_ASSIGNED_EQUIPMENT()
  {
    int eventData = (int) GameSection.GetEventData();
    int dId = this.deliveryInfo[eventData].dId;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) dId);
    bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(dId);
    if (is_enough_material)
    {
      GameSection.StayEvent();
      this.SendDeliveryComplete(eventData, dId, is_enough_material, false);
    }
    else
      GameSection.SetEventData((object) deliveryTableData);
  }

  private void OnQuery_COMPLETED_ASSIGNED_EQUIPMENT()
  {
    GameSection.SetEventData((object) this.clearedDeliveries[(int) GameSection.GetEventData()]);
  }

  protected virtual void InitNormalDelivery(int index, Transform t)
  {
    this.SetEvent(t, "SELECT_DELIVERY", index);
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
    this.SetupDeliveryListItem(t, deliveryTableData);
  }

  protected virtual void InitCompletedDelivery(int completedIndex, Transform t)
  {
    DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[completedIndex];
    bool flag = false;
    if (clearedDelivery != null)
    {
      QuestTable.QuestTableData questData = clearedDelivery.GetQuestData();
      if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
        flag = true;
    }
    if (flag)
      this.SetEvent(t, "SELECT_COMPLETED_DELIVERY_HAPPEN", completedIndex);
    else
      this.SetEvent(t, "SELECT_COMPLETED_DELIVERY", completedIndex);
    this.SetupDeliveryListItem(t, clearedDelivery);
    this.SetActive(t, (Enum) QuestEventSelectList.UI.OBJ_REQUEST_COMPLETED, true);
    this.SetCompletedHaveCount(t, clearedDelivery);
  }

  protected virtual void InitStory(int storyIndex, Transform t)
  {
    bool flag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
    if (this.HasChapterStory() & flag)
    {
      int eventId = this.eventData != null ? this.eventData.eventId : 0;
      this.SetEvent(t, "JUMP_TO_STORY_PAGE", eventId);
      this.SetLabelText(t, (Enum) QuestEventSelectList.UI.LBL_STORY_TITLE, "View All Stories");
    }
    else
    {
      this.SetEvent(t, "SELECT_STORY", storyIndex);
      this.SetLabelText(t, (Enum) QuestEventSelectList.UI.LBL_STORY_TITLE, this.stories[storyIndex].title);
    }
  }

  protected virtual void InitMap(Transform t)
  {
    this.SetEvent(t, "WORLDMAP", this.eventData.eventId);
    this.SetLabelText(t, (Enum) QuestEventSelectList.UI.LBL_STORY_TITLE, "マップ");
  }

  protected virtual void OnQuery_INFO()
  {
    GameSection.SetEventData((object) string.Format(WebViewManager.NewsWithLinkParamFormat, (object) this.eventData.linkName));
  }

  private void OnQuery_SELECT_COMPLETED_DELIVERY() => this.SetCompletedDeliveryEventData();

  private void OnQuery_SELECT_COMPLETED_DELIVERY_HAPPEN() => this.SetCompletedDeliveryEventData();

  private void SetCompletedDeliveryEventData()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) (int) this.clearedDeliveries[(int) GameSection.GetEventData()].id,
      (object) new DeliveryRewardList(),
      (object) true
    });
  }

  protected virtual void OnQuery_JUMP_TO_STORY_PAGE()
  {
    EventData[] eventDataArray = new EventData[2]
    {
      new EventData("SELECT_EVENT", (object) null),
      new EventData("SELECT_CHAPTER_FROM_OUTER", (object) null)
    };
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.GetChapterId(),
      (object) eventDataArray
    });
  }

  protected virtual void OnQuery_SELECT_STORY()
  {
    QuestEventSelectList.Story storey = this.stories[(int) GameSection.GetEventData()];
    string goingHomeEvent = GameSection.GetGoingHomeEvent();
    EventData[] eventDataArray;
    if (this.eventData == null || !MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.eventListData.IsNullOrEmpty<EventListData>() || MonoBehaviourSingleton<DeliveryManager>.I.GetEventListData(this.eventData.eventId) == null)
    {
      eventDataArray = new EventData[3]
      {
        new EventData(goingHomeEvent, (object) null),
        new EventData("TO_EVENT", (object) null),
        new EventData("SELECT", (object) this.eventData)
      };
    }
    else
    {
      EventListData eventListData = MonoBehaviourSingleton<DeliveryManager>.I.GetEventListData(this.eventData.eventId);
      eventDataArray = new EventData[4]
      {
        new EventData(goingHomeEvent, (object) null),
        new EventData("TO_EVENT", (object) null),
        eventListData.placeEnum == EVENT_DISPLAY_PLACE.PRESENT ? new EventData("TAB_PRESENT", (object) null) : new EventData("TAB_EVENT", (object) null),
        new EventData("SELECT", (object) this.eventData)
      };
    }
    GameSection.SetEventData((object) new object[4]
    {
      (object) storey.id,
      (object) "",
      (object) "",
      (object) eventDataArray
    });
  }

  protected virtual void OnQuery_WORLDMAP()
  {
    int eventId = (int) GameSection.GetEventData();
    RegionTable.Data data = Array.Find<RegionTable.Data>(Singleton<RegionTable>.I.GetData(), (Predicate<RegionTable.Data>) (o => o.eventId == eventId));
    if (data == null)
      return;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
    this.RequestEvent("DIRECT_EVENT", (object) (int) data.regionId);
  }

  private void OnQuery_PAGE_PREV()
  {
    this.isResetUI = true;
    this.nowPage = this.nowPage > 1 ? this.nowPage - 1 : this.pageMax;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.isResetUI = true;
    this.nowPage = this.nowPage < this.pageMax ? this.nowPage + 1 : 1;
    this.RefreshUI();
  }

  private void OnQuery_FISHING_RECORD()
  {
    GameSection.SetEventData((object) this.eventData.eventId);
  }

  protected new enum UI
  {
    TEX_EVENT_BG,
    BTN_INFO,
    TGL_BUTTON_ROOT,
    SPR_DELIVERY_BTN_SELECTED,
    OBJ_DELIVERY_ROOT,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
    GRD_DELIVERY_QUEST,
    TBL_DELIVERY_QUEST,
    STR_DELIVERY_NON_LIST,
    OBJ_REQUEST_COMPLETED,
    LBL_LOCATION_NAME,
    LBL_LOCATION_NAME_EFFECT,
    WGT_LOCATION_NAME_LIMIT,
    SCR_DELIVERY_QUEST,
    OBJ_IMAGE,
    BTN_EVENT,
    OBJ_FRAME,
    SPR_BG_FRAME,
    LBL_STORY_TITLE,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    LBL_MAX,
    LBL_NOW,
    BTN_INGAME_INFO,
    BTN_FISHING_RECORD,
    SPR_CLEARD_BLACK,
  }

  protected class Story
  {
    public int id;
    public string title;

    public Story(int id, string title)
    {
      this.id = id;
      this.title = title;
    }
  }

  public class ShowDeliveryData
  {
    public int index = -1;
    public bool isCompleted;
    public DeliveryTable.DeliveryData data;

    public ShowDeliveryData(int i, bool isComp, DeliveryTable.DeliveryData d)
    {
      this.index = i;
      this.isCompleted = isComp;
      this.data = d;
    }

    public ShowDeliveryData(int i, bool isComp, Delivery d)
    {
      this.index = i;
      this.isCompleted = isComp;
      this.data = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) d.dId);
    }
  }
}
