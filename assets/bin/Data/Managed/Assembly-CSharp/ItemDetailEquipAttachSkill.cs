// Decompiled with JetBrains decompiler
// Type: ItemDetailEquipAttachSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemDetailEquipAttachSkill : SkillInfoBase
{
  private ItemDetailEquip.CURRENT_SECTION callSection;
  private object eventData;
  private object equipData;
  private bool isSkillUniqItem;
  private SkillSlotUIData[] slotData;
  private int selectIndex;
  private bool lookOnly = true;
  private int sex;

  public override void Initialize()
  {
    this.selectIndex = -1;
    object[] eventData = GameSection.GetEventData() as object[];
    this.callSection = (ItemDetailEquip.CURRENT_SECTION) eventData[0];
    this.eventData = eventData[1];
    this.sex = -1;
    if (eventData.Length > 2)
      this.sex = (int) eventData[2];
    if (this.sex == -1)
      this.sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE || this.callSection == ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)
    {
      this.equipData = (object) (this.eventData as EquipItemTable.EquipItemData);
      this.slotData = this.GetSkillSlotData(this.eventData as EquipItemTable.EquipItemData, 0);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_ATTACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_DETACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_GROW, false);
    }
    else if (this.callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE)
    {
      EquipItemAndSkillData eventData = this.eventData as EquipItemAndSkillData;
      this.equipData = (object) eventData.equipItemInfo.tableData;
      this.slotData = eventData.skillSlotUIData;
      this.isSkillUniqItem = true;
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_ATTACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_DETACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_GROW, false);
    }
    else if (this.callSection == ItemDetailEquip.CURRENT_SECTION.QUEST_ROOM)
    {
      this.equipData = (object) (this.eventData as EquipItemInfo);
      this.slotData = this.GetSkillSlotData(this.eventData as EquipItemInfo);
      this.isSkillUniqItem = true;
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_ATTACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_DETACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_GROW, false);
    }
    else if (this.callSection == ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT)
    {
      EquipItemAndSkillData eventData = this.eventData as EquipItemAndSkillData;
      this.equipData = (object) eventData.equipItemInfo;
      this.slotData = eventData.skillSlotUIData;
      this.isSkillUniqItem = true;
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_ATTACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_DETACH, false);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_GROW, false);
    }
    else
    {
      this.equipData = (object) (this.eventData as EquipItemInfo);
      this.slotData = this.GetSkillSlotData(this.eventData as EquipItemInfo);
      this.isSkillUniqItem = true;
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_ATTACH, true);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_DETACH, true);
      this.SetActive((Enum) ItemDetailEquipAttachSkill.UI.BTN_GROW, true);
      this.lookOnly = false;
    }
    this.SetToggle((Enum) ItemDetailEquipAttachSkill.UI.TGL_WINDOW_TITLE, this.lookOnly);
    if (this.slotData == null)
      return;
    Transform table_item = (Transform) null;
    this.SetTable((Enum) ItemDetailEquipAttachSkill.UI.TBL_SKILL_LIST, "EquipSetDetailTopItem", 1, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      table_item = t;
      EquipItemTable.EquipItemData table = (EquipItemTable.EquipItemData) null;
      int num1 = 1;
      if (this.callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE || this.callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE || this.callSection == ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)
      {
        table = this.equipData as EquipItemTable.EquipItemData;
      }
      else
      {
        EquipItemInfo equipData = this.equipData as EquipItemInfo;
        table = equipData.tableData;
        num1 = equipData.level;
        int exceed = equipData.exceed;
      }
      ItemIcon.CreateEquipItemIconByEquipItemTable(table, this.sex, this.FindCtrl(t, (Enum) ItemDetailEquipAttachSkill.UI.OBJ_ICON_ROOT)).SetEnableCollider(false);
      this.SetActive(t, (Enum) ItemDetailEquipAttachSkill.UI.SPR_EQUIP_INDEX_ICON, false);
      string text = Utility.TrimText(table.name, ((Component) this.GetCtrl((Enum) ItemDetailEquipAttachSkill.UI.LBL_EQUIP_NAME)).GetComponent<UILabel>());
      this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EQUIP_NAME, text);
      this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EQUIP_NOW_LV, num1.ToString());
      this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EQUIP_MAX_LV, table.maxLv.ToString());
      this.SetGrid(t, (Enum) ItemDetailEquipAttachSkill.UI.GRD_ATTACH_SKILL, "EquipSetDetailTopItem2", this.slotData.Length, true, (Action<int, Transform, bool>) ((i2, t2, is_recycle2) =>
      {
        int event_data = i2;
        SkillItemInfo skillItemInfo = this.slotData[i2].itemData;
        bool is_attached = skillItemInfo != null && skillItemInfo.tableData.type == this.slotData[i2].slotData.slotType;
        this.SetSkillIcon(t2, (Enum) ItemDetailEquipAttachSkill.UI.TEX_SKILL_ICON, this.slotData[i2].slotData.slotType, is_attached, false);
        this.SetEvent(t2, is_attached ? "SLOT_DETAIL" : "SLOT", event_data);
        this.SetLongTouch(t2, "SLOT_DETAIL", (object) event_data);
        if (!is_attached)
          skillItemInfo = (SkillItemInfo) null;
        this.SetToggle(t2, (Enum) ItemDetailEquipAttachSkill.UI.TGL_ACTIVE_OBJ, skillItemInfo != null);
        this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME, true);
        this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME_NOT_ENABLE_TYPE, false);
        if (skillItemInfo == null)
        {
          this.SetLabelText(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME, this.sectionData.GetText("EMPTY_SLOT"));
          this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.SPR_ENABLE_WEAPON_TYPE, false);
        }
        else
        {
          SkillItemTable.SkillItemData tableData = skillItemInfo.tableData;
          this.SetLabelText(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME, tableData.name);
          this.SetLabelText(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME_NOT_ENABLE_TYPE, tableData.name);
          this.SetLabelText(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NOW_LV, skillItemInfo.level.ToString());
          this.SetLabelText(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_MAX_LV, skillItemInfo.tableData.GetMaxLv(skillItemInfo.exceedCnt).ToString());
          this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EX_LV, skillItemInfo.IsExceeded());
          this.SetLabelText(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EX_LV, StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) skillItemInfo.exceedCnt));
          EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
          this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.HasValue);
          if (!enableEquipType.HasValue)
            return;
          bool flag = enableEquipType.Value == table.type;
          this.SetSkillEquipIconKind(t2, (Enum) ItemDetailEquipAttachSkill.UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.Value, flag);
          this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME, flag);
          this.SetActive(t2, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME_NOT_ENABLE_TYPE, !flag);
        }
      }));
      this.GetComponent<UITable>(t, (Enum) ItemDetailEquipAttachSkill.UI.TBL_SPACE).Reposition();
      float y = t.localPosition.y;
      float num2 = this.FindCtrl(t, (Enum) ItemDetailEquipAttachSkill.UI.OBJ_SPACE).localPosition.y + this.FindCtrl(t, (Enum) ItemDetailEquipAttachSkill.UI.TBL_SPACE).localPosition.y;
      Vector3 localPosition = this.FindCtrl(t, (Enum) ItemDetailEquipAttachSkill.UI.SPR_EQUIP_INDEX_ICON).localPosition;
      localPosition.y = (float) (((double) num2 - (double) y) * 0.5);
      this.FindCtrl(t, (Enum) ItemDetailEquipAttachSkill.UI.SPR_EQUIP_INDEX_ICON).localPosition = localPosition;
    }));
    Transform ctrl1 = this.FindCtrl(table_item, (Enum) ItemDetailEquipAttachSkill.UI.OBJ_SPACE_COLLISION);
    Transform ctrl2 = this.GetCtrl((Enum) ItemDetailEquipAttachSkill.UI.OBJ_ANCHOR_BOTTOM);
    if (!Object.op_Inequality((Object) ctrl1, (Object) null) || !Object.op_Inequality((Object) ctrl2, (Object) null))
      return;
    this.UpdateAnchors();
    ctrl2.position = ctrl1.position;
    this.UpdateAnchors();
  }

  private void EmptySlot(Transform t, int slot_type)
  {
    this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NAME, this.sectionData.GetText("EMPTY_SLOT"));
    this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_NOW_LV, string.Empty);
    this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_MAX_LV, string.Empty);
    this.SetActive(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EX_LV, false);
    this.SetLabelText(t, (Enum) ItemDetailEquipAttachSkill.UI.LBL_EX_LV, string.Empty);
  }

  private void OnQuery_SLOT()
  {
    this.selectIndex = (int) GameSection.GetEventData();
    if (this.lookOnly || this.selectIndex < 0)
      GameSection.StopEvent();
    else
      GameSection.ChangeEvent("ATTACH", (object) new object[4]
      {
        (object) this.callSection,
        (object) this.slotData[this.selectIndex].itemData,
        (object) (this.eventData as EquipItemInfo),
        (object) this.selectIndex
      });
  }

  private void OnQuery_SLOT_DETAIL()
  {
    this.selectIndex = (int) GameSection.GetEventData();
    ItemDetailEquip.CURRENT_SECTION currentSection = this.callSection;
    if (currentSection != ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT)
      currentSection = ItemDetailEquip.CURRENT_SECTION.STATUS_SKILL_LIST;
    bool flag = false;
    if (this.isSkillUniqItem)
    {
      SkillItemInfo itemData = this.slotData[this.selectIndex].itemData;
      if (itemData != null)
      {
        SkillItemSortData skillItemSortData = new SkillItemSortData();
        skillItemSortData.SetItem((object) itemData);
        EquipItemInfo eventData = this.eventData as EquipItemInfo;
        GameSection.SetEventData((object) new object[4]
        {
          (object) currentSection,
          (object) skillItemSortData,
          (object) eventData,
          (object) this.selectIndex
        });
      }
      else
        flag = true;
    }
    else if (this.slotData[this.selectIndex].slotData.skill_id != 0U)
    {
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(this.slotData[this.selectIndex].slotData.skill_id);
      GameSection.SetEventData((object) new object[2]
      {
        (object) currentSection,
        (object) skillItemData
      });
    }
    else
      flag = true;
    if (!flag)
      return;
    GameSection.StopEvent();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE)) != (GameSection.NOTIFY_FLAG) 0 && this.eventData is EquipItemInfo eventData)
      this.eventData = (object) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(eventData.uniqueID);
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
  }

  public enum UI
  {
    TGL_WINDOW_TITLE,
    TBL_SKILL_LIST,
    BTN_DETACH,
    BTN_ATTACH,
    BTN_GROW,
    OBJ_ANCHOR_BOTTOM,
    SPR_EQUIP_INDEX_ICON,
    OBJ_ICON_ROOT,
    LBL_EQUIP_NAME,
    LBL_EQUIP_NOW_LV,
    LBL_EQUIP_MAX_LV,
    GRD_ATTACH_SKILL,
    TBL_SPACE,
    OBJ_SPACE,
    OBJ_ITEM_ANCHOR_D,
    OBJ_SPACE_COLLISION,
    TGL_ACTIVE_OBJ,
    LBL_NAME,
    LBL_NAME_NOT_ENABLE_TYPE,
    LBL_NOW_LV,
    LBL_MAX_LV,
    LBL_EX_LV,
    TEX_SKILL_ICON,
    SPR_ENABLE_WEAPON_TYPE,
  }
}
