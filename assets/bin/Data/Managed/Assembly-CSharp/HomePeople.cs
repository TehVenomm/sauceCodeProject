using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePeople
{
	private const int charaMax = 14;

	private const float GROUP_RADIUS = 1f;

	private Transform peopleRoot;

	private WayPoint centerPoint;

	private Vector3 groupCenterPos;

	private List<WayPoint> leafPoints = new List<WayPoint>();

	private OutGameCharacterCreater creater;

	public bool isInitialized
	{
		get;
		private set;
	}

	public bool isPeopleInitialized
	{
		get;
		private set;
	}

	public HomeSelfCharacter selfChara
	{
		get;
		private set;
	}

	public List<HomeCharacterBase> charas
	{
		get;
		private set;
	}

	public List<LoungePlayer> loungePlayers
	{
		get;
		private set;
	}

	public HomePeople()
		: this()
	{
	}

	public void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback)
	{
		if (!(selfChara != null))
		{
			selfChara = (creater.CreateSelf(this, peopleRoot, notice_callback) as HomeSelfCharacter);
			charas.Add(selfChara);
		}
	}

	public bool CreateLoungePlayer(LoungeModel.SlotInfo slotInfo, bool useMovingEntry, bool checkEquip)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (slotInfo == null)
		{
			return false;
		}
		if (slotInfo.userInfo == null)
		{
			return false;
		}
		CharaInfo userInfo = slotInfo.userInfo;
		LoungePlayer loungePlayer = GetLoungePlayer(userInfo.userId);
		if (loungePlayer != null)
		{
			if (checkEquip)
			{
				this.StartCoroutine(CheckEquipChanged(loungePlayer.LoungeCharaInfo, userInfo, useMovingEntry));
			}
			return false;
		}
		LoungePlayer item = creater.CreateLoungePlayer(this, peopleRoot, userInfo, useMovingEntry);
		charas.Add(item);
		loungePlayers.Add(item);
		return true;
	}

	public bool DestroyLoungePlayer(int id)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer loungePlayer = GetLoungePlayer(id);
		if (loungePlayer == null || loungePlayer.get_gameObject() == null)
		{
			return false;
		}
		OnDestroyHomeCharacter(loungePlayer);
		OnDestroyLoungePlayer(loungePlayer);
		Object.DestroyObject(loungePlayer.get_gameObject());
		return true;
	}

	public void SetInitialPositionLoungePlayer(int id, Vector3 initialPos, LOUNGE_ACTION_TYPE type)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSetInitialPositionLoungePlayer(id, initialPos, type));
	}

	public void MoveLoungePlayer(int id, Vector3 targetPos)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer loungePlayer = GetLoungePlayer(id);
		if (!(loungePlayer == null) && !(loungePlayer.get_gameObject() == null))
		{
			loungePlayer.SetMoveTargetPosition(targetPos);
		}
	}

	public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			return GetLoungeMoveNPCTargetPosition(chara, wayPoint);
		}
		float num = 1.5f;
		num *= num;
		bool flag = groupCenterPos.y != -1f;
		Vector2 val = groupCenterPos.ToVector2XZ();
		for (int i = 0; i < 8; i++)
		{
			Vector2 val2 = wayPoint.GetPosInCollider().ToVector2XZ();
			if (flag)
			{
				Vector2 val3 = val2 - val;
				if (val3.get_sqrMagnitude() < num)
				{
					continue;
				}
			}
			int j = 0;
			int count;
			for (count = charas.Count; j < count; j++)
			{
				HomeCharacterBase homeCharacterBase = charas[j];
				if (homeCharacterBase != chara && !homeCharacterBase.isLoading && homeCharacterBase.get_isActiveAndEnabled())
				{
					Vector2 val4 = val2 - homeCharacterBase.moveTargetPos.ToVector2XZ();
					if (val4.get_sqrMagnitude() < 0.25f)
					{
						break;
					}
				}
			}
			if (j == count)
			{
				return val2.ToVector3XZ();
			}
		}
		return wayPoint.GetPosInCollider();
	}

	public HomeNPCCharacter GetHomeNPCCharacter(int npcID)
	{
		if (charas == null)
		{
			return null;
		}
		HomeNPCCharacter result = null;
		for (int i = 0; i < charas.Count; i++)
		{
			HomeNPCCharacter homeNPCCharacter = charas[i] as HomeNPCCharacter;
			if (!(homeNPCCharacter == null) && npcID == homeNPCCharacter.npcInfo.npcID)
			{
				result = homeNPCCharacter;
				break;
			}
		}
		return result;
	}

	public void OnDestroyHomeCharacter(HomeCharacterBase chara)
	{
		charas.Remove(chara);
	}

	public void OnDestroyLoungePlayer(LoungePlayer chara)
	{
		loungePlayers.Remove(chara);
	}

	private IEnumerator Start()
	{
		creater = new OutGameCharacterCreater();
		charas = new List<HomeCharacterBase>(16);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			loungePlayers = new List<LoungePlayer>(8);
		}
		peopleRoot = Utility.CreateGameObject("PeopleRoot", this.get_transform(), -1);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			yield return (object)this.StartCoroutine(SetupLounge());
		}
		else if (MonoBehaviourSingleton<GuildStageManager>.IsValid())
		{
			yield return (object)this.StartCoroutine(SetupGuild());
		}
		else
		{
			yield return (object)this.StartCoroutine(LoadPeopleWayPoint());
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
			{
				yield return (object)this.StartCoroutine(GetHomePlayerCharacterList());
			}
			isInitialized = true;
			yield return (object)this.StartCoroutine(LocateHomePeople());
			isPeopleInitialized = true;
			yield return (object)this.StartCoroutine(WatchHomePeople());
		}
	}

	private IEnumerator GetHomePlayerCharacterList()
	{
		if (MonoBehaviourSingleton<FriendManager>.I.IsHomeCharaCached)
		{
			MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList(null);
			yield return (object)null;
		}
		else
		{
			bool wait = true;
			MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList(delegate
			{
				((_003CGetHomePlayerCharacterList_003Ec__IteratorBD)/*Error near IL_0063: stateMachine*/)._003Cwait_003E__0 = false;
			});
			while (wait)
			{
				yield return (object)null;
			}
		}
	}

	private IEnumerator DoSetInitialPositionLoungePlayer(int id, Vector3 pos, LOUNGE_ACTION_TYPE type)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer target = GetLoungePlayer(id);
		while (target == null || target.get_gameObject() == null)
		{
			target = GetLoungePlayer(id);
			yield return (object)null;
		}
		while (target.isLoading)
		{
			yield return (object)null;
		}
		target.SetInitialPosition(pos, type);
	}

	private IEnumerator LoadPeopleWayPoint()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		string wayPointName = "PeopleWayPoints";
		LoadObject lo_way_points = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			wayPointName
		}, false);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		Transform wayPoints = ResourceUtility.Realizes(lo_way_points.loadedObject, this.get_transform(), -1);
		Utility.ForEach(wayPoints, delegate(Transform o)
		{
			if (o.get_name().StartsWith("LEAF"))
			{
				((_003CLoadPeopleWayPoint_003Ec__IteratorBF)/*Error near IL_00ba: stateMachine*/)._003C_003Ef__this.leafPoints.Add(o.GetComponent<WayPoint>());
			}
			else if (o.get_name() == "CENTER")
			{
				((_003CLoadPeopleWayPoint_003Ec__IteratorBF)/*Error near IL_00ba: stateMachine*/)._003C_003Ef__this.centerPoint = o.GetComponent<WayPoint>();
			}
			return false;
		});
	}

	private IEnumerator LoadLoungeWayPoint(string wayPointName)
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadWayPoints = loadQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			wayPointName
		}, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		Transform wayPoints = ResourceUtility.Realizes(loadWayPoints.loadedObject, this.get_transform(), -1);
		Utility.ForEach(wayPoints, delegate(Transform o)
		{
			if (o.get_name() == "CENTER")
			{
				((_003CLoadLoungeWayPoint_003Ec__IteratorC0)/*Error near IL_00af: stateMachine*/)._003C_003Ef__this.centerPoint = o.GetComponent<WayPoint>();
			}
			return false;
		});
	}

	private IEnumerator WatchHomePeople()
	{
		while (true)
		{
			yield return (object)null;
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) && charas.Count < 14)
			{
				yield return (object)new WaitForSeconds(Random.Range(3f, 6f));
				if (AppMain.isReset)
				{
					break;
				}
				WayPoint way_point = leafPoints[Random.Range(0, leafPoints.Count)];
				CreateChara(way_point);
			}
		}
	}

	private IEnumerator LocateHomePeople()
	{
		MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.SetupNPCSituations();
		OutGameSettingsManager.HomeScene.NPC[] npcs = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.npcs;
		foreach (OutGameSettingsManager.HomeScene.NPC npc in npcs)
		{
			HomeCharacterBase chara = creater.CreateNPC(this, peopleRoot, npc);
			if (chara != null)
			{
				charas.Add(chara);
			}
		}
		groupCenterPos = new Vector3(0f, -1f, 0f);
		if (Random.Range(0, 8) == 0)
		{
			int num2 = Random.Range(2, 5);
			List<HomeCharacterBase> list = new List<HomeCharacterBase>();
			for (int k = 0; k < num2; k++)
			{
				list.Add(CreateChara(centerPoint));
			}
			groupCenterPos = list[0]._transform.get_localPosition();
			float angle_step = 360f / (float)num2;
			float angle = Random.get_value() * 360f;
			Vector3 DIST = new Vector3(0f, 0f, 1f);
			int j = 0;
			while (j < num2)
			{
				Transform t = list[j]._transform;
				t.set_localPosition(Quaternion.AngleAxis(angle, Vector3.get_up()) * DIST + groupCenterPos);
				t.LookAt(groupCenterPos);
				list[j].StopDiscussion();
				j++;
				angle += angle_step;
			}
		}
		else
		{
			int num = Random.Range(1, 5);
			if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
			{
				num = 0;
			}
			for (int i = 0; i < num; i++)
			{
				CreateChara(centerPoint);
			}
		}
		while (IsLoadingCharacter())
		{
			yield return (object)null;
		}
	}

	private IEnumerator SetupLounge()
	{
		MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.SetupNPCSituations();
		OutGameSettingsManager.LoungeScene.NPC[] npcs = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.npcs;
		foreach (OutGameSettingsManager.LoungeScene.NPC npc in npcs)
		{
			HomeCharacterBase chara;
			if (string.IsNullOrEmpty(npc.wayPointName))
			{
				chara = creater.CreateNPC(this, peopleRoot, npc);
			}
			else
			{
				yield return (object)this.StartCoroutine(LoadLoungeWayPoint(npc.wayPointName));
				chara = creater.CreateLoungeMoveNPC(this, peopleRoot, centerPoint, npc);
			}
			if (chara != null)
			{
				charas.Add(chara);
			}
		}
		while (IsLoadingCharacter())
		{
			yield return (object)null;
		}
		isInitialized = true;
		isPeopleInitialized = true;
	}

	private IEnumerator SetupGuild()
	{
		MonoBehaviourSingleton<OutGameSettingsManager>.I.guildScene.SetupNPCSituations();
		OutGameSettingsManager.GuildScene.NPC[] npcs = MonoBehaviourSingleton<OutGameSettingsManager>.I.guildScene.npcs;
		foreach (OutGameSettingsManager.GuildScene.NPC npc in npcs)
		{
			HomeCharacterBase chara = creater.CreateNPC(this, peopleRoot, npc);
			if (chara != null)
			{
				charas.Add(chara);
			}
		}
		while (IsLoadingCharacter())
		{
			yield return (object)null;
		}
		isInitialized = true;
		isPeopleInitialized = true;
	}

	public LoungePlayer GetLoungePlayer(int id)
	{
		for (int i = 0; i < loungePlayers.Count; i++)
		{
			CharaInfo loungeCharaInfo = loungePlayers[i].LoungeCharaInfo;
			if (loungeCharaInfo.userId == id)
			{
				return loungePlayers[i];
			}
		}
		return null;
	}

	private IEnumerator CheckEquipChanged(CharaInfo beforeCharaInfo, CharaInfo currentCharaInfo, bool useMovingEntry)
	{
		if (CheckEquipDiff(beforeCharaInfo, currentCharaInfo))
		{
			DestroyLoungePlayer(beforeCharaInfo.userId);
			yield return (object)null;
			LoungePlayer player = creater.CreateLoungePlayer(this, peopleRoot, currentCharaInfo, useMovingEntry);
			charas.Add(player);
			loungePlayers.Add(player);
		}
	}

	private bool CheckEquipDiff(CharaInfo beforeCharaInfo, CharaInfo currentCharaInfo)
	{
		if (beforeCharaInfo.showHelm != currentCharaInfo.showHelm)
		{
			return true;
		}
		List<CharaInfo.EquipItem> equipSet = beforeCharaInfo.equipSet;
		List<CharaInfo.EquipItem> equipSet2 = currentCharaInfo.equipSet;
		if (equipSet.Count != equipSet2.Count)
		{
			return true;
		}
		List<int> list = new List<int>(7);
		List<int> list2 = new List<int>(7);
		for (int i = 0; i < equipSet.Count; i++)
		{
			list.Add(equipSet[i].eId);
			list2.Add(equipSet2[i].eId);
		}
		List<int> list3 = list;
		List<int> list4 = list2;
		list3.RemoveAll(list4.Contains);
		if (list.Count > 0)
		{
			return true;
		}
		return !beforeCharaInfo.isEqualAccessory(currentCharaInfo.accessory);
	}

	private bool IsLoadingCharacter()
	{
		return charas.Find((HomeCharacterBase o) => o.isLoading) != null;
	}

	private HomeCharacterBase CreateChara(WayPoint way_point)
	{
		FriendCharaInfo chara_info = null;
		if (Random.Range(0, 1) == 0)
		{
			List<FriendCharaInfo> chara = MonoBehaviourSingleton<FriendManager>.I.homeCharas.chara;
			int count = chara.Count;
			if (count > 0)
			{
				int num = Random.Range(0, count);
				int num2 = num;
				do
				{
					FriendCharaInfo info = chara[num2];
					if (info != null && !(charas.Find((HomeCharacterBase o) => o.GetFriendCharaInfo() != null && o.GetFriendCharaInfo().userId == info.userId) != null))
					{
						chara_info = info;
						break;
					}
					num2 = (num2 + 1) % count;
				}
				while (num != num2);
			}
		}
		HomeCharacterBase homeCharacterBase = creater.CreatePlayer(this, peopleRoot, chara_info, way_point);
		charas.Add(homeCharacterBase);
		return homeCharacterBase;
	}

	private void LateUpdate()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int count = charas.Count; i < count; i++)
		{
			HomeCharacterBase homeCharacterBase = charas[i];
			if (!homeCharacterBase.isLoading && homeCharacterBase.get_isActiveAndEnabled())
			{
				Vector2 val = homeCharacterBase._transform.get_localPosition().ToVector2XZ();
				HomeCharacterBase homeCharacterBase2 = null;
				float num = 9f;
				Vector2 val2 = Vector2.get_zero();
				Vector2 val3 = Vector2.get_zero();
				for (int j = i + 1; j < count; j++)
				{
					HomeCharacterBase homeCharacterBase3 = charas[j];
					if (!homeCharacterBase3.isLoading && homeCharacterBase3.get_isActiveAndEnabled())
					{
						Vector2 val4 = homeCharacterBase3._transform.get_localPosition().ToVector2XZ();
						Vector2 val5 = val - val4;
						float sqrMagnitude = val5.get_sqrMagnitude();
						if (sqrMagnitude > 0f && sqrMagnitude < num)
						{
							homeCharacterBase2 = homeCharacterBase3;
							val3 = val4;
							val2 = val5;
							num = sqrMagnitude;
						}
					}
				}
				if (homeCharacterBase2 != null)
				{
					float num2 = Mathf.Sqrt(num);
					Vector2 val6 = val2 / num2;
					float num3 = 1f - num2 / 3f;
					num3 = num3 * Time.get_deltaTime() * 0.9f;
					if (num3 > 0.5f)
					{
						num3 = 0.5f;
					}
					Vector2 val7 = val6 * num3;
					if (!homeCharacterBase.isStop)
					{
						homeCharacterBase._transform.set_localPosition((val + val7).ToVector3XZ());
					}
					if (!homeCharacterBase2.isStop)
					{
						homeCharacterBase2._transform.set_localPosition((val3 - val7).ToVector3XZ());
					}
				}
			}
		}
	}

	private Vector3 GetLoungeMoveNPCTargetPosition(HomeCharacterBase chara, WayPoint wayPoint)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (!(chara is LoungeMoveNPC))
		{
			return wayPoint.GetPosInCollider();
		}
		Vector2 vector = wayPoint.GetPosInCollider().ToVector2XZ();
		return vector.ToVector3XZ();
	}
}
