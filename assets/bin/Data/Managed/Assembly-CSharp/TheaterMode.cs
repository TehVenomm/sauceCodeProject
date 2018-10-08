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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		if (array != null)
		{
			m_openChapterId = (int)array[0];
			requestEndEventArray = (array[1] as EventData[]);
		}
		isRenewalFlag = (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal);
		if (isRenewalFlag)
		{
			this.StartCoroutine(DoInitialize2());
		}
		else
		{
			this.StartCoroutine("DoInitialize");
		}
	}

	private IEnumerator DoInitialize()
	{
		Utility.CreateGameObjectAndComponent("TheaterModeTable", this.get_gameObject().get_transform(), -1);
		Utility.CreateGameObjectAndComponent("TheaterModeChapterTable", this.get_gameObject().get_transform(), -1);
		while (MonoBehaviourSingleton<TheaterModeChapterTable>.I.isLoading || MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
		{
			yield return (object)null;
		}
		Dictionary<int, int> checkList = new Dictionary<int, int>();
		Dictionary<int, uint> idList = new Dictionary<int, uint>();
		Dictionary<uint, int> revIdList = new Dictionary<uint, int>();
		new Dictionary<uint, int>();
		Dictionary<uint, List<int>> deliveryList = new Dictionary<uint, List<int>>();
		MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterData(delegate(TheaterModeTable.TheaterModeData data)
		{
			if (!((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_00d8: stateMachine*/)._003CcheckList_003E__0.ContainsKey(data.script_id) && !((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_00d8: stateMachine*/)._003CidList_003E__1.ContainsKey(data.script_id))
			{
				((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_00d8: stateMachine*/)._003CcheckList_003E__0.Add(data.script_id, 0);
				((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_00d8: stateMachine*/)._003CidList_003E__1.Add(data.script_id, data.story_id);
				((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_00d8: stateMachine*/)._003CrevIdList_003E__2.Add(data.story_id, data.script_id);
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
				if (data.storyId != 0 && ((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0.ContainsKey(data.storyId))
				{
					if (((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0[data.storyId] == 0)
					{
						((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0[data.storyId] = -1;
					}
					((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_014e: stateMachine*/)._003CquestList_003E__3.Add(data.questID, data.storyId);
					ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(data.questID);
					if (clearStatusQuestData != null && (clearStatusQuestData.questStatus == 3 || clearStatusQuestData.questStatus == 4))
					{
						((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0[data.storyId] = 1;
					}
				}
			});
		}
		Singleton<DeliveryTable>.I.AllDeliveryData(delegate(DeliveryTable.DeliveryData data)
		{
			if (data.clearEventID != 0)
			{
				int clearEventID = (int)data.clearEventID;
				if (((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0.ContainsKey(clearEventID))
				{
					if (((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[clearEventID] == 0)
					{
						((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[clearEventID] = -1;
					}
					((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003C_003Ef__this.AddDeliveryList(((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CdeliveryList_003E__4, data.id, clearEventID);
					CLEAR_STATUS clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id);
					if (clearStatusDelivery == CLEAR_STATUS.CLEAR || clearStatusDelivery == CLEAR_STATUS.ALL_CLEAR)
					{
						((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[clearEventID] = 1;
					}
				}
			}
			if (data.readScriptId != 0)
			{
				int readScriptId = (int)data.readScriptId;
				if (((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0.ContainsKey(readScriptId))
				{
					if (((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[readScriptId] == 0)
					{
						((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[readScriptId] = -1;
					}
					((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003C_003Ef__this.AddDeliveryList(((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CdeliveryList_003E__4, data.id, readScriptId);
					CLEAR_STATUS clearStatusDelivery2 = MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id);
					if (clearStatusDelivery2 == CLEAR_STATUS.CLEAR || clearStatusDelivery2 == CLEAR_STATUS.ALL_CLEAR)
					{
						((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[readScriptId] = 1;
					}
				}
			}
		});
		if (s_connectCache == null)
		{
			TheaterModeModel.RequestSendForm send_form = new TheaterModeModel.RequestSendForm();
			foreach (KeyValuePair<uint, List<int>> item in deliveryList)
			{
				List<int> storyIdList = item.Value;
				if (storyIdList != null && storyIdList.Count >= 1)
				{
					int m = 0;
					for (int length = storyIdList.Count; m < length; m++)
					{
						int storyId2 = storyIdList[m];
						if (checkList[storyId2] < 1)
						{
							send_form.theaterList.Add(new TheaterModePostData((int)idList[storyId2], (int)item.Key, 0));
						}
					}
				}
			}
			foreach (KeyValuePair<int, int> item2 in checkList)
			{
				int storyId = item2.Key;
				if (checkList[storyId] == 0)
				{
					send_form.theaterList.Add(new TheaterModePostData((int)idList[storyId], 0, storyId));
				}
			}
			bool isEndConnection = false;
			Protocol.Send(TheaterModeModel.URL, send_form, delegate(TheaterModeModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret == null || ret.result == null || ret.result.theaterList == null)
					{
						return;
					}
					s_connectCache = ret.result;
				}
				((_003CDoInitialize_003Ec__IteratorFB)/*Error near IL_0362: stateMachine*/)._003CisEndConnection_003E__15 = true;
			}, string.Empty);
			while (!isEndConnection)
			{
				yield return (object)null;
			}
		}
		for (int l = 0; l < s_connectCache.theaterList.Count; l++)
		{
			if (s_connectCache.theaterList[l].isOpen)
			{
				checkList[revIdList[(uint)s_connectCache.theaterList[l].theaterId]] = 1;
			}
			else
			{
				checkList[revIdList[(uint)s_connectCache.theaterList[l].theaterId]] = -1;
			}
		}
		m_canViewStoryList = MonoBehaviourSingleton<TheaterModeTable>.I.GetTableFromOKDic(checkList);
		List<uint> chapterList = new List<uint>();
		int k = 0;
		for (int count2 = m_canViewStoryList.Count; k < count2; k++)
		{
			uint chapter = (uint)m_canViewStoryList[k].chapter_id;
			if (!chapterList.Contains(chapter))
			{
				chapterList.Add(chapter);
			}
		}
		m_canViewChapterList = MonoBehaviourSingleton<TheaterModeChapterTable>.I.GetPickedData(chapterList);
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
			if (a.order != 0)
			{
				return 1;
			}
			return (int)(b.chapter_id - a.chapter_id);
		});
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bannerTable = new Dictionary<int, LoadObject>(m_canViewChapterList.Count);
		for (int j = 0; j < m_canViewChapterList.Count; j++)
		{
			TheaterModeChapterTable.TheaterModeChapterData table = m_canViewChapterList[j];
			if (!bannerTable.ContainsKey(table.banner_id))
			{
				string bannerImg = ResourceName.GetEventBanner(table.banner_id);
				LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, bannerImg, false);
				bannerTable.Add(table.banner_id, obj);
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		m_mainChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		m_eventChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
		int i = 0;
		for (int count = m_canViewChapterList.Count; i < count; i++)
		{
			if (m_canViewChapterList[i].is_main == 0)
			{
				m_eventChapterList.Add(m_canViewChapterList[i]);
			}
			else
			{
				m_mainChapterList.Add(m_canViewChapterList[i]);
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

	public unsafe override void UpdateUI()
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
			int i = 0;
			for (int count = dispList.Count; i < count; i++)
			{
				if (i >= (m_nowPage - 1) * 10 && i < m_nowPage * 10)
				{
					list.Add(dispList[i]);
				}
			}
			dispList = list;
		}
		if (dispList == null || dispList.Count == 0)
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, true);
		}
		else
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, false);
		}
		SetLabelText((Enum)UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, m_nowPage.ToString());
		_003CUpdateUI_003Ec__AnonStorey3D6 _003CUpdateUI_003Ec__AnonStorey3D;
		SetDynamicList((Enum)UI.GRD_EVENT_QUEST, "TheaterModeListItem", dispList.Count, true, null, null, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
			SetActive((Enum)UI.BTN_TAB_MAIN_OFF, false);
			SetActive((Enum)UI.BTN_TAB_MAIN_ON, true);
			SetActive((Enum)UI.BTN_TAB_EVENT_OFF, true);
			SetActive((Enum)UI.BTN_TAB_EVENT_ON, false);
			SetActive((Enum)UI.SPR_TAB_EVENT, false);
			SetActive((Enum)UI.SPR_TAB_MAIN, true);
			SetPaging();
			RefreshUI();
		}
	}

	private unsafe void OnQuery_SELECT_EVENT()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (m_isSelectMain)
		{
			m_isSelectMain = false;
			SetActive((Enum)UI.BTN_TAB_MAIN_OFF, true);
			SetActive((Enum)UI.BTN_TAB_MAIN_ON, false);
			SetActive((Enum)UI.BTN_TAB_EVENT_OFF, false);
			SetActive((Enum)UI.BTN_TAB_EVENT_ON, true);
			SetActive((Enum)UI.SPR_TAB_EVENT, true);
			SetActive((Enum)UI.SPR_TAB_MAIN, false);
			SetPaging();
			if (isRenewalFlag)
			{
				if (!IsDownloadingBanner)
				{
					this.StartCoroutine(LoadBannerImage(m_nowPage, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
				}
			}
			else
			{
				RefreshUI();
			}
		}
	}

	private unsafe void OnQuery_PAGE_PREV()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (isRenewalFlag)
		{
			if (!IsDownloadingBanner)
			{
				m_nowPage = ((m_nowPage <= 1) ? m_pageMax : (m_nowPage - 1));
				this.StartCoroutine(LoadBannerImage(m_nowPage, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
			}
		}
		else
		{
			m_nowPage = ((m_nowPage <= 1) ? m_pageMax : (m_nowPage - 1));
			RefreshUI();
		}
	}

	private unsafe void OnQuery_PAGE_NEXT()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (isRenewalFlag)
		{
			if (!IsDownloadingBanner)
			{
				m_nowPage = ((m_nowPage >= m_pageMax) ? 1 : (m_nowPage + 1));
				this.StartCoroutine(LoadBannerImage(m_nowPage, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, true);
			m_pageMax = 1;
		}
		else
		{
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, true);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, false);
			m_pageMax = Mathf.CeilToInt((float)list.Count / 10f);
		}
		SetLabelText((Enum)UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, m_nowPage.ToString());
	}

	private void OnQuery_STORY()
	{
	}

	private void OnQuery_BUTTONBACK()
	{
		s_connectCache = null;
		Debug.Log((object)"CLEAR CACHE!!");
		GameSection.ChangeEvent("[BACK]", null);
	}

	private IEnumerator DoInitialize2()
	{
		yield return (object)InitTables();
		InitChapterTables();
		yield return (object)this.StartCoroutine(LoadBannerImage(1, null));
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
		Utility.CreateGameObjectAndComponent("TheaterModeTable", this.get_gameObject().get_transform(), -1);
		Utility.CreateGameObjectAndComponent("TheaterModeChapterTable", this.get_gameObject().get_transform(), -1);
		while (MonoBehaviourSingleton<TheaterModeChapterTable>.I.isLoading || MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
		{
			yield return (object)null;
		}
		Dictionary<int, int> checkList = new Dictionary<int, int>();
		Dictionary<int, uint> idList = new Dictionary<int, uint>();
		Dictionary<uint, int> revIdList = new Dictionary<uint, int>();
		Dictionary<uint, int> questList = new Dictionary<uint, int>();
		Dictionary<uint, List<int>> deliveryList = new Dictionary<uint, List<int>>();
		Dictionary<int, int> stateList = new Dictionary<int, int>();
		MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterDataDesc(delegate(TheaterModeTable.TheaterModeData data)
		{
			if (!((_003CInitTables_003Ec__IteratorFD)/*Error near IL_00df: stateMachine*/)._003CcheckList_003E__0.ContainsKey(data.script_id) && !((_003CInitTables_003Ec__IteratorFD)/*Error near IL_00df: stateMachine*/)._003CidList_003E__1.ContainsKey(data.script_id))
			{
				((_003CInitTables_003Ec__IteratorFD)/*Error near IL_00df: stateMachine*/)._003CcheckList_003E__0.Add(data.script_id, 0);
				((_003CInitTables_003Ec__IteratorFD)/*Error near IL_00df: stateMachine*/)._003CstateList_003E__5.Add(data.script_id, data.state_id);
				((_003CInitTables_003Ec__IteratorFD)/*Error near IL_00df: stateMachine*/)._003CidList_003E__1.Add(data.script_id, data.story_id);
				((_003CInitTables_003Ec__IteratorFD)/*Error near IL_00df: stateMachine*/)._003CrevIdList_003E__2.Add(data.story_id, data.script_id);
			}
		});
		SetTutorialStoryForceEnableView(checkList);
		SetQuestStorySetting(checkList, stateList, questList);
		SetDeliveryStorySetting(checkList, stateList, deliveryList);
		yield return (object)this.StartCoroutine(RequestPrologueStoryStatusAPI(checkList, deliveryList, idList));
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
						else if (questMgr.IsEventOpen(data.eventId) || isMainStory)
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
		if (i.IsEventOpen(_eventId) || flag)
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
			TheaterModeModel.RequestSendForm send_form = new TheaterModeModel.RequestSendForm();
			foreach (KeyValuePair<uint, List<int>> _delivery in _deliveryList)
			{
				List<int> storyIdList = _delivery.Value;
				if (storyIdList != null && storyIdList.Count >= 1)
				{
					int i = 0;
					for (int length = storyIdList.Count; i < length; i++)
					{
						int storyId2 = storyIdList[i];
						if (_checkList[storyId2] == 0)
						{
							send_form.theaterList.Add(new TheaterModePostData((int)_idList[storyId2], (int)_delivery.Key, 0));
						}
					}
				}
			}
			foreach (KeyValuePair<int, int> _check in _checkList)
			{
				int storyId = _check.Key;
				if (_checkList[storyId] == 0)
				{
					send_form.theaterList.Add(new TheaterModePostData((int)_idList[storyId], 0, storyId));
				}
			}
			bool isEndConnection = false;
			Protocol.Send(TheaterModeModel.URL, send_form, delegate(TheaterModeModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret == null || ret.result == null || ret.result.theaterList == null)
					{
						return;
					}
					s_connectCache = ret.result;
				}
				((_003CRequestPrologueStoryStatusAPI_003Ec__IteratorFE)/*Error near IL_0212: stateMachine*/)._003CisEndConnection_003E__10 = true;
			}, string.Empty);
			while (!isEndConnection)
			{
				yield return (object)null;
			}
		}
	}

	private void CheckPrologueStorySettings(Dictionary<int, int> _checkList, Dictionary<uint, int> _revIdList)
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
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
			if (a.order != 0)
			{
				return 1;
			}
			return (int)(b.chapter_id - a.chapter_id);
		});
		return result;
	}

	private IEnumerator LoadBannerImage(int _nextVisiblePageNum, Action _onCompleteCallback)
	{
		m_isDownloadingBanner = true;
		if (m_canViewChapterList == null || _nextVisiblePageNum < 1)
		{
			m_isDownloadingBanner = false;
			if (_onCompleteCallback != null)
			{
				_onCompleteCallback.Invoke();
			}
		}
		else
		{
			int startIndex = (_nextVisiblePageNum - 1) * 10;
			int bannerCount = (startIndex + 10 <= m_canViewChapterList.Count) ? (startIndex + 10) : m_canViewChapterList.Count;
			LoadingQueue loadingQueue = new LoadingQueue(this);
			prevBannerTable = new Dictionary<int, LoadObject>(bannerTable);
			bannerTable.Clear();
			for (int i = startIndex; i < bannerCount; i++)
			{
				TheaterModeChapterTable.TheaterModeChapterData table = m_canViewChapterList[i];
				if (!bannerTable.ContainsKey(table.banner_id))
				{
					string bannerImg = ResourceName.GetEventBanner(table.banner_id);
					LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, bannerImg, false);
					bannerTable.Add(table.banner_id, obj);
				}
			}
			if (loadingQueue.IsLoading())
			{
				yield return (object)loadingQueue.Wait();
			}
			m_isDownloadingBanner = false;
			if (_onCompleteCallback != null)
			{
				_onCompleteCallback.Invoke();
			}
		}
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
			GameSection.ChangeEvent("BACK", null);
		}
		else
		{
			TheaterModeChapterTable.TheaterModeChapterData chapterData = GetChapterData(m_openChapterId);
			if (chapterData == null)
			{
				GameSection.ChangeEvent("BACK", null);
			}
			else
			{
				GameSection.SetEventData(new object[2]
				{
					GetCanViewChapterList((int)chapterData.chapter_id),
					chapterData.chapter_name
				});
				GameSection.ChangeEvent("STORY", null);
			}
		}
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
