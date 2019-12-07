using System.Collections;
using UnityEngine;

public class CrystalShopSpecialStarter : GameSection
{
	private enum UI
	{
		OBJ_MODEL
	}

	private ShopReceiver.PaymentPurchaseData purchaseData;

	public override void Initialize()
	{
		purchaseData = (GameSection.GetEventData() as ShopReceiver.PaymentPurchaseData);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetModel(UI.OBJ_MODEL, "RoyalChest_Open");
	}

	public override void StartSection()
	{
		ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(purchaseData.productId);
		StartCoroutine(Wait(pack.openAnimEndTime));
	}

	private IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		RequestEvent("BUNDLE_NOTICE", purchaseData);
		GameSection.BackSection();
	}
}
