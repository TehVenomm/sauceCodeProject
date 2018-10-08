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
		if ((UnityEngine.Object)detailBase != (UnityEngine.Object)null)
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

	protected override void UpdateInventoryUI()
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
		SetDynamicList(inventoryUI, null, inventory.datas.Length + 2, false, delegate(int i)
		{
			if (i == 0)
			{
				return !isVisibleEmptySkill && find_index >= 0;
			}
			bool flag = false;
			bool flag2 = true;
			int num2 = i - 1;
			if (find_index >= 0)
			{
				if (num2 == 0)
				{
					flag = true;
				}
				else
				{
					num2--;
				}
			}
			if (!flag && (num2 >= inventory.datas.Length || (find_index >= 0 && num2 == find_index)))
			{
				flag2 = false;
			}
			if (flag2)
			{
				SortCompareData sortCompareData2 = inventory.datas[num2];
				if (sortCompareData2 == null || !sortCompareData2.IsPriority(inventory.sortSettings.orderTypeAsc))
				{
					flag2 = false;
				}
			}
			return flag2;
		}, null, delegate(int i, Transform t, bool is_recycle)
		{
			if (i == 0)
			{
				if (!isVisibleEmptySkill)
				{
					CreateRemoveIcon(t, "SELECT", -1, 100, selectIndex == -1, base.sectionData.GetText("STR_DETACH"));
				}
			}
			else
			{
				int num = i - 1;
				if (find_index >= 0)
				{
					num = ((num != 0) ? (num - 1) : find_index);
				}
				SetActive(t, true);
				SortCompareData sortCompareData = inventory.datas[num];
				SkillItemInfo skillItemInfo = sortCompareData.GetItemData() as SkillItemInfo;
				ITEM_ICON_TYPE iconType = sortCompareData.GetIconType();
				bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, sortCompareData.GetUniqID());
				ItemIcon itemIcon = CreateItemIconDetail(iconType, sortCompareData.GetIconID(), sortCompareData.GetRarity(), sortCompareData as SkillItemSortData, base.IsShowMainStatus, t, "SELECT", num, is_new, 100, selectIndex == num, skillItemInfo.IsCurrentEquipSetAttached, sortCompareData.IsExceeded(), false);
				itemIcon.SetItemID(sortCompareData.GetTableID());
				SetLongTouch(itemIcon.transform, "DETAIL", num);
				if ((UnityEngine.Object)itemIcon != (UnityEngine.Object)null && sortCompareData != null)
				{
					itemIcon.SetInitData(sortCompareData);
				}
				if (!m_generatedIconList.Contains(itemIcon))
				{
					m_generatedIconList.Add(itemIcon);
				}
			}
		});
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

	private void ToNotEnableSkillTypeConfirm()
	{
		if (is_not_enable_skill_type)
		{
			is_not_enable_skill_type = false;
			GameSection.ChangeEvent("COME_BACK", null);
			Action action = delegate
			{
				SortCompareData sortCompareData = inventory.datas[selectIndex];
				SkillItemInfo skillItemInfo = sortCompareData.GetItemData() as SkillItemInfo;
				EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
				DispatchEvent("NOT_SKILL_ENABLE_TYPE", new object[1]
				{
					MonoBehaviourSingleton<StatusManager>.I.GetEquipItemGroupString(enableEquipType.Value)
				});
			};
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreenName().Contains(base.name))
			{
				action();
			}
			else
			{
				StartCoroutine(DelayCall(action));
			}
		}
	}

	private IEnumerator DelayCall(Action call)
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("ItemDetailEquipSkillSelect") || MonoBehaviourSingleton<UIManager>.I.IsTransitioning() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		call();
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
			GameSection.ResumeEvent(is_success, null);
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
			GameSection.ResumeEvent(is_success, null);
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
