// Decompiled with JetBrains decompiler
// Type: SmithEvolveSelectMaterialEquipItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithEvolveSelectMaterialEquipItem : EquipSelectBase
{
  private uint equipId;
  private int needLv;
  private int equipIndex;
  private ulong[] selectedUniqueId;
  private EquipItemTable.EquipItemData data;
  private Transform detailBase;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.equipId = (uint) eventData[0];
    this.needLv = (int) eventData[1];
    this.selectedUniqueId = (ulong[]) eventData[2];
    this.equipIndex = (int) eventData[3];
    this.data = Singleton<EquipItemTable>.I.GetEquipItemData(this.equipId);
    this.InitializeCaption(this.data.IsWeapon() ? this.sectionData.GetText("CAPTION_WEAPON") : this.sectionData.GetText("CAPTION_DEFENCE"));
    base.Initialize();
  }

  protected override void OnOpen() => this.InitLocalInventory();

  public override void UpdateUI()
  {
    this.detailBase = this.GetCtrl((Enum) SmithEvolveSelectMaterialEquipItem.UI.OBJ_INFO_ROOT);
    if (Object.op_Inequality((Object) this.detailBase, (Object) null))
    {
      this.SetFontStyle(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.STR_TITLE, (FontStyle) 2);
      this.SetFontStyle(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.STR_SELL, (FontStyle) 2);
      this.SetFontStyle(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.STR_NEED, (FontStyle) 2);
      this.SetFontStyle(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.STR_HAVE, (FontStyle) 2);
      this.SetLabelText(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.LBL_NAME, this.data.name);
      ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
      {
        icon_type = ItemIcon.GetItemIconType(this.data.type),
        icon_id = this.data.GetIconID(),
        rarity = new RARITY_TYPE?(this.data.rarity),
        parent = this.FindCtrl(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.OBJ_ICON_ROOT),
        element = this.data.GetTargetElementPriorityToTable()
      }).SetEnableCollider(false);
      this.SetLabelText(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.LBL_NEED_LV, this.needLv.ToString());
      this.SetLabelText(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.LBL_HAVE_NUM, MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNum(this.equipId).ToString());
      this.SetLabelText(this.detailBase, (Enum) SmithEvolveSelectMaterialEquipItem.UI.LBL_SELL, this.data.sale.ToString());
    }
    this.LocalInventory();
  }

  private void InitializeCaption(string caption)
  {
    Transform ctrl = this.GetCtrl((Enum) SmithEvolveSelectMaterialEquipItem.UI.OBJ_CAPTION_3);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    this.SetLabelText(ctrl, (Enum) SmithEvolveSelectMaterialEquipItem.UI.LBL_CAPTION, caption);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected override void InitSort()
  {
    if (MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType))
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.WEAPON, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
    else
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ARMOR, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
  }

  protected override void InitLocalInventory()
  {
    List<EquipItemInfo> inventory = new List<EquipItemInfo>();
    MonoBehaviourSingleton<InventoryManager>.I.ForAllEquipItemInventory((Action<EquipItemInfo>) (item =>
    {
      for (int index = 0; index < this.selectedUniqueId.Length; ++index)
      {
        if ((long) this.selectedUniqueId[index] == (long) item.uniqueID)
          return;
      }
      if ((int) item.tableID != (int) this.equipId)
        return;
      inventory.Add(item);
    }));
    this.localInventoryEquipData = (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(inventory.ToArray());
  }

  protected override void LocalInventory()
  {
    this.SetupEnableInventoryUI();
    if (this.localInventoryEquipData == null)
      return;
    this.SetLabelText((Enum) SmithEvolveSelectMaterialEquipItem.UI.LBL_SORT, this.sortSettings.GetSortLabel());
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    this.SetDynamicList((Enum) this.InventoryUI, (string) null, this.localInventoryEquipData.Length + 1, false, (Func<int, bool>) (i =>
    {
      if (i == 0)
        return true;
      SortCompareData sortCompareData = this.localInventoryEquipData[i - 1];
      return sortCompareData != null && sortCompareData.IsPriority(this.sortSettings.orderTypeAsc);
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i == 0)
      {
        this.CreateRemoveIcon(t, "SELECT", -1, is_select: this.selectInventoryIndex == -1, name: this.sectionData.GetText("STR_DETACH"));
      }
      else
      {
        int index = i - 1;
        uint tableId = this.localInventoryEquipData[index].GetTableID();
        if (tableId == 0U)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetActive(t, true);
          Singleton<EquipItemTable>.I.GetEquipItemData(tableId);
          EquipItemSortData equipItemSortData = this.localInventoryEquipData[index] as EquipItemSortData;
          EquipItemInfo itemData = equipItemSortData.GetItemData() as EquipItemInfo;
          bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(equipItemSortData.GetIconType(), equipItemSortData.GetUniqID());
          SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(itemData);
          int equip_index = equipItemSortData.IsEquipping() ? 0 : -1;
          ItemIcon itemIconDetail = this.CreateItemIconDetail(equipItemSortData, skillSlotData, this.IsShowMainStatus, t, "SELECT", i - 1, is_new: is_new, equip_index: equip_index);
          itemIconDetail.SetItemID(equipItemSortData.GetTableID());
          itemIconDetail.SetGrayout(itemData.level < this.needLv);
          object[] event_data = new object[2]
          {
            (object) ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE,
            (object) itemData
          };
          this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) event_data);
          if (Object.op_Inequality((Object) itemIconDetail, (Object) null) && equipItemSortData != null)
            itemIconDetail.SetInitData((SortCompareData) equipItemSortData);
          if (!Object.op_Inequality((Object) itemIconDetail, (Object) null) || this.m_generatedIconList.Contains(itemIconDetail))
            return;
          this.m_generatedIconList.Add(itemIconDetail);
        }
      }
    }));
  }

  protected override void EquipParam()
  {
  }

  protected override int GetSelectItemIndex()
  {
    for (int selectItemIndex = 0; selectItemIndex < this.localInventoryEquipData.Length; ++selectItemIndex)
    {
      if ((long) this.selectedUniqueId[this.equipIndex] == (long) this.localInventoryEquipData[selectItemIndex].GetUniqID())
        return selectItemIndex;
    }
    return -1;
  }

  private void OnQuery_SECTION_BACK() => GameSection.SetEventData((object) this.selectedUniqueId);

  private void OnQuery_SELECT()
  {
    this.selectInventoryIndex = (int) GameSection.GetEventData();
    if (this.selectInventoryIndex == -1)
    {
      this.selectedUniqueId[this.equipIndex] = 0UL;
    }
    else
    {
      EquipItemSortData equipItemSortData = this.localInventoryEquipData[this.selectInventoryIndex] as EquipItemSortData;
      if (equipItemSortData.IsFavorite())
      {
        GameSection.ChangeEvent("NOT_SELECT_FAVORITE");
        return;
      }
      if (equipItemSortData.IsEquipping())
      {
        GameSection.ChangeEvent("NOT_SELECT_EQUIPPING");
        return;
      }
      if (equipItemSortData.GetLevel() < this.needLv)
      {
        GameSection.ChangeEvent("NOT_SELECT_LOW_LEVEL");
        return;
      }
      this.selectedUniqueId[this.equipIndex] = equipItemSortData.GetUniqID();
    }
    GameSection.SetEventData((object) this.selectedUniqueId);
  }

  protected override void OnQueryDetail()
  {
  }

  protected override bool sorting()
  {
    this.InitLocalInventory();
    return true;
  }

  protected void OnCloseDialog_SmithSelectEquipSort() => this.OnCloseSortDialog();

  public new enum UI
  {
    OBJ_CAPTION_3,
    LBL_CAPTION,
    OBJ_INFO_ROOT,
    OBJ_ICON_ROOT,
    STR_TITLE,
    LBL_NAME,
    STR_SELL,
    LBL_SELL,
    SPR_COIN,
    SPR_SELL_BG,
    SPR_NEED,
    STR_NEED,
    LBL_NEED_LV,
    SPR_HAVE,
    STR_HAVE,
    LBL_HAVE_NUM,
    BTN_CHANGE,
    TGL_CHANGE_INVENTORY,
    LBL_ICON_DISP,
    SPR_SMALL_ICON,
    BTN_SORT,
    ICON_DESC,
    TGL_ICON_ASC,
    LBL_SORT,
    OBJ_EQUIP_WINDOW,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    OBJ_BACK,
    BTN_BACK,
  }
}
