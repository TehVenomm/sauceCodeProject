using Network;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TaskTop : GameSection
{
	private enum UI
	{
		BTN_INACTIVE_ACHIEVE_LIST_L,
		BTN_INACTIVE_ACHIEVE_LIST_R,
		BTN_ACHIEVE_LIST_L,
		BTN_ACHIEVE_LIST_R,
		BTN_ACHIEVE_LIST_L_ADD,
		BTN_ACHIEVE_LIST_R_ADD,
		SCR_INVENTORY,
		GRD_INVENTORY,
		LBL_CURRENT_NUM,
		LBL_PAGE_NOW,
		LBL_PAGE_MAX,
		LBL_SHOW_TYPE,
		OBJ_ICON_ROOT,
		OBJ_CLEARED_ITEM,
		OBJ_NOT_CLEARED_ITEM,
		SPR_NOT_RECIEVED,
		SPR_RECIEVED,
		SPR_GAUGE,
		LBL_REWARD_NAME,
		LBL_CONDITION,
		LBL_GAUGE,
		LBL_ITEM
	}

	private enum SHOW_TYPE
	{
		NOT_ACHIEVED,
		ACHIVED,
		MAX_NUM
	}

	public class TaskData
	{
		public TaskTable.TaskData tableData;

		public TaskInfo info;
	}

	private readonly string[] STRING_KEY = new string[2]
	{
		"STR_NOTACHIEVED",
		"STR_ACHIEVED"
	};

	private const int ONE_PAGE_ITEM_NUM = 10;

	private readonly string LIST_ITEM_PREFAB_NAME = "TaskListItem";

	private int currentPageIndex;

	private int pageMaxNum;

	private SHOW_TYPE showType;

	private Transform gridTransform;

	private UIScrollView scrollView;

	private List<TaskData>[] taskDataLists = new List<TaskData>[2];

	[CompilerGenerated]
	private static Comparison<TaskData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static Comparison<TaskData> _003C_003Ef__mg_0024cache1;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "TaskTable";
		}
	}

	public override void Initialize()
	{
		gridTransform = GetCtrl(UI.GRD_INVENTORY);
		scrollView = base.GetComponent<UIScrollView>((Enum)UI.SCR_INVENTORY);
		InitTaskDataLists();
		SendTaskList();
		base.Initialize();
	}

	private void InitTaskDataLists()
	{
		List<TaskInfo> taskInfos = MonoBehaviourSingleton<AchievementManager>.I.GetTaskInfos();
		int count = taskInfos.Count;
		taskDataLists[0] = new List<TaskData>(count);
		taskDataLists[1] = new List<TaskData>(count);
		for (int i = 0; i < count; i++)
		{
			TaskData taskData = CreateTaskData(taskInfos[i]);
			if (taskData.info.status == 3)
			{
				taskDataLists[1].Add(taskData);
			}
			else
			{
				taskDataLists[0].Add(taskData);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			if (taskDataLists[j] != null)
			{
				taskDataLists[j].Sort(CompareByStatusAndId);
			}
		}
	}

	private static int CompareByStatusAndId(TaskData a, TaskData b)
	{
		if (b.info.status < a.info.status)
		{
			return -1;
		}
		if (a.info.status == b.info.status)
		{
			if (a.tableData == null || b.tableData == null)
			{
				Debug.LogError((object)("<color=red>" + ((a.tableData != null) ? b.info.taskId : a.info.taskId) + " not found</color>"));
				return -1;
			}
			return a.tableData.orderNo - b.tableData.orderNo;
		}
		return 1;
	}

	private TaskData CreateTaskData(TaskInfo info)
	{
		TaskData taskData = new TaskData();
		taskData.info = info;
		taskData.tableData = Singleton<TaskTable>.I.Get((uint)info.taskId);
		return taskData;
	}

	private void InsertTaskData(SHOW_TYPE type, TaskData data)
	{
		taskDataLists[(int)type].Add(data);
		taskDataLists[(int)type].Sort(CompareByStatusAndId);
	}

	private void InsertTaskData(SHOW_TYPE type, TaskInfo info)
	{
		InsertTaskData(type, CreateTaskData(info));
	}

	public override void UpdateUI()
	{
		pageMaxNum = taskDataLists[(int)showType].Count / 10 + 1;
		SetLabelText((Enum)UI.LBL_PAGE_MAX, pageMaxNum.ToString());
		SetLabelText((Enum)UI.LBL_CURRENT_NUM, taskDataLists[1].Count.ToString());
		bool flag = currentPageIndex != 0;
		SetActive((Enum)UI.BTN_ACHIEVE_LIST_L, flag);
		SetActive((Enum)UI.BTN_ACHIEVE_LIST_L_ADD, flag);
		SetActive((Enum)UI.BTN_INACTIVE_ACHIEVE_LIST_L, !flag);
		bool flag2 = currentPageIndex + 1 < pageMaxNum;
		SetActive((Enum)UI.BTN_ACHIEVE_LIST_R, flag2);
		SetActive((Enum)UI.BTN_ACHIEVE_LIST_R_ADD, flag2);
		SetActive((Enum)UI.BTN_INACTIVE_ACHIEVE_LIST_R, !flag2);
		string text = base.sectionData.GetText(STRING_KEY[(int)showType]);
		SetLabelText((Enum)UI.LBL_SHOW_TYPE, text);
		SetLabelText((Enum)UI.LBL_PAGE_NOW, (currentPageIndex + 1).ToString());
		UpdateInventory();
		if (scrollView != null)
		{
			scrollView.ResetPosition();
		}
		base.UpdateUI();
	}

	private void UpdateInventory()
	{
		if (gridTransform == null)
		{
			gridTransform = GetCtrl(UI.GRD_INVENTORY);
		}
		int start = currentPageIndex * 10;
		int item_num = 10;
		if (taskDataLists[(int)showType].Count - start < 10)
		{
			item_num = taskDataLists[(int)showType].Count - start;
		}
		SetDynamicList(gridTransform, LIST_ITEM_PREFAB_NAME, item_num, reset: true, null, null, delegate(int i, Transform t, bool isRecycle)
		{
			InitListItem(taskDataLists[(int)showType][start + i], t);
		});
	}

	private void InitListItem(TaskData data, Transform root)
	{
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		SetActive(root, UI.OBJ_CLEARED_ITEM, data.info.status == 2 || data.info.status == 3);
		SetActive(root, UI.OBJ_NOT_CLEARED_ITEM, data.info.status == 1);
		Transform val = FindCtrl(root, UI.SPR_GAUGE);
		int num = (data.tableData == null) ? int.MaxValue : data.tableData.goalNum;
		int num2 = (data.tableData != null) ? data.tableData.rewardNum : 0;
		int num3 = (data.tableData != null) ? data.tableData.itemId : 0;
		string text = (data.tableData == null) ? string.Empty : data.tableData.title;
		string text2 = (data.tableData == null) ? string.Empty : data.tableData.detail;
		REWARD_TYPE rEWARD_TYPE = (data.tableData != null) ? data.tableData.rewardType : REWARD_TYPE.NONE;
		if (val != null)
		{
			val.set_localScale(new Vector3(Mathf.Clamp((float)data.info.progress / (float)num, 0f, 1f), 1f, 1f));
		}
		SetLabelText(root, UI.LBL_GAUGE, data.info.progress.ToString() + "/" + num.ToString());
		SetLabelText(root, UI.LBL_CONDITION, text);
		SetLabelText(root, UI.LBL_REWARD_NAME, text2);
		if (num2 <= 1)
		{
			SetActive(root, UI.LBL_ITEM, is_visible: false);
		}
		else
		{
			SetActive(root, UI.LBL_ITEM, is_visible: true);
			SetLabelText(root, UI.LBL_ITEM, "x" + num2.ToString());
		}
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(rEWARD_TYPE, (uint)num3, FindCtrl(root, UI.OBJ_ICON_ROOT));
		SetMaterialInfo(itemIcon._transform, rEWARD_TYPE, (uint)num3, scrollView.get_transform());
		UIButton component = root.GetComponent<UIButton>();
		if (component != null)
		{
			component.tweenTarget = itemIcon.get_gameObject();
		}
		GameObject gameObject = FindCtrl(root, UI.SPR_NOT_RECIEVED).get_gameObject();
		GameObject gameObject2 = FindCtrl(root, UI.SPR_RECIEVED).get_gameObject();
		if (data.info.status == 2)
		{
			SetButtonEnabled(root, is_enabled: true);
			SetEvent(root, "RECEIVE_REWARD", data);
			gameObject.SetActive(true);
			gameObject2.SetActive(false);
		}
		else if (data.info.status == 3)
		{
			SetButtonEnabled(root, is_enabled: false);
			gameObject.SetActive(false);
			gameObject2.SetActive(true);
		}
		else
		{
			SetButtonEnabled(root, is_enabled: false);
		}
	}

	private void OnQuery_NEXT_PAGE()
	{
		if (pageMaxNum > currentPageIndex + 1)
		{
			currentPageIndex++;
			UpdateUI();
		}
	}

	private void OnQuery_PREV_PAGE()
	{
		if (currentPageIndex > 0)
		{
			currentPageIndex--;
			UpdateUI();
		}
	}

	private void OnQuery_CHANGE_SHOW_TYPE()
	{
		currentPageIndex = 0;
		if (showType == SHOW_TYPE.NOT_ACHIEVED)
		{
			showType = SHOW_TYPE.ACHIVED;
		}
		else
		{
			showType = SHOW_TYPE.NOT_ACHIEVED;
		}
		UpdateUI();
	}

	private void OnQuery_RECEIVE_REWARD()
	{
		TaskData data = GameSection.GetEventData() as TaskData;
		if (data != null)
		{
			GameSection.StayEvent();
			TaskCompleteModel.RequestSendForm requestSendForm = new TaskCompleteModel.RequestSendForm();
			requestSendForm.uId = data.info.taskId;
			Protocol.Send<TaskCompleteModel.RequestSendForm, TaskCompleteModel>(TaskCompleteModel.URL, requestSendForm, delegate
			{
				taskDataLists[0].Remove(data);
				UpdateUI();
				GameSection.ResumeEvent(is_resume: true);
			}, string.Empty);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_TASK_LIST;
	}

	public override void OnNotify(NOTIFY_FLAG notify_flags)
	{
		if ((notify_flags & NOTIFY_FLAG.UPDATE_TASK_LIST) != (NOTIFY_FLAG)0L)
		{
			InitTaskDataLists();
			base.OnNotify(notify_flags);
		}
	}

	public void SendTaskList()
	{
		Protocol.Send<TaskListModel>(TaskListModel.URL, delegate
		{
		}, string.Empty);
	}
}
