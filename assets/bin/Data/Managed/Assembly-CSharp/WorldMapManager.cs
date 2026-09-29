// Decompiled with JetBrains decompiler
// Type: WorldMapManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class WorldMapManager : MonoBehaviourSingleton<WorldMapManager>
{
  private List<int> traveledMapList = new List<int>();
  private List<FieldPortal> fieldPortalList = new List<FieldPortal>();
  private bool displayQuestTargetMode;
  private int questTargetMapID;
  private int[] questTargetPortalIDs;
  private uint jumpPortalID;
  private bool firstSetTravelList = true;

  public WorldMapManager.TransferInfo transferInfo { get; set; }

  public int releaseRegionIdfromBoard { get; set; }

  public int openNewFieldId { get; set; }

  public void PushDisplayQuestTarget(int mapID, int[] portalIDs)
  {
    this.displayQuestTargetMode = true;
    this.questTargetMapID = mapID;
    this.questTargetPortalIDs = portalIDs;
  }

  public void PopDisplayQuestTarget(out int mapID, out int[] portalIDs)
  {
    mapID = this.questTargetMapID;
    portalIDs = this.questTargetPortalIDs;
    this.questTargetMapID = -1;
    this.questTargetPortalIDs = (int[]) null;
    this.displayQuestTargetMode = false;
  }

  public bool isDisplayQuestTargetMode() => this.displayQuestTargetMode;

  public bool ignoreTutorial { get; set; }

  public int eventMapRegionID { get; set; }

  public int releaseCrystalNum { get; private set; }

  public List<int> releasedRegionIds { get; private set; }

  public void SetReleasedRegion()
  {
    this.releasedRegionIds = MonoBehaviourSingleton<OnceManager>.I.result.region;
  }

  public void AddReleasedRegion(int regionId) => this.releasedRegionIds.Add(regionId);

  public bool IsTraveledMap(int mapId)
  {
    return QuestManager.IsValidInGameExplore() ? MonoBehaviourSingleton<QuestManager>.I.MapIsTraveldInExplore(mapId) : this.traveledMapList.IndexOf(mapId) >= 0;
  }

  public uint[] GetOpenRegionIdList()
  {
    List<uint> uintList = new List<uint>();
    foreach (RegionTable.Data data in Singleton<RegionTable>.I.GetData())
    {
      if (!data.HasStartAt() || !(data.startAt > TimeManager.GetNow()))
      {
        foreach (FieldMapTable.FieldMapTableData map in Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data.regionId))
        {
          if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
          {
            uintList.Add(map.regionId);
            break;
          }
        }
      }
    }
    return uintList.ToArray();
  }

  public uint[] GetOpenRegionIdListInWorldMap()
  {
    List<uint> uintList = new List<uint>();
    foreach (RegionTable.Data data in Singleton<RegionTable>.I.GetData())
    {
      if (100U > data.regionId && (!data.HasStartAt() || !(data.startAt > TimeManager.GetNow())))
      {
        foreach (FieldMapTable.FieldMapTableData map in Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data.regionId))
        {
          if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
          {
            uintList.Add(map.regionId);
            break;
          }
        }
      }
    }
    return uintList.ToArray();
  }

  public uint[] GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE type)
  {
    List<uint> uintList = new List<uint>();
    foreach (RegionTable.Data data in Singleton<RegionTable>.I.GetData())
    {
      if (100U > data.regionId && type == data.difficulty && (!data.HasStartAt() || !(data.startAt > TimeManager.GetNow())))
      {
        foreach (FieldMapTable.FieldMapTableData map in Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data.regionId))
        {
          if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
          {
            uintList.Add(map.regionId);
            break;
          }
        }
      }
    }
    return uintList.ToArray();
  }

  public uint[] GetValidRegionIdListInWorldMap()
  {
    List<uint> uintList = new List<uint>();
    foreach (RegionTable.Data data in Singleton<RegionTable>.I.GetData())
    {
      if (100U > data.regionId && (!data.HasStartAt() || !(data.startAt > TimeManager.GetNow())))
        uintList.Add(data.regionId);
    }
    return uintList.ToArray();
  }

  public uint[] GetValidRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE type)
  {
    List<uint> uintList = new List<uint>();
    foreach (RegionTable.Data data in Singleton<RegionTable>.I.GetData())
    {
      if (100U > data.regionId && (!data.HasStartAt() || !(data.startAt > TimeManager.GetNow())) && type == data.difficulty)
        uintList.Add(data.regionId);
    }
    return uintList.ToArray();
  }

  public bool IsAllOpenedMap(int regionId)
  {
    RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) regionId);
    if (data == null)
      return false;
    foreach (FieldMapTable.FieldMapTableData map in Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(data.regionId))
    {
      if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
        return false;
    }
    return true;
  }

  public bool IsOpenRegion(uint regionId)
  {
    foreach (FieldMapTable.FieldMapTableData map in Singleton<FieldMapTable>.I.GetFieldMapDataInRegion(regionId))
    {
      if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(map))
        return true;
    }
    return false;
  }

  public FieldPortal GetFieldPortal(int portalId)
  {
    return this.fieldPortalList.Find((Predicate<FieldPortal>) (p => p.pId == portalId));
  }

  public bool IsTraveledPortal(uint portalId)
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
    return portalData != null && this.IsTraveledPortal(portalData);
  }

  public bool IsTraveledPortal(FieldMapTable.PortalTableData portal)
  {
    if (QuestManager.IsValidInGameExplore())
      return MonoBehaviourSingleton<QuestManager>.I.PortalIsUsedInExplore((int) portal.portalID);
    if (portal.srcMapID > 0U && !this.IsTraveledMap((int) portal.srcMapID))
      return false;
    FieldPortal fieldPortal = this.GetFieldPortal((int) portal.portalID);
    return fieldPortal != null && fieldPortal.used;
  }

  public int GetPortalPoint(uint portalId)
  {
    FieldPortal fieldPortal = this.GetFieldPortal((int) portalId);
    return fieldPortal == null ? 0 : fieldPortal.point;
  }

  public void SetWorldMapTraveledList()
  {
    if (!this.firstSetTravelList)
      return;
    this.firstSetTravelList = false;
    OnceTraveledListModel.Param traveledlist = MonoBehaviourSingleton<OnceManager>.I.result.traveledlist;
    this.traveledMapList = traveledlist.travel;
    this.fieldPortalList = traveledlist.portal;
  }

  public void SendDebugSetTraveled(int mapId, int cnt, Action<bool> call_back)
  {
    Protocol.Send<DebugSetTraveledModel.RequestSendForm, DebugSetTraveledModel>(DebugSetTraveledModel.URL, new DebugSetTraveledModel.RequestSendForm()
    {
      mapId = mapId,
      cnt = cnt
    }, (Action<DebugSetTraveledModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendDebugUsedPortal(int portalId, int used, Action<bool> call_back)
  {
    Protocol.Send<DebugUsedPortalModel.RequestSendForm, DebugUsedPortalModel>(DebugUsedPortalModel.URL, new DebugUsedPortalModel.RequestSendForm()
    {
      pId = portalId,
      used = used
    }, (Action<DebugUsedPortalModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendDebugSetPortalPoint(int portalId, int point, Action<bool> call_back)
  {
    Protocol.Send<DebugSetPortalPointModel.RequestSendForm, DebugSetPortalPointModel>(DebugSetPortalPointModel.URL, new DebugSetPortalPointModel.RequestSendForm()
    {
      pId = portalId,
      point = point
    }, (Action<DebugSetPortalPointModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void Dirty()
  {
  }

  public void OnDiff(BaseModelDiff.DiffTraveled diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      uint[] idListInWorldMap = this.GetOpenRegionIdListInWorldMap();
      this.traveledMapList.AddRange((IEnumerable<int>) diff.add);
      flag = true;
      if (diff.add.Contains(10010500))
        GameSaveData.instance.happyTimeForRating = true;
      List<uint> uintList = new List<uint>((IEnumerable<uint>) this.GetOpenRegionIdListInWorldMap());
      for (int index = 0; index < idListInWorldMap.Length; ++index)
        uintList.Remove(idListInWorldMap[index]);
      if (uintList.Contains(1U) || uintList.Contains(3U) || uintList.Contains(5U) || uintList.Contains(7U))
        GameSaveData.instance.happyTimeForRating = true;
    }
    if (!flag)
      return;
    this.Dirty();
  }

  public void OnDiff(BaseModelDiff.DiffFieldPortal diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      this.fieldPortalList.AddRange((IEnumerable<FieldPortal>) diff.add);
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<FieldPortal>) (portal =>
      {
        FieldPortal fieldPortal = this.fieldPortalList.Find((Predicate<FieldPortal>) (f => f.pId == portal.pId));
        if (fieldPortal == null)
          return;
        fieldPortal.used = portal.used;
        fieldPortal.point = portal.point;
      }));
      flag = true;
    }
    if (!flag)
      return;
    this.Dirty();
  }

  public void SetJumpPortalID(uint portalID) => this.jumpPortalID = portalID;

  public uint GetJumpPortalID() => this.jumpPortalID;

  public static bool IsValidPortalIDs(int[] ids) => ids != null && ids.Length != 0 && 0 < ids[0];

  public void SendRegionCrystalNum(int regionId, Action<bool, string> call_back)
  {
    Protocol.Send<RegionCrystalNumModel.RequestSendForm, RegionCrystalNumModel>(RegionCrystalNumModel.URL, new RegionCrystalNumModel.RequestSendForm()
    {
      regionId = regionId
    }, (Action<RegionCrystalNumModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      string str = "";
      if (flag)
      {
        this.releaseCrystalNum = ret.result.crystalNum;
        str = ret.result.text;
      }
      call_back(flag, str);
    }));
  }

  public void SendRegionOpen(int regionId, Action<bool> call_back)
  {
    Protocol.Send<RegionOpenModel.RequestSendForm, RegionOpenModel>(RegionOpenModel.URL, new RegionOpenModel.RequestSendForm()
    {
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal,
      regionId = regionId,
      useCrystal = this.releaseCrystalNum
    }, (Action<RegionOpenModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        this.releasedRegionIds.Add(regionId);
      call_back(flag);
    }));
  }

  public bool IsShowedOpenRegion(int regionId)
  {
    RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) regionId);
    if (data.difficulty != REGION_DIFFICULTY_TYPE.NORMAL || data.regionId == 0U || GameSaveData.instance.showedOpenRegionIds.Contains(regionId))
      return true;
    foreach (FieldMapTable.FieldMapTableData fieldMapTableData in Singleton<FieldMapTable>.I.GetFieldMapDataInRegion((uint) regionId))
    {
      if (this.traveledMapList.Contains((int) fieldMapTableData.mapID))
      {
        GameSaveData.instance.AddShowedOpenRegionId((int) fieldMapTableData.regionId);
        if ((int) fieldMapTableData.mapID != this.openNewFieldId)
          return true;
      }
    }
    return false;
  }

  public bool ExistRegionDirection()
  {
    foreach (uint regionIdListInWorld in this.GetOpenRegionIdListInWorldMap())
    {
      if (regionIdListInWorld != 0U && !this.IsShowedOpenRegion((int) regionIdListInWorld))
        return true;
    }
    return false;
  }

  public bool IsExistedWorld2()
  {
    foreach (uint regionIdListInWorld in this.GetValidRegionIdListInWorldMap())
    {
      RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionIdListInWorld);
      if (data.regionId < 100U && data.worldId == 2)
        return true;
    }
    return false;
  }

  public bool NeedDirectionOpenRegion(int regionId)
  {
    return regionId < 100 && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && !this.IsShowedOpenRegion(regionId);
  }

  public class TransferInfo
  {
    public int nextRegionId;
    public bool nextInGame;

    public TransferInfo(int regionId, bool directInGame)
    {
      this.nextRegionId = regionId;
      this.nextInGame = directInGame;
    }
  }
}
