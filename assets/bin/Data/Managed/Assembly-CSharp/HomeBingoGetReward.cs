using Network;
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
		SetLabelText(UI.LBL_TITLE, eventData.name);
	}

	public override void UpdateUI()
	{
		DeliveryRewardTable.DeliveryRewardData[] deliveryRewardTableData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(deliveryData.id);
		UpdateRewardIcon(deliveryRewardTableData);
		base.UpdateUI();
	}

	private void UpdateRewardIcon(DeliveryRewardTable.DeliveryRewardData[] rewards)
	{
		if (rewards != null && rewards.Length > 0)
		{
			int exp = 0;
			SetGrid(UI.GRD_REWARD, string.Empty, rewards.Length, false, delegate(int index, Transform t, bool is_recycle)
			{
				DeliveryRewardTable.DeliveryRewardData.Reward reward = rewards[index].reward;
				bool is_visible = false;
				if (reward.type == REWARD_TYPE.EXP)
				{
					exp += reward.num;
				}
				else
				{
					is_visible = true;
					ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
					SetMaterialInfo(itemIcon.transform, reward.type, reward.item_id, null);
					itemIcon.SetRewardBG(true);
				}
				SetActive(t, is_visible);
			});
		}
	}

	private void OnQuery_CLOSE()
	{
		if ((Object)null != (Object)glowModel_)
		{
			glowModel_.gameObject.SetActive(false);
		}
		GameSection.BackSection();
	}
}
