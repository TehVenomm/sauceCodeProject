using UnityEngine;

public class FortuneWheelServerLogItem : MonoBehaviour
{
	[SerializeField]
	private UILabel LBL_LOG;

	[SerializeField]
	private Transform OBJ_ITEM_ICON;

	[SerializeField]
	private GameObject OBJ_JACKPOT;

	public FortuneWheelServerLogItem()
		: this()
	{
	}

	public void InitJackpot(string textLog)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		LBL_LOG.text = textLog;
		Vector2 printedSize = LBL_LOG.printedSize;
		float x = printedSize.x - 210f + 30f;
		Vector3 localPosition = OBJ_JACKPOT.get_transform().get_localPosition();
		localPosition.x = x;
		OBJ_JACKPOT.get_transform().set_localPosition(localPosition);
		OBJ_JACKPOT.SetActive(true);
		OBJ_ITEM_ICON.get_gameObject().SetActive(false);
	}

	public void InitLog(string textLog, REWARD_TYPE item_type, uint icon_id)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.get_gameObject().SetActive(true);
		LBL_LOG.text = textLog;
		OBJ_JACKPOT.SetActive(false);
		OBJ_ITEM_ICON.get_gameObject().SetActive(true);
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, OBJ_ITEM_ICON);
		if (itemIcon != null)
		{
			itemIcon.SetSpinLogIcon();
		}
		Vector2 printedSize = LBL_LOG.printedSize;
		float x = printedSize.x - 210f + 30f;
		Vector3 localPosition = OBJ_ITEM_ICON.get_localPosition();
		localPosition.x = x;
		OBJ_ITEM_ICON.set_localPosition(localPosition);
	}
}
