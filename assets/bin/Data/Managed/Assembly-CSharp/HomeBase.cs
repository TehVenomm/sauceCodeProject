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
		BTN_MISSION,
		BTN_TICKET,
		BTN_GIFTBOX,
		BTN_CHAT,
		OBJ_BALOON_ROOT,
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
		OBJ_GUILD,
		BTN_GUILD_NO_GUILD,
		BTN_GUILD,
		SPR_LOCK_GUILD,
		SPR_GUILD_EMBLEM_1,
		SPR_GUILD_EMBLEM_2,
		SPR_GUILD_EMBLEM_3,
		SPR_BADGE
	}

	public static readonly string QuestBalloonName = "QUEST_COUNTER_BALLOON";

	public static bool OnTalkPamelaTutorial;

	public static bool OnAfterGacha2Tutorial;

	private Transform mdlArrow;

	private List<LoginBonus> limitedLoginBonus;

	private bool triger_tutorial_gacha_1;

	private bool triger_tutorial_force_item;

	private bool triger_tutorial_gacha_2;

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

	private bool fromQuestCounterAreaEvent;

	private HomeTopBonusTime bonusTime;

	public override bool useOnPressBackKey => true;

	public override void OnPressBackKey()
	{
		Native.applicationQuit();
	}

	public override void InitializeReopen()
	{
		base.InitializeReopen();
		StartCoroutine(IESetupLoginBonus());
	}

	private IEnumerator IESetupLoginBonus()
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)new WaitForSeconds(0.04f);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && limitedLoginBonus != null && limitedLoginBonus.Count > 0)
		{
			SetupLoginBonus();
		}
		if (HomeTutorialManager.DoesTutorialAfterGacha2())
		{
			homeTutorialManager.ExcuteDoSetupTutorialAfterGacha2();
		}
		yield return (object)null;
	}

	public override void Initialize()
	{
		DestroyInGameTutorialManager();
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<InGameManager>.I.selfCacheObject != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(MonoBehaviourSingleton<InGameManager>.I.selfCacheObject);
		}
		MonoBehaviourSingleton<InventoryManager>.I.SetList();
		NetworkNative.createRegistrationId();
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = false;
		}
		MonoBehaviourSingleton<StatusManager>.I.SetUserStatus();
		if (MonoBehaviourSingleton<SmithManager>.IsValid())
		{
			MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(true);
		}
		SetupNotice();
		if (MonoBehaviourSingleton<ShopManager>.IsValid() && !MonoBehaviourSingleton<ShopManager>.I.HasCheckPromotionItem)
		{
			MonoBehaviourSingleton<ShopManager>.I.SendCheckPromotion();
		}
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.tutorialMessage != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetErrorResendQuestGachaFlag();
		}
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
		}
		StartCoroutine(DoInitialize());
	}

	public override void StartSection()
	{
		SetupPointShop();
		SetUpBingo();
		CheckEventLock();
		if (!CheckOpenGacha() && !CheckNeededGotoGacha() && !CheckNeededOpenQuest() && !CheckJoinClanIngame() && !CheckInvitedClanBySNS() && !CheckInvitedPartyBySNS() && !CheckInvitedLoungeBySNS() && !CheckMutualFollowBySNS())
		{
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04) && !TutorialStep.HasDeliveryRewardCompleted())
			{
				DispatchEvent("QUEST_COUNTER", null);
			}
			else if (!TutorialStep.HasChangeEquipCompleted())
			{
				if (TutorialStep.IsPlayingStudioTutorial() && !TutorialStep.isSendFirstRewardComplete)
				{
					TutorialStep_6();
				}
			}
			else
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP))
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
		}
	}

	public override void UpdateUI()
	{
		SetFontStyle(UI.LBL_NOTICE, FontStyle.Italic);
		UpdateUIOfTutorial();
		CheckBalloons();
		if ((UnityEngine.Object)questBalloon != (UnityEngine.Object)null)
		{
			ResetTween(questBalloon, 0);
			PlayTween(questBalloon, true, null, false, 0);
			if ((UnityEngine.Object)storyBalloon != (UnityEngine.Object)null)
			{
				storyBalloon.parent.gameObject.SetActive(false);
				storyBalloon = null;
			}
		}
		else if ((UnityEngine.Object)storyBalloon != (UnityEngine.Object)null)
		{
			ResetTween(storyBalloon, 0);
			PlayTween(storyBalloon, true, null, false, 0);
		}
		if ((UnityEngine.Object)orderBalloon != (UnityEngine.Object)null)
		{
			ResetTween(orderBalloon, 0);
			PlayTween(orderBalloon, true, null, false, 0);
		}
		UpdateEventBalloon();
		UpdateTicketNum();
		UpdateGuildRequest();
		UpdatePointShop();
		UpdateGiftboxNum();
	}

	private void UpdateGuildBtn()
	{
		SetActive(UI.OBJ_GUILD, true);
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId != -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null)
			{
				SetActive(UI.BTN_GUILD, true);
				SetActive(UI.BTN_GUILD_NO_GUILD, false);
				SetActive(UI.SPR_LOCK_GUILD, false);
				UpdateClanBadge();
				SetSprite(UI.SPR_GUILD_EMBLEM_1, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[0]));
				SetSprite(UI.SPR_GUILD_EMBLEM_2, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[1]));
				SetSprite(UI.SPR_GUILD_EMBLEM_3, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[2]));
			}
			else
			{
				SetActive(UI.BTN_GUILD, false);
				SetActive(UI.BTN_GUILD_NO_GUILD, true);
				SetActive(UI.SPR_LOCK_GUILD, false);
			}
		}
		else
		{
			SetActive(UI.BTN_GUILD, false);
			SetActive(UI.BTN_GUILD_NO_GUILD, false);
			SetActive(UI.SPR_LOCK_GUILD, true);
		}
	}

	private void UpdateClanBadge()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guilMemberList != null)
		{
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD), UI.SPR_BADGE, MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters != null && MonoBehaviourSingleton<GuildManager>.I.guilMemberList.result.requesters.Count > 0);
		}
		else
		{
			SetActive(FindCtrl(base._transform, UI.BTN_GUILD), UI.SPR_BADGE, false);
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.CHANGED_SCENE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
			{
				if (!MonoBehaviourSingleton<UIManager>.I.mainChat.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.mainChat.Open(UITransition.TYPE.OPEN);
				}
				MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_EVENT_BANNER) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.bannerView != (UnityEngine.Object)null && !MonoBehaviourSingleton<UIManager>.I.bannerView.isOpen && TutorialStep.HasAllTutorialCompleted() && !HomeTutorialManager.ShouldRunGachaTutorial())
			{
				MonoBehaviourSingleton<UIManager>.I.bannerView.Open(UITransition.TYPE.OPEN);
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_PARTY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.invitationButton != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen && IsCurrentSectionHomeOrLounge())
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Open(UITransition.TYPE.OPEN);
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Close(UITransition.TYPE.CLOSE);
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_RALLY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.invitationButton != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen && IsCurrentSectionHomeOrLounge())
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Open(UITransition.TYPE.OPEN);
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationButton.Close(UITransition.TYPE.CLOSE);
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.RESET_DARK_MARKET) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.blackMarkeButton != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted())
			{
				if (!MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.ResetMarketTime();
					MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Open(UITransition.TYPE.OPEN);
				}
				else if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.ResetMarketTime();
				}
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
				SetBadge(GetCtrl(UI.BTN_MISSION), num, SpriteAlignment.TopLeft, 8, -8, false);
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
		if ((NOTIFY_FLAG.TRANSITION_END & flags) != (NOTIFY_FLAG)0L && MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.ClearAnnounce();
			if (MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.IsKnockDownRaidBossByEventItemCountList())
			{
				MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.PlayKnockDown(false, null);
			}
		}
		base.OnNotify(flags);
	}

	public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && !((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat == (UnityEngine.Object)null))
		{
			if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
			{
				SetBadge(UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNum(), SpriteAlignment.TopLeft, -5, -29, false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(true);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(false);
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
		CheckEventLock();
	}

	private IEnumerator DoInitialize()
	{
		yield return (object)StartCoroutine(WaitInitializeManager());
		CreateSelfCharacter();
		yield return (object)StartCoroutine(LoadTutorialMessage());
		yield return (object)StartCoroutine(CreatePuniCon());
		yield return (object)StartCoroutine(SendHomeInfo());
		if (FieldRewardPool.HasSave())
		{
			FieldRewardPool fieldRewardPool = FieldRewardPool.LoadAndCreate();
			bool wait = true;
			fieldRewardPool.SendFieldDrop(delegate
			{
				((_003CDoInitialize_003Ec__Iterator6C)/*Error near IL_0112: stateMachine*/)._003Cwait_003E__1 = false;
			});
			while (wait)
			{
				yield return (object)null;
			}
		}
		yield return (object)StartCoroutine(WaitLoadHomeCharacters());
		yield return (object)StartCoroutine(DoTutorial());
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
		bool wait_guild = true;
		GuildItemManager.I.Init(delegate
		{
			((_003CDoInitialize_003Ec__Iterator6C)/*Error near IL_0214: stateMachine*/)._003Cwait_guild_003E__2 = false;
		});
		bool wait_info = true;
		MonoBehaviourSingleton<GuildManager>.I.SendClanInfo(delegate
		{
			((_003CDoInitialize_003Ec__Iterator6C)/*Error near IL_0231: stateMachine*/)._003Cwait_info_003E__3 = false;
		});
		while (wait_guild || wait_info)
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<GuildManager>.I.GetClanStat(null);
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15)
		{
			SetActive(UI.OBJ_GUILD, true);
		}
		else
		{
			SetActive(UI.OBJ_GUILD, false);
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
		{
			bool wait_clan_invite = true;
			MonoBehaviourSingleton<GuildManager>.I.SendInvitedGuild(delegate
			{
				((_003CDoInitialize_003Ec__Iterator6C)/*Error near IL_02dc: stateMachine*/)._003Cwait_clan_invite_003E__4 = false;
			}, true);
			while (wait_clan_invite)
			{
				yield return (object)null;
			}
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
		{
			bool wait_clan_donate_invite = true;
			MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList(delegate
			{
				((_003CDoInitialize_003Ec__Iterator6C)/*Error near IL_032d: stateMachine*/)._003Cwait_clan_donate_invite_003E__5 = false;
			}, true);
			while (wait_clan_donate_invite)
			{
				yield return (object)null;
			}
		}
		base.Initialize();
	}

	private IEnumerator DoTutorial()
	{
		bool isDeliveryTutorial = HomeTutorialManager.DoesTutorial();
		if (isDeliveryTutorial)
		{
			homeTutorialManager = base.gameObject.AddComponent<HomeTutorialManager>();
			if (!((UnityEngine.Object)homeTutorialManager == (UnityEngine.Object)null))
			{
				if (isDeliveryTutorial)
				{
					homeTutorialManager.Setup();
					StartCoroutine(SetupArrow());
				}
				while (homeTutorialManager.IsLoading())
				{
					yield return (object)null;
				}
				OnTalkPamelaTutorial = true;
			}
		}
		else if (HomeTutorialManager.ShouldRunGachaTutorial() && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			homeTutorialManager = base.gameObject.GetComponent<HomeTutorialManager>();
			if ((UnityEngine.Object)homeTutorialManager == (UnityEngine.Object)null)
			{
				homeTutorialManager = base.gameObject.AddComponent<HomeTutorialManager>();
			}
			if (!((UnityEngine.Object)homeTutorialManager == (UnityEngine.Object)null))
			{
				homeTutorialManager.SetupGachaQuestTutorial();
				while (homeTutorialManager.IsLoading())
				{
					yield return (object)null;
				}
			}
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
		{
			homeTutorialManager = base.gameObject.AddComponent<HomeTutorialManager>();
			if (!((UnityEngine.Object)homeTutorialManager == (UnityEngine.Object)null))
			{
				homeTutorialManager.Setup();
				StartCoroutine(SetupArrow());
				while (homeTutorialManager.IsLoading())
				{
					yield return (object)null;
				}
				OnAfterGacha2Tutorial = true;
			}
		}
	}

	private IEnumerator SetupArrow()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
		{
			"mdl_arrow_01"
		}, false);
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		Vector3 ARROW_OFFSET = new Vector3(-4.28f, 1.66f, 14.61f);
		Vector3 ARROW_SCALE = new Vector3(4f, 4f, 4f);
		mdlArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		ResourceUtility.Realizes(loadedArrow.loadedObject, mdlArrow, -1);
		mdlArrow.localScale = ARROW_SCALE;
		mdlArrow.position = ARROW_OFFSET;
	}

	private IEnumerator LoadTutorialMessage()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && !((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.tutorialMessage != (UnityEngine.Object)null) && UserInfoManager.IsNeedsTutorialMessage())
		{
			bool loadingTutorialMessage = true;
			MonoBehaviourSingleton<UIManager>.I.LoadTutorialMessage(delegate
			{
				((_003CLoadTutorialMessage_003Ec__Iterator6F)/*Error near IL_0065: stateMachine*/)._003CloadingTutorialMessage_003E__0 = false;
			});
			while (loadingTutorialMessage)
			{
				yield return (object)null;
			}
		}
	}

	protected abstract void CreateSelfCharacter();

	protected abstract IEnumerator WaitInitializeManager();

	private void DestroyInGameTutorialManager()
	{
		InGameTutorialManager component = MonoBehaviourSingleton<AppMain>.I.GetComponent<InGameTutorialManager>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(component);
		}
	}

	protected abstract IEnumerator SendHomeInfo();

	protected abstract IEnumerator WaitLoadHomeCharacters();

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
			}, false);
		}
		yield return (object)load_queue.Wait();
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
		validLoginBonus = (flag && flag2 && !flag3);
	}

	protected virtual void SetIconAndBalloon()
	{
		if ((UnityEngine.Object)MonoBehaviourSingleton<StageManager>.I.stageObject != (UnityEngine.Object)null)
		{
			Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/QUEST_ICON_POS");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				questIconPos = transform.position;
			}
			transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/ORDER_ICON_POS");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				orderIconPos = transform.position;
			}
		}
		CheckBalloons();
		if (MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList != null && MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList.Count > 0 && !MonoBehaviourSingleton<GachaDecoManager>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<GachaDecoManager>();
		}
	}

	private void CheckBalloons()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 4)
		{
			if ((UnityEngine.Object)questBalloon == (UnityEngine.Object)null)
			{
				if (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableNormalDeliveryNum() > 0)
				{
					questBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon((!MonoBehaviourSingleton<DeliveryManager>.I.hasProgressDailyDelivery) ? UI_Common.BALLOON_TYPE.COMPLETABLE_NORMAL_L : UI_Common.BALLOON_TYPE.COMPLETABLE_DAILY, GetCtrl(UI.OBJ_BALOON_ROOT));
				}
				else if (GameSaveData.instance.IsRecommendedDeliveryCheck())
				{
					questBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(MonoBehaviourSingleton<DeliveryManager>.I.hasProgressDailyDelivery ? UI_Common.BALLOON_TYPE.NEW_DAILY : UI_Common.BALLOON_TYPE.NEW_NORMAL_L, GetCtrl(UI.OBJ_BALOON_ROOT));
				}
				else if ((UnityEngine.Object)storyBalloon == (UnityEngine.Object)null && MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(new DELIVERY_TYPE[1]
				{
					DELIVERY_TYPE.STORY
				}))
				{
					storyBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_L, GetCtrl(UI.OBJ_BALOON_ROOT));
				}
			}
			if ((UnityEngine.Object)orderBalloon == (UnityEngine.Object)null)
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
	}

	protected abstract bool CheckNeededGotoGacha();

	protected abstract bool CheckNeededOpenQuest();

	protected abstract void SetupLoginBonus();

	protected abstract void SetupPointShop();

	private void SetUpBingo()
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist())
		{
			if ((UnityEngine.Object)MonoBehaviourSingleton<StageManager>.I.stageObject != (UnityEngine.Object)null)
			{
				Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/BINGO_ICON_POS");
				if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
				{
					bingoIconPos = transform.position;
				}
			}
			bingoBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateBingoBalloon(GetCtrl(UI.OBJ_BALOON_ROOT));
			if ((UnityEngine.Object)bingoBalloon != (UnityEngine.Object)null)
			{
				ResetTween(bingoBalloon, 0);
				PlayTween(bingoBalloon, true, null, false, 0);
			}
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
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
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
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
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
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
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
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
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
		MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((LoginBonus x) => x.priority == 0 && x.type != 0);
		List<LoginBonus> list = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.priority == 0 && x.type == 0);
		if (list != null && list.Count > 0)
		{
			limitedLoginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.FindAll((LoginBonus x) => x.type != 0);
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.RemoveAll((LoginBonus x) => x.type != 0);
			DispatchEvent("LOGIN_BONUS", null);
		}
		else if (logInBonusLimitedCount >= 3)
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
			DispatchEvent("LIMITED_LOGIN_BONUS", null);
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
			DispatchEvent("LIMITED_LOGIN_BONUS", null);
		}
	}

	protected virtual void UpdateUIOfTutorial()
	{
		bool flag = !HomeTutorialManager.ShouldRunGachaTutorial();
		if ((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted() && flag)
		{
			SetActive(UI.BTN_CHAT, !MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow());
		}
		else
		{
			SetActive(UI.BTN_CHAT, false);
		}
		SetActive(UI.BTN_STORAGE, TutorialStep.HasAllTutorialCompleted() && flag);
		bool is_visible = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade > 0 && flag;
		SetActive(UI.BTN_MISSION, is_visible);
		MonoBehaviourSingleton<UIManager>.I.mainStatus.SetMenuButtonEnable(TutorialStep.HasAllTutorialCompleted() && flag);
		MonoBehaviourSingleton<UIManager>.I.mainMenu.SetMenuButtonEnable(flag);
	}

	private void UpdateTicketNum()
	{
		if (MonoBehaviourSingleton<InventoryManager>.IsValid())
		{
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.type == ITEM_TYPE.TICKET, 0, false);
			SetBadge(UI.BTN_TICKET, itemNum, SpriteAlignment.TopLeft, 8, -8, false);
		}
	}

	private void UpdateGiftboxNum()
	{
		SetBadge(UI.BTN_GIFTBOX, MonoBehaviourSingleton<PresentManager>.I.presentNum, SpriteAlignment.TopLeft, 8, -8, false);
	}

	private void UpdateGuildRequest()
	{
		SetActive(UI.BTN_GUILD_REQUEST, false);
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			int num = (from g in MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList
			where g.questId > 0
			where g.GetQuestRemainTime().TotalSeconds < 0.0
			select g).Count();
			SetBadge(GetCtrl(UI.BTN_GUILD_REQUEST), num, SpriteAlignment.TopRight, -8, -8, false);
		}
	}

	private void UpdatePointShop()
	{
		bool flag = (MonoBehaviourSingleton<HomeManager>.IsValid() && MonoBehaviourSingleton<HomeManager>.I.IsPointShopOpen) || (MonoBehaviourSingleton<LoungeManager>.IsValid() && MonoBehaviourSingleton<LoungeManager>.I.IsPointShopOpen);
		bool isGuildRequestOpen = MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen;
		SetActive(UI.BTN_POINT_SHOP, false);
	}

	protected virtual void LateUpdate()
	{
		UpdateGuildBtn();
		UpdateNotice();
		UpdateBalloon();
		DirectDelivery();
		if (MonoBehaviourSingleton<GachaDecoManager>.IsValid())
		{
			MonoBehaviourSingleton<GachaDecoManager>.I.SetVisible(IsValidGachaDeco());
		}
		if (IsValidDispatchEventInUpdate() && !CheckOnceShowObjects())
		{
			UpdateBonusTime();
		}
	}

	private void DirectDelivery()
	{
		if (IsCurrentSectionHomeOrLounge() && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			if (TutorialStep.HasChangeEquipCompleted() && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.CLAIM_REWARD) && executeTutorialClaimReward)
			{
				List<LoginBonus> logInBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus;
				if (logInBonus == null || logInBonus.Count == 0 || logInBonus[0].priority <= 0)
				{
					TutorialClaimReward();
					return;
				}
			}
			if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.CLAIM_REWARD))
			{
				if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
				{
					if (!triger_tutorial_gacha_1)
					{
						DispatchEvent("HOME_TUTORIAL", null);
					}
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
				{
					if (!triger_tutorial_force_item)
					{
						DispatchEvent("HOME_TUTORIAL", null);
					}
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
				{
					if (!triger_tutorial_gacha_2)
					{
						DispatchEvent("HOME_TUTORIAL", null);
					}
				}
				else if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
				{
					if (executeTutorialStep6)
					{
						TutorialStep_6();
					}
					else if (executeTutorialEnd)
					{
						TutorialStep_END();
					}
					else if (MonoBehaviourSingleton<DeliveryManager>.IsValid())
					{
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
							DispatchEvent("HOME_TUTORIAL", null);
						}
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
			DispatchEvent("BLACK_MARKET_BANNER", null);
			return true;
		}
		if (GameSaveData.instance.showHomeOneTimesOfferSSDay != DateTime.UtcNow.Day && MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
		{
			GameSaveData.instance.showHomeOneTimesOfferSSDay = DateTime.UtcNow.Day;
			DispatchEvent("BANNER_ONETIMESOFFERSS", null);
			return true;
		}
		if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count > 0)
		{
			for (int i = 0; i < MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count; i++)
			{
				if (!GameSaveData.instance.showIAPAdsPop.Contains(MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].productId) && !GameSaveData.instance.iAPBundleBought.Contains(MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].productId))
				{
					GameSaveData.instance.showIAPAdsPop = $"{GameSaveData.instance.showIAPAdsPop}/{MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].productId}";
					DispatchEvent("IAP_ADD_POP", MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].productId);
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
				int num = UnityEngine.Random.Range(1, 3);
				if (num == 1)
				{
					DispatchEvent("BANNER_INVITE", null);
				}
				else
				{
					DispatchEvent("BANNER_GUARANTEDSS", null);
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
				int num2 = UnityEngine.Random.Range(1, 3);
				if (num2 == 1)
				{
					DispatchEvent("BANNER_INVITE", null);
				}
				else
				{
					DispatchEvent("BANNER_GUARANTEDSS", null);
				}
				return true;
			}
		}
		if (GameSaveData.instance.showHomeBannerOfferDay != DateTime.UtcNow.Day && MonoBehaviourSingleton<ShopManager>.I.isNeedShowBundleOffer())
		{
			GameSaveData.instance.showHomeBannerOfferDay = DateTime.UtcNow.Day;
			MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = false;
			DispatchEvent("BANNER_OFFER", null);
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
		if (MonoBehaviourSingleton<UserInfoManager>.I.needOpenNewsPage && TutorialStep.HasAllTutorialCompleted() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.OnOpenNewsPage();
			MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
			return true;
		}
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.alertMessage))
		{
			DispatchEvent("ALERT_MESSAGE", null);
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
				DispatchEvent("OPEN_REVIEW_DIALOG", null);
				GameSaveData.instance.ratingPopupHaveShow = true;
			}
			needShowAppReviewAppeal = false;
			return true;
		}
		if (needShowShadowChallengeFirst)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.isShadowChallengeFirst)
			{
				DispatchEvent("OPEN_SHADOW_CHALLENGE", null);
			}
			needShowShadowChallengeFirst = false;
			return true;
		}
		if (GameSaveData.instance.showUnlockQuestEvent)
		{
			DispatchEvent("QUEST_UNLOCK", null);
			GameSaveData.instance.showUnlockQuestEvent = false;
			return true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.needBlackMarketNotifi)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.needBlackMarketNotifi = false;
			if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
			{
				MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37u), string.Empty);
			}
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
		return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop";
	}

	private bool IsCurrentSectionGuild()
	{
		return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "GuildTop";
	}

	protected override void OnOpen()
	{
		InitializeChat();
		if ((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.bannerView != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted() && !HomeTutorialManager.ShouldRunGachaTutorial())
		{
			MonoBehaviourSingleton<UIManager>.I.bannerView.Open(UITransition.TYPE.OPEN);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationButton.Open(UITransition.TYPE.OPEN);
		}
		if (shouldFrameInNPC006)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.shouldDispAdvancedOffer())
			{
				RequestEvent("ADVANCED_OFFER", null);
			}
			FrameInNPC006();
		}
		if (TutorialStep.HasAllTutorialCompleted() && !string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			int num = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			if (num > 0)
			{
				MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Open(UITransition.TYPE.OPEN);
			}
		}
	}

	protected override void OnCloseStart()
	{
		if ((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		if ((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.bannerView != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.bannerView.Close(UITransition.TYPE.CLOSE);
		}
		MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.Close(UITransition.TYPE.CLOSE);
		MonoBehaviourSingleton<UIManager>.I.invitationButton.Close(UITransition.TYPE.CLOSE);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<PuniConManager>.IsValid())
		{
			UnityEngine.Object.Destroy(MonoBehaviourSingleton<PuniConManager>.I.gameObject);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.RemoveObserver(this);
		}
		if (MonoBehaviourSingleton<GachaDecoManager>.IsValid())
		{
			UnityEngine.Object.Destroy(MonoBehaviourSingleton<GachaDecoManager>.I);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_EQUIP_CHANGE | NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE | NOTIFY_FLAG.UPDATE_DELIVERY_OVER | NOTIFY_FLAG.UPDATE_TASK_LIST;
	}

	private void UpdateBalloon()
	{
		if ((UnityEngine.Object)questBalloon != (UnityEngine.Object)null)
		{
			if (TutorialStep.HasAllTutorialCompleted() && (GameSaveData.instance.IsRecommendedDeliveryCheck() || MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableNormalDeliveryNum() > 0))
			{
				SetBalloonPosition(questBalloon, questIconPos);
			}
			else
			{
				questBalloon.parent.gameObject.SetActive(false);
				questBalloon = null;
				RefreshUI();
			}
		}
		else if ((UnityEngine.Object)storyBalloon != (UnityEngine.Object)null)
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
				storyBalloon.parent.gameObject.SetActive(false);
				storyBalloon = null;
				RefreshUI();
			}
		}
		if ((UnityEngine.Object)orderBalloon != (UnityEngine.Object)null)
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
				orderBalloon.parent.gameObject.SetActive(false);
				orderBalloon = null;
			}
		}
		if ((UnityEngine.Object)eventBalloon != (UnityEngine.Object)null)
		{
			if (HasNotCheckedEvent())
			{
				SetBalloonPosition(eventBalloon, eventIconPos);
			}
			else
			{
				eventBalloon.parent.gameObject.SetActive(false);
				eventBalloon = null;
			}
		}
		if ((UnityEngine.Object)pointShopBalloon != (UnityEngine.Object)null)
		{
			if ((MonoBehaviourSingleton<HomeManager>.IsValid() && MonoBehaviourSingleton<HomeManager>.I.IsPointShopOpen) || (MonoBehaviourSingleton<LoungeManager>.IsValid() && MonoBehaviourSingleton<LoungeManager>.I.IsPointShopOpen))
			{
				SetBalloonPosition(pointShopBalloon, pointShopIconPos);
			}
			else
			{
				pointShopBalloon.parent.gameObject.SetActive(false);
				pointShopBalloon = null;
			}
		}
		if ((UnityEngine.Object)bingoBalloon != (UnityEngine.Object)null)
		{
			if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist())
			{
				SetBalloonPosition(bingoBalloon, bingoIconPos);
			}
			else
			{
				bingoBalloon.parent.gameObject.SetActive(false);
				bingoBalloon = null;
			}
		}
	}

	protected void SetBalloonPosition(Transform balloon, Vector3 iconPos)
	{
		Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(iconPos));
		position.z = ((!(position.z >= 0f)) ? (-100f) : 0f);
		balloon.position = position;
	}

	private void UpdateEventBalloon()
	{
		if (TutorialStep.HasAllTutorialCompleted() && !waitEventBalloon && HasNotCheckedEvent())
		{
			if (currentEventBalloonType != 0)
			{
				if (currentEventBalloonType == GetEventBalloonType())
				{
					return;
				}
				eventBalloon.parent.gameObject.SetActive(false);
				eventBalloon = null;
			}
			if ((UnityEngine.Object)MonoBehaviourSingleton<StageManager>.I.stageObject != (UnityEngine.Object)null)
			{
				Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/EVENT_ICON_POS");
				if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
				{
					eventIconPos = transform.position;
				}
			}
			currentEventBalloonType = GetEventBalloonType();
			eventBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateEventBalloon(GetCtrl(UI.OBJ_BALOON_ROOT), currentEventBalloonType);
			if ((UnityEngine.Object)eventBalloon != (UnityEngine.Object)null)
			{
				ResetTween(eventBalloon, 0);
				PlayTween(eventBalloon, true, null, false, 0);
			}
		}
	}

	private UI_Common.EVENT_BALLOON_TYPE GetEventBalloonType()
	{
		return (MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum() <= 0) ? UI_Common.EVENT_BALLOON_TYPE.NEW : UI_Common.EVENT_BALLOON_TYPE.COMPLETABLE;
	}

	private bool HasNotCheckedEvent()
	{
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		int i = 0;
		for (int count = eventList.Count; i < count; i++)
		{
			Network.EventData eventData = eventList[i];
			if (!eventData.readPrologueStory)
			{
				return true;
			}
		}
		return MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum() > 0;
	}

	private void SetupNotice()
	{
		noticeTransform = GetCtrl(UI.OBJ_NOTICE);
		noticeObject = noticeTransform.gameObject;
		noticeTween = GetComponent<TweenAlpha>(UI.OBJ_NOTICE);
		SetActive(UI.OBJ_NOTICE, false);
	}

	private void SetUpBonusTime()
	{
		Transform ctrl = GetCtrl(UI.OBJ_BONUS_TIME_ROOT);
		if (!((UnityEngine.Object)ctrl == (UnityEngine.Object)null))
		{
			bonusTime = ctrl.gameObject.AddComponent<HomeTopBonusTime>();
			bonusTime.InitUI();
			bonusTime.SetUp();
		}
	}

	protected void OnNoticeAreaEvent(HomeStageAreaEvent area_event)
	{
		if (TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && !((UnityEngine.Object)TutorialMessage.GetCursor(0) != (UnityEngine.Object)null))
		{
			noticeEventTo = area_event;
		}
	}

	private void UpdateNotice()
	{
		if (!((UnityEngine.Object)noticeTransform == (UnityEngine.Object)null))
		{
			UpdateNoticeStatus();
			if (noticeObject.activeSelf)
			{
				UpdateNoticePosition();
			}
		}
	}

	private void UpdateNoticeStatus()
	{
		if (!((UnityEngine.Object)noticeEvent == (UnityEngine.Object)noticeEventTo) && !noticeTween.enabled)
		{
			if (noticeTween.value != 0f)
			{
				noticeTween.PlayReverse();
			}
			else if ((UnityEngine.Object)noticeEventTo == (UnityEngine.Object)null)
			{
				noticeObject.SetActive(false);
				noticeEvent = noticeEventTo;
			}
			else if (!IsShowNoticeByArena() && !(this is LoungeTop))
			{
				noticeObject.SetActive(false);
				noticeEvent = noticeEventTo;
			}
			else
			{
				SetLabelText(UI.LBL_NOTICE, base.sectionData.GetText(noticeEventTo.eventName));
				noticePos = noticeEventTo._transform.position;
				noticePos.y += noticeEventTo.noticeViewHeight;
				UpdateNoticeLock();
				if (!string.IsNullOrEmpty(noticeEventTo.noticeButtonName))
				{
					SetActive(UI.OBJ_BUTTON_NOTICE, true);
					SetActive(UI.OBJ_NORMAL_NOTICE, false);
					SetActiveAreaEventButton(noticeEventTo.noticeButtonName, true);
				}
				else
				{
					SetActive(UI.OBJ_BUTTON_NOTICE, false);
					SetActive(UI.OBJ_NORMAL_NOTICE, true);
				}
				noticeObject.SetActive(true);
				noticeTween.PlayForward();
				noticeEvent = noticeEventTo;
			}
		}
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
		SetActive(UI.OBJ_NOTICE_LOCK, false);
		if (noticeEventTo.eventName.Contains("ARENA_LIST") && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50 && MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen)
		{
			SetActive(UI.OBJ_NOTICE_LOCK, true);
			SetLabelText(UI.LBL_NOTICE_LOCK, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, 50) + "で解放");
		}
	}

	private void UpdateNoticePosition()
	{
		Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(noticePos));
		position.z = ((!(position.z >= 0f)) ? (-100f) : 0f);
		noticeTransform.position = position;
	}

	protected virtual void SetActiveAreaEventButton(string btnName, bool active)
	{
	}

	private void CheckNotifyQuestRemainTime()
	{
		if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() != 0)
		{
			TimeSpan minRemainTime = TimeSpan.MaxValue;
			List<ulong> ids = new List<ulong>();
			MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo item)
			{
				foreach (float remainTime in item.remainTimes)
				{
					float num3 = remainTime;
					TimeSpan timeSpan = TimeSpan.FromSeconds((double)num3);
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
			int num = 2147483647;
			int[] nOTIFY_QUEST_REMAIN_DAY = GameDefine.NOTIFY_QUEST_REMAIN_DAY;
			foreach (int num2 in nOTIFY_QUEST_REMAIN_DAY)
			{
				if (minRemainTime.TotalDays < (double)num2 && num2 < num)
				{
					num = num2;
				}
			}
			if (num == 2147483647)
			{
				GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
			}
			else if (GameSaveData.instance.isIncludeNotifyQuestID(ids))
			{
				if (num == GameSaveData.instance.lastRemainDayThreshold)
				{
					GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
				}
				else
				{
					string remainText = GetRemainText(minRemainTime);
					DispatchEvent("NOTICE_QUEST_REMAIN", new object[1]
					{
						remainText
					});
					GameSaveData.instance.updateLastNotifyQuestRemainTime(num, ids);
				}
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
		shouldFrameInNPC006 = false;
		StartCoroutine(_FrameInNPC006());
	}

	private IEnumerator _FrameInNPC006()
	{
		PlayerAnimCtrl animCtrl = npc06Info.loader.GetAnimator().GetComponent<PlayerAnimCtrl>();
		PLCA defaultAnim = animCtrl.defaultAnim;
		AnimatorCullingMode cullingMode = animCtrl.animator.cullingMode;
		while (npc06Info.IsLeaveState())
		{
			yield return (object)null;
		}
		Transform moveTransform = Utility.Find(npc06Info._transform, "Move");
		int i = 0;
		for (int len = moveTransform.childCount; i < len; i++)
		{
			Transform c = moveTransform.GetChild(i);
			if (c.name.StartsWith("LIB"))
			{
				UnityEngine.Object.Destroy(c.gameObject);
				break;
			}
		}
		npc06Info.gameObject.SetActive(true);
		npc06Info.PushOutControll();
		npc06Info._transform.localPosition = npc06Info.npcInfo.GetSituation().pos;
		npc06Info._transform.localEulerAngles = new Vector3(0f, npc06Info.npcInfo.GetSituation().rot, 0f);
		animCtrl.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		animCtrl.Play(PLCA.EVENT_MOVE, true);
		bool wait = true;
		Action<PlayerAnimCtrl, PLCA> onEnd = delegate(PlayerAnimCtrl pac, PLCA plca)
		{
			if (plca == PLCA.EVENT_MOVE)
			{
				((_003C_FrameInNPC006_003Ec__Iterator71)/*Error near IL_01f5: stateMachine*/)._003Cwait_003E__7 = false;
			}
		};
		Action<PlayerAnimCtrl, PLCA> origOnEnd = animCtrl.onEnd;
		animCtrl.onEnd = onEnd;
		while (wait)
		{
			yield return (object)null;
		}
		animCtrl.onEnd = origOnEnd;
		animCtrl.animator.cullingMode = cullingMode;
		animCtrl.defaultAnim = defaultAnim;
		animCtrl.Play(PLCA.EVENT_IDLE, false);
		npc06Info.PopState();
		HomeDragonRandomMove move = npc06Info.loader.GetAnimator().gameObject.AddComponent<HomeDragonRandomMove>();
		move.Reset();
		waitEventBalloon = false;
		UpdateEventBalloon();
	}

	protected virtual void InitializeChat()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.Open(UITransition.TYPE.OPEN);
			MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver(this);
			UIButton component = GetCtrl(UI.BTN_CHAT).GetComponent<UIButton>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				component.onClick.Clear();
			}
			AddChatClickDelegate(component);
		}
	}

	protected abstract void AddChatClickDelegate(UIButton button);

	protected bool StopEventUntilTheAllTutorialCompleted()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || (UnityEngine.Object)TutorialMessage.GetCursor(0) != (UnityEngine.Object)null || ((UnityEngine.Object)homeTutorialManager != (UnityEngine.Object)null && HomeTutorialManager.ShouldRunGachaTutorial())))
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
			DispatchEvent("TUTORIAL_STEP_6", null);
		}
	}

	private void TutorialStep_END()
	{
		if (!((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.tutorialMessage == (UnityEngine.Object)null))
		{
			executeTutorialEnd = false;
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialEnd", delegate
			{
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
					{
						MonoBehaviourSingleton<UIManager>.I.mainStatus.SetMenuButtonEnable(true);
						RefreshUI();
						if (MonoBehaviourSingleton<QuestManager>.I.ExistsExploreEvent())
						{
							Protocol.Force(delegate
							{
								MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialBit(TUTORIAL_MENU_BIT.EXPLORE, null);
							});
						}
					});
				});
			});
		}
	}

	private void TutorialClaimReward()
	{
		if (!((UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.tutorialMessage == (UnityEngine.Object)null))
		{
			executeTutorialClaimReward = false;
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "ClaimReward", null);
		}
	}

	protected abstract void CheckEventLock();

	private void OnQuery_COMPLETE_READ_STORY()
	{
		int scriptId = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<DeliveryManager>.I.SendReadStoryRead(scriptId, delegate(bool is_success, Error recv_reward)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_TO_GIFTBOX()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_TUTORIAL_STEP_6()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
		{
			GameSection.ResumeEvent(false, null);
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
		_OnQuery_FIELD(false);
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
		GameSection.ChangeEvent("QUEST_COUNTER", null);
		OnQuery_QUEST_COUNTER();
	}

	private void OnQuery_BINGO()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
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
					List<DeliveryTable.DeliveryData> deliveryTableDataList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryTableDataList(false);
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
						GameSection.ChangeStayEvent("MINI_BINGO", null);
					}
				}
				else
				{
					GameSection.ChangeStayEvent("MINI_BINGO", null);
				}
			}
			GameSection.ResumeEvent(true, null);
		});
	}

	private void OnQuery_QUEST_COUNTER()
	{
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
		else if ((UnityEngine.Object)homeTutorialManager != (UnityEngine.Object)null)
		{
			if (HomeTutorialManager.ShouldRunGachaTutorial())
			{
				GameSection.StopEvent();
				return;
			}
			homeTutorialManager.CloseDialog();
		}
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
		OnTalkPamelaTutorial = false;
		if ((UnityEngine.Object)mdlArrow != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(mdlArrow.gameObject);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && OnAfterGacha2Tutorial)
		{
			OnAfterGacha2Tutorial = false;
			if ((UnityEngine.Object)homeTutorialManager != (UnityEngine.Object)null)
			{
				homeTutorialManager.DisableTargetArea();
			}
		}
	}

	private void OnQuery_EVENT_COUNTER_AREA()
	{
		fromQuestCounterAreaEvent = true;
		GameSection.ChangeEvent("EVENT_COUNTER", null);
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
		else if ((UnityEngine.Object)homeTutorialManager != (UnityEngine.Object)null)
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
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
	}

	private void OnQuery_GACHA_QUEST_COUNTER_AREA()
	{
		fromQuestCounterAreaEvent = true;
		GameSection.ChangeEvent("GACHA_QUEST_COUNTER", null);
		OnQuery_GACHA_QUEST_COUNTER();
	}

	private void OnQuery_GACHA_QUEST_COUNTER()
	{
		bool flag = !fromQuestCounterAreaEvent;
		fromQuestCounterAreaEvent = false;
		if (flag)
		{
			StopEventUntilTheAllTutorialCompleted();
		}
		else if ((UnityEngine.Object)homeTutorialManager != (UnityEngine.Object)null)
		{
			if (HomeTutorialManager.DoesTutorial())
			{
				GameSection.StopEvent();
				return;
			}
			homeTutorialManager.CloseDialog();
		}
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
	}

	private void OnQuery_EXPLORE()
	{
		if (!StopEventUntilTheAllTutorialCompleted() && !GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
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
		if (!((UnityEngine.Object)bonusTime == (UnityEngine.Object)null))
		{
			bonusTime.UpdateBonusTime();
		}
	}

	private void OnQuery_NONE()
	{
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep6_2", null);
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
		{
			triger_tutorial_gacha_1 = true;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
		{
			triger_tutorial_force_item = true;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
		{
			triger_tutorial_gacha_2 = true;
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
					MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
				}
				GameSection.ResumeEvent(is_success, null);
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
			RequestEvent("ALERT_MESSAGE", null);
		}
	}

	private void OnQuery_BANNER_NEXT()
	{
		MonoBehaviourSingleton<UIManager>.I.bannerView.NextBanner(2, true);
	}

	private void OnQuery_BANNER_PREV()
	{
		MonoBehaviourSingleton<UIManager>.I.bannerView.NextBanner(2, false);
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
			GameSection.ResumeEvent(is_success, null);
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
}
