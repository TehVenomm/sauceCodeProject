// Decompiled with JetBrains decompiler
// Type: QuestArenaSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestArenaSelectList : QuestEventSelectList
{
  private int m_lastBGMId;
  private const string ARENA_FRAME_SPRITE = "RequestPlate_Arena";
  private List<Delivery> visibleDeliveryList = new List<Delivery>();
  private List<DeliveryTable.DeliveryData> notClearDevliveries = new List<DeliveryTable.DeliveryData>();
  private List<uint> timeAttackDeliveryIds = new List<uint>();
  private List<ArenaTable.ArenaData> arenaDataList = new List<ArenaTable.ArenaData>();
  private ArenaUserRecordModel.Param record;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "DeliveryRewardTable";
      yield return "FieldMapTable";
      yield return "ArenaTable";
    }
  }

  protected override bool showMap => false;

  public override void Initialize() => base.Initialize();

  protected override IEnumerator DoInitialize()
  {
    if (GameSection.GetEventData() == null)
    {
      bool is_recv_delivery = false;
      MonoBehaviourSingleton<QuestManager>.I.SendGetEventList((Action<bool>) (b => is_recv_delivery = true));
      while (!is_recv_delivery)
        yield return (object) null;
      Network.EventData arenaDataFromList = MonoBehaviourSingleton<QuestManager>.I.FindArenaDataFromList();
      this.eventData = arenaDataFromList;
      GameSection.SetEventData((object) arenaDataFromList);
      MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
    }
    if (this.eventData == null)
    {
      this.StartCoroutine(this.LoadDisableBanner());
    }
    else
    {
      if (MonoBehaviourSingleton<SoundManager>.IsValid())
      {
        this.m_lastBGMId = MonoBehaviourSingleton<SoundManager>.I.requestBGMID;
        SoundManager.RequestBGM(3);
      }
      if (MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking)
        yield return (object) this.StartCoroutine(this.SendGetMyRcord());
      this.StartCoroutine(base.DoInitialize());
    }
  }

  private IEnumerator LoadDisableBanner()
  {
    string eventBg = ResourceName.GetEventBG(10012200);
    Hash128 hash128 = new Hash128();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null))
      hash128 = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(eventBg));
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null) || ((Hash128) ref hash128).isValid)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_bg = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_BG, eventBg);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.SetTexture((Enum) QuestArenaSelectList.UI.TEX_EVENT_BG, (Texture) (lo_bg.loadedObject as Texture2D));
      lo_bg = (LoadObject) null;
    }
    this.EndInitialize();
  }

  public override void UpdateUI()
  {
    if (this.eventData == null)
    {
      this.SetActive((Enum) QuestArenaSelectList.UI.BTN_INFO, false);
      this.SetActive((Enum) QuestArenaSelectList.UI.LBL_SUB_TITLE, false);
      this.UpdateTitle();
      this.UpdateNoArenaTable();
    }
    else
    {
      this.CreateArenaList();
      this.CreateVisibleDeliveryList();
      base.UpdateUI();
      this.UpdateSubTitle();
      this.UpdateTitle();
    }
  }

  public override void StartSection()
  {
    base.StartSection();
    if (this.eventData == null)
      return;
    if (!this.IsPlayableVersion())
    {
      this.RequestEvent("SELECT_VERSION", (object) string.Format(this.sectionData.GetText("REQUIRE_HIGHER_VERSION"), (object) this.eventData.minVersion));
    }
    else
    {
      if (this.eventData.readPrologueStory || this.eventData.prologueStoryId <= 0)
        return;
      this.StartAutoPrologue();
    }
  }

  private bool IsPlayableVersion()
  {
    return this.eventData == null || this.eventData.IsPlayableWith(NetworkNative.getNativeVersionFromName());
  }

  private void StartAutoPrologue()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData("AUTO_PROLOGUE", (object) new object[4]
      {
        (object) this.eventData.prologueStoryId,
        (object) "",
        (object) "",
        (object) new EventData[2]
        {
          new EventData(GameSection.GetGoingHomeEvent(), (object) null),
          new EventData("ARENA_LIST", (object) this.eventData)
        }
      })
    });
  }

  private void OnQuery_AUTO_PROLOGUE()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(this.eventData.eventId, (Action<bool, Error>) ((success, error) =>
    {
      this.eventData.readPrologueStory = true;
      GameSection.ResumeEvent(success);
    }));
  }

  private IEnumerator SendGetMyRcord()
  {
    bool isFinishGetRecord = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetArenaUserRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, this.eventData.eventId, (Action<bool, ArenaUserRecordModel.Param>) ((b, result) =>
    {
      isFinishGetRecord = true;
      this.record = result;
    }));
    while (!isFinishGetRecord)
      yield return (object) null;
  }

  private void UpdateSubTitle()
  {
    this.SetActive((Enum) QuestArenaSelectList.UI.LBL_SUB_TITLE, false);
    this.SetLabelText((Enum) QuestArenaSelectList.UI.LBL_SUB_TITLE, this.eventData.name);
  }

  private void UpdateTitle()
  {
    string text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 27U);
    this.SetLabelText((Enum) QuestArenaSelectList.UI.LBL_LOCATION_NAME, text);
    this.SetLabelText((Enum) QuestArenaSelectList.UI.LBL_LOCATION_NAME_EFFECT, text);
  }

  protected override void UpdateTable()
  {
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    this._SorteliveryList();
    int item_num = this.notClearDevliveries.Count + this.clearedDeliveries.Count + 1;
    if (this.showStory)
      item_num += num1 + this.stories.Count;
    if (this.notClearDevliveries == null || item_num == 0)
    {
      this.SetActive((Enum) QuestArenaSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestArenaSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestArenaSelectList.UI.TBL_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestArenaSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestArenaSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestArenaSelectList.UI.TBL_DELIVERY_QUEST, true);
      int questStartIndex = 0;
      questStartIndex++;
      int completedStartIndex = this.notClearDevliveries.Count + questStartIndex;
      int borderIndex = completedStartIndex + this.clearedDeliveries.Count;
      int storyStartIndex = borderIndex;
      if (this.stories.Count > 0)
        ++storyStartIndex;
      Transform ctrl = this.GetCtrl((Enum) QuestArenaSelectList.UI.TBL_DELIVERY_QUEST);
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
      this.SetTable((Enum) QuestArenaSelectList.UI.TBL_DELIVERY_QUEST, "", item_num, false, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        Transform transform = (Transform) null;
        if (i >= storyStartIndex)
          return !this.HasChapterStory() || i == storyStartIndex || !isRenewalFlag ? this.Realizes("QuestEventStoryItem", parent) : (Transform) null;
        if (i >= borderIndex)
          transform = this.Realizes("QuestEventBorderItem", parent);
        else if (i >= questStartIndex)
          transform = this.Realizes("QuestRequestItemArena", parent);
        else if (i == 0)
          transform = this.Realizes("QuestArenaRequestItemToRanking", parent);
        return transform;
      }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        if (Object.op_Equality((Object) t, (Object) null))
          return;
        this.SetActive(t, true);
        if (i >= storyStartIndex)
          this.InitStory(i - storyStartIndex, t);
        else if (i < borderIndex)
        {
          if (i >= completedStartIndex)
            this.InitCompletedDelivery(i - completedStartIndex, t);
          else if (i >= questStartIndex)
            this.InitNormalDelivery(i - questStartIndex, t);
          else if (i == 0)
            this.InitGoToRankingButton(t);
        }
        if (i >= storyStartIndex || i == 0)
          return;
        this.SetSprite(t, (Enum) QuestArenaSelectList.UI.SPR_FRAME, "RequestPlate_Arena");
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestArenaSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
      this.RepositionTable();
    }
  }

  protected void UpdateNoArenaTable()
  {
    this.SetTable((Enum) QuestArenaSelectList.UI.TBL_DELIVERY_QUEST, "", 1, false, (Func<int, Transform, Transform>) ((i, parent) =>
    {
      Transform transform = (Transform) null;
      if (i == 0)
        transform = this.Realizes("QuestArenaRequestItemToRanking", parent);
      return transform;
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.SetActive(t, true);
      if (i != 0)
        return;
      this.InitGoToRankingButton(t);
    }));
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestArenaSelectList.UI.SCR_DELIVERY_QUEST)).enabled = false;
    this.RepositionTable();
  }

  private void CreateArenaList()
  {
    this.arenaDataList.Clear();
    for (int index = 0; index < this.deliveryInfo.Length; ++index)
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
      ArenaTable.ArenaData arenaData = deliveryTableData.GetArenaData();
      if (arenaData == null)
        Debug.LogError((object) $"{((Object) this).name} {deliveryTableData.name} : arenaDataが見つかりません");
      else
        this.arenaDataList.Add(arenaData);
    }
  }

  private void CreateVisibleDeliveryList()
  {
    this.visibleDeliveryList.Clear();
    int index = 0;
    for (int length = this.deliveryInfo.Length; index < length; ++index)
      this.visibleDeliveryList.Add(this.deliveryInfo[index]);
  }

  protected override List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
  {
    return this.CreateClearedDliveryList(ARENA_RANK.S);
  }

  private List<DeliveryTable.DeliveryData> CreateClearedDliveryList(ARENA_RANK borderRank)
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
          ArenaTable.ArenaData arenaData = deliveryTableData.GetArenaData();
          if (arenaData != null && arenaData.rank >= borderRank && deliveryTableData.GetConditionType() != DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
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
    }
    return clearedDliveryList;
  }

  protected override void InitStory(int index, Transform t)
  {
    bool flag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
    if (this.HasChapterStory() & flag)
    {
      base.InitStory(index, t);
    }
    else
    {
      this.SetEvent(t, "SELECT_RUSH_STORY", index);
      this.SetLabelText(t, (Enum) QuestArenaSelectList.UI.LBL_STORY_TITLE, this.stories[index].title);
    }
  }

  protected override void InitNormalDelivery(int index, Transform t)
  {
    DeliveryTable.DeliveryData notClearDevlivery = this.notClearDevliveries[index];
    if (this.timeAttackDeliveryIds.Contains(notClearDevlivery.id))
    {
      this.SetEvent(t, "SELECT_TIMEATTACK_RUSH", index);
      this.SetUpCompletedArenaListItem(t, notClearDevlivery);
      this.SetCompletedHaveCount(t, notClearDevlivery);
    }
    else
    {
      this.SetEvent(t, "SELECT_RUSH", index);
      if (notClearDevlivery.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
        this.SetUpArenaListItemRankUp(t, notClearDevlivery);
      else
        this.SetUpArenaListItem(t, notClearDevlivery);
    }
  }

  private void SetUpArenaListItem(Transform t, DeliveryTable.DeliveryData info)
  {
    QuestRequestItemArena requestItemArena = ((Component) t).GetComponent<QuestRequestItemArena>();
    if (Object.op_Equality((Object) requestItemArena, (Object) null))
      requestItemArena = ((Component) t).gameObject.AddComponent<QuestRequestItemArena>();
    requestItemArena.InitUI();
    requestItemArena.Setup(t, info);
  }

  private void SetUpArenaListItemRankUp(Transform t, DeliveryTable.DeliveryData info)
  {
    QuestRequestItemArenaRankUp requestItemArenaRankUp = ((Component) t).GetComponent<QuestRequestItemArenaRankUp>();
    if (Object.op_Equality((Object) requestItemArenaRankUp, (Object) null))
      requestItemArenaRankUp = ((Component) t).gameObject.AddComponent<QuestRequestItemArenaRankUp>();
    requestItemArenaRankUp.InitUI();
    requestItemArenaRankUp.Setup(t, info);
  }

  private void SetUpCompletedArenaListItem(Transform t, DeliveryTable.DeliveryData info)
  {
    QuestRequestItemArena requestItemArena = ((Component) t).GetComponent<QuestRequestItemArena>();
    if (Object.op_Equality((Object) requestItemArena, (Object) null))
      requestItemArena = ((Component) t).gameObject.AddComponent<QuestRequestItemArena>();
    requestItemArena.InitUI();
    requestItemArena.SetupComplete(t, info, this.record);
  }

  protected override void InitCompletedDelivery(int completedIndex, Transform t)
  {
    DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[completedIndex];
    this.SetEvent(t, "SELECT_COMPLETED_RUSH", completedIndex);
    if (clearedDelivery.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
    {
      this.SetUpArenaListItemRankUp(t, clearedDelivery);
      this.SetActive(t, (Enum) QuestArenaSelectList.UI.OBJ_REQUEST_COMPLETED, true);
    }
    else
      this.SetUpCompletedArenaListItem(t, clearedDelivery);
    this.SetCompletedHaveCount(t, clearedDelivery);
  }

  private void OnQuery_SELECT_RUSH()
  {
    DeliveryTable.DeliveryData dd = this.notClearDevliveries[(int) GameSection.GetEventData()];
    Delivery notClearDelivery = this.GetNotClearDelivery(dd.id);
    if (MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) dd.id))
    {
      this.changeToDeliveryClearEvent = true;
      bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
      bool enable_clear_event = dd.clearEventID > 0U;
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
            GameSection.ChangeStayEvent("RUSH_REWARD", (object) new object[2]
            {
              (object) (int) dd.id,
              (object) recv_reward
            });
          }
          else
            GameSection.ChangeStayEvent("CLEAR_EVENT", (object) new object[3]
            {
              (object) (int) dd.clearEventID,
              (object) (int) dd.id,
              (object) recv_reward
            });
        }
        else
          this.changeToDeliveryClearEvent = false;
        GameSection.ResumeEvent(is_success);
      }));
    }
    else if (dd.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
    {
      GameSection.SetEventData((object) new object[2]
      {
        (object) (int) dd.id,
        null
      });
    }
    else
    {
      ArenaTable.ArenaData arenaData = dd.GetArenaData();
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) arenaData.questIds[0]);
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
      GameSection.ChangeEvent("TO_ROOM", (object) dd);
    }
  }

  private void InitGoToRankingButton(Transform t)
  {
    this.SetEvent(t, "RANKING", (object) this.eventData);
  }

  private void OnQuery_RANKING()
  {
    if (GameSection.GetEventData() is Network.EventData)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("RANK", (object) null),
      new EventData("LAST", (object) new object[2]
      {
        (object) "null",
        (object) -1
      })
    });
  }

  private void OnQuery_SELECT_COMPLETED_RUSH()
  {
    DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[(int) GameSection.GetEventData()];
    if (clearedDelivery.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
    {
      GameSection.SetEventData((object) new object[3]
      {
        (object) (int) clearedDelivery.id,
        (object) new DeliveryRewardList(),
        (object) true
      });
    }
    else
    {
      ArenaTable.ArenaData arenaData = clearedDelivery.GetArenaData();
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) arenaData.questIds[0]);
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
      GameSection.ChangeEvent("TO_ROOM", (object) clearedDelivery);
    }
  }

  private void OnQuery_SELECT_TIMEATTACK_RUSH()
  {
    DeliveryTable.DeliveryData notClearDevlivery = this.notClearDevliveries[(int) GameSection.GetEventData()];
    ArenaTable.ArenaData arenaData = notClearDevlivery.GetArenaData();
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) arenaData.questIds[0]);
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
    GameSection.ChangeEvent("TO_ROOM", (object) notClearDevlivery);
  }

  private void OnQuery_SELECT_RUSH_STORY()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) this.stories[(int) GameSection.GetEventData()].id,
      (object) "",
      (object) "",
      (object) new EventData[2]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("ARENA_LIST", (object) this.eventData)
      }
    });
  }

  private void OnQuery_CLOSE()
  {
    if (this.m_lastBGMId <= 0)
      return;
    SoundManager.RequestBGM(this.m_lastBGMId);
  }

  private void OnQuery_SECTION_BACK()
  {
    if (this.m_lastBGMId <= 0)
      return;
    SoundManager.RequestBGM(this.m_lastBGMId);
  }

  private void _SorteliveryList()
  {
    this.notClearDevliveries.Clear();
    this.timeAttackDeliveryIds.Clear();
    for (int index = 0; index < this.visibleDeliveryList.Count; ++index)
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.visibleDeliveryList[index].dId);
      if (deliveryTableData != null)
        this.notClearDevliveries.Add(deliveryTableData);
    }
    for (int index = 0; index < this.clearedDeliveries.Count; ++index)
    {
      DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[index];
      if (clearedDelivery != null)
      {
        ArenaTable.ArenaData arenaData = clearedDelivery.GetArenaData();
        if (arenaData != null && arenaData.rank == ARENA_RANK.S)
        {
          this.notClearDevliveries.Add(clearedDelivery);
          this.clearedDeliveries.Remove(clearedDelivery);
          this.timeAttackDeliveryIds.Add(clearedDelivery.id);
          --index;
        }
      }
    }
    this.notClearDevliveries.Sort((IComparer<DeliveryTable.DeliveryData>) new QuestArenaSelectList.ArenaSort());
  }

  private Delivery GetNotClearDelivery(uint deliveryId)
  {
    return this.visibleDeliveryList.Find((Predicate<Delivery>) (d => (long) d.dId == (long) deliveryId));
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.Arena);

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
    SPR_FRAME,
    LBL_SUB_TITLE,
  }

  public class ArenaSort : IComparer<DeliveryTable.DeliveryData>
  {
    public int Compare(DeliveryTable.DeliveryData x, DeliveryTable.DeliveryData y)
    {
      bool flag1 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) x.id);
      bool flag2 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) y.id);
      if (flag1 == flag2)
        return y.displayOrder - x.displayOrder;
      return flag1 ? -1 : 1;
    }
  }
}
