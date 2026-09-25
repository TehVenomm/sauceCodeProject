// Decompiled with JetBrains decompiler
// Type: CreatePickupItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class CreatePickupItemTable : Singleton<CreatePickupItemTable>, IDataTable
{
  private UIntKeyTable<CreatePickupItemTable.CreatePickupItemData> pickupTable;

  public void CreateTable(string csv_text)
  {
    this.pickupTable = TableUtility.CreateUIntKeyTable<CreatePickupItemTable.CreatePickupItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<CreatePickupItemTable.CreatePickupItemData>(CreatePickupItemTable.CreatePickupItemData.cb), "pickupId,createId,locationId,preShowTime,postShowTime,openOnly");
    this.pickupTable.TrimExcess();
  }

  public CreatePickupItemTable.CreatePickupItemData GetLevelTable(uint id)
  {
    if (this.pickupTable == null)
      return (CreatePickupItemTable.CreatePickupItemData) null;
    CreatePickupItemTable.CreatePickupItemData levelTable = this.pickupTable.Get(id);
    if (levelTable != null)
      return levelTable;
    Log.Error("CreatePickupItemData is NULL :: id(Lv) = " + (object) id);
    return (CreatePickupItemTable.CreatePickupItemData) null;
  }

  private int GetSortIndex(
    List<CreatePickupItemTable.SortData> sort_data,
    SmithCreateItemInfo search_data)
  {
    return sort_data.Find((Predicate<CreatePickupItemTable.SortData>) (_data => _data.data == search_data)).index;
  }

  public CreatePickupItemTable.CreatePickupItemData GetPickupCreateItem(uint create_item_id)
  {
    CreatePickupItemTable.CreatePickupItemData find_data = (CreatePickupItemTable.CreatePickupItemData) null;
    this.pickupTable.ForEach((Action<CreatePickupItemTable.CreatePickupItemData>) (data =>
    {
      if (find_data != null || (int) data.createTableID != (int) create_item_id)
        return;
      find_data = data;
    }));
    return find_data;
  }

  public SmithCreateItemInfo[] GetPickupItemAry(SortBase.TYPE item_type = SortBase.TYPE.EQUIP_ALL)
  {
    if (!Singleton<EquipItemTable>.IsValid() || !Singleton<QuestTable>.IsValid())
      return (SmithCreateItemInfo[]) null;
    List<SmithCreateItemInfo> list = new List<SmithCreateItemInfo>();
    List<CreatePickupItemTable.SortData> sort_data = new List<CreatePickupItemTable.SortData>();
    this.pickupTable.ForEach((Action<CreatePickupItemTable.CreatePickupItemData>) (pickup_data =>
    {
      if (pickup_data.eventLocationID != 0U)
        return;
      CreateEquipItemTable.CreateEquipItemData equipItemTableData = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemTableData(pickup_data.createTableID);
      if (equipItemTableData == null)
        return;
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemTableData.equipItemID);
      if (equipItemData == null)
        return;
      SmithCreateItemInfo _data = new SmithCreateItemInfo(equipItemData, equipItemTableData);
      SortBase.TYPE type;
      switch (_data.equipTableData.type)
      {
        case EQUIPMENT_TYPE.ONE_HAND_SWORD:
          type = SortBase.TYPE.ONE_HAND_SWORD;
          break;
        case EQUIPMENT_TYPE.TWO_HAND_SWORD:
          type = SortBase.TYPE.TWO_HAND_SWORD;
          break;
        case EQUIPMENT_TYPE.SPEAR:
          type = SortBase.TYPE.SPEAR;
          break;
        case EQUIPMENT_TYPE.PAIR_SWORDS:
          type = SortBase.TYPE.PAIR_SWORDS;
          break;
        case EQUIPMENT_TYPE.ARROW:
          type = SortBase.TYPE.ARROW;
          break;
        case EQUIPMENT_TYPE.ARMOR:
        case EQUIPMENT_TYPE.VISUAL_ARMOR:
          type = SortBase.TYPE.ARMOR;
          break;
        case EQUIPMENT_TYPE.HELM:
        case EQUIPMENT_TYPE.VISUAL_HELM:
          type = SortBase.TYPE.HELM;
          break;
        case EQUIPMENT_TYPE.ARM:
        case EQUIPMENT_TYPE.VISUAL_ARM:
          type = SortBase.TYPE.ARMOR;
          break;
        case EQUIPMENT_TYPE.LEG:
        case EQUIPMENT_TYPE.VISUAL_LEG:
          type = SortBase.TYPE.LEG;
          break;
        default:
          type = SortBase.TYPE.NONE;
          break;
      }
      if ((type & item_type) == SortBase.TYPE.NONE)
        return;
      bool flag = true;
      if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingKeyMaterial(equipItemTableData.needKeyOrder, equipItemTableData.needMaterial))
        flag = false;
      if ((int) equipItemTableData.researchLv > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.researchLv)
        flag = false;
      if (!flag)
        return;
      list.Add(_data);
      sort_data.Add(new CreatePickupItemTable.SortData(_data, (int) pickup_data.id));
    }));
    list.Sort((Comparison<SmithCreateItemInfo>) ((l, r) => this.GetSortIndex(sort_data, l) - this.GetSortIndex(sort_data, r)));
    return list.ToArray();
  }

  public class CreatePickupItemData
  {
    public uint id;
    public uint createTableID;
    public uint eventLocationID;
    public int preShowTime;
    public int postShowTime;
    public bool openOnly;
    public const string NT = "pickupId,createId,locationId,preShowTime,postShowTime,openOnly";

    public static bool cb(
      CSVReader csv_reader,
      CreatePickupItemTable.CreatePickupItemData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.createTableID);
      csv_reader.Pop(ref data.eventLocationID);
      csv_reader.Pop(ref data.preShowTime);
      csv_reader.Pop(ref data.postShowTime);
      csv_reader.Pop(ref data.openOnly);
      return true;
    }
  }

  private class SortData
  {
    public int index;
    public SmithCreateItemInfo data;

    public SortData(SmithCreateItemInfo _data, int _index)
    {
      this.data = _data;
      this.index = _index;
    }
  }
}
