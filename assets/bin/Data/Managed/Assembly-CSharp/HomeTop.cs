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
		BTN_GUILD_REQUEST,
		BTN_POINT_SHOP,
		BTN_COMMUNITY,
		OBJ_CLAN_SCOUT,
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
		BTN_MENU_GG_OFF,
		BTN_GOWRAP_GG,
		BTN_CRYSTAL_SHOP_GG,
		BTN_POINT_SHOP_GG,
		BTN_CLAN_SCOUT
	}

	private bool isHighlightPurchase;

	private bool isHighlightPikeShop;

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
		{
			SetActive((Enum)UI.OBJ_CLAN_SCOUT, is_visible: false);
		}
		else if ((NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE & flags) != (NOTIFY_FLAG)0L && MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.clanInviteNum > 0)
			{
				SetActive((Enum)UI.OBJ_CLAN_SCOUT, is_visible: true);
				base.GetComponent<UITweenCtrl>((Enum)UI.BTN_CLAN_SCOUT).Play();
			}
			else
			{
				SetActive((Enum)UI.OBJ_CLAN_SCOUT, is_visible: false);
			}
		}
		base.OnNotify(flags);
	}

	protected override void OnNotifyUpdateUserStatus()
	{
		base.OnNotifyUpdateUserStatus();
		RefreshUI();
	}

	protected override void InitializeChat()
	{
		base.InitializeChat();
		MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(active: true);
		MonoBehaviourSingleton<UIManager>.I.mainChat.HomeType = MainChat.HOME_TYPE.HOME_TOP;
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
	}

	protected override void AddChatClickDelegate(UIButton btnChat)
	{
		btnChat.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowFullWithEdit));
	}

	protected override void UpdateUIOfTutorial()
	{
		bool flag = HomeTutorialManager.ShouldRunGachaTutorial() || HomeTutorialManager.ShouldRunQuestShadowTutorial();
		bool flag2 = TutorialStep.HasAllTutorialCompleted();
		bool visible = !flag && flag2;
		UpdateCommunityButton(visible);
		base.UpdateUIOfTutorial();
	}

	private void UpdateCommunityButton(bool _visible)
	{
		Transform ctrl = GetCtrl(UI.BTN_COMMUNITY);
		if (!(ctrl == null))
		{
			SetActive(ctrl, _visible);
			if (_visible && MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
			}
		}
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
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
			}
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				return false;
			}
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
			{
				return false;
			}
			EventData[] autoEvents = new EventData[3]
			{
				new EventData("COMMUNITY", null),
				new EventData("LOUNGE", null),
				new EventData("INVITED_LOUNGE", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			return true;
		}
		return false;
	}

	protected override void CheckEventLock()
	{
		if (MonoBehaviourSingleton<HomeManager>.IsValid() && !isEventLockLoading)
		{
			if (eventLockMesh == null)
			{
				this.StartCoroutine(LoadEventLock());
			}
			else
			{
				eventLockMesh.get_gameObject().SetActive((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel);
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
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		while (waitEventBalloon)
		{
			yield return null;
		}
		HomeNPCCharacter eventNPC = MonoBehaviourSingleton<HomeManager>.I.IHomePeople.GetHomeNPCCharacter(6);
		if (eventNPC != null)
		{
			Vector3 localPosition = default(Vector3);
			localPosition._002Ector(0f, 1.79f, 0.504f);
			Transform val = Utility.CreateGameObject("EventLockBanner", eventNPC._transform);
			ResourceUtility.Realizes(loadedArrow.loadedObject, val);
			val.set_localPosition(localPosition);
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				val.get_gameObject().SetActive(true);
			}
			else
			{
				val.get_gameObject().SetActive(false);
			}
			eventLockMesh = val;
		}
		isEventLockLoading = false;
		yield return null;
	}

	private void OnQuery_POINT_SHOP()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.ChangeEvent("POINT_SHOP_FROM_BUTTON");
		}
	}

	private void OnQuery_GUILD_MESSAGE()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	private void OnQuery_GUILD()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
	}

	private void OnQuery_GOWRAP()
	{
		GameSaveData.instance.dayShowNewsNotification = DateTime.UtcNow.AddSeconds(-10800.0).Day;
		SetBadge((Enum)UI.BTN_GOWRAP_GG, 0, 9, 0, 0, is_scale_normalize: false);
		MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
	}

	private void OnQuery_MENU_ACTION()
	{
		bool flag = !IsActive(UI.SPR_MENU_GG);
		SetActive((Enum)UI.SPR_MENU_GG, flag);
		SetActive((Enum)UI.BTN_MENU_GG_ON, !flag);
		SetActive((Enum)UI.OBJ_MENU_GIFT_ON, IsActive(UI.BTN_MENU_GG_ON));
		SetActive((Enum)UI.BTN_MENU_GG_OFF, flag);
		if (flag)
		{
			if (GameSaveData.instance.IsShowNewsNotification())
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, -1, 3, 0, -8, is_scale_normalize: false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, 0, 3, 0, 0, is_scale_normalize: false);
			}
			if (isHighlightPurchase)
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, -1, 3, 0, -8, is_scale_normalize: false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, 0, 3, 0, -8, is_scale_normalize: false);
			}
			if (isHighlightPikeShop)
			{
				SetBadge((Enum)UI.BTN_POINT_SHOP_GG, -1, 3, 0, -8, is_scale_normalize: false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_POINT_SHOP_GG, 0, 3, 0, -8, is_scale_normalize: false);
			}
		}
		else if (isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || isHighlightPikeShop || ShouldEnableGiftIcon())
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	private void CheckHighlightPurchase()
	{
		isHighlightPurchase = false;
		if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList == null)
		{
			return;
		}
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

	private void OnCloseDialog_MenuTop()
	{
		if (IsActive(UI.SPR_MENU_GG))
		{
			if (GameSaveData.instance.IsShowNewsNotification())
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, -1, 3, 0, -8, is_scale_normalize: false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_GOWRAP_GG, 0, 3, 0, -8, is_scale_normalize: false);
			}
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !isHighlightPikeShop && !ShouldEnableGiftIcon())
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	private void OnCloseDialog_CrystalShopTop()
	{
		CheckHighlightPurchase();
		if (IsActive(UI.SPR_MENU_GG))
		{
			if (isHighlightPurchase)
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, -1, 3, 0, -8, is_scale_normalize: false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_CRYSTAL_SHOP_GG, 0, 3, 0, -8, is_scale_normalize: false);
			}
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !isHighlightPikeShop && !ShouldEnableGiftIcon())
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	private void OnCloseDialog_HomePointShop()
	{
		if (IsActive(UI.SPR_MENU_GG))
		{
			SetBadge((Enum)UI.BTN_POINT_SHOP_GG, 0, 3, 0, -8, is_scale_normalize: false);
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !ShouldEnableGiftIcon())
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
		isHighlightPikeShop = false;
	}

	private IEnumerator WaitForCheckpikeShop()
	{
		isHighlightPikeShop = false;
		Protocol.SendAsync("ajax/pointshop/list", null, delegate(PointShopModel ret)
		{
			if (ret.Error == Error.None)
			{
				bool flag = PlayerPrefs.GetInt("Pike_Shop_Event", 0) == 1;
				isHighlightPikeShop = ret.result.Any((PointShop x) => x.isEvent);
				if (isHighlightPikeShop)
				{
					if (flag)
					{
						isHighlightPikeShop = false;
					}
				}
				else
				{
					PlayerPrefs.SetInt("Pike_Shop_Event", 0);
				}
			}
		}, string.Empty);
		yield break;
	}
}
