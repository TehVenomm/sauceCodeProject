using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestRequestItem : UIBehaviour
{
	private enum UI
	{
		TEX_NPC,
		LBL_DELIVERY_COMMENT,
		OBJ_REQUEST_OK,
		OBJ_REQUEST_COMPLETED,
		LBL_HAVE,
		LBL_NEED,
		LBL_NEED_ITEM_NAME,
		LBL_LIMIT,
		SPR_TYPE_NORMAL,
		SPR_TYPE_EVENT,
		SPR_TYPE_STORY,
		SPR_TYPE_HARD,
		SPR_TYPE_SUB_EVENT,
		SPR_TYPE_EVENT_TEXT,
		SPR_TYPE_DAILY_TEXT,
		SPR_TYPE_WEEKLY_TEXT,
		SPR_DROP_DIFFICULTY_RARE,
		SPR_DROP_DIFFICULTY_SUPER_RARE,
		SPR_FRAME,
		OBJ_ICON_ROOT_1,
		OBJ_ICON_ROOT_2,
		GRD_ICON_ROOT,
		OBJ_LEVEL_LIMIT,
		LBL_LEVEL_LIMIT,
		SPR_TYPE_TEXT_STORY,
		SPR_TYPE_TEXT_STORY_HARD,
		SPR_TYPE_TEXT_SUB_HARD
	}

	private readonly string[] SPR_FRAME_TYPE = new string[5]
	{
		"RequestPlate_Base",
		"RequestPlate_Event",
		"RequestPlate_Story",
		"RequestPlate_Hard",
		"RequestPlate_SubEvent"
	};

	public virtual void Setup(Transform t, DeliveryTable.DeliveryData info)
	{
		SetIcon(t, info);
		SetDeliveryName(t, info);
		bool is_visible = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)info.id);
		SetActive(t, UI.OBJ_REQUEST_OK, is_visible);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, false);
		MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int)info.id, out int have, out int need, out string item_name, out string limit_time);
		SetLabelText(t, UI.LBL_HAVE, have.ToString());
		SetLabelText(t, UI.LBL_NEED, need.ToString());
		SetLabelText(t, UI.LBL_NEED_ITEM_NAME, item_name);
		SetLabelText(t, UI.LBL_LIMIT, limit_time);
		SetFrame(t, info);
		if (info.GetUIType() == DeliveryTable.UIType.STORY)
		{
			SetActive(t, UI.SPR_TYPE_TEXT_STORY, info.GetRegionDifficultyType() == REGION_DIFFICULTY_TYPE.NORMAL);
			SetActive(t, UI.SPR_TYPE_TEXT_STORY_HARD, info.GetRegionDifficultyType() == REGION_DIFFICULTY_TYPE.HARD);
		}
		if (info.GetUIType() == DeliveryTable.UIType.NONE)
		{
			SetActive(t, UI.SPR_TYPE_TEXT_SUB_HARD, info.GetRegionDifficultyType() == REGION_DIFFICULTY_TYPE.HARD);
		}
		DeliveryTable.UIType uIType = info.GetUITextType();
		if (uIType == DeliveryTable.UIType.NONE)
		{
			uIType = info.GetUIType();
		}
		SetActive(t, UI.SPR_TYPE_EVENT_TEXT, uIType == DeliveryTable.UIType.EVENT);
		SetActive(t, UI.SPR_TYPE_DAILY_TEXT, uIType == DeliveryTable.UIType.DAILY);
		SetActive(t, UI.SPR_TYPE_WEEKLY_TEXT, uIType == DeliveryTable.UIType.WEEKLY);
		SetActive(t, UI.SPR_DROP_DIFFICULTY_RARE, info.GetDeliveryDropRarity() == DELIVERY_DROP_DIFFICULTY.RARE);
		SetActive(t, UI.SPR_DROP_DIFFICULTY_SUPER_RARE, info.GetDeliveryDropRarity() == DELIVERY_DROP_DIFFICULTY.SUPER_RARE);
		DeliveryDropRareTextColor componentInChildren = t.GetComponentInChildren<DeliveryDropRareTextColor>();
		if ((Object)componentInChildren != (Object)null)
		{
			UILabel component = GetComponent<UILabel>(t, UI.LBL_NEED_ITEM_NAME);
			if ((Object)component != (Object)null)
			{
				SetColor(t, UI.LBL_NEED_ITEM_NAME, componentInChildren.GetRarityColor(info.GetDeliveryDropRarity()));
			}
		}
		SetSprite(t, UI.SPR_FRAME, SPR_FRAME_TYPE[info.DeliveryTypeIndex()]);
		int num = 0;
		List<DeliveryRewardTable.DeliveryRewardData.Reward> list = new List<DeliveryRewardTable.DeliveryRewardData.Reward>();
		DeliveryRewardTable.DeliveryRewardData[] deliveryRewardTableData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(info.id);
		if (deliveryRewardTableData != null)
		{
			DeliveryRewardTable.DeliveryRewardData[] array = deliveryRewardTableData;
			foreach (DeliveryRewardTable.DeliveryRewardData deliveryRewardData in array)
			{
				list.Add(deliveryRewardData.reward);
			}
		}
		List<PointShopGetPointTable.Data> fromDeiliveryId = Singleton<PointShopGetPointTable>.I.GetFromDeiliveryId(info.id);
		if (fromDeiliveryId.Any())
		{
			foreach (PointShopGetPointTable.Data item in fromDeiliveryId)
			{
				DeliveryRewardTable.DeliveryRewardData.Reward reward = new DeliveryRewardTable.DeliveryRewardData.Reward();
				reward.item_id = item.pointShopId;
				reward.num = item.basePoint;
				reward.type = REWARD_TYPE.POINT_SHOP_POINT;
				list.Add(reward);
			}
		}
		if (list.Any())
		{
			if (list.Count >= 2)
			{
				list = (from x in list
				orderby GetRewardPriority(x)
				select x).ToList();
			}
			UI[] array2 = new UI[2]
			{
				UI.OBJ_ICON_ROOT_1,
				UI.OBJ_ICON_ROOT_2
			};
			for (int j = 0; j < 2; j++)
			{
				bool flag = list.Count >= j + 1;
				SetActive(t, array2[j], flag);
				if (flag)
				{
					DeliveryRewardTable.DeliveryRewardData.Reward reward2 = list[j];
					ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(reward2.type, reward2.item_id, FindCtrl(t, array2[j]), -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST);
					if ((Object)itemIcon != (Object)null)
					{
						itemIcon.SetEnableCollider(false);
					}
				}
			}
		}
		QuestTable.QuestTableData questData = info.GetQuestData();
		if (questData != null)
		{
			bool flag2 = questData.level > (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			UIWidget[] componentsInChildren = FindCtrl(t, UI.SPR_FRAME).GetComponentsInChildren<UIWidget>();
			UIWidget[] array3 = componentsInChildren;
			foreach (UIWidget uIWidget in array3)
			{
				if (flag2 && !uIWidget.name.Contains("Mask"))
				{
					uIWidget.color = Color.gray;
				}
			}
			SetActive(t, UI.OBJ_LEVEL_LIMIT, flag2);
			SetLabelText(t, UI.LBL_LEVEL_LIMIT, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 3u), questData.level));
			SetButtonEnabled(t, !flag2);
		}
		else
		{
			SetActive(t, UI.OBJ_LEVEL_LIMIT, false);
		}
		UIGrid component2 = GetComponent<UIGrid>(t, UI.GRD_ICON_ROOT);
		if ((Object)component2 != (Object)null)
		{
			component2.Reposition();
		}
	}

	protected virtual void SetFrame(Transform t, DeliveryTable.DeliveryData info)
	{
		DeliveryTable.UIType uIType = info.GetUIType();
		SetActive(t, UI.SPR_TYPE_NORMAL, uIType == DeliveryTable.UIType.NONE);
		SetActive(t, UI.SPR_TYPE_EVENT, uIType == DeliveryTable.UIType.EVENT || uIType == DeliveryTable.UIType.DAILY || uIType == DeliveryTable.UIType.WEEKLY);
		SetActive(t, UI.SPR_TYPE_STORY, uIType == DeliveryTable.UIType.STORY);
		SetActive(t, UI.SPR_TYPE_HARD, uIType == DeliveryTable.UIType.HARD);
		SetActive(t, UI.SPR_TYPE_SUB_EVENT, uIType == DeliveryTable.UIType.SUB_EVENT);
	}

	protected virtual void SetIcon(Transform t, DeliveryTable.DeliveryData info)
	{
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData((int)info.npcID);
		SetNPCIcon(t, UI.TEX_NPC, nPCData.npcModelID, false);
	}

	protected virtual void SetDeliveryName(Transform t, DeliveryTable.DeliveryData info)
	{
		SetLabelText(t, UI.LBL_DELIVERY_COMMENT, info.name);
	}

	private int GetRewardPriority(DeliveryRewardTable.DeliveryRewardData.Reward reward)
	{
		switch (reward.type)
		{
		case REWARD_TYPE.CRYSTAL:
			return 0;
		case REWARD_TYPE.EQUIP_ITEM:
			return 2;
		case REWARD_TYPE.POINT_SHOP_POINT:
			return 3;
		case REWARD_TYPE.ITEM:
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(reward.item_id);
			if (itemData.type == ITEM_TYPE.TICKET)
			{
				return 1;
			}
			return 4;
		}
		default:
			return 4;
		}
	}
}
