package com.google.firebase.iid;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.VisibleForTesting;
import android.util.Log;
import com.google.android.gms.common.util.concurrent.NamedThreadFactory;
import com.google.android.gms.internal.firebase_messaging.zza;
import com.google.android.gms.internal.firebase_messaging.zzf;
import com.google.android.gms.tasks.Task;
import java.util.concurrent.ScheduledExecutorService;
import javax.annotation.concurrent.GuardedBy;

public final class zzac {
    @GuardedBy("MessengerIpcClient.class")
    private static zzac zzby;
    /* access modifiers changed from: private */
    public final Context zzag;
    /* access modifiers changed from: private */
    public final ScheduledExecutorService zzbz;
    @GuardedBy("this")
    private zzae zzca = new zzae(this);
    @GuardedBy("this")
    private int zzcb = 1;

    @VisibleForTesting
    private zzac(Context context, ScheduledExecutorService scheduledExecutorService) {
        this.zzbz = scheduledExecutorService;
        this.zzag = context.getApplicationContext();
    }

    private final <T> Task<T> zza(zzaj<T> zzaj) {
        Task<T> task;
        synchronized (this) {
            if (Log.isLoggable("MessengerIpcClient", 3)) {
                String valueOf = String.valueOf(zzaj);
                Log.d("MessengerIpcClient", new StringBuilder(String.valueOf(valueOf).length() + 9).append("Queueing ").append(valueOf).toString());
            }
            if (!this.zzca.zzb(zzaj)) {
                this.zzca = new zzae(this);
                this.zzca.zzb(zzaj);
            }
            task = zzaj.zzcl.getTask();
        }
        return task;
    }

    public static zzac zzc(Context context) {
        zzac zzac;
        synchronized (zzac.class) {
            try {
                if (zzby == null) {
                    zzby = new zzac(context, zza.zza().zza(1, new NamedThreadFactory("MessengerIpcClient"), zzf.zze));
                }
                zzac = zzby;
            } finally {
                Class<zzac> cls = zzac.class;
            }
        }
        return zzac;
    }

    private final int zzx() {
        int i;
        synchronized (this) {
            i = this.zzcb;
            this.zzcb = i + 1;
        }
        return i;
    }

    public final Task<Void> zza(int i, Bundle bundle) {
        return zza((zzaj<T>) new zzak<T>(zzx(), 2, bundle));
    }

    public final Task<Bundle> zzb(int i, Bundle bundle) {
        return zza((zzaj<T>) new zzal<T>(zzx(), 1, bundle));
    }
}
