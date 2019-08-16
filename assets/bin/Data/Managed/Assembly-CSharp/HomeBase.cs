using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class HomeBase : GameSection
{
	private enum UI
	{
		OBJ_NOTICE,
		LBL_NOTICE,
		BTN_STORAGE,
		BTN_MISSION_GG,
		BTN_TICKET,
		BTN_GIFTBOX,
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
		BTN_CLAN_SCOUT
	}

	public static readonly string QuestBalloonName = "QUEST_COUNTER_BALLOON";

	protected IHomeManager iHomeManager;

	protected OutGameSettingsManager.HomeScene homeSetting;

	public static bool OnTalkPamelaTutorial;

	public static bool OnAfterGacha2Tutorial;

	private Transform mdlArrow;

	private Transform mdlArrowQuest;

	public static bool OnClickQuestForTutorial;

	public static bool isFirstTimeDisplayTextTutorial;

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

	public override bool useOnPressBackKey => true;

	private void OnDisable()
	{
		if (homeTutorialManager != null)
		{
			homeTutorialManager.DeleteArrow();
			Debug.Log((object)"Delete Arrow");
		}
	}

	public override void OnPressBackKey()
	{
		Native.applicationQuit();
	}

	public override void InitializeReopen()
	{
		base.InitializeReopen();
		this.StartCoroutine(IESetupLoginBonus());
	}

	private IEnumerator IESetupLoginBonus()
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)new WaitForSeconds(0.04f);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && limitedLoginBonus != null && limitedLoginBonus.Count > 0 && _isWaitingLoginBonus)
		{
			SetupLoginBonus();
		}
		if (HomeTutorialManager.DoesTutorialAfterGacha2())
		{
		}
		yield return null;
	}

	public override void Initialize()
	{
		DestroyInGameTutorialManager();
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.selfCacheObject != null)
		{
			Object.Destroy(MonoBehaviourSingleton<InGameManager>.I.selfCacheObject);
		}
		MonoBehaviourSingleton<InventoryManager>.I.SetList();
		NetworkNative.createRegistrationId();
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if (component != null)
		{
			component.set_enabled(false);
		}
		MonoBehaviourSingleton<StatusManager>.I.SetUserStatus();
		if (MonoBehaviourSingleton<SmithManager>.IsValid())
		{
			MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData();
		}
		SetupNotice();
		if (MonoBehaviourSingleton<ShopManager>.IsValid() && !MonoBehaviourSingleton<ShopManager>.I.HasCheckPromotionItem)
		{
			MonoBehaviourSingleton<ShopManager>.I.SendCheckPromotion();
		}
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			prevQuest = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade;
			MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetErrorResendQuestGachaFlag();
		}
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
		}
		iHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		homeSetting = iHomeManager.GetSceneSetting();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_7_town_hall_1, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_7_town_hall_1.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_7_town_hall_1, "Tutorial");
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_10_town_hall_2, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_10_town_hall_2.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_10_town_hall_2, "Tutorial");
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_12_town_hall_3, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_12_town_hall_3.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_12_town_hall_3, "Tutorial");
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_14_town_hall_4, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_14_town_hall_4.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_14_town_hall_4, "Tutorial");
		}
		if (MonoBehaviourSingleton<ShopManager>.IsValid())
		{
			MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList(null);
		}
		this.StartCoroutine(DoInitialize());
	}

	public override void StartSection()
	{
		SetupPointShop();
		SetUpBingo();
		CheckEventLock();
		if (CheckOpenGacha() || CheckNeededGotoGacha() || CheckNeededOpenQuest() || CheckJoinClanIngame() || CheckInvitedClanBySNS() || CheckInvitedPartyBySNS() || CheckInvitedLoungeBySNS() || CheckMutualFollowBySNS())
		{
			return;
		}
		if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04) && !TutorialStep.HasDeliveryRewardCompleted())
		{
			DispatchEvent("QUEST_COUNTER");
			return;
		}
		if (!TutorialStep.HasChangeEquipCompleted())
		{
			if (TutorialStep.IsPlayingStudioTutorial() && !TutorialStep.isSendFirstRewardComplete)
			{
				TutorialStep_6();
			}
			return;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			SetupLoginBonus();
		}
		needCheckNotifyQuestRemain = true;
		needShowAppReviewAppeal = true;
		needShowShadowChallengeFirst = true;
		needCountdown = true;
		if (MonoBehaviourSingleton<AccountManager>.I.logInBonusLimitedCount < 3)
		{
			needShowDailyDelivery = true;
		}
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			needFollowCheck = true;
		}
	}

	public override void UpdateUI()
	{
		SetFontStyle((Enum)UI.LBL_NOTICE, 2);
		UpdateUIOfTutorial();
		CheckBalloons();
		if (questBalloon != null)
		{
			ResetTween(questBalloon);
			PlayTween(questBalloon, forward: true, null, is_input_block: false);
			if (storyBalloon != null)
			{
				storyBalloon.get_parent().get_gameObject().SetActive(false);
				storyBalloon = null;
			}
		}
		else if (storyBalloon != null)
		{
			ResetTween(storyBalloon);
			PlayTween(storyBalloon, forward: true, null, is_input_block: false);
		}
		if (orderBalloon != null)
		{
			ResetTween(orderBalloon);
			PlayTween(orderBalloon, forward: true, null, is_input_block: false);
		}
		UpdateEventBalloon();
		UpdateTicketNum();
		UpdateGuildRequest();
		UpdatePointShop();
		UpdateGiftboxNum();
		UpdateGuildBtn();
	}

	private void UpdateGuildBtn()
	{
	}

	private void UpdateClanBadge()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guilMemberList != null)
		{
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD), UI.SPR_BADGE, MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters != null && MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters.Count > 0);
		}
		else
		{
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD), UI.SPR_BADGE, is_visible: false);
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.CHANGED_SCENE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
			{
				if (!MonoBehaviourSingleton<UIManager>.I.mainChat.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
				}
				MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_EVENT_BANNER) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.bannerView != null && !MonoBehaviourSingleton<UIManager>.I.bannerView.isOpen && TutorialStep.HasAllTutorialCompleted() && !HomeTutorialManager.ShouldRunGachaTutorial())
			{
				MonoBehaviourSingleton<UIManager>.I.bannerView.Open();
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_PARTY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationButton != null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen && IsCurrentSectionHomeOrLounge())
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Open();
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_RALLY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationButton != null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen && IsCurrentSectionHomeOrLounge())
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Open();
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.RESET_DARK_MARKET) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.blackMarkeButton != null && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.ResetMarketTime();
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_TASK_LIST) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<AchievementManager>.IsValid())
			{
				List<TaskInfo> taskInfos = MonoBehaviourSingleton<AchievementManager>.I.GetTaskInfos();
				int num = 0;
				for (int i = 0; i < taskInfos.Count; i++)
				{
					if (taskInfos[i].status == 2)
					{
						num++;
					}
				}
				_taskBadgeNum = num;
				SetBadge(GetCtrl(UI.BTN_MISSION_GG), num, 3, 8, 8);
				SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
				SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			UpdateTicketNum();
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_CHANGE) != (NOTIFY_FLAG)0L)
		{
			UpdateGuildRequest();
		}
		if ((flags & NOTIFY_FLAG.UPDATE_PRESENT_NUM) != (NOTIFY_FLAG)0L || (flags & NOTIFY_FLAG.UPDATE_PRESENT_LIST) != (NOTIFY_FLAG)0L)
		{
			UpdateGiftboxNum();
		}
		if ((NOTIFY_FLAG.UPDATE_USER_STATUS & flags) != (NOTIFY_FLAG)0L)
		{
			OnNotifyUpdateUserStatus();
		}
		if ((NOTIFY_FLAG.TRANSITION_END & flags) != (NOTIFY_FLAG)0L && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss != null)
		{
			MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.ClearAnnounce();
			if (MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.IsKnockDownRaidBossByEventItemCountList())
			{
				MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.PlayKnockDown();
			}
		}
		if ((NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE & flags) != (NOTIFY_FLAG)0L)
		{
			UpdateCommunityBadge();
		}
		base.OnNotify(flags);
	}

	public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && !(MonoBehaviourSingleton<UIManager>.I.mainChat == null))
		{
			if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
			{
				SetBadge((Enum)UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNum(), 1, -5, -29, is_scale_normalize: false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(true);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(false);
			}
		}
	}

	protected virtual void OnNotifyUpdateUserStatus()
	{
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level != prevLevel)
		{
			MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUp();
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade != prevQuest)
		{
			SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
			prevQuest = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade;
		}
		CheckEventLock();
	}

	private IEnumerator DoInitialize()
	{
		yield return this.StartCoroutine(WaitInitializeManager());
		CreateSelfCharacter();
		yield return this.StartCoroutine(LoadTutorialMessage());
		yield return this.StartCoroutine(CreatePuniCon());
		yield return this.StartCoroutine(SendHomeInfo());
		if (FieldRewardPool.HasSave())
		{
			FieldRewardPool fieldRewardPool = FieldRewardPool.LoadAndCreate();
			bool wait = true;
			fieldRewardPool.SendFieldDrop(delegate
			{
				wait = false;
			});
			while (wait)
			{
				yield return null;
			}
		}
		yield return this.StartCoroutine(WaitLoadHomeCharacters());
		yield return this.StartCoroutine(DoTutorial());
		if (!Singleton<TutorialMessageTable>.IsValid() || !TutorialStep.HasChangeEquipCompleted() || TutorialStep.HasAllTutorialCompleted())
		{
			transferNoticeNewDelivery = true;
		}
		SetupValidLoginBonus();
		if (validLoginBonus)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.skipTrantisionEnd = true;
			waitEventBalloon = true;
		}
		SetIconAndBalloon();
		SetUpBonusTime();
		MonoBehaviourSingleton<GuildManager>.I.GetClanStat();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			bool isWait = true;
			MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", delegate
			{
				isWait = false;
			});
			while (isWait)
			{
				yield return null;
			}
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
		{
			bool wait_clan_donate_invite = true;
			MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList(delegate
			{
				wait_clan_donate_invite = false;
			}, isResumed: true);
			while (wait_clan_donate_invite)
			{
				yield return null;
			}
		}
		base.Initialize();
	}

	private IEnumerator DoTutorial()
	{
		bool isDeliveryTutorial = HomeTutorialManager.DoesTutorial();
		if (isDeliveryTutorial)
		{
			homeTutorialManager = this.get_gameObject().AddComponent<HomeTutorialManager>();
			if (!(homeTutorialManager == null))
			{
				if (isDeliveryTutorial)
				{
					homeTutorialManager.Setup();
					this.StartCoroutine(SetupArrow());
				}
				while (homeTutorialManager.IsLoading())
				{
					yield return null;
				}
				OnTalkPamelaTutorial = true;
			}
		}
		else if (HomeTutorialManager.ShouldRunGachaTutorial() && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			homeTutorialManager = this.get_gameObject().GetComponent<HomeTutorialManager>();
			if (homeTutorialManager == null)
			{
				homeTutorialManager = this.get_gameObject().AddComponent<HomeTutorialManager>();
			}
			if (!(homeTutorialManager == null))
			{
				homeTutorialManager.SetupGachaQuestTutorial();
				OnClickQuestForTutorial = true;
				while (homeTutorialManager.IsLoading())
				{
					yield return null;
				}
			}
		}
		else if (HomeTutorialManager.ShouldRunQuestShadowTutorial() && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			homeTutorialManager = this.get_gameObject().GetComponent<HomeTutorialManager>();
			if (homeTutorialManager == null)
			{
				homeTutorialManager = this.get_gameObject().AddComponent<HomeTutorialManager>();
			}
			if (!(homeTutorialManager == null))
			{
				homeTutorialManager.SetupGachaQuestTutorial();
				this.StartCoroutine(SetupArrowForQuest());
				OnClickQuestForTutorial = true;
				while (homeTutorialManager.IsLoading())
				{
					yield return null;
				}
			}
		}
		else
		{
			if (!MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) || (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS)))
			{
				yield break;
			}
			isFirstTimeDisplayTextTutorial = false;
			bool loadNeedBit = false;
			if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM))
			{
				TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM, delegate
				{
					isFirstTimeDisplayTextTutorial = true;
					loadNeedBit = true;
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_16_tutorial_end, "Tutorial");
					Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_16_tutorial_end.ToString()));
					MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_16_tutorial_end, "Tutorial");
				});
			}
			else
			{
				loadNeedBit = true;
			}
			while (!loadNeedBit)
			{
				yield return null;
			}
			homeTutorialManager = this.get_gameObject().AddComponent<HomeTutorialManager>();
			if (!(homeTutorialManager == null))
			{
				homeTutorialManager.Setup();
				while (homeTutorialManager.IsLoading())
				{
					Debug.Log((object)"Wait homeTutorialManager Loading!");
					yield return null;
				}
			}
		}
	}

	private IEnumerator SetupArrow()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
		{
			"mdl_arrow_01"
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Vector3 ARROW_OFFSET = new Vector3(-4.28f, 1.66f, 14.61f);
		Vector3 ARROW_SCALE = new Vector3(4f, 4f, 4f);
		mdlArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
		ResourceUtility.Realizes(loadedArrow.loadedObject, mdlArrow);
		mdlArrow.set_localScale(ARROW_SCALE);
		mdlArrow.set_position(ARROW_OFFSET);
	}

	private IEnumerator SetupArrowForQuest()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
		{
			"mdl_arrow_01"
		});
		loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Vector3 ARROW_SCALE = new Vector3(4f, 4f, 4f);
		Vector3 ARROW_OFFSET = new Vector3(3.2f, 2.8f, 14f);
		mdlArrowQuest = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
		ResourceUtility.Realizes(loadedArrow.loadedObject, mdlArrowQuest);
		mdlArrowQuest.set_localScale(ARROW_SCALE);
		mdlArrowQuest.set_position(ARROW_OFFSET);
		homeTutorialManager = this.get_gameObject().GetComponent<HomeTutorialManager>();
		if (homeTutorialManager == null)
		{
			homeTutorialManager = this.get_gameObject().AddComponent<HomeTutorialManager>();
		}
		if (homeTutorialManager != null)
		{
			homeTutorialManager.dialog.OpenAfterGacha2();
			homeTutorialManager.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 4u));
		}
	}

	private IEnumerator LoadTutorialMessage()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && !(MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null) && UserInfoManager.IsNeedsTutorialMessage())
		{
			bool loadingTutorialMessage = true;
			MonoBehaviourSingleton<UIManager>.I.LoadTutorialMessage(delegate
			{
				loadingTutorialMessage = false;
			});
			while (loadingTutorialMessage)
			{
				yield return null;
			}
		}
	}

	protected void CreateSelfCharacter()
	{
		iHomeManager.IHomePeople.CreateSelfCharacter(OnNoticeAreaEvent);
	}

	protected IEnumerator WaitInitializeManager()
	{
		while (!iHomeManager.IsInitialized)
		{
			yield return null;
		}
	}

	private void DestroyInGameTutorialManager()
	{
		InGameTutorialManager component = MonoBehaviourSingleton<AppMain>.I.GetComponent<InGameTutorialManager>();
		if (component != null)
		{
			Object.Destroy(component);
		}
	}

	protected virtual IEnumerator SendHomeInfo()
	{
		bool wait = true;
		_isWaitingLoginBonus = false;
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM))
		{
			if (!_isReceiveLoginBonus)
			{
				Debug.LogWarning((object)"SendHomeInfo if _isReceiveLoginBonus false");
				MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(delegate(bool result, bool acquireLoginBonus, int taskBadgeNum)
				{
					_acquireLoginBonus = acquireLoginBonus;
					_taskBadgeNum = taskBadgeNum;
					SetBadge(GetCtrl(UI.BTN_MISSION_GG), _taskBadgeNum, 1, 8, -8);
					SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
					SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
					if (_acquireLoginBonus && MonoBehaviourSingleton<AccountManager>.IsValid())
					{
						MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus(delegate(bool _result)
						{
							wait = false;
							if (_result)
							{
								_isReceiveLoginBonus = true;
								Debug.LogWarning((object)"_isReceiveLoginBonus true");
								_isWaitingLoginBonus = true;
							}
						});
					}
					else
					{
						wait = false;
						_isReceiveLoginBonus = true;
					}
				});
			}
			else if (_isHomeInfoCached)
			{
				SetBadge(GetCtrl(UI.BTN_MISSION_GG), _taskBadgeNum, 1, 8, -8);
				if (_acquireLoginBonus && MonoBehaviourSingleton<AccountManager>.IsValid())
				{
					MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus(delegate
					{
						wait = false;
						MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(delegate(bool b, bool acquireLoginBonus, int taskBadgeNum)
						{
							_acquireLoginBonus = acquireLoginBonus;
							_taskBadgeNum = taskBadgeNum;
							SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
							SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
						});
					});
				}
				else
				{
					wait = false;
					MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(delegate(bool result, bool acquireLoginBonus, int taskBadgeNum)
					{
						_isHomeInfoCached = true;
						_acquireLoginBonus = acquireLoginBonus;
						_taskBadgeNum = taskBadgeNum;
						SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
						SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
					});
				}
			}
			else
			{
				MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(delegate(bool result, bool acquireLoginBonus, int taskBadgeNum)
				{
					wait = false;
					_isHomeInfoCached = true;
					_acquireLoginBonus = acquireLoginBonus;
					_taskBadgeNum = taskBadgeNum;
					SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
					SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
				});
			}
		}
		else
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(delegate(bool result, bool acquireLoginBonus, int taskBadgeNum)
			{
				_acquireLoginBonus = acquireLoginBonus;
				_taskBadgeNum = taskBadgeNum;
				SetActive(GetCtrl(UI.BTN_MISSION_GG), UI.OBJ_GIFT, ShouldEnableGiftIcon());
				SetActive((Enum)UI.OBJ_MENU_GIFT_ON, ShouldEnableGiftIcon());
				if (_acquireLoginBonus && MonoBehaviourSingleton<AccountManager>.IsValid())
				{
					MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus(delegate
					{
						wait = false;
						_isWaitingLoginBonus = true;
					});
				}
				else
				{
					wait = false;
				}
			});
		}
		while (wait)
		{
			yield return null;
		}
	}

	protected IEnumerator WaitLoadHomeCharacters()
	{
		while (iHomeManager.IHomePeople.selfChara.isLoading || !iHomeManager.IHomePeople.isPeopleInitialized)
		{
			yield return null;
		}
	}

	private IEnumerator CreatePuniCon()
	{
		LoadingQueue load_queue = null;
		LoadObject lo_punicon = null;
		if (HomeSelfCharacter.CTRL)
		{
			load_queue = new LoadingQueue(this);
			lo_punicon = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGame", new string[1]
			{
				"PuniConManager"
			});
		}
		yield return load_queue.Wait();
		if (lo_punicon != null)
		{
			ResourceUtility.Realizes(lo_punicon.loadedObjects[0].obj, MonoBehaviourSingleton<UIManager>.I._transform, 5);
		}
	}

	private void SetupValidLoginBonus()
	{
		if ((MonoBehaviourSingleton<AccountManager>.I.logInBonus == null || MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count == 0) && GameSaveData.instance.logInBonus != null && GameSaveData.instance.logInBonus.Count > 0)
		{
			MonoBehaviourSingleton<AccountManager>.I.SetLoginBonusFromCache(GameSaveData.instance.logInBonus);
		}
		bool flag = MonoBehaviourSingleton<AccountManager>.I.IsRecvLogInBonus && MonoBehaviourSingleton<AccountManager>.I.logInBonus != null && MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count > 0;
		bool flag2 = TutorialStep.HasDailyBonusUnlocked();
		bool flag3 = MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent();
		validLoginBonus = (flag && flag2 && !flag3 && MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_UPGRADE_ITEM) && !isFirstTimeDisplayTextTutorial);
	}

	protected virtual void SetIconAndBalloon()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/QUEST_ICON_POS");
			if (val != null)
			{
				questIconPos = val.get_position();
			}
			val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/ORDER_ICON_POS");
			if (val != null)
			{
				orderIconPos = val.get_position();
			}
		}
		CheckBalloons();
		if (MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList != null && MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList.Count > 0 && !MonoBehaviourSingleton<GachaDecoManager>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<GachaDecoManager>();
		}
	}

	private void CheckBalloons()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep <= 4)
		{
			return;
		}
		if (questBalloon == null)
		{
			if (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableNormalDeliveryNum() > 0)
			{
				questBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon((!MonoBehaviourSingleton<DeliveryManager>.I.hasProgressDailyDelivery) ? UI_Common.BALLOON_TYPE.COMPLETABLE_NORMAL_L : UI_Common.BALLOON_TYPE.COMPLETABLE_DAILY, GetCtrl(UI.OBJ_BALOON_ROOT));
			}
			else if (GameSaveData.instance.IsRecommendedDeliveryCheck())
			{
				questBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(MonoBehaviourSingleton<DeliveryManager>.I.hasProgressDailyDelivery ? UI_Common.BALLOON_TYPE.NEW_DAILY : UI_Common.BALLOON_TYPE.NEW_NORMAL_L, GetCtrl(UI.OBJ_BALOON_ROOT));
			}
			else if (storyBalloon == null && MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(new DELIVERY_TYPE[1]
			{
				DELIVERY_TYPE.STORY
			}))
			{
				storyBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_L, GetCtrl(UI.OBJ_BALOON_ROOT));
			}
		}
		if (orderBalloon == null)
		{
			if (GameSaveData.instance.IsRecommendedChallengeCheck())
			{
				orderBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_SHADOW_CHALLENGE, GetCtrl(UI.OBJ_BALOON_ROOT));
			}
			else if (GameSaveData.instance.IsRecommendedOrderCheck())
			{
				orderBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_R, GetCtrl(UI.OBJ_BALOON_ROOT));
			}
		}
	}

	protected bool CheckNeededGotoGacha()
	{
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.oncePurchaseGachaProductId))
		{
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("MAIN_MENU_SHOP", null),
				new EventData("FORCE_ONCE_PURCHASE_GACHA", MonoBehaviourSingleton<UserInfoManager>.I.oncePurchaseGachaProductId)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			return true;
		}
		return false;
	}

	protected bool CheckNeededOpenQuest()
	{
		if (HomeTutorialManager.ShouldRunGachaTutorial() || iHomeManager.IsJumpToGacha)
		{
			iHomeManager.IsJumpToGacha = false;
			DispatchEvent("GACHA_QUEST_COUNTER_AREA");
			return true;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest)
		{
			MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = false;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest)
		{
			MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = false;
			DispatchEvent("GACHA_QUEST_COUNTER_AREA");
			return true;
		}
		return false;
	}

	protected void SetupLoginBonus()
	{
		if (validLoginBonus)
		{
			shouldFrameInNPC006 = true;
			CheckLoginBonusFirst();
			return;
		}
		HomeNPCCharacter homeNPCCharacter = iHomeManager.IHomePeople.GetHomeNPCCharacter(6);
		homeNPCCharacter.HideShadow();
		HomeDragonRandomMove homeDragonRandomMove = homeNPCCharacter.loader.GetAnimator().get_gameObject().AddComponent<HomeDragonRandomMove>();
		homeDragonRandomMove.Reset();
	}

	protected void SetupPointShop()
	{
		if (iHomeManager != null && iHomeManager.IsPointShopOpen && !MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
		}
	}

	private void SetUpBingo()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<QuestManager>.IsValid() || !MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist())
		{
			return;
		}
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/BINGO_ICON_POS");
			if (val != null)
			{
				bingoIconPos = val.get_position();
			}
		}
		bingoBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateBingoBalloon(GetCtrl(UI.OBJ_BALOON_ROOT));
		if (bingoBalloon != null)
		{
			ResetTween(bingoBalloon);
			PlayTween(bingoBalloon, forward: true, null, is_input_block: false);
		}
	}

	private bool CheckOpenGacha()
	{
		string @string = PlayerPrefs.GetString("gc");
		if (!string.IsNullOrEmpty(@string))
		{
			PlayerPrefs.SetString("gc", string.Empty);
			EventData[] array = null;
			if (@string.Equals("magi"))
			{
				array = new EventData[2]
				{
					new EventData("MAIN_MENU_SHOP", null),
					new EventData("MAGI_GACHA", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
				return true;
			}
			if (@string.Equals("behemoth"))
			{
				array = new EventData[1]
				{
					new EventData("MAIN_MENU_SHOP", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
				return true;
			}
		}
		return false;
	}

	private bool CheckNeedOpenUrl()
	{
		string @string = PlayerPrefs.GetString("ur");
		if (!string.IsNullOrEmpty(@string))
		{
			PlayerPrefs.SetString("ur", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
			}
			Application.OpenURL(@string);
			return true;
		}
		return false;
	}

	private bool CheckInvitedClanBySNS()
	{
		string @string = PlayerPrefs.GetString("ic");
		if (!string.IsNullOrEmpty(@string))
		{
			PlayerPrefs.SetString("ic", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
			}
			EventData[] autoEvents = new EventData[3]
			{
				new EventData("GUILD", null),
				new EventData("SEARCH", null),
				new EventData("INFO", int.Parse(@string))
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	private bool CheckJoinClanIngame()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.showJoinClanInGame)
		{
			EventData[] autoEvents = new EventData[1]
			{
				new EventData("GUILD_MESSAGE", null)
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<UserInfoManager>.I.showJoinClanInGame = false;
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	private bool CheckInvitedPartyBySNS()
	{
		string @string = PlayerPrefs.GetString("im");
		if (!string.IsNullOrEmpty(@string))
		{
			MonoBehaviourSingleton<PartyManager>.I.InviteValue = @string;
			PlayerPrefs.SetString("im", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
			}
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("INVITED_ROOM", null)
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	protected abstract bool CheckInvitedLoungeBySNS();

	private bool CheckMutualFollowBySNS()
	{
		string @string = PlayerPrefs.GetString("fc");
		if (!string.IsNullOrEmpty(@string))
		{
			MonoBehaviourSingleton<FriendManager>.I.MutualFollowValue = @string;
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
			}
			EventData[] autoEvents = new EventData[5]
			{
				new EventData("MUTUAL_FOLLOW", null),
				new EventData("MAIN_MENU_MENU", null),
				new EventData("FRIEND", null),
				new EventData("FOLLOW_LIST", null),
				new EventData("MUTUAL_FOLLOW_MESSAGE", null)
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				PlayerPrefs.SetString("fc", string.Empty);
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	protected void CheckLoginBonusFirst()
	{
		GameSaveData.instance.logInBonus = null;
		GameSaveData.Save();
		int logInBonusLimitedCount = MonoBehaviourSingleton<AccountManager>.I.logInBonusLimitedCount;
		if (limitedLoginBonus != null && limitedLoginBonus.Count > 0)
		{
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.Clear();
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange(limitedLoginBonus);
		}
		MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((LoginBonus x) => (x.priority == 0 && x.type != 0) || x.reward == null || x.reward.Count == 0);
		List<LoginBonus> list = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.priority == 0 && x.type == 0);
		if (list != null && list.Count > 0)
		{
			limitedLoginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.type != 0);
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((LoginBonus x) => x.type != 0);
			DispatchEvent("LOGIN_BONUS");
			return;
		}
		Debug.LogWarning((object)("limited_bonus_count : " + logInBonusLimitedCount));
		if (logInBonusLimitedCount >= 3)
		{
			List<LoginBonus> collection = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.priority == 0);
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.Sort((LoginBonus x, LoginBonus y) => y.priority - x.priority);
			for (int num = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count - 1; num >= 3; num--)
			{
				MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAt(num);
			}
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange(collection);
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((LoginBonus x) => x.type == 0);
			if (list != null && list.Count > 0)
			{
				MonoBehaviourSingleton<AccountManager>.I.logInBonus.Add(list[0]);
			}
			GameSection.StopEvent();
			DispatchEvent("LIMITED_LOGIN_BONUS");
		}
		else if (logInBonusLimitedCount == 2)
		{
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((LoginBonus x) => x.type == 0);
			List<LoginBonus> collection2 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.priority == 0);
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.Sort((LoginBonus x, LoginBonus y) => y.priority - x.priority);
			for (int num2 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count - 1; num2 >= 2; num2--)
			{
				MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAt(num2);
			}
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange(collection2);
			DispatchEvent("LIMITED_LOGIN_BONUS");
		}
		else if (logInBonusLimitedCount <= 1)
		{
			List<LoginBonus> collection3 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.priority == 0 && x.type != 0);
			for (int num3 = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count - 1; num3 > 0; num3--)
			{
				if (MonoBehaviourSingleton<AccountManager>.I.logInBonus[num3].priority == 0 && MonoBehaviourSingleton<AccountManager>.I.logInBonus[num3].type != 0)
				{
					MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAt(num3);
				}
			}
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.AddRange(collection3);
			DispatchEvent("LIMITED_LOGIN_BONUS");
		}
		_isWaitingLoginBonus = false;
	}

	protected virtual void UpdateUIOfTutorial()
	{
		bool flag = !HomeTutorialManager.ShouldRunGachaTutorial();
		if (MonoBehaviourSingleton<UIManager>.I.mainChat != null && TutorialStep.HasAllTutorialCompleted() && flag)
		{
			SetActive((Enum)UI.BTN_CHAT, !MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow());
		}
		else
		{
			SetActive((Enum)UI.BTN_CHAT, is_visible: false);
		}
		SetActive((Enum)UI.BTN_STORAGE, TutorialStep.HasAllTutorialCompleted() && flag);
		MonoBehaviourSingleton<UIManager>.I.mainStatus.SetMenuButtonEnable(TutorialStep.HasAllTutorialCompleted() && flag);
		MonoBehaviourSingleton<UIManager>.I.mainMenu.SetMenuButtonEnable(flag);
	}

	protected bool ShouldEnableMissionButton()
	{
		return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade > 0 && !HomeTutorialManager.ShouldRunGachaTutorial();
	}

	protected bool ShouldEnableGiftIcon()
	{
		return _taskBadgeNum > 0;
	}

	private void UpdateTicketNum()
	{
		if (MonoBehaviourSingleton<InventoryManager>.IsValid())
		{
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.type == ITEM_TYPE.TICKET);
			SetBadge((Enum)UI.BTN_TICKET, itemNum, 1, 8, -8, is_scale_normalize: false);
		}
	}

	private void UpdateGiftboxNum()
	{
		SetBadge((Enum)UI.BTN_GIFTBOX, MonoBehaviourSingleton<PresentManager>.I.presentNum, 1, 8, -8, is_scale_normalize: false);
	}

	private void UpdateGuildRequest()
	{
		SetActive((Enum)UI.BTN_GUILD_REQUEST, is_visible: false);
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			int num = (from g in MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList
			where g.questId > 0
			where g.GetQuestRemainTime().TotalSeconds < 0.0
			select g).Count();
			SetBadge(GetCtrl(UI.BTN_GUILD_REQUEST), num, 3, -8, -8);
		}
	}

	private void UpdatePointShop()
	{
		if (iHomeManager != null)
		{
			bool isPointShopOpen = iHomeManager.IsPointShopOpen;
			bool isGuildRequestOpen = MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen;
			SetActive((Enum)UI.BTN_POINT_SHOP, is_visible: false);
		}
	}

	protected virtual void LateUpdate()
	{
		if (base.isInitialized && MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.UpdateUnreadMessage();
		}
		UpdateNotice();
		UpdateBalloon();
		DirectDelivery();
		if (MonoBehaviourSingleton<GachaDecoManager>.IsValid())
		{
			MonoBehaviourSingleton<GachaDecoManager>.I.SetVisible(IsValidGachaDeco());
		}
		if (IsValidDispatchEventInUpdate() && !CheckShowHomeBanner())
		{
			UpdateBonusTime();
		}
	}

	private void DirectDelivery()
	{
		if (!IsCurrentSectionHomeOrLounge() || MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return;
		}
		if (TutorialStep.HasChangeEquipCompleted() && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.CLAIM_REWARD) && executeTutorialClaimReward)
		{
			List<LoginBonus> logInBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus;
			if (logInBonus == null || logInBonus.Count == 0 || logInBonus[0].priority <= 0)
			{
				TutorialClaimReward();
				return;
			}
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.CLAIM_REWARD))
		{
			return;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
		{
			if (!triger_tutorial_gacha_1)
			{
				DispatchEvent("HOME_TUTORIAL");
			}
		}
		else
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
			{
				return;
			}
			if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
			{
				if (!triger_tutorial_force_item)
				{
					DispatchEvent("HOME_TUTORIAL");
				}
			}
			else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.DONE_CHANGE_WEAPON) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
			{
				if (!triger_tutorial_change_item)
				{
					DispatchEvent("HOME_TUTORIAL");
				}
			}
			else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
			{
				if (!triger_tutorial_gacha_2)
				{
					DispatchEvent("HOME_TUTORIAL");
				}
			}
			else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN))
			{
				if (!triger_tutorial_upgrade)
				{
					DispatchEvent("HOME_TUTORIAL");
				}
			}
			else
			{
				if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
				{
					return;
				}
				if (executeTutorialStep6)
				{
					TutorialStep_6();
				}
				else if (executeTutorialEnd)
				{
					TutorialStep_END();
				}
				else
				{
					if (!MonoBehaviourSingleton<DeliveryManager>.IsValid())
					{
						return;
					}
					if (!transferNoticeNewDelivery)
					{
						transferNoticeNewDelivery = true;
						int[] ary = MonoBehaviourSingleton<DeliveryManager>.I.GetRecvStoryDelivery();
						if (ary != null && ary.Length > 0)
						{
							int i = 0;
							for (int num = ary.Length; i < num; i++)
							{
								if (MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.FindIndex((int id) => id == ary[i]) == -1)
								{
									MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Add(ary[i]);
								}
							}
						}
					}
					if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
					{
						if (TutorialStep.HasChangeEquipCompleted() && !TutorialStep.isSendFirstRewardComplete)
						{
							int num2 = MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene[0];
							if (num2 != 0)
							{
								MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.RemoveAt(0);
								DispatchEvent("NOTICE_NEW_DELIVERY", num2);
							}
						}
					}
					else if (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() != 0)
					{
						MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = true;
						EventData[] autoEvents = new EventData[2]
						{
							new EventData("QUEST_COUNTER", null),
							new EventData("SELECT_DELIVERY", MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery())
						};
						MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
					}
					else if (MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() != null)
					{
						Network.EventData eventCleardDeliveryData = MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData();
						EventData[] autoEvents2 = CreateAutoClearDeliveryEvent(eventCleardDeliveryData);
						MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
						MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents2);
					}
					else if (TutorialStep.HasAllTutorialCompleted() && base.isOpen && !sendTutorialTrigger)
					{
						sendTutorialTrigger = true;
						DispatchEvent("HOME_TUTORIAL");
					}
				}
			}
		}
	}

	private EventData[] CreateAutoClearDeliveryEvent(Network.EventData data)
	{
		switch (data.eventType)
		{
		case 15:
			return new EventData[2]
			{
				new EventData("EVENT_COUNTER", null),
				new EventData("SELECT_ARENA", data)
			};
		case 16:
			return new EventData[1]
			{
				new EventData("BINGO", true)
			};
		case 31:
			return new EventData[2]
			{
				new EventData("EVENT_COUNTER", null),
				new EventData("SELECT_TRIAL", data)
			};
		default:
			return new EventData[2]
			{
				new EventData("EVENT_COUNTER", null),
				new EventData("SELECT", data)
			};
		}
	}

	private bool CheckOnceShowObjects()
	{
		if (MonoBehaviourSingleton<GlobalSettingsManager>.I.enableBlackMarketBanner && !MonoBehaviourSingleton<UserInfoManager>.I.showBlackMarketBanner)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.showBlackMarketBanner = true;
			DispatchEvent("BLACK_MARKET_BANNER");
			return true;
		}
		if (MonoBehaviourSingleton<GlobalSettingsManager>.I.enableFortuneWheelBanner && !MonoBehaviourSingleton<UserInfoManager>.I.showFortuneWheel)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.showFortuneWheel = true;
			DispatchEvent("FORTUNE_WHEEL_BANNER");
			return true;
		}
		if (GameSaveData.instance.showHomeOneTimesOfferSSDay != DateTime.UtcNow.Day && MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
		{
			GameSaveData.instance.showHomeOneTimesOfferSSDay = DateTime.UtcNow.Day;
			DispatchEvent("BANNER_ONETIMESOFFERSS");
			return true;
		}
		if (MonoBehaviourSingleton<ShopManager>.IsValid() && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList != null && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups != null && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count > 0)
		{
			List<SkuAdsData> skuPopups = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups;
			int i = 0;
			for (int count = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count; i < count; i++)
			{
				if (!GameSaveData.instance.showIAPAdsPop.Contains(skuPopups[i].productId) && !GameSaveData.instance.iAPBundleBought.Contains(skuPopups[i].productId))
				{
					GameSaveData.instance.showIAPAdsPop = $"{GameSaveData.instance.showIAPAdsPop}/{skuPopups[i].productId}";
					DispatchEvent("IAP_ADD_POP", skuPopups[i].productId);
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
				{
					GameSaveData.instance.spentSummonTicket = 0;
				}
				GameSaveData.instance.showHomeBannerInviteDay = DateTime.UtcNow.Day;
				int num = Random.Range(1, 3);
				if (num == 1)
				{
					DispatchEvent("BANNER_INVITE");
				}
				else
				{
					DispatchEvent("BANNER_GUARANTEDSS");
				}
				return true;
			}
			if (GameSaveData.instance.spentSummonTicket >= 10)
			{
				GameSaveData.instance.spentSummonTicket = 0;
				if (GameSaveData.instance.spent25Gems >= 25)
				{
					GameSaveData.instance.spent25Gems = 0;
				}
				GameSaveData.instance.showHomeBannerInviteDay = DateTime.UtcNow.Day;
				int num2 = Random.Range(1, 3);
				if (num2 == 1)
				{
					DispatchEvent("BANNER_INVITE");
				}
				else
				{
					DispatchEvent("BANNER_GUARANTEDSS");
				}
				return true;
			}
		}
		if (GameSaveData.instance.showHomeBannerOfferDay != DateTime.UtcNow.Day && MonoBehaviourSingleton<ShopManager>.I.isNeedShowBundleOffer())
		{
			GameSaveData.instance.showHomeBannerOfferDay = DateTime.UtcNow.Day;
			MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = false;
			DispatchEvent("BANNER_OFFER");
			return true;
		}
		if (needFollowCheck)
		{
			needFollowCheck = false;
			if (CheckMutualFollowBySNS())
			{
				return true;
			}
		}
		if (needCountdown && Singleton<CountdownTable>.IsValid())
		{
			needCountdown = false;
			CountdownTable.CountdownData countdownData = Singleton<CountdownTable>.I.GetCountdownData(TimeManager.GetNow());
			int @int = PlayerPrefs.GetInt("COUNTDOWN_SHOWED_REMAIN", -1);
			if (countdownData != null && countdownData.imageID != @int)
			{
				DispatchEvent("COUNTDOWN", countdownData.imageID);
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
			DispatchEvent("ALERT_MESSAGE");
			return true;
		}
		if (needCheckNotifyQuestRemain)
		{
			CheckNotifyQuestRemainTime();
			needCheckNotifyQuestRemain = false;
			return true;
		}
		if (needShowDailyDelivery)
		{
			if (GameSaveData.instance.IsRecommendedDailyDeliveryCheckAtHome())
			{
				DispatchEvent("TO_QUEST", "DAILY");
				GameSaveData.instance.recommendedDailyDeliveryCheckAtHome = 0;
			}
			needShowDailyDelivery = false;
			return true;
		}
		if (needShowAppReviewAppeal)
		{
			if (!GameSaveData.instance.ratingPopupHaveShow && GameSaveData.instance.happyTimeForRating)
			{
				DispatchEvent("OPEN_REVIEW_DIALOG");
				GameSaveData.instance.ratingPopupHaveShow = true;
			}
			needShowAppReviewAppeal = false;
			return true;
		}
		if (needShowShadowChallengeFirst)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.isShadowChallengeFirst)
			{
				DispatchEvent("OPEN_SHADOW_CHALLENGE");
			}
			needShowShadowChallengeFirst = false;
			return true;
		}
		if (GameSaveData.instance.showUnlockQuestEvent)
		{
			DispatchEvent("QUEST_UNLOCK");
			GameSaveData.instance.showUnlockQuestEvent = false;
			return true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.needBlackMarketNotifi)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.needBlackMarketNotifi = false;
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37u), string.Empty);
		}
		return false;
	}

	private bool IsValidDispatchEventInUpdate()
	{
		if (!HomeSelfCharacter.CTRL)
		{
			return false;
		}
		if (executeTutorialEnd)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage())
		{
			return false;
		}
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			return false;
		}
		if (!IsCurrentSectionHomeOrLounge())
		{
			return false;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST)))
		{
			return false;
		}
		if (_isWaitingLoginBonus)
		{
			Debug.LogWarning((object)"_isWaitingLoginBonus");
			return false;
		}
		return true;
	}

	private bool IsValidGachaDeco()
	{
		if (!HomeSelfCharacter.CTRL)
		{
			return false;
		}
		if (executeTutorialEnd)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage())
		{
			return false;
		}
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_END))
		{
			return false;
		}
		return true;
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
		InitializeChat();
		if (MonoBehaviourSingleton<UIManager>.I.bannerView != null && TutorialStep.HasAllTutorialCompleted() && !HomeTutorialManager.ShouldRunGachaTutorial())
		{
			MonoBehaviourSingleton<UIManager>.I.bannerView.Open();
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationButton.Open();
		}
		if (shouldFrameInNPC006)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.shouldDispAdvancedOffer())
			{
				RequestEvent("ADVANCED_OFFER");
			}
			FrameInNPC006();
		}
		if (TutorialStep.HasAllTutorialCompleted())
		{
			MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Open();
			if (GameSaveData.instance.canShowWheelFortune)
			{
				MonoBehaviourSingleton<UIManager>.I.fortuneWheelButton.Open();
			}
		}
	}

	protected override void OnCloseStart()
	{
		if (MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		if (MonoBehaviourSingleton<UIManager>.I.bannerView != null)
		{
			MonoBehaviourSingleton<UIManager>.I.bannerView.Close();
		}
		MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Close();
		MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
		MonoBehaviourSingleton<UIManager>.I.fortuneWheelButton.Close();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<PuniConManager>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<PuniConManager>.I.get_gameObject());
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.RemoveObserver(this);
		}
		if (MonoBehaviourSingleton<GachaDecoManager>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<GachaDecoManager>.I);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_EQUIP_CHANGE | NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE | NOTIFY_FLAG.UPDATE_DELIVERY_OVER | NOTIFY_FLAG.UPDATE_TASK_LIST;
	}

	private void UpdateBalloon()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		if (questBalloon != null)
		{
			if (TutorialStep.HasAllTutorialCompleted() && (GameSaveData.instance.IsRecommendedDeliveryCheck() || MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableNormalDeliveryNum() > 0))
			{
				SetBalloonPosition(questBalloon, questIconPos);
			}
			else
			{
				questBalloon.get_parent().get_gameObject().SetActive(false);
				questBalloon = null;
				RefreshUI();
			}
		}
		else if (storyBalloon != null)
		{
			if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(new DELIVERY_TYPE[1]
			{
				DELIVERY_TYPE.STORY
			}))
			{
				SetBalloonPosition(storyBalloon, questIconPos);
			}
			else
			{
				storyBalloon.get_parent().get_gameObject().SetActive(false);
				storyBalloon = null;
				RefreshUI();
			}
		}
		if (orderBalloon != null)
		{
			if (GameSaveData.instance.IsRecommendedChallengeCheck())
			{
				SetBalloonPosition(orderBalloon, orderIconPos);
			}
			else if (GameSaveData.instance.IsRecommendedOrderCheck())
			{
				SetBalloonPosition(orderBalloon, orderIconPos);
			}
			else
			{
				orderBalloon.get_parent().get_gameObject().SetActive(false);
				orderBalloon = null;
			}
		}
		if (eventBalloon != null)
		{
			if (HasNotCheckedEvent())
			{
				SetBalloonPosition(eventBalloon, eventIconPos);
			}
			else
			{
				eventBalloon.get_parent().get_gameObject().SetActive(false);
				eventBalloon = null;
			}
		}
		if (pointShopBalloon != null)
		{
			if (iHomeManager != null && iHomeManager.IsPointShopOpen)
			{
				SetBalloonPosition(pointShopBalloon, pointShopIconPos);
			}
			else
			{
				pointShopBalloon.get_parent().get_gameObject().SetActive(false);
				pointShopBalloon = null;
			}
		}
		if (bingoBalloon != null)
		{
			if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist())
			{
				SetBalloonPosition(bingoBalloon, bingoIconPos);
				return;
			}
			bingoBalloon.get_parent().get_gameObject().SetActive(false);
			bingoBalloon = null;
		}
	}

	protected void SetBalloonPosition(Transform balloon, Vector3 iconPos)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(iconPos));
		position.z = ((!(position.z >= 0f)) ? (-100f) : 0f);
		balloon.set_position(position);
	}

	private void UpdateEventBalloon()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (!TutorialStep.HasAllTutorialCompleted() || waitEventBalloon || !HasNotCheckedEvent())
		{
			return;
		}
		if (currentEventBalloonType != 0)
		{
			if (currentEventBalloonType == GetEventBalloonType())
			{
				return;
			}
			eventBalloon.get_parent().get_gameObject().SetActive(false);
			eventBalloon = null;
		}
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/EVENT_ICON_POS");
			if (val != null)
			{
				eventIconPos = val.get_position();
			}
		}
		currentEventBalloonType = GetEventBalloonType();
		eventBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateEventBalloon(GetCtrl(UI.OBJ_BALOON_ROOT), currentEventBalloonType);
		if (eventBalloon != null)
		{
			ResetTween(eventBalloon);
			PlayTween(eventBalloon, forward: true, null, is_input_block: false);
		}
	}

	private UI_Common.EVENT_BALLOON_TYPE GetEventBalloonType()
	{
		return (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum() <= 0) ? UI_Common.EVENT_BALLOON_TYPE.NEW : UI_Common.EVENT_BALLOON_TYPE.COMPLETABLE;
	}

	private bool HasNotCheckedEvent()
	{
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		if (eventList != null && eventList.Count > 0)
		{
			int i = 0;
			for (int count = eventList.Count; i < count; i++)
			{
				Network.EventData eventData = eventList[i];
				if (!eventData.readPrologueStory)
				{
					return true;
				}
			}
		}
		return MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum() > 0;
	}

	private void SetupNotice()
	{
		noticeTransform = GetCtrl(UI.OBJ_NOTICE);
		noticeObject = noticeTransform.get_gameObject();
		noticeTween = base.GetComponent<TweenAlpha>((Enum)UI.OBJ_NOTICE);
		SetActive((Enum)UI.OBJ_NOTICE, is_visible: false);
	}

	private void SetUpBonusTime()
	{
		Transform ctrl = GetCtrl(UI.OBJ_BONUS_TIME_ROOT);
		if (!(ctrl == null))
		{
			bonusTime = ctrl.get_gameObject().AddComponent<HomeTopBonusTime>();
			bonusTime.InitUI();
			bonusTime.SetUp();
		}
	}

	protected void OnNoticeAreaEvent(HomeStageAreaEvent area_event)
	{
		if (TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && !(TutorialMessage.GetCursor() != null))
		{
			noticeEventTo = area_event;
		}
	}

	private void UpdateNotice()
	{
		if (!(noticeTransform == null))
		{
			UpdateNoticeStatus();
			if (noticeObject.get_activeSelf())
			{
				UpdateNoticePosition();
			}
		}
	}

	private void UpdateNoticeStatus()
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (noticeEvent == noticeEventTo || noticeTween.get_enabled())
		{
			return;
		}
		if (noticeTween.value != 0f)
		{
			noticeTween.PlayReverse();
			return;
		}
		if (noticeEventTo == null)
		{
			noticeObject.SetActive(false);
			noticeEvent = noticeEventTo;
			return;
		}
		if (!IsShowNoticeByArena() && !(this is LoungeTop))
		{
			noticeObject.SetActive(false);
			noticeEvent = noticeEventTo;
			return;
		}
		SetLabelText((Enum)UI.LBL_NOTICE, base.sectionData.GetText(noticeEventTo.eventName));
		noticePos = noticeEventTo._transform.get_position();
		ref Vector3 reference = ref noticePos;
		reference.y += noticeEventTo.noticeViewHeight;
		UpdateNoticeLock();
		if (!string.IsNullOrEmpty(noticeEventTo.noticeButtonName))
		{
			SetActive((Enum)UI.OBJ_BUTTON_NOTICE, is_visible: true);
			SetActive((Enum)UI.OBJ_NORMAL_NOTICE, is_visible: false);
			SetActiveAreaEventButton(noticeEventTo.noticeButtonName, active: true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_BUTTON_NOTICE, is_visible: false);
			SetActive((Enum)UI.OBJ_NORMAL_NOTICE, is_visible: true);
		}
		noticeObject.SetActive(true);
		noticeTween.PlayForward();
		noticeEvent = noticeEventTo;
	}

	private bool IsShowNoticeByArena()
	{
		if (!noticeEventTo.eventName.Contains("ARENA_LIST"))
		{
			return true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen)
		{
			return true;
		}
		return false;
	}

	private void UpdateNoticeLock()
	{
		SetActive((Enum)UI.OBJ_NOTICE_LOCK, is_visible: false);
		if (noticeEventTo.eventName.Contains("ARENA_LIST") && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50 && MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen)
		{
			SetActive((Enum)UI.OBJ_NOTICE_LOCK, is_visible: true);
			SetLabelText((Enum)UI.LBL_NOTICE_LOCK, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, 50) + "で解放");
		}
	}

	private void UpdateNoticePosition()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(noticePos));
		position.z = ((!(position.z >= 0f)) ? (-100f) : 0f);
		noticeTransform.set_position(position);
	}

	protected virtual void SetActiveAreaEventButton(string btnName, bool active)
	{
	}

	private void CheckNotifyQuestRemainTime()
	{
		if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() == 0)
		{
			return;
		}
		TimeSpan minRemainTime = TimeSpan.MaxValue;
		List<ulong> ids = new List<ulong>();
		MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo item)
		{
			foreach (float remainTime in item.remainTimes)
			{
				float num3 = remainTime;
				TimeSpan timeSpan = TimeSpan.FromSeconds(num3);
				if (minRemainTime.CompareTo(timeSpan) == 1)
				{
					minRemainTime = timeSpan;
					ids.Clear();
					ids.Add(item.uniqueID);
				}
				else if (minRemainTime.CompareTo(timeSpan) == 0)
				{
					ids.Add(item.uniqueID);
				}
			}
		});
		int num = int.MaxValue;
		int[] nOTIFY_QUEST_REMAIN_DAY = GameDefine.NOTIFY_QUEST_REMAIN_DAY;
		foreach (int num2 in nOTIFY_QUEST_REMAIN_DAY)
		{
			if (minRemainTime.TotalDays < (double)num2 && num2 < num)
			{
				num = num2;
			}
		}
		if (num == int.MaxValue)
		{
			GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
		}
		else if (GameSaveData.instance.isIncludeNotifyQuestID(ids))
		{
			if (num == GameSaveData.instance.lastRemainDayThreshold)
			{
				GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
				return;
			}
			string remainText = GetRemainText(minRemainTime);
			DispatchEvent("NOTICE_QUEST_REMAIN", new object[1]
			{
				remainText
			});
			GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
		}
		else
		{
			string remainText2 = GetRemainText(minRemainTime);
			DispatchEvent("NOTICE_QUEST_REMAIN", new object[1]
			{
				remainText2
			});
			GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
		}
	}

	private string GetRemainText(TimeSpan minRemainTime)
	{
		if (minRemainTime.TotalDays > 1.0)
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 9u), minRemainTime.Days);
		}
		return string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10u), minRemainTime.Hours);
	}

	protected virtual void FrameInNPC006()
	{
		npc06Info = iHomeManager.IHomePeople.GetHomeNPCCharacter(6);
		shouldFrameInNPC006 = false;
		this.StartCoroutine(_FrameInNPC006());
	}

	private IEnumerator _FrameInNPC006()
	{
		PlayerAnimCtrl animCtrl = npc06Info.loader.GetAnimator().GetComponent<PlayerAnimCtrl>();
		PLCA defaultAnim = animCtrl.defaultAnim;
		AnimatorCullingMode cullingMode = animCtrl.animator.get_cullingMode();
		while (npc06Info.IsLeaveState())
		{
			yield return null;
		}
		Transform moveTransform = Utility.Find(npc06Info._transform, "Move");
		int i = 0;
		for (int childCount = moveTransform.get_childCount(); i < childCount; i++)
		{
			Transform child = moveTransform.GetChild(i);
			if (child.get_name().StartsWith("LIB"))
			{
				Object.Destroy(child.get_gameObject());
				break;
			}
		}
		npc06Info.get_gameObject().SetActive(true);
		npc06Info.PushOutControll();
		npc06Info._transform.set_localPosition(npc06Info.npcInfo.GetSituation().pos);
		npc06Info._transform.set_localEulerAngles(new Vector3(0f, npc06Info.npcInfo.GetSituation().rot, 0f));
		animCtrl.animator.set_cullingMode(0);
		animCtrl.Play(PLCA.EVENT_MOVE, instant: true);
		bool wait = true;
		Action<PlayerAnimCtrl, PLCA> onEnd = delegate(PlayerAnimCtrl pac, PLCA plca)
		{
			if (plca == PLCA.EVENT_MOVE)
			{
				wait = false;
			}
		};
		Action<PlayerAnimCtrl, PLCA> origOnEnd = animCtrl.onEnd;
		animCtrl.onEnd = onEnd;
		while (wait)
		{
			yield return null;
		}
		animCtrl.onEnd = origOnEnd;
		animCtrl.animator.set_cullingMode(cullingMode);
		animCtrl.defaultAnim = defaultAnim;
		animCtrl.Play(PLCA.EVENT_IDLE);
		npc06Info.PopState();
		HomeDragonRandomMove move = npc06Info.loader.GetAnimator().get_gameObject().AddComponent<HomeDragonRandomMove>();
		move.Reset();
		waitEventBalloon = false;
		UpdateEventBalloon();
	}

	protected virtual void InitializeChat()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
			MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver(this);
			UIButton component = GetCtrl(UI.BTN_CHAT).GetComponent<UIButton>();
			if (component != null)
			{
				component.onClick.Clear();
			}
			AddChatClickDelegate(component);
		}
	}

	protected abstract void AddChatClickDelegate(UIButton button);

	protected bool StopEventUntilTheAllTutorialCompleted()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor() != null || (homeTutorialManager != null && HomeTutorialManager.ShouldRunGachaTutorial()) || (homeTutorialManager != null && HomeTutorialManager.ShouldRunQuestShadowTutorial())))
		{
			GameSection.StopEvent();
			return true;
		}
		return false;
	}

	private void TutorialStep_6()
	{
		if (TutorialStep.IsPlayingStudioTutorial())
		{
			executeTutorialStep6 = false;
			DispatchEvent("TUTORIAL_STEP_6");
		}
	}

	private void TutorialStep_END()
	{
		if (!(MonoBehaviourSingleton<UIManager>.I.tutorialMessage == null))
		{
			executeTutorialEnd = false;
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialEnd", delegate
			{
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
					{
						MonoBehaviourSingleton<UIManager>.I.mainStatus.SetMenuButtonEnable(is_enable: true);
						RefreshUI();
						if (MonoBehaviourSingleton<QuestManager>.I.ExistsExploreEvent())
						{
							Protocol.Force(delegate
							{
								MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialBit(TUTORIAL_MENU_BIT.EXPLORE);
							});
						}
					});
				});
			});
		}
	}

	protected void UpdateCommunityBadge()
	{
		Transform ctrl = GetCtrl(UI.BTN_COMMUNITY);
		if (!(ctrl == null) && ctrl.get_gameObject().get_activeSelf())
		{
			int num = MonoBehaviourSingleton<UserInfoManager>.I.clanRequestNum;
			if (num < 0)
			{
				num = 0;
			}
			SetBadge(ctrl, num, 3, -8, -8);
		}
	}

	private void TutorialClaimReward()
	{
		if (!(MonoBehaviourSingleton<UIManager>.I.tutorialMessage == null))
		{
			executeTutorialClaimReward = false;
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "ClaimReward");
		}
	}

	protected abstract void CheckEventLock();

	private void OnQuery_COMPLETE_READ_STORY()
	{
		int scriptId = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<DeliveryManager>.I.SendReadStoryRead(scriptId, delegate(bool is_success, Error recv_reward)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_TO_GIFTBOX()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_TUTORIAL_STEP_6()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
		{
			GameSection.ResumeEvent(is_resume: false);
			string section_name = "TutorialStep6_1";
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.EQUIP_CREATE_07))
			{
				section_name = "TutorialStep6_1_2";
			}
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", section_name, delegate
			{
				TutorialStep.isSendFirstRewardComplete = false;
				MonoBehaviourSingleton<UIManager>.I.UpdateMainUI();
				MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateMainMenu();
				RefreshUI();
				MonoBehaviourSingleton<UIManager>.I.tutorialMessage.UpdateFocusCursol();
			});
		});
	}

	private void OnQuery_FIELD()
	{
		_OnQuery_FIELD(fromQuest: false);
	}

	private void OnQuery_TO_FIELD()
	{
		if (!StopEventUntilTheAllTutorialCompleted())
		{
			OnQuery_FIELD();
		}
	}

	private void OnQuery_SHOP()
	{
		StopEventUntilTheAllTutorialCompleted();
	}

	private void OnQuery_TO_SHOP()
	{
		StopEventUntilTheAllTutorialCompleted();
	}

	private void OnQuery_STATUS()
	{
		StopEventUntilTheAllTutorialCompleted();
	}

	private void OnQuery_TO_STATUS()
	{
		StopEventUntilTheAllTutorialCompleted();
	}

	private void OnQuery_FRIEND()
	{
		StopEventUntilTheAllTutorialCompleted();
	}

	private void OnQuery_HOME_FRIENDS()
	{
		StopEventUntilTheAllTutorialCompleted();
	}

	private void OnQuery_QUEST_COUNTER_AREA()
	{
		fromQuestCounterAreaEvent = true;
		GameSection.ChangeEvent("QUEST_COUNTER");
		OnQuery_QUEST_COUNTER();
	}

	private void OnQuery_BINGO()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
		GameSection.StayEvent();
		MonoBehaviourSingleton<QuestManager>.I.SendGetBingoEventList(delegate
		{
			List<Network.EventData> validBingoDataListInSection = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
			if (validBingoDataListInSection != null && validBingoDataListInSection.Count > 0)
			{
				if (validBingoDataListInSection.Count == 1)
				{
					Network.EventData firstEvent = validBingoDataListInSection[0];
					List<DeliveryTable.DeliveryData> deliveryTableDataList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryTableDataList(do_sort: false);
					List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
					int num = (from d in deliveryTableDataList
					where d.IsEvent() && d.eventID == firstEvent.eventId
					select d).Count();
					int num2 = 0;
					for (int i = 0; i < clearStatusDelivery.Count; i++)
					{
						ClearStatusDelivery clearStatusDelivery2 = clearStatusDelivery[i];
						DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)clearStatusDelivery2.deliveryId);
						if (deliveryTableData == null)
						{
							Log.Warning("DeliveryTable Not Found : dId " + clearStatusDelivery2.deliveryId);
						}
						else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == firstEvent.eventId && clearStatusDelivery2.deliveryStatus == 3)
						{
							num2++;
						}
					}
					if (num + num2 == 18)
					{
						GameSection.ChangeStayEvent("MINI_BINGO");
					}
				}
				else
				{
					GameSection.ChangeStayEvent("MINI_BINGO");
				}
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	private void OnQuery_QUEST_COUNTER()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2, delegate
			{
				if (homeTutorialManager != null)
				{
					homeTutorialManager.ForceDeleteArrow(isPamela: true);
				}
			});
		}
		fromQuestCounterAreaEvent = true;
		bool flag = !fromQuestCounterAreaEvent;
		fromQuestCounterAreaEvent = false;
		if (flag)
		{
			if (StopEventUntilTheAllTutorialCompleted())
			{
				return;
			}
		}
		else if (homeTutorialManager != null)
		{
			if (HomeTutorialManager.ShouldRunGachaTutorial() || HomeTutorialManager.ShouldRunQuestShadowTutorial())
			{
				GameSection.StopEvent();
				return;
			}
			homeTutorialManager.CloseDialog();
		}
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
		OnTalkPamelaTutorial = false;
		if (mdlArrow != null)
		{
			Object.Destroy(mdlArrow.get_gameObject());
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && OnAfterGacha2Tutorial)
		{
			OnAfterGacha2Tutorial = false;
			if (homeTutorialManager != null)
			{
				homeTutorialManager.DisableTargetArea();
			}
		}
	}

	private void OnQuery_EVENT_COUNTER_AREA()
	{
		fromQuestCounterAreaEvent = true;
		GameSection.ChangeEvent("EVENT_COUNTER");
		OnQuery_EVENT_COUNTER();
	}

	private void OnQuery_EVENT_COUNTER()
	{
		bool flag = !fromQuestCounterAreaEvent;
		fromQuestCounterAreaEvent = false;
		if (flag)
		{
			StopEventUntilTheAllTutorialCompleted();
		}
		else if (homeTutorialManager != null)
		{
			if (HomeTutorialManager.DoesTutorial() || HomeTutorialManager.ShouldRunGachaTutorial())
			{
				GameSection.StopEvent();
				return;
			}
			homeTutorialManager.CloseDialog();
		}
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	private void OnQuery_GACHA_QUEST_COUNTER_AREA()
	{
		fromQuestCounterAreaEvent = true;
		GameSection.ChangeEvent("GACHA_QUEST_COUNTER");
		OnQuery_GACHA_QUEST_COUNTER();
	}

	private void OnQuery_GACHA_QUEST_COUNTER()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST, delegate
			{
				if (homeTutorialManager != null)
				{
					homeTutorialManager.ForceDeleteArrow(isPamela: false);
				}
			});
		}
		fromQuestCounterAreaEvent = true;
		bool flag = !fromQuestCounterAreaEvent;
		fromQuestCounterAreaEvent = false;
		if (flag)
		{
			StopEventUntilTheAllTutorialCompleted();
		}
		else
		{
			if (homeTutorialManager != null)
			{
				if (HomeTutorialManager.DoesTutorial())
				{
					GameSection.StopEvent();
					return;
				}
				homeTutorialManager.CloseDialog();
			}
			OnClickQuestForTutorial = false;
			if (mdlArrowQuest != null)
			{
				Object.Destroy(mdlArrowQuest.get_gameObject());
			}
		}
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	private void OnQuery_EXPLORE()
	{
		if (!StopEventUntilTheAllTutorialCompleted() && !GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	private void OnQuery_GUILD_SETTING()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId != -1)
		{
			MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = true;
		}
	}

	private void OnQuery_BONUS_TIME()
	{
		bonusTime.OnTap();
	}

	private void UpdateBonusTime()
	{
		if (!(bonusTime == null))
		{
			bonusTime.UpdateBonusTime();
		}
	}

	private void OnQuery_NONE()
	{
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep6_2");
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
		{
			triger_tutorial_gacha_1 = true;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
		{
			triger_tutorial_force_item = true;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.DONE_CHANGE_WEAPON) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
		{
			triger_tutorial_change_item = true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
		{
			triger_tutorial_gacha_2 = true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			triger_tutorial_upgrade = true;
		}
	}

	protected void OnQuery_POINT_SHOP_FROM_BUTTON()
	{
		if (iHomeManager == null || !iHomeManager.IsPointShopOpen)
		{
			GameSection.StopEvent();
		}
		else if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	protected void OnQuery_GUILD_REQUEST()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	private void OnQuery_MUTUAL_FOLLOW()
	{
		string mutualFollowValue = MonoBehaviourSingleton<FriendManager>.I.MutualFollowValue;
		if (!string.IsNullOrEmpty(mutualFollowValue))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<FriendManager>.I.SendMutualFollow(mutualFollowValue, delegate(bool is_success)
			{
				if (!is_success)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
				}
				GameSection.ResumeEvent(is_success);
			});
		}
	}

	private void OnQuery_ALERT_MESSAGE()
	{
		GameSection.SetEventData(MonoBehaviourSingleton<UserInfoManager>.I.alertMessage);
		MonoBehaviourSingleton<UserInfoManager>.I.OnReadAlertMessage();
	}

	private void OnQuery_AlertMessage_OK()
	{
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.alertMessage))
		{
			RequestEvent("ALERT_MESSAGE");
		}
	}

	private void OnQuery_BANNER_NEXT()
	{
		MonoBehaviourSingleton<UIManager>.I.bannerView.NextBanner(2);
	}

	private void OnQuery_BANNER_PREV()
	{
		MonoBehaviourSingleton<UIManager>.I.bannerView.NextBanner(2, forward: false);
	}

	private void OnQuery_HomeToFieldConfirm_YES()
	{
		OnQuery_FIELD();
	}

	private void OnQuery_FRIEND_PROMOTION()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnCloseDialog_QuestAcceptDeliveryDetail()
	{
		if (TutorialStep.IsPlayingFirstReward())
		{
			executeTutorialStep6 = true;
		}
	}

	private void OnCloseDialog_HomeNoticeNewDelivery()
	{
		if (!TutorialMessageTable.HasReadTutorialEnd())
		{
			executeTutorialEnd = true;
		}
		if (GameSaveData.instance.IsRecommendedDeliveryCheck())
		{
			GameSaveData.instance.recommendedDeliveryCheck = 0;
			GameSaveData.Save();
			RefreshUI();
		}
	}

	protected virtual void OnQuery_COMMUNITY()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader())
		{
			GameSection.ChangeEvent("COMMUNITY_LEADER");
		}
		GameSection.StayEvent();
		int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(id, delegate(UserClanData userClanData)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
			GameSection.ResumeEvent(userClanData != null);
		});
	}

	private bool CheckShowHomeBanner()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList.Count <= 0)
		{
			return false;
		}
		string text = ";";
		string text2 = "-";
		double value = 14400.0;
		DateTime dateTime = DateTime.Now.AddSeconds(value);
		int i = 0;
		for (int count = MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList.Count; i < count; i++)
		{
			Network.HomeBanner homeBanner = MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList[i];
			if (homeBanner == null)
			{
				continue;
			}
			bool flag = true;
			string text3 = "BANNDER_ID_" + homeBanner.bannerId.ToString();
			if (GameSaveData.instance != null && GameSaveData.instance.showHomeBanners != null && GameSaveData.instance.showHomeBanners.Contains(text3))
			{
				flag = false;
				string[] array = GameSaveData.instance.showHomeBanners.Split(';');
				if (array != null && array.Length > 0)
				{
					int j = 0;
					for (int num = array.Length; j < num; j++)
					{
						if (string.IsNullOrEmpty(array[j]))
						{
							continue;
						}
						string[] array2 = array[j].Split('-');
						if (array2 == null || array2.Length <= 1 || !(array2[0] == text3))
						{
							continue;
						}
						DateTime.TryParse(array2[1], out DateTime result);
						if ((DateTime.Now - result).Milliseconds <= 0)
						{
							continue;
						}
						HOME_TYPE homeType = (HOME_TYPE)homeBanner.homeType;
						if (homeType == HOME_TYPE.PURCHASE_BUNDLE)
						{
							string purchaseBundle = GetPurchaseBundle(homeBanner.targetString);
							if (string.IsNullOrEmpty(purchaseBundle))
							{
								return false;
							}
							homeBanner.targetString = purchaseBundle;
						}
						string showHomeBanners = GameSaveData.instance.showHomeBanners;
						int num2 = GameSaveData.instance.showHomeBanners.IndexOf(text3) + array2[0].Length + array2[1].Length + 2;
						showHomeBanners = showHomeBanners.Substring(num2, showHomeBanners.Length - num2);
						string text4 = showHomeBanners;
						showHomeBanners = text4 + text3 + text2 + DateTime.Now.AddSeconds(value);
						showHomeBanners += text;
						GameSaveData.instance.showHomeBanners = showHomeBanners;
						GameSaveData.Save();
						DispatchEvent("HOME_BANNER", homeBanner);
						return true;
					}
				}
			}
			if (!flag)
			{
				continue;
			}
			HOME_TYPE homeType2 = (HOME_TYPE)homeBanner.homeType;
			if (homeType2 == HOME_TYPE.PURCHASE_BUNDLE)
			{
				string purchaseBundle2 = GetPurchaseBundle(homeBanner.targetString);
				if (string.IsNullOrEmpty(purchaseBundle2))
				{
					return false;
				}
				homeBanner.targetString = purchaseBundle2;
			}
			text3 = text3 + text2 + dateTime;
			GameSaveData instance = GameSaveData.instance;
			instance.showHomeBanners = instance.showHomeBanners + text3 + text;
			DispatchEvent("HOME_BANNER", homeBanner);
			return true;
		}
		return false;
	}

	public string GetPurchaseBundle(string targetString)
	{
		string result = "bundle";
		if (string.IsNullOrEmpty(targetString))
		{
			return result;
		}
		string[] array = targetString.Split(',');
		if (array == null || array.Length <= 0)
		{
			return result;
		}
		if (!MonoBehaviourSingleton<ShopManager>.IsValid() || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList == null || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList == null || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Count <= 0)
		{
			return result;
		}
		List<ProductData> list = (from o in MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList
		where o.productType == 2
		select o).ToList();
		int num = 0;
		int num2 = array.Length;
		int num3 = 0;
		int count = list.Count;
		bool flag = false;
		string targetId;
		for (num = 0; num < num2; num++)
		{
			targetId = array[num];
			if (string.IsNullOrEmpty(targetId))
			{
				continue;
			}
			targetId = targetId.Trim();
			ProductDataTable.PackInfo packInfo = Singleton<ProductDataTable>.I.packs.Find((ProductDataTable.PackInfo data) => data.bundleId == targetId);
			if (packInfo == null)
			{
				continue;
			}
			flag = true;
			for (num3 = 0; num3 < count; num3++)
			{
				ProductData productData = list[num3];
				if (productData != null && !string.IsNullOrEmpty(productData.productId) && productData.productId.Trim().Equals(targetId))
				{
					return targetId;
				}
			}
		}
		if (flag)
		{
			return null;
		}
		return result;
	}
}
