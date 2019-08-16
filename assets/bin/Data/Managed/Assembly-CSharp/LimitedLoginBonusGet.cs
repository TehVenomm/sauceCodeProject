using Network;
using System;
using System.Collections;
using UnityEngine;

public class LimitedLoginBonusGet : GameSection
{
	private enum UI
	{
		LBL_LOGIN_DAYS,
		GRD_GET_ITEM,
		LBL_GET_ITEM,
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL
	}

	private Transform texModel_;

	private UIModelRenderTexture texModelRenderTexture_;

	private UITexture texModelTexture_;

	private Transform texInnerModel_;

	private UIModelRenderTexture texInnerModelRenderTexture_;

	private UITexture texInnerModelTexture_;

	private Transform glowModel_;

	private bool isModel;

	private LoginBonus.LoginBonusReward reward;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		base.Initialize();
		texModel_ = Utility.Find(base._transform, "TEX_MODEL");
		texModelRenderTexture_ = UIModelRenderTexture.Get(texModel_);
		texModelTexture_ = texModel_.GetComponent<UITexture>();
		texInnerModel_ = Utility.Find(base._transform, "TEX_INNER_MODEL");
		texInnerModelRenderTexture_ = UIModelRenderTexture.Get(texInnerModel_);
		texInnerModelTexture_ = texInnerModel_.GetComponent<UITexture>();
		glowModel_ = Utility.Find(base._transform, "LIB_00000003");
		LoginBonus loginBonus = (LoginBonus)GameSection.GetEventData();
		if (loginBonus == null)
		{
			return;
		}
		SetLabelText((Enum)UI.LBL_LOGIN_DAYS, loginBonus.name);
		if (loginBonus.reward.Count > 0)
		{
			reward = loginBonus.reward[0];
			SetLabelText((Enum)UI.LBL_GET_ITEM, reward.name);
			if (reward.type == 14)
			{
				SetRenderAccessoryModel((Enum)UI.TEX_MODEL, (uint)reward.itemId, reward.GetScale(), rotation: true, light_rotation: false);
				texModelTexture_.width = 300;
				texModelTexture_.height = 300;
				isModel = true;
			}
			else if (reward.type == 5)
			{
				uint itemId = (uint)reward.itemId;
				texModelRenderTexture_.InitSkillItem(texModelTexture_, itemId, rotation: true, light_rotation: false, 45f);
				texInnerModelRenderTexture_.InitSkillItemSymbol(texInnerModelTexture_, itemId, rotation: true, 17f);
				isModel = true;
			}
			else if (reward.type == 4)
			{
				SetRenderEquipModel((Enum)UI.TEX_MODEL, (uint)reward.itemId, -1, -1, reward.GetScale());
				texModelTexture_.width = 300;
				texModelTexture_.height = 300;
				isModel = true;
			}
			else if (reward.type == 1 || reward.type == 2)
			{
				uint itemModelID = GetItemModelID((REWARD_TYPE)reward.type, reward.itemId);
				texModelRenderTexture_.InitItem(texModelTexture_, itemModelID);
				isModel = true;
			}
			else if (reward.type == 3 && IsDispItem3D(reward.itemId))
			{
				uint itemModelID2 = GetItemModelID((REWARD_TYPE)reward.type, reward.itemId);
				texModelRenderTexture_.InitItem(texModelTexture_, itemModelID2);
				isModel = true;
			}
			if (!isModel)
			{
				this.StartCoroutine("LoadIcon");
			}
			float rotateSpeed = 35f;
			texModelRenderTexture_.SetRotateSpeed(rotateSpeed);
			texInnerModelRenderTexture_.SetRotateSpeed(rotateSpeed);
		}
	}

	private IEnumerator LoadIcon()
	{
		yield return null;
		LoginBonus.LoginBonusReward r = reward;
		ItemIcon icon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)r.type, (uint)r.itemId, GetCtrl(UI.OBJ_DETAIL_ROOT));
		icon.transform.set_localScale(new Vector3(1.5f, 1.5f, 1.5f));
		GetCtrl(UI.OBJ_DETAIL_ROOT).set_localPosition(new Vector3(0f, 50f, 0f));
	}

	private bool IsDispItem3D(int itemID)
	{
		if (itemID == 7000100 || itemID == 7000101 || itemID == 7000200 || itemID == 7000201 || itemID == 7000300 || itemID == 7000301 || itemID == 1200000)
		{
			return true;
		}
		return false;
	}

	private uint GetItemModelID(REWARD_TYPE type, int itemID)
	{
		uint result = uint.MaxValue;
		switch (type)
		{
		case REWARD_TYPE.CRYSTAL:
			result = 1u;
			break;
		case REWARD_TYPE.MONEY:
			result = 2u;
			break;
		case REWARD_TYPE.ITEM:
			result = (uint)itemID;
			break;
		}
		return result;
	}

	private void OnQuery_CLOSE()
	{
		if (null != glowModel_)
		{
			glowModel_.get_gameObject().SetActive(false);
		}
		GameSection.BackSection();
	}
}
