// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Android.AndroidClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Com.Google.Android.Gms.Games.Stats;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.Android;

internal class AndroidClient : IClientImpl
{
  internal const string BridgeActivityClass = "com.google.games.bridge.NativeBridgeActivity";
  private const string LaunchBridgeMethod = "launchBridgeIntent";
  private const string LaunchBridgeSignature = "(Landroid/app/Activity;Landroid/content/Intent;)V";
  private TokenClient tokenClient;
  private static AndroidJavaObject invisible;

  public PlatformConfiguration CreatePlatformConfiguration(PlayGamesClientConfiguration clientConfig)
  {
    AndroidPlatformConfiguration platformConfiguration = AndroidPlatformConfiguration.Create();
    using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
    {
      platformConfiguration.SetActivity(activity.GetRawObject());
      platformConfiguration.SetOptionalIntentHandlerForUI((Action<IntPtr>) (intent =>
      {
        IntPtr intentRef = AndroidJNI.NewGlobalRef(intent);
        PlayGamesHelperObject.RunOnGameThread((Action) (() =>
        {
          try
          {
            AndroidClient.LaunchBridgeIntent(intentRef);
          }
          finally
          {
            AndroidJNI.DeleteGlobalRef(intentRef);
          }
        }));
      }));
      if (clientConfig.IsHidingPopups)
        platformConfiguration.SetOptionalViewForPopups(this.CreateHiddenView(activity.GetRawObject()));
    }
    return (PlatformConfiguration) platformConfiguration;
  }

  public TokenClient CreateTokenClient(bool reset)
  {
    if (this.tokenClient == null)
      this.tokenClient = (TokenClient) new AndroidTokenClient();
    else if (reset)
      this.tokenClient.Signout();
    return this.tokenClient;
  }

  private IntPtr CreateHiddenView(IntPtr activity)
  {
    if (AndroidClient.invisible == null || AndroidClient.invisible.GetRawObject() == IntPtr.Zero)
    {
      AndroidClient.invisible = new AndroidJavaObject("android.view.View", new object[1]
      {
        (object) activity
      });
      AndroidClient.invisible.Call("setVisibility", new object[1]
      {
        (object) 4
      });
      AndroidClient.invisible.Call("setClickable", new object[1]
      {
        (object) false
      });
    }
    return AndroidClient.invisible.GetRawObject();
  }

  private static void LaunchBridgeIntent(IntPtr bridgedIntent)
  {
    object[] objArray = new object[2];
    jvalue[] jniArgArray = AndroidJNIHelper.CreateJNIArgArray(objArray);
    try
    {
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.NativeBridgeActivity"))
      {
        using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
        {
          IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(((AndroidJavaObject) androidJavaClass).GetRawClass(), "launchBridgeIntent", "(Landroid/app/Activity;Landroid/content/Intent;)V");
          jniArgArray[0].l = activity.GetRawObject();
          jniArgArray[1].l = bridgedIntent;
          AndroidJNI.CallStaticVoidMethod(((AndroidJavaObject) androidJavaClass).GetRawClass(), staticMethodId, jniArgArray);
        }
      }
    }
    catch (Exception ex)
    {
      Logger.e("Exception launching bridge intent: " + ex.Message);
      Logger.e(ex.ToString());
    }
    finally
    {
      AndroidJNIHelper.DeleteJNIArgArray(objArray, jniArgArray);
    }
  }

  public void Signout()
  {
    if (this.tokenClient == null)
      return;
    this.tokenClient.Signout();
  }

  public void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
  {
    GoogleApiClient arg_GoogleApiClient_1 = new GoogleApiClient(apiClient);
    AndroidClient.StatsResultCallback arg_ResultCallback_1;
    try
    {
      arg_ResultCallback_1 = new AndroidClient.StatsResultCallback((Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats>) ((result, stats) =>
      {
        Debug.Log((object) ("Result for getStats: " + (object) result));
        GooglePlayGames.BasicApi.PlayerStats playerStats = (GooglePlayGames.BasicApi.PlayerStats) null;
        if (stats != null)
        {
          playerStats = new GooglePlayGames.BasicApi.PlayerStats();
          playerStats.AvgSessonLength = stats.getAverageSessionLength();
          playerStats.DaysSinceLastPlayed = stats.getDaysSinceLastPlayed();
          playerStats.NumberOfPurchases = stats.getNumberOfPurchases();
          playerStats.NumberOfSessions = stats.getNumberOfSessions();
          playerStats.SessPercentile = stats.getSessionPercentile();
          playerStats.SpendPercentile = stats.getSpendPercentile();
          playerStats.ChurnProbability = stats.getChurnProbability();
          playerStats.SpendProbability = stats.getSpendProbability();
          playerStats.HighSpenderProbability = stats.getHighSpenderProbability();
          playerStats.TotalSpendNext28Days = stats.getTotalSpendNext28Days();
        }
        callback((CommonStatusCodes) result, playerStats);
      }));
    }
    catch (Exception ex)
    {
      Debug.LogException(ex);
      callback(CommonStatusCodes.DeveloperError, (GooglePlayGames.BasicApi.PlayerStats) null);
      return;
    }
    Com.Google.Android.Gms.Games.Games.Stats.loadPlayerStats(arg_GoogleApiClient_1, true).setResultCallback((ResultCallback<Stats_LoadPlayerStatsResultObject>) arg_ResultCallback_1);
  }

  private class StatsResultCallback : ResultCallbackProxy<Stats_LoadPlayerStatsResultObject>
  {
    private Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback;

    public StatsResultCallback(Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback)
    {
      this.callback = callback;
    }

    public override void OnResult(Stats_LoadPlayerStatsResultObject arg_Result_1)
    {
      this.callback(arg_Result_1.getStatus().getStatusCode(), arg_Result_1.getPlayerStats());
    }
  }
}
