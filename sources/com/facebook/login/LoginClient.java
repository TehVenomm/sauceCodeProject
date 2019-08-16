package com.facebook.login;

import android.content.Intent;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentActivity;
import android.text.TextUtils;
import com.facebook.AccessToken;
import com.facebook.FacebookException;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.common.C0618R;
import com.facebook.internal.CallbackManagerImpl.RequestCodeOffset;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.Utility;
import com.facebook.internal.Validate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;

class LoginClient implements Parcelable {
    public static final Creator<LoginClient> CREATOR = new Creator<LoginClient>() {
        public LoginClient createFromParcel(Parcel parcel) {
            return new LoginClient(parcel);
        }

        public LoginClient[] newArray(int i) {
            return new LoginClient[i];
        }
    };
    BackgroundProcessingListener backgroundProcessingListener;
    boolean checkedInternetPermission;
    int currentHandler = -1;
    Map<String, String> extraData;
    Fragment fragment;
    LoginMethodHandler[] handlersToTry;
    Map<String, String> loggingExtras;
    private LoginLogger loginLogger;
    OnCompletedListener onCompletedListener;
    Request pendingRequest;

    interface BackgroundProcessingListener {
        void onBackgroundProcessingStarted();

        void onBackgroundProcessingStopped();
    }

    public interface OnCompletedListener {
        void onCompleted(Result result);
    }

    public static class Request implements Parcelable {
        public static final Creator<Request> CREATOR = new Creator<Request>() {
            public Request createFromParcel(Parcel parcel) {
                return new Request(parcel);
            }

            public Request[] newArray(int i) {
                return new Request[i];
            }
        };
        private final String applicationId;
        private final String authId;
        private String authType;
        private final DefaultAudience defaultAudience;
        private String deviceAuthTargetUserId;
        private String deviceRedirectUriString;
        private boolean isRerequest;
        private final LoginBehavior loginBehavior;
        private Set<String> permissions;

        private Request(Parcel parcel) {
            DefaultAudience defaultAudience2 = null;
            this.isRerequest = false;
            String readString = parcel.readString();
            this.loginBehavior = readString != null ? LoginBehavior.valueOf(readString) : null;
            ArrayList arrayList = new ArrayList();
            parcel.readStringList(arrayList);
            this.permissions = new HashSet(arrayList);
            String readString2 = parcel.readString();
            if (readString2 != null) {
                defaultAudience2 = DefaultAudience.valueOf(readString2);
            }
            this.defaultAudience = defaultAudience2;
            this.applicationId = parcel.readString();
            this.authId = parcel.readString();
            this.isRerequest = parcel.readByte() != 0;
            this.deviceRedirectUriString = parcel.readString();
            this.authType = parcel.readString();
            this.deviceAuthTargetUserId = parcel.readString();
        }

        Request(LoginBehavior loginBehavior2, Set<String> set, DefaultAudience defaultAudience2, String str, String str2, String str3) {
            this.isRerequest = false;
            this.loginBehavior = loginBehavior2;
            if (set == null) {
                set = new HashSet<>();
            }
            this.permissions = set;
            this.defaultAudience = defaultAudience2;
            this.authType = str;
            this.applicationId = str2;
            this.authId = str3;
        }

        public int describeContents() {
            return 0;
        }

        /* access modifiers changed from: 0000 */
        public String getApplicationId() {
            return this.applicationId;
        }

        /* access modifiers changed from: 0000 */
        public String getAuthId() {
            return this.authId;
        }

        /* access modifiers changed from: 0000 */
        public String getAuthType() {
            return this.authType;
        }

        /* access modifiers changed from: 0000 */
        public DefaultAudience getDefaultAudience() {
            return this.defaultAudience;
        }

        /* access modifiers changed from: 0000 */
        public String getDeviceAuthTargetUserId() {
            return this.deviceAuthTargetUserId;
        }

        /* access modifiers changed from: 0000 */
        public String getDeviceRedirectUriString() {
            return this.deviceRedirectUriString;
        }

        /* access modifiers changed from: 0000 */
        public LoginBehavior getLoginBehavior() {
            return this.loginBehavior;
        }

