using Network;
using System;
using System.Collections;
using UnityEngine;

public class StatusTop : SkillInfoBase
{
	private enum UI
	{
		OBJ_STATUS_UI_ROOT,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		BTN_EQUIP_SET_L,
		BTN_EQUIP_SET_R,
		LBL_NOW,
		LBL_MAX,
		OBJ_EQUIP_ROOT,
		OBJ_EQUIP_ROT_ROOT,
		OBJ_ICON_WEAPON_1,
		OBJ_ICON_WEAPON_2,
		OBJ_ICON_WEAPON_3,
		OBJ_ICON_ARMOR,
		OBJ_ICON_HELM,
		OBJ_ICON_ARM,
		OBJ_ICON_LEG,
		BTN_ICON_WEAPON_1,
		BTN_ICON_WEAPON_2,
		BTN_ICON_WEAPON_3,
		BTN_ICON_ARMOR,
		BTN_ICON_HELM,
		BTN_ICON_ARM,
		BTN_ICON_LEG,
		OBJ_VISUAL_ROOT,
		OBJ_ICON_VISUAL_ARMOR,
		OBJ_ICON_VISUAL_HELM,
		OBJ_ICON_VISUAL_ARM,
		OBJ_ICON_VISUAL_LEG,
		BTN_ICON_VISUAL_ARMOR_BASE,
		BTN_ICON_VISUAL_HELM_BASE,
		BTN_ICON_VISUAL_ARM_BASE,
		BTN_ICON_VISUAL_LEG_BASE,
		SPR_AVATAR_ACTIVE,
		SPR_PARAMETER_ACTIVE,
		BTN_AVATAR_INACTIVE,
		BTN_PARAMETER_INACTIVE,
		OBJ_EQUIP_SET_SELECT,
		OBJ_STUDIO_BUTTON_ROOT,
		OBJ_AVATAR_BUTTON_ROOT,
		OBJ_PARAMETER_BUTTON_ROOT,
		BTN_VISIBLE_UI,
		BTN_INVISIBLE_UI,
		BTN_VISIBLE_HELM,
		BTN_INVISIBLE_HELM,
		TGL_VISIBLE_HELM_BUTTON,
		TGL_VISIBLE_UI_BUTTON,
		LBL_LEVEL_WEAPON_1,
		LBL_LEVEL_WEAPON_2,
		LBL_LEVEL_WEAPON_3,
		LBL_LEVEL_ARMOR,
		LBL_LEVEL_HELM,
		LBL_LEVEL_ARM,
		LBL_LEVEL_LEG,
		LBL_LEVEL_WEAPON_1_SHADOW,
		LBL_LEVEL_WEAPON_2_SHADOW,
		LBL_LEVEL_WEAPON_3_SHADOW,
		LBL_LEVEL_ARMOR_SHADOW,
		LBL_LEVEL_HELM_SHADOW,
		LBL_LEVEL_ARM_SHADOW,
		LBL_LEVEL_LEG_SHADOW,
		TGL_SHOW_EQUIP_TYPE,
		BTN_STUDIO,
		BTN_EQUIPLIST,
		OBJ_SKILL_BUTTON_ROOT,
		OBJ_WITH_MONSTER_ROOT,
		OBJ_WITHOUT_MONSTER_ROOT,
		BTN_EQUIP_SET_COPY,
		BTN_EQUIP_SET_PASTE,
		BTN_EQUIP_SET_DELETE,
		LBL_SET_NAME,
		SCR_DRUM,
		GRD_DRUM,
		LBL_EQUIP_NO,
		ANCHOR_RIGHT_TOP,
		BTN_UNIQUE
	}

	private enum EQUIP_SET_COPY_MODE
	{
		NONE,
		COPY
	}

	public EquipSetInfo[] localEquipSet;

	public int equipSetNo;

	private int SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();

	private EQUIP_SET_COPY_MODE equipSetCopyMode;

	private int equipSetCopyNo;

	private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;

	private bool showEquipMode = true;

