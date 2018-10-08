using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestGetReward : GameSection
{
	private enum UI
	{
		OBJ_FRAME,
		LBL_GOLD,
		LBL_CRYSTAL,
		LBL_EXP,
		GRD_ICON,
		BTN_CENTER
	}

	private struct RewardData
	{
		public REWARD_TYPE reward_type;

		public ITEM_ICON_TYPE icon_type;

		public int icon_id;

		public uint item_id;

		public RARITY_TYPE? rarity;

		public ELEMENT_TYPE element;

		public EQUIPMENT_TYPE? magi_enable_equip_type;

		public int num;

		public int enemy_icon_id;

		public int enemy_icon_id2;

		public GET_TYPE getType;
	}

	protected QuestCompleteReward reward;

	protected List<QuestCompleteReward.SellItem> sell;

	private bool isStoryComplete;

	private bool isDelivery;

	public override void Initialize()
	{
		object eventData = GameSection.GetEventData();
		DeliveryRewardList deliveryRewardList = eventData as DeliveryRewardList;
		if (deliveryRewardList != null)
		{
			reward = deliveryRewardList.delivery;
			sell = deliveryRewardList.sell;
			isDelivery = true;
		}
		else
		{
			StoryRewardList storyRewardList = eventData as StoryRewardList;
			if (storyRewardList != null)
			{
				reward = storyRewardList.story;
				isStoryComplete = true;
			}
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (reward == null)
		{
			SetActive((Enum)UI.OBJ_FRAME, false);
		}
		else
		{
			SetFullScreenButton((Enum)UI.BTN_CENTER);
			SetActive((Enum)UI.OBJ_FRAME, true);
			SetLabelText((Enum)UI.LBL_GOLD, reward.money.ToString("N0"));
			SetLabelText((Enum)UI.LBL_CRYSTAL, reward.crystal.ToString("N0"));
			SetLabelText((Enum)UI.LBL_EXP, reward.exp.ToString("N0"));
			int num = (reward.crystal > 0) ? 1 : 0;
			int num2 = reward.item.Count + reward.equipItem.Count + reward.skillItem.Count + num;
			int num3 = num + reward.item.Count;
			int num4 = num + reward.item.Count + reward.equipItem.Count;
			RewardData[] data = new RewardData[num2];
			int j = 0;
			for (int num5 = num2; j < num5; j++)
			{
				data[j] = default(RewardData);
				if (j < num)
				{
					data[j].reward_type = REWARD_TYPE.CRYSTAL;
					data[j].icon_type = ITEM_ICON_TYPE.NONE;
					data[j].icon_id = 1;
					data[j].item_id = 0u;
					data[j].rarity = null;
					data[j].element = ELEMENT_TYPE.MAX;
					data[j].magi_enable_equip_type = null;
					data[j].num = reward.crystal;
					data[j].enemy_icon_id = 0;
					data[j].enemy_icon_id2 = 0;
					data[j].getType = GET_TYPE.PAY;
				}
				else if (j < num3)
				{
					int index = j - num;
					QuestCompleteReward.Item item = reward.item[index];
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					data[j].reward_type = REWARD_TYPE.ITEM;
					data[j].icon_type = ItemIcon.GetItemIconType(itemData.type);
					data[j].icon_id = itemData.iconID;
					data[j].item_id = itemData.id;
					data[j].rarity = itemData.rarity;
					data[j].element = ELEMENT_TYPE.MAX;
					data[j].magi_enable_equip_type = null;
					data[j].num = item.num;
					data[j].enemy_icon_id = itemData.enemyIconID;
					data[j].enemy_icon_id2 = itemData.enemyIconID2;
					data[j].getType = GET_TYPE.PAY;
				}
				else if (j < num4)
				{
					int index2 = j - num3;
					QuestCompleteReward.EquipItem equipItem = reward.equipItem[index2];
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equipItem.equipItemId);
					data[j].reward_type = REWARD_TYPE.EQUIP_ITEM;
					data[j].icon_type = ItemIcon.GetItemIconType(equipItemData.type);
					data[j].icon_id = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
					data[j].item_id = equipItemData.id;
					data[j].rarity = equipItemData.rarity;
					data[j].element = equipItemData.GetTargetElementPriorityToTable();
					data[j].magi_enable_equip_type = null;
					data[j].num = ((equipItem.num != 1) ? equipItem.num : (-1));
					data[j].enemy_icon_id = 0;
					data[j].enemy_icon_id2 = 0;
					data[j].getType = equipItemData.getType;
				}
				else
				{
					int index3 = j - num4;
					QuestCompleteReward.SkillItem skillItem = reward.skillItem[index3];
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skillItem.skillItemId);
					data[j].reward_type = REWARD_TYPE.SKILL_ITEM;
					data[j].icon_type = ItemIcon.GetItemIconType(skillItemData.type);
					data[j].icon_id = skillItemData.iconID;
					data[j].item_id = skillItemData.id;
					data[j].rarity = skillItemData.rarity;
					data[j].element = ELEMENT_TYPE.MAX;
					data[j].magi_enable_equip_type = skillItemData.GetEnableEquipType();
					data[j].num = ((skillItem.num != 1) ? skillItem.num : (-1));
					data[j].enemy_icon_id = 0;
					data[j].enemy_icon_id2 = 0;
					data[j].getType = GET_TYPE.PAY;
				}
			}
			SetGrid(UI.GRD_ICON, string.Empty, num2, false, delegate(int i, Transform t, bool is_recycle)
			{
				ItemIcon itemIcon = ItemIcon.Create(data[i].icon_type, data[i].icon_id, data[i].rarity, t, data[i].element, data[i].magi_enable_equip_type, data[i].num, "REWARD", i, false, -1, false, null, false, data[i].enemy_icon_id, data[i].enemy_icon_id2, false, data[i].getType);
				SetMaterialInfo(itemIcon.transform, data[i].reward_type, data[i].item_id, null);
			});
		}
	}

	public void OnQuery_OK()
	{
		if (isDelivery)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<QuestManager>.I.SendGetDeliveryList(delegate(bool b)
			{
				GameSection.ChangeStayEvent("FROM_DELIVERY", null);
				GameSection.ResumeEvent(b, null);
			});
		}
		else if (isStoryComplete)
		{
			GameSection.ChangeEvent("TO_SELECT", null);
		}
		else
		{
			GameSection.BackSection();
		}
	}
}
