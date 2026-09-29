// Decompiled with JetBrains decompiler
// Type: DeliveryManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable disable
public class DeliveryManager : MonoBehaviourSingleton<DeliveryManager>
{
  private List<Delivery> delivery;
  private const string UNKNOWN_MAP = "????????";
  private bool checkNewDeliveryAtHomeScene;
  public List<int> noticeNewDeliveryAtHomeScene = new List<int>();
  public List<int> noticeNewDeliveryAtInGame = new List<int>();
  public float dailyUpdateRemainTime;
  public float weeklyUpdateRemainTime;
  private List<Coroutine> remainTimeCoroutine = new List<Coroutine>();
  private Coroutine m_coroutine;
  private int m_compDeliveryId;
  private Coroutine m_coroutinePortal;
  private List<EventNormalListData> eventNormalListData;
  public bool isUpdateEventListData;
  public List<EventListData> eventListData;
  private bool firstSetGetList = true;

  public bool initialized { get; private set; }

  public List<ClearStatusDelivery> clearStatusDelivery { private set; get; }

  public bool isNoticeNewDeliveryAtHomeScene => this.noticeNewDeliveryAtHomeScene.Count > 0;

  public bool isStoryEventEnd { get; set; }

  public List<int> releasedEventIds { get; private set; }

  public void AddReleasedRegion(int regionId)
  {
    if (this.releasedEventIds == null)
      this.releasedEventIds = new List<int>();
    this.releasedEventIds.Add(regionId);
  }

  public void UpdateDeliveryReaminTime(float daily, float weekly)
  {
    foreach (Coroutine coroutine in this.remainTimeCoroutine)
      this.StopCoroutine(coroutine);
    this.remainTimeCoroutine = new List<Coroutine>();
    if ((double) daily < 0.0 || (double) weekly < 0.0)
      return;
    Coroutine coroutine1 = this.StartCoroutine(this.CheckUpdateDeliveryItem(daily));
    Coroutine coroutine2 = this.StartCoroutine(this.CheckUpdateDeliveryItem(weekly));
    this.remainTimeCoroutine.Add(coroutine1);
    this.remainTimeCoroutine.Add(coroutine2);
    this.dailyUpdateRemainTime = daily;
    this.weeklyUpdateRemainTime = weekly;
  }

