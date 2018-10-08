using Network;
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
		SetActive(UI.OBJ_NORMAL_ROOT, !isComplete);
		SetActive(UI.OBJ_COMPLETE_ROOT, isComplete);
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
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < reward_num)
			{
				if (i < sell_data_ary.Length)
				{
					int enemy_icon_id = 0;
					int enemy_icon_id2 = 0;
					object itemData = sell_data_ary[i].GetItemData();
					if (itemData is ItemSortData)
					{
						ItemSortData itemSortData = itemData as ItemSortData;
						enemy_icon_id = itemSortData.itemData.tableData.enemyIconID;
						enemy_icon_id2 = itemSortData.itemData.tableData.enemyIconID2;
					}
					GET_TYPE getType = sell_data_ary[i].GetGetType();
					ItemIcon itemIcon = ItemIcon.Create(sell_data_ary[i].GetIconType(), sell_data_ary[i].GetIconID(), sell_data_ary[i].GetRarity(), t, sell_data_ary[i].GetIconElement(), sell_data_ary[i].GetIconMagiEnableType(), sell_data_ary[i].GetNum(), null, 0, false, -1, false, null, false, enemy_icon_id, enemy_icon_id2, false, getType, ELEMENT_TYPE.MAX);
					itemIcon.SetRewardBG(true);
					SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID(), null);
				}
				else if (shouldAddGold)
				{
					ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1u, t, totalGold, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
					itemIcon2.SetRewardBG(true);
					SetMaterialInfo(itemIcon2.transform, REWARD_TYPE.MONEY, 0u, null);
					shouldAddGold = false;
				}
				else if (shouldAddMissionPoint)
				{
					ItemIcon.GetIconShowData(REWARD_TYPE.POINT_SHOP_POINT, (uint)missionPointData.pointShopId, out int icon_id, out ITEM_ICON_TYPE icon_type, out RARITY_TYPE? rarity, out ELEMENT_TYPE element, out ELEMENT_TYPE _, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _, 0);
					ItemIcon itemIcon3 = ItemIcon.Create(icon_type, icon_id, rarity, t, element, null, missionPointData.missionPoint, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
					itemIcon3.SetRewardBG(true);
					int id = (!missionPointData.isEvent) ? 1 : 0;
					SetMaterialInfo(itemIcon3.transform, REWARD_TYPE.POINT_SHOP_POINT, (uint)id, null);
					shouldAddMissionPoint = false;
				}
				else
				{
					ItemIcon itemIcon4 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1u, t, crystalNum, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
					itemIcon4.SetRewardBG(true);
					SetMaterialInfo(itemIcon4.transform, REWARD_TYPE.CRYSTAL, 0u, null);
				}
			}
			else
			{
				SetActive(t, false);
			}
		});
	}

	protected override int GetSellGold()
	{
		return totalGold;
	}
}
