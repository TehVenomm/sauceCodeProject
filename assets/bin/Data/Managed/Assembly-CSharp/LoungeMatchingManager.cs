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

	private const int RETRY_COUNT = 3;

	private const float RETRY_TIMER = 5f;

	private bool isChangeStarted;

	private Coroutine afkCoroutine;

	private string inviteValue = string.Empty;

	public Action<LoungeMemberStatus> OnChangeMemberStatus = delegate
	{
	};

	private ChatLoungeConnection connection;

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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().AddComponent<LoungeWebSocket>();
		this.get_gameObject().AddComponent<LoungeNetworkManager>();
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
		}, true);
	}

	public void TryConnect(bool connect, bool regist)
	{
		if (connect && regist)
		{
			Lounge_Model_RoomJoined lounge_Model_RoomJoined = new Lounge_Model_RoomJoined();
			lounge_Model_RoomJoined.id = 1005;
			lounge_Model_RoomJoined.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomJoined, false, null, null);
			SetLoungeMemberesStatus();
			AFKCheck();
		}
	}

	private unsafe void SetLoungeMemberesStatus()
	{
		if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			List<Party_Model_RegisterACK.UserInfo> data = MonoBehaviourSingleton<LoungeNetworkManager>.I.registerAck.GetConvertUserInfo().Where(new Func<Party_Model_RegisterACK.UserInfo, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
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
		return MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite.Count > 0;
	}

	public static bool IsValidNotEmptyList()
	{
		return MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.Count > 0;
	}

	public static bool IsValidInLounge()
	{
		return MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge();
	}

	public bool IsUserInLounge(int user_id)
	{
		LoungeModel.SlotInfo slotInfoByUserId = GetSlotInfoByUserId(user_id);
		return IsValidInLounge() && slotInfoByUserId != null && slotInfoByUserId.userInfo != null;
	}

	public bool IsInLounge()
	{
		return loungeData != null;
	}

	public string GetLoungeId()
	{
		return (loungeData == null) ? string.Empty : loungeData.id;
	}

	public string GetLoungeNumber()
	{
		return (loungeData == null) ? string.Empty : loungeData.loungeNumber;
	}

	public string GetInviteMessage()
	{
		return (inviteFriendInfo == null) ? string.Empty : inviteFriendInfo.inviteMessage;
	}

	public string GetInviteHelpURL()
	{
		return (inviteFriendInfo == null) ? string.Empty : inviteFriendInfo.linkUrl;
	}

	public PARTY_STATUS GetStatus()
	{
		return (PARTY_STATUS)((loungeData != null) ? loungeData.status : 0);
	}

	public int GetOwnerUserId()
	{
		return (loungeData != null) ? loungeData.ownerUserId : 0;
	}

	public int GetSlotIndex(int user_id)
	{
		return (loungeData == null) ? (-1) : loungeData.slotInfos.FindIndex((LoungeModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == user_id);
	}

	public LoungeModel.SlotInfo GetSlotInfoByIndex(int idx)
	{
		if (loungeData != null && idx < loungeData.slotInfos.Count)
		{
			return loungeData.slotInfos[idx];
		}
		return null;
	}

	public LoungeModel.SlotInfo GetSlotInfoByUserId(int user_id)
	{
		int slotIndex = GetSlotIndex(user_id);
		return (slotIndex < 0) ? null : GetSlotInfoByIndex(slotIndex);
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
			loungeData.slotInfos.ForEach(delegate(LoungeModel.SlotInfo slot)
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
		return Guid.NewGuid().ToString().Replace("-", string.Empty);
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
		SetSearchRequest(null);
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
			bool flag = false;
			Error error = ret.Error;
			if (error == Error.None || error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				if (ret.Error == Error.None)
				{
					flag = true;
				}
				UpdateLoungeList(ret.result.lounges);
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendSearchRandomMatching(Action<bool, Error> call_back)
	{
		ClearLounge();
		SetSearchRequest(null);
		LoungeModel.RequestSearchRandomMatching requestSearchRandomMatching = new LoungeModel.RequestSearchRandomMatching();
		requestSearchRandomMatching.token = GenerateToken();
		requestSearchRandomMatching.order = searchRequest.order;
		requestSearchRandomMatching.label = (int)searchRequest.label;
		requestSearchRandomMatching.name = searchRequest.loungeName;
		SaveSearchSettings();
		Protocol.Send(LoungeModel.RequestSearchRandomMatching.path, requestSearchRandomMatching, delegate(LoungeModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None && ret.result.lounge != null)
			{
				flag = true;
				UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				if (ret.result.randomMatchingInfo != null)
				{
					UpdateRandomMatchingInfo(ret.result.randomMatchingInfo);
				}
				Dirty();
			}
			if (call_back != null)
			{
				call_back.Invoke(flag, ret.Error);
			}
		}, string.Empty);
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
		string @string = PlayerPrefs.GetString("LOUNGE_SEARCH_NAME_KEY", string.Empty);
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
		string @string = PlayerPrefs.GetString("LOUNGE_CREATE_NAME_KEY", string.Empty);
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
			bool flag = false;
			switch (ret.Error)
			{
			case Error.None:
				flag = true;
				UpdateLounge(ret.result.lounge, null, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				Dirty();
				break;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendApply(string loungeNumber, Action<bool, Error> call_back)
	{
		ClearLounge();
		LoungeModel.RequestApply requestApply = new LoungeModel.RequestApply();
		requestApply.token = GenerateToken();
		requestApply.loungeNumber = loungeNumber;
		Protocol.Send(LoungeModel.RequestApply.path, requestApply, delegate(LoungeModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				Dirty();
			}
			if (call_back != null)
			{
				call_back.Invoke(flag, ret.Error);
			}
		}, string.Empty);
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
		}, string.Empty);
	}

	public unsafe void SendInfo(Action<bool> call_back, bool force = false)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		if (force)
		{
			_003CSendInfo_003Ec__AnonStorey65C _003CSendInfo_003Ec__AnonStorey65C;
			Protocol.Force(new Action((object)_003CSendInfo_003Ec__AnonStorey65C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		}, string.Empty);
	}

	public void SendLeave(Action<bool> call_back)
	{
		if (loungeData == null)
		{
			call_back(false);
		}
		else
		{
			LoungeLeaveModel.RequestSendForm requestSendForm = new LoungeLeaveModel.RequestSendForm();
			requestSendForm.id = loungeData.id;
			if (followLoungeMember != null)
			{
				followLoungeMember.Clear();
			}
			Protocol.Send(LoungeLeaveModel.URL, requestSendForm, delegate(LoungeLeaveModel ret)
			{
				//IL_0037: Unknown result type (might be due to invalid IL or missing references)
				bool obj = false;
				Error error = ret.Error;
				if (error == Error.None || error == Error.ERR_PARTY_NOT_FOUND_PARTY)
				{
					this.StartCoroutine(DoLeave(call_back, ret));
				}
				else
				{
					call_back(obj);
				}
			}, string.Empty);
		}
	}

	public void SendEdit(LoungeModel.RequestEdit lounge_setting, Action<bool> call_back)
	{
		if (loungeData == null)
		{
			call_back(false);
		}
		else
		{
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
			}, string.Empty);
		}
	}

	private unsafe IEnumerator DoLeave(Action<bool> call_back, LoungeLeaveModel ret)
	{
		bool is_success = false;
		Error error = ret.Error;
		if (error == Error.None || error == Error.ERR_PARTY_NOT_FOUND_PARTY)
		{
			if (IsHostChange(ret.result.lounge, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id))
			{
				Lounge_Model_RoomHostChanged hostChange = new Lounge_Model_RoomHostChanged
				{
					id = 1005,
					hostid = ret.result.lounge.ownerUserId
				};
				MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(hostChange, false, null, null);
				yield return (object)0;
			}
			is_success = true;
			Lounge_Model_RoomLeaved packet = new Lounge_Model_RoomLeaved
			{
				id = 1005,
				token = GenerateToken(),
				cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id
			};
			LoungeNetworkManager i = MonoBehaviourSingleton<LoungeNetworkManager>.I;
			Lounge_Model_RoomLeaved model = packet;
			if (_003CDoLeave_003Ec__Iterator258._003C_003Ef__am_0024cacheA == null)
			{
				_003CDoLeave_003Ec__Iterator258._003C_003Ef__am_0024cacheA = new Func<Coop_Model_ACK, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendBroadcast(model, false, _003CDoLeave_003Ec__Iterator258._003C_003Ef__am_0024cacheA, null);
			MonoBehaviourSingleton<ChatManager>.I.DestroyLoungeChat();
			StopAFKCheck();
			ClearLounge();
			Dirty();
		}
		call_back(is_success);
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
			call_back.Invoke(false, (LoungeInviteCharaInfo[])null);
		}
		else
		{
			LoungeInviteListModel.RequestSendForm requestSendForm = new LoungeInviteListModel.RequestSendForm();
			requestSendForm.id = loungeData.id;
			Protocol.Send(LoungeInviteListModel.URL, requestSendForm, delegate(LoungeInviteListModel ret)
			{
				bool flag = false;
				LoungeInviteCharaInfo[] array = null;
				if (ret.Error == Error.None)
				{
					flag = true;
					array = ret.result.ToArray();
				}
				call_back.Invoke(flag, array);
			}, string.Empty);
		}
	}

	public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
	{
		if (loungeData == null)
		{
			call_back.Invoke(false, (int[])null);
		}
		else
		{
			LoungeInviteModel.RequestSendForm requestSendForm = new LoungeInviteModel.RequestSendForm();
			requestSendForm.id = loungeData.id;
			foreach (int item in userIds)
			{
				requestSendForm.ids.Add(item);
			}
			Protocol.Send(LoungeInviteModel.URL, requestSendForm, delegate(LoungeInviteModel ret)
			{
				bool flag = false;
				if (ret.Error == Error.None)
				{
					flag = true;
					call_back.Invoke(flag, ret.result.ToArray());
				}
				else
				{
					call_back.Invoke(flag, (int[])null);
				}
			}, string.Empty);
		}
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
		}, string.Empty);
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
				call_back.Invoke(ret.Error == Error.None, ret.result.parties);
			}, string.Empty);
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
				MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(model, false, null, null);
			}
			call_back(flag);
		}, string.Empty);
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
					LoungeModel.SlotInfo slotInfo = ret.result.lounge.slotInfos.Find((LoungeModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == kickedUserId);
					if (slotInfo != null)
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
							MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(model, false, null, null);
						}
						Lounge_Model_AFK_Kick model2 = new Lounge_Model_AFK_Kick
						{
							id = 1005,
							cid = kickedUserId,
							token = GenerateToken()
						};
						MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(model2, false, null, null);
					}
					UpdateLounge(ret.result.lounge, ret.result.friend, ret.result.loungeServer, ret.result.inviteFriendInfo, ret.result.firstMetUserIds);
				}
				else
				{
					call_back(flag);
				}
			}
			call_back(flag);
		}, string.Empty);
	}

	public void SendSearchFollowerRoom(Action<bool, List<LoungeSearchFollowerRoomModel.LoungeFollowerModel>, List<int>> call_back)
	{
		Protocol.Send(LoungeSearchFollowerRoomModel.URL, delegate(LoungeSearchFollowerRoomModel ret)
		{
			call_back.Invoke(ret.Error == Error.None, ret.result.lounges, ret.result.firstMetUserIds);
		}, string.Empty);
	}

	public void SendIsLounge()
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			Lounge_Model_MemberLounge lounge_Model_MemberLounge = new Lounge_Model_MemberLounge();
			lounge_Model_MemberLounge.id = 1005;
			lounge_Model_MemberLounge.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberLounge, false, null, null);
		}
	}

	public void SendInLounge()
	{
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			Lounge_Model_MemberLounge lounge_Model_MemberLounge = new Lounge_Model_MemberLounge();
			lounge_Model_MemberLounge.id = 1005;
			lounge_Model_MemberLounge.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberLounge, false, null, null);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberField, false, null, null);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberQuest, false, null, null);
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
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_MemberArena, false, null, null);
		}
	}

	public void Kick(int userId)
	{
		if (userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			LoungeModel.SlotInfo slotInfoByUserId = GetSlotInfoByUserId(userId);
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
				string text = StringTable.Get(STRING_CATEGORY.IN_GAME, 140u);
				UIInGamePopupDialog.PushOpen(text, false, 1.4f);
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
		LoungeNetworkManager.ConnectData connectData = new LoungeNetworkManager.ConnectData();
		connectData.path = loungeServerData.wsHost;
		connectData.ports = loungeServerData.wsPorts;
		connectData.fromId = id;
		connectData.ackPrefix = slotIndex;
		connectData.roomId = loungeData.id;
		connectData.owner = loungeData.ownerUserId;
		connectData.ownerToken = loungeServerData.token;
		connectData.uid = id;
		connectData.signature = loungeServerData.signature;
		return connectData;
	}

	public unsafe void ConnectServer()
	{
		LoungeNetworkManager.ConnectData webSockConnectData = GetWebSockConnectData();
		if (webSockConnectData == null)
		{
			TryConnect(false, false);
		}
		else if (!MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			TryConnect(false, false);
		}
		else
		{
			MonoBehaviourSingleton<LoungeNetworkManager>.I.ConnectAndRegist(webSockConnectData, new Action<bool, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void SaveLoungeConditionSettings()
	{
		PlayerPrefs.SetInt("LOUNGE_CREATE_STAMP_KEY", createRequest.stampId);
		PlayerPrefs.SetInt("LOUNGE_CREATE_LEVEL_MIN_KEY", createRequest.minLevel);
		PlayerPrefs.SetInt("LOUNGE_CREATE_LEVEL_MAX_KEY", createRequest.maxLevel);
		PlayerPrefs.SetInt("LOUNGE_CREATE_CAPACITY_KEY", createRequest.capacity);
		PlayerPrefs.SetInt("LOUNGE_CREATE_LABEL_KEY", (int)createRequest.label);
		int num = createRequest.isLock ? 1 : 0;
		PlayerPrefs.SetInt("LOUNGE_CREATE_LOCK_KEY", num);
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
		if (this.loungeMemberStatus != null)
		{
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
	}

	public void OnRecvMemberMoveField(Lounge_Model_MemberField model)
	{
		if (this.loungeMemberStatus != null)
		{
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
	}

	public void OnRecvMemberMoveQuest(Lounge_Model_MemberQuest model)
	{
		if (this.loungeMemberStatus != null)
		{
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
	}

	public void OnRecvMemberMoveArena(Lounge_Model_MemberArena model)
	{
		if (this.loungeMemberStatus != null)
		{
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
	}

	private void AFKCheck()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		if (afkCoroutine != null)
		{
			this.StopCoroutine(afkCoroutine);
		}
		afkCoroutine = this.StartCoroutine(DoAFKCheck());
	}

	private unsafe IEnumerator DoAFKCheck()
	{
		if (IsValidInLounge() && loungeMemberStatus != null)
		{
			List<LoungeMemberStatus> allMember = loungeMemberStatus.GetAll();
			if (!allMember.IsNullOrEmpty())
			{
				List<LoungeMemberStatus> source = allMember;
				if (_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache7 == null)
				{
					_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache7 = new Func<LoungeMemberStatus, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				if (source.Where(_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache7).Any())
				{
					List<LoungeMemberStatus> source2 = allMember;
					if (_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache8 == null)
					{
						_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache8 = new Func<LoungeMemberStatus, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					IEnumerable<LoungeMemberStatus> source3 = source2.Where(_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache8);
					if (_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache9 == null)
					{
						_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache9 = new Func<LoungeMemberStatus, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					IEnumerable<LoungeMemberStatus> source4 = source3.Where(_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cache9);
					if (_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cacheA == null)
					{
						_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cacheA = new Func<LoungeMemberStatus, DateTime>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					LoungeMemberStatus fastest = source4.OrderBy<LoungeMemberStatus, DateTime>(_003CDoAFKCheck_003Ec__Iterator259._003C_003Ef__am_0024cacheA).FirstOrDefault();
					if (fastest != null)
					{
						double waitTime = (AFK_KICK_TIME - (TimeManager.GetNow().ToUniversalTime() - fastest.lastExecTime)).TotalSeconds;
						if (waitTime > 0.0)
						{
							yield return (object)new WaitForSeconds((float)waitTime);
						}
						bool wait = true;
						Protocol.Force(new Action((object)/*Error near IL_01a6: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
						while (wait)
						{
							yield return (object)null;
						}
						AFKCheck();
					}
				}
			}
		}
	}

	public void StopAFKCheck()
	{
		if (afkCoroutine != null)
		{
			this.StopCoroutine(afkCoroutine);
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
		}, string.Empty);
	}

	public void GetRallyList(Action<bool> call_back)
	{
		if (!IsValidInLounge())
		{
			rallyInvite = new List<LoungeModel.SlotInfo>();
			call_back(true);
		}
		else
		{
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
					}, false);
				}
				else
				{
					call_back(flag);
				}
			}, string.Empty);
		}
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
