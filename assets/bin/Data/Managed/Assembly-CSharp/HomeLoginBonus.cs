using Network;
using UnityEngine;

public class HomeLoginBonus : GameSection
{
	private enum UI
	{
		LBL_LOGIN_DAYS,
		GRD_GET_ITEM,
		LBL_GET_ITEM
	}

	private Transform texModel_;

	private UIModelRenderTexture texModelRenderTexture_;

	private UITexture texModelTexture_;

	private Transform texInnerModel_;

	private UIModelRenderTexture texInnerModelRenderTexture_;

	private UITexture texInnerModelTexture_;

	private Transform glowModel_;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		base.Initialize();
		MonoBehaviourSingleton<AccountManager>.I.DisplayLogInBonusSection();
		texModel_ = Utility.Find(base._transform, "TEX_MODEL");
		texModelRenderTexture_ = UIModelRenderTexture.Get(texModel_);
		texModelTexture_ = texModel_.GetComponent<UITexture>();
		texInnerModel_ = Utility.Find(base._transform, "TEX_INNER_MODEL");
		texInnerModelRenderTexture_ = UIModelRenderTexture.Get(texInnerModel_);
		texInnerModelTexture_ = texInnerModel_.GetComponent<UITexture>();
		glowModel_ = Utility.Find(base._transform, "LIB_00000003");
		LoginBonus loginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Find((LoginBonus obj) => obj.type == 0);
		if (loginBonus != null)
		{
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.Remove(loginBonus);
			SetLabelText(UI.LBL_LOGIN_DAYS, loginBonus.total.ToString());
			if (loginBonus.reward.Count > 0)
			{
				LoginBonus.LoginBonusReward loginBonusReward = loginBonus.reward[0];
				SetLabelText(UI.LBL_GET_ITEM, loginBonusReward.name);
				float rotateSpeed = 35f;
				if (loginBonusReward.type == 5)
				{
					uint itemId = (uint)loginBonusReward.itemId;
					texModelRenderTexture_.InitSkillItem(texModelTexture_, itemId, true, false, 45f);
					texInnerModelRenderTexture_.InitSkillItemSymbol(texInnerModelTexture_, itemId, true, 17f);
				}
				else
				{
					uint itemModelID = GetItemModelID((REWARD_TYPE)loginBonusReward.type, loginBonusReward.itemId);
					texModelRenderTexture_.InitItem(texModelTexture_, itemModelID, true);
				}
				texModelRenderTexture_.SetRotateSpeed(rotateSpeed);
				texInnerModelRenderTexture_.SetRotateSpeed(rotateSpeed);
			}
		}
	}

	protected override void OnOpen()
	{
		HomeLoginBonusTheater.PlayRandomVoice(HomeLoginBonusTheater.voiceCheers);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
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
		if ((Object)null != (Object)glowModel_)
		{
			glowModel_.gameObject.SetActive(false);
		}
		if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count > 0)
		{
			GameSection.ChangeEvent("LIMITED_LOGIN_BONUS", null);
		}
		else
		{
			GameSection.BackSection();
		}
	}
}
