using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheaterStory : GameSection
{
	protected enum UI
	{
		SCR_LIST,
		GRD_LIST,
		STR_STORY_NON_LIST,
		LBL_CHAPTER_NAME,
		LBL_STORY_TITLE,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_NOW,
		LBL_MAX
	}

	public const int PAGING = 10;

	private List<TheaterModeTable.TheaterModeData> m_canViewStoryList;

	private string m_chapterName = string.Empty;

	private int m_nowPage = 1;

	private int m_pageMax = 1;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		m_canViewStoryList = (array[0] as List<TheaterModeTable.TheaterModeData>);
		m_chapterName = (array[1] as string);
		m_canViewStoryList.Sort(delegate(TheaterModeTable.TheaterModeData a, TheaterModeTable.TheaterModeData b)
		{
			if (b.order != 0 && a.order != 0)
			{
				return a.order - b.order;
			}
			if (b.order != 0)
			{
				return 1;
			}
			if (a.order != 0)
			{
				return -1;
			}
			return (int)(a.story_id - b.story_id);
		});
		SetPaging();
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		yield return (object)null;
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		EventData[] events = new EventData[4]
		{
			new EventData(name, null),
			new EventData("MAIN_MENU_MENU", null),
			new EventData("THEATERMODE", null),
			new EventData("STORY", new object[2]
			{
				m_canViewStoryList,
				m_chapterName
			})
		};
		if (m_canViewStoryList.Count > 0)
		{
			SetActive(this.get_gameObject().get_transform(), UI.STR_STORY_NON_LIST, false);
		}
		else
		{
			SetActive(this.get_gameObject().get_transform(), UI.STR_STORY_NON_LIST, true);
		}
		SetActive(this.get_gameObject().get_transform(), UI.LBL_CHAPTER_NAME, true);
		SetLabelText(this.get_gameObject().get_transform(), UI.LBL_CHAPTER_NAME, m_chapterName);
		SetLabelText((Enum)UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, m_nowPage.ToString());
		List<TheaterModeTable.TheaterModeData> dispList = m_canViewStoryList;
		if (m_pageMax > 1)
		{
			List<TheaterModeTable.TheaterModeData> list = new List<TheaterModeTable.TheaterModeData>();
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
		_003CUpdateUI_003Ec__AnonStorey3F1 _003CUpdateUI_003Ec__AnonStorey3F;
		SetDynamicList((Enum)UI.GRD_LIST, "TheaterStoryListItem", dispList.Count, true, null, null, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			RefreshUI();
		}
	}

	private void OnQuery_PLAY_STORY()
	{
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
		List<TheaterModeTable.TheaterModeData> canViewStoryList = m_canViewStoryList;
		if (canViewStoryList.Count <= 10)
		{
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, true);
			m_pageMax = 1;
		}
		else
		{
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, true);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, false);
			m_pageMax = canViewStoryList.Count / 10 + 1;
		}
		SetLabelText((Enum)UI.LBL_MAX, m_pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, m_nowPage.ToString());
	}
}
