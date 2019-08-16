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
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		allData = (from x in Singleton<DegreeTable>.I.GetAll()
		where x.type == DEGREE_TYPE.FRAME || x.type == DEGREE_TYPE.SPECIAL_FRAME
		select x).ToList();
		allData.Sort((DegreeTable.DegreeData a, DegreeTable.DegreeData b) => (int)(a.id - b.id));
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || allData.Count == 0)
		{
			userHaveData = new List<DegreeTable.DegreeData>();
			base.Initialize();
			yield break;
		}
		userHaveData = (from x in allData
		where x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds)
		select x).ToList();
		showAll = true;
		currentPage = 1;
		yield return 0;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		List<DegreeTable.DegreeData> currentShow = (!showAll) ? userHaveData : allData;
		maxPage = currentShow.Count / GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT;
		if (currentShow.Count % GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT > 0)
		{
			maxPage++;
		}
		int item_num = Mathf.Min(GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT, currentShow.Count - (currentPage - 1) * GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT);
		SetGrid(UI.GRD_FRAME, "DegreePlate", item_num, reset: true, delegate(int i, Transform t, bool b)
		{
			int index = i + (currentPage - 1) * GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT;
			DegreePlate component = t.GetComponent<DegreePlate>();
			DegreeTable.DegreeData degreeData = currentShow[index];
			SetEvent(t, "FRAME_SELECT", degreeData);
			if (degreeData.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
			{
				component.SetFrame((int)degreeData.id);
				component.GetComponent<Collider>().set_enabled(true);
				component.get_gameObject().AddComponent<UIDragScrollView>();
			}
			else if (degreeData.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
			{
				component.SetUnknownFrame();
				component.GetComponent<Collider>().set_enabled(false);
			}
			else
			{
				component.SetFrame((int)degreeData.id);
				component.GetComponent<Collider>().set_enabled(false);
			}
		});
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
		currentPage = 1;
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
