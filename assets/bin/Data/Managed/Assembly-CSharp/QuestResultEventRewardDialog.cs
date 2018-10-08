using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultEventRewardDialog : ItemSellConfirm
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
		LBL_REWARD_TITLE,
		GRD_EVENT_REWARD,
		OBJ_SCROLL_VIEW
	}

	private int totalGold;

	private int crystalNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalGold = (int)array[1];
		crystalNum = (int)array[2];
		SetLabelText((Enum)UI.LBL_REWARD_TITLE, (string)array[3]);
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
			reward_num++;
		}
		if (totalGold > 0)
		{
			reward_num++;
		}
		bool shouldAddGold = totalGold > 0;
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_EVENT_REWARD, null, sELL_SELECT_MAX, false, delegate(int i, Transform t, bool is_recycle)
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
						ItemSortData itemSortData = itemData2 as ItemSortData;
						enemy_icon_id = itemSortData.itemData.tableData.enemyIconID;
						enemy_icon_id2 = itemSortData.itemData.tableData.enemyIconID2;
					}
					ItemIcon itemIcon = null;
					if (itemData[i].GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
					{
						itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam
						{
							icon_type = itemData[i].GetIconType(),
							icon_id = itemData[i].GetIconID(),
							rarity = new RARITY_TYPE?(itemData[i].GetRarity()),
							parent = t,
							element = itemData[i].GetIconElement(),
							magi_enable_equip_type = itemData[i].GetIconMagiEnableType(),
							num = itemData[i].GetNum(),
							enemy_icon_id = enemy_icon_id,
							enemy_icon_id2 = enemy_icon_id2,
							questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
						});
					}
					else
					{
						GET_TYPE getType = itemData[i].GetGetType();
						itemIcon = ItemIcon.Create(itemData[i].GetIconType(), itemData[i].GetIconID(), itemData[i].GetRarity(), t, itemData[i].GetIconElement(), itemData[i].GetIconMagiEnableType(), itemData[i].GetNum(), null, 0, false, -1, false, null, false, enemy_icon_id, enemy_icon_id2, false, getType);
					}
					itemIcon.SetRewardBG(true);
					SetMaterialInfo(itemIcon.transform, itemData[i].GetMaterialType(), itemData[i].GetTableID(), GetCtrl(UI.OBJ_SCROLL_VIEW));
				}
				else if (shouldAddGold)
				{
					ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1u, t, totalGold, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
					itemIcon2.SetRewardBG(true);
					SetMaterialInfo(itemIcon2.transform, REWARD_TYPE.MONEY, 0u, GetCtrl(UI.OBJ_SCROLL_VIEW));
					shouldAddGold = false;
				}
				else
				{
					ItemIcon itemIcon3 = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.CRYSTAL, 1u, t, crystalNum, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
					itemIcon3.SetRewardBG(true);
					SetMaterialInfo(itemIcon3.transform, REWARD_TYPE.CRYSTAL, 0u, GetCtrl(UI.OBJ_SCROLL_VIEW));
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
