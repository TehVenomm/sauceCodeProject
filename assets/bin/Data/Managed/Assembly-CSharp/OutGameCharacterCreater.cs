using Network;
using System;
using UnityEngine;

public class OutGameCharacterCreater
{
	public HomeCharacterBase CreateSelf(IHomePeople home_people, Transform parent, Action<HomeStageAreaEvent> notice_callback)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		HomeSelfCharacter homeSelfCharacter = Utility.CreateGameObjectAndComponent("HomeSelfCharacter", parent) as HomeSelfCharacter;
		homeSelfCharacter.SetHomePeople(home_people);
		homeSelfCharacter.StopDiscussion();
		homeSelfCharacter.SetNoticeCallback(notice_callback);
		IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		OutGameSettingsManager.HomeScene sceneSetting = currentIHomeManager.GetSceneSetting();
		Vector3 position;
		float num;
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			position = sceneSetting.selfInitStoryEndPos;
			num = sceneSetting.selfInitStoryEndRot;
		}
		else
		{
			position = sceneSetting.selfInitPos;
			num = sceneSetting.selfInitRot;
		}
		homeSelfCharacter._transform.set_position(position);
		homeSelfCharacter._transform.set_eulerAngles(new Vector3(0f, num, 0f));
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			homeSelfCharacter.SetChatEvent();
		}
		return homeSelfCharacter;
	}

	public LoungePlayer CreateLoungePlayer(IHomePeople homePeople, Transform parent, CharaInfo chara_info, bool useMovingEntry)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		LoungePlayer loungePlayer = Utility.CreateGameObjectAndComponent("LoungePlayer", parent) as LoungePlayer;
		loungePlayer.SetHomePeople(homePeople);
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

	public T CreateLoungePlayer<T>(IHomePeople homePeople, OutGameSettingsManager.LoungeScene loungeSetting, Transform parent, CharaInfo chara_info, bool useMovingEntry) where T : LoungePlayer
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		T result = Utility.CreateGameObjectAndComponent(typeof(T).Name, parent) as T;
		result.SetHomePeople(homePeople);
		result.StopDiscussion();
		result.SetLoungeCharaInfo(chara_info);
		Vector3 val = default(Vector3);
		val._002Ector(0f, 0f, -1f);
		float selfInitRot = loungeSetting.selfInitRot;
		result._transform.set_position((!useMovingEntry) ? loungeSetting.selfInitPos : val);
		result._transform.set_eulerAngles(new Vector3(0f, selfInitRot, 0f));
		result.SetMoveTargetPosition(loungeSetting.selfInitPos);
		result.SetChatEvent();
		return result;
	}

	public HomeCharacterBase CreateNPC(IHomePeople homePeople, Transform parent, OutGameSettingsManager.HomeScene.NPC npc)
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
		HomeNPCCharacter homeNPCCharacter = (!npc.overrideComponentName.IsNullOrWhiteSpace()) ? (Utility.CreateGameObjectAndComponent(npc.overrideComponentName, parent) as HomeNPCCharacter) : (Utility.CreateGameObjectAndComponent("HomeNPCCharacter", parent) as HomeNPCCharacter);
		homeNPCCharacter.SetNPCInfo(npc);
		homeNPCCharacter.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		homeNPCCharacter.SetHomePeople(homePeople);
		homeNPCCharacter._transform.set_position(situation.pos);
		homeNPCCharacter._transform.set_eulerAngles(new Vector3(0f, situation.rot, 0f));
		homeNPCCharacter._transform.set_localScale(new Vector3(npc.scaleX, 1f, 1f));
		homeNPCCharacter.StopDiscussion();
		return homeNPCCharacter;
	}

	public HomeCharacterBase CreatePlayer(IHomePeople homePeople, Transform parent, FriendCharaInfo chara_info, WayPoint way_point)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		HomePlayerCharacter homePlayerCharacter = Utility.CreateGameObjectAndComponent("HomePlayerCharacter", parent) as HomePlayerCharacter;
		Transform transform = homePlayerCharacter.get_transform();
		transform.set_position(way_point.GetPosInCollider());
		homePlayerCharacter.SetHomePeople(homePeople);
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
			num = Random.Range(0, 360);
			num2 = Random.Range(-2f, 2f);
		}
		homePlayerCharacter.SetWaitTime(num2);
		transform.set_eulerAngles(new Vector3(0f, num, 0f));
		return homePlayerCharacter;
	}

	public HomeCharacterBase CreateLoungeMoveNPC(IHomePeople homePeople, Transform parent, WayPoint way_point, OutGameSettingsManager.HomeScene.NPC npc)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		LoungeMoveNPC loungeMoveNPC = Utility.CreateGameObjectAndComponent("LoungeMoveNPC", parent) as LoungeMoveNPC;
		Transform transform = loungeMoveNPC.get_transform();
		transform.set_position(way_point.GetPosInCollider());
		float num = Random.Range(0, 360);
		transform.set_eulerAngles(new Vector3(0f, num, 0f));
		float waitTime = Random.Range(-2f, 2f);
		loungeMoveNPC.SetWaitTime(waitTime);
		loungeMoveNPC.SetHomePeople(homePeople);
		loungeMoveNPC.SetWayPoint(way_point);
		loungeMoveNPC.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
		loungeMoveNPC.SetNPCInfo(npc);
		return loungeMoveNPC;
	}
}
