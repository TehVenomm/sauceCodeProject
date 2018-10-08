package com.google.android.gms.auth.api.signin.internal;

public final class zzo {
    private static int zzedb = 31;
    private int zzedc = 1;

    public final int zzaan() {
        return this.zzedc;
    }

    public final zzo zzaq(boolean z) {
        this.zzedc = (z ? 1 : 0) + (zzedb * this.zzedc);
        return this;
    }

    public final zzo zzo(Object obj) {
        this.zzedc = (obj == null ? 0 : obj.hashCode()) + (zzedb * this.zzedc);
        return this;
    }
}
