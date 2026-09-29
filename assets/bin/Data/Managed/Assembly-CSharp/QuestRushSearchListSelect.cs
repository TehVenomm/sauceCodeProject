// Decompiled with JetBrains decompiler
// Type: QuestRushSearchListSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestRushSearchListSelect : QuestSearchListSelectBase
{
  private const string LIST_ITEM_PREFAB_NAME = "QuestRushSearchListSelectItem";
  private QuestRushSearchRoomCondition.RushSearchRequestParam defaultParam;
  private List<int> questIdList = new List<int>();

  public override void Initialize()
  {
    this.questIdList = MonoBehaviourSingleton<PartyManager>.I.nowRushQuestIds;
    base.Initialize();
  }

  protected override void SendSearchRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendRushSearch((Action<bool, Error>) ((is_success, err) =>
    {
      onFinish();
      if (!is_success && this.isInitialized)
      {
        if (err != Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
          return;
        GameSection.ChangeStayEvent("NOT_FOUND_QUEST");
        if (cb == null)
          return;
        cb(true);
      }
      else
      {
        if (cb == null)
          return;
        cb(is_success);
      }
    }), false);
  }

  protected override void ResetSearchRequest()
  {
    MonoBehaviourSingleton<PartyManager>.I.ResetRushSearchRequest();
    if (this.defaultParam == null)
      this.defaultParam = this.questIdList == null ? new QuestRushSearchRoomCondition.RushSearchRequestParam(0, 0) : new QuestRushSearchRoomCondition.RushSearchRequestParam(this.questIdList.First<int>(), this.questIdList.Last<int>());
    MonoBehaviourSingleton<PartyManager>.I.SetRushSearchRequest(this.defaultParam);
  }

  public override void UpdateUI()
  {
    if (!PartyManager.IsValidNotEmptyList())
    {
      this.SetActive((Enum) QuestRushSearchListSelect.UI.GRD_QUEST, false);
      this.SetActive((Enum) QuestRushSearchListSelect.UI.STR_NON_LIST, true);
    }
    else
    {
      PartyModel.Party[] partys = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
      this.SetActive((Enum) QuestRushSearchListSelect.UI.GRD_QUEST, true);
      this.SetActive((Enum) QuestRushSearchListSelect.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) QuestRushSearchListSelect.UI.GRD_QUEST, "QuestRushSearchListSelectItem", partys.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) partys[i].quest.questId);
        if (questData == null)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetEvent(t, "SELECT_ROOM", i);
          this.SetQuestData(questData, t);
          this.SetPartyData(partys[i], t);
          this.SetDeliveryData(questData.questID, t);
          this.SetStatusIconInfo(partys[i], t);
          this.SetMemberIcon(t, questData);
        }
      }));
      base.UpdateUI();
    }
  }

  protected override void SetQuestData(QuestTable.QuestTableData questData, Transform t)
  {
    this.SetLabelText(t, (Enum) QuestRushSearchListSelect.UI.LBL_QUEST_NAME, questData.questText);
    if (questData.userNumLimit < 4)
      this.SetActive(t, (Enum) QuestRushSearchListSelect.UI.TGL_MEMBER_3, false);
    if (questData.userNumLimit < 3)
      this.SetActive(t, (Enum) QuestRushSearchListSelect.UI.TGL_MEMBER_2, false);
    if (questData.userNumLimit < 2)
      this.SetActive(t, (Enum) QuestRushSearchListSelect.UI.TGL_MEMBER_1, false);
    ResourceLoad.LoadWithSetUITexture(((Component) this.FindCtrl(t, (Enum) QuestRushSearchListSelect.UI.TEX_RUSH_IMAGE)).GetComponent<UITexture>(), RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int) questData.rushIconId));
  }

  private void SetDeliveryData(uint questId, Transform t)
  {
    DeliveryTable.DeliveryData tableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questId);
    this.SetActive(t, (Enum) QuestRushSearchListSelect.UI.SPR_TYPE_DIFFICULTY, tableDataFromQuestId != null && tableDataFromQuestId.difficulty >= DIFFICULTY_MODE.HARD);
  }

  private void OnCloseDialog_QuestRushSearchRoomCondition() => this.CloseSearchRoomCondition();

  protected new enum UI
  {
    GRD_QUEST,
    LBL_HOST_NAME,
    LBL_HOST_LV,
    TGL_MEMBER_1,
    TGL_MEMBER_2,
    TGL_MEMBER_3,
    LBL_LV,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
    LBL_QUEST_NAME,
    STR_NON_LIST,
    TEX_RUSH_IMAGE,
    SPR_TYPE_DIFFICULTY,
  }
}
