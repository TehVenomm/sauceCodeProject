package com.google.android.gms.common.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;

final class zzac implements zzg {
    private /* synthetic */ OnConnectionFailedListener zzfuf;

    zzac(OnConnectionFailedListener onConnectionFailedListener) {
        this.zzfuf = onConnectionFailedListener;
    }

    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzfuf.onConnectionFailed(connectionResult);
    }
}
