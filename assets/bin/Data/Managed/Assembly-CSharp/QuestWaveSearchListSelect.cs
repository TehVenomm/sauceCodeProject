// Decompiled with JetBrains decompiler
// Type: QuestWaveSearchListSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class QuestWaveSearchListSelect : QuestSearchListSelectBase
{
  private const string LIST_ITEM_PREFAB_NAME = "QuestWaveSearchListSelectItem";
  private int eventId;

  public override void Initialize()
  {
    this.eventId = (int) GameSection.GetEventData();
    base.Initialize();
  }

  protected override void SendSearchRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendEventSearch(this.eventId, (Action<bool, Error>) ((is_success, err) =>
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
    }));
  }

  protected override void ResetSearchRequest()
  {
  }

  public override void UpdateUI()
  {
    if (!PartyManager.IsValidNotEmptyList())
    {
      this.SetActive((Enum) QuestWaveSearchListSelect.UI.GRD_QUEST, false);
      this.SetActive((Enum) QuestWaveSearchListSelect.UI.STR_NON_LIST, true);
    }
    else
    {
      PartyModel.Party[] partys = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
      this.SetActive((Enum) QuestWaveSearchListSelect.UI.GRD_QUEST, true);
      this.SetActive((Enum) QuestWaveSearchListSelect.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) QuestWaveSearchListSelect.UI.GRD_QUEST, "QuestWaveSearchListSelectItem", partys.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
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
    this.SetLabelText(t, (Enum) QuestWaveSearchListSelect.UI.LBL_QUEST_NAME, questData.questText);
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    if (enemyData == null)
      return;
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(questData.rarity), this.FindCtrl(t, (Enum) QuestWaveSearchListSelect.UI.OBJ_ENEMY), enemyData.element);
  }

  private void SetDeliveryData(uint questId, Transform t)
  {
    DeliveryTable.DeliveryData tableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questId);
    this.SetActive(t, (Enum) QuestWaveSearchListSelect.UI.SPR_TYPE_DIFFICULTY, tableDataFromQuestId != null && tableDataFromQuestId.difficulty >= DIFFICULTY_MODE.HARD);
  }

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
    OBJ_ENEMY,
    SPR_TYPE_DIFFICULTY,
  }
}
