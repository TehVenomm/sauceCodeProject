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
		callSection = (ItemDetailEquip.CURRENT_SECTION)array[0];
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

	public override void UpdateUI()
	{
		SetToggle(UI.TGL_WINDOW_TITLE, lookOnly);
		SetActive(UI.BTN_ATTACH, !lookOnly);
		SetActive(UI.BTN_DETACH, !lookOnly);
		SetActive(UI.BTN_GROW, !lookOnly);
		int num = equipAndSkill.Length;
		Transform ctrl = GetCtrl(UI.TBL_SKILL_LIST);
		for (int j = 0; j < num; j++)
		{
			if (j < ctrl.childCount)
			{
				Transform child = ctrl.GetChild(j);
				Transform transform = FindCtrl(child, UI.SPR_EQUIP_INDEX_ICON);
				Vector3 localPosition = transform.localPosition;
				localPosition.y = 0f;
				Vector3 vector3 = child.localPosition = (transform.localPosition = localPosition);
				FindCtrl(child, UI.GRD_ATTACH_SKILL).DestroyChildren();
				GetComponent<UITable>(child, UI.TBL_SPACE).Reposition();
			}
		}
		SetTable(UI.TBL_SKILL_LIST, "EquipSetDetailTopItem", num, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			EquipSetDetailTop equipSetDetailTop = this;
			EquipItemInfo item = equipAndSkill[i].equipItemInfo;
			if (item == null || item.GetMaxSlot() == 0)
			{
				SetActive(t, is_visible: false);
			}
			else
			{
				ItemIcon.CreateEquipItemIconByEquipItemInfo(item, sex, FindCtrl(t, UI.OBJ_ICON_ROOT)).SetEnableCollider(is_enable: false);
				SetEquipIndexIcon(t, UI.SPR_EQUIP_INDEX_ICON, i);
				SetLabelText(t, UI.LBL_EQUIP_NAME, item.tableData.name);
				SetLabelText(t, UI.LBL_EQUIP_NOW_LV, item.level.ToString());
				SetLabelText(t, UI.LBL_EQUIP_MAX_LV, item.tableData.maxLv.ToString());
				SkillSlotUIData[] slotData = equipAndSkill[i].skillSlotUIData;
				SetGrid(t, UI.GRD_ATTACH_SKILL, "EquipSetDetailTopItem2", item.GetMaxSlot(), reset: true, delegate(int i2, Transform t2, bool is_recycle2)
				{
					int num3 = (i << 16) + i2;
					SkillItemInfo skillItemInfo = slotData[i2].itemData;
					bool flag = skillItemInfo != null && slotData[i2].slotData.skill_id != 0 && skillItemInfo.tableData.type == slotData[i2].slotData.slotType;
					equipSetDetailTop.SetSkillIcon(t2, UI.TEX_SKILL_ICON, slotData[i2].slotData.slotType, flag, is_button_icon: false);
					if (!flag)
					{
						skillItemInfo = null;
					}
					equipSetDetailTop.SetToggle(t2, UI.TGL_ACTIVE_OBJ, skillItemInfo != null);
					equipSetDetailTop.SetActive(t2, UI.LBL_NAME, is_visible: true);
					equipSetDetailTop.SetActive(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, is_visible: false);
					equipSetDetailTop.SetEvent(t2, flag ? "SLOT_DETAIL" : "SLOT", num3);
					equipSetDetailTop.SetLongTouch(t2, "SLOT_DETAIL", num3);
					if (skillItemInfo == null)
					{
						equipSetDetailTop.SetLabelText(t2, UI.LBL_NAME, equipSetDetailTop.sectionData.GetText("EMPTY_SLOT"));
						equipSetDetailTop.SetActive(t2, UI.SPR_ENABLE_WEAPON_TYPE, is_visible: false);
					}
					else
					{
						SkillItemTable.SkillItemData tableData = skillItemInfo.tableData;
						equipSetDetailTop.SetLabelText(t2, UI.LBL_NAME, tableData.name);
						equipSetDetailTop.SetLabelText(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, tableData.name);
						equipSetDetailTop.SetLabelText(t2, UI.LBL_NOW_LV, skillItemInfo.level.ToString());
						equipSetDetailTop.SetLabelText(t2, UI.LBL_MAX_LV, skillItemInfo.tableData.GetMaxLv(skillItemInfo.exceedCnt).ToString());
						bool flag2 = skillItemInfo.IsExceeded();
						equipSetDetailTop.SetActive(t2, UI.LBL_EX_LV, flag2);
						if (flag2)
						{
							equipSetDetailTop.SetSupportEncoding(t2, UI.LBL_EX_LV, isEnable: true);
							equipSetDetailTop.SetLabelText(t2, UI.LBL_EX_LV, UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 9u, skillItemInfo.exceedCnt), ExceedSkillItemTable.color));
						}
						EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
						equipSetDetailTop.SetActive(t2, UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.HasValue);
						if (enableEquipType.HasValue)
						{
							bool flag3 = enableEquipType.Value == item.tableData.type;
							equipSetDetailTop.SetSkillEquipIconKind(t2, UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.Value, flag3);
							equipSetDetailTop.SetActive(t2, UI.LBL_NAME, flag3);
							equipSetDetailTop.SetActive(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, !flag3);
						}
					}
				});
			}
			GetComponent<UITable>(t, UI.TBL_SPACE).Reposition();
			float y = t.localPosition.y;
			float num2 = FindCtrl(t, UI.OBJ_SPACE).localPosition.y + FindCtrl(t, UI.TBL_SPACE).localPosition.y;
			Vector3 localPosition2 = FindCtrl(t, UI.SPR_EQUIP_INDEX_ICON).localPosition;
			localPosition2.y = (num2 - y) * 0.5f;
			FindCtrl(t, UI.SPR_EQUIP_INDEX_ICON).localPosition = localPosition2;
		});
	}

	protected virtual void OnQuery_SLOT()
	{
		int num = (int)GameSection.GetEventData();
		selectEquipIndex = num >> 16;
		selectSkillIndex = num % 65536;
		if (lookOnly || selectSkillIndex < 0 || selectEquipIndex < 0)
		{
			GameSection.StopEvent();
			return;
		}
		if (MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount() <= 0)
		{
			GameSection.ChangeEvent("NOT_HAVE_SKILL_ITEM");
			return;
		}
		GameSection.ChangeEvent("ATTACH");
		EquipItemInfo equipItemInfo = equipAndSkill[selectEquipIndex].equipItemInfo;
		SkillItemInfo skillItemInfo = equipItemInfo.GetSkillItem(selectSkillIndex);
		if (StatusManager.IsUnique())
		{
			skillItemInfo = equipItemInfo.GetUniqueSkillItem(selectSkillIndex);
		}
		GameSection.SetEventData(new object[4]
		{
			callSection,
			skillItemInfo,
			equipItemInfo,
			selectSkillIndex
		});
	}

	private void OnQuery_SLOT_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		selectEquipIndex = num >> 16;
		selectSkillIndex = num % 65536;
		if (lookOnly || selectSkillIndex < 0 || selectEquipIndex < 0)
		{
			GameSection.StopEvent();
			return;
		}
		SkillItemInfo itemData = equipAndSkill[selectEquipIndex].skillSlotUIData[selectSkillIndex].itemData;
		EquipItemInfo equipItemInfo = equipAndSkill[selectEquipIndex].equipItemInfo;
		if (itemData == null)
		{
			GameSection.StopEvent();
			return;
		}
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
