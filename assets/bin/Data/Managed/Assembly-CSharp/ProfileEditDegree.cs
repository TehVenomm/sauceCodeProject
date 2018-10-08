using Network;
using System;
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

	private Color[] selectColors = (Color[])new Color[2];

	private Color[] normalColors = (Color[])new Color[2];

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "DegreeTable";
		}
	}

	public unsafe override void Initialize()
	{
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		currentDegrees = MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds.ToList();
		showAll = true;
		currentTab = WORD_TAB.PREFIX;
		SpoileColor();
		DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint)currentDegrees[0]);
		currentSelectData = ((data.type != DEGREE_TYPE.SPECIAL_FRAME) ? Singleton<DegreeTable>.I.GetData((uint)currentDegrees[1]) : null);
		List<DegreeTable.DegreeData> all = Singleton<DegreeTable>.I.GetAll();
		all.Sort((DegreeTable.DegreeData a, DegreeTable.DegreeData b) => (int)(a.id - b.id));
		List<DegreeTable.DegreeData> source = all;
		if (_003C_003Ef__am_0024cache15 == null)
		{
			_003C_003Ef__am_0024cache15 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		allNounData = source.Where(_003C_003Ef__am_0024cache15).ToList();
		List<DegreeTable.DegreeData> source2 = all;
		if (_003C_003Ef__am_0024cache16 == null)
		{
			_003C_003Ef__am_0024cache16 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		allConData = source2.Where(_003C_003Ef__am_0024cache16).ToList();
		List<DegreeTable.DegreeData> source3 = allNounData;
		if (_003C_003Ef__am_0024cache17 == null)
		{
			_003C_003Ef__am_0024cache17 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		userHaveNounData = source3.Where(_003C_003Ef__am_0024cache17).ToList();
		List<DegreeTable.DegreeData> source4 = allConData;
		if (_003C_003Ef__am_0024cache18 == null)
		{
			_003C_003Ef__am_0024cache18 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		userHaveConData = source4.Where(_003C_003Ef__am_0024cache18).ToList();
		List<DegreeTable.DegreeData> source5 = all;
		if (_003C_003Ef__am_0024cache19 == null)
		{
			_003C_003Ef__am_0024cache19 = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<DegreeTable.DegreeData> source6 = source5.Where(_003C_003Ef__am_0024cache19);
		if (_003C_003Ef__am_0024cache1A == null)
		{
			_003C_003Ef__am_0024cache1A = new Func<DegreeTable.DegreeData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		userHaveFrameData = source6.Where(_003C_003Ef__am_0024cache1A).ToList();
		currentPage = 1;
		currentPlate = GetCtrl(UI.OBJ_DEGREE_PLATE_ROOT).GetComponent<DegreePlate>();
		arrowTrans = GetCtrl(UI.SPR_TAB_SELECTING);
		arrowLocalPos = arrowTrans.get_localPosition();
		arrowlocalScale = arrowTrans.get_localScale();
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

	public unsafe override void UpdateUI()
	{
		base.UpdateUI();
		if (currentTab == WORD_TAB.CONJUNCTION)
		{
			currentShowData = ((!showAll) ? userHaveConData : allConData);
		}
		else
		{
			currentShowData = ((!showAll) ? userHaveNounData : allNounData);
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
			SetLabelText((Enum)UI.LBL_PREFIX, string.Empty);
			SetLabelText((Enum)UI.LBL_CONJUNCTION, string.Empty);
			SetLabelText((Enum)UI.LBL_SUFFIX, string.Empty);
			SetLabelText((Enum)UI.LBL_PAGE_MAX, maxPage.ToString());
			SetLabelText((Enum)UI.LBL_PAGE_NOW, currentPage.ToString());
			SetActive((Enum)UI.OBJ_ARROW_ACTIVE_ROOT, false);
			SetActive((Enum)UI.OBJ_ARROW_INACTIVE_ROOT, true);
			SetActive((Enum)UI.LBL_NO_SELECTABLE_FRAME, true);
			currentPlate.Initialize(currentDegrees, false, delegate
			{
			});
			object grid_ctrl_enum = UI.GRD_WORD_LIST;
			if (_003C_003Ef__am_0024cache1C == null)
			{
				_003C_003Ef__am_0024cache1C = new Action<int, Transform, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			SetGrid((Enum)grid_ctrl_enum, "DegreeWordList", 0, false, _003C_003Ef__am_0024cache1C);
		}
		else
		{
			SetLabelText((Enum)UI.LBL_PREFIX, Singleton<DegreeTable>.I.GetData((uint)currentDegrees[1]).name);
			SetLabelText((Enum)UI.LBL_CONJUNCTION, Singleton<DegreeTable>.I.GetData((uint)currentDegrees[2]).name);
			SetLabelText((Enum)UI.LBL_SUFFIX, Singleton<DegreeTable>.I.GetData((uint)currentDegrees[3]).name);
			SetLabelText((Enum)UI.LBL_SORT, (!showAll) ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 21u) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 20u));
			SetLabelText((Enum)UI.LBL_PAGE_MAX, maxPage.ToString());
			SetLabelText((Enum)UI.LBL_PAGE_NOW, currentPage.ToString());
			SetActive((Enum)UI.OBJ_ARROW_ACTIVE_ROOT, maxPage > 1);
			SetActive((Enum)UI.OBJ_ARROW_INACTIVE_ROOT, maxPage == 1);
			SetActive((Enum)UI.LBL_NO_SELECTABLE_FRAME, false);
			currentPlate.Initialize(currentDegrees, false, delegate
			{
			});
			int item_num = Mathf.Min(GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT, currentShowData.Count - (currentPage - 1) * GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT);
			SetGrid(UI.GRD_WORD_LIST, "DegreeWordList", item_num, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
			GameSection.ResumeEvent(x.Error == Error.None, null, false);
			if (x.Error == Error.None)
			{
				RequestEvent("[BACK]", null);
			}
		}, string.Empty);
	}

	private void SetDegreeDetail()
	{
		if (currentSelectData == null)
		{
			SetLabelText((Enum)UI.LBL_SELECTED_DEGREE_NAME, "---");
			SetLabelText((Enum)UI.LBL_SELECTED_DEGREE_REQUIREMENT, "---");
		}
		else
		{
			SetLabelText((Enum)UI.LBL_SELECTED_DEGREE_NAME, (!currentSelectData.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds)) ? currentSelectData.name : "???");
			SetLabelText((Enum)UI.LBL_SELECTED_DEGREE_REQUIREMENT, (!currentSelectData.IsSecretText(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds)) ? currentSelectData.requirementText : "???");
		}
	}

	private void SetTab(WORD_TAB selectTab, DEGREE_TYPE frameType)
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		if (frameType != DEGREE_TYPE.SPECIAL_FRAME)
		{
			SetButtonSprite((Enum)UI.BTN_PREFIX, TAB_SPRITE_NAME[(selectTab == WORD_TAB.PREFIX) ? 1 : 0], false);
			SetButtonSprite((Enum)UI.BTN_CONJUNCTION, TAB_SPRITE_NAME[(selectTab == WORD_TAB.CONJUNCTION) ? 1 : 0], false);
			SetButtonSprite((Enum)UI.BTN_SUFFIX, TAB_SPRITE_NAME[(selectTab == WORD_TAB.SUFFIX) ? 1 : 0], false);
			SetButtonEnabled((Enum)UI.BTN_PREFIX, selectTab != WORD_TAB.PREFIX);
			SetButtonEnabled((Enum)UI.BTN_CONJUNCTION, selectTab != WORD_TAB.CONJUNCTION);
			SetButtonEnabled((Enum)UI.BTN_SUFFIX, selectTab != WORD_TAB.SUFFIX);
			GetCtrl(UI.LBL_PREFIX).GetComponent<UILabel>().color = ((selectTab == WORD_TAB.PREFIX) ? selectColors[0] : normalColors[0]);
			GetCtrl(UI.LBL_PREFIX).GetComponent<UILabel>().effectColor = ((selectTab == WORD_TAB.PREFIX) ? selectColors[1] : normalColors[1]);
			GetCtrl(UI.LBL_CONJUNCTION).GetComponent<UILabel>().color = ((selectTab == WORD_TAB.CONJUNCTION) ? selectColors[0] : normalColors[0]);
			GetCtrl(UI.LBL_CONJUNCTION).GetComponent<UILabel>().effectColor = ((selectTab == WORD_TAB.CONJUNCTION) ? selectColors[1] : normalColors[1]);
			GetCtrl(UI.LBL_SUFFIX).GetComponent<UILabel>().color = ((selectTab == WORD_TAB.SUFFIX) ? selectColors[0] : normalColors[0]);
			GetCtrl(UI.LBL_SUFFIX).GetComponent<UILabel>().effectColor = ((selectTab == WORD_TAB.SUFFIX) ? selectColors[1] : normalColors[1]);
			SetActive((Enum)UI.SPR_PREFIX_SELECT, selectTab == WORD_TAB.PREFIX);
			SetActive((Enum)UI.SPR_CONJUNCTION_SELECT, selectTab == WORD_TAB.CONJUNCTION);
			SetActive((Enum)UI.SPR_SUFFIX_SELECT, selectTab == WORD_TAB.SUFFIX);
			arrowTrans.get_gameObject().SetActive(true);
			switch (selectTab)
			{
			default:
				arrowTrans.set_parent(GetCtrl(UI.BTN_PREFIX));
				break;
			case WORD_TAB.CONJUNCTION:
				arrowTrans.set_parent(GetCtrl(UI.BTN_CONJUNCTION));
				break;
			case WORD_TAB.SUFFIX:
				arrowTrans.set_parent(GetCtrl(UI.BTN_SUFFIX));
				break;
			}
			arrowTrans.set_localPosition(arrowLocalPos);
			arrowTrans.set_localScale(arrowlocalScale);
		}
		else
		{
			SetSprite((Enum)UI.BTN_PREFIX, TAB_SPRITE_NAME[2]);
			SetSprite((Enum)UI.BTN_CONJUNCTION, TAB_SPRITE_NAME[2]);
			SetSprite((Enum)UI.BTN_SUFFIX, TAB_SPRITE_NAME[2]);
			SetButtonEnabled((Enum)UI.BTN_PREFIX, false);
			SetButtonEnabled((Enum)UI.BTN_CONJUNCTION, false);
			SetButtonEnabled((Enum)UI.BTN_SUFFIX, false);
			SetActive((Enum)UI.SPR_PREFIX_SELECT, false);
			SetActive((Enum)UI.SPR_CONJUNCTION_SELECT, false);
			SetActive((Enum)UI.SPR_SUFFIX_SELECT, false);
			arrowTrans.get_gameObject().SetActive(false);
		}
	}

	private void SpoileColor()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		UILabel component = GetCtrl(UI.LBL_PREFIX).GetComponent<UILabel>();
		UILabel component2 = GetCtrl(UI.LBL_CONJUNCTION).GetComponent<UILabel>();
		selectColors[0] = component.color;
		selectColors[1] = component.effectColor;
		normalColors[0] = component2.color;
		normalColors[1] = component2.effectColor;
	}
}
