package com.google.android.gms.measurement;

import android.app.Service;
import android.app.job.JobParameters;
import android.content.Intent;
import android.os.IBinder;
import android.support.annotation.MainThread;
import android.support.v4.content.WakefulBroadcastReceiver;
import com.google.android.gms.internal.zzcez;
import com.google.android.gms.internal.zzcfc;

public final class AppMeasurementService extends Service implements zzcfc {
    private zzcez zzikm;

    private final zzcez zzats() {
        if (this.zzikm == null) {
            this.zzikm = new zzcez(this);
        }
        return this.zzikm;
    }

    public final boolean callServiceStopSelfResult(int i) {
        return stopSelfResult(i);
    }

    @MainThread
    public final IBinder onBind(Intent intent) {
        return zzats().onBind(intent);
    }

    @MainThread
    public final void onCreate() {
        super.onCreate();
        zzats().onCreate();
    }

    @MainThread
    public final void onDestroy() {
        zzats().onDestroy();
        super.onDestroy();
    }

    @MainThread
    public final void onRebind(Intent intent) {
        zzats().onRebind(intent);
    }

    @MainThread
    public final int onStartCommand(Intent intent, int i, int i2) {
        zzats().onStartCommand(intent, i, i2);
        WakefulBroadcastReceiver.completeWakefulIntent(intent);
        return 2;
    }

    @MainThread
    public final boolean onUnbind(Intent intent) {
        return zzats().onUnbind(intent);
    }

    public final void zza(JobParameters jobParameters, boolean z) {
        throw new UnsupportedOperationException();
    }
}
