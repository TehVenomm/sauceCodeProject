package com.google.android.gms.internal.measurement;

public final class zzjs implements zzdb<zzjr> {
    private static zzjs zzarh = new zzjs();
    private final zzdb<zzjr> zzapj;

    public zzjs() {
        this(zzda.zzg(new zzju()));
    }

    private zzjs(zzdb<zzjr> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzyx() {
        return ((zzjr) zzarh.get()).zzyx();
    }

    public final /* synthetic */ Object get() {
        return (zzjr) this.zzapj.get();
    }
}
