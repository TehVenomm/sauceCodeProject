package com.google.android.gms.internal.measurement;

public final class zzli implements zzdb<zzlh> {
    private static zzli zzasu = new zzli();
    private final zzdb<zzlh> zzapj;

    public zzli() {
        this(zzda.zzg(new zzlk()));
    }

    private zzli(zzdb<zzlh> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzv() {
        return ((zzlh) zzasu.get()).zzzv();
    }

    public final /* synthetic */ Object get() {
        return (zzlh) this.zzapj.get();
    }
}
