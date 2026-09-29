// Decompiled with JetBrains decompiler
// Type: QuestSeriesArenaEventList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class QuestSeriesArenaEventList : QuestEventSelectList
{
  private const int SERIES_ARENA_BGM = 134;
  private static int m_lastBGMId;
  private Dictionary<int, LoadObject> bannerTable;
  protected List<EventListData> seriesArenaEventList = new List<EventListData>();
  protected EventListData seriesArenaTopData;

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
    if (this.eventData == null)
    {
      bool is_recv_delivery = false;
      MonoBehaviourSingleton<QuestManager>.I.SendGetEventList((Action<bool>) (b => is_recv_delivery = true));
      while (!is_recv_delivery)
        yield return (object) null;
      is_recv_delivery = false;
      MonoBehaviourSingleton<DeliveryManager>.I.SendEventList((Action<bool>) (b => is_recv_delivery = true));
      while (!is_recv_delivery)
        yield return (object) null;
      EventListData seriesArenaTopData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
      this.eventData = this.ChoiceEventData(seriesArenaTopData);
      GameSection.SetEventData((object) seriesArenaTopData);
    }
    if (MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 3)
    {
      QuestSeriesArenaEventList.m_lastBGMId = MonoBehaviourSingleton<SoundManager>.I.playingBGMID;
      SoundManager.RequestBGM(134);
    }
    this.seriesArenaTopData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
    yield return (object) this.StartCoroutine(this.LoadSeriesArenaTopBanner());
    this.GetDeliveryList();
    this.EndInitialize();
  }

  protected IEnumerator LoadSeriesArenaTopBanner()
  {
    string resourceName = ResourceName.GetEventBG(this.seriesArenaTopData.bannerId);
    Hash128 hash = new Hash128();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null))
      hash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(resourceName));
    Utility.CreateGameObjectAndComponent("TheaterModeTable", ((Component) this).gameObject.transform);
    while (MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
      yield return (object) null;
    if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null) || ((Hash128) ref hash).isValid)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_bg = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_BG, resourceName);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.SetTexture((Enum) QuestSeriesArenaEventList.UI.TEX_EVENT_BG, (Texture) (lo_bg.loadedObject as Texture2D));
      lo_bg = (LoadObject) null;
    }
  }

  protected IEnumerator LoadBanner(Transform t, int bannerId)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject obj = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_ICON, ResourceName.GetEventBanner(bannerId));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Texture2D loadedObject = obj.loadedObject as Texture2D;
    if (Object.op_Inequality((Object) loadedObject, (Object) null))
    {
      Transform ctrl = this.FindCtrl(t, (Enum) QuestSeriesArenaEventList.UI.TEX_EVENT_BANNER);
      this.SetTexture(ctrl, (Texture) loadedObject);
      this.SetActive(ctrl, true);
    }
  }

  protected void UpdateNoArenaTable()
  {
    this.SetTable((Enum) QuestSeriesArenaEventList.UI.TBL_DELIVERY_QUEST, "", 1, false, (Func<int, Transform, Transform>) ((i, parent) =>
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
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestSeriesArenaEventList.UI.SCR_DELIVERY_QUEST)).enabled = false;
    this.RepositionTable();
  }

  public override void UpdateUI()
  {
    this.CreateSeriesArenaList();
    if (this.eventData == null)
      return;
    if (this.seriesArenaEventList.Count == 0 && this.eventData.enableRanking)
      this.UpdateNoArenaTable();
    else
      base.UpdateUI();
  }

  public override void StartSection()
  {
    base.StartSection();
    if (this.eventData == null)
      return;
    if (this.seriesArenaEventList.Count == 0 && this.eventData.enableRanking)
      this.UpdateNoArenaTable();
    else if (!this.IsPlayableVersion())
    {
      this.RequestEvent("SELECT_VERSION", (object) string.Format(this.sectionData.GetText("REQUIRE_HIGHER_VERSION"), (object) this.seriesArenaTopData.minVersion));
    }
    else
    {
      if (!GameSaveData.instance.IsTutorialSeriesArena())
        return;
      this.RequestEvent("HOW_TO");
    }
  }

  private bool IsPlayableVersion()
  {
    return this.seriesArenaTopData.IsPlayableWith(NetworkNative.getNativeVersionFromName());
  }

  protected override void UpdateTable()
  {
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    int count = this.seriesArenaEventList.Count;
    if (this.eventData.enableRanking)
      ++count;
    if (this.showStory)
      count += num1 + this.stories.Count;
    int questStartIndex = 0;
    if (this.eventData.enableRanking)
      questStartIndex++;
    int borderIndex = this.seriesArenaEventList.Count + questStartIndex;
    int storyStartIndex = borderIndex;
    if (this.stories.Count > 0)
      ++storyStartIndex;
    Transform ctrl = this.GetCtrl((Enum) QuestSeriesArenaEventList.UI.TBL_DELIVERY_QUEST);
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
    this.SetTable((Enum) QuestSeriesArenaEventList.UI.TBL_DELIVERY_QUEST, "", count, false, (Func<int, Transform, Transform>) ((i, parent) =>
    {
      Transform transform = (Transform) null;
      if (i >= storyStartIndex)
        return !this.HasChapterStory() || i == storyStartIndex || !isRenewalFlag ? this.Realizes("QuestEventStoryItem", parent) : (Transform) null;
      if (i >= borderIndex)
        transform = this.Realizes("QuestEventBorderItem", parent);
      else if (i >= questStartIndex)
        transform = this.Realizes("QuestEventListItemSeriesArena", parent);
      else if (i == 0)
        transform = this.Realizes("QuestArenaRequestItemToRanking", parent);
      return transform;
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
        {
          this.InitNormalDelivery(i - questStartIndex, t);
        }
        else
        {
          if (i != 0)
            return;
          this.InitGoToRankingButton(t);
        }
      }
    }));
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestSeriesArenaEventList.UI.SCR_DELIVERY_QUEST)).enabled = true;
    this.RepositionTable();
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
      this.SetEvent(t, "SELECT_SERIES_ARENA_STORY", index);
      this.SetLabelText(t, (Enum) QuestSeriesArenaEventList.UI.LBL_STORY_TITLE, this.stories[index].title);
    }
  }

  private void OnQuery_SELECT_SERIES_ARENA_STORY()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) this.stories[(int) GameSection.GetEventData()].id,
      (object) "",
      (object) "",
      (object) new EventData[3]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("EVENT_COUNTER"),
        new EventData("SELECT_SERIES_ARENA")
      }
    });
  }

  protected void CreateSeriesArenaList()
  {
    this.seriesArenaEventList = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaDataList();
  }

  protected override void InitNormalDelivery(int index, Transform t)
  {
    EventListData seriesArenaEvent = this.seriesArenaEventList[index];
    this.SetEvent(t, "SELECT_SERIES_ARENA", seriesArenaEvent.eventId);
    this.SetUpSeriesArenaListItem(t, seriesArenaEvent);
  }

  private void OnQuery_SELECT_SERIES_ARENA()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId((int) GameSection.GetEventData());
  }

  protected void InitGoToRankingButton(Transform t) => this.SetEventName(t, "RANKING");

  private void SetUpSeriesArenaListItem(Transform t, EventListData info)
  {
    int bannerId = info.bannerId;
    this.StartCoroutine(this.LoadBanner(t, bannerId));
    this.SetActive(t, (Enum) QuestSeriesArenaEventList.UI.SPR_ICON_NEW, info.leftBadgeEnum == BADGE_1_CATEGORY.NEW_EVENT);
    this.SetActive(t, (Enum) QuestSeriesArenaEventList.UI.SPR_ICON_EVENTREWARD, info.rightBadgeEnum == BADGE_2_CATEGORY.EVENT_REWARD);
    bool is_visible = MonoBehaviourSingleton<QuestManager>.I.CheckEventMissionAllClear(info.eventId);
    this.SetActive(t, (Enum) QuestSeriesArenaEventList.UI.SPR_MISSION_CROWN_OFF, !is_visible);
    this.SetActive(t, (Enum) QuestSeriesArenaEventList.UI.SPR_MISSION_CROWN_ON, is_visible);
    StringBuilder stringBuilder = new StringBuilder();
    if (info.rightBadgeEnum == BADGE_2_CATEGORY.DELIVERY_NUM)
    {
      stringBuilder.Append("[000000]");
      stringBuilder.Append(info.rightValue);
    }
    else if (info.leftBadgeEnum == BADGE_1_CATEGORY.CLEARED)
    {
      stringBuilder.Append("[000000]");
      stringBuilder.Append("全依頼クリア");
    }
    RARITY_TYPE rarity = Singleton<QuestTable>.I.GetEventQuestData(info.eventId).rarity;
    ResourceLoad.LoadWithSetUITexture(((Component) this.FindCtrl(t, (Enum) QuestSeriesArenaEventList.UI.TEX_ICON)).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(rarity));
    this.SetLabelText(t, (Enum) QuestSeriesArenaEventList.UI.LBL_RIGHT_VALUE, stringBuilder.ToString());
    this.SetSupportEncoding(t, (Enum) QuestSeriesArenaEventList.UI.LBL_RIGHT_VALUE, true);
    this.SetActive(t, (Enum) QuestSeriesArenaEventList.UI.LBL_RIGHT_VALUE, true);
    this.SetActive(t, (Enum) QuestSeriesArenaEventList.UI.SPR_ICON_EXISTPRESENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(info.eventId) > 0);
  }

  protected void OnQuery_CLOSE()
  {
    if (QuestSeriesArenaEventList.m_lastBGMId <= 0)
      return;
    SoundManager.RequestBGM(QuestSeriesArenaEventList.m_lastBGMId);
  }

  private void OnQuery_SECTION_BACK()
  {
    if (QuestSeriesArenaEventList.m_lastBGMId <= 0)
      return;
    SoundManager.RequestBGM(QuestSeriesArenaEventList.m_lastBGMId);
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.SeriesArena);

  protected virtual void OnQuery_TO_UNIQUE_STATUS()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(0);
  }

  private Network.EventData ChoiceEventData(EventListData eld)
  {
    if (eld == null)
      return (Network.EventData) eld;
    return !MonoBehaviourSingleton<QuestManager>.IsValid() ? (Network.EventData) eld : MonoBehaviourSingleton<QuestManager>.I._GetEventData(eld.eventId) ?? (Network.EventData) eld;
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
    SPR_FRAME,
    TEX_EVENT_BANNER,
    LBL_RIGHT_VALUE,
    SPR_ICON_EXISTPRESENT,
    SPR_ICON_NEW,
    SPR_ICON_EVENTREWARD,
    TEX_ICON,
    SPR_MISSION_CROWN_ON,
    SPR_MISSION_CROWN_OFF,
    LBL_STORY_TITLE,
  }
}
