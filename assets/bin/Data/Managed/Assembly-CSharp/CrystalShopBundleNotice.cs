using System;
using System.Text;

public class CrystalShopBundleNotice : GameSection
{
	private enum UI
	{
		LBL_BONUS_NAME,
		ProvisionalLabel
	}

	private ShopReceiver.PaymentPurchaseData purchaseData;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		purchaseData = (GameSection.GetEventData() as ShopReceiver.PaymentPurchaseData);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (purchaseData != null)
		{
			SetLabelText(UI.LBL_BONUS_NAME, purchaseData.productName);
			StringBuilder sb = new StringBuilder();
			int count = purchaseData.bundle.Length;
			int index = 0;
			Array.ForEach(purchaseData.bundle, delegate(ShopReceiver.PaymentPurchaseData.PaymentItemData o)
			{
				int num = ++index;
				if (index < count)
				{
					sb.AppendLine(o.name);
				}
				else
				{
					sb.Append(o.name);
				}
			});
			SetLabelText(UI.ProvisionalLabel, sb.ToString());
			UpdateAnchors();
		}
	}

	private void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}
