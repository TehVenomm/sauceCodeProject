// Decompiled with JetBrains decompiler
// Type: QuestRushSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestRushSelectList : QuestEventSelectList
{
  private const string RUSH_FRAME_SPRITE = "RequestPlate_Rush";
  private QuestRushPointModel.Param currentData;

  protected override bool showMap => false;

  protected override IEnumerator DoInitialize()
  {
    bool isCarnival = MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(this.eventData.eventId);
    if (!isCarnival)
      yield return (object) this.StartCoroutine(this.GetCurrentStatus());
    this.SetActive((Enum) QuestRushSelectList.UI.OBJ_CURRENT_STATUS, !isCarnival);
    this.SetActive((Enum) QuestRushSelectList.UI.OBJ_NEXT_REWARD_ROOT, !isCarnival);
    this.SetActive((Enum) QuestRushSelectList.UI.SPR_NORMAL_INFO, !isCarnival);
    this.SetActive((Enum) QuestRushSelectList.UI.SPR_CARNIVAL_INFO, isCarnival);
    yield return (object) this.StartCoroutine(base.DoInitialize());
  }

  protected override void UpdateTable()
  {
    if (this.currentData != null)
      this.SetLabelText((Enum) QuestRushSelectList.UI.LBL_CURRENT_POINT, StringTable.Format(STRING_CATEGORY.RUSH, 0U, (object) this.currentData.point));
    this.SetLabelText((Enum) QuestRushSelectList.UI.LBL_POINT_TITLE, StringTable.Get(STRING_CATEGORY.RUSH, 1U));
    if (this.currentData != null && this.currentData.reward != null && this.currentData.reward.reward.Count > 0)
    {
      this.SetActive((Enum) QuestRushSelectList.UI.OBJ_NEXT_REWARD_ROOT, true);
      this.SetLabelText((Enum) QuestRushSelectList.UI.LBL_NEXT_POINT, StringTable.Format(STRING_CATEGORY.RUSH, 0U, (object) this.currentData.reward.point));
      int num = Mathf.Min(2, this.currentData.reward.reward.Count);
      string text = "";
      for (int index = 0; index < num; ++index)
      {
        QuestRushPointModel.Param.Reward reward = this.currentData.reward.reward[index];
        Transform ctrl;
        if (index == 0)
        {
          ctrl = this.GetCtrl((Enum) QuestRushSelectList.UI.OBJ_NEXT_REWARD_ICON_POS1);
          text = Utility.GetRewardName((REWARD_TYPE) reward.type, (uint) reward.itemId);
        }
        else
        {
          ctrl = this.GetCtrl((Enum) QuestRushSelectList.UI.OBJ_NEXT_REWARD_ICON_POS2);
          text = $"{text}\n{Utility.GetRewardName((REWARD_TYPE) reward.type, (uint) reward.itemId)}";
        }
        ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, ctrl, reward.num);
      }
      if (this.currentData.reward.reward.Count == 1)
        this.SetActive((Enum) QuestRushSelectList.UI.OBJ_NEXT_REWARD_ICON_POS2, false);
      this.SetLabelText((Enum) QuestRushSelectList.UI.LBL_NEXT_REWARD_NAME, text);
      ((Component) this.GetCtrl((Enum) QuestRushSelectList.UI.GRD_NEXT_ICON)).GetComponent<UIGrid>().Reposition();
    }
    else
      this.SetActive((Enum) QuestRushSelectList.UI.OBJ_NEXT_REWARD_ROOT, false);
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    int item_num = this.deliveryInfo.Length + this.clearedDeliveries.Count + 1;
    if (this.showStory)
      item_num += num1 + this.stories.Count;
    if (this.deliveryInfo == null || item_num == 0)
    {
      this.SetActive((Enum) QuestRushSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestRushSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestRushSelectList.UI.TBL_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestRushSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestRushSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestRushSelectList.UI.TBL_DELIVERY_QUEST, true);
      int questStartIndex = 0;
      questStartIndex++;
      int completedStartIndex = this.deliveryInfo.Length + questStartIndex;
      int borderIndex = completedStartIndex + this.clearedDeliveries.Count;
      int storyStartIndex = borderIndex;
      if (this.stories.Count > 0)
        ++storyStartIndex;
      Transform ctrl = this.GetCtrl((Enum) QuestRushSelectList.UI.TBL_DELIVERY_QUEST);
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
      this.SetTable((Enum) QuestRushSelectList.UI.TBL_DELIVERY_QUEST, "", item_num, false, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        Transform transform = (Transform) null;
        if (i >= storyStartIndex)
          return !this.HasChapterStory() || i == storyStartIndex || !isRenewalFlag ? this.Realizes("QuestEventStoryItem", parent) : (Transform) null;
        if (i >= borderIndex)
          transform = this.Realizes("QuestEventBorderItem", parent);
        else if (i >= questStartIndex)
          transform = this.Realizes("QuestRequestItemRush", parent);
        else if (i == 0)
          transform = this.Realizes("QuestRushRequestItemToSearch", parent);
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
            this.InitGoToSearchButton(t);
        }
        if (i >= storyStartIndex || i == 0)
          return;
        this.SetSprite(t, (Enum) QuestRushSelectList.UI.SPR_FRAME, "RequestPlate_Rush");
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestRushSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
      this.RepositionTable();
    }
  }

  protected override void UpdateGrid()
  {
    int item_num = this.deliveryInfo.Length + this.clearedDeliveries.Count;
    if (this.deliveryInfo == null || item_num == 0)
    {
      this.SetActive((Enum) QuestRushSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestRushSelectList.UI.GRD_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestRushSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestRushSelectList.UI.GRD_DELIVERY_QUEST, true);
      this.SetDynamicList((Enum) QuestRushSelectList.UI.GRD_DELIVERY_QUEST, "QuestRequestItemRush", item_num, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetActive(t, true);
        bool is_visible = i >= this.deliveryInfo.Length;
        DeliveryTable.DeliveryData deliveryData;
        if (!is_visible)
        {
          this.SetEvent(t, "SELECT_RUSH", i);
          deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[i].dId);
        }
        else
        {
          this.SetEvent(t, "SELECT_COMPLETED_RUSH", i - this.deliveryInfo.Length);
          deliveryData = this.clearedDeliveries[i - this.deliveryInfo.Length];
        }
        this.SetupDeliveryListItem(t, deliveryData);
        this.SetActive(t, (Enum) QuestRushSelectList.UI.OBJ_REQUEST_COMPLETED, is_visible);
        this.SetDifficultySprite(t, deliveryData);
        this.SetSprite(t, (Enum) QuestRushSelectList.UI.SPR_FRAME, "RequestPlate_Rush");
      }));
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
      this.SetEvent(t, "SELECT_RUSH_STORY", index);
      this.SetLabelText(t, (Enum) QuestRushSelectList.UI.LBL_STORY_TITLE, this.stories[index].title);
    }
  }

  protected override void InitNormalDelivery(int index, Transform t)
  {
    this.SetEvent(t, "SELECT_RUSH", index);
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
    this.SetupDeliveryListItem(t, deliveryTableData);
    this.SetDifficultySprite(t, deliveryTableData);
  }

  protected override void InitCompletedDelivery(int completedIndex, Transform t)
  {
    int num = this.clearedDeliveries.Count - 1 - completedIndex;
    DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[num];
    this.SetEvent(t, "SELECT_COMPLETED_RUSH", num);
    this.SetupDeliveryListItem(t, clearedDelivery);
    this.SetActive(t, (Enum) QuestRushSelectList.UI.OBJ_REQUEST_COMPLETED, true);
    this.SetDifficultySprite(t, clearedDelivery);
    this.SetCompletedHaveCount(t, clearedDelivery);
  }

  private void OnQuery_SELECT_RUSH()
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
          if (is_tutorial)
            TutorialStep.isSendFirstRewardComplete = true;
          if (!enable_clear_event)
          {
            MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
            GameSection.ChangeStayEvent("RUSH_REWARD", (object) new object[2]
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
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) delivery_id,
        null
      });
  }

  private void InitGoToSearchButton(Transform t) => this.SetEvent(t, "TO_SEARCH", (object) null);

  private void OnQuery_TO_SEARCH()
  {
    MonoBehaviourSingleton<PartyManager>.I.SetNowRushQuestIds(this.CreateQuestIdList());
  }

  private List<int> CreateQuestIdList()
  {
    List<int> questIdList = new List<int>();
    if (this.clearedDeliveries != null)
    {
      int index = 0;
      for (int count = this.clearedDeliveries.Count; index < count; ++index)
        questIdList.Add((int) this.clearedDeliveries[index].needs[0].questId);
    }
    if (this.deliveryInfo != null)
    {
      int index = 0;
      for (int length = this.deliveryInfo.Length; index < length; ++index)
      {
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
        questIdList.Add((int) deliveryTableData.needs[0].questId);
      }
    }
    return questIdList;
  }

  private void OnQuery_SELECT_COMPLETED_RUSH()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) (int) this.clearedDeliveries[(int) GameSection.GetEventData()].id,
      (object) new DeliveryRewardList(),
      (object) true
    });
  }

  private void OnQuery_SELECT_RUSH_STORY()
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

  private void SetDifficultySprite(Transform t, DeliveryTable.DeliveryData dd)
  {
    this.SetActive(t, (Enum) QuestRushSelectList.UI.SPR_TYPE_DIFFICULTY, dd.difficulty >= DIFFICULTY_MODE.HARD);
  }

  private IEnumerator GetCurrentStatus()
  {
    bool isRequest = true;
    Protocol.Send<QuestRushPointModel.RequestSendForm, QuestRushPointModel>(QuestRushPointModel.URL, new QuestRushPointModel.RequestSendForm()
    {
      eid = this.eventData.eventId
    }, (Action<QuestRushPointModel>) (result =>
    {
      isRequest = false;
      this.currentData = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  protected override void OnQuery_INFO()
  {
    GameSection.SetEventData((object) string.Format(WebViewManager.NewsWithLinkParamFormat, (object) (this.eventData.linkName + "_REWARD")));
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.Rush);

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
    SPR_TYPE_DIFFICULTY,
    LBL_POINT_TITLE,
    LBL_CURRENT_POINT,
    OBJ_NEXT_REWARD_ROOT,
    LBL_NEXT_REWARD_NAME,
    LBL_NEXT_POINT,
    GRD_NEXT_ICON,
    OBJ_NEXT_REWARD_ICON_POS1,
    OBJ_NEXT_REWARD_ICON_POS2,
    OBJ_CURRENT_STATUS,
    SPR_NORMAL_INFO,
    SPR_CARNIVAL_INFO,
  }
}