  private IEnumerator CheckUpdateDeliveryItem(float remainTime)
  {
    yield return (object) new WaitForSeconds(remainTime);
    while (Protocol.isBusy)
      yield return (object) null;
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<QuestManager>.I.SendGetDeliveryList((Action<bool>) (b => { }))));
  }

  public bool IsExistDelivery(DELIVERY_TYPE[] typeList)
  {
    foreach (Delivery delivery in this.delivery)
    {
      foreach (DELIVERY_TYPE type in typeList)
      {
        if ((DELIVERY_TYPE) delivery.type == type && delivery.dId > 0)
          return true;
      }
    }
    return false;
  }

  public bool IsExistNotClearDelivery(DELIVERY_CONDITION_TYPE[] conditionTypeList)
  {
    int index1 = 0;
    for (int count = this.delivery.Count; index1 < count; ++index1)
    {
      if (this.delivery[index1].dId > 0)
      {
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.delivery[index1].dId);
        if (deliveryTableData != null && deliveryTableData.needs != null && deliveryTableData.needs.Length != 0)
        {
          int index2 = 0;
          for (int length = deliveryTableData.needs.Length; index2 < length; ++index2)
          {
            DeliveryTable.DeliveryData.NeedData need = deliveryTableData.needs[index2];
            if (need != null && ((IEnumerable<DELIVERY_CONDITION_TYPE>) conditionTypeList).Contains<DELIVERY_CONDITION_TYPE>(need.conditionType) && !this.IsClearDelivery(deliveryTableData.id))
              return true;
          }
        }
      }
    }
    return false;
  }

  public List<EventListData> FindSeriesArenaDataList()
  {
    List<EventListData> eventList = new List<EventListData>();
    MonoBehaviourSingleton<DeliveryManager>.I.eventListData.ForEach((Action<EventListData>) (data =>
    {
      if (data.eventTypeEnum != EVENT_TYPE.SERIES_ARENA)
        return;
      eventList.Add(data);
    }));
    return eventList;
  }

  public EventListData FindSeriesArenaTopData()
  {
    return MonoBehaviourSingleton<DeliveryManager>.I.eventListData.Find((Predicate<EventListData>) (data => data.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR));
  }

  public CLEAR_STATUS GetClearStatusDelivery(uint deliveryId)
  {
    CLEAR_STATUS clearStatusDelivery1 = CLEAR_STATUS.NEW;
    ClearStatusDelivery clearStatusDelivery2 = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (data => (long) data.deliveryId == (long) deliveryId));
    if (clearStatusDelivery2 != null)
      clearStatusDelivery1 = (CLEAR_STATUS) clearStatusDelivery2.deliveryStatus;
    return clearStatusDelivery1;
  }

  public bool IsClearDelivery(uint deliveryId)
  {
    switch (this.GetClearStatusDelivery(deliveryId))
    {
      case CLEAR_STATUS.CLEAR:
      case CLEAR_STATUS.ALL_CLEAR:
        return true;
      default:
        return false;
    }
  }

  public bool IsAppearDelivery(uint deliveryId)
  {
    if (deliveryId == 0U)
      return false;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
    if (deliveryTableData == null)
      return false;
    return deliveryTableData.appearDeliveryId == 0U || this.IsClearDelivery(deliveryTableData.appearDeliveryId);
  }

  public bool hasProgressDailyDelivery
  {
    get
    {
      if (MonoBehaviourSingleton<DeliveryManager>.I.delivery == null)
        return false;
      bool found = false;
      this.delivery.ForEach((Action<Delivery>) (data =>
      {
        if (data.dId <= 0 || data.type != 0 || this.IsClearDelivery((uint) data.dId))
          return;
        found = true;
      }));
      return found;
    }
  }

  public List<Delivery> GetEventDeliveryList(int event_id, bool do_sort = false)
  {
    List<Delivery> list = new List<Delivery>();
    MonoBehaviourSingleton<DeliveryManager>.I.delivery.ForEach((Action<Delivery>) (d =>
    {
      if (d.dId <= 0)
        return;
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) d.dId);
      if (deliveryTableData.eventID != event_id || deliveryTableData.type != DELIVERY_TYPE.EVENT && deliveryTableData.type != DELIVERY_TYPE.SUB_EVENT)
        return;
      list.Add(d);
    }));
    return list;
  }

  public List<Delivery> GetNormalDeliveryList(int region_id, bool do_sort = false)
  {
    List<Delivery> list = new List<Delivery>();
    MonoBehaviourSingleton<DeliveryManager>.I.delivery.ForEach((Action<Delivery>) (d =>
    {
      if (d.dId <= 0)
        return;
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) d.dId);
      if (deliveryTableData.regionId != region_id || deliveryTableData.type != DELIVERY_TYPE.STORY && deliveryTableData.type != DELIVERY_TYPE.ONCE)
        return;
      list.Add(d);
    }));
    return list;
  }

  public Delivery[] GetDeliveryList(bool do_sort = true)
  {
    if (MonoBehaviourSingleton<DeliveryManager>.I.delivery == null)
      return new Delivery[0];
    List<Delivery> list = new List<Delivery>();
    MonoBehaviourSingleton<DeliveryManager>.I.delivery.ForEach((Action<Delivery>) (d =>
    {
      if (d.dId <= 0 || list.Exists((Predicate<Delivery>) (x => x.dId == d.dId)))
        return;
      list.Add(d);
    }));
    if (do_sort)
      list.Sort((Comparison<Delivery>) ((l, r) =>
      {
        DeliveryTable.DeliveryData deliveryTableData1 = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) l.dId);
        DeliveryTable.DeliveryData deliveryTableData2 = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) r.dId);
        int id1 = deliveryTableData1 != null ? (int) deliveryTableData1.id : 0;
        uint id2 = deliveryTableData2 != null ? deliveryTableData2.id : 0U;
        int length1 = id1 != 0 ? deliveryTableData1.needs.Length : 0;
        int length2 = id2 != 0U ? deliveryTableData2.needs.Length : 0;
        int num1 = id1 != 0 ? 1 : 0;
        int num2 = id2 != 0U ? 1 : 0;
        int idx1 = 0;
        for (int index = length1; idx1 < index; ++idx1)
        {
          int have;
          int need;
          MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(l.dId, out have, out need, (uint) idx1);
          if (need > 0 && need > have)
          {
            num1 = 0;
            break;
          }
        }
        int idx2 = 0;
        for (int index = length2; idx2 < index; ++idx2)
        {
          int have;
          int need;
          MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(r.dId, out have, out need, (uint) idx2);
          if (need > 0 && need > have)
          {
            num2 = 0;
            break;
          }
        }
        int deliveryList1 = num2 - num1;
        if (deliveryList1 != 0)
          return deliveryList1;
        if (l.order != r.order)
          return r.order - l.order;
        int deliveryList2 = Singleton<DeliveryTable>.I.GetSortPriority(deliveryTableData1.type) - Singleton<DeliveryTable>.I.GetSortPriority(deliveryTableData2.type);
        if (deliveryList2 == 0)
          deliveryList2 = l.dId - r.dId;
        return deliveryList2;
      }));
    return list.ToArray();
  }

  public List<DeliveryTable.DeliveryData> GetDeliveryTableDataList(bool do_sort = true)
  {
    List<DeliveryTable.DeliveryData> deliveryTableDataList = new List<DeliveryTable.DeliveryData>();
    Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(do_sort);
    int index = 0;
    for (int length = deliveryList.Length; index < length; ++index)
    {
      Delivery delivery = deliveryList[index];
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery.dId);
      if (deliveryTableData == null)
        Log.Warning("DeliveryTable Not Found : dId " + (object) delivery.dId);
      else
        deliveryTableDataList.Add(deliveryTableData);
    }
    return deliveryTableDataList;
  }

  public bool IsCompletableDelivery(int delivery_id)
  {
    ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (data => data.deliveryId == delivery_id));
    if (clearStatusDelivery == null)
      return false;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    int idx = 0;
    for (int length = deliveryTableData.needs.Length; idx < length; ++idx)
    {
      if (deliveryTableData.needs[idx].IsValid() && (long) clearStatusDelivery.GetNeedCount((uint) idx) < (long) (uint) deliveryTableData.needs[idx].needNum)
        return false;
    }
    return true;
  }

  public bool IsAllClearedEvent(int eventId)
  {
    int index = 0;
    for (int count = this.delivery.Count; index < count; ++index)
    {
      Delivery delivery = this.delivery[index];
      if (delivery.dId != 0 && Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery.dId).eventID == eventId)
        return false;
    }
    return true;
  }

  public int GetCompletableDeliveryNum() => this.CountCompletableDeliveryNum();

  public int GetCompletableDeliveryNum(DELIVERY_TYPE[] delivery_type)
  {
    return this.CountCompletableDeliveryNum(delivery_type: delivery_type);
  }

  public int GetCompletableEventDeliveryNum()
  {
    return this.CountCompletableDeliveryNum((Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>) ((clearStatus, table) => table.IsEvent()));
  }

  public int GetCompletableNormalDeliveryNum()
  {
    return this.CountCompletableDeliveryNum((Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>) ((clearStatus, table) => !table.IsEvent()));
  }

  public int GetCompletableEventDeliveryNum(int event_id)
  {
    return this.CountCompletableDeliveryNum((Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>) ((clearStatus, table) => table.IsEvent() && table.eventID == event_id));
  }

  public int GetCompletableSeriesArenaEventDeliveryNum()
  {
    List<EventListData> seriesArenaList = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaDataList();
    return this.CountCompletableDeliveryNum((Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>) ((clearStatus, table) => table.IsEvent() && seriesArenaList.Any<EventListData>((Func<EventListData, bool>) (seriesArena => seriesArena.eventId == table.eventID))));
  }

  public int GetCompletableRegionDeliveryNum(int regionId, int groupId)
  {
    return this.CountCompletableDeliveryNum((Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>) ((clearStatus, table) =>
    {
      if (table.regionId == regionId)
        return true;
      return groupId > 0 && table.regionId == groupId;
    }), new DELIVERY_TYPE[2]
    {
      DELIVERY_TYPE.STORY,
      DELIVERY_TYPE.ONCE
    });
  }

  private int CountCompletableDeliveryNum(
    Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool> condition = null,
    DELIVERY_TYPE[] delivery_type = null)
  {
    if (this.clearStatusDelivery == null || this.clearStatusDelivery.Count == 0)
      return 0;
    int num = 0;
    this.clearStatusDelivery.ForEach((Action<ClearStatusDelivery>) (data =>
    {
      if (data == null || this.delivery.FindIndex((Predicate<Delivery>) (x => x.dId == data.deliveryId)) < 0 || this.IsLimit(data.deliveryId))
        return;
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) data.deliveryId);
      if (data.deliveryStatus >= 3 && (deliveryTableData.type != DELIVERY_TYPE.EVENT && deliveryTableData.type != DELIVERY_TYPE.SUB_EVENT || !this.delivery.Exists((Predicate<Delivery>) (x => x.dId == data.deliveryId))) || deliveryTableData.subType == DELIVERY_SUB_TYPE.BINGO || deliveryTableData.subType == DELIVERY_SUB_TYPE.ROW_BINGO || deliveryTableData.subType == DELIVERY_SUB_TYPE.ALL_BINGO)
        return;
      if (delivery_type != null)
      {
        bool flag = false;
        foreach (DELIVERY_TYPE deliveryType in delivery_type)
        {
          if (deliveryTableData.type == deliveryType)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return;
      }
      if (deliveryTableData == null)
        return;
      int idx = 0;
      for (int length = deliveryTableData.needs.Length; idx < length; ++idx)
      {
        if (deliveryTableData.needs[idx].IsValid() && (long) data.GetNeedCount((uint) idx) < (long) (uint) deliveryTableData.needs[idx].needNum)
          return;
      }
      if (condition != null && !condition(data, deliveryTableData))
        return;
      ++num;
    }));
    return num;
  }

  public uint GetCompletableStoryDelivery()
  {
    if (this.clearStatusDelivery == null || this.clearStatusDelivery.Count == 0)
      return 0;
    uint id = 0;
    this.clearStatusDelivery.ForEach((Action<ClearStatusDelivery>) (data =>
    {
      if (data == null || id != 0U || data.deliveryStatus >= 3)
        return;
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) data.deliveryId);
      if (deliveryTableData == null || deliveryTableData.type != DELIVERY_TYPE.STORY)
        return;
      int idx = 0;
      for (int length = deliveryTableData.needs.Length; idx < length; ++idx)
      {
        if (deliveryTableData.needs[idx].IsValid() && (long) data.GetNeedCount((uint) idx) < (long) (uint) deliveryTableData.needs[idx].needNum)
          return;
      }
      id = deliveryTableData.id;
    }));
    return id;
  }

  public bool HasClearEventID(uint deliveryId)
  {
    if (deliveryId <= 0U)
      return false;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
    return deliveryTableData != null && deliveryTableData.clearEventID > 0U;
  }

  public void GetProgressDelivery(int delivery_id, out int have, out int need, uint idx = 0)
  {
    have = 0;
    need = 0;
    ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (data => data.deliveryId == delivery_id));
    if (clearStatusDelivery != null)
      have = clearStatusDelivery.GetNeedCount(idx);
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    if (deliveryTableData == null)
      return;
    need = (int) deliveryTableData.GetNeedItemNum(idx);
  }

  public void GetAllProgressDelivery(int delivery_id, out int have, out int need)
  {
    have = 0;
    need = 0;
    ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (data => data.deliveryId == delivery_id));
    if (clearStatusDelivery != null)
      have = clearStatusDelivery.GetAllNeedCount();
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    if (deliveryTableData == null)
      return;
    need = (int) deliveryTableData.GetAllNeedItemNum();
  }

  private bool IsLimit(int delivery_id)
  {
    string s = string.Empty;
    Delivery delivery = this.delivery.Find((Predicate<Delivery>) (data => data.dId == delivery_id));
    if (delivery != null && (delivery.type == 11 || delivery.type == 12))
      s = delivery.limit;
    if (string.IsNullOrEmpty(s))
      return false;
    DateTime now = TimeManager.GetNow();
    return DateTime.Parse(s).CompareTo(now) < 0;
  }

  public string GetLimitText(int delivery_id)
  {
    string s = string.Empty;
    int have;
    int need;
    this.GetAllProgressDelivery(delivery_id, out have, out need);
    if (need > 0 && have >= need)
      return s;
    Delivery delivery = this.delivery.Find((Predicate<Delivery>) (data => data.dId == delivery_id));
    if (delivery != null)
      s = delivery.limit;
    if (string.IsNullOrEmpty(s))
      return s;
    DateTime now = TimeManager.GetNow();
    DateTime dateTime = DateTime.Parse(s);
    if (dateTime.CompareTo(now) < 0)
      return StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 11U);
    TimeSpan timeSpan = dateTime.Subtract(now);
    StringBuilder stringBuilder = new StringBuilder("");
    if (timeSpan.Days > 0)
      stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 9U), (object) timeSpan.Days));
    else if (timeSpan.Hours > 0)
      stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10U), (object) timeSpan.Hours));
    else
      stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10U), (object) 1));
    stringBuilder.Append(" " + StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 12U));
    return stringBuilder.ToString();
  }

  public string GetTargetItemName(int delivery_id, uint idx = 0)
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    return deliveryTableData != null ? deliveryTableData.GetNeedItemName(idx) : string.Empty;
  }

  public void GetDeliveryData(
    int delivery_id,
    out int have,
    out int need,
    out string item_name,
    out string limit_time)
  {
    this.GetProgressDelivery(delivery_id, out have, out need);
    limit_time = this.GetLimitText(delivery_id);
    item_name = this.GetTargetItemName(delivery_id);
  }

  public void GetDeliveryDataAllNeeds(
    int delivery_id,
    out int have,
    out int need,
    out string item_name,
    out string limit_time)
  {
    this.GetAllProgressDelivery(delivery_id, out have, out need);
    limit_time = this.GetLimitText(delivery_id);
    item_name = this.GetTargetItemName(delivery_id);
  }

  public void GetTargetEnemyData(
    int delivery_id,
    out uint jump_quest_id,
    out uint jump_map_id,
    out string map_name,
    out string enemy_name,
    out DIFFICULTY_TYPE? difficulty,
    out int[] targetPortalID)
  {
    jump_quest_id = 0U;
    jump_map_id = 0U;
    map_name = string.Empty;
    enemy_name = string.Empty;
    difficulty = new DIFFICULTY_TYPE?();
    targetPortalID = (int[]) null;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    if (deliveryTableData == null)
      return;
    uint enemy_id = deliveryTableData.GetEnemyID();
    uint mapId = deliveryTableData.GetMapID();
    int id = (int) Singleton<FieldMapTable>.I.GetTargetEnemyPopMapID(enemy_id);
    if (id != 0)
    {
      if (deliveryTableData.jumpMapID < 0)
      {
        id = -1;
      }
      else
      {
        if (mapId > 0U)
          id = (int) mapId;
        if (deliveryTableData.jumpMapID > 0)
          id = deliveryTableData.jumpMapID;
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) id);
        if (fieldMapData != null)
        {
          map_name = fieldMapData.mapName;
          jump_map_id = (uint) id;
        }
        else
          id = -1;
      }
      if (id == -1)
      {
        map_name = "????????";
        jump_map_id = 0U;
      }
    }
    else if (deliveryTableData.jumpMapID > 0)
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) deliveryTableData.jumpMapID);
      if (fieldMapData != null)
      {
        map_name = fieldMapData.mapName;
        targetPortalID = deliveryTableData.targetPortalID;
        if (targetPortalID != null)
        {
          bool flag = false;
          int index = 0;
          for (int length = targetPortalID.Length; index < length; ++index)
          {
            if (targetPortalID[index] != 0)
            {
              flag = true;
              break;
            }
          }
          if (flag)
            map_name = string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 2U), (object) map_name);
        }
        jump_map_id = (uint) deliveryTableData.jumpMapID;
        enemy_name = StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 1U);
      }
      else
      {
        map_name = "????????";
        jump_map_id = 0U;
      }
    }
    else
    {
      bool is_find = false;
      string tmp_name = string.Empty;
      uint tmp_quest_id = 0;
      DIFFICULTY_TYPE tmp_difficulty = DIFFICULTY_TYPE.LV1;
      Singleton<QuestTable>.I.AllQuestData((Action<QuestTable.QuestTableData>) (data =>
      {
        if (is_find)
          return;
        for (int index = 0; index < data.seriesNum; ++index)
        {
          if (data.enemyID[index] == (int) enemy_id)
          {
            is_find = true;
            tmp_name = data.questText;
            tmp_difficulty = data.difficulty;
            tmp_quest_id = data.questID;
          }
        }
      }));
      if (is_find)
      {
        map_name = tmp_name;
        difficulty = new DIFFICULTY_TYPE?(tmp_difficulty);
        jump_quest_id = tmp_quest_id;
      }
    }
    if (!string.IsNullOrEmpty(deliveryTableData.placeName))
      map_name = deliveryTableData.placeName;
    if (!string.IsNullOrEmpty(deliveryTableData.enemyName))
      enemy_name = deliveryTableData.enemyName;
    if (!string.IsNullOrEmpty(enemy_name))
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemy_id);
    if (enemyData != null)
      enemy_name = enemyData.name;
    if (enemy_id != 0U || mapId <= 0U)
      return;
    enemy_name = StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 0U);
  }

  public int ProgressDelivery(int delivery_id, int need_index, int add_num)
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    ClearStatusDelivery target_delivery_clear_status = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (data => data.deliveryId == delivery_id));
    if (target_delivery_clear_status == null)
    {
      target_delivery_clear_status = new ClearStatusDelivery();
      target_delivery_clear_status.deliveryId = delivery_id;
      if (deliveryTableData != null)
      {
        int num = 0;
        for (int length = deliveryTableData.needs.Length; num < length; ++num)
          target_delivery_clear_status.needCount.Add(0);
      }
      this.clearStatusDelivery.Add(target_delivery_clear_status);
    }
    int num1 = target_delivery_clear_status.needCount[need_index];
    target_delivery_clear_status.needCount[need_index] += add_num;
    if (target_delivery_clear_status.needCount[need_index] > (int) deliveryTableData.needs[need_index].needNum)
    {
      target_delivery_clear_status.needCount[need_index] = (int) deliveryTableData.needs[need_index].needNum;
      add_num = target_delivery_clear_status.needCount[need_index] - num1;
    }
    if (this.IsClearTutorialDelivery(deliveryTableData, target_delivery_clear_status) && UITutorialFieldHelper.IsValid())
      UITutorialFieldHelper.I.OnCollectItem();
    return add_num;
  }

  public bool IsClearTutorialDelivery(
    DeliveryTable.DeliveryData table,
    ClearStatusDelivery target_delivery_clear_status)
  {
    if (!TutorialStep.IsPlayingFirstBackHome() && !TutorialStep.IsPlayingFirstDelivery())
      return false;
    if (TutorialStep.IsPlayingFirstDelivery())
    {
      if (table == null || table.needs == null || target_delivery_clear_status.needCount.Count < table.needs.Length)
        return false;
      int index = 0;
      for (int length = table.needs.Length; index < length; ++index)
      {
        if (target_delivery_clear_status.needCount[index] < (int) table.needs[index].needNum)
          return false;
      }
    }
    return true;
  }

  public int[] GetRecvStoryDelivery()
  {
    List<int> list = new List<int>();
    this.delivery.ForEach((Action<Delivery>) (data =>
    {
      if (data.type != 8)
        return;
      list.Add(data.dId);
    }));
    return list.ToArray();
  }

  public void SetList()
  {
    if (!this.firstSetGetList)
      return;
    this.firstSetGetList = false;
    OnceDeliveryModel.Param delivery = MonoBehaviourSingleton<OnceManager>.I.result.delivery;
    this.delivery = delivery.delivery;
    this.clearStatusDelivery = delivery.clearStatusDelivery;
  }

  public void SendDeliveryComplete(
    string uId,
    bool enable_clear_event,
    Action<bool, DeliveryRewardList> call_back)
  {
    DeliveryCompleteModel.RequestSendForm postData = new DeliveryCompleteModel.RequestSendForm();
    postData.uId = uId;
    if (enable_clear_event)
      this.checkNewDeliveryAtHomeScene = true;
    Protocol.Send<DeliveryCompleteModel.RequestSendForm, DeliveryCompleteModel>(DeliveryCompleteModel.URL, postData, (Action<DeliveryCompleteModel>) (ret =>
    {
      this.checkNewDeliveryAtHomeScene = false;
      bool flag = false;
      DeliveryRewardList deliveryRewardList = (DeliveryRewardList) null;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          deliveryRewardList = ret.result.reward;
          if (ret.result.openRegionIds != null && ret.result.openRegionIds.Count > 0)
          {
            foreach (int openRegionId in ret.result.openRegionIds)
              MonoBehaviourSingleton<WorldMapManager>.I.AddReleasedRegion(openRegionId);
          }
          if (ret.result.openEventIds != null && ret.result.openEventIds.Count > 0)
          {
            using (List<int>.Enumerator enumerator = ret.result.openEventIds.GetEnumerator())
            {
              while (enumerator.MoveNext())
                this.AddReleasedRegion(enumerator.Current);
              break;
            }
          }
          break;
        case Error.WRN_DELIVERY_OVER:
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_OVER);
          break;
      }
      call_back(flag, deliveryRewardList);
    }));
  }

  public void SendDeliveryUpdate(Action<bool> call_back)
  {
    Protocol.Send<DeliveryUpdateModel>(DeliveryUpdateModel.URL, (Action<DeliveryUpdateModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendGetClearStatusList(
    List<DELIVERY_CONDITION_TYPE> condiditionTypeList,
    Action<bool, DeliveryGetClearStatusModel.Param> call_back)
  {
    DeliveryGetClearStatusModel.RequestSendForm postData = new DeliveryGetClearStatusModel.RequestSendForm();
    List<int> intList = new List<int>(condiditionTypeList.Count);
    int index = 0;
    for (int count = condiditionTypeList.Count; index < count; ++index)
      intList.Add((int) condiditionTypeList[index]);
    postData.conditionTypes = intList;
    Protocol.Send<DeliveryGetClearStatusModel.RequestSendForm, DeliveryGetClearStatusModel>(DeliveryGetClearStatusModel.URL, postData, (Action<DeliveryGetClearStatusModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.result);
    }));
  }

  public void UpdateClearStatuses(List<ClearStatusDelivery> clearStatusUpdateList)
  {
    if (clearStatusUpdateList == null || clearStatusUpdateList.Count <= 0)
      return;
    int i = 0;
    for (int count = clearStatusUpdateList.Count; i < count; i++)
    {
      ClearStatusDelivery clearStatusUpdate = clearStatusUpdateList[i];
      if (clearStatusUpdate != null && clearStatusUpdate.deliveryId > 0)
      {
        ClearStatusDelivery data = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (status => status.deliveryId == clearStatusUpdateList[i].deliveryId));
        if (data == null || data.deliveryId <= 0)
        {
          this.clearStatusDelivery.Add(clearStatusUpdate);
          this.CheckCompletableClearStatus(clearStatusUpdate);
        }
        else
        {
          int num = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(clearStatusUpdate.deliveryId) ? 1 : 0;
          data.deliveryId = clearStatusUpdate.deliveryId;
          data.deliveryStatus = clearStatusUpdate.deliveryStatus;
          data.needCount = clearStatusUpdate.needCount;
          if (num != 0)
            break;
          this.CheckCompletableClearStatus(data);
        }
      }
    }
  }

  public void SendReadStoryRead(int scriptId, Action<bool, Error> call_back)
  {
    Protocol.Send<ReadStoryReadModel.RequestSendForm, ReadStoryReadModel>(ReadStoryReadModel.URL, new ReadStoryReadModel.RequestSendForm()
    {
      scriptNum = scriptId
    }, (Action<ReadStoryReadModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.Error);
    }));
  }

  public void SendEventNormalList(Action<bool> call_back)
  {
    this.eventNormalListData = (List<EventNormalListData>) null;
    Protocol.Send<EventNormalListModel>(EventNormalListModel.URL, (Action<EventNormalListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.eventNormalListData = ret.result;
      }
      call_back(flag);
    }));
  }

  public EventNormalListData GetEventNormalListData(int regionId)
  {
    return this.eventNormalListData == null ? (EventNormalListData) null : this.eventNormalListData.Find((Predicate<EventNormalListData>) (x => x.regionId == regionId));
  }

  public void SendEventList(Action<bool> call_back)
  {
    this.eventListData = (List<EventListData>) null;
    Protocol.Send<EventListModel>(EventListModel.URL, (Action<EventListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.eventListData = ret.result;
        if (!this.eventListData.IsNullOrEmpty<EventListData>())
        {
          int index = 0;
          for (int count = this.eventListData.Count; index < count; ++index)
          {
            this.eventListData[index].OnRecv();
            this.eventListData[index].SetupEnum();
            this.eventListData[index].orderNo = index;
          }
        }
      }
      this.isUpdateEventListData = true;
      call_back(flag);
    }));
  }

  public bool IsCarnivalEvent(int eventId)
  {
    if (!this.HasEventListData())
      return false;
    List<EventListData> eventListData = MonoBehaviourSingleton<DeliveryManager>.I.eventListData;
    for (int index = 0; index < eventListData.Count; ++index)
    {
      if (eventListData[index].eventId == eventId && (eventListData[index].place == 110 || eventListData[index].place == 111))
        return true;
    }
    return false;
  }

  public bool HasEventListData() => !this.eventListData.IsNullOrEmpty<EventListData>();

  public EventListData GetEventListData(int eventId)
  {
    return this.eventListData == null ? (EventListData) null : this.eventListData.Find((Predicate<EventListData>) (x => x.eventId == eventId));
  }

  public void SendDebugSetDeliveryCount(
    string uId,
    Action<bool> call_back,
    int cnt0 = 0,
    int cnt1 = 0,
    int cnt2 = 0,
    int cnt3 = 0,
    int cnt4 = 0)
  {
    Protocol.Send<DebugSetDeliveryCntModel.RequestSendForm, DebugSetDeliveryCntModel>(DebugSetDeliveryCntModel.URL, new DebugSetDeliveryCntModel.RequestSendForm()
    {
      uId = uId,
      cnts = {
        cnt0,
        cnt1,
        cnt2,
        cnt3,
        cnt4
      }
    }, (Action<DebugSetDeliveryCntModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendDebugSetDeliveryCountByDeliveryId(
    int deliveryId,
    int cnt0,
    int cnt1,
    int cnt2,
    int cnt3,
    int cnt4,
    Action<bool> call_back)
  {
    Delivery delivery = this.delivery.Find((Predicate<Delivery>) (d => d.dId == deliveryId));
    if (delivery == null)
      return;
    this.SendDebugSetDeliveryCount(delivery.uId, (Action<bool>) (b => call_back(b)), cnt0, cnt1, cnt2, cnt3, cnt4);
  }

  public void SendDebugGetDelivery(int did, Action<bool> call_back)
  {
    Protocol.Send<DebugGetDeliveryModel.RequestSendForm, DebugGetDeliveryModel>(DebugGetDeliveryModel.URL, new DebugGetDeliveryModel.RequestSendForm()
    {
      did = did
    }, (Action<DebugGetDeliveryModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  private void DirtyDelivery()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE);
  }

  private void DirtyClearDelivery()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS);
  }

  public void OnDiff(BaseModelDiff.DiffDelivery diff)
  {
    bool normal_delivery_notice = false;
    bool daily_delivery_updated = false;
    bool weekly_delivery_updated = false;
    bool flag1 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      diff.add.ForEach((Action<Delivery>) (data =>
      {
        this.delivery.Add(data);
        bool flag2 = false;
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) data.dId);
        if (deliveryTableData.type == DELIVERY_TYPE.STORY || deliveryTableData.type == DELIVERY_TYPE.ONCE || deliveryTableData.type == DELIVERY_TYPE.ETC || deliveryTableData.type == DELIVERY_TYPE.DAILY || deliveryTableData.type == DELIVERY_TYPE.WEEKLY)
        {
          flag2 = true;
          normal_delivery_notice = true;
        }
        if (deliveryTableData.type == DELIVERY_TYPE.DAILY && data.dId > 0)
          daily_delivery_updated = true;
        if (deliveryTableData.type == DELIVERY_TYPE.WEEKLY && data.dId > 0)
          weekly_delivery_updated = true;
        if (this.checkNewDeliveryAtHomeScene && data.dId != 0 && flag2)
          this.noticeNewDeliveryAtHomeScene.Add(data.dId);
        if (!FieldManager.IsValidInGameNoQuest() || !flag2)
          return;
        this.noticeNewDeliveryAtInGame.Add(data.dId);
      }));
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<Delivery>) (data =>
      {
        Delivery delivery = this.delivery.Find((Predicate<Delivery>) (list_data => list_data.uId == data.uId));
        delivery.uId = data.uId;
        delivery.dId = data.dId;
        delivery.type = data.type;
        delivery.limit = data.limit;
        delivery.order = data.order;
        bool flag3 = false;
        if (data.dId == 0)
          return;
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) data.dId);
        if (deliveryTableData.type == DELIVERY_TYPE.STORY || deliveryTableData.type == DELIVERY_TYPE.ONCE || deliveryTableData.type == DELIVERY_TYPE.ETC || deliveryTableData.type == DELIVERY_TYPE.DAILY || deliveryTableData.type == DELIVERY_TYPE.WEEKLY)
        {
          flag3 = true;
          normal_delivery_notice = true;
        }
        if (deliveryTableData.type == DELIVERY_TYPE.DAILY && data.dId > 0)
          daily_delivery_updated = true;
        if (deliveryTableData.type == DELIVERY_TYPE.WEEKLY && data.dId > 0)
          weekly_delivery_updated = true;
        if (this.checkNewDeliveryAtHomeScene && data.dId != 0 && flag3)
          this.noticeNewDeliveryAtHomeScene.Add(data.dId);
        if (!FieldManager.IsValidInGameNoQuest() || !flag3)
          return;
        this.noticeNewDeliveryAtInGame.Add(data.dId);
      }));
      flag1 = true;
    }
    if (!flag1)
      return;
    if (normal_delivery_notice && !GameSaveData.instance.IsRecommendedDeliveryCheck())
    {
      GameSaveData.instance.recommendedDeliveryCheck = 1;
      GameSaveData.Save();
    }
    if (daily_delivery_updated && !GameSaveData.instance.IsRecommendedDailyDeliveryCheck())
    {
      GameSaveData.instance.recommendedDailyDeliveryCheck = 1;
      GameSaveData.instance.recommendedDailyDeliveryCheckAtHome = 1;
      GameSaveData.Save();
    }
    if (weekly_delivery_updated && !GameSaveData.instance.IsRecommendedWeeklyDeliveryCheck())
    {
      GameSaveData.instance.recommendedWeeklyDeliveryCheck = 1;
      GameSaveData.Save();
    }
    this.DirtyDelivery();
  }

  public void OnDiff(BaseModelDiff.DiffClearStatusDelivery diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      diff.add.ForEach((Action<ClearStatusDelivery>) (data =>
      {
        this.clearStatusDelivery.RemoveAll((Predicate<ClearStatusDelivery>) (find_data => find_data.deliveryId == data.deliveryId));
        this.clearStatusDelivery.Add(data);
        this.CheckCompletableClearStatus(data);
      }));
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<ClearStatusDelivery>) (data =>
      {
        ClearStatusDelivery data1 = this.clearStatusDelivery.Find((Predicate<ClearStatusDelivery>) (list_data => list_data.deliveryId == data.deliveryId));
        data1.deliveryId = data.deliveryId;
        data1.deliveryStatus = data.deliveryStatus;
        data1.needCount = data.needCount;
        this.CheckCompletableClearStatus(data1);
      }));
      flag = true;
    }
    if (!flag)
      return;
    this.DirtyClearDelivery();
  }

  private void CheckCompletableClearStatus(ClearStatusDelivery data)
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(data.deliveryId))
      return;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) data.deliveryId);
    if (deliveryTableData.IsStoryDelivery() || deliveryTableData.GetConditionType() == DELIVERY_CONDITION_TYPE.NONE || this.IsDefeatFieldConditionType(deliveryTableData.GetConditionType()) || this.IsDeliveryArena(deliveryTableData) || !MonoBehaviourSingleton<UIAnnounceBand>.IsValid())
      return;
    string conditionTitle = !DeliveryManager.IsDeliveryBingo(deliveryTableData) ? StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 1U) : StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 2U);
    MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(deliveryTableData.name, conditionTitle);
    SoundManager.PlayOneshotJingle(40000030);
  }

  public Network.EventData GetEventCleardDeliveryData()
  {
    if (this.m_compDeliveryId == 0)
      return (Network.EventData) null;
    List<Network.EventData> eventDataList1 = new List<Network.EventData>((IEnumerable<Network.EventData>) MonoBehaviourSingleton<QuestManager>.I.eventList);
    eventDataList1.RemoveAll((Predicate<Network.EventData>) (e => e.HasEndDate() && e.GetRest() < 0));
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.m_compDeliveryId);
    if (deliveryTableData == null)
      return (Network.EventData) null;
    int eventId = deliveryTableData.eventID;
    Network.EventData cleardDeliveryData = (Network.EventData) null;
    int index1 = 0;
    for (int count = eventDataList1.Count; index1 < count; ++index1)
    {
      if (eventDataList1[index1].eventId == eventId)
      {
        cleardDeliveryData = eventDataList1[index1];
        break;
      }
    }
    List<Network.EventData> eventDataList2 = new List<Network.EventData>((IEnumerable<Network.EventData>) MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection());
    int index2 = 0;
    for (int count = eventDataList2.Count; index2 < count; ++index2)
    {
      if (eventDataList2[index2].eventId == eventId)
      {
        cleardDeliveryData = eventDataList2[index2];
        break;
      }
    }
    return cleardDeliveryData;
  }

  public void DeleteCleardDeliveryId() => this.m_compDeliveryId = 0;

  public void CheckAnnouncePortalOpen()
  {
    if (this.m_coroutinePortal != null)
      return;
    this.m_coroutinePortal = this.StartCoroutine(this.CheckPortalOpen());
  }

  public void CheckAnnounceHomeReturn(int delivery_id)
  {
    if (this.m_compDeliveryId != 0)
      return;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    if (!deliveryTableData.IsClearDialogInGame() || this.IsDeliveryExplore(deliveryTableData) || this.IsDeliveryRush(deliveryTableData) || this.IsDeliveryWave(deliveryTableData))
      return;
    if (this.m_coroutine != null)
      this.StopCoroutine(this.m_coroutine);
    if (deliveryTableData.IsEvent())
      this.m_compDeliveryId = delivery_id;
    this.m_coroutine = this.StartCoroutine(this.CheckRequestHomeReturn());
  }

  private bool IsDeliveryExplore(DeliveryTable.DeliveryData delivery)
  {
    List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
    if (eventList == null)
      return false;
    int index = 0;
    for (int count = eventList.Count; index < count; ++index)
    {
      if (eventList[index].eventId == delivery.eventID && eventList[index].eventType == 4)
        return true;
    }
    return false;
  }

  private bool IsDeliveryRush(DeliveryTable.DeliveryData delivery)
  {
    List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
    if (eventList == null)
      return false;
    int index = 0;
    for (int count = eventList.Count; index < count; ++index)
    {
      if (eventList[index].eventId == delivery.eventID && eventList[index].eventType == 12)
        return true;
    }
    return false;
  }

  private bool IsDeliveryWave(DeliveryTable.DeliveryData delivery)
  {
    List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
    if (eventList == null)
      return false;
    int index = 0;
    for (int count = eventList.Count; index < count; ++index)
    {
      if (eventList[index].eventId == delivery.eventID && eventList[index].eventType == 27)
        return true;
    }
    return false;
  }

  private bool IsDeliveryArena(DeliveryTable.DeliveryData delivery)
  {
    List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
    if (eventList == null)
      return false;
    for (int index = 0; index < eventList.Count; ++index)
    {
      if (eventList[index].eventId == delivery.eventID && eventList[index].eventType == 15)
        return true;
    }
    return false;
  }

  private static bool IsDeliveryBingo(DeliveryTable.DeliveryData delivery)
  {
    return delivery.subType == DELIVERY_SUB_TYPE.BINGO || delivery.subType == DELIVERY_SUB_TYPE.ROW_BINGO || delivery.subType == DELIVERY_SUB_TYPE.ALL_BINGO;
  }

  public static bool IsDeliveryBingo(uint id)
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(id);
    return deliveryTableData != null && DeliveryManager.IsDeliveryBingo(deliveryTableData);
  }

  private IEnumerator CheckRequestHomeReturn()
  {
    yield return (object) null;
    while (!this.IsDeleteCleardAnnounce())
    {
      if (this.IsDispCleardAnnounce())
      {
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", ((Component) this).gameObject, "CLEARED_RETURN");
        this.m_coroutine = (Coroutine) null;
        yield break;
      }
      yield return (object) null;
    }
    this.m_coroutine = (Coroutine) null;
  }

  private IEnumerator CheckPortalOpen()
  {
    yield return (object) null;
    while (!this.IsDeleteCleardAnnounce())
    {
      if (this.IsDispCleardAnnounce())
      {
        if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
        {
          this.m_coroutinePortal = (Coroutine) null;
          yield break;
        }
        List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
        if (portalObjectList == null)
        {
          this.m_coroutinePortal = (Coroutine) null;
          yield break;
        }
        bool flag = false;
        for (int index = 0; index < portalObjectList.Count; ++index)
        {
          FieldMapTable.PortalTableData portalData = portalObjectList[index].portalData;
          if ((FieldManager.IsOpenPortalClearOrder(portalData) ? 1 : (FieldManager.IsOpenPortal(portalData) ? 1 : 0)) != 0 && GameSaveData.instance.isNewReleasePortal(portalObjectList[index].portalID))
          {
            PortalObject root_object = portalObjectList[index];
            portalObjectList[index] = PortalObject.Create(root_object.portalInfo, root_object._transform.parent);
            ((Component) MonoBehaviourSingleton<InGameManager>.I).gameObject.AddComponent<PortalUnlockEvent>().AddPortal(portalObjectList[index]);
            GameSaveData.instance.newReleasePortals.Remove(portalObjectList[index].portalID);
            MonoBehaviourSingleton<MiniMap>.I.Detach((MonoBehaviour) root_object);
            Object.Destroy((Object) ((Component) root_object).gameObject);
            flag = true;
          }
        }
        if (flag)
          MonoBehaviourSingleton<FieldManager>.I.ResetPortalPointToIndex();
        this.m_coroutinePortal = (Coroutine) null;
        yield break;
      }
      yield return (object) null;
    }
    this.m_coroutinePortal = (Coroutine) null;
  }

  private bool IsDeleteCleardAnnounce()
  {
    return !TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge();
  }

  private bool IsDispCleardAnnounce()
  {
    return !MonoBehaviourSingleton<UIManager>.I.IsTransitioning() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop) && FieldManager.IsValidInGameNoBoss() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain";
  }

  public static bool IsInvalidClearInGame(DELIVERY_TYPE type, DIFFICULTY_MODE fieldMode)
  {
    if (type == DELIVERY_TYPE.ONCE && fieldMode == DIFFICULTY_MODE.HARD)
      return true;
    return type != DELIVERY_TYPE.ONCE && type != DELIVERY_TYPE.SUB_EVENT;
  }

  public bool IsDefeatFieldConditionType(DELIVERY_CONDITION_TYPE conditionType)
  {
    return new List<DELIVERY_CONDITION_TYPE>()
    {
      DELIVERY_CONDITION_TYPE.DEFEAT_FIELD_ENEMY_ID
    }.Contains(conditionType);
  }
}
