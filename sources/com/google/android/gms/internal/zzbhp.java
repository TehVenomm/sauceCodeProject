package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveFile;

public final class zzbhp extends zza {
    public static final Creator<zzbhp> CREATOR = new zzbhq();
    private int zzgcw;

    public zzbhp(int i) {
        boolean z = i == DriveFile.MODE_WRITE_ONLY || i == DriveFile.MODE_READ_WRITE;
        zzbp.zzb(z, (Object) "Cannot create a new read-only contents!");
        this.zzgcw = i;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgcw);
        zzd.zzai(parcel, zze);
    }
}
