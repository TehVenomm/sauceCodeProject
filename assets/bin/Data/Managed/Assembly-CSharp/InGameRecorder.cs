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

		public int recoveredHp;
	}

	public InGameProgress.PROGRESS_END_TYPE progressEndType;

	public string rushRemainTimeToString = "";

	public float arenaElapsedTime;

	public string arenaRemainTimeToString = "";

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

	public PlayerRecord GetPlayer(int id, int? user_id = null)
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
			if (coopClient != null)
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
			int? user_id = (sync_host.charaInfo != null) ? new int?(sync_host.charaInfo.userId) : null;
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
		PlayerRecord player = GetPlayer(id);
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

	public void SetEnemyRecoveredHP(int id, int recoveredHp)
	{
		EnemyRecord enemyRecord = enemies.Find((EnemyRecord o) => o.id == id);
		if (enemyRecord == null)
		{
			enemyRecord = new EnemyRecord();
			enemyRecord.id = id;
			enemies.Add(enemyRecord);
		}
		enemyRecord.recoveredHp = recoveredHp;
	}

	public void RecordEnemyRecoveredHP(int id, int recoveredHp)
	{
		EnemyRecord enemyRecord = enemies.Find((EnemyRecord o) => o.id == id);
		if (enemyRecord == null)
		{
			enemyRecord = new EnemyRecord();
			enemyRecord.id = id;
			enemies.Add(enemyRecord);
		}
		enemyRecord.recoveredHp += recoveredHp;
	}

	public void RecordGivenDamage(int player_id, int damage)
	{
		PlayerRecord player = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayer(player_id);
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

	public int GetTotalEnemyHpContainsHealed()
	{
		int total = 0;
		enemies.ForEach(delegate(EnemyRecord o)
		{
			total += o.hp;
			total += o.recoveredHp;
		});
		return total;
	}

	public int GetEnemyRecoveredHpById(int id)
	{
		return enemies.Find((EnemyRecord o) => o.id == id)?.recoveredHp ?? 0;
	}

	public void OnInGameEnd(bool is_victory)
	{
		isVictory = is_victory;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<PlayerRecord> list = new List<PlayerRecord>();
			players.ForEach(delegate(PlayerRecord o)
			{
				if (!(MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(o.id) == null))
				{
					list.Add(o);
				}
			});
			players = list;
		}
		players.Sort((PlayerRecord a, PlayerRecord b) => (a.givenTotalDamage != b.givenTotalDamage) ? ((a.givenTotalDamage <= b.givenTotalDamage) ? 1 : (-1)) : (a.id - b.id));
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
			if (playerRecord != null && stageObject != null)
			{
				Vector3 position = stageObject._transform.position;
				position.y = 0f;
				pickupPlayerPos = position;
				pickupPlayerRot = stageObject._transform.eulerAngles.y;
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
		int anim_id = isVictory ? (-1) : 91;
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
			Transform transform = Utility.CreateGameObject("Player:" + i, MonoBehaviourSingleton<StageManager>.I._transform);
			array[i] = transform.gameObject.AddComponent<PlayerLoader>();
			array[i].StartLoad(playerRecord.playerLoadInfo, -1, anim_id, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: false, need_dev_frame_instantiate: false, ShaderGlobal.GetCharacterShaderType(), null);
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
		yield return null;
		int anim_type = isVictory ? (-1) : 91;
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
		int i = player_records.Count;
		while (j < i)
		{
			PlayerRecord playerRecord = player_records[j];
			Transform player_t = Utility.CreateGameObject("Player:" + j, MonoBehaviourSingleton<StageManager>.I._transform);
			player_loaders[j] = player_t.gameObject.AddComponent<PlayerLoader>();
			player_loaders[j].StartLoad(playerRecord.playerLoadInfo, -1, anim_type, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: false, need_dev_frame_instantiate: false, ShaderGlobal.GetCharacterShaderType(), null);
			while (player_loaders[j].isLoading)
			{
				yield return null;
			}
			int num = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss.Length;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && pickupPlayer == null && j < num)
			{
				player_t.position = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss[j];
				player_t.eulerAngles = new Vector3(0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerRots[j], 0f);
			}
			yield return null;
			int num2 = j + 1;
			j = num2;
		}
		if (pickupPlayer != null)
		{
			Transform transform = player_loaders[0].transform;
			transform.position = pickupPlayerPos;
			transform.eulerAngles = new Vector3(0f, pickupPlayerRot, 0f);
		}
		yield return null;
		playerModels = player_loaders;
		callback?.Invoke(playerModels);
	}

	public void DeletePlayerModels()
	{
		if (playerModels == null)
		{
			return;
		}
		int i = 0;
		for (int num = playerModels.Length; i < num; i++)
		{
			if (playerModels[i] != null)
			{
				UnityEngine.Object.DestroyImmediate(playerModels[i].gameObject);
				playerModels[i] = null;
			}
		}
		playerModels = null;
	}

	public void SetRecordsForExplore(List<ExplorePlayerStatus> playerStatuses, PartyModel.Party party, ExploreBossStatus bossStatus, bool isInGame)
	{
		List<PlayerRecord> list = new List<PlayerRecord>();
		foreach (ExplorePlayerStatus p in playerStatuses)
		{
			if (p.isSelf)
			{
				PlayerRecord selfPlayerRecord = GetSelfPlayerRecord();
				if (selfPlayerRecord != null)
				{
					PlayerRecord playerRecord = p.CreateInGameRecord(selfPlayerRecord.charaInfo);
					if (isInGame && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
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
		players.Clear();
		players.AddRange(list);
		players.Sort(delegate(PlayerRecord a, PlayerRecord b)
		{
			int num = b.givenTotalDamage - a.givenTotalDamage;
			return (num != 0) ? num : (a.id - b.id);
		});
		enemies.Clear();
		EnemyRecord enemyRecord = new EnemyRecord();
		if (bossStatus == null || (int)bossStatus.hpMax <= 0)
		{
			enemyRecord.hp = 10000000;
		}
		else
		{
			enemyRecord.id = bossStatus.coopEnemyId;
			enemyRecord.hp = bossStatus.hpMax;
			enemyRecord.recoveredHp = bossStatus.recoveredHP;
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
			string text = "";
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
