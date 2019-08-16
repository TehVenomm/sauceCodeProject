package com.google.android.gms.internal.measurement;

public final class zzjv implements zzjw {
    private static final zzcm<Long> zzapw;
    private static final zzcm<Boolean> zzark;

    static {
        zzct zzct = new zzct(zzcn.zzdh("com.google.android.gms.measurement"));
        zzark = zzct.zzb("measurement.upload_dsid_enabled", false);
        zzapw = zzct.zze("measurement.id.upload_dsid_enabled", 0);
    }

    public final boolean zzyy() {
        return ((Boolean) zzark.get()).booleanValue();
    }
}
