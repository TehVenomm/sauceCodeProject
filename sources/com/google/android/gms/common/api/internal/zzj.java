package com.google.android.gms.common.api.internal;

import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.zza;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Set;

public final class zzj {
    private final ArrayMap<zzh<?>, ConnectionResult> zzfgd = new ArrayMap();
    private final TaskCompletionSource<Void> zzfij = new TaskCompletionSource();
    private int zzfik;
    private boolean zzfil = false;

    public zzj(Iterable<? extends GoogleApi<?>> iterable) {
        for (GoogleApi zzafj : iterable) {
            this.zzfgd.put(zzafj.zzafj(), null);
        }
        this.zzfik = this.zzfgd.keySet().size();
    }

    public final Task<Void> getTask() {
        return this.zzfij.getTask();
    }

    public final void zza(zzh<?> zzh, ConnectionResult connectionResult) {
        this.zzfgd.put(zzh, connectionResult);
        this.zzfik--;
        if (!connectionResult.isSuccess()) {
            this.zzfil = true;
        }
        if (this.zzfik != 0) {
            return;
        }
        if (this.zzfil) {
            this.zzfij.setException(new zza(this.zzfgd));
            return;
        }
        this.zzfij.setResult(null);
    }

    public final Set<zzh<?>> zzafw() {
        return this.zzfgd.keySet();
    }

    public final void zzafx() {
        this.zzfij.setResult(null);
    }
}
