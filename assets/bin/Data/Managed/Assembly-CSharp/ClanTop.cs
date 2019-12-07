using Network;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ClanTop : HomeBase
{
	private enum UI
	{
		OBJ_NOTICE,
		LBL_NOTICE,
		BTN_STORAGE,
		BTN_MISSION,
		BTN_TICKET,
		BTN_TRADING_POST,
		BTN_CHAT,
		OBJ_BALOON_ROOT,
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
		SPR_MENU_GG,
		OBJ_MENU_GG,
		BTN_MENU_GG_OFF,
		BTN_GOWRAP_GG,
		BTN_CRYSTAL_SHOP_GG,
		BTN_POINT_SHOP_GG
	}

	private const float RoomPartyInterval = 3f;

	private Vector3 loungeQuestIconPos;

	private Transform loungeQuestBalloon;

	private float roomPartyTimer;

	private Transform chairBtn;

	private ChairPoint nearChairPoint;

	private bool isHighlightPurchase;

	private bool isHighlightPikeShop;

	public override void Initialize()
	{
		chairBtn = GetCtrl(UI.BTN_CHAIR);
		base.gameObject.AddComponent<ClanTopBalloonControl>();
		base.Initialize();
		SetActive(UI.BTN_TRADING_POST, TradingPostManager.IsTradingEnable());
	}

	public override void StartSection()
	{
		base.StartSection();
	}

	protected override void InitializeChat()
	{
		base.InitializeChat();
		MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(active: false);
		MonoBehaviourSingleton<UIManager>.I.mainChat.HomeType = MainChat.HOME_TYPE.CLAN_TOP;
	}

	protected override void AddChatClickDelegate(UIButton btnChat)
	{
		btnChat.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowFull));
	}

	protected override void UpdateUIOfTutorial()
	{
		bool num = HomeTutorialManager.ShouldRunGachaTutorial();
		bool flag = TutorialStep.HasAllTutorialCompleted();
		bool visible = !num && flag;
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
				UpdateCommunityBadge();
			}
		}
	}

	protected override bool CheckInvitedLoungeBySNS()
	{
		return false;
	}

	protected override void SetIconAndBalloon()
	{
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/BINGO_ICON_POS");
			if (transform != null)
			{
				loungeQuestIconPos = transform.position;
			}
		}
		CreateClanBoardIcon();
		base.SetIconAndBalloon();
	}

	private void CreateClanBoardIcon()
	{
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0)
		{
			SetGoingHomeEvent();
		}
		base.UpdateUI();
	}

	private void Update()
	{
		if (!(chairBtn == null) && chairBtn.gameObject.activeSelf && !(nearChairPoint == null) && nearChairPoint.sittingChara != null)
		{
			SetActive(UI.BTN_CHAIR, is_visible: false);
		}
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
	}

	private IEnumerator UpdateClanQuestBalloon()
	{
		yield break;
	}

	protected override void SetActiveAreaEventButton(string btnName, bool active)
	{
		if (btnName == "BTN_CHAIR" && MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			nearChairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara._transform.position);
			if (nearChairPoint.sittingChara != null)
			{
				SetActive(UI.BTN_CHAIR, is_visible: false);
			}
			else
			{
				SetActive(UI.BTN_CHAIR, active);
			}
		}
	}

	private void Sit()
	{
		SetActiveAreaEventButton("BTN_CHAIR", active: false);
		MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara.Sit();
		MonoBehaviourSingleton<ClanManager>.I.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.SITTING);
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

	private void OnQuery_MENU_ACTION()
	{
		bool flag = !IsActive(UI.SPR_MENU_GG);
		SetActive(UI.SPR_MENU_GG, flag);
		SetActive(UI.BTN_MENU_GG_ON, !flag);
		SetActive(UI.OBJ_MENU_GIFT_ON, IsActive(UI.BTN_MENU_GG_ON));
		SetActive(UI.BTN_MENU_GG_OFF, flag);
		if (flag)
		{
			if (GameSaveData.instance.IsShowNewsNotification())
			{
				SetBadge(UI.BTN_GOWRAP_GG, -1, SpriteAlignment.TopRight, 0, -8);
			}
			else
			{
				SetBadge(UI.BTN_GOWRAP_GG, 0, SpriteAlignment.TopRight, 0, 0);
			}
			if (isHighlightPurchase)
			{
				SetBadge(UI.BTN_CRYSTAL_SHOP_GG, -1, SpriteAlignment.TopRight, 0, -8);
			}
			else
			{
				SetBadge(UI.BTN_CRYSTAL_SHOP_GG, 0, SpriteAlignment.TopRight, 0, -8);
			}
			if (isHighlightPikeShop)
			{
				SetBadge(UI.BTN_POINT_SHOP_GG, -1, SpriteAlignment.TopRight, 0, -8);
			}
			else
			{
				SetBadge(UI.BTN_POINT_SHOP_GG, 0, SpriteAlignment.TopRight, 0, -8);
			}
		}
		else if (isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || isHighlightPikeShop || ShouldEnableGiftIcon())
		{
			SetActive(UI.OBJ_MENU_GIFT_ON, is_visible: true);
		}
		else
		{
			SetActive(UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	protected override void OnQuery_COMMUNITY()
	{
		base.OnQuery_COMMUNITY();
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
		{
			GameSection.ChangeEvent("CLAN_NO_CLAN_ERROR");
		}
	}

	protected void OnCloseDialog_ClanKickedDialog()
	{
		SetGoingHomeEvent();
	}

	protected void OnCloseDialog_ClanAFKKickedDialog()
	{
		SetGoingHomeEvent();
	}

	protected void OnCloseDialog_ClanNoClanDialog()
	{
		SetGoingHomeEvent();
	}

	protected void OnCloseDialog_ClanSecessionDialog()
	{
		SetGoingHomeEvent();
	}

	protected void OnQuery_ClanRoomRankUp_OK()
	{
		SetGoingHomeEvent();
	}

	private void OnCloseDialog_MenuTop()
	{
		if (IsActive(UI.SPR_MENU_GG))
		{
			if (GameSaveData.instance.IsShowNewsNotification())
			{
				SetBadge(UI.BTN_GOWRAP_GG, -1, SpriteAlignment.TopRight, 0, -8);
			}
			else
			{
				SetBadge(UI.BTN_GOWRAP_GG, 0, SpriteAlignment.TopRight, 0, -8);
			}
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !isHighlightPikeShop && !ShouldEnableGiftIcon())
		{
			SetActive(UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	private void OnCloseDialog_CrystalShopTop()
	{
		CheckHighlightPurchase();
		if (IsActive(UI.SPR_MENU_GG))
		{
			if (isHighlightPurchase)
			{
				SetBadge(UI.BTN_CRYSTAL_SHOP_GG, -1, SpriteAlignment.TopRight, 0, -8);
			}
			else
			{
				SetBadge(UI.BTN_CRYSTAL_SHOP_GG, 0, SpriteAlignment.TopRight, 0, -8);
			}
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !isHighlightPikeShop && !ShouldEnableGiftIcon())
		{
			SetActive(UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
	}

	private void OnCloseDialog_HomePointShop()
	{
		if (IsActive(UI.SPR_MENU_GG))
		{
			SetBadge(UI.BTN_POINT_SHOP_GG, 0, SpriteAlignment.TopRight, 0, -8);
		}
		else if (!isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !ShouldEnableGiftIcon())
		{
			SetActive(UI.OBJ_MENU_GIFT_ON, is_visible: false);
		}
		isHighlightPikeShop = false;
	}

	protected void OnQuery_NOTICE_BOARD()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestNoticeBoard(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void SetGoingHomeEvent()
	{
		EventData[] autoEvents = new EventData[1]
		{
			new EventData("MAIN_MENU_HOME", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_CHAIR()
	{
		if (MonoBehaviourSingleton<ClanManager>.I.HomeCamera.viewMode != HomeCamera.VIEW_MODE.SITTING)
		{
			Sit();
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((NOTIFY_FLAG.TRANSITION_END & flags) != (NOTIFY_FLAG)0L && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.clanCreate != null)
		{
			MonoBehaviourSingleton<UIManager>.I.clanCreate.ClearAnnounce();
			int num = int.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
			if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.isClanCreatedNow)
			{
				MonoBehaviourSingleton<UIManager>.I.clanCreate.Play();
				MonoBehaviourSingleton<ClanMatchingManager>.I.OnCreateAnnounce();
				PlayerPrefs.SetInt("CLAN_LAST_IDL_KEY", num);
				PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", 1);
				PlayerPrefs.Save();
			}
			else
			{
				int @int = PlayerPrefs.GetInt("CLAN_LAST_IDL_KEY", -1);
				int int2 = PlayerPrefs.GetInt("CLAN_LAST_LEVEL_KEY", 1);
				if (num == @int)
				{
					if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData != null && MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level > int2)
					{
						if (int2 != 0)
						{
							MonoBehaviourSingleton<UIManager>.I.clanCreate.Play(isForcePlay: false, null, UIClanCreateAnnounce.eType.LevelUp);
						}
						PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level);
						PlayerPrefs.Save();
					}
				}
				else
				{
					PlayerPrefs.SetInt("CLAN_CHAT_READ_ID_KEY", -1);
					PlayerPrefs.SetInt("CLAN_LAST_IDL_KEY", num);
					PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level);
					PlayerPrefs.Save();
				}
			}
		}
		base.OnNotify(flags);
	}

	protected override void CheckEventLock()
	{
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
				if (@string.IndexOf(productData.productId) < 0)
				{
					isHighlightPurchase = true;
					return;
				}
			}
			else if (productData.productType == 2)
			{
				if (string2.IndexOf(productData.productId) < 0)
				{
					isHighlightPurchase = true;
					return;
				}
			}
			else if (productData.productType == 3 && string3.IndexOf(productData.productId) < 0)
			{
				break;
			}
			num++;
		}
		isHighlightPurchase = true;
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
		});
		yield break;
	}
}
