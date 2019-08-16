package com.google.android.gms.internal.measurement;

public final class zzjg implements zzdb<zzjf> {
    private static zzjg zzapn = new zzjg();
    private final zzdb<zzjf> zzapj;

    public zzjg() {
        this(zzda.zzg(new zzji()));
    }

    private zzjg(zzdb<zzjf> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzxi() {
        return ((zzjf) zzapn.get()).zzxi();
    }

    public final /* synthetic */ Object get() {
        return (zzjf) this.zzapj.get();
    }
}
