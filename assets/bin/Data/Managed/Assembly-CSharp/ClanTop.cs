// Decompiled with JetBrains decompiler
// Type: ClanTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

#nullable disable
public class ClanTop : HomeBase
{
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
    this.chairBtn = this.GetCtrl((Enum) ClanTop.UI.BTN_CHAIR);
    ((Component) this).gameObject.AddComponent<ClanTopBalloonControl>();
    base.Initialize();
    this.SetActive((Enum) ClanTop.UI.BTN_TRADING_POST, TradingPostManager.IsTradingEnable());
  }

  public override void StartSection() => base.StartSection();

  protected override void InitializeChat()
  {
    base.InitializeChat();
    MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(false);
    MonoBehaviourSingleton<UIManager>.I.mainChat.HomeType = MainChat.HOME_TYPE.CLAN_TOP;
  }

  protected override void AddChatClickDelegate(UIButton btnChat)
  {
    btnChat.onClick.Add(new EventDelegate(new EventDelegate.Callback(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowFull)));
  }

  protected override void UpdateUIOfTutorial()
  {
    int num = HomeTutorialManager.ShouldRunGachaTutorial() ? 1 : 0;
    bool flag = TutorialStep.HasAllTutorialCompleted();
    this.UpdateCommunityButton(num == 0 && flag);
    base.UpdateUIOfTutorial();
  }

  private void UpdateCommunityButton(bool _visible)
  {
    Transform ctrl = this.GetCtrl((Enum) ClanTop.UI.BTN_COMMUNITY);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    this.SetActive(ctrl, _visible);
    if (!_visible || !MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    this.UpdateCommunityBadge();
  }

  protected override bool CheckInvitedLoungeBySNS() => false;

  protected override void SetIconAndBalloon()
  {
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
    {
      Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/BINGO_ICON_POS");
      if (Object.op_Inequality((Object) transform, (Object) null))
        this.loungeQuestIconPos = transform.position;
    }
    this.CreateClanBoardIcon();
    base.SetIconAndBalloon();
  }

  private void CreateClanBoardIcon()
  {
  }

  public override void UpdateUI()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0)
      this.SetGoingHomeEvent();
    base.UpdateUI();
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.chairBtn, (Object) null) || !((Component) this.chairBtn).gameObject.activeSelf || Object.op_Equality((Object) this.nearChairPoint, (Object) null) || !Object.op_Inequality((Object) this.nearChairPoint.sittingChara, (Object) null))
      return;
    this.SetActive((Enum) ClanTop.UI.BTN_CHAIR, false);
  }

  protected override void LateUpdate() => base.LateUpdate();

  private IEnumerator UpdateClanQuestBalloon()
  {
    yield break;
  }

  protected override void SetActiveAreaEventButton(string btnName, bool active)
  {
    if (!(btnName == "BTN_CHAIR") || !MonoBehaviourSingleton<ClanManager>.IsValid())
      return;
    this.nearChairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara._transform.position);
    if (Object.op_Inequality((Object) this.nearChairPoint.sittingChara, (Object) null))
      this.SetActive((Enum) ClanTop.UI.BTN_CHAIR, false);
    else
      this.SetActive((Enum) ClanTop.UI.BTN_CHAIR, active);
  }

  private void Sit()
  {
    this.SetActiveAreaEventButton("BTN_CHAIR", false);
    MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara.Sit();
    MonoBehaviourSingleton<ClanManager>.I.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.SITTING);
  }

  private void OnQuery_POINT_SHOP()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      GameSection.StopEvent();
    else
      GameSection.ChangeEvent("POINT_SHOP_FROM_BUTTON");
  }

  private void OnQuery_MENU_ACTION()
  {
    bool is_visible = !this.IsActive((Enum) ClanTop.UI.SPR_MENU_GG);
    this.SetActive((Enum) ClanTop.UI.SPR_MENU_GG, is_visible);
    this.SetActive((Enum) ClanTop.UI.BTN_MENU_GG_ON, !is_visible);
    this.SetActive((Enum) ClanTop.UI.OBJ_MENU_GIFT_ON, this.IsActive((Enum) ClanTop.UI.BTN_MENU_GG_ON));
    this.SetActive((Enum) ClanTop.UI.BTN_MENU_GG_OFF, is_visible);
    if (is_visible)
    {
      if (GameSaveData.instance.IsShowNewsNotification())
        this.SetBadge((Enum) ClanTop.UI.BTN_GOWRAP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) ClanTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 3, 0, 0);
      if (this.isHighlightPurchase)
        this.SetBadge((Enum) ClanTop.UI.BTN_CRYSTAL_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) ClanTop.UI.BTN_CRYSTAL_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
      if (this.isHighlightPikeShop)
        this.SetBadge((Enum) ClanTop.UI.BTN_POINT_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) ClanTop.UI.BTN_POINT_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
      this.SetActive((Enum) ClanTop.UI.OBJ_MENU_GIFT_ON, true);
    else
      this.SetActive((Enum) ClanTop.UI.OBJ_MENU_GIFT_ON, false);
  }

  protected override void OnQuery_COMMUNITY()
  {
    base.OnQuery_COMMUNITY();
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData != null)
      return;
    GameSection.ChangeEvent("CLAN_NO_CLAN_ERROR");
  }

  protected void OnCloseDialog_ClanKickedDialog() => this.SetGoingHomeEvent();

  protected void OnCloseDialog_ClanAFKKickedDialog() => this.SetGoingHomeEvent();

  protected void OnCloseDialog_ClanNoClanDialog() => this.SetGoingHomeEvent();

  protected void OnCloseDialog_ClanSecessionDialog() => this.SetGoingHomeEvent();

  protected void OnQuery_ClanRoomRankUp_OK() => this.SetGoingHomeEvent();

  private void OnCloseDialog_MenuTop()
  {
    if (this.IsActive((Enum) ClanTop.UI.SPR_MENU_GG))
    {
      if (GameSaveData.instance.IsShowNewsNotification())
        this.SetBadge((Enum) ClanTop.UI.BTN_GOWRAP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) ClanTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else
    {
      if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
        return;
      this.SetActive((Enum) ClanTop.UI.OBJ_MENU_GIFT_ON, false);
    }
  }

  private void OnCloseDialog_CrystalShopTop()
  {
    this.CheckHighlightPurchase();
    if (this.IsActive((Enum) ClanTop.UI.SPR_MENU_GG))
    {
      if (this.isHighlightPurchase)
        this.SetBadge((Enum) ClanTop.UI.BTN_CRYSTAL_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) ClanTop.UI.BTN_CRYSTAL_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else
    {
      if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
        return;
      this.SetActive((Enum) ClanTop.UI.OBJ_MENU_GIFT_ON, false);
    }
  }

  private void OnCloseDialog_HomePointShop()
  {
    if (this.IsActive((Enum) ClanTop.UI.SPR_MENU_GG))
      this.SetBadge((Enum) ClanTop.UI.BTN_POINT_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    else if (!this.isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !this.ShouldEnableGiftIcon())
      this.SetActive((Enum) ClanTop.UI.OBJ_MENU_GIFT_ON, false);
    this.isHighlightPikeShop = false;
  }

  protected void OnQuery_NOTICE_BOARD()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestNoticeBoard((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void SetGoingHomeEvent()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData("MAIN_MENU_HOME", (object) null)
    });
  }

  private void OnQuery_CHAIR()
  {
    if (MonoBehaviourSingleton<ClanManager>.I.HomeCamera.viewMode == HomeCamera.VIEW_MODE.SITTING)
      return;
    this.Sit();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((GameSection.NOTIFY_FLAG.TRANSITION_END & flags) != (GameSection.NOTIFY_FLAG) 0 && MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.clanCreate, (Object) null))
    {
      MonoBehaviourSingleton<UIManager>.I.clanCreate.ClearAnnounce();
      int num1 = int.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
      if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.isClanCreatedNow)
      {
        MonoBehaviourSingleton<UIManager>.I.clanCreate.Play(false, (System.Action) null, UIClanCreateAnnounce.eType.Create);
        MonoBehaviourSingleton<ClanMatchingManager>.I.OnCreateAnnounce();
        PlayerPrefs.SetInt("CLAN_LAST_IDL_KEY", num1);
        PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", 1);
        PlayerPrefs.Save();
      }
      else
      {
        int num2 = PlayerPrefs.GetInt("CLAN_LAST_IDL_KEY", -1);
        int num3 = PlayerPrefs.GetInt("CLAN_LAST_LEVEL_KEY", 1);
        if (num1 == num2)
        {
          if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData != null && MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level > num3)
          {
            if (num3 != 0)
              MonoBehaviourSingleton<UIManager>.I.clanCreate.Play(type: UIClanCreateAnnounce.eType.LevelUp);
            PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level);
            PlayerPrefs.Save();
          }
        }
        else
        {
          PlayerPrefs.SetInt("CLAN_CHAT_READ_ID_KEY", -1);
          PlayerPrefs.SetInt("CLAN_LAST_IDL_KEY", num1);
          PlayerPrefs.SetInt("CLAN_LAST_LEVEL_KEY", MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level);
          PlayerPrefs.Save();
        }
      }
    }
    base.OnNotify(flags);
  }

  protected override void CheckEventLock()
  {
    if (!MonoBehaviourSingleton<ClanManager>.IsValid() || this.isEventLockLoading)
      return;
    if (Object.op_Equality((Object) this.eventLockMesh, (Object) null))
      this.StartCoroutine(this.LoadEventLock());
    else
      ((Component) this.eventLockMesh).gameObject.SetActive((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel);
  }

  protected IEnumerator LoadEventLock()
  {
    this.isEventLockLoading = true;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.NPC_MODEL, "lockeventbanner", new string[1]
    {
      "LockEventBanner"
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    while (this.waitEventBalloon)
      yield return (object) null;
    HomeNPCCharacter homeNpcCharacter = MonoBehaviourSingleton<ClanManager>.I.IHomePeople.GetHomeNPCCharacter(6);
    if (Object.op_Inequality((Object) homeNpcCharacter, (Object) null))
    {
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(0.0f, 1.79f, 0.504f);
      Transform gameObject = Utility.CreateGameObject("EventLockBanner", homeNpcCharacter._transform);
      ResourceUtility.Realizes(loadedArrow.loadedObject, gameObject);
      gameObject.localPosition = vector3;
      if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
        ((Component) gameObject).gameObject.SetActive(true);
      else
        ((Component) gameObject).gameObject.SetActive(false);
      this.eventLockMesh = gameObject;
    }
    this.isEventLockLoading = false;
    yield return (object) null;
  }

  private void CheckHighlightPurchase()
  {
    this.isHighlightPurchase = false;
    if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList == null)
      return;
    string str1 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Gem", string.Empty);
    string str2 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
    string str3 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
    int count = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Count;
    for (int index = 0; index < count; ++index)
    {
      Network.ProductData shop = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList[index];
      if (shop.productType == 1)
      {
        if (str1.IndexOf(shop.productId) < 0)
        {
          this.isHighlightPurchase = true;
          break;
        }
      }
      else if (shop.productType == 2)
      {
        if (str2.IndexOf(shop.productId) < 0)
        {
          this.isHighlightPurchase = true;
          break;
        }
      }
      else if (shop.productType == 3 && str3.IndexOf(shop.productId) < 0)
      {
        this.isHighlightPurchase = true;
        break;
      }
    }
  }

  private IEnumerator WaitForCheckpikeShop()
  {
    this.isHighlightPikeShop = false;
    Protocol.SendAsync<PointShopModel>("ajax/pointshop/list", (WWWForm) null, (Action<PointShopModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      bool flag = PlayerPrefs.GetInt("Pike_Shop_Event", 0) == 1;
      this.isHighlightPikeShop = ret.result.Any<PointShop>((Func<PointShop, bool>) (x => x.isEvent));
      if (this.isHighlightPikeShop)
      {
        if (!flag)
          return;
        this.isHighlightPikeShop = false;
      }
      else
        PlayerPrefs.SetInt("Pike_Shop_Event", 0);
    }));
    yield break;
  }

  private new enum UI
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
    BTN_POINT_SHOP_GG,
  }
}
