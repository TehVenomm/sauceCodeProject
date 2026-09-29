// Decompiled with JetBrains decompiler
// Type: NPCTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class NPCTable : Singleton<NPCTable>, IDataTable
{
  private UIntKeyTable<NPCTable.NPCData> npcDataTable;
  public List<int>[] npcTypeOnNpcIdList = new List<int>[Enum.GetNames(typeof (NPCTable.NPC_TYPE)).Length];

  public void CreateTable(string csv_text)
  {
    this.npcDataTable = TableUtility.CreateUIntKeyTable<NPCTable.NPCData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<NPCTable.NPCData>(NPCTable.NPCData.cb), "id,npcType,npcmdl,sex,face,scolor,hair,hcolor,bdy,hlm,arm,leg,anim,jp,displayName,specialMdl,questids");
    this.npcDataTable.TrimExcess();
    int index = 0;
    for (int length = this.npcTypeOnNpcIdList.Length; index < length; ++index)
      this.npcTypeOnNpcIdList[index] = new List<int>();
    this.npcDataTable.ForEach((Action<NPCTable.NPCData>) (npcData => this.npcTypeOnNpcIdList[(int) npcData.npcType].Add(npcData.id)));
  }

  public NPCTable.NPCData GetNPCData(int npc_id) => this.npcDataTable.Get((uint) npc_id);

  public NPCTable.NPCData GetNPCData(string name)
  {
    NPCTable.NPCData data = (NPCTable.NPCData) null;
    this.npcDataTable.ForEach((Action<NPCTable.NPCData>) (o =>
    {
      if (data != null || !(o.name == name))
        return;
      data = o;
    }));
    return data;
  }

  public NPCTable.NPCData GetNPCDataRandom(NPCTable.NPC_TYPE npc_type, List<int> exclusion_ids = null)
  {
    List<int> range = this.npcTypeOnNpcIdList[(int) npc_type].GetRange(0, this.npcTypeOnNpcIdList[(int) npc_type].Count);
    if (exclusion_ids != null)
    {
      int index1 = 0;
      for (int count = exclusion_ids.Count; index1 < count; ++index1)
      {
        int index2 = range.IndexOf(exclusion_ids[index1]);
        if (index2 >= 0)
          range.RemoveAt(index2);
      }
    }
    if (range.Count <= 0)
      range = this.npcTypeOnNpcIdList[(int) npc_type].GetRange(0, this.npcTypeOnNpcIdList[(int) npc_type].Count);
    int index = (int) ((double) Random.value * (double) range.Count);
    return this.npcDataTable.Get((uint) range[index]);
  }

  public NPCTable.NPCData GetNPCDataRandomFromQuestSpecial(int questid, List<int> exclusion_ids = null)
  {
    List<int> range = this.npcTypeOnNpcIdList[3].GetRange(0, this.npcTypeOnNpcIdList[3].Count);
    if (exclusion_ids != null)
    {
      int index1 = 0;
      for (int count = exclusion_ids.Count; index1 < count; ++index1)
      {
        int index2 = range.IndexOf(exclusion_ids[index1]);
        if (index2 >= 0)
          range.RemoveAt(index2);
      }
    }
    List<int> intList = new List<int>();
    for (int index3 = 0; index3 < range.Count; ++index3)
    {
      NPCTable.NPCData npcData = this.npcDataTable.Get((uint) range[index3]);
      if (npcData != null && npcData.questids != null && npcData.questids.Length != 0)
      {
        for (int index4 = 0; index4 < npcData.questids.Length; ++index4)
        {
          if (npcData.questids[index4] == questid)
          {
            intList.Add(npcData.id);
            break;
          }
        }
      }
    }
    if (intList.Count <= 0)
      return (NPCTable.NPCData) null;
    int index = (int) ((double) Random.value * (double) intList.Count);
    return this.npcDataTable.Get((uint) intList[index]);
  }

  public enum NPC_TYPE
  {
    OFFICIAL,
    FIGURE,
    FIGURE_TUTORIAL,
    QUEST_SPECIAL,
  }

  public class NPCData
  {
    public int id;
    public string name;
    public string displayName;
    public NPCTable.NPC_TYPE npcType;
    public int npcModelID = -1;
    public int specialModelID = -1;
    public int sexID;
    public int faceTypeID;
    public int skinColorID;
    public int hairStyleID;
    public int hairColorID;
    public int bdy;
    public int hlm;
    public int arm;
    public int leg;
    public string anim;
    public int[] questids;
    public const string NT = "id,npcType,npcmdl,sex,face,scolor,hair,hcolor,bdy,hlm,arm,leg,anim,jp,displayName,specialMdl,questids";

    public static bool cb(CSVReader csv_reader, NPCTable.NPCData data, ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop<NPCTable.NPC_TYPE>(ref data.npcType);
      csv_reader.Pop(ref data.npcModelID);
      csv_reader.Pop(ref data.sexID);
      csv_reader.Pop(ref data.faceTypeID);
      csv_reader.Pop(ref data.skinColorID);
      csv_reader.Pop(ref data.hairStyleID);
      csv_reader.Pop(ref data.hairColorID);
      csv_reader.Pop(ref data.bdy);
      csv_reader.Pop(ref data.hlm);
      csv_reader.Pop(ref data.arm);
      csv_reader.Pop(ref data.leg);
      csv_reader.Pop(ref data.anim);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.displayName);
      csv_reader.Pop(ref data.specialModelID);
      if (string.IsNullOrEmpty(data.displayName))
        data.displayName = data.name;
      string buff = "";
      csv_reader.Pop(ref buff);
      data.questids = TableUtility.ParseStringToIntArray(buff);
      return true;
    }

    public bool IsUsePlayerModel() => this.npcModelID <= -1;

    public PlayerLoadInfo CreatePlayerLoadInfo()
    {
      PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
      playerLoadInfo.SetFace(this.sexID, this.faceTypeID, this.skinColorID);
      playerLoadInfo.SetHair(this.sexID, this.hairStyleID, this.hairColorID);
      playerLoadInfo.SetEquipBody(this.sexID, (uint) this.bdy);
      playerLoadInfo.SetEquipHead(this.sexID, (uint) this.hlm);
      playerLoadInfo.SetEquipArm(this.sexID, (uint) this.arm);
      playerLoadInfo.SetEquipLeg(this.sexID, (uint) this.leg);
      return playerLoadInfo;
    }

    public void CopyCharaInfo(CharaInfo info)
    {
      info.userId = 0;
      info.name = this.displayName;
      info.sex = this.sexID;
      info.faceId = this.faceTypeID;
      info.hairId = this.hairStyleID;
      info.hairColorId = this.hairColorID;
      info.skinId = this.skinColorID;
      info.aId = this.bdy;
      info.hId = this.hlm;
      info.rId = this.arm;
      info.lId = this.leg;
    }

    public ModelLoaderBase LoadModel(
      GameObject go,
      bool need_shadow,
      bool enable_light_probe,
      Action<Animator> on_complete,
      bool useSpecialModel)
    {
      if (this.IsUsePlayerModel())
      {
        PlayerLoader loader = go.AddComponent<PlayerLoader>();
        PlayerLoadInfo playerLoadInfo = this.CreatePlayerLoadInfo();
        loader.StartLoad(playerLoadInfo, go.layer, 99, false, false, need_shadow, enable_light_probe, false, false, FieldManager.IsValidInField(), true, enable_light_probe ? ShaderGlobal.GetCharacterShaderType() : SHADER_TYPE.UI, (PlayerLoader.OnCompleteLoad) (o =>
        {
          if (on_complete == null)
            return;
          on_complete(loader.animator);
        }));
        return (ModelLoaderBase) loader;
      }
      NPCLoader loader1 = go.AddComponent<NPCLoader>();
      HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow());
      int npcModelId = Singleton<HomeThemeTable>.I.GetNpcModelID(homeThemeData, this.id);
      int num = npcModelId > 0 ? npcModelId : this.specialModelID;
      int npc_model_id = !useSpecialModel || num <= 0 ? this.npcModelID : num;
      loader1.Load(npc_model_id, go.layer, need_shadow, enable_light_probe, enable_light_probe ? ShaderGlobal.GetCharacterShaderType() : SHADER_TYPE.UI, (System.Action) (() =>
      {
        if (on_complete == null)
          return;
        on_complete(loader1.animator);
      }));
      return (ModelLoaderBase) loader1;
    }

    public override string ToString()
    {
      return $"id={this.id}, name={this.name}, type={this.npcType}, sex={this.sexID}";
    }
  }
}
