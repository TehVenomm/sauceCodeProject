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
		if (GetLoungePlayer(userInfo.userId) != null)
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
		if (loungePlayer == null || loungePlayer.gameObject == null)
		{
			return false;
		}
		OnDestroyHomeCharacter(loungePlayer);
		OnDestroyLoungePlayer(loungePlayer);
		UnityEngine.Object.Destroy(loungePlayer.gameObject);
		return true;
	}

	public void SetInitialPositionLoungePlayer(int id, Vector3 initialPos, LOUNGE_ACTION_TYPE type)
	{
		StartCoroutine(DoSetInitialPositionLoungePlayer(id, initialPos, type));
	}

	public void MoveLoungePlayer(int id, Vector3 targetPos)
	{
		LoungePlayer loungePlayer = GetLoungePlayer(id);
		if (!(loungePlayer == null) && !(loungePlayer.gameObject == null))
		{
			loungePlayer.SetMoveTargetPosition(targetPos);
		}
	}

	public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
	{
		if (!(chara is LoungeMoveNPC))
		{
			return wayPoint.GetPosInCollider();
		}
		return wayPoint.GetPosInCollider().ToVector2XZ().ToVector3XZ();
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
		peopleRoot = Utility.CreateGameObject("PeopleRoot", base.transform);
		yield return StartCoroutine(LocateLoungePeople());
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
		LoungePlayer target = GetLoungePlayer(id);
		while (target == null || target.gameObject == null)
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
		LoadingQueue loadingQueue = new LoadingQueue(this);
		string text = "PeopleWayPoints";
		LoadObject lo_way_points = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			text
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Utility.ForEach(ResourceUtility.Realizes(lo_way_points.loadedObject, base.transform), delegate(Transform o)
		{
			if (o.name.StartsWith("LEAF"))
			{
				leafPoints.Add(o.GetComponent<WayPoint>());
			}
			else if (o.name == "CENTER")
			{
				centerPoint = o.GetComponent<WayPoint>();
			}
			return false;
		});
	}

	protected IEnumerator LoadLoungeWayPoint(string wayPointName)
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadWayPoints = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			wayPointName
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Utility.ForEach(ResourceUtility.Realizes(loadWayPoints.loadedObject, base.transform), delegate(Transform o)
		{
			if (o.name == "CENTER")
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
			HomeCharacterBase homeCharacterBase;
			if (string.IsNullOrEmpty(npc.wayPointName))
			{
				homeCharacterBase = creater.CreateNPC(this, peopleRoot, npc);
			}
			else
			{
				yield return StartCoroutine(LoadLoungeWayPoint(npc.wayPointName));
				homeCharacterBase = creater.CreateLoungeMoveNPC(this, peopleRoot, centerPoint, npc);
			}
			if (homeCharacterBase != null)
			{
				charas.Add(homeCharacterBase);
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
			if (loungePlayers[i].LoungeCharaInfo.userId == id)
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
		int i = 0;
		for (int count = charas.Count; i < count; i++)
		{
			HomeCharacterBase homeCharacterBase = charas[i];
			if (homeCharacterBase.isLoading || !homeCharacterBase.isActiveAndEnabled)
			{
				continue;
			}
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
			if (homeCharacterBase2 != null)
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
