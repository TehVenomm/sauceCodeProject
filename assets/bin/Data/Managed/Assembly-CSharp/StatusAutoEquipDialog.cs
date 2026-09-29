// Decompiled with JetBrains decompiler
// Type: StatusAutoEquipDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StatusAutoEquipDialog : GameSection
{
  private bool isMismatchElem;
  private StatusEquip.LocalEquipSetData selectEquipSetData;
  private int weaponIndex;
  private int elementIndex;
  private bool isCurrent;
  private StatusEquip.ChangeEquipData[] newEquipments;
  private List<ulong> selectedWeaponIds;
  private EquipItemInfo[] weaponItemInfoList;
  private StatusAutoEquipDialog.UI[] weapons = new StatusAutoEquipDialog.UI[6]
  {
    StatusAutoEquipDialog.UI.BTN_NONE_WEAPON,
    StatusAutoEquipDialog.UI.BTN_ONE_HAND,
    StatusAutoEquipDialog.UI.BTN_TWO_HAND,
    StatusAutoEquipDialog.UI.BTN_SPEAR,
    StatusAutoEquipDialog.UI.BTN_PAIR,
    StatusAutoEquipDialog.UI.BTN_ARROW
  };
  private StatusAutoEquipDialog.UI[] elements = new StatusAutoEquipDialog.UI[7]
  {
    StatusAutoEquipDialog.UI.BTN_NONE_ELEMENT,
    StatusAutoEquipDialog.UI.BTN_FIRE,
    StatusAutoEquipDialog.UI.BTN_WATER,
    StatusAutoEquipDialog.UI.BTN_THUNDER,
    StatusAutoEquipDialog.UI.BTN_SOIL,
    StatusAutoEquipDialog.UI.BTN_LIGHT,
    StatusAutoEquipDialog.UI.BTN_DARK
  };
  private StatusAutoEquipDialog.UI[] states = new StatusAutoEquipDialog.UI[2]
  {
    StatusAutoEquipDialog.UI.BTN_CURRENT,
    StatusAutoEquipDialog.UI.BTN_POTENTIAL
  };

  public override void Initialize()
  {
    this.selectEquipSetData = GameSection.GetEventData() as StatusEquip.LocalEquipSetData;
    base.Initialize();
  }

  private void OnQuery_OK()
  {
    this.weaponIndex = this.GetToggleIndex(this.weapons);
    this.elementIndex = this.GetToggleIndex(this.elements);
    this.isCurrent = this.GetToggleIndex(this.states) == 0;
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = this.ChangeWeaponTypeToInventoryType(this.weaponIndex);
    this.weaponItemInfoList = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
    if (this.weaponItemInfoList == null || this.weaponItemInfoList.Length == 0)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Get(STRING_CATEGORY.AUTO_EQUIP, 0U)), (Action<string>) (ret => { }));
      GameSection.StopEvent();
    }
    else
    {
      this.newEquipments = new StatusEquip.ChangeEquipData[7];
      this.selectedWeaponIds = new List<ulong>(3);
      for (int _index = 0; _index < this.newEquipments.Length; ++_index)
      {
        this.newEquipments[_index] = new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, _index, (EquipItemInfo) null);
        switch (_index)
        {
          case 0:
            this.SelectWeapon0();
            break;
          case 1:
            this.SelectWeapon1();
            break;
          case 2:
            this.SelectWeapon2();
            break;
          case 3:
            this.SelectArmor();
            break;
          case 4:
            this.SelectHelm();
            break;
          case 5:
            this.SelectArm();
            break;
          case 6:
            this.SelectLeg();
            break;
        }
      }
      GameSection.SetEventData((object) this.newEquipments);
      if (this.isMismatchElem)
        GameSection.ChangeEvent("ELMENT_WARNING");
      else
        GameSection.ChangeEvent("COMPLETE");
    }
  }

  private void OnQuery_StatusAutoEquipWarningElementDialog_OK()
  {
    GameSection.SetEventData((object) this.newEquipments);
  }

  private void OnQuery_QuestAcceptArenaRoomAutoEquipWarningElementDialog_OK()
  {
    GameSection.SetEventData((object) this.newEquipments);
  }

  private void SelectWeapon0()
  {
    this.newEquipments[0].item = this.GetWeaponMaxAtk(true);
    if (this.elementIndex == 0 || this.ChangeElementToMasterDefineElement(this.elementIndex, StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL) == (ELEMENT_TYPE) this.newEquipments[0].item.GetElemAtkType())
      return;
    this.isMismatchElem = true;
  }

  private void SelectWeapon1() => this.newEquipments[1].item = this.GetWeaponMaxAtk(true);

  private void SelectWeapon2() => this.newEquipments[2].item = this.GetWeaponMaxAtk(true);

  private void SelectHelm()
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.HELM;
    this.newEquipments[4].item = this.GetEquipMaxDef(MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone(), true, true, true);
  }

  private void SelectArmor()
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ARMOR;
    this.newEquipments[3].item = this.GetEquipMaxDef(MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone(), true, true, true);
  }

  private void SelectArm()
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ARM;
    this.newEquipments[5].item = this.GetEquipMaxDef(MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone(), true, true, true);
  }

  private void SelectLeg()
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.LEG;
    this.newEquipments[6].item = this.GetEquipMaxDef(MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone(), true, true, true);
  }

  private EquipItemInfo GetWeaponMaxAtk(
    bool validElement,
    StatusAutoEquipDialog.ELEMENT_CONDITION condition = StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL)
  {
    EquipItemInfo weaponMaxAtk = (EquipItemInfo) null;
    int num1 = -1;
    for (int index1 = 0; index1 < this.weaponItemInfoList.Length; ++index1)
    {
      if (validElement && this.elementIndex != 0)
      {
        ELEMENT_TYPE masterDefineElement = this.ChangeElementToMasterDefineElement(this.elementIndex, condition);
        int elemAtkType = this.weaponItemInfoList[index1].GetElemAtkType();
        if (masterDefineElement != (ELEMENT_TYPE) elemAtkType && condition == StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL || (masterDefineElement == (ELEMENT_TYPE) elemAtkType || 6 == elemAtkType) && condition == StatusAutoEquipDialog.ELEMENT_CONDITION.DISADVANTAGEOUS)
          continue;
      }
      int num2 = 0;
      if (this.isCurrent)
      {
        num2 = this.weaponItemInfoList[index1].atk + this.weaponItemInfoList[index1].elemAtk;
      }
      else
      {
        EquipItemTable.EquipItemData tableData = this.weaponItemInfoList[index1].tableData;
        GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint) tableData.maxLv);
        if (growEquipItemData != null)
        {
          EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll = tableData.GetExceedParam((uint) this.weaponItemInfoList[index1].exceed) ?? new EquipItemExceedParamTable.EquipItemExceedParamAll();
          int num3 = growEquipItemData.GetGrowParamAtk((int) tableData.baseAtk) + (int) itemExceedParamAll.atk;
          int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(tableData.atkElement);
          int index2 = 0;
          for (int length = growParamElemAtk.Length; index2 < length; ++index2)
            growParamElemAtk[index2] += itemExceedParamAll.atkElement[index2];
          int num4 = Mathf.Max(growParamElemAtk);
          num2 = num3 + num4;
        }
      }
      if (num2 != 0)
      {
        int type = (int) this.weaponItemInfoList[index1].tableData.type;
        if (type >= 4)
          --type;
        if (type < MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate.Length)
          num2 = (int) ((double) num2 / (double) MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[type]);
      }
      if (num2 > num1 && !this.selectedWeaponIds.Contains(this.weaponItemInfoList[index1].uniqueID))
      {
        weaponMaxAtk = this.weaponItemInfoList[index1];
        num1 = num2;
      }
    }
    if (weaponMaxAtk != null)
      this.selectedWeaponIds.Add(weaponMaxAtk.uniqueID);
    if (weaponMaxAtk == null & validElement && condition == StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL)
      weaponMaxAtk = this.GetWeaponMaxAtk(true, StatusAutoEquipDialog.ELEMENT_CONDITION.DISADVANTAGEOUS);
    else if (weaponMaxAtk == null & validElement && condition == StatusAutoEquipDialog.ELEMENT_CONDITION.DISADVANTAGEOUS)
      weaponMaxAtk = this.GetWeaponMaxAtk(false);
    return weaponMaxAtk;
  }

  private EquipItemInfo GetEquipMaxDef(
    EquipItemInfo[] items,
    bool validElement,
    bool validAbilityWeaponType,
    bool checkOnlyFixAbility)
  {
    EquipItemInfo equipMaxDef = (EquipItemInfo) null;
    int num1 = -1;
    int num2 = -1;
    for (int index1 = 0; index1 < items.Length; ++index1)
    {
      if ((!validAbilityWeaponType || this.weaponIndex == 0 || this.HasFixAbilityWithWeaponType(items[index1], this.weaponIndex, checkOnlyFixAbility)) && (!validElement || this.elementIndex == 0 || this.ChangeElementToMasterDefineElement(this.elementIndex, StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF) == (ELEMENT_TYPE) items[index1].GetElemDefType()))
      {
        int num3 = 0;
        int num4 = 0;
        if (this.isCurrent)
        {
          num3 = items[index1].def + items[index1].elemDef;
          num4 = items[index1].hp;
        }
        else
        {
          EquipItemTable.EquipItemData tableData = items[index1].tableData;
          GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint) tableData.maxLv);
          if (growEquipItemData != null)
          {
            EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll = tableData.GetExceedParam(4U) ?? new EquipItemExceedParamTable.EquipItemExceedParamAll();
            int num5 = growEquipItemData.GetGrowParamDef((int) tableData.baseDef) + (int) itemExceedParamAll.def;
            int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(tableData.defElement);
            int index2 = 0;
            for (int length = growParamElemDef.Length; index2 < length; ++index2)
              growParamElemDef[index2] += itemExceedParamAll.defElement[index2];
            int num6 = Mathf.Max(growParamElemDef);
            num3 = num5 + num6;
            num4 = growEquipItemData.GetGrowParamHp((int) tableData.baseHp) + (int) itemExceedParamAll.hp;
          }
        }
        if (num3 > num1 || num3 == num1 && num4 > num2)
        {
          equipMaxDef = items[index1];
          num1 = num3;
          num2 = num4;
        }
      }
    }
    if (equipMaxDef == null & validElement & validAbilityWeaponType & checkOnlyFixAbility)
      equipMaxDef = this.GetEquipMaxDef(items, true, true, false);
    else if (equipMaxDef == null & validElement & validAbilityWeaponType && !checkOnlyFixAbility)
      equipMaxDef = this.GetEquipMaxDef(items, false, true, true);
    else if (((equipMaxDef != null ? 0 : (!validElement ? 1 : 0)) & (validAbilityWeaponType ? 1 : 0) & (checkOnlyFixAbility ? 1 : 0)) != 0)
      equipMaxDef = this.GetEquipMaxDef(items, false, true, false);
    else if (((equipMaxDef != null ? 0 : (!validElement ? 1 : 0)) & (validAbilityWeaponType ? 1 : 0)) != 0 && !checkOnlyFixAbility)
      equipMaxDef = this.GetEquipMaxDef(items, true, false, false);
    else if (equipMaxDef == null)
      equipMaxDef = this.GetEquipMaxDef(items, false, false, false);
    return equipMaxDef;
  }

  private int GetToggleIndex(StatusAutoEquipDialog.UI[] uiArray)
  {
    int toggleIndex = 0;
    for (int index = 0; index < uiArray.Length; ++index)
    {
      UIToggle component = ((Component) this.GetCtrl((Enum) uiArray[index])).GetComponent<UIToggle>();
      if (!Object.op_Equality((Object) component, (Object) null) && component.value)
      {
        toggleIndex = index;
        break;
      }
    }
    return toggleIndex;
  }

  private InventoryManager.INVENTORY_TYPE ChangeWeaponTypeToInventoryType(int weaponIndex)
  {
    InventoryManager.INVENTORY_TYPE inventoryType = InventoryManager.INVENTORY_TYPE.ALL_WEAPON;
    switch (weaponIndex)
    {
      case 1:
        inventoryType = InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD;
        break;
      case 2:
        inventoryType = InventoryManager.INVENTORY_TYPE.TWO_HAND_SWORD;
        break;
      case 3:
        inventoryType = InventoryManager.INVENTORY_TYPE.SPEAR;
        break;
      case 4:
        inventoryType = InventoryManager.INVENTORY_TYPE.PAIR_SWORDS;
        break;
      case 5:
        inventoryType = InventoryManager.INVENTORY_TYPE.ARROW;
        break;
    }
    return inventoryType;
  }

  private ELEMENT_TYPE ChangeElementToMasterDefineElement(
    int elementIndex,
    StatusAutoEquipDialog.ELEMENT_CONDITION condition)
  {
    ELEMENT_TYPE masterDefineElement = ELEMENT_TYPE.MAX;
    switch (elementIndex)
    {
      case 1:
        switch (condition)
        {
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL:
            masterDefineElement = ELEMENT_TYPE.FIRE;
            break;
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF:
            masterDefineElement = ELEMENT_TYPE.SOIL;
            break;
          default:
            masterDefineElement = ELEMENT_TYPE.THUNDER;
            break;
        }
        break;
      case 2:
        switch (condition)
        {
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL:
            masterDefineElement = ELEMENT_TYPE.WATER;
            break;
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF:
            masterDefineElement = ELEMENT_TYPE.FIRE;
            break;
          default:
            masterDefineElement = ELEMENT_TYPE.SOIL;
            break;
        }
        break;
      case 3:
        switch (condition)
        {
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL:
            masterDefineElement = ELEMENT_TYPE.THUNDER;
            break;
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF:
            masterDefineElement = ELEMENT_TYPE.WATER;
            break;
          default:
            masterDefineElement = ELEMENT_TYPE.FIRE;
            break;
        }
        break;
      case 4:
        switch (condition)
        {
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL:
            masterDefineElement = ELEMENT_TYPE.SOIL;
            break;
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF:
            masterDefineElement = ELEMENT_TYPE.THUNDER;
            break;
          default:
            masterDefineElement = ELEMENT_TYPE.WATER;
            break;
        }
        break;
      case 5:
        switch (condition)
        {
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL:
            masterDefineElement = ELEMENT_TYPE.LIGHT;
            break;
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF:
            masterDefineElement = ELEMENT_TYPE.DARK;
            break;
          default:
            masterDefineElement = ELEMENT_TYPE.MAX;
            break;
        }
        break;
      case 6:
        switch (condition)
        {
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EQUAL:
            masterDefineElement = ELEMENT_TYPE.DARK;
            break;
          case StatusAutoEquipDialog.ELEMENT_CONDITION.EFFECTIVE_DEF:
            masterDefineElement = ELEMENT_TYPE.LIGHT;
            break;
          default:
            masterDefineElement = ELEMENT_TYPE.MAX;
            break;
        }
        break;
    }
    return masterDefineElement;
  }

  private bool HasFixAbilityWithWeaponType(
    EquipItemInfo item,
    int wepIndex,
    bool checkOnlyFixAbility)
  {
    if (item.ability == null || item.ability.Length == 0)
      return false;
    for (int index1 = 0; index1 < item.ability.Length; ++index1)
    {
      if (!checkOnlyFixAbility || item.IsFixedAbility(index1))
      {
        AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(item.ability[index1].id, item.ability[index1].ap);
        if (abilityData != null)
        {
          ENABLE_EQUIP_TYPE enableEquipType = abilityData.enableEquipType;
          if (enableEquipType != ENABLE_EQUIP_TYPE.ALL && (enableEquipType == ENABLE_EQUIP_TYPE.ONE_HAND_SWORD && wepIndex == 1 || enableEquipType == ENABLE_EQUIP_TYPE.TWO_HAND_SWORD && wepIndex == 2 || enableEquipType == ENABLE_EQUIP_TYPE.SPEAR && wepIndex == 3 || enableEquipType == ENABLE_EQUIP_TYPE.PAIR_SWORDS && wepIndex == 4 || enableEquipType == ENABLE_EQUIP_TYPE.ARROW && wepIndex == 5))
            return true;
          AbilityDataTable.AbilityData.AbilityInfo[] info = abilityData.info;
          for (int index2 = 0; index2 < info.Length; ++index2)
          {
            if (!string.IsNullOrEmpty(info[index2].target))
            {
              string target = info[index2].target;
              if (target == "ONE_HAND_SWORD" && wepIndex == 1 || target == "TWO_HAND_SWORD" && wepIndex == 2 || target == "SPEAR" && wepIndex == 3 || target == "PAIR_SWORDS" && wepIndex == 4 || target == "ARROW" && wepIndex == 5)
                return true;
            }
          }
        }
      }
    }
    return false;
  }

  protected enum UI
  {
    BTN_NONE_WEAPON,
    BTN_ONE_HAND,
    BTN_TWO_HAND,
    BTN_SPEAR,
    BTN_PAIR,
    BTN_ARROW,
    BTN_NONE_ELEMENT,
    BTN_FIRE,
    BTN_WATER,
    BTN_THUNDER,
    BTN_SOIL,
    BTN_LIGHT,
    BTN_DARK,
    BTN_CURRENT,
    BTN_POTENTIAL,
  }

  private enum ELEMENT_CONDITION
  {
    EQUAL,
    EFFECTIVE_DEF,
    DISADVANTAGEOUS,
  }

  private enum WEAPON_TYPE
  {
    NONE,
    ONE_HAND,
    TWO_HAND,
    SPEAR,
    PAIR,
    ARROW,
  }

  private enum ELEMENT
  {
    NONE,
    FIRE,
    WATER,
    THUNDER,
    SOIL,
    LIGHT,
    DARK,
  }

  private new enum STATE
  {
    CURRENT,
    POTENTIAL,
  }
}
