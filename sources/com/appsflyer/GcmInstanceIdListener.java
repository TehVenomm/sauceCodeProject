package com.appsflyer;

import com.google.android.gms.iid.InstanceID;
import com.google.android.gms.iid.InstanceIDListenerService;
import com.google.android.gms.stats.CodePackage;

public class GcmInstanceIdListener extends InstanceIDListenerService {
    /* JADX WARNING: type inference failed for: r7v0, types: [android.content.Context, com.appsflyer.GcmInstanceIdListener, com.google.android.gms.iid.InstanceIDListenerService] */
    public void onTokenRefresh() {
        String str;
        GcmInstanceIdListener.super.onTokenRefresh();
        String string = AppsFlyerProperties.getInstance().getString("gcmProjectNumber");
        long currentTimeMillis = System.currentTimeMillis();
        try {
            str = InstanceID.getInstance(getApplicationContext()).getToken(string, CodePackage.GCM, null);
        } catch (Throwable th) {
            AFLogger.afErrorLog("Error registering for uninstall tracking", th);
            str = null;
        }
        if (str != null) {
            AFLogger.afInfoLog("GCM Refreshed Token = ".concat(String.valueOf(str)));
            C0432d r1 = C0432d.m277(AppsFlyerProperties.getInstance().getString("afUninstallToken"));
            C0432d dVar = new C0432d(currentTimeMillis, str);
            if (r1.mo6558(dVar)) {
                C0467u.m369(getApplicationContext(), dVar);
            }
        }
    }
}
