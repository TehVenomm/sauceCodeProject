// Decompiled with JetBrains decompiler
// Type: CoopRoomPacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopRoomPacketSender : MonoBehaviour
{
  private CoopRoom coopRoom { get; set; }

  protected virtual void Awake()
  {
    this.coopRoom = ((Component) this).gameObject.GetComponent<CoopRoom>();
  }

  protected virtual void Start()
  {
  }

  public void SendSyncAllPortalPoint(List<ExplorePortalPoint> portals, int toClientId)
  {
    Coop_Model_RoomSyncAllPortalPoint model = new Coop_Model_RoomSyncAllPortalPoint();
    model.id = 1001;
    model.SetFromExplorePortalList(portals);
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<Coop_Model_RoomSyncAllPortalPoint>(toClientId, model);
  }

  public void SendUpdatePortalPoint(int portalId, int point, int x, int z)
  {
    Coop_Model_RoomUpdatePortalPoint model = new Coop_Model_RoomUpdatePortalPoint();
    model.id = 1001;
    model.pid = portalId;
    model.pt = point;
    model.x = x;
    model.z = z;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomUpdatePortalPoint>(model);
  }

  public void SendSyncExploreBoss(ExploreStatus explore, int toClientId = -1)
  {
    Coop_Model_RoomSyncExploreBoss model = new Coop_Model_RoomSyncExploreBoss();
    model.id = 1001;
    model.mId = explore.GetCurrentBossMapId();
    if (explore.bossStatus != null)
    {
      model.ceId = (int) explore.bossStatus.coopEnemyId;
      model.hp = (int) explore.bossStatus.hp;
      model.hpm = (int) explore.bossStatus.hpMax;
      model.bhp = (int) explore.bossStatus.barrierHp;
      model.shp = (int) explore.bossStatus.shieldHp;
      model.SetRegions(explore.bossStatus.regionWorks);
      model.concussionTotal = explore.bossStatus.concussionTotal;
      model.concussionMax = explore.bossStatus.concussionMax;
      model.concussionExtend = explore.bossStatus.concussionExtend;
      model.angid = explore.bossStatus.nowAngryId;
      model.eangids = explore.bossStatus.execAngryIds;
      model.isMM = explore.bossStatus.isMadMode;
      model.deadReviveCount = explore.bossStatus.deadReviveCount;
      model.recoveredHP = explore.bossStatus.deadReviveCount;
    }
    else
      model.hp = -1;
    if (toClientId > 0)
      MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<Coop_Model_RoomSyncExploreBoss>(toClientId, model);
    else
      MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomSyncExploreBoss>(model);
  }

  public void SendSyncExploreBossMap(int mapId, int toClientId = -1)
  {
    Coop_Model_RoomSyncExploreBossMap model = new Coop_Model_RoomSyncExploreBossMap();
    model.id = 1001;
    model.mId = mapId;
    if (toClientId > 0)
      MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<Coop_Model_RoomSyncExploreBossMap>(toClientId, model);
    else
      MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomSyncExploreBossMap>(model);
  }

  public void SendExploreBossDamage(int totalDamage)
  {
    Coop_Model_RoomExploreBossDamage model = new Coop_Model_RoomExploreBossDamage();
    model.id = 1001;
    model.dmg = totalDamage;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomExploreBossDamage>(model, false);
  }

  public void SendExploreBossDead(Enemy boss, List<ExplorePlayerStatus> statuses)
  {
    Coop_Model_RoomExploreBossDead model = new Coop_Model_RoomExploreBossDead();
    model.id = 1001;
    model.downCount = boss.downCount;
    model.concussionTotal = boss.concussionTotal;
    model.concussionMax = boss.concussionMax;
    model.concussionExtend = boss.concussionExtend;
    model.breakIds = boss.GetBreakRegionIDList();
    if (statuses != null)
    {
      int index = 0;
      for (int count = statuses.Count; index < count; ++index)
        model.AddTotalDamageFromExplorePlayerStatus(statuses[index]);
    }
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomExploreBossDead>(model);
  }

  public void SendExploreAlive()
  {
    Coop_Model_RoomExploreAlive model = new Coop_Model_RoomExploreAlive();
    model.id = 1001;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomExploreAlive>(model, false);
  }

  public void SendExploreAliveRequest()
  {
    Coop_Model_RoomExploreAliveRequest model = new Coop_Model_RoomExploreAliveRequest();
    model.id = 1001;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomExploreAliveRequest>(model, false);
  }

  public void SendNotifyEncounterBoss(int mapId, int portalId)
  {
    Coop_Model_RoomNotifyEncounterBoss model = new Coop_Model_RoomNotifyEncounterBoss();
    model.id = 1001;
    model.mid = mapId;
    model.pid = portalId;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomNotifyEncounterBoss>(model);
  }

  public void SendNotifyTraceBoss(int mapId, int lastCount)
  {
    Coop_Model_RoomNotifyTraceBoss model = new Coop_Model_RoomNotifyTraceBoss();
    model.id = 1001;
    model.mid = mapId;
    model.lc = lastCount;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomNotifyTraceBoss>(model);
  }

  public void SendSyncPlayerStatus(Self self, int toClientId = -1)
  {
    if (Object.op_Equality((Object) this.coopRoom.clients.Find((Predicate<CoopClient>) (x => x.stageId != MonoBehaviourSingleton<CoopManager>.I.coopMyClient.stageId)), (Object) null))
      return;
    Coop_Model_RoomSyncPlayerStatus model = new Coop_Model_RoomSyncPlayerStatus();
    model.id = 1001;
    model.hp = self.hp;
    model.buff = self.buffParam.CreateSyncParamIfNeeded();
    model.wid = self.weaponData.eId;
    if (QuestManager.IsValidInGameExplore())
    {
      ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
      if (explorePlayerStatus != null)
        model.SetExtraStatus(self, explorePlayerStatus.extraStatus);
      else
        model.SetExtraStatus(self, (List<int>) null);
    }
    if (toClientId > 0)
      MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<Coop_Model_RoomSyncPlayerStatus>(toClientId, model, false);
    else
      MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomSyncPlayerStatus>(model, false);
  }

  public void SendChatStamp(int stamp_id)
  {
    Coop_Model_RoomChatStamp model = new Coop_Model_RoomChatStamp();
    model.id = 1001;
    model.userId = 0;
    model.stampId = stamp_id;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
      model.userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomChatStamp>(model, false);
  }

  public void SendMoveField(int portalId)
  {
    Coop_Model_RoomMoveField model = new Coop_Model_RoomMoveField();
    model.id = 1001;
    model.pid = portalId;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomMoveField>(model);
  }

  public void SendRushRequest()
  {
    Coop_Model_RushRequest model = new Coop_Model_RushRequest();
    model.id = 1001;
    model.requestRushIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RushRequest>(model);
  }

  public void SendRushRequested(int toClientId, int requestRushIndex)
  {
    Coop_Model_RushRequested model = new Coop_Model_RushRequested();
    model.id = 1001;
    model.currentWaveIndex = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex();
    model.syncData = MonoBehaviourSingleton<InGameManager>.I.GetRushSyncData(requestRushIndex);
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<Coop_Model_RushRequested>(toClientId, model);
  }

  public void SendSyncDefenseBattle(float endurance)
  {
    Coop_Model_RoomSyncDefenseBattle model = new Coop_Model_RoomSyncDefenseBattle();
    model.id = 1001;
    model.endurance = endurance;
    MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<Coop_Model_RoomSyncDefenseBattle>(model);
  }
}
