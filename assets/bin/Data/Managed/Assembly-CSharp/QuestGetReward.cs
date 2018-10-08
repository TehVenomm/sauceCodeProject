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

	public unsafe override void UpdateUI()
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
			int num2 = reward.item.Count + reward.equipItem.Count + reward.skillItem.Count + reward.accessoryItem.Count + num;
			int num3 = num + reward.item.Count;
			int num4 = num3 + reward.equipItem.Count;
			int num5 = num4 + reward.accessoryItem.Count;
			RewardData[] data = new RewardData[num2];
			int i = 0;
			for (int num6 = num2; i < num6; i++)
			{
				data[i] = default(RewardData);
				if (i < num)
				{
					data[i].reward_type = REWARD_TYPE.CRYSTAL;
					data[i].icon_type = ITEM_ICON_TYPE.NONE;
					data[i].icon_id = 1;
					data[i].item_id = 0u;
					data[i].rarity = null;
					data[i].element = ELEMENT_TYPE.MAX;
					data[i].magi_enable_equip_type = null;
					data[i].num = reward.crystal;
					data[i].enemy_icon_id = 0;
					data[i].enemy_icon_id2 = 0;
					data[i].getType = GET_TYPE.PAY;
				}
				else if (i < num3)
				{
					int index = i - num;
					QuestCompleteReward.Item item = reward.item[index];
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
					data[i].reward_type = REWARD_TYPE.ITEM;
					data[i].icon_type = ItemIcon.GetItemIconType(itemData.type);
					data[i].icon_id = itemData.iconID;
					data[i].item_id = itemData.id;
					data[i].rarity = itemData.rarity;
					data[i].element = ELEMENT_TYPE.MAX;
					data[i].magi_enable_equip_type = null;
					data[i].num = item.num;
					data[i].enemy_icon_id = itemData.enemyIconID;
					data[i].enemy_icon_id2 = itemData.enemyIconID2;
					data[i].getType = GET_TYPE.PAY;
				}
				else if (i < num4)
				{
					int index2 = i - num3;
					QuestCompleteReward.EquipItem equipItem = reward.equipItem[index2];
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equipItem.equipItemId);
					data[i].reward_type = REWARD_TYPE.EQUIP_ITEM;
					data[i].icon_type = ItemIcon.GetItemIconType(equipItemData.type);
					data[i].icon_id = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
					data[i].item_id = equipItemData.id;
					data[i].rarity = equipItemData.rarity;
					data[i].element = equipItemData.GetTargetElementPriorityToTable();
					data[i].magi_enable_equip_type = null;
					data[i].num = ((equipItem.num != 1) ? equipItem.num : (-1));
					data[i].enemy_icon_id = 0;
					data[i].enemy_icon_id2 = 0;
					data[i].getType = equipItemData.getType;
				}
				else if (i < num5)
				{
					int index3 = i - num4;
					QuestCompleteReward.SkillItem skillItem = reward.skillItem[index3];
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skillItem.skillItemId);
					data[i].reward_type = REWARD_TYPE.SKILL_ITEM;
					data[i].icon_type = ItemIcon.GetItemIconType(skillItemData.type);
					data[i].icon_id = skillItemData.iconID;
					data[i].item_id = skillItemData.id;
					data[i].rarity = skillItemData.rarity;
					data[i].element = ELEMENT_TYPE.MAX;
					data[i].magi_enable_equip_type = skillItemData.GetEnableEquipType();
					data[i].num = ((skillItem.num != 1) ? skillItem.num : (-1));
					data[i].enemy_icon_id = 0;
					data[i].enemy_icon_id2 = 0;
					data[i].getType = GET_TYPE.PAY;
				}
				else
				{
					int index4 = i - num5;
					QuestCompleteReward.AccessoryItem accessoryItem = reward.accessoryItem[index4];
					AccessoryTable.AccessoryData data2 = Singleton<AccessoryTable>.I.GetData((uint)accessoryItem.accessoryId);
					data[i].reward_type = REWARD_TYPE.ACCESSORY;
					data[i].icon_type = ITEM_ICON_TYPE.ACCESSORY;
					data[i].icon_id = (int)data2.accessoryId;
					data[i].item_id = data2.accessoryId;
					data[i].rarity = data2.rarity;
					data[i].element = ELEMENT_TYPE.MAX;
					data[i].magi_enable_equip_type = null;
					data[i].num = ((accessoryItem.num != 1) ? accessoryItem.num : (-1));
					data[i].enemy_icon_id = 0;
					data[i].enemy_icon_id2 = 0;
					data[i].getType = data2.getType;
				}
			}
			_003CUpdateUI_003Ec__AnonStorey401 _003CUpdateUI_003Ec__AnonStorey;
			SetGrid(UI.GRD_ICON, string.Empty, num2, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
