// Decompiled with JetBrains decompiler
// Type: GrowEquipItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class GrowEquipItemTable : Singleton<GrowEquipItemTable>
{
  private DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemData> growTableData;
  private DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData> needTableData;
  private DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData> needUniqueTableData;

  public DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemData> GrowTableData
  {
    get => this.growTableData;
  }

  public static DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemData> CreateGrowTableCSV(
    string csv_text)
  {
    return TableUtility.CreateDoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEquipItemTable.GrowEquipItemData>(GrowEquipItemTable.GrowEquipItemData.cb), "growId,level,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public void CreateGrowTable(string csv_text)
  {
    this.growTableData = GrowEquipItemTable.CreateGrowTableCSV(csv_text);
  }

  public void AddGrowTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemData>(this.growTableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEquipItemTable.GrowEquipItemData>(GrowEquipItemTable.GrowEquipItemData.cb), "growId,level,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public static DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData> CreateNeedTableCSV(
    string csv_text)
  {
    return TableUtility.CreateDoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEquipItemTable.GrowEquipItemNeedItemData>(GrowEquipItemTable.GrowEquipItemNeedItemData.cb), "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public void CreateNeedTable(string csv_text)
  {
    this.needTableData = GrowEquipItemTable.CreateNeedTableCSV(csv_text);
  }

  public void AddNeedTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData>(this.needTableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEquipItemTable.GrowEquipItemNeedItemData>(GrowEquipItemTable.GrowEquipItemNeedItemData.cb), "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public static DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemData> CreateGrowTableBinary(
    byte[] bytes)
  {
    return TableUtility.CreateDoubleUIntKeyTableFromBinary<GrowEquipItemTable.GrowEquipItemData>(bytes);
  }

  public void CreateGrowTable(byte[] bytes)
  {
    this.growTableData = GrowEquipItemTable.CreateGrowTableBinary(bytes);
  }

  public static DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData> CreateNeedTableBinary(
    byte[] bytes)
  {
    DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData> needTableBinary = new DoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData>();
    BinaryTableReader reader = new BinaryTableReader(bytes);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      uint key2 = reader.ReadUInt32();
      GrowEquipItemTable.GrowEquipItemNeedItemData itemNeedItemData = new GrowEquipItemTable.GrowEquipItemNeedItemData();
      itemNeedItemData.LoadFromBinary(reader, ref key1, ref key2);
      needTableBinary.Add(key1, key2, itemNeedItemData);
    }
    return needTableBinary;
  }

  public void CreateNeedTable(byte[] bytes)
  {
    this.needTableData = GrowEquipItemTable.CreateNeedTableBinary(bytes);
  }

  public void CreateNeedTable(MemoryStream stream)
  {
    this.needTableData = TableUtility.CreateDoubleUIntKeyTableFromBinary<GrowEquipItemTable.GrowEquipItemNeedItemData>(stream);
  }

  public void CreateNeedUniqueTable(string csv_text)
  {
    this.needUniqueTableData = TableUtility.CreateDoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEquipItemTable.GrowEquipItemNeedItemData>(GrowEquipItemTable.GrowEquipItemNeedItemData.cb), "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public void AddNeedUniqueTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData>(this.needUniqueTableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEquipItemTable.GrowEquipItemNeedItemData>(GrowEquipItemTable.GrowEquipItemNeedItemData.cb), "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public GrowEquipItemTable.GrowEquipItemData GetGrowEquipItemData(uint id, uint lv)
  {
    if (this.growTableData == null)
      return (GrowEquipItemTable.GrowEquipItemData) null;
    UIntKeyTable<GrowEquipItemTable.GrowEquipItemData> uintKeyTable = this.growTableData.Get(id);
    if (uintKeyTable == null)
    {
      Log.Error($"GrowEquipItemTable is NULL :: grow id = {(object) id} Lv = {(object) lv}");
      return (GrowEquipItemTable.GrowEquipItemData) null;
    }
    GrowEquipItemTable.GrowEquipItemData growEquipItemData1 = uintKeyTable.Get(lv);
    if (growEquipItemData1 != null && lv > 1U)
      return growEquipItemData1;
    GrowEquipItemTable.GrowEquipItemData under = (GrowEquipItemTable.GrowEquipItemData) null;
    GrowEquipItemTable.GrowEquipItemData over = (GrowEquipItemTable.GrowEquipItemData) null;
    uintKeyTable.ForEach((Action<GrowEquipItemTable.GrowEquipItemData>) (table =>
    {
      if ((uint) table.lv > lv && (over == null || (uint) table.lv < (uint) over.lv))
        over = table;
      if ((uint) table.lv > lv || under != null && (uint) table.lv <= (uint) under.lv)
        return;
      under = table;
    }));
    if (under != null && over == null)
      return under;
    if (under == null)
    {
      under = new GrowEquipItemTable.GrowEquipItemData()
      {
        lv = (XorUInt) 1U,
        id = (XorUInt) id,
        atk = new GrowRate()
      };
      under.atk.rate = (XorInt) 100;
      under.atk.add = (XorInt) 0;
      under.def = new GrowRate();
      under.def.rate = (XorInt) 100;
      under.def.add = (XorInt) 0;
      under.hp = new GrowRate();
      under.hp.rate = (XorInt) 100;
      under.hp.add = (XorInt) 0;
      under.elemAtk = new GrowRate[6];
      for (int index = 0; index < 6; ++index)
      {
        under.elemAtk[index] = new GrowRate();
        under.elemAtk[index].rate = (XorInt) 100;
        under.elemAtk[index].add = (XorInt) 0;
      }
      under.elemDef = new GrowRate[6];
      for (int index = 0; index < 6; ++index)
      {
        under.elemDef[index] = new GrowRate();
        under.elemDef[index].rate = (XorInt) 100;
        under.elemDef[index].add = (XorInt) 0;
      }
    }
    GrowEquipItemTable.GrowEquipItemData growEquipItemData2 = new GrowEquipItemTable.GrowEquipItemData();
    float num = (float) (lv - (uint) under.lv) / (float) ((uint) over.lv - (uint) under.lv);
    growEquipItemData2.id = (XorUInt) id;
    growEquipItemData2.lv = (XorUInt) lv;
    growEquipItemData2.atk = new GrowRate();
    growEquipItemData2.atk.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.atk.rate, (float) (int) over.atk.rate, num));
    growEquipItemData2.atk.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.atk.add, (float) (int) over.atk.add, num));
    growEquipItemData2.def = new GrowRate();
    growEquipItemData2.def.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.def.rate, (float) (int) over.def.rate, num));
    growEquipItemData2.def.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.def.add, (float) (int) over.def.add, num));
    growEquipItemData2.hp = new GrowRate();
    growEquipItemData2.hp.rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.hp.rate, (float) (int) over.hp.rate, num));
    growEquipItemData2.hp.add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.hp.add, (float) (int) over.hp.add, num));
    growEquipItemData2.elemAtk = new GrowRate[6];
    for (int index = 0; index < 6; ++index)
    {
      growEquipItemData2.elemAtk[index] = new GrowRate();
      growEquipItemData2.elemAtk[index].rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemAtk[index].rate, (float) (int) over.elemAtk[index].rate, num));
      growEquipItemData2.elemAtk[index].add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemAtk[index].add, (float) (int) over.elemAtk[index].add, num));
    }
    growEquipItemData2.elemDef = new GrowRate[6];
    for (int index = 0; index < 6; ++index)
    {
      growEquipItemData2.elemDef[index] = new GrowRate();
      growEquipItemData2.elemDef[index].rate = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemDef[index].rate, (float) (int) over.elemDef[index].rate, num));
      growEquipItemData2.elemDef[index].add = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) under.elemDef[index].add, (float) (int) over.elemDef[index].add, num));
    }
    return growEquipItemData2;
  }

  public GrowEquipItemTable.GrowEquipItemNeedItemData GetGrowEquipItemNeedItemData(uint id, uint lv)
  {
    if (this.needTableData == null)
      return (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    UIntKeyTable<GrowEquipItemTable.GrowEquipItemNeedItemData> uintKeyTable = this.needTableData.Get(id);
    if (uintKeyTable == null)
    {
      Log.Error($"GetGrowEquipItemNeedItemData is NULL :: need id = {(object) id} Lv = {(object) lv}");
      return (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    }
    GrowEquipItemTable.GrowEquipItemNeedItemData itemNeedItemData1 = uintKeyTable.Get(lv);
    if (itemNeedItemData1 != null)
      return itemNeedItemData1;
    GrowEquipItemTable.GrowEquipItemNeedItemData under = (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    GrowEquipItemTable.GrowEquipItemNeedItemData over = (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    uintKeyTable.ForEach((Action<GrowEquipItemTable.GrowEquipItemNeedItemData>) (table =>
    {
      if (table.lv > lv && (over == null || table.lv < over.lv))
        over = table;
      if (table.lv > lv || under != null && table.lv <= under.lv)
        return;
      under = table;
    }));
    if (under != null && over == null)
      return under;
    if (under == null)
      return (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    GrowEquipItemTable.GrowEquipItemNeedItemData itemNeedItemData2 = new GrowEquipItemTable.GrowEquipItemNeedItemData();
    float lerp_value = (float) (lv - under.lv) / (float) (over.lv - under.lv);
    itemNeedItemData2.id = id;
    itemNeedItemData2.lv = lv;
    List<NeedMaterial> material_list = new List<NeedMaterial>();
    Array.ForEach<NeedMaterial>(under.needMaterial, (Action<NeedMaterial>) (material_data => Array.ForEach<NeedMaterial>(over.needMaterial, (Action<NeedMaterial>) (over_need_material =>
    {
      if ((int) over_need_material.itemID != (int) material_data.itemID)
        return;
      material_list.Add(new NeedMaterial(over_need_material.itemID, Mathf.FloorToInt(Mathf.Lerp((float) material_data.num, (float) over_need_material.num, lerp_value))));
    }))));
    itemNeedItemData2.needMaterial = material_list.ToArray();
    itemNeedItemData2.needMoney = Mathf.FloorToInt(Mathf.Lerp((float) under.needMoney, (float) over.needMoney, lerp_value));
    return itemNeedItemData2;
  }

  public GrowEquipItemTable.GrowEquipItemNeedItemData GetGrowEquipItemNeedUniqueItemData(
    uint needUniqueId,
    uint lv)
  {
    if (needUniqueId == 0U)
      return (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    if (this.needUniqueTableData == null)
      return (GrowEquipItemTable.GrowEquipItemNeedItemData) null;
    return this.needUniqueTableData.Get(needUniqueId)?.Get(lv);
  }

  public class GrowEquipItemData : IDoubleUIntKeyBinaryTableData
  {
    public XorUInt id = (XorUInt) 0U;
    public XorUInt lv = (XorUInt) 0U;
    public GrowRate atk;
    public GrowRate def;
    public GrowRate hp;
    public GrowRate[] elemAtk;
    public GrowRate[] elemDef;
    public const string NT = "growId,level,atkRate,atkAdd,defRate,defAdd,hpRate,hpAdd,fireAtkRate,fireAtkAdd,waterAtkRate,waterAtkAdd,thunderAtkRate,thunderAtkAdd,earthAtkRate,earthAtkAdd,lightAtkRate,lightAtkAdd,darkAtkRate,darkAtkAdd,fireDefRate,fireDefAdd,waterDefRate,waterDefAdd,thunderDefRate,thunderDefAdd,earthDefRate,earthDefAdd,lightDefRate,lightDefAdd,darkDefRate,darkDefAdd";

    public static bool cb(
      CSVReader csv_reader,
      GrowEquipItemTable.GrowEquipItemData data,
      ref uint key1,
      ref uint key2)
    {
      data.id = (XorUInt) key1;
      data.lv = (XorUInt) key2;
      data.atk = new GrowRate();
      csv_reader.Pop(ref data.atk.rate);
      csv_reader.Pop(ref data.atk.add);
      data.def = new GrowRate();
      csv_reader.Pop(ref data.def.rate);
      csv_reader.Pop(ref data.def.add);
      data.hp = new GrowRate();
      csv_reader.Pop(ref data.hp.rate);
      csv_reader.Pop(ref data.hp.add);
      data.elemAtk = new GrowRate[6];
      int index1 = 0;
      for (int index2 = 6; index1 < index2; ++index1)
      {
        data.elemAtk[index1] = new GrowRate();
        csv_reader.Pop(ref data.elemAtk[index1].rate);
        csv_reader.Pop(ref data.elemAtk[index1].add);
      }
      data.elemDef = new GrowRate[6];
      int index3 = 0;
      for (int index4 = 6; index3 < index4; ++index3)
      {
        data.elemDef[index3] = new GrowRate();
        csv_reader.Pop(ref data.elemDef[index3].rate);
        csv_reader.Pop(ref data.elemDef[index3].add);
      }
      return true;
    }

    public int GetGrowParamAtk(int base_atk)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_atk, this.atk);
    }

    public int GetGrowParamDef(int base_def)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_def, this.def);
    }

    public int GetGrowParamHp(int base_hp)
    {
      return MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_hp, this.hp);
    }

    public int[] GetGrowParamElemAtk(int[] base_elem_atk)
    {
      int length = base_elem_atk.Length;
      int[] growParamElemAtk = new int[length];
      int index1 = 0;
      for (int index2 = length; index1 < index2; ++index1)
        growParamElemAtk[index1] = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_elem_atk[index1], this.elemAtk[index1], true);
      return growParamElemAtk;
    }

    public int[] GetGrowParamElemDef(int[] base_elem_def)
    {
      int length = base_elem_def.Length;
      int[] growParamElemDef = new int[length];
      int index1 = 0;
      for (int index2 = length; index1 < index2; ++index1)
        growParamElemDef[index1] = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(base_elem_def[index1], this.elemDef[index1], true);
      return growParamElemDef;
    }

    public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
    {
      this.id = (XorUInt) key1;
      this.lv = (XorUInt) key2;
      this.atk = new GrowRate();
      this.atk.rate = (XorInt) reader.ReadInt32();
      this.atk.add = (XorInt) reader.ReadInt32();
      this.def = new GrowRate();
      this.def.rate = (XorInt) reader.ReadInt32();
      this.def.add = (XorInt) reader.ReadInt32();
      this.hp = new GrowRate();
      this.hp.rate = (XorInt) reader.ReadInt32();
      this.hp.add = (XorInt) reader.ReadInt32();
      this.elemAtk = new GrowRate[6];
      int index1 = 0;
      for (int index2 = 6; index1 < index2; ++index1)
      {
        this.elemAtk[index1] = new GrowRate();
        this.elemAtk[index1].rate = (XorInt) reader.ReadInt32();
        this.elemAtk[index1].add = (XorInt) reader.ReadInt32();
      }
      this.elemDef = new GrowRate[6];
      int index3 = 0;
      for (int index4 = 6; index3 < index4; ++index3)
      {
        this.elemDef[index3] = new GrowRate();
        this.elemDef[index3].rate = (XorInt) reader.ReadInt32();
        this.elemDef[index3].add = (XorInt) reader.ReadInt32();
      }
    }

    public void DumpBinary(BinaryWriter writer)
    {
      writer.Write((int) this.atk.rate);
      writer.Write((int) this.atk.add);
      writer.Write((int) this.def.rate);
      writer.Write((int) this.def.add);
      writer.Write((int) this.hp.rate);
      writer.Write((int) this.hp.add);
      int index1 = 0;
      for (int index2 = 6; index1 < index2; ++index1)
      {
        writer.Write((int) this.elemAtk[index1].rate);
        writer.Write((int) this.elemAtk[index1].add);
      }
      int index3 = 0;
      for (int index4 = 6; index3 < index4; ++index3)
      {
        writer.Write((int) this.elemDef[index3].rate);
        writer.Write((int) this.elemDef[index3].add);
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is GrowEquipItemTable.GrowEquipItemData growEquipItemData))
        return false;
      bool flag = (int) this.id.value == (int) growEquipItemData.id.value && (int) this.lv.value == (int) growEquipItemData.lv.value && this.atk.Equals((object) growEquipItemData.atk) && this.def.Equals((object) growEquipItemData.def) && this.hp.Equals((object) growEquipItemData.hp);
      for (int index = 0; index < this.elemAtk.Length; ++index)
        flag = flag && this.elemAtk[index].Equals((object) growEquipItemData.elemAtk[index]);
      for (int index = 0; index < this.elemDef.Length; ++index)
        flag = flag && this.elemDef[index].Equals((object) growEquipItemData.elemDef[index]);
      return flag;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => $"id:{(object) this.id}, lv:{(object) this.lv}";
  }

  public class GrowEquipItemNeedItemData : IDoubleUIntKeyBinaryTableData
  {
    public uint id;
    public uint lv;
    public NeedMaterial[] needMaterial;
    public int needMoney;
    private static readonly int NEED_MATERIAL_LENGTH_MAX = 10;
    public const string NT = "needId,level,itemID_0,itemNum_0,itemID_1,itemNum_1,itemID_2,itemNum_2,itemID_3,itemNum_3,itemID_4,itemNum_4,itemID_5,itemNum_5,itemID_6,itemNum_6,itemID_7,itemNum_7,itemID_8,itemNum_8,itemID_9,itemNum_9,money";
    private static List<NeedMaterial> need_material = new List<NeedMaterial>();

    public static bool cb(
      CSVReader csv_reader,
      GrowEquipItemTable.GrowEquipItemNeedItemData data,
      ref uint key1,
      ref uint key2)
    {
      data.id = key1;
      data.lv = key2;
      List<NeedMaterial> needMaterialList = new List<NeedMaterial>();
      for (int index = 0; index < GrowEquipItemTable.GrowEquipItemNeedItemData.NEED_MATERIAL_LENGTH_MAX; ++index)
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
      return true;
    }

    public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
    {
      this.id = key1;
      this.lv = key2;
      GrowEquipItemTable.GrowEquipItemNeedItemData.need_material.Clear();
      for (int index = 0; index < GrowEquipItemTable.GrowEquipItemNeedItemData.NEED_MATERIAL_LENGTH_MAX; ++index)
      {
        uint _item_id = reader.ReadUInt32();
        int _num = reader.ReadInt32();
        if (_item_id != 0U && _num != 0)
          GrowEquipItemTable.GrowEquipItemNeedItemData.need_material.Add(new NeedMaterial(_item_id, _num));
      }
      this.needMaterial = GrowEquipItemTable.GrowEquipItemNeedItemData.need_material.ToArray();
      this.needMoney = reader.ReadInt32();
    }

    public void DumpBinary(BinaryWriter writer)
    {
      for (int index = 0; index < GrowEquipItemTable.GrowEquipItemNeedItemData.NEED_MATERIAL_LENGTH_MAX; ++index)
      {
        if (index < this.needMaterial.Length)
        {
          NeedMaterial needMaterial = this.needMaterial[index];
          writer.Write(needMaterial.itemID);
          writer.Write(needMaterial.num);
        }
        else
        {
          writer.Write(0U);
          writer.Write(0);
        }
      }
      writer.Write(this.needMoney);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is GrowEquipItemTable.GrowEquipItemNeedItemData itemNeedItemData))
        return false;
      bool flag = (int) this.id == (int) itemNeedItemData.id && (int) this.lv == (int) itemNeedItemData.lv && this.needMoney == itemNeedItemData.needMoney;
      if (this.needMaterial.Length == itemNeedItemData.needMaterial.Length)
      {
        for (int index = 0; index < this.needMaterial.Length; ++index)
          flag = flag && this.needMaterial[index].Equals((object) itemNeedItemData.needMaterial[index]);
      }
      else
        flag = false;
      return flag;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
      return $"id:{(object) this.id}, lv:{(object) this.lv}, needMoney:{(object) this.needMoney}";
    }
  }
}
