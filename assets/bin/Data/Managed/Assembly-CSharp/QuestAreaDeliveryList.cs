// Decompiled with JetBrains decompiler
// Type: QuestAreaDeliveryList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAreaDeliveryList : QuestEventSelectList
{
  private List<QuestEventSelectList.ShowDeliveryData> deliveryList = new List<QuestEventSelectList.ShowDeliveryData>();
  private List<uint> showCheckList = new List<uint>();
  protected RegionTable.Data regionData;

  protected override IEnumerator DoInitialize()
  {
    int eventData = (int) GameSection.GetEventData();
    this.regionData = Singleton<RegionTable>.I.GetData((uint) eventData);
    this.SetActive((Enum) QuestAreaDeliveryList.UI.BTN_SUMMARY, eventData != 0);
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject bannerObj = loadQueue.Load(RESOURCE_CATEGORY.AREA_BACKGROUND, ResourceName.GetAreaBG(eventData));
    if (loadQueue.IsLoading())
      yield return (object) loadQueue.Wait();
    Texture2D loadedObject = bannerObj.loadedObject as Texture2D;
    if (Object.op_Inequality((Object) loadedObject, (Object) null))
      this.SetTexture((Enum) QuestAreaDeliveryList.UI.TEX_AREA_BG, (Texture) loadedObject);
    if (this.ShouldShowEventMapButton())
    {
      LoadObject item = loadQueue.Load(RESOURCE_CATEGORY.QUEST_ITEM, "QEM_10000001");
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      this.SetTexture((Enum) QuestAreaDeliveryList.UI.TEX_EVENT_BG, (Texture) (item.loadedObject as Texture2D));
      if (item != null && Object.op_Inequality((Object) null, item.loadedObject))
        this.mapItem = (item.loadedObject as GameObject).transform;
      item = (LoadObject) null;
    }
    this.SetAreaName();
    this.EndInitialize();
  }

  public override void UpdateUI()
  {
    this.stories.Clear();
    this.GetDeliveryList();
    this.clearedDeliveries = this.CreateClearedDliveryList();
    this.UpdateTable();
    this.UpdateAnchors();
    this.SetAreaName();
    this.title.Update();
    this.titleEffect.Update();
    this.isResetUI = false;
  }

  private void SetAreaName()
  {
    this.SetLabelText((Enum) QuestAreaDeliveryList.UI.LBL_LOCATION_NAME, this.regionData.regionName);
    this.SetLabelText((Enum) QuestAreaDeliveryList.UI.LBL_LOCATION_NAME_EFFECT, this.regionData.regionName);
  }

  protected override void UpdateTable()
  {
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    this.SetDeliveryTable();
    this.pageMax = 1 + (this.deliveryList.Count - 1) / 10;
    bool is_visible = this.pageMax > 1;
    this.SetActive((Enum) QuestAreaDeliveryList.UI.OBJ_ACTIVE_ROOT, is_visible);
    this.SetActive((Enum) QuestAreaDeliveryList.UI.OBJ_INACTIVE_ROOT, !is_visible);
    this.SetLabelText((Enum) QuestAreaDeliveryList.UI.LBL_MAX, this.pageMax.ToString());
    this.SetLabelText((Enum) QuestAreaDeliveryList.UI.LBL_NOW, this.nowPage.ToString());
    QuestEventSelectList.ShowDeliveryData[] showList = this.GetPagingList<QuestEventSelectList.ShowDeliveryData>(this.deliveryList.ToArray(), 10, this.nowPage);
    bool flag1 = false;
    if (showList.Length == this.showCheckList.Count)
    {
      for (int index = 0; index < showList.Length; ++index)
      {
        if ((int) this.showCheckList[index] != (int) showList[index].data.id)
        {
          flag1 = true;
          break;
        }
      }
    }
    else
      flag1 = true;
    if (!flag1)
    {
      this.RepositionTable();
    }
    else
    {
      this.showCheckList.Clear();
      for (int index = 0; index < showList.Length; ++index)
        this.showCheckList.Add(showList[index].data.id);
      int length = showList.Length;
      if (this.showStory)
        length += num1 + this.stories.Count;
      this.SetActive((Enum) QuestAreaDeliveryList.UI.TBL_DELIVERY_QUEST, true);
      bool flag2 = false;
      if (this.ShouldShowEventMapButton())
      {
        flag2 = true;
        ++length;
      }
      int questStartIndex = 0;
      if (flag2)
        ++questStartIndex;
      int borderIndex = questStartIndex + showList.Length;
      int storyStartIndex = borderIndex;
      if (this.stories.Count > 0)
        ++storyStartIndex;
      Transform ctrl = this.GetCtrl((Enum) QuestAreaDeliveryList.UI.TBL_DELIVERY_QUEST);
      int num2 = 0;
      for (int childCount = ctrl.childCount; num2 < childCount; ++num2)
      {
        Transform child = ctrl.GetChild(0);
        child.parent = (Transform) null;
        Object.Destroy((Object) ((Component) child).gameObject);
      }
      bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
      this.SetTable((Enum) QuestAreaDeliveryList.UI.TBL_DELIVERY_QUEST, "", length, this.isResetUI, (Func<int, Transform, Transform>) ((i, parent) =>
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
          if (i >= borderIndex)
            return;
          if (i >= questStartIndex)
            this.InitDelivery(showList[i - questStartIndex], t);
          else
            this.InitMap(t);
        }
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestAreaDeliveryList.UI.SCR_DELIVERY_QUEST)).enabled = true;
      this.RepositionTable();
    }
  }

  protected void SetDeliveryTable()
  {
    this.deliveryList.Clear();
    if (this.deliveryInfo != null)
    {
      for (int i = 0; i < this.deliveryInfo.Length; ++i)
        this.deliveryList.Add(new QuestEventSelectList.ShowDeliveryData(i, false, this.deliveryInfo[i]));
    }
    if (this.clearedDeliveries == null)
      return;
    for (int index = 0; index < this.clearedDeliveries.Count; ++index)
      this.deliveryList.Add(new QuestEventSelectList.ShowDeliveryData(index, true, this.clearedDeliveries[index]));
  }

  protected IEnumerator StartUpdateTable()
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
    this.SetActive((Enum) QuestAreaDeliveryList.UI.OBJ_ACTIVE_ROOT, is_visible);
    this.SetActive((Enum) QuestAreaDeliveryList.UI.OBJ_INACTIVE_ROOT, !is_visible);
    this.SetLabelText((Enum) QuestAreaDeliveryList.UI.LBL_MAX, this.pageMax.ToString());
    this.SetLabelText((Enum) QuestAreaDeliveryList.UI.LBL_NOW, this.nowPage.ToString());
    QuestEventSelectList.ShowDeliveryData[] showList = this.GetPagingList<QuestEventSelectList.ShowDeliveryData>(showDeliveryDataList.ToArray(), 10, this.nowPage);
    int length = showList.Length;
    if (this.showStory)
      length += num1 + this.stories.Count;
    this.SetActive((Enum) QuestAreaDeliveryList.UI.TBL_DELIVERY_QUEST, true);
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
    Transform ctrl = this.GetCtrl((Enum) QuestAreaDeliveryList.UI.TBL_DELIVERY_QUEST);
    int num2 = 0;
    for (int childCount = ctrl.childCount; num2 < childCount; ++num2)
    {
      Transform child = ctrl.GetChild(0);
      child.parent = (Transform) null;
      Object.Destroy((Object) ((Component) child).gameObject);
    }
    bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
    yield return (object) this.SetTableAsync((Enum) QuestAreaDeliveryList.UI.TBL_DELIVERY_QUEST, "", length, this.isResetUI, (Func<int, Transform, Transform>) ((i, parent) =>
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
        if (i >= borderIndex)
          return;
        if (i >= questStartIndex)
          this.InitDelivery(showList[i - questStartIndex], t);
        else
          this.InitMap(t);
      }
    }));
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestAreaDeliveryList.UI.SCR_DELIVERY_QUEST)).enabled = true;
    this.RepositionTable();
  }

  protected override void InitMap(Transform t)
  {
    this.SetEvent(t, "WORLDMAP", (object) this.regionData.regionId);
    this.SetLabelText(t, (Enum) QuestAreaDeliveryList.UI.LBL_STORY_TITLE, "マップ");
  }

  protected override void GetDeliveryList()
  {
    this.deliveryInfo = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList();
    int groupId = this.regionData.groupId;
    uint regionId = this.regionData.regionId;
    List<Delivery> deliveryList = new List<Delivery>();
    int index = 0;
    for (int length = this.deliveryInfo.Length; index < length; ++index)
    {
      Delivery delivery = this.deliveryInfo[index];
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery.dId);
      if (deliveryTableData == null)
        Log.Warning("DeliveryTable Not Found : dId " + (object) delivery.dId);
      else if (!deliveryTableData.IsEvent())
      {
        if (Singleton<NPCTable>.I.GetNPCData((int) deliveryTableData.npcID) == null)
          Log.Error($"DeliveryTable NPC ID Found  : dId {(object) delivery.dId} : npcID {(object) deliveryTableData.npcID}");
        else if ((long) deliveryTableData.regionId == (long) regionId)
          deliveryList.Add(delivery);
        else if (groupId > 0 && deliveryTableData.regionId == groupId)
          deliveryList.Add(delivery);
      }
    }
    this.deliveryInfo = deliveryList.ToArray();
  }

  protected override List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
  {
    List<DeliveryTable.DeliveryData> clearedDliveryList = new List<DeliveryTable.DeliveryData>();
    List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
    int groupId = this.regionData.groupId;
    uint regionId = this.regionData.regionId;
    int index = 0;
    for (int count = clearStatusDelivery.Count; index < count; ++index)
    {
      ClearStatusDelivery d = clearStatusDelivery[index];
      if (d.deliveryStatus == 3)
      {
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) d.deliveryId);
        if ((deliveryTableData.type == DELIVERY_TYPE.STORY || deliveryTableData.type == DELIVERY_TYPE.ONCE) && ((long) deliveryTableData.regionId == (long) regionId || groupId > 0 && deliveryTableData.regionId == groupId) && !Array.Exists<Delivery>(this.deliveryInfo, (Predicate<Delivery>) (x => x.dId == d.deliveryId)))
        {
          clearedDliveryList.Add(deliveryTableData);
          if (deliveryTableData.readScriptId > 0U)
          {
            string title = deliveryTableData.clearEventTitle;
            if (string.IsNullOrEmpty(title))
              title = deliveryTableData.name;
            this.stories.Add(new QuestEventSelectList.Story((int) deliveryTableData.readScriptId, title));
          }
          else if (deliveryTableData.clearEventID > 0U)
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

  protected override bool ShouldShowEventMapButton() => this.showMap;

  protected override void OnQuery_SELECT_STORY()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) this.stories[(int) GameSection.GetEventData()].id,
      (object) "",
      (object) "",
      (object) new EventData[3]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("TO_QUEST", (object) null),
        new EventData("SELECT_AREA", (object) (int) this.regionData.regionId)
      }
    });
  }

  protected override void OnQuery_WORLDMAP()
  {
    if (this.regionData == null)
      return;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
    this.RequestEvent("OPEN_REGION_CHANGE", (object) (int) this.regionData.regionId);
  }

  private void OnQuery_SUMMARY()
  {
    GameSection.SetEventData((object) (int) this.regionData.regionId);
  }

  protected override void OnQuery_JUMP_TO_STORY_PAGE()
  {
    EventData[] eventDataArray = new EventData[1]
    {
      new EventData("SELECT_CHAPTER_FROM_OUTER", (object) null)
    };
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.GetChapterId(),
      (object) eventDataArray
    });
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
    TEX_AREA_BG,
    GRD_DELIVERY,
    BTN_SUMMARY,
  }
}
