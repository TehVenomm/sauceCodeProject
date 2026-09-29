// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SmithGrowSkillSelect : SkillSelectBaseSecond
{
  private int preInventoryDataSize;

  public override void Initialize()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
      null,
      null
    });
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetVisibleEmptySkillType(false);
    this.SetActive((Enum) SmithGrowSkillSelect.UI.BTN_DECISION, false);
    this.SetActive((Enum) SmithGrowSkillSelect.UI.BTN_SKILL_DECISION, true);
    this.SetLabelText((Enum) SmithGrowSkillSelect.UI.STR_SKILL_DECISION, this.sectionData.GetText("STR_DECISION"));
    this.SetLabelText((Enum) SmithGrowSkillSelect.UI.STR_SKILL_DECISION_R, this.sectionData.GetText("STR_DECISION"));
  }

  protected override void SetInventoryIsEmptyParam() => this.isVisibleEmptySkill = false;

  protected override ItemStorageTop.SkillItemInventory CreateInventory()
  {
    return new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.GROW_BASE_SKILL_ITEM);
  }

  protected override void OnOpen()
  {
    this.SetEnabledUIModelRenderTexture((Enum) SmithGrowSkillSelect.UI.TEX_MODEL, true);
    this.SetEnabledUIModelRenderTexture((Enum) SmithGrowSkillSelect.UI.TEX_INNER_MODEL, true);
    base.OnOpen();
  }

  protected override void OnClose()
  {
    this.SetEnabledUIModelRenderTexture((Enum) SmithGrowSkillSelect.UI.TEX_MODEL, false);
    this.SetEnabledUIModelRenderTexture((Enum) SmithGrowSkillSelect.UI.TEX_INNER_MODEL, false);
    base.OnClose();
  }

  protected override void UpdateInventoryUI()
  {
    this.SetupEnableInventoryUI();
    bool reset = false;
    int length = this.inventory.datas.Length;
    if (this.preInventoryDataSize != length)
    {
      reset = true;
      this.preInventoryDataSize = length;
    }
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    this.SetDynamicList((Enum) this.inventoryUI, (string) null, length, reset, (Func<int, bool>) (i =>
    {
      SortCompareData data = this.inventory.datas[i];
      if (data == null || !data.IsPriority(this.inventory.sortSettings.orderTypeAsc) || !(data.GetItemData() is SkillItemInfo itemData2))
        return false;
      return !itemData2.IsLevelMax() || itemData2.IsEnableExceed();
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      SortCompareData data = this.inventory.datas[i];
      if (data == null || !data.IsPriority(this.inventory.sortSettings.orderTypeAsc) || !(data.GetItemData() is SkillItemInfo itemData4) || itemData4.IsLevelMax() && !itemData4.IsExistNextExceed())
        return;
      ITEM_ICON_TYPE iconType = data.GetIconType();
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, data.GetUniqID());
      bool isShowEnableExceed = itemData4.IsEnableExceed() && itemData4.exceedCnt == 0;
      bool isValidExceed = itemData4.IsEnableExceed();
      ItemIcon itemIconDetail = this.CreateItemIconDetail(iconType, data.GetIconID(), new RARITY_TYPE?(data.GetRarity()), data as SkillItemSortData, this.IsShowMainStatus, t, "SELECT", i, is_new, 100, this.selectIndex == i, data.IsEquipping(), isValidExceed, isShowEnableExceed);
      itemIconDetail.SetItemID(data.GetTableID());
      itemIconDetail.SetButtonColor(this.inventory.datas[i].IsPriority(this.inventory.sortSettings.orderTypeAsc), true);
      this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) i);
      if (Object.op_Inequality((Object) itemIconDetail, (Object) null) && data != null)
        itemIconDetail.SetInitData(data);
      if (this.m_generatedIconList.Contains(itemIconDetail))
        return;
      this.m_generatedIconList.Add(itemIconDetail);
    }));
  }

  private void SetEnabledUIModelRenderTexture(Enum ctrl_enum, bool enabled)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    if (!Object.op_Implicit((Object) ctrl))
      return;
    UIModelRenderTexture component = ((Component) ctrl).GetComponent<UIModelRenderTexture>();
    if (!Object.op_Implicit((Object) component))
      return;
    ((Behaviour) component).enabled = enabled;
    if (enabled)
      return;
    component.Clear();
  }

  protected override void OnDecision()
  {
    SkillItemInfo itemData = this.inventory.datas[this.selectIndex].GetItemData() as SkillItemInfo;
    if (itemData.IsLevelMax() && !itemData.IsExistNextExceed())
      Log.Error(LOG.GAMESCENE, "level max && max exceed");
    GameSection.ChangeEvent("DECISION", (object) new object[2]
    {
      this.inventory.datas[this.selectIndex].GetItemData(),
      null
    });
  }

  protected override object[] CreateDetailEventData(int index)
  {
    return new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
      (object) this.inventory.datas[index]
    };
  }

  private void OnCloseDialog_SmithSort() => this.OnCloseSort();

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_GROW | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.equipSkillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(this.inventory.datas[this.selectIndex].GetUniqID());
      this.selectSkillItem = (SkillItemInfo) null;
      this.updateInventory = true;
      MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate = true;
    }
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_SKILL_GROW | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    if (event_name == "SELECT")
    {
      ulong num = (ulong) event_data;
      int _data = 0;
      for (int length = this.inventory.datas.Length; _data < length; ++_data)
      {
        if ((long) this.inventory.datas[_data].GetUniqID() == (long) num)
          return new EventData(event_name, (object) _data);
      }
    }
    return base.CheckAutoEvent(event_name, event_data);
  }

  public void OnQuery_SECTION_BACK() => MonoBehaviourSingleton<SmithManager>.I.BackSection();

  protected new enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
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
    BTN_DECISION,
    STR_DECISION_R,
    BTN_SKILL_DECISION,
    STR_SKILL_DECISION,
    STR_SKILL_DECISION_R,
    OBJ_SKILL_INFO_ROOT,
    LBL_EQUIP_ITEM_NAME,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    LBL_SORT,
    BTN_BACK,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    BTN_CHANGE_INVENTORY,
    OBJ_EMPTY_SKILL_ROOT,
    TEX_EMPTY_SKILL,
    SPR_EMPTY_SKILL,
    LBL_EMPTY_SKILL_TYPE,
    OBJ_CAPTION_3,
    LBL_CAPTION,
  }
}
