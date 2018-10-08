package com.appsflyer;

import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.FirebaseInstanceIdService;

public class FirebaseInstanceIdListener extends FirebaseInstanceIdService {
    public void onTokenRefresh() {
        String token;
        super.onTokenRefresh();
        long currentTimeMillis = System.currentTimeMillis();
        try {
            token = FirebaseInstanceId.getInstance().getToken();
        } catch (Throwable th) {
            AFLogger.afErrorLog("Error registering for uninstall tracking", th);
            token = null;
        }
        if (token != null) {
            AFLogger.afInfoLog("Firebase Refreshed Token = ".concat(String.valueOf(token)));
            C0265d ˊ = C0265d.m286(AppsFlyerProperties.getInstance().getString("afUninstallToken"));
            C0265d c0265d = new C0265d(currentTimeMillis, token);
            if (ˊ.m290(c0265d)) {
                C0299u.m375(getApplicationContext(), c0265d);
            }
        }
    }
}
