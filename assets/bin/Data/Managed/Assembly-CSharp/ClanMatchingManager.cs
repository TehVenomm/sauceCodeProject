using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClanMatchingManager : MonoBehaviourSingleton<ClanMatchingManager>
{
	private bool isChangeStarted;

	public Action<LoungeMemberStatus> OnChangeMemberStatus = delegate
	{
	};

	private Coroutine afkCoroutine;

	private const int RETRY_COUNT = 3;

	private const float RETRY_TIMER = 5f;

	private Coroutine popupNoticeBoardCoroutine;

	private readonly TimeSpan AFK_KICK_TIME = TimeSpan.FromMinutes(30.0);

	private string LastReadCharacterMessageId = "1";

	public bool UsingChatConnection;

	public string CachedMessageClanId = string.Empty;

	public int chatUpdateInterval = 5;

	public string LastReadMessageId = "1";

	public int UnreadMessageCount;

	private float UnreadCountSyncFixedTime = -1f;

	protected ChatClanConnection chatConnection;

	protected List<ClanChatMessageModel> chatMessagesDESC = new List<ClanChatMessageModel>();

	private const string stamp_start = "[STAMP]";

	private static DateTime dtepoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public ClanData clanData
	{
		get;
		private set;
	}

	public PartyModel.Party partyData
	{
		get;
		private set;
	}

	public List<PartyModel.Party> clanRoomParties
	{
		get;
		private set;
	}

	public ClanServer clanServerData
	{
		get;
		private set;
	}

	public LoungeMemberesStatus loungeMemberStatus
	{
		get;
		private set;
	}

	public UserClanData userClanData
	{
		get;
		private set;
	}

	public bool isKicked
	{
		get;
		private set;
	}

	public bool isResume
	{
		get;
		private set;
	}

	public bool isClanCreatedNow
	{
		get;
		private set;
	}

	public ClanSettings.CreateRequestParam createRequest
	{
		get;
		private set;
	}

	public List<ClanData> clans
	{
		get;
		private set;
	}

	public List<ClanData> scoutClans
	{
		get;
		private set;
	}

	public ClanSearchModel.RequestSendForm searchRequest
	{
		get;
		private set;
	}

	public bool EnableClanChat
	{
		get
		{
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat > 0)
			{
				return true;
			}
			return false;
		}
	}

	public event Action<ClanChatMessageModel> OnReceiveCharacterMessage;

	public ClanMatchingManager()
	{
		isClanCreatedNow = false;
		partyData = null;
		clanServerData = null;
	}

	public void OnCreateAnnounce()
	{
		isClanCreatedNow = false;
	}

	protected override void Awake()
	{
		base.Awake();
		this.get_gameObject().AddComponent<LoungeWebSocket>();
		this.get_gameObject().AddComponent<ClanNetworkManager>();
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			if (isResume)
			{
				ResumeConnect();
			}
		}
		else if (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
		{
			isResume = true;
			StopAFKCheck();
			ClearClan();
		}
	}

	public void Dirty()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE);
			if (isChangeStarted)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_START);
			}
		}
		AFKCheck();
	}

	private void UpdateClan(ClanData clan)
	{
		clanData = clan;
	}

	public void ClearClanData()
	{
		clanData = null;
	}

	private void UpdateParty(PartyModel.Party party, ClanServer clanServer)
	{
		if (partyData != null && partyData.status == 10 && (partyData.status == 100 || partyData.status == 105))
		{
			isChangeStarted = true;
		}
		partyData = party;
		if (clanServer != null)
		{
			clanServerData = clanServer;
			if (MonoBehaviourSingleton<ClanManager>.IsValid() && !MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
			{
				ConnectServer();
			}
		}
		if (loungeMemberStatus != null)
		{
			loungeMemberStatus.SyncPartyMember(partyData);
		}
	}

	public void ResumeConnect()
	{
		SendInfo(delegate
		{
			isResume = false;
		}, force: true);
	}

	public void TryConnect(bool connect, bool regist)
	{
		if (connect && regist)
		{
			Lounge_Model_RoomJoined lounge_Model_RoomJoined = new Lounge_Model_RoomJoined();
			lounge_Model_RoomJoined.id = 1005;
			lounge_Model_RoomJoined.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_RoomJoined);
			SetLoungeMemberesStatus();
			AFKCheck();
		}
	}

	public bool IsConnected()
	{
		return MonoBehaviourSingleton<ClanMatchingManager>.I.loungeMemberStatus != null;
	}

	private void SetLoungeMemberesStatus()
	{
		if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
		{
			List<Party_Model_RegisterACK.UserInfo> data = (from x in MonoBehaviourSingleton<ClanNetworkManager>.I.registerAck.GetConvertUserInfo()
			where GetSlotInfoByUserId(x.userId) != null
			select x).ToList();
			loungeMemberStatus = new LoungeMemberesStatus(data);
		}
	}

	public void SendRoomParty(Action<bool, List<PartyModel.Party>> call_back)
	{
		if (partyData != null)
		{
			Protocol.Send(ClanRoomQuestModel.URL, delegate(ClanRoomQuestModel ret)
			{
				UpdateRoomQuest(ret.result.parties);
				call_back(ret.Error == Error.None, ret.result.parties);
				if (ret.Error == Error.WRN_CLAN_NOT_JOINED && MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.clanData != null)
				{
					Kick(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
				}
			}, string.Empty);
		}
	}

	private void ClearClan()
	{
		partyData = null;
		clanServerData = null;
		loungeMemberStatus = null;
		if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanNetworkManager>.I.Close(1000);
		}
	}

	public static bool IsValidInClan()
	{
		return MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan();
	}

	public bool IsUserInClanBase(int user_id)
	{
		PartyModel.SlotInfo slotInfoByUserId = GetSlotInfoByUserId(user_id);
		return IsValidInClan() && slotInfoByUserId != null && slotInfoByUserId.userInfo != null;
	}

	public bool IsInClan()
	{
		return MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsInBase();
	}

	public string GetPartyId()
	{
		return (partyData == null) ? string.Empty : partyData.id;
	}

	public int GetSlotIndex(int user_id)
	{
		return (partyData == null) ? (-1) : partyData.slotInfos.FindIndex((PartyModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == user_id);
	}

	public PARTY_STATUS GetStatus()
	{
		return (PARTY_STATUS)((partyData != null) ? partyData.status : 0);
	}

	public PartyModel.SlotInfo GetSlotInfoByIndex(int idx)
	{
		if (partyData != null && idx < partyData.slotInfos.Count)
		{
			return partyData.slotInfos[idx];
		}
		return null;
	}

	public PartyModel.SlotInfo GetSlotInfoByUserId(int user_id)
	{
		int slotIndex = GetSlotIndex(user_id);
		return (slotIndex < 0) ? null : GetSlotInfoByIndex(slotIndex);
	}

	public int GetMemberCount()
	{
		if (partyData == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < partyData.slotInfos.Count; i++)
		{
			if (partyData.slotInfos[i].userInfo != null)
			{
				num++;
			}
		}
		return num;
	}

	public static string GenerateToken()
	{
		return Guid.NewGuid().ToString().Replace("-", string.Empty);
	}

	public void SendInfo(Action<bool> call_back, bool force = false)
	{
		if (force)
		{
			Protocol.Force(delegate
			{
				DoSendInfo(call_back);
			});
		}
		else
		{
			DoSendInfo(call_back);
		}
	}

	private void DoSendInfo(Action<bool> call_back)
	{
		Protocol.Send(ClanBaseInfoModel.URL, delegate(ClanBaseInfoModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				if (ret.result != null && ret.result.clanParty != null && ret.result.clanServer != null)
				{
					obj = true;
					UpdateParty(ret.result.clanParty, ret.result.clanServer);
					Dirty();
				}
				else
				{
					StopAFKCheck();
					ClearClan();
				}
			}
			else
			{
				StopAFKCheck();
				ClearClan();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendEnterToClanBase(Action<bool> call_back)
	{
		ClearClan();
		Protocol.Send(ClanEnterBaseModel.URL, delegate(ClanEnterBaseModel ret)
		{
			if (ret.Error == Error.None)
			{
				UpdateParty(ret.result.clanParty, ret.result.clanServer);
				MonoBehaviourSingleton<ClanManager>.I.SetNoticeBoardData(ret.result.clanNoticeBoard, isSaveBoardVersion: false);
				int @int = PlayerPrefs.GetInt("CLAN_BOARD_READ_ID_KEY", -1);
				if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && ret.result.clanNoticeBoard != null && ret.result.clanNoticeBoard.version > @int)
				{
					PopupNoticeBoard();
				}
				call_back(obj: true);
			}
		}, string.Empty);
	}

	public void SendLeaveFromClanBase(Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(obj: false);
		}
		else
		{
			Protocol.Send(ClanLeaveBaseModel.URL, delegate(ClanLeaveBaseModel ret)
			{
				bool obj = false;
				Error error = ret.Error;
				if (error == Error.None || error == Error.ERR_PARTY_NOT_FOUND_PARTY)
				{
					if (MonoBehaviourSingleton<ClanManager>.IsValid())
					{
						obj = true;
						DoLeaveFromClanBase(call_back, ret);
					}
					call_back(obj);
				}
				else
				{
					call_back(obj);
				}
			}, string.Empty);
		}
	}

	private void DoLeaveFromClanBase(Action<bool> call_back, ClanLeaveBaseModel ret)
	{
		SendLeavePacket();
		StopAFKCheck();
		ClearClan();
		Dirty();
	}

	public void SendInClanBase()
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			Lounge_Model_MemberLounge lounge_Model_MemberLounge = new Lounge_Model_MemberLounge();
			lounge_Model_MemberLounge.id = 1005;
			lounge_Model_MemberLounge.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_MemberLounge);
		}
	}

	public void SendStartField()
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected() && FieldManager.IsValidInField())
		{
			Lounge_Model_MemberField lounge_Model_MemberField = new Lounge_Model_MemberField();
			lounge_Model_MemberField.id = 1005;
			lounge_Model_MemberField.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			int.TryParse(MonoBehaviourSingleton<FieldManager>.I.GetFieldId(), out lounge_Model_MemberField.fid);
			lounge_Model_MemberField.fmid = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
			if (MonoBehaviourSingleton<PartyManager>.I.IsInParty())
			{
				int.TryParse(MonoBehaviourSingleton<PartyManager>.I.GetPartyId(), out lounge_Model_MemberField.pid);
				lounge_Model_MemberField.qid = (int)MonoBehaviourSingleton<PartyManager>.I.GetQuestId();
				lounge_Model_MemberField.h = (MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId() == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			}
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_MemberField);
		}
	}

	public void SendStartQuest(PartyModel.Party party)
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected() && MonoBehaviourSingleton<PartyManager>.IsValid())
		{
			Lounge_Model_MemberQuest lounge_Model_MemberQuest = new Lounge_Model_MemberQuest();
			lounge_Model_MemberQuest.id = 1005;
			lounge_Model_MemberQuest.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			int.TryParse(party.id, out lounge_Model_MemberQuest.pid);
			lounge_Model_MemberQuest.qid = party.quest.questId;
			lounge_Model_MemberQuest.h = (party.ownerUserId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_MemberQuest);
		}
	}

	public void SendStartArena(int arenaId)
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			Lounge_Model_MemberArena lounge_Model_MemberArena = new Lounge_Model_MemberArena();
			lounge_Model_MemberArena.id = 1005;
			lounge_Model_MemberArena.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			lounge_Model_MemberArena.aid = arenaId;
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_MemberArena);
		}
	}

	public void SetClanCreateRequest(ClanSettings.CreateRequestParam request = null)
	{
		if (request != null)
		{
			createRequest = request;
		}
		else if (createRequest == null)
		{
			ResetClanCreateRequest();
		}
	}

	public void ResetClanCreateRequest()
	{
		createRequest = new ClanSettings.CreateRequestParam();
	}

	public void SendCreate(Action<bool, Error> call_back)
	{
		ClearClan();
		ClanCreateModel.RequestSendForm requestSendForm = new ClanCreateModel.RequestSendForm();
		requestSendForm.name = createRequest.clanName;
		requestSendForm.jt = (createRequest.isLock ? 1 : 0);
		requestSendForm.lbl = (int)createRequest.label;
		requestSendForm.cmt = createRequest.comment;
		requestSendForm.tag = createRequest.clanTag;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(ClanCreateModel.URL, requestSendForm, delegate(ClanCreateModel ret)
		{
			bool arg = false;
			switch (ret.Error)
			{
			case Error.None:
				arg = true;
				UpdateParty(ret.result.clanParty, ret.result.clanServer);
				isClanCreatedNow = true;
				Dirty();
				break;
			}
			call_back(arg, ret.Error);
		}, string.Empty);
	}

	public int GetOwnerUserId()
	{
		return (partyData != null) ? partyData.ownerUserId : 0;
	}

	public void RequestApply(ClanApplyModel.RequestSendForm send_param, Action<bool> call_back)
	{
		Protocol.Send(ClanApplyModel.URL, send_param, delegate(ClanApplyModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (ret.result.clanParty != null && ret.result.clanServer != null)
				{
					UpdateParty(ret.result.clanParty, ret.result.clanServer);
				}
			}
			call_back(obj);
		}, string.Empty);
	}

	public void RequestApplyCancel(string clanId, Action<bool> call_back)
	{
		ClanApplyCancelModel.RequestSendForm requestSendForm = new ClanApplyCancelModel.RequestSendForm();
		requestSendForm.cId = clanId;
		Protocol.Send(ClanApplyCancelModel.URL, requestSendForm, delegate(ClanApplyCancelModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void RequestEdit(ClanEditClanModel.RequestSendForm clanSetting, Action<bool> call_back)
	{
		Protocol.Send(ClanEditClanModel.URL, clanSetting, delegate(ClanModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				UpdateClan(ret.result);
			}
			call_back(obj);
		}, string.Empty);
	}

	private void SendLeavePacket()
	{
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			Lounge_Model_RoomLeaved lounge_Model_RoomLeaved = new Lounge_Model_RoomLeaved();
			lounge_Model_RoomLeaved.id = 1005;
			lounge_Model_RoomLeaved.token = GenerateToken();
			lounge_Model_RoomLeaved.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_RoomLeaved, promise: false, delegate
			{
				ClearClan();
				StopAFKCheck();
				return true;
			});
		}
	}

	public void RequestLeave(Action<bool> call_back)
	{
		Protocol.Send(ClanLeaveModel.URL, delegate(ClanModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				SendLeavePacket();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void RequestKick(ClanKickModel.RequestSendForm send_param, Action<bool> call_back)
	{
		Protocol.Send(ClanKickModel.URL, send_param, delegate(ClanModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				Lounge_Model_RoomKick model = new Lounge_Model_RoomKick
				{
					id = 1005,
					token = GenerateToken(),
					cid = send_param.uId
				};
				MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(model);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void RequestEditMember(ClanEditMemberModel.RequestSendForm send_param, Action<bool> call_back)
	{
		Protocol.Send(ClanEditMemberModel.URL, send_param, delegate(ClanModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void RequestEditNoticeBoard(string msg, Action<bool> call_back)
	{
		ClanNoticeBoardUpdateModel.RequestSendForm requestSendForm = new ClanNoticeBoardUpdateModel.RequestSendForm();
		requestSendForm.body = msg;
		Protocol.Send(ClanNoticeBoardUpdateModel.URL, requestSendForm, delegate(ClanNoticeBoardUpdateModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			MonoBehaviourSingleton<ClanManager>.I.SetNoticeBoardData(ret.result.clanNoticeBoard, isSaveBoardVersion: true);
			call_back(obj);
		}, string.Empty);
	}

	public void RequestNoticeBoard(Action<bool> call_back)
	{
		Protocol.Send(ClanNoticeBoardModel.URL, delegate(ClanNoticeBoardModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			MonoBehaviourSingleton<ClanManager>.I.SetNoticeBoardData(ret.result.clanNoticeBoard, isSaveBoardVersion: true);
			call_back(obj);
		}, string.Empty);
	}

	private void PopupNoticeBoard()
	{
		if (popupNoticeBoardCoroutine != null)
		{
			this.StopCoroutine(popupNoticeBoardCoroutine);
			popupNoticeBoardCoroutine = null;
		}
		if (!IsAbleToPopupNoticeBoard())
		{
			popupNoticeBoardCoroutine = this.StartCoroutine(DelayPlay());
			return;
		}
		EventData[] autoEvents = new EventData[1]
		{
			new EventData("NOTICE_BOARD", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private IEnumerator DelayPlay()
	{
		int waitCount = 0;
		while (!IsAbleToPopupNoticeBoard() || waitCount < 3)
		{
			waitCount = (IsAbleToPopupNoticeBoard() ? (waitCount + 1) : 0);
			yield return null;
		}
		EventData[] autoEventData = new EventData[1]
		{
			new EventData("NOTICE_BOARD", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEventData);
	}

	private bool IsAbleToPopupNoticeBoard()
	{
		if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() != null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.levelUp.IsWaitDelay())
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.levelUp.IsPlaying())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop")
		{
			return true;
		}
		return false;
	}

	public static bool IsValidNotEmptyList()
	{
		return MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.clans != null && MonoBehaviourSingleton<ClanMatchingManager>.I.clans.Count > 0;
	}

	public static bool IsScoutValidNotEmptyList()
	{
		return MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans != null && MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans.Count > 0;
	}

	public void RequestSearch(Action<bool, Error> call_back, bool saveSettings)
	{
		clans = null;
		if (searchRequest == null)
		{
			ResetSearchRequest();
		}
		if (saveSettings)
		{
			SaveSearchSettings();
		}
		Protocol.Send(ClanSearchModel.URL, searchRequest, delegate(ClanSearchModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
				UpdateSearchList(ret.result);
			}
			call_back(arg, ret.Error);
		}, string.Empty);
	}

	public void SetSearchRequest(ClanSearchModel.RequestSendForm request)
	{
		if (request != null)
		{
			searchRequest = request;
		}
		else
		{
			ResetSearchRequest();
		}
	}

	public void ResetSearchRequest()
	{
		searchRequest = new ClanSearchModel.RequestSendForm();
	}

	private void UpdateRoomQuest(List<PartyModel.Party> parties)
	{
		clanRoomParties = parties;
	}

	public void LoadSearchRequestFromPrefs()
	{
		searchRequest = new ClanSearchModel.RequestSendForm();
		searchRequest.jt = PlayerPrefs.GetInt("CLAN_SEARCH_JT_KEY", -1);
		searchRequest.lbl = PlayerPrefs.GetInt("CLAN_SEARCH_LBL_KEY", 0);
		searchRequest.isCF = PlayerPrefs.GetInt("CLAN_SEARCH_ISCF_KEY", 0);
	}

	private void SaveSearchSettings()
	{
		PlayerPrefs.SetInt("CLAN_SEARCH_JT_KEY", searchRequest.jt);
		PlayerPrefs.SetInt("CLAN_SEARCH_LBL_KEY", searchRequest.lbl);
		PlayerPrefs.SetInt("CLAN_SEARCH_ISCF_KEY", searchRequest.isCF);
		PlayerPrefs.Save();
	}

	private void UpdateSearchList(List<ClanData> clans)
	{
		this.clans = clans;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SEARCH_ROOM_LIST);
	}

	public void RequestDetail(string clanId, Action<ClanDetailModel.Param> call_back)
	{
		ClanDetailModel.RequestSendForm requestSendForm = new ClanDetailModel.RequestSendForm();
		requestSendForm.cId = clanId;
		Protocol.Send(ClanDetailModel.URL, requestSendForm, delegate(ClanDetailModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				if (clanId == "0")
				{
					UpdateClan(ret.result.clan);
				}
				call_back(ret.result);
			}
		}, string.Empty);
	}

	public void RequestUserDetail(int uId, Action<UserClanData> call_back)
	{
		ClanUserDetailModel.RequestSendForm requestSendForm = new ClanUserDetailModel.RequestSendForm();
		requestSendForm.uId = uId;
		Protocol.Send(ClanUserDetailModel.URL, requestSendForm, delegate(ClanUserDetailModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				userClanData = ret.result;
				call_back(ret.result);
			}
		}, string.Empty);
	}

	public void Kick(int userId)
	{
		if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			PartyModel.SlotInfo slotInfoByUserId = GetSlotInfoByUserId(userId);
			partyData.slotInfos.Remove(slotInfoByUserId);
		}
		if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			ClearClan();
			MonoBehaviourSingleton<UserInfoManager>.I.LeaveClan();
			StopAFKCheck();
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanManager>.I.OnRecvRoomKick(userId);
		}
	}

	public void AFKKick(int userId)
	{
		if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			ClearClan();
			StopAFKCheck();
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanManager>.I.OnRecvRoomAFKKick(userId);
		}
	}

	public ClanNetworkManager.ConnectData GetWebSockConnectData()
	{
		if (partyData == null || clanServerData == null)
		{
			Log.Error(LOG.WEBSOCK, "NotFound ConnectData");
			return null;
		}
		int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		int slotIndex = GetSlotIndex(id);
		if (slotIndex < 0)
		{
			return null;
		}
		ClanNetworkManager.ConnectData connectData = new ClanNetworkManager.ConnectData();
		connectData.path = clanServerData.wsHost;
		connectData.ports = clanServerData.wsPorts;
		connectData.fromId = id;
		connectData.ackPrefix = slotIndex;
		connectData.roomId = partyData.id;
		connectData.owner = partyData.ownerUserId;
		connectData.ownerToken = clanServerData.token;
		connectData.uid = id;
		connectData.signature = clanServerData.signature;
		return connectData;
	}

	public void ConnectServer()
	{
		ClanNetworkManager.ConnectData webSockConnectData = GetWebSockConnectData();
		if (webSockConnectData == null)
		{
			TryConnect(connect: false, regist: false);
		}
		else if (!MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
		{
			TryConnect(connect: false, regist: false);
		}
		else
		{
			MonoBehaviourSingleton<ClanNetworkManager>.I.ConnectAndRegist(webSockConnectData, delegate(bool is_connect, bool is_regist)
			{
				TryConnect(is_connect, is_regist);
			});
		}
	}

	public void OnRecvMemberMoveLounge(int userId)
	{
		if (this.loungeMemberStatus == null)
		{
			return;
		}
		LoungeMemberStatus loungeMemberStatus = this.loungeMemberStatus[userId];
		LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatus.GetStatus();
		if (loungeMemberStatus != null)
		{
			loungeMemberStatus.ToLounge();
			OnChangeMemberStatus.SafeInvoke(loungeMemberStatus);
			if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
			}
		}
		AFKCheck();
	}

	public void OnRecvMemberMoveField(Lounge_Model_MemberField model)
	{
		if (this.loungeMemberStatus == null)
		{
			return;
		}
		LoungeMemberStatus loungeMemberStatus = this.loungeMemberStatus[model.cid];
		LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatus.GetStatus();
		if (loungeMemberStatus != null)
		{
			loungeMemberStatus.ToField(model.fid.ToString(), model.fmid, model.pid.ToString(), model.qid, model.h);
			OnChangeMemberStatus.SafeInvoke(loungeMemberStatus);
			if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
			}
		}
		AFKCheck();
	}

	public void OnRecvMemberMoveQuest(Lounge_Model_MemberQuest model)
	{
		if (this.loungeMemberStatus == null)
		{
			return;
		}
		LoungeMemberStatus loungeMemberStatus = this.loungeMemberStatus[model.cid];
		LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatus.GetStatus();
		if (loungeMemberStatus != null)
		{
			loungeMemberStatus.ToQuest(model.pid.ToString(), model.qid, model.h);
			OnChangeMemberStatus.SafeInvoke(loungeMemberStatus);
			if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
			}
		}
		AFKCheck();
	}

	public void OnRecvMemberMoveArena(Lounge_Model_MemberArena model)
	{
		if (this.loungeMemberStatus == null)
		{
			return;
		}
		LoungeMemberStatus loungeMemberStatus = this.loungeMemberStatus[model.cid];
		LoungeMemberStatus.MEMBER_STATUS status = loungeMemberStatus.GetStatus();
		loungeMemberStatus.ToArena(model.aid);
		if (loungeMemberStatus != null)
		{
			OnChangeMemberStatus.SafeInvoke(loungeMemberStatus);
			if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
			}
		}
		AFKCheck();
	}

	public void DebugSendRoomPartyAFKKick(int userId)
	{
		SendRoomPartyAFKKick(userId, delegate
		{
			IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
			currentIHomeManager.IHomePeople.CastToLoungePeople()?.DestroyLoungePlayer(userId);
		});
	}

	public void SendRoomPartyAFKKick(int kickedUserId, Action<bool> call_back = null)
	{
		ClanKickBaseModel.RequestSendForm requestSendForm = new ClanKickBaseModel.RequestSendForm();
		requestSendForm.uId = kickedUserId;
		Protocol.Send(ClanKickBaseModel.URL, requestSendForm, delegate(ClanKickBaseModel ret)
		{
			bool flag = ret.Error == Error.None;
			if (flag)
			{
				if (ret.Error == Error.None)
				{
					PartyModel.SlotInfo slotInfo = ret.result.clanParty.slotInfos.Find((PartyModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == kickedUserId);
					if (slotInfo != null)
					{
						loungeMemberStatus[kickedUserId].UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
					}
					else
					{
						Lounge_Model_AFK_Kick model = new Lounge_Model_AFK_Kick
						{
							id = 1005,
							cid = kickedUserId,
							token = GenerateToken()
						};
						MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(model);
					}
					UpdateParty(ret.result.clanParty, null);
				}
				else
				{
					call_back(flag);
				}
			}
			call_back(flag);
		}, string.Empty);
	}

	private void AFKCheck()
	{
		if (afkCoroutine != null)
		{
			this.StopCoroutine(afkCoroutine);
		}
		afkCoroutine = this.StartCoroutine(DoAFKCheck());
	}

	private IEnumerator DoAFKCheck()
	{
		if (!IsValidInClan() || loungeMemberStatus == null)
		{
			yield break;
		}
		List<LoungeMemberStatus> allMember = loungeMemberStatus.GetAll();
		if (allMember.IsNullOrEmpty() || !(from x in allMember
		where x.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id
		select x).Any())
		{
			yield break;
		}
		LoungeMemberStatus fastest = (from x in allMember
		where x.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id
		where x.GetStatus() == LoungeMemberStatus.MEMBER_STATUS.LOUNGE
		orderby x.lastExecTime
		select x).FirstOrDefault();
		if (fastest != null)
		{
			double waitTime = (AFK_KICK_TIME - (TimeManager.GetNow().ToUniversalTime() - fastest.lastExecTime)).TotalSeconds;
			if (waitTime > 0.0)
			{
				yield return (object)new WaitForSeconds((float)waitTime);
			}
			bool wait = true;
			Protocol.Force(delegate
			{
				SendRoomPartyAFKKick(fastest.userId, delegate
				{
					IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
					currentIHomeManager.IHomePeople.CastToLoungePeople()?.DestroyLoungePlayer(fastest.userId);
					wait = false;
				});
			});
			while (wait)
			{
				yield return null;
			}
			AFKCheck();
		}
	}

	public void StopAFKCheck()
	{
		if (afkCoroutine != null)
		{
			this.StopCoroutine(afkCoroutine);
		}
	}

	public ChatClanConnection GetChatConnection()
	{
		if (chatConnection == null)
		{
			chatConnection = new ChatClanConnection();
			LastReadMessageId = PlayerPrefs.GetString("CLAN_CHAT_LAST_READ_ID_KEY", "1");
		}
		return chatConnection;
	}

	public void ChatResetCache()
	{
		chatMessagesDESC.Clear();
	}

	public string ChatGetLatestCacheId()
	{
		if (chatMessagesDESC.Count > 0)
		{
			return chatMessagesDESC[0].id;
		}
		return string.Empty;
	}

	public void OnReadMessage(string messageId)
	{
		long result = 0L;
		long result2 = 0L;
		if (long.TryParse(LastReadMessageId, out result) && long.TryParse(messageId, out result2) && result2 > result)
		{
			LastReadMessageId = messageId;
			PlayerPrefs.SetString("CLAN_CHAT_LAST_READ_ID_KEY", LastReadMessageId);
			PlayerPrefs.Save();
			UnreadMessageCount = 0;
		}
	}

	public void UpdateUnreadMessage()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.get_internetReachability() != 0 && EnableClanChat && !UsingChatConnection && (!(UnreadCountSyncFixedTime > 0f) || !(UnreadCountSyncFixedTime + (float)chatUpdateInterval > Time.get_fixedTime())))
		{
			UnreadCountSyncFixedTime = Time.get_fixedTime();
			UpdateUnreadMessageCount(delegate
			{
				UpdateUnreadCharacterMessage(delegate
				{
				});
			});
		}
	}

	private void UpdateUnreadMessageCount(Action callback)
	{
		ClanChatMessageUpdateModel.RequestSendForm send_form = new ClanChatMessageUpdateModel.RequestSendForm();
		send_form.cLatestId = LastReadMessageId;
		Protocol.Try(delegate
		{
			UsingChatConnection = true;
			Protocol.Send(ClanChatMessageUpdateModel.URL, send_form, delegate(ClanChatMessageUpdateModel ret)
			{
				UsingChatConnection = false;
				if (ret.Error == Error.None)
				{
					int num = 0;
					for (int i = 0; i < ret.result.messages.Count; i++)
					{
						if (ret.result.messages[i].userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
						{
							num++;
						}
					}
					UnreadMessageCount = num;
					checkMessageType(ret.result.messages);
				}
				if (callback != null)
				{
					callback();
				}
			}, string.Empty);
		});
	}

	private void UpdateUnreadCharacterMessage(Action callback)
	{
		ClanChatMessageUpdateModel.RequestSendForm send_form = new ClanChatMessageUpdateModel.RequestSendForm();
		send_form.cLatestId = LastReadCharacterMessageId;
		Protocol.Try(delegate
		{
			UsingChatConnection = true;
			Protocol.Send(ClanChatMessageUpdateModel.URL, send_form, delegate(ClanChatMessageUpdateModel ret)
			{
				UsingChatConnection = false;
				if (ret.Error == Error.None)
				{
					checkMessageType(ret.result.messages);
					for (int num = ret.result.messages.Count - 1; num >= 0; num--)
					{
						if (this.OnReceiveCharacterMessage != null)
						{
							this.OnReceiveCharacterMessage(ret.result.messages[num]);
						}
						LastReadCharacterMessageId = ret.result.messages[num].id;
					}
				}
				if (callback != null)
				{
					callback();
				}
			}, string.Empty);
		});
	}

	public void ChatMessage(string message)
	{
		if (EnableClanChat)
		{
			string cLatestId = "0";
			if (chatMessagesDESC != null && chatMessagesDESC.Count > 0)
			{
				cLatestId = chatMessagesDESC[0].id;
			}
			ClanChatPostMessageModel.RequestSendForm send_form = new ClanChatPostMessageModel.RequestSendForm();
			string text = message;
			if (text.Length > 32)
			{
				text = text.Substring(0, 32);
			}
			send_form.message = text;
			send_form.cLatestId = cLatestId;
			Protocol.Try(delegate
			{
				UsingChatConnection = true;
				Protocol.Send(ClanChatPostMessageModel.URL, send_form, delegate(ClanChatPostMessageModel ret)
				{
					UsingChatConnection = false;
					bool flag = false;
					if (ret.Error == Error.None)
					{
						flag = true;
						List<ClanChatMessageModel> collection = chatMessagesDESC;
						chatMessagesDESC = ret.result.messages;
						chatMessagesDESC.AddRange(collection);
						chatConnection.OnAfterSendUserMessage();
					}
				}, string.Empty);
			});
		}
	}

	public void ChatStamp(int stamp_id)
	{
		ChatMessage(convertStampIdToString(stamp_id));
	}

	public void ChatGetNewMessage(int maxDispatchNum, string latestId = "")
	{
		if (EnableClanChat && !UsingChatConnection && (string.IsNullOrEmpty(latestId) || !dispatchEventNewMessageFromChache(maxDispatchNum, latestId)))
		{
			chatGetLatestMessageFromServer(delegate
			{
				dispatchEventNewMessageFromChache(maxDispatchNum, latestId);
			});
		}
	}

	private bool dispatchEventNewMessageFromChache(int maxDispatchNum, string latestId = "")
	{
		bool flag = false;
		if (string.IsNullOrEmpty(latestId))
		{
			flag = true;
		}
		int num = 0;
		for (int num2 = chatMessagesDESC.Count - 1; num2 >= 0; num2--)
		{
			if (flag)
			{
				dispatchEventChatReceiveNew(chatMessagesDESC[num2]);
				num++;
				if (num >= maxDispatchNum)
				{
					break;
				}
			}
			else if (chatMessagesDESC[num2].id == latestId)
			{
				flag = true;
			}
		}
		if (num > 0)
		{
			return true;
		}
		return false;
	}

	private void chatGetLatestMessageFromServer(Action callback)
	{
		if (EnableClanChat)
		{
			string cLatestId = "0";
			if (chatMessagesDESC != null && chatMessagesDESC.Count > 0)
			{
				cLatestId = chatMessagesDESC[0].id;
			}
			CachedMessageClanId = MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
			ClanChatMessageUpdateModel.RequestSendForm send_form = new ClanChatMessageUpdateModel.RequestSendForm();
			send_form.cLatestId = cLatestId;
			Protocol.Try(delegate
			{
				UsingChatConnection = true;
				Protocol.Send(ClanChatMessageUpdateModel.URL, send_form, delegate(ClanChatMessageUpdateModel ret)
				{
					UsingChatConnection = false;
					bool flag = false;
					if (ret.Error == Error.None)
					{
						flag = true;
						int count = chatMessagesDESC.Count;
						List<ClanChatMessageModel> collection = chatMessagesDESC;
						chatMessagesDESC = ret.result.messages;
						chatMessagesDESC.AddRange(collection);
						chatUpdateInterval = ret.result.updateInterval;
						if (callback != null && chatMessagesDESC.Count > count)
						{
							callback();
						}
					}
				}, string.Empty);
			});
		}
	}

	private void dispatchEventChatReceiveNew(ClanChatMessageModel model)
	{
		if (model.type != 1)
		{
			chatConnection.OnReceiveNotification(convertSystemMessage(model), model.id);
			return;
		}
		int num = convertStringToStampId(model.body);
		if (num >= 0)
		{
			chatConnection.OnReceiveStamp(model.userId, convertUsername(model), num, model.id);
		}
		else
		{
			chatConnection.OnReceiveMessage(model.userId, convertUsername(model), model.body, model.id);
		}
	}

	private void checkMessageType(List<ClanChatMessageModel> messages)
	{
		if (messages.IsNullOrEmpty())
		{
			return;
		}
		bool flag = false;
		int num = PlayerPrefs.GetInt("CLAN_CHAT_READ_ID_KEY", -1);
		for (int num2 = messages.Count - 1; num2 >= 0; num2--)
		{
			ClanChatMessageModel clanChatMessageModel = messages[num2];
			int num3 = int.Parse(clanChatMessageModel.id);
			if (num3 > num)
			{
				bool flag2 = false;
				switch (clanChatMessageModel.type)
				{
				case 9:
					StartRequestClanData();
					if (MonoBehaviourSingleton<UIAnnounceBand>.IsValid())
					{
						flag2 = true;
						MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(clanChatMessageModel.body, StringTable.Get(STRING_CATEGORY.CLAN, 3u));
					}
					break;
				case 8:
					flag2 = true;
					if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
					{
						if (clanChatMessageModel.value > 0)
						{
							StartRankUp();
						}
						else if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.clanCreate != null)
						{
							MonoBehaviourSingleton<UIManager>.I.clanCreate.Play(isForcePlay: false, null, UIClanCreateAnnounce.eType.LevelUp);
							StartRequestClanData(delegate
							{
								PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level);
								PlayerPrefs.Save();
							});
						}
					}
					break;
				}
				if (flag2)
				{
					num = num3;
					PlayerPrefs.SetInt("CLAN_CHAT_READ_ID_KEY", num3);
					flag = true;
				}
			}
		}
		if (flag)
		{
			PlayerPrefs.Save();
		}
	}

	public void StartRequestClanData(Action cb = null)
	{
		this.StartCoroutine(RequestClanData(cb));
	}

	private IEnumerator RequestClanData(Action cb)
	{
		bool wait = true;
		Protocol.Force(delegate
		{
			RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, delegate(UserClanData userClandata)
			{
				userClanData = userClandata;
				MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClandata);
				wait = false;
			});
		});
		while (wait)
		{
			yield return null;
		}
		cb?.Invoke();
	}

	public void StartRankUp()
	{
		this.StartCoroutine(_RankUp());
	}

	private IEnumerator _RankUp()
	{
		while (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ClanTop")
		{
			yield return null;
		}
		while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return null;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ClanTop", this.get_gameObject(), "ROOM_RANKUP");
	}

	public void ChatGetOldMessage(int maxDispatchNum, string oldestId = "")
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat != 0 && !UsingChatConnection && (string.IsNullOrEmpty(oldestId) || !dispatchChatOldMessageFromChache(maxDispatchNum, oldestId)))
		{
			chatGetOldestMessageFromServer(delegate
			{
				dispatchChatOldMessageFromChache(maxDispatchNum, oldestId);
			});
		}
	}

	private bool dispatchChatOldMessageFromChache(int maxDispatchNum, string oldestId = "")
	{
		bool flag = false;
		if (string.IsNullOrEmpty(oldestId))
		{
			flag = true;
		}
		int num = 0;
		for (int i = 0; i < chatMessagesDESC.Count; i++)
		{
			if (flag)
			{
				dispatchEventChatReceiveOld(chatMessagesDESC[i]);
				num++;
				if (num >= maxDispatchNum)
				{
					break;
				}
			}
			else if (chatMessagesDESC[i].id == oldestId)
			{
				flag = true;
			}
		}
		if (num > 0)
		{
			return true;
		}
		return false;
	}

	private void chatGetOldestMessageFromServer(Action callback)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat != 0)
		{
			string fromId = "0";
			if (chatMessagesDESC != null && chatMessagesDESC.Count > 0)
			{
				fromId = chatMessagesDESC[chatMessagesDESC.Count - 1].id;
			}
			ClanChatMessageHistoryModel.RequestSendForm send_form = new ClanChatMessageHistoryModel.RequestSendForm();
			send_form.fromId = fromId;
			Protocol.Try(delegate
			{
				Protocol.Send(ClanChatMessageHistoryModel.URL, send_form, delegate(ClanChatMessageUpdateModel ret)
				{
					bool flag = false;
					if (ret.Error == Error.None)
					{
						flag = true;
						chatUpdateInterval = ret.result.updateInterval;
						if (ret.result.messages.Count > 0)
						{
							chatMessagesDESC.AddRange(ret.result.messages);
							if (callback != null)
							{
								callback();
							}
						}
					}
				}, string.Empty);
			});
		}
	}

	private void dispatchEventChatReceiveOld(ClanChatMessageModel model)
	{
		if (model.type != 1)
		{
			chatConnection.OnReceiveNotificationOld(convertSystemMessage(model), model.id);
			return;
		}
		int num = convertStringToStampId(model.body);
		if (num >= 0)
		{
			chatConnection.OnReceiveStampOld(model.userId, convertUsername(model), num, model.id);
		}
		else
		{
			chatConnection.OnReceiveMessageOld(model.userId, convertUsername(model), model.body, model.id);
		}
	}

	public static string convertStampIdToString(int stamp_id)
	{
		return "[STAMP]" + stamp_id.ToString();
	}

	public static int convertStringToStampId(string message)
	{
		if (message == null)
		{
			return -1;
		}
		if (message.StartsWith("[STAMP]"))
		{
			string text = message.Substring("[STAMP]".Length, message.Length - "[STAMP]".Length);
			if (string.IsNullOrEmpty(text))
			{
				return -1;
			}
			int result = -1;
			if (int.TryParse(text, out result))
			{
				return result;
			}
			return -1;
		}
		return -1;
	}

	private string convertUsername(ClanChatMessageModel model)
	{
		return appendDate(model.userName, model.createdAt);
	}

	private string convertSystemMessage(ClanChatMessageModel model)
	{
		return appendDate(model.body, model.createdAt);
	}

	private string appendDate(string txt, int createdAt)
	{
		return txt + dtepoc.AddSeconds(createdAt + 32400).ToString(" M/dd HH:mm");
	}

	public string ConvertDateIntToString(string txt, int timeAt)
	{
		return appendDate(txt, timeAt);
	}

	public void SendRequestList(Action<bool, List<FriendCharaInfo>> call_back)
	{
		Protocol.Send(ClanRequestListModel.URL, delegate(ClanRequestListModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			if (call_back != null)
			{
				call_back(arg, ret.result);
			}
		}, string.Empty);
	}

	public void SendAcceptRequest(int userId, Action<bool> call_back)
	{
		ClanAcceptRequestModel.RequestSendForm requestSendForm = new ClanAcceptRequestModel.RequestSendForm();
		requestSendForm.uId = userId;
		Protocol.Send(ClanAcceptRequestModel.URL, requestSendForm, delegate(ClanAcceptRequestModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			if (call_back != null)
			{
				call_back(obj);
			}
		}, string.Empty);
	}

	public void SendRejectRequest(int userId, Action<bool> call_back)
	{
		ClanRejectRequestModel.RequestSendForm requestSendForm = new ClanRejectRequestModel.RequestSendForm();
		requestSendForm.uId = userId;
		Protocol.Send(ClanRejectRequestModel.URL, requestSendForm, delegate(ClanRejectRequestModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			if (call_back != null)
			{
				call_back(obj);
			}
		}, string.Empty);
	}

	public void SendSymbolEditRequest(ClanSymbolData symbol, Action<bool> call_back)
	{
		ClanSymbolEditRequestModel.RequestSendForm requestSendForm = new ClanSymbolEditRequestModel.RequestSendForm();
		requestSendForm.mark = symbol.m;
		requestSendForm.markOption = symbol.mo;
		requestSendForm.frame = symbol.f;
		requestSendForm.frameOption = symbol.fo;
		requestSendForm.pattern = symbol.p;
		requestSendForm.patternOption = symbol.po;
		Protocol.Send(ClanSymbolEditRequestModel.URL, requestSendForm, delegate(ClanSymbolEditRequestModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			if (call_back != null)
			{
				call_back(obj);
			}
		}, string.Empty);
	}

	public void SendClanInvite(int userId, Action<bool> call_back)
	{
		ClanInviteModel.RequestSendForm requestSendForm = new ClanInviteModel.RequestSendForm();
		requestSendForm.uId = userId;
		Protocol.Send(ClanInviteModel.URL, requestSendForm, delegate(ClanInviteModel ret)
		{
			bool obj = false;
			switch (ret.Error)
			{
			case Error.None:
				obj = true;
				break;
			case Error.WRN_CLAN_NO_VACANCY:
				clanData.num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM;
				break;
			}
			if (call_back != null)
			{
				call_back(obj);
			}
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
		}, string.Empty);
	}

	public void SendClanInviteList(Action<bool> call_back)
	{
		Protocol.Send(ClanInviteListModel.URL, delegate(ClanInviteListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			scoutClans = ret.result;
			MonoBehaviourSingleton<UserInfoManager>.I.SetClanScoutNum(scoutClans.Count);
			if (call_back != null)
			{
				call_back(obj);
			}
		}, string.Empty);
	}

	public void RemoveClanScoutList(string cId)
	{
		scoutClans.RemoveAll((ClanData c) => c.cId.Contains(cId));
		MonoBehaviourSingleton<UserInfoManager>.I.SetClanScoutNum(scoutClans.Count);
	}

	public void SendClanAcceptInvite(int cId, Action<bool> call_back)
	{
		ClanAcceptInviteModel.RequestSendForm requestSendForm = new ClanAcceptInviteModel.RequestSendForm();
		requestSendForm.cId = cId;
		Protocol.Send(ClanAcceptInviteModel.URL, requestSendForm, delegate(ClanAcceptInviteModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (ret.result.clanParty != null && ret.result.clanServer != null)
				{
					UpdateParty(ret.result.clanParty, ret.result.clanServer);
				}
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendClanCancelInvite(int userId, Action<bool> call_back)
	{
		ClanCancelInviteModel.RequestSendForm requestSendForm = new ClanCancelInviteModel.RequestSendForm();
		requestSendForm.uId = userId;
		Protocol.Send(ClanCancelInviteModel.URL, requestSendForm, delegate(ClanCancelInviteModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			if (call_back != null)
			{
				call_back(obj);
			}
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
		}, string.Empty);
	}

	public void SendClanRejectInvite(int clanId, Action<bool> call_back)
	{
		ClanRejectInviteModel.RequestSendForm requestSendForm = new ClanRejectInviteModel.RequestSendForm();
		requestSendForm.cId = clanId;
		Protocol.Send(ClanRejectInviteModel.URL, requestSendForm, delegate(ClanRejectInviteModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}
}
