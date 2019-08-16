package com.google.android.gms.internal.measurement;

public final class zzkk implements zzdb<zzkj> {
    private static zzkk zzarw = new zzkk();
    private final zzdb<zzkj> zzapj;

    public zzkk() {
        this(zzda.zzg(new zzkm()));
    }

    private zzkk(zzdb<zzkj> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzf() {
        return ((zzkj) zzarw.get()).zzzf();
    }

    public static boolean zzzg() {
        return ((zzkj) zzarw.get()).zzzg();
    }

    public final /* synthetic */ Object get() {
        return (zzkj) this.zzapj.get();
    }
}
