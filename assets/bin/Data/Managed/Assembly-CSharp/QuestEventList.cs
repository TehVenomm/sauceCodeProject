// Decompiled with JetBrains decompiler
// Type: QuestEventList
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
public class QuestEventList : GameSection
{
  private readonly int kBannerId_Explore = 10000003;
  private readonly int kBannerId_Rush = 10000004;
  private readonly int kBannerId_Wave = 10000005;
  private readonly int kRedRestTime = 345600;
  private readonly string kBackHomeEvent = "SELECT_BACKHOME";
  private readonly string kToAreaEvent = "TO_AREA_EVENT";
  private readonly float UPDATE_INTERVAL_SEC = 30f;
  private readonly float kItemLeftPosX = -113f;
  private readonly float kItemRightPosX = 113f;
  private readonly float kItemNormalHeight = 110f;
  private readonly float kItemBBoxHeight = 278f;
  private readonly float kItemSBoxHeight = 140f;
  private List<EventListData> eventList;
  private Dictionary<int, LoadObject> bannerTable;
  protected bool isInGame;
  protected bool firstUpdate = true;
  protected bool[] hasNewEvent = new bool[3];
  private QuestEventList.eDispTab dispTab;
  protected QuestEventList.UI eventNewSprite;
  protected QuestEventList.UI presentNewSprite;
  protected QuestEventList.UI eventOn;
  protected QuestEventList.UI presentOn;
  private QuestCarnivalPointModel.Param currentCarnivalData;
  private QuestEventList.CARNIVAL_STATUS carnivalStatus;
  private readonly string[] SPR_RANKING_NUMBER = new string[10]
  {
    "RankingNumber_0",
    "RankingNumber_1",
    "RankingNumber_2",
    "RankingNumber_3",
    "RankingNumber_4",
    "RankingNumber_5",
    "RankingNumber_6",
    "RankingNumber_7",
    "RankingNumber_8",
    "RankingNumber_9"
  };
  protected QuestEventList.UI[] rankNumbers = new QuestEventList.UI[6]
  {
    QuestEventList.UI.SPR_RANK_0,
    QuestEventList.UI.SPR_RANK_1,
    QuestEventList.UI.SPR_RANK_2,
    QuestEventList.UI.SPR_RANK_3,
    QuestEventList.UI.SPR_RANK_4,
    QuestEventList.UI.SPR_RANK_5
  };
  private Coroutine checkUpdate;
  private bool isCarnival;

  public override void Initialize() => this.StartCoroutine("DoInitialize");

