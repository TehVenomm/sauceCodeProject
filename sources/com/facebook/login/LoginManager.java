package com.facebook.login;

import android.app.Activity;
import android.app.Fragment;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import com.facebook.AccessToken;
import com.facebook.CallbackManager;
import com.facebook.FacebookActivity;
import com.facebook.FacebookAuthorizationException;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.GraphResponse;
import com.facebook.LoginStatusCallback;
import com.facebook.Profile;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.internal.CallbackManagerImpl;
import com.facebook.internal.CallbackManagerImpl.Callback;
import com.facebook.internal.CallbackManagerImpl.RequestCodeOffset;
import com.facebook.internal.FragmentWrapper;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.PlatformServiceClient.CompletedListener;
import com.facebook.internal.ServerProtocol;
import com.facebook.internal.Utility;
import com.facebook.internal.Validate;
import com.facebook.login.LoginClient.Request;
import com.facebook.login.LoginClient.Result;
import com.facebook.share.internal.ShareConstants;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import p018jp.colopl.drapro.LocalNotificationAlarmReceiver;

public class LoginManager {
    private static final String EXPRESS_LOGIN_ALLOWED = "express_login_allowed";
    private static final String MANAGE_PERMISSION_PREFIX = "manage";
    private static final Set<String> OTHER_PUBLISH_PERMISSIONS = getOtherPublishPermissions();
    private static final String PREFERENCE_LOGIN_MANAGER = "com.facebook.loginManager";
    private static final String PUBLISH_PERMISSION_PREFIX = "publish";
    private static volatile LoginManager instance;
    private String authType = ServerProtocol.DIALOG_REREQUEST_AUTH_TYPE;
    private DefaultAudience defaultAudience = DefaultAudience.FRIENDS;
    private LoginBehavior loginBehavior = LoginBehavior.NATIVE_WITH_FALLBACK;
    private final SharedPreferences sharedPreferences;

    private static class ActivityStartActivityDelegate implements StartActivityDelegate {
        private final Activity activity;

        ActivityStartActivityDelegate(Activity activity2) {
            Validate.notNull(activity2, LocalNotificationAlarmReceiver.EXTRA_ACTIVITY);
            this.activity = activity2;
        }

        public Activity getActivityContext() {
            return this.activity;
        }

        public void startActivityForResult(Intent intent, int i) {
            this.activity.startActivityForResult(intent, i);
        }
    }

    private static class FragmentStartActivityDelegate implements StartActivityDelegate {
        private final FragmentWrapper fragment;

        FragmentStartActivityDelegate(FragmentWrapper fragmentWrapper) {
            Validate.notNull(fragmentWrapper, "fragment");
            this.fragment = fragmentWrapper;
        }

        public Activity getActivityContext() {
            return this.fragment.getActivity();
        }

        public void startActivityForResult(Intent intent, int i) {
            this.fragment.startActivityForResult(intent, i);
        }
    }

    private static class LoginLoggerHolder {
        private static LoginLogger logger;

        private LoginLoggerHolder() {
        }

        /* access modifiers changed from: private */
        public static LoginLogger getLogger(Context context) {
            LoginLogger loginLogger;
            synchronized (LoginLoggerHolder.class) {
                if (context == null) {
                    try {
                        context = FacebookSdk.getApplicationContext();
                    } catch (Throwable th) {
                        Class<LoginLoggerHolder> cls = LoginLoggerHolder.class;
                        throw th;
                    }
                }
                if (context == null) {
                    loginLogger = null;
                } else {
                    if (logger == null) {
                        logger = new LoginLogger(context, FacebookSdk.getApplicationId());
                    }
                    loginLogger = logger;
                }
            }
            return loginLogger;
        }
    }

    LoginManager() {
        Validate.sdkInitialized();
        this.sharedPreferences = FacebookSdk.getApplicationContext().getSharedPreferences(PREFERENCE_LOGIN_MANAGER, 0);
    }

    static LoginResult computeLoginResult(Request request, AccessToken accessToken) {
        Set permissions = request.getPermissions();
        HashSet hashSet = new HashSet(accessToken.getPermissions());
        if (request.isRerequest()) {
            hashSet.retainAll(permissions);
        }
        HashSet hashSet2 = new HashSet(permissions);
        hashSet2.removeAll(hashSet);
        return new LoginResult(accessToken, hashSet, hashSet2);
    }

    private Request createLoginRequestFromResponse(GraphResponse graphResponse) {
        Validate.notNull(graphResponse, "response");
        AccessToken accessToken = graphResponse.getRequest().getAccessToken();
        return createLoginRequest(accessToken != null ? accessToken.getPermissions() : null);
    }

