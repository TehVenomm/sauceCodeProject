using UnityEngine;

public class FortuneWheelSpinItem : MonoBehaviour
{
	[HideInInspector]
	public int itemId;

	[HideInInspector]
	public Transform _trans;

	public GameObject jackpotIcon;

	public void CreateItemIcon(int itemId, REWARD_TYPE type, uint rewardId)
	{
		_trans = base.transform;
		if (type != REWARD_TYPE.JACKPOT)
		{
			jackpotIcon.SetActive(false);
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(type, rewardId, _trans, -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if ((Object)itemIcon != (Object)null)
			{
				if (type == REWARD_TYPE.SKILL_ITEM)
				{
					itemIcon.SetSpinMachineSkillItem();
				}
				else
				{
					itemIcon.SetSpinMachineItem();
				}
			}
		}
	}

	public void SetRotate(float degree)
	{
		_trans.localEulerAngles = new Vector3(0f, 0f, degree);
	}

	public void SetScale(float scale)
	{
		_trans.localScale = new Vector3(scale, scale, 1f);
	}
}
