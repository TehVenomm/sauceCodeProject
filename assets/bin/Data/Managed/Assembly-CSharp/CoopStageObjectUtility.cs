using System;
using System.Collections.Generic;
using UnityEngine;

public static class CoopStageObjectUtility
{
	public static void SetAI(Character chara)
	{
		if (chara is Player)
		{
			chara.AddController<NpcController>();
		}
		else if (chara is Enemy)
		{
			chara.AddController<EnemyController>();
		}
		chara.SafeActIdle();
	}

	public static void SetCoopModeForAll(StageObject.COOP_MODE_TYPE coop_mode, int client_id)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.objectList.Count; i < count; i++)
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.objectList[i];
			if (stageObject.coopMode != coop_mode)
			{
				stageObject.SetCoopMode(coop_mode, client_id);
			}
		}
	}

	public static void SetOfflineForAll()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(delegate(StageObject obj)
		{
			obj.get_gameObject().SetActive(true);
		});
		MonoBehaviourSingleton<StageObjectManager>.I.ClearCacheObject();
		Vector3 val = Vector3.get_zero();
		if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			val = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.get_position();
		}
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.objectList.Count; i < count; i++)
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.objectList[i];
			if (!stageObject.IsCoopNone())
			{
				stageObject.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
			}
			stageObject.isCoopInitialized = true;
			if (stageObject is Player)
			{
				Player player = stageObject as Player;
				SetAI(player);
				if (!player.isSetAppearPos)
				{
					if (player is Self)
					{
						player.SetAppearPosOwner(val);
					}
					else
					{
						player.SetAppearPosGuest(val);
					}
				}
				if (player.isWaitBattleStart)
				{
					player.ActBattleStart();
				}
			}
			else if (stageObject is Enemy)
			{
				Enemy enemy = stageObject as Enemy;
				SetAI(enemy);
				if (!enemy.isSetAppearPos)
				{
					enemy.SetAppearPosEnemy();
				}
			}
		}
	}

	public static void TransfarOwner(StageObject obj, int owner_client_id)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == owner_client_id)
		{
			if (!CanControll(obj))
			{
				Log.Error(LOG.COOP, "TransfarOwner. field block obj({0}) to original", obj);
				return;
			}
			obj.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
			Character character = obj as Character;
			if (character != null)
			{
				SetAI(character);
				if (!character.isSetAppearPos)
				{
					character.SetAppearPos(character._position);
				}
				character.characterSender.SendInitialize();
			}
		}
		else
		{
			if (obj is Player)
			{
				obj.SetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET, owner_client_id);
			}
			else
			{
				obj.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, owner_client_id);
			}
			obj.isCoopInitialized = false;
			Character character2 = obj as Character;
			if (character2 != null)
			{
				character2.RemoveController();
				character2.SafeActIdle();
				character2.characterReceiver.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE);
			}
		}
	}

	public static void TransfarOwnerForClientObjects(int client_id, int owner_client_id)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		if (!FieldManager.IsValidInGameNoQuest())
		{
			int num = 0;
			while (num < MonoBehaviourSingleton<StageObjectManager>.I.cacheList.Count)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.cacheList[num] as Player;
				if (player != null)
				{
					player.get_gameObject().SetActive(true);
					MonoBehaviourSingleton<StageObjectManager>.I.cacheList.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}
		bool flag = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == client_id;
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.objectList.Count; i < count; i++)
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.objectList[i];
			if ((stageObject.coopClientId == client_id || (stageObject.coopClientId == 0 && flag)) && (!FieldManager.IsValidInGame() || !(stageObject is Enemy)) && (!FieldManager.IsValidInGameNoQuest() || !(stageObject is Player)))
			{
				TransfarOwner(stageObject, owner_client_id);
			}
		}
	}

	public static bool FillNonPlayer(int nonplayer_max, int client_num)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		if (!QuestManager.IsValidInGame())
		{
			return false;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			return false;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
		{
			return false;
		}
		if (QuestManager.IsValidTrial())
		{
			return false;
		}
		if (QuestManager.IsValidInGameSeriesArena())
		{
			return false;
		}
		int player_num = 0;
		MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach(delegate(StageObject o)
		{
			if (!o.isDestroyWaitFlag)
			{
				player_num++;
			}
		});
		MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(delegate(StageObject o)
		{
			if (o is Player && !o.isDestroyWaitFlag)
			{
				player_num++;
			}
		});
		if (client_num <= 0)
		{
			client_num = 1;
		}
		int num = nonplayer_max;
		if (QuestManager.IsValidInGame())
		{
			num = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestMaxTeamMemberNum();
		}
		int num2 = num - Mathf.Max(player_num, client_num);
		bool result = false;
		for (int i = 0; i < num2; i++)
		{
			int id = MonoBehaviourSingleton<CoopManager>.I.CreateUniqueNonPlayerID();
			MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(id, delegate(object o)
			{
				NonPlayer nonPlayer = o as NonPlayer;
				if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsBattle())
				{
					nonPlayer.ActBattleStart();
				}
				else if (nonPlayer.controller != null)
				{
					nonPlayer.controller.SetEnableControll(enable: false, ControllerBase.DISABLE_FLAG.BATTLE_START);
				}
			});
			result = true;
		}
		return result;
	}

	public static void ShrinkOriginalNonPlayer(int player_max)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		int num = player_max;
		if (QuestManager.IsValidInGame())
		{
			num = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestMaxTeamMemberNum();
		}
		int player_num = 0;
		MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach(delegate(StageObject o)
		{
			if (!o.isDestroyWaitFlag)
			{
				player_num++;
			}
		});
		MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(delegate(StageObject o)
		{
			if (o is Player && !o.isDestroyWaitFlag)
			{
				player_num++;
			}
		});
		int num2 = player_num - num;
		if (num2 > 0)
		{
			List<StageObject> destroyList = new List<StageObject>();
			Action<StageObject> action = delegate(StageObject o)
			{
				if (o is NonPlayer && !o.isDestroyWaitFlag && (o.IsCoopNone() || o.IsOriginal()))
				{
					destroyList.Add(o);
				}
			};
			MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(action);
			MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.ForEach(action);
			int num3 = Math.Min(num2, destroyList.Count);
			for (int i = 0; i < num3; i++)
			{
				StageObject stageObject = destroyList[i];
				stageObject.DestroyObject();
			}
		}
	}

	public static void DestroyAllNonPlayer()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<StageObject> destroyList = new List<StageObject>();
			Action<StageObject> action = delegate(StageObject o)
			{
				if (o is NonPlayer && !o.isDestroyWaitFlag && (o.IsCoopNone() || o.IsOriginal()))
				{
					destroyList.Add(o);
				}
			};
			MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(action);
			MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.ForEach(action);
			Action<StageObject> action2 = delegate(StageObject o)
			{
				Player player = o as Player;
				if (player != null && player.isNpc)
				{
					destroyList.Add(o);
				}
			};
			MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach(action2);
			for (int i = 0; i < destroyList.Count; i++)
			{
				StageObject stageObject = destroyList[i];
				stageObject.DestroyObject();
			}
		}
	}

	public static void OnlySelf()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<StageObject> destroyList = new List<StageObject>();
			MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(delegate(StageObject o)
			{
				if (o is Player && !(o is Self))
				{
					destroyList.Add(o);
				}
			});
			MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach(delegate(StageObject o)
			{
				if (!(o is Self))
				{
					destroyList.Add(o);
				}
			});
			destroyList.ForEach(delegate(StageObject obj)
			{
				obj.DestroyObject();
			});
		}
	}

	public static bool CanControll(StageObject obj)
	{
		if (QuestManager.IsValidInGame())
		{
			return true;
		}
		if (FieldManager.IsValidInTutorial())
		{
			return true;
		}
		if (obj is Player)
		{
			if (obj is Self)
			{
				return true;
			}
			return false;
		}
		return true;
	}
}
