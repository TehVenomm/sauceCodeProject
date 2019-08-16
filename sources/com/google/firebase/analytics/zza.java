package com.google.firebase.analytics;

import android.os.Bundle;
import com.google.android.gms.internal.measurement.zzz;
import com.google.android.gms.measurement.internal.zzgk;
import com.google.android.gms.measurement.internal.zzgn;
import com.google.android.gms.measurement.internal.zzhi;
import java.util.List;
import java.util.Map;

final class zza implements zzhi {
    private final /* synthetic */ zzz zzabz;

    zza(zzz zzz) {
        this.zzabz = zzz;
    }

    public final void beginAdUnitExposure(String str) {
        this.zzabz.beginAdUnitExposure(str);
    }

    public final void clearConditionalUserProperty(String str, String str2, Bundle bundle) {
        this.zzabz.clearConditionalUserProperty(str, str2, bundle);
    }

    public final void endAdUnitExposure(String str) {
        this.zzabz.endAdUnitExposure(str);
    }

    public final long generateEventId() {
        return this.zzabz.generateEventId();
    }

    public final List<Bundle> getConditionalUserProperties(String str, String str2) {
        return this.zzabz.getConditionalUserProperties(str, str2);
    }

    public final String getCurrentScreenClass() {
        return this.zzabz.getCurrentScreenClass();
    }

    public final String getCurrentScreenName() {
        return this.zzabz.getCurrentScreenName();
    }

    public final String getGmpAppId() {
        return this.zzabz.getGmpAppId();
    }

    public final int getMaxUserProperties(String str) {
        return this.zzabz.getMaxUserProperties(str);
    }

    public final Map<String, Object> getUserProperties(String str, String str2, boolean z) {
        return this.zzabz.getUserProperties(str, str2, z);
    }

    public final void logEventInternal(String str, String str2, Bundle bundle) {
        this.zzabz.logEventInternal(str, str2, bundle);
    }

    public final void logEventInternalNoInterceptor(String str, String str2, Bundle bundle, long j) {
        this.zzabz.logEventInternalNoInterceptor(str, str2, bundle, j);
    }

    public final void setConditionalUserProperty(Bundle bundle) {
        this.zzabz.setConditionalUserProperty(bundle);
    }

    public final void setDataCollectionEnabled(boolean z) {
        this.zzabz.setDataCollectionEnabled(z);
    }

    public final void setMeasurementEnabled(boolean z) {
        this.zzabz.setMeasurementEnabled(z);
    }

    public final void setUserPropertyInternal(String str, String str2, Object obj) {
        this.zzabz.setUserPropertyInternal(str, str2, obj);
    }

    public final void zza(zzgk zzgk) {
        this.zzabz.zza(zzgk);
    }

    public final void zza(zzgn zzgn) {
        this.zzabz.zza(zzgn);
    }

    public final Object zzb(int i) {
        return this.zzabz.zzb(i);
    }

    public final void zzb(zzgn zzgn) {
        this.zzabz.zzb(zzgn);
    }

    public final String zzi() {
        return this.zzabz.zzi();
    }
}
