using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObjectManager : MonoBehaviourSingleton<StageObjectManager>
{
	[Serializable]
	public class CreatePlayerInfo
	{
		[Serializable]
		public class ExtentionInfo
		{
			public List<int> weaponIndexList = new List<int>();

			public int npcDataID;

			public int npcLv;

			public int npcLvIndex;

			public override string ToString()
			{
				string str = string.Empty;
				str += "w[";
				if (weaponIndexList != null)
				{
					weaponIndexList.ForEach(delegate(int w)
					{
						str = str + w + ",";
					});
				}
				str += "]";
				str = str + "," + npcDataID;
				str = str + "," + npcLv;
				str = str + "," + npcLvIndex;
				return base.ToString() + str;
			}
		}

		public CharaInfo charaInfo;

		public ExtentionInfo extentionInfo;
	}

	[Serializable]
	public class PlayerTransferInfo
	{
		public int weaponIndex = -1;

		public CharaInfo.EquipItem weaponData;

		public int hp;

		public int healHp;

		public int rescueCount;

		public int autoReviveCount;

		public bool isInitDead;

		public float initRescueTime;

		public float initContinueTime;

		public List<float> useGaugeCounterList;

		public BuffParam.BuffSyncParam buffSyncParam;

		public List<int> abilityCounterAttackNumList;

		public List<int> cleaveComboNumList;

		public float[] spActionGauges;

		public float[] evolveGauges;

		public override string ToString()
		{
			string str = string.Empty;
			str += weaponIndex;
			if (weaponData != null)
			{
				str = str + ",w=" + weaponData;
			}
			str = str + "," + hp;
			str = str + "," + healHp;
			if (useGaugeCounterList != null)
			{
				str += ",g[";
				useGaugeCounterList.ForEach(delegate(float u)
				{
					str = str + u + ",";
				});
				str += "]";
			}
			str = str + "," + buffSyncParam;
			if (spActionGauges != null)
			{
				str += ",sp[";
				for (int i = 0; i < spActionGauges.Length; i++)
				{
					str += spActionGauges[i];
					str += ",";
				}
				str += "]";
			}
			if (evolveGauges != null)
			{
				str += ",ev[";
				for (int j = 0; j < evolveGauges.Length; j++)
				{
					str += evolveGauges[j];
					str += ",";
				}
				str += "]";
			}
			return base.ToString() + str;
		}
	}

	public interface IDetachedNotify
	{
		void OnDetachedObject(StageObject stage_object);
	}

	protected List<IDetachedNotify> notifyInterfaces = new List<IDetachedNotify>();

	public int presentBulletObjIndex;

	public static bool appQuit
	{
		get;
		private set;
	}

	public Self self
	{
		get;
		private set;
	}

	public Enemy boss
	{
		get;
		private set;
	}

	public Enemy fieldEnemyBoss
	{
		get;
		private set;
	}

	public Transform physicsRoot
	{
		get;
		private set;
	}

	public List<StageObject> objectList
	{
		get;
		protected set;
	}

	public List<StageObject> characterList
	{
		get;
		protected set;
	}

	public List<StageObject> playerList
	{
		get;
		protected set;
	}

	public List<StageObject> nonplayerList
	{
		get;
		protected set;
	}

	public List<StageObject> enemyList
	{
		get;
		protected set;
	}

	public List<StageObject> gimmickList
	{
		get;
		protected set;
	}

	public List<StageObject> decoyList
	{
		get;
		protected set;
	}

	public List<StageObject> waveTargetList
	{
		get;
		protected set;
	}

	public List<StageObject> cacheList
	{
		get;
		protected set;
	}

	public List<Enemy> EnemyList
	{
		get;
		protected set;
	}

	public List<Enemy> enemyStokeList
	{
		get;
		protected set;
	}

	public List<IPresentBulletObject> presentBulletObjList
	{
		get;
		protected set;
	}

	public StageObjectManager()
	{
		self = null;
		boss = null;
		physicsRoot = null;
		objectList = new List<StageObject>();
		characterList = new List<StageObject>();
		playerList = new List<StageObject>();
		nonplayerList = new List<StageObject>();
		enemyList = new List<StageObject>();
		gimmickList = new List<StageObject>();
		decoyList = new List<StageObject>();
		waveTargetList = new List<StageObject>();
		cacheList = new List<StageObject>();
		enemyStokeList = new List<Enemy>();
		presentBulletObjList = new List<IPresentBulletObject>();
		EnemyList = new List<Enemy>();
	}

	private void OnApplicationQuit()
	{
		appQuit = true;
	}

	protected override void OnAttachServant(DisableNotifyMonoBehaviour servant)
	{
		base.OnAttachServant(servant);
		if (servant is StageObject)
		{
			StageObject stageObject = servant as StageObject;
			objectList.Add(stageObject);
			if (stageObject is Character)
			{
				characterList.Add(stageObject);
				if (stageObject is Player)
				{
					playerList.Add(stageObject);
					if (stageObject is NonPlayer)
					{
						nonplayerList.Add(stageObject);
					}
					if (stageObject is Self && self == null)
					{
						self = (stageObject as Self);
					}
				}
				else if (stageObject is Enemy)
				{
					Enemy enemy = stageObject as Enemy;
					enemyList.Add(stageObject);
					EnemyList.Add(enemy);
					if (boss == null && enemy.isBoss)
					{
						boss = enemy;
					}
				}
			}
			if (stageObject is GimmickObject)
			{
				gimmickList.Add(stageObject);
			}
			if (stageObject is DecoyBulletObject)
			{
				decoyList.Add(stageObject);
			}
			if (stageObject is FieldWaveTargetObject)
			{
				waveTargetList.Add(stageObject);
			}
		}
	}

	protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
	{
		base.OnDetachServant(servant);
		if (servant is StageObject)
		{
			StageObject stageObject = servant as StageObject;
			objectList.Remove(stageObject);
			if (stageObject is Character)
			{
				characterList.Remove(stageObject);
				if (stageObject is Player)
				{
					playerList.Remove(stageObject);
					if (stageObject is NonPlayer)
					{
						nonplayerList.Remove(stageObject);
					}
					if (self == stageObject as Self)
					{
						self = null;
					}
				}
				else if (stageObject is Enemy)
				{
					Enemy item = stageObject as Enemy;
					enemyList.Remove(stageObject);
					EnemyList.Remove(item);
					if (boss == stageObject as Enemy)
					{
						boss = null;
					}
				}
			}
			if (stageObject is DecoyBulletObject)
			{
				decoyList.Remove(stageObject);
			}
			if (stageObject is FieldWaveTargetObject)
			{
				waveTargetList.Remove(stageObject);
			}
			if (base.notifyServants != null)
			{
				int i = 0;
				for (int count = base.notifyServants.Count; i < count; i++)
				{
					((StageObject)base.notifyServants[i]).OnDetachedObject(stageObject);
				}
			}
			if (notifyInterfaces != null)
			{
				int j = 0;
				for (int count2 = notifyInterfaces.Count; j < count2; j++)
				{
					notifyInterfaces[j].OnDetachedObject(stageObject);
				}
			}
			if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.OnDetachedObject(stageObject);
			}
			if (MonoBehaviourSingleton<SoundManager>.IsValid())
			{
				MonoBehaviourSingleton<SoundManager>.I.OnDetachedObject(stageObject);
			}
		}
	}

	public void AddNotifyInterface(IDetachedNotify notify)
	{
		if (!notifyInterfaces.Contains(notify))
		{
			notifyInterfaces.Add(notify);
		}
	}

	public void RemoveNotifyInterface(IDetachedNotify notify)
	{
		notifyInterfaces.Remove(notify);
	}

	public void SetFieldEnemyBoss(Enemy enemy)
	{
		fieldEnemyBoss = enemy;
	}

	private void Start()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		if (physicsRoot == null)
		{
			GameObject val = new GameObject("PhysicsRoot");
			val.get_transform().set_parent(this.get_transform());
			physicsRoot = val.get_transform();
		}
	}

	private void Update()
	{
		int i = 0;
		for (int count = objectList.Count; i < count; i++)
		{
			StageObject stageObject = objectList[i];
			if (stageObject.isDestroyWaitFlag)
			{
				stageObject.DestroyObject();
			}
			if (count != objectList.Count)
			{
				count = objectList.Count;
				i--;
			}
		}
		int j = 0;
		for (int count2 = cacheList.Count; j < count2; j++)
		{
			StageObject stageObject2 = cacheList[j];
			if (stageObject2.isDestroyWaitFlag)
			{
				stageObject2.DestroyObject();
			}
			else
			{
				if (stageObject2.packetReceiver != null)
				{
					stageObject2.packetReceiver.OnUpdate();
				}
				if (stageObject2.packetSender != null)
				{
					stageObject2.packetSender.OnUpdate();
				}
			}
			if (count2 != cacheList.Count)
			{
				count2 = cacheList.Count;
				j--;
			}
		}
	}

	private void LateUpdate()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy.jostleSpeed;
		}
		if (!(num <= 0f))
		{
			int i = 0;
			for (int count = enemyList.Count; i < count; i++)
			{
				Enemy enemy = enemyList[i] as Enemy;
				if (enemy.isValidPush())
				{
					Vector2 val = enemy._position.ToVector2XZ();
					Enemy enemy2 = null;
					float num2 = 3.40282347E+38f;
					float num3 = 0f;
					Vector2 val2 = default(Vector2);
					val2._002Ector(0f, 0f);
					for (int j = i + 1; j < count; j++)
					{
						Enemy enemy3 = enemyList[j] as Enemy;
						if (enemy3.isValidPush())
						{
							Vector2 val3 = enemy3._position.ToVector2XZ();
							Vector2 val4 = val - val3;
							float sqrMagnitude = val4.get_sqrMagnitude();
							num3 = enemy.bodyRadius + enemy3.bodyRadius;
							if (sqrMagnitude > 0f && sqrMagnitude < num2 && sqrMagnitude < num3 * num3)
							{
								enemy2 = enemy3;
								val2 = val4;
								num2 = sqrMagnitude;
							}
						}
					}
					if (enemy2 != null)
					{
						float num4 = Mathf.Sqrt(num2);
						Vector2 val5 = val2 / num4;
						float num5 = 1f - num4 / num3;
						num5 = num5 * Time.get_deltaTime() * num;
						if (num5 > num3 * 0.5f)
						{
							num5 = num3 * 0.5f;
						}
						Vector2 vector = val5 * num5;
						enemy._position += vector.ToVector3XZ();
						enemy2._position -= vector.ToVector3XZ();
					}
				}
			}
		}
	}

	public void Init(InGameManager.IntervalTransferInfo transfer_info = null)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		Self self = null;
		if (transfer_info != null)
		{
			int i = 0;
			for (int count = transfer_info.playerInfoList.Count; i < count; i++)
			{
				InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = transfer_info.playerInfoList[i];
				Player player = null;
				CoopClient coopClient = null;
				if (MonoBehaviourSingleton<CoopManager>.IsValid() && !playerInfo.isSelf && playerInfo.coopClientId != 0)
				{
					coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(playerInfo.coopClientId);
				}
				if (playerInfo.isSelf)
				{
					player = CreatePlayer(0, playerInfo.createInfo, true, Vector3.get_zero(), 0f, playerInfo.transferInfo, null);
					if (player == null)
					{
						continue;
					}
					self = (player as Self);
					self.taskChecker = playerInfo.taskChecker;
				}
				else
				{
					bool flag = false;
					if (MonoBehaviourSingleton<CoopManager>.IsValid())
					{
						if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay)
						{
							flag = true;
						}
						else if (coopClient != null && coopClient.isLeave && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
						{
							flag = true;
						}
					}
					if (flag || playerInfo.coopMode == StageObject.COOP_MODE_TYPE.NONE || playerInfo.coopMode == StageObject.COOP_MODE_TYPE.ORIGINAL)
					{
						player = CreatePlayer(playerInfo.id, playerInfo.createInfo, false, Vector3.get_zero(), 0f, playerInfo.transferInfo, null);
						if (player == null)
						{
							continue;
						}
						player.SetAppearPosGuest(Vector3.get_zero());
						if (coopClient != null && playerInfo.isCoopPlayer)
						{
							coopClient.SetPlayerID(player.id);
						}
					}
					else
					{
						PlayerLoader.OnCompleteLoad callback = delegate(object o)
						{
							//IL_0008: Unknown result type (might be due to invalid IL or missing references)
							Player player2 = o as Player;
							player2.get_gameObject().SetActive(false);
							MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject(player2);
						};
						player = CreatePlayer(playerInfo.id, playerInfo.createInfo, false, Vector3.get_zero(), 0f, playerInfo.transferInfo, callback);
						if (player == null)
						{
							continue;
						}
						if (coopClient != null)
						{
							coopClient.SetCachePlayer(player.id, playerInfo.isCoopPlayer);
						}
					}
					if (flag || playerInfo.isNpcController)
					{
						if (!(player.controller is NpcController))
						{
							player.AddController<NpcController>();
						}
						if (QuestManager.IsValidInGameDefenseBattle())
						{
							player.DestroyObject();
						}
					}
				}
				if (player.controller != null)
				{
					player.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
				}
			}
		}
		if (self == null)
		{
			self = CreateSelf(0, Vector3.get_zero(), 0f);
			if (self != null && self.controller != null)
			{
				self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
			}
		}
		if (!(self == null))
		{
			if (QuestManager.IsValidInGame() && !MonoBehaviourSingleton<InGameManager>.I.IsNeedInitBoss())
			{
				self.SetAppearPosGuest(Vector3.get_zero());
			}
			if (FieldManager.IsValidInGameNoQuest() || MonoBehaviourSingleton<QuestManager>.I.IsExplore() || MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch())
			{
				self.SetAppearPosField();
			}
		}
	}

	public void InitForArena(InGameManager.IntervalTransferInfo transferInfo = null)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Self self = null;
		if (transferInfo != null)
		{
			InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = transferInfo.playerInfoList[0];
			self = (CreatePlayer(0, playerInfo.createInfo, true, Vector3.get_zero(), 0f, playerInfo.transferInfo, null) as Self);
			self.taskChecker = playerInfo.taskChecker;
			if (self.controller != null)
			{
				self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
			}
		}
		if (self == null)
		{
			self = CreateSelf(0, Vector3.get_zero(), 0f);
			if (self != null && self.controller != null)
			{
				self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
			}
		}
		if (self == null)
		{
			return;
		}
	}

	public Self CreateSelf(int id, Vector3 pos, float dir)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		CreatePlayerInfo createPlayerInfo = null;
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			createPlayerInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo();
		}
		if (createPlayerInfo == null)
		{
			return null;
		}
		return CreatePlayer(id, createPlayerInfo, true, pos, dir, null, null) as Self;
	}

	public Player CreateNonPlayer(int id, PlayerLoader.OnCompleteLoad callback = null)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		CreatePlayerInfo.ExtentionInfo extention_info = null;
		Player player = CreateNonPlayer(id, extention_info, Vector3.get_zero(), 0f, null, callback);
		if (player == null)
		{
			return null;
		}
		Vector3 appearPosGuest = Vector3.get_zero();
		if (boss != null)
		{
			appearPosGuest = boss._transform.get_position();
		}
		player.SetAppearPosGuest(appearPosGuest);
		return player;
	}

	public Player CreateNonPlayer(int id, CreatePlayerInfo.ExtentionInfo extention_info, Vector3 pos, float dir, PlayerTransferInfo transfer_info = null, PlayerLoader.OnCompleteLoad callback = null)
	{
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		CreatePlayerInfo createPlayerInfo = new CreatePlayerInfo();
		createPlayerInfo.charaInfo = new CharaInfo();
		bool flag = QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType() != QuestManager.VorgonQuetType.NONE;
		bool flag2 = false;
		if (extention_info != null)
		{
			createPlayerInfo.extentionInfo = extention_info;
		}
		else
		{
			createPlayerInfo.extentionInfo = new CreatePlayerInfo.ExtentionInfo();
			flag2 = true;
		}
		NPCTable.NPCData nPCData = null;
		if (flag2)
		{
			List<int> list = new List<int>();
			int i = 0;
			for (int count = nonplayerList.Count; i < count; i++)
			{
				NonPlayer nonPlayer = nonplayerList[i] as NonPlayer;
				if (nonPlayer != null)
				{
					list.Add(nonPlayer.npcId);
				}
			}
			nPCData = Singleton<NPCTable>.I.GetNPCDataRandom(NPCTable.NPC_TYPE.FIGURE, list);
			if (nPCData != null)
			{
				createPlayerInfo.extentionInfo.npcDataID = nPCData.id;
			}
		}
		else
		{
			nPCData = Singleton<NPCTable>.I.GetNPCData(createPlayerInfo.extentionInfo.npcDataID);
		}
		if (flag)
		{
			int npc_id = VorgonPreEventController.NPC_ID_LIST[id % 3];
			nPCData = Singleton<NPCTable>.I.GetNPCData(npc_id);
		}
		if (nPCData == null)
		{
			return null;
		}
		nPCData.CopyCharaInfo(createPlayerInfo.charaInfo);
		NpcLevelTable.NpcLevelData npcLevelData = null;
		if (flag2)
		{
			int lv = 1;
			if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID() > 0)
			{
				lv = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
			}
			if (flag)
			{
				lv = 80;
			}
			npcLevelData = Singleton<NpcLevelTable>.I.GetNpcLevelRandom((uint)lv);
			if (npcLevelData != null)
			{
				createPlayerInfo.extentionInfo.npcLv = (int)npcLevelData.lv;
				createPlayerInfo.extentionInfo.npcLvIndex = npcLevelData.lvIndex;
			}
		}
		else
		{
			npcLevelData = Singleton<NpcLevelTable>.I.GetNpcLevel((uint)createPlayerInfo.extentionInfo.npcLv, createPlayerInfo.extentionInfo.npcLvIndex);
		}
		if (npcLevelData == null)
		{
			return null;
		}
		npcLevelData.CopyHomeCharaInfo(createPlayerInfo.charaInfo, (!flag2) ? null : createPlayerInfo.extentionInfo);
		if (flag)
		{
			for (int j = 0; j < createPlayerInfo.charaInfo.equipSet.Count; j++)
			{
				createPlayerInfo.charaInfo.equipSet[j].eId = VorgonPreEventController.NPC_WEAPON_ID_LIST[id % 3];
			}
		}
		return CreatePlayer(id, createPlayerInfo, false, pos, dir, transfer_info, callback);
	}

	public Player CreateGuest(int id, CharaInfo charaInfo, PlayerLoader.OnCompleteLoad callback = null)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		CreatePlayerInfo createPlayerInfo = new CreatePlayerInfo();
		createPlayerInfo.charaInfo = charaInfo;
		Player player = CreatePlayer(id, createPlayerInfo, false, Vector3.get_zero(), 0f, null, callback);
		Vector3 appearPosGuest = Vector3.get_zero();
		if (boss != null)
		{
			appearPosGuest = boss._transform.get_position();
		}
		player.SetAppearPosGuest(appearPosGuest);
		return player;
	}

	public Player CreatePlayer(int id, CreatePlayerInfo create_info, bool self, Vector3 pos, float dir, PlayerTransferInfo transfer_info = null, PlayerLoader.OnCompleteLoad callback = null)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		if (create_info.charaInfo == null)
		{
			Log.Error("StageObjectManager.CreatePlayer() charaInfo is NULL");
			return null;
		}
		GameObject val = new GameObject();
		val.set_name("Player:" + id);
		val.get_transform().set_parent(base._transform);
		int num = 0;
		if (create_info.extentionInfo != null)
		{
			num = create_info.extentionInfo.npcDataID;
		}
		bool flag = num > 0;
		Player player;
		if (self)
		{
			player = val.AddComponent<Self>();
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.target = player._transform;
			}
		}
		else if (flag)
		{
			player = val.AddComponent<NonPlayer>();
			NonPlayer nonPlayer = player as NonPlayer;
			nonPlayer.npcId = num;
			if (Singleton<NPCTable>.IsValid())
			{
				nonPlayer.npcTableData = Singleton<NPCTable>.I.GetNPCData(num);
			}
			nonPlayer.lv = create_info.extentionInfo.npcLv;
			nonPlayer.lv_index = create_info.extentionInfo.npcLvIndex;
		}
		else
		{
			player = val.AddComponent<Player>();
		}
		player.id = id;
		player._transform.set_position(pos);
		player._transform.set_eulerAngles(new Vector3(0f, dir, 0f));
		player.SetState(create_info, transfer_info);
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			int userId = create_info.charaInfo.userId;
			InGameRecorder.PlayerRecord playerRecord = player.record = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayer(id, userId);
			playerRecord.isSelf = self;
			playerRecord.isNPC = flag;
			playerRecord.charaInfo = create_info.charaInfo;
			playerRecord.beforeLevel = create_info.charaInfo.level;
		}
		if (self)
		{
			player.AddController<SelfController>();
		}
		else if (flag)
		{
			player.AddController<NpcController>();
		}
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			EffectPlayProcessor component = MonoBehaviourSingleton<InGameSettingsManager>.I.get_gameObject().GetComponent<EffectPlayProcessor>();
			if (component != null && component.effectSettings != null)
			{
				player.effectPlayProcessor = val.AddComponent<EffectPlayProcessor>();
				player.effectPlayProcessor.effectSettings = component.effectSettings;
			}
		}
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		if (player.weaponData != null)
		{
			playerLoadInfo.SetEquipWeapon(create_info.charaInfo.sex, (uint)player.weaponData.eId);
		}
		playerLoadInfo.Apply(create_info.charaInfo, false, true, true, true);
		player.Load(playerLoadInfo, callback);
		player.OnSetPlayerStatus(create_info.charaInfo.level, create_info.charaInfo.atk, create_info.charaInfo.def, create_info.charaInfo.hp, false, transfer_info);
		player.StartFieldBuff(MonoBehaviourSingleton<FieldManager>.IsValid() ? MonoBehaviourSingleton<FieldManager>.I.currentFieldBuffId : 0);
		return player;
	}

	public Enemy CreateEnemyWithAI(int id, Vector3 pos, float dir, int enemyId, int enemyLv, bool isBoss, EnemyLoader.OnCompleteLoad callback = null)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		Enemy enemy = null;
		for (int i = 0; i < enemyStokeList.Count; i++)
		{
			if (enemyStokeList[i].enemyID == enemyId)
			{
				if (QuestManager.IsValidInGameWaveMatch())
				{
					if (enemyStokeList[i].isWaveMatchBoss != isBoss)
					{
						continue;
					}
				}
				else if (enemyStokeList[i].isBoss != isBoss)
				{
					continue;
				}
				enemy = enemyStokeList[i];
				enemy.ClearDead();
				enemy.get_gameObject().set_name("Enemy:" + id);
				enemyStokeList.Remove(enemy);
				break;
			}
		}
		if (enemy != null)
		{
			enemy.id = id;
			enemy._transform.set_parent(base._transform);
			enemy._transform.set_position(pos);
			enemy._transform.set_eulerAngles(new Vector3(0f, dir, 0f));
			if (QuestManager.IsValidInGame())
			{
				enemy.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
			}
			callback = CreateWrappedEnemyLoadCompletedDelegate(callback);
			if (callback != null)
			{
				this.StartCoroutine(_OnCallback(enemy, callback));
			}
			else
			{
				enemy.get_gameObject().SetActive(true);
			}
			return enemy;
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)enemyId);
		uint growId = enemyData.growId;
		GrowEnemyTable.GrowEnemyData growEnemyData = Singleton<GrowEnemyTable>.I.GetGrowEnemyData(growId, enemyLv);
		GameObject val = new GameObject();
		val.set_name("Enemy:" + id);
		val.SetActive(false);
		enemy = val.AddComponent<Enemy>();
		enemy.id = id;
		enemy.enemyID = (int)enemyData.id;
		enemy.isBoss = (!QuestManager.IsValidInGameWaveMatch() && isBoss);
		enemy.isWaveMatchBoss = isBoss;
		enemy.enemyTableData = enemyData;
		enemy.growTableData = growEnemyData;
		enemy.charaName = enemyData.name;
		enemy.enemyLevel = ((growEnemyData == null) ? ((int)enemyData.level) : ((int)growEnemyData.level));
		enemy.moveStopRange *= enemyData.modelScale;
		enemy.AddController<EnemyController>();
		if (QuestManager.IsValidInGame())
		{
			enemy.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
		}
		enemy._transform.set_parent(base._transform);
		enemy._transform.set_position(pos);
		enemy._transform.set_eulerAngles(new Vector3(0f, dir, 0f));
		callback = CreateWrappedEnemyLoadCompletedDelegate(callback);
		enemy.loader.StartLoad(enemyData.modelId, enemyData.animId, enemyData.modelScale, enemyData.baseEffectName, enemyData.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), -1, null, false, false, callback);
		return enemy;
	}

	public Enemy CreateEnemy(int id, Vector3 pos, float dir, int enemy_id, int enemy_lv, bool is_boss, bool set_ai = true, bool willStock = false, EnemyLoader.OnCompleteLoad callback = null)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)enemy_id);
		uint growId = enemyData.growId;
		GrowEnemyTable.GrowEnemyData growEnemyData = Singleton<GrowEnemyTable>.I.GetGrowEnemyData(growId, enemy_lv);
		bool flag = false;
		GameObject val = null;
		Enemy enemy = null;
		int i = 0;
		for (int count = enemyStokeList.Count; i < count; i++)
		{
			if (enemyStokeList[i].enemyID == enemy_id)
			{
				if (QuestManager.IsValidInGameWaveMatch())
				{
					if (enemyStokeList[i].isWaveMatchBoss != is_boss)
					{
						continue;
					}
				}
				else if (enemyStokeList[i].isBoss != is_boss)
				{
					continue;
				}
				enemy = enemyStokeList[i];
				enemy.ClearDead();
				val = enemy.get_gameObject();
				val.set_name("Enemy:" + id);
				enemyStokeList.Remove(enemy);
				flag = true;
				break;
			}
		}
		if (enemy == null)
		{
			val = new GameObject();
			val.set_name("Enemy:" + id);
			val.SetActive(false);
			enemy = val.AddComponent<Enemy>();
			enemy.enemyID = (int)enemyData.id;
			enemy.isBoss = (!QuestManager.IsValidInGameWaveMatch() && is_boss);
			enemy.isWaveMatchBoss = is_boss;
			enemy.enemyTableData = enemyData;
			enemy.growTableData = growEnemyData;
			enemy.charaName = enemyData.name;
			enemy.enemyLevel = ((growEnemyData == null) ? ((int)enemyData.level) : ((int)growEnemyData.level));
			enemy.moveStopRange *= enemyData.modelScale;
			if (set_ai)
			{
				enemy.AddController<EnemyController>();
			}
		}
		enemy.id = id;
		if (!flag)
		{
			val.SetActive(true);
		}
		if (QuestManager.IsValidInGame())
		{
			enemy.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
		}
		enemy._transform.set_parent(base._transform);
		enemy._transform.set_position(pos);
		enemy._transform.set_eulerAngles(new Vector3(0f, dir, 0f));
		callback = CreateWrappedEnemyLoadCompletedDelegate(callback);
		if (flag)
		{
			if (callback != null)
			{
				this.StartCoroutine(_OnCallback(enemy, callback));
			}
			else
			{
				val.SetActive(true);
			}
		}
		else
		{
			enemy.loader.StartLoad(enemyData.modelId, enemyData.animId, enemyData.modelScale, enemyData.baseEffectName, enemyData.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), -1, null, false, willStock, callback);
		}
		return enemy;
	}

	private EnemyLoader.OnCompleteLoad CreateWrappedEnemyLoadCompletedDelegate(EnemyLoader.OnCompleteLoad callback)
	{
		return delegate(Enemy e)
		{
			if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && e.isBoss)
			{
				ExploreBossStatus exploreBossStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossStatus();
				if (exploreBossStatus != null)
				{
					e.ApplyExploreBossStatus(exploreBossStatus);
				}
				else
				{
					MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(e);
				}
			}
			if (callback != null)
			{
				callback(e);
			}
		};
	}

	protected IEnumerator _OnCallback(Enemy enemy, EnemyLoader.OnCompleteLoad callback)
	{
		yield return (object)null;
		if (enemy.isStoke)
		{
			enemy.get_gameObject().SetActive(true);
			enemy.isStoke = false;
		}
		callback(enemy);
	}

	private StageObject Find(List<StageObject> list, int id)
	{
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			if (list[i].id == id)
			{
				return list[i];
			}
		}
		return null;
	}

	public StageObject FindObject(int id)
	{
		return Find(objectList, id);
	}

	public StageObject FindCharacter(int id)
	{
		return Find(characterList, id);
	}

	public StageObject FindPlayer(int id)
	{
		return Find(playerList, id);
	}

	public StageObject FindNonPlayer(int id)
	{
		return Find(nonplayerList, id);
	}

	public StageObject FindEnemy(int id)
	{
		return Find(enemyList, id);
	}

	public StageObject FindGimmick(int id)
	{
		return Find(gimmickList, id);
	}

	public StageObject FindDecoy(int id)
	{
		return Find(decoyList, id);
	}

	public StageObject FindWaveTarget(int id)
	{
		return Find(waveTargetList, id);
	}

	public StageObject FindCache(int id)
	{
		return Find(cacheList, id);
	}

	private StageObject Find(List<StageObject> list, Vector3 pos, float range)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		StageObject result = null;
		float num = range;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			StageObject stageObject = list[i];
			if (stageObject.get_gameObject().get_activeSelf())
			{
				Vector3 val = stageObject._transform.get_position() - pos;
				float magnitude = val.get_magnitude();
				if (magnitude < num)
				{
					num = magnitude;
					result = stageObject;
				}
			}
		}
		return result;
	}

	public StageObject FindObject(Vector3 pos, float range)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return Find(objectList, pos, range);
	}

	public StageObject FindCharacter(Vector3 pos, float range)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return Find(characterList, pos, range);
	}

	public StageObject FindPlayer(Vector3 pos, float range)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return Find(playerList, pos, range);
	}

	public StageObject FindNonPlayer(Vector3 pos, float range)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return Find(nonplayerList, pos, range);
	}

	public StageObject FindEnemy(Vector3 pos, float range)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return Find(enemyList, pos, range);
	}

	public void AddCacheObject(StageObject obj)
	{
		cacheList.Add(obj);
	}

	public void RemoveCacheObject(StageObject obj)
	{
		cacheList.Remove(obj);
	}

	public void ClearCacheObject()
	{
		cacheList.Clear();
	}

	public bool IsFieldEnemyBoss(int sid)
	{
		if (fieldEnemyBoss == null)
		{
			return false;
		}
		if (sid != fieldEnemyBoss.id)
		{
			return false;
		}
		return true;
	}

	public bool ExistsEnemyValiedHealAttack()
	{
		for (int i = 0; i < enemyList.Count; i++)
		{
			Enemy enemy = enemyList[i] as Enemy;
			if (enemy != null && enemy.healDamageRate > 0f)
			{
				return true;
			}
		}
		return false;
	}

	public void RemovePresentBulletObject(int presentBulletId)
	{
		if (presentBulletObjList != null && presentBulletObjList.Count > 0)
		{
			presentBulletObjList.RemoveAll((IPresentBulletObject item) => item.GetPresentBulletId() == presentBulletId);
		}
	}
}
