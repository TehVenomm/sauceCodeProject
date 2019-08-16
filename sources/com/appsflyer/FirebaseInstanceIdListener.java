package com.appsflyer;

import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.FirebaseInstanceIdService;

public class FirebaseInstanceIdListener extends FirebaseInstanceIdService {
    /* JADX WARNING: type inference failed for: r5v0, types: [android.content.Context, com.google.firebase.iid.FirebaseInstanceIdService, com.appsflyer.FirebaseInstanceIdListener] */
    public void onTokenRefresh() {
        String str;
        FirebaseInstanceIdListener.super.onTokenRefresh();
        long currentTimeMillis = System.currentTimeMillis();
        try {
            str = FirebaseInstanceId.getInstance().getToken();
        } catch (Throwable th) {
            AFLogger.afErrorLog("Error registering for uninstall tracking", th);
            str = null;
        }
        if (str != null) {
            AFLogger.afInfoLog("Firebase Refreshed Token = ".concat(String.valueOf(str)));
            C0432d r1 = C0432d.m277(AppsFlyerProperties.getInstance().getString("afUninstallToken"));
            C0432d dVar = new C0432d(currentTimeMillis, str);
            if (r1.mo6558(dVar)) {
                C0467u.m369(getApplicationContext(), dVar);
            }
        }
    }
}
