using UnityEngine;

public class FortuneWheelServerLogItem : MonoBehaviour
{
	[SerializeField]
	private UILabel LBL_LOG;

	[SerializeField]
	private Transform OBJ_ITEM_ICON;

	[SerializeField]
	private GameObject OBJ_JACKPOT;

	public void InitJackpot(string textLog)
	{
		LBL_LOG.text = textLog;
		float x = LBL_LOG.printedSize.x - 210f + 30f;
		Vector3 localPosition = OBJ_JACKPOT.transform.localPosition;
		localPosition.x = x;
		OBJ_JACKPOT.transform.localPosition = localPosition;
		OBJ_JACKPOT.SetActive(value: true);
		OBJ_ITEM_ICON.gameObject.SetActive(value: false);
	}

	public void InitLog(string textLog, REWARD_TYPE item_type, uint icon_id)
	{
		OBJ_JACKPOT.SetActive(value: false);
		OBJ_ITEM_ICON.gameObject.SetActive(value: true);
		LBL_LOG.text = textLog;
		OBJ_JACKPOT.SetActive(value: false);
		OBJ_ITEM_ICON.gameObject.SetActive(value: true);
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, OBJ_ITEM_ICON);
		if (itemIcon != null)
		{
			itemIcon.SetSpinLogIcon();
		}
		float x = LBL_LOG.printedSize.x - 210f + 30f;
		Vector3 localPosition = OBJ_ITEM_ICON.localPosition;
		localPosition.x = x;
		OBJ_ITEM_ICON.localPosition = localPosition;
	}
}
