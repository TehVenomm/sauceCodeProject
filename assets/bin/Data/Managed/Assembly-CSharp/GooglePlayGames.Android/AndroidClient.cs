using Com.Google.Android.Gms.Common.Api;
using Com.Google.Android.Gms.Games;
using Com.Google.Android.Gms.Games.Stats;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine;

namespace GooglePlayGames.Android
{
	internal class AndroidClient : IClientImpl
	{
		private class StatsResultCallback : ResultCallbackProxy<Stats_LoadPlayerStatsResultObject>
		{
			private Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback;

			public StatsResultCallback(Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback)
			{
				this.callback = callback;
			}

			public override void OnResult(Stats_LoadPlayerStatsResultObject arg_Result_1)
			{
				callback.Invoke(arg_Result_1.getStatus().getStatusCode(), arg_Result_1.getPlayerStats());
			}
		}

		internal const string BridgeActivityClass = "com.google.games.bridge.NativeBridgeActivity";

		private const string LaunchBridgeMethod = "launchBridgeIntent";

		private const string LaunchBridgeSignature = "(Landroid/app/Activity;Landroid/content/Intent;)V";

		private TokenClient tokenClient;

		private static AndroidJavaObject invisible;

		public unsafe PlatformConfiguration CreatePlatformConfiguration(PlayGamesClientConfiguration clientConfig)
		{
			AndroidPlatformConfiguration androidPlatformConfiguration = AndroidPlatformConfiguration.Create();
			AndroidJavaObject activity = AndroidTokenClient.GetActivity();
			try
			{
				androidPlatformConfiguration.SetActivity(activity.GetRawObject());
				androidPlatformConfiguration.SetOptionalIntentHandlerForUI(delegate(IntPtr intent)
				{
					//IL_0019: Unknown result type (might be due to invalid IL or missing references)
					//IL_001e: Expected O, but got Unknown
					IntPtr intentRef = AndroidJNI.NewGlobalRef(intent);
					_003CCreatePlatformConfiguration_003Ec__AnonStorey808 _003CCreatePlatformConfiguration_003Ec__AnonStorey;
					PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CCreatePlatformConfiguration_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				});
				if (!clientConfig.IsHidingPopups)
				{
					return androidPlatformConfiguration;
				}
				androidPlatformConfiguration.SetOptionalViewForPopups(CreateHiddenView(activity.GetRawObject()));
				return androidPlatformConfiguration;
			}
			finally
			{
				((IDisposable)activity)?.Dispose();
			}
		}

		public TokenClient CreateTokenClient(bool reset)
		{
			if (tokenClient == null)
			{
				tokenClient = new AndroidTokenClient();
			}
			else if (reset)
			{
				tokenClient.Signout();
			}
			return tokenClient;
		}

		private IntPtr CreateHiddenView(IntPtr activity)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			if (invisible == null || invisible.GetRawObject() == IntPtr.Zero)
			{
				invisible = new AndroidJavaObject("android.view.View", new object[1]
				{
					activity
				});
				invisible.Call("setVisibility", new object[1]
				{
					4
				});
				invisible.Call("setClickable", new object[1]
				{
					false
				});
			}
			return invisible.GetRawObject();
		}

		private static void LaunchBridgeIntent(IntPtr bridgedIntent)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			object[] array = new object[2];
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			try
			{
				AndroidJavaClass val = new AndroidJavaClass("com.google.games.bridge.NativeBridgeActivity");
				try
				{
					AndroidJavaObject activity = AndroidTokenClient.GetActivity();
					try
					{
						IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(val.GetRawClass(), "launchBridgeIntent", "(Landroid/app/Activity;Landroid/content/Intent;)V");
						array2[0].l = activity.GetRawObject();
						array2[1].l = bridgedIntent;
						AndroidJNI.CallStaticVoidMethod(val.GetRawClass(), staticMethodID, array2);
					}
					finally
					{
						((IDisposable)activity)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				Logger.e("Exception launching bridge intent: " + ex.Message);
				Logger.e(ex.ToString());
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(array, array2);
			}
		}

		public void Signout()
		{
			if (tokenClient != null)
			{
				tokenClient.Signout();
			}
		}

		public unsafe void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
		{
			GoogleApiClient arg_GoogleApiClient_ = new GoogleApiClient(apiClient);
			StatsResultCallback resultCallback;
			try
			{
				_003CGetPlayerStats_003Ec__AnonStorey809 _003CGetPlayerStats_003Ec__AnonStorey;
				resultCallback = new StatsResultCallback(new Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats>((object)_003CGetPlayerStats_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				callback.Invoke(CommonStatusCodes.DeveloperError, (GooglePlayGames.BasicApi.PlayerStats)null);
				return;
				IL_0049:;
			}
			PendingResult<Stats_LoadPlayerStatsResultObject> pendingResult = Games.Stats.loadPlayerStats(arg_GoogleApiClient_, true);
			pendingResult.setResultCallback(resultCallback);
		}
	}
}
