package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.os.IInterface;

public final class zzl implements ServiceConnection {
    private /* synthetic */ zzd zzftf;
    private final int zzfti;

    public zzl(zzd zzd, int i) {
        this.zzftf = zzd;
        this.zzfti = i;
    }

    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        if (iBinder == null) {
            this.zzftf.zzcc(16);
            return;
        }
        synchronized (this.zzftf.zzfsp) {
            zzax zzax;
            zzd zzd = this.zzftf;
            if (iBinder == null) {
                zzax = null;
            } else {
                IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IGmsServiceBroker");
                zzax = (queryLocalInterface == null || !(queryLocalInterface instanceof zzax)) ? new zzay(iBinder) : (zzax) queryLocalInterface;
            }
            zzd.zzfsq = zzax;
        }
        this.zzftf.zza(0, null, this.zzfti);
    }

    public final void onServiceDisconnected(ComponentName componentName) {
        synchronized (this.zzftf.zzfsp) {
            this.zzftf.zzfsq = null;
        }
        this.zzftf.mHandler.sendMessage(this.zzftf.mHandler.obtainMessage(6, this.zzfti, 1));
    }
}
