package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Status;

final class zzbg implements OnConnectionFailedListener {
    private /* synthetic */ zzda zzfmr;

    zzbg(zzbd zzbd, zzda zzda) {
        this.zzfmr = zzda;
    }

    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzfmr.setResult(new Status(8));
    }
}
