// Decompiled with JetBrains decompiler
// Type: AccessoryTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class AccessoryTable : Singleton<AccessoryTable>, IDataTable
{
  private UIntKeyTable<AccessoryTable.AccessoryData> dataTable;
  private UIntKeyTable<AccessoryTable.AccessoryInfoData> infoTable;

  public void CreateTable(string text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<AccessoryTable.AccessoryData>(text, new TableUtility.CallBackUIntKeyReadCSV<AccessoryTable.AccessoryData>(AccessoryTable.AccessoryData.cb), "accessoryId,rarity,name,orderValue,attachPlaceBit,getType,price,cantSell,detailScale");
    this.dataTable.TrimExcess();
  }

  public void CreateInfoTable(string text)
  {
    this.infoTable = TableUtility.CreateUIntKeyTable<AccessoryTable.AccessoryInfoData>(text, new TableUtility.CallBackUIntKeyReadCSV<AccessoryTable.AccessoryInfoData>(AccessoryTable.AccessoryInfoData.cb), "id,accessoryId,attachPlace,node,px,py,pz,rx,ry,rz,sx,sy,sz");
    this.infoTable.TrimExcess();
  }

  public AccessoryTable.AccessoryData GetData(uint aid)
  {
    return this.dataTable == null ? (AccessoryTable.AccessoryData) null : this.dataTable.Get(aid) ?? (AccessoryTable.AccessoryData) null;
  }

  public void ForEachData(Action<AccessoryTable.AccessoryData> cb) => this.dataTable.ForEach(cb);

  public AccessoryTable.AccessoryInfoData GetInfoData(uint uid)
  {
    return this.infoTable == null ? (AccessoryTable.AccessoryInfoData) null : this.infoTable.Get(uid) ?? (AccessoryTable.AccessoryInfoData) null;
  }

  public List<AccessoryTable.AccessoryInfoData> GetInfoList(uint aid)
  {
    if (this.infoTable == null)
      return (List<AccessoryTable.AccessoryInfoData>) null;
    List<AccessoryTable.AccessoryInfoData> list = new List<AccessoryTable.AccessoryInfoData>();
    this.infoTable.ForEach((Action<AccessoryTable.AccessoryInfoData>) (i =>
    {
      if ((int) i.accessoryId != (int) aid)
        return;
      list.Add(i);
    }));
    return list;
  }

  public class AccessoryData
  {
    public uint accessoryId;
    public RARITY_TYPE rarity;
    public string name;
    public int orderValue;
    public uint attachPlaceBit;
    public GET_TYPE getType;
    public int price;
    public bool cantSell;
    public float detailScale;
    public string descript;
    public string descriptPart;
    public const string NT = "accessoryId,rarity,name,orderValue,attachPlaceBit,getType,price,cantSell,detailScale";

    public static bool cb(CSVReader csv_reader, AccessoryTable.AccessoryData data, ref uint key)
    {
      data.accessoryId = key;
      csv_reader.PopEnum<RARITY_TYPE>(ref data.rarity, RARITY_TYPE.D);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.orderValue);
      csv_reader.Pop(ref data.attachPlaceBit);
      csv_reader.PopEnum<GET_TYPE>(ref data.getType, GET_TYPE.FREE);
      csv_reader.Pop(ref data.price);
      int num = 0;
      csv_reader.Pop(ref num);
      data.cantSell = num != 0;
      csv_reader.Pop(ref data.detailScale);
      bool flag = true;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index <= 9; ++index)
      {
        if (((long) data.attachPlaceBit & (long) (1 << index)) != 0L)
        {
          if (flag)
            flag = false;
          else
            stringBuilder.Append("/");
          switch (index)
          {
            case 0:
              stringBuilder.Append("Head");
              continue;
            case 1:
              stringBuilder.Append("Face");
              continue;
            case 2:
              stringBuilder.Append("Right Shoulder");
              continue;
            case 3:
              stringBuilder.Append("Left Shoulder");
              continue;
            case 4:
              stringBuilder.Append("Right Arm");
              continue;
            case 5:
              stringBuilder.Append("Left Arm");
              continue;
            case 6:
              stringBuilder.Append("Chest");
              continue;
            case 7:
              stringBuilder.Append("Hip");
              continue;
            case 8:
              stringBuilder.Append("Right Leg");
              continue;
            case 9:
              stringBuilder.Append("Left Leg");
              continue;
            default:
              continue;
          }
        }
      }
      data.descriptPart = stringBuilder.ToString();
      stringBuilder.Remove(0, stringBuilder.Length);
      stringBuilder.Append("Equippable Areas:");
      stringBuilder.AppendLine();
      stringBuilder.Append(data.descriptPart);
      data.descript = stringBuilder.ToString();
      return true;
    }
  }

  public class AccessoryInfoData
  {
    public uint id;
    public uint accessoryId;
    public ACCESSORY_PART attachPlace;
    public string node;
    public Vector3 offset;
    public Quaternion rotation;
    public Vector3 scale;
    public const string NT = "id,accessoryId,attachPlace,node,px,py,pz,rx,ry,rz,sx,sy,sz";

    public static bool cb(
      CSVReader csv_reader,
      AccessoryTable.AccessoryInfoData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.accessoryId);
      csv_reader.PopEnum<ACCESSORY_PART>(ref data.attachPlace, ACCESSORY_PART.NONE);
      csv_reader.Pop(ref data.node);
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      csv_reader.Pop(ref num1);
      csv_reader.Pop(ref num2);
      csv_reader.Pop(ref num3);
      data.offset = new Vector3(num1, num2, num3);
      csv_reader.Pop(ref num1);
      csv_reader.Pop(ref num2);
      csv_reader.Pop(ref num3);
      data.rotation = Quaternion.Euler(num1, num2, num3);
      csv_reader.Pop(ref num1);
      csv_reader.Pop(ref num2);
      csv_reader.Pop(ref num3);
      data.scale = new Vector3(num1, num2, num3);
      return true;
    }
  }
}
