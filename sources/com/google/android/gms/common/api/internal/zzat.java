package com.google.android.gms.common.api.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzj;
import java.lang.ref.WeakReference;

final class zzat implements zzj {
    private final Api<?> zzfda;
    private final boolean zzfjm;
    private final WeakReference<zzar> zzfls;

    public zzat(zzar zzar, Api<?> api, boolean z) {
        this.zzfls = new WeakReference(zzar);
        this.zzfda = api;
        this.zzfjm = z;
    }

    public final void zzf(@NonNull ConnectionResult connectionResult) {
        boolean z = false;
        zzar zzar = (zzar) this.zzfls.get();
        if (zzar != null) {
            if (Looper.myLooper() == zzar.zzflb.zzfjo.getLooper()) {
                z = true;
            }
            zzbp.zza(z, (Object) "onReportServiceBinding must be called on the GoogleApiClient handler thread");
            zzar.zzfjy.lock();
            try {
                if (zzar.zzbq(0)) {
                    if (!connectionResult.isSuccess()) {
                        zzar.zzb(connectionResult, this.zzfda, this.zzfjm);
                    }
                    if (zzar.zzagz()) {
                        zzar.zzaha();
                    }
                    zzar.zzfjy.unlock();
                }
            } finally {
                zzar.zzfjy.unlock();
            }
        }
    }
}
