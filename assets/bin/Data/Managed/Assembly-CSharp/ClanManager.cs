using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanManager : MonoBehaviourSingleton<ClanManager>, IHomeManager
{
	private const float SendInfoSpan = 3600f;

	private SpanTimer sendInfoSpan;

	private Queue<LoungeAnnounce.AnnounceData> clanAnnounceQueue = new Queue<LoungeAnnounce.AnnounceData>();

	private Coroutine loungeAnnounceCoroutine;

	public bool IsJumpToGacha
	{
		get;
		set;
	}

	public bool IsInitialized
	{
		get;
		private set;
	}

	public HomeCamera HomeCamera
	{
		get;
		private set;
	}

	public IHomePeople IHomePeople
	{
		get;
		private set;
	}

	public LoungeTableSet TableSet
	{
		get;
		private set;
	}

	public HomeFeatureBanner HomeFeatureBanner
	{
		get;
		private set;
	}

	public bool IsPointShopOpen
	{
		get;
		private set;
	}

	public int PointShopBannerId
	{
		get;
		private set;
	}

	public bool NeedLoungeQuestBalloonUpdate
	{
		get;
		private set;
	}

	public ClanNoticeBoardData noticeBoardData
	{
		get;
		private set;
	}

	public void SetPointShop(bool isOpen, int bannerId)
	{
		IsPointShopOpen = isOpen;
		PointShopBannerId = bannerId;
	}

	public void SetNoticeBoardData(ClanNoticeBoardData noticeBoardData, bool isSaveBoardVersion)
	{
		if (noticeBoardData == null)
		{
			noticeBoardData = new ClanNoticeBoardData();
		}
		this.noticeBoardData = noticeBoardData;
		if (isSaveBoardVersion && this.noticeBoardData != null)
		{
			PlayerPrefs.SetInt("CLAN_BOARD_READ_ID_KEY", this.noticeBoardData.version);
			PlayerPrefs.Save();
		}
	}

	public OutGameSettingsManager.HomeScene GetSceneSetting()
	{
		return MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene;
	}

	protected override void Awake()
	{
		base.Awake();
		sendInfoSpan = new SpanTimer(3600f);
	}

	private IEnumerator Start()
	{
		while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return null;
		}
		base.gameObject.AddComponent<ClanLvUnlockManager>();
		HomeCamera = base.gameObject.AddComponent<HomeCamera>();
		IHomePeople = base.gameObject.AddComponent<ClanPeople>();
		HomeFeatureBanner = base.gameObject.AddComponent<HomeFeatureBanner>();
		TableSet = base.gameObject.AddComponent<LoungeTableSet>();
		while (!HomeCamera.isInitialized || !IHomePeople.isInitialized || !TableSet.isInitialized)
		{
			yield return null;
		}
		ClanMatchingManager i = MonoBehaviourSingleton<ClanMatchingManager>.I;
		i.OnChangeMemberStatus = (Action<LoungeMemberStatus>)Delegate.Combine(i.OnChangeMemberStatus, new Action<LoungeMemberStatus>(OnChangeMemberStatus));
		IsInitialized = true;
		if (sendInfoSpan.IsReady())
		{
			yield return StartCoroutine(SendLoungeInfoForce());
		}
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
		{
			if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
			{
				yield break;
			}
			bool isWait = true;
			MonoBehaviourSingleton<ClanMatchingManager>.I.SendEnterToClanBase(delegate
			{
				isWait = false;
			});
			while (isWait)
			{
				yield return null;
			}
			while (!MonoBehaviourSingleton<ClanMatchingManager>.I.IsConnected())
			{
				bool num = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop";
				bool flag = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop";
				if (num | flag)
				{
					break;
				}
				yield return null;
			}
		}
		yield return StartCoroutine(CreateLoungePlayerFromSlotInfo());
		if (ClanMatchingManager.IsValidInClan())
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.SendInClanBase();
		}
	}

	private IEnumerator CreateLoungePlayerFromSlotInfo()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null || MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus == null)
		{
			yield break;
		}
		List<PartyModel.SlotInfo> data = MonoBehaviourSingleton<ClanMatchingManager>.I.partyData.slotInfos;
		for (int i = 0; i < data.Count; i++)
		{
			if (data[i].userInfo != null && data[i].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				LoungeMemberStatus.MEMBER_STATUS status = MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus[data[i].userInfo.userId].GetStatus();
				if (status == LoungeMemberStatus.MEMBER_STATUS.LOUNGE || status == LoungeMemberStatus.MEMBER_STATUS.QUEST_READY)
				{
					IHomePeople.CastToLoungePeople().CreateLoungePlayer(data[i], useMovingEntry: false);
					yield return null;
				}
			}
		}
	}

	private IEnumerator CreateCharacterRoomJoined(int userId)
	{
		yield return StartCoroutine(SendLoungeInfoForce());
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId);
		if (slotInfoByUserId != null && IHomePeople != null && IHomePeople.CastToLoungePeople().CreateLoungePlayer(slotInfoByUserId, useMovingEntry: true))
		{
			SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.JOIN_LOUNGE, slotInfoByUserId.userInfo.name));
			if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanNetworkManager>.I.JoinNotification(slotInfoByUserId.userInfo);
			}
		}
	}

	private void OnChangeMemberStatus(LoungeMemberStatus status)
	{
		int userId = status.userId;
		switch (status.GetStatus())
		{
		case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
			if (status.isHost)
			{
				CreatePartyAnnounce(userId);
			}
			break;
		case LoungeMemberStatus.MEMBER_STATUS.QUEST:
		case LoungeMemberStatus.MEMBER_STATUS.FIELD:
		case LoungeMemberStatus.MEMBER_STATUS.ARENA:
			IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
			SendRoomPosition(userId);
			NeedLoungeQuestBalloonUpdate = true;
			StartCoroutine(CreatePlayerOnChangedStatus(userId));
			break;
		}
	}

	private IEnumerator CreatePlayerOnChangedStatus(int userId)
	{
		yield return SendLoungeInfoForce();
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId);
		IHomePeople.CastToLoungePeople().CreateLoungePlayer(slotInfoByUserId, useMovingEntry: true);
		IHomePeople.CastToLoungePeople().ChangeEquipLoungePlayer(slotInfoByUserId, useMovingEntry: true);
	}

	private void CreatePartyAnnounce(int userId)
	{
		NeedLoungeQuestBalloonUpdate = true;
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId);
		LoungeAnnounce.AnnounceData announce = new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.CREATED_PARTY, slotInfoByUserId.userInfo.name);
		SetAnnounce(announce);
	}

	private void SetAnnounce(LoungeAnnounce.AnnounceData data)
	{
		if (data != null)
		{
			clanAnnounceQueue.Enqueue(data);
			if (loungeAnnounceCoroutine == null)
			{
				loungeAnnounceCoroutine = StartCoroutine(ShowAnnounce());
			}
		}
	}

	private IEnumerator ShowAnnounce()
	{
		ClanAnnounce announce = MonoBehaviourSingleton<UIManager>.I.clanAnnounce;
		if (announce == null)
		{
			loungeAnnounceCoroutine = null;
			yield break;
		}
		while (clanAnnounceQueue.Count > 0)
		{
			LoungeAnnounce.AnnounceData data = clanAnnounceQueue.Dequeue();
			bool wait = true;
			announce.Play(data, delegate
			{
				wait = false;
			});
			while (wait)
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.3f);
		}
		loungeAnnounceCoroutine = null;
	}

	private void Update()
	{
		if (sendInfoSpan.IsReady())
		{
			StartCoroutine(SendLoungeInfoForce());
		}
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
		{
			return;
		}
		List<PartyModel.SlotInfo> slotInfos = MonoBehaviourSingleton<ClanMatchingManager>.I.partyData.slotInfos;
		if (slotInfos == null)
		{
			return;
		}
		for (int i = 0; i < slotInfos.Count; i++)
		{
			if (IHomePeople == null)
			{
				break;
			}
			IHomePeople.CastToLoungePeople().UpdateLoungePlayersInfo(slotInfos[i]);
		}
	}

	protected override void _OnDestroy()
	{
		ClanMatchingManager i = MonoBehaviourSingleton<ClanMatchingManager>.I;
		i.OnChangeMemberStatus = (Action<LoungeMemberStatus>)Delegate.Remove(i.OnChangeMemberStatus, new Action<LoungeMemberStatus>(OnChangeMemberStatus));
		base._OnDestroy();
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			StartCoroutine(ResumeApp());
		}
	}

	private IEnumerator ResumeApp()
	{
		while (MonoBehaviourSingleton<ClanMatchingManager>.I.isResume)
		{
			yield return null;
		}
		while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
		{
			yield return null;
		}
		if (!CheckLeavedOnResume())
		{
			DestoryMembersOnResume();
			yield return null;
			StartCoroutine(CreateMembersOnResume());
			yield return null;
			ResetAllMemberAction();
		}
	}

	private bool CheckLeavedOnResume()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
		{
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("MAIN_MENU_CLAN", null),
				new EventData("CLAN_AFK_KICKED", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			MonoBehaviourSingleton<ClanMatchingManager>.I.StopAFKCheck();
			return true;
		}
		return false;
	}

	private void DestoryMembersOnResume()
	{
		if (IHomePeople == null || IHomePeople.CastToLoungePeople().loungePlayers == null)
		{
			return;
		}
		for (int i = 0; i < IHomePeople.CastToLoungePeople().loungePlayers.Count; i++)
		{
			int userId = IHomePeople.CastToLoungePeople().loungePlayers[i].GetUserId();
			if (userId == 0)
			{
				continue;
			}
			if (MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(userId) == null)
			{
				IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
			}
			else if (MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus != null)
			{
				LoungeMemberStatus.MEMBER_STATUS status = MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus[userId].GetStatus();
				if (status == LoungeMemberStatus.MEMBER_STATUS.QUEST || status == LoungeMemberStatus.MEMBER_STATUS.FIELD)
				{
					IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userId);
				}
			}
		}
	}

	private IEnumerator CreateMembersOnResume()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
		{
			yield break;
		}
		List<PartyModel.SlotInfo> slots = MonoBehaviourSingleton<ClanMatchingManager>.I.partyData.slotInfos;
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].userInfo == null)
			{
				continue;
			}
			int userId = slots[i].userInfo.userId;
			if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id || MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus == null)
			{
				continue;
			}
			LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus[userId];
			if (loungeMemberStatus != null)
			{
				LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatus.GetStatus();
				if (status == LoungeMemberStatus.MEMBER_STATUS.LOUNGE || status == LoungeMemberStatus.MEMBER_STATUS.QUEST_READY)
				{
					IHomePeople.CastToLoungePeople().CreateLoungePlayer(slots[i], useMovingEntry: false);
					IHomePeople.CastToLoungePeople().ChangeEquipLoungePlayer(slots[i], useMovingEntry: false);
					yield return null;
				}
			}
		}
	}

	private void ResetAllMemberAction()
	{
		if (IHomePeople != null)
		{
			for (int i = 0; i < IHomePeople.CastToLoungePeople().loungePlayers.Count; i++)
			{
				IHomePeople.CastToLoungePeople().loungePlayers[i].ResetAction();
			}
		}
	}

	public void OnRecvRoomJoined(int userId)
	{
		if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			StartCoroutine(CreateCharacterRoomJoined(userId));
		}
	}

	public void OnRecvRoomLeaved(int id)
	{
		if (IHomePeople != null)
		{
			if (IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id))
			{
				PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<ClanMatchingManager>.I.GetSlotInfoByUserId(id);
				SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.LEAVED_LOUNGE, slotInfoByUserId.userInfo.name));
			}
			if (id != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				StartCoroutine(SendLoungeInfoForce());
			}
		}
	}

	public void OnRecvRoomMove(int id, Vector3 targetPos)
	{
		if (IHomePeople != null)
		{
			IHomePeople.CastToLoungePeople().MoveLoungePlayer(id, targetPos);
		}
	}

	public void OnRecvRoomPosition(int id, Vector3 targetPos, LOUNGE_ACTION_TYPE type)
	{
		if (IHomePeople != null)
		{
			IHomePeople.CastToLoungePeople().SetInitialPositionLoungePlayer(id, targetPos, type);
		}
	}

	public void OnRecvRoomAction(int cid, int aid)
	{
		if (IHomePeople == null)
		{
			return;
		}
		LoungePlayer loungePlayer = IHomePeople.CastToLoungePeople().GetLoungePlayer(cid);
		if (!(loungePlayer == null))
		{
			loungePlayer.ResetAFKTimer();
			switch (aid)
			{
			case 1:
				loungePlayer.OnRecvSit();
				break;
			case 2:
				loungePlayer.OnRecvStandUp();
				break;
			case 5:
				loungePlayer.OnRecvToEquip();
				break;
			case 4:
				loungePlayer.OnRecvToGacha();
				break;
			case 6:
				loungePlayer.OnRecvAFK();
				break;
			default:
				loungePlayer.OnRecvNone();
				break;
			}
		}
	}

	public void OnRecvChatMessage(int userId)
	{
		if (IHomePeople != null)
		{
			LoungePlayer loungePlayer = IHomePeople.CastToLoungePeople().GetLoungePlayer(userId);
			if (!(loungePlayer == null))
			{
				loungePlayer.ResetAFKTimer();
			}
		}
	}

	public void OnRecvRoomKick(int id)
	{
		if (IHomePeople != null)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id)
			{
				EventData[] autoEvents = new EventData[2]
				{
					new EventData("MAIN_MENU_CLAN", null),
					new EventData("CLAN_KICKED", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				MonoBehaviourSingleton<ClanMatchingManager>.I.StopAFKCheck();
			}
			else
			{
				IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id);
			}
		}
	}

	public void OnRecvRoomAFKKick(int id)
	{
		if (IHomePeople != null)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id)
			{
				EventData[] autoEvents = new EventData[2]
				{
					new EventData("MAIN_MENU_CLAN", null),
					new EventData("CLAN_AFK_KICKED", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				MonoBehaviourSingleton<ClanMatchingManager>.I.StopAFKCheck();
			}
			else
			{
				IHomePeople.CastToLoungePeople().DestroyLoungePlayer(id);
			}
		}
	}

	private IEnumerator SendLoungeInfoForce()
	{
		bool wait = true;
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo(delegate
		{
			wait = false;
		}, force: true);
		while (wait)
		{
			yield return null;
		}
	}

	private void SendRoomPosition(int cid)
	{
		if (IHomePeople != null && !(IHomePeople.selfChara == null))
		{
			Vector3 position = IHomePeople.selfChara._transform.position;
			LOUNGE_ACTION_TYPE actionType = IHomePeople.selfChara.GetActionType();
			MonoBehaviourSingleton<LoungeNetworkManager>.I.RoomPosition(cid, position, actionType);
		}
	}
}
