package com.google.android.gms.measurement;

import android.content.Context;
import android.content.Intent;
import android.support.annotation.MainThread;
import android.support.v4.content.WakefulBroadcastReceiver;
import com.google.android.gms.internal.zzccf;
import com.google.android.gms.internal.zzcch;

public final class AppMeasurementReceiver extends WakefulBroadcastReceiver implements zzcch {
    private zzccf zzikl;

    @MainThread
    public final void doStartService(Context context, Intent intent) {
        WakefulBroadcastReceiver.startWakefulService(context, intent);
    }

    @MainThread
    public final void onReceive(Context context, Intent intent) {
        if (this.zzikl == null) {
            this.zzikl = new zzccf(this);
        }
        this.zzikl.onReceive(context, intent);
    }
}
