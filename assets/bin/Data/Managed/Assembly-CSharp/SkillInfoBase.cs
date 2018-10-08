using Network;
using System;
using System.Collections.Generic;

public abstract class SkillInfoBase : GameSection
{
	protected SkillSlotUIData[] GetSkillSlotData(EquipItemInfo equip)
	{
		if (equip == null)
		{
			return null;
		}
		int maxSlot = equip.GetMaxSlot();
		if (maxSlot == 0)
		{
			return null;
		}
		SkillSlotUIData[] ui_slot_data = new SkillSlotUIData[maxSlot];
		int currentSetNo = MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo();
		SkillItemInfo[] skillInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetSkillInventoryClone();
		SkillItemInfo[] array = Array.FindAll(skillInventoryClone, (SkillItemInfo skill_item) => skill_item.equipSetSkill.Find((EquipSetSkillData skill) => skill.equipItemUniqId == equip.uniqueID && skill.equipSetNo == currentSetNo) != null);
		if (array != null && array.Length > maxSlot)
		{
			Log.Error("Attach Skill Num is Over Skill Slot Num");
		}
		SkillItemTable.SkillSlotData[] slot_data = equip.tableData.GetSkillSlot(equip.exceed);
		Array.ForEach(array, delegate(SkillItemInfo info)
		{
			if (info != null)
			{
				EquipSetSkillData equipSetSkillData = info.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == currentSetNo);
				if (equipSetSkillData != null)
				{
					int num2 = equipSetSkillData.equipSlotNo;
					if (equip.IsExceedSkillSlot(num2))
					{
						num2 = equip.GetExceedSkillIndex(equipSetSkillData.equipSlotNo);
					}
					ui_slot_data[num2] = new SkillSlotUIData();
					ui_slot_data[num2].slotData = new SkillItemTable.SkillSlotData(info.tableData.id, slot_data[num2].slotType);
					ui_slot_data[num2].itemData = info;
				}
			}
		});
		int i = 0;
		for (int num = ui_slot_data.Length; i < num; i++)
		{
			if (ui_slot_data[i] == null)
			{
				ui_slot_data[i] = new SkillSlotUIData();
				ui_slot_data[i].slotData = new SkillItemTable.SkillSlotData(0u, equip.tableData.GetSkillSlot(equip.exceed)[i].slotType);
			}
		}
		return ui_slot_data;
	}

	protected SkillSlotUIData[] GetSkillSlotData(EquipItemTable.EquipItemData table_data, int exceed_cnt)
	{
		if (table_data == null)
		{
			return null;
		}
		if (table_data.GetSkillSlot(exceed_cnt) == null)
		{
			return null;
		}
		SkillItemTable.SkillSlotData[] skillSlot = table_data.GetSkillSlot(exceed_cnt);
		SkillSlotUIData[] array = new SkillSlotUIData[skillSlot.Length];
		int i = 0;
		for (int num = skillSlot.Length; i < num; i++)
		{
			array[i] = new SkillSlotUIData();
			if (skillSlot[i] != null)
			{
				array[i].slotData = skillSlot[i];
			}
			else
			{
				array[i].slotData = new SkillItemTable.SkillSlotData(0u, skillSlot[i].slotType);
			}
		}
		return array;
	}

	protected SkillSlotUIData[] GetEvolveInheritanceSkill(SkillSlotUIData[] before, EquipItemTable.EquipItemData after_equip_table, int exceed_cnt)
	{
		SkillSlotUIData[] skillSlotData = GetSkillSlotData(after_equip_table, exceed_cnt);
		if (skillSlotData == null)
		{
			return null;
		}
		int i = 0;
		for (int num = skillSlotData.Length; i < num; i++)
		{
			if (before != null && i < before.Length)
			{
				skillSlotData[i] = before[i];
			}
			else if (skillSlotData[i].slotData != null && skillSlotData[i].slotData.skill_id != 0)
			{
				Log.Error("Evolve Equip Expand Skill Slot Data is Not Empty :: index = " + i + " : ID = " + skillSlotData[i].slotData.skill_id);
				skillSlotData[i].slotData.skill_id = 0u;
			}
		}
		return skillSlotData;
	}

	private EquipItemAndSkillData[] _GetEquipSetAttachSkillListData(EquipSetInfo equip_set)
	{
		int num = equip_set.item.Length;
		EquipItemAndSkillData[] array = new EquipItemAndSkillData[num];
		int i = 0;
		for (int num2 = num; i < num2; i++)
		{
			EquipItemAndSkillData equipItemAndSkillData = new EquipItemAndSkillData();
			equipItemAndSkillData.equipItemInfo = equip_set.item[i];
			equipItemAndSkillData.skillSlotUIData = GetSkillSlotData(equipItemAndSkillData.equipItemInfo);
			array[i] = equipItemAndSkillData;
		}
		return array;
	}

	protected EquipItemAndSkillData[] GetLocalEquipSetAttachSkillListData(int equip_set_no)
	{
		EquipSetInfo equip_set = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[equip_set_no];
		return _GetEquipSetAttachSkillListData(equip_set);
	}

	protected EquipItemAndSkillData[] GetEquipSetAttachSkillListData(int equip_set_no)
	{
		EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(equip_set_no);
		return _GetEquipSetAttachSkillListData(equipSet);
	}

	protected EquipItemAndSkillData[] GetEquipSetAttachSkillListData(List<CharaInfo.EquipItem> equip_item_data)
	{
		EquipItemAndSkillData[] ary = new EquipItemAndSkillData[7];
		int weapon_cnt = 0;
		equip_item_data.ForEach(delegate(CharaInfo.EquipItem data)
		{
			EquipItemAndSkillData equipItemAndSkillData = new EquipItemAndSkillData
			{
				equipItemInfo = new EquipItemInfo(data)
			};
			int maxSlot = equipItemAndSkillData.equipItemInfo.GetMaxSlot();
			SkillSlotUIData[] array = new SkillSlotUIData[maxSlot];
			List<int> list = new List<int>();
			int j = 0;
			for (int num = maxSlot; j < num; j++)
			{
				array[j] = new SkillSlotUIData();
				array[j].slotData = equipItemAndSkillData.equipItemInfo.tableData.GetSkillSlot(data.exceed)[j];
				array[j].slotData.skill_id = 0u;
				array[j].itemData = null;
				int k = 0;
				for (int count = data.sIds.Count; k < count; k++)
				{
					if (list.IndexOf(k) == -1)
					{
						SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)data.sIds[k]);
						if (skillItemData.type == array[j].slotData.slotType)
						{
							int exceed = 0;
							if (k < data.sExs.Count)
							{
								exceed = data.sExs[k];
							}
							array[j].itemData = new SkillItemInfo(j, data.sIds[k], data.sLvs[k], exceed);
							array[j].slotData.skill_id = (uint)data.sIds[k];
							list.Add(k);
							break;
						}
					}
				}
			}
			equipItemAndSkillData.skillSlotUIData = array;
			int num2 = 0;
			switch (equipItemAndSkillData.equipItemInfo.tableData.type)
			{
			case EQUIPMENT_TYPE.ARMOR:
				num2 = 3;
				break;
			case EQUIPMENT_TYPE.HELM:
				num2 = 4;
				break;
			case EQUIPMENT_TYPE.ARM:
				num2 = 5;
				break;
			case EQUIPMENT_TYPE.LEG:
				num2 = 6;
				break;
			default:
				num2 = ++weapon_cnt;
				break;
			}
			ary[num2] = equipItemAndSkillData;
		});
		for (int i = 0; i < 7; i++)
		{
			if (ary[i] == null)
			{
				ary[i] = new EquipItemAndSkillData();
			}
		}
		return ary;
	}

	protected StatusEquipSetCopyModel.RequestSendForm CopyEquipSetInfo(EquipSetInfo equipSet, int equipSetNo)
	{
		StatusEquipSetCopyModel.RequestSendForm requestSendForm = new StatusEquipSetCopyModel.RequestSendForm();
		requestSendForm.no = equipSetNo;
		requestSendForm.name = equipSet.name;
		requestSendForm.wuid0 = ((equipSet.item[0] == null) ? "0" : equipSet.item[0].uniqueID.ToString());
		requestSendForm.wuid1 = ((equipSet.item[1] == null) ? "0" : equipSet.item[1].uniqueID.ToString());
		requestSendForm.wuid2 = ((equipSet.item[2] == null) ? "0" : equipSet.item[2].uniqueID.ToString());
		requestSendForm.auid = ((equipSet.item[3] == null) ? "0" : equipSet.item[3].uniqueID.ToString());
		requestSendForm.ruid = ((equipSet.item[5] == null) ? "0" : equipSet.item[5].uniqueID.ToString());
		requestSendForm.luid = ((equipSet.item[6] == null) ? "0" : equipSet.item[6].uniqueID.ToString());
		requestSendForm.huid = ((equipSet.item[4] == null) ? "0" : equipSet.item[4].uniqueID.ToString());
		requestSendForm.show = equipSet.showHelm;
		int i = 0;
		for (int num = equipSet.item.Length; i < num; i++)
		{
			EquipItemInfo equipItemInfo = equipSet.item[i];
			if (equipItemInfo != null)
			{
				SkillSlotUIData[] skillSlotData = GetSkillSlotData(equipItemInfo);
				if (skillSlotData != null)
				{
					int j = 0;
					for (int num2 = skillSlotData.Length; j < num2; j++)
					{
						SkillItemInfo itemData = skillSlotData[j].itemData;
						requestSendForm.euids.Add(equipItemInfo.uniqueID.ToString());
						requestSendForm.suids.Add((itemData == null) ? "0" : itemData.uniqueID.ToString());
						int num3 = j;
						if (equipItemInfo.IsExceedSkillSlot(num3))
						{
							num3 = equipItemInfo.GetExceedSkillSlotNo(num3);
						}
						requestSendForm.slots.Add(num3);
					}
				}
			}
		}
		return requestSendForm;
	}
}
