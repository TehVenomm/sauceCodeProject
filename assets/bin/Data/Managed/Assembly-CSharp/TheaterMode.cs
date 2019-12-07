using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheaterMode : GameSection
{
	protected enum UI
	{
		SCR_EVENT_QUEST,
		GRD_EVENT_QUEST,
		TEX_EVENT_BANNER,
		LBL_CHAPTER_NAME,
		MAIN_BG,
		STR_EVENT_NON_LIST,
		BTN_EVENT,
		OBJ_FRAME,
		SPR_BG_FRAME,
		SPR_CLEARED,
		SPR_NEW,
		BTN_TAB_MAIN_ON,
		BTN_TAB_MAIN_OFF,
		BTN_TAB_EVENT_ON,
		BTN_TAB_EVENT_OFF,
		SPR_TAB_MAIN,
		SPR_TAB_EVENT,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_NOW,
		LBL_MAX
	}

	private enum CHECK_VALUE
	{
		FALSE = -1,
		UNKNOWN,
		TRUE,
		MAX
	}

	public const int PAGING = 10;

	private List<TheaterModeTable.TheaterModeData> m_canViewStoryList;

	private List<TheaterModeChapterTable.TheaterModeChapterData> m_canViewChapterList;

	private List<TheaterModeChapterTable.TheaterModeChapterData> m_mainChapterList;

	private List<TheaterModeChapterTable.TheaterModeChapterData> m_eventChapterList;

	private Dictionary<int, LoadObject> bannerTable = new Dictionary<int, LoadObject>(10);

	private Dictionary<int, LoadObject> prevBannerTable = new Dictionary<int, LoadObject>(10);

	private static TheaterModeModel.Param s_connectCache;

	private bool m_isDownloadingBanner;

	private bool m_isSelectMain = true;

	private int m_nowPage = 1;

	private int m_pageMax = 1;

	private bool isRenewalFlag;

	private EventData[] requestEndEventArray;

	private int m_openChapterId = -1;

	public override string overrideBackKeyEvent => "BUTTONBACK";

	protected bool IsDownloadingBanner => m_isDownloadingBanner;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array != null)
		{
			m_openChapterId = (int)array[0];
			requestEndEventArray = (array[1] as EventData[]);
		}
		isRenewalFlag = (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal);
		if (isRenewalFlag)
		{
			StartCoroutine(DoInitialize2());
		}
		else
		{
			StartCoroutine("DoInitialize");
		}
	}

	private IEnumerator DoInitialize()
	{
		Utility.CreateGameObjectAndComponent("TheaterModeTable", base.gameObject.transform);
		Utility.CreateGameObjectAndComponent("TheaterModeChapterTable", base.gameObject.transform);
		while (MonoBehaviourSingleton<TheaterModeChapterTable>.I.isLoading || MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
		{
			yield return null;
		}
		Dictionary<int, int> checkList = new Dictionary<int, int>();
		Dictionary<int, uint> idList = new Dictionary<int, uint>();
		Dictionary<uint, int> revIdList = new Dictionary<uint, int>();
		Dictionary<uint, int> questList = new Dictionary<uint, int>();
		Dictionary<uint, List<int>> deliveryList = new Dictionary<uint, List<int>>();
		MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterData(delegate(TheaterModeTable.TheaterModeData data)
		{
			if (!checkList.ContainsKey(data.script_id) && !idList.ContainsKey(data.script_id))
			{
				checkList.Add(data.script_id, 0);
				idList.Add(data.script_id, data.story_id);
				revIdList.Add(data.story_id, data.script_id);
			}
		});
		if (checkList.ContainsKey(11000001))
		{
			checkList[11000001] = 1;
		}
		if (checkList.ContainsKey(11000002))
		{
			checkList[11000002] = 1;
		}
		if (Singleton<QuestTable>.IsValid() && MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			Singleton<QuestTable>.I.AllQuestData(delegate(QuestTable.QuestTableData data)
			{
				if (data.storyId != 0 && checkList.ContainsKey(data.storyId))
				{
					if (checkList[data.storyId] == 0)
					{
						checkList[data.storyId] = -1;
					}
					questList.Add(data.questID, data.storyId);
					ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(data.questID);
					if (clearStatusQuestData != null && (clearStatusQuestData.questStatus == 3 || clearStatusQuestData.questStatus == 4))
					{
						checkList[data.storyId] = 1;
					}
				}
			});
		}
		Singleton<DeliveryTable>.I.AllDeliveryData(delegate(DeliveryTable.DeliveryData data)
		{
			if (data.clearEventID != 0)
			{
				int clearEventID = (int)data.clearEventID;
				if (checkList.ContainsKey(clearEventID))
				{
					if (checkList[clearEventID] == 0)
					{
						checkList[clearEventID] = -1;
					}
					AddDeliveryList(deliveryList, data.id, clearEventID);
					CLEAR_STATUS clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id);
					if (clearStatusDelivery == CLEAR_STATUS.CLEAR || clearStatusDelivery == CLEAR_STATUS.ALL_CLEAR)
					{
						checkList[clearEventID] = 1;
					}
				}
			}
			if (data.readScriptId != 0)
			{
				int readScriptId = (int)data.readScriptId;
				if (checkList.ContainsKey(readScriptId))
				{
					if (checkList[readScriptId] == 0)
					{
						checkList[readScriptId] = -1;
					}
					AddDeliveryList(deliveryList, data.id, readScriptId);
					CLEAR_STATUS clearStatusDelivery2 = MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id);
					if (clearStatusDelivery2 == CLEAR_STATUS.CLEAR || clearStatusDelivery2 == CLEAR_STATUS.ALL_CLEAR)
					{
						checkList[readScriptId] = 1;
					}
				}
			}
		});
		if (s_connectCache == null)
		{
			TheaterModeModel.RequestSendForm requestSendForm = new TheaterModeModel.RequestSendForm();
			foreach (KeyValuePair<uint, List<int>> item in deliveryList)
			{
				List<int> value = item.Value;
				if (value != null && value.Count >= 1)
				{
					int i = 0;
					for (int count = value.Count; i < count; i++)
					{
						int key = value[i];
						if (checkList[key] < 1)
						{
							requestSendForm.theaterList.Add(new TheaterModePostData((int)idList[key], (int)item.Key, 0));
						}
					}
				}
			}
			foreach (KeyValuePair<int, int> item2 in checkList)
			{
				int key2 = item2.Key;
				if (checkList[key2] == 0)
				{
					requestSendForm.theaterList.Add(new TheaterModePostData((int)idList[key2], 0, key2));
				}
			}
			bool isEndConnection = false;
			Protocol.Send(TheaterModeModel.URL, requestSendForm, delegate(TheaterModeModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret == null || ret.result == null || ret.result.theaterList == null)
					{
						return;
					}
					s_connectCache = ret.result;
				}
				isEndConnection = true;
			});
			while (!isEndConnection)
			{
				yield return null;
			}
		}
		for (int j = 0; j < s_connectCache.theaterList.Count; j++)
		{
			if (s_connectCache.theaterList[j].isOpen)
			{
				checkList[revIdList[(uint)s_connectCache.theaterList[j].theaterId]] = 1;
			}
			else
			{
				checkList[revIdList[(uint)s_connectCache.theaterList[j].theaterId]] = -1;
			}
		}
		m_canViewStoryList = MonoBehaviourSingleton<TheaterModeTable>.I.GetTableFromOKDic(checkList);
		List<uint> list = new List<uint>();
		int k = 0;
		for (int count2 = m_canViewStoryList.Count; k < count2; k++)
		{
			uint chapter_id = (uint)m_canViewStoryList[k].chapter_id;
			if (!list.Contains(chapter_id))
			{
				list.Add(chapter_id);
			}
		}
		m_canViewChapterList = MonoBehaviourSingleton<TheaterModeChapterTable>.I.GetPickedData(list);
		m_canViewChapterList.Sort(delegate(TheaterModeChapterTable.TheaterModeChapterData a, TheaterModeChapterTable.TheaterModeChapterData b)
		{
			if (a.order != 0 && b.order != 0)
			{
				return b.order - a.order;
			}
			if (b.order != 0)
			{
				return -1;
			}
			return (int)((a.order != 0) ? 1 : (b.chapter_id - a.chapter_id));
		});
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bannerTable = new Dictionary<int, LoadObject>(m_canViewChapterList.Count);
		for (int l = 0; l < m_canViewChapterList.Count; l++)
		{
			TheaterModeChapterTable.TheaterModeChapterData theaterModeChapterData = m_canViewChapterList[l];
			if (!bannerTable.ContainsKey(theaterModeChapterData.banner_id))
			{
				string eventBanner = ResourceName.GetEventBanner(theaterModeChapterData.banner_id);
				LoadObject value2 = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.EVENT_ICON, eventBanner);
				bannerTable.Add(theaterModeChapterData.banner_id, value2);
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		m_mainChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		m_eventChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		int m = 0;
		for (int count3 = m_canViewChapterList.Count; m < count3; m++)
		{
			if (m_canViewChapterList[m].is_main == 0)
			{
				m_eventChapterList.Add(m_canViewChapterList[m]);
			}
			else
			{
				m_mainChapterList.Add(m_canViewChapterList[m]);
			}
		}
		SetPaging();
		m_isSelectMain = false;
		OnQuery_SELECT_MAIN();
		base.Initialize();
		if (requestEndEventArray != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(requestEndEventArray);
		}
	}

	public override void UpdateUI()
	{
		List<TheaterModeChapterTable.TheaterModeChapterData> dispList;
		if (m_isSelectMain)
		{
			dispList = m_mainChapterList;
		}
		else
		{
			dispList = m_eventChapterList;
		}
		if (m_pageMax > 1)
		{
			List<TheaterModeChapterTable.TheaterModeChapterData> list = new List<TheaterModeChapterTable.TheaterModeChapterData>();
			int j = 0;
			for (int count = dispList.Count; j < count; j++)
			{
				if (j >= (m_nowPage - 1) * 10 && j < m_nowPage * 10)
				{
					list.Add(dispList[j]);
				}
			}
			dispList = list;
		}
		if (dispList == null || dispList.Count == 0)
		{
			SetActive(UI.STR_EVENT_NON_LIST, is_visible: true);
		}
		else
		{
			SetActive(UI.STR_EVENT_NON_LIST, is_visible: false);
		}
		SetLabelText(UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText(UI.LBL_NOW, m_nowPage.ToString());
		SetDynamicList(UI.GRD_EVENT_QUEST, "TheaterModeListItem", dispList.Count, reset: true, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			if (m_isSelectMain)
			{
				SetActive(t, UI.MAIN_BG, is_visible: true);
				SetActive(t, UI.LBL_CHAPTER_NAME, is_visible: true);
				SetActive(t, UI.TEX_EVENT_BANNER, is_visible: false);
				SetLabelText(t, UI.LBL_CHAPTER_NAME, dispList[i].chapter_name);
			}
			else
			{
				bool flag = false;
				Texture2D texture2D = null;
				if (((!IsDownloadingBanner) ? bannerTable : prevBannerTable).TryGetValue(dispList[i].banner_id, out LoadObject value))
				{
					texture2D = (value.loadedObject as Texture2D);
					if (texture2D != null)
					{
						Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
						SetActive(t2, is_visible: true);
						SetTexture(t2, texture2D);
						SetActive(t, UI.LBL_CHAPTER_NAME, is_visible: false);
						SetActive(t, UI.MAIN_BG, is_visible: false);
						flag = true;
					}
				}
				if (!flag)
				{
					SetActive(t, UI.MAIN_BG, is_visible: true);
					SetActive(t, UI.LBL_CHAPTER_NAME, is_visible: true);
					SetActive(t, UI.TEX_EVENT_BANNER, is_visible: false);
					SetLabelText(t, UI.LBL_CHAPTER_NAME, dispList[i].chapter_name);
				}
			}
			SetEvent(t, "STORY", new object[2]
			{
				GetCanViewChapterList((int)dispList[i].chapter_id),
				dispList[i].chapter_name
			});
		});
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			RefreshUI();
		}
	}

	private List<TheaterModeTable.TheaterModeData> GetCanViewChapterList(int chapter_id)
	{
		List<TheaterModeTable.TheaterModeData> list = new List<TheaterModeTable.TheaterModeData>();
		int i = 0;
		for (int count = m_canViewStoryList.Count; i < count; i++)
		{
			if (m_canViewStoryList[i].chapter_id == chapter_id)
			{
				list.Add(m_canViewStoryList[i]);
			}
		}
		return list;
	}

	private void OnQuery_SELECT_MAIN()
	{
		if (!m_isSelectMain)
		{
			m_isSelectMain = true;
			SetActive(UI.BTN_TAB_MAIN_OFF, is_visible: false);
			SetActive(UI.BTN_TAB_MAIN_ON, is_visible: true);
			SetActive(UI.BTN_TAB_EVENT_OFF, is_visible: true);
			SetActive(UI.BTN_TAB_EVENT_ON, is_visible: false);
			SetActive(UI.SPR_TAB_EVENT, is_visible: false);
			SetActive(UI.SPR_TAB_MAIN, is_visible: true);
			SetPaging();
			RefreshUI();
		}
	}

	private void OnQuery_SELECT_EVENT()
	{
		if (!m_isSelectMain)
		{
			return;
		}
		m_isSelectMain = false;
		SetActive(UI.BTN_TAB_MAIN_OFF, is_visible: true);
		SetActive(UI.BTN_TAB_MAIN_ON, is_visible: false);
		SetActive(UI.BTN_TAB_EVENT_OFF, is_visible: false);
		SetActive(UI.BTN_TAB_EVENT_ON, is_visible: true);
		SetActive(UI.SPR_TAB_EVENT, is_visible: true);
		SetActive(UI.SPR_TAB_MAIN, is_visible: false);
		SetPaging();
		if (isRenewalFlag)
		{
			if (!IsDownloadingBanner)
			{
				StartCoroutine(LoadBannerImage(m_nowPage, delegate
				{
					RefreshUI();
					ReleasePrevBannerImage();
				}));
			}
		}
		else
		{
			RefreshUI();
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		if (isRenewalFlag)
		{
			if (!IsDownloadingBanner)
			{
				m_nowPage = ((m_nowPage > 1) ? (m_nowPage - 1) : m_pageMax);
				StartCoroutine(LoadBannerImage(m_nowPage, delegate
				{
					RefreshUI();
					ReleasePrevBannerImage();
				}));
			}
		}
		else
		{
			m_nowPage = ((m_nowPage > 1) ? (m_nowPage - 1) : m_pageMax);
			RefreshUI();
		}
	}

	private void OnQuery_PAGE_NEXT()
	{
		if (isRenewalFlag)
		{
			if (!IsDownloadingBanner)
			{
				m_nowPage = ((m_nowPage >= m_pageMax) ? 1 : (m_nowPage + 1));
				StartCoroutine(LoadBannerImage(m_nowPage, delegate
				{
					RefreshUI();
					ReleasePrevBannerImage();
				}));
			}
		}
		else
		{
			m_nowPage = ((m_nowPage >= m_pageMax) ? 1 : (m_nowPage + 1));
			RefreshUI();
		}
	}

	private void SetPaging()
	{
		m_nowPage = 1;
		List<TheaterModeChapterTable.TheaterModeChapterData> list = (!m_isSelectMain) ? m_eventChapterList : m_mainChapterList;
		if (list.Count <= 10)
		{
			SetActive(UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive(UI.OBJ_INACTIVE_ROOT, is_visible: true);
			m_pageMax = 1;
		}
		else
		{
			SetActive(UI.OBJ_ACTIVE_ROOT, is_visible: true);
			SetActive(UI.OBJ_INACTIVE_ROOT, is_visible: false);
			m_pageMax = Mathf.CeilToInt((float)list.Count / 10f);
		}
		SetLabelText(UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText(UI.LBL_NOW, m_nowPage.ToString());
	}

	private void OnQuery_STORY()
	{
	}

	private void OnQuery_BUTTONBACK()
	{
		s_connectCache = null;
		GameSection.ChangeEvent("[BACK]");
	}

	private IEnumerator DoInitialize2()
	{
		yield return InitTables();
		InitChapterTables();
		yield return StartCoroutine(LoadBannerImage(1, null));
		SetPaging();
		m_isSelectMain = false;
		OnQuery_SELECT_MAIN();
		base.Initialize();
		if (requestEndEventArray != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(requestEndEventArray);
		}
	}

	private IEnumerator InitTables()
	{
		Utility.CreateGameObjectAndComponent("TheaterModeTable", base.gameObject.transform);
		Utility.CreateGameObjectAndComponent("TheaterModeChapterTable", base.gameObject.transform);
		while (MonoBehaviourSingleton<TheaterModeChapterTable>.I.isLoading || MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
		{
			yield return null;
		}
		Dictionary<int, int> checkList = new Dictionary<int, int>();
		Dictionary<int, uint> idList = new Dictionary<int, uint>();
		Dictionary<uint, int> revIdList = new Dictionary<uint, int>();
		Dictionary<uint, int> questList = new Dictionary<uint, int>();
		Dictionary<uint, List<int>> deliveryList = new Dictionary<uint, List<int>>();
		Dictionary<int, int> stateList = new Dictionary<int, int>();
		MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterDataDesc(delegate(TheaterModeTable.TheaterModeData data)
		{
			if (!checkList.ContainsKey(data.script_id) && !idList.ContainsKey(data.script_id))
			{
				checkList.Add(data.script_id, 0);
				stateList.Add(data.script_id, data.state_id);
				idList.Add(data.script_id, data.story_id);
				revIdList.Add(data.story_id, data.script_id);
			}
		});
		SetTutorialStoryForceEnableView(checkList);
		SetQuestStorySetting(checkList, stateList, questList);
		SetDeliveryStorySetting(checkList, stateList, deliveryList);
		yield return StartCoroutine(RequestPrologueStoryStatusAPI(checkList, deliveryList, idList));
		CheckPrologueStorySettings(checkList, revIdList);
		m_canViewStoryList = MonoBehaviourSingleton<TheaterModeTable>.I.GetTableFromOKDic(checkList);
	}

	private void InitChapterTables()
	{
		List<uint> list = new List<uint>();
		int i = 0;
		for (int count = m_canViewStoryList.Count; i < count; i++)
		{
			uint chapter_id = (uint)m_canViewStoryList[i].chapter_id;
			if (!list.Contains(chapter_id))
			{
				list.Add(chapter_id);
			}
		}
		m_canViewChapterList = GetSortedEnableViewList(list);
		m_mainChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		m_eventChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		int j = 0;
		for (int count2 = m_canViewChapterList.Count; j < count2; j++)
		{
			if (m_canViewChapterList[j].is_main == 0)
			{
				m_eventChapterList.Add(m_canViewChapterList[j]);
			}
			else
			{
				m_mainChapterList.Add(m_canViewChapterList[j]);
			}
		}
	}

	private void SetTutorialStoryForceEnableView(Dictionary<int, int> _checkList)
	{
		if (_checkList != null)
		{
			if (_checkList.ContainsKey(11000001))
			{
				_checkList[11000001] = 1;
			}
			if (_checkList.ContainsKey(11000002))
			{
				_checkList[11000002] = 1;
			}
		}
	}

	private void SetQuestStorySetting(Dictionary<int, int> _checkList, Dictionary<int, int> _stateList, Dictionary<uint, int> _questList)
	{
		if (_checkList != null && _questList != null && Singleton<QuestTable>.IsValid() && MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			QuestManager questMgr = MonoBehaviourSingleton<QuestManager>.I;
			int stateId = 0;
			bool isMainStory = false;
			Singleton<QuestTable>.I.AllQuestDataAsc(delegate(QuestTable.QuestTableData data)
			{
				isMainStory = false;
				int storyId = data.storyId;
				if (storyId != 0 && _checkList.ContainsKey(storyId))
				{
					if (_stateList.TryGetValue(storyId, out stateId))
					{
						if (stateId == 0)
						{
							_checkList[storyId] = -1;
							return;
						}
						isMainStory = (stateId == 1);
					}
					if (!isMainStory || _checkList[storyId] != 1)
					{
						if (_checkList[storyId] == 0)
						{
							_checkList[storyId] = -1;
						}
						if (questMgr.IsFutureEvent(data.eventId))
						{
							_checkList[storyId] = -1;
						}
						else if (questMgr.IsEventOpen(data.eventId) | isMainStory)
						{
							_questList.Add(data.questID, storyId);
							if (questMgr.IsClearQuest(data.questID))
							{
								_checkList[storyId] = 1;
							}
							else
							{
								_checkList[storyId] = -1;
							}
						}
						else
						{
							_questList.Add(data.questID, storyId);
							_checkList[storyId] = 1;
						}
					}
				}
			});
		}
	}

	private void SetDeliveryStorySetting(Dictionary<int, int> _checkList, Dictionary<int, int> _stateList, Dictionary<uint, List<int>> _deliveryList)
	{
		if (Singleton<DeliveryTable>.IsValid())
		{
			Singleton<DeliveryTable>.I.AllDeliveryDataAsc(delegate(DeliveryTable.DeliveryData data)
			{
				SetIndividualDeliveryStorySetting(_checkList, _stateList, _deliveryList, data.id, data.eventID, (int)data.readScriptId);
				SetIndividualDeliveryStorySetting(_checkList, _stateList, _deliveryList, data.id, data.eventID, (int)data.clearEventID);
			});
		}
	}

	private bool SetIndividualDeliveryStorySetting(Dictionary<int, int> _checkList, Dictionary<int, int> _stateList, Dictionary<uint, List<int>> _deliveryList, uint _deliveryId, int _eventId, int storyId)
	{
		if (_checkList == null || _deliveryList == null || _stateList == null)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || !MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			return false;
		}
		QuestManager i = MonoBehaviourSingleton<QuestManager>.I;
		DeliveryManager i2 = MonoBehaviourSingleton<DeliveryManager>.I;
		int value = 0;
		bool flag = false;
		if (_stateList.TryGetValue(storyId, out value))
		{
			flag = (value == 1);
			if (value == 0)
			{
				_checkList[storyId] = -1;
				return true;
			}
		}
		if (i.IsFutureEvent(_eventId))
		{
			if (storyId != 0 && _checkList.ContainsKey(storyId))
			{
				_checkList[storyId] = -1;
				AddDeliveryList(_deliveryList, _deliveryId, storyId);
			}
			return true;
		}
		if (flag && _checkList[storyId] == 1)
		{
			return true;
		}
		if (i.IsEventOpen(_eventId) | flag)
		{
			if (storyId != 0 && _checkList.ContainsKey(storyId))
			{
				if (_checkList[storyId] == 0)
				{
					_checkList[storyId] = -1;
				}
				AddDeliveryList(_deliveryList, _deliveryId, storyId);
				if (i2.IsClearDelivery(_deliveryId))
				{
					_checkList[storyId] = 1;
				}
				else
				{
					_checkList[storyId] = -1;
				}
			}
		}
		else if (_checkList.ContainsKey(storyId))
		{
			_checkList[storyId] = 1;
			AddDeliveryList(_deliveryList, _deliveryId, storyId);
		}
		return true;
	}

	private IEnumerator RequestPrologueStoryStatusAPI(Dictionary<int, int> _checkList, Dictionary<uint, List<int>> _deliveryList, Dictionary<int, uint> _idList)
	{
		if (s_connectCache == null)
		{
			TheaterModeModel.RequestSendForm requestSendForm = new TheaterModeModel.RequestSendForm();
			foreach (KeyValuePair<uint, List<int>> _delivery in _deliveryList)
			{
				List<int> value = _delivery.Value;
				if (value != null && value.Count >= 1)
				{
					int i = 0;
					for (int count = value.Count; i < count; i++)
					{
						int key = value[i];
						if (_checkList[key] == 0)
						{
							requestSendForm.theaterList.Add(new TheaterModePostData((int)_idList[key], (int)_delivery.Key, 0));
						}
					}
				}
			}
			foreach (KeyValuePair<int, int> _check in _checkList)
			{
				int key2 = _check.Key;
				if (_checkList[key2] == 0)
				{
					requestSendForm.theaterList.Add(new TheaterModePostData((int)_idList[key2], 0, key2));
				}
			}
			bool isEndConnection = false;
			Protocol.Send(TheaterModeModel.URL, requestSendForm, delegate(TheaterModeModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret == null || ret.result == null || ret.result.theaterList == null)
					{
						return;
					}
					s_connectCache = ret.result;
				}
				isEndConnection = true;
			});
			while (!isEndConnection)
			{
				yield return null;
			}
		}
	}

	private void CheckPrologueStorySettings(Dictionary<int, int> _checkList, Dictionary<uint, int> _revIdList)
	{
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			return;
		}
		int i = 0;
		for (int count = s_connectCache.theaterList.Count; i < count; i++)
		{
			TheaterModeGetData theaterModeGetData = s_connectCache.theaterList[i];
			if (theaterModeGetData.isOpen)
			{
				_checkList[_revIdList[(uint)theaterModeGetData.theaterId]] = 1;
			}
			else
			{
				_checkList[_revIdList[(uint)theaterModeGetData.theaterId]] = -1;
			}
		}
	}

	private List<TheaterModeChapterTable.TheaterModeChapterData> GetSortedEnableViewList(List<uint> chapterList)
	{
		List<TheaterModeChapterTable.TheaterModeChapterData> result = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		if (!MonoBehaviourSingleton<TheaterModeChapterTable>.IsValid() || chapterList == null || chapterList.Count < 1)
		{
			return result;
		}
		result = MonoBehaviourSingleton<TheaterModeChapterTable>.I.GetPickedData(chapterList);
		result.Sort(delegate(TheaterModeChapterTable.TheaterModeChapterData a, TheaterModeChapterTable.TheaterModeChapterData b)
		{
			if (a.order != 0 && b.order != 0)
			{
				return b.order - a.order;
			}
			if (b.order != 0)
			{
				return -1;
			}
			return (int)((a.order != 0) ? 1 : (b.chapter_id - a.chapter_id));
		});
		return result;
	}

	private IEnumerator LoadBannerImage(int _nextVisiblePageNum, Action _onCompleteCallback)
	{
		m_isDownloadingBanner = true;
		if (m_canViewChapterList == null || _nextVisiblePageNum < 1)
		{
			m_isDownloadingBanner = false;
			_onCompleteCallback?.Invoke();
			yield break;
		}
		int num = (_nextVisiblePageNum - 1) * 10;
		int num2 = (num + 10 > m_canViewChapterList.Count) ? m_canViewChapterList.Count : (num + 10);
		LoadingQueue loadingQueue = new LoadingQueue(this);
		prevBannerTable = new Dictionary<int, LoadObject>(bannerTable);
		bannerTable.Clear();
		for (int i = num; i < num2; i++)
		{
			TheaterModeChapterTable.TheaterModeChapterData theaterModeChapterData = m_canViewChapterList[i];
			if (!bannerTable.ContainsKey(theaterModeChapterData.banner_id))
			{
				string eventBanner = ResourceName.GetEventBanner(theaterModeChapterData.banner_id);
				LoadObject value = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.EVENT_ICON, eventBanner);
				bannerTable.Add(theaterModeChapterData.banner_id, value);
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		m_isDownloadingBanner = false;
		_onCompleteCallback?.Invoke();
	}

	private void ReleasePrevBannerImage()
	{
		if (prevBannerTable.Count >= 1)
		{
			foreach (KeyValuePair<int, LoadObject> item in prevBannerTable)
			{
				item.Value.ReleaseAllResources();
			}
			prevBannerTable.Clear();
		}
	}

	private void AddDeliveryList(Dictionary<uint, List<int>> _deliveryList, uint _addKey, int _addValue)
	{
		List<int> value = null;
		if (_deliveryList.TryGetValue(_addKey, out value))
		{
			if (!value.Contains(_addValue))
			{
				value.Add(_addValue);
			}
		}
		else
		{
			value = new List<int>();
			value.Add(_addValue);
			_deliveryList.Add(_addKey, value);
		}
	}

	private void OnQuery_SELECT_CHAPTER_FROM_OUTER()
	{
		if (m_openChapterId < 0)
		{
			GameSection.ChangeEvent("BACK");
			return;
		}
		TheaterModeChapterTable.TheaterModeChapterData chapterData = GetChapterData(m_openChapterId);
		if (chapterData == null)
		{
			GameSection.ChangeEvent("BACK");
			return;
		}
		GameSection.SetEventData(new object[2]
		{
			GetCanViewChapterList((int)chapterData.chapter_id),
			chapterData.chapter_name
		});
		GameSection.ChangeEvent("STORY");
	}

	private TheaterModeChapterTable.TheaterModeChapterData GetChapterData(int _chapterId)
	{
		if (_chapterId < 0)
		{
			return null;
		}
		TheaterModeChapterTable.TheaterModeChapterData result = null;
		int i = 0;
		for (int count = m_canViewChapterList.Count; i < count; i++)
		{
			if (m_canViewChapterList[i].chapter_id == (uint)_chapterId)
			{
				result = m_canViewChapterList[i];
				break;
			}
		}
		return result;
	}
}
