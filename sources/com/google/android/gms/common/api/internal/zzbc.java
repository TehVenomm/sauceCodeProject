package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.Result;
import java.util.Collections;

public final class zzbc implements zzbk {
    private final zzbl zzflb;

    public zzbc(zzbl zzbl) {
        this.zzflb = zzbl;
    }

    public final void begin() {
        for (zze disconnect : this.zzflb.zzfmh.values()) {
            disconnect.disconnect();
        }
        this.zzflb.zzfjo.zzfmi = Collections.emptySet();
    }

    public final void connect() {
        this.zzflb.zzahk();
    }

    public final boolean disconnect() {
        return true;
    }

    public final void onConnected(Bundle bundle) {
    }

    public final void onConnectionSuspended(int i) {
    }

    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(T t) {
        this.zzflb.zzfjo.zzfkm.add(t);
        return t;
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(T t) {
        throw new IllegalStateException("GoogleApiClient is not connected yet.");
    }
}
