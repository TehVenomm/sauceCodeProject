package com.appsflyer;

import android.content.Context;
import android.location.Location;
import android.location.LocationManager;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

/* renamed from: com.appsflyer.j */
final class C0444j {

    /* renamed from: com.appsflyer.j$d */
    static final class C0445d {

        /* renamed from: ॱ */
        static final C0444j f292 = new C0444j();
    }

    C0444j() {
    }

    @Nullable
    /* renamed from: ॱ */
    static Location m310(@NonNull Context context) {
        Location location;
        Location location2;
        try {
            LocationManager locationManager = (LocationManager) context.getSystemService("location");
            String str = "network";
            if (m309(context, new String[]{"android.permission.ACCESS_FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION"})) {
                location = locationManager.getLastKnownLocation(str);
            } else {
                location = null;
            }
            String str2 = "gps";
            if (m309(context, new String[]{"android.permission.ACCESS_FINE_LOCATION"})) {
                location2 = locationManager.getLastKnownLocation(str2);
            } else {
                location2 = null;
            }
            if (location2 == null && location == null) {
                location2 = null;
            } else if (location2 == null && location != null) {
                location2 = location;
            } else if ((location != null || location2 == null) && 60000 < location.getTime() - location2.getTime()) {
                location2 = location;
            }
            if (location2 != null) {
                return location2;
            }
        } catch (Throwable th) {
        }
        return null;
    }

    /* renamed from: ˋ */
    private static boolean m309(@NonNull Context context, @NonNull String[] strArr) {
        for (String r3 : strArr) {
            if (C0439a.m291(context, r3)) {
                return true;
            }
        }
        return false;
    }
}
