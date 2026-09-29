// Decompiled with JetBrains decompiler
// Type: EvolveEquipItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class EvolveEquipItemTable : Singleton<EvolveEquipItemTable>, IDataTable
{
  private UIntKeyTable<EvolveEquipItemTable.EvolveEquipItemData> tableData;

  public void CreateTable(string csv_text)
  {
    this.tableData = TableUtility.CreateUIntKeyTable<EvolveEquipItemTable.EvolveEquipItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<EvolveEquipItemTable.EvolveEquipItemData>(EvolveEquipItemTable.EvolveEquipItemData.cb), "evolveId,baseEquipItemId,nextEquipItemId,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money,equipItemID_0,equipItemNum_0,needLv_0,equipItemID_1,equipItemNum_1,needLv_1,equipItemID_2,equipItemNum_2,needLv_2,equipItemID_3,equipItemNum_3,needLv_3,equipItemID_4,equipItemNum_4,needLv_4,equipItemID_5,equipItemNum_5,needLv_5,equipItemID_6,equipItemNum_6,needLv_6,equipItemID_7,equipItemNum_7,needLv_7,equipItemID_8,equipItemNum_8,needLv_8,equipItemID_9,equipItemNum_9,needLv_9");
    this.tableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<EvolveEquipItemTable.EvolveEquipItemData>(this.tableData, csv_text, new TableUtility.CallBackUIntKeyReadCSV<EvolveEquipItemTable.EvolveEquipItemData>(EvolveEquipItemTable.EvolveEquipItemData.cb), "evolveId,baseEquipItemId,nextEquipItemId,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money,equipItemID_0,equipItemNum_0,needLv_0,equipItemID_1,equipItemNum_1,needLv_1,equipItemID_2,equipItemNum_2,needLv_2,equipItemID_3,equipItemNum_3,needLv_3,equipItemID_4,equipItemNum_4,needLv_4,equipItemID_5,equipItemNum_5,needLv_5,equipItemID_6,equipItemNum_6,needLv_6,equipItemID_7,equipItemNum_7,needLv_7,equipItemID_8,equipItemNum_8,needLv_8,equipItemID_9,equipItemNum_9,needLv_9");
  }

  public EvolveEquipItemTable.EvolveEquipItemData[] GetEvolveEquipItemData(uint base_equip_id)
  {
    if (this.tableData == null)
      return (EvolveEquipItemTable.EvolveEquipItemData[]) null;
    List<EvolveEquipItemTable.EvolveEquipItemData> list = new List<EvolveEquipItemTable.EvolveEquipItemData>();
    this.tableData.ForEach((Action<EvolveEquipItemTable.EvolveEquipItemData>) (table =>
    {
      if ((int) table.equipBaseItemID != (int) base_equip_id)
        return;
      list.Add(table);
    }));
    if (list.Count == 0)
      return (EvolveEquipItemTable.EvolveEquipItemData[]) null;
    list.Sort((Comparison<EvolveEquipItemTable.EvolveEquipItemData>) ((l, r) => (int) l.equipEvolveItemID - (int) r.equipEvolveItemID));
    return list.ToArray();
  }

  public EvolveEquipItemTable.EvolveEquipItemData GetEvolveEquipItemDataFromEvolveEquipId(
    uint evolve_equip_id)
  {
    return this.tableData == null ? (EvolveEquipItemTable.EvolveEquipItemData) null : this.tableData.Find((Predicate<EvolveEquipItemTable.EvolveEquipItemData>) (table => (int) table.equipEvolveItemID == (int) evolve_equip_id));
  }

  public class EvolveEquipItemData
  {
    public uint id;
    public uint equipBaseItemID;
    public uint equipEvolveItemID;
    public NeedMaterial[] needMaterial;
    public XorInt needMoney = (XorInt) 0;
    public NeedEquip[] needEquip;
    public const string NT = "evolveId,baseEquipItemId,nextEquipItemId,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money,equipItemID_0,equipItemNum_0,needLv_0,equipItemID_1,equipItemNum_1,needLv_1,equipItemID_2,equipItemNum_2,needLv_2,equipItemID_3,equipItemNum_3,needLv_3,equipItemID_4,equipItemNum_4,needLv_4,equipItemID_5,equipItemNum_5,needLv_5,equipItemID_6,equipItemNum_6,needLv_6,equipItemID_7,equipItemNum_7,needLv_7,equipItemID_8,equipItemNum_8,needLv_8,equipItemID_9,equipItemNum_9,needLv_9";

    public static bool cb(
      CSVReader csv_reader,
      EvolveEquipItemTable.EvolveEquipItemData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.equipBaseItemID);
      csv_reader.Pop(ref data.equipEvolveItemID);
      List<NeedMaterial> needMaterialList = new List<NeedMaterial>();
      for (int index = 0; index < 10; ++index)
      {
        uint _item_id = 0;
        int _num = 0;
        csv_reader.Pop(ref _item_id);
        csv_reader.Pop(ref _num);
        if (_item_id != 0U && _num != 0)
          needMaterialList.Add(new NeedMaterial(_item_id, _num));
      }
      data.needMaterial = needMaterialList.ToArray();
      csv_reader.Pop(ref data.needMoney);
      List<NeedEquip> needEquipList = new List<NeedEquip>();
      for (int index = 0; index < 10; ++index)
      {
        uint _equip_item_id = 0;
        int _num = 0;
        int _need_lv = 0;
        csv_reader.Pop(ref _equip_item_id);
        csv_reader.Pop(ref _num);
        csv_reader.Pop(ref _need_lv);
        if (_equip_item_id != 0U && _num != 0 && _need_lv != 0)
          needEquipList.Add(new NeedEquip(_equip_item_id, _num, _need_lv));
      }
      data.needEquip = needEquipList.ToArray();
      return true;
    }
  }
}
