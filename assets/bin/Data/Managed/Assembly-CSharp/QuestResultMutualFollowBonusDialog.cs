using System.Collections.Generic;
using UnityEngine;

public class QuestResultMutualFollowBonusDialog : ItemSellConfirm
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
		GRD_REWARD_ICON
	}

	private int totalGold;

	private int crystalNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalGold = (int)array[1];
		crystalNum = (int)array[2];
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
		SortCompareData[] itemData = sellData.ToArray();
		int reward_num = itemData.Length;
		if (crystalNum > 0)
		{
			int num = ++reward_num;
		}
		if (totalGold > 0)
		{
			int num = ++reward_num;
		}
		bool shouldAddGold = totalGold > 0;
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < reward_num)
			{
				if (i < itemData.Length)
				{
					int enemy_icon_id = 0;
					int enemy_icon_id2 = 0;
					object itemData2 = itemData[i].GetItemData();
					if (itemData2 is ItemSortData)
					{
						ItemSortData obj = itemData2 as ItemSortData;
						enemy_icon_id = obj.itemData.tableData.enemyIconID;
						enemy_icon_id2 = obj.itemData.tableData.enemyIconID2;
					}
					ItemIcon itemIcon = null;
					itemIcon = ((itemData[i].GetIconType() != ITEM_ICON_TYPE.QUEST_ITEM) ? ItemIcon.Create(itemData[i].GetIconType(), itemData[i].GetIconID(), itemData[i].GetRarity(), t, itemData[i].GetIconElement(), itemData[i].GetIconMagiEnableType(), itemData[i].GetNum(), null, 0, is_new: false, -1, is_select: false, null, is_equipping: false, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, itemData[i].GetGetType()) : ItemIcon.Create(new ItemIcon.ItemIconCreateParam
					{
						icon_type = itemData[i].GetIconType(),
						icon_id = itemData[i].GetIconID(),
						rarity = itemData[i].GetRarity(),
						parent = t,
						element = itemData[i].GetIconElement(),
						magi_enable_equip_type = itemData[i].GetIconMagiEnableType(),
						num = itemData[i].GetNum(),
						enemy_icon_id = enemy_icon_id,
						enemy_icon_id2 = enemy_icon_id2,
						questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
					}));
					itemIcon.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon.transform, itemData[i].GetMaterialType(), itemData[i].GetTableID());
				}
				else if (shouldAddGold)
				{
					ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1u, t, totalGold);
					itemIcon2.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon2.transform, REWARD_TYPE.MONEY, 0u);
					shouldAddGold = false;
				}
				else
				{
					ItemIcon itemIcon3 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1u, t, crystalNum);
					itemIcon3.SetRewardBG(is_visible: true);
					SetMaterialInfo(itemIcon3.transform, REWARD_TYPE.CRYSTAL, 0u);
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
