using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailEquipSkillSelect : SkillSelectBaseSecond
{
	private int slotIndex;

	private bool is_not_enable_skill_type;

	private bool isSelfSectionChange;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		slotIndex = (int)array[3];
		GameSection.SetEventData(new object[3]
		{
			array[0],
			array[1],
			array[2]
		});
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetVisibleEmptySkillType(isVisibleEmptySkill, slotIndex);
		if (detailBase != null)
		{
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, false);
		}
	}

	protected override void SetInventoryIsEmptyParam()
	{
		bool is_empty = true;
		if (inventory != null && inventory.datas.Length > 0 && equipItem != null)
		{
			SkillItemTable.SkillSlotData[] skillSlot = equipItem.tableData.GetSkillSlot(equipItem.exceed);
			if (skillSlot != null && skillSlot.Length > slotIndex)
			{
				SKILL_SLOT_TYPE skill_slot_type = skillSlot[slotIndex].slotType;
				Array.ForEach(inventory.datas, delegate(SortCompareData _data)
				{
					if (is_empty && _data != null)
					{
						SkillItemInfo skillItemInfo = _data.GetItemData() as SkillItemInfo;
						if (skillItemInfo.tableData.type == skill_slot_type)
						{
							is_empty = false;
						}
					}
				});
			}
		}
		isVisibleEmptySkill = is_empty;
	}

	protected override ItemStorageTop.SkillItemInventory CreateInventory()
	{
		return new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.SKILL_ITEM, equipItem.tableData.GetSkillSlot(equipItem.exceed)[slotIndex].slotType, false);
	}

	protected unsafe override void UpdateInventoryUI()
	{
		int find_index = -1;
		if (equipSkillItem != null)
		{
			find_index = Array.FindIndex(inventory.datas, (SortCompareData data) => data.GetUniqID() == equipSkillItem.uniqueID);
			if (find_index > -1 && (inventory.datas[find_index] == null || !inventory.datas[find_index].IsPriority(inventory.sortSettings.orderTypeAsc)))
			{
				find_index = -1;
			}
		}
		SetupEnableInventoryUI();
		m_generatedIconList.Clear();
		UpdateNewIconInfo();
		_003CUpdateInventoryUI_003Ec__AnonStorey3EE _003CUpdateInventoryUI_003Ec__AnonStorey3EE;
		SetDynamicList((Enum)inventoryUI, (string)null, inventory.datas.Length + 2, false, new Func<int, bool>((object)_003CUpdateInventoryUI_003Ec__AnonStorey3EE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null, new Action<int, Transform, bool>((object)_003CUpdateInventoryUI_003Ec__AnonStorey3EE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override int GetInventoryFirstIndex()
	{
		return -1;
	}

	protected override void OnDecision()
	{
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		string sectionName = historyList[historyList.Count - 2].sectionName;
		bool flag = sectionName == "ItemDetailSkillDialog" || sectionName == "ItemDetailSkill";
		if (selectIndex == -1)
		{
			if (equipSkillItem == null)
			{
				GameSection.BackSection();
			}
			else
			{
				GameSection.ChangeEvent("DETACH", null);
				SendDetachEquipSkill();
			}
		}
		else
		{
			SortCompareData sortCompareData = inventory.datas[selectIndex];
			if (equipSkillItem != null && equipSkillItem.uniqueID == sortCompareData.GetUniqID())
			{
				GameSection.BackSection();
			}
			else
			{
				EquipItemInfo equipItemInfo = null;
				SkillItemInfo skillItemInfo = sortCompareData.GetItemData() as SkillItemInfo;
				if (skillItemInfo.IsCurrentEquipSetAttached)
				{
					EquipSetSkillData equipSetSkillData = skillItemInfo.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
					equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equipSetSkillData.equipItemUniqId);
				}
				if (equipSkillItem != null)
				{
					if (!skillItemInfo.IsCurrentEquipSetAttached)
					{
						GameSection.ChangeEvent((!flag) ? "EQUIP" : "EQUIP_DETAIL", null);
						CheckSendEquipSkill();
					}
					else
					{
						GameSection.ChangeEvent((!flag) ? "STEAL" : "STEAL_DETAIL", new object[5]
						{
							equipSkillItem.tableData.name,
							equipSkillItem.level.ToString(),
							equipItemInfo.tableData.name,
							sortCompareData.GetName(),
							sortCompareData.GetLevel().ToString()
						});
					}
				}
				else if (skillItemInfo.IsCurrentEquipSetAttached)
				{
					GameSection.ChangeEvent((!flag) ? "REPLACE" : "REPLACE_DETAIL", new object[3]
					{
						equipItemInfo.tableData.name,
						sortCompareData.GetName(),
						sortCompareData.GetLevel().ToString()
					});
				}
				else
				{
					GameSection.ChangeEvent((!flag) ? "EQUIP" : "EQUIP_DETAIL", null);
					CheckSendEquipSkill();
				}
			}
		}
	}

	private void CheckSendEquipSkill()
	{
		is_not_enable_skill_type = !CheckEnableSkillType();
		if (is_not_enable_skill_type)
		{
			ToNotEnableSkillTypeConfirm();
		}
		else
		{
			_SendEquipSkill();
		}
	}

	private bool CheckEnableSkillType()
	{
		SortCompareData sortCompareData = inventory.datas[selectIndex];
		if (sortCompareData != null)
		{
			SkillItemInfo skillItemInfo = sortCompareData.GetItemData() as SkillItemInfo;
			EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
			if (skillItemInfo != null && enableEquipType.HasValue && enableEquipType.Value != EQUIPMENT_TYPE.ARMOR && !skillItemInfo.tableData.IsEnableEquipType(equipItem.tableData.type))
			{
				return false;
			}
		}
		return true;
	}

	private unsafe void ToNotEnableSkillTypeConfirm()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (is_not_enable_skill_type)
		{
			is_not_enable_skill_type = false;
			GameSection.ChangeEvent("COME_BACK", null);
			Action val = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreenName().Contains(this.get_name()))
			{
				val.Invoke();
			}
			else
			{
				this.StartCoroutine(DelayCall(val));
			}
		}
	}

	private IEnumerator DelayCall(Action call)
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("ItemDetailEquipSkillSelect") || MonoBehaviourSingleton<UIManager>.I.IsTransitioning() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		call.Invoke();
	}

	private void OnQuery_ItemDetailSkillReplaceConfirm_YES()
	{
		CheckSendEquipSkill();
	}

	private void OnCloseDialog_ItemDetailSkillReplaceConfirm()
	{
		ToNotEnableSkillTypeConfirm();
	}

	private void OnQuery_ItemDetailSkillStealConfirm_YES()
	{
		CheckSendEquipSkill();
	}

	private void OnCloseDialog_ItemDetailSkillStealConfirm()
	{
		ToNotEnableSkillTypeConfirm();
	}

	private void OnQuery_ItemDetailSkillReplaceDetailConfirm_YES()
	{
		CheckSendEquipSkill();
	}

	private void OnCloseDialog_ItemDetailSkillReplaceDetailConfirm()
	{
		ToNotEnableSkillTypeConfirm();
	}

	private void OnQuery_ItemDetailSkillStealDetailConfirm_YES()
	{
		CheckSendEquipSkill();
	}

	private void OnCloseDialog_ItemDetailSkillStealDetailConfirm()
	{
		ToNotEnableSkillTypeConfirm();
	}

	private void OnQuery_ItemDetailNotSkillEnableTypeConfirm_YES()
	{
		_SendEquipSkill();
	}

	private void _SendEquipSkill()
	{
		SortCompareData sortCompareData = inventory.datas[selectIndex];
		int exceedSkillSlotNo = slotIndex;
		if (equipItem.IsExceedSkillSlot(exceedSkillSlotNo))
		{
			exceedSkillSlotNo = equipItem.GetExceedSkillSlotNo(exceedSkillSlotNo);
		}
		isSelfSectionChange = true;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.SendSetSkill(equipItem.uniqueID, sortCompareData.GetUniqID(), exceedSkillSlotNo, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), delegate(bool is_success)
		{
			if (!is_success)
			{
				isSelfSectionChange = false;
			}
			GameSection.ResumeEvent(is_success, null, false);
		});
	}

	private void SendDetachEquipSkill()
	{
		int exceedSkillSlotNo = slotIndex;
		if (equipItem.IsExceedSkillSlot(exceedSkillSlotNo))
		{
			exceedSkillSlotNo = equipItem.GetExceedSkillSlotNo(exceedSkillSlotNo);
		}
		isSelfSectionChange = true;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.SendDetachSkill(equipItem.uniqueID, exceedSkillSlotNo, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), delegate(bool is_success)
		{
			if (is_success)
			{
				equipSkillItem = null;
			}
			else
			{
				isSelfSectionChange = false;
			}
			GameSection.ResumeEvent(is_success, null, false);
		});
	}

	private void OnCloseDialog_ItemDetailEquipSkillSort()
	{
		OnCloseSort();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_FAVORITE) != (NOTIFY_FLAG)0L)
		{
			if (!isSelfSectionChange)
			{
				updateInventory = true;
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_SKILL_INVENTORY) != (NOTIFY_FLAG)0L && !isSelfSectionChange)
		{
			updateInventory = true;
		}
		base.OnNotify(flags);
		isSelfSectionChange = false;
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		if (!isSelfSectionChange)
		{
			return (NOTIFY_FLAG)0L;
		}
		return NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
	}
}
