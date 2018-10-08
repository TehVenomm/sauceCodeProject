package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzasa extends zza {
    public static final Creator<zzasa> CREATOR = new zzasb();
    private String accountType;
    private int zzdxt;
    private byte[] zzdzt;

    zzasa(int i, String str, byte[] bArr) {
        this.zzdxt = 1;
        this.accountType = (String) zzbp.zzu(str);
        this.zzdzt = (byte[]) zzbp.zzu(bArr);
    }

    public zzasa(String str, byte[] bArr) {
        this(1, str, bArr);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.accountType, false);
        zzd.zza(parcel, 3, this.zzdzt, false);
        zzd.zzai(parcel, zze);
    }
}
