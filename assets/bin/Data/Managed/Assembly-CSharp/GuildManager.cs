using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildManager : MonoBehaviourSingleton<GuildManager>
{
	public enum GUILD_TYPE
	{
		PUBLIC,
		PRIVATE,
		CLOSED
	}

	public class CreateGuildRequestParam
	{
		public int[] EmblemLayerIDs
		{
			get;
			private set;
		}

		public string GuildName
		{
			get;
			private set;
		}

		public string GuildTag
		{
			get;
			private set;
		}

		public string GuildDescribe
		{
			get;
			private set;
		}

		public int GuildLocation
		{
			get;
			private set;
		}

		public GUILD_TYPE GuildType
		{
			get;
			private set;
		}

		public int GuildMinLevel
		{
			get;
			private set;
		}

		public int GuildMapID
		{
			get;
			private set;
		}

		public int[] InvitedFriendIDs
		{
			get;
			private set;
		}

		public CreateGuildRequestParam()
		{
			EmblemLayerIDs = new int[3];
			EmblemLayerIDs[0] = -1;
			EmblemLayerIDs[1] = -1;
			EmblemLayerIDs[2] = -1;
			GuildType = GUILD_TYPE.PUBLIC;
			GuildMinLevel = 15;
		}

		public CreateGuildRequestParam(GuildStatisticInfo guidata)
		{
			if (guidata != null)
			{
				EmblemLayerIDs = guidata.emblem;
				GuildName = guidata.clanName;
				GuildTag = guidata.tag;
				GuildDescribe = guidata.description;
				GuildType = (GUILD_TYPE)guidata.privacy;
				GuildMinLevel = guidata.min_level;
			}
		}

		public void SetEmblemID(int layer, int id)
		{
			EmblemLayerIDs[layer] = id;
		}

		public void SetGuildName(string name)
		{
			GuildName = name;
		}

		public void SetGuildTag(string tag)
		{
			GuildTag = tag;
		}

		public void SetGuildDescribe(string des)
		{
			GuildDescribe = des;
		}

		public void SetGuildType(GUILD_TYPE type)
		{
			GuildType = type;
		}

		public void SetGuildMinLevel(int min_level)
		{
			GuildMinLevel = min_level;
		}

		public void SetGuildLocaltion(int location)
		{
			GuildLocation = location;
		}
	}

	public class GuildSearchRequestParam
	{
	}

	public const int NUM_GUILD_MAP = 2;

	public const int GUILD_LEVEL_REQUIRE = 15;

	public const int GUILD_CREATE_NEEDED_GEM = 15;

	public static int sDefaultEmblemIDLayer1 = 10001;

	public static int sDefaultEmblemIDLayer2 = 10011;

	public static int sDefaultEmblemIDLayer3 = 10021;

	private bool isEnterGUild;

	private string banReason;

	private CreateGuildRequestParam mCreateRequest = new CreateGuildRequestParam();

	private GuildSearchRequestParam mSearchRequest = new GuildSearchRequestParam();

	public string mSearchKeywork;

	private bool _isClanInfoCached;

	public List<FriendCharaInfo> talkUsers = new List<FriendCharaInfo>();

	public long askUpdate = -1L;

	public bool IsEnterGuild
	{
		get
		{
			return isEnterGUild;
		}
		set
		{
			isEnterGUild = value;
		}
	}

	public string BanReason
	{
		set
		{
			banReason = value;
		}
	}

	public GuildModel.Guild guildData
	{
		get;
		private set;
	}

	public GuildStatisticInfo guildStatData
	{
		get;
		private set;
	}

	public GuildStatisticInfo guildChangeData
	{
		get;
		private set;
	}

	public List<GuildSearchModel.GuildSearchInfo> guilds
	{
		get;
		private set;
	}

	public List<GuildInvitedModel.GuildInvitedInfo> guildInviteList
	{
		get;
		private set;
	}

	public List<DonateInvitationInfo> donateInviteList
	{
		get;
		private set;
	}

	public GuildMemberListModel guilMemberList
	{
		get;
		private set;
	}

	public GuildInfoModel.GuildInfo guildInfos
	{
		get;
		private set;
	}

	public List<FriendCharaInfo> members
	{
		get;
		set;
	}

	public FriendCharaInfo talkUser
	{
		get;
		private set;
	}

	public List<DonateInfo> donateList
	{
		get;
		set;
	}

	public double SystemTime
	{
		get;
		set;
	}

	public DonateInfo pinDonate
	{
		get;
		set;
	}

	public bool ExistsAskUpdate => true;

	public CreateGuildRequestParam GetCreateGuildRequestParam()
	{
		return mCreateRequest;
	}

	public void ClearCreateGuildRequestParam()
	{
		mCreateRequest = new CreateGuildRequestParam();
	}

	public GuildSearchRequestParam GetGuildSearchRequestParam()
	{
		return mSearchRequest;
	}

	public void ResetGuildSearchRequest()
	{
		mSearchRequest = new GuildSearchRequestParam();
	}

	public void CreateAddedGuildRequestParam(GuildStatisticInfo guildStat)
	{
		mCreateRequest = new CreateGuildRequestParam(guildStat);
	}

	public void SetGuildChangeData(CreateGuildRequestParam param)
	{
		if (param != null)
		{
			guildChangeData = new GuildStatisticInfo();
			guildChangeData.emblem = param.EmblemLayerIDs;
			guildChangeData.clanName = param.GuildName;
			guildChangeData.tag = param.GuildTag;
			guildChangeData.description = param.GuildDescribe;
			guildChangeData.privacy = (int)param.GuildType;
			guildChangeData.min_level = param.GuildMinLevel;
		}
	}

	private bool IsInGuild()
	{
		return guildData != null;
	}

	public static bool IsValidInGuild()
	{
		return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.IsInGuild();
	}

	public void SendCreate(List<int> inviteList, Action<bool, Error> callBack)
	{
		GuildModel.RequestCreate requestCreate = new GuildModel.RequestCreate();
		requestCreate.token = GenerateToken();
		requestCreate.name = mCreateRequest.GuildName;
		requestCreate.description = mCreateRequest.GuildDescribe;
		requestCreate.tag = mCreateRequest.GuildTag;
		requestCreate.emblem = new List<int>();
		requestCreate.emblem.Add(mCreateRequest.EmblemLayerIDs[0]);
		requestCreate.emblem.Add(mCreateRequest.EmblemLayerIDs[1]);
		requestCreate.emblem.Add(mCreateRequest.EmblemLayerIDs[2]);
		requestCreate.min_level = mCreateRequest.GuildMinLevel;
		requestCreate.location = mCreateRequest.GuildMapID;
		requestCreate.privacy = (int)mCreateRequest.GuildType;
		requestCreate.inviteList = new List<string>();
		int num = 0;
		while (inviteList != null && num < inviteList.Count)
		{
			requestCreate.inviteList.Add(inviteList[num].ToString());
			num++;
		}
		SaveGuildSettings();
		Protocol.Send(GuildModel.RequestCreate.path, requestCreate, delegate(GuildModel ret)
		{
			bool is_success = false;
			switch (ret.Error)
			{
			case Error.None:
				is_success = true;
				ClearCreateGuildRequestParam();
				UpdateGuild(ret.result.guildInfo);
				if (MonoBehaviourSingleton<ChatManager>.IsValid())
				{
					MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(ret.result.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate
					{
						callBack.Invoke(is_success, ret.Error);
					});
				}
				else
				{
					callBack.Invoke(is_success, ret.Error);
				}
				break;
			case Error.WRN_PARTY_TOO_MANY_PARTIES:
				Log.Error("Guild create fall");
				callBack.Invoke(is_success, ret.Error);
				break;
			default:
				callBack.Invoke(is_success, ret.Error);
				break;
			}
		}, string.Empty);
	}

	public void SendCheckClanSetting(int clanId, string clanName, string clanTag, string clanDescription, Action<bool, Error> call_back)
	{
		GuildModel.RequestCreateVerify requestCreateVerify = new GuildModel.RequestCreateVerify();
		requestCreateVerify.clanId = clanId;
		requestCreateVerify.token = GenerateToken();
		requestCreateVerify.name = clanName;
		requestCreateVerify.tag = clanTag;
		requestCreateVerify.description = clanDescription;
		Protocol.Send(GuildModel.RequestCreateVerify.verifyPath, requestCreateVerify, delegate(BaseModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				call_back.Invoke(flag, ret.Error);
			}
			else
			{
				call_back.Invoke(flag, ret.Error);
			}
		}, string.Empty);
	}

	public void SendDelete(Action<bool, Error> call_back)
	{
		GuildModel.RequestDelete requestDelete = new GuildModel.RequestDelete();
		requestDelete.token = GenerateToken();
		Protocol.Send(GuildModel.RequestDelete.path, requestDelete, delegate(BaseModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				UpdateGuild(null);
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendSearch(Action<bool, Error> call_back, bool saveSettings)
	{
		GuildSearchModel.RequestSearchWithKeyword requestSearchWithKeyword = new GuildSearchModel.RequestSearchWithKeyword();
		requestSearchWithKeyword.token = GenerateToken();
		requestSearchWithKeyword.keyword = mSearchKeywork;
		Protocol.Send(GuildSearchModel.RequestSearchWithKeyword.path, requestSearchWithKeyword, delegate(GuildSearchModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				guilds = ret.result.clanList;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendSearchWithID(int clanId, Action<bool, Error> call_back)
	{
		GuildSearchModelWithID.RequestSearchWithID requestSearchWithID = new GuildSearchModelWithID.RequestSearchWithID();
		requestSearchWithID.token = GenerateToken();
		requestSearchWithID.clanId = clanId;
		Protocol.Send(GuildSearchModelWithID.RequestSearchWithID.path, requestSearchWithID, delegate(GuildSearchModelWithID ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				guilds.Clear();
				if (ret.result.guildInfo != null)
				{
					guilds.Add(ret.result.guildInfo);
				}
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendRequestStatistic(int clan_id, Action<bool, GuildStatisticInfo> callback)
	{
		GuildStatisticModel.Form form = new GuildStatisticModel.Form();
		form.clanId = clan_id;
		Protocol.Send(GuildStatisticModel.URL, form, delegate(GuildStatisticModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void SendRequestJoin(int clanId, int recommentId, Action<bool, Error> call_back)
	{
		GuildModel.RequestJoin requestJoin = new GuildModel.RequestJoin();
		requestJoin.token = GenerateToken();
		requestJoin.clanId = clanId;
		requestJoin.recommendId = recommentId;
		Protocol.Send(GuildRequestJoinModel.URL, requestJoin, delegate(GuildRequestJoinModel ret)
		{
			bool is_success = false;
			if (ret.Error == Error.None)
			{
				is_success = true;
				guildInfos = ret.result;
				UpdateGuild(guildInfos.guildInfo);
				if (ret.result.status == 1 && MonoBehaviourSingleton<ChatManager>.IsValid())
				{
					MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, delegate
					{
						call_back.Invoke(is_success, ret.Error);
					});
				}
				else
				{
					call_back.Invoke(is_success, ret.Error);
				}
			}
			else
			{
				call_back.Invoke(is_success, ret.Error);
			}
		}, string.Empty);
	}

	public void SendKick(int userId, Action<bool, Error> call_back = null)
	{
		GuildModel.RequestKick requestKick = new GuildModel.RequestKick();
		requestKick.token = GenerateToken();
		requestKick.forUserId = userId;
		requestKick.reason = banReason.Replace("\n", "\\n");
		Protocol.Send(GuildModel.RequestKick.path, requestKick, delegate(BaseModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendAdminJoin(int requestId, int decision, Action<bool, Error> call_back)
	{
		GuildModel.RequestAdminJoin requestAdminJoin = new GuildModel.RequestAdminJoin();
		requestAdminJoin.token = GenerateToken();
		requestAdminJoin.requestId = requestId;
		requestAdminJoin.decision = decision;
		Protocol.Send(GuildModel.RequestAdminJoin.path, requestAdminJoin, delegate(BaseModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendRequestRequest(int clanId, int recommentId, Action<bool, Error> call_back)
	{
		GuildModel.RequestJoin requestJoin = new GuildModel.RequestJoin();
		requestJoin.token = GenerateToken();
		requestJoin.clanId = clanId;
		requestJoin.recommendId = recommentId;
		Protocol.Send(GuildRequestJoinModel.URL, requestJoin, delegate(GuildRequestJoinModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendChangeSetting(CreateGuildRequestParam requestParam, Action<bool, Error> call_back)
	{
		GuildModel.GuildChangeSetting guildChangeSetting = new GuildModel.GuildChangeSetting();
		guildChangeSetting.name = requestParam.GuildName;
		guildChangeSetting.description = requestParam.GuildDescribe;
		guildChangeSetting.tag = requestParam.GuildTag;
		guildChangeSetting.min_level = requestParam.GuildMinLevel;
		guildChangeSetting.emblem = requestParam.EmblemLayerIDs;
		guildChangeSetting.privacy = (int)requestParam.GuildType;
		guildChangeSetting.location = requestParam.GuildLocation;
		Protocol.Send(GuildChangeSettingModel.URL, guildChangeSetting, delegate(GuildChangeSettingModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				UpdateGuildData(ret);
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendLeave(Action<bool, Error> call_back)
	{
		GuildModel.RequestLeave requestLeave = new GuildModel.RequestLeave();
		requestLeave.token = GenerateToken();
		Protocol.Send(GuildModel.RequestLeave.path, requestLeave, delegate(BaseModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				UpdateGuild(null);
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void UpdateGuild(GuildModel.Guild guild)
	{
		guildData = guild;
		if (guildData != null)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId = guildData.clanId;
			GetClanStat(null);
			int @int = PlayerPrefs.GetInt("CLAN_ID");
			PlayerPrefs.SetInt("CLAN_ID", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
		}
		else
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId = -1;
			guildStatData = null;
			PlayerPrefs.SetInt("CLAN_ID", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
		}
	}

	private void UpdateGuildData(GuildChangeSettingModel updateData)
	{
		GuildChangeSettingModel.Result result = updateData.result;
		guildData.name = result.clanName;
		guildData.level = result.level;
		guildData.exp = result.exp;
		guildData.emblem = result.emblem;
	}

	public static bool IsValidNotEmptyGuildList()
	{
		return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.guilds != null && MonoBehaviourSingleton<GuildManager>.I.guilds.Count > 0;
	}

	private void SaveGuildSettings()
	{
	}

	public static string GenerateToken()
	{
		return Guid.NewGuid().ToString().Replace("-", string.Empty);
	}

	public void SendClanInfo(Action<bool> callBack)
	{
		if (_isClanInfoCached)
		{
			Protocol.SendAsync(GuildInfoModel.URL, delegate(GuildInfoModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret.result.invitation)
					{
						MonoBehaviourSingleton<UserInfoManager>.I.SetClanInviteHome();
					}
					guildInfos = ret.result;
					if (guildInfos.guildInfo == null)
					{
						PlayerPrefs.SetInt("CLAN_ID", -1);
					}
					else
					{
						SetAskUpdate(long.Parse(guildInfos.askUpdate));
						if (MonoBehaviourSingleton<ChatManager>.IsValid())
						{
							MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, null);
						}
						UpdateGuild(guildInfos.guildInfo);
						if (ret.result.receivable)
						{
							SendDonateReceive(null);
						}
					}
				}
			}, string.Empty);
			callBack(true);
		}
		else
		{
			Protocol.Send(GuildInfoModel.URL, delegate(GuildInfoModel ret)
			{
				bool isSuccess = false;
				if (ret.Error == Error.None)
				{
					_isClanInfoCached = true;
					isSuccess = true;
					if (ret.result.invitation)
					{
						MonoBehaviourSingleton<UserInfoManager>.I.SetClanInviteHome();
					}
					guildInfos = ret.result;
					if (guildInfos.guildInfo == null)
					{
						callBack(isSuccess);
						PlayerPrefs.SetInt("CLAN_ID", -1);
					}
					else
					{
						SetAskUpdate(long.Parse(guildInfos.askUpdate));
						if (MonoBehaviourSingleton<ChatManager>.IsValid())
						{
							MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, null);
						}
						UpdateGuild(guildInfos.guildInfo);
						if (ret.result.receivable)
						{
							SendDonateReceive(delegate
							{
								callBack(isSuccess);
							});
						}
						else
						{
							callBack(isSuccess);
						}
					}
				}
				else
				{
					callBack(isSuccess);
				}
			}, string.Empty);
		}
	}

	public unsafe void GetClanStat(Action<bool> call_back = null)
	{
		if (guildData != null && guildData.clanId != -1)
		{
			_003CGetClanStat_003Ec__AnonStorey612 _003CGetClanStat_003Ec__AnonStorey;
			SendRequestStatistic(guildData.clanId, new Action<bool, GuildStatisticInfo>((object)_003CGetClanStat_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	public void SendMemberList(int clan_id, Action<bool, GuildMemberListModel> callback)
	{
		GuildMemberListModel.RequestSendForm requestSendForm = new GuildMemberListModel.RequestSendForm();
		requestSendForm.clanId = clan_id;
		Protocol.Send(GuildMemberListModel.URL, requestSendForm, delegate(GuildMemberListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			members = ret.result.list;
			guilMemberList = ret;
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public unsafe void SetTalkUser(FriendCharaInfo message_user)
	{
		_003CSetTalkUser_003Ec__AnonStorey614 _003CSetTalkUser_003Ec__AnonStorey;
		FriendCharaInfo friendCharaInfo = talkUsers.FirstOrDefault(new Func<FriendCharaInfo, bool>((object)_003CSetTalkUser_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (friendCharaInfo == null)
		{
			talkUsers.Insert(0, message_user);
		}
		talkUser = message_user;
	}

	public unsafe bool AddTalkUser(int userId)
	{
		_003CAddTalkUser_003Ec__AnonStorey615 _003CAddTalkUser_003Ec__AnonStorey;
		FriendCharaInfo friendCharaInfo = talkUsers.FirstOrDefault(new Func<FriendCharaInfo, bool>((object)_003CAddTalkUser_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (friendCharaInfo != null)
		{
			return false;
		}
		friendCharaInfo = members.FirstOrDefault(new Func<FriendCharaInfo, bool>((object)_003CAddTalkUser_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (friendCharaInfo != null)
		{
			talkUsers.Add(friendCharaInfo);
			return true;
		}
		return false;
	}

	public unsafe void RemoveTalkUser(FriendCharaInfo message_user)
	{
		_003CRemoveTalkUser_003Ec__AnonStorey616 _003CRemoveTalkUser_003Ec__AnonStorey;
		FriendCharaInfo friendCharaInfo = talkUsers.FirstOrDefault(new Func<FriendCharaInfo, bool>((object)_003CRemoveTalkUser_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (friendCharaInfo != null)
		{
			talkUsers.Remove(friendCharaInfo);
		}
	}

	public void UpdateTalkUser()
	{
		if (talkUser == null && talkUsers.Count > 0)
		{
			talkUser = talkUsers[0];
		}
	}

	public void EmptyTalkUser()
	{
		talkUser = null;
	}

	public void SendClanChatLog(Action<bool, GuildChatModel> callback)
	{
		Protocol.Send(GuildChatModel.URL, delegate(GuildChatModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public void SendPrivateClanChatLog(int to, Action<bool, GuildPrivateChatModel> callback)
	{
		GuildPrivateChatModel.SendForm sendForm = new GuildPrivateChatModel.SendForm();
		sendForm.toUserId = to;
		GuildPrivateChatModel.SendForm postData = sendForm;
		Protocol.Send(GuildPrivateChatModel.URL, postData, delegate(GuildPrivateChatModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public void SendClanChatPin(int fromUserId, int chatId, string uuid, int type, string msg, Action<bool, GuildChatPinModel> callback)
	{
		GuildChatPinModel.SendForm sendForm = new GuildChatPinModel.SendForm();
		sendForm.id = chatId;
		sendForm.uuid = uuid;
		sendForm.type = type;
		sendForm.message = msg.Replace("\n", "\\n");
		sendForm.fromUserId = fromUserId;
		Protocol.Send(GuildChatPinModel.URL, sendForm, delegate(GuildChatPinModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public void SendClanChatUnPin(Action<bool, GuildChatUnPinModel> callback)
	{
		Protocol.Send(GuildChatUnPinModel.URL, delegate(GuildChatUnPinModel ret)
		{
			pinDonate = null;
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public void SendClanChatOnlineStatus(Action<bool, List<GuildMemberChatStatus>> callback)
	{
		Protocol.Send(GuildChatOnlineStatusModel.URL, delegate(GuildChatOnlineStatusModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret.result.online);
		}, string.Empty);
	}

	public void GetAllPinData(Action<bool, GuildGetPinModel> callback)
	{
		Protocol.Send(GuildGetPinModel.URL, delegate(GuildGetPinModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public void SetAskUpdate(long value)
	{
		if (value > 0 && value != -1)
		{
			askUpdate = value;
		}
	}

	public void SendDonateList(Action<bool> callback)
	{
		Protocol.Send(GuildDonate.GuildDonateListModel.URL, delegate(GuildDonate.GuildDonateListModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			donateList = ret.result.array;
			pinDonate = ret.result.pinDonate;
			DateTime time = DateTime.Parse(ret.currentTime);
			SystemTime = Utility.DateTimeToTimestampMilliseconds(time);
			callback(obj);
		}, string.Empty);
	}

	public void SendDonateFobbidenList(Action<bool, List<int>> callback)
	{
		Protocol.Send(GuildDonate.GuildDonateFobbidenModel.URL, delegate(GuildDonate.GuildDonateFobbidenModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret.result.array);
		}, string.Empty);
	}

	public void SendDonateRequest(int itemId, string itemName, string msg, int quatity, Action<bool> callback)
	{
		GuildDonate.GuildDonateRequestModel.Form form = new GuildDonate.GuildDonateRequestModel.Form();
		form.itemId = itemId;
		form.itemName = itemName;
		form.msg = msg;
		form.quantity = quatity;
		form.nickName = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
		Protocol.Send(GuildDonate.GuildDonateRequestModel.URL, form, delegate(GuildDonate.GuildDonateRequestModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				MonoBehaviourSingleton<UserInfoManager>.I.userStatus.nextDonationTime = DateTime.Parse(ret.result.expired);
			}
			callback(flag);
		}, string.Empty);
	}

	public void SendDonateSend(int donateId, int quatity, Action<bool> callback)
	{
		GuildDonate.GuildDonateSendModel.Form form = new GuildDonate.GuildDonateSendModel.Form();
		form.id = donateId;
		form.quantity = quatity;
		Protocol.Send(GuildDonate.GuildDonateSendModel.URL, form, delegate(GuildDonate.GuildDonateSendModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			callback(obj);
		}, string.Empty);
	}

	private static void SendDonateReceive(Action<bool> callback)
	{
		Protocol.SendAsync(GuildDonate.GuildDonateReceiveModel.URL, delegate(GuildDonate.GuildDonateReceiveModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			if (callback != null)
			{
				callback(obj);
			}
		}, string.Empty);
	}

	public void SendDonateInviteList(int donate_id, Action<bool, GuildDonate.GuildDonateInviteListModel> callback)
	{
		GuildDonate.GuildDonateInviteListModel.Form form = new GuildDonate.GuildDonateInviteListModel.Form();
		form.id = donate_id;
		Protocol.Send(GuildDonate.GuildDonateInviteListModel.URL, form, delegate(GuildDonate.GuildDonateInviteListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			callback.Invoke(flag, ret);
		}, string.Empty);
	}

	public void SendDonateInvite(int id, int user_id, Action<bool> callback)
	{
		GuildDonate.GuildDonateInviteModel.Form form = new GuildDonate.GuildDonateInviteModel.Form();
		form.userId = user_id;
		form.id = id;
		Protocol.Send(GuildDonate.GuildDonateInviteModel.URL, form, delegate(GuildDonate.GuildDonateInviteModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			callback(obj);
		}, string.Empty);
	}

	public void SendDonateInvitationList(Action<bool> callback, bool isResumed = false)
	{
		if (!IsValidInGuild())
		{
			donateInviteList = new List<DonateInvitationInfo>();
			callback(true);
		}
		else
		{
			donateInviteList = null;
			Protocol.Send(GuildDonate.GuildDonateInvitationListModel.URL, delegate(GuildDonate.GuildDonateInvitationListModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					donateInviteList = ret.result.array;
					if (donateInviteList.Count > 0)
					{
						FillterDonateInviteList();
					}
				}
				else
				{
					donateInviteList = new List<DonateInvitationInfo>();
				}
				if (isResumed && donateInviteList.Count > 0)
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SetClanDonateInviteHome();
				}
				callback(obj);
			}, string.Empty);
		}
	}

	private void FillterDonateInviteList()
	{
		double num = DateTimeToTimestampSeconds();
		int num2 = donateInviteList.Count;
		for (int i = 0; i < num2; i++)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != donateInviteList[i].userId)
			{
				double num3 = donateInviteList[i].expired / 1000.0 - num;
				if (num3 < 1.0)
				{
					donateInviteList.RemoveAt(i);
					num2--;
					i--;
				}
				else if (donateInviteList[i].itemNum >= donateInviteList[i].quantity)
				{
					donateInviteList.RemoveAt(i);
					num2--;
					i--;
				}
			}
		}
	}

	private double DateTimeToTimestampSeconds()
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow - d).TotalSeconds;
	}

	public static bool IsValidNotEmptyDonateInviteList()
	{
		return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.donateInviteList != null && MonoBehaviourSingleton<GuildManager>.I.donateInviteList.Count > 0;
	}

	public string GetInviteMessage()
	{
		if (guildData == null)
		{
			return string.Empty;
		}
		return guildData.inviteMessage;
	}

	public string GetInviteHelpURL()
	{
		if (guildData == null)
		{
			return string.Empty;
		}
		return (guildData.inviteFriendInfo == null) ? string.Empty : guildData.inviteFriendInfo.linkUrl;
	}

	public void SendInviteList(Action<bool, GuildInviteCharaInfo[]> call_back)
	{
		if (guildData == null)
		{
			call_back.Invoke(false, (GuildInviteCharaInfo[])null);
		}
		else
		{
			GuildInviteListModel.RequestSendForm requestSendForm = new GuildInviteListModel.RequestSendForm();
			requestSendForm.id = guildData.clanId.ToString();
			Protocol.Send(GuildInviteListModel.URL, requestSendForm, delegate(GuildInviteListModel ret)
			{
				bool flag = false;
				GuildInviteCharaInfo[] array = null;
				if (ret.Error == Error.None)
				{
					flag = true;
					array = ret.result.list.ToArray();
				}
				call_back.Invoke(flag, array);
			}, string.Empty);
		}
	}

	public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
	{
		if (guildData == null)
		{
			call_back.Invoke(false, (int[])null);
		}
		else
		{
			GuildInviteModel.RequestSendForm requestSendForm = new GuildInviteModel.RequestSendForm();
			requestSendForm.id = guildData.clanId.ToString();
			int[] array = userIds;
			foreach (int item in array)
			{
				requestSendForm.inviteList.Add(item);
			}
			Protocol.Send(GuildInviteModel.URL, requestSendForm, delegate(GuildInviteModel ret)
			{
				bool flag = false;
				if (ret.Error == Error.None)
				{
					flag = true;
					call_back.Invoke(flag, userIds);
				}
				else
				{
					call_back.Invoke(flag, (int[])null);
				}
			}, string.Empty);
		}
	}

	public void SendRejectInviteClan(int requestId, Action<bool> call_back)
	{
		GuildRejectInvitedModel.SendForm sendForm = new GuildRejectInvitedModel.SendForm();
		sendForm.requestId = requestId;
		Protocol.Send(GuildRejectInvitedModel.URL, sendForm, delegate(GuildRejectInvitedModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInvitedGuild(Action<bool> callBack, bool isResumed = false)
	{
		if (IsValidInGuild())
		{
			guildInviteList = new List<GuildInvitedModel.GuildInvitedInfo>();
			callBack?.Invoke(true);
		}
		else
		{
			guildInviteList = null;
			Protocol.SendAsync(GuildInvitedModel.RequestInvited.path, delegate(GuildInvitedModel ret)
			{
				guildInviteList = ((ret.Error != 0) ? new List<GuildInvitedModel.GuildInvitedInfo>() : ret.result.list);
				if (isResumed && guildInviteList.Count > 0)
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SetClanInviteHome();
				}
			}, string.Empty);
			callBack?.Invoke(true);
		}
	}

	public static bool IsValidNotEmptyInviteList()
	{
		return MonoBehaviourSingleton<GuildManager>.IsValid() && MonoBehaviourSingleton<GuildManager>.I.guildInviteList != null && MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Count > 0;
	}

	public void SendSearchFollowerRoom(Action<bool, List<GuildSearchFollowerRoomModel.GuildFollowerModel>> call_back)
	{
		Protocol.Send(GuildSearchFollowerRoomModel.URL, delegate(GuildSearchFollowerRoomModel ret)
		{
			call_back.Invoke(ret.Error == Error.None, ret.result.list);
		}, string.Empty);
	}
}
