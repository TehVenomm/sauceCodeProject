package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzx extends zza {
    public static final Creator<zzx> CREATOR = new zzy();
    public static final zzx zzgnx = new zzx("=");
    public static final zzx zzgny = new zzx("<");
    public static final zzx zzgnz = new zzx("<=");
    public static final zzx zzgoa = new zzx(">");
    public static final zzx zzgob = new zzx(">=");
    public static final zzx zzgoc = new zzx("and");
    public static final zzx zzgod = new zzx("or");
    private static zzx zzgoe = new zzx("not");
    public static final zzx zzgof = new zzx("contains");
    private String mTag;

    zzx(String str) {
        this.mTag = str;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            zzx zzx = (zzx) obj;
            if (this.mTag == null) {
                if (zzx.mTag != null) {
                    return false;
                }
            } else if (!this.mTag.equals(zzx.mTag)) {
                return false;
            }
        }
        return true;
    }

    public final String getTag() {
        return this.mTag;
    }

    public final int hashCode() {
        return (this.mTag == null ? 0 : this.mTag.hashCode()) + 31;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.mTag, false);
        zzd.zzai(parcel, zze);
    }
}
