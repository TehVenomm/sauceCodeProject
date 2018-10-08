using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusEquip : EquipSelectBase
{
	public class LocalEquipSetData
	{
		public int setNo;

		public int index;

		public EquipSetInfo equipSetInfo;

		public LocalEquipSetData(int _no, int _index, EquipSetInfo _set)
		{
			setNo = _no;
			index = _index;
			equipSetInfo = _set;
		}

		public int EquippingIndexOf(EquipItemInfo item)
		{
			if (item == null)
			{
				return -1;
			}
			int i = 0;
			for (int num = 7; i < num; i++)
			{
				if (equipSetInfo.item[i] != null && equipSetInfo.item[i].uniqueID == item.uniqueID)
				{
					return i;
				}
			}
			return -1;
		}

		public bool IsEnableChange(EquipItemInfo item)
		{
			if (item == null)
			{
				return true;
			}
			int i = 0;
			for (int num = 7; i < num; i++)
			{
				if (equipSetInfo.item[i] != null && equipSetInfo.item[i].uniqueID == item.uniqueID && i != index)
				{
					return false;
				}
			}
			return true;
		}

		public EquipItemInfo GetEquippingItem()
		{
			return equipSetInfo.item[index];
		}
	}

	public class ChangeEquipData
	{
		public int setNo;

		public int index;

		public EquipItemInfo item;

		public ChangeEquipData(int _no, int _index, EquipItemInfo _item)
		{
			setNo = _no;
			index = _index;
			item = _item;
		}
	}

	public class MigrationSkillData
	{
		public ulong toUniqueId
		{
			get;
			protected set;
		}

		public int toSlotNo
		{
			get;
			protected set;
		}

		public SkillItemInfo skill
		{
			get;
			protected set;
		}

		public MigrationSkillData(ulong toId, int slotNo, SkillItemInfo target)
		{
			toUniqueId = toId;
			toSlotNo = slotNo;
			skill = target;
		}
	}

	protected EquipItemInfo migrationOldItem;

	protected EquipItemInfo migrationSelectItem;

	protected int migrationSendCount;

	protected EquipItemInfo detailItem;

	public LocalEquipSetData selectEquipSetData
	{
		get;
		protected set;
	}

	protected override EquipItemInfo EquipItem
	{
		get
		{
			return MonoBehaviourSingleton<StatusManager>.I.GetSelectEquipItem();
		}
		set
		{
			MonoBehaviourSingleton<StatusManager>.I.SetSelectEquipItem(value);
		}
	}

	public override void Initialize()
	{
		EquipItemInfo equippingItem = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItem();
		selectEquipSetData = (GameSection.GetEventData() as LocalEquipSetData);
		EquipItem = equippingItem;
		if (equippingItem == null)
		{
			GameSection.SetEventData(-1);
			SelectingInventoryFirst();
		}
		else
		{
			selectInventoryIndex = GetSelectItemIndex();
		}
		base.Initialize();
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		_OnOpenStatusStage();
	}

	protected virtual void _OnOpenStatusStage()
	{
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetEquipSetData(selectEquipSetData);
			MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(EquipItem);
		}
		MonoBehaviourSingleton<FilterManager>.I.StartBlur(MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionStartBlurTime, MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionBlurStrength, MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionBlurDelay);
	}

	public override void Close(UITransition.TYPE type = UITransition.TYPE.CLOSE)
	{
		base.Close(type);
		_OnCloseStatusStage();
	}

	protected virtual void _OnCloseStatusStage()
	{
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetEquipSetData(null);
		}
		MonoBehaviourSingleton<FilterManager>.I.StopBlur(MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.equipSectionEndTime, 0f);
	}

	protected override void InitSort()
	{
		if (MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType))
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.WEAPON, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
		}
		else
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ARMOR, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
		}
	}

	protected override void InitLocalInventory()
	{
		MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
		localInventoryEquipData = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
	}

	protected override void SelectingInventoryFirst()
	{
		selectInventoryIndex = -1;
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	protected override string GetSelectTypeText()
	{
		string result = string.Empty;
		switch (selectEquipSetData.index)
		{
		case 0:
			result = base.sectionData.GetText("SELECT_WEAPON_1");
			break;
		case 1:
			result = base.sectionData.GetText("SELECT_WEAPON_2");
			break;
		case 2:
			result = base.sectionData.GetText("SELECT_WEAPON_3");
			break;
		case 3:
			result = base.sectionData.GetText("SELECT_ARMOR");
			break;
		case 4:
			result = base.sectionData.GetText("SELECT_HELM");
			break;
		case 5:
			result = base.sectionData.GetText("SELECT_ARM");
			break;
		case 6:
			result = base.sectionData.GetText("SELECT_LEG");
			break;
		}
		return result;
	}

	protected override void EquipParam()
	{
		EquipItemInfo compareItemData = GetCompareItemData();
		EquipItemInfo select_item = EquipItem;
		EquipSetInfo equipSetInfo = new EquipSetInfo(selectEquipSetData.equipSetInfo.item, selectEquipSetData.equipSetInfo.name, selectEquipSetData.equipSetInfo.showHelm, selectEquipSetData.equipSetInfo.acc);
		bool flag = false;
		int num = Array.FindIndex(selectEquipSetData.equipSetInfo.item, (EquipItemInfo item) => item != null && select_item != null && item.uniqueID == select_item.uniqueID);
		if (num != -1)
		{
			flag = true;
		}
		if (!flag)
		{
			equipSetInfo.item[selectEquipSetData.index] = select_item;
		}
		else
		{
			equipSetInfo.item[num] = compareItemData;
			equipSetInfo.item[selectEquipSetData.index] = select_item;
		}
		MonoBehaviourSingleton<StatusManager>.I.CalcSelfStatusParam(equipSetInfo, out int _atk, out int _def, out int _hp, out int _, out int _);
		SetLabelText((Enum)UI.LBL_STATUS_ATK, _atk.ToString());
		SetLabelText((Enum)UI.LBL_STATUS_DEF, _def.ToString());
		SetLabelText((Enum)UI.LBL_STATUS_HP, _hp.ToString());
		int atk = 0;
		int def = 0;
		int hp = 0;
		int atk2 = 0;
		int def2 = 0;
		int hp2 = 0;
		if (!flag)
		{
			CalcEquipAttachSkillStatus(compareItemData, out atk, out def, out hp);
			CalcEquipAttachSkillStatus(select_item, out atk2, out def2, out hp2);
		}
		else
		{
			atk += atk2;
			def += def2;
			hp += hp2;
			atk2 = atk;
			def2 = def;
			hp2 = hp;
		}
		if (selectEquipSetData.index != 1 && selectEquipSetData.index != 2)
		{
			int num2 = (compareItemData != null) ? (compareItemData.atk + compareItemData.elemAtk + atk) : 0;
			int num3 = (select_item != null) ? (select_item.atk + select_item.elemAtk + atk2) : 0;
			int num4 = num3 - num2;
			string format = (num4 <= 0) ? "{0}" : base.sectionData.GetText("DISP_PLUS");
			SetLabelCompareParam((Enum)UI.LBL_STATUS_ADD_ATK, num3, num2, string.Format(format, num4));
			SetActive((Enum)UI.LBL_STATUS_ADD_ATK, num4 != 0);
			int num5 = (compareItemData != null) ? (compareItemData.def + def) : 0;
			int num6 = (select_item != null) ? (select_item.def + def2) : 0;
			int num7 = num6 - num5;
			string format2 = (num7 <= 0) ? "{0}" : base.sectionData.GetText("DISP_PLUS");
			SetLabelCompareParam((Enum)UI.LBL_STATUS_ADD_DEF, num6, num5, string.Format(format2, num7));
			SetActive((Enum)UI.LBL_STATUS_ADD_DEF, num7 != 0);
			int num8 = (compareItemData != null) ? (compareItemData.hp + hp) : 0;
			int num9 = (select_item != null) ? (select_item.hp + hp2) : 0;
			int num10 = num9 - num8;
			string format3 = (num10 <= 0) ? "{0}" : base.sectionData.GetText("DISP_PLUS");
			SetLabelCompareParam((Enum)UI.LBL_STATUS_ADD_HP, num9, num8, string.Format(format3, num10));
			SetActive((Enum)UI.LBL_STATUS_ADD_HP, num10 != 0);
		}
		else
		{
			int num11 = atk;
			int num12 = atk2;
			int num13 = num12 - num11;
			string format4 = (num13 <= 0) ? "{0}" : base.sectionData.GetText("DISP_PLUS");
			SetLabelCompareParam((Enum)UI.LBL_STATUS_ADD_ATK, num12, num11, string.Format(format4, num13));
			SetActive((Enum)UI.LBL_STATUS_ADD_ATK, num13 != 0);
			int num14 = def;
			int num15 = def2;
			int num16 = num15 - num14;
			string format5 = (num16 <= 0) ? "{0}" : base.sectionData.GetText("DISP_PLUS");
			SetLabelCompareParam((Enum)UI.LBL_STATUS_ADD_DEF, num15, num14, string.Format(format5, num16));
			SetActive((Enum)UI.LBL_STATUS_ADD_DEF, num16 != 0);
			int num17 = hp;
			int num18 = hp2;
			int num19 = num18 - num17;
			string format6 = (num19 <= 0) ? "{0}" : base.sectionData.GetText("DISP_PLUS");
			SetLabelCompareParam((Enum)UI.LBL_STATUS_ADD_HP, num18, num17, string.Format(format6, num19));
			SetActive((Enum)UI.LBL_STATUS_ADD_HP, num19 != 0);
		}
		SetActive((Enum)UI.OBJ_SELL_ROOT, false);
		if (select_item == null)
		{
			string text = base.sectionData.GetText("NON_DATA");
			if (compareItemData != null)
			{
				if (compareItemData.tableData.IsWeapon())
				{
					SetActive((Enum)UI.OBJ_ELEM_ROOT, compareItemData.elemAtk > 0);
					SetElementSprite((Enum)UI.SPR_ELEM, compareItemData.GetElemAtkType());
					SetLabelCompareParam((Enum)UI.LBL_ATK, -compareItemData.atk, 0, 0);
					SetLabelCompareParam((Enum)UI.LBL_ELEM, -compareItemData.elemAtk, 0, 0);
				}
				else
				{
					SetActive((Enum)UI.OBJ_ELEM_ROOT, compareItemData.elemDef > 0);
					SetDefElementSprite((Enum)UI.SPR_ELEM, compareItemData.GetElemDefType());
					SetLabelCompareParam((Enum)UI.LBL_DEF, -compareItemData.def, 0, 0);
					SetLabelCompareParam((Enum)UI.LBL_ELEM, -compareItemData.elemDef, 0, 0);
				}
			}
			else
			{
				bool flag2 = selectEquipSetData.index < 3;
				SetLabelCompareParam((Enum)UI.LBL_ATK, 0, 0, -1);
				SetLabelCompareParam((Enum)UI.LBL_DEF, 0, 0, -1);
				SetActive((Enum)UI.OBJ_ATK_ROOT, flag2);
				SetActive((Enum)UI.OBJ_DEF_ROOT, !flag2);
				SetActive((Enum)UI.OBJ_ELEM_ROOT, false);
			}
			SetLabelText((Enum)UI.LBL_NAME, base.sectionData.GetText("EMPTY"));
			SetLabelCompareParam((Enum)UI.LBL_LV_NOW, 0, 0, text);
			SetLabelCompareParam((Enum)UI.LBL_LV_MAX, 0, 0, text);
			SetActive((Enum)UI.OBJ_SKILL_BUTTON_ROOT, false);
			SetActive((Enum)UI.TBL_ABILITY, false);
			SetActive((Enum)UI.STR_NON_ABILITY, false);
			SetActive((Enum)UI.SPR_IS_EVOLVE, false);
			SetEquipmentTypeIcon((Enum)UI.SPR_TYPE_ICON, (Enum)UI.SPR_TYPE_ICON_BG, (Enum)UI.SPR_TYPE_ICON_RARITY, (EquipItemTable.EquipItemData)null);
		}
		else
		{
			SetActive((Enum)UI.TBL_ABILITY, true);
			base.EquipParam();
		}
	}

	private void CalcEquipAttachSkillStatus(EquipItemInfo item, out int atk, out int def, out int hp)
	{
		int tmp_atk = 0;
		int tmp_def = 0;
		int tmp_hp = 0;
		if (item != null)
		{
			SkillSlotUIData[] skillSlotData = GetSkillSlotData(item);
			if (skillSlotData != null)
			{
				int index;
				Array.ForEach(skillSlotData, delegate(SkillSlotUIData data)
				{
					if (data != null && data.slotData.skill_id != 0)
					{
						ELEMENT_TYPE targetElement = item.GetTargetElement();
						int elem_atk = 0;
						if (item.tableData.IsWeapon())
						{
							switch (targetElement)
							{
							default:
								elem_atk = data.itemData.atkList[(int)(targetElement + 1)];
								break;
							case ELEMENT_TYPE.MULTI:
								index = 0;
								data.itemData.atkList.ForEach(delegate(int _elem_atk)
								{
									if (index > 0)
									{
										elem_atk += _elem_atk;
									}
									index++;
								});
								break;
							case ELEMENT_TYPE.MAX:
								break;
							}
						}
						tmp_atk += data.itemData.atk + elem_atk;
						tmp_def += data.itemData.def;
						tmp_hp += data.itemData.hp;
					}
				});
			}
		}
		atk = tmp_atk;
		def = tmp_def;
		hp = tmp_hp;
	}

	protected virtual bool IsCreateRemoveButton()
	{
		return selectEquipSetData.index != 0 && selectEquipSetData.index != 3;
	}

	protected unsafe override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
			bool created_remove_btn = false;
			EquipItemInfo equipping_item = GetCompareItemData();
			int find_index = -1;
			if (equipping_item != null)
			{
				find_index = Array.FindIndex(localInventoryEquipData, (SortCompareData data) => data.GetUniqID() == equipping_item.uniqueID);
				if (find_index > -1 && (localInventoryEquipData[find_index] == null || !localInventoryEquipData[find_index].IsPriority(sortSettings.orderTypeAsc)))
				{
					find_index = -1;
				}
			}
			created_remove_btn = IsCreateRemoveButton();
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			_003CLocalInventory_003Ec__AnonStorey43A _003CLocalInventory_003Ec__AnonStorey43A;
			SetDynamicList((Enum)InventoryUI, (string)null, localInventoryEquipData.Length + 2, false, new Func<int, bool>((object)_003CLocalInventory_003Ec__AnonStorey43A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null, new Action<int, Transform, bool>((object)_003CLocalInventory_003Ec__AnonStorey43A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	protected virtual bool IsNotEquip(bool is_not_equip_any_slot, bool is_equip_now_slot)
	{
		return is_not_equip_any_slot;
	}

	protected override EquipItemInfo GetCompareItemData()
	{
		return MonoBehaviourSingleton<StatusManager>.I.GetEquippingItem();
	}

	protected override void OnQuery_TRY_ON()
	{
		selectInventoryIndex = (int)GameSection.GetEventData();
		if (selectInventoryIndex < 0)
		{
			EquipItem = null;
		}
		else if (localInventoryEquipData != null)
		{
			EquipItem = (localInventoryEquipData[selectInventoryIndex].GetItemData() as EquipItemInfo);
		}
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(EquipItem);
		}
		base.OnQuery_TRY_ON();
	}

	protected virtual void ChangeSelectItem(EquipItemInfo select_item, EquipItemInfo old_item)
	{
		GameSection.SetEventData(new ChangeEquipData(selectEquipSetData.setNo, selectEquipSetData.index, select_item));
		if (old_item == null || select_item == null)
		{
			OnQuery_MAIN_MENU_STATUS();
		}
		else
		{
			bool flag = false;
			for (int i = 0; i < old_item.GetMaxSlot(); i++)
			{
				if (old_item.GetSkillItem(i, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()) != null)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				migrationOldItem = old_item;
				migrationSelectItem = select_item;
				GameSection.ChangeEvent("MIGRATION_SKILL_CONFIRM", null);
			}
			else
			{
				OnQuery_MAIN_MENU_STATUS();
			}
		}
	}

	protected virtual void NotChangeItem(EquipItemInfo select_item, EquipItemInfo old_item)
	{
		GameSection.SetEventData(new ChangeEquipData(selectEquipSetData.setNo, selectEquipSetData.index, old_item));
	}

	protected virtual bool IsAlreadyEquipItem(EquipItemInfo item)
	{
		return !selectEquipSetData.IsEnableChange(item);
	}

	protected override void OnQuery_SELECT_ITEM()
	{
		if (OnSelectItemAndChekIsGoStatus())
		{
			OnQuery_MAIN_MENU_STATUS();
		}
	}

	protected unsafe bool OnSelectItemAndChekIsGoStatus()
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		EquipItemInfo equipItem = EquipItem;
		ulong num = (equipItem == null) ? 0 : equipItem.uniqueID;
		if (num == 0L && !IsCreateRemoveButton())
		{
			GameSection.ChangeEvent("NO_SELECTED", null);
			return false;
		}
		if (IsAlreadyEquipItem(equipItem))
		{
			int num2 = selectEquipSetData.EquippingIndexOf(equipItem);
			if (num2 == 0 && selectEquipSetData.GetEquippingItem() == null)
			{
				GameSection.ChangeEvent("NOT_SWAP", null);
			}
			else
			{
				GameSection.ChangeEvent("SWAP_CONFIRM", new object[2]
				{
					(num2 + 1).ToString(),
					equipItem.tableData.name
				});
			}
			return false;
		}
		EquipItemInfo compareItemData = GetCompareItemData();
		ulong num3 = (compareItemData == null) ? 0 : compareItemData.uniqueID;
		if (num3 != num)
		{
			if (equipItem != null && !MonoBehaviourSingleton<GameSceneManager>.I.CheckEquipItemAndOpenUpdateAppDialog(equipItem.tableData, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
			{
				GameSection.StopEvent();
				return false;
			}
			if (equipItem != null && equipItem.tableData != null)
			{
				GameSaveData.instance.RemoveNewIconAndSave(ItemIcon.GetItemIconType(equipItem.tableData.type), equipItem.uniqueID);
			}
			ChangeSelectItem(equipItem, compareItemData);
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				TutorialStep.isChangeLocalEquip = true;
			}
			return false;
		}
		NotChangeItem(equipItem, compareItemData);
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			TutorialStep.isChangeLocalEquip = true;
		}
		return true;
	}

	protected override void OnQueryDetail()
	{
		int num = (int)GameSection.GetEventData();
		detailItem = null;
		if (num >= 0 && localInventoryEquipData != null)
		{
			detailItem = (localInventoryEquipData[num].GetItemData() as EquipItemInfo);
		}
		if (detailItem == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP,
				detailItem,
				selectEquipSetData.setNo
			});
		}
	}

	protected void OnCancelSelect()
	{
		EquipItem = GetCompareItemData();
		selectInventoryIndex = GetSelectItemIndex();
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY);
	}

	protected override void OnQuery_SKILL_ICON_BUTTON()
	{
		if (EquipItem == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP,
				EquipItem,
				MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex
			});
		}
	}

	private void OnQuery_ABILITY()
	{
		EquipSetInfo equipSetInfo = new EquipSetInfo(selectEquipSetData.equipSetInfo.item, selectEquipSetData.equipSetInfo.name, selectEquipSetData.equipSetInfo.showHelm, selectEquipSetData.equipSetInfo.acc);
		equipSetInfo.item[selectEquipSetData.index] = EquipItem;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		GameSection.SetEventData(new object[3]
		{
			equipSetInfo,
			MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(selectEquipSetData.setNo, new EquipItemAbilityCollection.SwapData(selectEquipSetData.index, EquipItem)),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(userStatus.atk, userStatus.def, userStatus.hp, null)
		});
	}

	private void OnQuery_NON_ABILITY()
	{
		EquipSetInfo equipSetInfo = new EquipSetInfo(selectEquipSetData.equipSetInfo.item, selectEquipSetData.equipSetInfo.name, selectEquipSetData.equipSetInfo.showHelm, selectEquipSetData.equipSetInfo.acc);
		equipSetInfo.item[selectEquipSetData.index] = EquipItem;
		GameSection.ChangeEvent("ABILITY", new object[2]
		{
			equipSetInfo,
			MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(selectEquipSetData.setNo, new EquipItemAbilityCollection.SwapData(selectEquipSetData.index, EquipItem))
		});
	}

	protected void OnCloseDialog_StatusEquipSort()
	{
		OnCloseSortDialog();
	}

	protected override bool sorting()
	{
		InitLocalInventory();
		return true;
	}

	protected override int GetSelectItemIndex()
	{
		EquipItemInfo equipItem = EquipItem;
		if (equipItem == null)
		{
			return -1;
		}
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			return -1;
		}
		int i = 0;
		for (int num = localInventoryEquipData.Length; i < num; i++)
		{
			if (localInventoryEquipData[i] != null && localInventoryEquipData[i].GetUniqID() == equipItem.uniqueID)
			{
				return i;
			}
		}
		return -1;
	}

	public override void OnNotify(NOTIFY_FLAG notify_flags)
	{
		if ((notify_flags & NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (NOTIFY_FLAG)0L)
		{
			if (detailItem != null)
			{
				EquipItemInfo equipItem = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(detailItem.uniqueID);
				MonoBehaviourSingleton<StatusManager>.I.UpdateLocalInventory(equipItem);
				InitLocalInventory();
			}
		}
		else if ((notify_flags & (NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY)) != (NOTIFY_FLAG)0L)
		{
			InitLocalInventory();
			if (sortSettings.Sort(localInventoryEquipData as EquipItemSortData[]))
			{
				selectInventoryIndex = GetSelectItemIndex();
				if (selectInventoryIndex == -1)
				{
					EquipItemInfo compareItemData = GetCompareItemData();
					if (compareItemData != null)
					{
						EquipItem = compareItemData;
						selectInventoryIndex = GetSelectItemIndex();
					}
					else if (!IsCreateRemoveButton())
					{
						selectInventoryIndex = 0;
					}
				}
				EquipItem = ((selectInventoryIndex == -1) ? null : (localInventoryEquipData[selectInventoryIndex].GetItemData() as EquipItemInfo));
				if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
				{
					MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(EquipItem);
				}
				SetDirty(InventoryUI);
			}
		}
		base.OnNotify(notify_flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY;
	}

	protected virtual void OnQuery_StatusSwapEquipConfirm_YES()
	{
		int num = selectEquipSetData.EquippingIndexOf(EquipItem);
		int index = selectEquipSetData.index;
		EquipItemInfo equipItemInfo = selectEquipSetData.equipSetInfo.item[index];
		selectEquipSetData.equipSetInfo.item[index] = selectEquipSetData.equipSetInfo.item[num];
		selectEquipSetData.equipSetInfo.item[num] = equipItemInfo;
		MonoBehaviourSingleton<StatusManager>.I.SwapWeapon(num, index);
	}

	protected unsafe virtual void OnQuery_StatusMigrationSkillConfirm_YES()
	{
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		List<SkillItemInfo> list = new List<SkillItemInfo>();
		List<MigrationSkillData> list2 = new List<MigrationSkillData>();
		for (int i = 0; i < migrationOldItem.GetMaxSlot(); i++)
		{
			bool flag = false;
			SkillItemInfo skillItem = migrationOldItem.GetSkillItem(i, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
			if (skillItem != null)
			{
				for (int j = 0; j < migrationSelectItem.GetMaxSlot(); j++)
				{
					SkillItemTable.SkillSlotData skillSlotData = migrationSelectItem.tableData.GetSkillSlot(migrationSelectItem.exceed)[j];
					if (skillSlotData != null && skillSlotData.slotType == skillItem.tableData.type)
					{
						int toSlot = j;
						if (migrationSelectItem.IsExceedSkillSlot(j))
						{
							toSlot = migrationSelectItem.GetExceedSkillSlotNo(j);
						}
						_003COnQuery_StatusMigrationSkillConfirm_YES_003Ec__AnonStorey43B _003COnQuery_StatusMigrationSkillConfirm_YES_003Ec__AnonStorey43B;
						if (list2.All(new Func<MigrationSkillData, bool>((object)_003COnQuery_StatusMigrationSkillConfirm_YES_003Ec__AnonStorey43B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
						{
							MigrationSkillData item = new MigrationSkillData(migrationSelectItem.uniqueID, toSlot, skillItem);
							list2.Add(item);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list.Add(skillItem);
				}
			}
		}
		migrationSendCount = list2.Count + list.Count;
		GameSection.SetEventData(new ChangeEquipData(selectEquipSetData.setNo, selectEquipSetData.index, migrationSelectItem));
		GameSection.StayEvent();
		this.StartCoroutine(SendReplacementSkill(list2, list));
	}

	private IEnumerator SendReplacementSkill(List<MigrationSkillData> migrationSkill, List<SkillItemInfo> detachSkill)
	{
		foreach (MigrationSkillData item in migrationSkill)
		{
			bool isSendFinish2 = false;
			MigrationSkillData i = item;
			MonoBehaviourSingleton<StatusManager>.I.SendSetSkill(i.toUniqueId, i.skill.uniqueID, i.toSlotNo, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo(), delegate(bool isSucces)
			{
				((_003CSendReplacementSkill_003Ec__Iterator136)/*Error near IL_00ac: stateMachine*/)._003C_003Ef__this.MigrationSkillCallback(isSucces);
				((_003CSendReplacementSkill_003Ec__Iterator136)/*Error near IL_00ac: stateMachine*/)._003CisSendFinish_003E__0 = true;
			});
			if (!isSendFinish2)
			{
				yield return (object)null;
			}
		}
		foreach (SkillItemInfo item2 in detachSkill)
		{
			bool isSendFinish2 = false;
			SkillItemInfo d = item2;
			EquipSetSkillData setInfo = d.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
			MonoBehaviourSingleton<StatusManager>.I.SendDetachSkill(setInfo.equipItemUniqId, setInfo.equipSlotNo, setInfo.equipSetNo, delegate(bool isSucces)
			{
				((_003CSendReplacementSkill_003Ec__Iterator136)/*Error near IL_01a9: stateMachine*/)._003C_003Ef__this.MigrationSkillCallback(isSucces);
				((_003CSendReplacementSkill_003Ec__Iterator136)/*Error near IL_01a9: stateMachine*/)._003CisSendFinish_003E__0 = true;
			});
			if (!isSendFinish2)
			{
				yield return (object)null;
			}
		}
	}

	protected virtual void OnQuery_StatusMigrationSkillConfirm_NO()
	{
		RequestRemoveAllSkillFromCurrentEquipment();
	}

	private void MigrationSkillCallback(bool result)
	{
		if (result)
		{
			migrationSendCount--;
			if (migrationSendCount == 0)
			{
				GameSection.ResumeEvent(true, null, false);
			}
		}
		else
		{
			GameSection.ResumeEvent(false, null, false);
		}
	}

	protected void RequestRemoveAllSkillFromCurrentEquipment()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		GameSection.SetEventData(new ChangeEquipData(selectEquipSetData.setNo, selectEquipSetData.index, migrationSelectItem));
		if (migrationOldItem != null)
		{
			int currentEquipSetNo = MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo();
			GameSection.StayEvent();
			this.StartCoroutine(SendRemoveAllSkill(migrationOldItem.uniqueID, currentEquipSetNo));
		}
	}

	private IEnumerator SendRemoveAllSkill(ulong _equipmentId, int _setNo)
	{
		bool isSendFinish = false;
		migrationSendCount = 1;
		MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkill(_equipmentId, _setNo, delegate(bool isSucces)
		{
			((_003CSendRemoveAllSkill_003Ec__Iterator137)/*Error near IL_0045: stateMachine*/)._003C_003Ef__this.MigrationSkillCallback(isSucces);
			((_003CSendRemoveAllSkill_003Ec__Iterator137)/*Error near IL_0045: stateMachine*/)._003CisSendFinish_003E__0 = true;
		});
		if (!isSendFinish)
		{
			yield return (object)null;
		}
	}
}
