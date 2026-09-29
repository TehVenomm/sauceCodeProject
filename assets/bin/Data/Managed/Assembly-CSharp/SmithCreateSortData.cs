// Decompiled with JetBrains decompiler
// Type: SmithCreateSortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithCreateSortData : SortCompareData
{
  public SmithCreateItemInfo createData;

  public override object GetItemData() => (object) this.createData;

  public override void SetItem(object item) => this.createData = (SmithCreateItemInfo) item;

  public override void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
    switch (requirement)
    {
      case SortBase.SORT_REQUIREMENT.NUM:
        this.sortingData = 1L;
        break;
      case SortBase.SORT_REQUIREMENT.GET:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.RARITY:
        this.sortingData = (long) this.createData.equipTableData.rarity;
        break;
      case SortBase.SORT_REQUIREMENT.LV:
        this.sortingData = 1L;
        break;
      case SortBase.SORT_REQUIREMENT.ATK:
        this.sortingData = (long) ((int) this.createData.equipTableData.baseAtk + this.createData.equipTableData.baseElemAtk);
        break;
      case SortBase.SORT_REQUIREMENT.DEF:
        this.sortingData = (long) ((int) this.createData.equipTableData.baseDef + this.createData.equipTableData.baseElemDef);
        break;
      case SortBase.SORT_REQUIREMENT.SALE:
        this.sortingData = (long) this.createData.equipTableData.sale;
        break;
      case SortBase.SORT_REQUIREMENT.SOCKET:
        this.sortingData = (long) this.createData.equipTableData.maxSlot;
        break;
      case SortBase.SORT_REQUIREMENT.PRICE:
        this.sortingData = (long) (int) this.createData.smithCreateTableData.needMoney;
        break;
      case SortBase.SORT_REQUIREMENT.ELEMENT:
        this.sortingData = 6L - (long) this.GetIconElement();
        break;
      default:
        this.sortingData = 0L;
        CreatePickupItemTable.CreatePickupItemData pickupCreateItem = Singleton<CreatePickupItemTable>.I.GetPickupCreateItem(this.createData.smithCreateTableData.id);
        if (pickupCreateItem == null)
          break;
        this.sortingData = (long) pickupCreateItem.id;
        break;
    }
  }

  public override bool IsFavorite() => false;

  public override RARITY_TYPE GetRarity() => this.createData.equipTableData.rarity;

  public override ELEMENT_TYPE GetIconElement()
  {
    return !this.createData.equipTableData.IsWeapon() ? (ELEMENT_TYPE) this.createData.equipTableData.GetElemDefTypePriorityToTable() : (ELEMENT_TYPE) this.createData.equipTableData.GetElemAtkTypePriorityToTable();
  }

  public override int GetItemType()
  {
    return this.EquipmentTypeToSortBaseType(this.createData.equipTableData.type);
  }

  public override uint GetTableID() => this.createData.equipTableData.id;

  public override string GetName() => this.createData.equipTableData.name;

  public override ItemStatus GetItemStatus()
  {
    return (ItemStatus) this.createData.equipTableData.GetDefaultSkillBuffParam();
  }

  public override ITEM_ICON_TYPE GetIconType()
  {
    return ItemIcon.GetItemIconType(this.createData.equipTableData.type);
  }

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.EQUIP_ITEM;

  public override GET_TYPE GetGetType() => this.createData.equipTableData.getType;

  public override uint GetMainorSortWeight()
  {
    return (uint) (0 + ((int) this.EquipmentTypeToMinorSortValue(this.createData.equipTableData.type) << 26) + ((int) this.ElementTypeToMinorSortValue(this.GetIconElement()) << 23) + ((int) this.GetRarity() << 20) + ((int) this.createData.equipTableData.spAttackType << 15) + ((int) this.GetTypeToMinorSortValue(this.GetGetType()) << 7));
  }

  public ItemIconDetail.ICON_STATUS GetIconStatus()
  {
    return !MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(this.createData.smithCreateTableData.needMaterial) || (int) this.createData.smithCreateTableData.needMoney > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money ? ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL : ItemIconDetail.ICON_STATUS.NONE;
  }

  public override int getEquipFilterPay()
  {
    return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(this.createData) & 3;
  }

  public override int getEquipFilterCreatable()
  {
    return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(this.createData) & 12;
  }

  public override int getEquipFilterObtained()
  {
    return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(this.createData) & 48 /*0x30*/;
  }

  public override bool getEquipFilterPayAndCreatable(int filter)
  {
    int num1 = MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(this.createData);
    int num2 = num1 & 3;
    int num3 = num1 & 12;
    int num4 = num1 & 48 /*0x30*/;
    return (filter & num2) == 0 || (filter & num3) == 0 || (filter & num4) == 0;
  }
}
