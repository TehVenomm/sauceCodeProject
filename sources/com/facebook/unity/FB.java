package com.facebook.unity;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.Signature;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.util.Base64;
import android.util.Log;
import com.facebook.AccessToken;
import com.facebook.AccessToken.AccessTokenRefreshCallback;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.FacebookSdk.InitializeCallback;
import com.facebook.appevents.AppEventsLogger;
import com.facebook.appevents.internal.ActivityLifecycleTracker;
import com.facebook.applinks.AppLinkData;
import com.facebook.applinks.AppLinkData.CompletionHandler;
import com.facebook.internal.BundleJSONConverter;
import com.facebook.internal.InternalSettings;
import com.facebook.internal.Utility;
import com.facebook.login.LoginManager;
import com.facebook.share.widget.ShareDialog.Mode;
import com.facebook.unity.FBUnityLoginActivity.LoginType;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.math.BigDecimal;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Currency;
import java.util.Locale;
import java.util.concurrent.atomic.AtomicBoolean;
import org.json.JSONException;

public class FB {
    static final String FB_UNITY_OBJECT = "UnityFacebookSDKPlugin";
    static Mode ShareDialogMode = Mode.AUTOMATIC;
    static final String TAG = FB.class.getName();
    private static AtomicBoolean activateAppCalled = new AtomicBoolean();
    private static AppEventsLogger appEventsLogger;
    private static Intent intent;

    private static void ActivateApp(String str) {
        if (activateAppCalled.compareAndSet(false, true)) {
            final Activity unityActivity = getUnityActivity();
            if (str != null) {
                AppEventsLogger.activateApp(unityActivity.getApplication(), str);
            } else {
                AppEventsLogger.activateApp(unityActivity.getApplication());
            }
            new Handler(Looper.getMainLooper()).post(new Runnable() {
                public void run() {
                    ActivityLifecycleTracker.onActivityCreated(unityActivity);
                    ActivityLifecycleTracker.onActivityResumed(unityActivity);
                }
            });
            return;
        }
        Log.w(TAG, "Activite app only needs to be called once");
    }

