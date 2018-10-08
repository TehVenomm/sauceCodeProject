using Network;
using System;
using UnityEngine;

public class OutGameCharacterCreater
{
	public HomeCharacterBase CreateSelf(HomePeople home_people, Transform parent, Action<HomeStageAreaEvent> notice_callback)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		HomeSelfCharacter homeSelfCharacter = Utility.CreateGameObjectAndComponent("HomeSelfCharacter", parent, -1) as HomeSelfCharacter;
		homeSelfCharacter.SetHomePeople(home_people);
		homeSelfCharacter.StopDiscussion();
		homeSelfCharacter.SetNoticeCallback(notice_callback);
		OutGameSettingsManager.HomeScene homeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene;
		OutGameSettingsManager.LoungeScene loungeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
		OutGameSettingsManager.GuildScene guildScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.guildScene;
		Vector3 position;
		float num;
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			position = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitStoryEndPos : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitStoryEndPos : loungeScene.selfInitStoryEndPos));
			num = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitStoryEndRot : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitStoryEndRot : loungeScene.selfInitStoryEndRot));
		}
		else
		{
			position = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitPos : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitPos : loungeScene.selfInitPos));
			num = (MonoBehaviourSingleton<HomeManager>.IsValid() ? homeScene.selfInitRot : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? guildScene.selfInitRot : loungeScene.selfInitRot));
		}
		homeSelfCharacter._transform.set_position(position);
		homeSelfCharacter._transform.set_eulerAngles(new Vector3(0f, num, 0f));
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			homeSelfCharacter.SetChatEvent();
		}
		return homeSelfCharacter;
	}

	public LoungePlayer CreateLoungePlayer(HomePeople home_people, Transform parent, CharaInfo chara_info, bool useMovingEntry)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer loungePlayer = Utility.CreateGameObjectAndComponent("LoungePlayer", parent, -1) as LoungePlayer;
		loungePlayer.SetHomePeople(home_people);
		loungePlayer.StopDiscussion();
		loungePlayer.SetLoungeCharaInfo(chara_info);
		OutGameSettingsManager.LoungeScene loungeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
		Vector3 val = default(Vector3);
		val._002Ector(0f, 0f, -1f);
		float selfInitRot = loungeScene.selfInitRot;
		loungePlayer._transform.set_position((!useMovingEntry) ? loungeScene.selfInitPos : val);
		loungePlayer._transform.set_eulerAngles(new Vector3(0f, selfInitRot, 0f));
		loungePlayer.SetMoveTargetPosition(loungeScene.selfInitPos);
		loungePlayer.SetChatEvent();
		return loungePlayer;
	}

	public HomeCharacterBase CreateNPC(HomePeople home_people, Transform parent, OutGameSettingsManager.HomeScene.NPC npc)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
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
		homeNPCCharacter._transform.set_position(situation.pos);
		homeNPCCharacter._transform.set_eulerAngles(new Vector3(0f, situation.rot, 0f));
		homeNPCCharacter._transform.set_localScale(new Vector3(npc.scaleX, 1f, 1f));
		homeNPCCharacter.StopDiscussion();
		return homeNPCCharacter;
	}

	public HomeCharacterBase CreatePlayer(HomePeople home_people, Transform parent, FriendCharaInfo chara_info, WayPoint way_point)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		HomePlayerCharacter homePlayerCharacter = Utility.CreateGameObjectAndComponent("HomePlayerCharacter", parent, -1) as HomePlayerCharacter;
		Transform val = homePlayerCharacter.get_transform();
		val.set_position(way_point.GetPosInCollider());
		homePlayerCharacter.SetHomePeople(home_people);
		homePlayerCharacter.SetWayPoint(way_point);
		homePlayerCharacter.SetFriendCharcterInfo(chara_info);
		float num = 0f;
		float num2 = 0f;
		if (!way_point.get_name().StartsWith("LEAF"))
		{
			Vector3 eulerAngles = way_point.get_transform().get_eulerAngles();
			num = eulerAngles.y;
			num2 = 0f;
		}
		else
		{
			num = (float)Random.Range(0, 360);
			num2 = Random.Range(-2f, 2f);
		}
		homePlayerCharacter.SetWaitTime(num2);
		val.set_eulerAngles(new Vector3(0f, num, 0f));
		return homePlayerCharacter;
	}

	public HomeCharacterBase CreateLoungeMoveNPC(HomePeople home_people, Transform parent, WayPoint way_point, OutGameSettingsManager.HomeScene.NPC npc)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		LoungeMoveNPC loungeMoveNPC = Utility.CreateGameObjectAndComponent("LoungeMoveNPC", parent, -1) as LoungeMoveNPC;
		Transform val = loungeMoveNPC.get_transform();
		val.set_position(way_point.GetPosInCollider());
		float num = (float)Random.Range(0, 360);
		val.set_eulerAngles(new Vector3(0f, num, 0f));
		float waitTime = Random.Range(-2f, 2f);
		loungeMoveNPC.SetWaitTime(waitTime);
		loungeMoveNPC.SetHomePeople(home_people);
		loungeMoveNPC.SetWayPoint(way_point);
		loungeMoveNPC.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		loungeMoveNPC.SetNPCInfo(npc);
		return loungeMoveNPC;
	}
}
