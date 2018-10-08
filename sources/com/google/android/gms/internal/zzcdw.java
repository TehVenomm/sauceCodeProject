package com.google.android.gms.internal;

import android.os.Bundle;

final class zzcdw implements Runnable {
    private /* synthetic */ String val$name;
    private /* synthetic */ String zziab;
    private /* synthetic */ String zziud;
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ long zziuu;
    private /* synthetic */ Bundle zziuv;
    private /* synthetic */ boolean zziuw;
    private /* synthetic */ boolean zziux;
    private /* synthetic */ boolean zziuy;

    zzcdw(zzcdo zzcdo, String str, String str2, long j, Bundle bundle, boolean z, boolean z2, boolean z3, String str3) {
        this.zziup = zzcdo;
        this.zziud = str;
        this.val$name = str2;
        this.zziuu = j;
        this.zziuv = bundle;
        this.zziuw = z;
        this.zziux = z2;
        this.zziuy = z3;
        this.zziab = str3;
    }

    public final void run() {
        this.zziup.zzb(this.zziud, this.val$name, this.zziuu, this.zziuv, this.zziuw, this.zziux, this.zziuy, this.zziab);
    }
}
