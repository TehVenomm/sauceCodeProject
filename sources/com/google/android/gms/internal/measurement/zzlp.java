package com.google.android.gms.internal.measurement;

public final class zzlp implements zzdb<zzls> {
    private static zzlp zzasz = new zzlp();
    private final zzdb<zzls> zzapj;

    public zzlp() {
        this(zzda.zzg(new zzlr()));
    }

    private zzlp(zzdb<zzls> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzy() {
        return ((zzls) zzasz.get()).zzzy();
    }

    public final /* synthetic */ Object get() {
        return (zzls) this.zzapj.get();
    }
}
