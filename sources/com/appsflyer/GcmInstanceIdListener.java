package com.appsflyer;

import com.google.android.gms.iid.InstanceID;
import com.google.android.gms.iid.InstanceIDListenerService;

public class GcmInstanceIdListener extends InstanceIDListenerService {
    public void onTokenRefresh() {
        super.onTokenRefresh();
        String string = AppsFlyerProperties.getInstance().getString("gcmProjectNumber");
        long currentTimeMillis = System.currentTimeMillis();
        try {
            string = InstanceID.getInstance(getApplicationContext()).getToken(string, "GCM", null);
        } catch (Throwable th) {
            AFLogger.afErrorLog("Error registering for uninstall tracking", th);
            string = null;
        }
        if (string != null) {
            AFLogger.afInfoLog("GCM Refreshed Token = ".concat(String.valueOf(string)));
            C0265d ˊ = C0265d.m286(AppsFlyerProperties.getInstance().getString("afUninstallToken"));
            C0265d c0265d = new C0265d(currentTimeMillis, string);
            if (ˊ.m290(c0265d)) {
                C0299u.m375(getApplicationContext(), c0265d);
            }
        }
    }
}
