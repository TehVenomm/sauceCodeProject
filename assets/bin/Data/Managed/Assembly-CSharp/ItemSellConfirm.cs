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
		if (sellData != null)
		{
			SetLabelText(UI.STR_TITLE_R, base.sectionData.GetText("STR_TITLE"));
			SetActive(UI.STR_INCLUDE_RARE, isRareConfirm);
			SetActive(UI.STR_MAIN_TEXT, !isHideMainText);
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
			SetActive(UI.OBJ_GOLD, num != 0);
			SetLabelText(UI.LBL_TOTAL, num.ToString());
			if (isButtonSingle)
			{
				SetActive(UI.BTN_CENTER, true);
				SetActive(UI.BTN_0, false);
				SetActive(UI.BTN_1, false);
			}
			else
			{
				SetActive(UI.BTN_CENTER, false);
				SetActive(UI.BTN_0, true);
				SetActive(UI.BTN_1, true);
			}
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
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < sell_data_ary.Length)
			{
				int enemy_icon_id = 0;
				int enemy_icon_id2 = 0;
				bool is_equipping = false;
				SortCompareData sortCompareData = sell_data_ary[i];
				if (sortCompareData is ItemSortData)
				{
					ItemSortData itemSortData = sortCompareData as ItemSortData;
					enemy_icon_id = itemSortData.itemData.tableData.enemyIconID;
					enemy_icon_id2 = itemSortData.itemData.tableData.enemyIconID2;
				}
				else if (sortCompareData is SkillItemSortData)
				{
					SkillItemSortData skillItemSortData = sortCompareData as SkillItemSortData;
					is_equipping = skillItemSortData.IsEquipping();
				}
				else if (sortCompareData is AbilityItemSortData)
				{
					enemy_icon_id = (sortCompareData as AbilityItemSortData).itemData.GetItemTableData().enemyIconID;
					enemy_icon_id2 = (sortCompareData as AbilityItemSortData).itemData.GetItemTableData().enemyIconID2;
				}
				GET_TYPE getType = sell_data_ary[i].GetGetType();
				ItemIcon itemIcon = ItemIcon.Create(sell_data_ary[i].GetIconType(), sell_data_ary[i].GetIconID(), sell_data_ary[i].GetRarity(), t, sell_data_ary[i].GetIconElement(), sell_data_ary[i].GetIconMagiEnableType(), GetTargetIconNum(sell_data_ary, i), null, 0, false, -1, false, null, is_equipping, enemy_icon_id, enemy_icon_id2, false, getType, sell_data_ary[i].GetIconElementSub());
				itemIcon.SetRewardBG(isShowIconBG());
				Transform ctrl = GetCtrl(UI.SCR_ICON);
				SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID(), ctrl);
			}
			else
			{
				SetActive(t, false);
			}
		});
	}

	protected virtual int GetTargetIconNum(SortCompareData[] sell_data_ary, int i)
	{
		SortCompareData sortCompareData = sell_data_ary[i];
		return sortCompareData.GetNum();
	}
}
