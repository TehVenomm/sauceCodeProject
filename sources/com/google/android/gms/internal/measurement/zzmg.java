package com.google.android.gms.internal.measurement;

public final class zzmg implements zzdb<zzmf> {
    private static zzmg zzatu = new zzmg();
    private final zzdb<zzmf> zzapj;

    public zzmg() {
        this(zzda.zzg(new zzmi()));
    }

    private zzmg(zzdb<zzmf> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzaam() {
        return ((zzmf) zzatu.get()).zzaam();
    }

    public final /* synthetic */ Object get() {
        return (zzmf) this.zzapj.get();
    }
}
