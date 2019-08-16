package com.google.android.gms.internal.measurement;

public final class zzkq implements zzdb<zzkp> {
    private static zzkq zzasb = new zzkq();
    private final zzdb<zzkp> zzapj;

    public zzkq() {
        this(zzda.zzg(new zzks()));
    }

    private zzkq(zzdb<zzkp> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzi() {
        return ((zzkp) zzasb.get()).zzzi();
    }

    public final /* synthetic */ Object get() {
        return (zzkp) this.zzapj.get();
    }
}
