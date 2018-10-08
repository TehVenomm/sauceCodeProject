using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithExceedDialog : GameSection
{
	private enum UI
	{
		SPR_COUNT_0_ON,
		SPR_COUNT_1_ON,
		SPR_COUNT_2_ON,
		SPR_COUNT_3_ON,
		LBL_EXCEED_0,
		LBL_EXCEED_1,
		LBL_EXCEED_2,
		LBL_EXCEED_3,
		SPR_EXCEED_0_ON,
		SPR_EXCEED_1_ON,
		SPR_EXCEED_2_ON,
		SPR_EXCEED_3_ON,
		LBL_USE_MATERIAL_NAME,
		SCR_LAPIS_ROOT,
		GRD_LAPIS,
		OBJ_VALID_EXCEED_ROOT,
		OBJ_INVALID_EXCEED_ROOT,
		LBL_MAX_COUNT,
		OBJ_MATERIAL_ICON_ROOT,
		SPR_EXCEED_BTN_BG,
		SPR_EXCEED_GRAYOUT,
		OBJ_SELECT,
		BTN_AIM_R,
		BTN_AIM_L,
		BTN_AIM_R_INACTIVE,
		BTN_AIM_L_INACTIVE,
		LBL_SELECT_NOW,
		LBL_SELECT_MAX,
		OBJ_EXCEED_LIMITED,
		LBL_LIMITED
	}

	private const int MAX_SHOW_EXCEED_NUM = 3;

	protected int exceedCount;

	protected EquipItemTable.EquipItemData itemTable;

	private EquipItemExceedTable.EquipItemExceedData exceedData;

	private SmithManager.SmithGrowData smithData;

	private int selectIndex;

	private Color paramColor = Color.get_white();

	private int selectPageIndex;

	private int maxPageIndex;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "EquipItemExceedTable";
			yield return "LimitedEquipItemExceedTable";
		}
	}

	protected virtual bool IsValidExceedSection()
	{
		return true;
	}

	protected virtual void SetupExceedData()
	{
		exceedCount = smithData.selectEquipData.exceed;
		itemTable = smithData.selectEquipData.tableData;
	}

	public override void Initialize()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		SetupExceedData();
		exceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedDataIncludeLimited(itemTable);
		selectPageIndex = 0;
		int num = Mathf.CeilToInt((float)exceedData.exceed.Length / 3f);
		maxPageIndex = num - 1;
		SetLabelText((Enum)UI.LBL_SELECT_MAX, num.ToString());
		SetLabelText((Enum)UI.LBL_SELECT_NOW, (selectPageIndex + 1).ToString());
		if (num == 0)
		{
			SetActive((Enum)UI.OBJ_SELECT, false);
		}
		else if (num <= 1)
		{
			SetActive((Enum)UI.BTN_AIM_R, false);
			SetActive((Enum)UI.BTN_AIM_L, false);
		}
		else
		{
			SetActive((Enum)UI.BTN_AIM_R_INACTIVE, false);
			SetActive((Enum)UI.BTN_AIM_L_INACTIVE, false);
		}
		UILabel component = base.GetComponent<UILabel>((Enum)UI.LBL_EXCEED_0);
		if (component != null)
		{
			paramColor = component.color;
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		base.UpdateUI();
		SetActive((Enum)UI.SPR_COUNT_0_ON, exceedCount > 0);
		SetActive((Enum)UI.SPR_COUNT_1_ON, exceedCount > 1);
		SetActive((Enum)UI.SPR_COUNT_2_ON, exceedCount > 2);
		SetActive((Enum)UI.SPR_COUNT_3_ON, exceedCount > 3);
		SetLabelText((Enum)UI.LBL_SELECT_NOW, (selectPageIndex + 1).ToString());
		UpdateBonusDetail(true);
		bool is_only_lapis = true;
		int item_num = exceedData.exceed.Length;
		_003CUpdateUI_003Ec__AnonStorey3D9 _003CUpdateUI_003Ec__AnonStorey3D;
		SetGrid(UI.GRD_LAPIS, "SmithExceedItem", item_num, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		bool flag = exceedCount < 4;
		SetActive((Enum)UI.OBJ_VALID_EXCEED_ROOT, flag);
		SetActive((Enum)UI.OBJ_INVALID_EXCEED_ROOT, !flag);
		int id = (!is_only_lapis) ? 6 : 5;
		string text = StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, (uint)id);
		SetLabelText((Enum)UI.LBL_USE_MATERIAL_NAME, text);
	}

	protected void UpdateBonusDetail(bool changeNextColor = true)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.SPR_EXCEED_0_ON, exceedCount > 0);
		SetActive((Enum)UI.SPR_EXCEED_1_ON, exceedCount > 1);
		SetActive((Enum)UI.SPR_EXCEED_2_ON, exceedCount > 2);
		SetActive((Enum)UI.SPR_EXCEED_3_ON, exceedCount > 3);
		EquipItemTable.EquipItemData equipItemData = itemTable;
		Color color = (!changeNextColor || exceedCount != 0) ? paramColor : Color.get_yellow();
		SetLabelText((Enum)UI.LBL_EXCEED_0, equipItemData.GetExceedParamName(1));
		SetColor((Enum)UI.LBL_EXCEED_0, color);
		color = ((!changeNextColor || exceedCount != 1) ? paramColor : Color.get_yellow());
		SetLabelText((Enum)UI.LBL_EXCEED_1, equipItemData.GetExceedParamName(2));
		SetColor((Enum)UI.LBL_EXCEED_1, color);
		color = ((!changeNextColor || exceedCount != 2) ? paramColor : Color.get_yellow());
		SetLabelText((Enum)UI.LBL_EXCEED_2, equipItemData.GetExceedParamName(3));
		SetColor((Enum)UI.LBL_EXCEED_2, color);
		color = ((!changeNextColor || exceedCount != 3) ? paramColor : Color.get_yellow());
		SetLabelText((Enum)UI.LBL_EXCEED_3, equipItemData.GetExceedParamName(4));
		SetColor((Enum)UI.LBL_EXCEED_3, color);
	}

	private void OnQuery_NEXT()
	{
		if (smithData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			selectIndex = (int)GameSection.GetEventData();
			int exceed = smithData.selectEquipData.exceed;
			int needNum = (int)exceedData.exceed[selectIndex].getNeedNum(exceed + 1);
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(exceedData.exceed[selectIndex].itemId);
			GameSection.SetEventData(new object[3]
			{
				itemData.name,
				needNum,
				smithData.selectEquipData.tableData.name
			});
		}
	}

	private void OnQuery_NEED()
	{
		if (smithData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			int num = (int)GameSection.GetEventData();
			int exceed = smithData.selectEquipData.exceed;
			int needNum = (int)exceedData.exceed[num].getNeedNum(exceed + 1);
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(exceedData.exceed[num].itemId);
			uint id = 7u;
			if (itemData.type == ITEM_TYPE.LAPIS)
			{
				id = (uint)((!Singleton<EquipItemExceedTable>.I.IsFreeLapis(itemData.rarity, itemData.id, itemData.eventId)) ? 4 : 3);
				if (Singleton<LimitedEquipItemExceedTable>.I.IsLimitedLapis(itemData.id))
				{
					id = 8u;
				}
			}
			string text = string.Format(StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, id), itemData.rarity.ToString());
			GameSection.SetEventData(new object[4]
			{
				itemData.name,
				needNum,
				smithData.selectEquipData.tableData.name,
				text
			});
		}
	}

	private unsafe void OnQuery_SmithExceedConfirm_YES()
	{
		if (smithData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			EquipItemInfo selectEquipData = smithData.selectEquipData;
			if (selectEquipData == null)
			{
				GameSection.StopEvent();
			}
			else
			{
				SmithManager.ResultData result_data = new SmithManager.ResultData();
				result_data.beforeRarity = (int)selectEquipData.tableData.rarity;
				result_data.beforeLevel = selectEquipData.level;
				result_data.beforeMaxLevel = selectEquipData.tableData.maxLv;
				result_data.beforeExceedCnt = selectEquipData.exceed;
				result_data.beforeAtk = selectEquipData.atk;
				result_data.beforeDef = selectEquipData.def;
				result_data.beforeHp = selectEquipData.hp;
				result_data.beforeElemAtk = selectEquipData.elemAtk;
				result_data.beforeElemDef = selectEquipData.elemDef;
				result_data.isExceed = true;
				EquipItemExceedTable.EquipItemExceedData equipItemExceedDataIncludeLimited = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedDataIncludeLimited(selectEquipData.tableData);
				if (equipItemExceedDataIncludeLimited == null || equipItemExceedDataIncludeLimited.exceed.Length - 1 < selectIndex)
				{
					GameSection.StopEvent();
				}
				else
				{
					uint itemId = equipItemExceedDataIncludeLimited.exceed[selectIndex].itemId;
					GameSection.StayEvent();
					_003COnQuery_SmithExceedConfirm_YES_003Ec__AnonStorey3DA _003COnQuery_SmithExceedConfirm_YES_003Ec__AnonStorey3DA;
					MonoBehaviourSingleton<SmithManager>.I.SendExceedEquipItem(selectEquipData.uniqueID, itemId, new Action<Error, EquipItemInfo>((object)_003COnQuery_SmithExceedConfirm_YES_003Ec__AnonStorey3DA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
			}
		}
	}

	private void OnQuery_SmithExceedConfirm_NO()
	{
	}

	private void OnQuery_AIM_R()
	{
		if (selectPageIndex >= maxPageIndex)
		{
			selectPageIndex = 0;
		}
		else
		{
			selectPageIndex++;
		}
		RefreshUI();
	}

	private void OnQuery_AIM_L()
	{
		if (selectPageIndex <= 0)
		{
			selectPageIndex = maxPageIndex;
		}
		else
		{
			selectPageIndex--;
		}
		RefreshUI();
	}
}
