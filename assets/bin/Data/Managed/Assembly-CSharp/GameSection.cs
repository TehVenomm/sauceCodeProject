// Decompiled with JetBrains decompiler
// Type: GameSection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GameSection : UIBehaviour
{
  private static object[] expandStorageEventData;

  public bool isInitialized { get; private set; }

  public bool isExited { get; private set; }

  public bool isReOpenInitialized { get; set; }

  public bool isLoadedRequireDataTable { get; private set; }

  public virtual string overrideBackKeyEvent => (string) null;

  public virtual bool useOnPressBackKey => false;

  public virtual void OnPressBackKey()
  {
  }

  protected override void Awake() => base.Awake();

  protected override void OnDestroy() => base.OnDestroy();

  public virtual void Initialize() => this.isInitialized = true;

  public virtual void Exit() => this.isExited = true;

  public virtual void InitializeReopen() => this.isReOpenInitialized = true;

  public virtual void StartSection()
  {
  }

  public virtual EventData CheckAutoEvent(string event_name, object event_data) => (EventData) null;

  public virtual IEnumerable<string> requireDataTable
  {
    get
    {
      yield break;
    }
  }

  protected void DispatchEvent(string event_name, object event_data = null)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (GameSection), ((Component) this).gameObject, event_name, event_data);
  }

  protected void RequestEvent(string event_name, object event_data = null)
  {
    GameSceneEvent.request = new GameSceneEvent();
    GameSceneEvent.request.eventName = event_name;
    GameSceneEvent.request.userData = event_data;
  }

  protected static void ChangeEvent(string event_name, object event_data = null)
  {
    GameSceneEvent.current.eventName = event_name;
    if (event_data == null)
      return;
    GameSceneEvent.current.userData = event_data;
  }

  protected static void SetEventData(object event_data)
  {
    GameSceneEvent.current.userData = event_data;
  }

  protected static object GetEventData() => GameSceneEvent.current.userData;

  protected static void StopEvent() => GameSceneEvent.current.isExecute = false;

  protected static void StayEvent() => GameSceneEvent.Stay();

  protected static void ChangeStayEvent(string event_name, object event_data = null)
  {
    GameSceneEvent.ChangeStay(event_name, event_data);
  }

  protected static void ChangeStayEventData(object event_data)
  {
    GameSceneEvent.ChangeStayEventData(event_data);
  }

  protected static void PushStayEvent() => GameSceneEvent.PushStay();

  protected static void PopStayEvent() => GameSceneEvent.PopStay();

  protected static void ResumeEvent(bool is_resume, object userData = null, bool is_send_query = false)
  {
    if (is_resume)
    {
      GameSceneEvent.Resume(userData, is_send_query);
    }
    else
    {
      GameSceneEvent.Cancel();
      GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
      if (!Object.op_Inequality((Object) currentSection, (Object) null) || !currentSection.isClose)
        return;
      currentSection.Open();
    }
  }

  protected static void CancelEventAndBackSection()
  {
    GameSceneEvent.Cancel();
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
  }

  protected static void BackSection()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
  }

  public static bool CheckCrystal(int price_num, int requiredItemId = 0, bool change_event_name = true)
  {
    if (requiredItemId == 0)
    {
      if (price_num <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal)
        return true;
    }
    else
    {
      int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) requiredItemId), 1);
      if (price_num <= itemNum)
        return true;
    }
    if (change_event_name)
      GameSection.ChangeEvent("NOT_ENOUGTH");
    return false;
  }

  public void LoadRequireDataTable()
  {
    this.isLoadedRequireDataTable = false;
    this.StartCoroutine(this.WaitDataTable());
  }

  protected IEnumerator WaitDataTable()
  {
    foreach (string dataTable in this.requireDataTable)
    {
      while (!MonoBehaviourSingleton<DataTableManager>.IsValid())
        yield return (object) null;
      MonoBehaviourSingleton<DataTableManager>.I.ChangePriorityTop(dataTable);
      while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading(dataTable))
        yield return (object) null;
    }
    this.isLoadedRequireDataTable = true;
  }

  protected void DoWaitProtocolBusyFinish(System.Action callback)
  {
    this.StartCoroutine(this.WaitProtocolBusyFinish(callback));
  }

  protected IEnumerator WaitProtocolBusyFinish(System.Action callback)
  {
    while (Protocol.isBusy)
      yield return (object) null;
    if (callback != null)
      callback();
  }

  private void OnQuery_ITEM_SHOP()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ShopManager>.I.SendGetShop((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void ChangeScene(string to_scene, string to_section = null)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(to_scene, to_section);
  }

  public static string GetGoingHomeEvent()
  {
    string goingHomeEvent = "MAIN_MENU_HOME";
    if (LoungeMatchingManager.IsValidInLounge())
      goingHomeEvent = "MAIN_MENU_LOUNGE";
    else if (ClanMatchingManager.IsValidInClan())
      goingHomeEvent = "MAIN_MENU_CLAN";
    return goingHomeEvent;
  }

  protected void OnQuery_MAIN_MENU_HOME() => this.ChangeScene("Home", "HomeTop");

  protected void OnQuery_MAIN_MENU_LOUNGE() => this.ChangeScene("Lounge", "LoungeTop");

  protected void OnQuery_MAIN_MENU_CLAN() => this.ChangeScene("Clan", "ClanTop");

  protected void OnQuery_MAIN_MENU_STATUS() => this.ChangeScene("Status", "StatusTop");

  protected void OnQuery_MAIN_MENU_UNIQUE_STATUS()
  {
    this.ChangeScene("UniqueStatus", "UniqueStatusTop");
  }

  protected void TO_UNIQUE_OR_MAIN_STATUS()
  {
    if (StatusManager.IsUnique())
      this.OnQuery_MAIN_MENU_UNIQUE_STATUS();
    else
      this.OnQuery_MAIN_MENU_STATUS();
  }

  private void OnQuery_MAIN_MENU_STUDIO()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge() || MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan())
      this.SendToStuido();
    this.ChangeScene("Status", "StatusTop");
  }

  protected virtual void OnQuery_MAIN_MENU_QUEST() => this.ChangeScene("WorldMap", "WorldMap");

  private void OnQuery_MAIN_MENU_SHOP()
  {
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge() || MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan())
      this.SendToShop();
    this.ChangeScene("Shop", "ShopTop");
  }

  private void OnQuery_MAIN_MENU_GACHA() => this.ChangeScene("Gacha");

  private void OnQuery_MAIN_MENU_GATHER() => this.ChangeScene("Gather", "GatherTop");

  private void OnQuery_MAIN_MENU_MENU()
  {
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.Find("MenuTop"), (Object) null))
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
    }
    else
    {
      if (MonoBehaviourSingleton<InputManager>.IsValid())
        MonoBehaviourSingleton<InputManager>.I.Untouch();
      this.ChangeScene("Menu", "MenuTop");
    }
  }

  private void OnQuery_CHAT_AGE_CONFIRM()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    this.ChangeScene("CommonDialog", "AgeConfirm");
  }

  private void SendToStuido()
  {
    Lounge_Model_RoomAction model = new Lounge_Model_RoomAction();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.aid = 5;
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomAction>(model);
  }

  private void SendToShop()
  {
    Lounge_Model_RoomAction model = new Lounge_Model_RoomAction();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.aid = 4;
    MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomAction>(model);
  }

  protected virtual void OnQuery_QUEST_ROOM_IN_GAME()
  {
    QuestTable.QuestTableData table = GameSection.GetEventData() as QuestTable.QuestTableData;
    bool flag = MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge();
    if (table == null || !flag)
    {
      GameSection.StopEvent();
    }
    else
    {
      bool is_free_join = true;
      if (table.questType == QUEST_TYPE.EVENT)
        is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(table.questID, is_free_join);
      GameSection.StayEvent();
      CoopApp.EnterPartyQuest((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) =>
      {
        bool is_resume = is_s;
        if (is_r)
        {
          if (is_s)
            QuestRoomObserver.OffObserve();
        }
        else if (!is_c && table.questType != QUEST_TYPE.ORDER)
        {
          GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
          is_resume = true;
        }
        GameSection.ResumeEvent(is_resume);
      }));
    }
  }

  public void OnQuery_IN_GAME_FIELD()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
  }

  public void OnQuery_QUEST_TO_FIELD() => this._OnQuery_FIELD(true);

  public void OnQuery_TUTORIAL_TO_FIELD() => this._OnQuery_FIELD(false);

  protected void _OnQuery_FIELD(bool fromQuest)
  {
    WorldMapOpenNewField.EVENT_TYPE _eventType = WorldMapOpenNewField.EVENT_TYPE.NONE;
    uint portal_id = (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.linkFieldPortalID;
    if (fromQuest)
    {
      _eventType = WorldMapOpenNewField.EVENT_TYPE.QUEST_TO_FIELD;
      portal_id = MonoBehaviourSingleton<WorldMapManager>.I.GetJumpPortalID();
    }
    else if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.linkFieldPortalID))
      _eventType = WorldMapOpenNewField.EVENT_TYPE.ONLY_CAMERA_MOVE;
    if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portal_id, false))
    {
      GameSection.StopEvent();
    }
    else
    {
      GameSection.SetEventData((object) new WorldMapOpenNewField.SectionEventData(_eventType, ENEMY_TYPE.BAT));
      GameSection.StayEvent();
      CoopApp.EnterField(portal_id, 0U, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
      {
        if (!is_connect)
        {
          GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
          GameSection.ResumeEvent(true);
        }
        else
          GameSection.ResumeEvent(is_regist);
      }));
    }
  }

  private void OnQuery_APP_VERSION_RESTRICTION()
  {
    this.ChangeScene("CommonDialog", "VersionRestriction");
  }

  protected void OnQuery_VersionRestriction_YES() => Native.launchMyselfMarket();

  private void OnQuery_APP_VERSION_RESTRICTION_AUTO()
  {
    this.ChangeScene("CommonDialog", "VersionRestriction_AUTO");
  }

  protected void OnQuery_VersionRestriction_AUTO_YES() => Native.launchMyselfMarket();

  private void OnQuery_EXP_NEXT_SHOW()
  {
    MonoBehaviourSingleton<UIManager>.I.mainStatus.OnQuery_EXP_NEXT_SHOW();
  }

  private void OnQuery_EXP_NEXT_HIDE()
  {
    MonoBehaviourSingleton<UIManager>.I.mainStatus.OnQuery_EXP_NEXT_HIDE();
  }

  private void OnQuery_SHOW_GEMS_DIALOG()
  {
    MonoBehaviourSingleton<UIManager>.I.mainStatus.OnQuery_SHOW_GEMS_DIALOG();
  }

  private void OnQuery_SHOW_PROFILE_DIALOG()
  {
    MonoBehaviourSingleton<UIManager>.I.mainStatus.OnQuery_SHOW_PROFILE_DIALOG();
  }

  protected virtual void OnQuery_GachaConfirm_YES()
  {
    if (!GameSection.CheckCrystal(MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0 ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum : MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId))
      return;
    GameSection.StayEvent();
    this.DoGacha((Action<Error>) (ret =>
    {
      switch (ret)
      {
        case Error.None:
          if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST)
            GameSection.ChangeStayEvent("YES_QUEST");
          GameSection.ResumeEvent(true);
          break;
        case Error.ERR_CRYSTAL_NOT_ENOUGH:
          GameSection.ChangeStayEvent("NOT_ENOUGTH");
          GameSection.ResumeEvent(true);
          break;
        default:
          GameSection.ResumeEvent(false);
          break;
      }
    }));
  }

  protected void DoGacha(Action<Error> callback)
  {
    MonoBehaviourSingleton<GachaManager>.I.SendGachaGacha(MonoBehaviourSingleton<GachaManager>.I.selectGacha.gachaId, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId, MonoBehaviourSingleton<GachaManager>.I.selectGacha.productId, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.guaranteeCampaignId, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.campaignType, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.remainCount, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.userCount, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.hasFreeGachaReward, MonoBehaviourSingleton<GachaManager>.I.selectGacha.seriesId, callback);
  }

  public void OnQuery_BANNER_GACHA()
  {
    GACHA_TYPE eventData = (GACHA_TYPE) GameSection.GetEventData();
    EventData[] event_datas;
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP))
      event_datas = new EventData[1]
      {
        new EventData("MAIN_MENU_SHOP", (object) null)
      };
    else if (eventData == GACHA_TYPE.SKILL)
      event_datas = new EventData[2]
      {
        new EventData("MAIN_MENU_SHOP", (object) null),
        new EventData("MAGI_GACHA", (object) null)
      };
    else
      event_datas = new EventData[2]
      {
        new EventData("MAIN_MENU_SHOP", (object) null),
        new EventData("QUEST_GACHA", (object) null)
      };
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  public void OnQuery_BANNER_EVENT_DELIVERY()
  {
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
    {
      EventData[] event_datas = new EventData[1]
      {
        new EventData("QUEST_LOCK", (object) null)
      };
      GameSection.StopEvent();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
    }
    else
    {
      EventData[] event_datas = new EventData[2]
      {
        new EventData("EVENT_COUNTER", (object) null),
        new EventData("SELECT", (object) (int) GameSection.GetEventData())
      };
      GameSection.StopEvent();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
    }
  }

  public void OnQuery_BANNER_EXPLORE_DELIVERY()
  {
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
    {
      EventData[] event_datas = new EventData[1]
      {
        new EventData("QUEST_LOCK", (object) null)
      };
      GameSection.StopEvent();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
    }
    else
    {
      EventData[] event_datas = new EventData[2]
      {
        new EventData("EXPLORE", (object) null),
        new EventData("SELECT_EXPLORE", (object) (int) GameSection.GetEventData())
      };
      GameSection.StopEvent();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
    }
  }

  public void OnQuery_BANNER_NEWS()
  {
    GameSection.SetEventData((object) WebViewManager.CreateNewsWithLinkParamUrl(((int) GameSection.GetEventData()).ToString()));
    this.ChangeScene("CommonDialog", "InformationDialog");
  }

  public void OnQuery_BANNER_CRYSTAL_SHOP()
  {
    EventData[] event_datas = new EventData[2]
    {
      new EventData("MAIN_MENU_SHOP", (object) null),
      new EventData("CRYSTAL_SHOP", (object) null)
    };
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  public void OnQuery_BANNER_LOGIN_BONUS()
  {
    GameSection.ChangeEvent("LIMITED_LOGIN_BONUS_VIEW", (object) (int) GameSection.GetEventData());
  }

  private void OnQuery_EXPAND_STORAGE()
  {
    GameSection.expandStorageEventData = (object[]) null;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    if (userStatus.maxEquipItem == constDefine.INVENTORY_EXTEND_EQUIP_ITEM_MAX && userStatus.maxSkillItem == constDefine.INVENTORY_EXTEND_SKILL_ITEM_MAX)
    {
      this.ChangeScene("ItemStorage", "ItemStorageNotExpandMessage");
    }
    else
    {
      int num1 = Mathf.Min(userStatus.maxEquipItem + constDefine.INVENTORY_EXTEND_EQUIP_ITEM, constDefine.INVENTORY_EXTEND_EQUIP_ITEM_MAX);
      int num2 = Mathf.Min(userStatus.maxSkillItem + constDefine.INVENTORY_EXTEND_SKILL_ITEM, constDefine.INVENTORY_EXTEND_SKILL_ITEM_MAX);
      GameSection.expandStorageEventData = new object[5]
      {
        (object) constDefine.INVENTORY_EXTEND_USE_CRYSTAL,
        (object) userStatus.maxEquipItem,
        (object) num1,
        (object) userStatus.maxSkillItem,
        (object) num2
      };
      GameSection.SetEventData((object) GameSection.expandStorageEventData);
      this.ChangeScene("ItemStorage", "ItemStorageExpandConfirm");
    }
  }

  private void OnQuery_ItemStorageExpandConfirm_YES()
  {
    if (GameSection.expandStorageEventData == null)
    {
      Log.Error(LOG.OUTGAME, "EXPAND_STORAGE data is NULL");
      GameSection.StopEvent();
    }
    else
    {
      if (!GameSection.CheckCrystal((int) GameSection.expandStorageEventData[0]))
        return;
      GameSection.SetEventData((object) GameSection.expandStorageEventData);
      GameSection.StayEvent();
      MonoBehaviourSingleton<InventoryManager>.I.SendInventoryExtend((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
  }

  public void OpenStorage()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge())
    {
      this.ChangeScene("ItemStorage", "ItemStorageTop");
      foreach (GameSectionHistory.HistoryData historyData in MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().ToArray())
      {
        if (historyData.sectionName != "HomeTop" && historyData.sectionName != "LoungeTop")
          MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(historyData.sectionName);
      }
    }
    else
      this.ChangeScene("InGame", "InGameItem");
  }

  public void ToSmith() => this.ChangeScene("Status", "StatusToSmith");

  public void ToPointShop()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
    {
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(this.sectionData.sectionName);
      this.ChangeScene("Home", "HomePointShop");
    }
    else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene")
    {
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(this.sectionData.sectionName);
      this.ChangeScene("Lounge", "HomePointShop");
    }
    else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
    {
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(this.sectionData.sectionName);
      this.ChangeScene("Clan", "HomePointShop");
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData(GameSection.GetGoingHomeEvent()),
        new EventData("POINT_SHOP")
      });
  }

  public void ToSeriesArena()
  {
    string goingHomeEvent = GameSection.GetGoingHomeEvent();
    List<EventData> eventDataList = new List<EventData>();
    int currentSeriesArenaId = MonoBehaviourSingleton<QuestManager>.I.currentSeriesArenaId;
    eventDataList.Add(new EventData(goingHomeEvent));
    eventDataList.Add(new EventData("EVENT_COUNTER"));
    eventDataList.Add(new EventData("SELECT_SERIES_ARENA"));
    if (currentSeriesArenaId > 0)
      eventDataList.Add(new EventData("SELECT_SERIES_ARENA", (object) MonoBehaviourSingleton<QuestManager>.I.currentSeriesArenaId));
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(eventDataList.ToArray());
  }

  public void ToGachaQuest()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
      this.ChangeScene("Home", "QuestAcceptSearchListSelect");
    else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene")
      this.ChangeScene("Lounge", "QuestAcceptSearchListSelect");
    else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
      this.ChangeScene("Clan", "QuestAcceptSearchListSelect");
    else if (LoungeMatchingManager.IsValidInLounge())
    {
      this.OnQuery_MAIN_MENU_LOUNGE();
      MonoBehaviourSingleton<LoungeManager>.I.IsJumpToGacha = true;
    }
    else if (ClanMatchingManager.IsValidInClan())
    {
      this.OnQuery_MAIN_MENU_CLAN();
      MonoBehaviourSingleton<HomeManager>.I.IsJumpToGacha = true;
    }
    else
    {
      this.OnQuery_MAIN_MENU_HOME();
      MonoBehaviourSingleton<HomeManager>.I.IsJumpToGacha = true;
    }
  }

  private void OnQuery_SCREENSHOT_SHARING()
  {
    MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("share_screenshot", "Social");
    MonoBehaviourSingleton<GGNativeShare>.I.ShareScreenshotWithText();
    if (PlayerPrefs.GetInt("share_screenshot", -1) == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    GameSection.StayEvent();
    Protocol.Send<ScreenshotSharingModel>(ScreenshotSharingModel.URL, (Action<ScreenshotSharingModel>) (ret =>
    {
      GameSection.ResumeEvent(true);
      PlayerPrefs.SetInt("share_screenshot", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    }));
  }

  public void OnQuery_FORCE_MOVETO_HOME()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home", "");
  }

  public void OnQuery_FORCE_MOVETO_LOUNGE()
  {
    string roomPass = GameSection.GetEventData() as string;
    GameSection.StayEvent();
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendLeave((Action<bool>) (isSuccess => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(roomPass, (Action<bool, Error>) ((isSucceed, error) =>
      {
        if (isSucceed)
          MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge", "");
        GameSection.ResumeEvent(true);
      }))));
    else
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(roomPass, (Action<bool, Error>) ((isSucceed, error) =>
      {
        if (isSucceed)
          MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge", "");
        GameSection.ResumeEvent(true);
      }));
  }

  public bool ContainsHistory(string sceneName, string sectionName)
  {
    foreach (GameSectionHistory.HistoryData historyData in MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().ToArray())
    {
      if (historyData.sceneName == sceneName && historyData.sectionName == sectionName)
        return true;
    }
    return false;
  }

  [Flags]
  public enum NOTIFY_FLAG : long
  {
    PRETREAT_SCENE = 1,
    CHANGED_SCENE = 2,
    TRANSITION_END = 4,
    RECEIVE_COOP_ROOM_START = 8,
    RECEIVE_COOP_ROOM_UPDATE = 16, // 0x0000000000000010
    UPDATE_PRESENT_LIST = 32, // 0x0000000000000020
    UPDATE_PRESENT_NUM = 64, // 0x0000000000000040
    UPDATE_USER_INFO = 128, // 0x0000000000000080
    UPDATE_USER_STATUS = 256, // 0x0000000000000100
    UPDATE_EQUIP_CHANGE = 512, // 0x0000000000000200
    UPDATE_EQUIP_GROW = 1024, // 0x0000000000000400
    UPDATE_EQUIP_EVOLVE = 2048, // 0x0000000000000800
    UPDATE_EQUIP_FAVORITE = 4096, // 0x0000000000001000
    UPDATE_SKILL_CHANGE = 8192, // 0x0000000000002000
    UPDATE_SKILL_GROW = 16384, // 0x0000000000004000
    UPDATE_SKILL_FAVORITE = 32768, // 0x0000000000008000
    UPDATE_EQUIP_SET = 65536, // 0x0000000000010000
    UPDATE_ITEM_INVENTORY = 131072, // 0x0000000000020000
    UPDATE_EQUIP_INVENTORY = 262144, // 0x0000000000040000
    UPDATE_SKILL_INVENTORY = 524288, // 0x0000000000080000
    UPDATE_QUEST_ITEM_INVENTORY = 1048576, // 0x0000000000100000
    UPDATE_DELIVERY_UPDATE = 2097152, // 0x0000000000200000
    UPDATE_DELIVERY_OVER = 4194304, // 0x0000000000400000
    UPDATE_SEARCH_ROOM_LIST = 8388608, // 0x0000000000800000
    UPDATE_QUEST_CLEAR_STATUS = 16777216, // 0x0000000001000000
    UPDATE_FRIEND_LIST = 67108864, // 0x0000000004000000
    UPDATE_FRIEND_PARAM = 134217728, // 0x0000000008000000
    UPDATE_GATHER_OBJECT = 268435456, // 0x0000000010000000
    REMOVE_NEW_ICON = 536870912, // 0x0000000020000000
    UPDATE_EVENT_BANNER = 1073741824, // 0x0000000040000000
    UPDATE_SMITH_BADGE = 2147483648, // 0x0000000080000000
    UPDATE_INVENTORY_CAPACITY = 4294967296, // 0x0000000100000000
    UPDATE_PARTY_INVITE = 8589934592, // 0x0000000200000000
    UPDATE_EQUIP_ABILITY = 17179869184, // 0x0000000400000000
    UPDATE_TASK_LIST = 34359738368, // 0x0000000800000000
    UPDATE_DEGREE_FRAME = 68719476736, // 0x0000001000000000
    UPDATE_EQUIP_SET_INFO = 137438953472, // 0x0000002000000000
    LOUNGE_KICKED = 274877906944, // 0x0000004000000000
    UPDATE_ABILITY_ITEM_INVENTORY = 549755813888, // 0x0000008000000000
    UPDATE_ABILITY_ITEM_CHANGE = 1099511627776, // 0x0000010000000000
    UPDATE_GUILD_REQUEST = UPDATE_EQUIP_CHANGE, // 0x0000000000000200
    UPDATE_ACCESSORY_INVENTORY = UPDATE_EQUIP_GROW, // 0x0000000000000400
    UPDATE_CLAN_APPLY_REQUEST = UPDATE_EQUIP_EVOLVE, // 0x0000000000000800
    FACEBOOK_LOGIN = 17592186044416, // 0x0000100000000000
    UPDATE_RALLY_INVITE = 35184372088832, // 0x0000200000000000
    UPDATE_GUILD_LIST = 70368744177664, // 0x0000400000000000
    UPDATE_DARK_MARKET = 140737488355328, // 0x0000800000000000
    RESET_DARK_MARKET = 281474976710656, // 0x0001000000000000
    UPDATE_TRADING_POST = 562949953421312, // 0x0002000000000000
    UPDATE_TRADING_POST_ITEM_DETAIL = 1125899906842624, // 0x0004000000000000
    UPDATE_CLAN_SCOUT_REQUEST = UPDATE_SKILL_INVENTORY, // 0x0000000000080000
    UPDATE_LINK_ROB = UPDATE_QUEST_ITEM_INVENTORY, // 0x0000000000100000
    UPDATE_TRADING_POST_SOLD = 9007199254740992, // 0x0020000000000000
  }
}
