package com.appsflyer;

import android.content.ContentResolver;
import android.content.Context;
import android.os.Build;
import android.provider.Settings.Secure;
import com.appsflyer.C0284m.C0281a;
import com.appsflyer.C0286n.C0285b;
import com.appsflyer.share.Constants;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.ads.identifier.AdvertisingIdClient.Info;
import com.google.android.gms.common.GoogleApiAvailability;
import java.util.Map;

/* renamed from: com.appsflyer.o */
final class C0287o {
    C0287o() {
    }

    /* renamed from: ˊ */
    static C0286n m333(ContentResolver contentResolver) {
        if (contentResolver == null || AppsFlyerProperties.getInstance().getString("amazon_aid") != null || !"Amazon".equals(Build.MANUFACTURER)) {
            return null;
        }
        int i = Secure.getInt(contentResolver, "limit_ad_tracking", 2);
        if (i == 0) {
            return new C0286n(C0285b.AMAZON, Secure.getString(contentResolver, Constants.URL_ADVERTISING_ID), false);
        } else if (i == 2) {
            return null;
        } else {
            String str = "";
            try {
                str = Secure.getString(contentResolver, Constants.URL_ADVERTISING_ID);
            } catch (Throwable th) {
                AFLogger.afErrorLog("Couldn't fetch Amazon Advertising ID (Ad-Tracking is limited!)", th);
            }
            return new C0286n(C0285b.AMAZON, str, true);
        }
    }

