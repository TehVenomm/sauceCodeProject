// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Android.AndroidTokenClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.Android;

internal class AndroidTokenClient : TokenClient
{
  private const string TokenFragmentClass = "com.google.games.bridge.TokenFragment";
  private const string FetchTokenSignature = "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;";
  private const string FetchTokenMethod = "fetchToken";
  private bool requestEmail;
  private bool requestAuthCode;
  private bool requestIdToken;
  private List<string> oauthScopes;
  private string webClientId;
  private bool forceRefresh;
  private bool hidePopups;
  private string accountName;
  private string email;
  private string authCode;
  private string idToken;

  public static AndroidJavaObject GetActivity()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
      return ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity");
  }

  public void SetRequestAuthCode(bool flag, bool forceRefresh)
  {
    this.requestAuthCode = flag;
    this.forceRefresh = forceRefresh;
  }

  public void SetRequestEmail(bool flag) => this.requestEmail = flag;

  public void SetRequestIdToken(bool flag) => this.requestIdToken = flag;

  public void SetWebClientId(string webClientId) => this.webClientId = webClientId;

  public void SetHidePopups(bool flag) => this.hidePopups = flag;

  public void SetAccountName(string accountName) => this.accountName = accountName;

  public void AddOauthScopes(string[] scopes)
  {
    if (scopes == null)
      return;
    if (this.oauthScopes == null)
      this.oauthScopes = new List<string>();
    this.oauthScopes.AddRange((IEnumerable<string>) scopes);
  }

  public void Signout()
  {
    this.authCode = (string) null;
    this.email = (string) null;
    this.idToken = (string) null;
    PlayGamesHelperObject.RunOnGameThread((Action) (() =>
    {
      Debug.Log((object) "Calling Signout in token client");
      ((AndroidJavaObject) new AndroidJavaClass("com.google.games.bridge.TokenFragment")).CallStatic("signOut", Array.Empty<object>());
    }));
  }

  public bool NeedsToRun()
  {
    if (this.requestAuthCode && string.IsNullOrEmpty(this.authCode) || this.requestEmail && string.IsNullOrEmpty(this.email))
      return true;
    return this.requestIdToken && string.IsNullOrEmpty(this.idToken);
  }

  public void FetchTokens(Action callback)
  {
    PlayGamesHelperObject.RunOnGameThread((Action) (() => this.DoFetchToken(callback)));
  }

  internal void DoFetchToken(Action callback)
  {
    object[] objArray = new object[9];
    jvalue[] jniArgArray = AndroidJNIHelper.CreateJNIArgArray(objArray);
    try
    {
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.TokenFragment"))
      {
        using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
        {
          IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(((AndroidJavaObject) androidJavaClass).GetRawClass(), "fetchToken", "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;");
          jniArgArray[0].l = activity.GetRawObject();
          jniArgArray[1].z = this.requestAuthCode;
          jniArgArray[2].z = this.requestEmail;
          jniArgArray[3].z = this.requestIdToken;
          jniArgArray[4].l = AndroidJNI.NewStringUTF(this.webClientId);
          jniArgArray[5].z = this.forceRefresh;
          jniArgArray[6].l = AndroidJNIHelper.ConvertToJNIArray((Array) this.oauthScopes.ToArray());
          jniArgArray[7].z = this.hidePopups;
          jniArgArray[8].l = AndroidJNI.NewStringUTF(this.accountName);
          new PendingResult<TokenResult>(AndroidJNI.CallStaticObjectMethod(((AndroidJavaObject) androidJavaClass).GetRawClass(), staticMethodId, jniArgArray)).setResultCallback((ResultCallback<TokenResult>) new TokenResultCallback((Action<int, string, string, string>) ((rc, authCode, email, idToken) =>
          {
            this.authCode = authCode;
            this.email = email;
            this.idToken = idToken;
            callback();
          })));
        }
      }
    }
    catch (Exception ex)
    {
      Logger.e("Exception launching token request: " + ex.Message);
      Logger.e(ex.ToString());
    }
    finally
    {
      AndroidJNIHelper.DeleteJNIArgArray(objArray, jniArgArray);
    }
  }

  internal static void FetchToken(
    bool fetchAuthCode,
    bool fetchEmail,
    bool fetchIdToken,
    string webClientId,
    bool forceRefresh,
    Action<int, string, string, string> callback)
  {
    object[] objArray = new object[7];
    jvalue[] jniArgArray = AndroidJNIHelper.CreateJNIArgArray(objArray);
    try
    {
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.TokenFragment"))
      {
        using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
        {
          IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(((AndroidJavaObject) androidJavaClass).GetRawClass(), "fetchToken", "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;");
          jniArgArray[0].l = activity.GetRawObject();
          jniArgArray[1].z = fetchAuthCode;
          jniArgArray[2].z = fetchEmail;
          jniArgArray[3].z = fetchIdToken;
          jniArgArray[4].l = AndroidJNI.NewStringUTF(webClientId);
          jniArgArray[5].z = forceRefresh;
          new PendingResult<TokenResult>(AndroidJNI.CallStaticObjectMethod(((AndroidJavaObject) androidJavaClass).GetRawClass(), staticMethodId, jniArgArray)).setResultCallback((ResultCallback<TokenResult>) new TokenResultCallback(callback));
        }
      }
    }
    catch (Exception ex)
    {
      Logger.e("Exception launching token request: " + ex.Message);
      Logger.e(ex.ToString());
    }
    finally
    {
      AndroidJNIHelper.DeleteJNIArgArray(objArray, jniArgArray);
    }
  }

  public string GetEmail() => this.email;

  public string GetAuthCode() => this.authCode;

  public string GetIdToken() => this.idToken;
}
