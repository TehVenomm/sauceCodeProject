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
		Vector2 printedSize = LBL_LOG.printedSize;
		float x = printedSize.x - 210f + 40f;
		Vector3 localPosition = OBJ_JACKPOT.transform.localPosition;
		localPosition.x = x;
		OBJ_JACKPOT.transform.localPosition = localPosition;
		OBJ_JACKPOT.SetActive(true);
		OBJ_ITEM_ICON.gameObject.SetActive(false);
	}

	public void InitLog(string textLog, REWARD_TYPE item_type, uint icon_id)
	{
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.gameObject.SetActive(true);
		LBL_LOG.text = textLog;
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.gameObject.SetActive(true);
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, OBJ_ITEM_ICON, -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
		if ((Object)itemIcon != (Object)null)
		{
			itemIcon.SetSpinLogIcon();
		}
		Vector2 printedSize = LBL_LOG.printedSize;
		float x = printedSize.x - 210f + 30f;
		Vector3 localPosition = OBJ_ITEM_ICON.localPosition;
		localPosition.x = x;
		OBJ_ITEM_ICON.localPosition = localPosition;
	}
}
