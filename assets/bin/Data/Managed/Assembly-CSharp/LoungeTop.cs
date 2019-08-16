using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LoungeTop : HomeBase
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
		BTN_CLAN_SCOUT,
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
		BTN_GUILD_FAVOR_GG
	}

	private bool isHighlightPurchase;

	private bool isHighlightPikeShop;

	private const float RoomPartyInterval = 3f;

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
		MonoBehaviourSingleton<LoungeManager>.I.SetLoungeQuestBalloon(request: true);
		SetActive((Enum)UI.OBJ_MENU_GG, is_visible: true);
		CheckHighlightPurchase();
		if (isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || ShouldEnableGiftIcon())
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	protected override void InitializeChat()
	{
		base.InitializeChat();
		MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(active: false);
		MonoBehaviourSingleton<UIManager>.I.mainChat.HomeType = MainChat.HOME_TYPE.LOUNGE_TOP;
	}

	protected override void AddChatClickDelegate(UIButton btnChat)
	{
		btnChat.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly));
	}

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
		loungeQuestBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeQuestBalloon(GetCtrl(UI.OBJ_BALOON_ROOT));
		loungeQuestBalloon.get_parent().get_gameObject().SetActive(false);
	}

	private void Update()
	{
		if (!(chairBtn == null) && chairBtn.get_gameObject().get_activeSelf() && !(nearChairPoint == null) && nearChairPoint.sittingChara != null)
		{
			SetActive((Enum)UI.BTN_CHAIR, is_visible: false);
		}
	}

	protected override void LateUpdate()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		roomPartyTimer += Time.get_deltaTime();
		if (MonoBehaviourSingleton<LoungeManager>.I.NeedLoungeQuestBalloonUpdate)
		{
			MonoBehaviourSingleton<LoungeManager>.I.SetLoungeQuestBalloon(request: false);
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
					ResetTween(loungeQuestBalloon);
					PlayTween(loungeQuestBalloon, forward: true, null, is_input_block: false);
				}
			}
			else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.parties == null || MonoBehaviourSingleton<LoungeMatchingManager>.I.parties.Count == 0)
			{
				loungeQuestBalloon.get_parent().get_gameObject().SetActive(false);
			}
		}
		base.LateUpdate();
	}

	private IEnumerator UpdateLoungeQuestBalloon()
	{
		if (loungeQuestBalloon == null)
		{
			yield break;
		}
		bool wait = true;
		Protocol.Try(delegate
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRoomParty(delegate
			{
				wait = false;
			});
		});
		while (wait)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.parties != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.parties.Count > 0)
		{
			if (!loungeQuestBalloon.get_parent().get_gameObject().get_activeSelf())
			{
				loungeQuestBalloon.get_parent().get_gameObject().SetActive(true);
				ResetTween(loungeQuestBalloon);
				PlayTween(loungeQuestBalloon, forward: true, null, is_input_block: false);
			}
		}
		else
		{
			loungeQuestBalloon.get_parent().get_gameObject().SetActive(false);
		}
	}

	protected override void SetActiveAreaEventButton(string btnName, bool active)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (btnName != null && btnName == "BTN_CHAIR" && iHomeManager != null)
		{
			nearChairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(iHomeManager.IHomePeople.selfChara._transform.get_position());
			if (nearChairPoint.sittingChara != null)
			{
				SetActive((Enum)UI.BTN_CHAIR, is_visible: false);
			}
			else
			{
				SetActive((Enum)UI.BTN_CHAIR, active);
			}
		}
	}

	private void Sit()
	{
		SetActiveAreaEventButton("BTN_CHAIR", active: false);
		iHomeManager.IHomePeople.selfChara.Sit();
		iHomeManager.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.SITTING);
	}

	protected override void CheckEventLock()
	{
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
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		while (waitEventBalloon)
		{
			yield return null;
		}
		HomeNPCCharacter eventNPC = MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.GetHomeNPCCharacter(6);
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

	private void OnQuery_CHAIR()
	{
		if (iHomeManager.HomeCamera.viewMode != HomeCamera.VIEW_MODE.SITTING)
		{
			Sit();
		}
	}

	private void OnQuery_LOUNGE_QUEST_COUNTER()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR");
		}
	}

	protected override void OnQuery_COMMUNITY()
	{
		base.OnQuery_COMMUNITY();
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR");
		}
	}

	private void OnQuery_LOUNGE_SETTINGS()
	{
		if (!GameSceneManager.isAutoEventSkip)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR");
		}
	}

	private void OnCloseDialog_KickedMessage()
	{
		Protocol.Force(delegate
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
			{
			});
		});
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
