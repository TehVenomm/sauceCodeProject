using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSellConfirm : GameSection
{
	public enum UI
	{
		STR_INCLUDE_RARE,
		STR_MAIN_TEXT,
		STR_TITLE_R,
		GRD_ICON,
		LBL_TOTAL,
		OBJ_GOLD,
		BTN_0,
		BTN_1,
		BTN_CENTER,
		SCR_ICON,
		GRD_REWARD_ICON
	}

	protected List<SortCompareData> sellData;

	protected bool isRareConfirm
	{
		get;
		set;
	}

	protected bool isEquipConfirm
	{
		get;
		set;
	}

	protected bool isExceedConfirm
	{
		get;
		set;
	}

	protected bool isExceedEquipmentConfirm
	{
		get;
		set;
	}

	protected bool isHideMainText
	{
		get;
		set;
	}

	protected bool isButtonSingle
	{
		get;
		set;
	}

	protected virtual bool isShowIconBG()
	{
		return true;
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (sellData == null)
		{
			return;
		}
		SetLabelText((Enum)UI.STR_TITLE_R, base.sectionData.GetText("STR_TITLE"));
		SetActive((Enum)UI.STR_INCLUDE_RARE, isRareConfirm);
		SetActive((Enum)UI.STR_MAIN_TEXT, !isHideMainText);
		DrawIcon();
		SortCompareData[] array = sellData.ToArray();
		int num = GetSellGold();
		if (num == 0)
		{
			int i = 0;
			for (int num2 = array.Length; i < num2; i++)
			{
				num += array[i].GetSalePrice();
			}
		}
		SetActive((Enum)UI.OBJ_GOLD, num != 0);
		SetLabelText((Enum)UI.LBL_TOTAL, num.ToString());
		if (isButtonSingle)
		{
			SetActive((Enum)UI.BTN_CENTER, is_visible: true);
			SetActive((Enum)UI.BTN_0, is_visible: false);
			SetActive((Enum)UI.BTN_1, is_visible: false);
		}
		else
		{
			SetActive((Enum)UI.BTN_CENTER, is_visible: false);
			SetActive((Enum)UI.BTN_0, is_visible: true);
			SetActive((Enum)UI.BTN_1, is_visible: true);
		}
	}

	protected virtual int GetSellGold()
	{
		return 0;
	}

	protected virtual void DrawIcon()
	{
		SortCompareData[] sell_data_ary = sellData.ToArray();
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < sell_data_ary.Length)
			{
				int num = 0;
				int num2 = 0;
				bool flag = false;
				SortCompareData sortCompareData = sell_data_ary[i];
				if (sortCompareData is ItemSortData)
				{
					ItemSortData itemSortData = sortCompareData as ItemSortData;
					num = itemSortData.itemData.tableData.enemyIconID;
					num2 = itemSortData.itemData.tableData.enemyIconID2;
				}
				else if (sortCompareData is SkillItemSortData)
				{
					SkillItemSortData skillItemSortData = sortCompareData as SkillItemSortData;
					flag = skillItemSortData.IsEquipping();
				}
				else if (sortCompareData is AbilityItemSortData)
				{
					num = (sortCompareData as AbilityItemSortData).itemData.GetItemTableData().enemyIconID;
					num2 = (sortCompareData as AbilityItemSortData).itemData.GetItemTableData().enemyIconID2;
				}
				ITEM_ICON_TYPE iconType = sell_data_ary[i].GetIconType();
				int iconID = sell_data_ary[i].GetIconID();
				RARITY_TYPE? rarity = sell_data_ary[i].GetRarity();
				ELEMENT_TYPE iconElement = sell_data_ary[i].GetIconElement();
				EQUIPMENT_TYPE? iconMagiEnableType = sell_data_ary[i].GetIconMagiEnableType();
				int targetIconNum = GetTargetIconNum(sell_data_ary, i);
				string event_name = null;
				int event_data = 0;
				bool is_new = false;
				int toggle_group = -1;
				bool is_select = false;
				string icon_under_text = null;
				bool is_equipping = flag;
				int enemy_icon_id = num;
				int enemy_icon_id2 = num2;
				GET_TYPE getType = sell_data_ary[i].GetGetType();
				ItemIcon itemIcon = ItemIcon.Create(iconType, iconID, rarity, t, iconElement, iconMagiEnableType, targetIconNum, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType, sell_data_ary[i].GetIconElementSub());
				itemIcon.SetRewardBG(isShowIconBG());
				Transform ctrl = GetCtrl(UI.SCR_ICON);
				SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID(), ctrl);
			}
			else
			{
				SetActive(t, is_visible: false);
			}
		});
	}

	protected virtual int GetTargetIconNum(SortCompareData[] sell_data_ary, int i)
	{
		SortCompareData sortCompareData = sell_data_ary[i];
		return sortCompareData.GetNum();
	}
}
