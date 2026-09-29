// Decompiled with JetBrains decompiler
// Type: CreateEquipItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class CreateEquipItemTable : Singleton<CreateEquipItemTable>, IDataTable
{
  private UIntKeyTable<CreateEquipItemTable.CreateEquipItemData> tableData;

  public void CreateTable(string csv_text)
  {
    this.tableData = TableUtility.CreateUIntKeyTable<CreateEquipItemTable.CreateEquipItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<CreateEquipItemTable.CreateEquipItemData>(CreateEquipItemTable.CreateEquipItemData.cb), "createId,equipItemId,researchLv,pickupPriority,keyOrder,isKey_0,itemID_0,itemNum_0,isKey_1,itemID_1,itemNum_1,isKey_2,itemID_2,itemNum_2,isKey_3,itemID_3,itemNum_3,isKey_4,itemID_4,itemNum_4,isKey_5,itemID_5,itemNum_5,isKey_6,itemID_6,itemNum_6,isKey_7,itemID_7,itemNum_7,isKey_8,itemID_8,itemNum_8,isKey_9,itemID_9,itemNum_9,money");
    this.tableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<CreateEquipItemTable.CreateEquipItemData>(this.tableData, csv_text, new TableUtility.CallBackUIntKeyReadCSV<CreateEquipItemTable.CreateEquipItemData>(CreateEquipItemTable.CreateEquipItemData.cb), "createId,equipItemId,researchLv,pickupPriority,keyOrder,isKey_0,itemID_0,itemNum_0,isKey_1,itemID_1,itemNum_1,isKey_2,itemID_2,itemNum_2,isKey_3,itemID_3,itemNum_3,isKey_4,itemID_4,itemNum_4,isKey_5,itemID_5,itemNum_5,isKey_6,itemID_6,itemNum_6,isKey_7,itemID_7,itemNum_7,isKey_8,itemID_8,itemNum_8,isKey_9,itemID_9,itemNum_9,money");
  }

  public CreateEquipItemTable.CreateEquipItemData GetCreateEquipItemTableData(uint id)
  {
    return this.tableData == null ? (CreateEquipItemTable.CreateEquipItemData) null : this.tableData.Get(id);
  }

  public SmithCreateItemInfo[] GetCreateEquipItemDataAry(EQUIPMENT_TYPE type)
  {
    if (!Singleton<EquipItemTable>.IsValid())
      return (SmithCreateItemInfo[]) null;
    List<SmithCreateItemInfo> list = new List<SmithCreateItemInfo>();
    this.tableData.ForEach((Action<CreateEquipItemTable.CreateEquipItemData>) (create_equip_item_table =>
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(create_equip_item_table.equipItemID);
      if (equipItemData == null || equipItemData.type != type)
        return;
      list.Add(new SmithCreateItemInfo(equipItemData, create_equip_item_table));
    }));
    return list.ToArray();
  }

  public CreateEquipItemTable.CreateEquipItemData[] GetCreatableEquipItem(uint material_id)
  {
    if (!Singleton<EquipItemTable>.IsValid())
      return (CreateEquipItemTable.CreateEquipItemData[]) null;
    List<CreateEquipItemTable.CreateEquipItemData> list = new List<CreateEquipItemTable.CreateEquipItemData>();
    this.tableData.ForEach((Action<CreateEquipItemTable.CreateEquipItemData>) (create_equip_item_table =>
    {
      if ((int) create_equip_item_table.needMaterial[0].itemID != (int) material_id)
        return;
      list.Add(create_equip_item_table);
    }));
    list.Sort((Comparison<CreateEquipItemTable.CreateEquipItemData>) ((l, r) =>
    {
      int creatableEquipItem = (l.pickupPriority == 0 ? 100 : l.pickupPriority) - (r.pickupPriority == 0 ? 100 : r.pickupPriority);
      if (creatableEquipItem == 0)
        creatableEquipItem = (int) r.equipItemID - (int) l.equipItemID;
      return creatableEquipItem;
    }));
    return list.ToArray();
  }

  public CreateEquipItemTable.CreateEquipItemData[] GetSortedCreateEquipItemsByPart(uint materialId)
  {
    return ((IEnumerable<CreateEquipItemTable.CreateEquipItemData>) Singleton<CreateEquipItemTable>.I.GetCreatableEquipItem(materialId)).OrderBy<CreateEquipItemTable.CreateEquipItemData, int>((Func<CreateEquipItemTable.CreateEquipItemData, int>) (creatableEquipItem =>
    {
      switch (Singleton<EquipItemTable>.I.GetEquipItemData(creatableEquipItem.equipItemID).type)
      {
        case EQUIPMENT_TYPE.ONE_HAND_SWORD:
        case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        case EQUIPMENT_TYPE.SPEAR:
        case EQUIPMENT_TYPE.PAIR_SWORDS:
        case EQUIPMENT_TYPE.ARROW:
          return 1;
        case EQUIPMENT_TYPE.ARMOR:
        case EQUIPMENT_TYPE.VISUAL_ARMOR:
          return 3;
        case EQUIPMENT_TYPE.HELM:
        case EQUIPMENT_TYPE.HAIR:
        case EQUIPMENT_TYPE.VISUAL_HELM:
          return 2;
        case EQUIPMENT_TYPE.ARM:
        case EQUIPMENT_TYPE.VISUAL_ARM:
          return 4;
        case EQUIPMENT_TYPE.LEG:
        case EQUIPMENT_TYPE.VISUAL_LEG:
          return 5;
        default:
          return 0;
      }
    })).ToArray<CreateEquipItemTable.CreateEquipItemData>();
  }

  public CreateEquipItemTable.CreateEquipItemData GetCreateEquipItemByPart(
    uint materialId,
    EQUIPMENT_TYPE type)
  {
    CreateEquipItemTable.CreateEquipItemData[] creatableEquipItem = Singleton<CreateEquipItemTable>.I.GetCreatableEquipItem(materialId);
    int index = 0;
    for (int length = creatableEquipItem.Length; index < length; ++index)
    {
      CreateEquipItemTable.CreateEquipItemData createEquipItemByPart = creatableEquipItem[index];
      if (Singleton<EquipItemTable>.I.GetEquipItemData(createEquipItemByPart.equipItemID).type == type)
        return createEquipItemByPart;
    }
    return (CreateEquipItemTable.CreateEquipItemData) null;
  }

  public CreateEquipItemTable.CreateEquipItemData GetCreateItemDataByEquipItem(uint equipId)
  {
    return this.tableData.Find((Predicate<CreateEquipItemTable.CreateEquipItemData>) (x => (int) x.equipItemID == (int) equipId));
  }

  public class CreateEquipItemData
  {
    public uint id;
    public uint equipItemID;
    public XorInt researchLv = (XorInt) 0;
    public int pickupPriority;
    public LOGICAL_ORDER_TYPE needKeyOrder;
    public NeedMaterial[] needMaterial;
    public XorInt needMoney = (XorInt) 0;
    public const string NT = "createId,equipItemId,researchLv,pickupPriority,keyOrder,isKey_0,itemID_0,itemNum_0,isKey_1,itemID_1,itemNum_1,isKey_2,itemID_2,itemNum_2,isKey_3,itemID_3,itemNum_3,isKey_4,itemID_4,itemNum_4,isKey_5,itemID_5,itemNum_5,isKey_6,itemID_6,itemNum_6,isKey_7,itemID_7,itemNum_7,isKey_8,itemID_8,itemNum_8,isKey_9,itemID_9,itemNum_9,money";

    public static bool cb(
      CSVReader csv_reader,
      CreateEquipItemTable.CreateEquipItemData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.equipItemID);
      csv_reader.Pop(ref data.researchLv);
      csv_reader.Pop(ref data.pickupPriority);
      csv_reader.Pop<LOGICAL_ORDER_TYPE>(ref data.needKeyOrder);
      List<NeedMaterial> needMaterialList = new List<NeedMaterial>();
      for (int index = 0; index < 10; ++index)
      {
        bool _is_key = false;
        uint _item_id = 0;
        int _num = 0;
        csv_reader.Pop(ref _is_key);
        csv_reader.Pop(ref _item_id);
        csv_reader.Pop(ref _num);
        if (_item_id != 0U && _num != 0)
          needMaterialList.Add(new NeedMaterial(_is_key, _item_id, _num));
      }
      data.needMaterial = needMaterialList.ToArray();
      csv_reader.Pop(ref data.needMoney);
      return true;
    }
  }
}
