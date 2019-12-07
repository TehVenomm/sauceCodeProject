using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviourSingleton<UserInfoManager>
{
	public bool needBlackMarketNotifi = true;

	public bool rallyInviteChat;

	private bool partyInviteHome;

	private bool partyInviteChat;

	private bool partyInviteResume;

	public bool showJoinClanInGame;

	public bool showBlackMarketBanner;

	public bool showFortuneWheel;

	public bool showTradingPost;

	public bool repeatPartyEnable;

	public bool isWheelOfFortuneOn;

	private List<string> m_alertMessages;

	public int clanInviteNum;

	public PointShopManager PointShopManager = new PointShopManager();

	private static readonly TUTORIAL_MENU_BIT[] needTutorialBits = new TUTORIAL_MENU_BIT[28]
	{
		TUTORIAL_MENU_BIT.GACHA1,
		TUTORIAL_MENU_BIT.GACHA2,
		TUTORIAL_MENU_BIT.GACHA_QUEST_WIN,
		TUTORIAL_MENU_BIT.GACHA_QUEST_GACHA_RESULT,
		TUTORIAL_MENU_BIT.GACHA_QUEST_START,
		TUTORIAL_MENU_BIT.GACHA_QUEST_BATTLE_RESULT,
		TUTORIAL_MENU_BIT.GACHA_QUEST_END,
		TUTORIAL_MENU_BIT.SKILL_EQUIP,
		TUTORIAL_MENU_BIT.CLAIM_REWARD,
		TUTORIAL_MENU_BIT.FORGE_ITEM,
		TUTORIAL_MENU_BIT.AFTER_GACHA2,
		TUTORIAL_MENU_BIT.SHADOW_QUEST_START,
		TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN,
		TUTORIAL_MENU_BIT.SHADOW_QUEST_RESULT,
		TUTORIAL_MENU_BIT.SHADOW_QUEST_END,
		TUTORIAL_MENU_BIT.UPGRADE_ITEM,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL2,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL3,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL4,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL5,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL6,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL7,
		TUTORIAL_MENU_BIT.UPGRADE_LEVEL8,
		TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM,
		TUTORIAL_MENU_BIT.WORLD_MAP,
		TUTORIAL_MENU_BIT.AFTER_QUEST,
		TUTORIAL_MENU_BIT.AFTER_MAINSTATUS,
		TUTORIAL_MENU_BIT.DONE_CHANGE_WEAPON
	};

	public UserInfo userInfo
	{
		get;
		private set;
	}

	public UserStatus userStatus
	{
		get;
		private set;
	}

	public List<EventBanner> eventBannerList
	{
		get;
		private set;
	}

	public List<GachaDeco> gachaDecoList
	{
		get;
		private set;
	}

	public long gachaDecoDateBase
	{
		get;
		private set;
	}

	public int newsID
	{
		get;
		private set;
	}

	public bool needOpenNewsPage
	{
		get;
		private set;
	}

	public DateTime? mysteryTimeDt
	{
		get;
		private set;
	}

	public List<int> favoriteStampIds
	{
		get;
		private set;
	}

	public List<int> unlockStampIds
	{
		get;
		private set;
	}

	public List<int> selectedDegreeIds
	{
		get;
		private set;
	}

	public List<int> unlockedDegreeIds
	{
		get;
		private set;
	}

	public int crystalChangeName
	{
		get;
		private set;
	}

	public string userIdHash
	{
		get;
		set;
	}

	public string oncePurchaseGachaProductId
	{
		get;
		set;
	}

	public UserClanData userClan
	{
		get;
		private set;
	}

	public ClanAdvisaryData advisory
	{
		get;
		set;
	}

	public bool needShowOneTimesOfferSS
	{
		get;
		set;
	}

	public bool isShowAppReviewAppeal
	{
		get;
		private set;
	}

	public bool isArenaOpen
	{
		get;
		private set;
	}

	public bool isJoinedArenaRanking
	{
		get;
		private set;
	}

	public bool isGuildRequestOpen
	{
		get;
		private set;
	}

	public DISPLAY_TYPE clanDisplayType
	{
		get;
		private set;
	}

	public int clanRequestNum
	{
		get;
		private set;
	}

	public bool isAcquiredUserInfo
	{
		get;
		private set;
	}

	public bool isShadowChallengeFirst
	{
		get;
		private set;
	}

	public bool isTheaterRenewal
	{
		get;
		private set;
	}

	public string alertMessage
	{
		get
		{
			if (m_alertMessages.Count <= 0)
			{
				return null;
			}
			return m_alertMessages[0];
		}
	}

	public bool ExistsPartyInvite => partyInviteHome | partyInviteChat | partyInviteResume | rallyInviteChat;

	public bool ExistsRallyInvite => rallyInviteChat;

	public List<Network.HomeBanner> homeBannerList
	{
		get;
		private set;
	}

	private int equipItemNum => MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetCount();

	private int skillItemNum => MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount();

	public static bool IsValidUser()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			return MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null;
		}
		return false;
	}

	public int SetClanRequestNum(int num)
	{
		if (clanRequestNum != num)
		{
			clanRequestNum = num;
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
		}
		return clanRequestNum;
	}

	public int DecreaseClanRequestNum()
	{
		clanRequestNum--;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
		return clanRequestNum;
	}

	public void SetClanScoutNum(int scoutNum)
	{
		clanInviteNum = scoutNum;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
	}

	public void LeaveClan()
	{
		userClan.Clear();
	}

	public bool IsRegisteredClan()
	{
		return userClan.IsRegistered();
	}

	public void ClearPartyInvite()
	{
		partyInviteHome = false;
		partyInviteChat = false;
		partyInviteResume = false;
		rallyInviteChat = false;
	}

	public void SetPartyInviteChat(bool flag)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
		partyInviteChat = flag;
	}

	public void SetRallyInviteChat(bool flag)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_RALLY_INVITE);
		rallyInviteChat = flag;
	}

	public void SetPartyInviteResume(bool flag)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
		partyInviteResume = flag;
	}

	public void SetClanInviteHome()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
		partyInviteHome = true;
	}

	public void SetClanDonateInviteHome()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
		partyInviteHome = true;
	}

	public void OnReadAlertMessage()
	{
		m_alertMessages.RemoveAt(0);
	}

	public void SetEventBannerList(List<EventBanner> list)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER);
		if (list == null)
		{
			ResetEventBannerList();
		}
		else
		{
			eventBannerList = list;
		}
	}

	public void ResetEventBannerList()
	{
		if (eventBannerList != null)
		{
			eventBannerList.Clear();
		}
	}

	public void SetGachaDecoList(List<GachaDeco> list)
	{
		gachaDecoList = list;
		gachaDecoList.Sort((GachaDeco a, GachaDeco b) => a.orderNo.CompareTo(b.orderNo));
		gachaDecoDateBase = TimeManager.GetNow().Ticks;
	}

	public bool shouldDispAdvancedOffer()
	{
		if (!userInfo.IsAdvanced)
		{
			return userInfo.isCharged;
		}
		return false;
	}

	public bool CheckTutorialBit(TUTORIAL_MENU_BIT bit)
	{
		if (bit == TUTORIAL_MENU_BIT.MAX)
		{
			return false;
		}
		return (userStatus.TutorialBit & (1L << (int)bit)) != 0;
	}

	public bool IsEndTutorialBit()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
		{
			return (userStatus.TutorialBit & 0x200000000) != 0;
		}
		return false;
	}

	public bool IsEndTutorial()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS))
			{
				return MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST);
			}
			return false;
		}
		return false;
	}

	public bool CheckTutorialBitUnlock(TUTORIAL_MENU_BIT bit, long oldTutorialBit)
	{
		if ((oldTutorialBit & (1L << (int)bit)) != 0)
		{
			return false;
		}
		return CheckTutorialBit(bit);
	}

	public static bool IsRegisterdAge()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			return false;
		}
		return true;
	}

	public static bool IsEnableCommunication()
	{
		return true;
	}

	public static bool IsNeedsTutorialMessage()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			return true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialBit == null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 9)
		{
			return true;
		}
		for (int i = 0; i < needTutorialBits.Length; i++)
		{
			if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(needTutorialBits[i]))
			{
				return true;
			}
		}
		return false;
	}

	private UserInfoManager()
	{
		userInfo = new UserInfo();
		userStatus = new UserStatus();
		favoriteStampIds = new List<int>();
		m_alertMessages = new List<string>();
		userClan = new UserClanData();
	}

	private void Update()
	{
		if (userInfo != null && userInfo.id > 0 && userInfo.constDefine.ALIVE_CHECK_SEC > 0 && Time.time - MonoBehaviourSingleton<NetworkManager>.I.lastRequestTime >= (float)userInfo.constDefine.ALIVE_CHECK_SEC)
		{
			SendAlive();
		}
	}

	public void DirtyUserInfo()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_USER_INFO);
	}

	public void DirtyUserStatus()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS);
	}

	private void SetUserInfo(UserInfo user_info)
	{
		LoadFavoriteStamp();
		userInfo = user_info;
		CrashlyticsReporter.SetUserInfo(user_info);
		MonoBehaviourSingleton<GoWrapManager>.I.setGUID(user_info.code);
		DirtyUserInfo();
	}

	private void SetUserStatus(UserStatus user_status)
	{
		userStatus = user_status;
		DirtyUserStatus();
	}

	public void SetUserClan(UserClanData user_clan)
	{
		userClan = user_clan;
	}

	public void SetFavoriteStamp(List<int> setFavorites)
	{
		if (setFavorites.Count > 10)
		{
			setFavorites.RemoveRange(10, setFavorites.Count - 10);
		}
		favoriteStampIds = setFavorites;
		SaveFavoriteStamp();
	}

	private void SaveFavoriteStamp()
	{
		PlayerPrefs.SetString("FAVORITE_STAMP_KEY", favoriteStampIds.ToJoinString());
		PlayerPrefs.Save();
	}

	private void LoadFavoriteStamp()
	{
		string @string = PlayerPrefs.GetString("FAVORITE_STAMP_KEY", "");
		if (string.IsNullOrEmpty(@string))
		{
			favoriteStampIds = GameDefine.FAVORITE_STAMP_DEFAULT;
			return;
		}
		favoriteStampIds = GameDefine.FAVORITE_STAMP_DEFAULT;
		string[] array = @string.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			if (int.TryParse(array[i], out int result))
			{
				favoriteStampIds[i] = result;
			}
		}
	}

	public void SetUserInfoAndUserStatus(UserInfo user_info, UserStatus user_status, UserClanData user_clan, List<int> unlock_stamps, List<int> selected_Degrees, List<int> unlocked_Degrees)
	{
		SetUserInfo(user_info);
		SetUserStatus(user_status);
		SetUserClan(user_clan);
		unlockStampIds = unlock_stamps;
		selectedDegreeIds = selected_Degrees;
		unlockedDegreeIds = unlocked_Degrees;
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.OnUpdateUnlockStampList();
		}
	}

	public bool IsUnlockedStamp(int stampId)
	{
		if (unlockStampIds != null)
		{
			return unlockStampIds.Contains(stampId);
		}
		return false;
	}

	public bool IsUnlockedDegree(int degreeId)
	{
		if (unlockedDegreeIds != null)
		{
			return unlockedDegreeIds.Contains(degreeId);
		}
		return false;
	}

	public void UpdateUnlockDegrees(BaseModelDiff.DiffUnlockDegree diff)
	{
		unlockedDegreeIds = diff.update;
	}

	public void UpdateSelectedDegrees(BaseModelDiff.DiffSelectedDegree diff)
	{
		if (diff.add != null)
		{
			foreach (SelectDegree item in diff.add)
			{
				selectedDegreeIds[item.positionNo] = item.degreeId;
			}
		}
		if (diff.update != null)
		{
			foreach (SelectDegree item2 in diff.update)
			{
				selectedDegreeIds[item2.positionNo] = item2.degreeId;
			}
		}
	}

	public void UpdateUnlockStamps(BaseModelDiff.DiffStamp diff)
	{
		unlockStampIds = diff.update;
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.OnUpdateUnlockStampList();
		}
	}

	public void SetRecvUserInfo(UserInfo user_info, int tutorialStep = 0)
	{
		SetUserInfo(user_info);
		if (tutorialStep > userStatus.tutorialStep)
		{
			userStatus.tutorialStep = tutorialStep;
		}
	}

	public void SetNewsID(int news_id)
	{
		int @int = PlayerPrefs.GetInt("LastNewsID", -1);
		newsID = news_id;
		string @string = PlayerPrefs.GetString("fcm_registed", "");
		if (news_id != @int || string.IsNullOrEmpty(@string) || !NetworkNative.getNativeVersionNameRemoveDot().Equals(@string))
		{
			NetworkNative.createRegistrationId();
		}
		if (!needOpenNewsPage && news_id != @int && news_id != -1)
		{
			needOpenNewsPage = true;
		}
	}

	public void SetNextDonationTime(string time_str)
	{
		userStatus.nextDonationTime = DateTime.Parse(time_str);
	}

	public void OnOpenNewsPage()
	{
		PlayerPrefs.SetInt("LastNewsID", newsID);
		needOpenNewsPage = false;
	}

	public bool IsOverEquipItem()
	{
		return GetOverEquipItemNum() > 0;
	}

	public int GetOverEquipItemNum()
	{
		return equipItemNum - userStatus.maxEquipItem;
	}

	public bool IsOverSkillItem()
	{
		return GetOverSkillItemNum() > 0;
	}

	public int GetOverSkillItemNum()
	{
		return skillItemNum - userStatus.maxSkillItem;
	}

	public bool IsStorageOverflow()
	{
		if (!IsOverEquipItem())
		{
			return IsOverSkillItem();
		}
		return true;
	}

	public bool IsEquipVisualItem(ulong uniq_id)
	{
		if (uniq_id == 0L)
		{
			return false;
		}
		UserStatus userStatus = this.userStatus;
		if (ulong.Parse(userStatus.armorUniqId) != uniq_id && ulong.Parse(userStatus.helmUniqId) != uniq_id && ulong.Parse(userStatus.armUniqId) != uniq_id)
		{
			return ulong.Parse(userStatus.legUniqId) == uniq_id;
		}
		return true;
	}

	public int GetFaceModelID()
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetFaceModelID(userStatus.sex, userStatus.faceId);
	}

	public int GetHairModelID()
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairModelID(userStatus.sex, userStatus.hairId);
	}

	public Color GetSkinColor()
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(userStatus.skinId);
	}

	public Color GetHairColor()
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(userStatus.hairColorId);
	}

	public int GetEnemyLevelFromUserLevel()
	{
		return GetEnemyLevelFromUserLevel(userStatus.level);
	}

	private int GetEnemyLevelFromUserLevel(int userLevel)
	{
		int qUEST_ITEM_LEVEL_MAX = userInfo.constDefine.QUEST_ITEM_LEVEL_MAX;
		return (Mathf.Clamp(userLevel, 10, qUEST_ITEM_LEVEL_MAX) + 9) / 10 * 10;
	}

	public void SendHomeInfo(Action<bool, bool, int> callBack)
	{
		HomeInfoModel.SendForm sendForm = new HomeInfoModel.SendForm();
		sendForm.appStr = NetworkNative.getAppStr();
		Protocol.SendAsync("ajax/home/info", sendForm, delegate(HomeInfoModel ret)
		{
			bool arg = false;
			bool arg2 = false;
			int arg3 = 0;
			if (ret.Error == Error.None)
			{
				arg = true;
				isAcquiredUserInfo = true;
				arg2 = ret.result.loginBonus;
				oncePurchaseGachaProductId = ret.result.productId;
				needShowOneTimesOfferSS = ret.result.isOneTimesOfferActive;
				if (MonoBehaviourSingleton<TradingPostManager>.IsValid())
				{
					MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostInfo(ret.result);
				}
				partyInviteHome = (showJoinClanInGame ? PartyManager.IsValidNotEmptyList() : ret.result.party);
				if (!partyInviteHome)
				{
					ClearPartyInvite();
				}
				m_alertMessages.AddRange(ret.result.alertMessages);
				SetEventBannerList(ret.result.banner);
				SetGachaDecoList(ret.result.gachaDeco);
				SetNewsID(ret.result.newsId);
				SetNextDonationTime(ret.result.nextDonationTime);
				advisory = ret.result.advisory;
				isShowAppReviewAppeal = (ret.result.isDisplayReview >= 1);
				isArenaOpen = ret.result.isArenaOpen;
				isJoinedArenaRanking = ret.result.isJoinedArenaRanking;
				isGuildRequestOpen = ret.result.isGuildRequestOpen;
				isTheaterRenewal = ret.result.isTheaterRenewal;
				SetClanScoutNum(ret.result.clanInviteNum);
				clanDisplayType = (DISPLAY_TYPE)ret.result.clanDisplayType;
				SetClanRequestNum(ret.result.clanRequestNum);
				if (MonoBehaviourSingleton<ChatManager>.IsValid())
				{
					MonoBehaviourSingleton<ChatManager>.I.OnNotifyUpdateChannnelInfo(ret.result.chat);
				}
				if (MonoBehaviourSingleton<QuestManager>.IsValid())
				{
					MonoBehaviourSingleton<QuestManager>.I.SetEventList(ret.result.events);
					MonoBehaviourSingleton<QuestManager>.I.SetFutureEventList(ret.result.futureEventIds);
					MonoBehaviourSingleton<QuestManager>.I.SetBingoEventList(ret.result.bingoEvents);
				}
				if (MonoBehaviourSingleton<FriendManager>.IsValid())
				{
					MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(ret.result.message);
				}
				GameSceneGlobalSettings.GetCurrentIHomeManager()?.SetPointShop(ret.result.isPointShopOpen, ret.result.pointShopBanner);
				if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid())
				{
					MonoBehaviourSingleton<LoungeMatchingManager>.I.SetOpenLounge(ret.result.isLoungeOpen);
				}
				arg3 = ret.result.task;
				if (MonoBehaviourSingleton<DeliveryManager>.IsValid())
				{
					MonoBehaviourSingleton<DeliveryManager>.I.UpdateDeliveryReaminTime(ret.result.dailyRemainTime, ret.result.weeklyRemainTime);
				}
				isShadowChallengeFirst = (ret.result.isShadowChallengeFirst != 0);
				if (ret.result.isShadowChallengeFirst != 0)
				{
					GameSaveData.instance.recommendedChallengeCheck = 1;
					GameSaveData.Save();
				}
				crystalChangeName = ret.result.crystalChangeName;
				if (MonoBehaviourSingleton<StatusManager>.IsValid())
				{
					MonoBehaviourSingleton<StatusManager>.I.SetTimeSlotEvents(ret.result.timeSlotEvents);
				}
				if (!string.IsNullOrEmpty(ret.currentTime) && MonoBehaviourSingleton<GoGameTimeManager>.IsValid())
				{
					GoGameTimeManager.SetServerTime(ret.currentTime);
				}
				if (!string.IsNullOrEmpty(ret.result.blackShopEndDate))
				{
					int num = (int)GoGameTimeManager.GetRemainTime(ret.result.blackShopEndDate).TotalSeconds;
					if (num > 0)
					{
						if (!GameSaveData.instance.resetMarketTime.Equals(ret.result.blackShopEndDate))
						{
							GameSaveData.instance.canShowNoteDarkMarket = true;
							GameSaveData.instance.resetMarketTime = ret.result.blackShopEndDate;
						}
						MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.InitTime(num);
					}
				}
				else
				{
					GameSaveData.instance.resetMarketTime = string.Empty;
				}
				isWheelOfFortuneOn = ret.result.isWheelOfFortuneOn;
				GameSaveData.instance.canShowWheelFortune = isWheelOfFortuneOn;
				SetHomeBannerList(ret.result.homeBanner);
			}
			callBack(arg, arg2, arg3);
		});
	}

	public void SendChangeName(string name, Action<bool> call_back)
	{
		OptionChangeNameModel.RequestSendForm requestSendForm = new OptionChangeNameModel.RequestSendForm();
		requestSendForm.name = name;
		Protocol.Send(OptionChangeNameModel.URL, requestSendForm, delegate(OptionChangeNameModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendEditFigure(int sex, int faceId, int haireId, int haireColorId, int skinId, int voiceId, string name, Action<bool> call_back)
	{
		OptionEditFigureModel.RequestSendForm requestSendForm = new OptionEditFigureModel.RequestSendForm();
		requestSendForm.sex = sex;
		requestSendForm.face = faceId;
		requestSendForm.hair = haireId;
		requestSendForm.color = haireColorId;
		requestSendForm.skin = skinId;
		requestSendForm.voice = voiceId;
		requestSendForm.name = name;
		Protocol.Send(OptionEditFigureModel.URL, requestSendForm, delegate(OptionEditFigureModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendEditComment(string comment, Action<bool> call_back)
	{
		OptionEditCommentModel.RequestSendForm requestSendForm = new OptionEditCommentModel.RequestSendForm();
		requestSendForm.comment = comment;
		Protocol.Send(OptionEditCommentModel.URL, requestSendForm, delegate(OptionEditCommentModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendBirthday(int year, int month, int day, Action<bool> call_back)
	{
		OptionBirthdayModel.RequestSendForm requestSendForm = new OptionBirthdayModel.RequestSendForm();
		requestSendForm.year = year;
		requestSendForm.month = month;
		requestSendForm.day = day;
		Protocol.Send(OptionBirthdayModel.URL, requestSendForm, delegate(OptionBirthdayModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendStopper(bool is_enable_stopper, Action<bool> call_back)
	{
		OptionStopperModel.RequestSendForm requestSendForm = new OptionStopperModel.RequestSendForm();
		requestSendForm.on = (is_enable_stopper ? 1 : 0);
		Protocol.Send(OptionStopperModel.URL, requestSendForm, delegate(OptionStopperModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendParentalPassword(string password, string confirm_password, Action<Error> call_back)
	{
		OptionSetParentPassModel.RequestSendForm requestSendForm = new OptionSetParentPassModel.RequestSendForm();
		requestSendForm.password = password;
		requestSendForm.confirmPassword = confirm_password;
		Protocol.Send(OptionSetParentPassModel.URL, requestSendForm, delegate(OptionSetParentPassModel ret)
		{
			call_back(ret.Error);
		});
	}

	public void SendResetParentalPassword(string password, Action<Error> call_back)
	{
		OptionResetParentPassModel.RequestSendForm requestSendForm = new OptionResetParentPassModel.RequestSendForm();
		requestSendForm.password = password;
		Protocol.Send(OptionResetParentPassModel.URL, requestSendForm, delegate(OptionResetParentPassModel ret)
		{
			call_back(ret.Error);
		});
	}

	public void SendInviteInput(string code, Action<bool> call_back)
	{
		InviteInputModel.RequestSendForm requestSendForm = new InviteInputModel.RequestSendForm();
		requestSendForm.code = code;
		Protocol.Send(InviteInputModel.URL, requestSendForm, delegate(InviteInputModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendPushNotificationDeviceEnable(int enable, Action<bool> call_back)
	{
		PushNotificationDeviceEnableModel.RequestSendForm requestSendForm = new PushNotificationDeviceEnableModel.RequestSendForm();
		requestSendForm.enable = enable;
		Protocol.Send(PushNotificationDeviceEnableModel.URL, requestSendForm, delegate(PushNotificationDeviceEnableModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendAppReviewInfo(int starValue, int buttonNum, Action<bool> call_back)
	{
		AppReviewInfoModel.RequestSendForm requestSendForm = new AppReviewInfoModel.RequestSendForm();
		requestSendForm.starValue = starValue;
		requestSendForm.replyAction = buttonNum;
		Protocol.Send(AppReviewInfoModel.URL, requestSendForm, delegate(AppReviewInfoModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendOpinionMessage(string msg, Action<bool> call_back)
	{
		OpinionPostModel.RequestSendForm requestSendForm = new OpinionPostModel.RequestSendForm();
		requestSendForm.msg = msg;
		Protocol.Send(OpinionPostModel.URL, requestSendForm, delegate(OpinionPostModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendTutorialStep(Action<bool> call_back)
	{
		if (userStatus.tutorialStep >= 9)
		{
			call_back(obj: true);
			return;
		}
		UserStatusTutorialModel.RequestSendForm requestSendForm = new UserStatusTutorialModel.RequestSendForm();
		requestSendForm.bit = 0;
		Protocol.Send(UserStatusTutorialModel.URL, requestSendForm, delegate(UserStatusTutorialModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendTutorialBit(TUTORIAL_MENU_BIT bit, Action<bool> call_back = null)
	{
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			if (call_back != null)
			{
				call_back(obj: false);
			}
		}
		else if (CheckTutorialBit(bit))
		{
			if (call_back != null)
			{
				call_back(obj: true);
			}
		}
		else
		{
			UserStatusTutorialModel.RequestSendForm requestSendForm = new UserStatusTutorialModel.RequestSendForm();
			requestSendForm.bit = (int)bit;
			Protocol.Send(UserStatusTutorialModel.URL, requestSendForm, delegate(UserStatusTutorialModel ret)
			{
				bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
				if (call_back != null)
				{
					call_back(obj);
				}
			});
		}
	}

	public void SendDebugNextTutorial()
	{
		Protocol.Send(DebugNextTutorialModel.URL, delegate(DebugNextTutorialModel ret)
		{
			if (ret.Error == Error.None)
			{
				userStatus.tutorialStep = ret.tutorial;
			}
		});
	}

	public void SendDebugResearchLv(int lv, Action<bool> call_back)
	{
		DebugSetResearchLvModel.RequestSendForm requestSendForm = new DebugSetResearchLvModel.RequestSendForm();
		requestSendForm.lv = lv;
		Protocol.Send(DebugSetResearchLvModel.URL, requestSendForm, delegate(DebugSetResearchLvModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendDebugSetEditNameTime(string date, Action<bool> call_back)
	{
		DebugSetEditNameTimeModel.RequestSendForm requestSendForm = new DebugSetEditNameTimeModel.RequestSendForm();
		requestSendForm.date = date;
		Protocol.Send(DebugSetEditNameTimeModel.URL, requestSendForm, delegate(DebugSetEditNameTimeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		});
	}

	public void SendDebugSetGrade(int fieldGrade, int questGrade, Action<bool> call_back)
	{
		DebugSetGradeModel.RequestSendForm requestSendForm = new DebugSetGradeModel.RequestSendForm();
		requestSendForm.fg = fieldGrade;
		requestSendForm.qg = questGrade;
		Protocol.Send(DebugSetGradeModel.URL, requestSendForm, delegate(DebugSetGradeModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendDebutResetGrade(Action<bool> call_back)
	{
		Protocol.Send(DebugResetGradeModel.URL, delegate(DebugResetGradeModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendDebugSetLv(int lv, Action<bool> call_back)
	{
		DebugSetLvModel.RequestSendForm requestSendForm = new DebugSetLvModel.RequestSendForm();
		requestSendForm.lv = lv;
		Protocol.Send(DebugSetLvModel.URL, requestSendForm, delegate(DebugSetLvModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendDebugSetTutorial(int step, string bit, Action<bool> call_back)
	{
		DebugSetTutorialModel.RequestSendForm requestSendForm = new DebugSetTutorialModel.RequestSendForm();
		requestSendForm.step = step;
		requestSendForm.bit = bit;
		Protocol.Send(DebugSetTutorialModel.URL, requestSendForm, delegate(DebugSetTutorialModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public void SendAlive(Action<bool> call_back = null)
	{
		MonoBehaviourSingleton<NetworkManager>.I.Request(StatusAliveModel.URL, delegate(StatusAliveModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			if (call_back != null)
			{
				call_back(obj);
			}
		});
	}

	public void SendGatherItemRecord(int userId, int eventId, Action<bool, GatherItemUserRecordModel> call_back = null)
	{
		GatherItemUserRecordModel.RequestSendForm requestSendForm = new GatherItemUserRecordModel.RequestSendForm();
		requestSendForm.userId = userId;
		requestSendForm.eventId = eventId;
		Protocol.Send(GatherItemUserRecordModel.URL, requestSendForm, delegate(GatherItemUserRecordModel ret)
		{
			bool arg = ErrorCodeChecker.IsSuccess(ret.Error);
			if (call_back != null)
			{
				call_back(arg, ret);
			}
		});
	}

	public void OnDiff(BaseModelDiff.DiffUser diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.name))
		{
			userInfo.name = diff.name[0].name;
			userInfo.editNameAt = diff.name[0].editNameAt;
			flag = true;
		}
		if (Utility.IsExist(diff.comment))
		{
			userInfo.comment = diff.comment[0];
			flag = true;
		}
		if (Utility.IsExist(diff.birthday))
		{
			userInfo.birthday = diff.birthday[0].birthday;
			userInfo.communityFlag = diff.birthday[0].communityFlag;
			flag = true;
		}
		if (Utility.IsExist(diff.option))
		{
			userInfo.isParentPassSet = diff.option[0].isParentPassSet;
			userInfo.isStopperSet = diff.option[0].isStopperSet;
			flag = true;
		}
		if (Utility.IsExist(diff.inputInviteFlag))
		{
			userInfo.inputInviteFlag = diff.inputInviteFlag[0];
			flag = true;
		}
		if (Utility.IsExist(diff.pushEnable))
		{
			userInfo.pushEnable = diff.pushEnable[0];
			flag = true;
		}
		if (flag)
		{
			DirtyUserInfo();
		}
	}

	public void OnDiff(BaseModelDiff.DiffStatus diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.views))
		{
			BaseModelDiff.DiffStatus.Views views = diff.views[0];
			userStatus.sex = views.sex;
			userStatus.faceId = views.faceId;
			userStatus.hairId = views.hairId;
			userStatus.hairColorId = views.hairColorId;
			userStatus.skinId = views.skinId;
			userStatus.voiceId = views.voiceId;
			flag = true;
		}
		if (Utility.IsExist(diff.grow))
		{
			BaseModelDiff.DiffStatus.Grow grow = diff.grow[0];
			if ((int)userStatus.level < grow.level)
			{
				if ((int)userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel && grow.level >= MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
				{
					GameSaveData.instance.showUnlockQuestEvent = true;
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("value", grow.level);
				MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("reach_level", "Gameplay", dictionary);
				if ((int)userStatus.level < 50 && grow.level >= 50)
				{
					Debug.Log("track event lv user lv:" + userStatus.level + "grow lv:" + grow.level);
					MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("reach_level_50", "Gameplay", dictionary);
				}
			}
			userStatus.level = grow.level;
			userStatus.exp = grow.exp;
			userStatus.expPrev = grow.expPrev;
			userStatus.expNext = grow.expNext;
			userStatus.hp = grow.hp;
			userStatus.atk = grow.atk;
			userStatus.def = grow.def;
			userStatus.maxFollow = grow.maxFollow;
			flag = true;
		}
		if (Utility.IsExist(diff.money))
		{
			userStatus.money = diff.money[0];
			flag = true;
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene" && MonoBehaviourSingleton<SmithManager>.IsValid())
			{
				MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(is_force: true);
			}
		}
		if (Utility.IsExist(diff.crystal))
		{
			if (userStatus.crystal > diff.crystal[0])
			{
				int num = userStatus.crystal - diff.crystal[0];
				GameSaveData.instance.spent25Gems += num;
			}
			userStatus.crystal = diff.crystal[0];
			flag = true;
		}
		if (Utility.IsExist(diff.eSetNo))
		{
			userStatus.eSetNo = diff.eSetNo[0];
			flag = true;
		}
		if (Utility.IsExist(diff.ueSetNo))
		{
			userStatus.ueSetNo = diff.ueSetNo[0];
			flag = true;
		}
		if (Utility.IsExist(diff.titleId))
		{
			userStatus.titleId = diff.titleId[0];
			flag = true;
		}
		if (Utility.IsExist(diff.capacity))
		{
			BaseModelDiff.DiffStatus.Capacity capacity = diff.capacity[0];
			userStatus.maxEquipItem = capacity.maxEquipItem;
			userStatus.maxSkillItem = capacity.maxSkillItem;
			flag = true;
		}
		if (Utility.IsExist(diff.tutorialStep))
		{
			userStatus.tutorialStep = diff.tutorialStep[0];
			flag = true;
			if (userStatus.tutorialStep != 4)
			{
				if (userStatus.tutorialStep == 7)
				{
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_weapon_crafting, "Tutorial");
				}
				else
				{
					_ = userStatus.tutorialStep;
					_ = 8;
				}
			}
		}
		if (Utility.IsExist(diff.tutorialBit))
		{
			long oldTutorialBit = 0L;
			if (userStatus.tutorialBit != null)
			{
				oldTutorialBit = userStatus.TutorialBit;
			}
			userStatus.tutorialBit = diff.tutorialBit[0];
			flag = true;
			if (CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA_QUEST_START, oldTutorialBit))
			{
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_9_behemoth_fight_begin_1, "Tutorial");
				Debug.LogWarning("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_9_behemoth_fight_begin_1.ToString());
				MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_9_behemoth_fight_begin_1, "Tutorial");
			}
			else if (CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.SHADOW_QUEST_START, oldTutorialBit))
			{
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_13_behemoth_fight_begin_2, "Tutorial");
				Debug.LogWarning("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_13_behemoth_fight_begin_2.ToString());
				MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_13_behemoth_fight_begin_2, "Tutorial");
			}
			else if (!CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA1, oldTutorialBit) && !CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA_QUEST_BATTLE_RESULT, oldTutorialBit) && !CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA2, oldTutorialBit) && !CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.SKILL_EQUIP, oldTutorialBit) && !CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.CLAIM_REWARD, oldTutorialBit))
			{
				if (CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.FORGE_ITEM, oldTutorialBit))
				{
					GameSaveData.instance.SetPushTrackEquipTutorial(canPush: true);
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_weapon_crafting, "Tutorial");
				}
				else if (CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.AFTER_GACHA2, oldTutorialBit))
				{
					MonoBehaviourSingleton<NativeGameService>.I.SignInFirstTime();
				}
			}
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
			{
				MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetErrorResendQuestGachaFlag();
			}
		}
		if (Utility.IsExist(diff.tutorialQuestId))
		{
			userStatus.tutorialQuestId = diff.tutorialQuestId[0];
			flag = true;
		}
		if (Utility.IsExist(diff.researchLv))
		{
			userStatus.researchLv = diff.researchLv[0];
			flag = true;
		}
		if (Utility.IsExist(diff.questGrade))
		{
			userStatus.questGrade = diff.questGrade[0];
			flag = true;
		}
		if (Utility.IsExist(diff.fieldGrade))
		{
			userStatus.fieldGrade = diff.fieldGrade[0];
			flag = true;
		}
		if (Utility.IsExist(diff.showEquip))
		{
			BaseModelDiff.DiffStatus.ShowEquip showEquip = diff.showEquip[0];
			userStatus.armorUniqId = showEquip.armorUniqId;
			userStatus.helmUniqId = showEquip.helmUniqId;
			userStatus.armUniqId = showEquip.armUniqId;
			userStatus.legUniqId = showEquip.legUniqId;
			userStatus.showHelm = showEquip.showHelm;
			flag = true;
		}
		if (Utility.IsExist(diff.fairyNum))
		{
			userStatus.fairyNum = diff.fairyNum[0];
			flag = true;
		}
		if (Utility.IsExist(diff.maxEquipItemTargetNum))
		{
			userStatus.maxEquipItemTargetNum = diff.maxEquipItemTargetNum[0];
			flag = true;
		}
		if (flag)
		{
			DirtyUserStatus();
		}
	}

	public void OnDiff(BaseModelDiff.DiffUserClan diff)
	{
		if (Utility.IsExist(diff.update))
		{
			userClan = diff.update[0];
		}
	}

	public void OnDiff(BaseModelDiff.DiffNotice diff)
	{
		if (Utility.IsExist(diff.login))
		{
			FieldManager.IsValidInGame();
		}
	}

	public void OnDiff(BaseModelDiff.DiffServerConstDefine diff)
	{
		if (Utility.IsExist(diff.update) && userInfo != null)
		{
			userInfo.constDefine = diff.update[0];
		}
	}

	public void SetHomeBannerList(List<Network.HomeBanner> list)
	{
		if (list == null)
		{
			ResetHomeBannerList();
		}
		else
		{
			homeBannerList = list;
		}
	}

	public void ResetHomeBannerList()
	{
		if (homeBannerList != null)
		{
			homeBannerList.Clear();
		}
	}
}
