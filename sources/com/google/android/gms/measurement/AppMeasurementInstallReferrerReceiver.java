package com.google.android.gms.measurement;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.support.annotation.MainThread;
import com.google.android.gms.internal.zzccf;
import com.google.android.gms.internal.zzcch;

public final class AppMeasurementInstallReferrerReceiver extends BroadcastReceiver implements zzcch {
    private zzccf zzikl;

    public final void doStartService(Context context, Intent intent) {
    }

    @MainThread
    public final void onReceive(Context context, Intent intent) {
        if (this.zzikl == null) {
            this.zzikl = new zzccf(this);
        }
        this.zzikl.onReceive(context, intent);
    }
}
