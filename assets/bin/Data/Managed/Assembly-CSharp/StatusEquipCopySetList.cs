using System;
using UnityEngine;

public class StatusEquipCopySetList : SkillInfoBase
{
	public enum UI
	{
		SCR_SET_LIST,
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
		SPR_ELEMENT_ICON_0,
		SPR_ELEMENT_ICON_1,
		SPR_ELEMENT_ICON_2,
		BTN_EQUIP_SET_COPY,
		BTN_EQUIP_SET_PASTE,
		BTN_EQUIP_SET_DELETE,
		SPR_SELECT_FRAME
	}

	private enum EQUIP_SET_COPY_MODE
	{
		NONE,
		COPY
	}

	private int equipSetNo;

	private int equipSetMax;

	private int copyEquipSetNo;

	private EquipSetInfo[] localEquipSet;

	private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;

	private bool isInitializedScroll;

	internal string[] WEAPON_TYPE_ICON_SPRITE_NAME = new string[6]
	{
		"ItemIconKind_Sword",
		"ItemIconKind_Brade",
		"ItemIconKind_Lance",
		string.Empty,
		"ItemIconKind_Edge",
		"ItemIconKind_Allow"
	};

	internal string[] ELEMENT_ICON_NAME = new string[7]
	{
		"IconElementFire",
		"IconElementWater",
		"IconElementThunder",
		"IconElementSoil",
		"IconElementLight",
		"IconElementDark",
		string.Empty
	};

	public override void Initialize()
	{
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			equipSetMax = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
			localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSets();
		}
		equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		SetDynamicList((Enum)UI.GRD_SET_LIST, "StatusEquipSetListItem", equipSetMax, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool isRecycle)
		{
			SetEquipSetInfo(t, i);
		});
		if (!isInitializedScroll)
		{
			int num = Mathf.Max(0, equipSetNo - 1);
			UIGrid component = base.GetComponent<UIGrid>((Enum)UI.GRD_SET_LIST);
			if (component != null)
			{
				MoveRelativeScrollView((Enum)UI.SCR_SET_LIST, Vector3.get_up() * component.cellHeight * (float)num);
			}
			isInitializedScroll = true;
		}
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
		ELEMENT_TYPE[] weaponElementTypes = localEquipSet[setNo].GetWeaponElementTypes();
		SetSprite(t, UI.SPR_ELEMENT_ICON_0, GetWeaponElementIconSpriteName(weaponElementTypes[0]));
		SetSprite(t, UI.SPR_ELEMENT_ICON_1, GetWeaponElementIconSpriteName(weaponElementTypes[1]));
		SetSprite(t, UI.SPR_ELEMENT_ICON_2, GetWeaponElementIconSpriteName(weaponElementTypes[2]));
		SetEvent(t, "EQUIP_SET_COPY", setNo);
		SetEvent(t, UI.BTN_EQUIP_SET_COPY, "EQUIP_SET_COPY", setNo);
		SetActive(t, UI.BTN_EQUIP_SET_NAME, is_visible: false);
		SetActive(t, UI.BTN_EQUIP_SET_COPY, is_visible: true);
		SetActive(t, UI.BTN_EQUIP_SET_DELETE, is_visible: false);
		SetActive(t, UI.BTN_EQUIP_SET_PASTE, is_visible: false);
		SetActive(t, UI.SPR_SELECT_FRAME, setNo == equipSetNo);
	}

	private string GetWeaponIconSpriteName(EQUIPMENT_TYPE eqType)
	{
		if (eqType > EQUIPMENT_TYPE.ARROW)
		{
			return string.Empty;
		}
		return WEAPON_TYPE_ICON_SPRITE_NAME[(int)eqType];
	}

	private string GetWeaponElementIconSpriteName(ELEMENT_TYPE elemType)
	{
		if (elemType > ELEMENT_TYPE.MAX)
		{
			return string.Empty;
		}
		return ELEMENT_ICON_NAME[(int)elemType];
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
		copyEquipSetNo = (int)GameSection.GetEventData();
		if (MonoBehaviourSingleton<StatusManager>.I.CopyEquipSetCheck(copyEquipSetNo))
		{
			if (MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet().order > 0)
			{
				GameSection.ChangeEvent("EQUIP_COPY_CONFIRM", new object[1]
				{
					copyEquipSetNo + 1
				});
			}
			else
			{
				TO_EQUIP_TOP();
			}
		}
		else if (MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet().order > 0)
		{
			GameSection.ChangeEvent("EQUIPING_ORDER_COPY_CONFIRM", new object[1]
			{
				copyEquipSetNo + 1
			});
		}
		else
		{
			GameSection.ChangeEvent("EQUIPING_COPY_CONFIRM", new object[1]
			{
				copyEquipSetNo + 1
			});
		}
	}

	private void OnQuery_StatusOrderEquipCopyConfirm_YES()
	{
		OrderEquipSetCopy();
	}

	private void OnQuery_StatusEquipingCopyConfirm_YES()
	{
		EquipSetCopy();
	}

	private void OnQuery_StatusOrderEquipingCopyConfirm_YES()
	{
		OrderEquipSetCopy();
	}

	protected virtual void TO_EQUIP_TOP()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("UniqueStatus", "UniqueStatusTop");
		EquipSetCopy();
	}

	protected void OrderEquipSetCopy()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(MonoBehaviourSingleton<StatusManager>.I.GetCurrentUniqueEquipSetNo(), delegate
		{
			EquipSetCopy();
		});
	}

	protected virtual void EquipSetCopy()
	{
		if (!GameSceneEvent.IsStay())
		{
			GameSection.StayEvent();
		}
		MonoBehaviourSingleton<StatusManager>.I.CopyEquipSet(copyEquipSetNo, delegate(bool isSucces)
		{
			GameSection.ResumeEvent(isSucces);
		});
	}

	private void OnCloseDialog_StatusChangedEquipSetName()
	{
		RefreshUI();
	}
}
