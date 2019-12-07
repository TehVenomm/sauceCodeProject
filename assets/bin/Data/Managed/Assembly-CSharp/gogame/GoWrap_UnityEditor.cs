using System;
using System.Collections.Generic;
using UnityEngine;

namespace gogame
{
	public class GoWrap_UnityEditor : IGoWrap
	{
		private List<string> customUrlSchemes;

		private IGoWrapDelegate goWrapDelegate;

		private string guid;

		private VipStatus vipStatus;

		private string objName;

		public void initGoWrap(string objName)
		{
			this.objName = objName;
		}

		public void SendMessage(string methodName, object value)
		{
			if (objName != null)
			{
				GameObject gameObject = GameObject.Find(objName);
				if (!(gameObject == null))
				{
					gameObject.SendMessage(methodName, value);
				}
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

		public void showMenu()
		{
			UnityEditorGoWrapMenu.ShowGoWrapMenu();
		}

		public void setCustomUrlSchemes(List<string> schemes)
		{
			customUrlSchemes = schemes;
			if (customUrlSchemes == null)
			{
				Debug.Log("[goWrap] Custom URL schemes cleared");
			}
			else
			{
				Debug.Log("[goWrap] Custom URL schemes: " + string.Join(", ", customUrlSchemes.ToArray()));
			}
		}

		public void setIOSDeviceToken(byte[] deviceToken)
		{
			Debug.LogWarning("[goWrap] setIOSDeviceToken() not supported in the Unity editor");
		}

		public void setGuid(string guid)
		{
			this.guid = guid;
			Debug.Log($"[goWrap] guid={this.guid}");
		}

		public void setVipStatus(VipStatus vipStatus)
		{
			this.vipStatus = vipStatus;
			if (this.vipStatus != null)
			{
				Debug.Log($"[goWrap] vipStatus vip={this.vipStatus.vip}, suspended={this.vipStatus.suspended}, suspensionMessage={this.vipStatus.suspensionMessage}");
			}
			else
			{
				Debug.Log("[goWrap] vipStatus=null");
			}
		}

		public void trackEvent(string name, string category)
		{
			Debug.Log($"[goWrap] trackEvent(name={name}, category={category})");
		}

		public void trackEvent(string name, string category, long value)
		{
			Debug.Log($"[goWrap] trackEvent(name={name}, category={category}, value={value})");
		}

		public void trackEvent(string name, string category, Dictionary<string, object> values)
		{
			string arg = null;
			if (values != null)
			{
				JSONObject jSONObject = new JSONObject();
				foreach (KeyValuePair<string, object> value2 in values)
				{
					string key = value2.Key;
					object value = value2.Value;
					if (value != null)
					{
						if (value is bool)
						{
							jSONObject.AddField(key, (bool)value);
						}
						else if (value is decimal)
						{
							jSONObject.AddField(key, Convert.ToSingle(value));
						}
						else if (value is double)
						{
							jSONObject.AddField(key, Convert.ToSingle(value));
						}
						else if (value is float)
						{
							jSONObject.AddField(key, (float)value);
						}
						else if (value is int)
						{
							jSONObject.AddField(key, (int)value);
						}
						else if (value is uint)
						{
							jSONObject.AddField(key, Convert.ToInt64(value));
						}
						else if (value is long)
						{
							jSONObject.AddField(key, (long)value);
						}
						else if (value is ulong)
						{
							jSONObject.AddField(key, Convert.ToInt64(value));
						}
						else if (value is short)
						{
							jSONObject.AddField(key, (short)value);
						}
						else if (value is ushort)
						{
							jSONObject.AddField(key, Convert.ToInt32(value));
						}
						else if (value is byte)
						{
							jSONObject.AddField(key, Convert.ToInt16(value));
						}
						else if (value is sbyte)
						{
							jSONObject.AddField(key, Convert.ToInt16(value));
						}
						else if (value is string)
						{
							jSONObject.AddField(key, (string)value);
						}
						else
						{
							jSONObject.AddField(key, $"[object {value.GetType().Name}]");
						}
					}
				}
				arg = jSONObject.Print();
			}
			Debug.Log($"[goWrap] trackEvent(name={name}, category={category}, values={arg})");
		}

		public void trackPurchase(string productId, string currencyCode, double price)
		{
			Debug.Log($"[goWrap] trackPurchase(productId={productId}, currencyCode={currencyCode}, price={price})");
		}

		public void trackPurchase(string productId, string currencyCode, double price, string purchaseData, string signature)
		{
			Debug.Log($"[goWrap] trackPurchase(productId={productId}, currencyCode={currencyCode}, price={price}, purchaseData={purchaseData}, signature={signature})");
		}

		public bool hasOffers()
		{
			Debug.LogWarning("[goWrap] hasOffers() not supported in the Unity editor");
			return false;
		}

		public void showOffers()
		{
			Debug.LogWarning("[goWrap] showOffers() not supported in the Unity editor");
		}

		public bool hasBannerAds()
		{
			Debug.LogWarning("[goWrap] hasBannerAds() not supported in the Unity editor");
			return false;
		}

		public bool hasBannerAds(BannerAdSize size)
		{
			Debug.LogWarning("[goWrap] hasBannerAds() not supported in the Unity editor");
			return false;
		}

		public void showBannerAd()
		{
			Debug.LogWarning("[goWrap] showBannerAd() not supported in the Unity editor");
		}

		public void showBannerAd(BannerAdSize size)
		{
			Debug.LogWarning("[goWrap] showBannerAd() not supported in the Unity editor");
		}

		public void hideBannerAd()
		{
			Debug.LogWarning("[goWrap] hideBannerAd() not supported in the Unity editor");
		}

		public bool hasInterstitialAds()
		{
			Debug.LogWarning("[goWrap] hasInterstitialAds() not supported in the Unity editor");
			return false;
		}

		public void showInterstitialAd()
		{
			Debug.LogWarning("[goWrap] showInterstitialAd() not supported in the Unity editor");
		}

		public bool hasRewardedAds()
		{
			Debug.LogWarning("[goWrap] hasRewardedAds() not supported in the Unity editor");
			return false;
		}

		public void showRewardedAd()
		{
			Debug.LogWarning("[goWrap] showRewardedAd() not supported in the Unity editor");
		}
	}
}
