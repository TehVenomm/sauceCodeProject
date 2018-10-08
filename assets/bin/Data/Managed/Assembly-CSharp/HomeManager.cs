using Network;
using System;
using System.Collections;

public class HomeManager : MonoBehaviourSingleton<HomeManager>
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

	public HomePeople HomePeople
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
		}, string.Empty);
	}

	private IEnumerator Start()
	{
		while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return (object)null;
		}
		HomeCamera = base.gameObject.AddComponent<HomeCamera>();
		HomePeople = base.gameObject.AddComponent<HomePeople>();
		HomeFeatureBanner = base.gameObject.AddComponent<HomeFeatureBanner>();
		while (!HomeCamera.isInitialized)
		{
			yield return (object)null;
		}
		while (HomePeople.isInitialized)
		{
			yield return (object)null;
		}
		IsInitialized = true;
	}
}
