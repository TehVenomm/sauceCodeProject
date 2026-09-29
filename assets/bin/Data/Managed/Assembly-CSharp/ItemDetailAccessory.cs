// Decompiled with JetBrains decompiler
// Type: ItemDetailAccessory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemDetailAccessory : SkillInfoBase
{
  protected ItemDetailEquip.CURRENT_SECTION callSection;
  protected AccessoryInfo info;
  protected Transform detailBase;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.callSection = (ItemDetailEquip.CURRENT_SECTION) eventData[0];
    if (eventData[1] is AccessorySortData accessorySortData)
    {
      this.info = accessorySortData.itemData;
      GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ACCESSORY, accessorySortData.GetUniqID());
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.detailBase = this.SetPrefab(this.GetCtrl((Enum) ItemDetailAccessory.UI.OBJ_DETAIL_ROOT), "ItemDetailAccessoryBase");
    if (Object.op_Equality((Object) this.detailBase, (Object) null))
      return;
    this.SetLabelText(this.detailBase, (Enum) ItemDetailAccessory.UI.LBL_NAME, this.info.tableData.name);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailAccessory.UI.LBL_DESCRIPTION, this.info.tableData.descriptPart);
    this.SetAccessoryRarityIcon(this.FindCtrl(this.detailBase, (Enum) ItemDetailAccessory.UI.SPR_SKILL_TYPE_ICON_BG), this.FindCtrl(this.detailBase, (Enum) ItemDetailAccessory.UI.SPR_SKILL_TYPE_ICON_RARITY), this.info.tableData);
    this.SetRenderAccessoryModel((Enum) ItemDetailAccessory.UI.TEX_MODEL, this.info.tableData.accessoryId, this.info.tableData.detailScale);
  }

  protected enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    LBL_NAME,
    LBL_DESCRIPTION,
    SPR_SKILL_TYPE_ICON,
    SPR_SKILL_TYPE_ICON_BG,
    SPR_SKILL_TYPE_ICON_RARITY,
  }
}
