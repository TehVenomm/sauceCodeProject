using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePeople : MonoBehaviour, IHomePeople
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

	public ILoungePeople CastToLoungePeople()
	{
		return null;
	}

	public void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback)
	{
		if (!(selfChara != null))
		{
			selfChara = (creater.CreateSelf(this, peopleRoot, notice_callback) as HomeSelfCharacter);
			charas.Add(selfChara);
		}
	}

	public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
	{
		float num = 1.5f;
		num *= num;
		bool flag = groupCenterPos.y != -1f;
		Vector2 b = groupCenterPos.ToVector2XZ();
		for (int i = 0; i < 8; i++)
		{
			Vector2 vector = wayPoint.GetPosInCollider().ToVector2XZ();
			if (flag && (vector - b).sqrMagnitude < num)
			{
				continue;
			}
			int j = 0;
			int count;
			for (count = charas.Count; j < count; j++)
			{
				HomeCharacterBase homeCharacterBase = charas[j];
				if (homeCharacterBase != chara && !homeCharacterBase.isLoading && homeCharacterBase.isActiveAndEnabled && (vector - homeCharacterBase.moveTargetPos.ToVector2XZ()).sqrMagnitude < 0.25f)
				{
					break;
				}
			}
			if (j == count)
			{
				return vector.ToVector3XZ();
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

	private IEnumerator Start()
	{
		creater = new OutGameCharacterCreater();
		charas = new List<HomeCharacterBase>(16);
		peopleRoot = Utility.CreateGameObject("PeopleRoot", base.transform);
		yield return StartCoroutine(LoadPeopleWayPoint());
		if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
		{
			yield return StartCoroutine(GetHomePlayerCharacterList());
		}
		isInitialized = true;
		yield return StartCoroutine(LocateHomePeople());
		isPeopleInitialized = true;
		yield return StartCoroutine(WatchHomePeople());
	}

	private IEnumerator GetHomePlayerCharacterList()
	{
		if (MonoBehaviourSingleton<FriendManager>.I.IsHomeCharaCached)
		{
			MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList(null);
			yield return null;
			yield break;
		}
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

	private IEnumerator WatchHomePeople()
	{
		while (true)
		{
			yield return null;
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) && charas.Count < 14)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));
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
			HomeCharacterBase homeCharacterBase = creater.CreateNPC(this, peopleRoot, npc);
			if (homeCharacterBase != null)
			{
				charas.Add(homeCharacterBase);
			}
		}
		groupCenterPos = new Vector3(0f, -1f, 0f);
		if (UnityEngine.Random.Range(0, 8) == 0)
		{
			int num = UnityEngine.Random.Range(2, 5);
			List<HomeCharacterBase> list = new List<HomeCharacterBase>();
			for (int j = 0; j < num; j++)
			{
				list.Add(CreateChara(centerPoint));
			}
			groupCenterPos = list[0]._transform.localPosition;
			float num2 = 360f / (float)num;
			float num3 = UnityEngine.Random.value * 360f;
			Vector3 point = new Vector3(0f, 0f, 1f);
			int num4 = 0;
			while (num4 < num)
			{
				Transform transform = list[num4]._transform;
				transform.localPosition = Quaternion.AngleAxis(num3, Vector3.up) * point + groupCenterPos;
				transform.LookAt(groupCenterPos);
				list[num4].StopDiscussion();
				num4++;
				num3 += num2;
			}
		}
		else
		{
			int num5 = UnityEngine.Random.Range(1, 5);
			if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
			{
				num5 = 0;
			}
			for (int k = 0; k < num5; k++)
			{
				CreateChara(centerPoint);
			}
		}
		while (IsLoadingCharacter())
		{
			yield return null;
		}
	}

	private bool IsLoadingCharacter()
	{
		return charas.Find((HomeCharacterBase o) => o.isLoading) != null;
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
					if (info == null || charas.Find((HomeCharacterBase o) => o.GetFriendCharaInfo() != null && o.GetFriendCharaInfo().userId == info.userId) != null)
					{
						num2 = (num2 + 1) % count;
						continue;
					}
					chara_info = info;
					break;
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
			if (homeCharacterBase.isLoading || !homeCharacterBase.isActiveAndEnabled)
			{
				continue;
			}
			Vector2 a = homeCharacterBase._transform.localPosition.ToVector2XZ();
			HomeCharacterBase homeCharacterBase2 = null;
			float num = 9f;
			Vector2 a2 = Vector2.zero;
			Vector2 a3 = Vector2.zero;
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
