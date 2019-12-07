using System;
using System.Collections.Generic;
using UnityEngine;

namespace gogame
{
	public class GoWrap_Android : IGoWrap
	{
		private IGoWrapDelegate goWrapDelegate;

		public void initGoWrap(string objName)
		{
			runOnUiThread(delegate
			{
				try
				{
					using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("net.gogame.gowrap.Bootstrap"))
					{
						androidJavaClass2.CallStatic("unityInit");
					}
				}
				catch (Exception)
				{
					Debug.LogWarning("Could not locate class net.gogame.gowrap.Bootstrap, probably running before wrap");
				}
			});
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.unity.GoWrapUnityPlugin"))
			{
				androidJavaClass.CallStatic("initialize", objName);
			}
		}

		public IGoWrapDelegate getDelegate()
		{
			return goWrapDelegate;
		}

		public void setDelegate(IGoWrapDelegate goWrapDelegate)
		{
			this.goWrapDelegate = goWrapDelegate;
		}

		public void setGuid(string guid)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				androidJavaClass.CallStatic("setGuid", guid);
			}
		}

		public void setVipStatus(VipStatus vipStatus)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				if (vipStatus != null)
				{
					using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("net.gogame.gowrap.sdk.VipStatus"))
					{
						androidJavaObject.Call("setVip", vipStatus.vip);
						androidJavaObject.Call("setSuspended", vipStatus.suspended);
						androidJavaObject.Call("setSuspensionMessage", vipStatus.suspensionMessage);
						androidJavaClass.CallStatic("setVipStatus", androidJavaObject);
					}
				}
				else
				{
					androidJavaClass.CallStatic("setVipStatus", null);
				}
			}
		}

		public bool hasOffers()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				return androidJavaClass.CallStatic<bool>("hasOffers", Array.Empty<object>());
			}
		}

		public void showOffers()
		{
			runOnUiThread(delegate
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
				{
					androidJavaClass.CallStatic("showOffers");
				}
			});
		}

		public bool hasBannerAds()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				return androidJavaClass.CallStatic<bool>("hasBannerAds", Array.Empty<object>());
			}
		}

		public bool hasBannerAds(BannerAdSize size)
		{
			using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap$BannerAdSize"))
				{
					AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>(size.ToString());
					return androidJavaClass2.CallStatic<bool>("hasBannerAds", new object[1]
					{
						@static
					});
				}
			}
		}

		public void showBannerAd()
		{
			runOnUiThread(delegate
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
				{
					androidJavaClass.CallStatic("showBannerAd");
				}
			});
		}

		public void showBannerAd(BannerAdSize size)
		{
			runOnUiThread(delegate
			{
				using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
				{
					using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap$BannerAdSize"))
					{
						AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>(size.ToString());
						androidJavaClass2.CallStatic("showBannerAd", @static);
					}
				}
			});
		}

		public void hideBannerAd()
		{
			runOnUiThread(delegate
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
				{
					androidJavaClass.CallStatic("hideBannerAd");
				}
			});
		}

		public bool hasInterstitialAds()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				return androidJavaClass.CallStatic<bool>("hasInterstitialAds", Array.Empty<object>());
			}
		}

		public void showInterstitialAd()
		{
			runOnUiThread(delegate
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
				{
					androidJavaClass.CallStatic("showInterstitialAd");
				}
			});
		}

		public bool hasRewardedAds()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				return androidJavaClass.CallStatic<bool>("hasRewardedAds", Array.Empty<object>());
			}
		}

		public void showRewardedAd()
		{
			runOnUiThread(delegate
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
				{
					androidJavaClass.CallStatic("showRewardedAd");
				}
			});
		}

		public void showMenu()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				androidJavaClass.CallStatic("showMenu");
			}
		}

		public void trackEvent(string name, string category)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				androidJavaClass.CallStatic("trackEvent", category, name);
			}
		}

		public void trackEvent(string name, string category, long value)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				androidJavaClass.CallStatic("trackEvent", category, name, value);
			}
		}

		public void trackEvent(string name, string category, Dictionary<string, object> values)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				using (AndroidJavaObject androidJavaObject3 = toJavaMap(values))
				{
					IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(androidJavaClass.GetRawClass(), "trackEvent", "(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V");
					object[] array = new object[3];
					AndroidJavaObject androidJavaObject = toAndroidJavaObject(category);
					AndroidJavaObject androidJavaObject2 = toAndroidJavaObject(name);
					array[0] = androidJavaObject;
					array[1] = androidJavaObject2;
					array[2] = androidJavaObject3;
					try
					{
						AndroidJNI.CallStaticVoidMethod(androidJavaClass.GetRawClass(), staticMethodID, AndroidJNIHelper.CreateJNIArgArray(array));
					}
					finally
					{
						if (androidJavaObject != null)
						{
							androidJavaObject.Dispose();
							androidJavaObject = null;
						}
						if (androidJavaObject2 != null)
						{
							androidJavaObject2.Dispose();
							androidJavaObject2 = null;
						}
					}
				}
			}
		}

		public void trackPurchase(string productId, string currencyCode, double price, string purchaseData, string signature)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				androidJavaClass.CallStatic("trackPurchase", productId, currencyCode, price, purchaseData, signature);
			}
		}

		public void setCustomUrlSchemes(List<string> schemes)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.ArrayList");
				IntPtr methodID = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "add", "(Ljava/lang/Object;)Z");
				object[] array = new object[1];
				foreach (string scheme in schemes)
				{
					string text = (string)(array[0] = scheme);
					AndroidJNI.CallBooleanMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
				}
				androidJavaClass.CallStatic("setCustomUrlSchemes", androidJavaObject);
			}
		}

		public void setIOSDeviceToken(byte[] deviceToken)
		{
		}

		private void runOnUiThread(AndroidJavaRunnable runnable)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					androidJavaObject.Call("runOnUiThread", runnable);
				}
			}
		}

		private AndroidJavaObject toAndroidJavaObject(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is bool)
			{
				return new AndroidJavaObject("java.lang.Boolean", value);
			}
			if (value is decimal)
			{
				decimal value2 = (decimal)value;
				return new AndroidJavaObject("java.lang.Double", (double)value2);
			}
			if (value is double)
			{
				return new AndroidJavaObject("java.lang.Double", value);
			}
			if (value is float)
			{
				return new AndroidJavaObject("java.lang.Float", value);
			}
			if (value is int)
			{
				return new AndroidJavaObject("java.lang.Integer", value);
			}
			if (value is uint)
			{
				uint num = (uint)value;
				return new AndroidJavaObject("java.lang.Long", (long)num);
			}
			if (value is long)
			{
				return new AndroidJavaObject("java.lang.Long", value);
			}
			if (value is ulong)
			{
				ulong num2 = (ulong)value;
				return new AndroidJavaObject("java.lang.Long", (long)num2);
			}
			if (value is short)
			{
				return new AndroidJavaObject("java.lang.Short", value);
			}
			if (value is ushort)
			{
				ushort num3 = (ushort)value;
				return new AndroidJavaObject("java.lang.Integer", (int)num3);
			}
			if (value is byte)
			{
				byte b = (byte)value;
				return new AndroidJavaObject("java.lang.Short", (short)b);
			}
			if (value is sbyte)
			{
				sbyte b2 = (sbyte)value;
				return new AndroidJavaObject("java.lang.Short", (short)b2);
			}
			if (value is string)
			{
				return new AndroidJavaObject("java.lang.String", value);
			}
			return new AndroidJavaObject("java.lang.String", value.ToString());
		}

		private AndroidJavaObject toJavaMap(Dictionary<string, object> values)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.HashMap");
			IntPtr methodID = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			object[] array = new object[2];
			foreach (KeyValuePair<string, object> value in values)
			{
				using (AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("java.lang.String", value.Key))
				{
					AndroidJavaObject androidJavaObject2 = toAndroidJavaObject(value.Value);
					if (androidJavaObject2 != null)
					{
						using (androidJavaObject2)
						{
							array[0] = androidJavaObject3;
							array[1] = androidJavaObject2;
							AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
						}
					}
				}
			}
			return androidJavaObject;
		}

		public void trackPurchase(string productId, string currencyCode, double price)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
			{
				androidJavaClass.CallStatic("trackPurchase", productId, currencyCode, price);
			}
		}
	}
}
