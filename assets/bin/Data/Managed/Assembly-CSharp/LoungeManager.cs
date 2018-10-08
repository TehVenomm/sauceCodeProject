using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeManager : MonoBehaviourSingleton<LoungeManager>
{
	private enum SE
	{
		WAVE = 40000363
	}

	private const float SendInfoSpan = 3600f;

	private SpanTimer sendInfoSpan;

	private Queue<LoungeAnnounce.AnnounceData> loungeAnnounceQueue = new Queue<LoungeAnnounce.AnnounceData>();

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

	public HomePeople HomePeople
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

	public void SetPointShop(bool isOpen, int bannerId)
	{
		IsPointShopOpen = isOpen;
		PointShopBannerId = bannerId;
	}

	public void OnRecvRoomJoined(int userId)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			this.StartCoroutine(CreateCharacterRoomJoined(userId));
		}
	}

	public void OnRecvRoomLeaved(int id)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (!(HomePeople == null))
		{
			if (HomePeople.DestroyLoungePlayer(id))
			{
				LoungeModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(id);
				SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.LEAVED_LOUNGE, slotInfoByUserId.userInfo.name));
			}
			if (id != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				this.StartCoroutine(SendLoungeInfoForce());
			}
		}
	}

	public void OnRecvRoomMove(int id, Vector3 targetPos)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (!(HomePeople == null))
		{
			HomePeople.MoveLoungePlayer(id, targetPos);
		}
	}

	public void OnRecvRoomPosition(int id, Vector3 targetPos, LOUNGE_ACTION_TYPE type)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (!(HomePeople == null))
		{
			HomePeople.SetInitialPositionLoungePlayer(id, targetPos, type);
		}
	}

	public void OnRecvRoomAction(int cid, int aid)
	{
		if (!(HomePeople == null))
		{
			LoungePlayer loungePlayer = HomePeople.GetLoungePlayer(cid);
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
	}

	public void OnRecvChatMessage(int userId)
	{
		if (!(HomePeople == null))
		{
			LoungePlayer loungePlayer = HomePeople.GetLoungePlayer(userId);
			if (!(loungePlayer == null))
			{
				loungePlayer.ResetAFKTimer();
			}
		}
	}

	public void OnRecvRoomKick(int id)
	{
		if (!(HomePeople == null))
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id)
			{
				EventData[] autoEvents = new EventData[2]
				{
					new EventData("MAIN_MENU_LOUNGE", null),
					new EventData("LOUNGE_KICKED", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			}
			else
			{
				HomePeople.DestroyLoungePlayer(id);
			}
		}
	}

	public void SetLoungeQuestBalloon(bool request)
	{
		NeedLoungeQuestBalloonUpdate = request;
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
			yield return (object)null;
		}
		HomeCamera = this.get_gameObject().AddComponent<HomeCamera>();
		HomePeople = this.get_gameObject().AddComponent<HomePeople>();
		HomeFeatureBanner = this.get_gameObject().AddComponent<HomeFeatureBanner>();
		TableSet = this.get_gameObject().AddComponent<LoungeTableSet>();
		while (!HomeCamera.isInitialized || !HomePeople.isInitialized || !TableSet.isInitialized)
		{
			yield return (object)null;
		}
		LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
		i.OnChangeMemberStatus = (Action<LoungeMemberStatus>)Delegate.Combine(i.OnChangeMemberStatus, new Action<LoungeMemberStatus>(OnChangeMemberStatus));
		if (LoungeMatchingManager.IsValidInLounge())
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInLounge();
		}
		IsInitialized = true;
		yield return (object)this.StartCoroutine(SendLoungeInfoForce());
		yield return (object)this.StartCoroutine(CreateLoungePlayerFromSlotInfo());
		yield return (object)this.StartCoroutine(LoadSE());
		PlayWaveSound();
	}

	private IEnumerator LoadSE()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		int[] seList = (int[])Enum.GetValues(typeof(SE));
		int[] array = seList;
		foreach (int seId in array)
		{
			loadQueue.CacheSE(seId, null);
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
	}

	private void PlayWaveSound()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Transform val = Utility.CreateGameObject("WaveAudioObjectPos", base._transform, -1);
		val.set_position(MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.waveSoundPoint);
		SoundManager.PlayLoopSE(40000363, null, val);
	}

	private IEnumerator CreateLoungePlayerFromSlotInfo()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
		{
			List<LoungeModel.SlotInfo> data = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
			for (int i = 0; i < data.Count; i++)
			{
				if (data[i].userInfo != null && data[i].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
				{
					if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
					{
						break;
					}
					LoungeMemberStatus status = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[data[i].userInfo.userId];
					LoungeMemberStatus.MEMBER_STATUS partyStatus = status.GetStatus();
					if (partyStatus == LoungeMemberStatus.MEMBER_STATUS.LOUNGE || partyStatus == LoungeMemberStatus.MEMBER_STATUS.QUEST_READY)
					{
						HomePeople.CreateLoungePlayer(data[i], false, false);
						yield return (object)null;
					}
				}
			}
		}
	}

	private IEnumerator CreateCharacterRoomJoined(int userId)
	{
		yield return (object)this.StartCoroutine(SendLoungeInfoForce());
		LoungeModel.SlotInfo slot = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId);
		if (slot != null && HomePeople != null && HomePeople.CreateLoungePlayer(slot, true, false))
		{
			SetAnnounce(new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.JOIN_LOUNGE, slot.userInfo.name));
			if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<LoungeNetworkManager>.I.JoinNotification(slot.userInfo);
			}
		}
	}

	private void OnChangeMemberStatus(LoungeMemberStatus status)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
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
			HomePeople.DestroyLoungePlayer(userId);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
			SendRoomPosition(userId);
			NeedLoungeQuestBalloonUpdate = true;
			this.StartCoroutine(CreatePlayerOnChangedStatus(userId));
			break;
		}
	}

	private IEnumerator CreatePlayerOnChangedStatus(int userId)
	{
		yield return (object)SendLoungeInfoForce();
		LoungeModel.SlotInfo slot = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId);
		HomePeople.CreateLoungePlayer(slot, true, true);
	}

	private void CreatePartyAnnounce(int userId)
	{
		NeedLoungeQuestBalloonUpdate = true;
		LoungeModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId);
		LoungeAnnounce.AnnounceData announce = new LoungeAnnounce.AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE.CREATED_PARTY, slotInfoByUserId.userInfo.name);
		SetAnnounce(announce);
	}

	private void SetAnnounce(LoungeAnnounce.AnnounceData data)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		if (!object.ReferenceEquals(data, null))
		{
			loungeAnnounceQueue.Enqueue(data);
			if (object.ReferenceEquals(loungeAnnounceCoroutine, null))
			{
				loungeAnnounceCoroutine = this.StartCoroutine(ShowAnnounce());
			}
		}
	}

	private unsafe IEnumerator ShowAnnounce()
	{
		LoungeAnnounce announce = MonoBehaviourSingleton<UIManager>.I.loungeAnnounce;
		if (announce == null)
		{
			loungeAnnounceCoroutine = null;
		}
		else
		{
			while (loungeAnnounceQueue.Count > 0)
			{
				LoungeAnnounce.AnnounceData annouceData = loungeAnnounceQueue.Dequeue();
				bool wait = true;
				announce.Play(annouceData, new Action((object)/*Error near IL_0085: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				while (wait)
				{
					yield return (object)null;
				}
				yield return (object)new WaitForSeconds(0.3f);
			}
			loungeAnnounceCoroutine = null;
		}
	}

	private void Update()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (sendInfoSpan.IsReady())
		{
			this.StartCoroutine(SendLoungeInfoForce());
		}
	}

	protected override void _OnDestroy()
	{
		LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
		i.OnChangeMemberStatus = (Action<LoungeMemberStatus>)Delegate.Remove(i.OnChangeMemberStatus, new Action<LoungeMemberStatus>(OnChangeMemberStatus));
		base._OnDestroy();
	}

	private void OnApplicationPause(bool pause)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (!pause)
		{
			this.StartCoroutine(ResumeApp());
		}
	}

	private IEnumerator ResumeApp()
	{
		while (MonoBehaviourSingleton<LoungeMatchingManager>.I.isResume)
		{
			yield return (object)null;
		}
		while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
		{
			yield return (object)null;
		}
		if (!CheckLeavedOnResume())
		{
			DestoryMembersOnResume();
			yield return (object)null;
			this.StartCoroutine(CreateMembersOnResume());
			yield return (object)null;
			ResetAllMemberAction();
		}
	}

	private bool CheckLeavedOnResume()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("MAIN_MENU_LOUNGE", null),
				new EventData("LOUNGE_KICKED", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			MonoBehaviourSingleton<LoungeMatchingManager>.I.StopAFKCheck();
			return true;
		}
		return false;
	}

	private void DestoryMembersOnResume()
	{
		if (!(HomePeople == null) && HomePeople.loungePlayers != null)
		{
			for (int i = 0; i < HomePeople.loungePlayers.Count; i++)
			{
				int userId = HomePeople.loungePlayers[i].GetUserId();
				if (userId != 0)
				{
					LoungeModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(userId);
					if (slotInfoByUserId == null)
					{
						HomePeople.DestroyLoungePlayer(userId);
					}
					else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
					{
						LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
						LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatus.GetStatus();
						if (status == LoungeMemberStatus.MEMBER_STATUS.QUEST || status == LoungeMemberStatus.MEMBER_STATUS.FIELD)
						{
							HomePeople.DestroyLoungePlayer(userId);
						}
					}
				}
			}
		}
	}

	private IEnumerator CreateMembersOnResume()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
		{
			List<LoungeModel.SlotInfo> slots = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
			for (int i = 0; i < slots.Count; i++)
			{
				if (slots[i].userInfo != null)
				{
					int userId = slots[i].userInfo.userId;
					if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id && MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
					{
						LoungeMemberStatus status = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
						if (status != null)
						{
							LoungeMemberStatus.MEMBER_STATUS partyStatus = status.GetStatus();
							if (partyStatus == LoungeMemberStatus.MEMBER_STATUS.LOUNGE || partyStatus == LoungeMemberStatus.MEMBER_STATUS.QUEST_READY)
							{
								HomePeople.CreateLoungePlayer(slots[i], false, true);
								yield return (object)null;
							}
						}
					}
				}
			}
		}
	}

	private void ResetAllMemberAction()
	{
		if (!(HomePeople == null))
		{
			for (int i = 0; i < HomePeople.loungePlayers.Count; i++)
			{
				HomePeople.loungePlayers[i].ResetAction();
			}
		}
	}

	private IEnumerator SendLoungeInfoForce()
	{
		bool wait = true;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
		{
			((_003CSendLoungeInfoForce_003Ec__IteratorFC)/*Error near IL_002d: stateMachine*/)._003Cwait_003E__0 = false;
		}, true);
		while (wait)
		{
			yield return (object)null;
		}
	}

	private void SendRoomPosition(int cid)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (!(HomePeople == null) && !(HomePeople.selfChara == null))
		{
			Vector3 position = HomePeople.selfChara._transform.get_position();
			LOUNGE_ACTION_TYPE actionType = HomePeople.selfChara.GetActionType();
			MonoBehaviourSingleton<LoungeNetworkManager>.I.RoomPosition(cid, position, actionType);
		}
	}
}
