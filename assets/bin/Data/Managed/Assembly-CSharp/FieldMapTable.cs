// Decompiled with JetBrains decompiler
// Type: FieldMapTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldMapTable : Singleton<FieldMapTable>
{
  private UIntKeyTable<FieldMapTable.FieldMapTableData> fieldMapTable;
  private UIntKeyTable<FieldMapTable.PortalTableData> portalTable;
  private UIntKeyTable<List<FieldMapTable.PortalTableData>> portalSrcMapIDTable;
  private UIntKeyTable<List<FieldMapTable.EnemyPopTableData>> enemyPopTable;
  private UIntKeyTable<FieldMapTable.GatherPointTableData> gatherPointTable;
  private UIntKeyTable<List<FieldMapTable.GatherPointTableData>> gatherPointMapIDTable;
  private UIntKeyTable<FieldMapTable.GatherPointViewTableData> gatherPointViewTable;
  private UIntKeyTable<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointTable;
  private UIntKeyTable<List<FieldMapTable.FieldGimmickPointTableData>> fieldGimmickPointMapIDTable;
  private UIntKeyTable<FieldMapTable.FieldGimmickActionTableData> fieldGimmickActionTable;

  public void CreateFieldMapTable(string csv_text)
  {
    this.fieldMapTable = TableUtility.CreateUIntKeyTable<FieldMapTable.FieldMapTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.FieldMapTableData>(FieldMapTable.FieldMapTableData.cb), "mapId,regionId,mapName,stageName,happenStageName,fieldGrade,fieldMode,eventId,jumpPortalId,bgmId,happenBgmId,linkQuestID,childRegionId,iconId,questIconId,fieldBuffId,camOffsetPortraitPos,camOffsetPortraitRot,camOffsetLandscapePos,camOffsetLandscapeRot");
  }

  public void AddFieldMapTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<FieldMapTable.FieldMapTableData>(this.fieldMapTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.FieldMapTableData>(FieldMapTable.FieldMapTableData.cb), "mapId,regionId,mapName,stageName,happenStageName,fieldGrade,fieldMode,eventId,jumpPortalId,bgmId,happenBgmId,linkQuestID,childRegionId,iconId,questIconId,fieldBuffId,camOffsetPortraitPos,camOffsetPortraitRot,camOffsetLandscapePos,camOffsetLandscapeRot");
  }

  public void CreatePortalTable(string csv_text)
  {
    this.portalTable = TableUtility.CreateUIntKeyTable<FieldMapTable.PortalTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.PortalTableData>(FieldMapTable.PortalTableData.cb), "portalId,linkPortalId,srcMapId,srcX,srcZ,dstMapId,dstX,dstZ,dstDir,mapX,mapY,dstQuestId,showDeliveryId,hideQuestId,appearQuestId,appearDeliveryId,travelMapId,openPriority,portalPoint,notAppearText,placeText,startAt,banEnemy,appearRegionId");
    this.portalSrcMapIDTable = new UIntKeyTable<List<FieldMapTable.PortalTableData>>();
    this.portalTable.ForEach((Action<FieldMapTable.PortalTableData>) (portalData =>
    {
      uint srcMapId = portalData.srcMapID;
      List<FieldMapTable.PortalTableData> portalTableDataList = this.portalSrcMapIDTable.Get(srcMapId);
      if (portalTableDataList == null)
      {
        portalTableDataList = new List<FieldMapTable.PortalTableData>();
        this.portalSrcMapIDTable.Add(srcMapId, portalTableDataList);
      }
      portalTableDataList.Add(portalData);
    }));
  }

  public void AddPortalTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<FieldMapTable.PortalTableData>(this.portalTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.PortalTableData>(FieldMapTable.PortalTableData.cb), "portalId,linkPortalId,srcMapId,srcX,srcZ,dstMapId,dstX,dstZ,dstDir,mapX,mapY,dstQuestId,showDeliveryId,hideQuestId,appearQuestId,appearDeliveryId,travelMapId,openPriority,portalPoint,notAppearText,placeText,startAt,banEnemy,appearRegionId");
    this.portalSrcMapIDTable = new UIntKeyTable<List<FieldMapTable.PortalTableData>>();
    this.portalTable.ForEach((Action<FieldMapTable.PortalTableData>) (portalData =>
    {
      uint srcMapId = portalData.srcMapID;
      List<FieldMapTable.PortalTableData> portalTableDataList = this.portalSrcMapIDTable.Get(srcMapId);
      if (portalTableDataList == null)
      {
        portalTableDataList = new List<FieldMapTable.PortalTableData>();
        this.portalSrcMapIDTable.Add(srcMapId, portalTableDataList);
      }
      portalTableDataList.Add(portalData);
    }));
  }

  public void CreateEnemyPopTable(string csv_text)
  {
    this.enemyPopTable = TableUtility.CreateUIntKeyListTable<FieldMapTable.EnemyPopTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.EnemyPopTableData>(FieldMapTable.EnemyPopTableData.cb), "mapId,popX,rotY,popY,popZ,popRadius,enemyId,enemyLv,waveNo,popNumMin,popNumMax,popNumInit,popNumTotal,popTimeMin,popTimeMax,bigMonsterFlag,bossFlag,autoActivate,scountigRange,scoutingSight,scoutingAudibility,enemyPopType,escapeTime,walkSpeedMin,walkSpeedMax");
  }

  public void AddEnemyPopTable(string csv_text)
  {
    TableUtility.AddUIntKeyListTable<FieldMapTable.EnemyPopTableData>(this.enemyPopTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.EnemyPopTableData>(FieldMapTable.EnemyPopTableData.cb), "mapId,popX,rotY,popY,popZ,popRadius,enemyId,enemyLv,waveNo,popNumMin,popNumMax,popNumInit,popNumTotal,popTimeMin,popTimeMax,bigMonsterFlag,bossFlag,autoActivate,scountigRange,scoutingSight,scoutingAudibility,enemyPopType,escapeTime,walkSpeedMin,walkSpeedMax");
  }

  public void CreateGatherPointTable(string csv_text)
  {
    this.gatherPointTable = TableUtility.CreateUIntKeyTable<FieldMapTable.GatherPointTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.GatherPointTableData>(FieldMapTable.GatherPointTableData.cb), "pointId,pointMapId,pointX,pointZ,pointDir,viewId,maxNumRate,addNumRate,growthInterval,gimmickType,value1");
    this.gatherPointMapIDTable = new UIntKeyTable<List<FieldMapTable.GatherPointTableData>>();
    this.gatherPointTable.ForEach((Action<FieldMapTable.GatherPointTableData>) (pointData =>
    {
      uint pointMapId = pointData.pointMapID;
      List<FieldMapTable.GatherPointTableData> gatherPointTableDataList = this.gatherPointMapIDTable.Get(pointMapId);
      if (gatherPointTableDataList == null)
      {
        gatherPointTableDataList = new List<FieldMapTable.GatherPointTableData>();
        this.gatherPointMapIDTable.Add(pointMapId, gatherPointTableDataList);
      }
      gatherPointTableDataList.Add(pointData);
    }));
  }

  public void CreateGatherPointViewTable(string csv_text)
  {
    this.gatherPointViewTable = TableUtility.CreateUIntKeyTable<FieldMapTable.GatherPointViewTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.GatherPointViewTableData>(FieldMapTable.GatherPointViewTableData.cb), "id,modelId,modelHideNodeName,gatherEffectName,colRadius,targetRadius,targetEffectName,targetEffectShift,targetEffectHeight,actStateName,toolModelName,toolNodeName,iconId,itemDetailText");
  }

  public void CreateGimmickPointTable(string csv_text)
  {
    this.fieldGimmickPointTable = TableUtility.CreateUIntKeyTable<FieldMapTable.FieldGimmickPointTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.FieldGimmickPointTableData>(FieldMapTable.FieldGimmickPointTableData.cb), "pointId,gimmickType,pointMapId,pointX,pointZ,pointDir,value1,value2");
    this.fieldGimmickPointMapIDTable = new UIntKeyTable<List<FieldMapTable.FieldGimmickPointTableData>>();
    this.fieldGimmickPointTable.ForEach((Action<FieldMapTable.FieldGimmickPointTableData>) (pointData =>
    {
      uint pointMapId = pointData.pointMapID;
      List<FieldMapTable.FieldGimmickPointTableData> gimmickPointTableDataList = this.fieldGimmickPointMapIDTable.Get(pointMapId);
      if (gimmickPointTableDataList == null)
      {
        gimmickPointTableDataList = new List<FieldMapTable.FieldGimmickPointTableData>();
        this.fieldGimmickPointMapIDTable.Add(pointMapId, gimmickPointTableDataList);
      }
      gimmickPointTableDataList.Add(pointData);
    }));
  }

  public void CreateGimmickActionTable(string csv_text)
  {
    this.fieldGimmickActionTable = TableUtility.CreateUIntKeyTable<FieldMapTable.FieldGimmickActionTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapTable.FieldGimmickActionTableData>(FieldMapTable.FieldGimmickActionTableData.cb), "actionId,radius,start,duration,interval,reactionType,force,angle,loopTime");
  }

  public FieldMapTable.FieldMapTableData GetFieldMapData(uint id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (FieldMapTable.FieldMapTableData) null : this.fieldMapTable.Get(id);
  }

  public FieldMapTable.FieldMapTableData[] GetFieldMapDataInRegion(uint regionId)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (FieldMapTable.FieldMapTableData[]) null;
    List<FieldMapTable.FieldMapTableData> data = new List<FieldMapTable.FieldMapTableData>(20);
    this.fieldMapTable.ForEach((Action<FieldMapTable.FieldMapTableData>) (d =>
    {
      if ((int) d.regionId != (int) regionId)
        return;
      data.Add(d);
    }));
    return data.ToArray();
  }

  public FieldMapTable.PortalTableData GetPortalData(uint id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (FieldMapTable.PortalTableData) null : this.portalTable.Get(id);
  }

  public List<FieldMapTable.PortalTableData> GetPortalListByMapID(uint map_id, bool do_sort = false)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (List<FieldMapTable.PortalTableData>) null;
    List<FieldMapTable.PortalTableData> portalListByMapId = this.portalSrcMapIDTable.Get(map_id);
    if (portalListByMapId != null & do_sort)
      portalListByMapId.Sort((Comparison<FieldMapTable.PortalTableData>) ((l, r) => (int) l.openPriority - (int) r.openPriority));
    return portalListByMapId;
  }

  public List<FieldMapTable.PortalTableData> GetDeliveryRelationPortalData(uint delivery_id)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (List<FieldMapTable.PortalTableData>) null;
    List<FieldMapTable.PortalTableData> ret = new List<FieldMapTable.PortalTableData>();
    this.portalTable.ForEach((Action<FieldMapTable.PortalTableData>) (data =>
    {
      if ((int) data.appearDeliveryId != (int) delivery_id)
        return;
      ret.Add(data);
    }));
    List<FieldMapTable.PortalTableData> remove_list = new List<FieldMapTable.PortalTableData>();
    ret.ForEach((Action<FieldMapTable.PortalTableData>) (data =>
    {
      if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) data.srcMapID))
      {
        if (data.linkPortalId == 0U)
          return;
        FieldMapTable.PortalTableData portalTableData = this.portalTable.Get(data.linkPortalId);
        if (portalTableData == null)
          return;
        remove_list.Add(portalTableData);
      }
      else
        remove_list.Add(data);
    }));
    remove_list.ForEach((Action<FieldMapTable.PortalTableData>) (remove_portal => ret.RemoveAll((Predicate<FieldMapTable.PortalTableData>) (data => (int) data.portalID == (int) remove_portal.portalID))));
    return ret;
  }

  public List<FieldMapTable.EnemyPopTableData> GetEnemyPopList(uint map_id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (List<FieldMapTable.EnemyPopTableData>) null : this.enemyPopTable.Get(map_id);
  }

  public FieldMapTable.EnemyPopTableData GetEnemyPopData(uint map_id, int index)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (FieldMapTable.EnemyPopTableData) null;
    List<FieldMapTable.EnemyPopTableData> enemyPopList = this.GetEnemyPopList(map_id);
    if (enemyPopList == null || enemyPopList.Count <= 0)
      return (FieldMapTable.EnemyPopTableData) null;
    return index < 0 || enemyPopList.Count <= index ? (FieldMapTable.EnemyPopTableData) null : enemyPopList[index];
  }

  public List<FieldMapTable.EnemyPopTableData> GetRareOrBossEnemyList(int map_id)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (List<FieldMapTable.EnemyPopTableData>) null;
    List<FieldMapTable.EnemyPopTableData> enemyPopTableDataList = this.enemyPopTable.Get((uint) map_id);
    List<FieldMapTable.EnemyPopTableData> rareOrBossEnemyList = new List<FieldMapTable.EnemyPopTableData>();
    int index = 0;
    for (int count = enemyPopTableDataList.Count; index < count; ++index)
    {
      if (enemyPopTableDataList[index].enemyPopType != ENEMY_POP_TYPE.NONE)
        rareOrBossEnemyList.Add(enemyPopTableDataList[index]);
    }
    return rareOrBossEnemyList;
  }

  public uint GetTargetEnemyPopMapID(uint enemy_id)
  {
    uint ret = 0;
    this.enemyPopTable.ForEach((Action<List<FieldMapTable.EnemyPopTableData>>) (data => data.ForEach((Action<FieldMapTable.EnemyPopTableData>) (pop_data =>
    {
      if (pop_data.mapID > ret && ret != 0U || (int) pop_data.enemyID != (int) enemy_id || pop_data.enemyID == 0U)
        return;
      ret = pop_data.mapID;
    }))));
    return ret;
  }

  public List<uint> GetTargetEnemyPopMapIDs(uint enemy_id)
  {
    List<uint> ret = new List<uint>();
    this.enemyPopTable.ForEach((Action<List<FieldMapTable.EnemyPopTableData>>) (data => data.ForEach((Action<FieldMapTable.EnemyPopTableData>) (pop_data =>
    {
      if ((int) pop_data.enemyID != (int) enemy_id || pop_data.enemyID == 0U)
        return;
      ret.Add(pop_data.mapID);
    }))));
    return ret;
  }

  public FieldMapTable.GatherPointTableData GetGatherPointData(uint id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (FieldMapTable.GatherPointTableData) null : this.gatherPointTable.Get(id);
  }

  public List<FieldMapTable.GatherPointTableData> GetGatherPointListByMapID(uint map_id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (List<FieldMapTable.GatherPointTableData>) null : this.gatherPointMapIDTable.Get(map_id);
  }

  public FieldMapTable.GatherPointViewTableData GetGatherPointViewData(uint id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (FieldMapTable.GatherPointViewTableData) null : this.gatherPointViewTable.Get(id);
  }

  public FieldMapTable.FieldGimmickPointTableData GetFieldGimmickPointData(uint id)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (FieldMapTable.FieldGimmickPointTableData) null;
    return this.fieldGimmickPointTable == null || this.fieldGimmickPointTable.GetCount() == 0 ? (FieldMapTable.FieldGimmickPointTableData) null : this.fieldGimmickPointTable.Get(id);
  }

  public List<FieldMapTable.FieldGimmickPointTableData> GetFieldGimmickPointListByMapID(uint mapID)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (List<FieldMapTable.FieldGimmickPointTableData>) null : this.fieldGimmickPointMapIDTable.Get(mapID);
  }

  public FieldMapTable.FieldGimmickActionTableData GetFieldGimmickActionData(uint id)
  {
    return !Singleton<FieldMapTable>.IsValid() ? (FieldMapTable.FieldGimmickActionTableData) null : this.fieldGimmickActionTable.Get(id);
  }

  public class FieldMapTableData
  {
    public const uint NOT_CONECTING_REGION_ID = 4294967295 /*0xFFFFFFFF*/;
    public const uint DEFAULT_ICON_ID = 0;
    public uint mapID;
    public uint regionId;
    public string mapName;
    public string stageName;
    public string happenStageName;
    public int grade;
    public DIFFICULTY_MODE fieldMode = DIFFICULTY_MODE.NORMAL;
    public int eventId;
    public uint jumpPortalID;
    public int bgmID;
    public int happenBgmID;
    public uint linkQuestID;
    public uint childRegionId = uint.MaxValue;
    public uint iconId;
    public uint questIconId;
    public uint fieldBuffId;
    public Vector3 camOffsetPortraitPos = Vector3.zero;
    public Vector3 camOffsetPortraitRot = Vector3.zero;
    public Vector3 camOffsetLandscapePos = Vector3.zero;
    public Vector3 camOffsetLandscapeRot = Vector3.zero;
    public const string NT = "mapId,regionId,mapName,stageName,happenStageName,fieldGrade,fieldMode,eventId,jumpPortalId,bgmId,happenBgmId,linkQuestID,childRegionId,iconId,questIconId,fieldBuffId,camOffsetPortraitPos,camOffsetPortraitRot,camOffsetLandscapePos,camOffsetLandscapeRot";

    public static bool cb(CSVReader csv_reader, FieldMapTable.FieldMapTableData data, ref uint key)
    {
      data.mapID = key;
      csv_reader.Pop(ref data.regionId);
      csv_reader.Pop(ref data.mapName);
      csv_reader.Pop(ref data.stageName);
      csv_reader.Pop(ref data.happenStageName);
      csv_reader.Pop(ref data.grade);
      csv_reader.Pop<DIFFICULTY_MODE>(ref data.fieldMode);
      csv_reader.Pop(ref data.eventId);
      csv_reader.Pop(ref data.jumpPortalID);
      csv_reader.Pop(ref data.bgmID);
      csv_reader.Pop(ref data.happenBgmID);
      csv_reader.Pop(ref data.linkQuestID);
      csv_reader.Pop(ref data.childRegionId);
      csv_reader.Pop(ref data.iconId);
      csv_reader.Pop(ref data.questIconId);
      csv_reader.Pop(ref data.fieldBuffId);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      FieldMapTable.FieldMapTableData.TryParseNonSplitStrToVector3(empty, out data.camOffsetPortraitPos);
      csv_reader.Pop(ref empty);
      FieldMapTable.FieldMapTableData.TryParseNonSplitStrToVector3(empty, out data.camOffsetPortraitRot);
      csv_reader.Pop(ref empty);
      FieldMapTable.FieldMapTableData.TryParseNonSplitStrToVector3(empty, out data.camOffsetLandscapePos);
      csv_reader.Pop(ref empty);
      FieldMapTable.FieldMapTableData.TryParseNonSplitStrToVector3(empty, out data.camOffsetLandscapeRot);
      return true;
    }

    private static bool TryParseNonSplitStrToVector3(string str, out Vector3 vec)
    {
      vec = Vector3.zero;
      if (string.IsNullOrEmpty(str))
        return false;
      string[] strArray = str.Split(':');
      if (strArray.Length < 3)
        return false;
      vec = new Vector3(strArray[0].ToFloatOrDefault(), strArray[1].ToFloatOrDefault(), strArray[2].ToFloatOrDefault());
      return true;
    }

    public bool IsEventData => this.eventId != 0;

    public bool hasChildRegion => this.childRegionId != uint.MaxValue;

    public bool IsExistQuestIconId() => this.questIconId >= 1U;
  }

  public class PortalTableData
  {
    public uint portalID;
    public uint srcMapID;
    public uint linkPortalId;
    public float srcX;
    public float srcZ;
    public uint dstMapID;
    public float dstX;
    public float dstZ;
    public float dstDir;
    public float mapX = float.MaxValue;
    public float mapY = float.MaxValue;
    public uint dstQuestID;
    public uint showDeliveryId;
    public uint hideQuestId;
    public uint appearQuestId;
    public uint appearDeliveryId;
    public uint travelMapId;
    public uint openPriority;
    public uint portalPoint;
    public string notAppearText;
    public string placeText;
    public DateTime startAt;
    public uint banEnemy;
    public uint appearRegionId;
    public const string NT = "portalId,linkPortalId,srcMapId,srcX,srcZ,dstMapId,dstX,dstZ,dstDir,mapX,mapY,dstQuestId,showDeliveryId,hideQuestId,appearQuestId,appearDeliveryId,travelMapId,openPriority,portalPoint,notAppearText,placeText,startAt,banEnemy,appearRegionId";

    public static bool cb(CSVReader csv_reader, FieldMapTable.PortalTableData data, ref uint key)
    {
      data.portalID = key;
      csv_reader.Pop(ref data.linkPortalId);
      csv_reader.Pop(ref data.srcMapID);
      csv_reader.Pop(ref data.srcX);
      csv_reader.Pop(ref data.srcZ);
      csv_reader.Pop(ref data.dstMapID);
      csv_reader.Pop(ref data.dstX);
      csv_reader.Pop(ref data.dstZ);
      csv_reader.Pop(ref data.dstDir);
      csv_reader.Pop(ref data.mapX);
      csv_reader.Pop(ref data.mapY);
      csv_reader.Pop(ref data.dstQuestID);
      csv_reader.Pop(ref data.showDeliveryId);
      csv_reader.Pop(ref data.hideQuestId);
      csv_reader.Pop(ref data.appearQuestId);
      csv_reader.Pop(ref data.appearDeliveryId);
      csv_reader.Pop(ref data.travelMapId);
      csv_reader.Pop(ref data.openPriority);
      csv_reader.Pop(ref data.portalPoint);
      csv_reader.Pop(ref data.notAppearText);
      csv_reader.Pop(ref data.placeText);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
        DateTime.TryParse(empty, out data.startAt);
      csv_reader.Pop(ref data.banEnemy);
      csv_reader.Pop(ref data.appearRegionId);
      return true;
    }

    public bool isUnlockedTime() => TimeManager.GetNow() >= this.startAt;

    public bool IsWarpPortal() => this.banEnemy == 1U;
  }

  public class EnemyPopTableData
  {
    public uint mapID;
    public float popX;
    public bool enableRotY;
    public float rotY;
    public bool enablePopY;
    public float popY;
    public float popZ;
    public float popRadius;
    public uint enemyID;
    public uint enemyLv;
    public int waveNo;
    public int popNumMin;
    public int popNumMax;
    public int popNumInit;
    public int popNumTotal;
    public float popTimeMin;
    public float popTimeMax;
    public bool bigMonsterFlag;
    public bool bossFlag;
    public bool autoActivate;
    public BrainParam.ScountingParam scoutingParam = new BrainParam.ScountingParam();
    public ENEMY_POP_TYPE enemyPopType;
    public int escapeTime;
    public float walkSpeedMin = 1f;
    public float walkSpeedMax = 1f;
    public bool enableWalkSpeed;
    private Vector3 cachedPopBasePosition = Vector3.zero;
    private bool isPopBasePositionCached;
    public const string NT = "mapId,popX,rotY,popY,popZ,popRadius,enemyId,enemyLv,waveNo,popNumMin,popNumMax,popNumInit,popNumTotal,popTimeMin,popTimeMax,bigMonsterFlag,bossFlag,autoActivate,scountigRange,scoutingSight,scoutingAudibility,enemyPopType,escapeTime,walkSpeedMin,walkSpeedMax";

    public static bool cb(CSVReader csv_reader, FieldMapTable.EnemyPopTableData data, ref uint key)
    {
      data.mapID = key;
      csv_reader.Pop(ref data.popX);
      if (csv_reader.Pop(ref data.rotY) == CSVReader.PopResult.SUCCESS)
        data.enableRotY = true;
      if (csv_reader.Pop(ref data.popY) == CSVReader.PopResult.SUCCESS)
        data.enablePopY = true;
      csv_reader.Pop(ref data.popZ);
      csv_reader.Pop(ref data.popRadius);
      csv_reader.Pop(ref data.enemyID);
      csv_reader.Pop(ref data.enemyLv);
      csv_reader.Pop(ref data.waveNo);
      csv_reader.Pop(ref data.popNumMin);
      csv_reader.Pop(ref data.popNumMax);
      csv_reader.Pop(ref data.popNumInit);
      csv_reader.Pop(ref data.popNumTotal);
      csv_reader.Pop(ref data.popTimeMin);
      csv_reader.Pop(ref data.popTimeMax);
      csv_reader.Pop(ref data.bigMonsterFlag);
      csv_reader.Pop(ref data.bossFlag);
      csv_reader.Pop(ref data.autoActivate);
      float num = 0.0f;
      csv_reader.Pop(ref num);
      data.scoutingParam.scountigRangeSqr = num * num;
      csv_reader.Pop(ref num);
      data.scoutingParam.scoutingSightCos = Mathf.Cos((float) Math.PI / 180f * num);
      csv_reader.Pop(ref num);
      data.scoutingParam.scoutingAudibilitySqr = num * num;
      csv_reader.PopEnum<ENEMY_POP_TYPE>(ref data.enemyPopType, ENEMY_POP_TYPE.NONE);
      csv_reader.Pop(ref data.escapeTime);
      CSVReader.PopResult popResult1 = csv_reader.Pop(ref data.walkSpeedMin);
      CSVReader.PopResult popResult2 = csv_reader.Pop(ref data.walkSpeedMax);
      CSVReader.PopResult success = CSVReader.PopResult.SUCCESS;
      if (popResult1 == success && popResult2 == CSVReader.PopResult.SUCCESS)
        data.enableWalkSpeed = true;
      return true;
    }

    public float GeneratePopTime() => Random.Range(this.popTimeMin, this.popTimeMax);

    public float GenerateWalkSpeed()
    {
      if (!this.enableWalkSpeed)
        return 1f;
      if ((double) this.walkSpeedMin < (double) this.walkSpeedMax)
        return Random.Range(this.walkSpeedMin, this.walkSpeedMax);
      return (double) this.walkSpeedMin > (double) this.walkSpeedMax ? Random.Range(this.walkSpeedMax, this.walkSpeedMin) : this.walkSpeedMin;
    }

    public Vector3 GeneratePopPosVec3()
    {
      if (!this.isPopBasePositionCached)
        this.cachedPopBasePosition = new Vector3(this.popX, this.popY, this.popZ);
      return this.cachedPopBasePosition;
    }
  }

  public class GatherPointTableData
  {
    public uint pointID;
    public uint pointMapID;
    public float pointX;
    public float pointZ;
    public float pointDir;
    public uint viewID;
    public uint maxNumRate;
    public uint addNumRate;
    public uint growthInterval;
    public FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE gimmickType;
    public float value1;
    public const string NT = "pointId,pointMapId,pointX,pointZ,pointDir,viewId,maxNumRate,addNumRate,growthInterval,gimmickType,value1";

    public static bool cb(
      CSVReader csv_reader,
      FieldMapTable.GatherPointTableData data,
      ref uint key)
    {
      data.pointID = key;
      csv_reader.Pop(ref data.pointMapID);
      csv_reader.Pop(ref data.pointX);
      csv_reader.Pop(ref data.pointZ);
      csv_reader.Pop(ref data.pointDir);
      csv_reader.Pop(ref data.viewID);
      csv_reader.Pop(ref data.maxNumRate);
      csv_reader.Pop(ref data.addNumRate);
      csv_reader.Pop(ref data.growthInterval);
      csv_reader.Pop<FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE>(ref data.gimmickType);
      csv_reader.Pop(ref data.value1);
      return true;
    }

    public static FieldMapTable.GatherPointTableData.GatherType GetGatherType(
      FieldMapTable.GatherPointTableData data)
    {
      return data != null && data.growthInterval > 0U ? FieldMapTable.GatherPointTableData.GatherType.Growth : FieldMapTable.GatherPointTableData.GatherType.Basic;
    }

    public FieldMapTable.FieldGimmickPointTableData CloneAsGimmickData()
    {
      return new FieldMapTable.FieldGimmickPointTableData()
      {
        pointID = this.pointID,
        pointMapID = this.pointMapID,
        pointX = this.pointX,
        pointZ = this.pointZ,
        pointDir = this.pointDir,
        gimmickType = this.gimmickType,
        value1 = this.value1
      };
    }

    public enum GatherType
    {
      Basic,
      Growth,
    }
  }

  public class GatherPointViewTableData
  {
    public uint viewID;
    public uint modelID;
    public string modelHideNodeName;
    public string gatherEffectName;
    public float colRadius;
    public float targetRadius;
    public string targetEffectName;
    public float targetEffectShift;
    public float targetEffectHeight;
    public string actStateName;
    public string toolModelName;
    public string toolNodeName;
    public uint iconID;
    public string itemDetailText;
    public const string NT = "id,modelId,modelHideNodeName,gatherEffectName,colRadius,targetRadius,targetEffectName,targetEffectShift,targetEffectHeight,actStateName,toolModelName,toolNodeName,iconId,itemDetailText";

    public static bool cb(
      CSVReader csv_reader,
      FieldMapTable.GatherPointViewTableData data,
      ref uint key)
    {
      data.viewID = key;
      csv_reader.Pop(ref data.modelID);
      csv_reader.Pop(ref data.modelHideNodeName);
      csv_reader.Pop(ref data.gatherEffectName);
      csv_reader.Pop(ref data.colRadius);
      csv_reader.Pop(ref data.targetRadius);
      csv_reader.Pop(ref data.targetEffectName);
      csv_reader.Pop(ref data.targetEffectShift);
      csv_reader.Pop(ref data.targetEffectHeight);
      csv_reader.Pop(ref data.actStateName);
      csv_reader.Pop(ref data.toolModelName);
      csv_reader.Pop(ref data.toolNodeName);
      csv_reader.Pop(ref data.iconID);
      csv_reader.Pop(ref data.itemDetailText);
      return true;
    }
  }

  public class FieldGimmickPointTableData
  {
    public uint pointID;
    public FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE gimmickType;
    public uint pointMapID;
    public float pointX;
    public float pointZ;
    public float pointDir;
    public float value1;
    public string value2 = "";
    public const string NT = "pointId,gimmickType,pointMapId,pointX,pointZ,pointDir,value1,value2";

    public static bool cb(
      CSVReader csv_reader,
      FieldMapTable.FieldGimmickPointTableData data,
      ref uint key)
    {
      data.pointID = key;
      csv_reader.Pop<FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE>(ref data.gimmickType);
      csv_reader.Pop(ref data.pointMapID);
      csv_reader.Pop(ref data.pointX);
      csv_reader.Pop(ref data.pointZ);
      csv_reader.Pop(ref data.pointDir);
      csv_reader.Pop(ref data.value1);
      csv_reader.Pop(ref data.value2);
      return true;
    }

    public enum GIMMICK_TYPE
    {
      NONE,
      HEALING,
      CANNON,
      BOMBROCK,
      GEYSER,
      SONAR,
      CANNON_HEAVY,
      CANNON_RAPID,
      CANNON_SPECIAL,
      WAVE_TARGET,
      WAVE_TARGET2,
      CANNON_FIELD,
      READ_STORY,
      FISHING,
      BINGO,
      CHAT,
      WAVE_TARGET3,
      GENERATOR,
      CANDYWOOD,
      COOP_FISHING,
      SUPPLY,
      CARRIABLE_TURRET,
      CARRIABLE_EVOLVE_ITEM,
      CARRIABLE_DECOY,
      CARRIABLE_BUFF_POINT,
      PORTAL_GIMMICK,
      CARRIABLE_BOMB,
      QUEST,
    }
  }

  public class FieldGimmickActionTableData
  {
    public const float DEFAULT_RADIUS = 1f;
    public const float DEFAULT_START = -1f;
    public const float DEFAULT_DURATION = 5f;
    public const float DEFAULT_INTERVAL = 5f;
    public const float DEFAULT_FORCE = 500f;
    public const float DEFAULT_ANGLE = 20f;
    public const float DEFAULT_LOOP_TIME = 4f;
    public uint actionId;
    public float radius = 1f;
    public float start = -1f;
    public float duration = 5f;
    public float interval = 5f;
    public Character.REACTION_TYPE reactionType;
    public float force = 500f;
    public float angle = 20f;
    public float loopTime = 4f;
    public const string NT = "actionId,radius,start,duration,interval,reactionType,force,angle,loopTime";

    public static bool cb(
      CSVReader csv_reader,
      FieldMapTable.FieldGimmickActionTableData data,
      ref uint key)
    {
      data.actionId = key;
      csv_reader.Pop(ref data.radius);
      csv_reader.Pop(ref data.start);
      csv_reader.Pop(ref data.duration);
      csv_reader.Pop(ref data.interval);
      csv_reader.Pop<Character.REACTION_TYPE>(ref data.reactionType);
      csv_reader.Pop(ref data.force);
      csv_reader.Pop(ref data.angle);
      csv_reader.Pop(ref data.loopTime);
      return true;
    }
  }
}
