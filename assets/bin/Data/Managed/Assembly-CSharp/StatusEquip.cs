// Decompiled with JetBrains decompiler
// Type: StatusEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class StatusEquip : EquipSelectBase
{
  protected EquipItemInfo migrationOldItem;
  protected EquipItemInfo migrationSelectItem;
  protected int migrationSendCount;
  protected EquipItemInfo detailItem;

  public StatusEquip.LocalEquipSetData selectEquipSetData { get; protected set; }

  public override void Initialize()
  {
    EquipItemInfo equippingItem = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItem();
    this.selectEquipSetData = GameSection.GetEventData() as StatusEquip.LocalEquipSetData;
    this.EquipItem = equippingItem;
    if (equippingItem == null)
    {
      GameSection.SetEventData((object) -1);
      this.SelectingInventoryFirst();
    }
    else
      this.selectInventoryIndex = this.GetSelectItemIndex();
    base.Initialize();
  }

  protected override void OnOpen()
  {
    base.OnOpen();
    this._OnOpenStatusStage();
  }

  protected virtual void _OnOpenStatusStage()
  {
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
    {
      MonoBehaviourSingleton<StatusStageManager>.I.SetEquipSetData(this.selectEquipSetData);
      MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(this.EquipItem);
    }
    MonoBehaviourSingleton<FilterManager>.I.StartBlur(MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionStartBlurTime, MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionBlurStrength, MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionBlurDelay);
  }

  public override void Close(UITransition.TYPE type = UITransition.TYPE.CLOSE)
  {
    base.Close(type);
    this._OnCloseStatusStage();
  }

  protected virtual void _OnCloseStatusStage()
  {
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetEquipSetData((StatusEquip.LocalEquipSetData) null);
    MonoBehaviourSingleton<FilterManager>.I.StopBlur(MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionEndTime);
  }

  protected override void InitSort()
  {
    bool isWeapon = MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType);
    if (isWeapon)
      this.sortSettings = SortSettings.CreateMemSortSettings(this.GetDialogType(isWeapon), SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
    else
      this.sortSettings = SortSettings.CreateMemSortSettings(this.GetDialogType(isWeapon), SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
  }

  protected virtual SortBase.DIALOG_TYPE GetDialogType(bool isWeapon)
  {
    return !isWeapon ? SortBase.DIALOG_TYPE.ARMOR : SortBase.DIALOG_TYPE.WEAPON;
  }

  protected override void InitLocalInventory()
  {
    MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
    this.localInventoryEquipData = this.CreateSortAry();
  }

  protected virtual SortCompareData[] CreateSortAry()
  {
    return (SortCompareData[]) this.sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
  }

  protected override void SelectingInventoryFirst() => this.selectInventoryIndex = -1;

  public override void UpdateUI() => base.UpdateUI();

  protected override string GetSelectTypeText()
  {
    string selectTypeText = string.Empty;
    switch (this.selectEquipSetData.index)
    {
      case 0:
        selectTypeText = this.sectionData.GetText("SELECT_WEAPON_1");
        break;
      case 1:
        selectTypeText = this.sectionData.GetText("SELECT_WEAPON_2");
        break;
      case 2:
        selectTypeText = this.sectionData.GetText("SELECT_WEAPON_3");
        break;
      case 3:
        selectTypeText = this.sectionData.GetText("SELECT_ARMOR");
        break;
      case 4:
        selectTypeText = this.sectionData.GetText("SELECT_HELM");
        break;
      case 5:
        selectTypeText = this.sectionData.GetText("SELECT_ARM");
        break;
      case 6:
        selectTypeText = this.sectionData.GetText("SELECT_LEG");
        break;
    }
    return selectTypeText;
  }

  protected override void EquipParam()
  {
    EquipItemInfo compareItemData = this.GetCompareItemData();
    EquipItemInfo select_item = this.EquipItem;
    EquipSetInfo set_info = new EquipSetInfo(this.selectEquipSetData.equipSetInfo.item, this.selectEquipSetData.equipSetInfo.name, this.selectEquipSetData.equipSetInfo.showHelm, this.selectEquipSetData.equipSetInfo.acc);
    bool flag = false;
    int index = Array.FindIndex<EquipItemInfo>(this.selectEquipSetData.equipSetInfo.item, (Predicate<EquipItemInfo>) (item => item != null && select_item != null && (long) item.uniqueID == (long) select_item.uniqueID));
    if (index != -1)
      flag = true;
    if (!flag)
    {
      set_info.item[this.selectEquipSetData.index] = select_item;
    }
    else
    {
      set_info.item[index] = compareItemData;
      set_info.item[this.selectEquipSetData.index] = select_item;
    }
    int _atk;
    int _def;
    int _hp;
    MonoBehaviourSingleton<StatusManager>.I.CalcSelfStatusParam(set_info, out _atk, out _def, out _hp, out int _, out int _);
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_STATUS_ATK, _atk.ToString());
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_STATUS_DEF, _def.ToString());
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_STATUS_HP, _hp.ToString());
    int atk1 = 0;
    int def1 = 0;
    int hp1 = 0;
    int atk2 = 0;
    int def2 = 0;
    int hp2 = 0;
    if (!flag)
    {
      this.CalcEquipAttachSkillStatus(compareItemData, out atk1, out def1, out hp1);
      this.CalcEquipAttachSkillStatus(select_item, out atk2, out def2, out hp2);
    }
    else
    {
      atk1 += atk2;
      def1 += def2;
      hp1 += hp2;
      atk2 = atk1;
      def2 = def1;
      hp2 = hp1;
    }
    if (this.selectEquipSetData.index != 1 && this.selectEquipSetData.index != 2)
    {
      int before_value1 = compareItemData != null ? compareItemData.atk + compareItemData.elemAtk + atk1 : 0;
      int after_value1 = select_item != null ? select_item.atk + select_item.elemAtk + atk2 : 0;
      int num1 = after_value1 - before_value1;
      string format1 = num1 > 0 ? this.sectionData.GetText("DISP_PLUS") : "{0}";
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_ATK, after_value1, before_value1, string.Format(format1, (object) num1));
      this.SetActive((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_ATK, num1 != 0);
      int before_value2 = compareItemData != null ? compareItemData.def + def1 : 0;
      int after_value2 = select_item != null ? select_item.def + def2 : 0;
      int num2 = after_value2 - before_value2;
      string format2 = num2 > 0 ? this.sectionData.GetText("DISP_PLUS") : "{0}";
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_DEF, after_value2, before_value2, string.Format(format2, (object) num2));
      this.SetActive((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_DEF, num2 != 0);
      int before_value3 = compareItemData != null ? compareItemData.hp + hp1 : 0;
      int after_value3 = select_item != null ? select_item.hp + hp2 : 0;
      int num3 = after_value3 - before_value3;
      string format3 = num3 > 0 ? this.sectionData.GetText("DISP_PLUS") : "{0}";
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_HP, after_value3, before_value3, string.Format(format3, (object) num3));
      this.SetActive((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_HP, num3 != 0);
    }
    else
    {
      int before_value4 = atk1;
      int after_value4 = atk2;
      int num4 = after_value4 - before_value4;
      string format4 = num4 > 0 ? this.sectionData.GetText("DISP_PLUS") : "{0}";
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_ATK, after_value4, before_value4, string.Format(format4, (object) num4));
      this.SetActive((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_ATK, num4 != 0);
      int before_value5 = def1;
      int after_value5 = def2;
      int num5 = after_value5 - before_value5;
      string format5 = num5 > 0 ? this.sectionData.GetText("DISP_PLUS") : "{0}";
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_DEF, after_value5, before_value5, string.Format(format5, (object) num5));
      this.SetActive((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_DEF, num5 != 0);
      int before_value6 = hp1;
      int after_value6 = hp2;
      int num6 = after_value6 - before_value6;
      string format6 = num6 > 0 ? this.sectionData.GetText("DISP_PLUS") : "{0}";
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_HP, after_value6, before_value6, string.Format(format6, (object) num6));
      this.SetActive((Enum) EquipSelectBase.UI.LBL_STATUS_ADD_HP, num6 != 0);
    }
    this.SetActive((Enum) EquipSelectBase.UI.OBJ_SELL_ROOT, false);
    if (select_item == null)
    {
      string text = this.sectionData.GetText("NON_DATA");
      if (compareItemData != null)
      {
        if (compareItemData.tableData.IsWeapon())
        {
          this.SetActive((Enum) EquipSelectBase.UI.OBJ_ELEM_ROOT, compareItemData.elemAtk > 0);
          this.SetElementSprite((Enum) EquipSelectBase.UI.SPR_ELEM, compareItemData.GetElemAtkType());
          this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ATK, -compareItemData.atk, 0, 0);
          this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ELEM, -compareItemData.elemAtk, 0, 0);
        }
        else
        {
          this.SetActive((Enum) EquipSelectBase.UI.OBJ_ELEM_ROOT, compareItemData.elemDef > 0);
          this.SetDefElementSprite((Enum) EquipSelectBase.UI.SPR_ELEM, compareItemData.GetElemDefType());
          this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_DEF, -compareItemData.def, 0, 0);
          this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ELEM, -compareItemData.elemDef, 0, 0);
        }
      }
      else
      {
        bool is_visible = this.selectEquipSetData.index < 3;
        this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_ATK, 0, 0);
        this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_DEF, 0, 0);
        this.SetActive((Enum) EquipSelectBase.UI.OBJ_ATK_ROOT, is_visible);
        this.SetActive((Enum) EquipSelectBase.UI.OBJ_DEF_ROOT, !is_visible);
        this.SetActive((Enum) EquipSelectBase.UI.OBJ_ELEM_ROOT, false);
      }
      this.SetLabelText((Enum) EquipSelectBase.UI.LBL_NAME, this.sectionData.GetText("EMPTY"));
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_LV_NOW, 0, 0, text);
      this.SetLabelCompareParam((Enum) EquipSelectBase.UI.LBL_LV_MAX, 0, 0, text);
      this.SetActive((Enum) EquipSelectBase.UI.OBJ_SKILL_BUTTON_ROOT, false);
      this.SetActive((Enum) EquipSelectBase.UI.TBL_ABILITY, false);
      this.SetActive((Enum) EquipSelectBase.UI.STR_NON_ABILITY, false);
      this.SetActive((Enum) EquipSelectBase.UI.SPR_IS_EVOLVE, false);
      this.SetEquipmentTypeIcon((Enum) EquipSelectBase.UI.SPR_TYPE_ICON, (Enum) EquipSelectBase.UI.SPR_TYPE_ICON_BG, (Enum) EquipSelectBase.UI.SPR_TYPE_ICON_RARITY, (EquipItemTable.EquipItemData) null);
    }
    else
    {
      this.SetActive((Enum) EquipSelectBase.UI.TBL_ABILITY, true);
      base.EquipParam();
    }
  }

  private void CalcEquipAttachSkillStatus(
    EquipItemInfo item,
    out int atk,
    out int def,
    out int hp)
  {
    int tmp_atk = 0;
    int tmp_def = 0;
    int tmp_hp = 0;
    if (item != null)
    {
      SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(item);
      if (skillSlotData != null)
        Array.ForEach<SkillSlotUIData>(skillSlotData, (Action<SkillSlotUIData>) (data =>
        {
          if (data == null || data.slotData.skill_id == 0U)
            return;
          ELEMENT_TYPE targetElement = item.GetTargetElement();
          int elem_atk = 0;
          if (item.tableData.IsWeapon())
          {
            switch (targetElement)
            {
              case ELEMENT_TYPE.MULTI:
                int index = 0;
                data.itemData.atkList.ForEach((Action<int>) (_elem_atk =>
                {
                  if (index > 0)
                    elem_atk += _elem_atk;
                  ++index;
                }));
                break;
              case ELEMENT_TYPE.MAX:
                break;
              default:
                elem_atk = data.itemData.atkList[(int) (targetElement + 1)];
                break;
            }
          }
          tmp_atk += data.itemData.atk + elem_atk;
          tmp_def += data.itemData.def;
          tmp_hp += data.itemData.hp;
        }));
    }
    atk = tmp_atk;
    def = tmp_def;
    hp = tmp_hp;
  }

  protected virtual bool IsCreateRemoveButton()
  {
    return this.selectEquipSetData.index != 0 && this.selectEquipSetData.index != 3;
  }

  protected override void LocalInventory()
  {
    this.SetupEnableInventoryUI();
    if (this.localInventoryEquipData == null)
      return;
    this.SetLabelText((Enum) EquipSelectBase.UI.LBL_SORT, this.sortSettings.GetSortLabel());
    bool created_remove_btn = false;
    EquipItemInfo equipping_item = this.GetCompareItemData();
    int find_index = -1;
    if (equipping_item != null)
    {
      find_index = Array.FindIndex<SortCompareData>(this.localInventoryEquipData, (Predicate<SortCompareData>) (data => (long) data.GetUniqID() == (long) equipping_item.uniqueID));
      if (find_index > -1 && (this.localInventoryEquipData[find_index] == null || !this.localInventoryEquipData[find_index].IsPriority(this.sortSettings.orderTypeAsc)))
        find_index = -1;
    }
    created_remove_btn = this.IsCreateRemoveButton();
    this.m_generatedIconList.Clear();
    this.UpdateNewIconInfo();
    this.SetDynamicList((Enum) this.InventoryUI, (string) null, this.localInventoryEquipData.Length + 2, false, (Func<int, bool>) (i =>
    {
      if (created_remove_btn && i == 0)
        return true;
      bool flag1 = false;
      bool flag2 = true;
      int index = i;
      if (created_remove_btn)
        --index;
      if (find_index >= 0)
      {
        if (index == 0)
          flag1 = true;
        else
          --index;
      }
      if (!flag1 && (index >= this.localInventoryEquipData.Length || find_index >= 0 && index == find_index))
        flag2 = false;
      if (flag2)
      {
        SortCompareData sortCompareData = this.localInventoryEquipData[index];
        if (sortCompareData == null || !sortCompareData.IsPriority(this.sortSettings.orderTypeAsc))
          flag2 = false;
      }
      return flag2;
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i == 0 & created_remove_btn)
      {
        this.CreateRemoveIcon(t, "TRY_ON", -1, 100, this.selectInventoryIndex == -1, this.sectionData.GetText("STR_DETACH"));
      }
      else
      {
        int event_data = i;
        if (created_remove_btn)
          --event_data;
        bool is_equip_now_slot = false;
        if (find_index >= 0)
        {
          if (event_data == 0)
          {
            event_data = find_index;
            is_equip_now_slot = true;
          }
          else
            --event_data;
        }
        this.SetActive(t, true);
        EquipItemSortData equipItemSortData = this.localInventoryEquipData[event_data] as EquipItemSortData;
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemSortData.GetTableID());
        int num = this.selectEquipSetData.EquippingIndexOf(equipItemSortData.equipData);
        if (num < 0)
        {
          int equipIndex = this.GetEquipIndex(equipItemSortData.equipData);
          num = equipIndex >= 0 ? 3 : equipIndex;
        }
        bool is_select = event_data == this.selectInventoryIndex;
        ITEM_ICON_TYPE iconType = equipItemSortData.GetIconType();
        SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(equipItemSortData.GetItemData() as EquipItemInfo);
        bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, equipItemSortData.GetUniqID());
        ItemIcon itemIconDetail;
        if (this.IsNotEquip(num == -1, is_equip_now_slot))
        {
          itemIconDetail = this.CreateItemIconDetail(equipItemSortData, skillSlotData, this.IsShowMainStatus, t, "TRY_ON", event_data, is_new: is_new, toggle_group: 100, is_select: is_select);
        }
        else
        {
          int equip_index = -1;
          if (num > -1 | is_equip_now_slot)
            equip_index = !equipItemData.IsWeapon() ? 0 : num + 1;
          if (equipItemSortData != null && equipping_item != null && (long) equipItemSortData.GetUniqID() == (long) equipping_item.uniqueID)
            equipItemSortData.SetItem((object) equipping_item);
          itemIconDetail = this.CreateItemIconDetail(equipItemSortData, skillSlotData, this.IsShowMainStatus, t, "TRY_ON", event_data, is_new: is_new, toggle_group: 100, is_select: is_select, equip_index: equip_index);
        }
        if (Object.op_Inequality((Object) itemIconDetail, (Object) null))
        {
          itemIconDetail.SetItemID(equipItemSortData.GetTableID());
          itemIconDetail.SetInitData((SortCompareData) equipItemSortData);
          if (!this.m_generatedIconList.Contains(itemIconDetail))
            this.m_generatedIconList.Add(itemIconDetail);
        }
        this.SetLongTouch(itemIconDetail.transform, "DETAIL", (object) event_data);
      }
    }));
  }

  protected virtual bool IsNotEquip(bool is_not_equip_any_slot, bool is_equip_now_slot)
  {
    return is_not_equip_any_slot;
  }

  protected override EquipItemInfo EquipItem
  {
    get => MonoBehaviourSingleton<StatusManager>.I.GetSelectEquipItem();
    set => MonoBehaviourSingleton<StatusManager>.I.SetSelectEquipItem(value);
  }

  protected override EquipItemInfo GetCompareItemData()
  {
    return MonoBehaviourSingleton<StatusManager>.I.GetEquippingItem();
  }

  protected override void OnQuery_TRY_ON()
  {
    this.selectInventoryIndex = (int) GameSection.GetEventData();
    if (this.selectInventoryIndex < 0)
      this.EquipItem = (EquipItemInfo) null;
    else if (this.localInventoryEquipData != null)
      this.EquipItem = this.localInventoryEquipData[this.selectInventoryIndex].GetItemData() as EquipItemInfo;
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(this.EquipItem);
    base.OnQuery_TRY_ON();
  }

  protected virtual void ChangeSelectItem(EquipItemInfo select_item, EquipItemInfo old_item)
  {
    GameSection.SetEventData((object) new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, this.selectEquipSetData.index, select_item));
    if (old_item == null || select_item == null)
    {
      this.TO_UNIQUE_OR_MAIN_STATUS();
    }
    else
    {
      bool flag = false;
      for (int slotNo = 0; slotNo < old_item.GetMaxSlot(); ++slotNo)
      {
        if (MonoBehaviourSingleton<StatusManager>.I.GetUniqueOrHomeEquipSkill(old_item, slotNo, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()) != null)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        this.migrationOldItem = old_item;
        this.migrationSelectItem = select_item;
        GameSection.ChangeEvent("MIGRATION_SKILL_CONFIRM");
      }
      else
        this.TO_UNIQUE_OR_MAIN_STATUS();
    }
  }

  protected virtual void NotChangeItem(EquipItemInfo select_item, EquipItemInfo old_item)
  {
    GameSection.SetEventData((object) new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, this.selectEquipSetData.index, old_item));
  }

  protected virtual bool IsAlreadyEquipItem(EquipItemInfo item)
  {
    return !this.selectEquipSetData.IsEnableChange(item);
  }

  protected override void OnQuery_SELECT_ITEM()
  {
    if (!this.OnSelectItemAndChekIsGoStatus())
      return;
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }

  protected bool OnSelectItemAndChekIsGoStatus()
  {
    EquipItemInfo equipItem = this.EquipItem;
    ulong uniqueId = equipItem != null ? equipItem.uniqueID : 0UL;
    if (uniqueId == 0UL && !this.IsCreateRemoveButton())
    {
      GameSection.ChangeEvent("NO_SELECTED");
      return false;
    }
    if (this.IsAlreadyEquipItem(equipItem))
    {
      int equipIndex = this.GetEquipIndex(equipItem);
      if (this.IsRemoveEquipSloat(equipIndex))
        this.ChangeSwapEquipConfirm(equipIndex, equipItem.tableData.name);
      return false;
    }
    EquipItemInfo compareItemData = this.GetCompareItemData();
    if ((compareItemData != null ? (long) compareItemData.uniqueID : 0L) != (long) uniqueId)
    {
      if (equipItem != null && !MonoBehaviourSingleton<GameSceneManager>.I.CheckEquipItemAndOpenUpdateAppDialog(equipItem.tableData, new System.Action(this.OnCancelSelect)))
      {
        GameSection.StopEvent();
        return false;
      }
      if (equipItem != null && equipItem.tableData != null)
        GameSaveData.instance.RemoveNewIconAndSave(ItemIcon.GetItemIconType(equipItem.tableData.type), equipItem.uniqueID);
      this.ChangeSelectItem(equipItem, compareItemData);
      if (!TutorialStep.HasAllTutorialCompleted())
        TutorialStep.isChangeLocalEquip = true;
      return false;
    }
    this.NotChangeItem(equipItem, compareItemData);
    if (!TutorialStep.HasAllTutorialCompleted())
      TutorialStep.isChangeLocalEquip = true;
    return true;
  }

  protected virtual int GetEquipIndex(EquipItemInfo select_item)
  {
    return this.selectEquipSetData.EquippingIndexOf(select_item);
  }

  protected override void OnQueryDetail()
  {
    int eventData = (int) GameSection.GetEventData();
    this.detailItem = (EquipItemInfo) null;
    if (eventData >= 0 && this.localInventoryEquipData != null)
      this.detailItem = this.localInventoryEquipData[eventData].GetItemData() as EquipItemInfo;
    if (this.detailItem == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP,
        (object) this.detailItem,
        (object) this.selectEquipSetData.setNo
      });
  }

  protected void OnCancelSelect()
  {
    this.EquipItem = this.GetCompareItemData();
    this.selectInventoryIndex = this.GetSelectItemIndex();
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY);
  }

  protected override void OnQuery_SKILL_ICON_BUTTON()
  {
    if (this.EquipItem == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP,
        (object) this.EquipItem,
        (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex
      });
  }

  private void OnQuery_ABILITY()
  {
    EquipSetInfo equipSetInfo = new EquipSetInfo(this.selectEquipSetData.equipSetInfo.item, this.selectEquipSetData.equipSetInfo.name, this.selectEquipSetData.equipSetInfo.showHelm, this.selectEquipSetData.equipSetInfo.acc);
    equipSetInfo.item[this.selectEquipSetData.index] = this.EquipItem;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    GameSection.SetEventData((object) new object[3]
    {
      (object) equipSetInfo,
      (object) MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(this.selectEquipSetData.setNo, new EquipItemAbilityCollection.SwapData(this.selectEquipSetData.index, this.EquipItem)),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) userStatus.atk, (int) userStatus.def, (int) userStatus.hp, (List<CharaInfo.EquipItem>) null)
    });
  }

  private void OnQuery_NON_ABILITY()
  {
    EquipSetInfo equipSetInfo = new EquipSetInfo(this.selectEquipSetData.equipSetInfo.item, this.selectEquipSetData.equipSetInfo.name, this.selectEquipSetData.equipSetInfo.showHelm, this.selectEquipSetData.equipSetInfo.acc);
    equipSetInfo.item[this.selectEquipSetData.index] = this.EquipItem;
    GameSection.ChangeEvent("ABILITY", (object) new object[2]
    {
      (object) equipSetInfo,
      (object) MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(this.selectEquipSetData.setNo, new EquipItemAbilityCollection.SwapData(this.selectEquipSetData.index, this.EquipItem))
    });
  }

  protected void OnCloseDialog_StatusEquipSort() => this.OnCloseSortDialog();

  protected override bool sorting()
  {
    this.InitLocalInventory();
    return true;
  }

  protected override int GetSelectItemIndex()
  {
    EquipItemInfo equipItem = this.EquipItem;
    if (equipItem == null || this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
      return -1;
    int selectItemIndex = 0;
    for (int length = this.localInventoryEquipData.Length; selectItemIndex < length; ++selectItemIndex)
    {
      if (this.localInventoryEquipData[selectItemIndex] != null && (long) this.localInventoryEquipData[selectItemIndex].GetUniqID() == (long) equipItem.uniqueID)
        return selectItemIndex;
    }
    return -1;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
  {
    if ((notify_flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (this.detailItem != null)
      {
        MonoBehaviourSingleton<StatusManager>.I.UpdateLocalInventory(MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(this.detailItem.uniqueID));
        this.InitLocalInventory();
      }
    }
    else if ((notify_flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY)) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.InitLocalInventory();
      if (this.sortSettings.Sort<EquipItemSortData>(this.localInventoryEquipData as EquipItemSortData[]))
      {
        this.selectInventoryIndex = this.GetSelectItemIndex();
        if (this.selectInventoryIndex == -1)
        {
          EquipItemInfo compareItemData = this.GetCompareItemData();
          if (compareItemData != null)
          {
            this.EquipItem = compareItemData;
            this.selectInventoryIndex = this.GetSelectItemIndex();
          }
          else if (!this.IsCreateRemoveButton())
            this.selectInventoryIndex = 0;
        }
        this.EquipItem = this.selectInventoryIndex != -1 ? this.localInventoryEquipData[this.selectInventoryIndex].GetItemData() as EquipItemInfo : (EquipItemInfo) null;
        if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
          MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(this.EquipItem);
        this.SetDirty((Enum) this.InventoryUI);
      }
    }
    base.OnNotify(notify_flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY;
  }

  protected virtual void OnQuery_StatusSwapEquipConfirm_YES()
  {
    int swapIndex = this.selectEquipSetData.EquippingIndexOf(this.EquipItem);
    int index = this.selectEquipSetData.index;
    EquipItemInfo equipItemInfo = this.selectEquipSetData.equipSetInfo.item[index];
    this.selectEquipSetData.equipSetInfo.item[index] = this.selectEquipSetData.equipSetInfo.item[swapIndex];
    this.selectEquipSetData.equipSetInfo.item[swapIndex] = equipItemInfo;
    MonoBehaviourSingleton<StatusManager>.I.SwapWeapon(swapIndex, index);
  }

  protected virtual void OnQuery_StatusMigrationSkillConfirm_YES()
  {
    List<SkillItemInfo> detachSkill = new List<SkillItemInfo>();
    List<StatusEquip.MigrationSkillData> migrationSkillDataList = new List<StatusEquip.MigrationSkillData>();
    for (int slotNo = 0; slotNo < this.migrationOldItem.GetMaxSlot(); ++slotNo)
    {
      bool flag = false;
      SkillItemInfo orHomeEquipSkill = MonoBehaviourSingleton<StatusManager>.I.GetUniqueOrHomeEquipSkill(this.migrationOldItem, slotNo, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
      if (orHomeEquipSkill != null)
      {
        for (int index = 0; index < this.migrationSelectItem.GetMaxSlot(); ++index)
        {
          SkillItemTable.SkillSlotData skillSlotData = this.migrationSelectItem.tableData.GetSkillSlot(this.migrationSelectItem.exceed)[index];
          if (skillSlotData != null && skillSlotData.slotType == orHomeEquipSkill.tableData.type)
          {
            int toSlot = index;
            if (this.migrationSelectItem.IsExceedSkillSlot(index))
              toSlot = this.migrationSelectItem.GetExceedSkillSlotNo(index);
            if (migrationSkillDataList.All<StatusEquip.MigrationSkillData>((Func<StatusEquip.MigrationSkillData, bool>) (x => x.toSlotNo != toSlot)))
            {
              StatusEquip.MigrationSkillData migrationSkillData = new StatusEquip.MigrationSkillData(this.migrationSelectItem.uniqueID, toSlot, orHomeEquipSkill);
              migrationSkillDataList.Add(migrationSkillData);
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          detachSkill.Add(orHomeEquipSkill);
      }
    }
    this.migrationSendCount = migrationSkillDataList.Count + detachSkill.Count;
    GameSection.SetEventData((object) new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, this.selectEquipSetData.index, this.migrationSelectItem));
    GameSection.StayEvent();
    this.StartCoroutine(this.SendReplacementSkill(migrationSkillDataList, detachSkill));
  }

  protected virtual void ChangeSwapEquipConfirm(int slotNo, string equipName)
  {
    GameSection.ChangeEvent("SWAP_CONFIRM", (object) new object[2]
    {
      (object) (slotNo + 1).ToString(),
      (object) equipName
    });
  }

  private IEnumerator SendReplacementSkill(
    List<StatusEquip.MigrationSkillData> migrationSkill,
    List<SkillItemInfo> detachSkill)
  {
    bool isSendFinish = false;
    foreach (StatusEquip.MigrationSkillData migrationSkillData1 in migrationSkill)
    {
      isSendFinish = false;
      StatusEquip.MigrationSkillData migrationSkillData2 = migrationSkillData1;
      MonoBehaviourSingleton<StatusManager>.I.SendSetSkill(migrationSkillData2.toUniqueId, migrationSkillData2.skill.uniqueID, migrationSkillData2.toSlotNo, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), (Action<bool>) (isSucces =>
      {
        this.MigrationSkillCallback(isSucces);
        isSendFinish = true;
      }));
      if (!isSendFinish)
        yield return (object) null;
    }
    foreach (SkillItemInfo skillItemInfo1 in detachSkill)
    {
      isSendFinish = false;
      SkillItemInfo skillItemInfo2 = skillItemInfo1;
      EquipSetSkillData uniqueEquipSetSkill = skillItemInfo2.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()));
      if (StatusManager.IsUnique())
        uniqueEquipSetSkill = skillItemInfo2.uniqueEquipSetSkill;
      MonoBehaviourSingleton<StatusManager>.I.SendDetachSkill(uniqueEquipSetSkill.equipItemUniqId, uniqueEquipSetSkill.equipSlotNo, uniqueEquipSetSkill.equipSetNo, (Action<bool>) (isSucces =>
      {
        this.MigrationSkillCallback(isSucces);
        isSendFinish = true;
      }));
      if (!isSendFinish)
        yield return (object) null;
    }
  }

  protected virtual void OnQuery_StatusMigrationSkillConfirm_NO()
  {
    this.RequestRemoveAllSkillFromCurrentEquipment();
  }

  private void MigrationSkillCallback(bool result)
  {
    if (result)
    {
      --this.migrationSendCount;
      if (this.migrationSendCount != 0)
        return;
      GameSection.ResumeEvent(true);
    }
    else
      GameSection.ResumeEvent(false);
  }

  protected void RequestRemoveAllSkillFromCurrentEquipment()
  {
    GameSection.SetEventData((object) new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, this.selectEquipSetData.index, this.migrationSelectItem));
    if (this.migrationOldItem == null)
      return;
    int currentEquipSetNo = MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo();
    GameSection.StayEvent();
    this.StartCoroutine(this.SendRemoveAllSkill(this.migrationOldItem.uniqueID, currentEquipSetNo));
  }

  private IEnumerator SendRemoveAllSkill(ulong _equipmentId, int _setNo)
  {
    bool isSendFinish = false;
    this.migrationSendCount = 1;
    MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkill(_equipmentId, _setNo, (Action<bool>) (isSucces =>
    {
      this.MigrationSkillCallback(isSucces);
      isSendFinish = true;
    }));
    if (!isSendFinish)
      yield return (object) null;
  }

  protected virtual bool IsRemoveEquipSloat(int equip_slot_index)
  {
    if (equip_slot_index != 0 || this.selectEquipSetData.GetEquippingItem() != null)
      return true;
    GameSection.ChangeEvent("NOT_SWAP");
    return false;
  }

  public class LocalEquipSetData
  {
    public int setNo;
    public int index;
    public EquipSetInfo equipSetInfo;

    public LocalEquipSetData(int _no, int _index, EquipSetInfo _set)
    {
      this.setNo = _no;
      this.index = _index;
      this.equipSetInfo = _set;
    }

    public int EquippingIndexOf(EquipItemInfo item)
    {
      if (item == null)
        return -1;
      int index1 = 0;
      for (int index2 = 7; index1 < index2; ++index1)
      {
        if (this.equipSetInfo.item[index1] != null && (long) this.equipSetInfo.item[index1].uniqueID == (long) item.uniqueID)
          return index1;
      }
      return -1;
    }

    public bool IsEnableChange(EquipItemInfo item)
    {
      if (item == null)
        return true;
      int index1 = 0;
      for (int index2 = 7; index1 < index2; ++index1)
      {
        if (this.equipSetInfo.item[index1] != null && (long) this.equipSetInfo.item[index1].uniqueID == (long) item.uniqueID && index1 != this.index)
          return false;
      }
      return true;
    }

    public EquipItemInfo GetEquippingItem() => this.equipSetInfo.item[this.index];
  }

  public class ChangeEquipData
  {
    public int setNo;
    public int index;
    public EquipItemInfo item;

    public ChangeEquipData(int _no, int _index, EquipItemInfo _item)
    {
      this.setNo = _no;
      this.index = _index;
      this.item = _item;
    }
  }

  public class MigrationSkillData
  {
    public ulong toUniqueId { get; protected set; }

    public int toSlotNo { get; protected set; }

    public SkillItemInfo skill { get; protected set; }

    public MigrationSkillData(ulong toId, int slotNo, SkillItemInfo target)
    {
      this.toUniqueId = toId;
      this.toSlotNo = slotNo;
      this.skill = target;
    }
  }
}
