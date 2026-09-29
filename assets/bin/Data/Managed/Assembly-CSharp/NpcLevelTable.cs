// Decompiled with JetBrains decompiler
// Type: NpcLevelTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class NpcLevelTable : Singleton<NpcLevelTable>, IDataTable
{
  private UIntKeyTable<List<NpcLevelTable.NpcLevelData>> dataTable;
  private List<uint> lvList = new List<uint>();

  public void CreateTable(string csv)
  {
    this.dataTable = TableUtility.CreateUIntKeyListTable<NpcLevelTable.NpcLevelData>(csv, new TableUtility.CallBackUIntKeyReadCSV<NpcLevelTable.NpcLevelData>(NpcLevelTable.NpcLevelData.CB), "lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv");
    this.dataTable.TrimExcess();
    this.dataTable.ForEach((Action<List<NpcLevelTable.NpcLevelData>>) (list =>
    {
      this.lvList.Add(list[0].lv);
      int index = 0;
      for (int count = list.Count; index < count; ++index)
        list[index].lvIndex = index;
    }));
    this.lvList.Sort();
  }

  public List<NpcLevelTable.NpcLevelData> GetNpcLevelList(uint lv) => this.dataTable.Get(lv);

  public NpcLevelTable.NpcLevelData GetNpcLevelRandom(uint lv)
  {
    List<NpcLevelTable.NpcLevelData> npcLevelList = this.GetNpcLevelList(this.lvList.FindLast((Predicate<uint>) (l => l <= lv)));
    if (npcLevelList == null || npcLevelList.Count <= 0)
      return (NpcLevelTable.NpcLevelData) null;
    int index = (int) ((double) Random.value * (double) npcLevelList.Count);
    return npcLevelList[index];
  }

  public NpcLevelTable.NpcLevelData GetNpcLevel(uint lv, int lv_index)
  {
    List<NpcLevelTable.NpcLevelData> npcLevelList = this.GetNpcLevelList(lv);
    if (npcLevelList == null || npcLevelList.Count <= 0)
      return (NpcLevelTable.NpcLevelData) null;
    return lv_index < 0 || npcLevelList.Count <= lv_index ? (NpcLevelTable.NpcLevelData) null : npcLevelList[lv_index];
  }

  [Serializable]
  public class NpcLevelData
  {
    public uint lv;
    public int hp;
    public int atk;
    public int[] atk_attribute = new int[6];
    public int def;
    public int[] tolerance = new int[6];
    public CharaInfo.EquipItem[] equipItems = new CharaInfo.EquipItem[7];
    public int lvIndex;
    public const string NT = "lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv";

    public static bool CB(CSVReader csv, NpcLevelTable.NpcLevelData data, ref uint key1)
    {
      data.lv = key1;
      csv.Pop(ref data.hp);
      csv.Pop(ref data.atk);
      for (int index = 0; index < 6; ++index)
        csv.Pop(ref data.atk_attribute[index]);
      csv.Pop(ref data.def);
      for (int index = 0; index < 6; ++index)
        csv.Pop(ref data.tolerance[index]);
      int index1 = 0;
      for (int length = data.equipItems.Length; index1 < length; ++index1)
      {
        data.equipItems[index1] = new CharaInfo.EquipItem();
        csv.Pop(ref data.equipItems[index1].eId);
        csv.Pop(ref data.equipItems[index1].lv);
      }
      data.lvIndex = 0;
      return true;
    }

    public void CopyHomeCharaInfo(
      CharaInfo info,
      StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo,
      float atkRate = 1f)
    {
      info.level = (XorInt) (int) this.lv;
      info.hp = (XorInt) this.hp;
      info.atk = (XorInt) (int) ((double) this.atk * (double) atkRate);
      info.def = (XorInt) this.def;
      int index1 = 0;
      for (int length = this.equipItems.Length; index1 < length; ++index1)
      {
        CharaInfo.EquipItem equipItem = this.equipItems[index1];
        if (equipItem.eId > 0)
        {
          info.equipSet.Add(equipItem);
          if (extentionInfo != null && index1 >= 0 && index1 < 3)
          {
            int num = info.equipSet.Count - 1;
            extentionInfo.weaponIndexList.Add(num);
          }
        }
      }
      if (extentionInfo == null)
        return;
      int index2 = Utility.Random(extentionInfo.weaponIndexList.Count);
      int weaponIndex = extentionInfo.weaponIndexList[0];
      extentionInfo.weaponIndexList[0] = extentionInfo.weaponIndexList[index2];
      extentionInfo.weaponIndexList[index2] = weaponIndex;
    }

    public override string ToString()
    {
      string str = $"{string.Empty}{(object) this.lv},{(object) this.hp},{(object) this.atk},{(object) this.def}";
      int index = 0;
      for (int length = this.equipItems.Length; index < length; ++index)
        str = $"{$"{$"{str + ","}[{(object) index}]"}id={(object) this.equipItems[index].eId},"}lv={(object) this.equipItems[index].lv}";
      return str;
    }
  }
}
