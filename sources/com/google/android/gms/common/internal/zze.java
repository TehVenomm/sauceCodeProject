package com.google.android.gms.common.internal;

import android.app.PendingIntent;
import android.os.Bundle;
import android.support.annotation.BinderThread;
import com.google.android.gms.common.ConnectionResult;

abstract class zze extends zzi<Boolean> {
    private int statusCode;
    private Bundle zzfte;
    private /* synthetic */ zzd zzftf;

    @BinderThread
    protected zze(zzd zzd, int i, Bundle bundle) {
        this.zzftf = zzd;
        super(zzd, Boolean.valueOf(true));
        this.statusCode = i;
        this.zzfte = bundle;
    }

    protected abstract boolean zzajn();

    protected abstract void zzj(ConnectionResult connectionResult);

    protected final /* synthetic */ void zzs(Object obj) {
        PendingIntent pendingIntent = null;
        if (((Boolean) obj) == null) {
            this.zzftf.zza(1, null);
            return;
        }
        switch (this.statusCode) {
            case 0:
                if (!zzajn()) {
                    this.zzftf.zza(1, null);
                    zzj(new ConnectionResult(8, null));
                    return;
                }
                return;
            case 10:
                this.zzftf.zza(1, null);
                throw new IllegalStateException("A fatal developer error has occurred. Check the logs for further information.");
            default:
                this.zzftf.zza(1, null);
                if (this.zzfte != null) {
                    pendingIntent = (PendingIntent) this.zzfte.getParcelable("pendingIntent");
                }
                zzj(new ConnectionResult(this.statusCode, pendingIntent));
                return;
        }
    }
}
