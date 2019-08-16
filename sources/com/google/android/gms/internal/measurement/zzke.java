package com.google.android.gms.internal.measurement;

public final class zzke implements zzdb<zzkd> {
    private static zzke zzars = new zzke();
    private final zzdb<zzkd> zzapj;

    public zzke() {
        this(zzda.zzg(new zzkg()));
    }

    private zzke(zzdb<zzkd> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzd() {
        return ((zzkd) zzars.get()).zzzd();
    }

    public final /* synthetic */ Object get() {
        return (zzkd) this.zzapj.get();
    }
}
