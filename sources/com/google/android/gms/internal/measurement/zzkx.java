package com.google.android.gms.internal.measurement;

public final class zzkx implements zzdb<zzla> {
    private static zzkx zzasi = new zzkx();
    private final zzdb<zzla> zzapj;

    public zzkx() {
        this(zzda.zzg(new zzkz()));
    }

    private zzkx(zzdb<zzla> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzzo() {
        return ((zzla) zzasi.get()).zzzo();
    }

    public final /* synthetic */ Object get() {
        return (zzla) this.zzapj.get();
    }
}
