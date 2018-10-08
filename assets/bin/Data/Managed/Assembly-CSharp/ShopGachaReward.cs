using Network;
using System.Collections.Generic;
using UnityEngine;

public class ShopGachaReward : GameSection
{
	private enum UI
	{
		GRD_ICON
	}

	private List<QuestItem.SellItem> sellItem;

	public override void Initialize()
	{
		sellItem = (GameSection.GetEventData() as List<QuestItem.SellItem>);
		sellItem.Sort((QuestItem.SellItem l, QuestItem.SellItem r) => l.pri - r.pri);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		QuestItem.SellItem[] data_ary = sellItem.ToArray();
		int item_num = data_ary.Length;
		SetGrid(UI.GRD_ICON, null, item_num, false, delegate(int i, Transform t, bool is_recycle)
		{
			uint itemId = (uint)data_ary[i].itemId;
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)data_ary[i].type, itemId, t, data_ary[i].num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if ((Object)itemIcon != (Object)null)
			{
				itemIcon.SetRewardBG(true);
				SetMaterialInfo(itemIcon.transform, (REWARD_TYPE)data_ary[i].type, itemId, null);
			}
		});
	}
}
