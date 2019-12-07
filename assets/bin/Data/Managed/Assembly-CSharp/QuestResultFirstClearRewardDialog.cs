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
			int num = ++reward_num;
		}
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, reset: false, delegate(int i, Transform t, bool is_recycle)
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
						ItemSortData obj = itemData as ItemSortData;
						enemy_icon_id = obj.itemData.tableData.enemyIconID;
						enemy_icon_id2 = obj.itemData.tableData.enemyIconID2;
					}
					ItemIcon itemIcon = ItemIcon.Create(sell_data_ary[i].GetIconType(), sell_data_ary[i].GetIconID(), sell_data_ary[i].GetRarity(), t, sell_data_ary[i].GetIconElement(), sell_data_ary[i].GetIconMagiEnableType(), sell_data_ary[i].GetNum(), null, 0, is_new: false, -1, is_select: false, null, is_equipping: false, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, sell_data_ary[i].GetGetType());
					itemIcon.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon.transform, sell_data_ary[i].GetMaterialType(), sell_data_ary[i].GetTableID());
				}
				else
				{
					ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1u, t, crystalNum);
					itemIcon2.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon2.transform, REWARD_TYPE.CRYSTAL, 0u);
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
		return totalSell;
	}
}
