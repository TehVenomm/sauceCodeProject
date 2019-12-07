using UnityEngine;

public class FortuneWheelRewardLogItem : MonoBehaviour
{
	[SerializeField]
	private Transform OBJ_ITEM_ICON;

	[SerializeField]
	private GameObject OBJ_JACKPOT;

	public void InitJackpot()
	{
		OBJ_JACKPOT.SetActive(value: true);
		OBJ_ITEM_ICON.gameObject.SetActive(value: false);
	}

	public void InitLog(REWARD_TYPE item_type, uint icon_id, int num)
	{
		OBJ_JACKPOT.SetActive(value: false);
		OBJ_ITEM_ICON.gameObject.SetActive(value: true);
		OBJ_JACKPOT.SetActive(value: false);
		OBJ_ITEM_ICON.gameObject.SetActive(value: true);
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, OBJ_ITEM_ICON, num);
		if (itemIcon != null)
		{
			itemIcon.SetSpinUserLogIcon();
		}
	}
}
