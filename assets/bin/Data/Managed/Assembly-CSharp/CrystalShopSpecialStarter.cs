using System;
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
		SetModel((Enum)UI.OBJ_MODEL, "RoyalChest_Open");
	}

	public override void StartSection()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		GlobalSettingsManager.PackParam.PackInfo pack = MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.GetPack(purchaseData.productId);
		this.StartCoroutine(Wait(pack.openAnimEndTime));
	}

	private IEnumerator Wait(float time)
	{
		yield return (object)new WaitForSeconds(time);
		RequestEvent("BUNDLE_NOTICE", purchaseData);
		GameSection.BackSection();
	}
}
