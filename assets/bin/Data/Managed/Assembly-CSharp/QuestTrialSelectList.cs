// Decompiled with JetBrains decompiler
// Type: QuestTrialSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestTrialSelectList : QuestEventSelectList
{
  protected override bool showMap => false;

  protected override void UpdateTable()
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
    this.SetActive((Enum) QuestTrialSelectList.UI.OBJ_ACTIVE_ROOT, is_visible);
    this.SetActive((Enum) QuestTrialSelectList.UI.OBJ_INACTIVE_ROOT, !is_visible);
    this.SetLabelText((Enum) QuestTrialSelectList.UI.LBL_MAX, this.pageMax.ToString());
    this.SetLabelText((Enum) QuestTrialSelectList.UI.LBL_NOW, this.nowPage.ToString());
    QuestEventSelectList.ShowDeliveryData[] showList = this.GetPagingList<QuestEventSelectList.ShowDeliveryData>(showDeliveryDataList.ToArray(), 10, this.nowPage);
    int length = showList.Length;
    if (this.showStory)
      length += num1 + this.stories.Count;
    if (length == 0)
    {
      this.SetActive((Enum) QuestTrialSelectList.UI.STR_DELIVERY_NON_LIST, true);
      this.SetActive((Enum) QuestTrialSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestTrialSelectList.UI.TBL_DELIVERY_QUEST, false);
    }
    else
    {
      this.SetActive((Enum) QuestTrialSelectList.UI.STR_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestTrialSelectList.UI.GRD_DELIVERY_QUEST, false);
      this.SetActive((Enum) QuestTrialSelectList.UI.TBL_DELIVERY_QUEST, true);
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
      Transform ctrl = this.GetCtrl((Enum) QuestTrialSelectList.UI.TBL_DELIVERY_QUEST);
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
      this.SetTable((Enum) QuestTrialSelectList.UI.TBL_DELIVERY_QUEST, "", length, this.isResetUI, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        if (i < storyStartIndex)
          return i < borderIndex ? (i < questStartIndex ? (!Object.op_Inequality((Object) null, (Object) this.mapItem) ? this.Realizes("QuestEventBorderItem", parent) : ResourceUtility.Realizes((Object) ((Component) this.mapItem).gameObject, parent)) : this.Realizes("QuestRequestItemTrial", parent)) : this.Realizes("QuestEventBorderItem", parent);
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
          if (i >= borderIndex && i < storyStartIndex)
            return;
          if (i >= questStartIndex && i < borderIndex)
          {
            this.InitDelivery(showList[i - questStartIndex], t);
            this.ChangeDeliveryFrameSprite(t);
          }
          else
          {
            if (i >= questStartIndex)
              return;
            this.InitMap(t);
          }
        }
      }));
      ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestTrialSelectList.UI.SCR_DELIVERY_QUEST)).enabled = true;
      this.RepositionTable();
    }
  }

  protected override void UpdateCompletedDeliveryUI(Transform parent)
  {
    this.SetActive(parent, (Enum) QuestTrialSelectList.UI.SPR_CLEARD_BLACK, false);
  }

  protected override void ChangeDeliveryFrameSprite(Transform parent)
  {
    this.SetSprite(parent, (Enum) QuestTrialSelectList.UI.SPR_FRAME, "RequestPlate_Trial");
  }

  protected override void OnQuery_JUMP_TO_STORY_PAGE()
  {
    EventData[] eventDataArray = new EventData[2]
    {
      new EventData("SELECT_TRIAL", (object) null),
      new EventData("SELECT_CHAPTER_FROM_OUTER", (object) null)
    };
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.GetChapterId(),
      (object) eventDataArray
    });
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.Trial);

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
        new EventData("TO_EVENT", (object) null),
        new EventData("SELECT_TRIAL", (object) this.eventData)
      }
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
    SPR_CLEARD_BLACK,
    SPR_FRAME,
  }
}
