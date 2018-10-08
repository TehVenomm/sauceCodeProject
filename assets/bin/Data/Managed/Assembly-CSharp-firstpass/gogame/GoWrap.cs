using System.Collections.Generic;

namespace gogame
{
	public class GoWrap : IGoWrap
	{
		public static GoWrap INSTANCE = new GoWrap();

		private IGoWrap goWrap;

		private IGoWrapDelegate goWrapDelegate;

		public GoWrap()
		{
			goWrap = new GoWrap_Android();
		}

		public void initGoWrap(string objName)
		{
			if (goWrap != null)
			{
				goWrap.initGoWrap(objName);
			}
		}

		public IGoWrapDelegate getDelegate()
		{
			return goWrapDelegate;
		}

		public void setDelegate(IGoWrapDelegate goWrapDelegate)
		{
			this.goWrapDelegate = goWrapDelegate;
			if (goWrap != null)
			{
				goWrap.setDelegate(goWrapDelegate);
			}
		}

		public void setGuid(string guid)
		{
			if (goWrap != null)
			{
				goWrap.setGuid(guid);
			}
		}

		public void setVipStatus(VipStatus vipStatus)
		{
			if (goWrap != null)
			{
				goWrap.setVipStatus(vipStatus);
			}
		}

		public void showMenu()
		{
			if (goWrap != null)
			{
				goWrap.showMenu();
			}
		}

		public bool hasBannerAds()
		{
			if (goWrap != null)
			{
				return goWrap.hasBannerAds();
			}
			return false;
		}

		public bool hasBannerAds(BannerAdSize size)
		{
			if (goWrap != null)
			{
				return goWrap.hasBannerAds(size);
			}
			return false;
		}

		public void showBannerAd()
		{
			if (goWrap != null)
			{
				goWrap.showBannerAd();
			}
		}

		public void showBannerAd(BannerAdSize size)
		{
			if (goWrap != null)
			{
				goWrap.showBannerAd(size);
			}
		}

		public void hideBannerAd()
		{
			if (goWrap != null)
			{
				goWrap.hideBannerAd();
			}
		}

		public bool hasOffers()
		{
			if (goWrap != null)
			{
				return goWrap.hasOffers();
			}
			return false;
		}

		public void showOffers()
		{
			if (goWrap != null)
			{
				goWrap.showOffers();
			}
		}

		public bool hasInterstitialAds()
		{
			if (goWrap != null)
			{
				return goWrap.hasInterstitialAds();
			}
			return false;
		}

		public void showInterstitialAd()
		{
			if (goWrap != null)
			{
				goWrap.showInterstitialAd();
			}
		}

		public bool hasRewardedAds()
		{
			if (goWrap != null)
			{
				return goWrap.hasRewardedAds();
			}
			return false;
		}

		public void showRewardedAd()
		{
			if (goWrap != null)
			{
				goWrap.showRewardedAd();
			}
		}

		public void trackEvent(string name, string category)
		{
			if (goWrap != null)
			{
				goWrap.trackEvent(name, category);
			}
		}

		public void trackEvent(string name, string category, long value)
		{
			if (goWrap != null)
			{
				goWrap.trackEvent(name, category, value);
			}
		}

		public void trackEvent(string name, string category, Dictionary<string, object> values)
		{
			if (goWrap != null)
			{
				goWrap.trackEvent(name, category, values);
			}
		}

		public void trackPurchase(string productId, string currency, double price, string purchaseData, string signature)
		{
			if (goWrap != null)
			{
				goWrap.trackPurchase(productId, currency, price, purchaseData, signature);
			}
		}

		public void setCustomUrlSchemes(List<string> schemes)
		{
			if (goWrap != null)
			{
				goWrap.setCustomUrlSchemes(schemes);
			}
		}

		public void setIOSDeviceToken(byte[] deviceToken)
		{
		}
	}
}
