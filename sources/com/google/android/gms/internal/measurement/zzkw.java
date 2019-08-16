package com.google.android.gms.internal.measurement;

public final class zzkw implements zzdb<zzkv> {
    private static zzkw zzash = new zzkw();
    private final zzdb<zzkv> zzapj;

    public zzkw() {
        this(zzda.zzg(new zzky()));
    }

    private zzkw(zzdb<zzkv> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzm() {
        return ((zzkv) zzash.get()).zzzm();
    }

    public static boolean zzzn() {
        return ((zzkv) zzash.get()).zzzn();
    }

    public final /* synthetic */ Object get() {
        return (zzkv) this.zzapj.get();
    }
}
