using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanSettings : GameSection
{
	public enum UI
	{
		LBL_NAME_DEFAULT,
		LBL_TAG_DEFAULT,
		POP_TARGET_LOCK,
		LBL_TARGET_LOCK,
		POP_TARGET_LABEL,
		LBL_TARGET_LABEL,
		IPT_NAME,
		IPT_COMMENT,
		OBJ_CREATE,
		BTN_NEXT,
		BTN_NEXT_OFF,
		OBJ_CHANGE,
		BTN_CHANGE,
		BTN_CHANGE_OFF,
		BTN_INFO,
		SPR_TAG,
		IPT_TAG,
		LBL_INPUT_TAG
	}

	public class CreateRequestParam
	{
		public int stampId
		{
			get;
			private set;
		}

		public CLAN_LABEL label
		{
			get;
			private set;
		}

		public bool isLock
		{
			get;
			private set;
		}

		public string clanName
		{
			get;
			private set;
		}

		public string comment
		{
			get;
			private set;
		}

		public string clanTag
		{
			get;
			private set;
		}

		public CreateRequestParam()
		{
			stampId = 1;
			isLock = false;
			label = CLAN_LABEL.NONE;
			clanName = "";
			clanTag = "";
		}

		public CreateRequestParam(CLAN_LABEL label, bool isLock, string name, string comment, string tag)
		{
			this.isLock = isLock;
			this.label = label;
			clanName = name;
			this.comment = comment;
			clanTag = tag;
		}

		public void SetStampId(int id)
		{
			stampId = id;
		}

		public void SetClanName(string name)
		{
			clanName = name;
		}

		public void SetLabel(CLAN_LABEL label)
		{
			this.label = label;
		}

		public void SetLockSetting(bool isLock)
		{
			this.isLock = isLock;
		}

		public void SetComment(string comment)
		{
			this.comment = comment;
		}

		public void SetClanTag(string tag)
		{
			clanTag = tag;
		}
	}

	protected CreateRequestParam createRequest = new CreateRequestParam();

	private List<string> lockNames;

	protected string[] labels;

	private Transform lockPopup;

	private Transform labelPopup;

	protected int lockIndex;

	protected int labelIndex;

	private List<int> stampIdListCanUse;

	private GameObject stampListPrefab;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	protected IEnumerator DoInitialize()
	{
		SetActive(UI.OBJ_CHANGE, MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered());
		SetActive(UI.OBJ_CREATE, MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsNotRegistered());
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			bool isWait = true;
			MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", delegate
			{
				isWait = false;
			});
			while (isWait)
			{
				yield return null;
			}
			GetCurrentClanSettings();
		}
		lockNames = new List<string>();
		lockNames.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 0u));
		lockNames.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 1u));
		lockIndex = (createRequest.isLock ? 1 : 0);
		string[] allInCategory = StringTable.GetAllInCategory(STRING_CATEGORY.CLAN_LABEL);
		if (allInCategory != null && allInCategory.Length > 1)
		{
			labels = new string[allInCategory.Length - 1];
			for (int i = 1; i < allInCategory.Length; i++)
			{
				labels[i - 1] = allInCategory[i];
			}
		}
		labelIndex = (int)(createRequest.label - 1);
		if (labelIndex < 0)
		{
			labelIndex = 0;
		}
		if (labels != null && labelIndex >= labels.Length)
		{
			labelIndex = labels.Length - 1;
		}
		if (string.IsNullOrEmpty(createRequest.clanName))
		{
			createRequest.SetClanName(base.sectionData.GetText("DEFAULT_CLAN_NAME"));
		}
		SetInput(GetCtrl(UI.IPT_NAME), createRequest.clanName, 9, "", OnChangeClanName);
		if (string.IsNullOrEmpty(createRequest.comment))
		{
			createRequest.SetComment(base.sectionData.GetText("DEFAULT_CLAN_COMMENT"));
		}
		SetInput(GetCtrl(UI.IPT_COMMENT), createRequest.comment, 48, "", OnChangeComment);
		if (string.IsNullOrEmpty(createRequest.clanTag))
		{
			createRequest.SetClanTag(base.sectionData.GetText("DEFAULT_CLAN_TAG"));
		}
		SetInput(GetCtrl(UI.IPT_TAG), createRequest.clanTag, 4, "", OnChangeClanTag);
		SetTouchAndRelease(UI.BTN_INFO, "TAG_INFO_SHOW", "TAG_INFO_HIDE");
		SetActive(UI.SPR_TAG, is_visible: false);
		InitializeBase();
	}

	protected void InitializeBase()
	{
		base.Initialize();
	}

	private void GetCurrentClanSettings()
	{
		ClanData clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.clanData;
		bool isLock = (clanData.jt == 1) ? true : false;
		createRequest = new CreateRequestParam((CLAN_LABEL)clanData.lbl, isLock, clanData.name, clanData.cmt, clanData.tag);
	}

	public override void UpdateUI()
	{
		UpdateLock();
		UpdateLabel();
	}

	private void UpdateLock()
	{
		int index = lockIndex;
		SetLabelText(UI.LBL_TARGET_LOCK, lockNames[index]);
	}

	protected void UpdateLabel()
	{
		if (labels == null)
		{
			Debug.LogError("[UpdateLabel] labels are null");
			return;
		}
		int num = labelIndex;
		SetLabelText(UI.LBL_TARGET_LABEL, labels[num]);
	}

	protected virtual void OnChangeClanName()
	{
		string inputValue = GetInputValue(UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", "");
		inputValue = inputValue.Replace("\u3000", "");
		inputValue = inputValue.Replace("\n", "");
		createRequest.SetClanName(inputValue);
		SetActive(UI.LBL_NAME_DEFAULT, string.IsNullOrEmpty(inputValue));
		SetActive(UI.BTN_NEXT, !string.IsNullOrEmpty(inputValue) && CheckValidClanTag());
		SetActive(UI.BTN_NEXT_OFF, string.IsNullOrEmpty(inputValue) || !CheckValidClanTag());
		SetActive(UI.BTN_CHANGE, !string.IsNullOrEmpty(inputValue) && CheckValidClanTag());
		SetActive(UI.BTN_CHANGE_OFF, string.IsNullOrEmpty(inputValue) || !CheckValidClanTag());
	}

	protected virtual void OnChangeComment()
	{
		string inputValue = GetInputValue(UI.IPT_COMMENT);
		inputValue = inputValue.Replace("\n", "");
		createRequest.SetComment(inputValue);
	}

	protected virtual void OnChangeClanTag()
	{
		string inputValue = GetInputValue(UI.IPT_TAG);
		inputValue = inputValue.Replace(" ", "");
		inputValue = inputValue.Replace("\u3000", "");
		inputValue = inputValue.Replace("\n", "");
		inputValue = inputValue.ToUpper();
		SetLabelText(UI.LBL_INPUT_TAG, inputValue);
		createRequest.SetClanTag(inputValue);
		SetActive(UI.LBL_TAG_DEFAULT, string.IsNullOrEmpty(inputValue));
		SetActive(UI.BTN_NEXT, !string.IsNullOrEmpty(inputValue) && !string.IsNullOrEmpty(createRequest.clanName));
		SetActive(UI.BTN_NEXT_OFF, string.IsNullOrEmpty(inputValue) || string.IsNullOrEmpty(createRequest.clanName));
		SetActive(UI.BTN_CHANGE, !string.IsNullOrEmpty(inputValue) && !string.IsNullOrEmpty(createRequest.clanName));
		SetActive(UI.BTN_CHANGE_OFF, string.IsNullOrEmpty(inputValue) || string.IsNullOrEmpty(createRequest.clanName));
	}

	private void OnQuery_TARGET_LOCK()
	{
		if (lockPopup == null)
		{
			lockPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_LOCK), check_panel: false);
		}
		if (!(lockPopup == null))
		{
			bool[] array = new bool[lockNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = lockIndex;
			UIScrollablePopupList.CreatePopup(lockPopup, GetCtrl(UI.POP_TARGET_LOCK), 2, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, adjust_size: true, lockNames.ToArray(), array, select_index, delegate(int index)
			{
				lockIndex = index;
				createRequest.SetLockSetting(lockIndex == 1);
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_LABEL()
	{
		if (labelPopup == null)
		{
			labelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_LABEL), check_panel: false);
		}
		if (labelPopup == null)
		{
			return;
		}
		if (labels == null)
		{
			Debug.LogError("labels are null");
			return;
		}
		bool[] array = new bool[labels.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = true;
		}
		int select_index = labelIndex;
		UIScrollablePopupList.CreatePopup(labelPopup, GetCtrl(UI.POP_TARGET_LABEL), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, adjust_size: true, labels, array, select_index, delegate(int index)
		{
			labelIndex = index;
			SetParamLabel((CLAN_LABEL)(index + 1));
			RefreshUI();
		});
	}

	protected virtual void SetParamLabel(CLAN_LABEL label)
	{
		createRequest.SetLabel(label);
	}

	private void OnQuery_TAG_INFO_SHOW()
	{
		SetActive(UI.SPR_TAG, is_visible: true);
	}

	private void OnQuery_TAG_INFO_HIDE()
	{
		SetActive(UI.SPR_TAG, is_visible: false);
	}

	protected bool CheckValidClanTag()
	{
		string clanTag = createRequest.clanTag;
		if (string.IsNullOrEmpty(clanTag))
		{
			return false;
		}
		if (!clanTag.Contains(" ") && !clanTag.Contains("\u3000") && !clanTag.Contains("\n"))
		{
			return clanTag.Length <= 4;
		}
		return false;
	}
}
