using Network;
using System;

public class CrystalShopSpecialSold : GameSection
{
	private enum UI
	{
		LBL_DAY
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
			SetLabelText((Enum)UI.LBL_DAY, string.Format(base.sectionData.GetText("DAY"), _productData.remainingDay));
		}
	}

	private void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}
