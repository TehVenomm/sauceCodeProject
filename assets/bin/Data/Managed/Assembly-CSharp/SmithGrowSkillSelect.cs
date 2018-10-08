using System;
using UnityEngine;

public class SmithGrowSkillSelect : SkillSelectBaseSecond
{
	protected new enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_SELL,
		LBL_DESCRIPTION,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_SUB_STATUS,
		SPR_SKILL_TYPE_ICON,
		SPR_SKILL_TYPE_ICON_BG,
		SPR_SKILL_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_DESCRIPTION,
		STR_TITLE_STATUS,
		STR_TITLE_SELL,
		PRG_EXP_BAR,
		OBJ_NEXT_EXP_ROOT,
		BTN_DECISION,
		STR_DECISION_R,
		BTN_SKILL_DECISION,
		STR_SKILL_DECISION,
		STR_SKILL_DECISION_R,
		OBJ_SKILL_INFO_ROOT,
		LBL_EQUIP_ITEM_NAME,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		LBL_SORT,
		BTN_BACK,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		BTN_CHANGE_INVENTORY,
		OBJ_EMPTY_SKILL_ROOT,
		TEX_EMPTY_SKILL,
		SPR_EMPTY_SKILL,
		LBL_EMPTY_SKILL_TYPE,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	private int preInventoryDataSize;

	public override void Initialize()
	{
		GameSection.SetEventData(new object[3]
		{
			ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
			null,
			null
		});
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetVisibleEmptySkillType(false, 0);
		SetActive((Enum)UI.BTN_DECISION, false);
		SetActive((Enum)UI.BTN_SKILL_DECISION, true);
		SetLabelText((Enum)UI.STR_SKILL_DECISION, base.sectionData.GetText("STR_DECISION"));
		SetLabelText((Enum)UI.STR_SKILL_DECISION_R, base.sectionData.GetText("STR_DECISION"));
	}

	protected override void SetInventoryIsEmptyParam()
	{
		isVisibleEmptySkill = false;
	}

	protected override ItemStorageTop.SkillItemInventory CreateInventory()
	{
		return new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.GROW_BASE_SKILL_ITEM, SKILL_SLOT_TYPE.NONE, false);
	}

	protected override void OnOpen()
	{
		SetEnabledUIModelRenderTexture(UI.TEX_MODEL, true);
		SetEnabledUIModelRenderTexture(UI.TEX_INNER_MODEL, true);
		base.OnOpen();
	}

	protected override void OnClose()
	{
		SetEnabledUIModelRenderTexture(UI.TEX_MODEL, false);
		SetEnabledUIModelRenderTexture(UI.TEX_INNER_MODEL, false);
		base.OnClose();
	}

	protected unsafe override void UpdateInventoryUI()
	{
		SetupEnableInventoryUI();
		bool reset = false;
		int num = inventory.datas.Length;
		if (preInventoryDataSize != num)
		{
			reset = true;
			preInventoryDataSize = num;
		}
		m_generatedIconList.Clear();
		UpdateNewIconInfo();
		SetDynamicList((Enum)inventoryUI, (string)null, num, reset, new Func<int, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void SetEnabledUIModelRenderTexture(Enum ctrl_enum, bool enabled)
	{
		Transform ctrl = GetCtrl(ctrl_enum);
		if (Object.op_Implicit(ctrl))
		{
			UIModelRenderTexture component = ctrl.GetComponent<UIModelRenderTexture>();
			if (Object.op_Implicit(component))
			{
				component.set_enabled(enabled);
				if (!enabled)
				{
					component.Clear();
				}
			}
		}
	}

	protected override void OnDecision()
	{
		SkillItemInfo skillItemInfo = inventory.datas[selectIndex].GetItemData() as SkillItemInfo;
		if (skillItemInfo.IsLevelMax() && !skillItemInfo.IsExistNextExceed())
		{
			Log.Error(LOG.GAMESCENE, "level max && max exceed");
		}
		GameSection.ChangeEvent("DECISION", new object[2]
		{
			inventory.datas[selectIndex].GetItemData(),
			null
		});
	}

	protected override object[] CreateDetailEventData(int index)
	{
		return new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
			inventory.datas[index]
		};
	}

	private void OnCloseDialog_SmithSort()
	{
		OnCloseSort();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & (NOTIFY_FLAG.UPDATE_SKILL_GROW | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (NOTIFY_FLAG)0L)
		{
			equipSkillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(inventory.datas[selectIndex].GetUniqID());
			selectSkillItem = null;
			updateInventory = true;
			MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate = true;
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_SKILL_GROW | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "SELECT")
		{
			ulong num = (ulong)event_data;
			int i = 0;
			for (int num2 = inventory.datas.Length; i < num2; i++)
			{
				if (inventory.datas[i].GetUniqID() == num)
				{
					return new EventData(event_name, i);
				}
			}
		}
		return base.CheckAutoEvent(event_name, event_data);
	}
}