        /* access modifiers changed from: 0000 */
        public Set<String> getPermissions() {
            return this.permissions;
        }

        /* access modifiers changed from: 0000 */
        public boolean hasPublishPermission() {
            for (String isPublishPermission : this.permissions) {
                if (LoginManager.isPublishPermission(isPublishPermission)) {
                    return true;
                }
            }
            return false;
        }

        /* access modifiers changed from: 0000 */
        public boolean isRerequest() {
            return this.isRerequest;
        }

        /* access modifiers changed from: 0000 */
        public void setAuthType(String str) {
            this.authType = str;
        }

        /* access modifiers changed from: 0000 */
        public void setDeviceAuthTargetUserId(String str) {
            this.deviceAuthTargetUserId = str;
        }

        /* access modifiers changed from: 0000 */
        public void setDeviceRedirectUriString(String str) {
            this.deviceRedirectUriString = str;
        }

        /* access modifiers changed from: 0000 */
        public void setPermissions(Set<String> set) {
            Validate.notNull(set, NativeProtocol.RESULT_ARGS_PERMISSIONS);
            this.permissions = set;
        }

        /* access modifiers changed from: 0000 */
        public void setRerequest(boolean z) {
            this.isRerequest = z;
        }

        public void writeToParcel(Parcel parcel, int i) {
            String str = null;
            parcel.writeString(this.loginBehavior != null ? this.loginBehavior.name() : null);
            parcel.writeStringList(new ArrayList(this.permissions));
            if (this.defaultAudience != null) {
                str = this.defaultAudience.name();
            }
            parcel.writeString(str);
            parcel.writeString(this.applicationId);
            parcel.writeString(this.authId);
            parcel.writeByte((byte) (this.isRerequest ? 1 : 0));
            parcel.writeString(this.deviceRedirectUriString);
            parcel.writeString(this.authType);
            parcel.writeString(this.deviceAuthTargetUserId);
        }
    }

    public static class Result implements Parcelable {
        public static final Creator<Result> CREATOR = new Creator<Result>() {
            public Result createFromParcel(Parcel parcel) {
                return new Result(parcel);
            }

            public Result[] newArray(int i) {
                return new Result[i];
            }
        };
        final Code code;
        final String errorCode;
        final String errorMessage;
        public Map<String, String> extraData;
        public Map<String, String> loggingExtras;
        final Request request;
        final AccessToken token;

        enum Code {
            SUCCESS("success"),
            CANCEL("cancel"),
            ERROR("error");
            
            private final String loggingValue;

            private Code(String str) {
                this.loggingValue = str;
            }

            /* access modifiers changed from: 0000 */
            public String getLoggingValue() {
                return this.loggingValue;
            }
        }

        private Result(Parcel parcel) {
            this.code = Code.valueOf(parcel.readString());
            this.token = (AccessToken) parcel.readParcelable(AccessToken.class.getClassLoader());
            this.errorMessage = parcel.readString();
            this.errorCode = parcel.readString();
            this.request = (Request) parcel.readParcelable(Request.class.getClassLoader());
            this.loggingExtras = Utility.readStringMapFromParcel(parcel);
            this.extraData = Utility.readStringMapFromParcel(parcel);
        }

        Result(Request request2, Code code2, AccessToken accessToken, String str, String str2) {
            Validate.notNull(code2, "code");
            this.request = request2;
            this.token = accessToken;
            this.errorMessage = str;
            this.code = code2;
            this.errorCode = str2;
        }

        static Result createCancelResult(Request request2, String str) {
            return new Result(request2, Code.CANCEL, null, str, null);
        }

        static Result createErrorResult(Request request2, String str, String str2) {
            return createErrorResult(request2, str, str2, null);
        }

        static Result createErrorResult(Request request2, String str, String str2, String str3) {
            return new Result(request2, Code.ERROR, null, TextUtils.join(": ", Utility.asListNoNulls(str, str2)), str3);
        }

        static Result createTokenResult(Request request2, AccessToken accessToken) {
            return new Result(request2, Code.SUCCESS, accessToken, null, null);
        }

