using Network;
using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
	public int dataVersion;

	public int testCount;

	public float volumeBGM = 1f;

	public float volumeSE = 1f;

	public float touchInGameFlick = 0.5f;

	public float touchInGameLong = 0.5f;

	public string graphicOptionKey = "";

	public int voiceOption;

	public int languageOption;

	public bool headName = true;

	public bool canShowWheelFortune;

	public string resetMarketTime = string.Empty;

	public bool canShowNoteDarkMarket;

	public bool ratingPopupHaveShow;

	public bool happyTimeForRating;

	public bool isShowChatOfferBanner;

	public int showHomeBannerOfferDay;

	public int showHomeBannerInviteDay;

	public int spent25Gems;

	public int spentSummonTicket;

	public int showHomeOneTimesOfferSSDay;

	public bool defaultRepeatPartyOn = true;

	public bool enableLandscape = Native.GetDeviceAutoRotateSetting();

	public bool enableMinimapEnemy;

	public string arrowCameraKey = "";

	public int lastQusetID;

	public int lastNewClearQusetID;

	public int lvupMessageFlag;

	public int recommendedDeliveryCheck = 1;

	public int recommendedOrderCheck;

	public int recommendedChallengeCheck;

	public int recommendedDailyDeliveryCheck;

	public int recommendedWeeklyDeliveryCheck;

	public int recommendedDailyDeliveryCheckAtHome;

	public int dayShowNewsNotification;

	public ServerListTable.ServerData currentServer;

	public bool isFinishTradingPostTutorial;

	public List<LoginBonus> logInBonus;

	public string showIAPAdsPop;

	public string iAPBundleBought;

	public bool useVirtualPad;

	public bool showUnlockQuestEvent;

	public bool canPushTrackEquipTutorial;

	public string showHomeBanners;

	public long pushTrackTutorialBit;

	public bool isAutoMode;

	public List<int> checkedSmithCreateRecipe = new List<int>();

	public List<string> sortSaveData = new List<string>();

	public List<ulong> notifyQuestIDs = new List<ulong>();

	public int lastRemainDayThreshold;

	public List<string> newItems = new List<string>();

	public List<string> newSkillItems = new List<string>();

	public List<string> newEquipItems = new List<string>();

	public List<string> newQuestItems = new List<string>();

	public List<string> abilityItems = new List<string>();

	public List<string> accessoryItems = new List<string>();

	public List<string> newClanScoutList = new List<string>();

	public List<uint> newReleasePortals = new List<uint>();

	public List<int> showedOpenRegionIds = new List<int>();

	public int tutorialCompleteSeriesArena;

	private const int MAX_LOG = 50;

	public Dictionary<int, List<GuildMessage.ChatPostRequest>> chatLogs = new Dictionary<int, List<GuildMessage.ChatPostRequest>>();

	public int MutualFollowerInviteListSortType;

	public int MutualFollowerListSortType;

	public int FollowListSortType;

	public int FollowerListSortType;

	public int ScreenShotUIFilterType = -1;

	public static GameSaveData instance
	{
		get;
		private set;
	}

	public bool IsRecommendedDeliveryCheck()
	{
		return recommendedDeliveryCheck == 1;
	}

	public bool IsRecommendedOrderCheck()
	{
		return recommendedOrderCheck == 1;
	}

	public bool IsRecommendedChallengeCheck()
	{
		return recommendedChallengeCheck == 1;
	}

	public bool IsRecommendedDailyDeliveryCheck()
	{
		return recommendedDailyDeliveryCheck == 1;
	}

	public bool IsRecommendedWeeklyDeliveryCheck()
	{
		return recommendedWeeklyDeliveryCheck == 1;
	}

	public bool IsRecommendedDailyDeliveryCheckAtHome()
	{
		return recommendedDailyDeliveryCheckAtHome == 1;
	}

	public bool IsShowNewsNotification()
	{
		int day = DateTime.UtcNow.AddSeconds(-10800.0).Day;
		if (dayShowNewsNotification != day)
		{
			return true;
		}
		return false;
	}

	public void SetCurrentServer(ServerListTable.ServerData server)
	{
		currentServer = server;
		NetworkNative.setHost(currentServer.url);
		Save();
	}

	public void SetPushTrackEquipTutorial(bool canPush)
	{
		canPushTrackEquipTutorial = canPush;
		Save();
	}

	public void SetPushedTrackTutorialBit(TRACK_TUTORIAL_STEP_BIT bit)
	{
		pushTrackTutorialBit |= 1L << (int)bit;
		Save();
	}

	public bool IsPushedTrackTutorialBit(TRACK_TUTORIAL_STEP_BIT bit)
	{
		if (bit == TRACK_TUTORIAL_STEP_BIT.MAX)
		{
			return true;
		}
		return (pushTrackTutorialBit & (1L << (int)bit)) != 0;
	}

	public bool IsCheckedSmithCreateRecipe(int id)
	{
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)id);
		if (equipItemData == null || equipItemData.obtained.flag < 0)
		{
			return true;
		}
		return IsCheckSmithCreateRecipeBits(equipItemData.obtained.GetSequenceNumber());
	}

	public void AddCheckedSmithCreateRecipe(int[] ids)
	{
		if (ids == null || ids.Length == 0)
		{
			return;
		}
		int i = 0;
		for (int num = ids.Length; i < num; i++)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)ids[i]);
			if (equipItemData == null || equipItemData.obtained.flag < 0)
			{
				continue;
			}
			int sequenceNumber = equipItemData.obtained.GetSequenceNumber();
			if (IsCheckSmithCreateRecipeBits(sequenceNumber))
			{
				continue;
			}
			int num2 = sequenceNumber / 32;
			int num3 = 1 << sequenceNumber % 32;
			int count = checkedSmithCreateRecipe.Count;
			if (count <= num2)
			{
				int num4 = num2 - count;
				int j = 0;
				for (int num5 = num4; j < num5; j++)
				{
					checkedSmithCreateRecipe.Add(0);
				}
				checkedSmithCreateRecipe.Add(num3);
			}
			else
			{
				checkedSmithCreateRecipe[num2] |= num3;
			}
		}
	}

	private bool IsCheckSmithCreateRecipeBits(int number)
	{
		int num = number / 32;
		if (checkedSmithCreateRecipe.Count <= num)
		{
			return false;
		}
		int num2 = checkedSmithCreateRecipe[num];
		int num3 = 1 << number % 32;
		return (num2 & num3) != 0;
	}

	public string GetSortBit(SortSettings.SETTINGS_TYPE settings_type)
	{
		if (sortSaveData == null || sortSaveData.Count == 0)
		{
			return string.Empty;
		}
		int num = sortSaveData.FindIndex((string _data) => settings_type == SortSettings.GetSettingsTypeBySortBit(_data));
		if (num == -1)
		{
			return string.Empty;
		}
		return sortSaveData[num];
	}

	public void SetSortBit(SortSettings settings)
	{
		int num = sortSaveData.FindIndex((string _data) => settings.settingsType == SortSettings.GetSettingsTypeBySortBit(_data));
		if (num != -1)
		{
			sortSaveData.RemoveAt(num);
		}
		sortSaveData.Add(SortSettings.GetSortBit(settings));
	}

	public void DeleteSortBit(SortSettings.SETTINGS_TYPE settings_type)
	{
		int num = sortSaveData.FindIndex((string _data) => settings_type == SortSettings.GetSettingsTypeBySortBit(_data));
		if (num != -1)
		{
			sortSaveData.RemoveAt(num);
		}
	}

	public void DeleteAllSortBit()
	{
		sortSaveData.Clear();
	}

	public void updateLastNotifyQuestRemainTime(int remainDayThreshold, List<ulong> ids)
	{
		lastRemainDayThreshold = remainDayThreshold;
		notifyQuestIDs = ids;
		Save();
	}

	public bool isIncludeNotifyQuestID(List<ulong> ids)
	{
		foreach (ulong id in ids)
		{
			if (!notifyQuestIDs.Contains(id))
			{
				return false;
			}
		}
		return true;
	}

	public bool AddNewItem(ITEM_ICON_TYPE type, string str_uniq_id)
	{
		switch (type)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.USE_ITEM:
			if (!newItems.Contains(str_uniq_id))
			{
				newItems.Add(str_uniq_id);
				return true;
			}
			break;
		case ITEM_ICON_TYPE.SKILL_ATTACK:
		case ITEM_ICON_TYPE.SKILL_SUPPORT:
		case ITEM_ICON_TYPE.SKILL_HEAL:
		case ITEM_ICON_TYPE.SKILL_PASSIVE:
		case ITEM_ICON_TYPE.SKILL_GROW:
			if (!newSkillItems.Contains(str_uniq_id))
			{
				newSkillItems.Add(str_uniq_id);
				return true;
			}
			break;
		default:
			if (!newEquipItems.Contains(str_uniq_id))
			{
				newEquipItems.Add(str_uniq_id);
				return true;
			}
			break;
		case ITEM_ICON_TYPE.QUEST_ITEM:
			if (!newQuestItems.Contains(str_uniq_id))
			{
				newQuestItems.Add(str_uniq_id);
				return true;
			}
			break;
		case ITEM_ICON_TYPE.ABILITY_ITEM:
			if (!abilityItems.Contains(str_uniq_id))
			{
				abilityItems.Add(str_uniq_id);
				return true;
			}
			break;
		case ITEM_ICON_TYPE.ACCESSORY:
			if (!accessoryItems.Contains(str_uniq_id))
			{
				accessoryItems.Add(str_uniq_id);
				return true;
			}
			break;
		}
		return false;
	}

	public bool RemoveNewIcon(ITEM_ICON_TYPE type, ulong uniq_id)
	{
		return RemoveNewIcon(type, uniq_id.ToString());
	}

	public bool RemoveNewIcon(ITEM_ICON_TYPE type, string str_uniq_id)
	{
		switch (type)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.USE_ITEM:
			if (!newItems.Contains(str_uniq_id))
			{
				return false;
			}
			newItems.Remove(str_uniq_id);
			break;
		case ITEM_ICON_TYPE.SKILL_ATTACK:
		case ITEM_ICON_TYPE.SKILL_SUPPORT:
		case ITEM_ICON_TYPE.SKILL_HEAL:
		case ITEM_ICON_TYPE.SKILL_PASSIVE:
		case ITEM_ICON_TYPE.SKILL_GROW:
			if (!newSkillItems.Contains(str_uniq_id))
			{
				return false;
			}
			newSkillItems.Remove(str_uniq_id);
			break;
		default:
			if (!newEquipItems.Contains(str_uniq_id))
			{
				return false;
			}
			newEquipItems.Remove(str_uniq_id);
			break;
		case ITEM_ICON_TYPE.QUEST_ITEM:
			if (!newQuestItems.Contains(str_uniq_id))
			{
				return false;
			}
			newQuestItems.Remove(str_uniq_id);
			break;
		case ITEM_ICON_TYPE.ABILITY_ITEM:
			if (!abilityItems.Contains(str_uniq_id))
			{
				return false;
			}
			abilityItems.Remove(str_uniq_id);
			break;
		case ITEM_ICON_TYPE.ACCESSORY:
			if (!accessoryItems.Contains(str_uniq_id))
			{
				return false;
			}
			accessoryItems.Remove(str_uniq_id);
			break;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.REMOVE_NEW_ICON);
		return true;
	}

	public void RemoveNewIconAndSave(ITEM_ICON_TYPE type, ulong uniq_id)
	{
		if (RemoveNewIcon(type, uniq_id))
		{
			Save();
		}
	}

	public bool IsNewItem(ITEM_ICON_TYPE type, ulong uniq_id)
	{
		return IsNewItem(type, uniq_id.ToString());
	}

	public bool IsNewItem(ITEM_ICON_TYPE type, string str_uniq_id)
	{
		switch (type)
		{
		case ITEM_ICON_TYPE.NONE:
			return false;
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.USE_ITEM:
			return newItems.Contains(str_uniq_id);
		case ITEM_ICON_TYPE.SKILL_ATTACK:
		case ITEM_ICON_TYPE.SKILL_SUPPORT:
		case ITEM_ICON_TYPE.SKILL_HEAL:
		case ITEM_ICON_TYPE.SKILL_PASSIVE:
		case ITEM_ICON_TYPE.SKILL_GROW:
			return newSkillItems.Contains(str_uniq_id);
		default:
			return newEquipItems.Contains(str_uniq_id);
		case ITEM_ICON_TYPE.QUEST_ITEM:
			return newQuestItems.Contains(str_uniq_id);
		case ITEM_ICON_TYPE.ABILITY_ITEM:
			return abilityItems.Contains(str_uniq_id);
		case ITEM_ICON_TYPE.ACCESSORY:
			return accessoryItems.Contains(str_uniq_id);
		}
	}

	public bool isNewClanScout(string cId, int expiredAt)
	{
		string item = cId + ":" + expiredAt;
		bool num = !newClanScoutList.Contains(item);
		if (num)
		{
			newClanScoutList.Add(item);
			Save();
		}
		return num;
	}

	public bool isNewReleasePortal(uint id)
	{
		return newReleasePortals.Contains(id);
	}

	public void AddShowedOpenRegionId(int id)
	{
		if (!showedOpenRegionIds.Contains(id))
		{
			showedOpenRegionIds.Add(id);
			Save();
		}
	}

	public bool IsTutorialSeriesArena()
	{
		if (tutorialCompleteSeriesArena == 0)
		{
			tutorialCompleteSeriesArena = 1;
			Save();
			return true;
		}
		return false;
	}

	public bool IsOpenUniqueStatus()
	{
		return tutorialCompleteSeriesArena == 1;
	}

	public void AddChatLog(int user_id, GuildMessage.ChatPostRequest req)
	{
		if (chatLogs.ContainsKey(user_id))
		{
			List<GuildMessage.ChatPostRequest> list = chatLogs[user_id];
			if (list.Count >= 50)
			{
				list.RemoveAt(0);
			}
			list.Add(req);
		}
		else
		{
			chatLogs.Add(user_id, new List<GuildMessage.ChatPostRequest>
			{
				req
			});
		}
	}

	public void SetMutualFollowerInviteListSortType(int _v)
	{
		MutualFollowerInviteListSortType = _v;
		Save();
	}

	public void SetMutualFollowerListSortType(int _v)
	{
		MutualFollowerListSortType = _v;
		Save();
	}

	public void SetFollowListSortType(int _v)
	{
		FollowListSortType = _v;
		Save();
	}

	public void SetFollowerListSortType(int _v)
	{
		FollowerListSortType = _v;
		Save();
	}

	public void SetScreenShotUIFilterType(int _v)
	{
		ScreenShotUIFilterType = _v;
		Save();
	}

	public static void Load()
	{
		if (!SaveData.HasKey(SaveData.Key.Game))
		{
			instance = new GameSaveData();
			SaveData.SetData(SaveData.Key.Game, instance);
			SaveData.Save();
		}
		else
		{
			instance = SaveData.GetData<GameSaveData>(SaveData.Key.Game);
		}
	}

	public static void Save()
	{
		if (instance != null)
		{
			SaveData.SetData(SaveData.Key.Game, instance);
			SaveData.Save();
		}
	}

	public static void Delete()
	{
		SaveData.DeleteKey(SaveData.Key.Game);
		instance = new GameSaveData();
		SaveData.SetData(SaveData.Key.Game, instance);
		SaveData.Save();
	}
}
