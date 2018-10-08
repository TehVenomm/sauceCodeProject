using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviourSingleton<PartyManager>
{
	public class PartySetting
	{
		public bool isLock;

		public int level;

		public int total;

		public int reserveLimitLevel;

		public int cs;

		public int ex;

		public PartySetting(bool is_lock, int _level, int _total, int _cs = 0, int _ex = 0)
		{
			isLock = is_lock;
			level = _level;
			total = _total;
			reserveLimitLevel = level;
			cs = _cs;
			ex = _ex;
		}
	}

	private bool isChangeStarted;

	private string inviteValue = string.Empty;

	public bool is_repeat_quest;

	public int repeatPartyStatus;

	private int setting_cs;

	private int setting_ce;

	public List<PartyModel.Party> partys
	{
		get;
		private set;
	}

	public PartyModel.Party partyData
	{
		get;
		private set;
	}

	public PartyModel.InviteFriendInfo inviteFriendInfo
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

	public QuestSearchRoomCondition.SearchRequestParam searchRequest
	{
		get;
		private set;
	}

	public QuestSearchRoomCondition.SearchRequestParam searchRequestTemp
	{
		get;
		private set;
	}

	public QuestRushSearchRoomCondition.RushSearchRequestParam rushSearchRequest
	{
		get;
		private set;
	}

	public List<int> nowRushQuestIds
	{
		get;
		private set;
	}

	public List<FollowPartyMember> followPartyMember
	{
		get;
		private set;
	}

	public List<IsEquipPartyMember> isEquipPartyMember
	{
		get;
		private set;
	}

	public PartyModel.PartyServer partyServerData
	{
		get;
		private set;
	}

	public PartyModel.RandomMatchingInfo randomMatchingInfo
	{
		get;
		private set;
	}

	public QuestChallengeInfoModel.Param challengeInfo
	{
		get;
		private set;
	}

	public PartySetting partySetting
	{
		get;
		private set;
	}

	public PartyManager()
	{
		partys = null;
		partyData = null;
		partyServerData = null;
	}

	protected override void Awake()
	{
		base.Awake();
		base.gameObject.AddComponent<PartyWebSocket>();
		base.gameObject.AddComponent<PartyNetworkManager>();
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
	}

	private void UpdateParty(PartyModel.Party party, List<FollowPartyMember> followPartyMember, PartyModel.PartyServer partyServer, PartyModel.InviteFriendInfo inviteFriendInfo, List<IsEquipPartyMember> isEquipList = null)
	{
		if (partyData != null && partyData.status == 10 && (party.status == 100 || party.status == 105))
		{
			isChangeStarted = true;
		}
		this.inviteFriendInfo = inviteFriendInfo;
		if (partyData == null && party != null)
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendStartQuest(party);
		}
		partyData = party;
		if (followPartyMember != null)
		{
			this.followPartyMember = followPartyMember;
		}
		if (isEquipList != null)
		{
			isEquipPartyMember = isEquipList;
		}
		if (partyServer != null)
		{
			partyServerData = partyServer;
		}
		if (party != null && MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.Init(party);
			if (followPartyMember != null)
			{
				MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetPartyFollowInfo(followPartyMember);
			}
		}
	}

	private void ClearParty()
	{
		repeatPartyStatus = -1;
		is_repeat_quest = false;
		partyData = null;
		partyServerData = null;
		isChangeStarted = false;
		randomMatchingInfo = null;
	}

	private void UpdatePartyList(List<PartyModel.Party> partys)
	{
		this.partys = partys;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SEARCH_ROOM_LIST);
	}

	private void UpdateRandomMatchingInfo(PartyModel.RandomMatchingInfo info)
	{
		randomMatchingInfo = info;
	}

	public static bool IsValidNotEmptyList()
	{
		return MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.partys != null && MonoBehaviourSingleton<PartyManager>.I.partys.Count > 0;
	}

	public static bool IsValidInParty()
	{
		return MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.IsInParty();
	}

	public bool IsInParty()
	{
		return partyData != null;
	}

	public string GetPartyId()
	{
		return (partyData == null) ? string.Empty : partyData.id;
	}

	public string GetPartyNumber()
	{
		return (partyData == null) ? string.Empty : partyData.partyNumber;
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
		return (PARTY_STATUS)((partyData != null) ? partyData.status : 0);
	}

	public int GetOwnerUserId()
	{
		return (partyData != null) ? partyData.ownerUserId : 0;
	}

	public uint GetQuestId()
	{
		return (uint)((partyData != null && partyData.quest != null) ? partyData.quest.questId : 0);
	}

	public int GetSlotIndex(int user_id)
	{
		return (partyData == null) ? (-1) : partyData.slotInfos.FindIndex((PartyModel.SlotInfo s) => s.userInfo != null && s.userInfo.userId == user_id);
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

	public bool IsEquipChangeByIndex(int idx)
	{
		PartyModel.SlotInfo slotInfoByIndex = GetSlotInfoByIndex(idx);
		if (isEquipPartyMember != null)
		{
			for (int i = 0; i < isEquipPartyMember.Count; i++)
			{
				if (isEquipPartyMember[i].memberId == slotInfoByIndex.userInfo.userId)
				{
					return isEquipPartyMember[i].isEquip;
				}
			}
		}
		return false;
	}

	public bool IsPayingQuest()
	{
		if (partyData == null)
		{
			Log.Error("IsPayingQuest :: PartyData is NULL");
			return false;
		}
		return partyData.quest.paying != null && !partyData.quest.paying.free;
	}

	public List<int> GetMemberUserIdList(int my_userid = 0)
	{
		List<int> member_list = new List<int>();
		if (my_userid > 0)
		{
			member_list.Add(my_userid);
		}
		if (partyData != null && partyData.slotInfos != null)
		{
			partyData.slotInfos.ForEach(delegate(PartyModel.SlotInfo slot)
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

	public void SetFollowPartyMember(List<FollowPartyMember> _followPartyMember)
	{
		followPartyMember = _followPartyMember;
	}

	public FollowPartyMember GetFollowPartyMember(int userId)
	{
		if (followPartyMember == null)
		{
			return null;
		}
		return followPartyMember.Find((FollowPartyMember d) => d.userId == userId);
	}

	public void SendFollowAgency(List<int> send_follow_list, Action<bool> callback = null)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(send_follow_list, delegate(Error err, List<int> follow_list)
		{
			bool flag = err == Error.None && follow_list.Count > 0;
			if (flag)
			{
				bool updated = false;
				send_follow_list.ForEach(delegate(int userId)
				{
					FollowPartyMember followPartyMember = GetFollowPartyMember(userId);
					if (followPartyMember != null && !followPartyMember.following)
					{
						followPartyMember.following = true;
						updated = true;
					}
				});
				if (updated)
				{
					UpdateParty(partyData, followPartyMember, partyServerData, inviteFriendInfo, null);
				}
			}
			if (callback != null)
			{
				callback(flag);
			}
		});
	}

	public void SendUnFollowAgency(int send_unfollow_user_id, Action<bool> callback = null)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendUnfollowUser(send_unfollow_user_id, delegate(bool is_success)
		{
			if (is_success)
			{
				FollowPartyMember followPartyMember = GetFollowPartyMember(send_unfollow_user_id);
				if (followPartyMember != null && followPartyMember.following)
				{
					followPartyMember.following = false;
					UpdateParty(partyData, this.followPartyMember, partyServerData, inviteFriendInfo, null);
				}
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	public void SetSearchRequestTemp(QuestSearchRoomCondition.SearchRequestParam request)
	{
		if (request != null)
		{
			searchRequestTemp = searchRequest;
			searchRequest = request;
		}
	}

	public void ResetSearchRequestTemp()
	{
		if (searchRequestTemp != null)
		{
			searchRequest = searchRequestTemp;
			searchRequestTemp = null;
		}
	}

	public void SetSearchRequest(QuestSearchRoomCondition.SearchRequestParam request = null)
	{
		if (request != null)
		{
			ResetSearchRequestTemp();
			searchRequest = request;
		}
		else if (searchRequest == null)
		{
			ResetSearchRequestTemp();
			ResetSearchRequest();
		}
	}

	public void SetRushSearchRequest(QuestRushSearchRoomCondition.RushSearchRequestParam request)
	{
		if (request != null)
		{
			rushSearchRequest = request;
		}
	}

	public void ResetSearchRequest()
	{
		searchRequest = new QuestSearchRoomCondition.SearchRequestParam();
	}

	public void ResetRushSearchRequest()
	{
		rushSearchRequest = new QuestRushSearchRoomCondition.RushSearchRequestParam();
	}

	public void SendSearch(Action<bool, Error> call_back, bool saveSettings)
	{
		partys = null;
		SetSearchRequest(null);
		PartySearchModel.RequestSendForm requestSendForm = new PartySearchModel.RequestSendForm();
		requestSendForm.order = searchRequest.order;
		requestSendForm.rarityBit = searchRequest.rarityBit;
		requestSendForm.elementBit = searchRequest.elementBit;
		requestSendForm.enemyLevelMin = searchRequest.enemyLevelMin;
		requestSendForm.enemyLevelMax = searchRequest.enemyLevelMax;
		if (!string.IsNullOrEmpty(searchRequest.targetEnemySpeciesName))
		{
			requestSendForm.enemySpecies = searchRequest.GetEnemySpeciesId(searchRequest.targetEnemySpeciesName);
		}
		requestSendForm.questTypeBit = searchRequest.questTypeBit;
		if (saveSettings)
		{
			SaveGachaSearchSettings();
		}
		Protocol.Send(PartySearchModel.URL, requestSendForm, delegate(PartySearchModel ret)
		{
			bool arg = false;
			Error error = ret.Error;
			if (error == Error.None || error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				if (ret.Error == Error.None)
				{
					arg = true;
				}
				UpdatePartyList(ret.result.partys);
			}
			call_back(arg, ret.Error);
		}, string.Empty);
	}

	public void SendSearchRandomMatching(Action<bool, Error> call_back)
	{
		ClearParty();
		SetSearchRequest(null);
		PartyModel.RequestSearchRandomMatching requestSearchRandomMatching = new PartyModel.RequestSearchRandomMatching();
		requestSearchRandomMatching.token = GenerateToken();
		requestSearchRandomMatching.order = searchRequest.order;
		requestSearchRandomMatching.rarityBit = searchRequest.rarityBit;
		requestSearchRandomMatching.elementBit = searchRequest.elementBit;
		requestSearchRandomMatching.enemyLevelMin = searchRequest.enemyLevelMin;
		requestSearchRandomMatching.enemyLevelMax = searchRequest.enemyLevelMax;
		if (!string.IsNullOrEmpty(searchRequest.targetEnemySpeciesName))
		{
			requestSearchRandomMatching.enemySpecies = searchRequest.GetEnemySpeciesId(searchRequest.targetEnemySpeciesName);
		}
		requestSearchRandomMatching.questTypeBit = searchRequest.questTypeBit;
		SaveGachaSearchSettings();
		Protocol.Send(PartyModel.RequestSearchRandomMatching.path, requestSearchRandomMatching, delegate(PartyModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None && ret.result.party != null)
			{
				arg = true;
				UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, null);
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
		}, string.Empty);
	}

	private void SaveGachaSearchSettings()
	{
		PlayerPrefs.SetInt("GACHA_SEARCH_RAIRTY_KEY", searchRequest.rarityBit);
		PlayerPrefs.SetInt("GACHA_SEARCH_ELEMENT_KEY", searchRequest.elementBit);
		PlayerPrefs.SetInt("GACHA_SEARCH_LEVEL_MIN_KEY", searchRequest.enemyLevelMin);
		PlayerPrefs.SetInt("GACHA_SEARCH_LEVEL_MAX_KEY", searchRequest.enemyLevelMax);
		if (!string.IsNullOrEmpty(searchRequest.targetEnemySpeciesName))
		{
			PlayerPrefs.SetString("GACHA_SEARCH_SPECIES_KEY", searchRequest.targetEnemySpeciesName);
		}
		PlayerPrefs.Save();
	}

	public void SetSearchRequestFromPrefs()
	{
		searchRequest = new QuestSearchRoomCondition.SearchRequestParam();
		searchRequest.rarityBit = PlayerPrefs.GetInt("GACHA_SEARCH_RAIRTY_KEY", 8388607);
		searchRequest.elementBit = PlayerPrefs.GetInt("GACHA_SEARCH_ELEMENT_KEY", 8388607);
		searchRequest.enemyLevelMin = PlayerPrefs.GetInt("GACHA_SEARCH_LEVEL_MIN_KEY", 1);
		int defaultValue = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_LEVEL_MAX;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX > 0)
		{
			defaultValue = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX;
		}
		searchRequest.enemyLevelMax = PlayerPrefs.GetInt("GACHA_SEARCH_LEVEL_MAX_KEY", defaultValue);
		searchRequest.targetEnemySpeciesName = PlayerPrefs.GetString("GACHA_SEARCH_SPECIES_KEY", null);
	}

	public void SendRushSearch(Action<bool, Error> call_back, bool saveSettings)
	{
		partys = null;
		if (rushSearchRequest == null)
		{
			ResetRushSearchRequest();
		}
		PartySearchRushModel.RequestSendForm requestSendForm = new PartySearchRushModel.RequestSendForm();
		requestSendForm.floorMinQuestId = rushSearchRequest.minFloorQuestId;
		requestSendForm.floorMaxQuestId = rushSearchRequest.maxFloorQuestId;
		if (saveSettings)
		{
			SaveRushSearchSettings();
		}
		Protocol.Send(PartySearchRushModel.URL, requestSendForm, delegate(PartySearchModel ret)
		{
			bool arg = false;
			Error error = ret.Error;
			if (error == Error.None || error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				if (ret.Error == Error.None)
				{
					arg = true;
				}
				UpdatePartyList(ret.result.partys);
			}
			call_back(arg, ret.Error);
		}, string.Empty);
	}

	public void SendRushSearchRandomMatching(Action<bool, Error> call_back)
	{
		ClearParty();
		if (rushSearchRequest == null)
		{
			ResetRushSearchRequest();
		}
		PartyModel.RequestSearchRushRandomMatching requestSearchRushRandomMatching = new PartyModel.RequestSearchRushRandomMatching();
		requestSearchRushRandomMatching.token = GenerateToken();
		requestSearchRushRandomMatching.floorMinQuestId = rushSearchRequest.minFloorQuestId;
		requestSearchRushRandomMatching.floorMaxQuestId = rushSearchRequest.maxFloorQuestId;
		SaveRushSearchSettings();
		Protocol.Send(PartyModel.RequestSearchRushRandomMatching.path, requestSearchRushRandomMatching, delegate(PartyModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None && ret.result.party != null)
			{
				arg = true;
				UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, null);
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
		}, string.Empty);
	}

	private void SaveRushSearchSettings()
	{
		PlayerPrefs.SetInt("RUSH_SEARCH_MAX_QUESTID_KEY", rushSearchRequest.maxFloorQuestId);
		PlayerPrefs.SetInt("RUSH_SEARCH_MIN_QUESTID_KEY", rushSearchRequest.minFloorQuestId);
		PlayerPrefs.Save();
	}

	public void SetRushRequestFromPrefs()
	{
		rushSearchRequest = new QuestRushSearchRoomCondition.RushSearchRequestParam();
		rushSearchRequest.maxFloorQuestId = PlayerPrefs.GetInt("RUSH_SEARCH_MAX_QUESTID_KEY", 0);
		rushSearchRequest.minFloorQuestId = PlayerPrefs.GetInt("RUSH_SEARCH_MIN_QUESTID_KEY", 0);
	}

	public void SetNowRushQuestIds(List<int> idList)
	{
		nowRushQuestIds = idList;
	}

	public void SendEventSearch(int eventId, Action<bool, Error> call_back)
	{
		partys = null;
		PartySearchEventModel.RequestSendForm requestSendForm = new PartySearchEventModel.RequestSendForm();
		requestSendForm.eid = eventId;
		Protocol.Send(PartySearchEventModel.URL, requestSendForm, delegate(PartySearchModel ret)
		{
			bool arg = false;
			Error error = ret.Error;
			if (error == Error.None || error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				if (ret.Error == Error.None)
				{
					arg = true;
				}
				UpdatePartyList(ret.result.partys);
			}
			call_back(arg, ret.Error);
		}, string.Empty);
	}

	public void SetPartySetting(PartySetting setting)
	{
		partySetting = setting;
	}

	public void SendRandomMatching(int questId, int retryCount, bool isExplore, Action<bool, int, bool, float> call_back)
	{
		ClearParty();
		PartyModel.RequestRandomMatching requestRandomMatching = new PartyModel.RequestRandomMatching();
		requestRandomMatching.qid = questId;
		requestRandomMatching.retryCount = retryCount;
		requestRandomMatching.token = GenerateToken();
		requestRandomMatching.ce = (isExplore ? 1 : 0);
		Protocol.Send(PartyModel.RequestRandomMatching.path, requestRandomMatching, delegate(PartyModel ret)
		{
			bool arg = false;
			bool arg2 = false;
			int arg3 = 0;
			float arg4 = 0f;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg3 = ret.result.randomMatchingInfo.maxRetryCount;
				arg4 = ret.result.randomMatchingInfo.waitTime;
				if (ret.result.party != null)
				{
					UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, null);
					Dirty();
					arg3 = 0;
					arg2 = true;
				}
			}
			call_back(arg, arg3, arg2, arg4);
		}, string.Empty);
	}

	public void SendMatching(int questId, Action<Error, bool> call_back)
	{
		ClearParty();
		PartyModel.RequestMatching requestMatching = new PartyModel.RequestMatching();
		requestMatching.qid = questId;
		requestMatching.token = GenerateToken();
		Protocol.Send(PartyModel.RequestMatching.path, requestMatching, delegate(PartyModel ret)
		{
			bool arg = false;
			switch (ret.Error)
			{
			case Error.None:
				arg = true;
				UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, null);
				Dirty();
				break;
			case Error.WRN_QUEST_IS_ORDER:
				arg = false;
				break;
			}
			call_back(ret.Error, arg);
		}, string.Empty);
	}

	public void SendCreate(int questId, PartySetting party_setting, Action<bool> call_back)
	{
		ClearParty();
		PartyModel.RequestCreate requestCreate = new PartyModel.RequestCreate();
		requestCreate.qid = questId;
		requestCreate.token = GenerateToken();
		requestCreate.isLock = (party_setting.isLock ? 1 : 0);
		requestCreate.lv = party_setting.level;
		requestCreate.power = party_setting.total;
		requestCreate.cs = party_setting.cs;
		requestCreate.ce = party_setting.ex;
		setting_cs = party_setting.cs;
		setting_ce = party_setting.ex;
		if (followPartyMember != null)
		{
			followPartyMember.Clear();
		}
		Protocol.Send(PartyModel.RequestCreate.path, requestCreate, delegate(PartyModel ret)
		{
			bool obj = false;
			switch (ret.Error)
			{
			case Error.None:
				obj = true;
				MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable = ret.result.repeatFeatureEnable;
				UpdateParty(ret.result.party, null, ret.result.partyServer, ret.result.inviteFriendInfo, null);
				Dirty();
				break;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendApply(string partyNumber, Action<bool, Error> call_back, int questId = 0)
	{
		ClearParty();
		PartyModel.RequestApply requestApply = new PartyModel.RequestApply();
		requestApply.token = GenerateToken();
		requestApply.partyNumber = partyNumber;
		requestApply.qid = questId;
		Protocol.Send(PartyModel.RequestApply.path, requestApply, delegate(PartyModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
				UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, null);
				MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable = ret.result.repeatFeatureEnable;
				is_repeat_quest = (ret.result.repeatStatus == 1);
				Dirty();
			}
			if (call_back != null)
			{
				call_back(arg, ret.Error);
			}
		}, string.Empty);
	}

	public void SendEntry(string id, bool isLoungeBoard, Action<bool> call_back)
	{
		ClearParty();
		PartyModel.RequestEntry requestEntry = new PartyModel.RequestEntry();
		requestEntry.token = GenerateToken();
		requestEntry.id = id;
		requestEntry.isLoungeBoard = (isLoungeBoard ? 1 : 0);
		Protocol.Send(PartyModel.RequestEntry.path, requestEntry, delegate(PartyModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, null);
				MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable = ret.result.repeatFeatureEnable;
				is_repeat_quest = (ret.result.repeatStatus == 1);
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInfo(Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			if (MonoBehaviourSingleton<QuestManager>.IsValid())
			{
				MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.Clear();
			}
			PartyModel.RequestInfo requestInfo = new PartyModel.RequestInfo();
			requestInfo.id = partyData.id;
			Protocol.Send(PartyModel.RequestInfo.path, requestInfo, delegate(PartyModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					UpdateParty(ret.result.party, ret.result.friend, ret.result.partyServer, ret.result.inviteFriendInfo, ret.result.isEquipList);
					is_repeat_quest = (ret.result.repeatStatus == 1);
					Dirty();
				}
				else
				{
					ClearParty();
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void SendIsEquip(bool isEquip, Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			PartyModel.RequestIsEquip requestIsEquip = new PartyModel.RequestIsEquip();
			requestIsEquip.id = partyData.id;
			requestIsEquip.isEquip = isEquip;
			Protocol.Send(PartyModel.RequestIsEquip.path, requestIsEquip, delegate(PartyModel ret)
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

	public void SendReady(bool enable_ready, Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			int slotIndex = GetSlotIndex(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			if (slotIndex < 0)
			{
				call_back(false);
			}
			else
			{
				PARTY_PLAYER_STATUS pARTY_PLAYER_STATUS = (!enable_ready) ? PARTY_PLAYER_STATUS.JOINED : PARTY_PLAYER_STATUS.READY;
				if (partyData.slotInfos[slotIndex].status == (int)pARTY_PLAYER_STATUS)
				{
					call_back(true);
				}
				else
				{
					partyData.slotInfos[slotIndex].status = (int)pARTY_PLAYER_STATUS;
					PartyModel.RequestReady requestReady = new PartyModel.RequestReady();
					requestReady.id = partyData.id;
					requestReady.enable = (enable_ready ? 1 : 0);
					Protocol.Send(PartyModel.RequestReady.path, requestReady, delegate(PartyModel ret)
					{
						bool obj = false;
						if (ret.Error == Error.None)
						{
							obj = true;
							UpdateParty(ret.result.party, null, null, ret.result.inviteFriendInfo, null);
							Dirty();
						}
						call_back(obj);
					}, string.Empty);
				}
			}
		}
	}

	public void SendLeave(Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			PartyLeaveModel.RequestSendForm requestSendForm = new PartyLeaveModel.RequestSendForm();
			requestSendForm.id = partyData.id;
			if (followPartyMember != null)
			{
				followPartyMember.Clear();
			}
			Protocol.Send(PartyLeaveModel.URL, requestSendForm, delegate(PartyLeaveModel ret)
			{
				bool obj = false;
				Error error = ret.Error;
				if (error == Error.None || error == Error.ERR_PARTY_NOT_FOUND_PARTY)
				{
					obj = true;
					ClearParty();
					Dirty();
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void SendEdit(PartySetting party_setting, Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			PartyModel.RequestEdit requestEdit = new PartyModel.RequestEdit();
			requestEdit.id = partyData.id;
			requestEdit.isLock = (party_setting.isLock ? 1 : 0);
			requestEdit.lv = party_setting.level;
			requestEdit.power = party_setting.total;
			Protocol.Send(PartyModel.RequestEdit.path, requestEdit, delegate(PartyModel ret)
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

	public void SendInviteList(Action<bool, PartyInviteCharaInfo[]> call_back)
	{
		if (partyData == null)
		{
			call_back(false, null);
		}
		else
		{
			PartyInviteListModel.RequestSendForm requestSendForm = new PartyInviteListModel.RequestSendForm();
			requestSendForm.id = partyData.id;
			Protocol.Send(PartyInviteListModel.URL, requestSendForm, delegate(PartyInviteListModel ret)
			{
				bool arg = false;
				PartyInviteCharaInfo[] arg2 = null;
				if (ret.Error == Error.None)
				{
					arg = true;
					arg2 = ret.result.ToArray();
				}
				call_back(arg, arg2);
			}, string.Empty);
		}
	}

	public void SendInvite(int[] userIds, Action<bool, int[]> call_back)
	{
		if (partyData == null)
		{
			call_back(false, null);
		}
		else
		{
			PartyInviteModel.RequestSendForm requestSendForm = new PartyInviteModel.RequestSendForm();
			requestSendForm.id = partyData.id;
			foreach (int item in userIds)
			{
				requestSendForm.ids.Add(item);
			}
			Protocol.Send(PartyInviteModel.URL, requestSendForm, delegate(PartyInviteModel ret)
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
			}, string.Empty);
		}
	}

	public void SendInvitedParty(Action<bool> call_back, bool isResumed = false)
	{
		partys = null;
		Protocol.Send(PartyInvitedPartyModel.URL, delegate(PartyInvitedPartyModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				UpdatePartyList(ret.result.partys);
				if (IsValidNotEmptyList() && isResumed)
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SetPartyInviteResume(true);
				}
			}
			call_back(obj);
		}, string.Empty);
	}

	public PartyNetworkManager.ConnectData GetWebSockConnectData()
	{
		if (partyData == null || partyServerData == null)
		{
			return null;
		}
		int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		int slotIndex = GetSlotIndex(id);
		if (slotIndex < 0)
		{
			return null;
		}
		PartyNetworkManager.ConnectData connectData = new PartyNetworkManager.ConnectData();
		connectData.path = partyServerData.wsHost;
		connectData.ports = partyServerData.wsPorts;
		connectData.fromId = id;
		connectData.ackPrefix = slotIndex;
		connectData.roomId = partyData.id;
		connectData.owner = partyData.ownerUserId;
		connectData.ownerToken = partyServerData.token;
		connectData.uid = id;
		connectData.signature = partyServerData.signature;
		return connectData;
	}

	public void ConnectServer(Action<bool, bool> call_back = null)
	{
		PartyNetworkManager.ConnectData webSockConnectData = GetWebSockConnectData();
		if (webSockConnectData == null)
		{
			if (call_back != null)
			{
				call_back(false, false);
			}
		}
		else if (!MonoBehaviourSingleton<PartyNetworkManager>.IsValid())
		{
			if (call_back != null)
			{
				call_back(false, false);
			}
		}
		else
		{
			MonoBehaviourSingleton<PartyNetworkManager>.I.ConnectAndRegist(webSockConnectData, delegate(bool is_connect, bool is_regist)
			{
				if (!is_regist)
				{
					goto IL_0006;
				}
				goto IL_0006;
				IL_0006:
				if (call_back != null)
				{
					call_back(is_connect, is_regist);
				}
			});
		}
	}

	public void SendGetChallengeInfo(Action<bool, Error> call_back)
	{
		if (HomeTutorialManager.ShouldRunGachaTutorial())
		{
			challengeInfo = new QuestChallengeInfoModel.Param();
			if (call_back != null)
			{
				call_back(true, Error.None);
			}
		}
		else
		{
			challengeInfo = null;
			QuestChallengeInfoModel.RequestSendForm post_data = new QuestChallengeInfoModel.RequestSendForm();
			Protocol.Send(QuestChallengeInfoModel.URL, post_data, delegate(QuestChallengeInfoModel ret)
			{
				bool arg = false;
				if (ret.Error == Error.None)
				{
					arg = true;
					challengeInfo = ret.result;
				}
				if (call_back != null)
				{
					call_back(arg, ret.Error);
				}
			}, string.Empty);
		}
	}

	public void SendRepeat(bool isOn, Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			PartyRepeatModel.RequestSendForm requestSendForm = new PartyRepeatModel.RequestSendForm();
			requestSendForm.id = partyData.id;
			requestSendForm.st = (isOn ? 1 : 0);
			requestSendForm.cs = setting_cs;
			requestSendForm.ce = setting_ce;
			Protocol.Send(PartyRepeatModel.URL, requestSendForm, delegate(PartyRepeatModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					is_repeat_quest = isOn;
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void SendGetNextParty(Action<bool> call_back)
	{
		if (partyData == null)
		{
			call_back(false);
		}
		else
		{
			PartyNextModel.RequestSendForm requestSendForm = new PartyNextModel.RequestSendForm();
			requestSendForm.id = partyData.id;
			Protocol.Send(PartyNextModel.URL, requestSendForm, delegate(PartyNextModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					repeatPartyStatus = ret.result.repeatPartyStatus;
					if (ret.result.repeatPartyStatus > 0)
					{
						UpdateParty(ret.result.party, null, ret.result.partyServer, ret.result.inviteFriendInfo, null);
						is_repeat_quest = (ret.result.repeatStatus == 1);
					}
					obj = true;
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void UpdatePartyRepeat(PartyModel.Party party, List<FollowPartyMember> followPartyMember, PartyModel.PartyServer partyServer, PartyModel.InviteFriendInfo inviteFriendInfo, List<IsEquipPartyMember> isEquipList = null)
	{
		UpdateParty(party, followPartyMember, partyServer, inviteFriendInfo, isEquipList);
		Dirty();
	}
}
