using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithEvolveSelectMaterialEquipItem : EquipSelectBase
{
	public new enum UI
	{
		OBJ_CAPTION_3,
		LBL_CAPTION,
		OBJ_INFO_ROOT,
		OBJ_ICON_ROOT,
		STR_TITLE,
		LBL_NAME,
		STR_SELL,
		LBL_SELL,
		SPR_COIN,
		SPR_SELL_BG,
		SPR_NEED,
		STR_NEED,
		LBL_NEED_LV,
		SPR_HAVE,
		STR_HAVE,
		LBL_HAVE_NUM,
		BTN_CHANGE,
		TGL_CHANGE_INVENTORY,
		LBL_ICON_DISP,
		SPR_SMALL_ICON,
		BTN_SORT,
		ICON_DESC,
		TGL_ICON_ASC,
		LBL_SORT,
		OBJ_EQUIP_WINDOW,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		OBJ_BACK,
		BTN_BACK
	}

	private uint equipId;

	private int needLv;

	private int equipIndex;

	private ulong[] selectedUniqueId;

	private EquipItemTable.EquipItemData data;

	private Transform detailBase;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		equipId = (uint)array[0];
		needLv = (int)array[1];
		selectedUniqueId = (ulong[])array[2];
		equipIndex = (int)array[3];
		data = Singleton<EquipItemTable>.I.GetEquipItemData(equipId);
		string caption = (!data.IsWeapon()) ? base.sectionData.GetText("CAPTION_DEFENCE") : base.sectionData.GetText("CAPTION_WEAPON");
		InitializeCaption(caption);
		base.Initialize();
	}

	protected override void OnOpen()
	{
		InitLocalInventory();
	}

	public override void UpdateUI()
	{
		detailBase = GetCtrl(UI.OBJ_INFO_ROOT);
		if (detailBase != null)
		{
			SetFontStyle(detailBase, UI.STR_TITLE, 2);
			SetFontStyle(detailBase, UI.STR_SELL, 2);
			SetFontStyle(detailBase, UI.STR_NEED, 2);
			SetFontStyle(detailBase, UI.STR_HAVE, 2);
			SetLabelText(detailBase, UI.LBL_NAME, data.name);
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = ItemIcon.GetItemIconType(data.type);
			itemIconCreateParam.icon_id = data.GetIconID();
			itemIconCreateParam.rarity = data.rarity;
			itemIconCreateParam.parent = FindCtrl(detailBase, UI.OBJ_ICON_ROOT);
			itemIconCreateParam.element = data.GetTargetElementPriorityToTable();
			ItemIcon itemIcon = ItemIcon.Create(itemIconCreateParam);
			itemIcon.SetEnableCollider(is_enable: false);
			SetLabelText(detailBase, UI.LBL_NEED_LV, needLv.ToString());
			SetLabelText(detailBase, UI.LBL_HAVE_NUM, MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNum(equipId).ToString());
			SetLabelText(detailBase, UI.LBL_SELL, data.sale.ToString());
		}
		LocalInventory();
	}

	private void InitializeCaption(string caption)
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		if (ctrl == null)
		{
			return;
		}
		SetLabelText(ctrl, UI.LBL_CAPTION, caption);
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play();
		}
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
		List<EquipItemInfo> inventory = new List<EquipItemInfo>();
		MonoBehaviourSingleton<InventoryManager>.I.ForAllEquipItemInventory(delegate(EquipItemInfo item)
		{
			for (int i = 0; i < selectedUniqueId.Length; i++)
			{
				if (selectedUniqueId[i] == item.uniqueID)
				{
					return;
				}
			}
			if (item.tableID == equipId)
			{
				inventory.Add(item);
			}
		});
		localInventoryEquipData = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(inventory.ToArray());
	}

	protected override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			SetDynamicList((Enum)InventoryUI, (string)null, localInventoryEquipData.Length + 1, reset: false, (Func<int, bool>)delegate(int i)
			{
				if (i == 0)
				{
					return true;
				}
				int num2 = i - 1;
				SortCompareData sortCompareData = localInventoryEquipData[num2];
				if (sortCompareData == null || !sortCompareData.IsPriority(sortSettings.orderTypeAsc))
				{
					return false;
				}
				return true;
			}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				if (i == 0)
				{
					CreateRemoveIcon(t, "SELECT", -1, -1, selectInventoryIndex == -1, base.sectionData.GetText("STR_DETACH"));
				}
				else
				{
					int num = i - 1;
					uint tableID = localInventoryEquipData[num].GetTableID();
					if (tableID == 0)
					{
						SetActive(t, is_visible: false);
					}
					else
					{
						SetActive(t, is_visible: true);
						EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableID);
						EquipItemSortData equipItemSortData = localInventoryEquipData[num] as EquipItemSortData;
						EquipItemInfo equipItemInfo = equipItemSortData.GetItemData() as EquipItemInfo;
						ITEM_ICON_TYPE iconType = equipItemSortData.GetIconType();
						bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, equipItemSortData.GetUniqID());
						SkillSlotUIData[] skillSlotData = GetSkillSlotData(equipItemInfo);
						int equip_index = (!equipItemSortData.IsEquipping()) ? (-1) : 0;
						ItemIcon itemIcon = CreateItemIconDetail(equipItemSortData, skillSlotData, base.IsShowMainStatus, t, "SELECT", i - 1, ItemIconDetail.ICON_STATUS.NONE, is_new, -1, is_select: false, equip_index);
						itemIcon.SetItemID(equipItemSortData.GetTableID());
						itemIcon.SetGrayout(equipItemInfo.level < needLv);
						object[] event_data = new object[2]
						{
							ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE,
							equipItemInfo
						};
						SetLongTouch(itemIcon.transform, "DETAIL", event_data);
						if (itemIcon != null && equipItemSortData != null)
						{
							itemIcon.SetInitData(equipItemSortData);
						}
						if (itemIcon != null && !m_generatedIconList.Contains(itemIcon))
						{
							m_generatedIconList.Add(itemIcon);
						}
					}
				}
			});
		}
	}

	protected override void EquipParam()
	{
	}

	protected override int GetSelectItemIndex()
	{
		for (int i = 0; i < localInventoryEquipData.Length; i++)
		{
			if (selectedUniqueId[equipIndex] == localInventoryEquipData[i].GetUniqID())
			{
				return i;
			}
		}
		return -1;
	}

	private void OnQuery_SECTION_BACK()
	{
		GameSection.SetEventData(selectedUniqueId);
	}

	private void OnQuery_SELECT()
	{
		selectInventoryIndex = (int)GameSection.GetEventData();
		if (selectInventoryIndex == -1)
		{
			selectedUniqueId[equipIndex] = 0uL;
		}
		else
		{
			EquipItemSortData equipItemSortData = localInventoryEquipData[selectInventoryIndex] as EquipItemSortData;
			if (equipItemSortData.IsFavorite())
			{
				GameSection.ChangeEvent("NOT_SELECT_FAVORITE");
				return;
			}
			if (equipItemSortData.IsEquipping())
			{
				GameSection.ChangeEvent("NOT_SELECT_EQUIPPING");
				return;
			}
			if (equipItemSortData.GetLevel() < needLv)
			{
				GameSection.ChangeEvent("NOT_SELECT_LOW_LEVEL");
				return;
			}
			selectedUniqueId[equipIndex] = equipItemSortData.GetUniqID();
		}
		GameSection.SetEventData(selectedUniqueId);
	}

	protected override void OnQueryDetail()
	{
	}

	protected override bool sorting()
	{
		InitLocalInventory();
		return true;
	}

	protected void OnCloseDialog_SmithSelectEquipSort()
	{
		OnCloseSortDialog();
	}
}
