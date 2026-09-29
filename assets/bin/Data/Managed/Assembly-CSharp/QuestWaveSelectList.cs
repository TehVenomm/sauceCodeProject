// Decompiled with JetBrains decompiler
// Type: QuestWaveSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestWaveSelectList : QuestEventSelectList
{
  private const string FRAME_SPRITE = "RequestPlateBase";
  private QuestPointRewardModel.Param currentData;

  protected override bool showMap => false;

  protected override IEnumerator DoInitialize()
  {
    bool isCarnival = MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(this.eventData.eventId);
    if (!isCarnival)
      yield return (object) this.StartCoroutine(this.GetCurrentStatus());
    this.SetActive((Enum) QuestWaveSelectList.UI.OBJ_CURRENT_STATUS, !isCarnival);
    this.SetActive((Enum) QuestWaveSelectList.UI.OBJ_NEXT_REWARD_ROOT, !isCarnival);
    this.SetActive((Enum) QuestWaveSelectList.UI.SPR_NORMAL_INFO, !isCarnival);
    this.SetActive((Enum) QuestWaveSelectList.UI.SPR_CARNIVAL_INFO, isCarnival);
    this.SetActive((Enum) QuestWaveSelectList.UI.LBL_WAVE_TITLE, !isCarnival);
    yield return (object) this.StartCoroutine(base.DoInitialize());
  }

  private IEnumerator GetCurrentStatus()
  {
    bool isRequest = true;
    Protocol.Send<QuestPointRewardModel.RequestSendForm, QuestPointRewardModel>(QuestPointRewardModel.URL, new QuestPointRewardModel.RequestSendForm()
    {
      eid = this.eventData.eventId
    }, (Action<QuestPointRewardModel>) (result =>
    {
      isRequest = false;
      this.currentData = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  protected override void UpdateTable()
  {
    this.SetLabelText((Enum) QuestWaveSelectList.UI.LBL_WAVE_TITLE, this.eventData.name);
    if (this.currentData != null)
      this.SetLabelText((Enum) QuestWaveSelectList.UI.LBL_CURRENT_POINT, StringTable.Format(STRING_CATEGORY.WAVE_MATCH, 0U, (object) this.currentData.point));
    this.SetLabelText((Enum) QuestWaveSelectList.UI.LBL_POINT_TITLE, StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 1U));
    if (this.currentData != null && this.currentData.reward != null && this.currentData.reward.reward.Count > 0)
    {
      this.SetActive((Enum) QuestWaveSelectList.UI.OBJ_NEXT_REWARD_ROOT, true);
      QuestPointRewardModel.Param.Reward reward = this.currentData.reward.reward[0];
      ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, this.GetCtrl((Enum) QuestWaveSelectList.UI.OBJ_NEXT_REWARD_ICON_POS), reward.num);
      string rewardName = Utility.GetRewardName((REWARD_TYPE) reward.type, (uint) reward.itemId);
      this.SetLabelText((Enum) QuestWaveSelectList.UI.LBL_NEXT_POINT, StringTable.Format(STRING_CATEGORY.WAVE_MATCH, 0U, (object) this.currentData.reward.point));
      this.SetLabelText((Enum) QuestWaveSelectList.UI.LBL_NEXT_REWARD_NAME, rewardName);
    }
    else
      this.SetActive((Enum) QuestWaveSelectList.UI.OBJ_NEXT_REWARD_ROOT, false);
    int num1 = 0;
    if (this.stories.Count > 0)
      ++num1;
    int item_num = this.deliveryInfo.Length + this.clearedDeliveries.Count + 1;
    if (this.showStory)
      item_num += num1 + this.stories.Count;
    if (this.deliveryInfo == null || item_num == 0)
    {
      this.SetActive((Enum) QuestWaveSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestWaveSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestWaveSelectList.UI.TBL_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestWaveSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestWaveSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestWaveSelectList.UI.TBL_DELIVERY_QUEST, true);
      int questStartIndex = 0;
      questStartIndex++;
      int completedStartIndex = this.deliveryInfo.Length + questStartIndex;
      int borderIndex = completedStartIndex + this.clearedDeliveries.Count;
      int storyStartIndex = borderIndex;
      if (this.stories.Count > 0)
        ++storyStartIndex;
      Transform ctrl = this.GetCtrl((Enum) QuestWaveSelectList.UI.TBL_DELIVERY_QUEST);
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
      this.SetTable((Enum) QuestWaveSelectList.UI.TBL_DELIVERY_QUEST, "", item_num, false, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        Transform transform = (Transform) null;
        if (i >= storyStartIndex)
          return !this.HasChapterStory() || i == storyStartIndex || !isRenewalFlag ? this.Realizes("QuestEventStoryItem", parent) : (Transform) null;
        if (i >= borderIndex)
          transform = this.Realizes("QuestEventBorderItem", parent);
        else if (i >= questStartIndex)
          transform = this.Realizes("QuestRequestItemWave", parent);
        else if (i == 0)
          transform = this.Realizes("QuestWaveRequestItemToSearch", parent);
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
        this.SetSprite(t, (Enum) QuestWaveSelectList.UI.SPR_FRAME, "RequestPlateBase");
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestWaveSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
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
      this.SetEvent(t, "SELECT_WAVE_STORY", index);
      this.SetLabelText(t, (Enum) QuestWaveSelectList.UI.LBL_STORY_TITLE, this.stories[index].title);
    }
  }

  private void OnQuery_SELECT_WAVE_STORY()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) this.stories[(int) GameSection.GetEventData()].id,
      (object) "",
      (object) "",
      (object) new EventData[3]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("TO_EVENT", (object) null),
        new EventData("SELECT_WAVE", (object) this.eventData)
      }
    });
  }

  protected override void InitNormalDelivery(int index, Transform t)
  {
    this.SetEvent(t, "SELECT_WAVE", index);
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
    this.SetupDeliveryListItem(t, deliveryTableData);
    this.SetDifficultySprite(t, deliveryTableData);
  }

  private void OnQuery_SELECT_WAVE()
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
            GameSection.ChangeStayEvent("WAVE_REWARD", (object) new object[2]
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

  protected override void InitCompletedDelivery(int completedIndex, Transform t)
  {
    DeliveryTable.DeliveryData clearedDelivery = this.clearedDeliveries[completedIndex];
    this.SetEvent(t, "SELECT_COMPLETED_WAVE", completedIndex);
    this.SetupDeliveryListItem(t, clearedDelivery);
    this.SetDifficultySprite(t, clearedDelivery);
    this.SetActive(t, (Enum) QuestWaveSelectList.UI.OBJ_REQUEST_COMPLETED, true);
    this.SetCompletedHaveCount(t, clearedDelivery);
  }

  private void OnQuery_SELECT_COMPLETED_WAVE()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) (int) this.clearedDeliveries[(int) GameSection.GetEventData()].id,
      (object) new DeliveryRewardList(),
      (object) true
    });
  }

  private void InitGoToSearchButton(Transform t) => this.SetEvent(t, "TO_SEARCH", (object) null);

  private void OnQuery_TO_SEARCH() => GameSection.SetEventData((object) this.eventData.eventId);

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.Wave);

  private void SetDifficultySprite(Transform t, DeliveryTable.DeliveryData dd)
  {
    this.SetActive(t, (Enum) QuestWaveSelectList.UI.SPR_TYPE_DIFFICULTY, dd.difficulty >= DIFFICULTY_MODE.HARD);
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
    LBL_WAVE_TITLE,
    SPR_TYPE_DIFFICULTY,
    OBJ_CURRENT_STATUS,
    SPR_NORMAL_INFO,
    SPR_CARNIVAL_INFO,
  }
}
