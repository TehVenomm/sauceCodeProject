using Com.Google.Android.Gms.Common.Api;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.Android
{
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
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			try
			{
				return val.GetStatic<AndroidJavaObject>("currentActivity");
				IL_001c:
				AndroidJavaObject result;
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public void SetRequestAuthCode(bool flag, bool forceRefresh)
		{
			requestAuthCode = flag;
			this.forceRefresh = forceRefresh;
		}

		public void SetRequestEmail(bool flag)
		{
			requestEmail = flag;
		}

		public void SetRequestIdToken(bool flag)
		{
			requestIdToken = flag;
		}

		public void SetWebClientId(string webClientId)
		{
			this.webClientId = webClientId;
		}

		public void SetHidePopups(bool flag)
		{
			hidePopups = flag;
		}

		public void SetAccountName(string accountName)
		{
			this.accountName = accountName;
		}

		public void AddOauthScopes(string[] scopes)
		{
			if (scopes != null)
			{
				if (oauthScopes == null)
				{
					oauthScopes = new List<string>();
				}
				oauthScopes.AddRange(scopes);
			}
		}

		public void Signout()
		{
			authCode = null;
			email = null;
			idToken = null;
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				//IL_000f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Expected O, but got Unknown
				Debug.Log((object)"Calling Signout in token client");
				AndroidJavaClass val = new AndroidJavaClass("com.google.games.bridge.TokenFragment");
				val.CallStatic("signOut", new object[0]);
			});
		}

		public bool NeedsToRun()
		{
			return (requestAuthCode && string.IsNullOrEmpty(authCode)) || (requestEmail && string.IsNullOrEmpty(email)) || (requestIdToken && string.IsNullOrEmpty(idToken));
		}

		public void FetchTokens(Action callback)
		{
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				DoFetchToken(callback);
			});
		}

		internal void DoFetchToken(Action callback)
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			object[] array = new object[9];
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			try
			{
				AndroidJavaClass val = new AndroidJavaClass("com.google.games.bridge.TokenFragment");
				try
				{
					AndroidJavaObject activity = GetActivity();
					try
					{
						IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(val.GetRawClass(), "fetchToken", "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;");
						array2[0].l = activity.GetRawObject();
						array2[1].z = requestAuthCode;
						array2[2].z = requestEmail;
						array2[3].z = requestIdToken;
						array2[4].l = AndroidJNI.NewStringUTF(webClientId);
						array2[5].z = forceRefresh;
						array2[6].l = AndroidJNIHelper.ConvertToJNIArray((Array)oauthScopes.ToArray());
						array2[7].z = hidePopups;
						array2[8].l = AndroidJNI.NewStringUTF(accountName);
						IntPtr ptr = AndroidJNI.CallStaticObjectMethod(val.GetRawClass(), staticMethodID, array2);
						PendingResult<TokenResult> pendingResult = new PendingResult<TokenResult>(ptr);
						pendingResult.setResultCallback(new TokenResultCallback(delegate(int rc, string authCode, string email, string idToken)
						{
							this.authCode = authCode;
							this.email = email;
							this.idToken = idToken;
							callback();
						}));
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
				Logger.e("Exception launching token request: " + ex.Message);
				Logger.e(ex.ToString());
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(array, array2);
			}
		}

		internal static void FetchToken(bool fetchAuthCode, bool fetchEmail, bool fetchIdToken, string webClientId, bool forceRefresh, Action<int, string, string, string> callback)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			object[] array = new object[7];
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			try
			{
				AndroidJavaClass val = new AndroidJavaClass("com.google.games.bridge.TokenFragment");
				try
				{
					AndroidJavaObject activity = GetActivity();
					try
					{
						IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(val.GetRawClass(), "fetchToken", "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;");
						array2[0].l = activity.GetRawObject();
						array2[1].z = fetchAuthCode;
						array2[2].z = fetchEmail;
						array2[3].z = fetchIdToken;
						array2[4].l = AndroidJNI.NewStringUTF(webClientId);
						array2[5].z = forceRefresh;
						IntPtr ptr = AndroidJNI.CallStaticObjectMethod(val.GetRawClass(), staticMethodID, array2);
						PendingResult<TokenResult> pendingResult = new PendingResult<TokenResult>(ptr);
						pendingResult.setResultCallback(new TokenResultCallback(callback));
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
				Logger.e("Exception launching token request: " + ex.Message);
				Logger.e(ex.ToString());
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(array, array2);
			}
		}

		public string GetEmail()
		{
			return email;
		}

		public string GetAuthCode()
		{
			return authCode;
		}

		public string GetIdToken()
		{
			return idToken;
		}
	}
}
