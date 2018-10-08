package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import java.util.concurrent.atomic.AtomicReference;

final class zzbf implements ConnectionCallbacks {
    private /* synthetic */ zzbd zzfmp;
    private /* synthetic */ AtomicReference zzfmq;
    private /* synthetic */ zzda zzfmr;

    zzbf(zzbd zzbd, AtomicReference atomicReference, zzda zzda) {
        this.zzfmp = zzbd;
        this.zzfmq = atomicReference;
        this.zzfmr = zzda;
    }

    public final void onConnected(Bundle bundle) {
        this.zzfmp.zza((GoogleApiClient) this.zzfmq.get(), this.zzfmr, true);
    }

    public final void onConnectionSuspended(int i) {
    }
}
