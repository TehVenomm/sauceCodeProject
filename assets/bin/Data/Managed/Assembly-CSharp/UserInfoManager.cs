// Decompiled with JetBrains decompiler
// Type: UserInfoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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

  public static bool IsValidUser()
  {
    return MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null;
  }

  public Network.UserInfo userInfo { get; private set; }

  public UserStatus userStatus { get; private set; }

  public List<EventBanner> eventBannerList { get; private set; }

  public List<GachaDeco> gachaDecoList { get; private set; }

  public long gachaDecoDateBase { get; private set; }

  public int newsID { get; private set; }

  public bool needOpenNewsPage { get; private set; }

  public DateTime? mysteryTimeDt { get; private set; }

  public List<int> favoriteStampIds { get; private set; }

  public List<int> unlockStampIds { get; private set; }

  public List<int> selectedDegreeIds { get; private set; }

  public List<int> unlockedDegreeIds { get; private set; }

  public int crystalChangeName { get; private set; }

  public string userIdHash { get; set; }

  public string oncePurchaseGachaProductId { get; set; }

  public UserClanData userClan { get; private set; }

  public ClanAdvisaryData advisory { get; set; }

  public bool needShowOneTimesOfferSS { get; set; }

  public bool isShowAppReviewAppeal { get; private set; }

  public bool isArenaOpen { get; private set; }

  public bool isJoinedArenaRanking { get; private set; }

  public bool isGuildRequestOpen { get; private set; }

  public DISPLAY_TYPE clanDisplayType { get; private set; }

  public int clanRequestNum { get; private set; }

  public int Vip_Status { get; private set; } = -1;

  public int SetClanRequestNum(int num)
  {
    if (this.clanRequestNum != num)
    {
      this.clanRequestNum = num;
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
    }
    return this.clanRequestNum;
  }

  public int DecreaseClanRequestNum()
  {
    --this.clanRequestNum;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
    return this.clanRequestNum;
  }

  public void SetClanScoutNum(int scoutNum)
  {
    this.clanInviteNum = scoutNum;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
  }

  public void LeaveClan() => this.userClan.Clear();

  public bool IsRegisteredClan() => this.userClan.IsRegistered();

  public bool isAcquiredUserInfo { private set; get; }

  public bool isShadowChallengeFirst { get; private set; }

  public bool isTheaterRenewal { get; private set; }

  public string alertMessage
  {
    get => this.m_alertMessages.Count <= 0 ? (string) null : this.m_alertMessages[0];
  }

  public bool ExistsPartyInvite
  {
    get
    {
      return this.partyInviteHome | this.partyInviteChat | this.partyInviteResume | this.rallyInviteChat;
    }
  }

  public bool ExistsRallyInvite => this.rallyInviteChat;

  public List<Network.HomeBanner> homeBannerList { get; private set; }

  public void ClearPartyInvite()
  {
    this.partyInviteHome = false;
    this.partyInviteChat = false;
    this.partyInviteResume = false;
    this.rallyInviteChat = false;
  }

  public void SetPartyInviteChat(bool flag)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
    this.partyInviteChat = flag;
  }

  public void SetRallyInviteChat(bool flag)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_RALLY_INVITE);
    this.rallyInviteChat = flag;
  }

  public void SetPartyInviteResume(bool flag)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
    this.partyInviteResume = flag;
  }

  public void SetClanInviteHome()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
    this.partyInviteHome = true;
  }

  public void SetClanDonateInviteHome()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE);
    this.partyInviteHome = true;
  }

  public void OnReadAlertMessage() => this.m_alertMessages.RemoveAt(0);

  public void SetEventBannerList(List<EventBanner> list)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER);
    if (list == null)
      this.ResetEventBannerList();
    else
      this.eventBannerList = list;
  }

  public void ResetEventBannerList()
  {
    if (this.eventBannerList == null)
      return;
    this.eventBannerList.Clear();
  }

  public void SetGachaDecoList(List<GachaDeco> list)
  {
    this.gachaDecoList = list;
    this.gachaDecoList.Sort((Comparison<GachaDeco>) ((a, b) => a.orderNo.CompareTo(b.orderNo)));
    this.gachaDecoDateBase = TimeManager.GetNow().Ticks;
  }

  public bool shouldDispAdvancedOffer() => !this.userInfo.IsAdvanced && this.userInfo.isCharged;

  public bool CheckTutorialBit(TUTORIAL_MENU_BIT bit)
  {
    return bit != TUTORIAL_MENU_BIT.MAX && (this.userStatus.TutorialBit & 1L << (int) (bit & (TUTORIAL_MENU_BIT.CLAIM_REWARD | TUTORIAL_MENU_BIT.FORGE_ITEM))) != 0L;
  }

  public bool IsEndTutorialBit()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && (this.userStatus.TutorialBit & 8589934592L /*0x0200000000*/) != 0L;
  }

  public bool IsEndTutorial()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST);
  }

  public bool CheckTutorialBitUnlock(TUTORIAL_MENU_BIT bit, long oldTutorialBit)
  {
    return (oldTutorialBit & 1L << (int) (bit & (TUTORIAL_MENU_BIT.CLAIM_REWARD | TUTORIAL_MENU_BIT.FORGE_ITEM))) == 0L && this.CheckTutorialBit(bit);
  }

  public static bool IsRegisterdAge() => MonoBehaviourSingleton<UserInfoManager>.IsValid();

  public static bool IsEnableCommunication() => true;

  public static bool IsNeedsTutorialMessage()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return true;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialBit == null)
      return false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 9)
      return true;
    for (int index = 0; index < UserInfoManager.needTutorialBits.Length; ++index)
    {
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(UserInfoManager.needTutorialBits[index]))
        return true;
    }
    return false;
  }

  public static bool IsFinishTutorial()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || string.IsNullOrEmpty(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName()) || !MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userStatus == null || !MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady || !TutorialStep.HasAllTutorialCompleted())
      return false;
    return MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM);
  }

  private UserInfoManager()
  {
    this.userInfo = new Network.UserInfo();
    this.userStatus = new UserStatus();
    this.favoriteStampIds = new List<int>();
    this.m_alertMessages = new List<string>();
    this.userClan = new UserClanData();
  }

  private void Update()
  {
    if (this.userInfo == null || this.userInfo.id <= 0 || this.userInfo.constDefine.ALIVE_CHECK_SEC <= 0 || (double) Time.time - (double) MonoBehaviourSingleton<NetworkManager>.I.lastRequestTime < (double) this.userInfo.constDefine.ALIVE_CHECK_SEC)
      return;
    this.SendAlive();
  }

  public void DirtyUserInfo()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_USER_INFO);
  }

  public void DirtyUserStatus()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS);
  }

  private void SetUserInfo(Network.UserInfo user_info)
  {
    this.LoadFavoriteStamp();
    this.userInfo = user_info;
    CrashlyticsReporter.SetUserInfo(user_info);
    MonoBehaviourSingleton<GoWrapManager>.I.setGUID(user_info.code);
    this.DirtyUserInfo();
  }

  private void SetUserStatus(UserStatus user_status)
  {
    this.userStatus = user_status;
    this.DirtyUserStatus();
  }

  public void SetUserClan(UserClanData user_clan) => this.userClan = user_clan;

  public void SetFavoriteStamp(List<int> setFavorites)
  {
    if (setFavorites.Count > 10)
      setFavorites.RemoveRange(10, setFavorites.Count - 10);
    this.favoriteStampIds = setFavorites;
    this.SaveFavoriteStamp();
  }

  private void SaveFavoriteStamp()
  {
    PlayerPrefs.SetString("FAVORITE_STAMP_KEY", this.favoriteStampIds.ToJoinString<int>());
    PlayerPrefs.Save();
  }

  private void LoadFavoriteStamp()
  {
    string str = PlayerPrefs.GetString("FAVORITE_STAMP_KEY", "");
    if (string.IsNullOrEmpty(str))
    {
      this.favoriteStampIds = GameDefine.FAVORITE_STAMP_DEFAULT;
    }
    else
    {
      this.favoriteStampIds = GameDefine.FAVORITE_STAMP_DEFAULT;
      string[] strArray = str.Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        int result;
        if (int.TryParse(strArray[index], out result))
          this.favoriteStampIds[index] = result;
      }
    }
  }

  public void SetUserInfoAndUserStatus(
    Network.UserInfo user_info,
    UserStatus user_status,
    UserClanData user_clan,
    List<int> unlock_stamps,
    List<int> selected_Degrees,
    List<int> unlocked_Degrees)
  {
    this.SetUserInfo(user_info);
    this.SetUserStatus(user_status);
    this.SetUserClan(user_clan);
    this.unlockStampIds = unlock_stamps;
    this.selectedDegreeIds = selected_Degrees;
    this.unlockedDegreeIds = unlocked_Degrees;
    if (!MonoBehaviourSingleton<UIManager>.IsValid())
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.OnUpdateUnlockStampList();
  }

  public bool IsUnlockedStamp(int stampId)
  {
    return this.unlockStampIds != null && this.unlockStampIds.Contains(stampId);
  }

  public bool IsUnlockedDegree(int degreeId)
  {
    return this.unlockedDegreeIds != null && this.unlockedDegreeIds.Contains(degreeId);
  }

  public void UpdateUnlockDegrees(BaseModelDiff.DiffUnlockDegree diff)
  {
    this.unlockedDegreeIds = diff.update;
  }

  public void UpdateSelectedDegrees(BaseModelDiff.DiffSelectedDegree diff)
  {
    if (diff.add != null)
    {
      foreach (SelectDegree selectDegree in diff.add)
        this.selectedDegreeIds[selectDegree.positionNo] = selectDegree.degreeId;
    }
    if (diff.update == null)
      return;
    foreach (SelectDegree selectDegree in diff.update)
      this.selectedDegreeIds[selectDegree.positionNo] = selectDegree.degreeId;
  }

  public void UpdateUnlockStamps(BaseModelDiff.DiffStamp diff)
  {
    this.unlockStampIds = diff.update;
    if (!MonoBehaviourSingleton<UIManager>.IsValid())
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.OnUpdateUnlockStampList();
  }

  public void SetRecvUserInfo(Network.UserInfo user_info, int tutorialStep = 0)
  {
    this.SetUserInfo(user_info);
    if (tutorialStep <= this.userStatus.tutorialStep)
      return;
    this.userStatus.tutorialStep = tutorialStep;
  }

  public void SetNewsID(int news_id)
  {
    int num = PlayerPrefs.GetInt("LastNewsID", -1);
    this.newsID = news_id;
    string str = PlayerPrefs.GetString("fcm_registed", "");
    if (news_id != num || string.IsNullOrEmpty(str) || !NetworkNative.getNativeVersionNameRemoveDot().Equals(str))
      NetworkNative.createRegistrationId();
    if (this.needOpenNewsPage || news_id == num || news_id == -1)
      return;
    this.needOpenNewsPage = true;
  }

  public void SetNextDonationTime(string time_str)
  {
    this.userStatus.nextDonationTime = DateTime.Parse(time_str);
  }

  public void OnOpenNewsPage()
  {
    PlayerPrefs.SetInt("LastNewsID", this.newsID);
    this.needOpenNewsPage = false;
  }

  private int equipItemNum
  {
    get => MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetCount();
  }

  private int skillItemNum
  {
    get => MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount();
  }

  public bool IsOverEquipItem() => this.GetOverEquipItemNum() > 0;

  public int GetOverEquipItemNum() => this.equipItemNum - this.userStatus.maxEquipItem;

  public bool IsOverSkillItem() => this.GetOverSkillItemNum() > 0;

  public int GetOverSkillItemNum() => this.skillItemNum - this.userStatus.maxSkillItem;

  public bool IsStorageOverflow() => this.IsOverEquipItem() || this.IsOverSkillItem();

  public bool IsEquipVisualItem(ulong uniq_id)
  {
    if (uniq_id == 0UL)
      return false;
    UserStatus userStatus = this.userStatus;
    return (long) ulong.Parse(userStatus.armorUniqId) == (long) uniq_id || (long) ulong.Parse(userStatus.helmUniqId) == (long) uniq_id || (long) ulong.Parse(userStatus.armUniqId) == (long) uniq_id || (long) ulong.Parse(userStatus.legUniqId) == (long) uniq_id;
  }

  public int GetFaceModelID()
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetFaceModelID(this.userStatus.sex, this.userStatus.faceId);
  }

  public int GetHairModelID()
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairModelID(this.userStatus.sex, this.userStatus.hairId);
  }

  public Color GetSkinColor()
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(this.userStatus.skinId);
  }

  public Color GetHairColor()
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(this.userStatus.hairColorId);
  }

  public int GetEnemyLevelFromUserLevel()
  {
    return this.GetEnemyLevelFromUserLevel((int) this.userStatus.level);
  }

  private int GetEnemyLevelFromUserLevel(int userLevel)
  {
    int questItemLevelMax = this.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX;
    return (Mathf.Clamp(userLevel, 10, questItemLevelMax) + 9) / 10 * 10;
  }

  public void SendHomeInfo(Action<bool, bool, int> callBack)
  {
    Protocol.SendAsync<HomeInfoModel.SendForm, HomeInfoModel>("ajax/home/info", new HomeInfoModel.SendForm()
    {
      appStr = NetworkNative.getAppStr()
    }, (Action<HomeInfoModel>) (ret =>
    {
      bool flag1 = false;
      bool flag2 = false;
      int num = 0;
      if (ret.Error == Error.None)
      {
        flag1 = true;
        this.isAcquiredUserInfo = true;
        flag2 = ret.result.loginBonus;
        this.oncePurchaseGachaProductId = ret.result.productId;
        this.needShowOneTimesOfferSS = ret.result.isOneTimesOfferActive;
        if (MonoBehaviourSingleton<TradingPostManager>.IsValid())
          MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostInfo(ret.result);
        this.partyInviteHome = this.showJoinClanInGame ? PartyManager.IsValidNotEmptyList() : ret.result.party;
        if (!this.partyInviteHome)
          this.ClearPartyInvite();
        this.m_alertMessages.AddRange((IEnumerable<string>) ret.result.alertMessages);
        this.SetEventBannerList(ret.result.banner);
        this.SetGachaDecoList(ret.result.gachaDeco);
        this.SetNewsID(ret.result.newsId);
        this.SetNextDonationTime(ret.result.nextDonationTime);
        this.advisory = ret.result.advisory;
        this.isShowAppReviewAppeal = ret.result.isDisplayReview >= 1;
        this.isArenaOpen = ret.result.isArenaOpen;
        this.isJoinedArenaRanking = ret.result.isJoinedArenaRanking;
        this.isGuildRequestOpen = ret.result.isGuildRequestOpen;
        this.isTheaterRenewal = ret.result.isTheaterRenewal;
        this.SetClanScoutNum(ret.result.clanInviteNum);
        this.clanDisplayType = (DISPLAY_TYPE) ret.result.clanDisplayType;
        this.SetClanRequestNum(ret.result.clanRequestNum);
        if (MonoBehaviourSingleton<ChatManager>.IsValid())
          MonoBehaviourSingleton<ChatManager>.I.OnNotifyUpdateChannnelInfo(ret.result.chat);
        if (MonoBehaviourSingleton<QuestManager>.IsValid())
        {
          MonoBehaviourSingleton<QuestManager>.I.SetEventList(ret.result.events);
          MonoBehaviourSingleton<QuestManager>.I.SetFutureEventList(ret.result.futureEventIds);
          MonoBehaviourSingleton<QuestManager>.I.SetBingoEventList(ret.result.bingoEvents);
        }
        if (MonoBehaviourSingleton<FriendManager>.IsValid())
          MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(ret.result.message);
        GameSceneGlobalSettings.GetCurrentIHomeManager()?.SetPointShop(ret.result.isPointShopOpen, ret.result.pointShopBanner);
        if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid())
          MonoBehaviourSingleton<LoungeMatchingManager>.I.SetOpenLounge(ret.result.isLoungeOpen);
        num = ret.result.task;
        if (MonoBehaviourSingleton<DeliveryManager>.IsValid())
          MonoBehaviourSingleton<DeliveryManager>.I.UpdateDeliveryReaminTime(ret.result.dailyRemainTime, ret.result.weeklyRemainTime);
        this.isShadowChallengeFirst = ret.result.isShadowChallengeFirst != 0;
        if (ret.result.isShadowChallengeFirst != 0)
        {
          GameSaveData.instance.recommendedChallengeCheck = 1;
          GameSaveData.Save();
        }
        this.crystalChangeName = ret.result.crystalChangeName;
        if (MonoBehaviourSingleton<StatusManager>.IsValid())
          MonoBehaviourSingleton<StatusManager>.I.SetTimeSlotEvents(ret.result.timeSlotEvents);
        if (!string.IsNullOrEmpty(ret.currentTime) && MonoBehaviourSingleton<GoGameTimeManager>.IsValid())
          GoGameTimeManager.SetServerTime(ret.currentTime);
        if (!string.IsNullOrEmpty(ret.result.blackShopEndDate))
        {
          int totalSeconds = (int) GoGameTimeManager.GetRemainTime(ret.result.blackShopEndDate).TotalSeconds;
          if (totalSeconds > 0)
          {
            if (!GameSaveData.instance.resetMarketTime.Equals(ret.result.blackShopEndDate))
            {
              GameSaveData.instance.canShowNoteDarkMarket = true;
              GameSaveData.instance.resetMarketTime = ret.result.blackShopEndDate;
            }
            MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.InitTime(totalSeconds);
          }
        }
        else
          GameSaveData.instance.resetMarketTime = string.Empty;
        this.isWheelOfFortuneOn = ret.result.isWheelOfFortuneOn;
        GameSaveData.instance.canShowWheelFortune = this.isWheelOfFortuneOn;
        this.SetHomeBannerList(ret.result.homeBanner);
      }
      callBack(flag1, flag2, num);
    }));
  }

  public void SendChangeName(string name, Action<bool> call_back)
  {
    Protocol.Send<OptionChangeNameModel.RequestSendForm, OptionChangeNameModel>(OptionChangeNameModel.URL, new OptionChangeNameModel.RequestSendForm()
    {
      name = name
    }, (Action<OptionChangeNameModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendEditFigure(
    int sex,
    int faceId,
    int haireId,
    int haireColorId,
    int skinId,
    int voiceId,
    string name,
    Action<bool> call_back)
  {
    Protocol.Send<OptionEditFigureModel.RequestSendForm, OptionEditFigureModel>(OptionEditFigureModel.URL, new OptionEditFigureModel.RequestSendForm()
    {
      sex = sex,
      face = faceId,
      hair = haireId,
      color = haireColorId,
      skin = skinId,
      voice = voiceId,
      name = name
    }, (Action<OptionEditFigureModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendEditComment(string comment, Action<bool> call_back)
  {
    Protocol.Send<OptionEditCommentModel.RequestSendForm, OptionEditCommentModel>(OptionEditCommentModel.URL, new OptionEditCommentModel.RequestSendForm()
    {
      comment = comment
    }, (Action<OptionEditCommentModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendBirthday(int year, int month, int day, Action<bool> call_back)
  {
    Protocol.Send<OptionBirthdayModel.RequestSendForm, OptionBirthdayModel>(OptionBirthdayModel.URL, new OptionBirthdayModel.RequestSendForm()
    {
      year = year,
      month = month,
      day = day
    }, (Action<OptionBirthdayModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendStopper(bool is_enable_stopper, Action<bool> call_back)
  {
    Protocol.Send<OptionStopperModel.RequestSendForm, OptionStopperModel>(OptionStopperModel.URL, new OptionStopperModel.RequestSendForm()
    {
      on = is_enable_stopper ? 1 : 0
    }, (Action<OptionStopperModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendParentalPassword(
    string password,
    string confirm_password,
    Action<Error> call_back)
  {
    Protocol.Send<OptionSetParentPassModel.RequestSendForm, OptionSetParentPassModel>(OptionSetParentPassModel.URL, new OptionSetParentPassModel.RequestSendForm()
    {
      password = password,
      confirmPassword = confirm_password
    }, (Action<OptionSetParentPassModel>) (ret => call_back(ret.Error)));
  }

  public void SendResetParentalPassword(string password, Action<Error> call_back)
  {
    Protocol.Send<OptionResetParentPassModel.RequestSendForm, OptionResetParentPassModel>(OptionResetParentPassModel.URL, new OptionResetParentPassModel.RequestSendForm()
    {
      password = password
    }, (Action<OptionResetParentPassModel>) (ret => call_back(ret.Error)));
  }

  public void SendInviteInput(string code, Action<bool> call_back)
  {
    Protocol.Send<InviteInputModel.RequestSendForm, InviteInputModel>(InviteInputModel.URL, new InviteInputModel.RequestSendForm()
    {
      code = code
    }, (Action<InviteInputModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendPushNotificationDeviceEnable(int enable, Action<bool> call_back)
  {
    Protocol.Send<PushNotificationDeviceEnableModel.RequestSendForm, PushNotificationDeviceEnableModel>(PushNotificationDeviceEnableModel.URL, new PushNotificationDeviceEnableModel.RequestSendForm()
    {
      enable = enable
    }, (Action<PushNotificationDeviceEnableModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendAppReviewInfo(int starValue, int buttonNum, Action<bool> call_back)
  {
    Protocol.Send<AppReviewInfoModel.RequestSendForm, AppReviewInfoModel>(AppReviewInfoModel.URL, new AppReviewInfoModel.RequestSendForm()
    {
      starValue = starValue,
      replyAction = buttonNum
    }, (Action<AppReviewInfoModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendOpinionMessage(string msg, Action<bool> call_back)
  {
    Protocol.Send<OpinionPostModel.RequestSendForm, OpinionPostModel>(OpinionPostModel.URL, new OpinionPostModel.RequestSendForm()
    {
      msg = msg
    }, (Action<OpinionPostModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendTutorialStep(Action<bool> call_back)
  {
    if (this.userStatus.tutorialStep >= 9)
      call_back(true);
    else
      Protocol.Send<UserStatusTutorialModel.RequestSendForm, UserStatusTutorialModel>(UserStatusTutorialModel.URL, new UserStatusTutorialModel.RequestSendForm()
      {
        bit = 0
      }, (Action<UserStatusTutorialModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendTutorialBit(TUTORIAL_MENU_BIT bit, Action<bool> call_back = null)
  {
    if (!TutorialStep.HasAllTutorialCompleted())
    {
      if (call_back == null)
        return;
      call_back(false);
    }
    else if (this.CheckTutorialBit(bit))
    {
      if (call_back == null)
        return;
      call_back(true);
    }
    else
      Protocol.Send<UserStatusTutorialModel.RequestSendForm, UserStatusTutorialModel>(UserStatusTutorialModel.URL, new UserStatusTutorialModel.RequestSendForm()
      {
        bit = (int) bit
      }, (Action<UserStatusTutorialModel>) (ret =>
      {
        bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
        if (call_back == null)
          return;
        call_back(flag);
      }));
  }

  public void SendDebugNextTutorial()
  {
    Protocol.Send<DebugNextTutorialModel>(DebugNextTutorialModel.URL, (Action<DebugNextTutorialModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      this.userStatus.tutorialStep = ret.tutorial;
    }));
  }

  public void SendDebugResearchLv(int lv, Action<bool> call_back)
  {
    Protocol.Send<DebugSetResearchLvModel.RequestSendForm, DebugSetResearchLvModel>(DebugSetResearchLvModel.URL, new DebugSetResearchLvModel.RequestSendForm()
    {
      lv = lv
    }, (Action<DebugSetResearchLvModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendDebugSetEditNameTime(string date, Action<bool> call_back)
  {
    Protocol.Send<DebugSetEditNameTimeModel.RequestSendForm, DebugSetEditNameTimeModel>(DebugSetEditNameTimeModel.URL, new DebugSetEditNameTimeModel.RequestSendForm()
    {
      date = date
    }, (Action<DebugSetEditNameTimeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendDebugSetGrade(int fieldGrade, int questGrade, Action<bool> call_back)
  {
    Protocol.Send<DebugSetGradeModel.RequestSendForm, DebugSetGradeModel>(DebugSetGradeModel.URL, new DebugSetGradeModel.RequestSendForm()
    {
      fg = fieldGrade,
      qg = questGrade
    }, (Action<DebugSetGradeModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendDebutResetGrade(Action<bool> call_back)
  {
    Protocol.Send<DebugResetGradeModel>(DebugResetGradeModel.URL, (Action<DebugResetGradeModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendDebugSetLv(int lv, Action<bool> call_back)
  {
    Protocol.Send<DebugSetLvModel.RequestSendForm, DebugSetLvModel>(DebugSetLvModel.URL, new DebugSetLvModel.RequestSendForm()
    {
      lv = lv
    }, (Action<DebugSetLvModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendDebugSetTutorial(int step, string bit, Action<bool> call_back)
  {
    Protocol.Send<DebugSetTutorialModel.RequestSendForm, DebugSetTutorialModel>(DebugSetTutorialModel.URL, new DebugSetTutorialModel.RequestSendForm()
    {
      step = step,
      bit = bit
    }, (Action<DebugSetTutorialModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendAlive(Action<bool> call_back = null)
  {
    MonoBehaviourSingleton<NetworkManager>.I.Request<StatusAliveModel>(StatusAliveModel.URL, (Action<StatusAliveModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  public void SendGatherItemRecord(
    int userId,
    int eventId,
    Action<bool, GatherItemUserRecordModel> call_back = null)
  {
    Protocol.Send<GatherItemUserRecordModel.RequestSendForm, GatherItemUserRecordModel>(GatherItemUserRecordModel.URL, new GatherItemUserRecordModel.RequestSendForm()
    {
      userId = userId,
      eventId = eventId
    }, (Action<GatherItemUserRecordModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (call_back == null)
        return;
      call_back(flag, ret);
    }));
  }

  public void SendVipStatus(Action<bool> call_back = null)
  {
    Debug.Log((object) "Send  STATUS ");
    Protocol.Send<GoldIsVipModel>(GoldIsVipModel.URL, (Action<GoldIsVipModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        this.Vip_Status = ret.result ? 1 : 0;
      Debug.Log((object) ("VIP STATUS " + (object) this.Vip_Status));
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  public void OnDiff(BaseModelDiff.DiffUser diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.name))
    {
      this.userInfo.name = diff.name[0].name;
      this.userInfo.editNameAt = diff.name[0].editNameAt;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.comment))
    {
      this.userInfo.comment = diff.comment[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.birthday))
    {
      this.userInfo.birthday = diff.birthday[0].birthday;
      this.userInfo.communityFlag = diff.birthday[0].communityFlag;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.option))
    {
      this.userInfo.isParentPassSet = diff.option[0].isParentPassSet;
      this.userInfo.isStopperSet = diff.option[0].isStopperSet;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.inputInviteFlag))
    {
      this.userInfo.inputInviteFlag = diff.inputInviteFlag[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.pushEnable))
    {
      this.userInfo.pushEnable = diff.pushEnable[0];
      flag = true;
    }
    if (!flag)
      return;
    this.DirtyUserInfo();
  }

  public void OnDiff(BaseModelDiff.DiffStatus diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.views))
    {
      BaseModelDiff.DiffStatus.Views view = diff.views[0];
      this.userStatus.sex = view.sex;
      this.userStatus.faceId = view.faceId;
      this.userStatus.hairId = view.hairId;
      this.userStatus.hairColorId = view.hairColorId;
      this.userStatus.skinId = view.skinId;
      this.userStatus.voiceId = view.voiceId;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.grow))
    {
      BaseModelDiff.DiffStatus.Grow grow = diff.grow[0];
      if ((int) this.userStatus.level < grow.level)
      {
        if ((int) this.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel && grow.level >= MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
          GameSaveData.instance.showUnlockQuestEvent = true;
        Dictionary<string, object> values = new Dictionary<string, object>();
        values.Add("value", (object) grow.level);
        MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("reach_level", "Gameplay", values);
        if ((int) this.userStatus.level < 50 && grow.level >= 50)
        {
          Debug.Log((object) $"track event lv user lv:{(object) this.userStatus.level}grow lv:{(object) grow.level}");
          MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("reach_level_50", "Gameplay", values);
        }
      }
      this.userStatus.level = (XorInt) grow.level;
      this.userStatus.exp = (XorInt) grow.exp;
      this.userStatus.expPrev = (XorInt) grow.expPrev;
      this.userStatus.expNext = (XorInt) grow.expNext;
      this.userStatus.hp = (XorInt) grow.hp;
      this.userStatus.atk = (XorInt) grow.atk;
      this.userStatus.def = (XorInt) grow.def;
      this.userStatus.maxFollow = grow.maxFollow;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.money))
    {
      this.userStatus.money = diff.money[0];
      flag = true;
      if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene" && MonoBehaviourSingleton<SmithManager>.IsValid())
        MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(true);
    }
    if (Utility.IsExist((ICollection) diff.crystal))
    {
      if (this.userStatus.crystal > diff.crystal[0])
        GameSaveData.instance.spent25Gems += this.userStatus.crystal - diff.crystal[0];
      this.userStatus.crystal = diff.crystal[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.eSetNo))
    {
      this.userStatus.eSetNo = diff.eSetNo[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.ueSetNo))
    {
      this.userStatus.ueSetNo = diff.ueSetNo[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.titleId))
    {
      this.userStatus.titleId = diff.titleId[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.capacity))
    {
      BaseModelDiff.DiffStatus.Capacity capacity = diff.capacity[0];
      this.userStatus.maxEquipItem = capacity.maxEquipItem;
      this.userStatus.maxSkillItem = capacity.maxSkillItem;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.tutorialStep))
    {
      this.userStatus.tutorialStep = diff.tutorialStep[0];
      flag = true;
      if (this.userStatus.tutorialStep != 4)
      {
        if (this.userStatus.tutorialStep == 7)
        {
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_weapon_crafting, "Tutorial");
        }
        else
        {
          int tutorialStep = this.userStatus.tutorialStep;
        }
      }
    }
    if (Utility.IsExist((ICollection) diff.tutorialBit))
    {
      long oldTutorialBit = 0;
      if (this.userStatus.tutorialBit != null)
        oldTutorialBit = this.userStatus.TutorialBit;
      this.userStatus.tutorialBit = diff.tutorialBit[0];
      flag = true;
      if (this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA_QUEST_START, oldTutorialBit))
      {
        MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_9_behemoth_fight_begin_1, "Tutorial");
        Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_9_behemoth_fight_begin_1.ToString()));
        MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_9_behemoth_fight_begin_1, "Tutorial");
      }
      else if (this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.SHADOW_QUEST_START, oldTutorialBit))
      {
        MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_13_behemoth_fight_begin_2, "Tutorial");
        Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_13_behemoth_fight_begin_2.ToString()));
        MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_13_behemoth_fight_begin_2, "Tutorial");
      }
      else if (!this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA1, oldTutorialBit) && !this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA_QUEST_BATTLE_RESULT, oldTutorialBit) && !this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.GACHA2, oldTutorialBit) && !this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.SKILL_EQUIP, oldTutorialBit) && !this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.CLAIM_REWARD, oldTutorialBit))
      {
        if (this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.FORGE_ITEM, oldTutorialBit))
        {
          GameSaveData.instance.SetPushTrackEquipTutorial(true);
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_weapon_crafting, "Tutorial");
        }
        else if (this.CheckTutorialBitUnlock(TUTORIAL_MENU_BIT.AFTER_GACHA2, oldTutorialBit))
          MonoBehaviourSingleton<NativeGameService>.I.SignInFirstTime();
      }
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetErrorResendQuestGachaFlag();
    }
    if (Utility.IsExist((ICollection) diff.tutorialQuestId))
    {
      this.userStatus.tutorialQuestId = diff.tutorialQuestId[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.researchLv))
    {
      this.userStatus.researchLv = diff.researchLv[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.questGrade))
    {
      this.userStatus.questGrade = diff.questGrade[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.fieldGrade))
    {
      this.userStatus.fieldGrade = diff.fieldGrade[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.showEquip))
    {
      BaseModelDiff.DiffStatus.ShowEquip showEquip = diff.showEquip[0];
      this.userStatus.armorUniqId = showEquip.armorUniqId;
      this.userStatus.helmUniqId = showEquip.helmUniqId;
      this.userStatus.armUniqId = showEquip.armUniqId;
      this.userStatus.legUniqId = showEquip.legUniqId;
      this.userStatus.showHelm = showEquip.showHelm;
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.fairyNum))
    {
      this.userStatus.fairyNum = diff.fairyNum[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.maxEquipItemTargetNum))
    {
      this.userStatus.maxEquipItemTargetNum = diff.maxEquipItemTargetNum[0];
      flag = true;
    }
    if (!flag)
      return;
    this.DirtyUserStatus();
  }

  public void OnDiff(BaseModelDiff.DiffUserClan diff)
  {
    if (!Utility.IsExist((ICollection) diff.update))
      return;
    this.userClan = diff.update[0];
  }

  public void OnDiff(BaseModelDiff.DiffNotice diff)
  {
    if (!Utility.IsExist((ICollection) diff.login))
      return;
    FieldManager.IsValidInGame();
  }

  public void OnDiff(BaseModelDiff.DiffServerConstDefine diff)
  {
    if (!Utility.IsExist((ICollection) diff.update) || this.userInfo == null)
      return;
    this.userInfo.constDefine = diff.update[0];
  }

  public void SetHomeBannerList(List<Network.HomeBanner> list)
  {
    if (list == null)
      this.ResetHomeBannerList();
    else
      this.homeBannerList = list;
  }

  public void ResetHomeBannerList()
  {
    if (this.homeBannerList == null)
      return;
    this.homeBannerList.Clear();
  }
}
