using Network;
using System;

public class CrystalShopSpecialNotice : GameSection
{
	private enum UI
	{
		LBL_TITLE
	}

	private ProductData _productData;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		_productData = (GameSection.GetEventData() as ProductData);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (_productData != null)
		{
			GlobalSettingsManager.PackParam.SpecialInfo special = MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.GetSpecial(_productData.productId);
			if (special != null)
			{
				SetLabelText((Enum)UI.LBL_TITLE, base.sectionData.GetText(special.specialEvent));
			}
		}
	}

	private void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}
