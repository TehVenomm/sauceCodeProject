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

	public HomePeople()
		: this()
	{
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
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
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

	private IEnumerator Start()
	{
		creater = new OutGameCharacterCreater();
		charas = new List<HomeCharacterBase>(16);
		peopleRoot = Utility.CreateGameObject("PeopleRoot", this.get_transform());
		yield return this.StartCoroutine(LoadPeopleWayPoint());
		if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
		{
			yield return this.StartCoroutine(GetHomePlayerCharacterList());
		}
		isInitialized = true;
		yield return this.StartCoroutine(LocateHomePeople());
		isPeopleInitialized = true;
		yield return this.StartCoroutine(WatchHomePeople());
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

	private IEnumerator WatchHomePeople()
	{
		while (true)
		{
			yield return null;
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
			HomeCharacterBase homeCharacterBase = creater.CreateNPC(this, peopleRoot, npc);
			if (homeCharacterBase != null)
			{
				charas.Add(homeCharacterBase);
			}
		}
		groupCenterPos = new Vector3(0f, -1f, 0f);
		if (Random.Range(0, 8) == 0)
		{
			int num = Random.Range(2, 5);
			List<HomeCharacterBase> list = new List<HomeCharacterBase>();
			for (int j = 0; j < num; j++)
			{
				list.Add(CreateChara(centerPoint));
			}
			groupCenterPos = list[0]._transform.get_localPosition();
			float num2 = 360f / (float)num;
			float num3 = Random.get_value() * 360f;
			Vector3 val = default(Vector3);
			val._002Ector(0f, 0f, 1f);
			int num4 = 0;
			while (num4 < num)
			{
				Transform transform = list[num4]._transform;
				transform.set_localPosition(Quaternion.AngleAxis(num3, Vector3.get_up()) * val + groupCenterPos);
				transform.LookAt(groupCenterPos);
				list[num4].StopDiscussion();
				num4++;
				num3 += num2;
			}
		}
		else
		{
			int num5 = Random.Range(1, 5);
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
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
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
