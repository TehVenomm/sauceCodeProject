using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRecorder : MonoBehaviourSingleton<InGameRecorder>
{
	public abstract class CharacterRecord
	{
		public int id;
	}

	public class PlayerRecord : CharacterRecord
	{
		public bool isSelf;

		public bool isSyncOwner;

		public bool isNPC;

		public PlayerLoadInfo playerLoadInfo;

		public int animID;

		public CharaInfo charaInfo;

		public int beforeLevel;

		public int givenTotalDamage;

		public bool isShowModel = true;
	}

	public class PlayerRecordSyncHost
	{
		public int id;

		public bool isNPC;

		public PlayerLoadInfo playerLoadInfo;

		public int animID;

		public CharaInfo charaInfo;

		public int beforeLevel;

		public int givenTotalDamage;
	}

	public class EnemyRecord : CharacterRecord
	{
		public int hp;
	}

	public InGameProgress.PROGRESS_END_TYPE progressEndType;

	public string rushRemainTimeToString = string.Empty;

	public float arenaElapsedTime;

	public string arenaRemainTimeToString = string.Empty;

	public List<PlayerRecord> players
	{
		get;
		private set;
	}

	public List<EnemyRecord> enemies
	{
		get;
		private set;
	}

	public bool isVictory
	{
		get;
		private set;
	}

	public PlayerRecord pickupPlayer
	{
		get;
		private set;
	}

	public Vector3 pickupPlayerPos
	{
		get;
		private set;
	}

	public float pickupPlayerRot
	{
		get;
		private set;
	}

	public PlayerLoader[] playerModels
	{
		get;
		private set;
	}

	public InGameRecorder()
	{
		players = new List<PlayerRecord>();
		enemies = new List<EnemyRecord>();
	}

	public PlayerRecord GetPlayer(int id, int? user_id = default(int?))
	{
		int num = -1;
		if (user_id.HasValue)
		{
			num = user_id.Value;
		}
		PlayerRecord playerRecord = null;
		if (num > 0)
		{
			playerRecord = GetPlayerByUserId(num);
		}
		if (playerRecord != null)
		{
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByUserId(num);
			if ((UnityEngine.Object)coopClient != (UnityEngine.Object)null)
			{
				playerRecord.id = coopClient.playerId;
			}
		}
		else
		{
			playerRecord = players.Find((PlayerRecord o) => o.id == id);
			if (playerRecord == null)
			{
				playerRecord = new PlayerRecord();
				playerRecord.id = id;
				players.Add(playerRecord);
			}
		}
		return playerRecord;
	}

	public PlayerRecord GetPlayerByUserId(int userId)
	{
		return players.Find((PlayerRecord o) => o.charaInfo != null && o.charaInfo.userId == userId);
	}

	public List<PlayerRecordSyncHost> CreateSyncHostData()
	{
		List<PlayerRecordSyncHost> list = new List<PlayerRecordSyncHost>();
		int i = 0;
		for (int count = players.Count; i < count; i++)
		{
			PlayerRecord playerRecord = players[i];
			if (playerRecord.id >= 0)
			{
				PlayerRecordSyncHost playerRecordSyncHost = new PlayerRecordSyncHost();
				list.Add(playerRecordSyncHost);
				playerRecordSyncHost.id = playerRecord.id;
				playerRecordSyncHost.isNPC = playerRecord.isNPC;
				playerRecordSyncHost.playerLoadInfo = playerRecord.playerLoadInfo;
				playerRecordSyncHost.animID = playerRecord.animID;
				playerRecordSyncHost.charaInfo = playerRecord.charaInfo;
				playerRecordSyncHost.beforeLevel = playerRecord.beforeLevel;
				playerRecordSyncHost.givenTotalDamage = playerRecord.givenTotalDamage;
			}
		}
		return list;
	}

	public PlayerRecordSyncHost CreateSyncHostData(int userId)
	{
		PlayerRecordSyncHost playerRecordSyncHost = null;
		int i = 0;
		for (int count = players.Count; i < count; i++)
		{
			PlayerRecord playerRecord = players[i];
			if (playerRecord.id >= 0 && (playerRecord.charaInfo == null || playerRecord.charaInfo.userId == userId))
			{
				playerRecordSyncHost = new PlayerRecordSyncHost();
				playerRecordSyncHost.id = playerRecord.id;
				playerRecordSyncHost.isNPC = playerRecord.isNPC;
				playerRecordSyncHost.playerLoadInfo = playerRecord.playerLoadInfo;
				playerRecordSyncHost.animID = playerRecord.animID;
				playerRecordSyncHost.charaInfo = playerRecord.charaInfo;
				playerRecordSyncHost.beforeLevel = playerRecord.beforeLevel;
				playerRecordSyncHost.givenTotalDamage = playerRecord.givenTotalDamage;
			}
		}
		return playerRecordSyncHost;
	}

	public void ApplySyncHostData(List<PlayerRecordSyncHost> sync_host_list)
	{
		int i = 0;
		for (int count = sync_host_list.Count; i < count; i++)
		{
			PlayerRecordSyncHost sync_host = sync_host_list[i];
			ApplySyncHostData(sync_host);
		}
	}

	public void ApplySyncHostData(PlayerRecordSyncHost sync_host)
	{
		if (sync_host.id >= 0)
		{
			int? user_id = (sync_host.charaInfo == null) ? null : new int?(sync_host.charaInfo.userId);
			PlayerRecord player = GetPlayer(sync_host.id, user_id);
			player.givenTotalDamage = sync_host.givenTotalDamage;
			player.isNPC = sync_host.isNPC;
			player.playerLoadInfo = sync_host.playerLoadInfo;
			player.animID = sync_host.animID;
			player.charaInfo = sync_host.charaInfo;
			player.beforeLevel = sync_host.beforeLevel;
		}
	}

	public void ApplySyncOwnerData(int id)
	{
		PlayerRecord player = GetPlayer(id, null);
		if (!player.isSyncOwner)
		{
			player.isSyncOwner = true;
		}
	}

	public void RecordEnemyHP(int id, int hp)
	{
		EnemyRecord enemyRecord = enemies.Find((EnemyRecord o) => o.id == id);
		if (enemyRecord == null)
		{
			enemyRecord = new EnemyRecord();
			enemyRecord.id = id;
			enemies.Add(enemyRecord);
		}
		enemyRecord.hp = hp;
	}

	public void RecordGivenDamage(int player_id, int damage)
	{
		PlayerRecord player = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayer(player_id, null);
		if (player != null)
		{
			player.givenTotalDamage += damage;
		}
	}

	public int GetTotalEnemyHP()
	{
		int total = 0;
		enemies.ForEach(delegate(EnemyRecord o)
		{
			total += o.hp;
		});
		return total;
	}

	public void OnInGameEnd(bool is_victory)
	{
		isVictory = is_victory;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<PlayerRecord> list = new List<PlayerRecord>();
			players.ForEach(delegate(PlayerRecord o)
			{
				if (!((UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(o.id) == (UnityEngine.Object)null))
				{
					list.Add(o);
				}
			});
			players = list;
		}
		players.Sort(delegate(PlayerRecord a, PlayerRecord b)
		{
			if (a.givenTotalDamage != b.givenTotalDamage)
			{
				return (a.givenTotalDamage <= b.givenTotalDamage) ? 1 : (-1);
			}
			return a.id - b.id;
		});
		List<PlayerRecord> list2 = new List<PlayerRecord>();
		for (int i = 0; i < 8 && i < players.Count; i++)
		{
			list2.Add(players[i]);
		}
		players = list2;
		if (!isVictory && MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			StageObject stageObject = null;
			PlayerRecord playerRecord = null;
			playerRecord = GetSelfPlayerRecord();
			if (playerRecord != null)
			{
				stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerRecord.id);
			}
			if (playerRecord != null && (UnityEngine.Object)stageObject != (UnityEngine.Object)null)
			{
				Vector3 position = stageObject._transform.position;
				position.y = 0f;
				pickupPlayerPos = position;
				Vector3 eulerAngles = stageObject._transform.eulerAngles;
				pickupPlayerRot = eulerAngles.y;
				pickupPlayer = playerRecord;
			}
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			progressEndType = MonoBehaviourSingleton<InGameProgress>.I.progressEndType;
			rushRemainTimeToString = MonoBehaviourSingleton<InGameProgress>.I.GetRushRemainTimeToString();
			arenaRemainTimeToString = MonoBehaviourSingleton<InGameProgress>.I.GetArenaRemainTimeToString();
			arenaElapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime();
		}
	}

	public PlayerLoader[] CreatePlayerModels()
	{
		DeletePlayerModels();
		int anim_id = (!isVictory) ? 91 : (-1);
		List<PlayerRecord> list = players.FindAll((PlayerRecord x) => x.isShowModel);
		if (!isVictory)
		{
			pickupPlayer = GetSelfPlayerRecord();
		}
		if (pickupPlayer != null)
		{
			list = new List<PlayerRecord>();
			list.Add(pickupPlayer);
			pickupPlayer.playerLoadInfo.SetEquipWeapon(0, null);
		}
		PlayerLoader[] array = new PlayerLoader[list.Count];
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			PlayerRecord playerRecord = list[i];
			Transform transform = Utility.CreateGameObject("Player:" + i, MonoBehaviourSingleton<StageManager>.I._transform, -1);
			array[i] = transform.gameObject.AddComponent<PlayerLoader>();
			array[i].StartLoad(playerRecord.playerLoadInfo, -1, anim_id, false, false, true, true, false, false, false, false, ShaderGlobal.GetCharacterShaderType(), null, true, -1);
			int num = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss.Length;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && pickupPlayer == null && i < num)
			{
				transform.position = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss[i];
				transform.eulerAngles = new Vector3(0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerRots[i], 0f);
			}
		}
		if (pickupPlayer != null)
		{
			Transform transform2 = array[0].transform;
			transform2.position = pickupPlayerPos;
			transform2.eulerAngles = new Vector3(0f, pickupPlayerRot, 0f);
		}
		playerModels = array;
		return playerModels;
	}

	public void CreatePlayerModelsAsync(Action<PlayerLoader[]> callback)
	{
		StartCoroutine(DoCreatePlayerModelsAsync(callback));
	}

	private IEnumerator DoCreatePlayerModelsAsync(Action<PlayerLoader[]> callback)
	{
		DeletePlayerModels();
		yield return (object)null;
		int anim_type = (!isVictory) ? 91 : (-1);
		List<PlayerRecord> player_records = players.FindAll((PlayerRecord x) => x.isShowModel);
		if (!isVictory)
		{
			pickupPlayer = GetSelfPlayerRecord();
		}
		if (pickupPlayer != null)
		{
			player_records = new List<PlayerRecord>
			{
				pickupPlayer
			};
			pickupPlayer.playerLoadInfo.SetEquipWeapon(0, null);
		}
		PlayerLoader[] player_loaders = new PlayerLoader[player_records.Count];
		int j = 0;
		for (int i = player_records.Count; j < i; j++)
		{
			PlayerRecord player_record = player_records[j];
			Transform player_t = Utility.CreateGameObject("Player:" + j, MonoBehaviourSingleton<StageManager>.I._transform, -1);
			player_loaders[j] = player_t.gameObject.AddComponent<PlayerLoader>();
			player_loaders[j].StartLoad(player_record.playerLoadInfo, -1, anim_type, false, false, true, true, false, false, false, false, ShaderGlobal.GetCharacterShaderType(), null, true, -1);
			while (player_loaders[j].isLoading)
			{
				yield return (object)null;
			}
			int poss_max = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss.Length;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && pickupPlayer == null && j < poss_max)
			{
				player_t.position = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss[j];
				player_t.eulerAngles = new Vector3(0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerRots[j], 0f);
			}
			yield return (object)null;
		}
		if (pickupPlayer != null)
		{
			Transform player_t2 = player_loaders[0].transform;
			player_t2.position = pickupPlayerPos;
			player_t2.eulerAngles = new Vector3(0f, pickupPlayerRot, 0f);
		}
		yield return (object)null;
		playerModels = player_loaders;
		callback?.Invoke(playerModels);
	}

	public void DeletePlayerModels()
	{
		if (playerModels != null)
		{
			int i = 0;
			for (int num = playerModels.Length; i < num; i++)
			{
				if ((UnityEngine.Object)playerModels[i] != (UnityEngine.Object)null)
				{
					UnityEngine.Object.DestroyImmediate(playerModels[i].gameObject);
					playerModels[i] = null;
				}
			}
			playerModels = null;
		}
	}

	public void SetRecordsForExplore(List<ExplorePlayerStatus> playerStatuses, PartyModel.Party party, int bossHp, bool isInGame)
	{
		List<PlayerRecord> list = new List<PlayerRecord>();
		using (List<ExplorePlayerStatus>.Enumerator enumerator = playerStatuses.GetEnumerator())
		{
			ExplorePlayerStatus p;
			while (enumerator.MoveNext())
			{
				p = enumerator.Current;
				if (p.isSelf)
				{
					PlayerRecord selfPlayerRecord = GetSelfPlayerRecord();
					if (selfPlayerRecord != null)
					{
						PlayerRecord playerRecord = p.CreateInGameRecord(selfPlayerRecord.charaInfo);
						if (isInGame && (UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (UnityEngine.Object)null)
						{
							playerRecord.id = MonoBehaviourSingleton<StageObjectManager>.I.self.id;
						}
						list.Add(playerRecord);
					}
				}
				else
				{
					CharaInfo charaInfo = null;
					if (party != null)
					{
						PartyModel.SlotInfo slotInfo = party.slotInfos.Find((PartyModel.SlotInfo x) => x.userInfo != null && x.userInfo.userId == p.userId);
						if (slotInfo != null)
						{
							charaInfo = slotInfo.userInfo;
						}
					}
					PlayerRecord playerRecord2 = p.CreateInGameRecord(charaInfo);
					if (playerRecord2 != null && playerRecord2.charaInfo != null)
					{
						list.Add(playerRecord2);
					}
					PlayerRecord playerRecord3 = players.Find((PlayerRecord x) => x != null && x.charaInfo != null && x.charaInfo.userId == p.userId);
					if (playerRecord3 != null)
					{
						if (isInGame)
						{
							playerRecord2.id = playerRecord3.id;
						}
					}
					else
					{
						playerRecord2.isShowModel = false;
					}
				}
			}
		}
		players.Clear();
		players.AddRange(list);
		players.Sort(delegate(PlayerRecord a, PlayerRecord b)
		{
			int num = b.givenTotalDamage - a.givenTotalDamage;
			if (num != 0)
			{
				return num;
			}
			return a.id - b.id;
		});
		enemies.Clear();
		EnemyRecord enemyRecord = new EnemyRecord();
		if (bossHp <= 0)
		{
			enemyRecord.hp = 10000000;
		}
		else
		{
			enemyRecord.hp = bossHp;
		}
		enemies.Add(enemyRecord);
		if (!isVictory && MonoBehaviourSingleton<CoopManager>.IsValid() && !isInGame)
		{
			pickupPlayer = players.Find((PlayerRecord x) => x.isSelf);
		}
	}

	private PlayerRecord GetSelfPlayerRecord()
	{
		PlayerRecord pickupPlayer = this.pickupPlayer;
		if (pickupPlayer != null)
		{
			pickupPlayer.isSelf = true;
			return pickupPlayer;
		}
		if (players == null)
		{
			return null;
		}
		pickupPlayer = players.Find((PlayerRecord x) => x.isSelf);
		if (pickupPlayer != null)
		{
			return pickupPlayer;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			foreach (PlayerRecord player in players)
			{
				if (player != null && player.charaInfo != null && player.charaInfo.userId == id)
				{
					player.isSelf = true;
					return player;
				}
			}
		}
		if (pickupPlayer == null)
		{
			string text = string.Empty;
			foreach (PlayerRecord player2 in players)
			{
				text = text + player2.charaInfo.name + "(id=" + player2.id + ",userId=" + player2.charaInfo.userId + ")\n";
			}
		}
		return null;
	}

	public static void CheckAndRepairIsSelf(ref List<PlayerRecord> list)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null && list != null)
		{
			int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			int num = 0;
			foreach (PlayerRecord item in list)
			{
				if (item.charaInfo != null)
				{
					bool flag = item.charaInfo.userId == id;
					if (item.isSelf != flag)
					{
						Log.Warning(LOG.INGAME, item.charaInfo.name + "'s \"isSelf\" is Incorrect. ");
						item.isSelf = flag;
					}
					if (item.isSelf)
					{
						num++;
					}
				}
			}
			if (num != 1)
			{
				Log.Error(LOG.INGAME, "Number of self record is incorrect! (num=" + num + ")");
			}
		}
	}
}
