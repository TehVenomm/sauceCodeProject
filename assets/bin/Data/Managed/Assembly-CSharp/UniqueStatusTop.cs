using Network;
using System;
using System.Collections;
using UnityEngine;

public class UniqueStatusTop : SkillInfoBase
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
		SPR_PARAMETER_ACTIVE,
		BTN_PARAMETER_INACTIVE,
		OBJ_EQUIP_SET_SELECT,
		OBJ_STUDIO_BUTTON_ROOT,
		OBJ_PARAMETER_BUTTON_ROOT,
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
		LBL_SET_NAME,
		SCR_DRUM,
		GRD_DRUM,
		LBL_EQUIP_NO,
		OBJ_BACK,
		BTN_STATUS,
		BTN_MAGI_REMOVE
	}

	public EquipSetInfo[] localEquipSet;

	public int equipSetNo;

	private int SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum();

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

	private UI? tweenTarget;

	private UICenterOnChild uiCenterOnChild;

	private int showHelm;

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
		tweenTarget = null;
		SetActive((Enum)UI.OBJ_WITH_MONSTER_ROOT, is_visible: true);
		SetActive((Enum)UI.OBJ_WITHOUT_MONSTER_ROOT, is_visible: false);
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
			localEquipSet[changeEquipData.setNo].item[changeEquipData.index] = changeEquipData.item;
			MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipItem(localEquipSet[changeEquipData.setNo], changeEquipData.setNo, changeEquipData.index);
		}
		else if (eventData is StatusEquip.ChangeEquipData[])
		{
			StatusEquip.ChangeEquipData[] array = eventData as StatusEquip.ChangeEquipData[];
			for (int j = 0; j < array.Length; j++)
			{
				if ((array[j].index != 0 || array[j].item != null) && (array[j].index != 3 || array[j].item != null))
				{
					localEquipSet[array[j].setNo].item[array[j].index] = array[j].item;
					MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipItem(localEquipSet[array[j].setNo], array[j].setNo, array[j].index);
				}
			}
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
		EquipItemInfo equipItemInfo = localEquipSet[equipSetNo].item[3];
		playerLoadInfo.SetupLoadInfo(localEquipSet[equipSetNo], 0uL, 0uL, 0uL, 0uL, 0uL, localEquipSet[equipSetNo].showHelm == 1);
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		int anim_id = 0;
		if (playerLoadInfo.bodyModelID <= 0)
		{
			playerLoadInfo.SetEquipBody(userStatus.sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerArmEquipItemID);
		}
		if (localEquipSet[equipSetNo].item[0] == null)
		{
			anim_id = 98;
		}
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.LoadPlayer(playerLoadInfo, anim_id);
		}
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquip(delegate
		{
			base.Exit();
		});
	}

	public void SettingEquipSetInfo()
	{
		if (localEquipSet == null)
		{
			localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
			equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
			MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(equipSetNo);
			showHelm = localEquipSet[equipSetNo].showHelm;
		}
	}

	public void ForceSettingEquipSetInfo()
	{
		localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum();
		DrawEquipSetModel();
	}

	public override void UpdateUI()
	{
		int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
		SetBadge((Enum)UI.BTN_STUDIO, badgeTotalNum, 1, 8, -8, is_scale_normalize: true);
		DrawEquipModeButton();
		int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		EquipSetInfo equipSetInfo = localEquipSet[equipSetNo];
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
		DrawEquipSetModel();
		UI? uI = tweenTarget;
		if (uI.HasValue)
		{
			ResetTween((Enum)(object)tweenTarget);
			PlayTween((Enum)(object)tweenTarget, forward: true, null, is_input_block: false);
		}
		if (localEquipSet[equipSetNo].showHelm != showHelm)
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
			showHelm = localEquipSet[equipSetNo].showHelm;
		}
		SetToggleButton((Enum)UI.TGL_VISIBLE_HELM_BUTTON, showHelm == 1, (Action<bool>)delegate(bool is_active)
		{
			localEquipSet[equipSetNo].showHelm = (is_active ? 1 : 0);
			showHelm = localEquipSet[equipSetNo].showHelm;
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
		SetDynamicList((Enum)UI.GRD_DRUM, "equipno", SET_NO_MAX, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool isRecycle)
		{
			SetLabelText(t, UI.LBL_EQUIP_NO, (i + 1).ToString());
		});
		SetButtonEnabled((Enum)UI.BTN_MAGI_REMOVE, MonoBehaviourSingleton<StatusManager>.I.checkEquipMagi(equipSetNo));
		base.UpdateUI();
	}

	public void DrawEquipSetModel()
	{
		EquipSetCalculator uniqueEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSetCalculator(equipSetNo);
		SimpleStatus finalStatus = uniqueEquipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
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
		SetActive((Enum)UI.OBJ_EQUIP_ROOT, is_visible: true);
		SetActive((Enum)UI.OBJ_EQUIP_SET_SELECT, is_visible: true);
		SetActive((Enum)UI.SPR_PARAMETER_ACTIVE, is_visible: true);
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

	private void OnQuery_MODE_EQUIP()
	{
		ResetDetailTarget();
		tweenTarget = UI.OBJ_EQUIP_ROT_ROOT;
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetViewMode(StatusStageManager.VIEW_MODE.EQUIP);
		}
		ResetTween((Enum)UI.OBJ_PARAMETER_BUTTON_ROOT, 0);
		PlayTween((Enum)UI.OBJ_PARAMETER_BUTTON_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		RefreshUI();
	}

	private void OnQuery_TO_EVENT()
	{
		ToSeriesArena();
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
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquip(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
			base.OnQuery_MAIN_MENU_QUEST();
		});
	}

	private void CheckEquipChange()
	{
		tweenTarget = null;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquip(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_SKILL_LIST()
	{
		tweenTarget = null;
		GameSection.SetEventData(new object[4]
		{
			ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
			GetLocalEquipSetAttachSkillListData(equipSetNo),
			false,
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex
		});
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

	private void OnQuery_CHANGE_SET_NAME()
	{
		GameSection.SetEventData(new object[2]
		{
			equipSetNo,
			localEquipSet[equipSetNo]
		});
	}

	private void OnCloseDialog_UniqueStatusChangedEquipSetName()
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
		}
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (NOTIFY_FLAG)0L)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(localEquipSet);
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
		if (localEquipSet == null)
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
		}
		if (MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(localEquipSet);
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

	private void OnQuery_MAGI_REMOVE()
	{
		GameSection.SetEventData(new object[1]
		{
			equipSetNo + 1
		});
	}

	private void OnQuery_StatusMagiAllRemoveConfirm_YES()
	{
		this.StartCoroutine(sendMagiAllRemove());
	}

	protected IEnumerator sendMagiAllRemove()
	{
		bool wait = true;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(equipSetNo, delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		wait = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet(delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		wait = true;
		MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkillFromEvery(equipSetNo, delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		GameSection.ResumeEvent(is_resume: true);
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
}
