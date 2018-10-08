package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;

public final class zzclm extends zza {
    public static final Creator<zzclm> CREATOR = new zzcln();
    private static final String zzjfg = null;
    public static final zzclm zzjfh = new zzclm("", null);
    private int zzdxt;
    private final String zzjfi;
    @Nullable
    private final String zzjfj;

    zzclm(int i, @Nullable String str, @Nullable String str2) {
        this.zzdxt = ((Integer) zzbp.zzu(Integer.valueOf(i))).intValue();
        if (str == null) {
            str = "";
        }
        this.zzjfi = str;
        this.zzjfj = str2;
    }

    private zzclm(String str, String str2) {
        this(1, str, null);
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzclm)) {
                return false;
            }
            zzclm zzclm = (zzclm) obj;
            if (!zzbf.equal(this.zzjfi, zzclm.zzjfi)) {
                return false;
            }
            if (!zzbf.equal(this.zzjfj, zzclm.zzjfj)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjfi, this.zzjfj});
    }

    public final String toString() {
        String str = this.zzjfi;
        String str2 = this.zzjfj;
        return new StringBuilder((String.valueOf(str).length() + 40) + String.valueOf(str2).length()).append("NearbyDevice{handle=").append(str).append(", bluetoothAddress=").append(str2).append("}").toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 3, this.zzjfi, false);
        zzd.zza(parcel, 6, this.zzjfj, false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }
}
