// Decompiled with JetBrains decompiler
// Type: NpcLevelSpecialTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class NpcLevelSpecialTable : Singleton<NpcLevelSpecialTable>, IDataTable
{
  private UIntKeyTable<NpcLevelSpecialTable.NpcLevelSpecialData> dataTable;

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<NpcLevelSpecialTable.NpcLevelSpecialData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<NpcLevelSpecialTable.NpcLevelSpecialData>(NpcLevelSpecialTable.NpcLevelSpecialData.cb_special), "id,npcids,questids,lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv");
    this.dataTable.TrimExcess();
  }

  public NpcLevelSpecialTable.NpcLevelSpecialData GetNPCLevelSpecial(
    uint lv,
    int npcid,
    int questid)
  {
    if (this.dataTable == null || this.dataTable.GetCount() <= 0)
      return (NpcLevelSpecialTable.NpcLevelSpecialData) null;
    List<NpcLevelSpecialTable.NpcLevelSpecialData> candidates = new List<NpcLevelSpecialTable.NpcLevelSpecialData>();
    this.dataTable.ForEach((Action<NpcLevelSpecialTable.NpcLevelSpecialData>) (npc =>
    {
      if (!npc.ContainNPCID(npcid) || !npc.ContainQuestID(questid))
        return;
      candidates.Add(npc);
    }));
    NpcLevelSpecialTable.NpcLevelSpecialData npcInCandidates1 = this._getNpcInCandidates(candidates, lv);
    if (npcInCandidates1 != null)
      return npcInCandidates1;
    candidates.Clear();
    this.dataTable.ForEach((Action<NpcLevelSpecialTable.NpcLevelSpecialData>) (npc =>
    {
      if (!npc.ContainNPCID(npcid) || npc.HasQuestIds())
        return;
      candidates.Add(npc);
    }));
    NpcLevelSpecialTable.NpcLevelSpecialData npcInCandidates2 = this._getNpcInCandidates(candidates, lv);
    if (npcInCandidates2 != null)
      return npcInCandidates2;
    candidates.Clear();
    this.dataTable.ForEach((Action<NpcLevelSpecialTable.NpcLevelSpecialData>) (npc =>
    {
      if (npc.HasNPCIds() || !npc.ContainQuestID(questid))
        return;
      candidates.Add(npc);
    }));
    return this._getNpcInCandidates(candidates, lv) ?? (NpcLevelSpecialTable.NpcLevelSpecialData) null;
  }

  private NpcLevelSpecialTable.NpcLevelSpecialData _getNpcInCandidates(
    List<NpcLevelSpecialTable.NpcLevelSpecialData> candidates,
    uint lv)
  {
    NpcLevelSpecialTable.NpcLevelSpecialData npcInCandidates = (NpcLevelSpecialTable.NpcLevelSpecialData) null;
    if (candidates.Count > 0)
    {
      for (int index = 0; index < candidates.Count; ++index)
      {
        NpcLevelSpecialTable.NpcLevelSpecialData candidate = candidates[index];
        if (candidate.lv <= lv)
        {
          if (npcInCandidates == null)
            npcInCandidates = candidate;
          else if (npcInCandidates.lv < candidate.lv)
            npcInCandidates = candidate;
        }
      }
    }
    return npcInCandidates;
  }

  [Serializable]
  public class NpcLevelSpecialData : NpcLevelTable.NpcLevelData
  {
    public uint id;
    public int[] npcids;
    public int[] questids;
    public new const string NT = "id,npcids,questids,lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv";

    public static bool cb_special(
      CSVReader csv,
      NpcLevelSpecialTable.NpcLevelSpecialData data,
      ref uint key1)
    {
      data.id = key1;
      string buff1 = "";
      csv.Pop(ref buff1);
      data.npcids = TableUtility.ParseStringToIntArray(buff1);
      string buff2 = "";
      csv.Pop(ref buff2);
      data.questids = TableUtility.ParseStringToIntArray(buff2);
      csv.Pop(ref data.lv);
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
      return true;
    }

    public bool ContainNPCID(int npcid) => this.containInArray(this.npcids, npcid);

    public bool ContainQuestID(int questid) => this.containInArray(this.questids, questid);

    public bool HasNPCIds() => !this.isArrayNullOrEmpty(this.npcids);

    public bool HasQuestIds() => !this.isArrayNullOrEmpty(this.questids);

    private bool containInArray(int[] array, int id)
    {
      if (array == null)
        return false;
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] == id)
          return true;
      }
      return false;
    }

    private bool isArrayNullOrEmpty(int[] array) => array == null || array.Length == 0;
  }
}
