// Decompiled with JetBrains decompiler
// Type: EquipSetDetailTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class EquipSetDetailTop : SkillInfoBase
{
  protected EquipItemAndSkillData[] equipAndSkill;
  private bool lookOnly;
  private int sex = -1;
  protected int selectSkillIndex = -1;
  protected int selectEquipIndex = -1;
  private ItemDetailEquip.CURRENT_SECTION callSection;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.callSection = (ItemDetailEquip.CURRENT_SECTION) eventData[0];
    this.equipAndSkill = eventData[1] as EquipItemAndSkillData[];
    this.lookOnly = (bool) eventData[2];
    this.sex = (int) eventData[3];
    int index1 = 3;
    int index2 = 4;
    EquipItemAndSkillData itemAndSkillData = this.equipAndSkill[index1];
    this.equipAndSkill[index1] = this.equipAndSkill[index2];
    this.equipAndSkill[index2] = itemAndSkillData;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetToggle((Enum) EquipSetDetailTop.UI.TGL_WINDOW_TITLE, this.lookOnly);
    this.SetActive((Enum) EquipSetDetailTop.UI.BTN_ATTACH, !this.lookOnly);
    this.SetActive((Enum) EquipSetDetailTop.UI.BTN_DETACH, !this.lookOnly);
    this.SetActive((Enum) EquipSetDetailTop.UI.BTN_GROW, !this.lookOnly);
    int length = this.equipAndSkill.Length;
    Transform ctrl1 = this.GetCtrl((Enum) EquipSetDetailTop.UI.TBL_SKILL_LIST);
    for (int index = 0; index < length; ++index)
    {
      if (index < ctrl1.childCount)
      {
        Transform child = ctrl1.GetChild(index);
        Transform ctrl2 = this.FindCtrl(child, (Enum) EquipSetDetailTop.UI.SPR_EQUIP_INDEX_ICON);
        Vector3 localPosition = ctrl2.localPosition;
        localPosition.y = 0.0f;
        child.localPosition = ctrl2.localPosition = localPosition;
        this.FindCtrl(child, (Enum) EquipSetDetailTop.UI.GRD_ATTACH_SKILL).DestroyChildren();
        this.GetComponent<UITable>(child, (Enum) EquipSetDetailTop.UI.TBL_SPACE).Reposition();
      }
    }
    this.SetTable((Enum) EquipSetDetailTop.UI.TBL_SKILL_LIST, "EquipSetDetailTopItem", length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      EquipItemInfo item = this.equipAndSkill[i].equipItemInfo;
      if (item == null || item.GetMaxSlot() == 0)
      {
        this.SetActive(t, false);
      }
      else
      {
        ItemIcon.CreateEquipItemIconByEquipItemInfo(item, this.sex, this.FindCtrl(t, (Enum) EquipSetDetailTop.UI.OBJ_ICON_ROOT)).SetEnableCollider(false);
        this.SetEquipIndexIcon(t, (Enum) EquipSetDetailTop.UI.SPR_EQUIP_INDEX_ICON, i);
        this.SetLabelText(t, (Enum) EquipSetDetailTop.UI.LBL_EQUIP_NAME, item.tableData.name);
        this.SetLabelText(t, (Enum) EquipSetDetailTop.UI.LBL_EQUIP_NOW_LV, item.level.ToString());
        this.SetLabelText(t, (Enum) EquipSetDetailTop.UI.LBL_EQUIP_MAX_LV, item.tableData.maxLv.ToString());
        SkillSlotUIData[] slotData = this.equipAndSkill[i].skillSlotUIData;
        this.SetGrid(t, (Enum) EquipSetDetailTop.UI.GRD_ATTACH_SKILL, "EquipSetDetailTopItem2", item.GetMaxSlot(), true, (Action<int, Transform, bool>) ((i2, t2, is_recycle2) =>
        {
          int event_data = (i << 16 /*0x10*/) + i2;
          SkillItemInfo skillItemInfo = slotData[i2].itemData;
          bool is_attached = skillItemInfo != null && slotData[i2].slotData.skill_id != 0U && skillItemInfo.tableData.type == slotData[i2].slotData.slotType;
          this.SetSkillIcon(t2, (Enum) EquipSetDetailTop.UI.TEX_SKILL_ICON, slotData[i2].slotData.slotType, is_attached, false);
          if (!is_attached)
            skillItemInfo = (SkillItemInfo) null;
          this.SetToggle(t2, (Enum) EquipSetDetailTop.UI.TGL_ACTIVE_OBJ, skillItemInfo != null);
          this.SetActive(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME, true);
          this.SetActive(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME_NOT_ENABLE_TYPE, false);
          this.SetEvent(t2, is_attached ? "SLOT_DETAIL" : "SLOT", event_data);
          this.SetLongTouch(t2, "SLOT_DETAIL", (object) event_data);
          if (skillItemInfo == null)
          {
            this.SetLabelText(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME, this.sectionData.GetText("EMPTY_SLOT"));
            this.SetActive(t2, (Enum) EquipSetDetailTop.UI.SPR_ENABLE_WEAPON_TYPE, false);
          }
          else
          {
            SkillItemTable.SkillItemData tableData = skillItemInfo.tableData;
            this.SetLabelText(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME, tableData.name);
            this.SetLabelText(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME_NOT_ENABLE_TYPE, tableData.name);
            this.SetLabelText(t2, (Enum) EquipSetDetailTop.UI.LBL_NOW_LV, skillItemInfo.level.ToString());
            this.SetLabelText(t2, (Enum) EquipSetDetailTop.UI.LBL_MAX_LV, skillItemInfo.tableData.GetMaxLv(skillItemInfo.exceedCnt).ToString());
            bool is_visible = skillItemInfo.IsExceeded();
            this.SetActive(t2, (Enum) EquipSetDetailTop.UI.LBL_EX_LV, is_visible);
            if (is_visible)
            {
              this.SetSupportEncoding(t2, (Enum) EquipSetDetailTop.UI.LBL_EX_LV, true);
              this.SetLabelText(t2, (Enum) EquipSetDetailTop.UI.LBL_EX_LV, UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) skillItemInfo.exceedCnt), ExceedSkillItemTable.color));
            }
            EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
            this.SetActive(t2, (Enum) EquipSetDetailTop.UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.HasValue);
            if (!enableEquipType.HasValue)
              return;
            bool flag = enableEquipType.Value == item.tableData.type;
            this.SetSkillEquipIconKind(t2, (Enum) EquipSetDetailTop.UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.Value, flag);
            this.SetActive(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME, flag);
            this.SetActive(t2, (Enum) EquipSetDetailTop.UI.LBL_NAME_NOT_ENABLE_TYPE, !flag);
          }
        }));
      }
      this.GetComponent<UITable>(t, (Enum) EquipSetDetailTop.UI.TBL_SPACE).Reposition();
      float y = t.localPosition.y;
      float num = this.FindCtrl(t, (Enum) EquipSetDetailTop.UI.OBJ_SPACE).localPosition.y + this.FindCtrl(t, (Enum) EquipSetDetailTop.UI.TBL_SPACE).localPosition.y;
      Vector3 localPosition = this.FindCtrl(t, (Enum) EquipSetDetailTop.UI.SPR_EQUIP_INDEX_ICON).localPosition;
      localPosition.y = (float) (((double) num - (double) y) * 0.5);
      this.FindCtrl(t, (Enum) EquipSetDetailTop.UI.SPR_EQUIP_INDEX_ICON).localPosition = localPosition;
    }));
  }

  protected virtual void OnQuery_SLOT()
  {
    int eventData = (int) GameSection.GetEventData();
    this.selectEquipIndex = eventData >> 16 /*0x10*/;
    this.selectSkillIndex = eventData % 65536 /*0x010000*/;
    if (this.lookOnly || this.selectSkillIndex < 0 || this.selectEquipIndex < 0)
      GameSection.StopEvent();
    else if (MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount() <= 0)
    {
      GameSection.ChangeEvent("NOT_HAVE_SKILL_ITEM");
    }
    else
    {
      GameSection.ChangeEvent("ATTACH");
      EquipItemInfo equipItemInfo = this.equipAndSkill[this.selectEquipIndex].equipItemInfo;
      SkillItemInfo skillItemInfo = equipItemInfo.GetSkillItem(this.selectSkillIndex);
      if (StatusManager.IsUnique())
        skillItemInfo = equipItemInfo.GetUniqueSkillItem(this.selectSkillIndex);
      GameSection.SetEventData((object) new object[4]
      {
        (object) this.callSection,
        (object) skillItemInfo,
        (object) equipItemInfo,
        (object) this.selectSkillIndex
      });
    }
  }

  private void OnQuery_SLOT_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    this.selectEquipIndex = eventData >> 16 /*0x10*/;
    this.selectSkillIndex = eventData % 65536 /*0x010000*/;
    if (this.lookOnly || this.selectSkillIndex < 0 || this.selectEquipIndex < 0)
    {
      GameSection.StopEvent();
    }
    else
    {
      SkillItemInfo itemData = this.equipAndSkill[this.selectEquipIndex].skillSlotUIData[this.selectSkillIndex].itemData;
      EquipItemInfo equipItemInfo = this.equipAndSkill[this.selectEquipIndex].equipItemInfo;
      if (itemData == null)
      {
        GameSection.StopEvent();
      }
      else
      {
        SkillItemSortData skillItemSortData = new SkillItemSortData();
        skillItemSortData.SetItem((object) itemData);
        GameSection.SetEventData((object) new object[4]
        {
          (object) ItemDetailEquip.CURRENT_SECTION.STATUS_SKILL_LIST,
          (object) skillItemSortData,
          (object) equipItemInfo,
          (object) this.selectSkillIndex
        });
      }
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE)) != (GameSection.NOTIFY_FLAG) 0)
      Array.ForEach<EquipItemAndSkillData>(this.equipAndSkill, (Action<EquipItemAndSkillData>) (data =>
      {
        if (data == null || data.equipItemInfo == null)
          return;
        EquipItemInfo equipItem = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(data.equipItemInfo.uniqueID);
        data.skillSlotUIData = this.GetSkillSlotData(equipItem);
      }));
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
    SPR_EQUIP_INDEX_ICON,
    OBJ_ICON_ROOT,
    LBL_EQUIP_NAME,
    LBL_EQUIP_NOW_LV,
    LBL_EQUIP_MAX_LV,
    GRD_ATTACH_SKILL,
    TBL_SPACE,
    OBJ_SPACE,
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
