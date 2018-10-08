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

	public const int PAGING = 10;

	private List<TheaterModeTable.TheaterModeData> m_canViewStoryList;

	private List<TheaterModeChapterTable.TheaterModeChapterData> m_canViewChapterList;

	private List<TheaterModeChapterTable.TheaterModeChapterData> m_mainChapterList;

	private List<TheaterModeChapterTable.TheaterModeChapterData> m_eventChapterList;

	private Dictionary<int, LoadObject> bannerTable;

	private static TheaterModeModel.Param s_connectCache;

	private bool m_isSelectMain = true;

	private int m_nowPage = 1;

	private int m_pageMax = 1;

	public override string overrideBackKeyEvent => "BUTTONBACK";

	public override void Initialize()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine("DoInitialize");
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
		Dictionary<uint, int> deliveryList = new Dictionary<uint, int>();
		MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterData(delegate(TheaterModeTable.TheaterModeData data)
		{
			if (!((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_00d8: stateMachine*/)._003CcheckList_003E__0.ContainsKey(data.script_id) && !((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_00d8: stateMachine*/)._003CidList_003E__1.ContainsKey(data.script_id))
			{
				((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_00d8: stateMachine*/)._003CcheckList_003E__0.Add(data.script_id, 0);
				((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_00d8: stateMachine*/)._003CidList_003E__1.Add(data.script_id, data.story_id);
				((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_00d8: stateMachine*/)._003CrevIdList_003E__2.Add(data.story_id, data.script_id);
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
				if (data.storyId != 0 && ((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0.ContainsKey(data.storyId))
				{
					if (((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0[data.storyId] == 0)
					{
						((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0[data.storyId] = -1;
					}
					((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_014e: stateMachine*/)._003CquestList_003E__3.Add(data.questID, data.storyId);
					ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(data.questID);
					if (clearStatusQuestData != null && (clearStatusQuestData.questStatus == 3 || clearStatusQuestData.questStatus == 4))
					{
						((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_014e: stateMachine*/)._003CcheckList_003E__0[data.storyId] = 1;
					}
				}
			});
		}
		Singleton<DeliveryTable>.I.AllDeliveryData(delegate(DeliveryTable.DeliveryData data)
		{
			if (data.clearEventID != 0)
			{
				int clearEventID = (int)data.clearEventID;
				if (((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0.ContainsKey(clearEventID))
				{
					if (((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[clearEventID] == 0)
					{
						((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[clearEventID] = -1;
					}
					((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_0164: stateMachine*/)._003CdeliveryList_003E__4.Add(data.id, clearEventID);
					CLEAR_STATUS clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id);
					if (clearStatusDelivery == CLEAR_STATUS.CLEAR || clearStatusDelivery == CLEAR_STATUS.ALL_CLEAR)
					{
						((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_0164: stateMachine*/)._003CcheckList_003E__0[clearEventID] = 1;
					}
				}
			}
		});
		if (s_connectCache == null)
		{
			TheaterModeModel.RequestSendForm send_form = new TheaterModeModel.RequestSendForm();
			foreach (KeyValuePair<uint, int> item in deliveryList)
			{
				int storyId2 = item.Value;
				if (checkList[storyId2] < 1)
				{
					send_form.theaterList.Add(new TheaterModePostData((int)idList[storyId2], (int)item.Key, 0));
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
				((_003CDoInitialize_003Ec__IteratorEA)/*Error near IL_02ee: stateMachine*/)._003CisEndConnection_003E__12 = true;
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
			SetActive((Enum)UI.STR_EVENT_NON_LIST, true);
		}
		else
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, false);
		}
		SetLabelText((Enum)UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, m_nowPage.ToString());
		SetDynamicList((Enum)UI.GRD_EVENT_QUEST, "TheaterModeListItem", dispList.Count, true, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			if (m_isSelectMain)
			{
				SetActive(t, UI.MAIN_BG, true);
				SetActive(t, UI.LBL_CHAPTER_NAME, true);
				SetActive(t, UI.TEX_EVENT_BANNER, false);
				SetLabelText(t, UI.LBL_CHAPTER_NAME, dispList[i].chapter_name);
			}
			else
			{
				bool flag = false;
				Texture2D val = null;
				if (bannerTable.TryGetValue(dispList[i].banner_id, out LoadObject value))
				{
					val = (value.loadedObject as Texture2D);
					if (val != null)
					{
						Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
						SetActive(t2, true);
						SetTexture(t2, val);
						SetActive(t, UI.LBL_CHAPTER_NAME, false);
						SetActive(t, UI.MAIN_BG, false);
						flag = true;
					}
				}
				if (!flag)
				{
					SetActive(t, UI.MAIN_BG, true);
					SetActive(t, UI.LBL_CHAPTER_NAME, true);
					SetActive(t, UI.TEX_EVENT_BANNER, false);
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

	private void OnQuery_SELECT_EVENT()
	{
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
			RefreshUI();
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		m_nowPage = ((m_nowPage <= 1) ? m_pageMax : (m_nowPage - 1));
		RefreshUI();
	}

	private void OnQuery_PAGE_NEXT()
	{
		m_nowPage = ((m_nowPage >= m_pageMax) ? 1 : (m_nowPage + 1));
		RefreshUI();
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
			m_pageMax = list.Count / 10 + 1;
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
}
