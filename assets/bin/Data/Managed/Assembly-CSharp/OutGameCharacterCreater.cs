using Network;
using System;
using UnityEngine;

public class OutGameCharacterCreater
{
	public HomeCharacterBase CreateSelf(HomePeople home_people, Transform parent, Action<HomeStageAreaEvent> notice_callback)
	{
		HomeSelfCharacter homeSelfCharacter = Utility.CreateGameObjectAndComponent("HomeSelfCharacter", parent, -1) as HomeSelfCharacter;
		homeSelfCharacter.SetHomePeople(home_people);
		homeSelfCharacter.StopDiscussion();
		homeSelfCharacter.SetNoticeCallback(notice_callback);
		OutGameSettingsManager.HomeScene homeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene;
		OutGameSettingsManager.LoungeScene loungeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
		OutGameSettingsManager.GuildScene guildScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.guildScene;
		Vector3 position;
		float y;
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			position = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitStoryEndPos : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitStoryEndPos : loungeScene.selfInitStoryEndPos));
			y = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitStoryEndRot : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitStoryEndRot : loungeScene.selfInitStoryEndRot));
		}
		else
		{
			position = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitPos : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitPos : loungeScene.selfInitPos));
			y = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitRot : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitRot : loungeScene.selfInitRot));
		}
		homeSelfCharacter._transform.position = position;
		homeSelfCharacter._transform.eulerAngles = new Vector3(0f, y, 0f);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			homeSelfCharacter.SetChatEvent();
		}
		return homeSelfCharacter;
	}

	public LoungePlayer CreateLoungePlayer(HomePeople home_people, Transform parent, CharaInfo chara_info, bool useMovingEntry)
	{
		LoungePlayer loungePlayer = Utility.CreateGameObjectAndComponent("LoungePlayer", parent, -1) as LoungePlayer;
		loungePlayer.SetHomePeople(home_people);
		loungePlayer.StopDiscussion();
		loungePlayer.SetLoungeCharaInfo(chara_info);
		OutGameSettingsManager.LoungeScene loungeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
		Vector3 vector = new Vector3(0f, 0f, -1f);
		float selfInitRot = loungeScene.selfInitRot;
		loungePlayer._transform.position = ((!useMovingEntry) ? loungeScene.selfInitPos : vector);
		loungePlayer._transform.eulerAngles = new Vector3(0f, selfInitRot, 0f);
		loungePlayer.SetMoveTargetPosition(loungeScene.selfInitPos);
		loungePlayer.SetChatEvent();
		return loungePlayer;
	}

	public HomeCharacterBase CreateNPC(HomePeople home_people, Transform parent, OutGameSettingsManager.HomeScene.NPC npc)
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
		HomeNPCCharacter homeNPCCharacter = (!npc.overrideComponentName.IsNullOrWhiteSpace()) ? (Utility.CreateGameObjectAndComponent(npc.overrideComponentName, parent, -1) as HomeNPCCharacter) : (Utility.CreateGameObjectAndComponent("HomeNPCCharacter", parent, -1) as HomeNPCCharacter);
		homeNPCCharacter.SetNPCInfo(npc);
		homeNPCCharacter.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		homeNPCCharacter.SetHomePeople(home_people);
		homeNPCCharacter._transform.position = situation.pos;
		homeNPCCharacter._transform.eulerAngles = new Vector3(0f, situation.rot, 0f);
		homeNPCCharacter._transform.localScale = new Vector3(npc.scaleX, 1f, 1f);
		homeNPCCharacter.StopDiscussion();
		return homeNPCCharacter;
	}

	public HomeCharacterBase CreatePlayer(HomePeople home_people, Transform parent, FriendCharaInfo chara_info, WayPoint way_point)
	{
		HomePlayerCharacter homePlayerCharacter = Utility.CreateGameObjectAndComponent("HomePlayerCharacter", parent, -1) as HomePlayerCharacter;
		Transform transform = homePlayerCharacter.transform;
		transform.position = way_point.GetPosInCollider();
		homePlayerCharacter.SetHomePeople(home_people);
		homePlayerCharacter.SetWayPoint(way_point);
		homePlayerCharacter.SetFriendCharcterInfo(chara_info);
		float num = 0f;
		float num2 = 0f;
		if (!way_point.name.StartsWith("LEAF"))
		{
			Vector3 eulerAngles = way_point.transform.eulerAngles;
			num = eulerAngles.y;
			num2 = 0f;
		}
		else
		{
			num = (float)UnityEngine.Random.Range(0, 360);
			num2 = UnityEngine.Random.Range(-2f, 2f);
		}
		homePlayerCharacter.SetWaitTime(num2);
		transform.eulerAngles = new Vector3(0f, num, 0f);
		return homePlayerCharacter;
	}

	public HomeCharacterBase CreateLoungeMoveNPC(HomePeople home_people, Transform parent, WayPoint way_point, OutGameSettingsManager.HomeScene.NPC npc)
	{
		LoungeMoveNPC loungeMoveNPC = Utility.CreateGameObjectAndComponent("LoungeMoveNPC", parent, -1) as LoungeMoveNPC;
		Transform transform = loungeMoveNPC.transform;
		transform.position = way_point.GetPosInCollider();
		float y = (float)UnityEngine.Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0f, y, 0f);
		float waitTime = UnityEngine.Random.Range(-2f, 2f);
		loungeMoveNPC.SetWaitTime(waitTime);
		loungeMoveNPC.SetHomePeople(home_people);
		loungeMoveNPC.SetWayPoint(way_point);
		loungeMoveNPC.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		loungeMoveNPC.SetNPCInfo(npc);
		return loungeMoveNPC;
	}
}
