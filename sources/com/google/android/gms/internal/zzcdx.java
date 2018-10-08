package com.google.android.gms.internal;

final class zzcdx implements Runnable {
    private /* synthetic */ String val$name;
    private /* synthetic */ String zziud;
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ long zziuu;
    private /* synthetic */ Object zziuz;

    zzcdx(zzcdo zzcdo, String str, String str2, Object obj, long j) {
        this.zziup = zzcdo;
        this.zziud = str;
        this.val$name = str2;
        this.zziuz = obj;
        this.zziuu = j;
    }

    public final void run() {
        this.zziup.zza(this.zziud, this.val$name, this.zziuz, this.zziuu);
    }
}
