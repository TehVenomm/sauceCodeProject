package com.appsflyer;

import android.content.Context;
import android.location.Location;
import android.location.LocationManager;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.appsflyer.C0273g.C0271a;

/* renamed from: com.appsflyer.j */
final class C0278j {

    /* renamed from: com.appsflyer.j$d */
    static final class C0277d {
        /* renamed from: ॱ */
        static final C0278j f271 = new C0278j();
    }

    C0278j() {
    }

    @Nullable
    /* renamed from: ॱ */
    static Location m319(@NonNull Context context) {
        try {
            Location lastKnownLocation;
            Location lastKnownLocation2;
            LocationManager locationManager = (LocationManager) context.getSystemService("location");
            String str = "network";
            if (C0278j.m318(context, new String[]{"android.permission.ACCESS_FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION"})) {
                lastKnownLocation = locationManager.getLastKnownLocation(str);
            } else {
                lastKnownLocation = null;
            }
            String str2 = "gps";
            if (C0278j.m318(context, new String[]{"android.permission.ACCESS_FINE_LOCATION"})) {
                lastKnownLocation2 = locationManager.getLastKnownLocation(str2);
            } else {
                lastKnownLocation2 = null;
            }
            if (lastKnownLocation2 == null && lastKnownLocation == null) {
                lastKnownLocation2 = null;
            } else if (lastKnownLocation2 == null && lastKnownLocation != null) {
                lastKnownLocation2 = lastKnownLocation;
            } else if ((lastKnownLocation != null || lastKnownLocation2 == null) && 60000 < lastKnownLocation.getTime() - lastKnownLocation2.getTime()) {
                lastKnownLocation2 = lastKnownLocation;
            }
            if (lastKnownLocation2 != null) {
                return lastKnownLocation2;
            }
        } catch (Throwable th) {
        }
        return null;
    }

    /* renamed from: ˋ */
    private static boolean m318(@NonNull Context context, @NonNull String[] strArr) {
        for (String ˎ : strArr) {
            if (C0271a.m299(context, ˎ)) {
                return true;
            }
        }
        return false;
    }
}