        public int describeContents() {
            return 0;
        }

        public void writeToParcel(Parcel parcel, int i) {
            parcel.writeString(this.code.name());
            parcel.writeParcelable(this.token, i);
            parcel.writeString(this.errorMessage);
            parcel.writeString(this.errorCode);
            parcel.writeParcelable(this.request, i);
            Utility.writeStringMapToParcel(parcel, this.loggingExtras);
            Utility.writeStringMapToParcel(parcel, this.extraData);
        }
    }

    public LoginClient(Parcel parcel) {
        Parcelable[] readParcelableArray = parcel.readParcelableArray(LoginMethodHandler.class.getClassLoader());
        this.handlersToTry = new LoginMethodHandler[readParcelableArray.length];
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 < readParcelableArray.length) {
                this.handlersToTry[i2] = (LoginMethodHandler) readParcelableArray[i2];
                this.handlersToTry[i2].setLoginClient(this);
                i = i2 + 1;
            } else {
                this.currentHandler = parcel.readInt();
                this.pendingRequest = (Request) parcel.readParcelable(Request.class.getClassLoader());
                this.loggingExtras = Utility.readStringMapFromParcel(parcel);
                this.extraData = Utility.readStringMapFromParcel(parcel);
                return;
            }
        }
    }

    public LoginClient(Fragment fragment2) {
        this.fragment = fragment2;
    }

    private void addLoggingExtra(String str, String str2, boolean z) {
        if (this.loggingExtras == null) {
            this.loggingExtras = new HashMap();
        }
        if (this.loggingExtras.containsKey(str) && z) {
            str2 = ((String) this.loggingExtras.get(str)) + "," + str2;
        }
        this.loggingExtras.put(str, str2);
    }

    private void completeWithFailure() {
        complete(Result.createErrorResult(this.pendingRequest, "Login attempt failed.", null));
    }

    static String getE2E() {
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put("init", System.currentTimeMillis());
        } catch (JSONException e) {
        }
        return jSONObject.toString();
    }

    private LoginLogger getLogger() {
        if (this.loginLogger == null || !this.loginLogger.getApplicationId().equals(this.pendingRequest.getApplicationId())) {
            this.loginLogger = new LoginLogger(getActivity(), this.pendingRequest.getApplicationId());
        }
        return this.loginLogger;
    }

    public static int getLoginRequestCode() {
        return RequestCodeOffset.Login.toRequestCode();
    }

    private void logAuthorizationMethodComplete(String str, Result result, Map<String, String> map) {
        logAuthorizationMethodComplete(str, result.code.getLoggingValue(), result.errorMessage, result.errorCode, map);
    }

    private void logAuthorizationMethodComplete(String str, String str2, String str3, String str4, Map<String, String> map) {
        if (this.pendingRequest == null) {
            getLogger().logUnexpectedError("fb_mobile_login_method_complete", "Unexpected call to logCompleteLogin with null pendingAuthorizationRequest.", str);
        } else {
            getLogger().logAuthorizationMethodComplete(this.pendingRequest.getAuthId(), str, str2, str3, str4, map);
        }
    }

    private void notifyOnCompleteListener(Result result) {
        if (this.onCompletedListener != null) {
            this.onCompletedListener.onCompleted(result);
        }
    }

    /* access modifiers changed from: 0000 */
    public void addExtraData(String str, String str2, boolean z) {
        if (this.extraData == null) {
            this.extraData = new HashMap();
        }
        if (this.extraData.containsKey(str) && z) {
            str2 = ((String) this.extraData.get(str)) + "," + str2;
        }
        this.extraData.put(str, str2);
    }

    /* access modifiers changed from: 0000 */
    public void authorize(Request request) {
        if (request != null) {
            if (this.pendingRequest != null) {
                throw new FacebookException("Attempted to authorize while a request is pending.");
            } else if (!AccessToken.isCurrentAccessTokenActive() || checkInternetPermission()) {
                this.pendingRequest = request;
                this.handlersToTry = getHandlersToTry(request);
                tryNextHandler();
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public void cancelCurrentHandler() {
        if (this.currentHandler >= 0) {
            getCurrentHandler().cancel();
        }
    }

    /* access modifiers changed from: 0000 */
    public boolean checkInternetPermission() {
        if (this.checkedInternetPermission) {
            return true;
        }
        if (checkPermission("android.permission.INTERNET") != 0) {
            FragmentActivity activity = getActivity();
            complete(Result.createErrorResult(this.pendingRequest, activity.getString(C0618R.string.com_facebook_internet_permission_error_title), activity.getString(C0618R.string.com_facebook_internet_permission_error_message)));
            return false;
        }
        this.checkedInternetPermission = true;
        return true;
    }

    /* access modifiers changed from: 0000 */
    public int checkPermission(String str) {
        return getActivity().checkCallingOrSelfPermission(str);
    }

    /* access modifiers changed from: 0000 */
    public void complete(Result result) {
        LoginMethodHandler currentHandler2 = getCurrentHandler();
        if (currentHandler2 != null) {
            logAuthorizationMethodComplete(currentHandler2.getNameForLogging(), result, currentHandler2.methodLoggingExtras);
        }
        if (this.loggingExtras != null) {
            result.loggingExtras = this.loggingExtras;
        }
        if (this.extraData != null) {
            result.extraData = this.extraData;
        }
        this.handlersToTry = null;
        this.currentHandler = -1;
        this.pendingRequest = null;
        this.loggingExtras = null;
        notifyOnCompleteListener(result);
    }

    /* access modifiers changed from: 0000 */
    public void completeAndValidate(Result result) {
        if (result.token == null || !AccessToken.isCurrentAccessTokenActive()) {
            complete(result);
        } else {
            validateSameFbidAndFinish(result);
        }
    }

    public int describeContents() {
        return 0;
    }

    /* access modifiers changed from: 0000 */
    public FragmentActivity getActivity() {
        return this.fragment.getActivity();
    }

    /* access modifiers changed from: 0000 */
    public BackgroundProcessingListener getBackgroundProcessingListener() {
        return this.backgroundProcessingListener;
    }

    /* access modifiers changed from: 0000 */
    public LoginMethodHandler getCurrentHandler() {
        if (this.currentHandler >= 0) {
            return this.handlersToTry[this.currentHandler];
        }
        return null;
    }

    public Fragment getFragment() {
        return this.fragment;
    }

    /* access modifiers changed from: protected */
    public LoginMethodHandler[] getHandlersToTry(Request request) {
        ArrayList arrayList = new ArrayList();
        LoginBehavior loginBehavior = request.getLoginBehavior();
        if (loginBehavior.allowsGetTokenAuth()) {
            arrayList.add(new GetTokenLoginMethodHandler(this));
        }
        if (loginBehavior.allowsKatanaAuth()) {
            arrayList.add(new KatanaProxyLoginMethodHandler(this));
        }
        if (loginBehavior.allowsFacebookLiteAuth()) {
            arrayList.add(new FacebookLiteLoginMethodHandler(this));
        }
        if (loginBehavior.allowsCustomTabAuth()) {
            arrayList.add(new CustomTabLoginMethodHandler(this));
        }
        if (loginBehavior.allowsWebViewAuth()) {
            arrayList.add(new WebViewLoginMethodHandler(this));
        }
        if (loginBehavior.allowsDeviceAuth()) {
            arrayList.add(new DeviceAuthMethodHandler(this));
        }
        LoginMethodHandler[] loginMethodHandlerArr = new LoginMethodHandler[arrayList.size()];
        arrayList.toArray(loginMethodHandlerArr);
        return loginMethodHandlerArr;
    }

    /* access modifiers changed from: 0000 */
    public boolean getInProgress() {
        return this.pendingRequest != null && this.currentHandler >= 0;
    }

    /* access modifiers changed from: 0000 */
    public OnCompletedListener getOnCompletedListener() {
        return this.onCompletedListener;
    }

    public Request getPendingRequest() {
        return this.pendingRequest;
    }

    /* access modifiers changed from: 0000 */
    public void notifyBackgroundProcessingStart() {
        if (this.backgroundProcessingListener != null) {
            this.backgroundProcessingListener.onBackgroundProcessingStarted();
        }
    }

    /* access modifiers changed from: 0000 */
    public void notifyBackgroundProcessingStop() {
        if (this.backgroundProcessingListener != null) {
            this.backgroundProcessingListener.onBackgroundProcessingStopped();
        }
    }

    public boolean onActivityResult(int i, int i2, Intent intent) {
        if (this.pendingRequest != null) {
            return getCurrentHandler().onActivityResult(i, i2, intent);
        }
        return false;
    }

    /* access modifiers changed from: 0000 */
    public void setBackgroundProcessingListener(BackgroundProcessingListener backgroundProcessingListener2) {
        this.backgroundProcessingListener = backgroundProcessingListener2;
    }

    /* access modifiers changed from: 0000 */
    public void setFragment(Fragment fragment2) {
        if (this.fragment != null) {
            throw new FacebookException("Can't set fragment once it is already set.");
        }
        this.fragment = fragment2;
    }

    /* access modifiers changed from: 0000 */
    public void setOnCompletedListener(OnCompletedListener onCompletedListener2) {
        this.onCompletedListener = onCompletedListener2;
    }

    /* access modifiers changed from: 0000 */
    public void startOrContinueAuth(Request request) {
        if (!getInProgress()) {
            authorize(request);
        }
    }

    /* access modifiers changed from: 0000 */
    public boolean tryCurrentHandler() {
        boolean z = false;
        LoginMethodHandler currentHandler2 = getCurrentHandler();
        if (!currentHandler2.needsInternetPermission() || checkInternetPermission()) {
            z = currentHandler2.tryAuthorize(this.pendingRequest);
            if (z) {
                getLogger().logAuthorizationMethodStart(this.pendingRequest.getAuthId(), currentHandler2.getNameForLogging());
            } else {
                getLogger().logAuthorizationMethodNotTried(this.pendingRequest.getAuthId(), currentHandler2.getNameForLogging());
                addLoggingExtra("not_tried", currentHandler2.getNameForLogging(), true);
            }
        } else {
            addLoggingExtra("no_internet_permission", AppEventsConstants.EVENT_PARAM_VALUE_YES, false);
        }
        return z;
    }

    /* access modifiers changed from: 0000 */
    public void tryNextHandler() {
        if (this.currentHandler >= 0) {
            logAuthorizationMethodComplete(getCurrentHandler().getNameForLogging(), "skipped", null, null, getCurrentHandler().methodLoggingExtras);
        }
        while (this.handlersToTry != null && this.currentHandler < this.handlersToTry.length - 1) {
            this.currentHandler++;
            if (tryCurrentHandler()) {
                return;
            }
        }
        if (this.pendingRequest != null) {
            completeWithFailure();
        }
    }

    /* access modifiers changed from: 0000 */
    public void validateSameFbidAndFinish(Result result) {
        Result createErrorResult;
        if (result.token == null) {
            throw new FacebookException("Can't validate without a token");
        }
        AccessToken currentAccessToken = AccessToken.getCurrentAccessToken();
        AccessToken accessToken = result.token;
        if (!(currentAccessToken == null || accessToken == null)) {
            try {
                if (currentAccessToken.getUserId().equals(accessToken.getUserId())) {
                    createErrorResult = Result.createTokenResult(this.pendingRequest, result.token);
                    complete(createErrorResult);
                }
            } catch (Exception e) {
                complete(Result.createErrorResult(this.pendingRequest, "Caught exception", e.getMessage()));
                return;
            }
        }
        createErrorResult = Result.createErrorResult(this.pendingRequest, "User logged in as different Facebook user.", null);
        complete(createErrorResult);
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeParcelableArray(this.handlersToTry, i);
        parcel.writeInt(this.currentHandler);
        parcel.writeParcelable(this.pendingRequest, i);
        Utility.writeStringMapToParcel(parcel, this.loggingExtras);
        Utility.writeStringMapToParcel(parcel, this.extraData);
    }
}
