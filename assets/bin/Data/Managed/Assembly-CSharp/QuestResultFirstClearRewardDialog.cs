using System.Collections.Generic;
using UnityEngine;

public class QuestResultFirstClearRewardDialog : ItemSellConfirm
{
	private int totalSell;

	private int crystalNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalSell = (int)array[1];
		base.isRareConfirm = true;
		base.isHideMainText = true;
		base.isButtonSingle = true;
		base.Initialize();
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
				else
				{
					ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1u, t, crystalNum, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
					itemIcon2.SetRewardBG(true);
					SetMaterialInfo(itemIcon2.transform, REWARD_TYPE.CRYSTAL, 0u, null);
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
		return totalSell;
	}
}
