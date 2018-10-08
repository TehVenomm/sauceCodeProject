package com.google.android.gms.internal;

final class zzq implements Runnable {
    private /* synthetic */ String zzao;
    private /* synthetic */ long zzap;
    private /* synthetic */ zzp zzaq;

    zzq(zzp zzp, String str, long j) {
        this.zzaq = zzp;
        this.zzao = str;
        this.zzap = j;
    }

    public final void run() {
        this.zzaq.zzab.zza(this.zzao, this.zzap);
        this.zzaq.zzab.zzc(toString());
    }
}
