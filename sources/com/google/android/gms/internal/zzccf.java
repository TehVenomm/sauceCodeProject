package com.google.android.gms.internal;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.MainThread;
import com.google.android.gms.common.internal.zzbp;

public final class zzccf {
    private final zzcch zzirh;

    public zzccf(zzcch zzcch) {
        zzbp.zzu(zzcch);
        this.zzirh = zzcch;
    }

    public static boolean zzj(Context context, boolean z) {
        zzbp.zzu(context);
        return zzcfo.zza(context, "com.google.android.gms.measurement.AppMeasurementReceiver", false);
    }

    @MainThread
    public final void onReceive(Context context, Intent intent) {
        zzcco zzdm = zzcco.zzdm(context);
        zzcbo zzauk = zzdm.zzauk();
        if (intent == null) {
            zzauk.zzaye().log("Receiver called with null intent");
            return;
        }
        zzcap.zzawj();
        String action = intent.getAction();
        zzauk.zzayi().zzj("Local receiver got", action);
        if ("com.google.android.gms.measurement.UPLOAD".equals(action)) {
            zzcez.zzk(context, false);
            Intent className = new Intent().setClassName(context, "com.google.android.gms.measurement.AppMeasurementService");
            className.setAction("com.google.android.gms.measurement.UPLOAD");
            this.zzirh.doStartService(context, className);
        } else if ("com.android.vending.INSTALL_REFERRER".equals(action)) {
            action = intent.getStringExtra("referrer");
            if (action == null) {
                zzauk.zzayi().log("Install referrer extras are null");
                return;
            }
            zzauk.zzayg().zzj("Install referrer extras are", action);
            if (!action.contains("?")) {
                action = String.valueOf(action);
                action = action.length() != 0 ? "?".concat(action) : new String("?");
            }
            Bundle zzq = zzdm.zzaug().zzq(Uri.parse(action));
            if (zzq == null) {
                zzauk.zzayi().log("No campaign defined in install referrer broadcast");
                return;
            }
            long longExtra = intent.getLongExtra("referrer_timestamp_seconds", 0) * 1000;
            if (longExtra == 0) {
                zzauk.zzaye().log("Install referrer is missing timestamp");
            }
            zzdm.zzauj().zzg(new zzccg(this, zzdm, longExtra, zzq, context, zzauk));
        }
    }
}
