package com.google.android.gms.common.api.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;

final class zzdg implements Runnable {
    private /* synthetic */ Result zzfpi;
    private /* synthetic */ zzdf zzfpj;

    zzdg(zzdf zzdf, Result result) {
        this.zzfpj = zzdf;
        this.zzfpi = result;
    }

    @WorkerThread
    public final void run() {
        GoogleApiClient googleApiClient;
        try {
            zzs.zzfiy.set(Boolean.valueOf(true));
            this.zzfpj.zzfpg.sendMessage(this.zzfpj.zzfpg.obtainMessage(0, this.zzfpj.zzfpb.onSuccess(this.zzfpi)));
            zzs.zzfiy.set(Boolean.valueOf(false));
            zzdf.zzd(this.zzfpi);
            googleApiClient = (GoogleApiClient) this.zzfpj.zzfjb.get();
            if (googleApiClient != null) {
                googleApiClient.zzb(this.zzfpj);
            }
        } catch (RuntimeException e) {
            this.zzfpj.zzfpg.sendMessage(this.zzfpj.zzfpg.obtainMessage(1, e));
            zzs.zzfiy.set(Boolean.valueOf(false));
            zzdf.zzd(this.zzfpi);
            googleApiClient = (GoogleApiClient) this.zzfpj.zzfjb.get();
            if (googleApiClient != null) {
                googleApiClient.zzb(this.zzfpj);
            }
        } catch (Throwable th) {
            Throwable th2 = th;
            zzs.zzfiy.set(Boolean.valueOf(false));
            zzdf.zzd(this.zzfpi);
            googleApiClient = (GoogleApiClient) this.zzfpj.zzfjb.get();
            if (googleApiClient != null) {
                googleApiClient.zzb(this.zzfpj);
            }
        }
    }
}
