// Decompiled with JetBrains decompiler
// Type: CoopApp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopApp : MonoBehaviourSingleton<CoopApp>
{
  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.AddComponent<CoopManager>();
    ((Component) this).gameObject.AddComponent<KtbWebSocket>();
    ((Component) this).gameObject.AddComponent<CoopNetworkManager>();
    ((Component) this).gameObject.AddComponent<CoopOfflineManager>();
  }

  private static void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public static void EnterQuestOnly(Action<bool> call_back = null) => CoopApp.QuestStart(call_back);

  public static void EnterQuestOffline(Action<bool, bool, bool, bool> call_back = null)
  {
    CoopApp.StartCoopOffline((Action<bool, bool, bool>) ((is_m, is_c, is_r) => CoopApp.QuestStart((Action<bool>) (is_s =>
    {
      if (call_back == null)
        return;
      call_back(is_m, is_c, is_r, is_s);
    }))));
  }

  public static void EnterQuestOfflineAssignedEquipment(
    AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData,
    CharaInfo charaInfo,
    Action<bool, bool, bool, bool> callBack = null)
  {
    CoopApp.StartCoopOffline((Action<bool, bool, bool>) ((isMatching, isConnect, isRegist) => CoopApp.QuestStart((Action<bool>) (isStart =>
    {
      if (isStart)
      {
        if (assignedEquipmentData != null && charaInfo != null)
          MonoBehaviourSingleton<StatusManager>.I.SetAssignedEquipmentData(assignedEquipmentData, charaInfo);
        MonoBehaviourSingleton<QuestManager>.I.StartTrial();
      }
      if (callBack == null)
        return;
      callBack(isMatching, isConnect, isRegist, isStart);
    }))));
  }

  public static void EnterSeriesArenaQuestOffline(Action<bool, bool, bool, bool> callBack = null)
  {
    CoopApp.EnterQuestOffline(callBack);
  }

  public static void EnterArenaQuestOffline(Action<bool, bool, bool, bool> callBack = null)
  {
    QuestManager questMgr = MonoBehaviourSingleton<QuestManager>.I;
    MonoBehaviourSingleton<InGameManager>.I.SetArenaInfo(questMgr.currentArenaId);
    CoopApp.StartCoopOffline((Action<bool, bool, bool>) ((isMatching, isConnect, isRegist) =>
    {
      ArenaStartModel.RequestSendForm requestData = new ArenaStartModel.RequestSendForm();
      requestData.aid = questMgr.currentArenaId;
      requestData.qid = (int) MonoBehaviourSingleton<InGameManager>.I.GetFirstArenaQuestId();
      requestData.setNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
      questMgr.SetCurrentQuestID(MonoBehaviourSingleton<InGameManager>.I.GetFirstArenaQuestId());
      questMgr.SendArenaQuestStart(requestData, (Action<bool>) (isStart =>
      {
        CoopApp.Logd("QuestStarted: {0}: ", (object) isStart);
        if (isStart)
        {
          uint currentMapId = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
          if ((int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) currentMapId)
            MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(currentMapId, 0.0f, 0.0f, 0.0f);
          questMgr.resultUserCollection.AddSelf();
          MonoBehaviourSingleton<LoungeMatchingManager>.I.SendStartArena(questMgr.currentArenaId);
        }
        if (callBack == null)
          return;
        callBack(isMatching, isConnect, isRegist, isStart);
      }));
    }));
  }

  public static void EnterQuest(Action<bool, bool, bool, bool> call_back = null)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
    {
      CoopApp.Logd("already in quest.");
      if (call_back == null)
        return;
      call_back(true, true, true, true);
    }
    else
      CoopApp.MatchingQuestField((Action<bool>) (is_m => CoopApp.StartCoopAndQuestStart(is_m, true, call_back)));
  }

  public static void EnterPartyQuest(Action<bool, bool, bool, bool> call_back = null)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
    {
      CoopApp.Logd("already in party quest.");
      if (call_back == null)
        return;
      call_back(true, true, true, QuestManager.IsValidInGame());
    }
    else
      CoopApp.MatchingPartyField((Action<bool>) (is_m => CoopApp.StartCoopAndQuestStart(is_m, false, call_back)));
  }

  public static void EnterPartyField(Action<bool, bool, bool> call_back = null, bool is_reentry = false)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
    {
      CoopApp.Logd("already in party field.");
      if (call_back == null)
        return;
      call_back(true, true, true);
    }
    else
      CoopApp.MatchingPartyField((Action<bool>) (is_m =>
      {
        if (is_m)
        {
          CoopApp.StartCoop((Action<bool, bool>) ((is_c, is_r) =>
          {
            if (call_back == null)
              return;
            call_back(is_m, is_c, is_r);
          }), is_reentry);
        }
        else
        {
          if (call_back == null)
            return;
          call_back(is_m, false, false);
        }
      }), true);
  }

  public static void EnterField(
    uint portal_id,
    uint deliveryId,
    Action<bool, bool, bool> call_back = null)
  {
    CoopApp.EnterField(portal_id, deliveryId, 0, call_back);
  }

  public static void EnterField(
    uint portal_id,
    uint deliveryId,
    int _toUserId,
    Action<bool, bool, bool> call_back = null)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
    {
      CoopApp.Logd("already in field.");
      if (call_back == null)
        return;
      call_back(true, true, true);
    }
    else
    {
      if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && !MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portal_id, false))
        return;
      if ((int) portal_id != (int) MonoBehaviourSingleton<FieldManager>.I.currentPortalID)
        MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(portal_id);
      CoopApp.MatchingField(deliveryId, _toUserId, (Action<bool>) (is_m =>
      {
        if (is_m)
        {
          CoopApp.StartCoop((Action<bool, bool>) ((is_c, is_r) =>
          {
            if (call_back == null)
              return;
            call_back(is_m, is_c, is_r);
          }));
        }
        else
        {
          if (call_back == null)
            return;
          call_back(is_m, false, false);
        }
      }));
    }
  }

  public static void EnterQuestRandomMatching(Action<bool, bool, bool, bool> call_back = null)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsActivate())
    {
      CoopApp.Logd("already in quest.");
      if (call_back == null)
        return;
      call_back(true, true, true, true);
    }
    else
      CoopApp.RandomMatchingQuestField((Action<bool>) (isSuccess => CoopApp.StartCoopAndQuestStart(isSuccess, true, call_back)));
  }

  private static void StartCoopOffline(Action<bool, bool, bool> call_back = null)
  {
    if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
      return;
    StageObjectManager.CreatePlayerInfo createPlayerInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo();
    if (createPlayerInfo == null)
      return;
    CharaInfo chara_info = createPlayerInfo.charaInfo;
    if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
      MonoBehaviourSingleton<CoopOfflineManager>.I.Activate();
    MonoBehaviourSingleton<CoopNetworkManager>.I.Regist(new CoopNetworkManager.ConnectData(), (Action<bool>) (is_regist =>
    {
      if (is_regist)
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.Activate(new List<FieldModel.SlotInfo>()
        {
          new FieldModel.SlotInfo()
          {
            userId = chara_info.userId,
            userInfo = chara_info as FriendCharaInfo
          }
        });
      if (call_back == null)
        return;
      call_back(true, true, is_regist);
    }));
  }

  private static void StartCoop(Action<bool, bool> call_back = null, bool is_reentry = false)
  {
    MonoBehaviourSingleton<CoopManager>.I.Clear();
    MonoBehaviourSingleton<CoopNetworkManager>.I.ConnectAndRegist(MonoBehaviourSingleton<FieldManager>.I.GetWebSockConnectData(), (Action<bool, bool>) ((is_connect, is_regist) =>
    {
      if (is_regist)
      {
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.Activate(MonoBehaviourSingleton<FieldManager>.I.fieldData.field.slotInfos);
        if (call_back == null)
          return;
        call_back(is_connect, is_regist);
      }
      else if (is_reentry)
      {
        if (call_back == null)
          return;
        call_back(is_connect, is_regist);
      }
      else
        MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty((Action<bool>) (is_leave =>
        {
          if (call_back == null)
            return;
          call_back(is_connect, is_regist);
        }));
    }));
  }

  private static void StartCoopAndQuestStart(
    bool is_m,
    bool isFromField,
    Action<bool, bool, bool, bool> call_back = null)
  {
    if (is_m)
    {
      if (PartyManager.IsValidInParty())
      {
        PartyModel.Party partyData = MonoBehaviourSingleton<PartyManager>.I.partyData;
        PartyModel.ExploreInfo explore = partyData.quest.explore;
        if (explore != null)
        {
          MonoBehaviourSingleton<QuestManager>.I.SetExploreInfo(explore);
          MonoBehaviourSingleton<QuestManager>.I.SetExploreStatus(new ExploreStatus(explore, true));
        }
        if (partyData.quest.rush != null)
        {
          MonoBehaviourSingleton<InGameManager>.I.SetRushInfo(partyData.quest.questId, partyData.quest.rush);
          MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) partyData.quest.rush.waves[0].questId);
        }
      }
      MonoBehaviourSingleton<InGameManager>.I.currentJoinType = !isFromField ? CoopClient.CLIENT_JOIN_TYPE.FROM_QUEST_LIST : CoopClient.CLIENT_JOIN_TYPE.FROM_FIELD;
      CoopApp.StartCoop((Action<bool, bool>) ((is_c, is_r) =>
      {
        if (is_r)
        {
          CoopApp.QuestStart((Action<bool>) (is_s =>
          {
            if (call_back == null)
              return;
            call_back(is_m, is_c, is_r, is_s);
          }));
        }
        else
        {
          if (call_back == null)
            return;
          call_back(is_m, is_c, is_r, false);
        }
      }));
    }
    else
    {
      if (call_back == null)
        return;
      call_back(is_m, false, false, false);
    }
  }

  private static void MatchingField(uint _deliveryId, int _toUserId, Action<bool> call_back = null)
  {
    MonoBehaviourSingleton<FieldManager>.I.SendMatching((int) MonoBehaviourSingleton<FieldManager>.I.currentPortalID, _deliveryId, _toUserId, (Action<bool>) (is_matching =>
    {
      CoopApp.Logd("Matched:{0}", (object) is_matching);
      if (call_back == null)
        return;
      call_back(is_matching);
    }));
  }

  private static void MatchingQuestField(Action<bool> call_back = null)
  {
    MonoBehaviourSingleton<FieldManager>.I.SendQuest((int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID, (Action<bool>) (is_matching =>
    {
      CoopApp.Logd("Matched:{0}", (object) is_matching);
      if (call_back == null)
        return;
      call_back(is_matching);
    }));
  }

  private static void RandomMatchingQuestField(Action<bool> call_back = null)
  {
    int quest_id = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
    MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching(quest_id, 0, false, (Action<bool, int, bool, float>) ((is_success, maxRetryCount, isJoined, waitTime) =>
    {
      CoopApp.Logd("Matched:{0}", (object) is_success);
      if (is_success)
      {
        PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0);
        if (!isJoined)
        {
          MonoBehaviourSingleton<PartyManager>.I.SendCreate(quest_id, setting, (Action<bool>) (is_success2 =>
          {
            if (is_success2)
              MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
            CoopApp.MatchingPartyField(call_back);
          }));
        }
        else
        {
          MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
          CoopApp.MatchingPartyField(call_back);
        }
      }
      else
        call_back.SafeInvoke<bool>(false);
    }));
  }

  private static void MatchingPartyField(Action<bool> call_back = null, bool is_force_enter = false)
  {
    if (!PartyManager.IsValidInParty())
    {
      if (call_back == null)
        return;
      call_back(false);
    }
    else
    {
      string partyId = MonoBehaviourSingleton<PartyManager>.I.GetPartyId();
      bool is_owner = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
      if (is_force_enter)
        is_owner = false;
      MonoBehaviourSingleton<FieldManager>.I.SendParty(partyId, is_owner, (Action<bool>) (is_matching =>
      {
        CoopApp.Logd("Matched:{0}", (object) is_matching);
        if (call_back == null)
          return;
        call_back(is_matching);
      }));
    }
  }

  public static void UpdateField(Action<bool> call_back = null)
  {
    MonoBehaviourSingleton<FieldManager>.I.SendInfo((Action<bool>) (is_get =>
    {
      if (is_get)
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.SetSlotInfos(MonoBehaviourSingleton<FieldManager>.I.fieldData.field.slotInfos);
      if (call_back == null)
        return;
      call_back(is_get);
    }));
  }

  public static void QuestStart(Action<bool> call_back = null)
  {
    int eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    int questId = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
    bool currentQuestIsFreeJoin = MonoBehaviourSingleton<QuestManager>.I.currentQuestIsFreeJoin;
    ExploreStatus exploreStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus();
    if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
      questId = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.questId;
    MonoBehaviourSingleton<QuestManager>.I.SendQuestStart(questId, eSetNo, currentQuestIsFreeJoin, (Action<bool>) (is_start =>
    {
      CoopApp.Logd("QuestStarted:{0}", (object) is_start);
      if (is_start)
      {
        uint map_id = MonoBehaviourSingleton<QuestManager>.I.GetCurrentMapId();
        if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
          map_id = (uint) MonoBehaviourSingleton<QuestManager>.I.GetExploreStartMapId();
        if ((int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) map_id)
          MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID(map_id, 0.0f, 0.0f, 0.0f);
        MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.AddSelf();
        if (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestType() == QUEST_TYPE.ORDER && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialQuestId != MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId())
          MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = true;
      }
      if (call_back == null)
        return;
      call_back(is_start);
    }));
    if (exploreStatus != null)
      MonoBehaviourSingleton<QuestManager>.I.SetExploreStatus(exploreStatus);
    if (!PartyManager.IsValidInParty())
      return;
    PartyModel.ExploreInfo explore = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore;
    if (explore == null)
      return;
    MonoBehaviourSingleton<QuestManager>.I.SetExploreInfo(explore);
  }

  public static void QuestComplete(Action<bool, Error> call_back = null)
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid())
    {
      if (call_back == null)
        return;
      call_back(false, Error.Unknown);
    }
    else
    {
      List<List<int>> breakIds = new List<List<int>>();
      float hpRate = 100f;
      if (MonoBehaviourSingleton<CoopManager>.IsValid())
      {
        breakIds = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists;
        hpRate = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate;
      }
      if (QuestManager.IsValidInGameExplore())
      {
        List<int> exploreBossBreakIdList = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBreakIdList();
        if (breakIds == null)
        {
          MonoBehaviourSingleton<CoopManager>.I.coopStage.InitBossBreakIdList();
          breakIds = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists;
        }
        if (breakIds.Count == 0)
          breakIds.Add(exploreBossBreakIdList);
        else
          breakIds[0] = exploreBossBreakIdList;
        hpRate = 100f;
      }
      if (QuestManager.IsValidInGameWaveMatch())
      {
        List<int> intList = new List<int>();
        intList.Add(0);
        if (breakIds.Count > 0)
          breakIds[0] = intList;
        else
          breakIds.Add(intList);
      }
      List<int> missionClearStatuses = MonoBehaviourSingleton<InGameProgress>.I.GetMissionClearStatuses();
      List<int> memIds = (List<int>) null;
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
        memIds = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
      List<QuestCompleteModel.BattleUserLog> logs = (List<QuestCompleteModel.BattleUserLog>) null;
      if (MonoBehaviourSingleton<CoopManager>.IsValid())
        logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
      if (QuestManager.IsValidTrial())
        MonoBehaviourSingleton<QuestManager>.I.SendQuestCompleteTrial(missionClearStatuses, (Action<bool, Error>) ((is_comp, result) =>
        {
          CoopApp.Logd("Trial Completed:{0}", (object) is_comp);
          if (call_back == null)
            return;
          call_back(is_comp, result);
        }));
      else
        MonoBehaviourSingleton<QuestManager>.I.SendQuestComplete(breakIds, missionClearStatuses, memIds, hpRate, logs, (Action<bool, Error>) ((is_comp, result) =>
        {
          CoopApp.Logd("Quest Completed:{0}", (object) is_comp);
          if (call_back == null)
            return;
          call_back(is_comp, result);
        }));
    }
  }

  public static void ArenaComplete(Action<bool, Error> callBack = null)
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid() && callBack != null)
      callBack(false, Error.Unknown);
    else
      MonoBehaviourSingleton<QuestManager>.I.SendArenaComplete(callBack);
  }

  public static void QuestRetire(bool is_timeout, Action<bool> call_back = null)
  {
    string roomId = "";
    List<int> memIDs = (List<int>) null;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
      memIDs = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    List<QuestCompleteModel.BattleUserLog> logs = (List<QuestCompleteModel.BattleUserLog>) null;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
    MonoBehaviourSingleton<QuestManager>.I.SendQuestRetire(is_timeout, memIDs, roomId, logs, call_back);
  }

  public static void ArenaRetire(bool isTimeout, Action<bool> callBack = null)
  {
    ArenaRetireModel.RequestSendForm requestData = new ArenaRetireModel.RequestSendForm();
    requestData.timeout = isTimeout ? 1 : 0;
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      requestData.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaWaveNum();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      requestData.logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      requestData.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Implicit((Object) MonoBehaviourSingleton<StageObjectManager>.I.self))
    {
      requestData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
      MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
    }
    MonoBehaviourSingleton<QuestManager>.I.SendArenaRetire(requestData, callBack);
  }

  public void Leave(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false)
  {
    CoopApp.Logd("Leave. start");
    this.StartCoroutine(this.LeaveCoroutine(call_back, toHome, fieldRetire));
  }

  public void LeaveWithParty(Action<bool> call_back = null, bool toHome = false, bool fieldRetire = false)
  {
    CoopApp.Logd("LeaveWithParty. start");
    this.StartCoroutine(this.LeaveCoroutine(call_back, toHome, fieldRetire, true));
  }

  private IEnumerator LeaveCoroutine(
    Action<bool> call_back = null,
    bool toHome = false,
    bool fieldRetire = false,
    bool isParty = false)
  {
    bool is_success = true;
    if (FieldManager.IsValidInField())
    {
      bool is_leaved = false;
      bool wait = true;
      MonoBehaviourSingleton<FieldManager>.I.SendLeave(toHome, fieldRetire, (Action<bool>) (is_leave =>
      {
        wait = false;
        is_leaved = is_leave;
      }));
      while (wait)
        yield return (object) null;
      CoopApp.Logd("LeaveCoroutine. Field leaved:{0}", (object) is_leaved);
      is_success = is_leaved;
    }
    if (isParty && PartyManager.IsValidInParty())
    {
      bool is_leaved = false;
      bool wait = true;
      MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (is_leave =>
      {
        wait = false;
        is_leaved = is_leave;
      }));
      while (wait)
        yield return (object) null;
      CoopApp.Logd("LeaveCoroutine. Party leaved:{0}", (object) is_leaved);
    }
    if (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
    {
      bool wait = true;
      MonoBehaviourSingleton<CoopNetworkManager>.I.Close(call_back: (System.Action) (() => wait = false));
      while (wait)
        yield return (object) null;
    }
    MonoBehaviourSingleton<CoopManager>.I.Clear();
    if (call_back != null)
      call_back(is_success);
  }

  private IEnumerator OnApplicationPause(bool paused)
  {
    if (CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen())
    {
      CoopApp.Logd("OnApplicationPause. pause={0}, is_connect={1}", (object) paused, (object) CoopWebSocketSingleton<KtbWebSocket>.IsValidOpen());
      if (paused)
      {
        if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
        {
          if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.IsEnabledStandby())
          {
            MonoBehaviourSingleton<CoopNetworkManager>.I.Standby();
          }
          else
          {
            MonoBehaviourSingleton<CoopNetworkManager>.I.LoopBackRoomLeave(true);
            if (PartyManager.IsValidInParty() && !InGameManager.IsReentryNotLeaveParty())
              Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (is_leave => CoopApp.Logd("PartyLeave. {0}", (object) is_leave)))));
            MonoBehaviourSingleton<KtbWebSocket>.I.Close();
          }
        }
      }
      else
      {
        if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.IsEnabledStandby())
        {
          MonoBehaviourSingleton<CoopNetworkManager>.I.Resume();
          if (MonoBehaviourSingleton<InGameProgress>.IsValid())
            MonoBehaviourSingleton<CoopManager>.I.coopStage.packetSender.SendStageSyncTimeRequest();
          if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.playerList != null)
          {
            List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
            for (int index = 0; index < playerList.Count; ++index)
            {
              Player player = playerList[index] as Player;
              if (Object.op_Inequality((Object) player, (Object) null) && (double) player.rescueTime > 0.0 && !player.IsPrayed() && !player.isWaitingResurrectionHoming)
                player.playerSender.SendDeadCountRequest(player.id);
            }
          }
        }
        MonoBehaviourSingleton<KtbWebSocket>.I.ClearLastPacketReceivedTime();
        yield break;
      }
    }
  }
}
