package com.google.android.gms.internal.measurement;

public final class zzlq implements zzln {
    private static final zzcm<Long> zzapw;
    private static final zzcm<Boolean> zzata;

    static {
        zzct zzct = new zzct(zzcn.zzdh("com.google.android.gms.measurement"));
        zzata = zzct.zzb("measurement.reset_analytics.persist_time", false);
        zzapw = zzct.zze("measurement.id.reset_analytics.persist_time", 0);
    }

    public final boolean zzzx() {
        return ((Boolean) zzata.get()).booleanValue();
    }
}
