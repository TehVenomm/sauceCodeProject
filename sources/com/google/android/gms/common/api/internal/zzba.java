package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;

final class zzba implements ConnectionCallbacks, OnConnectionFailedListener {
    private /* synthetic */ zzar zzflr;

    private zzba(zzar zzar) {
        this.zzflr = zzar;
    }

    public final void onConnected(Bundle bundle) {
        this.zzflr.zzflj.zza(new zzay(this.zzflr));
    }

    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzflr.zzfjy.lock();
        try {
            if (this.zzflr.zzd(connectionResult)) {
                this.zzflr.zzahc();
                this.zzflr.zzaha();
            } else {
                this.zzflr.zze(connectionResult);
            }
            this.zzflr.zzfjy.unlock();
        } catch (Throwable th) {
            this.zzflr.zzfjy.unlock();
        }
    }

    public final void onConnectionSuspended(int i) {
    }
}
