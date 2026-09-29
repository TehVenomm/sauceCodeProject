// Decompiled with JetBrains decompiler
// Type: BattleUserLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BattleUserLog
{
  public List<QuestCompleteModel.BattleUserLog> list = new List<QuestCompleteModel.BattleUserLog>();

  public void Clear() => this.list.Clear();

  public void Add(Character to_chara, AttackedHitStatusOwner status)
  {
    if (to_chara.isDead || !status.validDamage)
      return;
    int skill_id = 0;
    if (status.skillParam != null)
      skill_id = status.skillParam.baseInfo.id;
    int damage = status.validDamage ? status.damage : 0;
    this.Add(to_chara, status.fromObjectID, skill_id, status.attackInfo.name, damage);
  }

  public void Add(Character to_chara, AttackedHitStatusFix status)
  {
    if (to_chara.isDead || !MonoBehaviourSingleton<CoopManager>.I.isStageHost)
      return;
    int skill_id = 0;
    if (status.skillParam != null)
      skill_id = status.skillParam.baseInfo.id;
    int damage = status.damage;
    this.Add(to_chara, status.fromObjectID, skill_id, status.attackInfo.name, damage);
  }

  public void Add(Character to_chara, Enemy.BleedSyncData.BleedDamageData bleed_damage)
  {
    if (bleed_damage.damage <= 0 || !to_chara.IsCoopNone() && !to_chara.IsOriginal())
      return;
    this.Add(to_chara, bleed_damage.ownerID, 0, "ARROW_BLEED", bleed_damage.damage);
  }

  public void Add(Character to_chara, BuffParam.BUFFTYPE type, int damage)
  {
    if (damage <= 0 || !to_chara.IsCoopNone() && !to_chara.IsOriginal())
      return;
    int playerId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.playerId;
    this.Add(to_chara, playerId, 0, "BUFF_" + type.ToString(), damage);
  }

  public void Add(
    Character to_chara,
    int from_objid,
    int skill_id,
    string attack_info_name,
    int damage)
  {
    if (this.list == null || string.IsNullOrEmpty(attack_info_name))
      return;
    int num1 = 0;
    int num2 = 0;
    bool flag = false;
    string charaName;
    switch (to_chara)
    {
      case Enemy _:
        Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(from_objid) as Player;
        if (Object.op_Equality((Object) player, (Object) null))
          return;
        flag = player.controller is NpcController;
        charaName = player.charaName;
        if (player is NonPlayer)
        {
          num1 = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userId;
          num2 = (player as NonPlayer).npcId;
          break;
        }
        CoopClient byPlayerId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByPlayerId(from_objid);
        if (Object.op_Inequality((Object) byPlayerId, (Object) null))
        {
          num1 = byPlayerId.userId;
          break;
        }
        if (player.record != null)
        {
          num1 = player.record.charaInfo.userId;
          break;
        }
        break;
      case Player _:
        Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(from_objid) as Enemy;
        if (Object.op_Equality((Object) enemy, (Object) null))
          return;
        num1 = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userId;
        charaName = enemy.charaName;
        num2 = enemy.enemyID;
        break;
      default:
        return;
    }
    QuestCompleteModel.BattleUserLog battleUserLog = (QuestCompleteModel.BattleUserLog) null;
    int index1 = 0;
    for (int count = this.list.Count; index1 < count; ++index1)
    {
      if (this.list[index1].userId == num1 && this.list[index1].isNpc == flag && this.list[index1].objId == from_objid && this.list[index1].leaveCnt == MonoBehaviourSingleton<CoopManager>.I.coopRoom.roomLeaveCnt)
      {
        battleUserLog = this.list[index1];
        break;
      }
    }
    if (battleUserLog == null)
    {
      battleUserLog = new QuestCompleteModel.BattleUserLog();
      battleUserLog.userId = num1;
      battleUserLog.name = charaName;
      battleUserLog.baseId = num2;
      battleUserLog.objId = from_objid;
      battleUserLog.isNpc = flag;
      battleUserLog.hostUserId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userId;
      battleUserLog.leaveCnt = MonoBehaviourSingleton<CoopManager>.I.coopRoom.roomLeaveCnt;
      battleUserLog.startRemaindTime = MonoBehaviourSingleton<InGameProgress>.IsValid() ? MonoBehaviourSingleton<InGameProgress>.I.remaindTime : 0.0f;
      this.list.Add(battleUserLog);
    }
    QuestCompleteModel.BattleUserLog.AtkInfo atkInfo = (QuestCompleteModel.BattleUserLog.AtkInfo) null;
    int index2 = 0;
    for (int count = battleUserLog.atkInfos.Count; index2 < count; ++index2)
    {
      if (battleUserLog.atkInfos[index2].name == attack_info_name && battleUserLog.atkInfos[index2].skillId == skill_id)
      {
        atkInfo = battleUserLog.atkInfos[index2];
        break;
      }
    }
    if (atkInfo == null)
    {
      atkInfo = new QuestCompleteModel.BattleUserLog.AtkInfo();
      atkInfo.name = attack_info_name;
      atkInfo.skillId = skill_id;
      battleUserLog.atkInfos.Add(atkInfo);
    }
    atkInfo.damage += damage;
    ++atkInfo.count;
  }

  public void LogDump()
  {
    this.list.ForEach((Action<QuestCompleteModel.BattleUserLog>) (user_log =>
    {
      Debug.Log((object) $"###### name : {user_log.name}, userId : {user_log.userId}, baseId : {user_log.baseId}, objId : {user_log.objId}, isNpc : {user_log.isNpc}");
      user_log.atkInfos.ForEach((Action<QuestCompleteModel.BattleUserLog.AtkInfo>) (atk_info => Debug.Log((object) $"atkinfo : {atk_info.name}, skillId : {atk_info.skillId}, count : {atk_info.count}, damage : {atk_info.damage}")));
    }));
  }
}
