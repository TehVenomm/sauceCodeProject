package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.job.JobParameters;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.os.IBinder;
import android.support.annotation.MainThread;
import com.facebook.internal.NativeProtocol;
import com.google.android.gms.common.internal.zzbp;

public final class zzcez<T extends Context & zzcfc> {
    private final T zziwi;

    public zzcez(T t) {
        zzbp.zzu(t);
        this.zziwi = t;
    }

    private final void zza(Integer num, JobParameters jobParameters) {
        zzcco zzdm = zzcco.zzdm(this.zziwi);
        zzdm.zzauj().zzg(new zzcfa(this, zzdm, num, zzdm.zzauk(), jobParameters));
    }

    private final zzcbo zzauk() {
        return zzcco.zzdm(this.zziwi).zzauk();
    }

    public static boolean zzk(Context context, boolean z) {
        zzbp.zzu(context);
        return VERSION.SDK_INT >= 24 ? zzcfo.zzw(context, "com.google.android.gms.measurement.AppMeasurementJobService") : zzcfo.zzw(context, "com.google.android.gms.measurement.AppMeasurementService");
    }

    @MainThread
    public final IBinder onBind(Intent intent) {
        if (intent == null) {
            zzauk().zzayc().log("onBind called with null intent");
            return null;
        }
        String action = intent.getAction();
        if ("com.google.android.gms.measurement.START".equals(action)) {
            return new zzcct(zzcco.zzdm(this.zziwi));
        }
        zzauk().zzaye().zzj("onBind received unknown action", action);
        return null;
    }

    @MainThread
    public final void onCreate() {
        zzcbo zzauk = zzcco.zzdm(this.zziwi).zzauk();
        zzcap.zzawj();
        zzauk.zzayi().log("Local AppMeasurementService is starting up");
    }

    @MainThread
    public final void onDestroy() {
        zzcbo zzauk = zzcco.zzdm(this.zziwi).zzauk();
        zzcap.zzawj();
        zzauk.zzayi().log("Local AppMeasurementService is shutting down");
    }

    @MainThread
    public final void onRebind(Intent intent) {
        if (intent == null) {
            zzauk().zzayc().log("onRebind called with null intent");
            return;
        }
        zzauk().zzayi().zzj("onRebind called. action", intent.getAction());
    }

    @MainThread
    public final int onStartCommand(Intent intent, int i, int i2) {
        zzcbo zzauk = zzcco.zzdm(this.zziwi).zzauk();
        if (intent == null) {
            zzauk.zzaye().log("AppMeasurementService started with null intent");
        } else {
            String action = intent.getAction();
            zzcap.zzawj();
            zzauk.zzayi().zze("Local AppMeasurementService called. startId, action", Integer.valueOf(i2), action);
            if ("com.google.android.gms.measurement.UPLOAD".equals(action)) {
                zza(Integer.valueOf(i2), null);
            }
        }
        return 2;
    }

    @TargetApi(24)
    @MainThread
    public final boolean onStartJob(JobParameters jobParameters) {
        zzcbo zzauk = zzcco.zzdm(this.zziwi).zzauk();
        String string = jobParameters.getExtras().getString(NativeProtocol.WEB_DIALOG_ACTION);
        zzcap.zzawj();
        zzauk.zzayi().zzj("Local AppMeasurementJobService called. action", string);
        if ("com.google.android.gms.measurement.UPLOAD".equals(string)) {
            zza(null, jobParameters);
        }
        return true;
    }

    @MainThread
    public final boolean onUnbind(Intent intent) {
        if (intent == null) {
            zzauk().zzayc().log("onUnbind called with null intent");
        } else {
            zzauk().zzayi().zzj("onUnbind called for intent. action", intent.getAction());
        }
        return true;
    }
}
