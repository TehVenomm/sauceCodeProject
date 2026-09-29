// Decompiled with JetBrains decompiler
// Type: GameSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
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

  public bool IsRecommendedDeliveryCheck() => this.recommendedDeliveryCheck == 1;

  public bool IsRecommendedOrderCheck() => this.recommendedOrderCheck == 1;

  public bool IsRecommendedChallengeCheck() => this.recommendedChallengeCheck == 1;

  public bool IsRecommendedDailyDeliveryCheck() => this.recommendedDailyDeliveryCheck == 1;

  public bool IsRecommendedWeeklyDeliveryCheck() => this.recommendedWeeklyDeliveryCheck == 1;

  public bool IsRecommendedDailyDeliveryCheckAtHome()
  {
    return this.recommendedDailyDeliveryCheckAtHome == 1;
  }

  public bool IsShowNewsNotification()
  {
    DateTime dateTime = DateTime.UtcNow;
    dateTime = dateTime.AddSeconds(-10800.0);
    return this.dayShowNewsNotification != dateTime.Day;
  }

  public void SetCurrentServer(ServerListTable.ServerData server)
  {
    this.currentServer = server;
    NetworkNative.setHost(this.currentServer.url);
    GameSaveData.Save();
  }

  public void SetPushTrackEquipTutorial(bool canPush)
  {
    this.canPushTrackEquipTutorial = canPush;
    GameSaveData.Save();
  }

  public void SetPushedTrackTutorialBit(TRACK_TUTORIAL_STEP_BIT bit)
  {
    this.pushTrackTutorialBit |= 1L << (int) (bit & (TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start | TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear));
    GameSaveData.Save();
  }

  public bool IsPushedTrackTutorialBit(TRACK_TUTORIAL_STEP_BIT bit)
  {
    return bit == TRACK_TUTORIAL_STEP_BIT.MAX || (this.pushTrackTutorialBit & 1L << (int) (bit & (TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start | TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear))) != 0L;
  }

  public bool IsCheckedSmithCreateRecipe(int id)
  {
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) id);
    return equipItemData == null || equipItemData.obtained.flag < 0 || this.IsCheckSmithCreateRecipeBits(equipItemData.obtained.GetSequenceNumber());
  }

  public void AddCheckedSmithCreateRecipe(int[] ids)
  {
    if (ids == null || ids.Length == 0)
      return;
    int index1 = 0;
    for (int length = ids.Length; index1 < length; ++index1)
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) ids[index1]);
      if (equipItemData != null && equipItemData.obtained.flag >= 0)
      {
        int sequenceNumber = equipItemData.obtained.GetSequenceNumber();
        if (!this.IsCheckSmithCreateRecipeBits(sequenceNumber))
        {
          int index2 = sequenceNumber / 32 /*0x20*/;
          int num1 = 1 << sequenceNumber % 32 /*0x20*/;
          int count = this.checkedSmithCreateRecipe.Count;
          if (count <= index2)
          {
            int num2 = index2 - count;
            int num3 = 0;
            for (int index3 = num2; num3 < index3; ++num3)
              this.checkedSmithCreateRecipe.Add(0);
            this.checkedSmithCreateRecipe.Add(num1);
          }
          else
            this.checkedSmithCreateRecipe[index2] |= num1;
        }
      }
    }
  }

  private bool IsCheckSmithCreateRecipeBits(int number)
  {
    int index = number / 32 /*0x20*/;
    return this.checkedSmithCreateRecipe.Count > index && (this.checkedSmithCreateRecipe[index] & 1 << number % 32 /*0x20*/) != 0;
  }

  public string GetSortBit(SortSettings.SETTINGS_TYPE settings_type)
  {
    if (this.sortSaveData == null || this.sortSaveData.Count == 0)
      return string.Empty;
    int index = this.sortSaveData.FindIndex((Predicate<string>) (_data => settings_type == SortSettings.GetSettingsTypeBySortBit(_data)));
    return index == -1 ? string.Empty : this.sortSaveData[index];
  }

  public void SetSortBit(SortSettings settings)
  {
    int index = this.sortSaveData.FindIndex((Predicate<string>) (_data => settings.settingsType == SortSettings.GetSettingsTypeBySortBit(_data)));
    if (index != -1)
      this.sortSaveData.RemoveAt(index);
    this.sortSaveData.Add(SortSettings.GetSortBit(settings));
  }

  public void DeleteSortBit(SortSettings.SETTINGS_TYPE settings_type)
  {
    int index = this.sortSaveData.FindIndex((Predicate<string>) (_data => settings_type == SortSettings.GetSettingsTypeBySortBit(_data)));
    if (index == -1)
      return;
    this.sortSaveData.RemoveAt(index);
  }

  public void DeleteAllSortBit() => this.sortSaveData.Clear();

  public void updateLastNotifyQuestRemainTime(int remainDayThreshold, List<ulong> ids)
  {
    this.lastRemainDayThreshold = remainDayThreshold;
    this.notifyQuestIDs = ids;
    GameSaveData.Save();
  }

  public bool isIncludeNotifyQuestID(List<ulong> ids)
  {
    foreach (ulong id in ids)
    {
      if (!this.notifyQuestIDs.Contains(id))
        return false;
    }
    return true;
  }

  public bool AddNewItem(ITEM_ICON_TYPE type, string str_uniq_id)
  {
    switch (type)
    {
      case ITEM_ICON_TYPE.SKILL_ATTACK:
      case ITEM_ICON_TYPE.SKILL_SUPPORT:
      case ITEM_ICON_TYPE.SKILL_HEAL:
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
      case ITEM_ICON_TYPE.SKILL_GROW:
        if (!this.newSkillItems.Contains(str_uniq_id))
        {
          this.newSkillItems.Add(str_uniq_id);
          return true;
        }
        break;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.USE_ITEM:
        if (!this.newItems.Contains(str_uniq_id))
        {
          this.newItems.Add(str_uniq_id);
          return true;
        }
        break;
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (!this.newQuestItems.Contains(str_uniq_id))
        {
          this.newQuestItems.Add(str_uniq_id);
          return true;
        }
        break;
      case ITEM_ICON_TYPE.ABILITY_ITEM:
        if (!this.abilityItems.Contains(str_uniq_id))
        {
          this.abilityItems.Add(str_uniq_id);
          return true;
        }
        break;
      case ITEM_ICON_TYPE.ACCESSORY:
        if (!this.accessoryItems.Contains(str_uniq_id))
        {
          this.accessoryItems.Add(str_uniq_id);
          return true;
        }
        break;
      default:
        if (!this.newEquipItems.Contains(str_uniq_id))
        {
          this.newEquipItems.Add(str_uniq_id);
          return true;
        }
        break;
    }
    return false;
  }

  public bool RemoveNewIcon(ITEM_ICON_TYPE type, ulong uniq_id)
  {
    return this.RemoveNewIcon(type, uniq_id.ToString());
  }

  public bool RemoveNewIcon(ITEM_ICON_TYPE type, string str_uniq_id)
  {
    switch (type)
    {
      case ITEM_ICON_TYPE.SKILL_ATTACK:
      case ITEM_ICON_TYPE.SKILL_SUPPORT:
      case ITEM_ICON_TYPE.SKILL_HEAL:
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
      case ITEM_ICON_TYPE.SKILL_GROW:
        if (!this.newSkillItems.Contains(str_uniq_id))
          return false;
        this.newSkillItems.Remove(str_uniq_id);
        break;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.USE_ITEM:
        if (!this.newItems.Contains(str_uniq_id))
          return false;
        this.newItems.Remove(str_uniq_id);
        break;
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (!this.newQuestItems.Contains(str_uniq_id))
          return false;
        this.newQuestItems.Remove(str_uniq_id);
        break;
      case ITEM_ICON_TYPE.ABILITY_ITEM:
        if (!this.abilityItems.Contains(str_uniq_id))
          return false;
        this.abilityItems.Remove(str_uniq_id);
        break;
      case ITEM_ICON_TYPE.ACCESSORY:
        if (!this.accessoryItems.Contains(str_uniq_id))
          return false;
        this.accessoryItems.Remove(str_uniq_id);
        break;
      default:
        if (!this.newEquipItems.Contains(str_uniq_id))
          return false;
        this.newEquipItems.Remove(str_uniq_id);
        break;
    }
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.REMOVE_NEW_ICON);
    return true;
  }

  public void RemoveNewIconAndSave(ITEM_ICON_TYPE type, ulong uniq_id)
  {
    if (!this.RemoveNewIcon(type, uniq_id))
      return;
    GameSaveData.Save();
  }

  public bool IsNewItem(ITEM_ICON_TYPE type, ulong uniq_id)
  {
    return this.IsNewItem(type, uniq_id.ToString());
  }

  public bool IsNewItem(ITEM_ICON_TYPE type, string str_uniq_id)
  {
    switch (type)
    {
      case ITEM_ICON_TYPE.NONE:
        return false;
      case ITEM_ICON_TYPE.SKILL_ATTACK:
      case ITEM_ICON_TYPE.SKILL_SUPPORT:
      case ITEM_ICON_TYPE.SKILL_HEAL:
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
      case ITEM_ICON_TYPE.SKILL_GROW:
        return this.newSkillItems.Contains(str_uniq_id);
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.USE_ITEM:
        return this.newItems.Contains(str_uniq_id);
      case ITEM_ICON_TYPE.QUEST_ITEM:
        return this.newQuestItems.Contains(str_uniq_id);
      case ITEM_ICON_TYPE.ABILITY_ITEM:
        return this.abilityItems.Contains(str_uniq_id);
      case ITEM_ICON_TYPE.ACCESSORY:
        return this.accessoryItems.Contains(str_uniq_id);
      default:
        return this.newEquipItems.Contains(str_uniq_id);
    }
  }

  public bool isNewClanScout(string cId, int expiredAt)
  {
    string str = $"{cId}:{(object) expiredAt}";
    int num = !this.newClanScoutList.Contains(str) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.newClanScoutList.Add(str);
    GameSaveData.Save();
    return num != 0;
  }

  public bool isNewReleasePortal(uint id) => this.newReleasePortals.Contains(id);

  public void AddShowedOpenRegionId(int id)
  {
    if (this.showedOpenRegionIds.Contains(id))
      return;
    this.showedOpenRegionIds.Add(id);
    GameSaveData.Save();
  }

  public bool IsTutorialSeriesArena()
  {
    if (this.tutorialCompleteSeriesArena != 0)
      return false;
    this.tutorialCompleteSeriesArena = 1;
    GameSaveData.Save();
    return true;
  }

  public bool IsOpenUniqueStatus() => this.tutorialCompleteSeriesArena == 1;

  public void AddChatLog(int user_id, GuildMessage.ChatPostRequest req)
  {
    if (this.chatLogs.ContainsKey(user_id))
    {
      List<GuildMessage.ChatPostRequest> chatLog = this.chatLogs[user_id];
      if (chatLog.Count >= 50)
        chatLog.RemoveAt(0);
      chatLog.Add(req);
    }
    else
      this.chatLogs.Add(user_id, new List<GuildMessage.ChatPostRequest>()
      {
        req
      });
  }

  public void SetMutualFollowerInviteListSortType(int _v)
  {
    this.MutualFollowerInviteListSortType = _v;
    GameSaveData.Save();
  }

  public void SetMutualFollowerListSortType(int _v)
  {
    this.MutualFollowerListSortType = _v;
    GameSaveData.Save();
  }

  public void SetFollowListSortType(int _v)
  {
    this.FollowListSortType = _v;
    GameSaveData.Save();
  }

  public void SetFollowerListSortType(int _v)
  {
    this.FollowerListSortType = _v;
    GameSaveData.Save();
  }

  public void SetScreenShotUIFilterType(int _v)
  {
    this.ScreenShotUIFilterType = _v;
    GameSaveData.Save();
  }

  public static GameSaveData instance { get; private set; }

  public static void Load()
  {
    if (!SaveData.HasKey(SaveData.Key.Game))
    {
      GameSaveData.instance = new GameSaveData();
      SaveData.SetData<GameSaveData>(SaveData.Key.Game, GameSaveData.instance);
      SaveData.Save();
    }
    else
      GameSaveData.instance = SaveData.GetData<GameSaveData>(SaveData.Key.Game);
  }

  public static void Save()
  {
    if (GameSaveData.instance == null)
      return;
    SaveData.SetData<GameSaveData>(SaveData.Key.Game, GameSaveData.instance);
    SaveData.Save();
  }

  public static void Delete()
  {
    SaveData.DeleteKey(SaveData.Key.Game);
    GameSaveData.instance = new GameSaveData();
    SaveData.SetData<GameSaveData>(SaveData.Key.Game, GameSaveData.instance);
    SaveData.Save();
  }
}
