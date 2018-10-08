package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.ParcelUuid;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzclk extends zza {
    public static final Creator<zzclk> CREATOR = new zzcll();
    private final int zzdxt;
    private final ParcelUuid zzjey;
    private final ParcelUuid zzjez;
    private final ParcelUuid zzjfa;
    private final byte[] zzjfb;
    private final byte[] zzjfc;
    private final int zzjfd;
    private final byte[] zzjfe;
    private final byte[] zzjff;

    zzclk(int i, ParcelUuid parcelUuid, ParcelUuid parcelUuid2, ParcelUuid parcelUuid3, byte[] bArr, byte[] bArr2, int i2, byte[] bArr3, byte[] bArr4) {
        this.zzdxt = i;
        this.zzjey = parcelUuid;
        this.zzjez = parcelUuid2;
        this.zzjfa = parcelUuid3;
        this.zzjfb = bArr;
        this.zzjfc = bArr2;
        this.zzjfd = i2;
        this.zzjfe = bArr3;
        this.zzjff = bArr4;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        zzclk zzclk = (zzclk) obj;
        return this.zzjfd == zzclk.zzjfd && Arrays.equals(this.zzjfe, zzclk.zzjfe) && Arrays.equals(this.zzjff, zzclk.zzjff) && zzbf.equal(this.zzjfa, zzclk.zzjfa) && Arrays.equals(this.zzjfb, zzclk.zzjfb) && Arrays.equals(this.zzjfc, zzclk.zzjfc) && zzbf.equal(this.zzjey, zzclk.zzjey) && zzbf.equal(this.zzjez, zzclk.zzjez);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzjfd), Integer.valueOf(Arrays.hashCode(this.zzjfe)), Integer.valueOf(Arrays.hashCode(this.zzjff)), this.zzjfa, Integer.valueOf(Arrays.hashCode(this.zzjfb)), Integer.valueOf(Arrays.hashCode(this.zzjfc)), this.zzjey, this.zzjez});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 4, this.zzjey, i, false);
        zzd.zza(parcel, 5, this.zzjez, i, false);
        zzd.zza(parcel, 6, this.zzjfa, i, false);
        zzd.zza(parcel, 7, this.zzjfb, false);
        zzd.zza(parcel, 8, this.zzjfc, false);
        zzd.zzc(parcel, 9, this.zzjfd);
        zzd.zza(parcel, 10, this.zzjfe, false);
        zzd.zza(parcel, 11, this.zzjff, false);
        zzd.zzai(parcel, zze);
    }
}