	private UI[] icons = new UI[7]
	{
		UI.OBJ_ICON_WEAPON_1,
		UI.OBJ_ICON_WEAPON_2,
		UI.OBJ_ICON_WEAPON_3,
		UI.OBJ_ICON_ARMOR,
		UI.OBJ_ICON_HELM,
		UI.OBJ_ICON_ARM,
		UI.OBJ_ICON_LEG
	};

	private EQUIPMENT_TYPE[] visualType = new EQUIPMENT_TYPE[4]
	{
		EQUIPMENT_TYPE.ARMOR,
		EQUIPMENT_TYPE.HELM,
		EQUIPMENT_TYPE.ARM,
		EQUIPMENT_TYPE.LEG
	};

	private UI[] iconsBtn = new UI[7]
	{
		UI.BTN_ICON_WEAPON_1,
		UI.BTN_ICON_WEAPON_2,
		UI.BTN_ICON_WEAPON_3,
		UI.BTN_ICON_ARMOR,
		UI.BTN_ICON_HELM,
		UI.BTN_ICON_ARM,
		UI.BTN_ICON_LEG
	};

	private UI[] iconsVisual = new UI[4]
	{
		UI.OBJ_ICON_VISUAL_ARMOR,
		UI.OBJ_ICON_VISUAL_HELM,
		UI.OBJ_ICON_VISUAL_ARM,
		UI.OBJ_ICON_VISUAL_LEG
	};

	private UI[] iconsVisualBtn = new UI[4]
	{
		UI.BTN_ICON_VISUAL_ARMOR_BASE,
		UI.BTN_ICON_VISUAL_HELM_BASE,
		UI.BTN_ICON_VISUAL_ARM_BASE,
		UI.BTN_ICON_VISUAL_LEG_BASE
	};

	private UI[] lblEquipLevel = new UI[7]
	{
		UI.LBL_LEVEL_WEAPON_1,
		UI.LBL_LEVEL_WEAPON_2,
		UI.LBL_LEVEL_WEAPON_3,
		UI.LBL_LEVEL_ARMOR,
		UI.LBL_LEVEL_HELM,
		UI.LBL_LEVEL_ARM,
		UI.LBL_LEVEL_LEG
	};

	private UI[] lblShadowEquipLevel = new UI[7]
	{
		UI.LBL_LEVEL_WEAPON_1_SHADOW,
		UI.LBL_LEVEL_WEAPON_2_SHADOW,
		UI.LBL_LEVEL_WEAPON_3_SHADOW,
		UI.LBL_LEVEL_ARMOR_SHADOW,
		UI.LBL_LEVEL_HELM_SHADOW,
		UI.LBL_LEVEL_ARM_SHADOW,
		UI.LBL_LEVEL_LEG_SHADOW
	};

	private StatusManager.LocalVisual visualEquip;

	private UI? tweenTarget;

	private UICenterOnChild uiCenterOnChild;

	private int detailEquipSetNo = -1;

	private EquipItemInfo visualDetailEquip;

	private int visualDetailItemIndex = -1;

	public override bool useOnPressBackKey => true;

	public override void OnPressBackKey()
	{
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		DispatchEvent(goingHomeEvent);
	}

