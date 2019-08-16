using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultMissionClearRewardDialog : ItemSellConfirm
{
	public new enum UI
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
		GRD_REWARD_ICON,
		OBJ_COMPLETE_ROOT,
		OBJ_NORMAL_ROOT
	}

	private int totalGold;

	private int crystalNum;

	private bool isComplete;

	private PointShopResultData missionPointData;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalGold = (int)array[1];
		crystalNum = (int)array[2];
		isComplete = (bool)array[3];
		missionPointData = (PointShopResultData)array[4];
		base.isRareConfirm = true;
		base.isHideMainText = true;
		base.isButtonSingle = true;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive((Enum)UI.OBJ_NORMAL_ROOT, !isComplete);
		SetActive((Enum)UI.OBJ_COMPLETE_ROOT, isComplete);
	}

	private void OnQuery_OK()
	{
		GameSection.BackSection();
	}

	protected override void DrawIcon()
	{
		SortCompareData[] sell_data_ary = sellData.ToArray();
		int reward_num = sell_data_ary.Length;
		if (crystalNum > 0)
		{
			reward_num++;
		}
		if (totalGold > 0)
		{
			reward_num++;
		}
		if (missionPointData != null && missionPointData.missionPoint > 0)
		{
			reward_num++;
		}
		bool shouldAddGold = totalGold > 0;
		bool shouldAddMissionPoint = missionPointData != null && missionPointData.missionPoint > 0;
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < reward_num)
			{
				if (i < sell_data_ary.Length)
				{
					int num = 0;
					int num2 = 0;
					object itemData = sell_data_ary[i].GetItemData();
					if (itemData is ItemSortData)
					{
						ItemSortData itemSortData = itemData as ItemSortData;
						num = itemSortData.itemData.tableData.enemyIconID;
						num2 = itemSortData.itemData.tableData.enemyIconID2;
					}
					ITEM_ICON_TYPE iconType = sell_data_ary[i].GetIconType();
					int iconID = sell_data_ary[i].GetIconID();
					RARITY_TYPE? rarity = sell_data_ary[i].GetRarity();
					ELEMENT_TYPE iconElement = sell_data_ary[i].GetIconElement();
					EQUIPMENT_TYPE? iconMagiEnableType = sell_data_ary[i].GetIconMagiEnableType();
					int num3 = sell_data_ary[i].GetNum();
					string event_name = null;
					int event_data = 0;
					bool is_new = false;
					int toggle_group = -1;
					bool is_select = false;
					string icon_under_text = null;
					bool is_equipping = false;
					int enemy_icon_id = num;
					int enemy_icon_id2 = num2;
					GET_TYPE getType = sell_data_ary[i].GetGetType();
					ItemIcon itemIcon = ItemIcon.Create(iconType, iconID, rarity, t, iconElement, iconMagiEnableType, num3, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType);
					itemIcon.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID());
				}
				else if (shouldAddGold)
				{
					ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1u, t, totalGold);
					itemIcon2.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon2.transform, REWARD_TYPE.MONEY, 0u);
					shouldAddGold = false;
				}
				else if (shouldAddMissionPoint)
				{
					ItemIcon.GetIconShowData(REWARD_TYPE.POINT_SHOP_POINT, (uint)missionPointData.pointShopId, out int icon_id, out ITEM_ICON_TYPE icon_type, out RARITY_TYPE? rarity2, out ELEMENT_TYPE element, out ELEMENT_TYPE _, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _);
					ItemIcon itemIcon3 = ItemIcon.Create(icon_type, icon_id, rarity2, t, element, null, missionPointData.missionPoint);
					itemIcon3.SetRewardBG(is_visible: true);
					int id = (!missionPointData.isEvent) ? 1 : 0;
					SetMaterialInfo(itemIcon3.transform, REWARD_TYPE.POINT_SHOP_POINT, (uint)id);
					shouldAddMissionPoint = false;
				}
				else
				{
					ItemIcon itemIcon4 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1u, t, crystalNum);
					itemIcon4.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon4.transform, REWARD_TYPE.CRYSTAL, 0u);
				}
			}
			else
			{
				SetActive(t, is_visible: false);
			}
		});
	}

	protected override int GetSellGold()
	{
		return totalGold;
	}
}
