package com.google.android.gms.internal.measurement;

public final class zzjm implements zzdb<zzjl> {
    private static zzjm zzapt = new zzjm();
    private final zzdb<zzjl> zzapj;

    public zzjm() {
        this(zzda.zzg(new zzjo()));
    }

    private zzjm(zzdb<zzjl> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzxm() {
        return ((zzjl) zzapt.get()).zzxm();
    }

    public final /* synthetic */ Object get() {
        return (zzjl) this.zzapj.get();
    }
}
