using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoungeMatchingManager : MonoBehaviourSingleton<LoungeMatchingManager>
{
	public class LoungeSetting
	{
		public bool isLock;

		public int level;

		public int total;

		public int reserveLimitLevel;

		public LoungeSetting(bool is_lock, int _level, int _total)
		{
			isLock = is_lock;
			level = _level;
			total = _total;
			reserveLimitLevel = level;
		}
	}

	private bool isChangeStarted;

	private Coroutine afkCoroutine;

	private string inviteValue = "";

	public Action<LoungeMemberStatus> OnChangeMemberStatus = delegate
	{
	};

	private ChatLoungeConnection connection;

	private const int RETRY_COUNT = 3;

	private const float RETRY_TIMER = 5f;

	private readonly TimeSpan AFK_KICK_TIME = TimeSpan.FromMinutes(30.0);

	public List<LoungeModel.Lounge> lounges
	{
		get;
		private set;
	}

	public List<PartyModel.Party> parties
	{
		get;
		private set;
	}

	public List<LoungeModel.SlotInfo> rallyInvite
	{
		get;
		private set;
	}

	public LoungeModel.Lounge loungeData
	{
		get;
		private set;
	}

	public LoungeModel.InviteFriendInfo inviteFriendInfo
	{
		get;
		private set;
	}

	public string InviteValue
	{
		get
		{
			return inviteValue;
		}
		set
		{
			inviteValue = value;
		}
	}

	public LoungeConditionSettings.CreateRequestParam createRequest
	{
		get;
		private set;
	}

	public LoungeSearchSettings.SearchRequestParam searchRequest
	{
		get;
		private set;
	}

	public List<FollowLoungeMember> followLoungeMember
	{
		get;
		private set;
	}

	public LoungeModel.LoungeServer loungeServerData
	{
		get;
		private set;
	}

	public LoungeModel.RandomMatchingInfo randomMatchingInfo
	{
		get;
		private set;
	}

	public List<int> firstMetUserIds
	{
		get;
		private set;
	}

	public LoungeMemberesStatus loungeMemberStatus
	{
		get;
		private set;
	}

	public bool isOpenLounge
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

	public LoungeSetting loungeSetting
	{
		get;
		private set;
	}

	public LoungeMatchingManager()
	{
		lounges = null;
		loungeData = null;
		loungeServerData = null;
	}

	protected override void Awake()
	{
		base.Awake();
		base.gameObject.AddComponent<LoungeWebSocket>();
		base.gameObject.AddComponent<LoungeNetworkManager>();
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
		else if (connection != null)
		{
			isResume = true;
			MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
			StopAFKCheck();
			ClearLounge();
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

	public void SetOpenLounge(bool isOpen)
	{
		isOpenLounge = isOpen;
	}

	private void UpdateLounge(LoungeModel.Lounge lounge, List<FollowLoungeMember> followLoungeMember, LoungeModel.LoungeServer loungeServer, LoungeModel.InviteFriendInfo inviteFriendInfo, List<int> firstMetUserIds)
	{
		if (loungeData != null && loungeData.status == 10 && (lounge.status == 100 || lounge.status == 105))
		{
			isChangeStarted = true;
		}
		this.inviteFriendInfo = inviteFriendInfo;
		this.firstMetUserIds = firstMetUserIds;
		loungeData = lounge;
		if (followLoungeMember != null)
		{
			this.followLoungeMember = followLoungeMember;
		}
		if (loungeServer != null)
		{
			loungeServerData = loungeServer;
			if (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
			{
				connection = MonoBehaviourSingleton<LoungeNetworkManager>.I.CreateChatConnection();
				MonoBehaviourSingleton<ChatManager>.I.CreateLoungeChat(connection);
				connection.Join(0, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
			}
		}
		if (loungeMemberStatus != null)
		{
			loungeMemberStatus.SyncLoungeMember(loungeData);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomJoined);
			SetLoungeMemberesStatus();
			AFKCheck();
		}
	}

	private void SetLoungeMemberesStatus()
	{
		if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			List<Party_Model_RegisterACK.UserInfo> data = (from x in MonoBehaviourSingleton<LoungeNetworkManager>.I.registerAck.GetConvertUserInfo()
				where GetSlotInfoByUserId(x.userId) != null
				select x).ToList();
			loungeMemberStatus = new LoungeMemberesStatus(data);
		}
	}

	private void ClearLounge()
	{
		loungeData = null;
		loungeServerData = null;
		isChangeStarted = false;
		randomMatchingInfo = null;
		loungeMemberStatus = null;
		connection = null;
	}

	private void UpdateLoungeList(List<LoungeModel.Lounge> lounges)
	{
		this.lounges = lounges;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SEARCH_ROOM_LIST);
	}

	private void UpdateRandomMatchingInfo(LoungeModel.RandomMatchingInfo info)
	{
		randomMatchingInfo = info;
	}

	public static bool IsValidNotEmptyRallyList()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite != null)
		{
			return MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite.Count > 0;
		}
		return false;
	}

	public static bool IsValidNotEmptyList()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges != null)
		{
			return MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.Count > 0;
		}
		return false;
	}

	public static bool IsValidInLounge()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid())
		{
			return MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge();
		}
		return false;
	}

	public bool IsUserInLounge(int user_id)
	{
		PartyModel.SlotInfo slotInfoByUserId = GetSlotInfoByUserId(user_id);
		if (IsValidInLounge() && slotInfoByUserId != null)
		{
			return slotInfoByUserId.userInfo != null;
		}
		return false;
	}

	public bool IsInLounge()
	{
		return loungeData != null;
	}

	public string GetLoungeId()
	{
		if (loungeData == null)
		{
			return "";
		}
		return loungeData.id;
	}

	public string GetLoungeNumber()
	{
		if (loungeData == null)
		{
			return "";
		}
		return loungeData.loungeNumber;
	}

	public string GetInviteMessage()
	{
		if (inviteFriendInfo == null)
		{
			return "";
		}
		return inviteFriendInfo.inviteMessage;
	}

	public string GetInviteHelpURL()
	{
		if (inviteFriendInfo == null)
		{
			return "";
		}
		return inviteFriendInfo.linkUrl;
	}

	public PARTY_STATUS GetStatus()
	{
		if (loungeData == null)
		{
			return PARTY_STATUS.NONE;
		}
		return (PARTY_STATUS)loungeData.status;
	}

	public int GetOwnerUserId()
	{
		if (loungeData == null)
		{
			return 0;
		}
		return loungeData.ownerUserId;
	}

	public int GetSlotIndex(int user_id)
	{
		if (loungeData == null)
		{
			return -1;
		}
		return loungeData.slotInfos.FindIndex((PartyModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == user_id);
	}

	public PartyModel.SlotInfo GetSlotInfoByIndex(int idx)
	{
		if (loungeData != null && idx < loungeData.slotInfos.Count)
		{
			return loungeData.slotInfos[idx];
		}
		return null;
	}

	public PartyModel.SlotInfo GetSlotInfoByUserId(int user_id)
	{
		int slotIndex = GetSlotIndex(user_id);
		if (slotIndex < 0)
		{
			return null;
		}
		return GetSlotInfoByIndex(slotIndex);
	}

	public int GetMemberCount()
	{
		if (loungeData == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < loungeData.slotInfos.Count; i++)
		{
			if (loungeData.slotInfos[i].userInfo != null)
			{
				num++;
			}
		}
		return num;
	}

	public bool CheckFirstMet(int userId)
	{
		int i = 0;
		for (int count = firstMetUserIds.Count; i < count; i++)
		{
			if (userId == firstMetUserIds[i])
			{
				return true;
			}
		}
		return false;
	}

	public List<int> GetMemberUserIdList(int my_userid = 0)
	{
		List<int> member_list = new List<int>();
		if (my_userid > 0)
		{
			member_list.Add(my_userid);
		}
		if (loungeData != null && loungeData.slotInfos != null)
		{
			loungeData.slotInfos.ForEach(delegate(PartyModel.SlotInfo slot)
			{
				if (slot.userInfo != null && (my_userid == 0 || my_userid != slot.userInfo.userId))
				{
					member_list.Add(slot.userInfo.userId);
				}
			});
		}
		return member_list;
	}

	public static string GenerateToken()
	{
		return Guid.NewGuid().ToString().Replace("-", "");
	}

	public void SetFollowLoungeMember(List<FollowLoungeMember> _followLoungeMember)
	{
		followLoungeMember = _followLoungeMember;
	}

	public FollowLoungeMember GetFollowLoungeMember(int userId)
	{
		if (followLoungeMember == null)
		{
			return null;
		}
		return followLoungeMember.Find((FollowLoungeMember d) => d.userId == userId);
	}

	public void SendSearch(Action<bool, Error> call_back, bool saveSettings)
	{
		lounges = null;
		SetSearchRequest();
		LoungeSearchModel.RequestSendForm requestSendForm = new LoungeSearchModel.RequestSendForm();
		requestSendForm.order = searchRequest.order;
		requestSendForm.label = (int)searchRequest.label;
		requestSendForm.name = searchRequest.loungeName;
		if (saveSettings)
		{
			SaveSearchSettings();
		}
		Protocol.Send(LoungeSearchModel.URL, requestSendForm, delegate(LoungeSearchModel ret)
		{
			bool arg = false;
			Error error = ret.Error;
			if (error == Error.None || error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				if (ret.Error == Error.None)
				{
					arg = true;
				}
				UpdateLoungeList(ret.result.lounges);
			}
			call_back(arg, ret.Error);
		});
	}

	public void SendSearchRandomMatching(Action<bool, Error> call_back)
	{
		ClearLounge();
		SetSearchRequest();
		LoungeModel.RequestSearchRandomMatching requestSearchRandomMatching = new LoungeModel.RequestSearchRandomMatching();
		requestSearchRandomMatching.token = GenerateToken();
		requestSearchRandomMatching.order = searchRequest.order;
		requestSearchRandomMatching.label = (int)searchRequest.label;
		requestSearchRandomMatching.name = searchRequest.loungeName;
		SaveSearchSettings();
		Protocol.Send(LoungeModel.RequestSearchRandomMatching.path, requestSearchRandomMatching, delegate(LoungeModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None && ret.result.lounge != null)
			{
				arg = true;
				UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				if (ret.result.randomMatchingInfo != null)
				{
					UpdateRandomMatchingInfo(ret.result.randomMatchingInfo);
				}
				Dirty();
			}
			if (call_back != null)
			{
				call_back(arg, ret.Error);
			}
		});
	}

	public void SetSearchRequest(LoungeSearchSettings.SearchRequestParam request = null)
	{
		if (request != null)
		{
			searchRequest = request;
		}
		else if (searchRequest == null)
		{
			ResetLoungeSearchRequest();
		}
	}

	public void ResetLoungeSearchRequest()
	{
		searchRequest = new LoungeSearchSettings.SearchRequestParam();
	}

	public void SetLoungeSearchRequestFromPrefs()
	{
		int order = 1;
		LOUNGE_LABEL @int = (LOUNGE_LABEL)PlayerPrefs.GetInt("LOUNGE_SEARCH_LABEL_KEY", 0);
		string @string = PlayerPrefs.GetString("LOUNGE_SEARCH_NAME_KEY", "");
		searchRequest = new LoungeSearchSettings.SearchRequestParam(order, @int, @string);
	}

	private void SaveSearchSettings()
	{
		PlayerPrefs.SetInt("LOUNGE_SEARCH_LABEL_KEY", (int)searchRequest.label);
		PlayerPrefs.SetString("LOUNGE_SEARCH_NAME_KEY", searchRequest.loungeName);
		PlayerPrefs.Save();
	}

	public void SetLoungeCreateRequest(LoungeConditionSettings.CreateRequestParam request = null)
	{
		if (request != null)
		{
			createRequest = request;
		}
		else if (createRequest == null)
		{
			ResetLoungeCreateRequest();
		}
	}

	public void ResetLoungeCreateRequest()
	{
		createRequest = new LoungeConditionSettings.CreateRequestParam();
	}

	public void SetLoungeCreateRequestFromPrefs()
	{
		int @int = PlayerPrefs.GetInt("LOUNGE_CREATE_STAMP_KEY", 1);
		int int2 = PlayerPrefs.GetInt("LOUNGE_CREATE_LEVEL_MIN_KEY", 15);
		int maxLevel = Singleton<UserLevelTable>.I.GetMaxLevel();
		maxLevel = PlayerPrefs.GetInt("LOUNGE_CREATE_LEVEL_MAX_KEY", maxLevel);
		int int3 = PlayerPrefs.GetInt("LOUNGE_CREATE_CAPACITY_KEY", 8);
		LOUNGE_LABEL int4 = (LOUNGE_LABEL)PlayerPrefs.GetInt("LOUNGE_CREATE_LABEL_KEY", 0);
		bool isLock = PlayerPrefs.GetInt("LOUNGE_CREATE_LOCK_KEY", 0) != 0;
		string @string = PlayerPrefs.GetString("LOUNGE_CREATE_NAME_KEY", "");
		createRequest = new LoungeConditionSettings.CreateRequestParam(@int, int2, maxLevel, int3, int4, isLock, @string);
	}

	public void SetLoungeSetting(LoungeSetting setting)
	{
		loungeSetting = setting;
	}

	public void SendCreate(Action<bool, Error> call_back)
	{
		ClearLounge();
		LoungeModel.RequestCreate requestCreate = new LoungeModel.RequestCreate();
		requestCreate.token = GenerateToken();
		requestCreate.label = (int)createRequest.label;
		requestCreate.minLv = createRequest.minLevel;
		requestCreate.maxLv = createRequest.maxLevel;
		requestCreate.name = createRequest.loungeName;
		requestCreate.num = createRequest.capacity;
		requestCreate.stampId = createRequest.stampId;
		requestCreate.isLock = (createRequest.isLock ? 1 : 0);
		SaveLoungeConditionSettings();
		if (followLoungeMember != null)
		{
			followLoungeMember.Clear();
		}
		Protocol.Send(LoungeModel.RequestCreate.path, requestCreate, delegate(LoungeModel ret)
		{
			bool arg = false;
			switch (ret.Error)
			{
			case Error.None:
				arg = true;
				UpdateLounge(ret.result.lounge, null, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				Dirty();
				break;
			}
			call_back(arg, ret.Error);
		});
	}

	public void SendApply(string loungeNumber, Action<bool, Error> call_back)
	{
		ClearLounge();
		LoungeModel.RequestApply requestApply = new LoungeModel.RequestApply();
		requestApply.token = GenerateToken();
		requestApply.loungeNumber = loungeNumber;
		Protocol.Send(LoungeModel.RequestApply.path, requestApply, delegate(LoungeModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
				UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				Dirty();
			}
			if (call_back != null)
			{
				call_back(arg, ret.Error);
			}
		});
	}

	public void SendEntry(string id, Action<bool> call_back)
	{
		ClearLounge();
		LoungeModel.RequestEntry requestEntry = new LoungeModel.RequestEntry();
		requestEntry.token = GenerateToken();
		requestEntry.id = id;
		Protocol.Send(LoungeModel.RequestEntry.path, requestEntry, delegate(LoungeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				Dirty();
			}
			call_back(obj);
		});
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
		LoungeModel.RequestInfo requestInfo = new LoungeModel.RequestInfo();
		requestInfo.id = GetLoungeId();
		Protocol.Send(LoungeModel.RequestInfo.path, requestInfo, delegate(LoungeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				if (ret.result != null && ret.result.lounge != null && ret.result.loungeServer != null)
				{
					obj = true;
					UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
					Dirty();
				}
				else
				{
					ClearLounge();
				}
			}
			else
			{
				ClearLounge();
			}
			call_back(obj);
		});
	}

	public void SendLeave(Action<bool> call_back)
	{
		if (loungeData == null)
		{
			call_back(obj: false);
			return;
		}
		LoungeLeaveModel.RequestSendForm requestSendForm = new LoungeLeaveModel.RequestSendForm();
		requestSendForm.id = loungeData.id;
		if (followLoungeMember != null)
		{
			followLoungeMember.Clear();
		}
		Protocol.Send(LoungeLeaveModel.URL, requestSendForm, delegate(LoungeLeaveModel ret)
		{
			bool obj = false;
			Error error = ret.Error;
			if (error == Error.None || error == Error.ERR_PARTY_NOT_FOUND_PARTY)
			{
				StartCoroutine(DoLeave(call_back, ret));
			}
			else
			{
				call_back(obj);
			}
		});
	}

	public void SendEdit(LoungeModel.RequestEdit lounge_setting, Action<bool> call_back)
	{
		if (loungeData == null)
		{
			call_back(obj: false);
			return;
		}
		lounge_setting.id = loungeData.id;
		Protocol.Send(LoungeModel.RequestEdit.path, lounge_setting, delegate(LoungeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
			}
			call_back(obj);
		});
	}

	private IEnumerator DoLeave(Action<bool> call_back, LoungeLeaveModel ret)
	{
		bool obj = false;
		Error error = ret.Error;
		if (error == Error.None || error == Error.ERR_PARTY_NOT_FOUND_PARTY)
		{
			if (IsHostChange(ret.result.lounge, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id))
			{
				Lounge_Model_RoomHostChanged lounge_Model_RoomHostChanged = new Lounge_Model_RoomHostChanged();
				lounge_Model_RoomHostChanged.id = 1005;
				lounge_Model_RoomHostChanged.hostid = ret.result.lounge.ownerUserId;
				MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomHostChanged);
				yield return 0;
			}
			obj = true;
			Lounge_Model_RoomLeaved lounge_Model_RoomLeaved = new Lounge_Model_RoomLeaved();
			lounge_Model_RoomLeaved.id = 1005;
			lounge_Model_RoomLeaved.token = GenerateToken();
			lounge_Model_RoomLeaved.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomLeaved, promise: false, (Coop_Model_ACK ack) => true);
			MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
			StopAFKCheck();
			ClearLounge();
			Dirty();
		}
		call_back(obj);
	}

	private bool IsHostChange(LoungeModel.Lounge lounge, int leaveUserId)
	{
		if (GetOwnerUserId() != leaveUserId)
		{
			return false;
		}
		if (lounge == null)
		{
			return false;
		}
		if (lounge.status == 30)
		{
			return false;
		}
		return true;
	}

	public void SendInviteList(Action<bool, LoungeInviteCharaInfo[]> call_back)
	{
		if (loungeData == null)
		{
			call_back(arg1: false, null);
			return;
		}
		LoungeInviteListModel.RequestSendForm requestSendForm = new LoungeInviteListModel.RequestSendForm();
		requestSendForm.id = loungeData.id;
		Protocol.Send(LoungeInviteListModel.URL, requestSendForm, delegate(LoungeInviteListModel ret)
		{
			bool arg = false;
			LoungeInviteCharaInfo[] arg2 = null;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg2 = ret.result.ToArray();
			}
			call_back(arg, arg2);
		});
	}

	public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
	{
		if (loungeData == null)
		{
			call_back(arg1: false, null);
			return;
		}
		LoungeInviteModel.RequestSendForm requestSendForm = new LoungeInviteModel.RequestSendForm();
		requestSendForm.id = loungeData.id;
		foreach (int item in userIds)
		{
			requestSendForm.ids.Add(item);
		}
		Protocol.Send(LoungeInviteModel.URL, requestSendForm, delegate(LoungeInviteModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
				call_back(arg, ret.result.ToArray());
			}
			else
			{
				call_back(arg, null);
			}
		});
	}

	public void SendInvitedLounge(Action<bool> call_back)
	{
		lounges = null;
		Protocol.Send(LoungeInvitedLoungeModel.URL, delegate(LoungeInvitedLoungeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				UpdateLoungeList(ret.result.lounges);
			}
			call_back(obj);
		});
	}

	public void SendRoomParty(Action<bool, List<PartyModel.Party>> call_back)
	{
		if (loungeData != null)
		{
			LoungeRoomPartyModel.RequestSendForm requestSendForm = new LoungeRoomPartyModel.RequestSendForm();
			requestSendForm.id = int.Parse(loungeData.id);
			Protocol.Send(LoungeRoomPartyModel.URL, requestSendForm, delegate(LoungeRoomPartyModel ret)
			{
				UpdateParties(ret.result.parties);
				call_back(ret.Error == Error.None, ret.result.parties);
			});
		}
	}

	private void UpdateParties(List<PartyModel.Party> parties)
	{
		this.parties = parties;
	}

	public void SendRoomPartyKick(Action<bool> call_back, int kickedUserId)
	{
		LoungeModel.RequestKick requestKick = new LoungeModel.RequestKick();
		requestKick.id = GetLoungeId();
		requestKick.userId = kickedUserId;
		Protocol.Send(LoungeModel.RequestKick.path, requestKick, delegate(BaseModel ret)
		{
			bool flag = ret.Error == Error.None;
			if (flag)
			{
				Lounge_Model_RoomKick model = new Lounge_Model_RoomKick
				{
					id = 1005,
					token = GenerateToken(),
					cid = kickedUserId
				};
				MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(model);
			}
			call_back(flag);
		});
	}

	public void SendRoomPartyAFKKick(int kickedUserId, Action<bool> call_back = null)
	{
		LoungeModel.RequestForceKick requestForceKick = new LoungeModel.RequestForceKick();
		requestForceKick.id = GetLoungeId();
		requestForceKick.userId = kickedUserId;
		Protocol.Send(LoungeModel.RequestForceKick.path, requestForceKick, delegate(LoungeModel ret)
		{
			bool flag = ret.Error == Error.None;
			if (flag)
			{
				if (ret.Error == Error.None)
				{
					if (ret.result.lounge.slotInfos.Find((PartyModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == kickedUserId) != null)
					{
						loungeMemberStatus[kickedUserId].UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
					}
					else
					{
						if (IsHostChange(ret.result.lounge, kickedUserId))
						{
							Lounge_Model_RoomHostChanged model = new Lounge_Model_RoomHostChanged
							{
								id = 1005,
								hostid = ret.result.lounge.ownerUserId
							};
							MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(model);
						}
						Lounge_Model_AFK_Kick model2 = new Lounge_Model_AFK_Kick
						{
							id = 1005,
							cid = kickedUserId,
							token = GenerateToken()
						};
						MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(model2);
					}
					UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				}
				else
				{
					call_back(flag);
				}
			}
			call_back(flag);
		});
	}

	public void SendSearchFollowerRoom(Action<bool, List<LoungeSearchFollowerRoomModel.LoungeFollowerModel>, List<int>> call_back)
	{
		Protocol.Send(LoungeSearchFollowerRoomModel.URL, delegate(LoungeSearchFollowerRoomModel ret)
		{
			call_back(ret.Error == Error.None, ret.result.lounges, ret.result.firstMetUserIds);
		});
	}

	public void SendIsLounge()
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			Lounge_Model_MemberLounge lounge_Model_MemberLounge = new Lounge_Model_MemberLounge();
			lounge_Model_MemberLounge.id = 1005;
			lounge_Model_MemberLounge.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberLounge);
		}
	}

	public void SendInLounge()
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			Lounge_Model_MemberLounge lounge_Model_MemberLounge = new Lounge_Model_MemberLounge();
			lounge_Model_MemberLounge.id = 1005;
			lounge_Model_MemberLounge.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberLounge);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberField);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberQuest);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberArena);
		}
	}

	public void Kick(int userId)
	{
		if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			PartyModel.SlotInfo slotInfoByUserId = GetSlotInfoByUserId(userId);
			loungeData.slotInfos.Remove(slotInfoByUserId);
		}
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				ClearLounge();
				MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
				StopAFKCheck();
			}
			MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomKick(userId);
		}
		else if (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			isKicked = true;
			if (FieldManager.IsValidInGameNoQuest())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.LOUNGE_KICKED);
			}
			if (QuestManager.IsValidInGame())
			{
				UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 140u), is_important: false, 1.4f);
			}
			ClearLounge();
			MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
			StopAFKCheck();
		}
	}

	public LoungeNetworkManager.ConnectData GetWebSockConnectData()
	{
		if (loungeData == null || loungeServerData == null)
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
		return new LoungeNetworkManager.ConnectData
		{
			path = loungeServerData.wsHost,
			ports = loungeServerData.wsPorts,
			fromId = id,
			ackPrefix = slotIndex,
			roomId = loungeData.id,
			owner = loungeData.ownerUserId,
			ownerToken = loungeServerData.token,
			uid = id,
			signature = loungeServerData.signature
		};
	}

	public void ConnectServer()
	{
		LoungeNetworkManager.ConnectData webSockConnectData = GetWebSockConnectData();
		if (webSockConnectData == null)
		{
			TryConnect(connect: false, regist: false);
		}
		else if (!MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			TryConnect(connect: false, regist: false);
		}
		else
		{
			MonoBehaviourSingleton<LoungeNetworkManager>.I.ConnectAndRegist(webSockConnectData, delegate(bool is_connect, bool is_regist)
			{
				TryConnect(is_connect, is_regist);
			});
		}
	}

	private void SaveLoungeConditionSettings()
	{
		PlayerPrefs.SetInt("LOUNGE_CREATE_STAMP_KEY", createRequest.stampId);
		PlayerPrefs.SetInt("LOUNGE_CREATE_LEVEL_MIN_KEY", createRequest.minLevel);
		PlayerPrefs.SetInt("LOUNGE_CREATE_LEVEL_MAX_KEY", createRequest.maxLevel);
		PlayerPrefs.SetInt("LOUNGE_CREATE_CAPACITY_KEY", createRequest.capacity);
		PlayerPrefs.SetInt("LOUNGE_CREATE_LABEL_KEY", (int)createRequest.label);
		int value = createRequest.isLock ? 1 : 0;
		PlayerPrefs.SetInt("LOUNGE_CREATE_LOCK_KEY", value);
		PlayerPrefs.SetString("LOUNGE_CREATE_NAME_KEY", createRequest.loungeName);
		PlayerPrefs.Save();
	}

	public void ChangeOwner(int owenerId)
	{
		loungeData.ownerUserId = owenerId;
	}

	public void CompleteKick()
	{
		isKicked = false;
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
			if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
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
			if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
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
			if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
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
			if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<LoungeNetworkManager>.I.MoveLoungeNotification(status, loungeMemberStatus);
			}
		}
		AFKCheck();
	}

	private void AFKCheck()
	{
		if (afkCoroutine != null)
		{
			StopCoroutine(afkCoroutine);
		}
		afkCoroutine = StartCoroutine(DoAFKCheck());
	}

	private IEnumerator DoAFKCheck()
	{
		if (!IsValidInLounge() || loungeMemberStatus == null)
		{
			yield break;
		}
		List<LoungeMemberStatus> all = loungeMemberStatus.GetAll();
		if (all.IsNullOrEmpty() || !all.Where((LoungeMemberStatus x) => x.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id).Any())
		{
			yield break;
		}
		LoungeMemberStatus fastest = (from x in all
			where x.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id
			where x.GetStatus() == LoungeMemberStatus.MEMBER_STATUS.LOUNGE
			orderby x.lastExecTime
			select x).FirstOrDefault();
		if (fastest != null)
		{
			double totalSeconds = (AFK_KICK_TIME - (TimeManager.GetNow().ToUniversalTime() - fastest.lastExecTime)).TotalSeconds;
			if (totalSeconds > 0.0)
			{
				yield return new WaitForSeconds((float)totalSeconds);
			}
			bool wait = true;
			Protocol.Force(delegate
			{
				SendRoomPartyAFKKick(fastest.userId, delegate
				{
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
			StopCoroutine(afkCoroutine);
		}
	}

	public void SendRally(Action<bool> call_back)
	{
		LoungeRallyModel.RequestSendForm requestSendForm = new LoungeRallyModel.RequestSendForm();
		requestSendForm.id = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.id;
		requestSendForm.pid = MonoBehaviourSingleton<PartyManager>.I.GetPartyId();
		Protocol.Send(LoungeRallyModel.URL, requestSendForm, delegate(LoungeRallyModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void GetRallyList(Action<bool> call_back)
	{
		if (!IsValidInLounge())
		{
			rallyInvite = new List<LoungeModel.SlotInfo>();
			call_back(obj: true);
			return;
		}
		rallyInvite = null;
		LoungeRallyListModel.RequestSendForm requestSendForm = new LoungeRallyListModel.RequestSendForm();
		requestSendForm.id = loungeData.id;
		Protocol.Send(LoungeRallyListModel.URL, requestSendForm, delegate(LoungeRallyListModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				rallyInvite = ret.result.slotInfos;
				flag = true;
			}
			if (rallyInvite == null)
			{
				rallyInvite = new List<LoungeModel.SlotInfo>();
			}
			if (flag && rallyInvite.Count > 0)
			{
				MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate(bool issuccess)
				{
					call_back(issuccess);
				});
			}
			else
			{
				call_back(flag);
			}
		});
	}

	public bool IsRallyUser(int userid)
	{
		if (rallyInvite == null || rallyInvite.Count == 0)
		{
			return false;
		}
		int count = rallyInvite.Count;
		for (int i = 0; i < count; i++)
		{
			if (rallyInvite[i].userInfo.userId == userid)
			{
				return true;
			}
		}
		return false;
	}
}
