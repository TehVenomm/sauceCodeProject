using Network;
using UnityEngine;

public class FortuneWheelSpinItem : MonoBehaviour
{
	[HideInInspector]
	public int id;

	[HideInInspector]
	public int indexPos;

	[HideInInspector]
	public Transform _trans;

	public UITexture iconTex;

	public string itemName;

	public REWARD_TYPE type;

	public string imgId
	{
		get;
		private set;
	}

	public FortuneWheelSpinItem()
		: this()
	{
	}

	public void CreateItemIcon(int id, REWARD_TYPE type, uint rewardId, int indexPos)
	{
		this.id = id;
		this.indexPos = indexPos;
		_trans = this.get_transform();
		if (type == REWARD_TYPE.JACKPOT)
		{
			return;
		}
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(type, rewardId, _trans);
		if (itemIcon != null)
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

	public void CreateItemIcon(FortuneWheelItem data, int indexPos)
	{
		id = data.id;
		this.indexPos = indexPos;
		_trans = this.get_transform();
		type = (REWARD_TYPE)data.rewardType;
		if (string.IsNullOrEmpty(data.imgId))
		{
			if (type == REWARD_TYPE.JACKPOT)
			{
				return;
			}
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(type, (uint)data.rewardId, _trans);
			if (itemIcon != null)
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
			iconTex.get_gameObject().SetActive(false);
		}
		else
		{
			imgId = imgId;
			ResourceLoad.LoadFortuneWheelIconTexture(iconTex, data.imgId, delegate(Texture tex)
			{
				if (iconTex != null)
				{
					iconTex.mainTexture = tex;
				}
			});
		}
	}

	public void CreateItemIcon(int indexPos, string imgId)
	{
		this.indexPos = indexPos;
		_trans = this.get_transform();
		this.imgId = imgId;
		ResourceLoad.LoadFortuneWheelIconTexture(iconTex, imgId, delegate(Texture tex)
		{
			if (iconTex != null)
			{
				iconTex.mainTexture = tex;
			}
		});
	}

	public void UpdateIcon(int itemId, REWARD_TYPE type, uint rewardId)
	{
		Object.Destroy(_trans.Find("ItemIcon").get_gameObject());
		if (type == REWARD_TYPE.JACKPOT)
		{
			return;
		}
		ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(type, rewardId, _trans);
		if (itemIcon != null)
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

	public void SetRotate(float degree)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		_trans.set_localEulerAngles(new Vector3(0f, 0f, degree));
	}

	public void SetScale(float scale)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localScale = _trans.get_localScale();
		if (localScale.x != scale)
		{
			Vector3 localScale2 = _trans.get_localScale();
			if (localScale2.y != scale)
			{
				_trans.set_localScale(new Vector3(scale, scale, 1f));
			}
		}
	}
}
