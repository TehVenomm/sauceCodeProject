package com.facebook.login;

import android.net.Uri;
import android.support.annotation.Nullable;
import com.facebook.login.LoginClient.Request;
import java.util.Collection;

public class DeviceLoginManager extends LoginManager {
    private static volatile DeviceLoginManager instance;
    @Nullable
    private String deviceAuthTargetUserId;
    private Uri deviceRedirectUri;

    public static DeviceLoginManager getInstance() {
        if (instance == null) {
            synchronized (DeviceLoginManager.class) {
                try {
                    if (instance == null) {
                        instance = new DeviceLoginManager();
                    }
                } finally {
                    while (true) {
                        Class<DeviceLoginManager> cls = DeviceLoginManager.class;
                    }
                }
            }
        }
        return instance;
    }

    /* access modifiers changed from: protected */
    public Request createLoginRequest(Collection<String> collection) {
        Request createLoginRequest = super.createLoginRequest(collection);
        Uri deviceRedirectUri2 = getDeviceRedirectUri();
        if (deviceRedirectUri2 != null) {
            createLoginRequest.setDeviceRedirectUriString(deviceRedirectUri2.toString());
        }
        String deviceAuthTargetUserId2 = getDeviceAuthTargetUserId();
        if (deviceAuthTargetUserId2 != null) {
            createLoginRequest.setDeviceAuthTargetUserId(deviceAuthTargetUserId2);
        }
        return createLoginRequest;
    }

    @Nullable
    public String getDeviceAuthTargetUserId() {
        return this.deviceAuthTargetUserId;
    }

    public Uri getDeviceRedirectUri() {
        return this.deviceRedirectUri;
    }

    public void setDeviceAuthTargetUserId(@Nullable String str) {
        this.deviceAuthTargetUserId = str;
    }

    public void setDeviceRedirectUri(Uri uri) {
        this.deviceRedirectUri = uri;
    }
}
