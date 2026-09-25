// Decompiled with JetBrains decompiler
// Type: QuestSeriesArenaSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestSeriesArenaSelectList : QuestSeriesArenaEventList
{
  private const string ARENA_FRAME_SPRITE = "RequestPlate_hero";
  private int eventId;
  private List<QuestEventSelectList.ShowDeliveryData> deliveryList = new List<QuestEventSelectList.ShowDeliveryData>();

  public override void Initialize()
  {
    this.eventId = (int) GameSection.GetEventData();
    base.Initialize();
  }

  protected override IEnumerator DoInitialize()
  {
    this.eventData = MonoBehaviourSingleton<QuestManager>.I._GetEventData(this.eventId);
    this.seriesArenaTopData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
    yield return (object) this.StartCoroutine(this.LoadSeriesArenaTopBanner());
    this.GetDeliveryList();
    this.EndInitialize();
  }

  protected override void OnQuery_TO_UNIQUE_STATUS()
  {
  }

  protected override void UpdateTable()
  {
    this.deliveryList = new List<QuestEventSelectList.ShowDeliveryData>();
    if (this.deliveryInfo != null)
    {
      for (int i = 0; i < this.deliveryInfo.Length; ++i)
        this.deliveryList.Add(new QuestEventSelectList.ShowDeliveryData(i, false, this.deliveryInfo[i]));
    }
    if (this.clearedDeliveries != null)
    {
      for (int index = 0; index < this.clearedDeliveries.Count; ++index)
        this.deliveryList.Add(new QuestEventSelectList.ShowDeliveryData(index, true, this.clearedDeliveries[index]));
    }
    this.deliveryList.Sort((IComparer<QuestEventSelectList.ShowDeliveryData>) new QuestSeriesArenaSelectList.SeriesArenaSort());
    int num1 = 0;
    if (this.stories.Count > 0)
    {
      int num2 = num1 + 1;
    }
    int count = this.deliveryList.Count;
    if (this.seriesArenaTopData.enableRanking)
      ++count;
    int questStartIndex = 0;
    if (this.seriesArenaTopData.enableRanking)
      questStartIndex++;
    Transform ctrl = this.GetCtrl((Enum) QuestSeriesArenaSelectList.UI.TBL_DELIVERY_QUEST);
    if (Object.op_Implicit((Object) ctrl))
    {
      int num3 = 0;
      for (int childCount = ctrl.childCount; num3 < childCount; ++num3)
      {
        Transform child = ctrl.GetChild(0);
        child.parent = (Transform) null;
        Object.Destroy((Object) ((Component) child).gameObject);
      }
    }
    int num4 = !MonoBehaviourSingleton<UserInfoManager>.IsValid() ? 0 : (MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal ? 1 : 0);
    this.SetTable((Enum) QuestSeriesArenaSelectList.UI.TBL_DELIVERY_QUEST, "", count, false, (Func<int, Transform, Transform>) ((i, parent) =>
    {
      Transform transform = (Transform) null;
      if (i >= questStartIndex)
        transform = this.Realizes("QuestRequestItemSeriesArena", parent);
      else if (i == 0)
        transform = this.Realizes("QuestArenaRequestItemToRanking", parent);
      return transform;
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (Object.op_Equality((Object) t, (Object) null))
        return;
      this.SetActive(t, true);
      if (i >= questStartIndex)
      {
        this.InitNormalDelivery(i - questStartIndex, t);
      }
      else
      {
        if (i != 0)
          return;
        this.InitGoToRankingButton(t);
      }
    }));
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestSeriesArenaSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
    this.RepositionTable();
  }

  protected override void InitNormalDelivery(int index, Transform t)
  {
    QuestEventSelectList.ShowDeliveryData delivery = this.deliveryList[index];
    this.SetEvent(t, "SELECT_SERIES_ARENA", index);
    this.SetUpSeriesArenaListItem(t, delivery.data);
    if (delivery.isCompleted)
      this.SetActive(t, (Enum) QuestSeriesArenaSelectList.UI.OBJ_REQUEST_COMPLETED, true);
    this.SetSprite(t, (Enum) QuestSeriesArenaSelectList.UI.SPR_FRAME, "RequestPlate_hero");
    bool is_visible = true;
    QuestInfoData.Mission[] missionData = QuestInfoData.CreateMissionData(delivery.data.GetQuestData());
    if (missionData == null)
    {
      this.SetActive(t, (Enum) QuestSeriesArenaSelectList.UI.SPR_MISSION_CROWN_ON, false);
      this.SetActive(t, (Enum) QuestSeriesArenaSelectList.UI.SPR_MISSION_CROWN_OFF, true);
    }
    else
    {
      for (int index1 = 0; index1 < missionData.Length; ++index1)
      {
        if (missionData[index1].state < CLEAR_STATUS.CLEAR)
        {
          is_visible = false;
          break;
        }
      }
      this.SetActive(t, (Enum) QuestSeriesArenaSelectList.UI.SPR_MISSION_CROWN_ON, is_visible);
      this.SetActive(t, (Enum) QuestSeriesArenaSelectList.UI.SPR_MISSION_CROWN_OFF, !is_visible);
    }
  }

  private void SetUpSeriesArenaListItem(Transform t, DeliveryTable.DeliveryData info)
  {
    QuestRequestItemSeriesArena requestItemSeriesArena = ((Component) t).GetComponent<QuestRequestItemSeriesArena>();
    if (Object.op_Equality((Object) requestItemSeriesArena, (Object) null))
      requestItemSeriesArena = ((Component) t).gameObject.AddComponent<QuestRequestItemSeriesArena>();
    requestItemSeriesArena.InitUI();
    requestItemSeriesArena.Setup(t, info);
  }

  private void OnQuery_SECTION_BACK()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(0);
  }

  private void OnQuery_SELECT_SERIES_ARENA()
  {
    DeliveryTable.DeliveryData data = this.deliveryList[(int) GameSection.GetEventData()].data;
    int num = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) data.id) ? 1 : 0;
    Delivery notClearDelivery = this.GetNotClearDelivery(data.id);
    if (num != 0)
    {
      this.changeToDeliveryClearEvent = true;
      bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
      bool enable_clear_event = data.clearEventID > 0U;
      GameSection.StayEvent();
      MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
      MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(notClearDelivery.uId, enable_clear_event, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) =>
      {
        if (is_success)
        {
          if (is_tutorial)
            TutorialStep.isSendFirstRewardComplete = true;
          if (!enable_clear_event)
          {
            MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
            GameSection.ChangeStayEvent("SERIES_ARENA_REWARD", (object) new object[2]
            {
              (object) (int) data.id,
              (object) recv_reward
            });
          }
          else
            GameSection.ChangeStayEvent("CLEAR_EVENT", (object) new object[3]
            {
              (object) (int) data.clearEventID,
              (object) (int) data.id,
              (object) recv_reward
            });
        }
        else
          this.changeToDeliveryClearEvent = false;
        MonoBehaviourSingleton<QuestManager>.I.SendGetEventList((Action<bool>) (q => MonoBehaviourSingleton<DeliveryManager>.I.SendEventList((Action<bool>) (d => GameSection.ResumeEvent(is_success)))));
      }));
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(data.GetQuestData().questID);
      GameSection.ChangeEvent("TO_ROOM", (object) data);
    }
  }

  private Delivery GetNotClearDelivery(uint deliveryId)
  {
    return this.deliveryInfo.Find<Delivery>((Predicate<Delivery>) (d => (long) d.dId == (long) deliveryId));
  }

  protected override void InitCompletedDelivery(int completedIndex, Transform t)
  {
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
    SPR_MISSION_CROWN_ON,
    SPR_MISSION_CROWN_OFF,
    SCR_DELIVERY_QUEST,
    OBJ_IMAGE,
    BTN_EVENT,
    OBJ_FRAME,
    SPR_BG_FRAME,
    SPR_FRAME,
  }

  public class SeriesArenaSort : IComparer<QuestEventSelectList.ShowDeliveryData>
  {
    public int Compare(
      QuestEventSelectList.ShowDeliveryData x,
      QuestEventSelectList.ShowDeliveryData y)
    {
      bool flag1 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) x.data.id);
      bool flag2 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) y.data.id);
      if (flag1 == flag2)
        return x.data.displayOrder - y.data.displayOrder;
      return flag1 ? -1 : 1;
    }
  }
}
