using System;
using System.Collections;
using UnityEngine;

public class QuestOffLineChangeEquipSet : QuestChangeEquipSet
{
	protected new enum UI
	{
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		SPR_COMMENT,
		LBL_COMMENT,
		OBJ_LAST_LOGIN,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_LEVEL,
		OBJ_LEVEL_ROOT,
		LBL_USER_ID,
		OBJ_USER_ID_ROOT,
		TEX_MODEL,
		BTN_FOLLOW,
		BTN_UNFOLLOW,
		OBJ_BLACKLIST_ROOT,
		BTN_BLACKLIST_IN,
		BTN_BLACKLIST_OUT,
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
		OBJ_EQUIP_ROOT,
		OBJ_EQUIP_SET_ROOT,
		OBJ_FRIEND_INFO_ROOT,
		OBJ_CHANGE_EQUIP_INFO_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_FOLLOW_ARROW_ROOT,
		SPR_FOLLOW_ARROW,
		SPR_FOLLOWER_ARROW,
		SPR_BLACKLIST_ICON,
		LBL_LEVEL_WEAPON_1,
		LBL_LEVEL_WEAPON_2,
		LBL_LEVEL_WEAPON_3,
		LBL_LEVEL_ARMOR,
		LBL_LEVEL_HELM,
		LBL_LEVEL_ARM,
		LBL_LEVEL_LEG,
		LBL_CHANGE_MODE,
		LBL_SET_NAME,
		OBJ_DEGREE_PLATE_ROOT,
		OBJ_SKILL_BUTTON_ROOT,
		OBJ_EQUIP_ROT_ROOT,
		BTN_EQUIP_SET_COPY,
		BTN_EQUIP_SET_PASTE,
		BTN_EQUIP_SET_DELETE
	}

	private enum EQUIP_SET_COPY_MODE
	{
		NONE,
		COPY
	}

	private bool showEquipMode = true;

	private EquipSetInfo[] localEquipSets;

	private EQUIP_SET_COPY_MODE equipSetCopyMode;

	private int equipSetCopyNo = -1;

	private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;

	protected override bool IsFriendInfo => false;

