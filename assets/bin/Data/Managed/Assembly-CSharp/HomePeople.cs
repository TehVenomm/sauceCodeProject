using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePeople : MonoBehaviour
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

	public void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback)
	{
		if (!((UnityEngine.Object)selfChara != (UnityEngine.Object)null))
		{
			selfChara = (creater.CreateSelf(this, peopleRoot, notice_callback) as HomeSelfCharacter);
			charas.Add(selfChara);
		}
	}

	public bool CreateLoungePlayer(LoungeModel.SlotInfo slotInfo, bool useMovingEntry, bool checkEquip)
	{
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
		if ((UnityEngine.Object)loungePlayer != (UnityEngine.Object)null)
		{
			if (checkEquip)
			{
				StartCoroutine(CheckEquipChanged(loungePlayer.LoungeCharaInfo, userInfo, useMovingEntry));
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
		LoungePlayer loungePlayer = GetLoungePlayer(id);
		if ((UnityEngine.Object)loungePlayer == (UnityEngine.Object)null || (UnityEngine.Object)loungePlayer.gameObject == (UnityEngine.Object)null)
		{
			return false;
		}
		OnDestroyHomeCharacter(loungePlayer);
		OnDestroyLoungePlayer(loungePlayer);
		UnityEngine.Object.DestroyObject(loungePlayer.gameObject);
		return true;
	}

	public void SetInitialPositionLoungePlayer(int id, Vector3 initialPos, LOUNGE_ACTION_TYPE type)
	{
		StartCoroutine(DoSetInitialPositionLoungePlayer(id, initialPos, type));
	}

	public void MoveLoungePlayer(int id, Vector3 targetPos)
	{
		LoungePlayer loungePlayer = GetLoungePlayer(id);
		if (!((UnityEngine.Object)loungePlayer == (UnityEngine.Object)null) && !((UnityEngine.Object)loungePlayer.gameObject == (UnityEngine.Object)null))
		{
			loungePlayer.SetMoveTargetPosition(targetPos);
		}
	}

	public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
	{
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			return GetLoungeMoveNPCTargetPosition(chara, wayPoint);
		}
		float num = 1.5f;
		num *= num;
		bool flag = groupCenterPos.y != -1f;
		Vector2 b = groupCenterPos.ToVector2XZ();
		for (int i = 0; i < 8; i++)
		{
			Vector2 vector = wayPoint.GetPosInCollider().ToVector2XZ();
			if (!flag || !((vector - b).sqrMagnitude < num))
			{
				int j = 0;
				int count;
				for (count = charas.Count; j < count; j++)
				{
					HomeCharacterBase homeCharacterBase = charas[j];
					if ((UnityEngine.Object)homeCharacterBase != (UnityEngine.Object)chara && !homeCharacterBase.isLoading && homeCharacterBase.isActiveAndEnabled && (vector - homeCharacterBase.moveTargetPos.ToVector2XZ()).sqrMagnitude < 0.25f)
					{
						break;
					}
				}
				if (j == count)
				{
					return vector.ToVector3XZ();
				}
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
			if (!((UnityEngine.Object)homeNPCCharacter == (UnityEngine.Object)null) && npcID == homeNPCCharacter.npcInfo.npcID)
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
		peopleRoot = Utility.CreateGameObject("PeopleRoot", base.transform, -1);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			yield return (object)StartCoroutine(SetupLounge());
		}
		else if (MonoBehaviourSingleton<GuildStageManager>.IsValid())
		{
			yield return (object)StartCoroutine(SetupGuild());
		}
		else
		{
			yield return (object)StartCoroutine(LoadPeopleWayPoint());
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
			{
				yield return (object)StartCoroutine(GetHomePlayerCharacterList());
			}
			isInitialized = true;
			yield return (object)StartCoroutine(LocateHomePeople());
			isPeopleInitialized = true;
			yield return (object)StartCoroutine(WatchHomePeople());
		}
	}

	private IEnumerator GetHomePlayerCharacterList()
	{
		bool wait = true;
		MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList(delegate
		{
			((_003CGetHomePlayerCharacterList_003Ec__IteratorB4)/*Error near IL_002d: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
	}

	private IEnumerator DoSetInitialPositionLoungePlayer(int id, Vector3 pos, LOUNGE_ACTION_TYPE type)
	{
		LoungePlayer target = GetLoungePlayer(id);
		while ((UnityEngine.Object)target == (UnityEngine.Object)null || (UnityEngine.Object)target.gameObject == (UnityEngine.Object)null)
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
		Transform wayPoints = ResourceUtility.Realizes(lo_way_points.loadedObject, base.transform, -1);
		Utility.ForEach(wayPoints, delegate(Transform o)
		{
			if (o.name.StartsWith("LEAF"))
			{
				((_003CLoadPeopleWayPoint_003Ec__IteratorB6)/*Error near IL_00ba: stateMachine*/)._003C_003Ef__this.leafPoints.Add(o.GetComponent<WayPoint>());
			}
			else if (o.name == "CENTER")
			{
				((_003CLoadPeopleWayPoint_003Ec__IteratorB6)/*Error near IL_00ba: stateMachine*/)._003C_003Ef__this.centerPoint = o.GetComponent<WayPoint>();
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
		Transform wayPoints = ResourceUtility.Realizes(loadWayPoints.loadedObject, base.transform, -1);
		Utility.ForEach(wayPoints, delegate(Transform o)
		{
			if (o.name == "CENTER")
			{
				((_003CLoadLoungeWayPoint_003Ec__IteratorB7)/*Error near IL_00af: stateMachine*/)._003C_003Ef__this.centerPoint = o.GetComponent<WayPoint>();
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
				yield return (object)new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));
				if (AppMain.isReset)
				{
					break;
				}
				WayPoint way_point = leafPoints[UnityEngine.Random.Range(0, leafPoints.Count)];
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
			if ((UnityEngine.Object)chara != (UnityEngine.Object)null)
			{
				charas.Add(chara);
			}
		}
		groupCenterPos = new Vector3(0f, -1f, 0f);
		if (UnityEngine.Random.Range(0, 8) == 0)
		{
			int num2 = UnityEngine.Random.Range(2, 5);
			List<HomeCharacterBase> list = new List<HomeCharacterBase>();
			for (int k = 0; k < num2; k++)
			{
				list.Add(CreateChara(centerPoint));
			}
			groupCenterPos = list[0]._transform.localPosition;
			float angle_step = 360f / (float)num2;
			float angle = UnityEngine.Random.value * 360f;
			Vector3 DIST = new Vector3(0f, 0f, 1f);
			int j = 0;
			while (j < num2)
			{
				Transform t = list[j]._transform;
				t.localPosition = Quaternion.AngleAxis(angle, Vector3.up) * DIST + groupCenterPos;
				t.LookAt(groupCenterPos);
				list[j].StopDiscussion();
				j++;
				angle += angle_step;
			}
		}
		else
		{
			int num = UnityEngine.Random.Range(1, 5);
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
				yield return (object)StartCoroutine(LoadLoungeWayPoint(npc.wayPointName));
				chara = creater.CreateLoungeMoveNPC(this, peopleRoot, centerPoint, npc);
			}
			if ((UnityEngine.Object)chara != (UnityEngine.Object)null)
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
			if ((UnityEngine.Object)chara != (UnityEngine.Object)null)
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
		return (UnityEngine.Object)charas.Find((HomeCharacterBase o) => o.isLoading) != (UnityEngine.Object)null;
	}

	private HomeCharacterBase CreateChara(WayPoint way_point)
	{
		FriendCharaInfo chara_info = null;
		if (UnityEngine.Random.Range(0, 1) == 0)
		{
			List<FriendCharaInfo> chara = MonoBehaviourSingleton<FriendManager>.I.homeCharas.chara;
			int count = chara.Count;
			if (count > 0)
			{
				int num = UnityEngine.Random.Range(0, count);
				int num2 = num;
				do
				{
					FriendCharaInfo info = chara[num2];
					if (info != null && !((UnityEngine.Object)charas.Find((HomeCharacterBase o) => o.GetFriendCharaInfo() != null && o.GetFriendCharaInfo().userId == info.userId) != (UnityEngine.Object)null))
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
		int i = 0;
		for (int count = charas.Count; i < count; i++)
		{
			HomeCharacterBase homeCharacterBase = charas[i];
			if (!homeCharacterBase.isLoading && homeCharacterBase.isActiveAndEnabled)
			{
				Vector2 a = homeCharacterBase._transform.localPosition.ToVector2XZ();
				HomeCharacterBase homeCharacterBase2 = null;
				float num = 9f;
				Vector2 a2 = new Vector2(0f, 0f);
				Vector2 a3 = new Vector2(0f, 0f);
				for (int j = i + 1; j < count; j++)
				{
					HomeCharacterBase homeCharacterBase3 = charas[j];
					if (!homeCharacterBase3.isLoading && homeCharacterBase3.isActiveAndEnabled)
					{
						Vector2 vector = homeCharacterBase3._transform.localPosition.ToVector2XZ();
						Vector2 vector2 = a - vector;
						float sqrMagnitude = vector2.sqrMagnitude;
						if (sqrMagnitude > 0f && sqrMagnitude < num)
						{
							homeCharacterBase2 = homeCharacterBase3;
							a3 = vector;
							a2 = vector2;
							num = sqrMagnitude;
						}
					}
				}
				if ((UnityEngine.Object)homeCharacterBase2 != (UnityEngine.Object)null)
				{
					float num2 = Mathf.Sqrt(num);
					Vector2 a4 = a2 / num2;
					float num3 = 1f - num2 / 3f;
					num3 = num3 * Time.deltaTime * 0.9f;
					if (num3 > 0.5f)
					{
						num3 = 0.5f;
					}
					Vector2 b = a4 * num3;
					if (!homeCharacterBase.isStop)
					{
						homeCharacterBase._transform.localPosition = (a + b).ToVector3XZ();
					}
					if (!homeCharacterBase2.isStop)
					{
						homeCharacterBase2._transform.localPosition = (a3 - b).ToVector3XZ();
					}
				}
			}
		}
	}

	private Vector3 GetLoungeMoveNPCTargetPosition(HomeCharacterBase chara, WayPoint wayPoint)
	{
		if (!(chara is LoungeMoveNPC))
		{
			return wayPoint.GetPosInCollider();
		}
		Vector2 vector = wayPoint.GetPosInCollider().ToVector2XZ();
		return vector.ToVector3XZ();
	}
}
