// Decompiled with JetBrains decompiler
// Type: HomeBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using App.Scripts.GoGame.Optimization;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public abstract class HomeBase : GameSection
{
  public static readonly string QuestBalloonName = "QUEST_COUNTER_BALLOON";
  protected IHomeManager iHomeManager;
  protected OutGameSettingsManager.HomeScene homeSetting;
  public static bool OnTalkPamelaTutorial = false;
  public static bool OnAfterGacha2Tutorial = false;
  private Transform mdlArrow;
  private Transform mdlArrowQuest;
  public static bool OnClickQuestForTutorial = false;
  public static bool isFirstTimeDisplayTextTutorial = false;
  private static bool _isHomeInfoCached;
  private static int _taskBadgeNum;
  private static bool _acquireLoginBonus;
  private static bool _isReceiveLoginBonus;
  public static bool _isWaitingLoginBonus;
  private List<LoginBonus> limitedLoginBonus;
  private bool triger_tutorial_gacha_1;
  private bool triger_tutorial_force_item;
  private bool triger_tutorial_change_item;
  private bool triger_tutorial_gacha_2;
  private bool triger_tutorial_upgrade;
  protected Transform eventLockMesh;
  protected bool isEventLockLoading;
  private GameObject noticeObject;
  private Transform noticeTransform;
  private TweenAlpha noticeTween;
  private HomeStageAreaEvent noticeEvent;
  private HomeStageAreaEvent noticeEventTo;
  private Vector3 noticePos;
  private Vector3 questIconPos;
  private Vector3 orderIconPos;
  private Vector3 eventIconPos;
  protected Vector3 pointShopIconPos;
  protected Vector3 bingoIconPos;
  private Transform questBalloon;
  private Transform storyBalloon;
  private Transform orderBalloon;
  private Transform eventBalloon;
  protected Transform pointShopBalloon;
  protected Transform bingoBalloon;
  private UI_Common.EVENT_BALLOON_TYPE currentEventBalloonType;
  protected bool waitEventBalloon;
  private HomeTutorialManager homeTutorialManager;
  private bool transferNoticeNewDelivery;
  private bool sendTutorialTrigger;
  private bool executeTutorialStep6;
  private bool executeTutorialEnd;
  private bool executeTutorialClaimReward = true;
  protected bool validLoginBonus;
  protected bool shouldFrameInNPC006;
  protected HomeNPCCharacter npc06Info;
  private bool needCheckNotifyQuestRemain;
  private bool needShowDailyDelivery;
  private bool needShowAppReviewAppeal;
  private bool needShowShadowChallengeFirst;
  private bool needCountdown;
  private bool needFollowCheck;
  private int prevLevel = -1;
  private int prevQuest = -1;
  private bool fromQuestCounterAreaEvent;
  private HomeTopBonusTime bonusTime;

  private void OnDisable()
  {
    if (!Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
      return;
    this.homeTutorialManager.DeleteArrow();
    Debug.Log((object) "Delete Arrow");
  }

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => Native.applicationQuit();

  public override void InitializeReopen()
  {
    base.InitializeReopen();
    this.StartCoroutine(this.IESetupLoginBonus());
  }

  private IEnumerator IESetupLoginBonus()
  {
    while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) new WaitForSeconds(0.04f);
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && this.limitedLoginBonus != null && this.limitedLoginBonus.Count > 0 && HomeBase._isWaitingLoginBonus)
      this.SetupLoginBonus();
    HomeTutorialManager.DoesTutorialAfterGacha2();
    yield return (object) null;
  }

  public override void Initialize()
  {
    this.DestroyInGameTutorialManager();
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<InGameManager>.I.selfCacheObject, (Object) null))
    {
      MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerModel();
      Object.Destroy((Object) MonoBehaviourSingleton<InGameManager>.I.selfCacheObject);
    }
    MonoBehaviourSingleton<InventoryManager>.I.SetList();
    NetworkNative.createRegistrationId();
    RenderTargetCacher component = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>();
    if (Object.op_Inequality((Object) component, (Object) null))
      ((Behaviour) component).enabled = false;
    MonoBehaviourSingleton<StatusManager>.I.SetUserStatus();
    if (MonoBehaviourSingleton<SmithManager>.IsValid())
      MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData();
    this.SetupNotice();
    if (MonoBehaviourSingleton<ShopManager>.IsValid() && !MonoBehaviourSingleton<ShopManager>.I.HasCheckPromotionItem)
      MonoBehaviourSingleton<ShopManager>.I.SendCheckPromotion();
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      this.prevLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
      this.prevQuest = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade;
      MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
    }
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetErrorResendQuestGachaFlag();
    if (MonoBehaviourSingleton<StatusManager>.IsValid())
      MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
    this.iHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
    this.homeSetting = this.iHomeManager.GetSceneSetting();
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_7_town_hall_1, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_7_town_hall_1.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_7_town_hall_1, "Tutorial");
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_10_town_hall_2, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_10_town_hall_2.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_10_town_hall_2, "Tutorial");
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_12_town_hall_3, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_12_town_hall_3.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_12_town_hall_3, "Tutorial");
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_14_town_hall_4, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_14_town_hall_4.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_14_town_hall_4, "Tutorial");
    }
    if (MonoBehaviourSingleton<ShopManager>.IsValid())
      MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList((Action<bool>) null);
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.Vip_Status == -1)
      MonoBehaviourSingleton<UserInfoManager>.I.SendVipStatus();
    this.StartCoroutine(this.DoInitialize());
  }

  public override void StartSection()
  {
    this.SetupPointShop();
    this.SetUpBingo();
    this.CheckEventLock();
    if (this.CheckOpenGacha() || this.CheckNeededGotoGacha() || this.CheckNeededOpenQuest() || this.CheckJoinClanIngame() || this.CheckInvitedClanBySNS() || this.CheckInvitedPartyBySNS() || this.CheckInvitedLoungeBySNS() || this.CheckMutualFollowBySNS())
      return;
    if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04) && !TutorialStep.HasDeliveryRewardCompleted())
      this.DispatchEvent("QUEST_COUNTER");
    else if (!TutorialStep.HasChangeEquipCompleted())
    {
      if (!TutorialStep.IsPlayingStudioTutorial() || TutorialStep.isSendFirstRewardComplete)
        return;
      this.TutorialStep_6();
    }
    else
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
        this.SetupLoginBonus();
      this.needCheckNotifyQuestRemain = true;
      this.needShowAppReviewAppeal = true;
      this.needShowShadowChallengeFirst = true;
      this.needCountdown = true;
      if (MonoBehaviourSingleton<AccountManager>.I.logInBonusLimitedCount < 3)
        this.needShowDailyDelivery = true;
      if (TutorialStep.HasAllTutorialCompleted())
        return;
      this.needFollowCheck = true;
    }
  }

  public override void UpdateUI()
  {
    this.SetFontStyle((Enum) HomeBase.UI.LBL_NOTICE, (FontStyle) 2);
    this.UpdateUIOfTutorial();
    this.CheckBalloons();
    if (Object.op_Inequality((Object) this.questBalloon, (Object) null))
    {
      this.ResetTween(this.questBalloon);
      this.PlayTween(this.questBalloon, is_input_block: false);
      if (Object.op_Inequality((Object) this.storyBalloon, (Object) null))
      {
        ((Component) this.storyBalloon.parent).gameObject.SetActive(false);
        this.storyBalloon = (Transform) null;
      }
    }
    else if (Object.op_Inequality((Object) this.storyBalloon, (Object) null))
    {
      this.ResetTween(this.storyBalloon);
      this.PlayTween(this.storyBalloon, is_input_block: false);
    }
    if (Object.op_Inequality((Object) this.orderBalloon, (Object) null))
    {
      this.ResetTween(this.orderBalloon);
      this.PlayTween(this.orderBalloon, is_input_block: false);
    }
    this.UpdateEventBalloon();
    this.UpdateTicketNum();
    this.UpdateGuildRequest();
    this.UpdatePointShop();
    this.UpdateGiftboxNum();
    this.UpdateGuildBtn();
  }

  private void UpdateGuildBtn()
  {
  }

  private void UpdateClanBadge()
  {
    if (MonoBehaviourSingleton<GuildManager>.I.guilMemberList != null)
      this.SetActive(this.FindCtrl(this._transform, (Enum) HomeBase.UI.BTN_GUILD), (Enum) HomeBase.UI.SPR_BADGE, MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters != null && MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters.Count > 0);
    else
      this.SetActive(this.FindCtrl(this._transform, (Enum) HomeBase.UI.BTN_GUILD), (Enum) HomeBase.UI.SPR_BADGE, false);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.CHANGED_SCENE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      {
        if (!MonoBehaviourSingleton<UIManager>.I.mainChat.isOpen)
          MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
        MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
      }
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.bannerView, (Object) null) && !MonoBehaviourSingleton<UIManager>.I.bannerView.isOpen && TutorialStep.HasAllTutorialCompleted() && !HomeTutorialManager.ShouldRunGachaTutorial())
        MonoBehaviourSingleton<UIManager>.I.bannerView.Open();
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationButton, (Object) null) && TutorialStep.HasAllTutorialCompleted())
      {
        if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen && this.IsCurrentSectionHomeOrLounge())
          MonoBehaviourSingleton<UIManager>.I.invitationButton.Open();
        else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen)
          MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
      }
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_RALLY_INVITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationButton, (Object) null) && TutorialStep.HasAllTutorialCompleted())
      {
        if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen && this.IsCurrentSectionHomeOrLounge())
          MonoBehaviourSingleton<UIManager>.I.invitationButton.Open();
        else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen)
          MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
      }
    }
    else if ((flags & GameSection.NOTIFY_FLAG.RESET_DARK_MARKET) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.blackMarkeButton, (Object) null) && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.ResetMarketTime();
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_TASK_LIST) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<AchievementManager>.IsValid())
      {
        List<TaskInfo> taskInfos = MonoBehaviourSingleton<AchievementManager>.I.GetTaskInfos();
        int num = 0;
        for (int index = 0; index < taskInfos.Count; ++index)
        {
          if (taskInfos[index].status == 2)
            ++num;
        }
        HomeBase._taskBadgeNum = num;
        this.SetBadge(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), num, (SpriteAlignment) 3, 8, 8);
        this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
        this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
      }
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
      this.UpdateTicketNum();
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
      this.UpdateGuildRequest();
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM) != (GameSection.NOTIFY_FLAG) 0 || (flags & GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.UpdateGiftboxNum();
    if ((GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS & flags) != (GameSection.NOTIFY_FLAG) 0)
      this.OnNotifyUpdateUserStatus();
    if ((GameSection.NOTIFY_FLAG.TRANSITION_END & flags) != (GameSection.NOTIFY_FLAG) 0 && MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss, (Object) null))
    {
      MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.ClearAnnounce();
      if (MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.IsKnockDownRaidBossByEventItemCountList())
        MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.PlayKnockDown();
    }
    if ((GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE & flags) != (GameSection.NOTIFY_FLAG) 0)
      this.UpdateCommunityBadge();
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST_SOLD) != (GameSection.NOTIFY_FLAG) 0)
      this.UpdateTradingPostSoldNum();
    else if (TradingPostManager.IsNewTradingPostSold())
    {
      MonoBehaviourSingleton<TradingPostManager>.I.UpdateTradingPostSoldCount(1);
      this.UpdateTradingPostSoldNum();
    }
    base.OnNotify(flags);
  }

  public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) || !UserInfoManager.IsFinishTutorial())
      return;
    if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != (MainChat.NOTIFY_FLAG) 0)
      this.SetBadge((Enum) HomeBase.UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNum(), (SpriteAlignment) 1, -5, -29);
    if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != (MainChat.NOTIFY_FLAG) 0)
      ((Component) this.GetCtrl((Enum) HomeBase.UI.BTN_CHAT)).gameObject.SetActive(true);
    if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != (MainChat.NOTIFY_FLAG) 0)
      ((Component) this.GetCtrl((Enum) HomeBase.UI.BTN_CHAT)).gameObject.SetActive(false);
    if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) == (MainChat.NOTIFY_FLAG) 0)
      return;
    ((Component) this.GetCtrl((Enum) HomeBase.UI.BTN_CHAT)).gameObject.SetActive(false);
  }

  protected virtual void OnNotifyUpdateUserStatus()
  {
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level != this.prevLevel)
    {
      MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUp();
      this.prevLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
    }
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade != this.prevQuest)
    {
      this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
      this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
      this.prevQuest = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade;
    }
    this.CheckEventLock();
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) this.StartCoroutine(this.WaitInitializeManager());
    this.CreateSelfCharacter();
    yield return (object) this.StartCoroutine(this.LoadTutorialMessage());
    yield return (object) this.StartCoroutine(this.CreatePuniCon());
    yield return (object) this.StartCoroutine(this.SendHomeInfo());
    if (FieldRewardPool.HasSave())
    {
      FieldRewardPool fieldRewardPool = FieldRewardPool.LoadAndCreate();
      bool wait = true;
      Action<bool> call_back = (Action<bool>) (b => wait = false);
      fieldRewardPool.SendFieldDrop(call_back);
      while (wait)
        yield return (object) null;
    }
    yield return (object) this.StartCoroutine(this.WaitLoadHomeCharacters());
    yield return (object) this.StartCoroutine(this.DoTutorial());
    if (!Singleton<TutorialMessageTable>.IsValid() || !TutorialStep.HasChangeEquipCompleted() || TutorialStep.HasAllTutorialCompleted())
      this.transferNoticeNewDelivery = true;
    this.SetupValidLoginBonus();
    if (this.validLoginBonus)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.skipTrantisionEnd = true;
      this.waitEventBalloon = true;
    }
    this.SetIconAndBalloon();
    this.SetUpBonusTime();
    MonoBehaviourSingleton<GuildManager>.I.GetClanStat();
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
    {
      bool isWait = true;
      MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", (Action<ClanDetailModel.Param>) (result => isWait = false));
      while (isWait)
        yield return (object) null;
    }
    if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
    {
      bool wait_clan_donate_invite = true;
      MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList((Action<bool>) (guildSuccess => wait_clan_donate_invite = false), true);
      while (wait_clan_donate_invite)
        yield return (object) null;
    }
    base.Initialize();
  }

  private IEnumerator DoTutorial()
  {
    bool flag = HomeTutorialManager.DoesTutorial();
    if (flag)
    {
      this.homeTutorialManager = ((Component) this).gameObject.AddComponent<HomeTutorialManager>();
      if (!Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
      {
        if (flag)
        {
          this.homeTutorialManager.Setup();
          this.StartCoroutine(this.SetupArrow());
        }
        while (this.homeTutorialManager.IsLoading())
          yield return (object) null;
        HomeBase.OnTalkPamelaTutorial = true;
      }
    }
    else if (HomeTutorialManager.ShouldRunGachaTutorial() && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
    {
      this.homeTutorialManager = ((Component) this).gameObject.GetComponent<HomeTutorialManager>();
      if (Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
        this.homeTutorialManager = ((Component) this).gameObject.AddComponent<HomeTutorialManager>();
      if (!Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
      {
        this.homeTutorialManager.SetupGachaQuestTutorial();
        HomeBase.OnClickQuestForTutorial = true;
        while (this.homeTutorialManager.IsLoading())
          yield return (object) null;
      }
    }
    else if (HomeTutorialManager.ShouldRunQuestShadowTutorial() && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
    {
      this.homeTutorialManager = ((Component) this).gameObject.GetComponent<HomeTutorialManager>();
      if (Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
        this.homeTutorialManager = ((Component) this).gameObject.AddComponent<HomeTutorialManager>();
      if (!Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
      {
        this.homeTutorialManager.SetupGachaQuestTutorial();
        this.StartCoroutine(this.SetupArrowForQuest());
        HomeBase.OnClickQuestForTutorial = true;
        while (this.homeTutorialManager.IsLoading())
          yield return (object) null;
      }
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS)))
    {
      HomeBase.isFirstTimeDisplayTextTutorial = false;
      bool loadNeedBit = false;
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM))
        TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM, (Action<bool>) (b =>
        {
          HomeBase.isFirstTimeDisplayTextTutorial = true;
          loadNeedBit = true;
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_16_tutorial_end, "Tutorial");
          Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_16_tutorial_end.ToString()));
          MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_16_tutorial_end, "Tutorial");
        }));
      else
        loadNeedBit = true;
      while (!loadNeedBit)
        yield return (object) null;
      this.homeTutorialManager = ((Component) this).gameObject.AddComponent<HomeTutorialManager>();
      if (!Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
      {
        this.homeTutorialManager.Setup();
        while (this.homeTutorialManager.IsLoading())
          yield return (object) null;
      }
    }
  }

  private IEnumerator SetupArrow()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
    {
      "mdl_arrow_01"
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector(-4.28f, 1.66f, 4125f * (float) Math.PI / 887f);
    Vector3 vector3_2;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_2).\u002Ector(4f, 4f, 4f);
    this.mdlArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
    ResourceUtility.Realizes(loadedArrow.loadedObject, this.mdlArrow);
    this.mdlArrow.localScale = vector3_2;
    this.mdlArrow.position = vector3_1;
  }

  private IEnumerator SetupArrowForQuest()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
    {
      "mdl_arrow_01"
    });
    loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector(4f, 4f, 4f);
    Vector3 vector3_2;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_2).\u002Ector(3.2f, 2.8f, 14f);
    this.mdlArrowQuest = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
    ResourceUtility.Realizes(loadedArrow.loadedObject, this.mdlArrowQuest);
    this.mdlArrowQuest.localScale = vector3_1;
    this.mdlArrowQuest.position = vector3_2;
    this.homeTutorialManager = ((Component) this).gameObject.GetComponent<HomeTutorialManager>();
    if (Object.op_Equality((Object) this.homeTutorialManager, (Object) null))
      this.homeTutorialManager = ((Component) this).gameObject.AddComponent<HomeTutorialManager>();
    if (Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
    {
      this.homeTutorialManager.dialog.OpenAfterGacha2();
      this.homeTutorialManager.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 4U));
    }
  }

  private IEnumerator LoadTutorialMessage()
  {
    if (MonoBehaviourSingleton<UIManager>.IsValid() && !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null) && UserInfoManager.IsNeedsTutorialMessage())
    {
      bool loadingTutorialMessage = true;
      MonoBehaviourSingleton<UIManager>.I.LoadTutorialMessage((System.Action) (() => loadingTutorialMessage = false));
      while (loadingTutorialMessage)
        yield return (object) null;
    }
  }

  protected void CreateSelfCharacter()
  {
    this.iHomeManager.IHomePeople.CreateSelfCharacter(new Action<HomeStageAreaEvent>(this.OnNoticeAreaEvent));
  }

  protected IEnumerator WaitInitializeManager()
  {
    while (!this.iHomeManager.IsInitialized)
      yield return (object) null;
  }

  private void DestroyInGameTutorialManager()
  {
    InGameTutorialManager component = ((Component) MonoBehaviourSingleton<AppMain>.I).GetComponent<InGameTutorialManager>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    Object.Destroy((Object) component);
  }

  protected virtual IEnumerator SendHomeInfo()
  {
    bool wait = true;
    HomeBase._isWaitingLoginBonus = false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM))
    {
      if (!HomeBase._isReceiveLoginBonus)
      {
        Debug.LogWarning((object) "SendHomeInfo if _isReceiveLoginBonus false");
        MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo((Action<bool, bool, int>) ((result, acquireLoginBonus, taskBadgeNum) =>
        {
          HomeBase._acquireLoginBonus = acquireLoginBonus;
          HomeBase._taskBadgeNum = taskBadgeNum;
          this.SetBadge(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), HomeBase._taskBadgeNum, (SpriteAlignment) 1, 8, -8);
          this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
          this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
          if (HomeBase._acquireLoginBonus && MonoBehaviourSingleton<AccountManager>.IsValid())
          {
            MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus((Action<bool>) (_result =>
            {
              wait = false;
              if (!_result)
                return;
              HomeBase._isReceiveLoginBonus = true;
              Debug.LogWarning((object) "_isReceiveLoginBonus true");
              HomeBase._isWaitingLoginBonus = true;
            }));
          }
          else
          {
            wait = false;
            HomeBase._isReceiveLoginBonus = true;
          }
        }));
      }
      else if (HomeBase._isHomeInfoCached)
      {
        this.SetBadge(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), HomeBase._taskBadgeNum, (SpriteAlignment) 1, 8, -8);
        if (HomeBase._acquireLoginBonus && MonoBehaviourSingleton<AccountManager>.IsValid())
        {
          MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus((Action<bool>) (result =>
          {
            wait = false;
            MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo((Action<bool, bool, int>) ((b, acquireLoginBonus, taskBadgeNum) =>
            {
              HomeBase._acquireLoginBonus = acquireLoginBonus;
              HomeBase._taskBadgeNum = taskBadgeNum;
              this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
              this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
            }));
          }));
        }
        else
        {
          wait = false;
          MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo((Action<bool, bool, int>) ((result, acquireLoginBonus, taskBadgeNum) =>
          {
            HomeBase._isHomeInfoCached = true;
            HomeBase._acquireLoginBonus = acquireLoginBonus;
            HomeBase._taskBadgeNum = taskBadgeNum;
            this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
            this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
          }));
        }
      }
      else
        MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo((Action<bool, bool, int>) ((result, acquireLoginBonus, taskBadgeNum) =>
        {
          wait = false;
          HomeBase._isHomeInfoCached = true;
          HomeBase._acquireLoginBonus = acquireLoginBonus;
          HomeBase._taskBadgeNum = taskBadgeNum;
          this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
          this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
        }));
    }
    else
      MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo((Action<bool, bool, int>) ((result, acquireLoginBonus, taskBadgeNum) =>
      {
        HomeBase._acquireLoginBonus = acquireLoginBonus;
        HomeBase._taskBadgeNum = taskBadgeNum;
        this.SetActive(this.GetCtrl((Enum) HomeBase.UI.BTN_MISSION_GG), (Enum) HomeBase.UI.OBJ_GIFT, this.ShouldEnableGiftIcon());
        this.SetActive((Enum) HomeBase.UI.OBJ_MENU_GIFT_ON, this.ShouldEnableGiftIcon());
        if (HomeBase._acquireLoginBonus && MonoBehaviourSingleton<AccountManager>.IsValid())
          MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus((Action<bool>) (r =>
          {
            wait = false;
            HomeBase._isWaitingLoginBonus = true;
          }));
        else
          wait = false;
      }));
    while (wait)
      yield return (object) null;
  }

  protected IEnumerator WaitLoadHomeCharacters()
  {
    while (this.iHomeManager.IHomePeople.selfChara.isLoading || !this.iHomeManager.IHomePeople.isPeopleInitialized)
      yield return (object) null;
  }

  private IEnumerator CreatePuniCon()
  {
    LoadingQueue loadingQueue = (LoadingQueue) null;
    LoadObject lo_punicon = (LoadObject) null;
    if (HomeSelfCharacter.CTRL)
    {
      loadingQueue = new LoadingQueue((MonoBehaviour) this);
      lo_punicon = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGame", new string[1]
      {
        "PuniConManager"
      });
    }
    yield return (object) loadingQueue.Wait();
    if (lo_punicon != null)
      ResourceUtility.Realizes(lo_punicon.loadedObjects[0].obj, MonoBehaviourSingleton<UIManager>.I._transform, 5);
  }

  private void SetupValidLoginBonus()
  {
    if ((MonoBehaviourSingleton<AccountManager>.I.logInBonus == null || MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count == 0) && GameSaveData.instance.logInBonus != null && GameSaveData.instance.logInBonus.Count > 0)
      MonoBehaviourSingleton<AccountManager>.I.SetLoginBonusFromCache(GameSaveData.instance.logInBonus);
    bool flag1 = MonoBehaviourSingleton<AccountManager>.I.IsRecvLogInBonus && MonoBehaviourSingleton<AccountManager>.I.logInBonus != null && MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count > 0;
    bool flag2 = TutorialStep.HasDailyBonusUnlocked();
    bool flag3 = MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent();
    this.validLoginBonus = flag1 & flag2 && !flag3 && MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM) && !HomeBase.isFirstTimeDisplayTextTutorial;
  }

  protected virtual void SetIconAndBalloon()
  {
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
    {
      Transform transform1 = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/QUEST_ICON_POS");
      if (Object.op_Inequality((Object) transform1, (Object) null))
        this.questIconPos = transform1.position;
      Transform transform2 = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/ORDER_ICON_POS");
      if (Object.op_Inequality((Object) transform2, (Object) null))
        this.orderIconPos = transform2.position;
    }
    this.CheckBalloons();
    if (MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList == null || MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList.Count <= 0 || MonoBehaviourSingleton<GachaDecoManager>.IsValid())
      return;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<GachaDecoManager>();
  }

  private void CheckBalloons()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep <= 4)
      return;
    if (Object.op_Equality((Object) this.questBalloon, (Object) null))
    {
      if (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableNormalDeliveryNum() > 0)
        this.questBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(MonoBehaviourSingleton<DeliveryManager>.I.hasProgressDailyDelivery ? UI_Common.BALLOON_TYPE.COMPLETABLE_DAILY : UI_Common.BALLOON_TYPE.COMPLETABLE_NORMAL_L, this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT));
      else if (GameSaveData.instance.IsRecommendedDeliveryCheck())
        this.questBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(MonoBehaviourSingleton<DeliveryManager>.I.hasProgressDailyDelivery ? UI_Common.BALLOON_TYPE.NEW_DAILY : UI_Common.BALLOON_TYPE.NEW_NORMAL_L, this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT));
      else if (Object.op_Equality((Object) this.storyBalloon, (Object) null))
      {
        if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(new DELIVERY_TYPE[1]
        {
          DELIVERY_TYPE.STORY
        }))
          this.storyBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_L, this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT));
      }
    }
    if (!Object.op_Equality((Object) this.orderBalloon, (Object) null))
      return;
    if (GameSaveData.instance.IsRecommendedChallengeCheck())
    {
      this.orderBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_SHADOW_CHALLENGE, this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT));
    }
    else
    {
      if (!GameSaveData.instance.IsRecommendedOrderCheck())
        return;
      this.orderBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_R, this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT));
    }
  }

  protected bool CheckNeededGotoGacha()
  {
    if (string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.oncePurchaseGachaProductId))
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("MAIN_MENU_SHOP", (object) null),
      new EventData("FORCE_ONCE_PURCHASE_GACHA", (object) MonoBehaviourSingleton<UserInfoManager>.I.oncePurchaseGachaProductId)
    });
    return true;
  }

  protected bool CheckNeededOpenQuest()
  {
    if (HomeTutorialManager.ShouldRunGachaTutorial() || this.iHomeManager.IsJumpToGacha)
    {
      this.iHomeManager.IsJumpToGacha = false;
      this.DispatchEvent("GACHA_QUEST_COUNTER_AREA");
      return true;
    }
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest)
      MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = false;
    if (!MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest)
      return false;
    MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = false;
    this.DispatchEvent("GACHA_QUEST_COUNTER_AREA");
    return true;
  }

  protected void SetupLoginBonus()
  {
    if (this.validLoginBonus)
    {
      this.shouldFrameInNPC006 = true;
      this.CheckLoginBonusFirst();
    }
    else
    {
      HomeNPCCharacter homeNpcCharacter = this.iHomeManager.IHomePeople.GetHomeNPCCharacter(6);
      homeNpcCharacter.HideShadow();
      ((Component) homeNpcCharacter.loader.GetAnimator()).gameObject.AddComponent<HomeDragonRandomMove>().Reset();
    }
  }

  protected void SetupPointShop()
  {
    if (this.iHomeManager == null || !this.iHomeManager.IsPointShopOpen)
      return;
    int num = MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen ? 1 : 0;
  }

  private void SetUpBingo()
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid() || !MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist())
      return;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
    {
      Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/BINGO_ICON_POS");
      if (Object.op_Inequality((Object) transform, (Object) null))
        this.bingoIconPos = transform.position;
    }
    this.bingoBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateBingoBalloon(this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT));
    if (!Object.op_Inequality((Object) this.bingoBalloon, (Object) null))
      return;
    this.ResetTween(this.bingoBalloon);
    this.PlayTween(this.bingoBalloon, is_input_block: false);
  }

  private bool CheckOpenGacha()
  {
    string str = PlayerPrefs.GetString("gc");
    if (!string.IsNullOrEmpty(str))
    {
      PlayerPrefs.SetString("gc", "");
      switch (str)
      {
        case "magi":
          MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
          {
            new EventData("MAIN_MENU_SHOP", (object) null),
            new EventData("MAGI_GACHA", (object) null)
          });
          return true;
        case "behemoth":
          MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
          {
            new EventData("MAIN_MENU_SHOP", (object) null)
          });
          return true;
      }
    }
    return false;
  }

  private bool CheckNeedOpenUrl()
  {
    string str = PlayerPrefs.GetString("ur");
    if (string.IsNullOrEmpty(str))
      return false;
    PlayerPrefs.SetString("ur", "");
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
      MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
    Application.OpenURL(str);
    return true;
  }

  private bool CheckInvitedClanBySNS()
  {
    string s = PlayerPrefs.GetString("ic");
    if (!string.IsNullOrEmpty(s))
    {
      PlayerPrefs.SetString("ic", "");
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      EventData[] event_datas = new EventData[3]
      {
        new EventData("GUILD", (object) null),
        new EventData("SEARCH", (object) null),
        new EventData("INFO", (object) int.Parse(s))
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  private bool CheckJoinClanIngame()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.showJoinClanInGame)
    {
      EventData[] event_datas = new EventData[1]
      {
        new EventData("GUILD_MESSAGE", (object) null)
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        MonoBehaviourSingleton<UserInfoManager>.I.showJoinClanInGame = false;
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  private bool CheckInvitedPartyBySNS()
  {
    string str = PlayerPrefs.GetString("im");
    if (!string.IsNullOrEmpty(str))
    {
      MonoBehaviourSingleton<PartyManager>.I.InviteValue = str;
      PlayerPrefs.SetString("im", "");
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      EventData[] event_datas = new EventData[2]
      {
        new EventData("GACHA_QUEST_COUNTER", (object) null),
        new EventData("INVITED_ROOM", (object) null)
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  protected abstract bool CheckInvitedLoungeBySNS();

  private bool CheckMutualFollowBySNS()
  {
    string str = PlayerPrefs.GetString("fc");
    if (!string.IsNullOrEmpty(str))
    {
      MonoBehaviourSingleton<FriendManager>.I.MutualFollowValue = str;
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      EventData[] event_datas = new EventData[5]
      {
        new EventData("MUTUAL_FOLLOW", (object) null),
        new EventData("MAIN_MENU_MENU", (object) null),
        new EventData("FRIEND", (object) null),
        new EventData("FOLLOW_LIST", (object) null),
        new EventData("MUTUAL_FOLLOW_MESSAGE", (object) null)
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        PlayerPrefs.SetString("fc", "");
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  protected void CheckLoginBonusFirst()
  {
    GameSaveData.instance.logInBonus = (List<LoginBonus>) null;
    GameSaveData.Save();
    int bonusLimitedCount = MonoBehaviourSingleton<AccountManager>.I.logInBonusLimitedCount;
    if (this.limitedLoginBonus != null && this.limitedLoginBonus.Count > 0)
    {
      MonoBehaviourSingleton<AccountManager>.I.logInBonus.Clear();
      MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange((IEnumerable<LoginBonus>) this.limitedLoginBonus);
    }
    MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((Predicate<LoginBonus>) (x => x.priority == 0 && x.type != 0 || x.reward == null || x.reward.Count == 0));
    List<LoginBonus> all1 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((Predicate<LoginBonus>) (x => x.priority == 0 && x.type == 0));
    if (all1 != null && all1.Count > 0)
    {
      this.limitedLoginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((Predicate<LoginBonus>) (x => x.type != 0));
      MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((Predicate<LoginBonus>) (x => x.type != 0));
      this.DispatchEvent("LOGIN_BONUS");
    }
    else
    {
      Debug.LogWarning((object) ("limited_bonus_count : " + (object) bonusLimitedCount));
      if (bonusLimitedCount >= 3)
      {
        List<LoginBonus> all2 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((Predicate<LoginBonus>) (x => x.priority == 0));
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.Sort((Comparison<LoginBonus>) ((x, y) => y.priority - x.priority));
        for (int index = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count - 1; index >= 3; --index)
          MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAt(index);
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange((IEnumerable<LoginBonus>) all2);
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((Predicate<LoginBonus>) (x => x.type == 0));
        if (all1 != null && all1.Count > 0)
          MonoBehaviourSingleton<AccountManager>.I.logInBonus.Add(all1[0]);
        GameSection.StopEvent();
        this.DispatchEvent("LIMITED_LOGIN_BONUS");
      }
      else if (bonusLimitedCount == 2)
      {
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((Predicate<LoginBonus>) (x => x.type == 0));
        List<LoginBonus> all3 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((Predicate<LoginBonus>) (x => x.priority == 0));
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.Sort((Comparison<LoginBonus>) ((x, y) => y.priority - x.priority));
        for (int index = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count - 1; index >= 2; --index)
          MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAt(index);
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange((IEnumerable<LoginBonus>) all3);
        this.DispatchEvent("LIMITED_LOGIN_BONUS");
      }
      else if (bonusLimitedCount <= 1)
      {
        List<LoginBonus> all4 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((Predicate<LoginBonus>) (x => x.priority == 0 && x.type != 0));
        for (int index = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count - 1; index > 0; --index)
        {
          if (MonoBehaviourSingleton<AccountManager>.I.logInBonus[index].priority == 0 && MonoBehaviourSingleton<AccountManager>.I.logInBonus[index].type != 0)
            MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAt(index);
        }
        MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange((IEnumerable<LoginBonus>) all4);
        this.DispatchEvent("LIMITED_LOGIN_BONUS");
      }
      HomeBase._isWaitingLoginBonus = false;
    }
  }

  protected virtual void UpdateUIOfTutorial()
  {
    bool is_enable = !HomeTutorialManager.ShouldRunGachaTutorial();
    if (((!Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) || !TutorialStep.HasAllTutorialCompleted() ? 0 : (UserInfoManager.IsFinishTutorial() ? 1 : 0)) & (is_enable ? 1 : 0)) != 0)
      this.SetActive((Enum) HomeBase.UI.BTN_CHAT, !MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow());
    else
      this.SetActive((Enum) HomeBase.UI.BTN_CHAT, false);
    this.SetActive((Enum) HomeBase.UI.BTN_STORAGE, TutorialStep.HasAllTutorialCompleted() & is_enable);
    MonoBehaviourSingleton<UIManager>.I.mainStatus.SetMenuButtonEnable(TutorialStep.HasAllTutorialCompleted() & is_enable);
    MonoBehaviourSingleton<UIManager>.I.mainMenu.SetMenuButtonEnable(is_enable);
  }

  protected bool ShouldEnableMissionButton()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade > 0 && !HomeTutorialManager.ShouldRunGachaTutorial();
  }

  protected bool ShouldEnableGiftIcon() => HomeBase._taskBadgeNum > 0;

  private void UpdateTicketNum()
  {
    if (!MonoBehaviourSingleton<InventoryManager>.IsValid())
      return;
    this.SetBadge((Enum) HomeBase.UI.BTN_TICKET, MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => x.tableData.type == ITEM_TYPE.TICKET)), (SpriteAlignment) 1, 8, -8);
  }

  private void UpdateGiftboxNum()
  {
    this.SetBadge((Enum) HomeBase.UI.BTN_GIFTBOX, MonoBehaviourSingleton<PresentManager>.I.presentNum, (SpriteAlignment) 1, 8, -8);
  }

  private void UpdateTradingPostSoldNum()
  {
    this.SetBadge((Enum) HomeBase.UI.BTN_TRADING_POST, MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSoldNum, (SpriteAlignment) 1, 8, -8);
  }

  private void UpdateGuildRequest()
  {
    this.SetActive((Enum) HomeBase.UI.BTN_GUILD_REQUEST, false);
    if (!MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      return;
    int num = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Where<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.questId > 0)).Where<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.GetQuestRemainTime().TotalSeconds < 0.0)).Count<GuildRequestItem>();
    this.SetBadge(this.GetCtrl((Enum) HomeBase.UI.BTN_GUILD_REQUEST), num, (SpriteAlignment) 3, -8, -8);
  }

  private void UpdatePointShop()
  {
    if (this.iHomeManager == null)
      return;
    int num1 = this.iHomeManager.IsPointShopOpen ? 1 : 0;
    int num2 = MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen ? 1 : 0;
    this.SetActive((Enum) HomeBase.UI.BTN_POINT_SHOP, false);
  }

  protected virtual void LateUpdate()
  {
    if (this.isInitialized && MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      MonoBehaviourSingleton<ClanMatchingManager>.I.UpdateUnreadMessage();
    this.UpdateNotice();
    this.UpdateBalloon();
    this.DirectDelivery();
    if (MonoBehaviourSingleton<GachaDecoManager>.IsValid())
      MonoBehaviourSingleton<GachaDecoManager>.I.SetVisible(this.IsValidGachaDeco());
    if (!this.IsValidDispatchEventInUpdate() || this.CheckShowHomeBanner())
      return;
    this.UpdateBonusTime();
  }

  private void DirectDelivery()
  {
    if (!this.IsCurrentSectionHomeOrLounge() || MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    if (TutorialStep.HasChangeEquipCompleted() && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.CLAIM_REWARD) && this.executeTutorialClaimReward)
    {
      List<LoginBonus> logInBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus;
      if (logInBonus == null || logInBonus.Count == 0 || logInBonus[0].priority <= 0)
      {
        this.TutorialClaimReward();
        return;
      }
    }
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.CLAIM_REWARD))
      return;
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
    {
      if (this.triger_tutorial_gacha_1)
        return;
      this.DispatchEvent("HOME_TUTORIAL");
    }
    else
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
        return;
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
      {
        if (this.triger_tutorial_force_item)
          return;
        this.DispatchEvent("HOME_TUTORIAL");
      }
      else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.DONE_CHANGE_WEAPON) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
      {
        if (this.triger_tutorial_change_item)
          return;
        this.DispatchEvent("HOME_TUTORIAL");
      }
      else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
      {
        if (this.triger_tutorial_gacha_2)
          return;
        this.DispatchEvent("HOME_TUTORIAL");
      }
      else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN))
      {
        if (this.triger_tutorial_upgrade)
          return;
        this.DispatchEvent("HOME_TUTORIAL");
      }
      else
      {
        if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
          return;
        if (this.executeTutorialStep6)
          this.TutorialStep_6();
        else if (this.executeTutorialEnd)
        {
          this.TutorialStep_END();
        }
        else
        {
          if (!MonoBehaviourSingleton<DeliveryManager>.IsValid())
            return;
          if (!this.transferNoticeNewDelivery)
          {
            this.transferNoticeNewDelivery = true;
            int[] ary = MonoBehaviourSingleton<DeliveryManager>.I.GetRecvStoryDelivery();
            if (ary != null && ary.Length != 0)
            {
              int i = 0;
              for (int length = ary.Length; i < length; i++)
              {
                if (MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.FindIndex((Predicate<int>) (id => id == ary[i])) == -1)
                  MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Add(ary[i]);
              }
            }
          }
          if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
          {
            if (!TutorialStep.HasChangeEquipCompleted() || TutorialStep.isSendFirstRewardComplete)
              return;
            int event_data = MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene[0];
            if (event_data == 0)
              return;
            MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.RemoveAt(0);
            this.DispatchEvent("NOTICE_NEW_DELIVERY", (object) event_data);
          }
          else if (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() != 0U)
          {
            MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = true;
            MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
            {
              new EventData("QUEST_COUNTER", (object) null),
              new EventData("SELECT_DELIVERY", (object) MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery())
            });
          }
          else if (MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() != null)
          {
            EventData[] clearDeliveryEvent = this.CreateAutoClearDeliveryEvent(MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData());
            MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
            MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(clearDeliveryEvent);
          }
          else
          {
            if (!TutorialStep.HasAllTutorialCompleted() || !this.isOpen || this.sendTutorialTrigger)
              return;
            this.sendTutorialTrigger = true;
            this.DispatchEvent("HOME_TUTORIAL");
          }
        }
      }
    }
  }

  private EventData[] CreateAutoClearDeliveryEvent(Network.EventData data)
  {
    EventData[] clearDeliveryEvent;
    switch ((EVENT_TYPE) data.eventType)
    {
      case EVENT_TYPE.ARENA:
        clearDeliveryEvent = new EventData[2]
        {
          new EventData("EVENT_COUNTER", (object) null),
          new EventData("SELECT_ARENA", (object) data)
        };
        break;
      case EVENT_TYPE.BINGO:
        clearDeliveryEvent = new EventData[1]
        {
          new EventData("BINGO", (object) true)
        };
        break;
      case EVENT_TYPE.TRIAL:
        clearDeliveryEvent = new EventData[2]
        {
          new EventData("EVENT_COUNTER", (object) null),
          new EventData("SELECT_TRIAL", (object) data)
        };
        break;
      default:
        clearDeliveryEvent = new EventData[2]
        {
          new EventData("EVENT_COUNTER", (object) null),
          new EventData("SELECT", (object) data)
        };
        break;
    }
    return clearDeliveryEvent;
  }

  private bool CheckOnceShowObjects()
  {
    if (MonoBehaviourSingleton<GlobalSettingsManager>.I.enableBlackMarketBanner && !MonoBehaviourSingleton<UserInfoManager>.I.showBlackMarketBanner)
    {
      MonoBehaviourSingleton<UserInfoManager>.I.showBlackMarketBanner = true;
      this.DispatchEvent("BLACK_MARKET_BANNER");
      return true;
    }
    if (MonoBehaviourSingleton<GlobalSettingsManager>.I.enableFortuneWheelBanner && !MonoBehaviourSingleton<UserInfoManager>.I.showFortuneWheel)
    {
      MonoBehaviourSingleton<UserInfoManager>.I.showFortuneWheel = true;
      this.DispatchEvent("FORTUNE_WHEEL_BANNER");
      return true;
    }
    if (GameSaveData.instance.showHomeOneTimesOfferSSDay != DateTime.UtcNow.Day && MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
    {
      GameSaveData.instance.showHomeOneTimesOfferSSDay = DateTime.UtcNow.Day;
      this.DispatchEvent("BANNER_ONETIMESOFFERSS");
      return true;
    }
    if (MonoBehaviourSingleton<ShopManager>.IsValid() && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList != null && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups != null && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count > 0)
    {
      List<SkuAdsData> skuPopups = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups;
      int index = 0;
      for (int count = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count; index < count; ++index)
      {
        if (!GameSaveData.instance.showIAPAdsPop.Contains(skuPopups[index].productId) && !GameSaveData.instance.iAPBundleBought.Contains(skuPopups[index].productId))
        {
          GameSaveData.instance.showIAPAdsPop = $"{GameSaveData.instance.showIAPAdsPop}/{skuPopups[index].productId}";
          this.DispatchEvent("IAP_ADD_POP", (object) skuPopups[index].productId);
          return true;
        }
      }
    }
    if (GameSaveData.instance.showHomeBannerInviteDay != DateTime.UtcNow.Day)
    {
      if (GameSaveData.instance.spent25Gems >= 25)
      {
        GameSaveData.instance.spent25Gems = 0;
        if (GameSaveData.instance.spentSummonTicket >= 10)
          GameSaveData.instance.spentSummonTicket = 0;
        GameSaveData.instance.showHomeBannerInviteDay = DateTime.UtcNow.Day;
        if (Random.Range(1, 3) == 1)
          this.DispatchEvent("BANNER_INVITE");
        else
          this.DispatchEvent("BANNER_GUARANTEDSS");
        return true;
      }
      if (GameSaveData.instance.spentSummonTicket >= 10)
      {
        GameSaveData.instance.spentSummonTicket = 0;
        if (GameSaveData.instance.spent25Gems >= 25)
          GameSaveData.instance.spent25Gems = 0;
        GameSaveData.instance.showHomeBannerInviteDay = DateTime.UtcNow.Day;
        if (Random.Range(1, 3) == 1)
          this.DispatchEvent("BANNER_INVITE");
        else
          this.DispatchEvent("BANNER_GUARANTEDSS");
        return true;
      }
    }
    if (GameSaveData.instance.showHomeBannerOfferDay != DateTime.UtcNow.Day && MonoBehaviourSingleton<ShopManager>.I.isNeedShowBundleOffer())
    {
      GameSaveData.instance.showHomeBannerOfferDay = DateTime.UtcNow.Day;
      MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = false;
      this.DispatchEvent("BANNER_OFFER");
      return true;
    }
    if (this.needFollowCheck)
    {
      this.needFollowCheck = false;
      if (this.CheckMutualFollowBySNS())
        return true;
    }
    if (this.needCountdown && Singleton<CountdownTable>.IsValid())
    {
      this.needCountdown = false;
      CountdownTable.CountdownData countdownData = Singleton<CountdownTable>.I.GetCountdownData(TimeManager.GetNow());
      int num = PlayerPrefs.GetInt("COUNTDOWN_SHOWED_REMAIN", -1);
      if (countdownData != null && countdownData.imageID != num)
      {
        this.DispatchEvent("COUNTDOWN", (object) countdownData.imageID);
        return true;
      }
    }
    if (MonoBehaviourSingleton<UserInfoManager>.I.needOpenNewsPage && TutorialStep.HasAllTutorialCompleted() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
    {
      MonoBehaviourSingleton<UserInfoManager>.I.OnOpenNewsPage();
      MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
      return true;
    }
    if (!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.alertMessage))
    {
      this.DispatchEvent("ALERT_MESSAGE");
      return true;
    }
    if (this.needCheckNotifyQuestRemain)
    {
      this.CheckNotifyQuestRemainTime();
      this.needCheckNotifyQuestRemain = false;
      return true;
    }
    if (this.needShowDailyDelivery)
    {
      if (GameSaveData.instance.IsRecommendedDailyDeliveryCheckAtHome())
      {
        this.DispatchEvent("TO_QUEST", (object) "DAILY");
        GameSaveData.instance.recommendedDailyDeliveryCheckAtHome = 0;
      }
      this.needShowDailyDelivery = false;
      return true;
    }
    if (this.needShowAppReviewAppeal)
    {
      if (!GameSaveData.instance.ratingPopupHaveShow && GameSaveData.instance.happyTimeForRating)
      {
        this.DispatchEvent("OPEN_REVIEW_DIALOG");
        GameSaveData.instance.ratingPopupHaveShow = true;
      }
      this.needShowAppReviewAppeal = false;
      return true;
    }
    if (this.needShowShadowChallengeFirst)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.isShadowChallengeFirst)
        this.DispatchEvent("OPEN_SHADOW_CHALLENGE");
      this.needShowShadowChallengeFirst = false;
      return true;
    }
    if (GameSaveData.instance.showUnlockQuestEvent)
    {
      this.DispatchEvent("QUEST_UNLOCK");
      GameSaveData.instance.showUnlockQuestEvent = false;
      return true;
    }
    if (MonoBehaviourSingleton<UserInfoManager>.I.needBlackMarketNotifi)
    {
      MonoBehaviourSingleton<UserInfoManager>.I.needBlackMarketNotifi = false;
      MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37U), "");
    }
    return false;
  }

  private bool IsValidDispatchEventInUpdate()
  {
    if (!HomeSelfCharacter.CTRL || this.executeTutorialEnd || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || !TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || !this.IsCurrentSectionHomeOrLounge() || MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST)))
      return false;
    if (!HomeBase._isWaitingLoginBonus)
      return true;
    Debug.LogWarning((object) "_isWaitingLoginBonus");
    return false;
  }

  private bool IsValidGachaDeco()
  {
    return HomeSelfCharacter.CTRL && !this.executeTutorialEnd && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_END);
  }

  private bool IsCurrentSectionHomeOrLounge()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop";
  }

  private bool IsCurrentSectionGuild()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "GuildTop";
  }

  protected override void OnOpen()
  {
    this.InitializeChat();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.bannerView, (Object) null) && TutorialStep.HasAllTutorialCompleted() && !HomeTutorialManager.ShouldRunGachaTutorial())
      MonoBehaviourSingleton<UIManager>.I.bannerView.Open();
    if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
      MonoBehaviourSingleton<UIManager>.I.invitationButton.Open();
    if (this.shouldFrameInNPC006)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.shouldDispAdvancedOffer())
        this.RequestEvent("ADVANCED_OFFER");
      this.FrameInNPC006();
    }
    if (!TutorialStep.HasAllTutorialCompleted())
      return;
    MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Open();
    if (!GameSaveData.instance.canShowWheelFortune)
      return;
    MonoBehaviourSingleton<UIManager>.I.fortuneWheelButton.Open();
  }

  protected override void OnCloseStart()
  {
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.bannerView, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.bannerView.Close();
    MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Close();
    MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
    MonoBehaviourSingleton<UIManager>.I.fortuneWheelButton.Close();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (MonoBehaviourSingleton<PuniConManager>.IsValid())
      Object.Destroy((Object) ((Component) MonoBehaviourSingleton<PuniConManager>.I).gameObject);
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.mainChat.RemoveObserver((UIBehaviour) this);
    if (!MonoBehaviourSingleton<GachaDecoManager>.IsValid())
      return;
    Object.Destroy((Object) MonoBehaviourSingleton<GachaDecoManager>.I);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE | GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_OVER | GameSection.NOTIFY_FLAG.UPDATE_TASK_LIST;
  }

  private void UpdateBalloon()
  {
    if (Object.op_Inequality((Object) this.questBalloon, (Object) null))
    {
      if (TutorialStep.HasAllTutorialCompleted() && (GameSaveData.instance.IsRecommendedDeliveryCheck() || MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableNormalDeliveryNum() > 0))
      {
        this.SetBalloonPosition(this.questBalloon, this.questIconPos);
      }
      else
      {
        ((Component) this.questBalloon.parent).gameObject.SetActive(false);
        this.questBalloon = (Transform) null;
        this.RefreshUI();
      }
    }
    else if (Object.op_Inequality((Object) this.storyBalloon, (Object) null))
    {
      if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(new DELIVERY_TYPE[1]
      {
        DELIVERY_TYPE.STORY
      }))
      {
        this.SetBalloonPosition(this.storyBalloon, this.questIconPos);
      }
      else
      {
        ((Component) this.storyBalloon.parent).gameObject.SetActive(false);
        this.storyBalloon = (Transform) null;
        this.RefreshUI();
      }
    }
    if (Object.op_Inequality((Object) this.orderBalloon, (Object) null))
    {
      if (GameSaveData.instance.IsRecommendedChallengeCheck())
        this.SetBalloonPosition(this.orderBalloon, this.orderIconPos);
      else if (GameSaveData.instance.IsRecommendedOrderCheck())
      {
        this.SetBalloonPosition(this.orderBalloon, this.orderIconPos);
      }
      else
      {
        ((Component) this.orderBalloon.parent).gameObject.SetActive(false);
        this.orderBalloon = (Transform) null;
      }
    }
    if (Object.op_Inequality((Object) this.eventBalloon, (Object) null))
    {
      if (this.HasNotCheckedEvent())
      {
        this.SetBalloonPosition(this.eventBalloon, this.eventIconPos);
      }
      else
      {
        ((Component) this.eventBalloon.parent).gameObject.SetActive(false);
        this.eventBalloon = (Transform) null;
      }
    }
    if (Object.op_Inequality((Object) this.pointShopBalloon, (Object) null))
    {
      if (this.iHomeManager != null && this.iHomeManager.IsPointShopOpen)
      {
        this.SetBalloonPosition(this.pointShopBalloon, this.pointShopIconPos);
      }
      else
      {
        ((Component) this.pointShopBalloon.parent).gameObject.SetActive(false);
        this.pointShopBalloon = (Transform) null;
      }
    }
    if (!Object.op_Inequality((Object) this.bingoBalloon, (Object) null))
      return;
    if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist())
    {
      this.SetBalloonPosition(this.bingoBalloon, this.bingoIconPos);
    }
    else
    {
      ((Component) this.bingoBalloon.parent).gameObject.SetActive(false);
      this.bingoBalloon = (Transform) null;
    }
  }

  protected void SetBalloonPosition(Transform balloon, Vector3 iconPos)
  {
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(iconPos));
    worldPoint.z = (double) worldPoint.z >= 0.0 ? 0.0f : -100f;
    balloon.position = worldPoint;
  }

  private void UpdateEventBalloon()
  {
    if (!TutorialStep.HasAllTutorialCompleted() || this.waitEventBalloon || !this.HasNotCheckedEvent())
      return;
    if (this.currentEventBalloonType != UI_Common.EVENT_BALLOON_TYPE.NONE)
    {
      if (this.currentEventBalloonType == this.GetEventBalloonType())
        return;
      ((Component) this.eventBalloon.parent).gameObject.SetActive(false);
      this.eventBalloon = (Transform) null;
    }
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
    {
      Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/EVENT_ICON_POS");
      if (Object.op_Inequality((Object) transform, (Object) null))
        this.eventIconPos = transform.position;
    }
    this.currentEventBalloonType = this.GetEventBalloonType();
    this.eventBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateEventBalloon(this.GetCtrl((Enum) HomeBase.UI.OBJ_BALOON_ROOT), this.currentEventBalloonType);
    if (!Object.op_Inequality((Object) this.eventBalloon, (Object) null))
      return;
    this.ResetTween(this.eventBalloon);
    this.PlayTween(this.eventBalloon, is_input_block: false);
  }

  private UI_Common.EVENT_BALLOON_TYPE GetEventBalloonType()
  {
    return MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum() <= 0 ? UI_Common.EVENT_BALLOON_TYPE.NEW : UI_Common.EVENT_BALLOON_TYPE.COMPLETABLE;
  }

  private bool HasNotCheckedEvent()
  {
    List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
    if (eventList != null && eventList.Count > 0)
    {
      int index = 0;
      for (int count = eventList.Count; index < count; ++index)
      {
        if (!eventList[index].readPrologueStory)
          return true;
      }
    }
    return MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum() > 0;
  }

  private void SetupNotice()
  {
    this.noticeTransform = this.GetCtrl((Enum) HomeBase.UI.OBJ_NOTICE);
    this.noticeObject = ((Component) this.noticeTransform).gameObject;
    this.noticeTween = this.GetComponent<TweenAlpha>((Enum) HomeBase.UI.OBJ_NOTICE);
    this.SetActive((Enum) HomeBase.UI.OBJ_NOTICE, false);
  }

  private void SetUpBonusTime()
  {
    Transform ctrl = this.GetCtrl((Enum) HomeBase.UI.OBJ_BONUS_TIME_ROOT);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    this.bonusTime = ((Component) ctrl).gameObject.AddComponent<HomeTopBonusTime>();
    this.bonusTime.InitUI();
    this.bonusTime.SetUp();
  }

  protected void OnNoticeAreaEvent(HomeStageAreaEvent area_event)
  {
    if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null))
      return;
    this.noticeEventTo = area_event;
  }

  private void UpdateNotice()
  {
    if (Object.op_Equality((Object) this.noticeTransform, (Object) null))
      return;
    this.UpdateNoticeStatus();
    if (!this.noticeObject.activeSelf)
      return;
    this.UpdateNoticePosition();
  }

  private void UpdateNoticeStatus()
  {
    if (Object.op_Equality((Object) this.noticeEvent, (Object) this.noticeEventTo) || ((Behaviour) this.noticeTween).enabled)
      return;
    if ((double) this.noticeTween.value != 0.0)
      this.noticeTween.PlayReverse();
    else if (Object.op_Equality((Object) this.noticeEventTo, (Object) null))
    {
      this.noticeObject.SetActive(false);
      this.noticeEvent = this.noticeEventTo;
    }
    else if (!this.IsShowNoticeByArena() && !(this is LoungeTop))
    {
      this.noticeObject.SetActive(false);
      this.noticeEvent = this.noticeEventTo;
    }
    else
    {
      this.SetLabelText((Enum) HomeBase.UI.LBL_NOTICE, this.sectionData.GetText(this.noticeEventTo.eventName));
      this.noticePos = this.noticeEventTo._transform.position;
      this.noticePos.y += this.noticeEventTo.noticeViewHeight;
      this.UpdateNoticeLock();
      if (!string.IsNullOrEmpty(this.noticeEventTo.noticeButtonName))
      {
        this.SetActive((Enum) HomeBase.UI.OBJ_BUTTON_NOTICE, true);
        this.SetActive((Enum) HomeBase.UI.OBJ_NORMAL_NOTICE, false);
        this.SetActiveAreaEventButton(this.noticeEventTo.noticeButtonName, true);
      }
      else
      {
        this.SetActive((Enum) HomeBase.UI.OBJ_BUTTON_NOTICE, false);
        this.SetActive((Enum) HomeBase.UI.OBJ_NORMAL_NOTICE, true);
      }
      this.noticeObject.SetActive(true);
      this.noticeTween.PlayForward();
      this.noticeEvent = this.noticeEventTo;
    }
  }

  private bool IsShowNoticeByArena()
  {
    return !this.noticeEventTo.eventName.Contains("ARENA_LIST") || MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen;
  }

  private void UpdateNoticeLock()
  {
    this.SetActive((Enum) HomeBase.UI.OBJ_NOTICE_LOCK, false);
    if (!this.noticeEventTo.eventName.Contains("ARENA_LIST") || (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50 || !MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen)
      return;
    this.SetActive((Enum) HomeBase.UI.OBJ_NOTICE_LOCK, true);
    this.SetLabelText((Enum) HomeBase.UI.LBL_NOTICE_LOCK, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1U, (object) 50) + "で解放");
  }

  private void UpdateNoticePosition()
  {
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(this.noticePos));
    worldPoint.z = (double) worldPoint.z >= 0.0 ? 0.0f : -100f;
    this.noticeTransform.position = worldPoint;
  }

  protected virtual void SetActiveAreaEventButton(string btnName, bool active)
  {
  }

  private void CheckNotifyQuestRemainTime()
  {
    if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() == 0)
      return;
    TimeSpan minRemainTime = TimeSpan.MaxValue;
    List<ulong> ids = new List<ulong>();
    MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (item =>
    {
      foreach (double remainTime in item.remainTimes)
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(remainTime);
        if (minRemainTime.CompareTo(timeSpan) == 1)
        {
          minRemainTime = timeSpan;
          ids.Clear();
          ids.Add(item.uniqueID);
        }
        else if (minRemainTime.CompareTo(timeSpan) == 0)
          ids.Add(item.uniqueID);
      }
    }));
    int remainDayThreshold = int.MaxValue;
    foreach (int num in GameDefine.NOTIFY_QUEST_REMAIN_DAY)
    {
      if (minRemainTime.TotalDays < (double) num && num < remainDayThreshold)
        remainDayThreshold = num;
    }
    if (remainDayThreshold == int.MaxValue)
      GameSaveData.instance.updateLastNotifyQuestRemainTime(remainDayThreshold, ids);
    else if (GameSaveData.instance.isIncludeNotifyQuestID(ids))
    {
      if (remainDayThreshold == GameSaveData.instance.lastRemainDayThreshold)
      {
        GameSaveData.instance.updateLastNotifyQuestRemainTime(remainDayThreshold, ids);
      }
      else
      {
        this.DispatchEvent("NOTICE_QUEST_REMAIN", (object) new object[1]
        {
          (object) this.GetRemainText(minRemainTime)
        });
        GameSaveData.instance.updateLastNotifyQuestRemainTime(remainDayThreshold, ids);
      }
    }
    else
    {
      this.DispatchEvent("NOTICE_QUEST_REMAIN", (object) new object[1]
      {
        (object) this.GetRemainText(minRemainTime)
      });
      GameSaveData.instance.updateLastNotifyQuestRemainTime(remainDayThreshold, ids);
    }
  }

  private string GetRemainText(TimeSpan minRemainTime)
  {
    return minRemainTime.TotalDays > 1.0 ? string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 9U), (object) minRemainTime.Days) : string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10U), (object) minRemainTime.Hours);
  }

  protected virtual void FrameInNPC006()
  {
    this.npc06Info = this.iHomeManager.IHomePeople.GetHomeNPCCharacter(6);
    this.shouldFrameInNPC006 = false;
    this.StartCoroutine(this._FrameInNPC006());
  }

  private IEnumerator _FrameInNPC006()
  {
    PlayerAnimCtrl animCtrl = ((Component) this.npc06Info.loader.GetAnimator()).GetComponent<PlayerAnimCtrl>();
    PLCA defaultAnim = animCtrl.defaultAnim;
    AnimatorCullingMode cullingMode = animCtrl.animator.cullingMode;
    while (this.npc06Info.IsLeaveState())
      yield return (object) null;
    Transform transform = Utility.Find(this.npc06Info._transform, "Move");
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
    {
      Transform child = transform.GetChild(num);
      if (((Object) child).name.StartsWith("LIB"))
      {
        Object.Destroy((Object) ((Component) child).gameObject);
        break;
      }
    }
    ((Component) this.npc06Info).gameObject.SetActive(true);
    this.npc06Info.PushOutControll();
    this.npc06Info._transform.localPosition = this.npc06Info.npcInfo.GetSituation().pos;
    this.npc06Info._transform.localEulerAngles = new Vector3(0.0f, this.npc06Info.npcInfo.GetSituation().rot, 0.0f);
    animCtrl.animator.cullingMode = (AnimatorCullingMode) 0;
    animCtrl.Play(PLCA.EVENT_MOVE, true);
    bool wait = true;
    Action<PlayerAnimCtrl, PLCA> action = (Action<PlayerAnimCtrl, PLCA>) ((pac, plca) =>
    {
      if (plca != PLCA.EVENT_MOVE)
        return;
      wait = false;
    });
    Action<PlayerAnimCtrl, PLCA> origOnEnd = animCtrl.onEnd;
    animCtrl.onEnd = action;
    while (wait)
      yield return (object) null;
    animCtrl.onEnd = origOnEnd;
    animCtrl.animator.cullingMode = cullingMode;
    animCtrl.defaultAnim = defaultAnim;
    animCtrl.Play(PLCA.EVENT_IDLE);
    this.npc06Info.PopState();
    ((Component) this.npc06Info.loader.GetAnimator()).gameObject.AddComponent<HomeDragonRandomMove>().Reset();
    this.waitEventBalloon = false;
    this.UpdateEventBalloon();
  }

  protected virtual void InitializeChat()
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
    MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver((UIBehaviour) this);
    UIButton component = ((Component) this.GetCtrl((Enum) HomeBase.UI.BTN_CHAT)).GetComponent<UIButton>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.onClick.Clear();
    this.AddChatClickDelegate(component);
  }

  protected abstract void AddChatClickDelegate(UIButton button);

  protected bool StopEventUntilTheAllTutorialCompleted()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() || TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && !Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null) && (!Object.op_Inequality((Object) this.homeTutorialManager, (Object) null) || !HomeTutorialManager.ShouldRunGachaTutorial()) && (!Object.op_Inequality((Object) this.homeTutorialManager, (Object) null) || !HomeTutorialManager.ShouldRunQuestShadowTutorial()))
      return false;
    GameSection.StopEvent();
    return true;
  }

  private void TutorialStep_6()
  {
    if (!TutorialStep.IsPlayingStudioTutorial())
      return;
    this.executeTutorialStep6 = false;
    this.DispatchEvent("TUTORIAL_STEP_6");
  }

  private void TutorialStep_END()
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return;
    this.executeTutorialEnd = false;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialEnd", (System.Action) (() => Protocol.Force((System.Action) (() => MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep((Action<bool>) (b =>
    {
      MonoBehaviourSingleton<UIManager>.I.mainStatus.SetMenuButtonEnable(true);
      this.RefreshUI();
      if (!MonoBehaviourSingleton<QuestManager>.I.ExistsExploreEvent())
        return;
      Protocol.Force((System.Action) (() => MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialBit(TUTORIAL_MENU_BIT.EXPLORE)));
    }))))));
  }

  protected void UpdateCommunityBadge()
  {
    Transform ctrl = this.GetCtrl((Enum) HomeBase.UI.BTN_COMMUNITY);
    if (Object.op_Equality((Object) ctrl, (Object) null) || !((Component) ctrl).gameObject.activeSelf)
      return;
    int num = MonoBehaviourSingleton<UserInfoManager>.I.clanRequestNum;
    if (num < 0)
      num = 0;
    this.SetBadge(ctrl, num, (SpriteAlignment) 3, -8, -8);
  }

  private void TutorialClaimReward()
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return;
    this.executeTutorialClaimReward = false;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "ClaimReward");
  }

  protected abstract void CheckEventLock();

  private void OnQuery_TRADING_POST()
  {
    if (TradingPostManager.IsTradingEnable())
      return;
    TradingPostManager.ShowUnavailableDialog();
  }

  private void OnCloseDialog_TradingPostTop()
  {
    this.UpdateTradingPostSoldNum();
    this.UpdateUI();
  }

  private void OnQuery_COMPLETE_READ_STORY()
  {
    int eventData = (int) GameSection.GetEventData();
    GameSection.StayEvent();
    MonoBehaviourSingleton<DeliveryManager>.I.SendReadStoryRead(eventData, (Action<bool, Error>) ((is_success, recv_reward) => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_TO_GIFTBOX()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_TUTORIAL_STEP_6()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep((Action<bool>) (b =>
    {
      GameSection.ResumeEvent(false);
      string section_name = "TutorialStep6_1";
      if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.EQUIP_CREATE_07))
        section_name = "TutorialStep6_1_2";
      MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", section_name, (System.Action) (() =>
      {
        TutorialStep.isSendFirstRewardComplete = false;
        MonoBehaviourSingleton<UIManager>.I.UpdateMainUI();
        MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateMainMenu();
        this.RefreshUI();
        MonoBehaviourSingleton<UIManager>.I.tutorialMessage.UpdateFocusCursol();
      }));
    }));
  }

  private void OnQuery_FIELD() => this._OnQuery_FIELD(false);

  private void OnQuery_TO_FIELD()
  {
    if (this.StopEventUntilTheAllTutorialCompleted())
      return;
    this.OnQuery_FIELD();
  }

  private void OnQuery_SHOP() => this.StopEventUntilTheAllTutorialCompleted();

  private void OnQuery_TO_SHOP() => this.StopEventUntilTheAllTutorialCompleted();

  private void OnQuery_STATUS() => this.StopEventUntilTheAllTutorialCompleted();

  private void OnQuery_TO_STATUS() => this.StopEventUntilTheAllTutorialCompleted();

  private void OnQuery_FRIEND() => this.StopEventUntilTheAllTutorialCompleted();

  private void OnQuery_HOME_FRIENDS() => this.StopEventUntilTheAllTutorialCompleted();

  private void OnQuery_QUEST_COUNTER_AREA()
  {
    this.fromQuestCounterAreaEvent = true;
    GameSection.ChangeEvent("QUEST_COUNTER");
    this.OnQuery_QUEST_COUNTER();
  }

  private void OnQuery_BINGO()
  {
    if (!GameSceneManager.isAutoEventSkip)
      SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
    GameSection.StayEvent();
    MonoBehaviourSingleton<QuestManager>.I.SendGetBingoEventList((Action<bool>) (b =>
    {
      List<Network.EventData> dataListInSection = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
      if (dataListInSection != null && dataListInSection.Count > 0)
      {
        if (dataListInSection.Count == 1)
        {
          Network.EventData firstEvent = dataListInSection[0];
          List<DeliveryTable.DeliveryData> deliveryTableDataList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryTableDataList(false);
          List<ClearStatusDelivery> clearStatusDelivery1 = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
          Func<DeliveryTable.DeliveryData, bool> predicate = (Func<DeliveryTable.DeliveryData, bool>) (d => d.IsEvent() && d.eventID == firstEvent.eventId);
          int num1 = deliveryTableDataList.Where<DeliveryTable.DeliveryData>(predicate).Count<DeliveryTable.DeliveryData>();
          int num2 = 0;
          for (int index = 0; index < clearStatusDelivery1.Count; ++index)
          {
            ClearStatusDelivery clearStatusDelivery2 = clearStatusDelivery1[index];
            DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) clearStatusDelivery2.deliveryId);
            if (deliveryTableData == null)
              Log.Warning("DeliveryTable Not Found : dId " + (object) clearStatusDelivery2.deliveryId);
            else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == firstEvent.eventId && clearStatusDelivery2.deliveryStatus == 3)
              ++num2;
          }
          if (num1 + num2 == 18)
            GameSection.ChangeStayEvent("MINI_BINGO");
        }
        else
          GameSection.ChangeStayEvent("MINI_BINGO");
      }
      GameSection.ResumeEvent(true);
    }));
  }

  private void OnQuery_QUEST_COUNTER()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
      TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2, (Action<bool>) (b =>
      {
        if (!Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
          return;
        this.homeTutorialManager.ForceDeleteArrow(true);
      }));
    this.fromQuestCounterAreaEvent = true;
    int num = !this.fromQuestCounterAreaEvent ? 1 : 0;
    this.fromQuestCounterAreaEvent = false;
    if (num != 0)
    {
      if (this.StopEventUntilTheAllTutorialCompleted())
        return;
    }
    else if (Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
    {
      if (HomeTutorialManager.ShouldRunGachaTutorial() || HomeTutorialManager.ShouldRunQuestShadowTutorial())
      {
        GameSection.StopEvent();
        return;
      }
      this.homeTutorialManager.CloseDialog();
    }
    if (!GameSceneManager.isAutoEventSkip)
      SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
    HomeBase.OnTalkPamelaTutorial = false;
    if (Object.op_Inequality((Object) this.mdlArrow, (Object) null))
      Object.Destroy((Object) ((Component) this.mdlArrow).gameObject);
    if (!MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady || !HomeBase.OnAfterGacha2Tutorial)
      return;
    HomeBase.OnAfterGacha2Tutorial = false;
    if (!Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
      return;
    this.homeTutorialManager.DisableTargetArea();
  }

  private void OnQuery_EVENT_COUNTER_AREA()
  {
    this.fromQuestCounterAreaEvent = true;
    GameSection.ChangeEvent("EVENT_COUNTER");
    this.OnQuery_EVENT_COUNTER();
  }

  private void OnQuery_EVENT_COUNTER()
  {
    int num = !this.fromQuestCounterAreaEvent ? 1 : 0;
    this.fromQuestCounterAreaEvent = false;
    if (num != 0)
      this.StopEventUntilTheAllTutorialCompleted();
    else if (Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
    {
      if (HomeTutorialManager.DoesTutorial() || HomeTutorialManager.ShouldRunGachaTutorial())
      {
        GameSection.StopEvent();
        return;
      }
      this.homeTutorialManager.CloseDialog();
    }
    if (GameSceneManager.isAutoEventSkip)
      return;
    SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
  }

  private void OnQuery_GACHA_QUEST_COUNTER_AREA()
  {
    this.fromQuestCounterAreaEvent = true;
    GameSection.ChangeEvent("GACHA_QUEST_COUNTER");
    this.OnQuery_GACHA_QUEST_COUNTER();
  }

  private void OnQuery_GACHA_QUEST_COUNTER()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
      TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST, (Action<bool>) (b =>
      {
        if (!Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
          return;
        this.homeTutorialManager.ForceDeleteArrow(false);
      }));
    this.fromQuestCounterAreaEvent = true;
    int num = !this.fromQuestCounterAreaEvent ? 1 : 0;
    this.fromQuestCounterAreaEvent = false;
    if (num != 0)
    {
      this.StopEventUntilTheAllTutorialCompleted();
    }
    else
    {
      if (Object.op_Inequality((Object) this.homeTutorialManager, (Object) null))
      {
        if (HomeTutorialManager.DoesTutorial())
        {
          GameSection.StopEvent();
          return;
        }
        this.homeTutorialManager.CloseDialog();
      }
      HomeBase.OnClickQuestForTutorial = false;
      if (Object.op_Inequality((Object) this.mdlArrowQuest, (Object) null))
        Object.Destroy((Object) ((Component) this.mdlArrowQuest).gameObject);
    }
    if (GameSceneManager.isAutoEventSkip)
      return;
    SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
  }

  private void OnQuery_EXPLORE()
  {
    if (this.StopEventUntilTheAllTutorialCompleted() || GameSceneManager.isAutoEventSkip)
      return;
    SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
  }

  private void OnQuery_GUILD_SETTING()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId == -1)
      return;
    MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = true;
  }

  private void OnQuery_BONUS_TIME() => this.bonusTime.OnTap();

  private void UpdateBonusTime()
  {
    if (Object.op_Equality((Object) this.bonusTime, (Object) null))
      return;
    this.bonusTime.UpdateBonusTime();
  }

  private void OnQuery_NONE()
  {
    if (!TutorialStep.HasAllTutorialCompleted())
      MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep6_2");
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
      this.triger_tutorial_gacha_1 = true;
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
      this.triger_tutorial_force_item = true;
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.DONE_CHANGE_WEAPON) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
      this.triger_tutorial_change_item = true;
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
      this.triger_tutorial_gacha_2 = true;
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
      return;
    this.triger_tutorial_upgrade = true;
  }

  protected void OnQuery_POINT_SHOP_FROM_BUTTON()
  {
    if (this.iHomeManager == null || !this.iHomeManager.IsPointShopOpen)
    {
      GameSection.StopEvent();
    }
    else
    {
      if (GameSceneManager.isAutoEventSkip)
        return;
      SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
    }
  }

  protected void OnQuery_GUILD_REQUEST()
  {
    if (GameSceneManager.isAutoEventSkip)
      return;
    SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
  }

  private void OnQuery_MUTUAL_FOLLOW()
  {
    string mutualFollowValue = MonoBehaviourSingleton<FriendManager>.I.MutualFollowValue;
    if (string.IsNullOrEmpty(mutualFollowValue))
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendMutualFollow(mutualFollowValue, (Action<bool>) (is_success =>
    {
      if (!is_success)
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_ALERT_MESSAGE()
  {
    GameSection.SetEventData((object) MonoBehaviourSingleton<UserInfoManager>.I.alertMessage);
    MonoBehaviourSingleton<UserInfoManager>.I.OnReadAlertMessage();
  }

  private void OnQuery_AlertMessage_OK()
  {
    if (string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.alertMessage))
      return;
    this.RequestEvent("ALERT_MESSAGE");
  }

  private void OnQuery_BANNER_NEXT()
  {
    MonoBehaviourSingleton<UIManager>.I.bannerView.NextBanner(2);
  }

  private void OnQuery_BANNER_PREV()
  {
    MonoBehaviourSingleton<UIManager>.I.bannerView.NextBanner(2, false);
  }

  private void OnQuery_HomeToFieldConfirm_YES() => this.OnQuery_FIELD();

  private void OnQuery_FRIEND_PROMOTION()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnCloseDialog_QuestAcceptDeliveryDetail()
  {
    if (!TutorialStep.IsPlayingFirstReward())
      return;
    this.executeTutorialStep6 = true;
  }

  private void OnCloseDialog_HomeNoticeNewDelivery()
  {
    if (!TutorialMessageTable.HasReadTutorialEnd())
      this.executeTutorialEnd = true;
    if (!GameSaveData.instance.IsRecommendedDeliveryCheck())
      return;
    GameSaveData.instance.recommendedDeliveryCheck = 0;
    GameSaveData.Save();
    this.RefreshUI();
  }

  protected virtual void OnQuery_COMMUNITY()
  {
    if (!GameSceneManager.isAutoEventSkip)
      SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader())
      GameSection.ChangeEvent("COMMUNITY_LEADER");
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClanData =>
    {
      MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
      GameSection.ResumeEvent(userClanData != null);
    }));
  }

  private bool CheckShowHomeBanner()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList.Count <= 0)
      return false;
    string str1 = ";";
    string str2 = "-";
    double num = 14400.0;
    DateTime dateTime = DateTime.Now.AddSeconds(num);
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList.Count; index1 < count; ++index1)
    {
      Network.HomeBanner homeBanner = MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList[index1];
      if (homeBanner != null)
      {
        bool flag = true;
        string str3 = "BANNDER_ID_" + homeBanner.bannerId.ToString();
        if (GameSaveData.instance != null && GameSaveData.instance.showHomeBanners != null && GameSaveData.instance.showHomeBanners.Contains(str3))
        {
          flag = false;
          string[] strArray1 = GameSaveData.instance.showHomeBanners.Split(';');
          if (strArray1 != null && strArray1.Length != 0)
          {
            int index2 = 0;
            for (int length = strArray1.Length; index2 < length; ++index2)
            {
              if (!string.IsNullOrEmpty(strArray1[index2]))
              {
                string[] strArray2 = strArray1[index2].Split('-');
                if (strArray2 != null && strArray2.Length > 1 && strArray2[0] == str3)
                {
                  DateTime result;
                  DateTime.TryParse(strArray2[1], out result);
                  if ((DateTime.Now - result).Milliseconds > 0)
                  {
                    if (homeBanner.homeType == 1)
                    {
                      string purchaseBundle = this.GetPurchaseBundle(homeBanner.targetString);
                      if (string.IsNullOrEmpty(purchaseBundle))
                        return false;
                      homeBanner.targetString = purchaseBundle;
                    }
                    string showHomeBanners = GameSaveData.instance.showHomeBanners;
                    int startIndex = GameSaveData.instance.showHomeBanners.IndexOf(str3) + strArray2[0].Length + strArray2[1].Length + 2;
                    GameSaveData.instance.showHomeBanners = showHomeBanners.Substring(startIndex, showHomeBanners.Length - startIndex) + str3 + str2 + (object) DateTime.Now.AddSeconds(num) + str1;
                    GameSaveData.Save();
                    this.DispatchEvent("HOME_BANNER", (object) homeBanner);
                    return true;
                  }
                }
              }
            }
          }
        }
        if (flag)
        {
          if (homeBanner.homeType == 1)
          {
            string purchaseBundle = this.GetPurchaseBundle(homeBanner.targetString);
            if (string.IsNullOrEmpty(purchaseBundle))
              return false;
            homeBanner.targetString = purchaseBundle;
          }
          string str4 = str3 + str2 + (object) dateTime;
          GameSaveData instance = GameSaveData.instance;
          instance.showHomeBanners = instance.showHomeBanners + str4 + str1;
          this.DispatchEvent("HOME_BANNER", (object) homeBanner);
          return true;
        }
      }
    }
    return false;
  }

  public string GetPurchaseBundle(string targetString)
  {
    string purchaseBundle = "bundle";
    if (string.IsNullOrEmpty(targetString))
      return purchaseBundle;
    string[] strArray = targetString.Split(',');
    if (strArray == null || strArray.Length == 0 || !MonoBehaviourSingleton<ShopManager>.IsValid() || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList == null || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList == null || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Count <= 0)
      return purchaseBundle;
    List<Network.ProductData> list = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where<Network.ProductData>((Func<Network.ProductData, bool>) (o => o.productType == 2)).ToList<Network.ProductData>();
    int length = strArray.Length;
    int count = list.Count;
    bool flag = false;
    for (int index1 = 0; index1 < length; ++index1)
    {
      string targetId = strArray[index1];
      if (!string.IsNullOrEmpty(targetId))
      {
        targetId = targetId.Trim();
        if (Singleton<ProductDataTable>.I.packs.Find((Predicate<ProductDataTable.PackInfo>) (data => data.bundleId == targetId)) != null)
        {
          flag = true;
          for (int index2 = 0; index2 < count; ++index2)
          {
            Network.ProductData productData = list[index2];
            if (productData != null && !string.IsNullOrEmpty(productData.productId) && productData.productId.Trim().Equals(targetId))
              return targetId;
          }
        }
      }
    }
    return flag ? (string) null : purchaseBundle;
  }

  private enum UI
  {
    OBJ_NOTICE,
    LBL_NOTICE,
    BTN_STORAGE,
    BTN_MISSION_GG,
    BTN_TICKET,
    BTN_GIFTBOX,
    BTN_TRADING_POST,
    BTN_CHAT,
    OBJ_BALOON_ROOT,
    OBJ_GIFT,
    OBJ_MENU_GIFT_ON,
    BTN_MENU_GG_ON,
    OBJ_EXPLORE_BALLOON_POS,
    BTN_CHAIR,
    OBJ_NORMAL_NOTICE,
    OBJ_BUTTON_NOTICE,
    OBJ_NOTICE_LOCK,
    LBL_NOTICE_LOCK,
    OBJ_BONUS_TIME_ROOT,
    OBJ_COUNTDOWN_ROOT,
    OBJ_LOUNGE,
    BTN_LOUNGE,
    SPR_LOCK_LOUNGE,
    BTN_EXPLORE,
    BTN_GUILD_REQUEST,
    BTN_POINT_SHOP,
    BTN_COMMUNITY,
    OBJ_GUILD,
    BTN_GUILD_NO_GUILD,
    BTN_GUILD,
    SPR_LOCK_GUILD,
    SPR_GUILD_EMBLEM_1,
    SPR_GUILD_EMBLEM_2,
    SPR_GUILD_EMBLEM_3,
    SPR_BADGE,
    OBJ_CLAN_SCOUT,
    BTN_CLAN_SCOUT,
  }
}
