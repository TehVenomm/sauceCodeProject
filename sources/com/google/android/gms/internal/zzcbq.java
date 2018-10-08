package com.google.android.gms.internal;

public final class zzcbq {
    private final int mPriority;
    private /* synthetic */ zzcbo zzipt;
    private final boolean zzipu;
    private final boolean zzipv;

    zzcbq(zzcbo zzcbo, int i, boolean z, boolean z2) {
        this.zzipt = zzcbo;
        this.mPriority = i;
        this.zzipu = z;
        this.zzipv = z2;
    }

    public final void log(String str) {
        this.zzipt.zza(this.mPriority, this.zzipu, this.zzipv, str, null, null, null);
    }

    public final void zzd(String str, Object obj, Object obj2, Object obj3) {
        this.zzipt.zza(this.mPriority, this.zzipu, this.zzipv, str, obj, obj2, obj3);
    }

    public final void zze(String str, Object obj, Object obj2) {
        this.zzipt.zza(this.mPriority, this.zzipu, this.zzipv, str, obj, obj2, null);
    }

    public final void zzj(String str, Object obj) {
        this.zzipt.zza(this.mPriority, this.zzipu, this.zzipv, str, obj, null, null);
    }
}
