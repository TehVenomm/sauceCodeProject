package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzbp;

public final class zzw implements ConnectionCallbacks, OnConnectionFailedListener {
    public final Api<?> zzfda;
    private final boolean zzfjm;
    private zzx zzfjn;

    public zzw(Api<?> api, boolean z) {
        this.zzfda = api;
        this.zzfjm = z;
    }

    private final void zzagg() {
        zzbp.zzb(this.zzfjn, (Object) "Callbacks must be attached to a ClientConnectionHelper instance before connecting the client.");
    }

    public final void onConnected(@Nullable Bundle bundle) {
        zzagg();
        this.zzfjn.onConnected(bundle);
    }

    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        zzagg();
        this.zzfjn.zza(connectionResult, this.zzfda, this.zzfjm);
    }

    public final void onConnectionSuspended(int i) {
        zzagg();
        this.zzfjn.onConnectionSuspended(i);
    }

    public final void zza(zzx zzx) {
        this.zzfjn = zzx;
    }
}