    private void finishLogin(AccessToken accessToken, Request request, FacebookException facebookException, boolean z, FacebookCallback<LoginResult> facebookCallback) {
        if (accessToken != null) {
            AccessToken.setCurrentAccessToken(accessToken);
            Profile.fetchProfileForCurrentAccessToken();
        }
        if (facebookCallback != null) {
            LoginResult loginResult = accessToken != null ? computeLoginResult(request, accessToken) : null;
            if (z || (loginResult != null && loginResult.getRecentlyGrantedPermissions().size() == 0)) {
                facebookCallback.onCancel();
            } else if (facebookException != null) {
                facebookCallback.onError(facebookException);
            } else if (accessToken != null) {
                setExpressLoginStatus(true);
                facebookCallback.onSuccess(loginResult);
            }
        }
    }

    @Nullable
    static Map<String, String> getExtraDataFromIntent(Intent intent) {
        if (intent == null) {
            return null;
        }
        Result result = (Result) intent.getParcelableExtra("com.facebook.LoginFragment:Result");
        if (result != null) {
            return result.extraData;
        }
        return null;
    }

    public static LoginManager getInstance() {
        if (instance == null) {
            synchronized (LoginManager.class) {
                try {
                    if (instance == null) {
                        instance = new LoginManager();
                    }
                } finally {
                    while (true) {
                        Class<LoginManager> cls = LoginManager.class;
                    }
                }
            }
        }
        return instance;
    }

    private static Set<String> getOtherPublishPermissions() {
        return Collections.unmodifiableSet(new HashSet<String>() {
            {
                add("ads_management");
                add("create_event");
                add("rsvp_event");
            }
        });
    }

    /* access modifiers changed from: private */
    @Nullable
    public static Profile getProfileFromBundle(Bundle bundle) {
        String string = bundle.getString(NativeProtocol.EXTRA_ARGS_PROFILE_NAME);
        String string2 = bundle.getString(NativeProtocol.EXTRA_ARGS_PROFILE_FIRST_NAME);
        String string3 = bundle.getString(NativeProtocol.EXTRA_ARGS_PROFILE_MIDDLE_NAME);
        String string4 = bundle.getString(NativeProtocol.EXTRA_ARGS_PROFILE_LAST_NAME);
        String string5 = bundle.getString(NativeProtocol.EXTRA_ARGS_PROFILE_LINK);
        String string6 = bundle.getString(NativeProtocol.EXTRA_ARGS_PROFILE_USER_ID);
        if (string == null || string2 == null || string3 == null || string4 == null || string5 == null || string6 == null) {
            return null;
        }
        return new Profile(string6, string2, string3, string4, string, Uri.parse(string5));
    }

    /* access modifiers changed from: private */
    public static void handleLoginStatusError(String str, String str2, String str3, LoginLogger loginLogger, LoginStatusCallback loginStatusCallback) {
        FacebookException facebookException = new FacebookException(str + ": " + str2);
        loginLogger.logLoginStatusError(str3, facebookException);
        loginStatusCallback.onError(facebookException);
    }

    private boolean isExpressLoginAllowed() {
        return this.sharedPreferences.getBoolean(EXPRESS_LOGIN_ALLOWED, true);
    }

    static boolean isPublishPermission(String str) {
        return str != null && (str.startsWith(PUBLISH_PERMISSION_PREFIX) || str.startsWith(MANAGE_PERMISSION_PREFIX) || OTHER_PUBLISH_PERMISSIONS.contains(str));
    }

