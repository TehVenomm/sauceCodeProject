using UnityEngine;

public class FortuneWheelRewardLogItem : MonoBehaviour
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
		OBJ_JACKPOT.SetActive(true);
		OBJ_ITEM_ICON.get_gameObject().SetActive(false);
	}

	public void InitLog(REWARD_TYPE item_type, uint icon_id, int num)
	{
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.get_gameObject().SetActive(true);
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.get_gameObject().SetActive(true);
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, OBJ_ITEM_ICON, num);
		if (itemIcon != null)
		{
			itemIcon.SetSpinUserLogIcon();
		}
	}
}
