package com.google.android.gms.internal.measurement;

public final class zzmm implements zzdb<zzml> {
    private static zzmm zzaty = new zzmm();
    private final zzdb<zzml> zzapj;

    public zzmm() {
        this(zzda.zzg(new zzmn()));
    }

    private zzmm(zzdb<zzml> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzaao() {
        return ((zzml) zzaty.get()).zzaao();
    }

    public final /* synthetic */ Object get() {
        return (zzml) this.zzapj.get();
    }
}
