// Decompiled with JetBrains decompiler
// Type: AvatarTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AvatarTable : Singleton<AvatarTable>, IDataTable
{
  private UIntKeyTable<AvatarTable.AvatarData> avatarTable;

  public int[] manHeadIDs { get; private set; }

  public int[] womanHeadIDs { get; private set; }

  public int[] manFaceIDs { get; private set; }

  public int[] womanFaceIDs { get; private set; }

  public Color[] skinColors { get; private set; }

  public Color[] hairColors { get; private set; }

  public int[] defaultHasManHeadIndexes { get; private set; }

  public int[] defaultHasWomanHeadIndexes { get; private set; }

  public int[] defaultHasManFaceIndexes { get; private set; }

  public int[] defaultHasWomanFaceIndexes { get; private set; }

  public int[] defaultHasSkinColorIndexes { get; private set; }

  public int[] defaultHasHairColorIndexes { get; private set; }

  public void CreateTable(string csv_table)
  {
    this.avatarTable = TableUtility.CreateUIntKeyTable<AvatarTable.AvatarData>(csv_table, new TableUtility.CallBackUIntKeyReadCSV<AvatarTable.AvatarData>(AvatarTable.AvatarData.cb), "dataIndex,rawId,type,name,index,manHeadID,womanHeadID,manFaceID,womanFaceID,R,G,B,R2,G2,B2,defaultHasManHeadIndex,defaultHasWomanHeadIndex,defaultHasManFaceIndex,defaultHasWomanFaceIndex,defaultHasSkinColorIndex,dafaultHasHairColorIndex");
    this.avatarTable.TrimExcess();
    this.ConvertTable();
  }

  public void AddTable(string csv_table)
  {
    TableUtility.AddUIntKeyTable<AvatarTable.AvatarData>(this.avatarTable, csv_table, new TableUtility.CallBackUIntKeyReadCSV<AvatarTable.AvatarData>(AvatarTable.AvatarData.cb), "dataIndex,rawId,type,name,index,manHeadID,womanHeadID,manFaceID,womanFaceID,R,G,B,R2,G2,B2,defaultHasManHeadIndex,defaultHasWomanHeadIndex,defaultHasManFaceIndex,defaultHasWomanFaceIndex,defaultHasSkinColorIndex,dafaultHasHairColorIndex");
  }

  public void ConvertTable()
  {
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    List<int> intList3 = new List<int>();
    List<int> intList4 = new List<int>();
    List<Color> colorList1 = new List<Color>();
    List<Color> colorList2 = new List<Color>();
    List<int> intList5 = new List<int>();
    List<int> intList6 = new List<int>();
    List<int> intList7 = new List<int>();
    List<int> intList8 = new List<int>();
    List<int> intList9 = new List<int>();
    List<int> intList10 = new List<int>();
    for (int index = 0; index < this.GetCount(); ++index)
    {
      AvatarTable.AvatarData data = this.GetData(index);
      if (data.manHeadID >= 0)
        intList1.Add(data.manHeadID);
      if (data.womanHeadID >= 0)
        intList2.Add(data.womanHeadID);
      if (data.manFaceID >= 0)
        intList3.Add(data.manFaceID);
      if (data.womanFaceID >= 0)
        intList4.Add(data.womanFaceID);
      if (data.hasSkinColor)
        colorList1.Add(Color32.op_Implicit(data.skinColor));
      if (data.hasHairColor)
        colorList2.Add(Color32.op_Implicit(data.hairColor));
      if (data.defaultHasManHeadIndex >= 0)
        intList5.Add(data.defaultHasManHeadIndex);
      if (data.defaultHasWomanHeadIndex >= 0)
        intList6.Add(data.defaultHasWomanHeadIndex);
      if (data.defaultHasManFaceIndex >= 0)
        intList7.Add(data.defaultHasManFaceIndex);
      if (data.defaultHasWomanFaceIndex >= 0)
        intList8.Add(data.defaultHasWomanFaceIndex);
      if (data.defaultHasSkinColorIndex >= 0)
        intList9.Add(data.defaultHasSkinColorIndex);
      if (data.defaultHasHairColorIndex >= 0)
        intList10.Add(data.defaultHasHairColorIndex);
    }
    this.manHeadIDs = intList1.ToArray();
    this.womanHeadIDs = intList2.ToArray();
    this.manFaceIDs = intList3.ToArray();
    this.womanFaceIDs = intList4.ToArray();
    this.skinColors = colorList1.ToArray();
    this.hairColors = colorList2.ToArray();
    this.defaultHasManHeadIndexes = intList5.ToArray();
    this.defaultHasWomanHeadIndexes = intList6.ToArray();
    this.defaultHasManFaceIndexes = intList7.ToArray();
    this.defaultHasWomanFaceIndexes = intList8.ToArray();
    this.defaultHasSkinColorIndexes = intList9.ToArray();
    this.defaultHasHairColorIndexes = intList10.ToArray();
  }

  public AvatarTable.AvatarData GetData(int index)
  {
    return this.avatarTable == null || this.avatarTable.GetCount() <= index ? (AvatarTable.AvatarData) null : this.avatarTable.Get((uint) index);
  }

  public AvatarTable.AvatarData GetData(uint index)
  {
    return this.avatarTable == null || (long) this.avatarTable.GetCount() <= (long) index ? (AvatarTable.AvatarData) null : this.avatarTable.Get(index);
  }

  public string GetHeadName(bool isWoman, int index)
  {
    return this.GetName(isWoman ? AvatarTable.Type.WomanHead : AvatarTable.Type.ManHead, index);
  }

  public string GetFaceName(bool isWoman, int index)
  {
    return this.GetName(isWoman ? AvatarTable.Type.WomanFace : AvatarTable.Type.ManFace, index);
  }

  public string GetVoiceName(bool isWoman, int index)
  {
    return this.GetName(isWoman ? AvatarTable.Type.WomanVoice : AvatarTable.Type.ManVoice, index);
  }

  public int GetRawId(AvatarTable.Type type, int index)
  {
    for (int index1 = 0; index1 < this.avatarTable.GetCount(); ++index1)
    {
      AvatarTable.AvatarData data = this.GetData(index1);
      if ((AvatarTable.Type) data.type == type && (long) data.index == (long) index)
        return data.rawId;
    }
    return -1;
  }

  private string GetName(AvatarTable.Type type, int index)
  {
    for (int index1 = 0; index1 < this.avatarTable.GetCount(); ++index1)
    {
      AvatarTable.AvatarData data = this.GetData(index1);
      if ((AvatarTable.Type) data.type == type && (long) data.index == (long) index)
        return data.name;
    }
    return "";
  }

  public int GetCount() => this.avatarTable == null ? -1 : this.avatarTable.GetCount();

  public enum Type
  {
    ManHead = 1,
    WomanHead = 2,
    ManFace = 3,
    WomanFace = 4,
    SkinColor = 5,
    HairColor = 6,
    ManVoice = 7,
    WomanVoice = 8,
  }

  public class AvatarData
  {
    public uint id;
    public int rawId;
    public int type;
    public string name;
    public uint index;
    public int manHeadID = -1;
    public int womanHeadID = -1;
    public int manFaceID = -1;
    public int womanFaceID = -1;
    public bool hasSkinColor;
    public Color32 skinColor;
    public bool hasHairColor;
    public Color32 hairColor;
    public int defaultHasManHeadIndex = -1;
    public int defaultHasWomanHeadIndex = -1;
    public int defaultHasManFaceIndex = -1;
    public int defaultHasWomanFaceIndex = -1;
    public int defaultHasSkinColorIndex = -1;
    public int defaultHasHairColorIndex = -1;
    public const string NT = "dataIndex,rawId,type,name,index,manHeadID,womanHeadID,manFaceID,womanFaceID,R,G,B,R2,G2,B2,defaultHasManHeadIndex,defaultHasWomanHeadIndex,defaultHasManFaceIndex,defaultHasWomanFaceIndex,defaultHasSkinColorIndex,dafaultHasHairColorIndex";

    public static bool cb(CSVReader csv_reader, AvatarTable.AvatarData data, ref uint key)
    {
      csv_reader.Pop(ref data.rawId);
      csv_reader.Pop(ref data.type);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.index);
      csv_reader.Pop(ref data.manHeadID);
      csv_reader.Pop(ref data.womanHeadID);
      csv_reader.Pop(ref data.manFaceID);
      csv_reader.Pop(ref data.womanFaceID);
      data.hasSkinColor = (bool) csv_reader.PopColor24(ref data.skinColor);
      data.hasHairColor = (bool) csv_reader.PopColor24(ref data.hairColor);
      csv_reader.Pop(ref data.defaultHasManHeadIndex);
      csv_reader.Pop(ref data.defaultHasWomanHeadIndex);
      csv_reader.Pop(ref data.defaultHasManFaceIndex);
      csv_reader.Pop(ref data.defaultHasWomanFaceIndex);
      csv_reader.Pop(ref data.defaultHasSkinColorIndex);
      csv_reader.Pop(ref data.defaultHasHairColorIndex);
      return true;
    }
  }
}
