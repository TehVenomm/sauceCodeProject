using System.Collections.Generic;

namespace gogame
{
	public interface IGoWrap
	{
		void initGoWrap(string objName);

		IGoWrapDelegate getDelegate();

		void setDelegate(IGoWrapDelegate goWrapDelegate);

		void setGuid(string guid);

		void setVipStatus(VipStatus vipStatus);

		bool hasBannerAds();

		bool hasBannerAds(BannerAdSize size);

		void showBannerAd();

		void showBannerAd(BannerAdSize size);

		void hideBannerAd();

		bool hasOffers();

		void showOffers();

		bool hasInterstitialAds();

		void showInterstitialAd();

		bool hasRewardedAds();

		void showRewardedAd();

		void showMenu();

		void trackEvent(string name, string category);

		void trackEvent(string name, string category, long value);

		void trackEvent(string name, string category, Dictionary<string, object> values);

		void trackPurchase(string productId, string currencyCode, double price, string purchaseData, string signature);

		void setCustomUrlSchemes(List<string> schemes);

		void setIOSDeviceToken(byte[] deviceToken);
	}
}
