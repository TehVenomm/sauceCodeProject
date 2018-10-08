package com.facebook.unity;

import android.app.Activity;
import android.text.TextUtils;
import android.util.Log;
import com.facebook.AccessToken;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.internal.NativeProtocol;
import com.facebook.login.LoginManager;
import com.facebook.login.LoginResult;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;

public class FBLogin {
    public static void addLoginParametersToMessage(UnityMessage unityMessage, AccessToken accessToken, String str) {
        unityMessage.put("key_hash", FB.getKeyHash());
        unityMessage.put("opened", Boolean.valueOf(true));
        unityMessage.put("access_token", accessToken.getToken());
        unityMessage.put("expiration_timestamp", Long.valueOf(accessToken.getExpires().getTime() / 1000).toString());
        unityMessage.put(AccessToken.USER_ID_KEY, accessToken.getUserId());
        unityMessage.put(NativeProtocol.RESULT_ARGS_PERMISSIONS, TextUtils.join(",", accessToken.getPermissions()));
        unityMessage.put("declined_permissions", TextUtils.join(",", accessToken.getDeclinedPermissions()));
        if (accessToken.getLastRefresh() != null) {
            unityMessage.put("last_refresh", Long.valueOf(accessToken.getLastRefresh().getTime() / 1000).toString());
        }
        if (str != null && !str.isEmpty()) {
            unityMessage.put(Constants.CALLBACK_ID_KEY, str);
        }
    }

    private static void login(String str, FBUnityLoginActivity fBUnityLoginActivity, boolean z) {
        String str2 = null;
        if (FacebookSdk.isInitialized()) {
            final UnityMessage unityMessage = new UnityMessage("OnLoginComplete");
            unityMessage.put("key_hash", FB.getKeyHash());
            UnityParams parse = UnityParams.parse(str, "couldn't parse login params: " + str);
            Collection arrayList = parse.hasString("scope").booleanValue() ? new ArrayList(Arrays.asList(parse.getString("scope").split(","))) : null;
            if (parse.has(Constants.CALLBACK_ID_KEY)) {
                str2 = parse.getString(Constants.CALLBACK_ID_KEY);
                unityMessage.put(Constants.CALLBACK_ID_KEY, str2);
            }
            LoginManager.getInstance().registerCallback(fBUnityLoginActivity.getCallbackManager(), new FacebookCallback<LoginResult>() {
                public void onCancel() {
                    unityMessage.putCancelled();
                    unityMessage.send();
                }

                public void onError(FacebookException facebookException) {
                    unityMessage.sendError(facebookException.getMessage());
                }

                public void onSuccess(LoginResult loginResult) {
                    FBLogin.sendLoginSuccessMessage(loginResult.getAccessToken(), str2);
                }
            });
            if (z) {
                LoginManager.getInstance().logInWithPublishPermissions((Activity) fBUnityLoginActivity, arrayList);
                return;
            } else {
                LoginManager.getInstance().logInWithReadPermissions((Activity) fBUnityLoginActivity, arrayList);
                return;
            }
        }
        Log.w(FB.TAG, "Facebook SDK not initialized. Call init() before calling login()");
    }

    public static void loginWithPublishPermissions(String str, FBUnityLoginActivity fBUnityLoginActivity) {
        login(str, fBUnityLoginActivity, true);
    }

    public static void loginWithReadPermissions(String str, FBUnityLoginActivity fBUnityLoginActivity) {
        login(str, fBUnityLoginActivity, false);
    }

    public static void sendLoginSuccessMessage(AccessToken accessToken, String str) {
        UnityMessage unityMessage = new UnityMessage("OnLoginComplete");
        addLoginParametersToMessage(unityMessage, accessToken, str);
        unityMessage.send();
    }
}
