// Decompiled with JetBrains decompiler
// Type: FieldManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldManager : MonoBehaviourSingleton<FieldManager>
{
  private int lastMapId;
  private FieldManager.CurrentFieldData current = new FieldManager.CurrentFieldData();
  private Vector3 cameraOffsetPortraitPos = Vector3.zero;
  private Quaternion cameraOffsetPortraitRot = Quaternion.identity;
  private Vector3 cameraOffsetLandscapePos = Vector3.zero;
  private Quaternion cameraOffsetLandscapeRot = Quaternion.identity;
  public List<int> fieldGatherPointIdList = new List<int>();
  public List<GatherGrowthInfo> fieldGatherGrowthList = new List<GatherGrowthInfo>();
  public const uint NUMBERTOP_MAPID_EVENT = 2;
  public const uint NUMBERTOP_MAPID_QUEST = 3;
  public const uint TUTORIAL_FIELD_ID_0 = 10000101;
  public const uint TUTORIAL_FIELD_ID_1 = 10000101;

  public static bool IsValidInField()
  {
    return MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData.field != null;
  }

  public static bool IsValidInGame()
  {
    return MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentMapID > 0U;
  }

  public static bool IsValidInGameNoQuest()
  {
    return FieldManager.IsValidInGame() && !QuestManager.IsValidInGame();
  }

  public static bool IsValidInGameNoBoss()
  {
    if (!FieldManager.IsValidInGame())
      return false;
    if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExplore())
      return (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
    return !QuestManager.IsValidInGame() || MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID() <= 0;
  }

  public static bool IsValidInTutorial()
  {
    return MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.isTutorialField;
  }

  public FieldModel.Param fieldData { get; private set; }

  public Error matchingErrorCode { get; private set; }

  public string noticeText { get; private set; }

  public FieldManager()
  {
    this.fieldData = new FieldModel.Param();
    this.noticeText = "";
  }

  public uint currentMapID => this.current.fieldTransitionInfo.mapID;

  public uint currentPortalID => this.current.fieldTransitionInfo.portalID;

  public FieldMapTable.FieldMapTableData currentMapData => this.current.mapData;

  public uint currentFieldBuffId
  {
    get => this.current.mapData != null ? this.current.mapData.fieldBuffId : 0U;
  }

  public float currentStartMapX => this.current.fieldTransitionInfo.mapX;

  public float currentStartMapZ => this.current.fieldTransitionInfo.mapZ;

  public float currentStartMapDir => this.current.fieldTransitionInfo.mapDir;

  public bool currentIsValidBoss => this.current.isValidBoss;

  public List<FieldMapPortalInfo> currentFieldPortalInfoList => this.current.fieldPortalInfoList;

  public List<int> currentFieldPointIdList => this.fieldGatherPointIdList;

  public List<GatherGrowthInfo> currentFieldGatherGrowthList => this.fieldGatherGrowthList;

  public Vector3 cameraOffsetPos_Vec
  {
    get
    {
      return MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this.cameraOffsetLandscapePos : this.cameraOffsetPortraitPos;
    }
  }

  public Quaternion cameraOffsetRot_Quat
  {
    get
    {
      return MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this.cameraOffsetLandscapeRot : this.cameraOffsetPortraitRot;
    }
  }

  public bool IsEnabledStandby()
  {
    return this.fieldData != null && this.fieldData.field != null && this.fieldData.field.enableStandby != 0;
  }

  private string GetHappenStageName()
  {
    FieldManager.FieldTransitionInfo backTransitionInfo = MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo;
    if (backTransitionInfo == null)
      return string.Empty;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(backTransitionInfo.mapID);
    return fieldMapData == null ? string.Empty : fieldMapData.happenStageName;
  }

  public int GetHappenMapBGMID()
  {
    FieldManager.FieldTransitionInfo backTransitionInfo = MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo;
    if (backTransitionInfo == null)
      return 0;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(backTransitionInfo.mapID);
    return fieldMapData == null ? 0 : fieldMapData.happenBgmID;
  }

  public string GetCurrentMapStageName()
  {
    if (this.current.mapData == null)
      return string.Empty;
    if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
    {
      string happenStageName = this.GetHappenStageName();
      if (!string.IsNullOrEmpty(happenStageName))
        return happenStageName;
    }
    if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
    {
      string bossMapStageName = MonoBehaviourSingleton<QuestManager>.I.GetCurrentBossMapStageName();
      if (!string.IsNullOrEmpty(bossMapStageName))
        return bossMapStageName;
    }
    return this.current.mapData.stageName;
  }

  public int GetCurrentMapBGMID() => this.current.mapData == null ? 0 : this.current.mapData.bgmID;

  public void ClearCurrentFieldData() => this.current = new FieldManager.CurrentFieldData();

  public bool useFastTravel { get; set; }

  public void SetCurrentFieldMapPortalID(uint portal_id)
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portal_id);
    if (portalData == null)
    {
      Log.Error(LOG.INGAME, "FieldManager.SetCurrentPortalID() portal data is none. portal_id : {0}", (object) portal_id);
    }
    else
    {
      this.SetCurrentFieldMapID(portalData.dstMapID, portalData.dstX, portalData.dstZ, portalData.dstDir);
      this.current.fieldTransitionInfo.portalID = portal_id;
    }
  }

  public void SetCurrentFieldMapPortalID(uint portal_id, float map_x, float map_z, float map_dir)
  {
    this.SetCurrentFieldMapPortalID(portal_id);
    this.current.fieldTransitionInfo.mapX = map_x;
    this.current.fieldTransitionInfo.mapZ = map_z;
    this.current.fieldTransitionInfo.mapDir = map_dir;
  }

  public void SetCurrentFieldMapID(uint map_id, float map_x, float map_z, float map_dir)
  {
    this.current = new FieldManager.CurrentFieldData();
    this.current.fieldTransitionInfo.portalID = 0U;
    this.current.fieldTransitionInfo.mapID = map_id;
    this.current.fieldTransitionInfo.mapX = map_x;
    this.current.fieldTransitionInfo.mapZ = map_z;
    this.current.fieldTransitionInfo.mapDir = map_dir;
    if (this.current.fieldTransitionInfo.mapID > 0U)
    {
      this.current.mapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.current.fieldTransitionInfo.mapID);
      this.cameraOffsetPortraitPos = this.current.mapData.camOffsetPortraitPos;
      this.cameraOffsetPortraitRot = Quaternion.Euler(this.current.mapData.camOffsetPortraitRot);
      this.cameraOffsetLandscapePos = this.current.mapData.camOffsetLandscapePos;
      this.cameraOffsetLandscapeRot = Quaternion.Euler(this.current.mapData.camOffsetLandscapeRot);
    }
    else
    {
      this.current.mapData = (FieldMapTable.FieldMapTableData) null;
      this.cameraOffsetPortraitPos = this.cameraOffsetLandscapePos = Vector3.zero;
      this.cameraOffsetPortraitRot = this.cameraOffsetLandscapeRot = Quaternion.identity;
    }
    this.current.isValidBoss = false;
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(this.currentMapID);
    if (enemyPopList != null)
    {
      int index = 0;
      for (int count = enemyPopList.Count; index < count; ++index)
      {
        FieldMapTable.EnemyPopTableData enemyPopTableData = enemyPopList[index];
        if (enemyPopTableData != null && enemyPopTableData.bossFlag)
        {
          this.current.isValidBoss = true;
          break;
        }
      }
    }
    this._SetFieldPortalInfoList(this.current.fieldTransitionInfo.mapID);
  }

  private void _SetFieldPortalInfoList(uint mapId)
  {
    int count = this.current.fieldPortalInfoList.Count;
    this.current.portalPointToIndex = -1;
    int num = 0;
    List<FieldMapTable.PortalTableData> portalListByMapId = Singleton<FieldMapTable>.I.GetPortalListByMapID(mapId, true);
    if (portalListByMapId != null)
    {
      num = portalListByMapId.Count;
      for (int index = 0; index < num; ++index)
      {
        FieldPortal _fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal((int) portalListByMapId[index].portalID);
        if (QuestManager.IsValidExplore() && _fieldPortal != null)
          _fieldPortal = new FieldPortal()
          {
            pId = _fieldPortal.pId,
            used = _fieldPortal.used,
            point = _fieldPortal.point
          };
        if (count <= index)
        {
          this.current.fieldPortalInfoList.Add(new FieldMapPortalInfo(portalListByMapId[index], _fieldPortal));
          ++count;
        }
        else
          this.current.fieldPortalInfoList[index].SetPortalData(portalListByMapId[index], _fieldPortal);
        if (this.current.portalPointToIndex < 0 && this.current.fieldPortalInfoList[index].IsAddPortalPoint())
          this.current.portalPointToIndex = index;
      }
    }
    for (int index = num; index < count; ++index)
      this.current.fieldPortalInfoList[index].Clear();
  }

  public FieldMapPortalInfo GetPortalPointToPortalInfo()
  {
    return this.current.portalPointToIndex < 0 ? (FieldMapPortalInfo) null : this.current.fieldPortalInfoList[this.current.portalPointToIndex];
  }

  public bool AddPortalPointToPortalInfo(int addPoint)
  {
    FieldMapPortalInfo pointToPortalInfo = this.GetPortalPointToPortalInfo();
    return pointToPortalInfo != null && this.AddPortalPointToPortalInfo(pointToPortalInfo, addPoint);
  }

  public bool AddPortalPointToPortalInfo(FieldMapPortalInfo portalInfo, int addPoint)
  {
    FieldPortal fieldPortal = portalInfo.fieldPortal;
    if (fieldPortal == null)
    {
      fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal((int) portalInfo.portalData.portalID);
      if (fieldPortal == null)
      {
        fieldPortal = new FieldPortal();
        fieldPortal.pId = (int) portalInfo.portalData.portalID;
        fieldPortal.used = false;
        fieldPortal.point = 0;
      }
      else if (QuestManager.IsValidExplore())
        fieldPortal = new FieldPortal()
        {
          pId = fieldPortal.pId,
          used = fieldPortal.used,
          point = fieldPortal.point
        };
      portalInfo.fieldPortal = fieldPortal;
    }
    fieldPortal.point += addPoint;
    if (!portalInfo.IsFull())
      return false;
    this._ResetPortalPointToIndex(this.current.portalPointToIndex);
    if (QuestManager.IsValidExplore())
      this._ResetPortalPointToIndex();
    return true;
  }

  public void ResetPortalPointToIndex() => this._ResetPortalPointToIndex();

  private int _ResetPortalPointToIndex(int startIndex = 0)
  {
    if (startIndex < 0)
      startIndex = 0;
    this.current.portalPointToIndex = -1;
    int index = startIndex;
    for (int count = this.current.fieldPortalInfoList.Count; index < count; ++index)
    {
      if (this.current.fieldPortalInfoList[index].IsValid() && this.current.fieldPortalInfoList[index].IsAddPortalPoint())
      {
        this.current.portalPointToIndex = index;
        break;
      }
    }
    return this.current.portalPointToIndex;
  }

  public FieldMapPortalInfo GetPortalInfo(uint portalId)
  {
    int index = 0;
    for (int count = this.current.fieldPortalInfoList.Count; index < count; ++index)
    {
      if (this.current.fieldPortalInfoList[index].IsValid() && (int) this.current.fieldPortalInfoList[index].portalData.portalID == (int) portalId)
        return this.current.fieldPortalInfoList[index];
    }
    return (FieldMapPortalInfo) null;
  }

  public void InitPortalPointForExplore(ExploreStatus exploreStatus)
  {
    if (this.current == null || this.current.fieldPortalInfoList == null)
      return;
    foreach (FieldMapPortalInfo fieldPortalInfo in this.current.fieldPortalInfoList)
    {
      if (fieldPortalInfo.IsValid())
      {
        Singleton<FieldMapTable>.I.GetPortalData(fieldPortalInfo.portalData.portalID);
        FieldPortal fieldPortal = new FieldPortal();
        fieldPortal.pId = (int) fieldPortalInfo.portalData.portalID;
        fieldPortal.used = false;
        fieldPortal.point = 0;
        ExplorePortalPoint portalData = exploreStatus.GetPortalData(fieldPortal.pId);
        if (portalData != null)
        {
          fieldPortal.point = portalData.point;
          fieldPortal.used = ExplorePortalPoint.USEDFLAG_CLOSED != portalData.used;
        }
        fieldPortalInfo.fieldPortal = fieldPortal;
      }
    }
    this._ResetPortalPointToIndex();
  }

  public void Dirty()
  {
  }

  private void UpdateFieldData(FieldModel.Param field_data, bool is_user_collection_init = true)
  {
    this.fieldData = field_data;
    if (((field_data.field == null ? 0 : (MonoBehaviourSingleton<QuestManager>.IsValid() ? 1 : 0)) & (is_user_collection_init ? 1 : 0)) == 0)
      return;
    MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.Init(field_data.field);
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendStartField();
  }

  public string GetFieldId() => this.fieldData.field == null ? "0" : this.fieldData.field.id;

  public int GetMapId() => this.fieldData.field == null ? 0 : this.fieldData.field.mapId;

  public CoopNetworkManager.ConnectData GetWebSockConnectData()
  {
    if (this.fieldData.field == null)
      return (CoopNetworkManager.ConnectData) null;
    int index1 = -1;
    List<FieldModel.SlotInfo> slotInfos = this.fieldData.field.slotInfos;
    int index2 = 0;
    for (int count = slotInfos.Count; index2 < count; ++index2)
    {
      if (slotInfos[index2].userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      {
        index1 = index2;
        break;
      }
    }
    if (index1 < 0)
      return (CoopNetworkManager.ConnectData) null;
    return new CoopNetworkManager.ConnectData()
    {
      path = this.fieldData.field.wsHost,
      ports = this.fieldData.field.wsPorts,
      fromId = this.fieldData.field.slotInfos[index1].userId,
      ackPrefix = index1,
      roomId = this.fieldData.field.id,
      token = this.fieldData.field.slotInfos[index1].token
    };
  }

  public static string GenerateToken() => Guid.NewGuid().ToString().Replace("-", "");

  public void SendMatching(int portalId, uint deliveryId, int _toUserId, Action<bool> call_back)
  {
    this.fieldData.field = (FieldModel.Field) null;
    FieldModel.RequestMatching postData = new FieldModel.RequestMatching();
    postData.portalId = portalId;
    postData.dId = (int) deliveryId;
    postData.token = FieldManager.GenerateToken();
    postData.prevId = this.lastMapId;
    postData.toUserId = _toUserId;
    this.fieldGatherPointIdList.Clear();
    this.fieldGatherGrowthList.Clear();
    Protocol.Send<FieldModel.RequestMatching, FieldModel>(FieldModel.RequestMatching.path, postData, (Action<FieldModel>) (ret =>
    {
      bool flag = false;
      this.matchingErrorCode = ret.Error;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.lastMapId = ret.result.field.mapId;
        if (ret.result.gather != null)
          this.fieldGatherPointIdList = ret.result.gather;
        if (ret.result.growth != null)
          this.fieldGatherGrowthList = ret.result.growth;
        if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss, (Object) null))
          MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.SetRaidBossHp(ret.result.raidBossHp);
        this.noticeText = ret.result.noticeText;
        this.UpdateFieldData(ret.result);
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendQuest(int questId, Action<bool> call_back)
  {
    this.fieldData.field = (FieldModel.Field) null;
    Protocol.Send<FieldModel.RequestQuest, FieldModel>(FieldModel.RequestQuest.path, new FieldModel.RequestQuest()
    {
      token = FieldManager.GenerateToken(),
      qid = questId
    }, (Action<FieldModel>) (ret =>
    {
      bool flag = false;
      this.matchingErrorCode = ret.Error;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.SetCurrentFieldMapID((uint) ret.result.field.mapId, 0.0f, 0.0f, 0.0f);
        this.UpdateFieldData(ret.result);
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendParty(string party_id, bool is_owner, Action<bool> call_back)
  {
    if (is_owner)
      this.SendCreate(party_id, call_back);
    else
      this.SendEnter(party_id, call_back);
  }

  public void SendCreate(string party_id, Action<bool> call_back)
  {
    this.fieldData.field = (FieldModel.Field) null;
    Protocol.Send<FieldModel.RequestCreate, FieldModel>(FieldModel.RequestCreate.path, new FieldModel.RequestCreate()
    {
      partyId = party_id,
      token = FieldManager.GenerateToken()
    }, (Action<FieldModel>) (ret =>
    {
      bool flag = false;
      this.matchingErrorCode = ret.Error;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.SetCurrentFieldMapID((uint) ret.result.field.mapId, 0.0f, 0.0f, 0.0f);
        this.UpdateFieldData(ret.result);
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendEnter(string party_id, Action<bool> call_back)
  {
    this.fieldData.field = (FieldModel.Field) null;
    Protocol.Send<FieldModel.RequestEnter, FieldModel>(FieldModel.RequestEnter.path, new FieldModel.RequestEnter()
    {
      partyId = party_id,
      token = FieldManager.GenerateToken()
    }, (Action<FieldModel>) (ret =>
    {
      bool flag = false;
      this.matchingErrorCode = ret.Error;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.SetCurrentFieldMapID((uint) ret.result.field.mapId, 0.0f, 0.0f, 0.0f);
        this.UpdateFieldData(ret.result);
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendInfo(Action<bool> call_back)
  {
    if (this.fieldData.field == null)
      call_back(false);
    else
      MonoBehaviourSingleton<NetworkManager>.I.Request<FieldModel>(FieldModel.RequestInfo.path, (Action<FieldModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.UpdateFieldData(ret.result, false);
          this.Dirty();
        }
        call_back(flag);
      }));
  }

  public void SendLeave(bool toHome, bool retire, Action<bool> call_back)
  {
    if (this.fieldData.field == null)
    {
      call_back(false);
    }
    else
    {
      FieldLeaveModel.RequestSendForm postData = new FieldLeaveModel.RequestSendForm();
      postData.toHome = 0;
      postData.retire = 0;
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      {
        postData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
        MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
      }
      if (toHome)
        postData.toHome = 1;
      if (retire)
        postData.retire = 1;
      Protocol.Send<FieldLeaveModel.RequestSendForm, FieldLeaveModel>(FieldLeaveModel.URL, postData, (Action<FieldLeaveModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          if (toHome)
            this.lastMapId = 0;
          this.fieldData.field = (FieldModel.Field) null;
          this.Dirty();
        }
        call_back(flag);
      }));
    }
  }

  public void SendFieldDrop(FieldDropModel.RequestSendForm send, Action<bool> call_back)
  {
    Protocol.Send<FieldDropModel.RequestSendForm, FieldDropModel>(FieldDropModel.URL, send, (Action<FieldDropModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendFieldContinue(Action<bool, Error> call_back)
  {
    Protocol.Send<FieldContinueModel.RequestSendForm, FieldContinueModel>(FieldContinueModel.URL, new FieldContinueModel.RequestSendForm()
    {
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Crystal
    }, (Action<FieldContinueModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error), ret.Error)));
  }

  public void SendFieldCharaList(Action<bool, List<FriendCharaInfo>> call_back)
  {
    Protocol.Send<FieldCharaListModel>(FieldCharaListModel.URL, (Action<FieldCharaListModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error), ret.result)));
  }

  public void SendFieldGather(int pointId, Action<bool, FieldGatherRewardList> call_back)
  {
    Protocol.Send<FieldGatherModel.RequestSendForm, FieldGatherModel>(FieldGatherModel.URL, new FieldGatherModel.RequestSendForm()
    {
      pId = pointId
    }, (Action<FieldGatherModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error), ret.result.reward)));
  }

  public void SendFieldGatherGimmick(
    int lotId,
    int time,
    int isPop,
    Action<bool, FieldFishModel.Param> call_back)
  {
    Protocol.Send<FieldFishModel.RequestSendForm, FieldFishModel>(FieldFishModel.URL, new FieldFishModel.RequestSendForm()
    {
      lotId = lotId,
      time = time,
      isPop = isPop
    }, (Action<FieldFishModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error), ret.result)));
  }

  public void SendFieldFishBossComplete(
    int ownerUserId,
    int isSuccess,
    Action<bool, FieldGatherRewardList> call_back)
  {
    Protocol.Send<FieldFishBossCompleteModel.RequestSendForm, FieldFishBossCompleteModel>(FieldFishBossCompleteModel.URL, new FieldFishBossCompleteModel.RequestSendForm()
    {
      ownerUserId = ownerUserId,
      isSuccess = isSuccess
    }, (Action<FieldFishBossCompleteModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error), ret.result.reward)));
  }

  public void SendFieldQuestOpenPortal(int portalId, Action<bool, Error> call_back)
  {
    Protocol.Send<FieldQuestOpenPortalModel.RequestSendForm, FieldQuestOpenPortalModel>(FieldQuestOpenPortalModel.URL, new FieldQuestOpenPortalModel.RequestSendForm()
    {
      portalId = portalId
    }, (Action<FieldQuestOpenPortalModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error), ret.Error)));
  }

  public void SendFieldQuestMapChange(int mapId, Action<bool, Error> call_back)
  {
    FieldQuestMapChangeModel.RequestSendForm postData = new FieldQuestMapChangeModel.RequestSendForm();
    postData.mapId = mapId;
    this.fieldGatherPointIdList.Clear();
    this.fieldGatherGrowthList.Clear();
    Protocol.Send<FieldQuestMapChangeModel.RequestSendForm, FieldQuestMapChangeModel>(FieldQuestMapChangeModel.URL, postData, (Action<FieldQuestMapChangeModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag && ret.result.gather != null)
      {
        this.fieldGatherPointIdList = ret.result.gather;
        if (ret.result.growth != null)
          this.fieldGatherGrowthList = ret.result.growth;
        if (MonoBehaviourSingleton<InGameProgress>.IsValid())
          MonoBehaviourSingleton<InGameProgress>.I.CheckGatherPointList();
      }
      call_back(flag, ret.Error);
    }));
  }

  public void SendDebugSetBoost(int type, int value, string end, Action<bool> call_back)
  {
    Protocol.Send<DebugSetBoostModel.RequestSendForm, DebugSetBoostModel>(DebugSetBoostModel.URL, new DebugSetBoostModel.RequestSendForm()
    {
      type = type,
      value = value,
      end = end
    }, (Action<DebugSetBoostModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS);
      call_back(flag);
    }));
  }

  public void SendDebugAppearFieldGather(int pointId, string appear, Action<bool> call_back)
  {
    Protocol.Send<DebugAppearFieldGatherModel.RequestSendForm, DebugAppearFieldGatherModel>(DebugAppearFieldGatherModel.URL, new DebugAppearFieldGatherModel.RequestSendForm()
    {
      pId = pointId,
      appear = appear
    }, (Action<DebugAppearFieldGatherModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public static bool IsShowPortal(uint portalId)
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
    return portalData != null && FieldManager.IsShowPortal(portalData);
  }

  public static bool IsShowPortal(FieldMapTable.PortalTableData portal)
  {
    return (portal.hideQuestId <= 0U || !MonoBehaviourSingleton<QuestManager>.I.IsClearQuest(portal.hideQuestId)) && (portal.showDeliveryId <= 0U || MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(portal.showDeliveryId));
  }

  public static bool IsOpenPortal(uint portalId)
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
    return portalData != null && FieldManager.IsOpenPortal(portalData);
  }

  public static bool IsOpenPortal(FieldMapTable.PortalTableData portal)
  {
    return (portal.srcMapID <= 0U || MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portal.srcMapID)) && (portal.dstMapID == 0U && portal.dstQuestID == 0U || MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal(portal) || FieldManager.IsOpenPortalClearOrder(portal) && (portal.portalPoint <= 0U || (long) MonoBehaviourSingleton<WorldMapManager>.I.GetPortalPoint(portal.portalID) >= (long) portal.portalPoint));
  }

  public static bool IsOpenPortalClearOrder(FieldMapTable.PortalTableData portal)
  {
    if (portal.appearQuestId > 0U && !MonoBehaviourSingleton<QuestManager>.I.IsClearQuest(portal.appearQuestId) || portal.appearDeliveryId > 0U && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(portal.appearDeliveryId))
      return false;
    if (portal.appearRegionId > 0U)
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
      if (fieldMapData == null || MonoBehaviourSingleton<WorldMapManager>.I.releasedRegionIds == null || !MonoBehaviourSingleton<WorldMapManager>.I.releasedRegionIds.Contains((int) fieldMapData.regionId))
        return false;
    }
    else if (portal.travelMapId > 0U && !MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portal.travelMapId))
      return false;
    return true;
  }

  public bool CanJumpToMap(uint mapId)
  {
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
    return fieldMapData != null && this.CanJumpToMap(fieldMapData);
  }

  public bool CanJumpToMap(FieldMapTable.FieldMapTableData map)
  {
    return FieldManager.IsOpenPortal(map.jumpPortalID);
  }

  public static bool IsToHardPortal(uint portalId)
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(portalId);
    return portalData != null && FieldManager.IsToHardPortal(portalData);
  }

  public static bool IsToHardPortal(FieldMapTable.PortalTableData portal)
  {
    if (portal.dstMapID != 0U)
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
      if (fieldMapData != null)
        return fieldMapData.fieldMode == DIFFICULTY_MODE.HARD;
    }
    return false;
  }

  public static bool IsEventMap(uint mapId)
  {
    return Singleton<FieldMapTable>.IsValid() && FieldManager.IsEventMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
  }

  public static bool IsEventMap(FieldMapTable.FieldMapTableData map)
  {
    return map != null && map.mapID / 10000000U == 2U;
  }

  public static bool IsQuestMap(uint mapId)
  {
    return Singleton<FieldMapTable>.IsValid() && FieldManager.IsQuestMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
  }

  public static bool IsQuestMap(FieldMapTable.FieldMapTableData map)
  {
    return map != null && map.mapID / 10000000U == 3U;
  }

  public static bool IsInWorldMap(uint mapId)
  {
    if (mapId == 0U)
      return true;
    return Singleton<FieldMapTable>.IsValid() && FieldManager.IsInWorldMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
  }

  public static bool IsInWorldMap(FieldMapTable.FieldMapTableData map)
  {
    return map != null && !FieldManager.IsEventMap(map) && !FieldManager.IsQuestMap(map);
  }

  public static bool HasWorldMap(uint mapId)
  {
    if (mapId == 0U)
      return true;
    return Singleton<FieldMapTable>.IsValid() && FieldManager.HasWorldMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
  }

  public static bool HasWorldMap(FieldMapTable.FieldMapTableData map)
  {
    return map != null && Singleton<RegionTable>.IsValid() && Array.Find<RegionTable.Data>(Singleton<RegionTable>.I.GetData(), (Predicate<RegionTable.Data>) (o => (int) o.regionId == (int) map.regionId)) != null;
  }

  public static bool IsEventRegion(uint regionID)
  {
    if (!Singleton<RegionTable>.IsValid())
      return false;
    RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionID);
    return data != null && 0 < data.eventId;
  }

  public void OnDiff(BaseModelDiff.DiffFieldPortal diff)
  {
    if (QuestManager.IsValidExplore())
      return;
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int i = 0;
      for (int count = diff.add.Count; i < count; i++)
      {
        FieldMapPortalInfo fieldMapPortalInfo = this.current.fieldPortalInfoList.Find((Predicate<FieldMapPortalInfo>) (p => p.IsValid() && (long) p.portalData.portalID == (long) diff.add[i].pId));
        if (fieldMapPortalInfo != null)
        {
          fieldMapPortalInfo.fieldPortal = diff.add[i];
          flag |= fieldMapPortalInfo.IsFull();
        }
      }
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int i = 0;
      for (int count = diff.update.Count; i < count; i++)
      {
        FieldMapPortalInfo fieldMapPortalInfo = this.current.fieldPortalInfoList.Find((Predicate<FieldMapPortalInfo>) (p => p.IsValid() && (long) p.portalData.portalID == (long) diff.update[i].pId));
        if (fieldMapPortalInfo != null)
        {
          if (fieldMapPortalInfo.fieldPortal != null)
          {
            fieldMapPortalInfo.fieldPortal.used = diff.update[i].used;
            fieldMapPortalInfo.fieldPortal.point = diff.update[i].point;
          }
          else
            fieldMapPortalInfo.fieldPortal = MonoBehaviourSingleton<WorldMapManager>.I.GetFieldPortal(diff.update[i].pId);
          flag |= fieldMapPortalInfo.IsFull();
        }
      }
    }
    if (!flag)
      return;
    this._ResetPortalPointToIndex();
  }

  public void OnDiff(BaseModelDiff.DiffFieldGather diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        if (!this.fieldGatherPointIdList.Contains(diff.add[index]))
        {
          this.fieldGatherPointIdList.Add(diff.add[index]);
          flag = true;
        }
      }
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      int index = 0;
      for (int count = diff.del.Count; index < count; ++index)
      {
        this.fieldGatherPointIdList.Remove(diff.del[index]);
        flag = true;
      }
    }
    if (!flag || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.CheckGatherPointList();
  }

  public void OnDiff(BaseModelDiff.DiffFieldGatherGrowth diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        if (!this.fieldGatherGrowthList.Contains(diff.add[index]))
        {
          this.fieldGatherGrowthList.Add(diff.add[index]);
          flag = true;
        }
      }
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      int index = 0;
      for (int count = diff.del.Count; index < count; ++index)
      {
        this.fieldGatherGrowthList.Remove(diff.del[index]);
        flag = true;
      }
    }
    if (!flag || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.CheckGatherPointList();
  }

  public void MatchingNotice()
  {
    if (string.IsNullOrEmpty(this.noticeText))
      return;
    if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(this.noticeText);
    this.noticeText = "";
  }

  public bool isTutorialField
  {
    get => this.currentPortalID == 10000101U || this.currentPortalID == 10000101U;
  }

  public class FieldTransitionInfo
  {
    public uint portalID;
    public uint mapID;
    public float mapX;
    public float mapZ;
    public float mapDir;
  }

  private class CurrentFieldData
  {
    public FieldManager.FieldTransitionInfo fieldTransitionInfo = new FieldManager.FieldTransitionInfo();
    public FieldMapTable.FieldMapTableData mapData;
    public bool isValidBoss;
    public List<FieldMapPortalInfo> fieldPortalInfoList = new List<FieldMapPortalInfo>();
    public int portalPointToIndex = -1;
  }
}
