using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSection : UIBehaviour
{
	[Flags]
	public enum NOTIFY_FLAG : long
	{
		PRETREAT_SCENE = 0x1,
		CHANGED_SCENE = 0x2,
		TRANSITION_END = 0x4,
		RECEIVE_COOP_ROOM_START = 0x8,
		RECEIVE_COOP_ROOM_UPDATE = 0x10,
		UPDATE_PRESENT_LIST = 0x20,
		UPDATE_PRESENT_NUM = 0x40,
		UPDATE_USER_INFO = 0x80,
		UPDATE_USER_STATUS = 0x100,
		UPDATE_EQUIP_CHANGE = 0x200,
		UPDATE_EQUIP_GROW = 0x400,
		UPDATE_EQUIP_EVOLVE = 0x800,
		UPDATE_EQUIP_FAVORITE = 0x1000,
		UPDATE_SKILL_CHANGE = 0x2000,
		UPDATE_SKILL_GROW = 0x4000,
		UPDATE_SKILL_FAVORITE = 0x8000,
		UPDATE_EQUIP_SET = 0x10000,
		UPDATE_ITEM_INVENTORY = 0x20000,
		UPDATE_EQUIP_INVENTORY = 0x40000,
		UPDATE_SKILL_INVENTORY = 0x80000,
		UPDATE_QUEST_ITEM_INVENTORY = 0x100000,
		UPDATE_DELIVERY_UPDATE = 0x200000,
		UPDATE_DELIVERY_OVER = 0x400000,
		UPDATE_SEARCH_ROOM_LIST = 0x800000,
		UPDATE_QUEST_CLEAR_STATUS = 0x1000000,
		UPDATE_FRIEND_LIST = 0x4000000,
		UPDATE_FRIEND_PARAM = 0x8000000,
		UPDATE_GATHER_OBJECT = 0x10000000,
		REMOVE_NEW_ICON = 0x20000000,
		UPDATE_EVENT_BANNER = 0x40000000,
		UPDATE_SMITH_BADGE = 0x80000000,
		UPDATE_INVENTORY_CAPACITY = 0x100000000,
		UPDATE_PARTY_INVITE = 0x200000000,
		UPDATE_EQUIP_ABILITY = 0x400000000,
		UPDATE_TASK_LIST = 0x800000000,
		UPDATE_DEGREE_FRAME = 0x1000000000,
		UPDATE_EQUIP_SET_INFO = 0x2000000000,
		LOUNGE_KICKED = 0x4000000000,
		UPDATE_ABILITY_ITEM_INVENTORY = 0x8000000000,
		UPDATE_ABILITY_ITEM_CHANGE = 0x10000000000,
		UPDATE_GUILD_REQUEST = 0x200,
		UPDATE_ACCESSORY_INVENTORY = 0x400,
		UPDATE_CLAN_APPLY_REQUEST = 0x800,
		FACEBOOK_LOGIN = 0x100000000000,
		UPDATE_RALLY_INVITE = 0x200000000000,
		UPDATE_GUILD_LIST = 0x400000000000,
		UPDATE_DARK_MARKET = 0x800000000000,
		RESET_DARK_MARKET = 0x1000000000000,
		UPDATE_TRADING_POST = 0x2000000000000,
		UPDATE_TRADING_POST_ITEM_DETAIL = 0x4000000000000,
		UPDATE_CLAN_SCOUT_REQUEST = 0x80000,
		UPDATE_LINK_ROB = 0x100000,
		UPDATE_TRADING_POST_SOLD = 0x20000000000000
	}

	private static object[] expandStorageEventData;

	public bool isInitialized
	{
		get;
		private set;
	}

	public bool isExited
	{
		get;
		private set;
	}

	public bool isReOpenInitialized
	{
		get;
		set;
	}

	public bool isLoadedRequireDataTable
	{
		get;
		private set;
	}

	public virtual string overrideBackKeyEvent => null;

	public virtual bool useOnPressBackKey => false;

	public virtual IEnumerable<string> requireDataTable
	{
		get
		{
			yield break;
		}
	}

	public virtual void OnPressBackKey()
	{
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public virtual void Initialize()
	{
		isInitialized = true;
	}

	public virtual void Exit()
	{
		isExited = true;
	}

	public virtual void InitializeReopen()
	{
		isReOpenInitialized = true;
	}

	public virtual void StartSection()
	{
	}

	public virtual EventData CheckAutoEvent(string event_name, object event_data)
	{
		return null;
	}

	protected void DispatchEvent(string event_name, object event_data = null)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("GameSection", base.gameObject, event_name, event_data);
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
		if (event_data != null)
		{
			GameSceneEvent.current.userData = event_data;
		}
	}

	protected static void SetEventData(object event_data)
	{
		GameSceneEvent.current.userData = event_data;
	}

	protected static object GetEventData()
	{
		return GameSceneEvent.current.userData;
	}

	protected static void StopEvent()
	{
		GameSceneEvent.current.isExecute = false;
	}

	protected static void StayEvent()
	{
		GameSceneEvent.Stay();
	}

	protected static void ChangeStayEvent(string event_name, object event_data = null)
	{
		GameSceneEvent.ChangeStay(event_name, event_data);
	}

	protected static void ChangeStayEventData(object event_data)
	{
		GameSceneEvent.ChangeStayEventData(event_data);
	}

	protected static void PushStayEvent()
	{
		GameSceneEvent.PushStay();
	}

	protected static void PopStayEvent()
	{
		GameSceneEvent.PopStay();
	}

	protected static void ResumeEvent(bool is_resume, object userData = null, bool is_send_query = false)
	{
		if (is_resume)
		{
			GameSceneEvent.Resume(userData, is_send_query);
			return;
		}
		GameSceneEvent.Cancel();
		GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
		if (currentSection != null && currentSection.isClose)
		{
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
			{
				return true;
			}
		}
		else
		{
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == requiredItemId, 1);
			if (price_num <= itemNum)
			{
				return true;
			}
		}
		if (change_event_name)
		{
			ChangeEvent("NOT_ENOUGTH");
		}
		return false;
	}

	public void LoadRequireDataTable()
	{
		isLoadedRequireDataTable = false;
		StartCoroutine(WaitDataTable());
	}

	protected IEnumerator WaitDataTable()
	{
		foreach (string dataTable in requireDataTable)
		{
			while (!MonoBehaviourSingleton<DataTableManager>.IsValid())
			{
				yield return null;
			}
			MonoBehaviourSingleton<DataTableManager>.I.ChangePriorityTop(dataTable);
			while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading(dataTable))
			{
				yield return null;
			}
		}
		isLoadedRequireDataTable = true;
	}

	protected void DoWaitProtocolBusyFinish(Action callback)
	{
		StartCoroutine(WaitProtocolBusyFinish(callback));
	}

	protected IEnumerator WaitProtocolBusyFinish(Action callback)
	{
		while (Protocol.isBusy)
		{
			yield return null;
		}
		callback?.Invoke();
	}

	private void OnQuery_ITEM_SHOP()
	{
		StayEvent();
		MonoBehaviourSingleton<ShopManager>.I.SendGetShop(delegate(bool is_success)
		{
			ResumeEvent(is_success);
		});
	}

	private void ChangeScene(string to_scene, string to_section = null)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(to_scene, to_section);
	}

	public static string GetGoingHomeEvent()
	{
		string result = "MAIN_MENU_HOME";
		if (LoungeMatchingManager.IsValidInLounge())
		{
			result = "MAIN_MENU_LOUNGE";
		}
		else if (ClanMatchingManager.IsValidInClan())
		{
			result = "MAIN_MENU_CLAN";
		}
		return result;
	}

	private void OnQuery_MAIN_MENU_HOME()
	{
		ChangeScene("Home", "HomeTop");
	}

	protected void OnQuery_MAIN_MENU_LOUNGE()
	{
		ChangeScene("Lounge", "LoungeTop");
	}

	protected void OnQuery_MAIN_MENU_CLAN()
	{
		ChangeScene("Clan", "ClanTop");
	}

	protected void OnQuery_MAIN_MENU_STATUS()
	{
		ChangeScene("Status", "StatusTop");
	}

	protected void OnQuery_MAIN_MENU_UNIQUE_STATUS()
	{
		ChangeScene("UniqueStatus", "UniqueStatusTop");
	}

	protected void TO_UNIQUE_OR_MAIN_STATUS()
	{
		if (StatusManager.IsUnique())
		{
			OnQuery_MAIN_MENU_UNIQUE_STATUS();
		}
		else
		{
			OnQuery_MAIN_MENU_STATUS();
		}
	}

	private void OnQuery_MAIN_MENU_STUDIO()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge() || MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan())
		{
			SendToStuido();
		}
		ChangeScene("Status", "StatusTop");
	}

	protected virtual void OnQuery_MAIN_MENU_QUEST()
	{
		ChangeScene("WorldMap", "WorldMap");
	}

	private void OnQuery_MAIN_MENU_SHOP()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge() || MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan())
		{
			SendToShop();
		}
		ChangeScene("Shop", "ShopTop");
	}

	private void OnQuery_MAIN_MENU_GACHA()
	{
		ChangeScene("Gacha");
	}

	private void OnQuery_MAIN_MENU_GATHER()
	{
		ChangeScene("Gather", "GatherTop");
	}

	private void OnQuery_MAIN_MENU_MENU()
	{
		if (MonoBehaviourSingleton<UIManager>.I.Find("MenuTop") != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
			return;
		}
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.Untouch();
		}
		ChangeScene("Menu", "MenuTop");
	}

	private void OnQuery_CHAT_AGE_CONFIRM()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			ChangeScene("CommonDialog", "AgeConfirm");
		}
	}

	private void SendToStuido()
	{
		Lounge_Model_RoomAction lounge_Model_RoomAction = new Lounge_Model_RoomAction();
		lounge_Model_RoomAction.id = 1005;
		lounge_Model_RoomAction.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomAction.aid = 5;
		MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction);
	}

	private void SendToShop()
	{
		Lounge_Model_RoomAction lounge_Model_RoomAction = new Lounge_Model_RoomAction();
		lounge_Model_RoomAction.id = 1005;
		lounge_Model_RoomAction.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomAction.aid = 4;
		MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction);
	}

	protected virtual void OnQuery_QUEST_ROOM_IN_GAME()
	{
		QuestTable.QuestTableData table = GetEventData() as QuestTable.QuestTableData;
		bool flag = MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge();
		if (table == null || !flag)
		{
			StopEvent();
			return;
		}
		bool is_free_join = true;
		if (table.questType == QUEST_TYPE.EVENT)
		{
			is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(table.questID, is_free_join);
		StayEvent();
		CoopApp.EnterPartyQuest(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
		{
			bool is_resume = is_s;
			if (is_r)
			{
				if (is_s)
				{
					QuestRoomObserver.OffObserve();
				}
			}
			else if (!is_c && table.questType != QUEST_TYPE.ORDER)
			{
				ChangeStayEvent("COOP_SERVER_INVALID");
				is_resume = true;
			}
			ResumeEvent(is_resume);
		});
	}

	public void OnQuery_IN_GAME_FIELD()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
	}

	public void OnQuery_QUEST_TO_FIELD()
	{
		_OnQuery_FIELD(fromQuest: true);
	}

	public void OnQuery_TUTORIAL_TO_FIELD()
	{
		_OnQuery_FIELD(fromQuest: false);
	}

	protected void _OnQuery_FIELD(bool fromQuest)
	{
		WorldMapOpenNewField.EVENT_TYPE eventType = WorldMapOpenNewField.EVENT_TYPE.NONE;
		uint portal_id = (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.linkFieldPortalID;
		if (fromQuest)
		{
			eventType = WorldMapOpenNewField.EVENT_TYPE.QUEST_TO_FIELD;
			portal_id = MonoBehaviourSingleton<WorldMapManager>.I.GetJumpPortalID();
		}
		else if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.linkFieldPortalID))
		{
			eventType = WorldMapOpenNewField.EVENT_TYPE.ONLY_CAMERA_MOVE;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portal_id, check_dst_quest: false))
		{
			StopEvent();
			return;
		}
		SetEventData(new WorldMapOpenNewField.SectionEventData(eventType, ENEMY_TYPE.BAT));
		StayEvent();
		CoopApp.EnterField(portal_id, 0u, delegate(bool is_matching, bool is_connect, bool is_regist)
		{
			if (!is_connect)
			{
				ChangeStayEvent("COOP_SERVER_INVALID");
				ResumeEvent(is_resume: true);
			}
			else
			{
				ResumeEvent(is_regist);
			}
		});
	}

	private void OnQuery_APP_VERSION_RESTRICTION()
	{
		ChangeScene("CommonDialog", "VersionRestriction");
	}

	protected void OnQuery_VersionRestriction_YES()
	{
		Native.launchMyselfMarket();
	}

	private void OnQuery_APP_VERSION_RESTRICTION_AUTO()
	{
		ChangeScene("CommonDialog", "VersionRestriction_AUTO");
	}

	protected void OnQuery_VersionRestriction_AUTO_YES()
	{
		Native.launchMyselfMarket();
	}

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
		if (CheckCrystal((MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0) ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum : MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId))
		{
			StayEvent();
			DoGacha(delegate(Error ret)
			{
				switch (ret)
				{
				case Error.None:
					if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST)
					{
						ChangeStayEvent("YES_QUEST");
					}
					ResumeEvent(is_resume: true);
					break;
				case Error.ERR_CRYSTAL_NOT_ENOUGH:
					ChangeStayEvent("NOT_ENOUGTH");
					ResumeEvent(is_resume: true);
					break;
				default:
					ResumeEvent(is_resume: false);
					break;
				}
			});
		}
	}

	protected void DoGacha(Action<Error> callback)
	{
		MonoBehaviourSingleton<GachaManager>.I.SendGachaGacha(MonoBehaviourSingleton<GachaManager>.I.selectGacha.gachaId, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId, MonoBehaviourSingleton<GachaManager>.I.selectGacha.productId, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.guaranteeCampaignId, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.campaignType, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.remainCount, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.userCount, MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee.hasFreeGachaReward, MonoBehaviourSingleton<GachaManager>.I.selectGacha.seriesId, callback);
	}

	public void OnQuery_BANNER_GACHA()
	{
		GACHA_TYPE gACHA_TYPE = (GACHA_TYPE)GetEventData();
		EventData[] autoEvents = (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP)) ? new EventData[1]
		{
			new EventData("MAIN_MENU_SHOP", null)
		} : ((gACHA_TYPE != GACHA_TYPE.SKILL) ? new EventData[2]
		{
			new EventData("MAIN_MENU_SHOP", null),
			new EventData("QUEST_GACHA", null)
		} : new EventData[2]
		{
			new EventData("MAIN_MENU_SHOP", null),
			new EventData("MAGI_GACHA", null)
		});
		StopEvent();
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public void OnQuery_BANNER_EVENT_DELIVERY()
	{
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
		{
			EventData[] autoEvents = new EventData[1]
			{
				new EventData("QUEST_LOCK", null)
			};
			StopEvent();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
		else
		{
			int num = (int)GetEventData();
			EventData[] autoEvents2 = new EventData[2]
			{
				new EventData("EVENT_COUNTER", null),
				new EventData("SELECT", num)
			};
			StopEvent();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents2);
		}
	}

	public void OnQuery_BANNER_EXPLORE_DELIVERY()
	{
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
		{
			EventData[] autoEvents = new EventData[1]
			{
				new EventData("QUEST_LOCK", null)
			};
			StopEvent();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
		else
		{
			int num = (int)GetEventData();
			EventData[] autoEvents2 = new EventData[2]
			{
				new EventData("EXPLORE", null),
				new EventData("SELECT_EXPLORE", num)
			};
			StopEvent();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents2);
		}
	}

	public void OnQuery_BANNER_NEWS()
	{
		SetEventData(WebViewManager.CreateNewsWithLinkParamUrl(((int)GetEventData()).ToString()));
		ChangeScene("CommonDialog", "InformationDialog");
	}

	public void OnQuery_BANNER_CRYSTAL_SHOP()
	{
		EventData[] autoEvents = new EventData[2]
		{
			new EventData("MAIN_MENU_SHOP", null),
			new EventData("CRYSTAL_SHOP", null)
		};
		StopEvent();
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public void OnQuery_BANNER_LOGIN_BONUS()
	{
		int num = (int)GetEventData();
		ChangeEvent("LIMITED_LOGIN_BONUS_VIEW", num);
	}

	private void OnQuery_EXPAND_STORAGE()
	{
		expandStorageEventData = null;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		if (userStatus.maxEquipItem == constDefine.INVENTORY_EXTEND_EQUIP_ITEM_MAX && userStatus.maxSkillItem == constDefine.INVENTORY_EXTEND_SKILL_ITEM_MAX)
		{
			ChangeScene("ItemStorage", "ItemStorageNotExpandMessage");
			return;
		}
		int num = Mathf.Min(userStatus.maxEquipItem + constDefine.INVENTORY_EXTEND_EQUIP_ITEM, constDefine.INVENTORY_EXTEND_EQUIP_ITEM_MAX);
		int num2 = Mathf.Min(userStatus.maxSkillItem + constDefine.INVENTORY_EXTEND_SKILL_ITEM, constDefine.INVENTORY_EXTEND_SKILL_ITEM_MAX);
		expandStorageEventData = new object[5]
		{
			constDefine.INVENTORY_EXTEND_USE_CRYSTAL,
			userStatus.maxEquipItem,
			num,
			userStatus.maxSkillItem,
			num2
		};
		SetEventData(expandStorageEventData);
		ChangeScene("ItemStorage", "ItemStorageExpandConfirm");
	}

	private void OnQuery_ItemStorageExpandConfirm_YES()
	{
		if (expandStorageEventData == null)
		{
			Log.Error(LOG.OUTGAME, "EXPAND_STORAGE data is NULL");
			StopEvent();
		}
		else if (CheckCrystal((int)expandStorageEventData[0]))
		{
			SetEventData(expandStorageEventData);
			StayEvent();
			MonoBehaviourSingleton<InventoryManager>.I.SendInventoryExtend(delegate(bool is_success)
			{
				ResumeEvent(is_success);
			});
		}
	}

	public void OpenStorage()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge())
		{
			ChangeScene("ItemStorage", "ItemStorageTop");
			GameSectionHistory.HistoryData[] array = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().ToArray();
			foreach (GameSectionHistory.HistoryData historyData in array)
			{
				if (historyData.sectionName != "HomeTop" && historyData.sectionName != "LoungeTop")
				{
					MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(historyData.sectionName);
				}
			}
		}
		else
		{
			ChangeScene("InGame", "InGameItem");
		}
	}

	public void ToSmith()
	{
		ChangeScene("Status", "StatusToSmith");
	}

	public void ToPointShop()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
		{
			MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(base.sectionData.sectionName);
			ChangeScene("Home", "HomePointShop");
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene")
		{
			MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(base.sectionData.sectionName);
			ChangeScene("Lounge", "HomePointShop");
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
		{
			MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(base.sectionData.sectionName);
			ChangeScene("Clan", "HomePointShop");
		}
		else
		{
			string goingHomeEvent = GetGoingHomeEvent();
			EventData[] autoEvents = new EventData[2]
			{
				new EventData(goingHomeEvent),
				new EventData("POINT_SHOP")
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	public void ToSeriesArena()
	{
		string goingHomeEvent = GetGoingHomeEvent();
		List<EventData> list = new List<EventData>();
		int currentSeriesArenaId = MonoBehaviourSingleton<QuestManager>.I.currentSeriesArenaId;
		list.Add(new EventData(goingHomeEvent));
		list.Add(new EventData("EVENT_COUNTER"));
		list.Add(new EventData("SELECT_SERIES_ARENA"));
		if (currentSeriesArenaId > 0)
		{
			list.Add(new EventData("SELECT_SERIES_ARENA", MonoBehaviourSingleton<QuestManager>.I.currentSeriesArenaId));
		}
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(list.ToArray());
	}

	public void ToGachaQuest()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
		{
			ChangeScene("Home", "QuestAcceptSearchListSelect");
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene")
		{
			ChangeScene("Lounge", "QuestAcceptSearchListSelect");
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
		{
			ChangeScene("Clan", "QuestAcceptSearchListSelect");
		}
		else if (LoungeMatchingManager.IsValidInLounge())
		{
			OnQuery_MAIN_MENU_LOUNGE();
			MonoBehaviourSingleton<LoungeManager>.I.IsJumpToGacha = true;
		}
		else if (ClanMatchingManager.IsValidInClan())
		{
			OnQuery_MAIN_MENU_CLAN();
			MonoBehaviourSingleton<HomeManager>.I.IsJumpToGacha = true;
		}
		else
		{
			OnQuery_MAIN_MENU_HOME();
			MonoBehaviourSingleton<HomeManager>.I.IsJumpToGacha = true;
		}
	}

	private void OnQuery_SCREENSHOT_SHARING()
	{
		MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("share_screenshot", "Social");
		MonoBehaviourSingleton<GGNativeShare>.I.ShareScreenshotWithText();
		if (PlayerPrefs.GetInt("share_screenshot", -1) != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			StayEvent();
			Protocol.Send<ScreenshotSharingModel>(ScreenshotSharingModel.URL, delegate
			{
				ResumeEvent(is_resume: true);
				PlayerPrefs.SetInt("share_screenshot", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			});
		}
	}

	public void OnQuery_FORCE_MOVETO_HOME()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home", "");
	}

	public void OnQuery_FORCE_MOVETO_LOUNGE()
	{
		string roomPass = GetEventData() as string;
		StayEvent();
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendLeave(delegate
			{
				MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(roomPass, delegate(bool isSucceed, Error error)
				{
					if (isSucceed)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge", "");
					}
					ResumeEvent(is_resume: true);
				});
			});
		}
		else
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(roomPass, delegate(bool isSucceed, Error error)
			{
				if (isSucceed)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge", "");
				}
				ResumeEvent(is_resume: true);
			});
		}
	}

	public bool ContainsHistory(string sceneName, string sectionName)
	{
		GameSectionHistory.HistoryData[] array = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().ToArray();
		foreach (GameSectionHistory.HistoryData historyData in array)
		{
			if (historyData.sceneName == sceneName && historyData.sectionName == sectionName)
			{
				return true;
			}
		}
		return false;
	}
}
