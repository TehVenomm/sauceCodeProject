using UnityEngine;

public class FortuneWheelRewardLogItem
{
	[SerializeField]
	private Transform OBJ_ITEM_ICON;

	[SerializeField]
	private GameObject OBJ_JACKPOT;

	public FortuneWheelRewardLogItem()
		: this()
	{
	}

	public void InitJackpot()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		OBJ_JACKPOT.SetActive(true);
		OBJ_ITEM_ICON.get_gameObject().SetActive(false);
	}

	public void InitLog(REWARD_TYPE item_type, uint icon_id, int num)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.get_gameObject().SetActive(true);
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.get_gameObject().SetActive(true);
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, OBJ_ITEM_ICON, num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
		if (itemIcon != null)
		{
			itemIcon.SetSpinUserLogIcon();
		}
	}
}
