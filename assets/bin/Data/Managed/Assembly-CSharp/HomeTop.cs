using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class HomeTop : HomeBase
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
		BTN_POINT_SHOP_GG
	}

	private bool isHighlightPurchase;

	private bool isHighlightPikeShop;

	protected override void OnNotifyUpdateUserStatus()
	{
		base.OnNotifyUpdateUserStatus();
		RefreshUI();
	}

	protected override void CreateSelfCharacter()
	{
		MonoBehaviourSingleton<HomeManager>.I.HomePeople.CreateSelfCharacter(base.OnNoticeAreaEvent);
	}

	protected override IEnumerator WaitInitializeManager()
	{
		yield return (object)this.StartCoroutine(WaitForCheckpikeShop());
		while (!MonoBehaviourSingleton<HomeManager>.I.IsInitialized)
		{
			yield return (object)null;
		}
	}

	protected override IEnumerator SendHomeInfo()
	{
		bool wait = true;
		MonoBehaviourSingleton<UserInfoManager>.I.SendHomeInfo(delegate(bool is_success, bool acquire_login_bonus, int taskBadgeNum)
		{
			if (acquire_login_bonus && MonoBehaviourSingleton<AccountManager>.IsValid())
			{
				MonoBehaviourSingleton<AccountManager>.I.SendLogInBonus(delegate
				{
					((_003CSendHomeInfo_003Ec__Iterator89)/*Error near IL_002d: stateMachine*/)._003Cwait_003E__0 = false;
				});
			}
			else
			{
				((_003CSendHomeInfo_003Ec__Iterator89)/*Error near IL_002d: stateMachine*/)._003Cwait_003E__0 = false;
			}
			((_003CSendHomeInfo_003Ec__Iterator89)/*Error near IL_002d: stateMachine*/)._003C_003Ef__this.SetBadge(((_003CSendHomeInfo_003Ec__Iterator89)/*Error near IL_002d: stateMachine*/)._003C_003Ef__this.GetCtrl(UI.BTN_MISSION), taskBadgeNum, 1, 8, -8, false);
		});
		while (wait)
		{
			yield return (object)null;
		}
	}

	protected override IEnumerator WaitLoadHomeCharacters()
	{
		while (MonoBehaviourSingleton<HomeManager>.I.HomePeople.selfChara.isLoading || !MonoBehaviourSingleton<HomeManager>.I.HomePeople.isPeopleInitialized)
		{
			yield return (object)null;
		}
	}

	protected override void InitializeChat()
	{
		base.InitializeChat();
		MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(true);
	}

	protected override void AddChatClickDelegate(UIButton btnChat)
	{
		btnChat.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowFullWithEdit));
	}

	protected override void UpdateUIOfTutorial()
	{
		bool flag = HomeTutorialManager.ShouldRunGachaTutorial();
		bool flag2 = TutorialStep.HasAllTutorialCompleted();
		bool flag3 = !flag && flag2;
		bool flag4 = MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.isOpenLounge;
		bool flag5 = (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
		SetActive((Enum)UI.BTN_LOUNGE, flag5);
		SetActive((Enum)UI.SPR_LOCK_LOUNGE, !flag5);
		bool is_visible = flag3 && flag4;
		bool is_visible2 = flag3 && !flag4;
		SetActive((Enum)UI.OBJ_LOUNGE, is_visible);
		SetActive((Enum)UI.BTN_EXPLORE, is_visible2);
		bool flag6 = !HomeTutorialManager.ShouldRunGachaTutorial();
		bool flag7 = MonoBehaviourSingleton<UIManager>.I.mainChat != null && TutorialStep.HasAllTutorialCompleted() && flag6;
		SetActive((Enum)UI.OBJ_MENU_GG, flag7);
		if (flag7)
		{
			CheckHighlightPurchase();
			if (isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || isHighlightPikeShop)
			{
				SetBadge((Enum)UI.BTN_MENU_GG_ON, -1, 3, 5, -25, false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_MENU_GG_ON, 0, 3, 0, 0, false);
			}
		}
		base.UpdateUIOfTutorial();
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
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("LOUNGE", null),
				new EventData("INVITED_LOUNGE", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
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

	protected override bool CheckNeededOpenQuest()
	{
		if (HomeTutorialManager.ShouldRunGachaTutorial() || MonoBehaviourSingleton<HomeManager>.I.IsJumpToGacha)
		{
			MonoBehaviourSingleton<HomeManager>.I.IsJumpToGacha = false;
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
			HomeNPCCharacter homeNPCCharacter = MonoBehaviourSingleton<HomeManager>.I.HomePeople.GetHomeNPCCharacter(6);
			homeNPCCharacter.HideShadow();
			HomeDragonRandomMove homeDragonRandomMove = homeNPCCharacter.loader.GetAnimator().get_gameObject().AddComponent<HomeDragonRandomMove>();
			homeDragonRandomMove.Reset();
		}
	}

	protected override void SetupPointShop()
	{
		if (MonoBehaviourSingleton<HomeManager>.IsValid() && MonoBehaviourSingleton<HomeManager>.I.IsPointShopOpen && MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			return;
		}
	}

	protected override void FrameInNPC006()
	{
		npc06Info = MonoBehaviourSingleton<HomeManager>.I.HomePeople.GetHomeNPCCharacter(6);
		base.FrameInNPC006();
	}

	protected override void CheckEventLock()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<HomeManager>.IsValid() && !isEventLockLoading)
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
		HomeNPCCharacter eventNPC = MonoBehaviourSingleton<HomeManager>.I.HomePeople.GetHomeNPCCharacter(6);
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
		if (!MonoBehaviourSingleton<HomeManager>.IsValid() || !MonoBehaviourSingleton<HomeManager>.I.IsPointShopOpen)
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

	private void OnQuery_LOUNGE()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
	}

	private void OnQuery_GUILD_REQUEST()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
	}

	private void OnQuery_GUILD_MESSAGE()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
	}

	private void OnQuery_GUILD()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
		}
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

	private IEnumerator WaitForCheckpikeShop()
	{
		isHighlightPikeShop = false;
		bool isWait = true;
		Protocol.Send(PointShopModel.URL, null, delegate(PointShopModel ret)
		{
			if (ret.Error == Error.None)
			{
				bool flag = PlayerPrefs.GetInt("Pike_Shop_Event", 0) == 1;
				((_003CWaitForCheckpikeShop_003Ec__Iterator8C)/*Error near IL_003a: stateMachine*/)._003C_003Ef__this.isHighlightPikeShop = ret.result.Any((PointShop x) => x.isEvent);
				if (((_003CWaitForCheckpikeShop_003Ec__Iterator8C)/*Error near IL_003a: stateMachine*/)._003C_003Ef__this.isHighlightPikeShop)
				{
					if (flag)
					{
						((_003CWaitForCheckpikeShop_003Ec__Iterator8C)/*Error near IL_003a: stateMachine*/)._003C_003Ef__this.isHighlightPikeShop = false;
					}
				}
				else
				{
					PlayerPrefs.SetInt("Pike_Shop_Event", 0);
				}
			}
			((_003CWaitForCheckpikeShop_003Ec__Iterator8C)/*Error near IL_003a: stateMachine*/)._003CisWait_003E__0 = false;
		}, string.Empty);
		while (isWait)
		{
			yield return (object)null;
		}
	}
}
