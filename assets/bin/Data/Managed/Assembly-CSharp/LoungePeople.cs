using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungePeople : MonoBehaviour, IHomePeople, ILoungePeople
{
	private ILoungePeople self;

	private const int charaMax = 14;

	private const float GROUP_RADIUS = 1f;

	protected Transform peopleRoot;

	protected WayPoint centerPoint;

	private Vector3 groupCenterPos;

	private List<WayPoint> leafPoints = new List<WayPoint>();

	protected OutGameCharacterCreater creater;

	public bool isInitialized
	{
		get;
		protected set;
	}

	public bool isPeopleInitialized
	{
		get;
		protected set;
	}

	public HomeSelfCharacter selfChara
	{
		get;
		protected set;
	}

	public List<HomeCharacterBase> charas
	{
		get;
		protected set;
	}

	public List<LoungePlayer> loungePlayers
	{
		get;
		protected set;
	}

	public LoungePeople()
		: this()
	{
	}

	public ILoungePeople CastToLoungePeople()
	{
		if (self == null)
		{
			self = this;
		}
		return self;
	}

	public void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback)
	{
		if (!(selfChara != null))
		{
			selfChara = (creater.CreateSelf(this, peopleRoot, notice_callback) as HomeSelfCharacter);
			charas.Add(selfChara);
		}
	}

	public virtual bool CreateLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry)
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
		if (loungePlayer != null)
		{
			return false;
		}
		LoungePlayer item = creater.CreateLoungePlayer<LoungePlayer>(this, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene, peopleRoot, userInfo, useMovingEntry);
		charas.Add(item);
		loungePlayers.Add(item);
		return true;
	}

	public bool ChangeEquipLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry)
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
		if (loungePlayer == null)
		{
			return false;
		}
		CheckEquipChanged(loungePlayer.LoungeCharaInfo, userInfo, useMovingEntry);
		return true;
	}

	public void UpdateLoungePlayersInfo(PartyModel.SlotInfo slotInfo)
	{
		for (int i = 0; i < loungePlayers.Count; i++)
		{
			if (loungePlayers[i].LoungeCharaInfo.userId == slotInfo.userInfo.userId)
			{
				loungePlayers[i].SetLoungeCharaInfo(slotInfo.userInfo);
			}
		}
	}

	public bool DestroyLoungePlayer(int id)
	{
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
		this.StartCoroutine(DoSetInitialPositionLoungePlayer(id, initialPos, type));
	}

	public void MoveLoungePlayer(int id, Vector3 targetPos)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer loungePlayer = GetLoungePlayer(id);
		if (!(loungePlayer == null) && !(loungePlayer.get_gameObject() == null))
		{
			loungePlayer.SetMoveTargetPosition(targetPos);
		}
	}

	public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
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

	protected virtual IEnumerator Start()
	{
		creater = new OutGameCharacterCreater();
		charas = new List<HomeCharacterBase>(16);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			loungePlayers = new List<LoungePlayer>(8);
		}
		peopleRoot = Utility.CreateGameObject("PeopleRoot", this.get_transform());
		yield return this.StartCoroutine(LocateLoungePeople());
		isInitialized = true;
		isPeopleInitialized = true;
	}

	private IEnumerator GetHomePlayerCharacterList()
	{
		bool wait = true;
		MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList(delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
	}

	private IEnumerator DoSetInitialPositionLoungePlayer(int id, Vector3 pos, LOUNGE_ACTION_TYPE type)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer target = GetLoungePlayer(id);
		while (target == null || target.get_gameObject() == null)
		{
			target = GetLoungePlayer(id);
			yield return null;
		}
		while (target.isLoading)
		{
			yield return null;
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
		});
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		Transform wayPoints = ResourceUtility.Realizes(lo_way_points.loadedObject, this.get_transform());
		Utility.ForEach(wayPoints, delegate(Transform o)
		{
			if (o.get_name().StartsWith("LEAF"))
			{
				leafPoints.Add(o.GetComponent<WayPoint>());
			}
			else if (o.get_name() == "CENTER")
			{
				centerPoint = o.GetComponent<WayPoint>();
			}
			return false;
		});
	}

	protected IEnumerator LoadLoungeWayPoint(string wayPointName)
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadWayPoints = loadQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			wayPointName
		});
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		Transform wayPoints = ResourceUtility.Realizes(loadWayPoints.loadedObject, this.get_transform());
		Utility.ForEach(wayPoints, delegate(Transform o)
		{
			if (o.get_name() == "CENTER")
			{
				centerPoint = o.GetComponent<WayPoint>();
			}
			return false;
		});
	}

	protected virtual IEnumerator LocateLoungePeople()
	{
		OutGameSettingsManager.HomeScene.NPC[] npcs = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.npcs;
		foreach (OutGameSettingsManager.HomeScene.NPC npc in npcs)
		{
			HomeCharacterBase chara;
			if (string.IsNullOrEmpty(npc.wayPointName))
			{
				chara = creater.CreateNPC(this, peopleRoot, npc);
			}
			else
			{
				yield return this.StartCoroutine(LoadLoungeWayPoint(npc.wayPointName));
				chara = creater.CreateLoungeMoveNPC(this, peopleRoot, centerPoint, npc);
			}
			if (chara != null)
			{
				charas.Add(chara);
			}
		}
		while (IsLoadingCharacter())
		{
			yield return null;
		}
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

	protected void CheckEquipChanged(CharaInfo beforeCharaInfo, CharaInfo currentCharaInfo, bool useMovingEntry)
	{
		if (CheckEquipDiff(beforeCharaInfo, currentCharaInfo))
		{
			DestroyLoungePlayer(beforeCharaInfo.userId);
			LoungePlayer item = creater.CreateLoungePlayer(this, peopleRoot, currentCharaInfo, useMovingEntry);
			charas.Add(item);
			loungePlayers.Add(item);
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
		list.RemoveAll(list2.Contains);
		if (list.Count > 0)
		{
			return true;
		}
		return !beforeCharaInfo.isEqualAccessory(currentCharaInfo.accessory);
	}

	protected bool IsLoadingCharacter()
	{
		return charas.Find((HomeCharacterBase o) => o.isLoading) != null;
	}

	private void LateUpdate()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int count = charas.Count; i < count; i++)
		{
			HomeCharacterBase homeCharacterBase = charas[i];
			if (homeCharacterBase.isLoading || !homeCharacterBase.get_isActiveAndEnabled())
			{
				continue;
			}
			Vector2 val = homeCharacterBase._transform.get_localPosition().ToVector2XZ();
			HomeCharacterBase homeCharacterBase2 = null;
			float num = 9f;
			Vector2 val2 = default(Vector2);
			val2._002Ector(0f, 0f);
			Vector2 val3 = default(Vector2);
			val3._002Ector(0f, 0f);
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
