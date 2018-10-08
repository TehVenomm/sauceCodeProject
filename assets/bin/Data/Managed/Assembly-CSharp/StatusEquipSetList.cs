using System;
using UnityEngine;

public class StatusEquipSetList : SkillInfoBase
{
	public enum UI
	{
		GRD_SET_LIST,
		LBL_SET_NAME,
		BTN_EQUIP_SET_NAME,
		LBL_SET_NO,
		LBL_HP,
		LBL_ATK,
		LBL_DEF,
		SPR_WEAPON_ICON_0,
		SPR_WEAPON_ICON_1,
		SPR_WEAPON_ICON_2,
		BTN_EQUIP_SET_COPY,
		BTN_EQUIP_SET_PASTE,
		BTN_EQUIP_SET_DELETE
	}

	private enum EQUIP_SET_COPY_MODE
	{
		NONE,
		COPY
	}

	private int equipSetMax;

	private int equipSetCopyNo;

	private int equipSetPastedNo;

	private EquipSetInfo[] localEquipSet;

	private EQUIP_SET_COPY_MODE equipSetCopyMode;

	private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;

	internal string[] WEAPON_TYPE_ICON_SPRITE_NAME = new string[6]
	{
		"ItemIconKind_Sword",
		"ItemIconKind_Brade",
		"ItemIconKind_Lance",
		string.Empty,
		"ItemIconKind_Edge",
		"ItemIconKind_Allow"
	};

	public override void Initialize()
	{
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			equipSetMax = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
			localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetDynamicList((Enum)UI.GRD_SET_LIST, "StatusEquipSetListItem", equipSetMax, false, null, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override int GetCurrentEquipSetNo()
	{
		if (equipSetCopyMode == EQUIP_SET_COPY_MODE.COPY)
		{
			return equipSetCopyNo;
		}
		return base.GetCurrentEquipSetNo();
	}

	private void SetEquipSetInfo(Transform t, int setNo)
	{
		EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(setNo);
		SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
		SetLabelText(t, UI.LBL_SET_NAME, localEquipSet[setNo].name);
		SetLabelText(t, UI.LBL_SET_NO, (setNo + 1).ToString());
		SetLabelText(t, UI.LBL_HP, finalStatus.hp.ToString());
		SetLabelText(t, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(t, UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
		EQUIPMENT_TYPE[] weaponTypes = localEquipSet[setNo].GetWeaponTypes();
		SetSprite(t, UI.SPR_WEAPON_ICON_0, GetWeaponIconSpriteName(weaponTypes[0]));
		SetSprite(t, UI.SPR_WEAPON_ICON_1, GetWeaponIconSpriteName(weaponTypes[1]));
		SetSprite(t, UI.SPR_WEAPON_ICON_2, GetWeaponIconSpriteName(weaponTypes[2]));
		DrawEquipSetCopyModeButton(t, setNo);
		SetEvent(t, "CHANGE_SET", setNo);
		SetEvent(t, UI.BTN_EQUIP_SET_NAME, "CHANGE_SET_NAME", setNo);
		SetEvent(t, UI.BTN_EQUIP_SET_COPY, "EQUIP_SET_COPY", setNo);
		SetEvent(t, UI.BTN_EQUIP_SET_PASTE, "EQUIP_SET_PASTE", setNo);
	}

	private string GetWeaponIconSpriteName(EQUIPMENT_TYPE eqType)
	{
		if (eqType > EQUIPMENT_TYPE.ARROW)
		{
			return string.Empty;
		}
		return WEAPON_TYPE_ICON_SPRITE_NAME[(int)eqType];
	}

	private void DrawEquipSetCopyModeButton(Transform t, int setNo)
	{
		bool flag = setNo == equipSetCopyNo;
		bool flag2 = equipSetCopyMode == EQUIP_SET_COPY_MODE.COPY;
		SetActive(t, UI.BTN_EQUIP_SET_COPY, !flag2);
		SetActive(t, UI.BTN_EQUIP_SET_PASTE, flag2 && !flag);
		SetActive(t, UI.BTN_EQUIP_SET_DELETE, flag2 && flag);
	}

	private void ResetEquipSetCopy()
	{
		equipSetCopyMode = EQUIP_SET_COPY_MODE.NONE;
		equipSetCopyNo = 0;
		equipSetPastedNo = 0;
	}

	private void OnQuery_CHANGE_SET()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(num);
	}

	private void OnQuery_CHANGE_SET_NAME()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[2]
		{
			num,
			localEquipSet[num]
		});
	}

	private void OnQuery_EQUIP_SET_COPY()
	{
		int num = (int)GameSection.GetEventData();
		equipSetCopyMode = EQUIP_SET_COPY_MODE.COPY;
		equipSetCopyNo = num;
		equipSetCopyForm = CopyEquipSetInfo(localEquipSet[num], num);
		RefreshUI();
	}

	private void OnQuery_EQUIP_SET_DELETE()
	{
		ResetEquipSetCopy();
		RefreshUI();
	}

	private void OnQuery_EQUIP_SET_PASTE()
	{
		equipSetPastedNo = (int)GameSection.GetEventData();
		GameSection.ChangeEvent("EQUIP_SET_PASTE_CONFIRM", null);
	}

	private void OnQuery_StatusTopEquipSetPasteConfirm_YES()
	{
		GameSection.SetEventData(null);
		GameSection.StayEvent();
		equipSetCopyForm.no = equipSetPastedNo;
		MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetCopy(equipSetCopyForm, delegate(bool is_success)
		{
			if (is_success)
			{
				if (MonoBehaviourSingleton<StatusManager>.IsValid())
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateLocalEquipSet(equipSetPastedNo);
				}
				ResetEquipSetCopy();
				RefreshUI();
			}
			GameSection.ResumeEvent(is_success, null, false);
		});
	}

	private void OnCloseDialog_StatusChangedEquipSetName()
	{
		RefreshUI();
	}
}
