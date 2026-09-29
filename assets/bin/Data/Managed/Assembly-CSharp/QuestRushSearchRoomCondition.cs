// Decompiled with JetBrains decompiler
// Type: QuestRushSearchRoomCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestRushSearchRoomCondition : QuestSearchRoomConditionBase
{
  private QuestRushSearchRoomCondition.RushSearchRequestParam searchParam = new QuestRushSearchRoomCondition.RushSearchRequestParam();
  private List<int> questIdList = new List<int>();
  private string[] maxFloorList;
  private string[] minFloorList;
  private Transform minFloorPopup;
  private Transform maxFloorPopup;
  private static readonly string PopUpPrefabName = "ScrollablePopupList";

  public override void Initialize()
  {
    this.questIdList = MonoBehaviourSingleton<PartyManager>.I.nowRushQuestIds;
    this.LoadSearchRequestParam();
    this.CopySearchRequestParam();
    this.CreateFloorPopText();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateMinFloor();
    this.UpdateMaxFloor();
  }

  private void UpdateMinFloor()
  {
    this.SetLabelText((Enum) QuestRushSearchRoomCondition.UI.LBL_TARGET_MIN_FLOOR, this.minFloorList[this.getCurrentSelectMinFloorIndex()]);
  }

  private void UpdateMaxFloor()
  {
    this.SetLabelText((Enum) QuestRushSearchRoomCondition.UI.LBL_TARGET_MAX_FLOOR, this.maxFloorList[this.getCurrentSelectMaxFloorIndex()]);
  }

  protected override void LoadSearchRequestParam()
  {
    MonoBehaviourSingleton<PartyManager>.I.SetRushRequestFromPrefs();
    bool flag = true;
    if (this.questIdList != null)
      flag = flag & this.questIdList.Contains(MonoBehaviourSingleton<PartyManager>.I.rushSearchRequest.minFloorQuestId) & this.questIdList.Contains(MonoBehaviourSingleton<PartyManager>.I.rushSearchRequest.maxFloorQuestId);
    if (flag)
      return;
    MonoBehaviourSingleton<PartyManager>.I.SetRushSearchRequest(new QuestRushSearchRoomCondition.RushSearchRequestParam(this.questIdList[0], this.questIdList[this.questIdList.Count - 1]));
  }

  protected override void CopySearchRequestParam()
  {
    QuestRushSearchRoomCondition.RushSearchRequestParam rushSearchRequest = MonoBehaviourSingleton<PartyManager>.I.rushSearchRequest;
    this.searchParam.minFloorQuestId = rushSearchRequest.minFloorQuestId;
    this.searchParam.maxFloorQuestId = rushSearchRequest.maxFloorQuestId;
  }

  protected override void SetCondition()
  {
    MonoBehaviourSingleton<PartyManager>.I.SetRushSearchRequest(this.searchParam);
  }

  protected override void SendSearch()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendRushSearch((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
        this.OnNotFoundQuest();
      GameSection.ResumeEvent(true);
    }), true);
  }

  protected override void SendRandomMatching()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendRushSearchRandomMatching((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success)
        this.OnNotFoundMatchingParty();
      GameSection.ResumeEvent(true);
    }));
  }

  private void CreateFloorPopText()
  {
    int count = this.questIdList.Count;
    int num1 = 0;
    this.maxFloorList = new string[count];
    this.minFloorList = new string[count];
    for (int index = 0; index < count; ++index)
    {
      this.minFloorList[index] = (num1 + 1).ToString();
      int num2 = QuestTable.GetSameRushQuestData(Singleton<QuestTable>.I.GetQuestData((uint) this.questIdList[index]).rushId).Count - 1;
      num1 += num2;
      this.maxFloorList[index] = num1.ToString();
    }
  }

  public void OnQuery_TARGET_MIN_FLOOR() => this.ShowMinFloorPopup();

  public void OnQuery_TARGET_MAX_FLOOR() => this.ShowMaxFloorPopup();

  private void ShowMinFloorPopup()
  {
    if (Object.op_Equality((Object) this.minFloorPopup, (Object) null))
      this.minFloorPopup = this.Realizes(QuestRushSearchRoomCondition.PopUpPrefabName, this.GetCtrl((Enum) QuestRushSearchRoomCondition.UI.POP_TARGET_MIN_FLOOR), false);
    if (Object.op_Equality((Object) this.minFloorPopup, (Object) null))
      return;
    int selectMinFloorIndex = this.getCurrentSelectMinFloorIndex();
    bool[] button_enable = new bool[this.minFloorList.Length];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index <= this.getCurrentSelectMaxFloorIndex();
    UIScrollablePopupList.CreatePopup(this.minFloorPopup, this.GetCtrl((Enum) QuestRushSearchRoomCondition.UI.POP_TARGET_MIN_FLOOR), 7, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.minFloorList, button_enable, selectMinFloorIndex, (Action<int>) (index =>
    {
      this.searchParam.minFloorQuestId = this.questIdList[index];
      this.RefreshUI();
    }));
  }

  private void ShowMaxFloorPopup()
  {
    if (Object.op_Equality((Object) this.maxFloorPopup, (Object) null))
      this.maxFloorPopup = this.Realizes(QuestRushSearchRoomCondition.PopUpPrefabName, this.GetCtrl((Enum) QuestRushSearchRoomCondition.UI.POP_TARGET_MAX_FLOOR), false);
    if (Object.op_Equality((Object) this.maxFloorPopup, (Object) null))
      return;
    int selectMaxFloorIndex = this.getCurrentSelectMaxFloorIndex();
    bool[] button_enable = new bool[this.maxFloorList.Length];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index >= this.getCurrentSelectMinFloorIndex();
    UIScrollablePopupList.CreatePopup(this.maxFloorPopup, this.GetCtrl((Enum) QuestRushSearchRoomCondition.UI.POP_TARGET_MAX_FLOOR), 7, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.maxFloorList, button_enable, selectMaxFloorIndex, (Action<int>) (index =>
    {
      this.searchParam.maxFloorQuestId = this.questIdList[index];
      this.RefreshUI();
    }));
  }

  private int getCurrentSelectMinFloorIndex()
  {
    return this.getFloorIndex(this.searchParam.minFloorQuestId);
  }

  private int getCurrentSelectMaxFloorIndex()
  {
    return this.getFloorIndex(this.searchParam.maxFloorQuestId);
  }

  private int getFloorIndex(int questId)
  {
    int floorIndex = this.questIdList.IndexOf(questId);
    if (floorIndex <= -1)
      floorIndex = 0;
    return floorIndex;
  }

  private enum UI
  {
    POP_TARGET_MIN_FLOOR,
    POP_TARGET_MAX_FLOOR,
    LBL_TARGET_MIN_FLOOR,
    LBL_TARGET_MAX_FLOOR,
  }

  public class RushSearchRequestParam
  {
    public int minFloorQuestId;
    public int maxFloorQuestId;

    public RushSearchRequestParam(int minQuestId, int maxQuestId)
    {
      this.minFloorQuestId = minQuestId;
      this.maxFloorQuestId = maxQuestId;
    }

    public RushSearchRequestParam()
    {
    }
  }
}
