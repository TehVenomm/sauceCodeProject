using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoungeTop : HomeBase
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
		SPR_BADGE,
		SPR_MENU_GG,
		OBJ_MENU_GG,
		BTN_MENU_GG_ON,
		BTN_MENU_GG_OFF,
		BTN_GOWRAP_GG,
		BTN_CRYSTAL_SHOP_GG,
		BTN_POINT_SHOP_GG,
		BTN_GUILD_FAVOR_GG
	}

	private const float RoomPartyInterval = 3f;

	private bool isHighlightPurchase;

	private bool isHighlightPikeShop;

	private Vector3 loungeQuestIconPos;

	private Transform loungeQuestBalloon;

	private float roomPartyTimer;

	private Transform chairBtn;

	private ChairPoint nearChairPoint;

	public override void Initialize()
	{
		chairBtn = GetCtrl(UI.BTN_CHAIR);
		base.Initialize();
	}

	public override void StartSection()
	{
		base.StartSection();
		roomPartyTimer = 4f;
		MonoBehaviourSingleton<LoungeManager>.I.SetLoungeQuestBalloon(true);
		SetActive((Enum)UI.OBJ_MENU_GG, true);
		CheckHighlightPurchase();
		if (isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification())
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, -1, 3, 5, -25, false);
		}
		else
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, 0, 3, 0, 0, false);
		}
	}

	protected override void CreateSelfCharacter()
	{
		MonoBehaviourSingleton<LoungeManager>.I.HomePeople.CreateSelfCharacter(base.OnNoticeAreaEvent);
	}

	protected override IEnumerator WaitInitializeManager()
	{
		yield return (object)this.StartCoroutine(WaitForCheckpikeShop());
		while (!MonoBehaviourSingleton<LoungeManager>.I.IsInitialized)
		{
			yield return (object)null;
		}
	}

	protected unsafe override IEnumerator SendHomeInfo()
	{
		bool wait = true;
		MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(new Action<bool, bool, int>((object)/*Error near IL_002d: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (wait)
		{
			yield return (object)null;
		}
	}

	protected override IEnumerator WaitLoadHomeCharacters()
	{
		while (MonoBehaviourSingleton<LoungeManager>.I.HomePeople.selfChara.isLoading || !MonoBehaviourSingleton<LoungeManager>.I.HomePeople.isPeopleInitialized)
		{
			yield return (object)null;
		}
	}

	protected override void InitializeChat()
	{
		base.InitializeChat();
		MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(false);
	}

	protected override void AddChatClickDelegate(UIButton btnChat)
	{
		btnChat.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly));
	}

	protected override bool CheckNeededOpenQuest()
	{
		if (HomeTutorialManager.ShouldRunGachaTutorial() || MonoBehaviourSingleton<LoungeManager>.I.IsJumpToGacha)
		{
			MonoBehaviourSingleton<LoungeManager>.I.IsJumpToGacha = false;
			DispatchEvent("GACHA_QUEST_COUNTER_AREA", null);
			return true;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest)
		{
			MonoBehaviourSingleton<QuestManager>.I.isBackGachaQuest = false;
			DispatchEvent("GACHA_QUEST_COUNTER_AREA", null);
			return true;
		}
		return false;
	}

	protected override bool CheckNeededGotoGacha()
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

	protected override bool CheckInvitedLoungeBySNS()
	{
		string @string = PlayerPrefs.GetString("il");
		if (!string.IsNullOrEmpty(@string))
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue = @string;
			PlayerPrefs.SetString("il", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
			}
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				return false;
			}
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
			{
				return false;
			}
			string[] array = @string.Split('_');
			if (array[0] == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.loungeNumber)
			{
				return false;
			}
			EventData[] autoEvents = new EventData[4]
			{
				new EventData("LOUNGE_SETTINGS", null),
				new EventData("EXIT", null),
				new EventData("LOUNGE", null),
				new EventData("INVITED_LOUNGE", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			return true;
		}
		return false;
	}

	protected override void SetIconAndBalloon()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/LOUNGE_QUEST_ICON_POS");
			if (val != null)
			{
				loungeQuestIconPos = val.get_position();
			}
		}
		CreateLoungeBoardIcon();
		base.SetIconAndBalloon();
	}

	private void CreateLoungeBoardIcon()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		loungeQuestBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeQuestBalloon(GetCtrl(UI.OBJ_BALOON_ROOT));
		loungeQuestBalloon.get_parent().get_gameObject().SetActive(false);
	}

	protected override void SetupLoginBonus()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (validLoginBonus)
		{
			shouldFrameInNPC006 = true;
			CheckLoginBonusFirst();
		}
		else
		{
			HomeNPCCharacter homeNPCCharacter = MonoBehaviourSingleton<LoungeManager>.I.HomePeople.GetHomeNPCCharacter(6);
			homeNPCCharacter.HideShadow();
			HomeDragonRandomMove homeDragonRandomMove = homeNPCCharacter.loader.GetAnimator().get_gameObject().AddComponent<HomeDragonRandomMove>();
			homeDragonRandomMove.Reset();
		}
	}

	protected override void SetupPointShop()
	{
		if (MonoBehaviourSingleton<LoungeManager>.IsValid() && MonoBehaviourSingleton<LoungeManager>.I.IsPointShopOpen && MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			return;
		}
	}

	private void Update()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!(chairBtn == null) && chairBtn.get_gameObject().get_activeSelf() && !(nearChairPoint == null) && nearChairPoint.sittingChara != null)
		{
			SetActive((Enum)UI.BTN_CHAIR, false);
		}
	}

	protected override void LateUpdate()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		roomPartyTimer += Time.get_deltaTime();
		if (MonoBehaviourSingleton<LoungeManager>.I.NeedLoungeQuestBalloonUpdate)
		{
			MonoBehaviourSingleton<LoungeManager>.I.SetLoungeQuestBalloon(false);
			if (roomPartyTimer > 3f)
			{
				this.StartCoroutine(UpdateLoungeQuestBalloon());
				roomPartyTimer = 0f;
			}
		}
		if (loungeQuestBalloon != null && loungeQuestBalloon.get_gameObject().get_activeSelf())
		{
			SetBalloonPosition(loungeQuestBalloon, loungeQuestIconPos);
		}
		if (loungeQuestBalloon != null)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsRallyInvite)
			{
				if (!loungeQuestBalloon.get_parent().get_gameObject().get_activeSelf())
				{
					loungeQuestBalloon.get_parent().get_gameObject().SetActive(true);
					ResetTween(loungeQuestBalloon, 0);
					PlayTween(loungeQuestBalloon, true, null, false, 0);
				}
			}
			else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.parties == null || MonoBehaviourSingleton<LoungeMatchingManager>.I.parties.Count == 0)
			{
				loungeQuestBalloon.get_parent().get_gameObject().SetActive(false);
			}
		}
		base.LateUpdate();
	}

	private unsafe IEnumerator UpdateLoungeQuestBalloon()
	{
		if (!(loungeQuestBalloon == null))
		{
			bool wait = true;
			Protocol.Try(new Action((object)/*Error near IL_0043: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			while (wait)
			{
				yield return (object)null;
			}
			if (MonoBehaviourSingleton<LoungeMatchingManager>.I.parties != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.parties.Count > 0)
			{
				if (!loungeQuestBalloon.get_parent().get_gameObject().get_activeSelf())
				{
					loungeQuestBalloon.get_parent().get_gameObject().SetActive(true);
					ResetTween(loungeQuestBalloon, 0);
					PlayTween(loungeQuestBalloon, true, null, false, 0);
				}
			}
			else
			{
				loungeQuestBalloon.get_parent().get_gameObject().SetActive(false);
			}
		}
	}

	protected override void SetActiveAreaEventButton(string btnName, bool active)
	{
		switch (btnName)
		{
		case "BTN_CHAIR":
			if (MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				nearChairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(MonoBehaviourSingleton<LoungeManager>.I.HomePeople.selfChara);
				if (nearChairPoint.sittingChara != null)
				{
					SetActive((Enum)UI.BTN_CHAIR, false);
				}
				else
				{
					SetActive((Enum)UI.BTN_CHAIR, active);
				}
			}
			break;
		}
	}

	protected override void FrameInNPC006()
	{
		npc06Info = MonoBehaviourSingleton<LoungeManager>.I.HomePeople.GetHomeNPCCharacter(6);
		base.FrameInNPC006();
	}

	private void Sit()
	{
		SetActiveAreaEventButton("BTN_CHAIR", false);
		MonoBehaviourSingleton<LoungeManager>.I.HomePeople.selfChara.Sit();
		MonoBehaviourSingleton<LoungeManager>.I.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.SITTING);
	}

	protected override void CheckEventLock()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<LoungeManager>.IsValid() && !isEventLockLoading)
		{
			if (eventLockMesh == null)
			{
				this.StartCoroutine(LoadEventLock());
			}
			else if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				eventLockMesh.get_gameObject().SetActive(true);
			}
			else
			{
				eventLockMesh.get_gameObject().SetActive(false);
			}
		}
	}

	private IEnumerator LoadEventLock()
	{
		isEventLockLoading = true;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.NPC_MODEL, "lockeventbanner", new string[1]
		{
			"LockEventBanner"
		}, false);
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		while (waitEventBalloon)
		{
			yield return (object)null;
		}
		HomeNPCCharacter eventNPC = MonoBehaviourSingleton<LoungeManager>.I.HomePeople.GetHomeNPCCharacter(6);
		if (eventNPC != null)
		{
			Vector3 MODEL_OFFSET = new Vector3(0f, 1.79f, 0.504f);
			Transform banner = Utility.CreateGameObject("EventLockBanner", eventNPC._transform, -1);
			ResourceUtility.Realizes(loadedArrow.loadedObject, banner, -1);
			banner.set_localPosition(MODEL_OFFSET);
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				banner.get_gameObject().SetActive(true);
			}
			else
			{
				banner.get_gameObject().SetActive(false);
			}
			eventLockMesh = banner;
		}
		isEventLockLoading = false;
		yield return (object)null;
	}

	private void OnQuery_POINT_SHOP()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.ChangeEvent("POINT_SHOP_FROM_BUTTON", null);
		}
	}

	private void OnQuery_POINT_SHOP_FROM_BUTTON()
	{
		if (!MonoBehaviourSingleton<LoungeManager>.IsValid() || !MonoBehaviourSingleton<LoungeManager>.I.IsPointShopOpen)
		{
			GameSection.StopEvent();
		}
		else
		{
			if (!GameSceneManager.isAutoEventSkip)
			{
				SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
			}
			PlayerPrefs.SetInt("Pike_Shop_Event", 1);
		}
	}

	private void OnQuery_GUILD_REQUEST()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
	}

	private void OnQuery_CHAIR()
	{
		if (MonoBehaviourSingleton<LoungeManager>.I.HomeCamera.viewMode != HomeCamera.VIEW_MODE.SITTING)
		{
			Sit();
		}
	}

	private void OnQuery_LOUNGE_QUEST_COUNTER()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR", null);
		}
	}

	private void OnQuery_LOUNGE_SETTINGS()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR", null);
		}
	}

	private unsafe void OnCloseDialog_KickedMessage()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		if (_003C_003Ef__am_0024cache8 == null)
		{
			_003C_003Ef__am_0024cache8 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		Protocol.Force(_003C_003Ef__am_0024cache8);
	}

	private void OnQuery_GOWRAP()
	{
		GameSaveData.instance.dayShowNewsNotification = DateTime.UtcNow.AddSeconds(-10800.0).Day;
		SetBadge((Enum)UI.BTN_GOWRAP_GG, 0, 9, 0, 0, false);
		MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
	}

	private void OnQuery_MENU_ACTION()
	{
		bool flag = !IsActive(UI.SPR_MENU_GG);
		SetActive((Enum)UI.SPR_MENU_GG, flag);
		SetActive((Enum)UI.BTN_MENU_GG_ON, !flag);
		SetActive((Enum)UI.BTN_MENU_GG_OFF, flag);
		if (flag)
		{
			if (GameSaveData.instance.IsShowNewsNotification())
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, -1, 3, 0, -8, false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, 0, 3, 0, 0, false);
			}
			if (isHighlightPurchase)
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, -1, 3, 0, -8, false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, 0, 3, 0, -8, false);
			}
			if (isHighlightPikeShop)
			{
				SetBadge((Enum)UI.BTN_POINT_SHOP_GG, -1, 3, 0, -8, false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_POINT_SHOP_GG, 0, 3, 0, -8, false);
			}
		}
		else if (isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || isHighlightPikeShop)
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, -1, 3, 5, -25, false);
		}
		else
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, 0, 3, 0, 0, false);
		}
	}

	private void CheckHighlightPurchase()
	{
		isHighlightPurchase = false;
		if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList != null)
		{
			string @string = PlayerPrefs.GetString("Purchase_Item_List_Tab_Gem", string.Empty);
			string string2 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
			string string3 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
			int count = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Count;
			int num = 0;
			while (true)
			{
				if (num >= count)
				{
					return;
				}
				ProductData productData = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList[num];
				if (productData.productType == 1)
				{
					int num2 = @string.IndexOf(productData.productId);
					if (num2 < 0)
					{
						isHighlightPurchase = true;
						return;
					}
				}
				else if (productData.productType == 2)
				{
					int num3 = string2.IndexOf(productData.productId);
					if (num3 < 0)
					{
						isHighlightPurchase = true;
						return;
					}
				}
				else if (productData.productType == 3)
				{
					int num4 = string3.IndexOf(productData.productId);
					if (num4 < 0)
					{
						break;
					}
				}
				num++;
			}
			isHighlightPurchase = true;
		}
	}

	private void OnCloseDialog_MenuTop()
	{
		if (IsActive(UI.SPR_MENU_GG))
		{
			if (GameSaveData.instance.IsShowNewsNotification())
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, -1, 3, 0, -8, false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, 0, 3, 0, -8, false);
			}
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !isHighlightPikeShop)
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, 0, 3, 0, 0, false);
		}
	}

	private void OnCloseDialog_CrystalShopTop()
	{
		CheckHighlightPurchase();
		if (IsActive(UI.SPR_MENU_GG))
		{
			if (isHighlightPurchase)
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, -1, 3, 0, -8, false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, 0, 3, 0, -8, false);
			}
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !isHighlightPikeShop)
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, 0, 3, 0, 0, false);
		}
	}

	private void OnCloseDialog_HomePointShop()
	{
		if (IsActive(UI.SPR_MENU_GG))
		{
			SetBadge((Enum)UI.BTN_POINT_SHOP_GG, 0, 3, 0, -8, false);
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification())
		{
			SetBadge((Enum)UI.BTN_MENU_GG_ON, 0, 3, 0, 0, false);
		}
		isHighlightPikeShop = false;
	}

	private unsafe IEnumerator WaitForCheckpikeShop()
	{
		isHighlightPikeShop = false;
		bool isWait = true;
		Protocol.Send(PointShopModel.URL, null, delegate(PointShopModel ret)
		{
			if (ret.Error == Error.None)
			{
				bool flag = PlayerPrefs.GetInt("Pike_Shop_Event", 0) == 1;
				LoungeTop _003C_003Ef__this = ((_003CWaitForCheckpikeShop_003Ec__IteratorE9)/*Error near IL_003a: stateMachine*/)._003C_003Ef__this;
				List<PointShop> result = ret.result;
				if (_003CWaitForCheckpikeShop_003Ec__IteratorE9._003C_003Ef__am_0024cache4 == null)
				{
					_003CWaitForCheckpikeShop_003Ec__IteratorE9._003C_003Ef__am_0024cache4 = new Func<PointShop, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				_003C_003Ef__this.isHighlightPikeShop = result.Any(_003CWaitForCheckpikeShop_003Ec__IteratorE9._003C_003Ef__am_0024cache4);
				if (((_003CWaitForCheckpikeShop_003Ec__IteratorE9)/*Error near IL_003a: stateMachine*/)._003C_003Ef__this.isHighlightPikeShop)
				{
					if (flag)
					{
						((_003CWaitForCheckpikeShop_003Ec__IteratorE9)/*Error near IL_003a: stateMachine*/)._003C_003Ef__this.isHighlightPikeShop = false;
					}
				}
				else
				{
					PlayerPrefs.SetInt("Pike_Shop_Event", 0);
				}
			}
			((_003CWaitForCheckpikeShop_003Ec__IteratorE9)/*Error near IL_003a: stateMachine*/)._003CisWait_003E__0 = false;
		}, string.Empty);
		while (isWait)
		{
			yield return (object)null;
		}
	}
}
