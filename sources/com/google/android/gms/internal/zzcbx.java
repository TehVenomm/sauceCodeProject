package com.google.android.gms.internal;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.annotation.MainThread;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;

class zzcbx extends BroadcastReceiver {
    private static String zzdtp = zzcbx.class.getName();
    private boolean mRegistered;
    private boolean zzdtq;
    private final zzcco zzikb;

    zzcbx(zzcco zzcco) {
        zzbp.zzu(zzcco);
        this.zzikb = zzcco;
    }

    @MainThread
    public void onReceive(Context context, Intent intent) {
        this.zzikb.zzwh();
        String action = intent.getAction();
        this.zzikb.zzauk().zzayi().zzj("NetworkBroadcastReceiver received action", action);
        if ("android.net.conn.CONNECTIVITY_CHANGE".equals(action)) {
            boolean zzyu = this.zzikb.zzayz().zzyu();
            if (this.zzdtq != zzyu) {
                this.zzdtq = zzyu;
                this.zzikb.zzauj().zzg(new zzcby(this, zzyu));
                return;
            }
            return;
        }
        this.zzikb.zzauk().zzaye().zzj("NetworkBroadcastReceiver received unknown action", action);
    }

    @WorkerThread
    public final void unregister() {
        this.zzikb.zzwh();
        this.zzikb.zzauj().zzug();
        this.zzikb.zzauj().zzug();
        if (this.mRegistered) {
            this.zzikb.zzauk().zzayi().log("Unregistering connectivity change receiver");
            this.mRegistered = false;
            this.zzdtq = false;
            try {
                this.zzikb.getContext().unregisterReceiver(this);
            } catch (IllegalArgumentException e) {
                this.zzikb.zzauk().zzayc().zzj("Failed to unregister the network broadcast receiver", e);
            }
        }
    }

    @WorkerThread
    public final void zzyr() {
        this.zzikb.zzwh();
        this.zzikb.zzauj().zzug();
        if (!this.mRegistered) {
            this.zzikb.getContext().registerReceiver(this, new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE"));
            this.zzdtq = this.zzikb.zzayz().zzyu();
            this.zzikb.zzauk().zzayi().zzj("Registering connectivity change receiver. Network connected", Boolean.valueOf(this.zzdtq));
            this.mRegistered = true;
        }
    }
}
