package com.google.android.gms.internal;

import com.google.android.gms.common.internal.safeparcel.SafeParcelable;

public abstract class zzbda extends zzbcx implements SafeParcelable {
    public final int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!getClass().isInstance(obj)) {
            return false;
        }
        zzbcx zzbcx = (zzbcx) obj;
        for (zzbcy zzbcy : zzzx().values()) {
            if (zza(zzbcy)) {
                if (!zzbcx.zza(zzbcy)) {
                    return false;
                }
                if (!zzb(zzbcy).equals(zzbcx.zzb(zzbcy))) {
                    return false;
                }
            } else if (zzbcx.zza(zzbcy)) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        int i = 0;
        for (zzbcy zzbcy : zzzx().values()) {
            i = zza(zzbcy) ? zzb(zzbcy).hashCode() + (i * 31) : i;
        }
        return i;
    }

    public Object zzgh(String str) {
        return null;
    }

    public boolean zzgi(String str) {
        return false;
    }
}
