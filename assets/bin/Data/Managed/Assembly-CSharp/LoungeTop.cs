// Decompiled with JetBrains decompiler
// Type: LoungeTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class LoungeTop : HomeBase
{
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
    this.chairBtn = this.GetCtrl((Enum) LoungeTop.UI.BTN_CHAIR);
    base.Initialize();
    this.SetActive((Enum) LoungeTop.UI.BTN_TRADING_POST, TradingPostManager.IsTradingEnable());
  }

  public override void StartSection()
  {
    base.StartSection();
    this.roomPartyTimer = 4f;
    MonoBehaviourSingleton<LoungeManager>.I.SetLoungeQuestBalloon(true);
    this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GG, true);
    this.CheckHighlightPurchase();
    if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.ShouldEnableGiftIcon())
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, true);
    else
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, false);
  }

  protected override void InitializeChat()
  {
    base.InitializeChat();
    MonoBehaviourSingleton<UIManager>.I.mainChat.SetActiveChannelSelect(false);
    MonoBehaviourSingleton<UIManager>.I.mainChat.HomeType = MainChat.HOME_TYPE.LOUNGE_TOP;
  }

  protected override void AddChatClickDelegate(UIButton btnChat)
  {
    btnChat.onClick.Add(new EventDelegate(new EventDelegate.Callback(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly)));
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
      this.SetActive((Enum) LoungeTop.UI.OBJ_CLAN_SCOUT, false);
    else if ((GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY & flags) != (GameSection.NOTIFY_FLAG) 0 && MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.stat == 0)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.clanInviteNum > 0)
      {
        this.SetActive((Enum) LoungeTop.UI.OBJ_CLAN_SCOUT, true);
        this.GetComponent<UITweenCtrl>((Enum) LoungeTop.UI.BTN_CLAN_SCOUT).Play();
      }
      else
        this.SetActive((Enum) LoungeTop.UI.OBJ_CLAN_SCOUT, false);
    }
    base.OnNotify(flags);
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
    if (str.Split('_')[0] == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.loungeNumber)
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[4]
    {
      new EventData("LOUNGE_SETTINGS", (object) null),
      new EventData("EXIT", (object) null),
      new EventData("LOUNGE", (object) null),
      new EventData("INVITED_LOUNGE", (object) null)
    });
    return true;
  }

  protected override void SetIconAndBalloon()
  {
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
    {
      Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/LOUNGE_QUEST_ICON_POS");
      if (Object.op_Inequality((Object) transform, (Object) null))
        this.loungeQuestIconPos = transform.position;
    }
    this.CreateLoungeBoardIcon();
    base.SetIconAndBalloon();
  }

  private void CreateLoungeBoardIcon()
  {
    this.loungeQuestBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeQuestBalloon(this.GetCtrl((Enum) LoungeTop.UI.OBJ_BALOON_ROOT));
    ((Component) this.loungeQuestBalloon.parent).gameObject.SetActive(false);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.chairBtn, (Object) null) || !((Component) this.chairBtn).gameObject.activeSelf || Object.op_Equality((Object) this.nearChairPoint, (Object) null) || !Object.op_Inequality((Object) this.nearChairPoint.sittingChara, (Object) null))
      return;
    this.SetActive((Enum) LoungeTop.UI.BTN_CHAIR, false);
  }

  protected override void LateUpdate()
  {
    this.roomPartyTimer += Time.deltaTime;
    if (MonoBehaviourSingleton<LoungeManager>.I.NeedLoungeQuestBalloonUpdate)
    {
      MonoBehaviourSingleton<LoungeManager>.I.SetLoungeQuestBalloon(false);
      if ((double) this.roomPartyTimer > 3.0)
      {
        this.StartCoroutine(this.UpdateLoungeQuestBalloon());
        this.roomPartyTimer = 0.0f;
      }
    }
    if (Object.op_Inequality((Object) this.loungeQuestBalloon, (Object) null) && ((Component) this.loungeQuestBalloon).gameObject.activeSelf)
      this.SetBalloonPosition(this.loungeQuestBalloon, this.loungeQuestIconPos);
    if (Object.op_Inequality((Object) this.loungeQuestBalloon, (Object) null))
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsRallyInvite)
      {
        if (!((Component) this.loungeQuestBalloon.parent).gameObject.activeSelf)
        {
          ((Component) this.loungeQuestBalloon.parent).gameObject.SetActive(true);
          this.ResetTween(this.loungeQuestBalloon);
          this.PlayTween(this.loungeQuestBalloon, is_input_block: false);
        }
      }
      else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.parties == null || MonoBehaviourSingleton<LoungeMatchingManager>.I.parties.Count == 0)
        ((Component) this.loungeQuestBalloon.parent).gameObject.SetActive(false);
    }
    base.LateUpdate();
  }

  private IEnumerator UpdateLoungeQuestBalloon()
  {
    if (!Object.op_Equality((Object) this.loungeQuestBalloon, (Object) null))
    {
      bool wait = true;
      Protocol.Try((System.Action) (() => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRoomParty((Action<bool, List<PartyModel.Party>>) ((isSuccess, parties) => wait = false))));
      while (wait)
        yield return (object) null;
      if (MonoBehaviourSingleton<LoungeMatchingManager>.I.parties != null && MonoBehaviourSingleton<LoungeMatchingManager>.I.parties.Count > 0)
      {
        if (!((Component) this.loungeQuestBalloon.parent).gameObject.activeSelf)
        {
          ((Component) this.loungeQuestBalloon.parent).gameObject.SetActive(true);
          this.ResetTween(this.loungeQuestBalloon);
          this.PlayTween(this.loungeQuestBalloon, is_input_block: false);
        }
      }
      else
        ((Component) this.loungeQuestBalloon.parent).gameObject.SetActive(false);
    }
  }

  protected override void SetActiveAreaEventButton(string btnName, bool active)
  {
    if (!(btnName == "BTN_CHAIR") || this.iHomeManager == null)
      return;
    this.nearChairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this.iHomeManager.IHomePeople.selfChara._transform.position);
    if (Object.op_Inequality((Object) this.nearChairPoint.sittingChara, (Object) null))
      this.SetActive((Enum) LoungeTop.UI.BTN_CHAIR, false);
    else
      this.SetActive((Enum) LoungeTop.UI.BTN_CHAIR, active);
  }

  private void Sit()
  {
    this.SetActiveAreaEventButton("BTN_CHAIR", false);
    this.iHomeManager.IHomePeople.selfChara.Sit();
    this.iHomeManager.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.SITTING);
  }

  protected override void CheckEventLock()
  {
    if (!MonoBehaviourSingleton<LoungeManager>.IsValid() || this.isEventLockLoading)
      return;
    if (Object.op_Equality((Object) this.eventLockMesh, (Object) null))
      this.StartCoroutine(this.LoadEventLock());
    else if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
      ((Component) this.eventLockMesh).gameObject.SetActive(true);
    else
      ((Component) this.eventLockMesh).gameObject.SetActive(false);
  }

  private IEnumerator LoadEventLock()
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
    HomeNPCCharacter homeNpcCharacter = MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.GetHomeNPCCharacter(6);
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

  private void OnQuery_CHAIR()
  {
    if (this.iHomeManager.HomeCamera.viewMode == HomeCamera.VIEW_MODE.SITTING)
      return;
    this.Sit();
  }

  private void OnQuery_LOUNGE_QUEST_COUNTER()
  {
    if (!GameSceneManager.isAutoEventSkip)
      SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      return;
    GameSection.ChangeEvent("ERROR");
  }

  protected override void OnQuery_COMMUNITY()
  {
    base.OnQuery_COMMUNITY();
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      return;
    GameSection.ChangeEvent("ERROR");
  }

  private void OnQuery_LOUNGE_SETTINGS()
  {
    if (!GameSceneManager.isAutoEventSkip)
      SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      return;
    GameSection.ChangeEvent("ERROR");
  }

  private void OnCloseDialog_KickedMessage()
  {
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (is_success => { }))));
  }

  private void OnQuery_GOWRAP()
  {
    GameSaveData instance = GameSaveData.instance;
    DateTime dateTime = DateTime.UtcNow;
    dateTime = dateTime.AddSeconds(-10800.0);
    int day = dateTime.Day;
    instance.dayShowNewsNotification = day;
    this.SetBadge((Enum) LoungeTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 9, 0, 0);
    MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
  }

  private void OnQuery_MENU_ACTION()
  {
    bool is_visible = !this.IsActive((Enum) LoungeTop.UI.SPR_MENU_GG);
    this.SetActive((Enum) LoungeTop.UI.SPR_MENU_GG, is_visible);
    this.SetActive((Enum) LoungeTop.UI.BTN_MENU_GG_ON, !is_visible);
    this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, this.IsActive((Enum) LoungeTop.UI.BTN_MENU_GG_ON));
    this.SetActive((Enum) LoungeTop.UI.BTN_MENU_GG_OFF, is_visible);
    if (is_visible)
    {
      if (GameSaveData.instance.IsShowNewsNotification())
        this.SetBadge((Enum) LoungeTop.UI.BTN_GOWRAP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) LoungeTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 3, 0, 0);
      if (this.isHighlightPurchase)
        this.SetBadge((Enum) LoungeTop.UI.BTN_CRYSTAL_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) LoungeTop.UI.BTN_CRYSTAL_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
      if (this.isHighlightPikeShop)
        this.SetBadge((Enum) LoungeTop.UI.BTN_POINT_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) LoungeTop.UI.BTN_POINT_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, true);
    else
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, false);
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
    if (this.IsActive((Enum) LoungeTop.UI.SPR_MENU_GG))
    {
      if (GameSaveData.instance.IsShowNewsNotification())
        this.SetBadge((Enum) LoungeTop.UI.BTN_GOWRAP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) LoungeTop.UI.BTN_GOWRAP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else
    {
      if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
        return;
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, false);
    }
  }

  private void OnCloseDialog_CrystalShopTop()
  {
    this.CheckHighlightPurchase();
    if (this.IsActive((Enum) LoungeTop.UI.SPR_MENU_GG))
    {
      if (this.isHighlightPurchase)
        this.SetBadge((Enum) LoungeTop.UI.BTN_CRYSTAL_SHOP_GG, -1, (SpriteAlignment) 3, 0, -8);
      else
        this.SetBadge((Enum) LoungeTop.UI.BTN_CRYSTAL_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    }
    else
    {
      if (this.isHighlightPurchase || GameSaveData.instance.IsShowNewsNotification() || this.isHighlightPikeShop || this.ShouldEnableGiftIcon())
        return;
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, false);
    }
  }

  private void OnCloseDialog_HomePointShop()
  {
    if (this.IsActive((Enum) LoungeTop.UI.SPR_MENU_GG))
      this.SetBadge((Enum) LoungeTop.UI.BTN_POINT_SHOP_GG, 0, (SpriteAlignment) 3, 0, -8);
    else if (!this.isHighlightPurchase && !GameSaveData.instance.IsShowNewsNotification() && !this.ShouldEnableGiftIcon())
      this.SetActive((Enum) LoungeTop.UI.OBJ_MENU_GIFT_ON, false);
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
    BTN_GUILD_FAVOR_GG,
  }
}
