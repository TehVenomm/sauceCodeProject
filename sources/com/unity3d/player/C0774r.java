package com.unity3d.player;

import android.content.Context;
import android.hardware.GeomagneticField;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.location.LocationProvider;
import android.os.Bundle;
import java.util.List;

/* renamed from: com.unity3d.player.r */
final class C0774r implements LocationListener {
    /* renamed from: a */
    private final Context f548a;
    /* renamed from: b */
    private final UnityPlayer f549b;
    /* renamed from: c */
    private Location f550c;
    /* renamed from: d */
    private float f551d = 0.0f;
    /* renamed from: e */
    private boolean f552e = false;
    /* renamed from: f */
    private int f553f = 0;
    /* renamed from: g */
    private boolean f554g = false;
    /* renamed from: h */
    private int f555h = 0;

    protected C0774r(Context context, UnityPlayer unityPlayer) {
        this.f548a = context;
        this.f549b = unityPlayer;
    }

    /* renamed from: a */
    private void m516a(int i) {
        this.f555h = i;
        this.f549b.nativeSetLocationStatus(i);
    }

    /* renamed from: a */
    private void m517a(Location location) {
        if (location != null && C0774r.m518a(location, this.f550c)) {
            this.f550c = location;
            this.f549b.nativeSetLocation((float) location.getLatitude(), (float) location.getLongitude(), (float) location.getAltitude(), location.getAccuracy(), ((double) location.getTime()) / 1000.0d, new GeomagneticField((float) this.f550c.getLatitude(), (float) this.f550c.getLongitude(), (float) this.f550c.getAltitude(), this.f550c.getTime()).getDeclination());
        }
    }

    /* renamed from: a */
    private static boolean m518a(Location location, Location location2) {
        if (location2 == null) {
            return true;
        }
        long time = location.getTime() - location2.getTime();
        boolean z = time > 120000;
        boolean z2 = time < -120000;
        boolean z3 = time > 0;
        if (z) {
            return true;
        }
        if (z2) {
            return false;
        }
        int accuracy = (int) (location.getAccuracy() - location2.getAccuracy());
        return !(accuracy < 0) ? (!z3 || (accuracy > 0)) ? z3 && ((accuracy > 200 ? 1 : 0) | (location.getAccuracy() == 0.0f ? 1 : 0)) == 0 && C0774r.m519a(location.getProvider(), location2.getProvider()) : true : true;
    }

    /* renamed from: a */
    private static boolean m519a(String str, String str2) {
        return str == null ? str2 == null : str.equals(str2);
    }

    /* renamed from: a */
    public final void m520a(float f) {
        this.f551d = f;
    }

    /* renamed from: a */
    public final boolean m521a() {
        return !((LocationManager) this.f548a.getSystemService("location")).getProviders(new Criteria(), true).isEmpty();
    }

    /* renamed from: b */
    public final void m522b() {
        this.f554g = false;
        if (this.f552e) {
            C0767m.Log(5, "Location_StartUpdatingLocation already started!");
        } else if (m521a()) {
            LocationManager locationManager = (LocationManager) this.f548a.getSystemService("location");
            m516a(1);
            List<String> providers = locationManager.getProviders(true);
            if (providers.isEmpty()) {
                m516a(3);
                return;
            }
            LocationProvider locationProvider;
            if (this.f553f == 2) {
                for (String provider : providers) {
                    LocationProvider provider2 = locationManager.getProvider(provider);
                    if (provider2.getAccuracy() == 2) {
                        locationProvider = provider2;
                        break;
                    }
                }
            }
            locationProvider = null;
            for (String provider3 : providers) {
                if (locationProvider == null || locationManager.getProvider(provider3).getAccuracy() != 1) {
                    m517a(locationManager.getLastKnownLocation(provider3));
                    locationManager.requestLocationUpdates(provider3, 0, this.f551d, this, this.f548a.getMainLooper());
                    this.f552e = true;
                }
            }
        } else {
            m516a(3);
        }
    }

    /* renamed from: b */
    public final void m523b(float f) {
        if (f < 100.0f) {
            this.f553f = 1;
        } else if (f < 500.0f) {
            this.f553f = 1;
        } else {
            this.f553f = 2;
        }
    }

    /* renamed from: c */
    public final void m524c() {
        ((LocationManager) this.f548a.getSystemService("location")).removeUpdates(this);
        this.f552e = false;
        this.f550c = null;
        m516a(0);
    }

    /* renamed from: d */
    public final void m525d() {
        if (this.f555h == 1 || this.f555h == 2) {
            this.f554g = true;
            m524c();
        }
    }

    /* renamed from: e */
    public final void m526e() {
        if (this.f554g) {
            m522b();
        }
    }

    public final void onLocationChanged(Location location) {
        m516a(2);
        m517a(location);
    }

    public final void onProviderDisabled(String str) {
        this.f550c = null;
    }

    public final void onProviderEnabled(String str) {
    }

    public final void onStatusChanged(String str, int i, Bundle bundle) {
    }
}
