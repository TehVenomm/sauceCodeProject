using Network;
using System.Collections;
using UnityEngine;

public class HomeBanner : BaseBanner
{
	private enum UI
	{
		BANNER
	}

	private Network.HomeBanner homeBanner;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void UpdateUI()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList.Count <= 0)
		{
			Close();
			return;
		}
		Network.HomeBanner homeBanner = GameSection.GetEventData() as Network.HomeBanner;
		StartCoroutine(LoadImg(homeBanner));
	}

	private IEnumerator LoadImg(Network.HomeBanner _homeBanner)
	{
		if (_homeBanner != null)
		{
			homeBanner = _homeBanner;
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.HOME_BANNER_ADS, ResourceName.GetHomeBanner(homeBanner.bannerId));
			yield return loadingQueue.Wait();
			UITexture component = FindCtrl(base._transform, UI.BANNER).GetComponent<UITexture>();
			Texture2D texture2D = lo.loadedObject as Texture2D;
			if (texture2D != null)
			{
				component.mainTexture = texture2D;
			}
			else
			{
				component.mainTexture = (Resources.Load("Texture/White") as Texture);
			}
			Transform transform = component.transform;
			switch (homeBanner.homeType)
			{
			case 0:
				SetEvent(transform, "CLOSE", 0);
				break;
			case 1:
			{
				string text = (!string.IsNullOrEmpty(homeBanner.targetString)) ? homeBanner.targetString : "bundle";
				SetEvent(transform, "OPEN_CRYSTAL_SHOP", text.Trim());
				break;
			}
			case 2:
				SetEvent(transform, "OPEN_CRYSTAL_SHOP", 0);
				break;
			case 3:
				SetEvent(transform, "BANNER_GACHA", GACHA_TYPE.QUEST);
				break;
			case 4:
				SetEvent(transform, "BANNER_GACHA", GACHA_TYPE.SKILL);
				break;
			case 5:
				SetEvent(transform, "OPEN_BANNER_NEWS", homeBanner.targetString);
				break;
			}
		}
	}

	public void OnQuery_OPEN_CRYSTAL_SHOP()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CrystalShop");
	}

	public void OnQuery_OPEN_BANNER_NEWS()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CommonDialog", "AdsWebView");
	}
}
