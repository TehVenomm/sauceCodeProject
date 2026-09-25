// Decompiled with JetBrains decompiler
// Type: QuestExploreSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestExploreSelectList : QuestEventSelectList
{
  private QuestExplorePointModel.Param currentData;
  private const string EXPLORE_FRAME_SPRITE = "RequestPlate_Explore";
  private List<DeliveryTable.DeliveryData> deliveryList = new List<DeliveryTable.DeliveryData>();
  private List<DeliveryTable.DeliveryData> allDeliveryList = new List<DeliveryTable.DeliveryData>();

  protected override bool showMap => false;

  protected override IEnumerator DoInitialize()
  {
    this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_HOST_LIMIT, this.eventData.hostCountLimit.ToString());
    this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_HOST_RESET_TIME, StringTable.Format(STRING_CATEGORY.EXPLORE, 2U, (object) QuestSpecialSelect.GetRemainTimeText(this.GetRemainTime())));
    yield return (object) this.StartCoroutine(this.GetCurrentStatus());
    yield return (object) this.StartCoroutine(base.DoInitialize());
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_LOCATION_NAME, this.eventData.name);
    this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_LOCATION_NAME_EFFECT, this.eventData.name);
    if (this.eventData.eventId == 99001001)
    {
      this.SetActive((Enum) QuestExploreSelectList.UI.BTN_INFO, false);
      this.SetActive(this.GetCtrl((Enum) QuestExploreSelectList.UI.OBJ_IMAGE), (Enum) QuestExploreSelectList.UI.BTN_INFO_NEW, true);
      this.SetActive((Enum) QuestExploreSelectList.UI.OBJ_CURRENT_STATUS, false);
    }
    else
    {
      this.SetActive((Enum) QuestExploreSelectList.UI.BTN_INFO, !string.IsNullOrEmpty(this.eventData.linkName));
      this.SetActive(this.GetCtrl((Enum) QuestExploreSelectList.UI.OBJ_IMAGE), (Enum) QuestExploreSelectList.UI.BTN_INFO_NEW, false);
      this.SetActive((Enum) QuestExploreSelectList.UI.OBJ_CURRENT_STATUS, true);
    }
    this.title.Update();
    this.titleEffect.Update();
    this.stories.Clear();
    if (this.eventData.prologueStoryId > 0)
      this.stories.Add(new QuestEventSelectList.Story(this.eventData.prologueStoryId, this.eventData.prologueTitle));
    this.clearedDeliveries = this.CreateClearedDliveryList();
    this.UpdateList();
    this.UpdateAnchors();
    this.isResetUI = false;
  }

  protected override void UpdateTable()
  {
    this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_CURRENT_POINT, StringTable.Format(STRING_CATEGORY.EXPLORE, 0U, (object) this.currentData.point));
    this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_POINT_TITLE, StringTable.Get(STRING_CATEGORY.EXPLORE, 1U));
    if (this.currentData.reward != null && this.currentData.reward.reward.Count > 0)
    {
      this.SetActive((Enum) QuestExploreSelectList.UI.OBJ_NEXT_REWARD_ROOT, true);
      QuestExplorePointModel.Param.Reward reward = this.currentData.reward.reward[0];
      ((Collider) ((Component) ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, this.GetCtrl((Enum) QuestExploreSelectList.UI.OBJ_NEXT_REWARD_ICON_POS), reward.num)).GetComponent<BoxCollider>()).enabled = false;
      string text = Utility.TrimText(Utility.GetRewardName((REWARD_TYPE) reward.type, (uint) reward.itemId), ((Component) this.GetCtrl((Enum) QuestExploreSelectList.UI.LBL_NEXT_REWARD_NAME)).GetComponent<UILabel>());
      this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_NEXT_POINT, StringTable.Format(STRING_CATEGORY.EXPLORE, 0U, (object) this.currentData.reward.point));
      this.SetLabelText((Enum) QuestExploreSelectList.UI.LBL_NEXT_REWARD_NAME, text);
    }
    else
      this.SetActive((Enum) QuestExploreSelectList.UI.OBJ_NEXT_REWARD_ROOT, false);
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    int item_num = this.deliveryInfo.Length + this.clearedDeliveries.Count + 1;
    if (this.showStory)
      item_num += num1 + this.stories.Count;
    if (this.deliveryInfo == null || item_num == 0)
    {
      this.SetActive((Enum) QuestExploreSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestExploreSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestExploreSelectList.UI.TBL_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestExploreSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestExploreSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestExploreSelectList.UI.TBL_DELIVERY_QUEST, true);
      this.SortDeliveryList();
      int questStartIndex = 0;
      questStartIndex++;
      int borderIndex = questStartIndex + this.deliveryInfo.Length + this.clearedDeliveries.Count;
      int storyStartIndex = borderIndex;
      if (this.stories.Count > 0)
        ++storyStartIndex;
      Transform ctrl = this.GetCtrl((Enum) QuestExploreSelectList.UI.TBL_DELIVERY_QUEST);
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
      this.SetTable((Enum) QuestExploreSelectList.UI.TBL_DELIVERY_QUEST, "", item_num, false, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        if (i < storyStartIndex)
          return i < borderIndex ? (i < questStartIndex ? (i != 0 ? this.Realizes("QuestEventBorderItem", parent) : this.Realizes("QuestExploreRequestItemToSearch", parent)) : (this.allDeliveryList[i - questStartIndex].type != DELIVERY_TYPE.SUB_EVENT ? this.Realizes("QuestRequestItemExplore", parent) : this.Realizes("QuestRequestItem", parent))) : this.Realizes("QuestEventBorderItem", parent);
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
          {
            DeliveryTable.DeliveryData allDelivery = this.allDeliveryList[i - questStartIndex];
            if (this.clearedDeliveries.Contains(allDelivery))
              this.InitCompletedDelivery(this.clearedDeliveries.IndexOf(allDelivery), t);
            else
              this.InitNormalDelivery(this.deliveryList.IndexOf(allDelivery), t);
          }
          else
          {
            if (i != 0)
              return;
            this.InitGoToSearchButton(t);
          }
        }
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestExploreSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
      this.RepositionTable();
    }
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
      this.SetEvent(t, "SELECT_EXPLORE_STORY", index);
      this.SetLabelText(t, (Enum) QuestExploreSelectList.UI.LBL_STORY_TITLE, this.stories[index].title);
    }
  }

  protected override void InitNormalDelivery(int index, Transform t)
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
    this.SetupDeliveryListItem(t, deliveryTableData);
    this.SetDifficultySprite(t, deliveryTableData);
    if (deliveryTableData.type == DELIVERY_TYPE.SUB_EVENT)
    {
      this.SetEvent(t, "SELECT_DELIVERY", index);
    }
    else
    {
      this.SetEvent(t, "SELECT_EXPLORE", index);
      this.SetSprite(t, (Enum) QuestExploreSelectList.UI.SPR_FRAME, "RequestPlate_Explore");
    }
  }

  protected override void InitCompletedDelivery(int completedIndex, Transform t)
  {
    DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[completedIndex];
    if (clearedDelivery.type == DELIVERY_TYPE.SUB_EVENT)
    {
      base.InitCompletedDelivery(completedIndex, t);
    }
    else
    {
      this.SetEvent(t, "SELECT_COMPLETED_EXPLORE", completedIndex);
      this.SetupDeliveryListItem(t, clearedDelivery);
      this.SetDifficultySprite(t, clearedDelivery);
      this.SetActive(t, (Enum) QuestExploreSelectList.UI.OBJ_REQUEST_COMPLETED, true);
      this.SetCompletedHaveCount(t, clearedDelivery);
      this.SetSprite(t, (Enum) QuestExploreSelectList.UI.SPR_FRAME, "RequestPlate_Explore");
    }
  }

  private void OnQuery_SELECT_EXPLORE()
  {
    int eventData = (int) GameSection.GetEventData();
    int num = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(this.deliveryInfo[eventData].dId) ? 1 : 0;
    int delivery_id = this.deliveryInfo[eventData].dId;
    if (num != 0)
    {
      DeliveryTable.DeliveryData table = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[eventData].dId);
      this.changeToDeliveryClearEvent = true;
      bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
      bool enable_clear_event = table.clearEventID > 0U;
      GameSection.StayEvent();
      MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
      MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(this.deliveryInfo[eventData].uId, enable_clear_event, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) =>
      {
        if (is_success)
        {
          List<FieldMapTable.PortalTableData> relationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint) delivery_id);
          for (int index = 0; index < relationPortalData.Count; ++index)
            GameSaveData.instance.newReleasePortals.Add(relationPortalData[index].portalID);
          if (is_tutorial)
            TutorialStep.isSendFirstRewardComplete = true;
          if (!enable_clear_event)
          {
            MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
            GameSection.ChangeStayEvent("EXPLORE_REWARD", (object) new object[2]
            {
              (object) delivery_id,
              (object) recv_reward
            });
          }
          else
            GameSection.ChangeStayEvent("CLEAR_EVENT", (object) new object[3]
            {
              (object) (int) table.clearEventID,
              (object) delivery_id,
              (object) recv_reward
            });
        }
        else
          this.changeToDeliveryClearEvent = false;
        GameSection.ResumeEvent(is_success);
      }));
    }
    else if (MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll().Where<AbilityItemInfo>((Func<AbilityItemInfo, bool>) (x => x.equipUniqueId == 0UL)).Count<AbilityItemInfo>() >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem)
      GameSection.ChangeEvent("LIMIT_ABILITY_ITEM");
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) delivery_id,
        null
      });
  }

  private void OnQuery_SELECT_COMPLETED_EXPLORE()
  {
    if (MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll().Where<AbilityItemInfo>((Func<AbilityItemInfo, bool>) (x => x.equipUniqueId == 0UL)).Count<AbilityItemInfo>() >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem)
      GameSection.ChangeEvent("LIMIT_ABILITY_ITEM");
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) (int) this.clearedDeliveries[(int) GameSection.GetEventData()].id,
        (object) new DeliveryRewardList(),
        (object) true
      });
  }

  private void OnQuery_SELECT_EXPLORE_STORY()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) this.stories[(int) GameSection.GetEventData()].id,
      (object) "",
      (object) "",
      (object) new EventData[3]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("EXPLORE", (object) null),
        new EventData("SELECT_EXPLORE", (object) this.eventData)
      }
    });
  }

  public TimeSpan GetRemainTime()
  {
    DateTime now = TimeManager.GetNow();
    TimeSpan timeSpan1 = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_1);
    DateTime dateTime1 = now.Date.Add(timeSpan1);
    if (dateTime1 > now)
      return dateTime1 - now;
    TimeSpan timeSpan2 = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_2);
    DateTime dateTime2 = now.Date.Add(timeSpan2);
    if (dateTime2 > now)
      return dateTime2 - now;
    TimeSpan timeSpan3 = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_3);
    DateTime dateTime3 = now.Date.Add(timeSpan3);
    if (dateTime3 > now)
      return dateTime3 - now;
    TimeSpan timeSpan4 = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_1);
    DateTime dateTime4 = now.Date.Add(timeSpan4).Add(TimeSpan.FromDays(1.0));
    if (dateTime4 > now)
      return dateTime4 - now;
    Log.Error("ホスト回復時間がおかしいようです");
    return TimeSpan.FromDays(1.0);
  }

  private IEnumerator GetCurrentStatus()
  {
    bool isRequest = true;
    Protocol.Send<QuestExplorePointModel.RequestSendForm, QuestExplorePointModel>(QuestExplorePointModel.URL, new QuestExplorePointModel.RequestSendForm()
    {
      eid = this.eventData.eventId
    }, (Action<QuestExplorePointModel>) (result =>
    {
      isRequest = false;
      this.currentData = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  private void InitGoToSearchButton(Transform t) => this.SetEvent(t, "TO_SEARCH", (object) null);

  private void OnQuery_TO_SEARCH() => GameSection.SetEventData((object) this.eventData.eventId);

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.Explore);

  private void SetDifficultySprite(Transform t, DeliveryTable.DeliveryData dd)
  {
    this.SetActive(t, (Enum) QuestExploreSelectList.UI.SPR_TYPE_DIFFICULTY, dd != null && dd.difficulty >= DIFFICULTY_MODE.HARD);
  }

  private void SortDeliveryList()
  {
    this.deliveryList.Clear();
    this.allDeliveryList.Clear();
    int index = 0;
    for (int length = this.deliveryInfo.Length; index < length; ++index)
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
      if (deliveryTableData != null)
        this.deliveryList.Add(deliveryTableData);
    }
    this.allDeliveryList.AddRange((IEnumerable<DeliveryTable.DeliveryData>) this.deliveryList);
    this.allDeliveryList.AddRange((IEnumerable<DeliveryTable.DeliveryData>) this.clearedDeliveries);
    this.allDeliveryList.Sort((IComparer<DeliveryTable.DeliveryData>) new QuestExploreSelectList.ExploreSort());
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
    SPR_FRAME,
    LBL_HOST_LIMIT,
    LBL_HOST_RESET_TIME,
    LBL_POINT_TITLE,
    LBL_CURRENT_POINT,
    OBJ_NEXT_REWARD_ROOT,
    LBL_NEXT_REWARD_NAME,
    LBL_NEXT_POINT,
    OBJ_NEXT_REWARD_ICON_POS,
    SPR_TYPE_DIFFICULTY,
    BTN_INFO_NEW,
    OBJ_CURRENT_STATUS,
  }

  private class ExploreSort : IComparer<DeliveryTable.DeliveryData>
  {
    public int Compare(DeliveryTable.DeliveryData x, DeliveryTable.DeliveryData y)
    {
      bool flag1 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) x.id);
      bool flag2 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) y.id);
      if (flag1 != flag2)
        return flag1 ? -1 : 1;
      if (x.type != y.type)
        return x.type == DELIVERY_TYPE.EVENT ? -1 : 1;
      if (x.type == DELIVERY_TYPE.SUB_EVENT && y.type == DELIVERY_TYPE.SUB_EVENT)
      {
        bool flag3 = MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(x.id);
        bool flag4 = MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(y.id);
        if (flag3 != flag4)
          return flag3 ? 1 : -1;
      }
      return (int) x.id - (int) y.id;
    }
  }
}
