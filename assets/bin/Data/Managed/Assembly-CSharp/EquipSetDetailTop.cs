using System;
using UnityEngine;

public class EquipSetDetailTop : SkillInfoBase
{
	public enum UI
	{
		TGL_WINDOW_TITLE,
		TBL_SKILL_LIST,
		BTN_DETACH,
		BTN_ATTACH,
		BTN_GROW,
		SPR_EQUIP_INDEX_ICON,
		OBJ_ICON_ROOT,
		LBL_EQUIP_NAME,
		LBL_EQUIP_NOW_LV,
		LBL_EQUIP_MAX_LV,
		GRD_ATTACH_SKILL,
		TBL_SPACE,
		OBJ_SPACE,
		TGL_ACTIVE_OBJ,
		LBL_NAME,
		LBL_NAME_NOT_ENABLE_TYPE,
		LBL_NOW_LV,
		LBL_MAX_LV,
		LBL_EX_LV,
		TEX_SKILL_ICON,
		SPR_ENABLE_WEAPON_TYPE
	}

	protected EquipItemAndSkillData[] equipAndSkill;

	private bool lookOnly;

	private int sex = -1;

	protected int selectSkillIndex = -1;

	protected int selectEquipIndex = -1;

	private ItemDetailEquip.CURRENT_SECTION callSection;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		callSection = (ItemDetailEquip.CURRENT_SECTION)(int)array[0];
		equipAndSkill = (array[1] as EquipItemAndSkillData[]);
		lookOnly = (bool)array[2];
		sex = (int)array[3];
		int num = 3;
		int num2 = 4;
		EquipItemAndSkillData equipItemAndSkillData = equipAndSkill[num];
		equipAndSkill[num] = equipAndSkill[num2];
		equipAndSkill[num2] = equipItemAndSkillData;
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		SetToggle((Enum)UI.TGL_WINDOW_TITLE, lookOnly);
		SetActive((Enum)UI.BTN_ATTACH, !lookOnly);
		SetActive((Enum)UI.BTN_DETACH, !lookOnly);
		SetActive((Enum)UI.BTN_GROW, !lookOnly);
		int num = equipAndSkill.Length;
		Transform ctrl = GetCtrl(UI.TBL_SKILL_LIST);
		for (int i = 0; i < num; i++)
		{
			if (i < ctrl.get_childCount())
			{
				Transform val = ctrl.GetChild(i);
				Transform val2 = FindCtrl(val, UI.SPR_EQUIP_INDEX_ICON);
				Vector3 localPosition = val2.get_localPosition();
				localPosition.y = 0f;
				Transform obj = val;
				Vector3 localPosition2 = localPosition;
				val2.set_localPosition(localPosition2);
				obj.set_localPosition(localPosition2);
				Transform t = FindCtrl(val, UI.GRD_ATTACH_SKILL);
				t.DestroyChildren();
				base.GetComponent<UITable>(val, (Enum)UI.TBL_SPACE).Reposition();
			}
		}
		SetTable(UI.TBL_SKILL_LIST, "EquipSetDetailTopItem", num, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected virtual void OnQuery_SLOT()
	{
		int num = (int)GameSection.GetEventData();
		selectEquipIndex = num >> 16;
		selectSkillIndex = num % 65536;
		if (lookOnly || selectSkillIndex < 0 || selectEquipIndex < 0)
		{
			GameSection.StopEvent();
		}
		else if (MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount() <= 0)
		{
			GameSection.ChangeEvent("NOT_HAVE_SKILL_ITEM", null);
		}
		else
		{
			GameSection.ChangeEvent("ATTACH", null);
			EquipItemInfo equipItemInfo = equipAndSkill[selectEquipIndex].equipItemInfo;
			GameSection.SetEventData(new object[4]
			{
				callSection,
				equipItemInfo.GetSkillItem(selectSkillIndex),
				equipItemInfo,
				selectSkillIndex
			});
		}
	}

	private void OnQuery_SLOT_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		selectEquipIndex = num >> 16;
		selectSkillIndex = num % 65536;
		if (lookOnly || selectSkillIndex < 0 || selectEquipIndex < 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			SkillItemInfo itemData = equipAndSkill[selectEquipIndex].skillSlotUIData[selectSkillIndex].itemData;
			EquipItemInfo equipItemInfo = equipAndSkill[selectEquipIndex].equipItemInfo;
			if (itemData == null)
			{
				GameSection.StopEvent();
			}
			else
			{
				SkillItemSortData skillItemSortData = new SkillItemSortData();
				skillItemSortData.SetItem(itemData);
				GameSection.SetEventData(new object[4]
				{
					ItemDetailEquip.CURRENT_SECTION.STATUS_SKILL_LIST,
					skillItemSortData,
					equipItemInfo,
					selectSkillIndex
				});
			}
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & (NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE)) != (NOTIFY_FLAG)0L)
		{
			Array.ForEach(equipAndSkill, delegate(EquipItemAndSkillData data)
			{
				if (data != null && data.equipItemInfo != null)
				{
					EquipItemInfo equipItem = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(data.equipItemInfo.uniqueID);
					data.skillSlotUIData = GetSkillSlotData(equipItem);
				}
			});
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
	}
}
