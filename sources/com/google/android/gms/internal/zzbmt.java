package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.query.internal.FilterHolder;

public final class zzbmt extends zza {
    public static final Creator<zzbmt> CREATOR = new zzbmu();
    private String zzehi;
    private String[] zzgee;
    private DriveId zzgeg;
    private FilterHolder zzgjt;

    public zzbmt(String str, String[] strArr, DriveId driveId, FilterHolder filterHolder) {
        this.zzehi = str;
        this.zzgee = strArr;
        this.zzgeg = driveId;
        this.zzgjt = filterHolder;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzehi, false);
        zzd.zza(parcel, 3, this.zzgee, false);
        zzd.zza(parcel, 4, this.zzgeg, i, false);
        zzd.zza(parcel, 5, this.zzgjt, i, false);
        zzd.zzai(parcel, zze);
    }
}
