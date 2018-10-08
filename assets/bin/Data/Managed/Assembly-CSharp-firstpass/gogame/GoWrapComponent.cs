using System.Collections.Generic;
using UnityEngine;

namespace gogame
{
	public class GoWrapComponent : IGoWrap
	{
		public GoWrapComponent()
			: this()
		{
		}

		public void handleAdsCompletedWithReward(string message)
		{
			Debug.Log((object)"handleAdsCompletedWithReward called.");
			JSONObject jSONObject = new JSONObject(message, -2, false, false);
			jSONObject.GetField(out string field, "rewardId", "DEFAULT");
			jSONObject.GetField(out int field2, "rewardQuantity", -1);
			GoWrap.INSTANCE.getDelegate().didCompleteRewardedAd(field, field2);
		}

		public void handleMenuOpened(string message)
		{
			Debug.Log((object)"handleMenuOpened called.");
			GoWrap.INSTANCE.getDelegate().onMenuOpened();
		}

		public void handleMenuClosed(string message)
		{
			Debug.Log((object)"handleMenuClosed called.");
			GoWrap.INSTANCE.getDelegate().onMenuClosed();
		}

		public void handleOnCustomUrl(string message)
		{
			Debug.Log((object)"handleOnCustomUrl called.");
			GoWrap.INSTANCE.getDelegate().onCustomUrl(message);
		}

		public void handleOnOffersAvailable(string message)
		{
			Debug.Log((object)"handleOnOffersAvailable called.");
			GoWrap.INSTANCE.getDelegate().onOffersAvailable();
		}

		public void initGoWrap(string objName)
		{
			GoWrap.INSTANCE.initGoWrap(objName);
		}

		public IGoWrapDelegate getDelegate()
		{
			return GoWrap.INSTANCE.getDelegate();
		}

		public void setDelegate(IGoWrapDelegate goWrapDelegate)
		{
			GoWrap.INSTANCE.setDelegate(goWrapDelegate);
		}

		public void setGuid(string guid)
		{
			GoWrap.INSTANCE.setGuid(guid);
		}

		public void setVipStatus(VipStatus vipStatus)
		{
			GoWrap.INSTANCE.setVipStatus(vipStatus);
		}

		public void showMenu()
		{
			GoWrap.INSTANCE.showMenu();
		}

		public bool hasBannerAds()
		{
			return GoWrap.INSTANCE.hasBannerAds();
		}

		public bool hasBannerAds(BannerAdSize size)
		{
			return GoWrap.INSTANCE.hasBannerAds(size);
		}

		public void showBannerAd()
		{
			GoWrap.INSTANCE.showBannerAd();
		}

		public void showBannerAd(BannerAdSize size)
		{
			GoWrap.INSTANCE.showBannerAd(size);
		}

		public void hideBannerAd()
		{
			GoWrap.INSTANCE.hideBannerAd();
		}

		public bool hasOffers()
		{
			return GoWrap.INSTANCE.hasOffers();
		}

		public void showOffers()
		{
			GoWrap.INSTANCE.showOffers();
		}

		public bool hasInterstitialAds()
		{
			return GoWrap.INSTANCE.hasInterstitialAds();
		}

		public void showInterstitialAd()
		{
			GoWrap.INSTANCE.showInterstitialAd();
		}

		public bool hasRewardedAds()
		{
			return GoWrap.INSTANCE.hasRewardedAds();
		}

		public void showRewardedAd()
		{
			GoWrap.INSTANCE.showRewardedAd();
		}

		public void trackEvent(string name, string category)
		{
			GoWrap.INSTANCE.trackEvent(name, category);
		}

		public void trackEvent(string name, string category, long value)
		{
			GoWrap.INSTANCE.trackEvent(name, category, value);
		}

		public void trackEvent(string name, string category, Dictionary<string, object> value)
		{
			GoWrap.INSTANCE.trackEvent(name, category, value);
		}

		public void trackPurchase(string productId, string currency, double price, string purchaseData, string signature)
		{
			GoWrap.INSTANCE.trackPurchase(productId, currency, price, purchaseData, signature);
		}

		public void setCustomUrlSchemes(List<string> schemes)
		{
			GoWrap.INSTANCE.setCustomUrlSchemes(schemes);
		}

		public void setIOSDeviceToken(byte[] deviceToken)
		{
			GoWrap.INSTANCE.setIOSDeviceToken(deviceToken);
		}
	}
}