	public override void Initialize()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		isChangeEquip = true;
		isVisualMode = false;
		isSelfData = true;
		selfCharaEquipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		record = InitializePlayerRecord();
		localEquipSets = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		localEquipSet = localEquipSets[selfCharaEquipSetNo];
		transRoot = SetPrefab((Enum)UI.OBJ_EQUIP_SET_ROOT, "OfflineChangeEquipSetBase");
		this.StartCoroutine(DoInitialize());
	}

	public override void UpdateUI()
	{
		localEquipSet = localEquipSets[selfCharaEquipSetNo];
		UpdateEquipSetUI();
		OnUpdateFriendDetailUI();
		ResetTween((Enum)UI.OBJ_EQUIP_ROOT, 0);
		PlayTween((Enum)UI.OBJ_EQUIP_ROOT, true, (EventDelegate.Callback)delegate
		{
		}, false, 0);
		UpdateCopyModeButton();
	}

	protected override void UpdateEquipSkillButton(EquipItemInfo item, int i)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(icons_btn[i]);
		bool flag = item != null && item.tableID != 0;
		if (flag)
		{
			SetSkillIconButton(ctrl, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", item.tableData, GetSkillSlotData(item), "SKILL_ICON_BUTTON", i);
		}
		FindCtrl(ctrl, UI.OBJ_SKILL_BUTTON_ROOT).get_gameObject().SetActive(flag);
	}

	protected override void ReloadModel()
	{
		SetLabelText(transRoot, UI.LBL_SET_NAME, localEquipSet.name);
		ReloadPlayerModelByLocalEquipSet();
	}

	private IEnumerator ReloadModelByLocalEquipSetCoroutine()
	{
		while (UIModelRenderTexture.Get(FindCtrl(transRoot, UI.TEX_MODEL)).IsLoadingPlayer())
		{
			yield return (object)null;
		}
		ReloadPlayerModelByLocalEquipSet();
	}

	protected void ReloadPlayerModelByLocalEquipSet()
	{
		SetLabelText(transRoot, UI.LBL_SET_NAME, localEquipSet.name);
		record.playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, isVisualMode, selfCharaEquipSetNo);
		record.playerLoadInfo.SetupLoadInfo(localEquipSet, 0uL, 0uL, 0uL, 0uL, 0uL, localEquipSet.showHelm == 1);
		SetRenderPlayerModel(record.playerLoadInfo);
	}

	protected override void OnQuery_SKILL_LIST()
	{
		GameSection.SetEventData(new object[4]
		{
			ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
			GetLocalEquipSetAttachSkillListData(selfCharaEquipSetNo),
			false,
			record.charaInfo.sex
		});
	}

	protected override void OnQuery_ABILITY()
	{
		OnQueryAbilityBase();
	}

	protected override void OnQuery_STATUS()
	{
		OnQueryStatusBase();
	}

	private void OnQuery_SKILL_ICON_BUTTON()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemInfo equipItemInfo = localEquipSet.item[num];
		if (equipItemInfo == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
				equipItemInfo
			});
		}
	}

	protected override void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		if (isVisualMode)
		{
			GameSection.ChangeEvent("VISUAL_DETAIL", null);
			OnQuery_VISUAL_DETAIL();
		}
		else
		{
			StatusEquip.LocalEquipSetData localEquipSetData = new StatusEquip.LocalEquipSetData(selfCharaEquipSetNo, num, localEquipSet);
			object[] array = CreateSelfEventData(num);
			if (localEquipSet.item[num] == null)
			{
				MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(null);
				MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = StatusTop.GetInventoryType(localEquipSet, num);
				ItemDetailEquip.DetailEquipEventData event_data = new ItemDetailEquip.DetailEquipEventData(array, localEquipSetData);
				GameSection.ChangeEvent("CHANGE_EQUIP", event_data);
			}
			else
			{
				object[] array2 = new object[array.Length + 1];
				int i = 0;
				for (int num2 = array.Length; i < num2; i++)
				{
					array2[i] = array[i];
				}
				array2[1] = GetLocalEquipSetAttachSkillListData(selfCharaEquipSetNo)[num];
				array2[array2.Length - 1] = localEquipSetData;
				GameSection.SetEventData(array2);
			}
		}
	}

	private void OnQuery_CHANGE_SET_NAME()
	{
		GameSection.SetEventData(new object[2]
		{
			selfCharaEquipSetNo,
			localEquipSets[selfCharaEquipSetNo]
		});
	}

	private void OnCloseDialog_QuestAcceptChangedEquipSetName()
	{
		SetLabelText((Enum)UI.LBL_SET_NAME, localEquipSets[selfCharaEquipSetNo].name);
	}

	protected override void OnQuery_SECTION_BACK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(selfCharaEquipSetNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null, false);
		});
		MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(-1);
		base.OnQuery_SECTION_BACK();
	}

	private void OnQuery_EQUIP_SET_COPY()
	{
		CopyEquipSet();
	}

	private void CopyEquipSet()
	{
		equipSetCopyMode = EQUIP_SET_COPY_MODE.COPY;
		equipSetCopyNo = selfCharaEquipSetNo;
		equipSetCopyForm = CopyEquipSetInfo(localEquipSets[selfCharaEquipSetNo], selfCharaEquipSetNo);
		UpdateCopyModeButton();
	}

	private void OnQuery_EQUIP_SET_DELETE()
	{
		ResetEquipSetCopy();
		UpdateCopyModeButton();
	}

	private void UpdateCopyModeButton()
	{
		bool flag = selfCharaEquipSetNo == equipSetCopyNo;
		bool flag2 = equipSetCopyMode == EQUIP_SET_COPY_MODE.COPY;
		SetActive(transRoot, UI.BTN_EQUIP_SET_COPY, !flag2);
		SetActive(transRoot, UI.BTN_EQUIP_SET_PASTE, flag2 && !flag);
		SetActive(transRoot, UI.BTN_EQUIP_SET_DELETE, flag2 && flag);
	}

	protected void OnQuery_QuestAcceptEquipSetPasteConfirm_YES()
	{
		GameSection.SetEventData(null);
		GameSection.StayEvent();
		equipSetCopyForm.no = selfCharaEquipSetNo;
		MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetCopy(equipSetCopyForm, delegate(bool is_success)
		{
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			if (is_success)
			{
				if (MonoBehaviourSingleton<StatusManager>.IsValid())
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateLocalEquipSet(selfCharaEquipSetNo);
				}
				ResetEquipSetCopy();
				localEquipSetUpdate();
				localEquipSet = localEquipSets[selfCharaEquipSetNo];
				this.StartCoroutine(ReloadModelByLocalEquipSetCoroutine());
				RefreshUI();
			}
			GameSection.ResumeEvent(is_success, null, false);
		});
	}

	private void ResetEquipSetCopy()
	{
		equipSetCopyMode = EQUIP_SET_COPY_MODE.NONE;
		equipSetCopyNo = 0;
	}

	private void OnCloseDialog()
	{
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		object eventData = GameSection.GetEventData();
		bool flag = false;
		if (eventData is StatusEquip.ChangeEquipData)
		{
			StatusEquip.ChangeEquipData changeEquipData = eventData as StatusEquip.ChangeEquipData;
			flag = (changeEquipData.item != localEquipSets[changeEquipData.setNo].item[changeEquipData.index]);
			if (showEquipMode)
			{
				localEquipSets[changeEquipData.setNo].item[changeEquipData.index] = changeEquipData.item;
				MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(localEquipSets[changeEquipData.setNo], changeEquipData.setNo, changeEquipData.index);
			}
			else
			{
				StatusManager.LocalVisual localVisualEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalVisualEquip();
				int index = changeEquipData.index;
				ulong num = (localVisualEquip.visualItem[index] == null) ? 0 : localVisualEquip.visualItem[index].uniqueID;
				localVisualEquip.visualItem[index] = changeEquipData.item;
				ulong num2 = (localVisualEquip.visualItem[index] == null) ? 0 : localVisualEquip.visualItem[index].uniqueID;
			}
			if (flag)
			{
				localEquipSet = localEquipSets[changeEquipData.setNo];
			}
		}
		else if (eventData is StatusEquip.ChangeEquipData[])
		{
			StatusEquip.ChangeEquipData[] array = eventData as StatusEquip.ChangeEquipData[];
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i].index != 0 || array[i].item != null) && (array[i].index != 3 || array[i].item != null))
				{
					localEquipSets[array[i].setNo].item[array[i].index] = array[i].item;
					MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(localEquipSets[array[i].setNo], array[i].setNo, array[i].index);
					flag = true;
				}
			}
		}
		if (flag)
		{
			localEquipSetUpdate();
			RefreshUI();
			this.StartCoroutine(ReloadModelByLocalEquipSetCoroutine());
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (NOTIFY_FLAG)0L)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSet(localEquipSet, selfCharaEquipSetNo);
		}
		base.OnNotify(flags);
	}

	private void localEquipSetUpdate()
	{
		int i = 0;
		for (int num = localEquipSets.Length; i < num; i++)
		{
			int j = 0;
			for (int num2 = localEquipSets[i].item.Length; j < num2; j++)
			{
				EquipItemInfo equipItemInfo = localEquipSets[i].item[j];
				if (equipItemInfo != null)
				{
					localEquipSets[i].item[j] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipItemInfo.uniqueID);
				}
			}
		}
		if (MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSets(localEquipSets);
		}
	}

	private void OnQuery_AUTO_EQUIP()
	{
		int selfCharaEquipSetNo = base.selfCharaEquipSetNo;
		if (selfCharaEquipSetNo < localEquipSets.Length)
		{
			GameSection.SetEventData(new StatusEquip.LocalEquipSetData(selfCharaEquipSetNo, 0, localEquipSets[selfCharaEquipSetNo]));
		}
	}
}
