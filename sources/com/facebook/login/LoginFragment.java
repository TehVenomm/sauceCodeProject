package com.facebook.login;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.LayoutRes;
import android.support.annotation.Nullable;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.facebook.common.C0618R;
import com.facebook.login.LoginClient.OnCompletedListener;
import com.facebook.login.LoginClient.Request;
import com.facebook.login.LoginClient.Result;

public class LoginFragment extends Fragment {
    static final String EXTRA_REQUEST = "request";
    private static final String NULL_CALLING_PKG_ERROR_MSG = "Cannot call LoginFragment with a null calling package. This can occur if the launchMode of the caller is singleInstance.";
    static final String REQUEST_KEY = "com.facebook.LoginFragment:Request";
    static final String RESULT_KEY = "com.facebook.LoginFragment:Result";
    private static final String SAVED_LOGIN_CLIENT = "loginClient";
    private static final String TAG = "LoginFragment";
    private String callingPackage;
    private LoginClient loginClient;
    private Request request;

    private void initializeCallingPackage(Activity activity) {
        ComponentName callingActivity = activity.getCallingActivity();
        if (callingActivity != null) {
            this.callingPackage = callingActivity.getPackageName();
        }
    }

    /* access modifiers changed from: private */
    public void onLoginClientCompleted(Result result) {
        this.request = null;
        int i = result.code == Code.CANCEL ? 0 : -1;
        Bundle bundle = new Bundle();
        bundle.putParcelable(RESULT_KEY, result);
        Intent intent = new Intent();
        intent.putExtras(bundle);
        if (isAdded()) {
            getActivity().setResult(i, intent);
            getActivity().finish();
        }
    }

    /* access modifiers changed from: protected */
    public LoginClient createLoginClient() {
        return new LoginClient((Fragment) this);
    }

    /* access modifiers changed from: protected */
    @LayoutRes
    public int getLayoutResId() {
        return C0618R.C0622layout.com_facebook_login_fragment;
    }

    /* access modifiers changed from: 0000 */
    public LoginClient getLoginClient() {
        return this.loginClient;
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        this.loginClient.onActivityResult(i, i2, intent);
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (bundle != null) {
            this.loginClient = (LoginClient) bundle.getParcelable(SAVED_LOGIN_CLIENT);
            this.loginClient.setFragment(this);
        } else {
            this.loginClient = createLoginClient();
        }
        this.loginClient.setOnCompletedListener(new OnCompletedListener() {
            public void onCompleted(Result result) {
                LoginFragment.this.onLoginClientCompleted(result);
            }
        });
        FragmentActivity activity = getActivity();
        if (activity != null) {
            initializeCallingPackage(activity);
            Intent intent = activity.getIntent();
            if (intent != null) {
                Bundle bundleExtra = intent.getBundleExtra(REQUEST_KEY);
                if (bundleExtra != null) {
                    this.request = (Request) bundleExtra.getParcelable("request");
                }
            }
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        View inflate = layoutInflater.inflate(getLayoutResId(), viewGroup, false);
        final View findViewById = inflate.findViewById(C0618R.C0621id.com_facebook_login_fragment_progress_bar);
        this.loginClient.setBackgroundProcessingListener(new BackgroundProcessingListener() {
            public void onBackgroundProcessingStarted() {
                findViewById.setVisibility(0);
            }

            public void onBackgroundProcessingStopped() {
                findViewById.setVisibility(8);
            }
        });
        return inflate;
    }

    public void onDestroy() {
        this.loginClient.cancelCurrentHandler();
        super.onDestroy();
    }

    public void onPause() {
        super.onPause();
        View findViewById = getView() == null ? null : getView().findViewById(C0618R.C0621id.com_facebook_login_fragment_progress_bar);
        if (findViewById != null) {
            findViewById.setVisibility(8);
        }
    }

    public void onResume() {
        super.onResume();
        if (this.callingPackage == null) {
            Log.e(TAG, NULL_CALLING_PKG_ERROR_MSG);
            getActivity().finish();
            return;
        }
        this.loginClient.startOrContinueAuth(this.request);
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putParcelable(SAVED_LOGIN_CLIENT, this.loginClient);
    }
}
