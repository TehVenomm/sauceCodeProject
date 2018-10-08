package com.appsflyer;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class SingleInstallBroadcastReceiver extends BroadcastReceiver {
    public void onReceive(Context context, Intent intent) {
        if (intent != null) {
            String stringExtra = intent.getStringExtra("referrer");
            if (stringExtra != null) {
                if (stringExtra.contains("AppsFlyer_Test") && intent.getStringExtra("TestIntegrationMode") != null) {
                    AppsFlyerLib.getInstance().m257(context, intent);
                    return;
                } else if (context.getSharedPreferences("appsflyer-data", 0).getString("referrer", null) != null) {
                    AppsFlyerLib.getInstance();
                    AppsFlyerLib.m243(context, stringExtra);
                    return;
                }
            }
            stringExtra = AppsFlyerProperties.getInstance().getString("referrer_timestamp");
            long currentTimeMillis = System.currentTimeMillis();
            if (stringExtra == null || currentTimeMillis - Long.valueOf(stringExtra).longValue() >= 2000) {
                AFLogger.afInfoLog("SingleInstallBroadcastReceiver called");
                AppsFlyerLib.getInstance().m257(context, intent);
                AppsFlyerProperties.getInstance().set("referrer_timestamp", String.valueOf(System.currentTimeMillis()));
            }
        }
    }
}