  private IEnumerator DoInitialize()
  {
    bool is_recv_delivery = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetEventList((Action<bool>) (b => is_recv_delivery = true));
    while (!is_recv_delivery)
      yield return (object) null;
    is_recv_delivery = false;
    MonoBehaviourSingleton<DeliveryManager>.I.SendEventList((Action<bool>) (b => is_recv_delivery = true));
    while (!is_recv_delivery)
      yield return (object) null;
    this.UpdateEventListData();
    this.isCarnival = this.IsCarnival();
    this.UpdateTabUISettings();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    this.bannerTable = new Dictionary<int, LoadObject>(this.eventList.Count);
    for (int index = 0; index < this.eventList.Count; ++index)
    {
      EventListData e = this.eventList[index];
      int bannerId = this.GetBannerId(e);
      if (!this.bannerTable.ContainsKey(bannerId))
      {
        LoadObject loadObject = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_ICON, this.GetBannerName(e, bannerId));
        this.bannerTable.Add(bannerId, loadObject);
      }
    }
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (this.isCarnival)
    {
      yield return (object) this.StartCoroutine(this.GetCurrentCarnivalStatus());
      int banner_id = 10017100;
      if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.carnivalEventId > 0)
        banner_id = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId;
      LoadObject eventBG = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_BG, ResourceName.GetEventBG(banner_id));
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.SetTexture((Enum) QuestEventList.UI.TEX_CARNIVAL_BG, (Texture) (eventBG.loadedObject as Texture2D));
      eventBG = (LoadObject) null;
      this.SetCarnivalPointInfo();
    }
    base.Initialize();
    this.ChangeTab();
  }

  private void SetCarnivalPointInfo()
  {
    this.SetActive((Enum) QuestEventList.UI.OBJ_CARNIVAL_STATUS, this.currentCarnivalData != null);
    if (this.currentCarnivalData == null)
      return;
    QuestCarnivalPointModel.Param currentCarnivalData = this.currentCarnivalData;
    this.carnivalStatus = (QuestEventList.CARNIVAL_STATUS) currentCarnivalData.status;
    string text1 = "";
    string text2 = "";
    bool flag = currentCarnivalData.rank > 0;
    bool is_visible;
    string text3;
    string text4;
    if (currentCarnivalData.status == 1)
    {
      is_visible = flag;
      if (flag)
        this.SetSpriteRank(currentCarnivalData.rank);
      else
        text1 = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 42U);
      text3 = currentCarnivalData.point.ToString("N0") + " pt";
      text4 = currentCarnivalData.pointForNextClass.ToString("N0") + " pt";
      this.SetActive((Enum) QuestEventList.UI.SPR_CARNIVAL_END, false);
    }
    else if (currentCarnivalData.status == 2)
    {
      is_visible = false;
      text3 = currentCarnivalData.point.ToString("N0") + " pt";
      text4 = "-------";
      text1 = "-------";
      text2 = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 38U);
      this.SetActive((Enum) QuestEventList.UI.SPR_CARNIVAL_END, true);
    }
    else
    {
      is_visible = flag;
      if (flag)
        this.SetSpriteRank(currentCarnivalData.rank);
      else
        text1 = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 42U);
      text3 = currentCarnivalData.point.ToString("N0") + " pt";
      text4 = "-------";
      text2 = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 39U);
      this.SetActive((Enum) QuestEventList.UI.SPR_CARNIVAL_END, true);
    }
    this.SetActive((Enum) QuestEventList.UI.LBL_CURRENT_RANK, !is_visible);
    this.SetActive((Enum) QuestEventList.UI.OBJ_RANK_ROOT, is_visible);
    this.SetLabelText((Enum) QuestEventList.UI.LBL_CURRENT_POINT, text3);
    this.SetLabelText((Enum) QuestEventList.UI.LBL_NEXT_POINT, text4);
    this.SetLabelText((Enum) QuestEventList.UI.LBL_CURRENT_RANK, text1);
    this.SetLabelText((Enum) QuestEventList.UI.LBL_CARNIVAL_STATUS, text2);
  }

  private void SetSpriteRank(int value)
  {
    value = Mathf.Clamp(value, 1, 999999);
    value = Mathf.Min(999999, value);
    string str = value.ToString();
    int length = str.Length;
    for (int index1 = 0; index1 < 6; ++index1)
    {
      int num = length - 1;
      if (index1 > num)
      {
        this.SetActive((Enum) this.rankNumbers[index1], false);
      }
      else
      {
        this.SetActive((Enum) this.rankNumbers[index1], true);
        int index2 = int.Parse(str[index1].ToString());
        this.SetSprite((Enum) this.rankNumbers[num - index1], this.SPR_RANKING_NUMBER[index2]);
      }
    }
  }

  private IEnumerator GetCurrentCarnivalStatus()
  {
    bool isRequest = true;
    Protocol.Send<QuestCarnivalPointModel.RequestSendForm, QuestCarnivalPointModel>(QuestCarnivalPointModel.URL, new QuestCarnivalPointModel.RequestSendForm()
    {
      eid = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId
    }, (Action<QuestCarnivalPointModel>) (result =>
    {
      isRequest = false;
      this.currentCarnivalData = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  private void UpdateTabUISettings()
  {
    this.eventNewSprite = QuestEventList.UI.SPR_EVENT_NEW;
    this.presentNewSprite = QuestEventList.UI.SPR_PRESENT_NEW;
    this.eventOn = QuestEventList.UI.OBJ_LEFT_ON;
    this.presentOn = QuestEventList.UI.OBJ_RIGHT_ON;
    if (!this.isCarnival)
      return;
    this.eventNewSprite = QuestEventList.UI.SPR_THREE_EVENT_NEW;
    this.presentNewSprite = QuestEventList.UI.SPR_THREE_PRESENT_NEW;
    this.eventOn = QuestEventList.UI.OBJ_THREE_LEFT_ON;
    this.presentOn = QuestEventList.UI.OBJ_THREE_CENTER_ON;
  }

  private void UpdateEventListData()
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.I.isUpdateEventListData)
      return;
    this.eventList = (List<EventListData>) null;
    this.eventList = new List<EventListData>((IEnumerable<EventListData>) MonoBehaviourSingleton<DeliveryManager>.I.eventListData);
    this.eventList.Sort((Comparison<EventListData>) ((a, b) =>
    {
      if (a.place != b.place)
        return a.place - b.place;
      int num1 = !a.enableEvent || !MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(a.eventId) ? 0 : 1;
      int num2 = !b.enableEvent || !MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(b.eventId) ? 0 : 1;
      return num1 != num2 ? num1 - num2 : a.orderNo - b.orderNo;
    }));
    MonoBehaviourSingleton<DeliveryManager>.I.isUpdateEventListData = false;
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
      return;
    this.RefreshUI();
  }

  public override void UpdateUI()
  {
    this.UpdateEventListData();
    this.UpdateEventList();
    this.UpdateCheck();
  }

  protected void UpdateEventList()
  {
    bool is_visible1 = this.IsActive((Enum) QuestEventList.UI.OBJ_QUEST_LIST_ROOT);
    bool is_visible2 = this.IsActive((Enum) QuestEventList.UI.OBJ_CARNIVAL_LIST_ROOT);
    this.SetActive((Enum) QuestEventList.UI.OBJ_QUEST_LIST_ROOT, true);
    this.SetActive((Enum) QuestEventList.UI.OBJ_CARNIVAL_LIST_ROOT, true);
    this._UpdateEvent();
    this._UpdatePresent();
    if (this.isCarnival)
      this._UpdateCarnival();
    if (this.firstUpdate)
    {
      this.firstUpdate = false;
      if (this.isCarnival && this.carnivalStatus == QuestEventList.CARNIVAL_STATUS.DOING)
        this.dispTab = QuestEventList.eDispTab.Carnival;
      else if (this.hasNewEvent[1])
        this.dispTab = QuestEventList.eDispTab.Present;
      this.ChangeTab();
      this.DispEventNonListLabel();
    }
    else
    {
      this.SetActive((Enum) QuestEventList.UI.OBJ_QUEST_LIST_ROOT, is_visible1);
      this.SetActive((Enum) QuestEventList.UI.OBJ_CARNIVAL_LIST_ROOT, is_visible2);
    }
  }

  private void _UpdateEvent()
  {
    if (!this.IsActive((Enum) QuestEventList.UI.OBJ_QUEST_LIST_ROOT))
      return;
    this.hasNewEvent[0] = false;
    EVENT_DISPLAY_PLACE lastPlace = EVENT_DISPLAY_PLACE.NONE;
    float offsetY = 0.0f;
    this.SetSimpleContent((Enum) QuestEventList.UI.OBJ_EVENT_ROOT, "", this.eventList.Count, false, (Func<int, bool>) (i =>
    {
      EventListData eventListData = this.eventList[i];
      return eventListData.placeEnum != EVENT_DISPLAY_PLACE.PRESENT && eventListData.placeEnum != 0;
    }), (Func<int, Transform, Transform>) ((i, parent) =>
    {
      switch (this.eventList[i].placeEnum)
      {
        case EVENT_DISPLAY_PLACE.NONE:
        case EVENT_DISPLAY_PLACE.PRESENT:
        case EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL:
        case EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX:
        case EVENT_DISPLAY_PLACE.SERIES_ARENA:
          return (Transform) null;
        case EVENT_DISPLAY_PLACE.LEFT_EVENT:
        case EVENT_DISPLAY_PLACE.RIGHT_EVENT:
          return this.Realizes("QuestEventListItemBBox", parent);
        case EVENT_DISPLAY_PLACE.LEFT_CONTENTS:
        case EVENT_DISPLAY_PLACE.RIGHT_CONTENTS:
          return this.Realizes("QuestEventListItemSBox", parent);
        default:
          return this.Realizes("QuestEventListItemNormal", parent);
      }
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (Object.op_Equality((Object) t, (Object) null))
        return;
      EventListData e = this.eventList[i];
      this._SetupItem(QuestEventList.eDispTab.Event, t, e);
      if (lastPlace == EVENT_DISPLAY_PLACE.LEFT_EVENT && e.placeEnum != EVENT_DISPLAY_PLACE.RIGHT_EVENT)
      {
        if ((double) offsetY == 0.0)
          offsetY -= this.kItemBBoxHeight * 0.5f;
        else
          offsetY -= this.kItemBBoxHeight;
      }
      else if (lastPlace == EVENT_DISPLAY_PLACE.LEFT_CONTENTS && e.placeEnum != EVENT_DISPLAY_PLACE.RIGHT_CONTENTS)
      {
        if ((double) offsetY == 0.0)
          offsetY -= this.kItemSBoxHeight * 0.5f;
        else
          offsetY -= this.kItemSBoxHeight;
      }
      Vector3 localPosition = t.localPosition;
      localPosition.x = e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_EVENT || e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS ? this.kItemLeftPosX : (e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_EVENT || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_CONTENTS ? this.kItemRightPosX : 0.0f);
      float num1;
      float num2;
      switch (e.placeEnum)
      {
        case EVENT_DISPLAY_PLACE.LEFT_EVENT:
        case EVENT_DISPLAY_PLACE.RIGHT_EVENT:
          num1 = this.kItemBBoxHeight;
          num2 = this.kItemBBoxHeight * 0.5f;
          break;
        case EVENT_DISPLAY_PLACE.LEFT_CONTENTS:
        case EVENT_DISPLAY_PLACE.RIGHT_CONTENTS:
          num1 = this.kItemSBoxHeight;
          num2 = this.kItemSBoxHeight * 0.5f;
          break;
        default:
          num1 = this.kItemNormalHeight;
          num2 = this.kItemNormalHeight * 0.5f;
          break;
      }
      localPosition.y = (double) offsetY != 0.0 ? offsetY - num2 : 0.0f;
      t.localPosition = localPosition;
      EVENT_DISPLAY_PLACE placeEnum = e.placeEnum;
      if (placeEnum <= EVENT_DISPLAY_PLACE.RIGHT_EVENT)
      {
        if (placeEnum != EVENT_DISPLAY_PLACE.LEFT_EVENT)
        {
          if (placeEnum == EVENT_DISPLAY_PLACE.RIGHT_EVENT)
            ;
        }
        else
          goto label_22;
      }
      else if (placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS)
        goto label_22;
      if ((double) offsetY == 0.0)
        offsetY -= num2;
      else
        offsetY -= num1;
label_22:
      lastPlace = e.placeEnum;
    }));
    this.SetActive((Enum) this.eventNewSprite, this.hasNewEvent[0]);
  }

  private void _UpdatePresent()
  {
    this.hasNewEvent[1] = false;
    this.SetDynamicList((Enum) QuestEventList.UI.GRD_EVENT_QUEST, "QuestEventListItemNormal", this.eventList.Count, false, (Func<int, bool>) (i => this.eventList[i].placeEnum == EVENT_DISPLAY_PLACE.PRESENT), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (Object.op_Equality((Object) t, (Object) null))
        return;
      this._SetupItem(QuestEventList.eDispTab.Present, t, this.eventList[i]);
    }));
    this.SetActive((Enum) this.presentNewSprite, this.hasNewEvent[1]);
  }

  private void _UpdateCarnival()
  {
    this.hasNewEvent[2] = false;
    Transform ctrl1 = this.GetCtrl((Enum) QuestEventList.UI.TBL_CARNIVAL_QUEST);
    if (Object.op_Implicit((Object) ctrl1))
    {
      int num = 0;
      for (int childCount = ctrl1.childCount; num < childCount; ++num)
      {
        Transform child = ctrl1.GetChild(0);
        child.parent = (Transform) null;
        Object.Destroy((Object) ((Component) child).gameObject);
      }
    }
    List<EventListData> carnivalEvents = new List<EventListData>();
    for (int index = 0; index < this.eventList.Count; ++index)
    {
      if (this.IsCarnivalEvent(this.eventList[index]))
        carnivalEvents.Add(this.eventList[index]);
    }
    int itemNum = carnivalEvents.Count;
    itemNum++;
    itemNum++;
    bool existSmallBoxEmpty = false;
    bool isLeftSmallBox = true;
    Transform targetPairBox = (Transform) null;
    if (this.carnivalStatus != QuestEventList.CARNIVAL_STATUS.DOING)
      itemNum = 1;
    this.SetTable((Enum) QuestEventList.UI.TBL_CARNIVAL_QUEST, "", itemNum, false, (Func<int, Transform, Transform>) ((i, parent) =>
    {
      Transform transform = (Transform) null;
      if (i == 0)
        transform = this.Realizes("CarnivalRankingCheckItem", parent);
      else if (i == itemNum - 1)
      {
        transform = this.Realizes("NormalQuestItem", parent);
      }
      else
      {
        switch (carnivalEvents[i - 1].placeEnum)
        {
          case EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL:
            transform = this.Realizes("QuestEventListItemNormal", parent);
            break;
          case EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX:
            if (!existSmallBoxEmpty)
            {
              transform = this.Realizes("QuestEventListItemPairSmallBox", parent);
              targetPairBox = transform;
            }
            else
              transform = targetPairBox;
            existSmallBoxEmpty = !existSmallBoxEmpty;
            break;
        }
      }
      return transform;
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (Object.op_Equality((Object) t, (Object) null))
        return;
      this.SetActive(t, true);
      if (i == 0)
        this.InitRankingCheckButton(t);
      else if (i == itemNum - 1)
      {
        this.InitGachaQuestButton(t);
      }
      else
      {
        EventListData e = carnivalEvents[i - 1];
        switch (e.placeEnum)
        {
          case EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL:
            this._SetupItem(QuestEventList.eDispTab.Carnival, t, e);
            break;
          case EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX:
            Transform ctrl2;
            if (isLeftSmallBox)
            {
              this.SetActive(t, (Enum) QuestEventList.UI.OBJ_RIGHT_SMALL_BOX, false);
              ctrl2 = this.FindCtrl(t, (Enum) QuestEventList.UI.OBJ_LEFT_SMALL_BOX);
            }
            else
            {
              this.SetActive(t, (Enum) QuestEventList.UI.OBJ_RIGHT_SMALL_BOX, true);
              ctrl2 = this.FindCtrl(t, (Enum) QuestEventList.UI.OBJ_RIGHT_SMALL_BOX);
            }
            this._SetupItem(QuestEventList.eDispTab.Carnival, ctrl2, e);
            isLeftSmallBox = !isLeftSmallBox;
            break;
        }
      }
    }));
    UIScrollView component = this.GetComponent<UIScrollView>((Enum) QuestEventList.UI.SCR_CARNIVAL_QUEST);
    ((Behaviour) component).enabled = true;
    component.ResetPosition();
    this.RepositionCarnivalTable();
    this.SetActive((Enum) QuestEventList.UI.SPR_CARNIVAL_NEW, this.hasNewEvent[2]);
  }

  private void RepositionCarnivalTable()
  {
    UITable component = this.GetComponent<UITable>((Enum) QuestEventList.UI.TBL_CARNIVAL_QUEST);
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

  private void InitRankingCheckButton(Transform parent)
  {
    this.SetEvent(parent, "TO_CARNIVAL_RANKING", (object) null);
  }

  private void InitGachaQuestButton(Transform parent)
  {
    this.SetEvent(parent, "TO_GACHA_QUEST_COUNTER", (object) null);
  }

  private void _SetupItem(QuestEventList.eDispTab tab, Transform t, EventListData e)
  {
    LoadObject loadObject;
    if (this.bannerTable.TryGetValue(this.GetBannerId(e), out loadObject))
    {
      Texture2D loadedObject = loadObject.loadedObject as Texture2D;
      if (Object.op_Inequality((Object) loadedObject, (Object) null))
      {
        Transform ctrl = this.FindCtrl(t, (Enum) QuestEventList.UI.TEX_EVENT_BANNER);
        this.SetTexture(ctrl, (Texture) loadedObject);
        this.SetActive(ctrl, true);
      }
    }
    if (!e.enableEvent && e.eventTypeEnum != EVENT_TYPE.ARENA)
      this.SetEvent(t, "SELECT_DISABLE", (object) e);
    else if (e.linkName.Equals(this.kToAreaEvent))
    {
      this.SetEvent(t, this.kToAreaEvent, (object) e);
    }
    else
    {
      switch (e.eventTypeEnum)
      {
        case EVENT_TYPE.EXPLORE:
        case EVENT_TYPE.RUSH:
        case EVENT_TYPE.WAVE:
        case EVENT_TYPE.TRIAL:
          this.SetEvent(t, this.isInGame ? this.kBackHomeEvent : this.ChangeEventTypeToSelectEventName(e.eventTypeEnum), (object) this.ChoiceEventData(e));
          break;
        case EVENT_TYPE.ARENA:
          this.SetArenaItem(t, e);
          break;
        case EVENT_TYPE.SERIES_ARENA_POINT_CLEAR:
          this.SetSeriesArenaItem(t, e);
          break;
        default:
          this.SetEvent(t, this.ChangeEventTypeToSelectEventName(e.eventTypeEnum), (object) this.ChoiceEventData(e));
          break;
      }
    }
    if (e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_EVENT)
      this.hasNewEvent[(int) tab] = true;
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_NEW, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_EVENT);
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_NEWDELIVERY, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DELIVERY);
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_NEWSTORY, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_STORY);
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_CLEARED, e.leftBadgeEnum == BADGE_1_CATEGORY.CLEARED);
    if (e.eventTypeEnum == EVENT_TYPE.EXPLORE || e.eventTypeEnum == EVENT_TYPE.WAVE)
    {
      if (this.IsDispTitle(e))
      {
        this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, true);
        this.SetLabelText(t, (Enum) QuestEventList.UI.LBL_TITLE, e.name);
      }
      else
        this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, false);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_ADDDIFFICULT, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DIFFICULT_DELIVERY);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_ADDDIFFICULT_RUSH, false);
    }
    else if (e.eventTypeEnum == EVENT_TYPE.RUSH)
    {
      if (this.IsDispTitle(e))
      {
        this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, true);
        this.SetLabelText(t, (Enum) QuestEventList.UI.LBL_TITLE, e.name);
      }
      else
        this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, false);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_ADDDIFFICULT, false);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_ADDDIFFICULT_RUSH, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DIFFICULT_DELIVERY);
    }
    else
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_ADDDIFFICULT, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DIFFICULT_DELIVERY);
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_EVENTREWARD, e.rightBadgeEnum == BADGE_2_CATEGORY.EVENT_REWARD);
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_RESULTANNOUNCEMENT, e.rightBadgeEnum == BADGE_2_CATEGORY.RESULT_ANNOUNCEMENT);
    this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_BOSSAPPEAR, e.rightBadgeEnum == BADGE_2_CATEGORY.BOSS_APPEAR);
    UIWigetCrossFade component = ((Component) t).GetComponent<UIWigetCrossFade>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      if (e.leftBadgeEnum != BADGE_1_CATEGORY.NONE && (e.rightBadgeEnum == BADGE_2_CATEGORY.EVENT_REWARD || e.rightBadgeEnum == BADGE_2_CATEGORY.RESULT_ANNOUNCEMENT))
        component.Play();
      else
        component.Reset();
    }
    if (e.linkName.Equals(this.kToAreaEvent))
    {
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_STATE, false);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_BANNER_DISABLE, false);
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_RIGHT_VALUE, false);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_EXISTPRESENT, false);
    }
    else if (e.HasEndDate() && e.GetRest() < 0)
    {
      if (e.rightBadgeEnum == BADGE_2_CATEGORY.NOW_AGGREGATING)
        this.SetLabelText(t, (Enum) QuestEventList.UI.LBL_STATE, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 38U));
      else
        this.SetLabelText(t, (Enum) QuestEventList.UI.LBL_STATE, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 39U));
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_STATE, true);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_BANNER_DISABLE, true);
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_RIGHT_VALUE, false);
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_EXISTPRESENT, false);
    }
    else
    {
      this.SetActive(t, (Enum) QuestEventList.UI.SPR_BANNER_DISABLE, false);
      StringBuilder stringBuilder = new StringBuilder();
      if (e.rightBadgeEnum == BADGE_2_CATEGORY.DELIVERY_NUM)
      {
        stringBuilder.Append("[000000]");
        stringBuilder.Append(e.rightValue);
        stringBuilder.Append(" / [-]");
      }
      else if (e.leftBadgeEnum == BADGE_1_CATEGORY.CLEARED)
      {
        stringBuilder.Append("[000000]");
        stringBuilder.Append(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 43U));
        stringBuilder.Append(" / [-]");
      }
      stringBuilder.Append(e.GetRest() < this.kRedRestTime ? "[FF0000]" : "[000000]");
      stringBuilder.Append(UIUtility.TimeFormatWithUnit(e.GetRest()));
      stringBuilder.Append(" " + StringTable.Get(STRING_CATEGORY.TIME, 4U));
      stringBuilder.Append("[-]");
      this.SetLabelText(t, (Enum) QuestEventList.UI.LBL_RIGHT_VALUE, stringBuilder.ToString());
      this.SetSupportEncoding(t, (Enum) QuestEventList.UI.LBL_RIGHT_VALUE, true);
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_RIGHT_VALUE, e.GetRest() > 0);
      if (e.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR)
        this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_EXISTPRESENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableSeriesArenaEventDeliveryNum() > 0);
      else
        this.SetActive(t, (Enum) QuestEventList.UI.SPR_ICON_EXISTPRESENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(e.eventId) > 0);
    }
  }

  private void SetArenaItem(Transform t, EventListData e)
  {
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)
    {
      this.SetEvent(t, "SELECT_ARENA", (object) this.ChoiceEventData(e));
      this.SetLabelText(t, (Enum) QuestEventList.UI.LBL_TITLE, e.name);
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, true);
    }
    else
    {
      this.SetEvent(t, "SELECT_DISABLE_ARENA", (object) this.ChoiceEventData(e));
      this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, false);
    }
    if (!this.isInGame)
      return;
    this.SetEvent(t, this.kBackHomeEvent, (object) e);
  }

  private void OnQuery_SELECT_SERIES_ARENA() => this._CheckEvent();

  private void SetSeriesArenaItem(Transform t, EventListData e)
  {
    this.SetActive(t, (Enum) QuestEventList.UI.LBL_TITLE, false);
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 100)
      this.SetEvent(t, "SELECT_SERIES_ARENA", (object) this.ChoiceEventData(e));
    else
      this.SetEvent(t, "SELECT_NOT_LEVEL_SERIES_ARENA", (object) this.ChoiceEventData(e));
    if (!this.isInGame)
      return;
    this.SetEvent(t, this.kBackHomeEvent, (object) e);
  }

  protected void UpdateCheck()
  {
    if (this.checkUpdate != null)
      this.StopCoroutine(this.checkUpdate);
    List<EventListData> all = this.eventList.FindAll((Predicate<EventListData>) (x => x.HasEndDate()));
    if (all == null || all.Count == 0)
    {
      this.checkUpdate = this.StartCoroutine(this.DoCheck(86400f));
    }
    else
    {
      int num = all.Min<EventListData>((Func<EventListData, int>) (data => data.GetRest()));
      this.checkUpdate = this.StartCoroutine(this.DoCheck((double) num < (double) this.UPDATE_INTERVAL_SEC ? this.UPDATE_INTERVAL_SEC : (float) num));
    }
  }

  private IEnumerator DoCheck(float updateTime)
  {
    yield return (object) new WaitForSeconds(updateTime);
    this.RefreshUI();
  }

  private void ChangeTab()
  {
    bool is_visible = this.dispTab == QuestEventList.eDispTab.Carnival;
    this.SetActive((Enum) QuestEventList.UI.OBJ_QUEST_LIST_ROOT, !is_visible);
    this.SetActive((Enum) QuestEventList.UI.OBJ_CARNIVAL_LIST_ROOT, is_visible);
    this.SetActive((Enum) QuestEventList.UI.OBJ_TWO_TAB, !this.isCarnival);
    this.SetActive((Enum) QuestEventList.UI.OBJ_THREE_TAB, this.isCarnival);
    this.SetActive((Enum) this.eventOn, this.dispTab == QuestEventList.eDispTab.Event);
    this.SetActive((Enum) this.presentOn, this.dispTab == QuestEventList.eDispTab.Present);
    if (this.isCarnival)
      this.SetActive((Enum) QuestEventList.UI.OBJ_THREE_RIGHT_ON, this.dispTab == QuestEventList.eDispTab.Carnival);
    this.SetActive((Enum) QuestEventList.UI.OBJ_EVENT_ROOT, this.dispTab == QuestEventList.eDispTab.Event);
    this.SetActive((Enum) QuestEventList.UI.GRD_EVENT_QUEST, this.dispTab == QuestEventList.eDispTab.Present);
    this.DispEventNonListLabel();
    UIScrollView component = ((Component) this.GetCtrl((Enum) QuestEventList.UI.SCR_EVENT_QUEST)).GetComponent<UIScrollView>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    ((Behaviour) component).enabled = true;
    component.ResetPosition();
  }

  protected virtual bool IsCarnival() => MonoBehaviourSingleton<QuestManager>.I.carnivalEventId > 0;

  private void DispEventNonListLabel()
  {
    Transform transform = this.dispTab == QuestEventList.eDispTab.Event ? this.GetCtrl((Enum) QuestEventList.UI.OBJ_EVENT_ROOT) : this.GetCtrl((Enum) QuestEventList.UI.GRD_EVENT_QUEST);
    this.SetActive((Enum) QuestEventList.UI.STR_EVENT_NON_LIST, !Object.op_Inequality((Object) transform, (Object) null) || transform.childCount <= 0);
  }

  private bool IsLoadEBI2(EventListData e)
  {
    return e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_EVENT || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_EVENT || e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_CONTENTS || e.placeEnum == EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX;
  }

  private bool IsDispTitle(EventListData e)
  {
    return !this.IsCarnivalEvent(e) && (e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_CONTENTS);
  }

  private bool IsCarnivalEvent(EventListData e)
  {
    return e.placeEnum == EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX || e.placeEnum == EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL;
  }

  private int GetBannerId(EventListData e)
  {
    switch (e.eventTypeEnum)
    {
      case EVENT_TYPE.EXPLORE:
        if (this.IsLoadEBI2(e) && !this.IsCarnivalEvent(e))
          return this.kBannerId_Explore;
        break;
      case EVENT_TYPE.RUSH:
        if (this.IsLoadEBI2(e) && !this.IsCarnivalEvent(e))
          return this.kBannerId_Rush;
        break;
      case EVENT_TYPE.ARENA:
        return (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50 ? e.bannerId : 10012201;
      case EVENT_TYPE.WAVE:
        if (this.IsLoadEBI2(e) && !this.IsCarnivalEvent(e))
          return this.kBannerId_Wave;
        break;
    }
    return e.bannerId;
  }

  private string GetBannerName(EventListData e, int bannerId)
  {
    bool flag = this.IsLoadEBI2(e);
    return !e.enableEvent && e.eventTypeEnum != EVENT_TYPE.ARENA ? (!flag ? ResourceName.GetEventBanner(bannerId, "_close") : ResourceName.GetEventBannerVer2(bannerId, "_close")) : (!flag ? ResourceName.GetEventBanner(bannerId) : ResourceName.GetEventBannerVer2(bannerId));
  }

  private Network.EventData ChoiceEventData(EventListData eld)
  {
    if (eld == null)
      return (Network.EventData) eld;
    return !MonoBehaviourSingleton<QuestManager>.IsValid() ? (Network.EventData) eld : MonoBehaviourSingleton<QuestManager>.I._GetEventData(eld.eventId) ?? (Network.EventData) eld;
  }

  private void OnCloseDialog_QuestEventEndedDialog() => this.RefreshUI();

  private void OnQuery_TAB_EVENT()
  {
    if (this.dispTab == QuestEventList.eDispTab.Event)
      return;
    this.dispTab = QuestEventList.eDispTab.Event;
    this.ChangeTab();
  }

  private void OnQuery_TAB_PRESENT()
  {
    if (this.dispTab == QuestEventList.eDispTab.Present)
      return;
    this.dispTab = QuestEventList.eDispTab.Present;
    this.ChangeTab();
  }

  private void OnQuery_TAB_CARNIVAL()
  {
    if (this.dispTab == QuestEventList.eDispTab.Carnival)
      return;
    this.dispTab = QuestEventList.eDispTab.Carnival;
    this.ChangeTab();
  }

  private void OnQuery_SELECT() => this._CheckEvent();

  private void OnQuery_SELECT_DISABLE()
  {
    EventListData ev = GameSection.GetEventData() as EventListData;
    Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => ev.preEventId == e.eventId)).First<Network.EventData>();
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) ev.preDeliveryId);
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 5U), (object) ev.name, (object) eventData.name, (object) deliveryTableData.name)), (Action<string>) (ret => { }));
  }

  private void OnQuery_InGameQuestBackHome_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("EVENT_COUNTER", (object) null),
      new EventData("TAB_EVENT", (object) null)
    });
  }

  private void OnQuery_TO_AREA_EVENT()
  {
    EventData[] event_datas;
    if (this.isInGame)
      event_datas = new EventData[3]
      {
        new EventData("[BACK]", (object) null),
        new EventData("QUEST_WINDOW", (object) null),
        new EventData("SELECT_NORMAL", (object) null)
      };
    else
      event_datas = new EventData[3]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("QUEST_COUNTER", (object) null),
        new EventData("SELECT_NORMAL", (object) null)
      };
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  private void OnQuery_SELECT_EXPLORE() => this._CheckEvent();

  private void OnQuery_SELECT_RUSH() => this._CheckEvent();

  private void OnQuery_SELECT_WAVE() => this._CheckEvent();

  private void OnQuery_SELECT_TRIAL() => this._CheckEvent();

  private void OnQuery_SELECT_ARENA() => this._CheckEvent();

  private void OnQuery_CARNIVAL_HELP()
  {
    GameSection.SetEventData((object) WebViewManager.Carnival);
  }

  private void OnQuery_TO_CARNIVAL_RANKING()
  {
    GameSection.SetEventData((object) this.currentCarnivalData.rankingURL);
  }

  private void OnQuery_CARNIVAL_INFO()
  {
    if (this.currentCarnivalData == null)
      return;
    string linkName = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId.ToString();
    if (!string.IsNullOrEmpty(this.currentCarnivalData.linkName))
      linkName = this.currentCarnivalData.linkName;
    GameSection.SetEventData((object) string.Format(WebViewManager.NewsWithLinkParamFormat, (object) linkName));
  }

  private void _CheckEvent()
  {
    Network.EventData ev = GameSection.GetEventData() as Network.EventData;
    if (ev == null)
      return;
    if (ev.HasEndDate() && ev.GetRest() < 0)
    {
      if (ev.eventTypeEnum == EVENT_TYPE.ARENA || ev.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR)
        GameSection.SetEventData((object) null);
      else
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
        if (ev.readPrologueStory)
          return;
        GameSection.StayEvent();
        MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, (Action<bool, Error>) ((success, error) =>
        {
          if (success)
          {
            if (ev.prologueStoryId > 0 && this.sectionData.GetEventData("STORY") != null)
            {
              string goingHomeEvent = GameSection.GetGoingHomeEvent();
              EventData[] eventDataArray;
              if (ev.eventTypeEnum == EVENT_TYPE.ARENA)
                eventDataArray = new EventData[4]
                {
                  new EventData(goingHomeEvent, (object) null),
                  new EventData("EVENT_COUNTER", (object) null),
                  this.dispTab == QuestEventList.eDispTab.Event ? new EventData("TAB_EVENT", (object) null) : new EventData("TAB_PRESENT", (object) null),
                  new EventData("SELECT_ARENA", (object) ev)
                };
              else
                eventDataArray = new EventData[4]
                {
                  new EventData(goingHomeEvent, (object) null),
                  new EventData("EVENT_COUNTER", (object) null),
                  this.dispTab == QuestEventList.eDispTab.Event ? new EventData("TAB_EVENT", (object) null) : new EventData("TAB_PRESENT", (object) null),
                  new EventData(this.ChangeEventTypeToSelectEventName(ev.eventTypeEnum), (object) ev.eventId)
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
          GameSection.ResumeEvent(true);
        }));
      }
    }
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    if ((event_name == "SELECT" || event_name == "SELECT_EXPLORE" || event_name == "SELECT_RUSH" || event_name == "SELECT_WAVE" ? 1 : (event_name == "SELECT_TRIAL" ? 1 : 0)) == 0 || !(event_data is int))
      return base.CheckAutoEvent(event_name, event_data);
    if (this.eventList != null)
    {
      int event_id = (int) event_data;
      EventListData _data = this.eventList.Find((Predicate<EventListData>) (e => e.eventId == event_id));
      if (_data != null && _data.enableEvent)
      {
        event_name = this.ChangeEventTypeToSelectEventName((EVENT_TYPE) _data.eventType);
        return new EventData(event_name, (object) _data);
      }
    }
    return new EventData("NONE", (object) null);
  }

  private string ChangeEventTypeToSelectEventName(EVENT_TYPE eventType)
  {
    switch (eventType)
    {
      case EVENT_TYPE.EXPLORE:
        return "SELECT_EXPLORE";
      case EVENT_TYPE.RUSH:
        return "SELECT_RUSH";
      case EVENT_TYPE.WAVE:
        return "SELECT_WAVE";
      case EVENT_TYPE.TRIAL:
        return "SELECT_TRIAL";
      case EVENT_TYPE.SERIES_ARENA_POINT_CLEAR:
        return "SELECT_SERIES_ARENA";
      default:
        return "SELECT";
    }
  }

  protected enum UI
  {
    OBJ_QUEST_LIST_ROOT,
    OBJ_CARNIVAL_LIST_ROOT,
    SCR_EVENT_QUEST,
    GRD_EVENT_QUEST,
    OBJ_EVENT_ROOT,
    OBJ_TWO_TAB,
    OBJ_LEFT_ON,
    OBJ_RIGHT_ON,
    SPR_EVENT_NEW,
    SPR_PRESENT_NEW,
    SPR_CARNIVAL_NEW,
    OBJ_THREE_TAB,
    SPR_THREE_EVENT_NEW,
    SPR_THREE_PRESENT_NEW,
    OBJ_THREE_LEFT_ON,
    OBJ_THREE_CENTER_ON,
    OBJ_THREE_RIGHT_ON,
    STR_EVENT_NON_LIST,
    BTN_EVENT,
    OBJ_FRAME,
    SPR_BG_FRAME,
    TEX_EVENT_BANNER,
    LBL_RIGHT_VALUE,
    SPR_BANNER_DISABLE,
    SPR_ICON_EXISTPRESENT,
    SPR_ICON_NEW,
    SPR_ICON_NEWDELIVERY,
    SPR_ICON_NEWSTORY,
    SPR_ICON_ADDDIFFICULT,
    SPR_ICON_ADDDIFFICULT_RUSH,
    SPR_ICON_CLEARED,
    SPR_ICON_EVENTREWARD,
    SPR_ICON_RESULTANNOUNCEMENT,
    SPR_ICON_BOSSAPPEAR,
    LBL_TITLE,
    LBL_STATE,
    TEX_CARNIVAL_BG,
    TBL_CARNIVAL_QUEST,
    OBJ_LEFT_SMALL_BOX,
    OBJ_RIGHT_SMALL_BOX,
    SCR_CARNIVAL_QUEST,
    LBL_CURRENT_POINT,
    LBL_CURRENT_RANK,
    LBL_NEXT_POINT,
    LBL_CARNIVAL_STATUS,
    OBJ_CARNIVAL_STATUS,
    SPR_CARNIVAL_END,
    OBJ_RANK_ROOT,
    SPR_RANK_0,
    SPR_RANK_1,
    SPR_RANK_2,
    SPR_RANK_3,
    SPR_RANK_4,
    SPR_RANK_5,
  }

  private enum eDispTab
  {
    Event,
    Present,
    Carnival,
    Max,
  }

  private enum CARNIVAL_STATUS
  {
    NONE,
    DOING,
    TOTALING,
    END,
  }
}
