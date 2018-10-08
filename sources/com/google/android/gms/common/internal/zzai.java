package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import java.util.HashSet;
import java.util.Set;

final class zzai implements ServiceConnection {
    private int mState = 2;
    private IBinder zzftk;
    private ComponentName zzfuq;
    private final Set<ServiceConnection> zzfuw = new HashSet();
    private boolean zzfux;
    private final zzag zzfuy;
    private /* synthetic */ zzah zzfuz;

    public zzai(zzah zzah, zzag zzag) {
        this.zzfuz = zzah;
        this.zzfuy = zzag;
    }

    public final IBinder getBinder() {
        return this.zzftk;
    }

    public final ComponentName getComponentName() {
        return this.zzfuq;
    }

    public final int getState() {
        return this.mState;
    }

    public final boolean isBound() {
        return this.zzfux;
    }

    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        synchronized (this.zzfuz.zzfus) {
            this.zzfuz.mHandler.removeMessages(1, this.zzfuy);
            this.zzftk = iBinder;
            this.zzfuq = componentName;
            for (ServiceConnection onServiceConnected : this.zzfuw) {
                onServiceConnected.onServiceConnected(componentName, iBinder);
            }
            this.mState = 1;
        }
    }

    public final void onServiceDisconnected(ComponentName componentName) {
        synchronized (this.zzfuz.zzfus) {
            this.zzfuz.mHandler.removeMessages(1, this.zzfuy);
            this.zzftk = null;
            this.zzfuq = componentName;
            for (ServiceConnection onServiceDisconnected : this.zzfuw) {
                onServiceDisconnected.onServiceDisconnected(componentName);
            }
            this.mState = 2;
        }
    }

    public final void zza(ServiceConnection serviceConnection, String str) {
        this.zzfuz.zzfut;
        this.zzfuz.mApplicationContext;
        this.zzfuy.zzakh();
        this.zzfuw.add(serviceConnection);
    }

    public final boolean zza(ServiceConnection serviceConnection) {
        return this.zzfuw.contains(serviceConnection);
    }

    public final boolean zzaki() {
        return this.zzfuw.isEmpty();
    }

    public final void zzb(ServiceConnection serviceConnection, String str) {
        this.zzfuz.zzfut;
        this.zzfuz.mApplicationContext;
        this.zzfuw.remove(serviceConnection);
    }

    public final void zzgb(String str) {
        this.mState = 3;
        this.zzfux = this.zzfuz.zzfut.zza(this.zzfuz.mApplicationContext, str, this.zzfuy.zzakh(), this, this.zzfuy.zzakg());
        if (this.zzfux) {
            this.zzfuz.mHandler.sendMessageDelayed(this.zzfuz.mHandler.obtainMessage(1, this.zzfuy), this.zzfuz.zzfuv);
            return;
        }
        this.mState = 2;
        try {
            this.zzfuz.zzfut;
            this.zzfuz.mApplicationContext.unbindService(this);
        } catch (IllegalArgumentException e) {
        }
    }

    public final void zzgc(String str) {
        this.zzfuz.mHandler.removeMessages(1, this.zzfuy);
        this.zzfuz.zzfut;
        this.zzfuz.mApplicationContext.unbindService(this);
        this.zzfux = false;
        this.mState = 2;
    }
}
