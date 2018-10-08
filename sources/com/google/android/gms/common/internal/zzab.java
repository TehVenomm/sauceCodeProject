package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;

final class zzab implements zzf {
    private /* synthetic */ ConnectionCallbacks zzfue;

    zzab(ConnectionCallbacks connectionCallbacks) {
        this.zzfue = connectionCallbacks;
    }

    public final void onConnected(@Nullable Bundle bundle) {
        this.zzfue.onConnected(bundle);
    }

    public final void onConnectionSuspended(int i) {
        this.zzfue.onConnectionSuspended(i);
    }
}
