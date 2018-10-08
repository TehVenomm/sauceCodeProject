package com.google.android.gms.measurement;

import android.annotation.TargetApi;
import android.app.job.JobParameters;
import android.app.job.JobService;
import android.content.Intent;
import android.support.annotation.MainThread;
import com.google.android.gms.internal.zzcez;
import com.google.android.gms.internal.zzcfc;

@TargetApi(24)
public final class AppMeasurementJobService extends JobService implements zzcfc {
    private zzcez zzikm;

    private final zzcez zzats() {
        if (this.zzikm == null) {
            this.zzikm = new zzcez(this);
        }
        return this.zzikm;
    }

    public final boolean callServiceStopSelfResult(int i) {
        throw new UnsupportedOperationException();
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

    public final boolean onStartJob(JobParameters jobParameters) {
        return zzats().onStartJob(jobParameters);
    }

    public final boolean onStopJob(JobParameters jobParameters) {
        return false;
    }

    @MainThread
    public final boolean onUnbind(Intent intent) {
        return zzats().onUnbind(intent);
    }

    @TargetApi(24)
    public final void zza(JobParameters jobParameters, boolean z) {
        jobFinished(jobParameters, false);
    }
}
