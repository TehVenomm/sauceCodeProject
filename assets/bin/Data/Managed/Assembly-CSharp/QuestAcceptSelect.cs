// Decompiled with JetBrains decompiler
// Type: QuestAcceptSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class QuestAcceptSelect : QuestSelect
{
  private uint materialId;
  protected Transform root;

  public override void Initialize()
  {
    this.root = this.SetPrefab(this.collectUI, nameof (QuestAcceptSelect));
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(this.questInfo.questData.tableData.questID);
    this.SetActive((Enum) QuestSelect.UI.OBJ_REWARD_ICON_ROOT, false);
    if (questItem == null || questItem.sellItems == null || questItem.sellItems.Count <= 0)
      return;
    int index = 0;
    for (int count = questItem.sellItems.Count; index < count; ++index)
    {
      QuestItem.SellItem sellItem = questItem.sellItems[index];
      REWARD_TYPE type = (REWARD_TYPE) sellItem.type;
      uint itemId = (uint) sellItem.itemId;
      this.materialId = itemId;
      if (sellItem.num <= 0)
      {
        Log.Error(LOG.OUTGAME, "QuestItem sold get item num is zero. type={0},itemId={1}", (object) type, (object) itemId);
        break;
      }
      int num = -1;
      this.SetActive((Enum) QuestSelect.UI.OBJ_REWARD_ICON_ROOT, true);
      ItemIcon.CreateRewardItemIcon(type, itemId, this.FindCtrl(this.root, (Enum) QuestSelect.UI.OBJ_MATERIAL_ICON_ROOT), num, "EQUIP_LIST", disable_rarity_text: true);
    }
  }

  public void OnCloseDialog_QuestAcceptRoomSettings() => this._OnCloseRoomSettings();

  public void OnCloseDialog_QuestAcceptStartChangeEquipSet() => this._OnCloseStartChangeEquipSet();

  protected virtual void OnQuery_GUILD_REQUEST()
  {
    GameSection.SetEventData((object) this.questInfo);
  }

  private void OnQuery_EQUIP_LIST()
  {
    if (this.materialId <= 0U)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.materialId
      });
  }

  private void OnQuery_QuestAcceptOrderCreateRoomConfirm_YES()
  {
    this._OnQueryOrderCreateRoomConfirm_YES();
  }

  private void OnCloseDialog_QuestAcceptOrderCreateRoomConfirm()
  {
    this._OnCloseDialogOrderCreateRoomConfirm();
  }
}
