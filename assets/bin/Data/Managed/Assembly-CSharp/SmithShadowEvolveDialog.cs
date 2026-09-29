// Decompiled with JetBrains decompiler
// Type: SmithShadowEvolveDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class SmithShadowEvolveDialog : GameSection
{
  private EquipItemInfo itemInfo;
  private EquipItemTable.EquipItemData shadowEvolveData;
  private CreateEquipItemTable.CreateEquipItemData createData;

  public override void Initialize()
  {
    this.itemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
    this.shadowEvolveData = this.itemInfo.tableData.GetShadowEvolveEquipTable();
    EquipItemTable.EquipItemData rootEquipTable = this.shadowEvolveData.GetRootEquipTable();
    this.createData = Singleton<CreateEquipItemTable>.I.GetCreateItemDataByEquipItem(rootEquipTable.id);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(this.createData.needMaterial[0].itemID);
    if (itemData == null)
      return;
    int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemData.id);
    int num = this.createData.needMaterial[0].num;
    bool is_visible = haveingItemNum >= num;
    this.SetMaterialInfo(ItemIconMaterial.CreateMaterialIcon(ItemIcon.GetItemIconType(itemData.type), itemData, this.GetCtrl((Enum) SmithShadowEvolveDialog.UI.OBJ_ICON_ROOT), haveingItemNum, num, "MATERIAL")._transform, REWARD_TYPE.ITEM, itemData.id);
    this.SetActive((Enum) SmithShadowEvolveDialog.UI.BTN_OK, is_visible);
    this.SetActive((Enum) SmithShadowEvolveDialog.UI.BTN_INACTIVE, !is_visible);
    base.UpdateUI();
  }

  private SmithManager.ResultData CreateResultData()
  {
    return new SmithManager.ResultData()
    {
      beforeRarity = (int) this.itemInfo.tableData.rarity,
      beforeLevel = this.itemInfo.level,
      beforeMaxLevel = this.itemInfo.tableData.maxLv,
      beforeExceedCnt = this.itemInfo.exceed,
      beforeAtk = this.itemInfo.atk,
      beforeDef = this.itemInfo.def,
      beforeHp = this.itemInfo.hp,
      beforeElemAtk = this.itemInfo.elemAtk,
      beforeElemDef = this.itemInfo.elemDef
    };
  }

  private void OnQuery_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.itemInfo.tableData.name
    });
  }

  private void OnQuery_SmithShadowEvolveConfirm_YES()
  {
    SmithManager.ResultData result_data = this.CreateResultData();
    GameSection.SetEventData((object) result_data);
    GameSection.StayEvent();
    MonoBehaviourSingleton<SmithManager>.I.SendShadowEvolveEquipItem(this.itemInfo.uniqueID, this.createData.needMaterial[0].itemID, (Action<Error, EquipItemInfo>) ((error, info) =>
    {
      if (error == Error.None)
      {
        result_data.itemData = (object) info;
        MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = info;
        MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
        MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
        GameSection.ResumeEvent(true);
      }
      else
        GameSection.ResumeEvent(false);
    }));
  }

  private enum UI
  {
    OBJ_ICON_ROOT,
    BTN_OK,
    BTN_INACTIVE,
  }
}
