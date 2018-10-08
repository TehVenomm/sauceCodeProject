package com.facebook;

import android.content.Intent;
import android.content.res.Configuration;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.util.Log;
import com.facebook.internal.AnalyticsEvents;
import com.facebook.internal.FacebookDialogFragment;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.Utility;
import com.facebook.login.LoginFragment;
import com.facebook.login.LoginManager;
import com.facebook.share.internal.DeviceShareDialogFragment;
import com.facebook.share.model.ShareContent;
import com.google.firebase.analytics.FirebaseAnalytics.Param;

public class FacebookActivity extends FragmentActivity {
    private static final int API_EC_DIALOG_CANCEL = 4201;
    private static String FRAGMENT_TAG = "SingleFragment";
    public static String PASS_THROUGH_CANCEL_ACTION = "PassThrough";
    private static final String TAG = FacebookActivity.class.getName();
    private Fragment singleFragment;

    private static final String getRedirectUrl() {
        return "fb" + FacebookSdk.getApplicationId() + "://authorize";
    }

    private void handlePassThroughError() {
        sendResult(null, NativeProtocol.getExceptionFromErrorData(NativeProtocol.getMethodArgumentsFromIntent(getIntent())));
    }

    private void handlePassThroughUrl(String str) {
        if (str != null && str.startsWith(getRedirectUrl())) {
            int i;
            Uri parse = Uri.parse(str);
            Bundle parseUrlQueryString = Utility.parseUrlQueryString(parse.getQuery());
            parseUrlQueryString.putAll(Utility.parseUrlQueryString(parse.getFragment()));
            if (!((this.singleFragment instanceof LoginFragment) && ((LoginFragment) this.singleFragment).validateChallengeParam(parseUrlQueryString))) {
                sendResult(null, new FacebookException("Invalid state parameter"));
            }
            String string = parseUrlQueryString.getString("error");
            if (string == null) {
                string = parseUrlQueryString.getString(NativeProtocol.BRIDGE_ARG_ERROR_TYPE);
            }
            String string2 = parseUrlQueryString.getString("error_msg");
            if (string2 == null) {
                string2 = parseUrlQueryString.getString(AnalyticsEvents.PARAMETER_SHARE_ERROR_MESSAGE);
            }
            if (string2 == null) {
                string2 = parseUrlQueryString.getString(NativeProtocol.BRIDGE_ARG_ERROR_DESCRIPTION);
            }
            String string3 = parseUrlQueryString.getString(NativeProtocol.BRIDGE_ARG_ERROR_CODE);
            if (Utility.isNullOrEmpty(string3)) {
                i = -1;
            } else {
                try {
                    i = Integer.parseInt(string3);
                } catch (NumberFormatException e) {
                    i = -1;
                }
            }
            if (Utility.isNullOrEmpty(string) && Utility.isNullOrEmpty(string2) && i == -1) {
                sendResult(parseUrlQueryString, null);
            } else if (string != null && (string.equals("access_denied") || string.equals("OAuthAccessDeniedException"))) {
                sendResult(null, new FacebookOperationCanceledException());
            } else if (i == API_EC_DIALOG_CANCEL) {
                sendResult(null, new FacebookOperationCanceledException());
            } else {
                sendResult(null, new FacebookServiceException(new FacebookRequestError(i, string, string2), string2));
            }
        }
    }

    public Fragment getCurrentFragment() {
        return this.singleFragment;
    }

    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        if (this.singleFragment != null) {
            this.singleFragment.onConfigurationChanged(configuration);
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (!FacebookSdk.isInitialized()) {
            Log.d(TAG, "Facebook SDK not initialized. Make sure you call sdkInitialize inside your Application's onCreate method.");
            FacebookSdk.sdkInitialize(getApplicationContext());
        }
        setContentView(C0365R.layout.com_facebook_activity_layout);
        Intent intent = getIntent();
        if (PASS_THROUGH_CANCEL_ACTION.equals(intent.getAction())) {
            handlePassThroughError();
            return;
        }
        FragmentManager supportFragmentManager = getSupportFragmentManager();
        Fragment findFragmentByTag = supportFragmentManager.findFragmentByTag(FRAGMENT_TAG);
        if (findFragmentByTag == null) {
            if (FacebookDialogFragment.TAG.equals(intent.getAction())) {
                findFragmentByTag = new FacebookDialogFragment();
                findFragmentByTag.setRetainInstance(true);
                findFragmentByTag.show(supportFragmentManager, FRAGMENT_TAG);
            } else if (DeviceShareDialogFragment.TAG.equals(intent.getAction())) {
                Fragment deviceShareDialogFragment = new DeviceShareDialogFragment();
                deviceShareDialogFragment.setRetainInstance(true);
                deviceShareDialogFragment.setShareContent((ShareContent) intent.getParcelableExtra(Param.CONTENT));
                deviceShareDialogFragment.show(supportFragmentManager, FRAGMENT_TAG);
                findFragmentByTag = deviceShareDialogFragment;
            } else {
                findFragmentByTag = new LoginFragment();
                findFragmentByTag.setRetainInstance(true);
                supportFragmentManager.beginTransaction().add(C0365R.id.com_facebook_fragment_container, findFragmentByTag, FRAGMENT_TAG).commit();
            }
        }
        this.singleFragment = findFragmentByTag;
    }

    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        handlePassThroughUrl(intent.getStringExtra("url"));
    }

    public void sendResult(Bundle bundle, FacebookException facebookException) {
        int i;
        Intent intent = getIntent();
        if (facebookException == null) {
            i = -1;
            LoginManager.setSuccessResult(intent, bundle);
        } else {
            i = 0;
            intent = NativeProtocol.createProtocolResultIntent(intent, bundle, facebookException);
        }
        setResult(i, intent);
        finish();
    }
}
