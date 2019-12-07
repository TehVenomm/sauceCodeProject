using Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProfileEditDegree : GameSection
{
	private enum UI
	{
		OBJ_DEGREE_PLATE_ROOT,
		SPR_TAB_SELECTING,
		BTN_PREFIX,
		LBL_PREFIX,
		SPR_PREFIX_SELECT,
		BTN_CONJUNCTION,
		LBL_CONJUNCTION,
		SPR_CONJUNCTION_SELECT,
		BTN_SUFFIX,
		LBL_SUFFIX,
		SPR_SUFFIX_SELECT,
		LBL_SORT,
		OBJ_ARROW_ACTIVE_ROOT,
		OBJ_ARROW_INACTIVE_ROOT,
		LBL_PAGE_NOW,
		LBL_PAGE_MAX,
		LBL_DEGREE_NAME,
		LBL_NO_SELECTABLE_FRAME,
		LBL_DEGREE_REQUIREMENT_TEXT,
		GRD_WORD_LIST,
		LBL_SELECTED_DEGREE_NAME,
		LBL_SELECTED_DEGREE_REQUIREMENT,
		LBL_WORD_NORMAL,
		LBL_WORD_SELECTED,
		LBL_WORD_UNKNOWN,
		SPR_WORD_SELECTED
	}

	private enum WORD_LIST_SELECT
	{
		NO_SELECT,
		SELECT,
		CLOSE
	}

	private enum WORD_ATTRIBUTE
	{
		NOUN,
		CONJUNCTION
	}

	private enum WORD_TAB
	{
		PREFIX = 1,
		CONJUNCTION,
		SUFFIX
	}

	public readonly string[] WORD_LIST_SPRITE_NAME = new string[3]
	{
		"Honor_SelectBtn_Normal",
		"Honor_SelectBtn_Select",
		"Honor_SelectBtn_Close"
	};

	public readonly string[] TAB_SPRITE_NAME = new string[3]
	{
		"Honor_TabBtn_Normal",
		"Honor_TabBtn_Select",
		"Honor_TabBtn_Unselect"
	};

	private List<int> currentDegrees;

	private DegreePlate currentPlate;

	private bool showAll;

	private Vector3 arrowLocalPos;

	private Vector3 arrowlocalScale;

	private Transform arrowTrans;

	private List<DegreeTable.DegreeData> allNounData;

	private List<DegreeTable.DegreeData> allConData;

	private List<DegreeTable.DegreeData> userHaveFrameData;

	private List<DegreeTable.DegreeData> userHaveNounData;

	private List<DegreeTable.DegreeData> userHaveConData;

	private List<DegreeTable.DegreeData> currentShowData;

	private DegreeTable.DegreeData currentSelectData;

	private int currentPage;

	private int maxPage;

	private WORD_TAB currentTab;

	private Color[] selectColors = new Color[2];

	private Color[] normalColors = new Color[2];

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "DegreeTable";
		}
	}

	public override void Initialize()
	{
		currentDegrees = MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds.ToList();
		showAll = true;
		currentTab = WORD_TAB.PREFIX;
		SpoileColor();
		DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[0]);
		currentSelectData = ((data.type == DEGREE_TYPE.SPECIAL_FRAME) ? null : Singleton<DegreeTable>.I.GetData((uint)currentDegrees[1]));
		List<DegreeTable.DegreeData> all = Singleton<DegreeTable>.I.GetAll();
		all.Sort((DegreeTable.DegreeData a, DegreeTable.DegreeData b) => (int)(a.id - b.id));
		allNounData = all.Where((DegreeTable.DegreeData x) => x.type == DEGREE_TYPE.NOUN).ToList();
		allConData = all.Where((DegreeTable.DegreeData x) => x.type == DEGREE_TYPE.CONJUNCTION).ToList();
		userHaveNounData = allNounData.Where((DegreeTable.DegreeData x) => x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds)).ToList();
		userHaveConData = allConData.Where((DegreeTable.DegreeData x) => x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds)).ToList();
		userHaveFrameData = (from x in all
			where x.type == DEGREE_TYPE.FRAME
			where x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds)
			select x).ToList();
		currentPage = 1;
		currentPlate = GetCtrl(UI.OBJ_DEGREE_PLATE_ROOT).GetComponent<DegreePlate>();
		arrowTrans = GetCtrl(UI.SPR_TAB_SELECTING);
		arrowLocalPos = arrowTrans.localPosition;
		arrowlocalScale = arrowTrans.localScale;
		base.Initialize();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_DEGREE_FRAME;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if (flags == NOTIFY_FLAG.UPDATE_DEGREE_FRAME)
		{
			ProfileChangeDegreeFrame.ChangeFrame changeFrame = GameSection.GetEventData() as ProfileChangeDegreeFrame.ChangeFrame;
			if (changeFrame != null)
			{
				currentDegrees[0] = (int)changeFrame.changeData.id;
				if (changeFrame.changeData.type == DEGREE_TYPE.SPECIAL_FRAME)
				{
					currentSelectData = null;
				}
				else
				{
					currentSelectData = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[(int)currentTab]);
				}
			}
		}
		base.OnNotify(flags);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (currentTab == WORD_TAB.CONJUNCTION)
		{
			currentShowData = (showAll ? allConData : userHaveConData);
		}
		else
		{
			currentShowData = (showAll ? allNounData : userHaveNounData);
		}
		maxPage = currentShowData.Count / GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT;
		if (currentShowData.Count % GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT > 0)
		{
			maxPage++;
		}
		DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[0]);
		if (data.type == DEGREE_TYPE.SPECIAL_FRAME)
		{
			maxPage = 1;
			currentPage = 1;
			SetLabelText(UI.LBL_PREFIX, "");
			SetLabelText(UI.LBL_CONJUNCTION, "");
			SetLabelText(UI.LBL_SUFFIX, "");
			SetLabelText(UI.LBL_PAGE_MAX, maxPage.ToString());
			SetLabelText(UI.LBL_PAGE_NOW, currentPage.ToString());
			SetActive(UI.OBJ_ARROW_ACTIVE_ROOT, is_visible: false);
			SetActive(UI.OBJ_ARROW_INACTIVE_ROOT, is_visible: true);
			SetActive(UI.LBL_NO_SELECTABLE_FRAME, is_visible: true);
			currentPlate.Initialize(currentDegrees, isButton: false, delegate
			{
			});
			SetGrid(UI.GRD_WORD_LIST, "DegreeWordList", 0, reset: false, delegate
			{
			});
		}
		else
		{
			SetLabelText(UI.LBL_PREFIX, Singleton<DegreeTable>.I.GetData((uint)currentDegrees[1]).name);
			SetLabelText(UI.LBL_CONJUNCTION, Singleton<DegreeTable>.I.GetData((uint)currentDegrees[2]).name);
			SetLabelText(UI.LBL_SUFFIX, Singleton<DegreeTable>.I.GetData((uint)currentDegrees[3]).name);
			SetLabelText(UI.LBL_SORT, showAll ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 20u) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 21u));
			SetLabelText(UI.LBL_PAGE_MAX, maxPage.ToString());
			SetLabelText(UI.LBL_PAGE_NOW, currentPage.ToString());
			SetActive(UI.OBJ_ARROW_ACTIVE_ROOT, maxPage > 1);
			SetActive(UI.OBJ_ARROW_INACTIVE_ROOT, maxPage == 1);
			SetActive(UI.LBL_NO_SELECTABLE_FRAME, is_visible: false);
			currentPlate.Initialize(currentDegrees, isButton: false, delegate
			{
			});
			int item_num = Mathf.Min(GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT, currentShowData.Count - (currentPage - 1) * GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT);
			SetGrid(UI.GRD_WORD_LIST, "DegreeWordList", item_num, reset: false, delegate(int i, Transform t, bool b)
			{
				t.gameObject.AddComponent<UIDragScrollView>();
				int index = i + (currentPage - 1) * GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT;
				DegreeTable.DegreeData degreeData = currentShowData[index];
				SetEvent(t, "SELECT", degreeData);
				if (degreeData.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
				{
					SetButtonSprite(t, (currentDegrees[(int)currentTab] == degreeData.id) ? WORD_LIST_SPRITE_NAME[1] : WORD_LIST_SPRITE_NAME[0]);
				}
				else
				{
					SetButtonSprite(t, WORD_LIST_SPRITE_NAME[2]);
				}
				if (!degreeData.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
				{
					SetLabelText(t, UI.LBL_WORD_NORMAL, degreeData.name);
					SetLabelText(t, UI.LBL_WORD_SELECTED, degreeData.name);
					SetActive(t, UI.LBL_WORD_NORMAL, currentDegrees[(int)currentTab] != (int)degreeData.id);
					SetActive(t, UI.LBL_WORD_SELECTED, currentDegrees[(int)currentTab] == (int)degreeData.id);
					SetActive(t, UI.LBL_WORD_UNKNOWN, is_visible: false);
				}
				else
				{
					SetActive(t, UI.LBL_WORD_NORMAL, is_visible: false);
					SetActive(t, UI.LBL_WORD_SELECTED, is_visible: false);
					SetActive(t, UI.LBL_WORD_UNKNOWN, is_visible: true);
				}
				SetActive(t, UI.SPR_WORD_SELECTED, degreeData == currentSelectData);
			});
		}
		SetTab(currentTab, data.type);
		SetDegreeDetail();
	}

	public void OnQuery_SELECT()
	{
		currentSelectData = (GameSection.GetEventData() as DegreeTable.DegreeData);
		if (currentSelectData.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
		{
			currentDegrees[(int)currentTab] = (int)currentSelectData.id;
		}
		RefreshUI();
	}

	public void OnQuery_SORT()
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

	public void OnQuery_OMAKASE()
	{
		int index = Random.Range(0, userHaveFrameData.Count);
		currentDegrees[0] = (int)userHaveFrameData[index].id;
		index = Random.Range(0, userHaveNounData.Count);
		currentDegrees[1] = (int)userHaveNounData[index].id;
		index = Random.Range(0, userHaveConData.Count);
		currentDegrees[2] = (int)userHaveConData[index].id;
		index = Random.Range(0, userHaveNounData.Count);
		currentDegrees[3] = (int)userHaveNounData[index].id;
		currentTab = WORD_TAB.PREFIX;
		currentPage = 1;
		currentSelectData = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[1]);
		RefreshUI();
	}

	public void OnQuery_ON_PREFIX()
	{
		currentTab = WORD_TAB.PREFIX;
		currentPage = 1;
		currentSelectData = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[1]);
		RefreshUI();
	}

	public void OnQuery_ON_CONJUNCTION()
	{
		currentTab = WORD_TAB.CONJUNCTION;
		currentPage = 1;
		currentSelectData = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[2]);
		RefreshUI();
	}

	public void OnQuery_ON_SUFFIX()
	{
		currentTab = WORD_TAB.SUFFIX;
		currentPage = 1;
		currentSelectData = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[3]);
		RefreshUI();
	}

	public void OnQuery_CONFIRM_CHANGE()
	{
		bool flag = false;
		for (int i = 0; i < currentDegrees.Count; i++)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds[i] != currentDegrees[i])
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			GameSection.StopEvent();
			GameSection.BackSection();
		}
	}

	public void OnQuery_ProfileChangeDegreeConfirmDialog_YES()
	{
		GameSection.StayEvent();
		DegreeEquipModel.RequestSendForm requestSendForm = new DegreeEquipModel.RequestSendForm();
		requestSendForm.degid0 = currentDegrees[0].ToString();
		requestSendForm.degid1 = currentDegrees[1].ToString();
		requestSendForm.degid2 = currentDegrees[2].ToString();
		requestSendForm.degid3 = currentDegrees[3].ToString();
		Protocol.Send(DegreeEquipModel.URL, requestSendForm, delegate(DegreeEquipModel x)
		{
			GameSection.ResumeEvent(x.Error == Error.None);
			if (x.Error == Error.None)
			{
				RequestEvent("[BACK]");
			}
		});
	}

	private void SetDegreeDetail()
	{
		if (currentSelectData == null)
		{
			SetLabelText(UI.LBL_SELECTED_DEGREE_NAME, "---");
			SetLabelText(UI.LBL_SELECTED_DEGREE_REQUIREMENT, "---");
		}
		else
		{
			SetLabelText(UI.LBL_SELECTED_DEGREE_NAME, currentSelectData.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds) ? "???" : currentSelectData.name);
			SetLabelText(UI.LBL_SELECTED_DEGREE_REQUIREMENT, currentSelectData.IsSecretText(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds) ? "???" : currentSelectData.requirementText);
		}
	}

	private void SetTab(WORD_TAB selectTab, DEGREE_TYPE frameType)
	{
		if (frameType != DEGREE_TYPE.SPECIAL_FRAME)
		{
			SetButtonSprite(UI.BTN_PREFIX, TAB_SPRITE_NAME[(selectTab == WORD_TAB.PREFIX) ? 1 : 0]);
			SetButtonSprite(UI.BTN_CONJUNCTION, TAB_SPRITE_NAME[(selectTab == WORD_TAB.CONJUNCTION) ? 1 : 0]);
			SetButtonSprite(UI.BTN_SUFFIX, TAB_SPRITE_NAME[(selectTab == WORD_TAB.SUFFIX) ? 1 : 0]);
			SetButtonEnabled(UI.BTN_PREFIX, selectTab != WORD_TAB.PREFIX);
			SetButtonEnabled(UI.BTN_CONJUNCTION, selectTab != WORD_TAB.CONJUNCTION);
			SetButtonEnabled(UI.BTN_SUFFIX, selectTab != WORD_TAB.SUFFIX);
			GetCtrl(UI.LBL_PREFIX).GetComponent<UILabel>().color = ((selectTab != WORD_TAB.PREFIX) ? normalColors[0] : selectColors[0]);
			GetCtrl(UI.LBL_PREFIX).GetComponent<UILabel>().effectColor = ((selectTab != WORD_TAB.PREFIX) ? normalColors[1] : selectColors[1]);
			GetCtrl(UI.LBL_CONJUNCTION).GetComponent<UILabel>().color = ((selectTab != WORD_TAB.CONJUNCTION) ? normalColors[0] : selectColors[0]);
			GetCtrl(UI.LBL_CONJUNCTION).GetComponent<UILabel>().effectColor = ((selectTab != WORD_TAB.CONJUNCTION) ? normalColors[1] : selectColors[1]);
			GetCtrl(UI.LBL_SUFFIX).GetComponent<UILabel>().color = ((selectTab != WORD_TAB.SUFFIX) ? normalColors[0] : selectColors[0]);
			GetCtrl(UI.LBL_SUFFIX).GetComponent<UILabel>().effectColor = ((selectTab != WORD_TAB.SUFFIX) ? normalColors[1] : selectColors[1]);
			SetActive(UI.SPR_PREFIX_SELECT, selectTab == WORD_TAB.PREFIX);
			SetActive(UI.SPR_CONJUNCTION_SELECT, selectTab == WORD_TAB.CONJUNCTION);
			SetActive(UI.SPR_SUFFIX_SELECT, selectTab == WORD_TAB.SUFFIX);
			arrowTrans.gameObject.SetActive(value: true);
			switch (selectTab)
			{
			default:
				arrowTrans.parent = GetCtrl(UI.BTN_PREFIX);
				break;
			case WORD_TAB.CONJUNCTION:
				arrowTrans.parent = GetCtrl(UI.BTN_CONJUNCTION);
				break;
			case WORD_TAB.SUFFIX:
				arrowTrans.parent = GetCtrl(UI.BTN_SUFFIX);
				break;
			}
			arrowTrans.localPosition = arrowLocalPos;
			arrowTrans.localScale = arrowlocalScale;
		}
		else
		{
			SetSprite(UI.BTN_PREFIX, TAB_SPRITE_NAME[2]);
			SetSprite(UI.BTN_CONJUNCTION, TAB_SPRITE_NAME[2]);
			SetSprite(UI.BTN_SUFFIX, TAB_SPRITE_NAME[2]);
			SetButtonEnabled(UI.BTN_PREFIX, is_enabled: false);
			SetButtonEnabled(UI.BTN_CONJUNCTION, is_enabled: false);
			SetButtonEnabled(UI.BTN_SUFFIX, is_enabled: false);
			SetActive(UI.SPR_PREFIX_SELECT, is_visible: false);
			SetActive(UI.SPR_CONJUNCTION_SELECT, is_visible: false);
			SetActive(UI.SPR_SUFFIX_SELECT, is_visible: false);
			arrowTrans.gameObject.SetActive(value: false);
		}
	}

	private void SpoileColor()
	{
		UILabel component = GetCtrl(UI.LBL_PREFIX).GetComponent<UILabel>();
		UILabel component2 = GetCtrl(UI.LBL_CONJUNCTION).GetComponent<UILabel>();
		selectColors[0] = component.color;
		selectColors[1] = component.effectColor;
		normalColors[0] = component2.color;
		normalColors[1] = component2.effectColor;
	}
}
