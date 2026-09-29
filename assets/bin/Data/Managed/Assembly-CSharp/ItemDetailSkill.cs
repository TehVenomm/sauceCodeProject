// Decompiled with JetBrains decompiler
// Type: ItemDetailSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailSkill : SkillInfoBase
{
  protected ItemDetailEquip.CURRENT_SECTION callSection;
  protected object itemData;
  protected Transform detailBase;
  private EquipItemInfo equipInfo;
  private int slotIndex;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.callSection = (ItemDetailEquip.CURRENT_SECTION) eventData[0];
    this.itemData = eventData[1];
    if (eventData.Length > 2)
    {
      this.equipInfo = eventData[2] as EquipItemInfo;
      this.slotIndex = (int) eventData[3];
    }
    if (this.itemData is SortCompareData itemData1)
      this.itemData = (object) (itemData1.GetItemData() as SkillItemInfo);
    if (this.itemData is SkillItemInfo itemData2)
      GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, itemData2.uniqueID);
    bool is_visible = this.equipInfo != null;
    this.SetActive((Enum) ItemDetailSkill.UI.BTN_CHANGE, is_visible);
    this.SetActive((Enum) ItemDetailSkill.UI.BTN_GROW, ItemDetailEquip.CanSmithSection(this.callSection) && itemData2 != null && !itemData2.IsLevelMax());
    this.SetActive((Enum) ItemDetailSkill.UI.BTN_SELL, MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene() && !is_visible);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetupDetailBase();
    SkillItemInfo itemData1 = this.itemData as SkillItemInfo;
    SkillItemTable.SkillItemData itemData2 = this.itemData as SkillItemTable.SkillItemData;
    if (itemData1 != null)
      this.SkillParam(itemData1);
    else if (itemData2 != null)
      this.SkillTableParam(itemData2);
    else
      this.NotDataEquipParam();
  }

  protected virtual void SetupDetailBase()
  {
    this.detailBase = this.SetPrefab(this.GetCtrl((Enum) ItemDetailSkill.UI.OBJ_DETAIL_ROOT), "ItemDetailSkillBase");
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailSkill.UI.STR_TITLE_ITEM_INFO, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailSkill.UI.STR_TITLE_DESCRIPTION, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailSkill.UI.STR_TITLE_STATUS, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailSkill.UI.STR_TITLE_SELL, (FontStyle) 2);
  }

  private void SkillParam(SkillItemInfo item)
  {
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_SUB_STATUS, true);
    SkillItemTable.SkillItemData tableData = item.tableData;
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_NAME, tableData.name);
    this.SkillCompareParam(item, this.GetCompareItem());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_SELL, item.sellPrice.ToString());
    this.SetSupportEncoding((Enum) ItemDetailSkill.UI.LBL_DESCRIPTION, true);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DESCRIPTION, item.GetExplanationText(true));
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_FAVORITE_ROOT, (this.callSection & (ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE | ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_MATERIAL | ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT | ItemDetailEquip.CURRENT_SECTION.UI_PARTS | ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)) == ItemDetailEquip.CURRENT_SECTION.NONE);
    this.ResetTween(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_FAVORITE);
    this.ResetTween(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_UNFAVORITE);
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_UNFAVORITE, !item.isFavorite);
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_FAVORITE, item.isFavorite);
    if (item.IsLevelMax())
      this.SetProgressInt(this.detailBase, (Enum) ItemDetailSkill.UI.PRG_EXP_BAR, item.exceedExp, item.exceedExpPrev, item.exceedExpNext);
    else
      this.SetProgressInt(this.detailBase, (Enum) ItemDetailSkill.UI.PRG_EXP_BAR, item.exp, item.expPrev, item.expNext);
    this.SetSkillSlotTypeIcon(this.detailBase, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON_BG, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON_RARITY, tableData);
    this.SetRenderSkillItemModel((Enum) ItemDetailSkill.UI.TEX_MODEL, tableData.id);
    this.SetRenderSkillItemSymbolModel((Enum) ItemDetailSkill.UI.TEX_INNER_MODEL, tableData.id);
  }

  private void SkillTableParam(SkillItemTable.SkillItemData table_data)
  {
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_SUB_STATUS, true);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_NAME, table_data.name);
    int level = 1;
    if (this.callSection == ItemDetailEquip.CURRENT_SECTION.SHOP_TOP)
      level = table_data.GetMaxLv(0);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_NOW, level.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_MAX, table_data.GetMaxLv(0).ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_ATK, table_data.baseAtk.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DEF, table_data.baseDef.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_HP, table_data.baseHp.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_SELL, table_data.baseSell.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DESCRIPTION, table_data.GetExplanationText(level));
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_FAVORITE_ROOT, false);
    this.SetRenderSkillItemModel((Enum) ItemDetailSkill.UI.TEX_MODEL, table_data.id);
    this.SetRenderSkillItemSymbolModel((Enum) ItemDetailSkill.UI.TEX_INNER_MODEL, table_data.id);
    this.SetProgressInt(this.detailBase, (Enum) ItemDetailSkill.UI.PRG_EXP_BAR, 0);
    this.SetSkillSlotTypeIcon(this.detailBase, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON_BG, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON_RARITY, table_data);
  }

  private void NotDataEquipParam()
  {
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_SUB_STATUS, false);
    this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_FAVORITE_ROOT, false);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_NAME, this.sectionData.GetText("EMPTY"));
    this.SetProgressInt(this.detailBase, (Enum) ItemDetailSkill.UI.PRG_EXP_BAR, 0);
    this.SetSkillSlotTypeIcon(this.detailBase, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON_BG, (Enum) ItemDetailSkill.UI.SPR_SKILL_TYPE_ICON_RARITY, (SkillItemTable.SkillItemData) null);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DESCRIPTION, string.Empty);
    this.ClearRenderModel((Enum) ItemDetailSkill.UI.TEX_MODEL);
    this.ClearRenderModel((Enum) ItemDetailSkill.UI.TEX_INNER_MODEL);
    this.SkillCompareParam((SkillItemInfo) null, this.GetCompareItem());
  }

  private void SkillCompareParam(SkillItemInfo item, SkillItemInfo compare_item)
  {
    if (item != null)
    {
      this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_NOW, item.level.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_MAX, item.GetMaxLevel().ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_EX, item.exceedCnt.ToString());
      this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.OBJ_LV_EX, item.IsExceeded());
      if (compare_item != null)
      {
        this.SetLabelCompareParam(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_ATK, item.atk, compare_item.atk);
        this.SetLabelCompareParam(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DEF, item.def, compare_item.def);
        this.SetLabelCompareParam(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_HP, item.hp, compare_item.hp);
      }
      else
      {
        this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_ATK, item.atk.ToString());
        this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DEF, item.def.ToString());
        this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_HP, item.hp.ToString());
      }
    }
    else
    {
      string text = this.sectionData.GetText("NON_DATA");
      this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_NOW, text);
      this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_LV_MAX, text);
      if (compare_item != null)
      {
        this.SetLabelCompareParam(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_ATK, -compare_item.atk, 0, 0);
        this.SetLabelCompareParam(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DEF, -compare_item.def, 0, 0);
        this.SetLabelCompareParam(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_HP, -compare_item.hp, 0, 0);
      }
      else
      {
        this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_ATK, 0.ToString());
        this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_DEF, 0.ToString());
        this.SetLabelText(this.detailBase, (Enum) ItemDetailSkill.UI.LBL_HP, 0.ToString());
      }
    }
  }

  protected virtual SkillItemInfo GetCompareItem() => (SkillItemInfo) null;

  protected void OnQuery_SWITCH_FAVORITE()
  {
    if (!(this.itemData is SkillItemInfo itemData))
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SendInventorySkillLock(itemData.uniqueID, (Action<bool, SkillItemInfo>) ((is_success, recv_item) =>
    {
      if (is_success)
      {
        if (recv_item.isFavorite)
        {
          this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_UNFAVORITE, false);
          this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_FAVORITE, true);
          this.ResetTween(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_FAVORITE);
          this.PlayTween(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_FAVORITE, callback: (EventDelegate.Callback) (() =>
          {
            GameSection.ChangeStayEvent("FAVORITE");
            GameSection.ResumeEvent(is_success);
          }));
        }
        else
        {
          this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_FAVORITE, false);
          this.SetActive(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_UNFAVORITE, true);
          this.ResetTween(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_UNFAVORITE);
          this.PlayTween(this.detailBase, (Enum) ItemDetailSkill.UI.TWN_UNFAVORITE, callback: (EventDelegate.Callback) (() =>
          {
            GameSection.ChangeStayEvent("RELEASE_FAVORITE");
            GameSection.ResumeEvent(is_success);
          }));
        }
        this.itemData = (object) recv_item;
      }
      else
        GameSection.ResumeEvent(is_success);
    }));
  }

  protected void OnQuery_SELL()
  {
    if (!(this.itemData is SkillItemInfo itemData))
      GameSection.StopEvent();
    else
      this.SellEvent(itemData);
  }

  private void OnQuery_CHANGE()
  {
    if (!(this.itemData is SkillItemInfo itemData))
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[4]
      {
        (object) this.callSection,
        (object) itemData,
        (object) this.equipInfo,
        (object) this.slotIndex
      });
  }

  private void OnQuery_GROW()
  {
    if (!(this.itemData is SkillItemInfo itemData))
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) itemData,
        null
      });
  }

  protected void SellEvent(SkillItemInfo skill)
  {
    SkillItemSortData skillItemSortData = new SkillItemSortData();
    skillItemSortData.SetItem((object) skill);
    if (!skillItemSortData.CanSale())
      GameSection.ChangeEvent("NOT_SALE_FAVORITE");
    else
      GameSection.ChangeEvent("SELL", (object) new object[2]
      {
        (object) ItemStorageTop.TAB_MODE.SKILL,
        (object) new List<SortCompareData>()
        {
          (SortCompareData) skillItemSortData
        }
      });
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE;
  }

  protected enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    OBJ_LV_EX,
    LBL_LV_EX,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_SELL,
    LBL_DESCRIPTION,
    OBJ_FAVORITE_ROOT,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_SUB_STATUS,
    SPR_SKILL_TYPE_ICON,
    SPR_SKILL_TYPE_ICON_BG,
    SPR_SKILL_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_DESCRIPTION,
    STR_TITLE_STATUS,
    STR_TITLE_SELL,
    PRG_EXP_BAR,
    OBJ_NEXT_EXP_ROOT,
    BTN_SELL,
    BTN_CHANGE,
    BTN_GROW,
  }
}
