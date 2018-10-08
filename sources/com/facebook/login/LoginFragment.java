package com.facebook.login;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.facebook.C0365R;
import com.facebook.FacebookActivity;
import com.facebook.FacebookOperationCanceledException;
import com.facebook.internal.ServerProtocol;
import com.facebook.internal.Utility;
import com.facebook.login.LoginClient.OnCompletedListener;
import com.facebook.login.LoginClient.Request;
import com.facebook.login.LoginClient.Result;
import org.json.JSONException;
import org.json.JSONObject;

public class LoginFragment extends Fragment {
    private static final int CHALLENGE_LENGTH = 20;
    static final String EXTRA_REQUEST = "request";
    private static final String NULL_CALLING_PKG_ERROR_MSG = "Cannot call LoginFragment with a null calling package. This can occur if the launchMode of the caller is singleInstance.";
    static final String REQUEST_KEY = "com.facebook.LoginFragment:Request";
    static final String RESULT_KEY = "com.facebook.LoginFragment:Result";
    private static final String SAVED_CHALLENGE = "challenge";
    private static final String SAVED_LOGIN_CLIENT = "loginClient";
    private static final String TAG = "LoginFragment";
    private String callingPackage;
    private String expectedChallenge;
    private LoginClient loginClient;
    private Request request;
    private boolean restarted;

    /* renamed from: com.facebook.login.LoginFragment$1 */
    class C04431 implements OnCompletedListener {
        C04431() {
        }

        public void onCompleted(Result result) {
            LoginFragment.this.onLoginClientCompleted(result);
        }
    }

    private void initializeCallingPackage(Activity activity) {
        ComponentName callingActivity = activity.getCallingActivity();
        if (callingActivity != null) {
            this.callingPackage = callingActivity.getPackageName();
        }
    }

    private void onLoginClientCompleted(Result result) {
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

    public String getChallengeParam() {
        return this.expectedChallenge;
    }

    LoginClient getLoginClient() {
        return this.loginClient;
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        this.loginClient.onActivityResult(i, i2, intent);
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.restarted = bundle != null;
        if (bundle != null) {
            this.loginClient = (LoginClient) bundle.getParcelable(SAVED_LOGIN_CLIENT);
            this.loginClient.setFragment(this);
            this.expectedChallenge = bundle.getString(SAVED_CHALLENGE);
        } else {
            this.loginClient = new LoginClient((Fragment) this);
            this.expectedChallenge = Utility.generateRandomString(20);
        }
        this.loginClient.setOnCompletedListener(new C04431());
        Activity activity = getActivity();
        if (activity != null) {
            initializeCallingPackage(activity);
            if (activity.getIntent() != null) {
                this.request = (Request) activity.getIntent().getBundleExtra(REQUEST_KEY).getParcelable("request");
            }
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        final View inflate = layoutInflater.inflate(C0365R.layout.com_facebook_login_fragment, viewGroup, false);
        this.loginClient.setBackgroundProcessingListener(new BackgroundProcessingListener() {
            public void onBackgroundProcessingStarted() {
                inflate.findViewById(C0365R.id.com_facebook_login_activity_progress_bar).setVisibility(0);
            }

            public void onBackgroundProcessingStopped() {
                inflate.findViewById(C0365R.id.com_facebook_login_activity_progress_bar).setVisibility(8);
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
        getActivity().findViewById(C0365R.id.com_facebook_login_activity_progress_bar).setVisibility(8);
    }

    public void onResume() {
        super.onResume();
        if (this.callingPackage == null) {
            Log.e(TAG, NULL_CALLING_PKG_ERROR_MSG);
            getActivity().finish();
            return;
        }
        if (this.restarted) {
            FragmentActivity activity = getActivity();
            if ((activity instanceof FacebookActivity) && (this.loginClient.getCurrentHandler() instanceof CustomTabLoginMethodHandler)) {
                ((FacebookActivity) activity).sendResult(null, new FacebookOperationCanceledException());
            }
        }
        this.restarted = true;
        this.loginClient.startOrContinueAuth(this.request);
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putParcelable(SAVED_LOGIN_CLIENT, this.loginClient);
        bundle.putString(SAVED_CHALLENGE, this.expectedChallenge);
    }

    public boolean validateChallengeParam(Bundle bundle) {
        boolean z = false;
        try {
            String string = bundle.getString(ServerProtocol.DIALOG_PARAM_STATE);
            if (string != null) {
                z = new JSONObject(string).getString("7_challenge").equals(this.expectedChallenge);
            }
        } catch (JSONException e) {
        }
        return z;
    }
}
