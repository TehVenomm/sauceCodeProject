// Decompiled with JetBrains decompiler
// Type: EquipItemSortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class EquipItemSortData : SortCompareData
{
  public EquipItemInfo equipData;

  public override object GetItemData() => (object) this.equipData;

  public override void SetItem(object item) => this.equipData = (EquipItemInfo) item;

  public override void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
    switch (requirement)
    {
      case SortBase.SORT_REQUIREMENT.NUM:
        this.sortingData = 1L;
        break;
      case SortBase.SORT_REQUIREMENT.RARITY:
        this.sortingData = (long) this.equipData.tableData.rarity;
        break;
      case SortBase.SORT_REQUIREMENT.LV:
        this.sortingData = (long) this.equipData.level;
        break;
      case SortBase.SORT_REQUIREMENT.ATK:
        this.sortingData = (long) (this.equipData.atk + this.equipData.elemAtk);
        break;
      case SortBase.SORT_REQUIREMENT.DEF:
        this.sortingData = (long) (this.equipData.def + this.equipData.elemDef);
        break;
      case SortBase.SORT_REQUIREMENT.SALE:
        this.sortingData = (long) this.equipData.tableData.sale;
        break;
      case SortBase.SORT_REQUIREMENT.SOCKET:
        this.sortingData = (long) this.equipData.GetMaxSlot();
        break;
      case SortBase.SORT_REQUIREMENT.PRICE:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.ELEMENT:
        this.sortingData = 6L - (long) this.GetIconElement();
        break;
      case SortBase.SORT_REQUIREMENT.ELEM_ATK:
        this.sortingData = (long) this.equipData.elemAtk;
        break;
      case SortBase.SORT_REQUIREMENT.ELEM_DEF:
        this.sortingData = (long) this.equipData.elemDef;
        break;
      default:
        this.sortingData = (long) this.equipData.uniqueID;
        break;
    }
  }

  public override bool IsFavorite() => this.equipData.isFavorite;

  public override int GetItemType()
  {
    return this.EquipmentTypeToSortBaseType(this.equipData.tableData.type);
  }

  public override ulong GetUniqID() => this.equipData.uniqueID;

  public override uint GetTableID() => this.equipData.tableID;

  public override string GetName() => this.equipData.tableData.name;

  public override int GetIconID()
  {
    return this.equipData.tableData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
  }

  public override ITEM_ICON_TYPE GetIconType()
  {
    return ItemIcon.GetItemIconType(this.equipData.tableData.type);
  }

  public override ELEMENT_TYPE GetIconElement() => this.equipData.GetTargetElementPriorityToTable();

  public override int GetNum() => -1;

  public override RARITY_TYPE GetRarity() => this.equipData.tableData.rarity;

  public override int GetSalePrice() => this.equipData.sellPrice;

  public override bool CanSale() => !this.IsEquipping() && !this.equipData.isFavorite;

  public override int GetLevel() => this.equipData.level;

  public override ItemStatus GetItemStatus() => this.equipData.GetEquipSkillParam();

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.EQUIP_ITEM;

  public override bool IsExceeded() => this.equipData.exceed > 0;

  public override GET_TYPE GetGetType() => this.equipData.tableData.getType;

  public override uint GetMainorSortWeight()
  {
    return (uint) (0 + ((int) this.EquipmentTypeToMinorSortValue(this.equipData.tableData.type) << 26) + ((int) this.ElementTypeToMinorSortValue(this.GetIconElement()) << 23) + ((int) this.GetRarity() << 20) + ((int) this.equipData.tableData.spAttackType << 15) + (this.equipData.level << 8) + ((int) this.GetTypeToMinorSortValue(this.GetGetType()) << 7));
  }

  public override bool IsUniqueEquipping()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "UniqueStatusScene" ? MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocal(this.equipData) : MonoBehaviourSingleton<StatusManager>.I.IsUniqueEquipping(this.equipData);
  }

  public override bool IsHomeEquipping()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "StatusScene" ? MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocal(this.equipData) : MonoBehaviourSingleton<StatusManager>.I.IsEquipping(this.equipData);
  }

  public override bool IsEquipping()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "StatusScene" ? MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocal(this.equipData) || this.IsVisualEquip() || MonoBehaviourSingleton<StatusManager>.I.IsUniqueEquipping(this.equipData) : (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "UniqueStatusScene" ? MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocal(this.equipData) || this.IsVisualEquip() || MonoBehaviourSingleton<StatusManager>.I.IsEquipping(this.equipData) : this._EquipSomewhere() || this.IsVisualEquip());
  }

  public override bool IsEquipSomewhere()
  {
    bool flag = this.IsEquipping();
    if (!flag)
      flag = this._EquipSomewhere();
    return flag;
  }

  private bool _EquipSomewhere()
  {
    int set_no1 = 0;
    for (int index1 = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum(); set_no1 < index1; ++set_no1)
    {
      int equip_slot = 0;
      for (int index2 = 7; equip_slot < index2; ++equip_slot)
      {
        EquipItemInfo equippingItemInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemInfo(equip_slot, set_no1);
        if (equippingItemInfo != null && (long) equippingItemInfo.uniqueID == (long) this.equipData.uniqueID)
          return true;
      }
    }
    int set_no2 = 0;
    for (int index3 = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum(); set_no2 < index3; ++set_no2)
    {
      int equip_slot = 0;
      for (int index4 = 7; equip_slot < index4; ++equip_slot)
      {
        EquipItemInfo equippingItemInfo = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemInfo(equip_slot, set_no2);
        if (equippingItemInfo != null && (long) equippingItemInfo.uniqueID == (long) this.equipData.uniqueID)
          return true;
      }
    }
    return false;
  }

  public bool IsVisualEquip()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "StatusScene")
      return MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocalVisual(this.equipData);
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    EQUIPMENT_TYPE type = this.equipData.tableData.type;
    bool flag = false;
    switch (type)
    {
      case EQUIPMENT_TYPE.ARMOR:
      case EQUIPMENT_TYPE.VISUAL_ARMOR:
        ulong result1 = 0;
        if (ulong.TryParse(userStatus.armorUniqId, out result1))
        {
          flag = (long) result1 == (long) this.GetUniqID();
          break;
        }
        break;
      case EQUIPMENT_TYPE.HELM:
      case EQUIPMENT_TYPE.VISUAL_HELM:
        ulong result2 = 0;
        if (ulong.TryParse(userStatus.helmUniqId, out result2))
        {
          flag = (long) result2 == (long) this.GetUniqID();
          break;
        }
        break;
      case EQUIPMENT_TYPE.ARM:
      case EQUIPMENT_TYPE.VISUAL_ARM:
        ulong result3 = 0;
        if (ulong.TryParse(userStatus.armUniqId, out result3))
        {
          flag = (long) result3 == (long) this.GetUniqID();
          break;
        }
        break;
      case EQUIPMENT_TYPE.LEG:
      case EQUIPMENT_TYPE.VISUAL_LEG:
        ulong result4 = 0;
        if (ulong.TryParse(userStatus.legUniqId, out result4))
        {
          flag = (long) result4 == (long) this.GetUniqID();
          break;
        }
        break;
      default:
        return false;
    }
    return flag;
  }

  public ItemIconDetail.ICON_STATUS GetIconStatus()
  {
    bool flag1 = this.IsLvMaxAndEnableEvolve();
    bool flag2 = this.IsEnoughMaterial();
    if ((!this.equipData.IsLevelMax() || this.equipData.tableData.IsEvolve() ? 0 : (this.equipData.IsExceedMax() ? 1 : 0)) != 0)
      return ItemIconDetail.ICON_STATUS.GROW_MAX;
    if (!flag2)
      return ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL;
    return flag1 ? ItemIconDetail.ICON_STATUS.VALID_EVOLVE : ItemIconDetail.ICON_STATUS.NONE;
  }

  private bool IsLvMaxAndEnableEvolve()
  {
    return this.equipData.tableData.IsEvolve() && this.equipData.IsLevelMax();
  }

  private bool IsEnoughMaterial()
  {
    if (this.equipData.IsLevelMax())
    {
      if (!this.equipData.tableData.IsEvolve())
        return true;
      EvolveEquipItemTable.EvolveEquipItemData[] evolveEquipItemData = Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemData(this.equipData.tableID);
      if (evolveEquipItemData == null)
      {
        Debug.LogWarning((object) ("Evolve Data is Not Found. : BaseItemID = " + (object) this.equipData.tableID));
        return false;
      }
      int index = 0;
      for (int length = evolveEquipItemData.Length; index < length; ++index)
      {
        if (MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(evolveEquipItemData[index].needMaterial) && MonoBehaviourSingleton<InventoryManager>.I.IsHaveingEquip(evolveEquipItemData[index].needEquip) && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money >= (int) evolveEquipItemData[index].needMoney)
          return true;
      }
      return false;
    }
    return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(this.equipData.nextNeedTableData.needMaterial) && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money >= this.equipData.nextNeedTableData.needMoney;
  }
}
