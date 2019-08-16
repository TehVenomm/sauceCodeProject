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
		this.StartCoroutine(LoadImg(homeBanner));
	}

	private IEnumerator LoadImg(Network.HomeBanner _homeBanner)
	{
		if (_homeBanner != null)
		{
			homeBanner = _homeBanner;
			LoadingQueue load = new LoadingQueue(this);
			LoadObject lo = load.Load(isEventAsset: true, RESOURCE_CATEGORY.HOME_BANNER_ADS, ResourceName.GetHomeBanner(homeBanner.bannerId));
			yield return load.Wait();
			UITexture sprw = FindCtrl(base._transform, UI.BANNER).GetComponent<UITexture>();
			Texture2D tex = lo.loadedObject as Texture2D;
			if (tex != null)
			{
				sprw.mainTexture = tex;
			}
			else
			{
				sprw.mainTexture = (Resources.Load("Texture/White") as Texture);
			}
			Transform _trans = sprw.get_transform();
			switch (homeBanner.homeType)
			{
			case 0:
				SetEvent(_trans, "CLOSE", 0);
				break;
			case 1:
			{
				string text = string.IsNullOrEmpty(homeBanner.targetString) ? "bundle" : homeBanner.targetString;
				SetEvent(_trans, "OPEN_CRYSTAL_SHOP", text.Trim());
				break;
			}
			case 2:
				SetEvent(_trans, "OPEN_CRYSTAL_SHOP", 0);
				break;
			case 3:
				SetEvent(_trans, "BANNER_GACHA", GACHA_TYPE.QUEST);
				break;
			case 4:
				SetEvent(_trans, "BANNER_GACHA", GACHA_TYPE.SKILL);
				break;
			case 5:
				SetEvent(_trans, "OPEN_BANNER_NEWS", homeBanner.targetString);
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
