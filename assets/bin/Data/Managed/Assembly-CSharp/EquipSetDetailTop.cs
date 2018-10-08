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

	public override void UpdateUI()
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
		for (int j = 0; j < num; j++)
		{
			if (j < ctrl.get_childCount())
			{
				Transform val = ctrl.GetChild(j);
				Transform val2 = FindCtrl(val, UI.SPR_EQUIP_INDEX_ICON);
				Vector3 localPosition = val2.get_localPosition();
				localPosition.y = 0f;
				Transform obj = val;
				Vector3 localPosition2 = localPosition;
				val2.set_localPosition(localPosition2);
				obj.set_localPosition(localPosition2);
				Transform t3 = FindCtrl(val, UI.GRD_ATTACH_SKILL);
				t3.DestroyChildren();
				base.GetComponent<UITable>(val, (Enum)UI.TBL_SPACE).Reposition();
			}
		}
		SetTable(UI.TBL_SKILL_LIST, "EquipSetDetailTopItem", num, false, delegate(int i, Transform t, bool is_recycle)
		{
			//IL_0180: Unknown result type (might be due to invalid IL or missing references)
			//IL_0185: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
			EquipSetDetailTop equipSetDetailTop = this;
			EquipItemInfo item = equipAndSkill[i].equipItemInfo;
			if (item == null || item.GetMaxSlot() == 0)
			{
				SetActive(t, false);
			}
			else
			{
				EquipSetDetailTop equipSetDetailTop2 = this;
				ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(item, sex, FindCtrl(t, UI.OBJ_ICON_ROOT), null, -1, null, 0, false, -1, false, null, false, false);
				itemIcon.SetEnableCollider(false);
				SetEquipIndexIcon(t, UI.SPR_EQUIP_INDEX_ICON, i);
				SetLabelText(t, UI.LBL_EQUIP_NAME, item.tableData.name);
				SetLabelText(t, UI.LBL_EQUIP_NOW_LV, item.level.ToString());
				SetLabelText(t, UI.LBL_EQUIP_MAX_LV, item.tableData.maxLv.ToString());
				SkillSlotUIData[] slotData = equipAndSkill[i].skillSlotUIData;
				SetGrid(t, UI.GRD_ATTACH_SKILL, "EquipSetDetailTopItem2", item.GetMaxSlot(), true, delegate(int i2, Transform t2, bool is_recycle2)
				{
					//IL_0237: Unknown result type (might be due to invalid IL or missing references)
					int num3 = (i << 16) + i2;
					SkillItemInfo skillItemInfo = slotData[i2].itemData;
					bool flag = skillItemInfo != null && slotData[i2].slotData.skill_id != 0 && skillItemInfo.tableData.type == slotData[i2].slotData.slotType;
					equipSetDetailTop2.SetSkillIcon(t2, UI.TEX_SKILL_ICON, slotData[i2].slotData.slotType, flag, false);
					if (!flag)
					{
						skillItemInfo = null;
					}
					equipSetDetailTop2.SetToggle(t2, UI.TGL_ACTIVE_OBJ, skillItemInfo != null);
					equipSetDetailTop2.SetActive(t2, UI.LBL_NAME, true);
					equipSetDetailTop2.SetActive(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, false);
					equipSetDetailTop2.SetEvent(t2, (!flag) ? "SLOT" : "SLOT_DETAIL", num3);
					equipSetDetailTop2.SetLongTouch(t2, "SLOT_DETAIL", num3);
					if (skillItemInfo == null)
					{
						equipSetDetailTop2.SetLabelText(t2, UI.LBL_NAME, equipSetDetailTop2.sectionData.GetText("EMPTY_SLOT"));
						equipSetDetailTop2.SetActive(t2, UI.SPR_ENABLE_WEAPON_TYPE, false);
					}
					else
					{
						SkillItemTable.SkillItemData tableData = skillItemInfo.tableData;
						equipSetDetailTop2.SetLabelText(t2, UI.LBL_NAME, tableData.name);
						equipSetDetailTop2.SetLabelText(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, tableData.name);
						equipSetDetailTop2.SetLabelText(t2, UI.LBL_NOW_LV, skillItemInfo.level.ToString());
						equipSetDetailTop2.SetLabelText(t2, UI.LBL_MAX_LV, skillItemInfo.tableData.GetMaxLv(skillItemInfo.exceedCnt).ToString());
						bool flag2 = skillItemInfo.IsExceeded();
						equipSetDetailTop2.SetActive(t2, UI.LBL_EX_LV, flag2);
						if (flag2)
						{
							equipSetDetailTop2.SetSupportEncoding(t2, UI.LBL_EX_LV, true);
							equipSetDetailTop2.SetLabelText(t2, UI.LBL_EX_LV, UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 9u, skillItemInfo.exceedCnt), ExceedSkillItemTable.color));
						}
						EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
						equipSetDetailTop2.SetActive(t2, UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.HasValue);
						if (enableEquipType.HasValue)
						{
							bool flag3 = enableEquipType.Value == item.tableData.type;
							equipSetDetailTop2.SetSkillEquipIconKind(t2, UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.Value, flag3);
							equipSetDetailTop2.SetActive(t2, UI.LBL_NAME, flag3);
							equipSetDetailTop2.SetActive(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, !flag3);
						}
					}
				});
			}
			base.GetComponent<UITable>(t, (Enum)UI.TBL_SPACE).Reposition();
			Vector3 localPosition3 = t.get_localPosition();
			float y = localPosition3.y;
			Vector3 localPosition4 = FindCtrl(t, UI.OBJ_SPACE).get_localPosition();
			float y2 = localPosition4.y;
			Vector3 localPosition5 = FindCtrl(t, UI.TBL_SPACE).get_localPosition();
			float num2 = y2 + localPosition5.y;
			Vector3 localPosition6 = FindCtrl(t, UI.SPR_EQUIP_INDEX_ICON).get_localPosition();
			localPosition6.y = (num2 - y) * 0.5f;
			FindCtrl(t, UI.SPR_EQUIP_INDEX_ICON).set_localPosition(localPosition6);
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
