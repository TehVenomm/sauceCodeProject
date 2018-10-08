using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProfileChangeDegreeFrame : GameSection
{
	private enum UI
	{
		OBJ_ACTIVE_ARROW_ROOT,
		OBJ_INACTIVE_ARROW_ROOT,
		LBL_ARROW_NOW,
		LBL_ARROW_MAX,
		LBL_SORT,
		GRD_FRAME
	}

	public class ChangeFrame
	{
		public DegreeTable.DegreeData changeData;

		public ChangeFrame(DegreeTable.DegreeData data)
		{
			changeData = data;
		}
	}

	public List<DegreeTable.DegreeData> allData;

	public List<DegreeTable.DegreeData> userHaveData;

	private bool showAll;

	private int currentPage;

	private int maxPage;

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		List<DegreeTable.DegreeData> all = Singleton<DegreeTable>.I.GetAll();
		if (_003CDoInitialize_003Ec__Iterator106._003C_003Ef__am_0024cache3 == null)
		{
			_003CDoInitialize_003Ec__Iterator106._003C_003Ef__am_0024cache3 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		allData = all.Where(_003CDoInitialize_003Ec__Iterator106._003C_003Ef__am_0024cache3).ToList();
		allData.Sort((DegreeTable.DegreeData a, DegreeTable.DegreeData b) => (int)(a.id - b.id));
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || allData.Count == 0)
		{
			userHaveData = new List<DegreeTable.DegreeData>();
			base.Initialize();
		}
		else
		{
			List<DegreeTable.DegreeData> source = allData;
			if (_003CDoInitialize_003Ec__Iterator106._003C_003Ef__am_0024cache5 == null)
			{
				_003CDoInitialize_003Ec__Iterator106._003C_003Ef__am_0024cache5 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			userHaveData = source.Where(_003CDoInitialize_003Ec__Iterator106._003C_003Ef__am_0024cache5).ToList();
			showAll = true;
			currentPage = 1;
			yield return (object)0;
			base.Initialize();
		}
	}

	public unsafe override void UpdateUI()
	{
		base.UpdateUI();
		List<DegreeTable.DegreeData> currentShow = (!showAll) ? userHaveData : allData;
		maxPage = currentShow.Count / GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT;
		if (currentShow.Count % GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT > 0)
		{
			maxPage++;
		}
		int item_num = Mathf.Min(GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT, currentShow.Count - (currentPage - 1) * GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT);
		_003CUpdateUI_003Ec__AnonStorey3DC _003CUpdateUI_003Ec__AnonStorey3DC;
		SetGrid(UI.GRD_FRAME, "DegreePlate", item_num, true, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3DC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetLabelText((Enum)UI.LBL_SORT, (!showAll) ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 21u) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 20u));
		bool flag = maxPage > 1;
		SetActive((Enum)UI.OBJ_ACTIVE_ARROW_ROOT, flag);
		SetActive((Enum)UI.OBJ_INACTIVE_ARROW_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_ARROW_NOW, currentPage.ToString());
		SetLabelText((Enum)UI.LBL_ARROW_MAX, maxPage.ToString());
	}

	private void OnQuery_SORT()
	{
		showAll = !showAll;
		RefreshUI();
	}

	private void OnQuery_PAGE_NEXT()
	{
		currentPage++;
		if (currentPage > maxPage)
		{
			currentPage = 1;
		}
		RefreshUI();
	}

	private void OnQuery_PAGE_PREV()
	{
		currentPage--;
		if (currentPage < 1)
		{
			currentPage = maxPage;
		}
		RefreshUI();
	}

	private void OnQuery_FRAME_SELECT()
	{
		DegreeTable.DegreeData data = GameSection.GetEventData() as DegreeTable.DegreeData;
		ChangeFrame eventData = new ChangeFrame(data);
		GameSection.SetEventData(eventData);
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_DEGREE_FRAME);
		GameSection.BackSection();
	}
}
