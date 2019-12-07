using Network;
using System.Collections;
using System.Collections.Generic;

public class ClanPeople : LoungePeople
{
	protected override IEnumerator Start()
	{
		creater = new OutGameCharacterCreater();
		base.charas = new List<HomeCharacterBase>(16);
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			base.loungePlayers = new List<LoungePlayer>(8);
		}
		peopleRoot = Utility.CreateGameObject("PeopleRoot", base.transform);
		yield return StartCoroutine(LocateLoungePeople());
		base.isInitialized = true;
		base.isPeopleInitialized = true;
	}

	protected override IEnumerator LocateLoungePeople()
	{
		OutGameSettingsManager.HomeScene.NPC[] npcs = MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene.npcs;
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
				base.charas.Add(homeCharacterBase);
			}
		}
		while (IsLoadingCharacter())
		{
			yield return null;
		}
	}

	public override bool CreateLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry)
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
		int cLAN_BASE_MAX_DISP_MEMBER_NUM = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_BASE_MAX_DISP_MEMBER_NUM;
		LoungePlayer loungePlayer = (base.loungePlayers.Count <= cLAN_BASE_MAX_DISP_MEMBER_NUM - 1) ? creater.CreateLoungePlayer<LoungePlayer>(this, MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene, peopleRoot, userInfo, useMovingEntry) : creater.CreateLoungePlayer<LoungeLightweightPlayer>(this, MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene, peopleRoot, userInfo, useMovingEntry);
		loungePlayer.SetClanChatEvent();
		base.charas.Add(loungePlayer);
		base.loungePlayers.Add(loungePlayer);
		return true;
	}
}
