package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.events.zze;
import com.google.android.gms.drive.events.zzp;
import com.google.android.gms.drive.events.zzt;

public final class zzbhg extends zza {
    public static final Creator<zzbhg> CREATOR = new zzbhh();
    final int zzfxs;
    final DriveId zzgcx;
    private zze zzget;
    private zzt zzgfu;
    private zzp zzgfv;

    public zzbhg(int i, DriveId driveId) {
        this((DriveId) zzbp.zzu(driveId), 1, null, null, null);
    }

    zzbhg(DriveId driveId, int i, zze zze, zzt zzt, zzp zzp) {
        this.zzgcx = driveId;
        this.zzfxs = i;
        this.zzget = zze;
        this.zzgfu = zzt;
        this.zzgfv = zzp;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgcx, i, false);
        zzd.zzc(parcel, 3, this.zzfxs);
        zzd.zza(parcel, 4, this.zzget, i, false);
        zzd.zza(parcel, 5, this.zzgfu, i, false);
        zzd.zza(parcel, 6, this.zzgfv, i, false);
        zzd.zzai(parcel, zze);
    }
}
