using System;
using System.Collections.Generic;
using UnityEngine;

namespace gogame
{
	public class GoWrap_Android : IGoWrap
	{
		private IGoWrapDelegate goWrapDelegate;

		private void runOnUiThread(AndroidJavaRunnable runnable)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			try
			{
				AndroidJavaObject @static = val.GetStatic<AndroidJavaObject>("currentActivity");
				try
				{
					@static.Call("runOnUiThread", new object[1]
					{
						runnable
					});
				}
				finally
				{
					((IDisposable)@static)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public unsafe void initGoWrap(string objName)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			if (_003C_003Ef__am_0024cache0 == null)
			{
				_003C_003Ef__am_0024cache0 = new AndroidJavaRunnable((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			runOnUiThread(_003C_003Ef__am_0024cache0);
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.unity.GoWrapUnityPlugin");
			try
			{
				val.CallStatic("initialize", new object[1]
				{
					objName
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
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
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				val.CallStatic("setGuid", new object[1]
				{
					guid
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void setVipStatus(VipStatus vipStatus)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				if (vipStatus != null)
				{
					AndroidJavaObject val2 = new AndroidJavaObject("net.gogame.gowrap.sdk.VipStatus", new object[0]);
					try
					{
						val2.Call("setVip", new object[1]
						{
							vipStatus.vip
						});
						val2.Call("setSuspended", new object[1]
						{
							vipStatus.suspended
						});
						val2.Call("setSuspensionMessage", new object[1]
						{
							vipStatus.suspensionMessage
						});
						val.CallStatic("setVipStatus", new object[1]
						{
							val2
						});
					}
					finally
					{
						((IDisposable)val2)?.Dispose();
					}
				}
				else
				{
					val.CallStatic("setVipStatus", (object[])null);
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public bool hasOffers()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				return val.CallStatic<bool>("hasOffers", new object[0]);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public unsafe void showOffers()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_003C_003Ef__am_0024cache1 == null)
			{
				_003C_003Ef__am_0024cache1 = new AndroidJavaRunnable((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			runOnUiThread(_003C_003Ef__am_0024cache1);
		}

		public bool hasBannerAds()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				return val.CallStatic<bool>("hasBannerAds", new object[0]);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public bool hasBannerAds(BannerAdSize size)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				AndroidJavaClass val2 = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap$BannerAdSize");
				try
				{
					AndroidJavaObject @static = val2.GetStatic<AndroidJavaObject>(size.ToString());
					return val.CallStatic<bool>("hasBannerAds", new object[1]
					{
						@static
					});
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public unsafe void showBannerAd()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_003C_003Ef__am_0024cache2 == null)
			{
				_003C_003Ef__am_0024cache2 = new AndroidJavaRunnable((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			runOnUiThread(_003C_003Ef__am_0024cache2);
		}

		public unsafe void showBannerAd(BannerAdSize size)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003CshowBannerAd_003Ec__AnonStorey0 _003CshowBannerAd_003Ec__AnonStorey;
			runOnUiThread(new AndroidJavaRunnable((object)_003CshowBannerAd_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public unsafe void hideBannerAd()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_003C_003Ef__am_0024cache3 == null)
			{
				_003C_003Ef__am_0024cache3 = new AndroidJavaRunnable((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			runOnUiThread(_003C_003Ef__am_0024cache3);
		}

		public bool hasInterstitialAds()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				return val.CallStatic<bool>("hasInterstitialAds", new object[0]);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public unsafe void showInterstitialAd()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_003C_003Ef__am_0024cache4 == null)
			{
				_003C_003Ef__am_0024cache4 = new AndroidJavaRunnable((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			runOnUiThread(_003C_003Ef__am_0024cache4);
		}

		public bool hasRewardedAds()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				return val.CallStatic<bool>("hasRewardedAds", new object[0]);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public unsafe void showRewardedAd()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_003C_003Ef__am_0024cache5 == null)
			{
				_003C_003Ef__am_0024cache5 = new AndroidJavaRunnable((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			runOnUiThread(_003C_003Ef__am_0024cache5);
		}

		public void showMenu()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				val.CallStatic("showMenu", new object[0]);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void trackEvent(string name, string category)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				val.CallStatic("trackEvent", new object[2]
				{
					category,
					name
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void trackEvent(string name, string category, long value)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				val.CallStatic("trackEvent", new object[3]
				{
					category,
					name,
					value
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		private AndroidJavaObject toAndroidJavaObject(object value)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Expected O, but got Unknown
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Expected O, but got Unknown
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Expected O, but got Unknown
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Expected O, but got Unknown
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Expected O, but got Unknown
			//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Expected O, but got Unknown
			//IL_020f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0215: Expected O, but got Unknown
			if (value == null)
			{
				return null;
			}
			if (!(value is bool))
			{
				if (value is decimal)
				{
					decimal value2 = (decimal)value;
					return new AndroidJavaObject("java.lang.Double", new object[1]
					{
						(double)value2
					});
				}
				if (!(value is double))
				{
					if (!(value is float))
					{
						if (!(value is int))
						{
							if (value is uint)
							{
								uint num = (uint)value;
								return new AndroidJavaObject("java.lang.Long", new object[1]
								{
									(long)num
								});
							}
							if (!(value is long))
							{
								if (value is ulong)
								{
									ulong num2 = (ulong)value;
									return new AndroidJavaObject("java.lang.Long", new object[1]
									{
										(long)num2
									});
								}
								if (!(value is short))
								{
									if (value is ushort)
									{
										ushort num3 = (ushort)value;
										return new AndroidJavaObject("java.lang.Integer", new object[1]
										{
											(int)num3
										});
									}
									if (value is byte)
									{
										byte b = (byte)value;
										return new AndroidJavaObject("java.lang.Short", new object[1]
										{
											(short)b
										});
									}
									if (value is sbyte)
									{
										sbyte b2 = (sbyte)value;
										return new AndroidJavaObject("java.lang.Short", new object[1]
										{
											(short)b2
										});
									}
									if (!(value is string))
									{
										return new AndroidJavaObject("java.lang.String", new object[1]
										{
											value.ToString()
										});
									}
									return new AndroidJavaObject("java.lang.String", new object[1]
									{
										value
									});
								}
								return new AndroidJavaObject("java.lang.Short", new object[1]
								{
									value
								});
							}
							return new AndroidJavaObject("java.lang.Long", new object[1]
							{
								value
							});
						}
						return new AndroidJavaObject("java.lang.Integer", new object[1]
						{
							value
						});
					}
					return new AndroidJavaObject("java.lang.Float", new object[1]
					{
						value
					});
				}
				return new AndroidJavaObject("java.lang.Double", new object[1]
				{
					value
				});
			}
			return new AndroidJavaObject("java.lang.Boolean", new object[1]
			{
				value
			});
		}

		private AndroidJavaObject toJavaMap(Dictionary<string, object> values)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			AndroidJavaObject val = new AndroidJavaObject("java.util.HashMap", new object[0]);
			IntPtr methodID = AndroidJNIHelper.GetMethodID(val.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			object[] array = new object[2];
			foreach (KeyValuePair<string, object> value in values)
			{
				AndroidJavaObject val2 = new AndroidJavaObject("java.lang.String", new object[1]
				{
					value.Key
				});
				try
				{
					AndroidJavaObject val3 = toAndroidJavaObject(value.Value);
					if (val3 != null)
					{
						AndroidJavaObject val4 = val3;
						try
						{
							array[0] = val2;
							array[1] = val3;
							AndroidJNI.CallObjectMethod(val.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
						}
						finally
						{
							((IDisposable)val4)?.Dispose();
						}
					}
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			return val;
		}

		public void trackEvent(string name, string category, Dictionary<string, object> values)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				AndroidJavaObject val2 = toJavaMap(values);
				try
				{
					val.CallStatic("trackEvent", new object[3]
					{
						category,
						name,
						val2
					});
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void trackPurchase(string productId, string currencyCode, double price)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				val.CallStatic("trackPurchase", new object[3]
				{
					productId,
					currencyCode,
					price
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void trackPurchase(string productId, string currencyCode, double price, string purchaseData, string signature)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				val.CallStatic("trackPurchase", new object[5]
				{
					productId,
					currencyCode,
					price,
					purchaseData,
					signature
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void setCustomUrlSchemes(List<string> schemes)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap");
			try
			{
				AndroidJavaObject val2 = new AndroidJavaObject("java.util.ArrayList", new object[0]);
				IntPtr methodID = AndroidJNIHelper.GetMethodID(val2.GetRawClass(), "add", "(Ljava/lang/Object;)Z");
				object[] array = new object[1];
				foreach (string scheme in schemes)
				{
					string text = (string)(array[0] = scheme);
					AndroidJNI.CallBooleanMethod(val2.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
				}
				val.CallStatic("setCustomUrlSchemes", new object[1]
				{
					val2
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void setIOSDeviceToken(byte[] deviceToken)
		{
		}
	}
}
