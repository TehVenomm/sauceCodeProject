package com.google.android.gms.common.api.internal;

public final class zzcl<L> {
    private final L mListener;
    private final String zzfor;

    zzcl(L l, String str) {
        this.mListener = l;
        this.zzfor = str;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzcl)) {
                return false;
            }
            zzcl zzcl = (zzcl) obj;
            if (this.mListener != zzcl.mListener) {
                return false;
            }
            if (!this.zzfor.equals(zzcl.zzfor)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return (System.identityHashCode(this.mListener) * 31) + this.zzfor.hashCode();
    }
}
