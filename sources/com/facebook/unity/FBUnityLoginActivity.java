package com.facebook.unity;

import android.os.Bundle;
import com.facebook.CallbackManager;

public class FBUnityLoginActivity extends BaseActivity {
    public static final String LOGIN_PARAMS = "login_params";
    public static final String LOGIN_TYPE = "login_type";

    public enum LoginType {
        READ,
        PUBLISH,
        TV_READ,
        TV_PUBLISH
    }

    public CallbackManager getCallbackManager() {
        return this.mCallbackManager;
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        LoginType loginType = (LoginType) getIntent().getSerializableExtra(LOGIN_TYPE);
        String stringExtra = getIntent().getStringExtra(LOGIN_PARAMS);
        switch (loginType) {
            case READ:
                FBLogin.loginWithReadPermissions(stringExtra, this);
                return;
            case PUBLISH:
                FBLogin.loginWithPublishPermissions(stringExtra, this);
                return;
            case TV_READ:
                FBLogin.loginForTVWithReadPermissions(stringExtra, this);
                return;
            case TV_PUBLISH:
                FBLogin.loginForTVWithPublishPermissions(stringExtra, this);
                return;
            default:
                return;
        }
    }
}
