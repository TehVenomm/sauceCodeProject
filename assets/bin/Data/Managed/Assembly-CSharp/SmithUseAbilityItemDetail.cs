// Decompiled with JetBrains decompiler
// Type: SmithUseAbilityItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class SmithUseAbilityItemDetail : GameSection
{
  private EquipItemInfo equipItemInfo;
  private AbilityItemSortData abilityItemInfo;

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS;
  }

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    if (eventData[0] is EquipItemInfo)
      this.equipItemInfo = eventData[0] as EquipItemInfo;
    if (eventData[1] is AbilityItemSortData)
      this.abilityItemInfo = eventData[1] as AbilityItemSortData;
    base.Initialize();
  }

  public override void InitializeReopen()
  {
    this.equipItemInfo = GameSection.GetEventData() as EquipItemInfo;
    base.InitializeReopen();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) SmithUseAbilityItemDetail.UI.LBL_NAME, this.equipItemInfo.tableData.name);
    this.SetLabelText((Enum) SmithUseAbilityItemDetail.UI.LBL_LV_NOW, this.equipItemInfo.level.ToString());
    this.SetLabelText((Enum) SmithUseAbilityItemDetail.UI.LBL_LV_MAX, this.equipItemInfo.tableData.maxLv.ToString());
    this.SetEquipmentTypeIcon((Enum) SmithUseAbilityItemDetail.UI.SPR_TYPE_ICON, (Enum) SmithUseAbilityItemDetail.UI.SPR_TYPE_ICON_BG, (Enum) SmithUseAbilityItemDetail.UI.SPR_TYPE_ICON_RARITY, this.equipItemInfo.tableData);
    Transform ctrl1 = this.GetCtrl((Enum) SmithUseAbilityItemDetail.UI.OBJ_BEFORE_ITEM_ROOT);
    Transform ctrl2 = this.GetCtrl((Enum) SmithUseAbilityItemDetail.UI.OBJ_AFTER_ITEM_ROOT);
    AbilityItemInfo abilityItem = this.equipItemInfo.GetAbilityItem();
    if (abilityItem == null)
    {
      this.SetLabelText(ctrl1, (Enum) SmithUseAbilityItemDetail.UI.LBL_ABILITY_ITEM_NAME, "");
      this.SetLabelText(ctrl1, (Enum) SmithUseAbilityItemDetail.UI.LBL_ABILITY_ITEM_DESC, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 28U));
    }
    else
    {
      this.SetLabelText(ctrl1, (Enum) SmithUseAbilityItemDetail.UI.LBL_ABILITY_ITEM_NAME, abilityItem.GetName());
      this.SetLabelText(ctrl1, (Enum) SmithUseAbilityItemDetail.UI.LBL_ABILITY_ITEM_DESC, abilityItem.GetDescription());
    }
    this.SetLabelText(ctrl2, (Enum) SmithUseAbilityItemDetail.UI.LBL_ABILITY_ITEM_NAME, this.abilityItemInfo.GetName());
    this.SetLabelText(ctrl2, (Enum) SmithUseAbilityItemDetail.UI.LBL_ABILITY_ITEM_DESC, this.abilityItemInfo.itemData.GetDescription());
  }

  private void OnQuery_START()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.abilityItemInfo.GetName(),
      (object) this.equipItemInfo.tableData.name
    });
  }

  private void OnQuery_SmithConfirmAbilityItem_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<SmithManager>.I.SendUseAbilityItem(this.equipItemInfo.uniqueID, this.abilityItemInfo.GetUniqID(), (Action<Error, EquipItemInfo>) ((error, itemInfo) => GameSection.ResumeEvent(error == Error.None)));
  }

  private void OnQuery_SEND()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<SmithManager>.I.SendUseAbilityItem(this.equipItemInfo.uniqueID, this.abilityItemInfo.GetUniqID(), (Action<Error, EquipItemInfo>) ((error, itemInfo) => GameSection.ResumeEvent(error != 0)));
  }

  private enum UI
  {
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    OBJ_BEFORE_ITEM_ROOT,
    OBJ_AFTER_ITEM_ROOT,
    LBL_ABILITY_ITEM_NAME,
    LBL_ABILITY_ITEM_DESC,
  }

  private enum BACK_TO
  {
    STATUS_TOP,
    STATUS_TOP_EQUIPDETAIL,
  }
}