    /* renamed from: ˊ */
    static void m334(Context context, Map<String, Object> map) {
        String id;
        String bool;
        String str;
        String str2;
        String str3;
        Throwable th;
        boolean z;
        int isGooglePlayServicesAvailable;
        C0281a ˏ;
        String ॱ;
        String str4;
        String str5 = null;
        boolean z2 = true;
        AFLogger.afInfoLog("Trying to fetch GAID..");
        boolean z3;
        try {
            Class.forName("com.google.android.gms.ads.identifier.AdvertisingIdClient");
            Info advertisingIdInfo = AdvertisingIdClient.getAdvertisingIdInfo(context);
            if (advertisingIdInfo != null) {
                id = advertisingIdInfo.getId();
                try {
                    bool = Boolean.toString(!advertisingIdInfo.isLimitAdTrackingEnabled() ? z2 : false);
                    if (id != null) {
                        try {
                            if (id.length() != 0) {
                                str = bool;
                                bool = id;
                            }
                        } catch (Throwable th2) {
                            str2 = bool;
                            str3 = id;
                            th = th2;
                            z = z2;
                            AFLogger.afErrorLog(th.getMessage(), th);
                            try {
                                isGooglePlayServicesAvailable = GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(context);
                            } catch (Throwable th22) {
                                AFLogger.afErrorLog(th22.getMessage(), th22);
                                isGooglePlayServicesAvailable = -1;
                            }
                            bool = th.getClass().getSimpleName();
                            AFLogger.afInfoLog("WARNING: Google Play Services is missing.");
                            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.ENABLE_GPS_FALLBACK, z2)) {
                                try {
                                    ˏ = C0284m.m330(context);
                                    ॱ = ˏ.m326();
                                    if (ˏ.m325()) {
                                        z2 = false;
                                    }
                                    str = Boolean.toString(z2);
                                    if (ॱ != null) {
                                    }
                                    str4 = "emptyOrNull (bypass)";
                                    bool = str;
                                    z3 = z;
                                    id = ॱ;
                                } catch (Throwable th3) {
                                    Throwable th4 = th3;
                                    AFLogger.afErrorLog(th4.getMessage(), th4);
                                    str4 = new StringBuilder().append(bool).append(Constants.URL_PATH_DELIMITER).append(th4.getClass().getSimpleName()).toString();
                                    str = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                                    bool = AppsFlyerProperties.getInstance().getString("advertiserIdEnabled");
                                    boolean z4;
                                    if (th4.getLocalizedMessage() != null) {
                                        AFLogger.afInfoLog(th4.getLocalizedMessage());
                                        z4 = z;
                                        id = str;
                                        z3 = z4;
                                    } else {
                                        AFLogger.afInfoLog(th4.toString());
                                        z4 = z;
                                        id = str;
                                        z3 = z4;
                                    }
                                }
                            } else {
                                str4 = bool;
                                z3 = z;
                                bool = str2;
                                id = str3;
                            }
                            if (context.getClass().getName().equals("android.app.ReceiverRestrictedContext")) {
                                id = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                                bool = AppsFlyerProperties.getInstance().getString("advertiserIdEnabled");
                                str4 = "context = android.app.ReceiverRestrictedContext";
                            }
                            if (str4 != null) {
                                map.put("gaidError", new StringBuilder().append(isGooglePlayServicesAvailable).append(": ").append(str4).toString());
                            }
                            if (id != null) {
                            }
                        }
                    }
                    z3 = z2;
                    str4 = "emptyOrNull";
                    isGooglePlayServicesAvailable = -1;
                } catch (Throwable th5) {
                    th = th5;
                    str2 = null;
                    str3 = id;
                    z = false;
                    AFLogger.afErrorLog(th.getMessage(), th);
                    isGooglePlayServicesAvailable = GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(context);
                    bool = th.getClass().getSimpleName();
                    AFLogger.afInfoLog("WARNING: Google Play Services is missing.");
                    if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.ENABLE_GPS_FALLBACK, z2)) {
                        str4 = bool;
                        z3 = z;
                        bool = str2;
                        id = str3;
                    } else {
                        ˏ = C0284m.m330(context);
                        ॱ = ˏ.m326();
                        if (ˏ.m325()) {
                            z2 = false;
                        }
                        str = Boolean.toString(z2);
                        if (ॱ != null) {
                        }
                        str4 = "emptyOrNull (bypass)";
                        bool = str;
                        z3 = z;
                        id = ॱ;
                    }
                    if (context.getClass().getName().equals("android.app.ReceiverRestrictedContext")) {
                        id = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                        bool = AppsFlyerProperties.getInstance().getString("advertiserIdEnabled");
                        str4 = "context = android.app.ReceiverRestrictedContext";
                    }
                    if (str4 != null) {
                        map.put("gaidError", new StringBuilder().append(isGooglePlayServicesAvailable).append(": ").append(str4).toString());
                    }
                    if (id != null) {
                    }
                }
                if (context.getClass().getName().equals("android.app.ReceiverRestrictedContext")) {
                    id = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                    bool = AppsFlyerProperties.getInstance().getString("advertiserIdEnabled");
                    str4 = "context = android.app.ReceiverRestrictedContext";
                }
                if (str4 != null) {
                    map.put("gaidError", new StringBuilder().append(isGooglePlayServicesAvailable).append(": ").append(str4).toString());
                }
                if (id != null && bool != null) {
                    map.put(ServerParameters.ADVERTISING_ID_PARAM, id);
                    map.put("advertiserIdEnabled", bool);
                    AppsFlyerProperties.getInstance().set(ServerParameters.ADVERTISING_ID_PARAM, id);
                    AppsFlyerProperties.getInstance().set("advertiserIdEnabled", bool);
                    map.put("isGaidWithGps", String.valueOf(z3));
                    return;
                }
            }
            bool = null;
            str = null;
            str5 = "gpsAdInfo-null";
            z2 = false;
            id = bool;
            bool = str;
            z3 = z2;
            str4 = str5;
            isGooglePlayServicesAvailable = -1;
        } catch (Throwable th6) {
            th = th6;
            z = false;
            str2 = null;
            str3 = null;
            AFLogger.afErrorLog(th.getMessage(), th);
            isGooglePlayServicesAvailable = GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(context);
            bool = th.getClass().getSimpleName();
            AFLogger.afInfoLog("WARNING: Google Play Services is missing.");
            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.ENABLE_GPS_FALLBACK, z2)) {
                ˏ = C0284m.m330(context);
                ॱ = ˏ.m326();
                if (ˏ.m325()) {
                    z2 = false;
                }
                str = Boolean.toString(z2);
                if (ॱ != null || ॱ.length() == 0) {
                    str4 = "emptyOrNull (bypass)";
                } else {
                    str4 = bool;
                }
                bool = str;
                z3 = z;
                id = ॱ;
            } else {
                str4 = bool;
                z3 = z;
                bool = str2;
                id = str3;
            }
            if (context.getClass().getName().equals("android.app.ReceiverRestrictedContext")) {
                id = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                bool = AppsFlyerProperties.getInstance().getString("advertiserIdEnabled");
                str4 = "context = android.app.ReceiverRestrictedContext";
            }
            if (str4 != null) {
                map.put("gaidError", new StringBuilder().append(isGooglePlayServicesAvailable).append(": ").append(str4).toString());
            }
            if (id != null) {
            }
        }
        if (context.getClass().getName().equals("android.app.ReceiverRestrictedContext")) {
            id = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
            bool = AppsFlyerProperties.getInstance().getString("advertiserIdEnabled");
            str4 = "context = android.app.ReceiverRestrictedContext";
        }
        if (str4 != null) {
            map.put("gaidError", new StringBuilder().append(isGooglePlayServicesAvailable).append(": ").append(str4).toString());
        }
        if (id != null) {
        }
    }
}