    private void logCompleteLogin(Context context, Code code, Map<String, String> map, Exception exc, boolean z, Request request) {
        LoginLogger access$000 = LoginLoggerHolder.getLogger(context);
        if (access$000 != null) {
            if (request == null) {
                access$000.logUnexpectedError("fb_mobile_login_complete", "Unexpected call to logCompleteLogin with null pendingAuthorizationRequest.");
                return;
            }
            HashMap hashMap = new HashMap();
            hashMap.put("try_login_activity", z ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
            access$000.logCompleteLogin(request.getAuthId(), hashMap, code, map, exc);
        }
    }

    private void logInWithPublishPermissions(FragmentWrapper fragmentWrapper, Collection<String> collection) {
        validatePublishPermissions(collection);
        startLogin(new FragmentStartActivityDelegate(fragmentWrapper), createLoginRequest(collection));
    }

    private void logInWithReadPermissions(FragmentWrapper fragmentWrapper, Collection<String> collection) {
        validateReadPermissions(collection);
        startLogin(new FragmentStartActivityDelegate(fragmentWrapper), createLoginRequest(collection));
    }

    private void logStartLogin(Context context, Request request) {
        LoginLogger access$000 = LoginLoggerHolder.getLogger(context);
        if (access$000 != null && request != null) {
            access$000.logStartLogin(request);
        }
    }

    private void reauthorizeDataAccess(FragmentWrapper fragmentWrapper) {
        startLogin(new FragmentStartActivityDelegate(fragmentWrapper), createReauthorizeRequest());
    }

    private void resolveError(FragmentWrapper fragmentWrapper, GraphResponse graphResponse) {
        startLogin(new FragmentStartActivityDelegate(fragmentWrapper), createLoginRequestFromResponse(graphResponse));
    }

    private boolean resolveIntent(Intent intent) {
        return FacebookSdk.getApplicationContext().getPackageManager().resolveActivity(intent, 0) != null;
    }

    private void retrieveLoginStatusImpl(Context context, LoginStatusCallback loginStatusCallback, long j) {
        String applicationId = FacebookSdk.getApplicationId();
        String uuid = UUID.randomUUID().toString();
        final LoginLogger loginLogger = new LoginLogger(context, applicationId);
        if (!isExpressLoginAllowed()) {
            loginLogger.logLoginStatusFailure(uuid);
            loginStatusCallback.onFailure();
            return;
        }
        LoginStatusClient loginStatusClient = new LoginStatusClient(context, applicationId, uuid, FacebookSdk.getGraphApiVersion(), j);
        final String str = uuid;
        final LoginStatusCallback loginStatusCallback2 = loginStatusCallback;
        final String str2 = applicationId;
        loginStatusClient.setCompletedListener(new CompletedListener() {
            public void completed(Bundle bundle) {
                if (bundle != null) {
                    String string = bundle.getString(NativeProtocol.STATUS_ERROR_TYPE);
                    String string2 = bundle.getString(NativeProtocol.STATUS_ERROR_DESCRIPTION);
                    if (string != null) {
                        LoginManager.handleLoginStatusError(string, string2, str, loginLogger, loginStatusCallback2);
                        return;
                    }
                    String string3 = bundle.getString(NativeProtocol.EXTRA_ACCESS_TOKEN);
                    Date bundleLongAsDate = Utility.getBundleLongAsDate(bundle, NativeProtocol.EXTRA_EXPIRES_SECONDS_SINCE_EPOCH, new Date(0));
                    ArrayList stringArrayList = bundle.getStringArrayList(NativeProtocol.EXTRA_PERMISSIONS);
                    String string4 = bundle.getString(NativeProtocol.RESULT_ARGS_SIGNED_REQUEST);
                    Date bundleLongAsDate2 = Utility.getBundleLongAsDate(bundle, NativeProtocol.EXTRA_DATA_ACCESS_EXPIRATION_TIME, new Date(0));
                    String str = !Utility.isNullOrEmpty(string4) ? LoginMethodHandler.getUserIDFromSignedRequest(string4) : null;
                    if (Utility.isNullOrEmpty(string3) || stringArrayList == null || stringArrayList.isEmpty() || Utility.isNullOrEmpty(str)) {
                        loginLogger.logLoginStatusFailure(str);
                        loginStatusCallback2.onFailure();
                        return;
                    }
                    AccessToken accessToken = new AccessToken(string3, str2, str, stringArrayList, null, null, bundleLongAsDate, null, bundleLongAsDate2);
                    AccessToken.setCurrentAccessToken(accessToken);
                    Profile access$200 = LoginManager.getProfileFromBundle(bundle);
                    if (access$200 != null) {
                        Profile.setCurrentProfile(access$200);
                    } else {
                        Profile.fetchProfileForCurrentAccessToken();
                    }
                    loginLogger.logLoginStatusSuccess(str);
                    loginStatusCallback2.onCompleted(accessToken);
                    return;
                }
                loginLogger.logLoginStatusFailure(str);
                loginStatusCallback2.onFailure();
            }
        });
        loginLogger.logLoginStatusStart(uuid);
        if (!loginStatusClient.start()) {
            loginLogger.logLoginStatusFailure(uuid);
            loginStatusCallback.onFailure();
        }
    }

    private void setExpressLoginStatus(boolean z) {
        Editor edit = this.sharedPreferences.edit();
        edit.putBoolean(EXPRESS_LOGIN_ALLOWED, z);
        edit.apply();
    }

    private void startLogin(StartActivityDelegate startActivityDelegate, Request request) throws FacebookException {
        logStartLogin(startActivityDelegate.getActivityContext(), request);
        CallbackManagerImpl.registerStaticCallback(RequestCodeOffset.Login.toRequestCode(), new Callback() {
            public boolean onActivityResult(int i, Intent intent) {
                return LoginManager.this.onActivityResult(i, intent);
            }
        });
        if (!tryFacebookActivity(startActivityDelegate, request)) {
            FacebookException facebookException = new FacebookException("Log in attempt failed: FacebookActivity could not be started. Please make sure you added FacebookActivity to the AndroidManifest.");
            logCompleteLogin(startActivityDelegate.getActivityContext(), Code.ERROR, null, facebookException, false, request);
            throw facebookException;
        }
    }

    private boolean tryFacebookActivity(StartActivityDelegate startActivityDelegate, Request request) {
        Intent facebookActivityIntent = getFacebookActivityIntent(request);
        if (!resolveIntent(facebookActivityIntent)) {
            return false;
        }
        try {
            startActivityDelegate.startActivityForResult(facebookActivityIntent, LoginClient.getLoginRequestCode());
            return true;
        } catch (ActivityNotFoundException e) {
            return false;
        }
    }

    private void validatePublishPermissions(Collection<String> collection) {
        if (collection != null) {
            for (String str : collection) {
                if (!isPublishPermission(str)) {
                    throw new FacebookException(String.format("Cannot pass a read permission (%s) to a request for publish authorization", new Object[]{str}));
                }
            }
        }
    }

    private void validateReadPermissions(Collection<String> collection) {
        if (collection != null) {
            for (String str : collection) {
                if (isPublishPermission(str)) {
                    throw new FacebookException(String.format("Cannot pass a publish or manage permission (%s) to a request for read authorization", new Object[]{str}));
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    public Request createLoginRequest(Collection<String> collection) {
        Request request = new Request(this.loginBehavior, Collections.unmodifiableSet(collection != null ? new HashSet(collection) : new HashSet()), this.defaultAudience, this.authType, FacebookSdk.getApplicationId(), UUID.randomUUID().toString());
        request.setRerequest(AccessToken.isCurrentAccessTokenActive());
        return request;
    }

    /* access modifiers changed from: protected */
    public Request createReauthorizeRequest() {
        return new Request(LoginBehavior.DIALOG_ONLY, new HashSet(), this.defaultAudience, "reauthorize", FacebookSdk.getApplicationId(), UUID.randomUUID().toString());
    }

    public String getAuthType() {
        return this.authType;
    }

    public DefaultAudience getDefaultAudience() {
        return this.defaultAudience;
    }

    /* access modifiers changed from: protected */
    public Intent getFacebookActivityIntent(Request request) {
        Intent intent = new Intent();
        intent.setClass(FacebookSdk.getApplicationContext(), FacebookActivity.class);
        intent.setAction(request.getLoginBehavior().toString());
        Bundle bundle = new Bundle();
        bundle.putParcelable(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID, request);
        intent.putExtra("com.facebook.LoginFragment:Request", bundle);
        return intent;
    }

    public LoginBehavior getLoginBehavior() {
        return this.loginBehavior;
    }

    public void logInWithPublishPermissions(Activity activity, Collection<String> collection) {
        validatePublishPermissions(collection);
        startLogin(new ActivityStartActivityDelegate(activity), createLoginRequest(collection));
    }

    public void logInWithPublishPermissions(Fragment fragment, Collection<String> collection) {
        logInWithPublishPermissions(new FragmentWrapper(fragment), collection);
    }

    public void logInWithPublishPermissions(android.support.p000v4.app.Fragment fragment, Collection<String> collection) {
        logInWithPublishPermissions(new FragmentWrapper(fragment), collection);
    }

    public void logInWithReadPermissions(Activity activity, Collection<String> collection) {
        validateReadPermissions(collection);
        startLogin(new ActivityStartActivityDelegate(activity), createLoginRequest(collection));
    }

    public void logInWithReadPermissions(Fragment fragment, Collection<String> collection) {
        logInWithReadPermissions(new FragmentWrapper(fragment), collection);
    }

    public void logInWithReadPermissions(android.support.p000v4.app.Fragment fragment, Collection<String> collection) {
        logInWithReadPermissions(new FragmentWrapper(fragment), collection);
    }

    public void logOut() {
        AccessToken.setCurrentAccessToken(null);
        Profile.setCurrentProfile(null);
        setExpressLoginStatus(false);
    }

    /* access modifiers changed from: 0000 */
    public boolean onActivityResult(int i, Intent intent) {
        return onActivityResult(i, intent, null);
    }

    /* access modifiers changed from: 0000 */
    public boolean onActivityResult(int i, Intent intent, FacebookCallback<LoginResult> facebookCallback) {
        Map<String, String> map;
        Code code;
        FacebookException facebookException;
        boolean z;
        FacebookException facebookException2;
        AccessToken accessToken = null;
        AccessToken accessToken2 = null;
        Code code2 = Code.ERROR;
        Request request = null;
        boolean z2 = false;
        if (intent != null) {
            Result result = (Result) intent.getParcelableExtra("com.facebook.LoginFragment:Result");
            if (result != null) {
                request = result.request;
                Code code3 = result.code;
                if (i == -1) {
                    if (result.code == Code.SUCCESS) {
                        accessToken2 = result.token;
                        z = false;
                        facebookException2 = null;
                    } else {
                        z = false;
                        facebookException2 = new FacebookAuthorizationException(result.errorMessage);
                    }
                } else if (i == 0) {
                    z = true;
                    facebookException2 = null;
                } else {
                    z = false;
                    facebookException2 = null;
                }
                map = result.loggingExtras;
                accessToken = accessToken2;
                code = code3;
                z2 = z;
                facebookException = facebookException2;
            }
            map = null;
            code = code2;
            facebookException = null;
        } else {
            if (i == 0) {
                z2 = true;
                code = Code.CANCEL;
                map = null;
                facebookException = null;
            }
            map = null;
            code = code2;
            facebookException = null;
        }
        FacebookException facebookException3 = (facebookException == null && accessToken == null && !z2) ? new FacebookException("Unexpected call to LoginManager.onActivityResult") : facebookException;
        logCompleteLogin(null, code, map, facebookException3, true, request);
        finishLogin(accessToken, request, facebookException3, z2, facebookCallback);
        return true;
    }

    public void reauthorizeDataAccess(Activity activity) {
        startLogin(new ActivityStartActivityDelegate(activity), createReauthorizeRequest());
    }

    public void reauthorizeDataAccess(android.support.p000v4.app.Fragment fragment) {
        reauthorizeDataAccess(new FragmentWrapper(fragment));
    }

    public void registerCallback(CallbackManager callbackManager, final FacebookCallback<LoginResult> facebookCallback) {
        if (!(callbackManager instanceof CallbackManagerImpl)) {
            throw new FacebookException("Unexpected CallbackManager, please use the provided Factory.");
        }
        ((CallbackManagerImpl) callbackManager).registerCallback(RequestCodeOffset.Login.toRequestCode(), new Callback() {
            public boolean onActivityResult(int i, Intent intent) {
                return LoginManager.this.onActivityResult(i, intent, facebookCallback);
            }
        });
    }

    public void resolveError(Activity activity, GraphResponse graphResponse) {
        startLogin(new ActivityStartActivityDelegate(activity), createLoginRequestFromResponse(graphResponse));
    }

    public void resolveError(Fragment fragment, GraphResponse graphResponse) {
        resolveError(new FragmentWrapper(fragment), graphResponse);
    }

    public void resolveError(android.support.p000v4.app.Fragment fragment, GraphResponse graphResponse) {
        resolveError(new FragmentWrapper(fragment), graphResponse);
    }

    public void retrieveLoginStatus(Context context, long j, LoginStatusCallback loginStatusCallback) {
        retrieveLoginStatusImpl(context, loginStatusCallback, j);
    }

    public void retrieveLoginStatus(Context context, LoginStatusCallback loginStatusCallback) {
        retrieveLoginStatus(context, 5000, loginStatusCallback);
    }

    public LoginManager setAuthType(String str) {
        this.authType = str;
        return this;
    }

    public LoginManager setDefaultAudience(DefaultAudience defaultAudience2) {
        this.defaultAudience = defaultAudience2;
        return this;
    }

    public LoginManager setLoginBehavior(LoginBehavior loginBehavior2) {
        this.loginBehavior = loginBehavior2;
        return this;
    }

    public void unregisterCallback(CallbackManager callbackManager) {
        if (!(callbackManager instanceof CallbackManagerImpl)) {
            throw new FacebookException("Unexpected CallbackManager, please use the provided Factory.");
        }
        ((CallbackManagerImpl) callbackManager).unregisterCallback(RequestCodeOffset.Login.toRequestCode());
    }
}