    @UnityCallable
    public static void AppInvite(String str) {
        Log.v(TAG, "AppInvite(" + str + ")");
        Intent intent = new Intent(getUnityActivity(), AppInviteDialogActivity.class);
        intent.putExtra(AppInviteDialogActivity.DIALOG_PARAMS, UnityParams.parse(str).getStringParams());
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void AppRequest(String str) {
        Log.v(TAG, "AppRequest(" + str + ")");
        Intent intent = new Intent(getUnityActivity(), FBUnityGameRequestActivity.class);
        intent.putExtra(FBUnityGameRequestActivity.GAME_REQUEST_PARAMS, UnityParams.parse(str).getStringParams());
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void FeedShare(String str) {
        Log.v(TAG, "FeedShare(" + str + ")");
        Bundle stringParams = UnityParams.parse(str).getStringParams();
        Intent intent = new Intent(getUnityActivity(), FBUnityDialogsActivity.class);
        intent.putExtra(FBUnityDialogsActivity.DIALOG_TYPE, Mode.FEED);
        intent.putExtra(FBUnityDialogsActivity.FEED_DIALOG_PARAMS, stringParams);
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void FetchDeferredAppLinkData(String str) {
        LogMethodCall("FetchDeferredAppLinkData", str);
        UnityParams parse = UnityParams.parse(str);
        final UnityMessage unityMessage = new UnityMessage("OnFetchDeferredAppLinkComplete");
        if (parse.hasString(Constants.CALLBACK_ID_KEY).booleanValue()) {
            unityMessage.put(Constants.CALLBACK_ID_KEY, parse.getString(Constants.CALLBACK_ID_KEY));
        }
        AppLinkData.fetchDeferredAppLinkData(getUnityActivity(), new CompletionHandler() {
            public void onDeferredAppLinkDataFetched(AppLinkData appLinkData) {
                FB.addAppLinkToMessage(unityMessage, appLinkData);
                unityMessage.send();
            }
        });
    }

    @UnityCallable
    public static void GameGroupCreate(String str) {
        Log.v(TAG, "GameGroupCreate(" + str + ")");
        Bundle stringParams = UnityParams.parse(str).getStringParams();
        Intent intent = new Intent(getUnityActivity(), FBUnityCreateGameGroupActivity.class);
        intent.putExtra(FBUnityCreateGameGroupActivity.CREATE_GAME_GROUP_PARAMS, stringParams);
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void GameGroupJoin(String str) {
        Log.v(TAG, "GameGroupJoin(" + str + ")");
        Bundle stringParams = UnityParams.parse(str).getStringParams();
        Intent intent = new Intent(getUnityActivity(), FBUnityJoinGameGroupActivity.class);
        intent.putExtra(FBUnityJoinGameGroupActivity.JOIN_GAME_GROUP_PARAMS, stringParams);
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void GetAppLink(String str) {
        Log.v(TAG, "GetAppLink(" + str + ")");
        UnityMessage createWithCallbackFromParams = UnityMessage.createWithCallbackFromParams("OnGetAppLinkComplete", UnityParams.parse(str));
        if (intent == null) {
            createWithCallbackFromParams.put("did_complete", Boolean.valueOf(true));
            createWithCallbackFromParams.send();
            return;
        }
        AppLinkData createFromAlApplinkData = AppLinkData.createFromAlApplinkData(intent);
        if (createFromAlApplinkData != null) {
            addAppLinkToMessage(createWithCallbackFromParams, createFromAlApplinkData);
            createWithCallbackFromParams.put("url", intent.getDataString());
        } else if (intent.getData() != null) {
            createWithCallbackFromParams.put("url", intent.getDataString());
        } else {
            createWithCallbackFromParams.put("did_complete", Boolean.valueOf(true));
        }
        createWithCallbackFromParams.send();
    }

    @UnityCallable
    public static String GetSdkVersion() {
        return FacebookSdk.getSdkVersion();
    }

    @UnityCallable
    public static void Init(String str) {
        Log.v(TAG, "Init(" + str + ")");
        UnityParams parse = UnityParams.parse(str, "couldn't parse init params: " + str);
        final String string = parse.hasString("appId").booleanValue() ? parse.getString("appId") : Utility.getMetadataApplicationId(getUnityActivity());
        FacebookSdk.setApplicationId(string);
        FacebookSdk.sdkInitialize(getUnityActivity(), new InitializeCallback() {
            public void onInitialized() {
                UnityMessage unityMessage = new UnityMessage("OnInitComplete");
                AccessToken currentAccessToken = AccessToken.getCurrentAccessToken();
                if (currentAccessToken != null) {
                    FBLogin.addLoginParametersToMessage(unityMessage, currentAccessToken, null);
                } else {
                    unityMessage.put("key_hash", FB.getKeyHash());
                }
                FB.ActivateApp(string);
                unityMessage.send();
            }
        });
    }

    @UnityCallable
    public static void LogAppEvent(String str) {
        Log.v(TAG, "LogAppEvent(" + str + ")");
        UnityParams parse = UnityParams.parse(str);
        Bundle bundle = new Bundle();
        if (parse.has("parameters")) {
            bundle = parse.getParamsObject("parameters").getStringParams();
        }
        if (parse.has("logPurchase")) {
            getAppEventsLogger().logPurchase(new BigDecimal(parse.getDouble("logPurchase")), Currency.getInstance(parse.getString(Param.CURRENCY)), bundle);
        } else if (!parse.hasString("logEvent").booleanValue()) {
            Log.e(TAG, "couldn't logPurchase or logEvent params: " + str);
        } else if (parse.has("valueToSum")) {
            getAppEventsLogger().logEvent(parse.getString("logEvent"), parse.getDouble("valueToSum"), bundle);
        } else {
            getAppEventsLogger().logEvent(parse.getString("logEvent"), bundle);
        }
    }

    private static void LogMethodCall(String str, String str2) {
        Log.v(TAG, String.format(Locale.ROOT, "%s(%s)", new Object[]{str, str2}));
    }

    @UnityCallable
    public static void LoginWithPublishPermissions(String str) {
        Log.v(TAG, "LoginWithPublishPermissions(" + str + ")");
        Intent intent = new Intent(getUnityActivity(), FBUnityLoginActivity.class);
        intent.putExtra(FBUnityLoginActivity.LOGIN_PARAMS, str);
        intent.putExtra(FBUnityLoginActivity.LOGIN_TYPE, LoginType.PUBLISH);
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void LoginWithReadPermissions(String str) {
        Log.v(TAG, "LoginWithReadPermissions(" + str + ")");
        Intent intent = new Intent(getUnityActivity(), FBUnityLoginActivity.class);
        intent.putExtra(FBUnityLoginActivity.LOGIN_PARAMS, str);
        intent.putExtra(FBUnityLoginActivity.LOGIN_TYPE, LoginType.READ);
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void Logout(String str) {
        Log.v(TAG, "Logout(" + str + ")");
        LoginManager.getInstance().logOut();
        UnityMessage unityMessage = new UnityMessage("OnLogoutComplete");
        unityMessage.put("did_complete", Boolean.valueOf(true));
        unityMessage.send();
    }

    @UnityCallable
    public static void RefreshCurrentAccessToken(String str) {
        LogMethodCall("RefreshCurrentAccessToken", str);
        UnityParams parse = UnityParams.parse(str);
        final UnityMessage unityMessage = new UnityMessage("OnRefreshCurrentAccessTokenComplete");
        if (parse.hasString(Constants.CALLBACK_ID_KEY).booleanValue()) {
            unityMessage.put(Constants.CALLBACK_ID_KEY, parse.getString(Constants.CALLBACK_ID_KEY));
        }
        AccessToken.refreshCurrentAccessTokenAsync(new AccessTokenRefreshCallback() {
            public void OnTokenRefreshFailed(FacebookException facebookException) {
                unityMessage.sendError(facebookException.getMessage());
            }

            public void OnTokenRefreshed(AccessToken accessToken) {
                FBLogin.addLoginParametersToMessage(unityMessage, accessToken, null);
                unityMessage.send();
            }
        });
        AppLinkData.fetchDeferredAppLinkData(getUnityActivity(), new CompletionHandler() {
            public void onDeferredAppLinkDataFetched(AppLinkData appLinkData) {
                FB.addAppLinkToMessage(unityMessage, appLinkData);
                unityMessage.send();
            }
        });
    }

    public static void SetIntent(Intent intent) {
        intent = intent;
    }

    public static void SetLimitEventUsage(String str) {
        Log.v(TAG, "SetLimitEventUsage(" + str + ")");
        FacebookSdk.setLimitEventAndDataUsage(getUnityActivity().getApplicationContext(), Boolean.valueOf(str).booleanValue());
    }

    @UnityCallable
    public static void SetShareDialogMode(String str) {
        Log.v(TAG, "SetShareDialogMode(" + str + ")");
        if (str.equalsIgnoreCase("NATIVE")) {
            ShareDialogMode = Mode.NATIVE;
        } else if (str.equalsIgnoreCase("WEB")) {
            ShareDialogMode = Mode.WEB;
        } else if (str.equalsIgnoreCase("FEED")) {
            ShareDialogMode = Mode.FEED;
        } else {
            ShareDialogMode = Mode.AUTOMATIC;
        }
    }

    @UnityCallable
    public static void SetUserAgentSuffix(String str) {
        Log.v(TAG, "SetUserAgentSuffix(" + str + ")");
        InternalSettings.setCustomUserAgent(str);
    }

    @UnityCallable
    public static void ShareLink(String str) {
        Log.v(TAG, "ShareLink(" + str + ")");
        Bundle stringParams = UnityParams.parse(str).getStringParams();
        Intent intent = new Intent(getUnityActivity(), FBUnityDialogsActivity.class);
        intent.putExtra(FBUnityDialogsActivity.DIALOG_TYPE, ShareDialogMode);
        intent.putExtra(FBUnityDialogsActivity.SHARE_DIALOG_PARAMS, stringParams);
        getUnityActivity().startActivity(intent);
    }

    private static void addAppLinkToMessage(UnityMessage unityMessage, AppLinkData appLinkData) {
        if (appLinkData == null) {
            unityMessage.put("did_complete", Boolean.valueOf(true));
            return;
        }
        unityMessage.put("ref", appLinkData.getRef());
        unityMessage.put("target_url", appLinkData.getTargetUri().toString());
        try {
            if (appLinkData.getArgumentBundle() != null) {
                unityMessage.put(AppLinkData.ARGUMENTS_EXTRAS_KEY, BundleJSONConverter.convertToJSON(appLinkData.getArgumentBundle()).toString());
            }
        } catch (JSONException e) {
            Log.e(TAG, e.getLocalizedMessage());
        }
    }

    private static AppEventsLogger getAppEventsLogger() {
        if (appEventsLogger == null) {
            appEventsLogger = AppEventsLogger.newLogger(getUnityActivity().getApplicationContext());
        }
        return appEventsLogger;
    }

    @TargetApi(8)
    public static String getKeyHash() {
        try {
            Activity unityActivity = getUnityActivity();
            if (unityActivity == null) {
                return "";
            }
            Signature[] signatureArr = unityActivity.getPackageManager().getPackageInfo(unityActivity.getPackageName(), 64).signatures;
            if (signatureArr.length < 0) {
                Signature signature = signatureArr[0];
                MessageDigest instance = MessageDigest.getInstance("SHA");
                instance.update(signature.toByteArray());
                String encodeToString = Base64.encodeToString(instance.digest(), 0);
                Log.d(TAG, "KeyHash: " + encodeToString);
                return encodeToString;
            }
            return "";
        } catch (NameNotFoundException e) {
        } catch (NoSuchAlgorithmException e2) {
        }
    }

    public static Activity getUnityActivity() {
        return UnityReflection.GetUnityActivity();
    }

    private static void startActivity(Class<?> cls, String str) {
        Intent intent = new Intent(getUnityActivity(), cls);
        intent.putExtra(BaseActivity.ACTIVITY_PARAMS, UnityParams.parse(str).getStringParams());
        getUnityActivity().startActivity(intent);
    }
}
