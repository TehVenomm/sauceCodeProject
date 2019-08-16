package com.google.firebase.analytics;

import java.util.concurrent.Callable;
import java.util.concurrent.TimeoutException;

final class zzb implements Callable<String> {
    private final /* synthetic */ FirebaseAnalytics zzaca;

    zzb(FirebaseAnalytics firebaseAnalytics) {
        this.zzaca = firebaseAnalytics;
    }

    public final /* synthetic */ Object call() throws Exception {
        String zza = this.zzaca.zzi();
        if (zza == null) {
            zza = this.zzaca.zzl ? this.zzaca.zzabu.getAppInstanceId() : this.zzaca.zzj.zzq().zzy(120000);
            if (zza == null) {
                throw new TimeoutException();
            }
            this.zzaca.zzbg(zza);
        }
        return zza;
    }
}
