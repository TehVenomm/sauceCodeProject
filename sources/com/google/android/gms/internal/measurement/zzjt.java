package com.google.android.gms.internal.measurement;

public final class zzjt implements zzdb<zzjw> {
    private static zzjt zzari = new zzjt();
    private final zzdb<zzjw> zzapj;

    public zzjt() {
        this(zzda.zzg(new zzjv()));
    }

    private zzjt(zzdb<zzjw> zzdb) {
        this.zzapj = zzda.zza(zzdb);
    }

    public static boolean zzyy() {
        return ((zzjw) zzari.get()).zzyy();
    }

    public final /* synthetic */ Object get() {
        return (zzjw) this.zzapj.get();
    }
}