	public override void Initialize()
	{
		showEquipMode = true;
		tweenTarget = null;
		SetActive((Enum)UI.OBJ_WITH_MONSTER_ROOT, is_visible: true);
		SetActive((Enum)UI.OBJ_WITHOUT_MONSTER_ROOT, is_visible: false);
		SetActive((Enum)UI.BTN_UNIQUE, GameSaveData.instance.IsOpenUniqueStatus());
		SettingEquipSetInfo();
		SetDynamicList((Enum)UI.GRD_DRUM, "equipno", SET_NO_MAX, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool isRecycle)
		{
			SetLabelText(t, UI.LBL_EQUIP_NO, (i + 1).ToString());
		});
		SetCenterOnChildFunc((Enum)UI.GRD_DRUM, (SpringPanel.OnFinished)delegate
		{
			int result = equipSetNo;
			if (int.TryParse(GetLabel(GetCenter(GetCtrl(UI.GRD_DRUM)), UI.LBL_EQUIP_NO), out result))
			{
				equipSetNo = Mathf.Clamp(result - 1, 0, SET_NO_MAX - 1);
			}
			MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(equipSetNo);
			tweenTarget = UI.OBJ_EQUIP_ROOT;
			RefreshUI();
		});
		SetCenter(GetCtrl(UI.GRD_DRUM), equipSetNo, is_instant: true);
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_11_weapon_tut, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_11_weapon_tut.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_11_weapon_tut, "Tutorial");
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_15_upgrading_tut, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_15_upgrading_tut.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_15_upgrading_tut, "Tutorial");
		}
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		Singleton<EquipItemTable>.I.CreateTableForEquipList();
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		base.Initialize();
	}

	protected override void OnOpen()
	{
		object eventData = GameSection.GetEventData();
		if (eventData is StatusEquip.ChangeEquipData)
		{
			StatusEquip.ChangeEquipData changeEquipData = eventData as StatusEquip.ChangeEquipData;
			if (showEquipMode)
			{
				localEquipSet[changeEquipData.setNo].item[changeEquipData.index] = changeEquipData.item;
				MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(localEquipSet[changeEquipData.setNo], changeEquipData.setNo, changeEquipData.index);
			}
			else
			{
				int index = changeEquipData.index;
				ulong num = (visualEquip.visualItem[index] == null) ? 0 : visualEquip.visualItem[index].uniqueID;
				visualEquip.visualItem[index] = changeEquipData.item;
				ulong num2 = (visualEquip.visualItem[index] == null) ? 0 : visualEquip.visualItem[index].uniqueID;
				if (num != num2)
				{
					UpdateModel();
				}
			}
		}
		else if (eventData is StatusEquip.ChangeEquipData[])
		{
			StatusEquip.ChangeEquipData[] array = eventData as StatusEquip.ChangeEquipData[];
			for (int j = 0; j < array.Length; j++)
			{
				if ((array[j].index != 0 || array[j].item != null) && (array[j].index != 3 || array[j].item != null))
				{
					localEquipSet[array[j].setNo].item[array[j].index] = array[j].item;
					MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(localEquipSet[array[j].setNo], array[j].setNo, array[j].index);
				}
			}
		}
		else
		{
			ResetEquipSetCopy();
		}
		localEquipSetUpdate();
		SetDynamicList((Enum)UI.GRD_DRUM, "equipno", SET_NO_MAX, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool isRecycle)
		{
			SetLabelText(t, UI.LBL_EQUIP_NO, (i + 1).ToString());
		});
		SetCenter(GetCtrl(UI.GRD_DRUM), equipSetNo, is_instant: true);
	}

	protected override void OnCloseStart()
	{
		ResetDetailTarget();
		OnClose();
	}

	private void UpdateModel()
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.SetupLoadInfo(localEquipSet[equipSetNo], 0uL, visualEquip.VisialID(0), visualEquip.VisialID(1), visualEquip.VisialID(2), visualEquip.VisialID(3), localEquipSet[equipSetNo].showHelm == 1);
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.LoadPlayer(playerLoadInfo);
		}
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(equipSetNo, delegate
		{
			base.Exit();
		});
	}

	public void SettingEquipSetInfo()
	{
		if (localEquipSet == null)
		{
			localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
			equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
			MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(equipSetNo);
		}
		if (visualEquip == null)
		{
			visualEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalVisualEquip();
		}
	}

	public void ForceSettingEquipSetInfo()
	{
		localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
		DrawEquipSetModel();
	}

	public override void UpdateUI()
	{
		int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
		SetBadge((Enum)UI.BTN_STUDIO, badgeTotalNum, 1, 8, -8, is_scale_normalize: true);
		DrawEquipModeButton();
		int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		if (showEquipMode)
		{
			EquipSetInfo equipSetInfo = localEquipSet[equipSetNo];
			if (equipSetInfo != null && equipSetInfo.item[4] == null)
			{
				SetActive((Enum)UI.TGL_VISIBLE_HELM_BUTTON, is_visible: false);
			}
			else
			{
				SetActive((Enum)UI.TGL_VISIBLE_HELM_BUTTON, is_visible: true);
			}
			int j = 0;
			for (int num = icons.Length; j < num; j++)
			{
				EquipItemInfo equipItemInfo = equipSetInfo.item[j];
				Transform ctrl = GetCtrl(icons[j]);
				ctrl.GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
				int k = 0;
				for (int count = Temporary.itemIconList.Count; k < count; k++)
				{
					Temporary.itemIconList[k].get_gameObject().SetActive(true);
				}
				Temporary.itemIconList.Clear();
				ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, sex, GetCtrl(icons[j]), null, -1, "DETAIL", j);
				int num2 = -1;
				string text = string.Empty;
				if (equipItemInfo != null && equipItemInfo.tableID != 0)
				{
					EquipItemTable.EquipItemData tableData = equipItemInfo.tableData;
					num2 = tableData.GetIconID(sex);
					text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), equipItemInfo.level);
				}
				itemIcon.get_gameObject().SetActive(num2 != -1);
				SetEvent((Enum)iconsBtn[j], (num2 == -1) ? "EQUIP" : "DETAIL", j);
				SetLabelText((Enum)lblEquipLevel[j], text);
				SetLabelText((Enum)lblShadowEquipLevel[j], text);
				if (num2 != -1)
				{
					itemIcon.SetEquipExt(equipItemInfo, base.GetComponent<UILabel>((Enum)lblEquipLevel[j]));
				}
				Transform ctrl2 = GetCtrl(iconsBtn[j]);
				bool flag = equipItemInfo != null && equipItemInfo.tableID != 0;
				if (flag)
				{
					Transform root = ctrl2;
					Enum ui_widget_enum = UI.OBJ_SKILL_BUTTON_ROOT;
					string skill_button_prefab_name = "SkillIconButtonTOP";
					EquipItemTable.EquipItemData tableData2 = equipItemInfo.tableData;
					SkillSlotUIData[] skillSlotData = GetSkillSlotData(equipItemInfo);
					int button_event_data = j;
					SetSkillIconButton(root, ui_widget_enum, skill_button_prefab_name, tableData2, skillSlotData, "SKILL_ICON_BUTTON", button_event_data);
				}
				FindCtrl(ctrl2, UI.OBJ_SKILL_BUTTON_ROOT).get_gameObject().SetActive(flag);
			}
		}
		else
		{
			int l = 0;
			for (int num3 = visualEquip.visualItem.Length; l < num3; l++)
			{
				EquipItemInfo equipItemInfo2 = visualEquip.visualItem[l];
				Transform ctrl3 = GetCtrl(iconsVisual[l]);
				ctrl3.GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
				int m = 0;
				for (int count2 = Temporary.itemIconList.Count; m < count2; m++)
				{
					Temporary.itemIconList[m].get_gameObject().SetActive(true);
				}
				Temporary.itemIconList.Clear();
				ItemIcon itemIcon2 = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo2, sex, ctrl3, null, -1, "AVATAR", l);
				SetLongTouch(itemIcon2.transform, "VISUAL_DETAIL", l);
				int num4 = -1;
				if (equipItemInfo2 != null)
				{
					num4 = equipItemInfo2.tableData.GetIconID(sex);
				}
				itemIcon2.get_gameObject().SetActive(num4 != -1);
				SetEvent((Enum)iconsVisualBtn[l], "AVATAR", l);
				SetLongTouch((Enum)iconsVisualBtn[l], "VISUAL_DETAIL", (object)l);
			}
		}
		DrawEquipSetModel();
		UI? uI = tweenTarget;
		if (uI.HasValue)
		{
			ResetTween((Enum)(object)tweenTarget);
			PlayTween((Enum)(object)tweenTarget, forward: true, null, is_input_block: false);
		}
		SetActive((Enum)UI.OBJ_STUDIO_BUTTON_ROOT, showEquipMode);
		SetActive((Enum)UI.TGL_VISIBLE_UI_BUTTON, !showEquipMode);
		SetToggle((Enum)UI.TGL_SHOW_EQUIP_TYPE, showEquipMode);
		if (visualEquip.isVisibleHelm != (localEquipSet[equipSetNo].showHelm == 1))
		{
			ResetTween((Enum)UI.BTN_VISIBLE_HELM, 0);
			ResetTween((Enum)UI.BTN_INVISIBLE_HELM, 0);
			if (localEquipSet[equipSetNo].showHelm == 1)
			{
				PlayTween((Enum)UI.BTN_INVISIBLE_HELM, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			}
			else
			{
				PlayTween((Enum)UI.BTN_VISIBLE_HELM, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			}
			visualEquip.isVisibleHelm = (localEquipSet[equipSetNo].showHelm == 1);
		}
		SetToggleButton((Enum)UI.TGL_VISIBLE_HELM_BUTTON, visualEquip.isVisibleHelm, (Action<bool>)delegate(bool is_active)
		{
			visualEquip.isVisibleHelm = is_active;
			localEquipSet[equipSetNo].showHelm = (visualEquip.isVisibleHelm ? 1 : 0);
			ResetTween((Enum)UI.BTN_VISIBLE_HELM, 0);
			ResetTween((Enum)UI.BTN_INVISIBLE_HELM, 0);
			if (is_active)
			{
				PlayTween((Enum)UI.BTN_INVISIBLE_HELM, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			}
			else
			{
				PlayTween((Enum)UI.BTN_VISIBLE_HELM, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			}
			UpdateModel();
		});
		DrawEquipSetCopyModeButton();
		SetDynamicList((Enum)UI.GRD_DRUM, "equipno", SET_NO_MAX, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool isRecycle)
		{
			SetLabelText(t, UI.LBL_EQUIP_NO, (i + 1).ToString());
		});
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			GetCtrl(UI.SCR_DRUM).get_gameObject().GetComponent<UIScrollView>().set_enabled(false);
		}
		base.UpdateUI();
	}

	public void DrawEquipSetModel()
	{
		EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(equipSetNo);
		SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
		SetLabelText((Enum)UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText((Enum)UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
		SetLabelText((Enum)UI.LBL_HP, finalStatus.hp.ToString());
		SetLabelText((Enum)UI.LBL_NOW, (equipSetNo + 1).ToString());
		SetLabelText((Enum)UI.LBL_MAX, SET_NO_MAX.ToString());
		SetLabelText((Enum)UI.LBL_SET_NAME, localEquipSet[equipSetNo].name);
		UpdateModel();
	}

	private void DrawEquipModeButton()
	{
		SetActive((Enum)UI.OBJ_EQUIP_ROOT, showEquipMode);
		SetActive((Enum)UI.BTN_AVATAR_INACTIVE, showEquipMode);
		SetActive((Enum)UI.OBJ_EQUIP_SET_SELECT, showEquipMode);
		SetActive((Enum)UI.SPR_PARAMETER_ACTIVE, showEquipMode);
		SetActive((Enum)UI.OBJ_VISUAL_ROOT, !showEquipMode);
		SetActive((Enum)UI.BTN_PARAMETER_INACTIVE, !showEquipMode);
		SetActive((Enum)UI.SPR_AVATAR_ACTIVE, !showEquipMode);
	}

	private void DrawEquipSetCopyModeButton()
	{
		bool flag = equipSetNo == equipSetCopyNo;
		bool flag2 = equipSetCopyMode == EQUIP_SET_COPY_MODE.COPY;
		SetActive((Enum)UI.BTN_EQUIP_SET_COPY, !flag2);
		SetActive((Enum)UI.BTN_EQUIP_SET_PASTE, flag2 && !flag);
		SetActive((Enum)UI.BTN_EQUIP_SET_DELETE, flag2 && flag);
	}

	private void OnQuery_EQUIP_SET_L()
	{
		if (equipSetNo > 0)
		{
			equipSetNo--;
		}
		else
		{
			equipSetNo = SET_NO_MAX - 1;
		}
		MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(equipSetNo);
		tweenTarget = UI.OBJ_EQUIP_ROOT;
		SetCenter(GetCtrl(UI.GRD_DRUM), equipSetNo, is_instant: true);
	}

	private void OnQuery_EQUIP_SET_R()
	{
		if (equipSetNo < SET_NO_MAX - 1)
		{
			equipSetNo++;
		}
		else
		{
			equipSetNo = 0;
		}
		MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(equipSetNo);
		tweenTarget = UI.OBJ_EQUIP_ROOT;
		SetCenter(GetCtrl(UI.GRD_DRUM), equipSetNo, is_instant: true);
	}

	private void OnQuery_EQUIP()
	{
		tweenTarget = null;
		int num = (int)GameSection.GetEventData();
		int num2 = equipSetNo;
		int num3 = (num2 != 0) ? (num % (num2 << 16)) : num;
		if (num2 < localEquipSet.Length && num3 < 7)
		{
			MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(localEquipSet[num2].item[num3]);
			MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = GetInventoryType(num3);
			GameSection.SetEventData(new StatusEquip.LocalEquipSetData(num2, num3, localEquipSet[num2]));
		}
	}

	private void OnQuery_AUTO_EQUIP()
	{
		int num = equipSetNo;
		if (num < localEquipSet.Length)
		{
			GameSection.SetEventData(new StatusEquip.LocalEquipSetData(num, 0, localEquipSet[num]));
		}
	}

	private void OnQuery_AVATAR()
	{
		int num = (int)GameSection.GetEventData();
		StatusEquip.LocalEquipSetData localEquipSetData = new StatusEquip.LocalEquipSetData(equipSetNo, num, localEquipSet[equipSetNo]);
		GameSection.SetEventData(new object[3]
		{
			visualType[num],
			visualEquip.visualItem[num],
			localEquipSetData
		});
	}

	private void OnQuery_MODE_EQUIP()
	{
		ResetDetailTarget();
		showEquipMode = true;
		tweenTarget = UI.OBJ_EQUIP_ROT_ROOT;
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetViewMode(StatusStageManager.VIEW_MODE.EQUIP);
		}
		ResetTween((Enum)UI.OBJ_PARAMETER_BUTTON_ROOT, 0);
		PlayTween((Enum)UI.OBJ_PARAMETER_BUTTON_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		RefreshUI();
	}

	private void OnQuery_MODE_VISUAL()
	{
		ResetDetailTarget();
		showEquipMode = false;
		tweenTarget = UI.OBJ_VISUAL_ROOT;
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetViewMode(StatusStageManager.VIEW_MODE.AVATAR);
		}
		ResetTween((Enum)UI.OBJ_AVATAR_BUTTON_ROOT, 0);
		PlayTween((Enum)UI.OBJ_AVATAR_BUTTON_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		RefreshUI();
	}

	private void OnQuery_TO_STORAGE()
	{
		CheckEquipChange();
	}

	private void OnQuery_STUDIO()
	{
		CheckEquipChange();
	}

	protected override void OnQuery_MAIN_MENU_QUEST()
	{
		tweenTarget = null;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(equipSetNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
			base.OnQuery_MAIN_MENU_QUEST();
		});
	}

	private void CheckEquipChange()
	{
		tweenTarget = null;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(equipSetNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_SKILL_LIST()
	{
		tweenTarget = null;
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP))
		{
		}
		GameSection.SetEventData(new object[4]
		{
			ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
			GetLocalEquipSetAttachSkillListData(equipSetNo),
			false,
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex
		});
	}

	private void ChangeWeaponForSkillTutorial()
	{
		int num = 10000000;
		EquipItemInfo equipItemInfo = localEquipSet[equipSetNo].item[0];
		if (equipItemInfo.tableID == num)
		{
			return;
		}
		EquipItemInfo equipItemInfo2 = localEquipSet[equipSetNo].item[1];
		if (equipItemInfo2 != null && equipItemInfo2.tableID == num)
		{
			SwapWeapon(0, 1);
			RefreshUI();
			return;
		}
		EquipItemInfo equipItemInfo3 = localEquipSet[equipSetNo].item[2];
		if (equipItemInfo3 != null && equipItemInfo3.tableID == num)
		{
			SwapWeapon(0, 2);
			RefreshUI();
			return;
		}
		EquipItemInfo weaponFromInventory = GetWeaponFromInventory(num);
		if (weaponFromInventory != null)
		{
			if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
			{
				MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(weaponFromInventory);
			}
			localEquipSet[equipSetNo].item[0] = weaponFromInventory;
			MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(localEquipSet[equipSetNo], equipSetNo, 0);
			RefreshUI();
		}
	}

	private void SwapWeapon(int swapIndex, int nowIndex)
	{
		EquipItemInfo equipItemInfo = localEquipSet[equipSetNo].item[nowIndex];
		localEquipSet[equipSetNo].item[nowIndex] = localEquipSet[equipSetNo].item[swapIndex];
		localEquipSet[equipSetNo].item[swapIndex] = equipItemInfo;
		MonoBehaviourSingleton<StatusManager>.I.SwapWeapon(swapIndex, nowIndex);
	}

	private EquipItemInfo GetWeaponFromInventory(int weaponId)
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ALL_WEAPON;
		MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
		EquipItemInfo[] array = MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[];
		int num = array.Length;
		EquipItemInfo equipItemInfo = null;
		for (int i = 0; i < num; i++)
		{
			if (array[i].tableID == weaponId)
			{
				return equipItemInfo = array[i];
			}
		}
		return null;
	}

	private void OnQuery_ABILITY()
	{
		tweenTarget = null;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		GameSection.SetEventData(new object[3]
		{
			localEquipSet[equipSetNo],
			MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(equipSetNo),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(userStatus.atk, userStatus.def, userStatus.hp, null)
		});
	}

	private void OnQuery_STATUS()
	{
		tweenTarget = null;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		GameSection.SetEventData(new object[3]
		{
			localEquipSet[equipSetNo],
			MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(equipSetNo),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(userStatus.atk, userStatus.def, userStatus.hp, null)
		});
	}

	private void OnQuery_CHARA_MAKE()
	{
		GameSection.SetEventData(new object[2]
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus
		});
	}

	private void OnQuery_CHANGE_SET_NAME()
	{
		GameSection.SetEventData(new object[2]
		{
			equipSetNo,
			localEquipSet[equipSetNo]
		});
	}

	private void OnQuery_EQUIP_SET_COPY()
	{
		equipSetCopyMode = EQUIP_SET_COPY_MODE.COPY;
		equipSetCopyNo = equipSetNo;
		equipSetCopyForm = CopyEquipSetInfo(localEquipSet[equipSetNo], equipSetNo);
		DrawEquipSetCopyModeButton();
	}

	private void OnQuery_EQUIP_SET_PASTE()
	{
		GameSection.ChangeEvent("EQUIP_SET_PASTE_CONFIRM");
	}

	private void ResetEquipSetCopy()
	{
		equipSetCopyMode = EQUIP_SET_COPY_MODE.NONE;
		equipSetCopyNo = 0;
	}

	protected void OnQuery_StatusTopEquipSetPasteConfirm_YES()
	{
		GameSection.SetEventData(null);
		GameSection.StayEvent();
		equipSetCopyForm.no = equipSetNo;
		MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetCopy(equipSetCopyForm, delegate(bool is_success)
		{
			if (is_success)
			{
				if (MonoBehaviourSingleton<StatusManager>.IsValid())
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateLocalEquipSet(equipSetNo);
				}
				RefreshUI();
				ResetEquipSetCopy();
				DrawEquipSetCopyModeButton();
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_EQUIP_SET_DELETE()
	{
		ResetEquipSetCopy();
		DrawEquipSetCopyModeButton();
	}

	private void OnCloseDialog_StatusChangedEquipSetName()
	{
		SetLabelText((Enum)UI.LBL_SET_NAME, localEquipSet[equipSetNo].name);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_EQUIP_GROW | NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SMITH_BADGE | NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (NOTIFY_FLAG)0L)
		{
			localEquipSetUpdate();
			if (visualDetailEquip != null && visualDetailItemIndex != -1)
			{
				visualEquip.visualItem[visualDetailItemIndex] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(visualDetailEquip.uniqueID);
			}
		}
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (NOTIFY_FLAG)0L)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSet(localEquipSet[equipSetNo], equipSetNo);
		}
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO) != (NOTIFY_FLAG)0L)
		{
			ForceSettingEquipSetInfo();
			DrawEquipSetModel();
		}
		base.OnNotify(flags);
	}

	private void localEquipSetUpdate()
	{
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			int j = 0;
			for (int num2 = localEquipSet[i].item.Length; j < num2; j++)
			{
				EquipItemInfo equipItemInfo = localEquipSet[i].item[j];
				if (equipItemInfo != null)
				{
					localEquipSet[i].item[j] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipItemInfo.uniqueID);
				}
			}
		}
		if (MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSets(localEquipSet);
		}
	}

	private InventoryManager.INVENTORY_TYPE GetInventoryType(int index)
	{
		switch (index)
		{
		default:
		{
			EquipItemInfo equipItemInfo = localEquipSet[equipSetNo].item[index];
			if (equipItemInfo != null)
			{
				int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemInfo.tableData.type);
				return (InventoryManager.INVENTORY_TYPE)(equipmentTypeIndex + 1);
			}
			return InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD;
		}
		case 3:
			return InventoryManager.INVENTORY_TYPE.ARMOR;
		case 4:
			return InventoryManager.INVENTORY_TYPE.HELM;
		case 5:
			return InventoryManager.INVENTORY_TYPE.ARM;
		case 6:
			return InventoryManager.INVENTORY_TYPE.LEG;
		}
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemInfo equipItemInfo = localEquipSet[equipSetNo].item[num];
		if (equipItemInfo == null)
		{
			GameSection.StopEvent();
			return;
		}
		detailEquipSetNo = equipSetNo;
		StatusEquip.LocalEquipSetData localEquipSetData = new StatusEquip.LocalEquipSetData(detailEquipSetNo, num, localEquipSet[detailEquipSetNo]);
		GameSection.SetEventData(new object[4]
		{
			ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
			equipItemInfo,
			equipSetNo,
			localEquipSetData
		});
	}

	private void OnQuery_SKILL_ICON_BUTTON()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemInfo equipItemInfo = localEquipSet[equipSetNo].item[num];
		if (equipItemInfo == null)
		{
			GameSection.StopEvent();
			return;
		}
		detailEquipSetNo = equipSetNo;
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
			equipItemInfo
		});
	}

	private void OnQuery_VISUAL_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		if (visualEquip.visualItem.Length > num && visualEquip.visualItem[num] != null)
		{
			visualDetailEquip = visualEquip.visualItem[num];
			visualDetailItemIndex = num;
			GameSection.ChangeEvent("DETAIL");
			GameSection.SetEventData(new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
				visualEquip.visualItem[num],
				equipSetNo
			});
		}
	}

	private void ResetDetailTarget()
	{
		detailEquipSetNo = -1;
		visualDetailEquip = null;
		visualDetailItemIndex = -1;
	}

	public static InventoryManager.INVENTORY_TYPE GetInventoryType(EquipSetInfo setInfo, int index)
	{
		switch (index)
		{
		default:
		{
			EquipItemInfo equipItemInfo = setInfo.item[index];
			if (equipItemInfo != null)
			{
				int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemInfo.tableData.type);
				return (InventoryManager.INVENTORY_TYPE)(equipmentTypeIndex + 1);
			}
			return InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD;
		}
		case 3:
			return InventoryManager.INVENTORY_TYPE.ARMOR;
		case 4:
			return InventoryManager.INVENTORY_TYPE.HELM;
		case 5:
			return InventoryManager.INVENTORY_TYPE.ARM;
		case 6:
			return InventoryManager.INVENTORY_TYPE.LEG;
		}
	}

	private void OnQuery_EQUIP_SET_LIST()
	{
		GameSection.SetEventData(equipSetNo);
	}

	private void OnCloseDialog_StatusEquipSetList()
	{
		object eventData = GameSection.GetEventData();
		if (eventData == null)
		{
			RefreshUI();
			return;
		}
		int num = (int)GameSection.GetEventData();
		if (num != equipSetNo)
		{
			equipSetNo = num;
			SetCenter(GetCtrl(UI.GRD_DRUM), equipSetNo, is_instant: true);
		}
	}
}
