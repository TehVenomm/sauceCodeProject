// Decompiled with JetBrains decompiler
// Type: QuestExploreList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestExploreList : GameSection
{
  private List<Network.EventData> eventList;
  private Dictionary<int, LoadObject> bannerTable;
  private static readonly float UPDATE_INTERVAL_SEC = 30f;
  private float nextUpdate = QuestExploreList.UPDATE_INTERVAL_SEC;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize() => this.StartCoroutine("DoInitialize");

  private IEnumerator DoInitialize()
  {
    bool is_recv_delivery = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetExploreList((Action<bool>) (b => is_recv_delivery = true));
    while (!is_recv_delivery)
      yield return (object) null;
    List<Network.EventData> eventDataList = new List<Network.EventData>((IEnumerable<Network.EventData>) MonoBehaviourSingleton<QuestManager>.I.eventList);
    this.eventList = new List<Network.EventData>();
    for (int index = 0; index < eventDataList.Count; ++index)
    {
      if (eventDataList[index].eventType == 4)
        this.eventList.Add(eventDataList[index]);
    }
    for (int index = 0; index < eventDataList.Count; ++index)
    {
      if (eventDataList[index].eventType == 12)
        this.eventList.Add(eventDataList[index]);
    }
    this.RemoveEndedEvents();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    this.bannerTable = new Dictionary<int, LoadObject>(this.eventList.Count);
    for (int index = 0; index < this.eventList.Count; ++index)
    {
      Network.EventData eventData = this.eventList[index];
      if (!this.bannerTable.ContainsKey(eventData.bannerId))
      {
        string eventBanner = ResourceName.GetEventBanner(eventData.bannerId);
        LoadObject loadObject = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_ICON, eventBanner);
        this.bannerTable.Add(eventData.bannerId, loadObject);
      }
    }
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    base.Initialize();
  }

  private void RemoveEndedEvents()
  {
    if (this.eventList == null)
      return;
    this.eventList.RemoveAll((Predicate<Network.EventData>) (e => e.HasEndDate() && e.GetRest() < 0));
  }

  public override void UpdateUI() => this.UpdateEventList();

  protected void UpdateEventList()
  {
    this.RemoveEndedEvents();
    if (this.eventList == null || this.eventList.Count == 0)
    {
      this.SetActive((Enum) QuestExploreList.UI.STR_EVENT_NON_LIST, true);
    }
    else
    {
      this.SetActive((Enum) QuestExploreList.UI.STR_EVENT_NON_LIST, false);
      this.SetDynamicList((Enum) QuestExploreList.UI.GRD_EVENT_QUEST, "QuestEventListSelectItem", this.eventList.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        Network.EventData eventData = this.eventList[i];
        LoadObject loadObject;
        if (this.bannerTable.TryGetValue(eventData.bannerId, out loadObject))
        {
          Texture2D loadedObject = loadObject.loadedObject as Texture2D;
          if (Object.op_Inequality((Object) loadedObject, (Object) null))
          {
            Transform ctrl = this.FindCtrl(t, (Enum) QuestExploreList.UI.TEX_EVENT_BANNER);
            this.SetActive(ctrl, true);
            this.SetTexture(ctrl, (Texture) loadedObject);
            this.SetActive(t, (Enum) QuestExploreList.UI.LBL_NO_BANNER, false);
          }
          else
          {
            this.SetActive(t, (Enum) QuestExploreList.UI.TEX_EVENT_BANNER, false);
            this.SetActive(t, (Enum) QuestExploreList.UI.LBL_NO_BANNER, true);
            string name = eventData.name;
            this.SetLabelText(t, (Enum) QuestExploreList.UI.LBL_NO_BANNER, name);
          }
        }
        if (!string.IsNullOrEmpty(eventData.endDate.date))
        {
          Transform ctrl1 = this.FindCtrl(t, (Enum) QuestExploreList.UI.LBL_LEFT);
          this.SetActive(ctrl1, true);
          this.SetLabelText(ctrl1, StringTable.Get(STRING_CATEGORY.TIME, 4U));
          Transform ctrl2 = this.FindCtrl(t, (Enum) QuestExploreList.UI.LBL_LEFT_TIME);
          this.SetActive(ctrl2, true);
          this.SetLabelText(ctrl2, UIUtility.TimeFormatWithUnit(eventData.GetRest()));
        }
        else
        {
          this.SetActive(t, (Enum) QuestExploreList.UI.LBL_LEFT, false);
          this.SetActive(t, (Enum) QuestExploreList.UI.LBL_LEFT_TIME, false);
        }
        this.SetEvent(t, "SELECT_EXPLORE", (object) eventData);
        Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
        bool flag = eventData.IsPlayableWith(nativeVersionFromName);
        bool is_visible1 = this.IsClearedEvent(eventData) & flag;
        bool is_visible2 = !is_visible1 && !eventData.readPrologueStory;
        this.SetActive(t, (Enum) QuestExploreList.UI.SPR_NEW, is_visible2);
        this.SetActive(t, (Enum) QuestExploreList.UI.SPR_CLEARED, is_visible1);
        this.SetBadge(this.FindCtrl(t, (Enum) QuestExploreList.UI.TEX_EVENT_BANNER), MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(eventData.eventId), (SpriteAlignment) 1, 16 /*0x10*/, -3);
      }));
    }
  }

  private bool IsClearedEvent(Network.EventData eventData)
  {
    return MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(eventData.eventId);
  }

  private void Update()
  {
    this.nextUpdate -= Time.deltaTime;
    if ((double) this.nextUpdate >= 0.0)
      return;
    this.RefreshUI();
    this.nextUpdate = QuestExploreList.UPDATE_INTERVAL_SEC;
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
      return;
    this.RefreshUI();
    this.nextUpdate = QuestExploreList.UPDATE_INTERVAL_SEC;
  }

  private void OnQuery_SELECT_EXPLORE()
  {
    Network.EventData ev = GameSection.GetEventData() as Network.EventData;
    if (ev == null)
      return;
    if (ev.HasEndDate() && ev.GetRest() < 0)
    {
      GameSection.ChangeEvent("SELECT_ENDED");
    }
    else
    {
      Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
      if (!ev.IsPlayableWith(nativeVersionFromName))
      {
        GameSection.ChangeEvent("SELECT_VERSION", (object) string.Format(this.sectionData.GetText("REQUIRE_HIGHER_VERSION"), (object) ev.minVersion));
      }
      else
      {
        if (!ev.readPrologueStory)
        {
          GameSection.StayEvent();
          MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, (Action<bool, Error>) ((success, error) =>
          {
            if (success)
            {
              if (ev.prologueStoryId > 0 && this.sectionData.GetEventData("STORY") != null)
              {
                EventData[] eventDataArray = new EventData[3]
                {
                  new EventData(GameSection.GetGoingHomeEvent(), (object) null),
                  new EventData("EXPLORE", (object) null),
                  new EventData("SELECT_EXPLORE", (object) ev.eventId)
                };
                GameSection.ChangeStayEvent("STORY", (object) new object[4]
                {
                  (object) ev.prologueStoryId,
                  (object) "",
                  (object) "",
                  (object) eventDataArray
                });
              }
              ev.readPrologueStory = true;
            }
            if (ev.eventType == 12)
              GameSection.ChangeStayEvent("SELECT_RUSH", (object) ev);
            GameSection.ResumeEvent(true);
          }));
        }
        if (ev.eventType != 12)
          return;
        GameSection.ChangeEvent("SELECT_RUSH", (object) ev);
      }
    }
  }

  private void OnCloseDialog_QuestEventEndedDialog() => this.RefreshUI();

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    if (!(event_name == "SELECT_EXPLORE") || !(event_data is int))
      return base.CheckAutoEvent(event_name, event_data);
    if (this.eventList != null)
    {
      int event_id = (int) event_data;
      Network.EventData _data = this.eventList.Find((Predicate<Network.EventData>) (e => e.eventId == event_id));
      if (_data != null)
        return new EventData(event_name, (object) _data);
    }
    return new EventData("NONE", (object) null);
  }

  protected enum UI
  {
    SCR_EVENT_QUEST,
    GRD_EVENT_QUEST,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
    OBJ_NPC,
    TEX_EVENT_BANNER,
    LBL_NO_BANNER,
    LBL_LEFT,
    LBL_LEFT_TIME,
    STR_EVENT_NON_LIST,
    BTN_EVENT,
    OBJ_FRAME,
    SPR_BG_FRAME,
    SPR_CLEARED,
    SPR_NEW,
  }
}
