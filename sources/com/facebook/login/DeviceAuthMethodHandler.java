package com.facebook.login;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.facebook.AccessToken;
import com.facebook.AccessTokenSource;
import com.facebook.login.LoginClient.Request;
import com.facebook.login.LoginClient.Result;
import java.util.Collection;
import java.util.Date;
import java.util.concurrent.ScheduledThreadPoolExecutor;

class DeviceAuthMethodHandler extends LoginMethodHandler {
    public static final Creator<DeviceAuthMethodHandler> CREATOR = new C04341();
    private static ScheduledThreadPoolExecutor backgroundExecutor;

    /* renamed from: com.facebook.login.DeviceAuthMethodHandler$1 */
    static final class C04341 implements Creator {
        C04341() {
        }

        public DeviceAuthMethodHandler createFromParcel(Parcel parcel) {
            return new DeviceAuthMethodHandler(parcel);
        }

        public DeviceAuthMethodHandler[] newArray(int i) {
            return new DeviceAuthMethodHandler[i];
        }
    }

    protected DeviceAuthMethodHandler(Parcel parcel) {
        super(parcel);
    }

    DeviceAuthMethodHandler(LoginClient loginClient) {
        super(loginClient);
    }

    public static ScheduledThreadPoolExecutor getBackgroundExecutor() {
        Class cls;
        synchronized (DeviceAuthMethodHandler.class) {
            try {
                if (backgroundExecutor == null) {
                    cls = true;
                    backgroundExecutor = new ScheduledThreadPoolExecutor(1);
                }
                ScheduledThreadPoolExecutor scheduledThreadPoolExecutor = backgroundExecutor;
                return scheduledThreadPoolExecutor;
            } finally {
                cls = DeviceAuthMethodHandler.class;
            }
        }
    }

    private void showDialog(Request request) {
        DeviceAuthDialog deviceAuthDialog = new DeviceAuthDialog();
        deviceAuthDialog.show(this.loginClient.getActivity().getSupportFragmentManager(), "login_with_facebook");
        deviceAuthDialog.startLogin(request);
    }

    public int describeContents() {
        return 0;
    }

    String getNameForLogging() {
        return "device_auth";
    }

    public void onCancel() {
        this.loginClient.completeAndValidate(Result.createCancelResult(this.loginClient.getPendingRequest(), "User canceled log in."));
    }

    public void onError(Exception exception) {
        this.loginClient.completeAndValidate(Result.createErrorResult(this.loginClient.getPendingRequest(), null, exception.getMessage()));
    }

    public void onSuccess(String str, String str2, String str3, Collection<String> collection, Collection<String> collection2, AccessTokenSource accessTokenSource, Date date, Date date2) {
        this.loginClient.completeAndValidate(Result.createTokenResult(this.loginClient.getPendingRequest(), new AccessToken(str, str2, str3, collection, collection2, accessTokenSource, date, date2)));
    }

    boolean tryAuthorize(Request request) {
        showDialog(request);
        return true;
    }

    public void writeToParcel(Parcel parcel, int i) {
        super.writeToParcel(parcel, i);
    }
}
