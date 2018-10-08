package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.zza;
import com.google.android.gms.drive.zzv;
import java.util.List;

public final class zzblm extends zzv {
    public static final Creator<zzblm> CREATOR = new zzbln();
    private DataHolder zzgiu;
    private List<DriveId> zzgiv;
    private zza zzgiw;
    private boolean zzgix;

    public zzblm(DataHolder dataHolder, List<DriveId> list, zza zza, boolean z) {
        this.zzgiu = dataHolder;
        this.zzgiv = list;
        this.zzgiw = zza;
        this.zzgix = z;
    }

    protected final void zzaj(Parcel parcel, int i) {
        int i2 = i | 1;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgiu, i2, false);
        zzd.zzc(parcel, 3, this.zzgiv, false);
        zzd.zza(parcel, 4, this.zzgiw, i2, false);
        zzd.zza(parcel, 5, this.zzgix);
        zzd.zzai(parcel, zze);
    }
}
