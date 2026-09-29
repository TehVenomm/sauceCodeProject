// Decompiled with JetBrains decompiler
// Type: SmithCreateItemSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SmithCreateItemSelect : SmithEquipSelectBase
{
  private SmithCreateItemInfo[] pickupWeapon;
  private SmithCreateItemInfo[] pickupArmor;
  private SortBase.TYPE[] transInventoryType = new SortBase.TYPE[11]
  {
    SortBase.TYPE.ONE_HAND_SWORD,
    SortBase.TYPE.TWO_HAND_SWORD,
    SortBase.TYPE.SPEAR,
    SortBase.TYPE.PAIR_SWORDS,
    SortBase.TYPE.ARROW,
    SortBase.TYPE.ARMOR,
    SortBase.TYPE.HELM,
    SortBase.TYPE.ARM,
    SortBase.TYPE.LEG,
    SortBase.TYPE.WEAPON_ALL,
    SortBase.TYPE.ARMOR_ALL
  };
  private EQUIPMENT_TYPE[] transInventoryTypeForEquipment = new EQUIPMENT_TYPE[11]
  {
    EQUIPMENT_TYPE.ONE_HAND_SWORD,
    EQUIPMENT_TYPE.TWO_HAND_SWORD,
    EQUIPMENT_TYPE.SPEAR,
    EQUIPMENT_TYPE.PAIR_SWORDS,
    EQUIPMENT_TYPE.ARROW,
    EQUIPMENT_TYPE.ARMOR,
    EQUIPMENT_TYPE.HELM,
    EQUIPMENT_TYPE.ARM,
    EQUIPMENT_TYPE.LEG,
    EQUIPMENT_TYPE.ONE_HAND_SWORD,
    EQUIPMENT_TYPE.ARMOR
  };

  protected override string prefabSuffix => "Create";

  protected override string GetSelectTypeText() => this.sectionData.GetText("TYPE_CREATE");

  protected override void OnClose()
  {
    this.RemoveCreateNewIcon(this.selectTypeIndex);
    base.OnClose();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE;
  }

  public override void Initialize()
  {
    EQUIPMENT_TYPE eventData = (EQUIPMENT_TYPE) GameSection.GetEventData();
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithCreateData>();
    smithData.selectCreateEquipItemType = this.TranslateInventoryType(UIBehaviour.GetEquipmentTypeIndex(eventData));
    this.smithType = SmithEquipBase.SmithType.GENERATE;
    GameSection.SetEventData((object) eventData);
    base.Initialize();
    this.pickupWeapon = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.WEAPON_ALL);
    this.pickupArmor = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.ARMOR_ALL);
    this.SetActive((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_PICKUP, this.pickupWeapon.Length != 0);
    this.SetActive((Enum) SmithEquipSelectBase.UI.BTN_ARMOR_PICKUP, this.pickupArmor.Length != 0);
    this.selectTypeIndex = (int) Mathf.Log((float) smithData.selectCreateEquipItemType, 2f);
    this.InitializeCaption(!MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(this.selectInventoryType) ? this.sectionData.GetText("CAPTION_DEFENCE") : this.sectionData.GetText("CAPTION_WEAPON"));
  }

  protected override void InitLocalInventory()
  {
    SortBase.TYPE createEquipItemType = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>().selectCreateEquipItemType;
    switch (createEquipItemType)
    {
      case SortBase.TYPE.WEAPON_ALL:
        this.localInventoryEquipData = (SortCompareData[]) SortCompareData.CreateSortDataAry<SmithCreateItemInfo, SmithCreateSortData>(this.pickupWeapon, this.sortSettings);
        break;
      case SortBase.TYPE.ARMOR_ALL:
        this.localInventoryEquipData = (SortCompareData[]) SortCompareData.CreateSortDataAry<SmithCreateItemInfo, SmithCreateSortData>(this.pickupArmor, this.sortSettings);
        break;
      default:
        this.localInventoryEquipData = (SortCompareData[]) this.sortSettings.CreateSortAry<SmithCreateItemInfo, SmithCreateSortData>(Singleton<CreateEquipItemTable>.I.GetCreateEquipItemDataAry(this.SortBaseTypeToEquipmentType(createEquipItemType)));
        break;
    }
    this.SelectingInventoryFirst();
  }

  public override void UpdateUI()
  {
    SmithManager.SmithBadgeData smithBadgeData = MonoBehaviourSingleton<SmithManager>.I.smithBadgeData;
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_HELM, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.HELM), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_ARMOR, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARMOR), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_ARM, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARM), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_LEG, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.LEG), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_1, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ONE_HAND_SWORD), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_2, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.TWO_HAND_SWORD), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_3, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.SPEAR), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_4, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.PAIR_SWORDS), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_5, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARROW), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_PICKUP, smithBadgeData.GetPickupBadgeNum(true), (SpriteAlignment) 6, 0, 0, true);
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_ARMOR_PICKUP, smithBadgeData.GetPickupBadgeNum(false), (SpriteAlignment) 6, 0, 0, true);
    this.SetActive(this.GetCtrl((Enum) this.uiTypeTab[this.weaponPickupIndex]).parent, this.pickupWeapon != null && this.pickupWeapon.Length != 0);
    this.SetActive(this.GetCtrl((Enum) this.uiTypeTab[this.armorPickupIndex]).parent, this.pickupArmor != null && this.pickupArmor.Length != 0);
    this.GetComponent<UIGrid>((Enum) SmithEquipSelectBase.UI.GRD_WEAPON).Reposition();
    this.GetComponent<UIGrid>((Enum) SmithEquipSelectBase.UI.GRD_ARMOR).Reposition();
    base.UpdateUI();
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
      return;
    this.SetBadge((Enum) SmithEquipSelectBase.UI.BTN_WEAPON_3, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.SPEAR) - 1, (SpriteAlignment) 6, 0, 0, true);
    if (!Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), "CreateItem");
  }

  protected override void SetupInventoryTypeToggole()
  {
    SortBase.TYPE createEquipItemType = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>().selectCreateEquipItemType;
    bool flag = false;
    if (createEquipItemType < SortBase.TYPE.ARMOR || createEquipItemType == SortBase.TYPE.WEAPON_ALL)
      flag = true;
    this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_ATK_ROOT, flag);
    this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_DEF_ROOT, !flag);
    this.SetToggleButton((Enum) SmithEquipSelectBase.UI.TGL_BUTTON_ROOT, flag, (Action<bool>) (is_active =>
    {
      SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
      smithData.selectCreateEquipItemType = is_active ? SortBase.TYPE.ONE_HAND_SWORD : SortBase.TYPE.HELM;
      int index = is_active ? 0 : 1;
      this.ResetTween((Enum) this.tabAnimTarget[index]);
      this.PlayTween((Enum) this.tabAnimTarget[index], is_input_block: false);
      this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_ATK_ROOT, is_active);
      this.SetActive((Enum) SmithEquipSelectBase.UI.OBJ_DEF_ROOT, !is_active);
      this.selectTypeIndex = (int) Mathf.Log((float) smithData.selectCreateEquipItemType, 2f);
      this.SetDirty((Enum) this.InventoryUI);
      this.InitSort();
      this.InitLocalInventory();
      this.LocalInventory();
      this.UpdateTabButton();
      if (TutorialStep.HasAllTutorialCompleted() || smithData.selectCreateEquipItemType != SortBase.TYPE.HELM)
        return;
      MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), "SelectArmor");
    }));
  }

  protected override void LocalInventory()
  {
    this.SetupEnableInventoryUI();
    this.SetLabelText((Enum) SmithEquipSelectBase.UI.LBL_SORT, this.sortSettings.GetSortLabel());
    if (this.localInventoryEquipData != null)
    {
      SortBase.TYPE type = this.TranslateInventoryType(this.selectTypeIndex);
      bool _is_pickup = type == SortBase.TYPE.WEAPON_ALL || type == SortBase.TYPE.ARMOR_ALL;
      this.m_generatedIconList.Clear();
      this.UpdateNewIconInfo();
      bool initItem = false;
      this.SetDynamicList((Enum) this.InventoryUI, "", this.localInventoryEquipData.Length, false, (Func<int, bool>) (check_index =>
      {
        if (!(this.localInventoryEquipData[check_index].GetItemData() is SmithCreateItemInfo itemData2) || !MonoBehaviourSingleton<InventoryManager>.I.IsHaveingKeyMaterial(itemData2.smithCreateTableData.needKeyOrder, itemData2.smithCreateTableData.needMaterial) || (int) itemData2.smithCreateTableData.researchLv > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.researchLv)
          return false;
        uint tableId = this.localInventoryEquipData[check_index].GetTableID();
        if (tableId == 20250105U && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) || tableId == 0U)
          return false;
        SortCompareData sortCompareData = this.localInventoryEquipData[check_index];
        return sortCompareData != null && sortCompareData.IsPriority(this.sortSettings.orderTypeAsc);
      }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        initItem = true;
        SmithCreateItemInfo itemData3 = this.localInventoryEquipData[i].GetItemData() as SmithCreateItemInfo;
        uint tableId = this.localInventoryEquipData[i].GetTableID();
        if (tableId == 20250105U && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
          return;
        if (tableId == 0U)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetActive(t, true);
          if (!(this.localInventoryEquipData[i] is SmithCreateSortData smithCreateSortData2) || !smithCreateSortData2.IsPriority(this.sortSettings.orderTypeAsc))
            return;
          EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableId);
          ITEM_ICON_TYPE iconType = this.localInventoryEquipData[i].GetIconType();
          SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(equipItemData, 0);
          bool is_new = MonoBehaviourSingleton<SmithManager>.I.NeedSmithBadge(itemData3, _is_pickup);
          ItemIcon createItemIconDetail = this.CreateSmithCreateItemIconDetail(iconType, equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), new RARITY_TYPE?(equipItemData.rarity), smithCreateSortData2, skillSlotData, this.IsShowMainStatus, t, "SELECT_ITEM", i, smithCreateSortData2.GetIconStatus(), is_new, getType: smithCreateSortData2.GetGetType());
          createItemIconDetail.SetItemID(smithCreateSortData2.GetTableID());
          createItemIconDetail.SetButtonColor(this.localInventoryEquipData[i].IsPriority(this.sortSettings.orderTypeAsc), true);
          this.SetLongTouch(createItemIconDetail.transform, "DETAIL", (object) i);
          if (Object.op_Inequality((Object) createItemIconDetail, (Object) null) && smithCreateSortData2 != null)
            createItemIconDetail.SetInitData((SortCompareData) smithCreateSortData2);
          if (!Object.op_Inequality((Object) createItemIconDetail, (Object) null) || this.m_generatedIconList.Contains(createItemIconDetail))
            return;
          this.m_generatedIconList.Add(createItemIconDetail);
        }
      }));
      this.SetActive(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, !initItem);
      this.SetLabelText(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799U));
    }
    else
    {
      this.SetActive(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, true);
      this.SetLabelText(this.GetCtrl((Enum) SmithEquipSelectBase.UI.OBJ_ROOT), (Enum) SmithEquipSelectBase.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799U));
    }
  }

  protected override void InitSort()
  {
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    if (smithData.selectCreateEquipItemType < SortBase.TYPE.ARMOR || smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
    {
      if (smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
        this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
      else
        this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
    }
    else if (smithData.selectCreateEquipItemType == SortBase.TYPE.ARMOR_ALL)
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
    else
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
  }

  protected override bool sorting()
  {
    this.InitLocalInventory();
    return true;
  }

  protected override void SelectingInventoryFirst()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0 || this.localInventoryEquipData[0] == null || !(this.localInventoryEquipData[0].GetItemData() is SmithCreateItemInfo itemData))
      return;
    this.selectInventoryIndex = 0;
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    smithData.generateTableData = itemData.equipTableData;
    smithData.createEquipItemTable = itemData.smithCreateTableData;
  }

  protected override int GetSelectItemIndex()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0 || this.localInventoryEquipData[0] == null)
      return -1;
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    int selectItemIndex = 0;
    for (int length = this.localInventoryEquipData.Length; selectItemIndex < length; ++selectItemIndex)
    {
      if (this.localInventoryEquipData[selectItemIndex].GetItemData() is SmithCreateItemInfo itemData && (int) itemData.equipTableData.id == (int) smithData.generateTableData.id && (int) itemData.smithCreateTableData.id == (int) smithData.createEquipItemTable.id)
        return selectItemIndex;
    }
    return -1;
  }

  protected void OnCloseDialog_SmithCreateItemSort() => this.OnCloseSortDialog();

  protected override void OnQuery_TRY_ON()
  {
    this.selectInventoryIndex = (int) GameSection.GetEventData();
    base.OnQuery_TRY_ON();
  }

  private void TryOn()
  {
    if (this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0)
      return;
    this.selectInventoryIndex = (int) GameSection.GetEventData();
    SmithCreateSortData smithCreateSortData = this.localInventoryEquipData[this.selectInventoryIndex] as SmithCreateSortData;
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    if (smithCreateSortData == null || smithData == null)
      return;
    smithData.createEquipItemTable = smithCreateSortData.createData.smithCreateTableData;
    smithData.generateTableData = smithCreateSortData.createData.equipTableData;
  }

  protected override void OnQuery_SELECT_ITEM()
  {
    this.TryOn();
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    if (smithData.createEquipItemTable != null && smithData.generateTableData != null)
      return;
    GameSection.StopEvent();
  }

  protected override void OnQueryDetail()
  {
    this.TryOn();
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE,
      (object) this.GetEquipTableData()
    });
  }

  protected override void OnQuery_SKILL_ICON_BUTTON() => GameSection.StopEvent();

  private void OnQuery_ABILITY()
  {
    int eventData = (int) GameSection.GetEventData();
    int index1 = eventData >> 16 /*0x10*/;
    int index2 = eventData % 65536 /*0x010000*/;
    SmithCreateItemInfo itemData = this.localInventoryEquipData[index1].GetItemData() as SmithCreateItemInfo;
    EquipItemAbility event_data = (EquipItemAbility) null;
    if (itemData != null)
      event_data = new EquipItemAbility((uint) itemData.equipTableData.fixedAbility[index2].id, 0);
    if (event_data == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) event_data);
  }

  protected override void OnQuery_TYPE_TAB()
  {
    int selectTypeIndex = this.selectTypeIndex;
    this.selectTypeIndex = (int) GameSection.GetEventData();
    this.RemoveCreateNewIcon(selectTypeIndex);
    MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>().selectCreateEquipItemType = this.TranslateInventoryType(this.selectTypeIndex);
    if (selectTypeIndex >= 9 && this.selectTypeIndex < 9 || selectTypeIndex < 9 && this.selectTypeIndex >= 9)
      this.InitSort();
    this.SetDirty((Enum) this.InventoryUI);
    this.InitLocalInventory();
    this.RefreshUI();
  }

  private SortBase.TYPE TranslateInventoryType(int index) => this.transInventoryType[index];

  private EQUIPMENT_TYPE TranslateInventoryTypeForEquipment(int index)
  {
    return this.transInventoryTypeForEquipment[index];
  }

  private void RemoveCreateNewIcon(int tab_index)
  {
    EQUIPMENT_TYPE type1 = this.TranslateInventoryTypeForEquipment(tab_index);
    if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(type1) <= 0)
      return;
    SortBase.TYPE type2 = this.TranslateInventoryType(tab_index);
    bool is_pickup = type2 == SortBase.TYPE.WEAPON_ALL || type2 == SortBase.TYPE.ARMOR_ALL;
    MonoBehaviourSingleton<SmithManager>.I.RemoveSmithBadge(type1, is_pickup);
    MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(true);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE);
  }
}
