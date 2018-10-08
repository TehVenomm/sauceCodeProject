using System.Collections.Generic;
using UnityEngine;

public class BattleUserLog
{
	public List<QuestCompleteModel.BattleUserLog> list = new List<QuestCompleteModel.BattleUserLog>();

	public void Clear()
	{
		list.Clear();
	}

	public void Add(Character to_chara, AttackedHitStatusOwner status)
	{
		if (!to_chara.isDead && status.validDamage)
		{
			int skill_id = 0;
			if (status.skillParam != null)
			{
				skill_id = status.skillParam.baseInfo.id;
			}
			int damage = status.validDamage ? status.damage : 0;
			Add(to_chara, status.fromObjectID, skill_id, status.attackInfo.name, damage);
		}
	}

	public void Add(Character to_chara, AttackedHitStatusFix status)
	{
		if (!to_chara.isDead && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
		{
			int skill_id = 0;
			if (status.skillParam != null)
			{
				skill_id = status.skillParam.baseInfo.id;
			}
			int damage = status.damage;
			Add(to_chara, status.fromObjectID, skill_id, status.attackInfo.name, damage);
		}
	}

	public void Add(Character to_chara, Enemy.BleedSyncData.BleedDamageData bleed_damage)
	{
		if (bleed_damage.damage > 0 && (to_chara.IsCoopNone() || to_chara.IsOriginal()))
		{
			Add(to_chara, bleed_damage.ownerID, 0, "ARROW_BLEED", bleed_damage.damage);
		}
	}

	public void Add(Character to_chara, BuffParam.BUFFTYPE type, int damage)
	{
		if (damage > 0 && (to_chara.IsCoopNone() || to_chara.IsOriginal()))
		{
			int playerId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.playerId;
			Add(to_chara, playerId, 0, "BUFF_" + type.ToString(), damage);
		}
	}

	public void Add(Character to_chara, int from_objid, int skill_id, string attack_info_name, int damage)
	{
		if (list != null && !string.IsNullOrEmpty(attack_info_name))
		{
			int num = 0;
			string empty = string.Empty;
			int baseId = 0;
			bool flag = false;
			if (to_chara is Enemy)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(from_objid) as Player;
				if (player == null)
				{
					return;
				}
				flag = (player.controller is NpcController);
				empty = player.charaName;
				if (player is NonPlayer)
				{
					num = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userId;
					baseId = (player as NonPlayer).npcId;
				}
				else
				{
					CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByPlayerId(from_objid);
					if (coopClient != null)
					{
						num = coopClient.userId;
					}
					else if (player.record != null)
					{
						num = player.record.charaInfo.userId;
					}
				}
			}
			else
			{
				if (!(to_chara is Player))
				{
					return;
				}
				Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(from_objid) as Enemy;
				if (enemy == null)
				{
					return;
				}
				num = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userId;
				empty = enemy.charaName;
				baseId = enemy.enemyID;
			}
			QuestCompleteModel.BattleUserLog battleUserLog = null;
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				if (list[i].userId == num && list[i].isNpc == flag && list[i].objId == from_objid && list[i].leaveCnt == MonoBehaviourSingleton<CoopManager>.I.coopRoom.roomLeaveCnt)
				{
					battleUserLog = list[i];
					break;
				}
			}
			if (battleUserLog == null)
			{
				battleUserLog = new QuestCompleteModel.BattleUserLog();
				battleUserLog.userId = num;
				battleUserLog.name = empty;
				battleUserLog.baseId = baseId;
				battleUserLog.objId = from_objid;
				battleUserLog.isNpc = flag;
				battleUserLog.hostUserId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userId;
				battleUserLog.leaveCnt = MonoBehaviourSingleton<CoopManager>.I.coopRoom.roomLeaveCnt;
				battleUserLog.startRemaindTime = ((!MonoBehaviourSingleton<InGameProgress>.IsValid()) ? 0f : MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
				list.Add(battleUserLog);
			}
			QuestCompleteModel.BattleUserLog.AtkInfo atkInfo = null;
			int j = 0;
			for (int count2 = battleUserLog.atkInfos.Count; j < count2; j++)
			{
				if (battleUserLog.atkInfos[j].name == attack_info_name && battleUserLog.atkInfos[j].skillId == skill_id)
				{
					atkInfo = battleUserLog.atkInfos[j];
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
			atkInfo.count++;
		}
	}

	public void LogDump()
	{
		list.ForEach(delegate(QuestCompleteModel.BattleUserLog user_log)
		{
			Debug.Log((object)$"###### name : {user_log.name}, userId : {user_log.userId}, baseId : {user_log.baseId}, objId : {user_log.objId}, isNpc : {user_log.isNpc}");
			user_log.atkInfos.ForEach(delegate(QuestCompleteModel.BattleUserLog.AtkInfo atk_info)
			{
				Debug.Log((object)$"atkinfo : {atk_info.name}, skillId : {atk_info.skillId}, count : {atk_info.count}, damage : {atk_info.damage}");
			});
		});
	}
}
