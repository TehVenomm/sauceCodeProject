package com.google.android.gms.internal.measurement;

public final class zzlj implements zzdb<zzlm> {
    private static zzlj zzasv = new zzlj();
    private final zzdb<zzlm> zzapj;

    public zzlj() {
        this(zzda.zzg(new zzll()));
    }

    private zzlj(zzdb<zzlm> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzw() {
        return ((zzlm) zzasv.get()).zzzw();
    }

    public final /* synthetic */ Object get() {
        return (zzlm) this.zzapj.get();
    }
}
