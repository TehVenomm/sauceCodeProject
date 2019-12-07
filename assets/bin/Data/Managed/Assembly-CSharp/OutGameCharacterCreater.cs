using Network;
using System;
using UnityEngine;

public class OutGameCharacterCreater
{
	public HomeCharacterBase CreateSelf(IHomePeople home_people, Transform parent, Action<HomeStageAreaEvent> notice_callback)
	{
		HomeSelfCharacter homeSelfCharacter = Utility.CreateGameObjectAndComponent("HomeSelfCharacter", parent) as HomeSelfCharacter;
		homeSelfCharacter.SetHomePeople(home_people);
		homeSelfCharacter.StopDiscussion();
		homeSelfCharacter.SetNoticeCallback(notice_callback);
		OutGameSettingsManager.HomeScene sceneSetting = GameSceneGlobalSettings.GetCurrentIHomeManager().GetSceneSetting();
		Vector3 position;
		float y;
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			position = sceneSetting.selfInitStoryEndPos;
			y = sceneSetting.selfInitStoryEndRot;
		}
		else
		{
			position = sceneSetting.selfInitPos;
			y = sceneSetting.selfInitRot;
		}
		homeSelfCharacter._transform.position = position;
		homeSelfCharacter._transform.eulerAngles = new Vector3(0f, y, 0f);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			homeSelfCharacter.SetChatEvent();
		}
		return homeSelfCharacter;
	}

	public LoungePlayer CreateLoungePlayer(IHomePeople homePeople, Transform parent, CharaInfo chara_info, bool useMovingEntry)
	{
		LoungePlayer obj = Utility.CreateGameObjectAndComponent("LoungePlayer", parent) as LoungePlayer;
		obj.SetHomePeople(homePeople);
		obj.StopDiscussion();
		obj.SetLoungeCharaInfo(chara_info);
		OutGameSettingsManager.LoungeScene loungeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
		Vector3 vector = new Vector3(0f, 0f, -1f);
		float selfInitRot = loungeScene.selfInitRot;
		obj._transform.position = (useMovingEntry ? vector : loungeScene.selfInitPos);
		obj._transform.eulerAngles = new Vector3(0f, selfInitRot, 0f);
		obj.SetMoveTargetPosition(loungeScene.selfInitPos);
		obj.SetChatEvent();
		return obj;
	}

	public T CreateLoungePlayer<T>(IHomePeople homePeople, OutGameSettingsManager.LoungeScene loungeSetting, Transform parent, CharaInfo chara_info, bool useMovingEntry) where T : LoungePlayer
	{
		T obj = Utility.CreateGameObjectAndComponent(typeof(T).Name, parent) as T;
		obj.SetHomePeople(homePeople);
		obj.StopDiscussion();
		obj.SetLoungeCharaInfo(chara_info);
		Vector3 vector = new Vector3(0f, 0f, -1f);
		float selfInitRot = loungeSetting.selfInitRot;
		obj._transform.position = (useMovingEntry ? vector : loungeSetting.selfInitPos);
		obj._transform.eulerAngles = new Vector3(0f, selfInitRot, 0f);
		obj.SetMoveTargetPosition(loungeSetting.selfInitPos);
		obj.SetChatEvent();
		return obj;
	}

	public HomeCharacterBase CreateNPC(IHomePeople homePeople, Transform parent, OutGameSettingsManager.HomeScene.NPC npc)
	{
		if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) && npc.npcID != 0)
		{
			return null;
		}
		OutGameSettingsManager.HomeScene.NPC.Situation situation = npc.GetSituation();
		if (situation == null)
		{
			return null;
		}
		HomeNPCCharacter homeNPCCharacter = (!npc.overrideComponentName.IsNullOrWhiteSpace()) ? (Utility.CreateGameObjectAndComponent(npc.overrideComponentName, parent) as HomeNPCCharacter) : (Utility.CreateGameObjectAndComponent("HomeNPCCharacter", parent) as HomeNPCCharacter);
		homeNPCCharacter.SetNPCInfo(npc);
		homeNPCCharacter.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		homeNPCCharacter.SetHomePeople(homePeople);
		homeNPCCharacter._transform.position = situation.pos;
		homeNPCCharacter._transform.eulerAngles = new Vector3(0f, situation.rot, 0f);
		homeNPCCharacter._transform.localScale = new Vector3(npc.scaleX, 1f, 1f);
		homeNPCCharacter.StopDiscussion();
		return homeNPCCharacter;
	}

	public HomeCharacterBase CreatePlayer(IHomePeople homePeople, Transform parent, FriendCharaInfo chara_info, WayPoint way_point)
	{
		HomePlayerCharacter obj = Utility.CreateGameObjectAndComponent("HomePlayerCharacter", parent) as HomePlayerCharacter;
		Transform transform = obj.transform;
		transform.position = way_point.GetPosInCollider();
		obj.SetHomePeople(homePeople);
		obj.SetWayPoint(way_point);
		obj.SetFriendCharcterInfo(chara_info);
		float num = 0f;
		float num2 = 0f;
		if (!way_point.name.StartsWith("LEAF"))
		{
			num = way_point.transform.eulerAngles.y;
			num2 = 0f;
		}
		else
		{
			num = UnityEngine.Random.Range(0, 360);
			num2 = UnityEngine.Random.Range(-2f, 2f);
		}
		obj.SetWaitTime(num2);
		transform.eulerAngles = new Vector3(0f, num, 0f);
		return obj;
	}

	public HomeCharacterBase CreateLoungeMoveNPC(IHomePeople homePeople, Transform parent, WayPoint way_point, OutGameSettingsManager.HomeScene.NPC npc)
	{
		LoungeMoveNPC obj = Utility.CreateGameObjectAndComponent("LoungeMoveNPC", parent) as LoungeMoveNPC;
		Transform transform = obj.transform;
		transform.position = way_point.GetPosInCollider();
		float y = UnityEngine.Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0f, y, 0f);
		float waitTime = UnityEngine.Random.Range(-2f, 2f);
		obj.SetWaitTime(waitTime);
		obj.SetHomePeople(homePeople);
		obj.SetWayPoint(way_point);
		obj.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		obj.SetNPCInfo(npc);
		return obj;
	}
}
