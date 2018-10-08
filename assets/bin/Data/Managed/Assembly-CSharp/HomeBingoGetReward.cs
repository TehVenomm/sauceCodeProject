using Network;
using System;
using UnityEngine;

public class HomeBingoGetReward : GameSection
{
	private enum UI
	{
		LBL_TITLE,
		GRD_GET_ITEM,
		LBL_GET_ITEM,
		GRD_REWARD
	}

	private struct Reward
	{
		public int itemId;

		public int type;

		public string name;
	}

	private Transform texModel_;

	private UIModelRenderTexture texModelRenderTexture_;

	private UITexture texModelTexture_;

	private Transform texInnerModel_;

	private UIModelRenderTexture texInnerModelRenderTexture_;

	private UITexture texInnerModelTexture_;

	private Transform glowModel_;

	private DeliveryTable.DeliveryData deliveryData;

	private Network.EventData eventData;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		deliveryData = (array[0] as DeliveryTable.DeliveryData);
		eventData = (array[1] as Network.EventData);
		base.Initialize();
		texModel_ = Utility.Find(base._transform, "TEX_MODEL");
		texModelRenderTexture_ = UIModelRenderTexture.Get(texModel_);
		texModelTexture_ = texModel_.GetComponent<UITexture>();
		texInnerModel_ = Utility.Find(base._transform, "TEX_INNER_MODEL");
		texInnerModelRenderTexture_ = UIModelRenderTexture.Get(texInnerModel_);
		texInnerModelTexture_ = texInnerModel_.GetComponent<UITexture>();
		glowModel_ = Utility.Find(base._transform, "LIB_00000003");
		SetLabelText((Enum)UI.LBL_TITLE, eventData.name);
	}

	public override void UpdateUI()
	{
		DeliveryRewardTable.DeliveryRewardData[] deliveryRewardTableData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(deliveryData.id);
		UpdateRewardIcon(deliveryRewardTableData);
		base.UpdateUI();
	}

	private unsafe void UpdateRewardIcon(DeliveryRewardTable.DeliveryRewardData[] rewards)
	{
		if (rewards != null && rewards.Length > 0)
		{
			int exp = 0;
			_003CUpdateRewardIcon_003Ec__AnonStorey377 _003CUpdateRewardIcon_003Ec__AnonStorey;
			SetGrid(UI.GRD_REWARD, string.Empty, rewards.Length, false, new Action<int, Transform, bool>((object)_003CUpdateRewardIcon_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void OnQuery_CLOSE()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (null != glowModel_)
		{
			glowModel_.get_gameObject().SetActive(false);
		}
		GameSection.BackSection();
	}
}
