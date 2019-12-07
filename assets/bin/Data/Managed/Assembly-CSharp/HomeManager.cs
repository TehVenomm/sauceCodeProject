using Network;
using System;
using System.Collections;

public class HomeManager : MonoBehaviourSingleton<HomeManager>, IHomeManager
{
	public bool IsJumpToGacha
	{
		get;
		set;
	}

	public bool IsInitialized
	{
		get;
		private set;
	}

	public HomeCamera HomeCamera
	{
		get;
		private set;
	}

	public IHomePeople IHomePeople
	{
		get;
		private set;
	}

	public HomeFeatureBanner HomeFeatureBanner
	{
		get;
		private set;
	}

	public bool IsPointShopOpen
	{
		get;
		private set;
	}

	public int PointShopBannerId
	{
		get;
		private set;
	}

	public void SetPointShop(bool isOpen, int bannerId)
	{
		IsPointShopOpen = isOpen;
		PointShopBannerId = bannerId;
	}

	public void SendDebugAddPointShopPoint(int pointShopId, int addPoint, Action<bool> call_back)
	{
		DebugAddPointShopPointModel.RequestSendForm requestSendForm = new DebugAddPointShopPointModel.RequestSendForm();
		requestSendForm.point = addPoint;
		requestSendForm.pointShopId = pointShopId;
		Protocol.Send(DebugAddPointShopPointModel.URL, requestSendForm, delegate(DebugAddPointShopPointModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		});
	}

	public OutGameSettingsManager.HomeScene GetSceneSetting()
	{
		return MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene;
	}

	private IEnumerator Start()
	{
		while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return null;
		}
		HomeCamera = base.gameObject.AddComponent<HomeCamera>();
		IHomePeople = base.gameObject.AddComponent<HomePeople>();
		HomeFeatureBanner = base.gameObject.AddComponent<HomeFeatureBanner>();
		while (!HomeCamera.isInitialized)
		{
			yield return null;
		}
		while (IHomePeople.isInitialized)
		{
			yield return null;
		}
		IsInitialized = true;
	}
}
