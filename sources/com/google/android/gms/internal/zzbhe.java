package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.drive.DriveId;
import java.util.Arrays;

public final class zzbhe extends zza {
    public static final Creator<zzbhe> CREATOR = new zzbhf();
    final int zzbyx;
    final DriveId zzgcx;
    final int zzgfp;
    final long zzgfs;
    final long zzgft;

    public zzbhe(int i, DriveId driveId, int i2, long j, long j2) {
        this.zzgfp = i;
        this.zzgcx = driveId;
        this.zzbyx = i2;
        this.zzgfs = j;
        this.zzgft = j2;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzbhe zzbhe = (zzbhe) obj;
        return this.zzgfp == zzbhe.zzgfp && zzbf.equal(this.zzgcx, zzbhe.zzgcx) && this.zzbyx == zzbhe.zzbyx && this.zzgfs == zzbhe.zzgfs && this.zzgft == zzbhe.zzgft;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzgfp), this.zzgcx, Integer.valueOf(this.zzbyx), Long.valueOf(this.zzgfs), Long.valueOf(this.zzgft)});
    }

    public final String toString() {
        return String.format("TransferProgressData[TransferType: %d, DriveId: %s, status: %d, bytes transferred: %d, total bytes: %d]", new Object[]{Integer.valueOf(this.zzgfp), this.zzgcx, Integer.valueOf(this.zzbyx), Long.valueOf(this.zzgfs), Long.valueOf(this.zzgft)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgfp);
        zzd.zza(parcel, 3, this.zzgcx, i, false);
        zzd.zzc(parcel, 4, this.zzbyx);
        zzd.zza(parcel, 5, this.zzgfs);
        zzd.zza(parcel, 6, this.zzgft);
        zzd.zzai(parcel, zze);
    }
}
