using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeConditionSettings : GameSection
{
	public enum UI
	{
		POP_TARGET_MIN_LEVEL,
		POP_TARGET_MAX_LEVEL,
		LBL_TARGET_MIN_LEVEL,
		LBL_TARGET_MAX_LEVEL,
		POP_TARGET_CAPACITY,
		LBL_TARGET_CAPACITY,
		POP_TARGET_LABEL,
		LBL_TARGET_LABEL,
		POP_TARGET_LOCK,
		LBL_TARGET_LOCK,
		IPT_NAME,
		OBJ_CREATE,
		OBJ_CHANGE,
		SPR_STAMP_LIST,
		SCR_STAMP_LIST,
		GRD_STAMP_LIST,
		BTN_STAMP,
		TEX_STAMP,
		OBJ_STAMP
	}

	public class CreateRequestParam
	{
		public int stampId
		{
			get;
			private set;
		}

		public int minLevel
		{
			get;
			private set;
		}

		public int maxLevel
		{
			get;
			private set;
		}

		public int capacity
		{
			get;
			private set;
		}

		public LOUNGE_LABEL label
		{
			get;
			private set;
		}

		public bool isLock
		{
			get;
			private set;
		}

		public string loungeName
		{
			get;
			private set;
		}

		public CreateRequestParam()
		{
			stampId = 1;
			minLevel = 15;
			maxLevel = Singleton<UserLevelTable>.I.GetMaxLevel();
			capacity = 8;
			label = LOUNGE_LABEL.NONE;
			isLock = false;
			loungeName = string.Empty;
		}

		public CreateRequestParam(int stampId, int minLevel, int maxLevel, int capacity, LOUNGE_LABEL label, bool isLock, string name)
		{
			this.stampId = stampId;
			this.minLevel = minLevel;
			this.maxLevel = maxLevel;
			this.capacity = capacity;
			this.label = label;
			this.isLock = isLock;
			loungeName = name;
		}

		public void SetStampId(int id)
		{
			stampId = id;
		}

		public void SetMinLevel(int level)
		{
			minLevel = level;
		}

		public void SetMaxLevel(int level)
		{
			maxLevel = level;
		}

		public void SetCapacity(int capacity)
		{
			this.capacity = capacity;
		}

		public void SetLabel(LOUNGE_LABEL label)
		{
			this.label = label;
		}

		public void SetLoungeName(string name)
		{
			loungeName = name;
		}

		public void SetLockSetting(bool isLock)
		{
			this.isLock = isLock;
		}
	}

	private CreateRequestParam createRequest = new CreateRequestParam();

	private List<string> levelNames;

	private List<int> levelList;

	private List<string> capacityNames;

	private List<int> capacityList;

	protected string[] labels;

	private List<string> lockNames;

	private Transform minLevelPopup;

	private Transform maxLevelPopup;

	private Transform capacityPopup;

	private Transform labelPopup;

	private Transform lockPopup;

	protected int minLevelIndex;

	protected int maxLevelIndex;

	protected int capacityIndex;

	protected int labelIndex;

	protected int lockIndex;

	private List<int> stampIdListCanUse;

	private GameObject stampListPrefab;

	public override void Initialize()
	{
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.OBJ_CHANGE, MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge());
		SetActive((Enum)UI.OBJ_CREATE, !MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge());
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
		{
			GetCurrentLoungeSettings();
		}
		else
		{
			CopyLoungeCreateRequestParam();
		}
		levelNames = new List<string>();
		levelList = new List<int>();
		CreateLevelPopText();
		for (int i = 0; i < levelList.Count; i++)
		{
			if (levelList[i] == createRequest.minLevel)
			{
				minLevelIndex = i;
			}
			if (levelList[i] == createRequest.maxLevel)
			{
				maxLevelIndex = i;
			}
		}
		capacityNames = new List<string>();
		capacityList = new List<int>();
		CreateCapacityPopText();
		for (int j = 0; j < capacityList.Count; j++)
		{
			if (capacityList[j] == createRequest.capacity)
			{
				capacityIndex = j;
				break;
			}
		}
		labels = StringTable.GetAllInCategory(STRING_CATEGORY.LOUNGE_LABEL);
		if (labels.Length > (int)createRequest.label)
		{
			labelIndex = (int)createRequest.label;
		}
		lockNames = new List<string>();
		lockNames.Add(base.sectionData.GetText("PUBLIC"));
		lockNames.Add(base.sectionData.GetText("LOCK"));
		lockIndex = (createRequest.isLock ? 1 : 0);
		if (string.IsNullOrEmpty(createRequest.loungeName))
		{
			createRequest.SetLoungeName(base.sectionData.GetText("DEFAULT_LOUNGE_NAME"));
		}
		SetInput((Enum)UI.IPT_NAME, createRequest.loungeName, 16, (EventDelegate.Callback)OnChangeLoungeName);
		this.StartCoroutine(LoadStampList());
	}

	protected void InitializeBase()
	{
		base.Initialize();
	}

	private IEnumerator LoadStampList()
	{
		SetActive((Enum)UI.SPR_STAMP_LIST, false);
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem", false);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		stampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		InitStamp();
		Transform scroll = GetCtrl(UI.SCR_STAMP_LIST);
		scroll.GetComponent<UIScrollView>().set_enabled(true);
		InitializeBase();
	}

	private unsafe void InitStamp()
	{
		if (stampIdListCanUse == null)
		{
			ResetStampIdList();
		}
		int count = stampIdListCanUse.Count;
		SetGrid(create_item_func: new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), grid_ctrl_enum: UI.GRD_STAMP_LIST, item_prefab_name: null, item_num: count, reset: true, item_init_func: new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private Transform CreateStampItem(int index, Transform parent)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(stampListPrefab, 5);
		val.set_parent(parent);
		val.set_localScale(Vector3.get_one());
		return val;
	}

	private unsafe void InitStampItem(int index, Transform iTransform, bool isRecycle)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		if (stampIdListCanUse != null)
		{
			int num = stampIdListCanUse[index];
			ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
			item.Init(num);
			if (!isRecycle)
			{
				if (num == createRequest.stampId)
				{
					SetStampTextre(item.StampId);
				}
				ChatStampListItem chatStampListItem = item;
				_003CInitStampItem_003Ec__AnonStorey3FC _003CInitStampItem_003Ec__AnonStorey3FC;
				chatStampListItem.onButton = Delegate.Combine((Delegate)chatStampListItem.onButton, (Delegate)new Action((object)_003CInitStampItem_003Ec__AnonStorey3FC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}

	private void SetStampTextre(int id)
	{
		Transform ctrl = GetCtrl(UI.OBJ_STAMP);
		ChatStampListItem component = ctrl.GetComponent<ChatStampListItem>();
		component.Init(id);
	}

	private void SelectStamp(int id)
	{
		SetActive((Enum)UI.SPR_STAMP_LIST, false);
		SetStampTextre(id);
		createRequest.SetStampId(id);
	}

	private void ResetStampIdList()
	{
		if (Singleton<StampTable>.IsValid() && Singleton<StampTable>.I.table != null)
		{
			if (stampIdListCanUse == null)
			{
				stampIdListCanUse = new List<int>();
			}
			stampIdListCanUse.Clear();
			Singleton<StampTable>.I.table.ForEach(delegate(StampTable.Data stamp_data)
			{
				int id = (int)stamp_data.id;
				if (MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(id))
				{
					stampIdListCanUse.Add(id);
				}
			});
		}
	}

	private void GetCurrentLoungeSettings()
	{
		LoungeModel.Lounge loungeData = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData;
		bool isLock = (loungeData.isLock == 1) ? true : false;
		int capacity = loungeData.num + 1;
		createRequest = new CreateRequestParam(loungeData.stampId, loungeData.minLv, loungeData.maxLv, capacity, (LOUNGE_LABEL)loungeData.label, isLock, loungeData.name);
	}

	private void CopyLoungeCreateRequestParam()
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SetLoungeCreateRequestFromPrefs();
		CreateRequestParam createRequestParam = MonoBehaviourSingleton<LoungeMatchingManager>.I.createRequest;
		createRequest = new CreateRequestParam(createRequestParam.stampId, createRequestParam.minLevel, createRequestParam.maxLevel, createRequestParam.capacity, createRequestParam.label, createRequestParam.isLock, createRequestParam.loungeName);
	}

	private void CreateLevelPopText()
	{
		int maxLevel = Singleton<UserLevelTable>.I.GetMaxLevel();
		levelList = new List<int>();
		int num = maxLevel / 10 + 1;
		for (int i = 0; i < num; i++)
		{
			if (i == 0)
			{
				levelList.Add(15);
			}
			else if (10 * i > 15)
			{
				levelList.Add(10 * i);
			}
		}
		for (int j = 0; j < levelList.Count; j++)
		{
			levelNames.Add(levelList[j].ToString());
		}
	}

	private void CreateCapacityPopText()
	{
		int num = 8;
		for (int i = 2; i <= num; i++)
		{
			capacityList.Add(i);
		}
		for (int j = 0; j < capacityList.Count; j++)
		{
			capacityNames.Add(capacityList[j].ToString());
		}
	}

	public override void UpdateUI()
	{
		UpdateMinLevel();
		UpdateMaxLevel();
		UpdateCapacity();
		UpdateLabel();
		UpdateLock();
	}

	private void UpdateMinLevel()
	{
		int index = minLevelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_MIN_LEVEL, levelNames[index]);
	}

	private void UpdateMaxLevel()
	{
		int index = maxLevelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_MAX_LEVEL, levelNames[index]);
	}

	private void UpdateCapacity()
	{
		int index = capacityIndex;
		SetLabelText((Enum)UI.LBL_TARGET_CAPACITY, capacityNames[index]);
	}

	protected void UpdateLabel()
	{
		int num = labelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_LABEL, labels[num]);
	}

	private void UpdateLock()
	{
		int index = lockIndex;
		SetLabelText((Enum)UI.LBL_TARGET_LOCK, lockNames[index]);
	}

	protected virtual void OnChangeLoungeName()
	{
		string inputValue = GetInputValue((Enum)UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", string.Empty);
		inputValue = inputValue.Replace("\u3000", string.Empty);
		createRequest.SetLoungeName(inputValue);
	}

	private void OnQuery_STAMP()
	{
		SetActive((Enum)UI.SPR_STAMP_LIST, true);
	}

	private void OnQuery_TARGET_MIN_LEVEL()
	{
		ShowMinLevelPopup();
	}

	private void ShowMinLevelPopup()
	{
		if (minLevelPopup == null)
		{
			minLevelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_MIN_LEVEL), false);
		}
		if (!(minLevelPopup == null))
		{
			bool[] array = new bool[levelNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i <= maxLevelIndex);
			}
			int select_index = minLevelIndex;
			UIScrollablePopupList.CreatePopup(minLevelPopup, GetCtrl(UI.POP_TARGET_MIN_LEVEL), 7, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, levelNames.ToArray(), array, select_index, delegate(int index)
			{
				minLevelIndex = index;
				createRequest.SetMinLevel(levelList[index]);
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_MAX_LEVEL()
	{
		ShowMaxLevelPopup();
	}

	private void ShowMaxLevelPopup()
	{
		if (maxLevelPopup == null)
		{
			maxLevelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_MAX_LEVEL), false);
		}
		if (!(maxLevelPopup == null))
		{
			bool[] array = new bool[levelNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i >= minLevelIndex);
			}
			int select_index = maxLevelIndex;
			UIScrollablePopupList.CreatePopup(maxLevelPopup, GetCtrl(UI.POP_TARGET_MAX_LEVEL), 8, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, levelNames.ToArray(), array, select_index, delegate(int index)
			{
				maxLevelIndex = index;
				createRequest.SetMaxLevel(levelList[index]);
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_CAPACITY()
	{
		if (capacityPopup == null)
		{
			capacityPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_CAPACITY), false);
		}
		if (!(capacityPopup == null))
		{
			bool[] array = new bool[capacityNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
				{
					array[i] = (i >= createRequest.capacity - 2);
				}
				else
				{
					array[i] = true;
				}
			}
			int select_index = capacityIndex;
			UIScrollablePopupList.CreatePopup(capacityPopup, GetCtrl(UI.POP_TARGET_CAPACITY), 6, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, capacityNames.ToArray(), array, select_index, delegate(int index)
			{
				capacityIndex = index;
				createRequest.SetCapacity(capacityList[index]);
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_LABEL()
	{
		if (labelPopup == null)
		{
			labelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_LABEL), false);
		}
		if (!(labelPopup == null))
		{
			bool[] array = new bool[labels.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = labelIndex;
			UIScrollablePopupList.CreatePopup(labelPopup, GetCtrl(UI.POP_TARGET_LABEL), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, labels, array, select_index, delegate(int index)
			{
				labelIndex = index;
				SetParamLabel((LOUNGE_LABEL)index);
				RefreshUI();
			});
		}
	}

	protected virtual void SetParamLabel(LOUNGE_LABEL label)
	{
		createRequest.SetLabel(label);
	}

	private void OnQuery_TARGET_LOCK()
	{
		if (lockPopup == null)
		{
			lockPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_LOCK), false);
		}
		if (!(lockPopup == null))
		{
			bool[] array = new bool[lockNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = lockIndex;
			UIScrollablePopupList.CreatePopup(lockPopup, GetCtrl(UI.POP_TARGET_LOCK), 2, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, lockNames.ToArray(), array, select_index, delegate(int index)
			{
				lockIndex = index;
				createRequest.SetLockSetting(lockIndex == 1);
				RefreshUI();
			});
		}
	}

	private unsafe void OnQuery_CREATE()
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SetLoungeCreateRequest(createRequest);
		GameSection.StayEvent();
		LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
		if (_003C_003Ef__am_0024cache13 == null)
		{
			_003C_003Ef__am_0024cache13 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendCreate(_003C_003Ef__am_0024cache13);
	}

	private void OnQuery_CHANGE()
	{
		LoungeModel.RequestEdit requestEdit = new LoungeModel.RequestEdit();
		requestEdit.stampId = createRequest.stampId;
		requestEdit.num = createRequest.capacity;
		requestEdit.label = (int)createRequest.label;
		requestEdit.isLock = (createRequest.isLock ? 1 : 0);
		requestEdit.minLv = createRequest.minLevel;
		requestEdit.maxLv = createRequest.maxLevel;
		requestEdit.name = createRequest.loungeName;
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEdit(requestEdit, delegate
		{
			GameSection.ResumeEvent(true, null, false);
		});
	}
}
