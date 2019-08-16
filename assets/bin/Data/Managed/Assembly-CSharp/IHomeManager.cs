public interface IHomeManager
{
	bool IsJumpToGacha
	{
		get;
		set;
	}

	bool IsInitialized
	{
		get;
	}

	HomeCamera HomeCamera
	{
		get;
	}

	IHomePeople IHomePeople
	{
		get;
	}

	HomeFeatureBanner HomeFeatureBanner
	{
		get;
	}

	bool IsPointShopOpen
	{
		get;
	}

	int PointShopBannerId
	{
		get;
	}

	void SetPointShop(bool isOpen, int bannerId);

	OutGameSettingsManager.HomeScene GetSceneSetting();
}
