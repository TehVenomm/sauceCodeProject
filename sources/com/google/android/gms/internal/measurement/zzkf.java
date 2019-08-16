package com.google.android.gms.internal.measurement;

public final class zzkf implements zzdb<zzki> {
    private static zzkf zzart = new zzkf();
    private final zzdb<zzki> zzapj;

    public zzkf() {
        this(zzda.zzg(new zzkh()));
    }

    private zzkf(zzdb<zzki> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzze() {
        return ((zzki) zzart.get()).zzze();
    }

    public final /* synthetic */ Object get() {
        return (zzki) this.zzapj.get();
    }
}
