// Decompiled with JetBrains decompiler
// Type: HomeTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

#nullable disable
public class HomeTop : HomeBase
{
  private bool isHighlightPurchase;
  private bool isHighlightPikeShop;

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
      this.SetActive((Enum) HomeTop.UI.OBJ_CLAN_SCOUT, false);
    else if ((GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY & flags) != (GameSection.NOTIFY_FLAG) 0 && MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.clanInviteNum > 0)
      {
        this.SetActive((Enum) HomeTop.UI.OBJ_CLAN_SCOUT, true);
        this.GetComponent<UITweenCtrl>((Enum) HomeTop.UI.BTN_CLAN_SCOUT).Play();
      }
      else
        this.SetActive((Enum) HomeTop.UI.OBJ_CLAN_SCOUT, false);
    }
    base.OnNotify(flags);
  }

  protected override void OnNotifyUpdateUserStatus()
  {
    base.OnNotifyUpdateUserStatus();
    this.RefreshUI();
  }

  public override void Initialize()
  {
    base.Initialize();
    this.SetActive((Enum) HomeTop.UI.BTN_TRADING_POST, TradingPostManager.IsTradingEnable());
  }

  protected override void InitializeChat()
  {
    base.InitializeChat();
    MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(true);
    MonoBehaviourSingleton<UIManager>.I.mainChat.HomeType = MainChat.HOME_TYPE.HOME_TOP;
  }

  protected override void LateUpdate() => base.LateUpdate();

  protected override void AddChatClickDelegate(UIButton btnChat)
  {
    btnChat.onClick.Add(new EventDelegate(new EventDelegate.Callback(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowFullWithEdit)));
  }

  protected override void UpdateUIOfTutorial()
  {
    int num = HomeTutorialManager.ShouldRunGachaTutorial() ? 1 : (HomeTutorialManager.ShouldRunQuestShadowTutorial() ? 1 : 0);
    bool flag = TutorialStep.HasAllTutorialCompleted();
    this.UpdateCommunityButton(num == 0 && flag);
    base.UpdateUIOfTutorial();
  }

  private void UpdateCommunityButton(bool _visible)
  {
    Transform ctrl = this.GetCtrl((Enum) HomeTop.UI.BTN_COMMUNITY);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    this.SetActive(ctrl, _visible);
    if (!_visible)
      return;
    MonoBehaviourSingleton<UserInfoManager>.IsValid();
  }

  protected override bool CheckInvitedLoungeBySNS()
  {
    string str = PlayerPrefs.GetString("il");
    if (string.IsNullOrEmpty(str))
      return false;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue = str;
    PlayerPrefs.SetString("il", "");
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
      MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
    if (!TutorialStep.HasAllTutorialCompleted() || (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
    {
      new EventData("COMMUNITY", (object) null),
      new EventData("LOUNGE", (object) null),
      new EventData("INVITED_LOUNGE", (object) null)
    });
    return true;
  }

  protected override void CheckEventLock()
  {
    if (!MonoBehaviourSingleton<HomeManager>.IsValid() || this.isEventLockLoading)
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
    HomeNPCCharacter homeNpcCharacter = MonoBehaviourSingleton<HomeManager>.I.IHomePeople.GetHomeNPCCharacter(6);
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

  private void OnQuery_POINT_SHOP()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      GameSection.StopEvent();
    else
      GameSection.ChangeEvent("POINT_SHOP_FROM_BUTTON");
  }

  private void OnQuery_GUILD_MESSAGE()
  {
    if (GameSceneManager.isAutoEventSkip)
      return;
    SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
  }

  private void OnQuery_GUILD()
  {
    if (GameSceneManager.isAutoEventSkip)
      return;
    SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
  }

  private void OnQuery_GOWRAP()
  {
    GameSaveData instance = GameSaveData.instance;
    DateTime dateTime = DateTime.UtcNow;
    dateTime = dateTime.AddSeconds(-10800.0);
    int day = dateTime.Day;
    instance.dayShowNewsNotification = day;
    this.SetBadge((Enum) HomeTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 9, 0, 0);
    MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
  }

  private void OnQuery_MENU_ACTION()
  {
    bool is_visible = !this.IsActive((Enum) HomeTop.UI.SPR_MENU_GG);
    this.SetActive((Enum) HomeTop.UI.SPR_MENU_GG, is_visible);
    this.SetActive((Enum) HomeTop.UI.BTN_MENU_GG_ON, !is_visible);
    this.SetActive((Enum) HomeTop.UI.OBJ_MENU_GIFT_ON, this.IsActive((Enum) HomeTop.UI.BTN_MENU_GG_ON));
    this.SetActive((Enum) HomeTop.UI.BTN_MENU_GG_OFF, is_visible);
    if (is_visible)
    {
      if (GameSaveData.instance.IsShowNewsNotification())
        this.SetBadge((Enum) HomeTop.UI.BTN_GOWRAP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) HomeTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 3, 0, 0);
      if (this.isHighlightPurchase)
        this.SetBadge((Enum) HomeTop.UI.BTN_CRYSTAL_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) HomeTop.UI.BTN_CRYSTAL_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
      if (this.isHighlightPikeShop)
        this.SetBadge((Enum) HomeTop.UI.BTN_POINT_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) HomeTop.UI.BTN_POINT_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
      this.SetActive((Enum) HomeTop.UI.OBJ_MENU_GIFT_ON, true);
    else
      this.SetActive((Enum) HomeTop.UI.OBJ_MENU_GIFT_ON, false);
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

  private void OnCloseDialog_MenuTop()
  {
    if (this.IsActive((Enum) HomeTop.UI.SPR_MENU_GG))
    {
      if (GameSaveData.instance.IsShowNewsNotification())
        this.SetBadge((Enum) HomeTop.UI.BTN_GOWRAP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) HomeTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else
    {
      if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
        return;
      this.SetActive((Enum) HomeTop.UI.OBJ_MENU_GIFT_ON, false);
    }
  }

  private void OnCloseDialog_CrystalShopTop()
  {
    this.CheckHighlightPurchase();
    if (this.IsActive((Enum) HomeTop.UI.SPR_MENU_GG))
    {
      if (this.isHighlightPurchase)
        this.SetBadge((Enum) HomeTop.UI.BTN_CRYSTAL_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) HomeTop.UI.BTN_CRYSTAL_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else
    {
      if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
        return;
      this.SetActive((Enum) HomeTop.UI.OBJ_MENU_GIFT_ON, false);
    }
  }

  private void OnCloseDialog_HomePointShop()
  {
    if (this.IsActive((Enum) HomeTop.UI.SPR_MENU_GG))
      this.SetBadge((Enum) HomeTop.UI.BTN_POINT_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    else if (!this.isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !this.ShouldEnableGiftIcon())
      this.SetActive((Enum) HomeTop.UI.OBJ_MENU_GIFT_ON, false);
    this.isHighlightPikeShop = false;
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
    BTN_CLAN_SCOUT,
  }
}
