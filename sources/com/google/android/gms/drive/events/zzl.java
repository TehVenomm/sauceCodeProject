package com.google.android.gms.drive.events;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzv;

public final class zzl extends zzv implements DriveEvent {
    public static final Creator<zzl> CREATOR = new zzm();
    private DataHolder zzfkz;
    private boolean zzgfm;
    private int zzgfn;

    public zzl(DataHolder dataHolder, boolean z, int i) {
        this.zzfkz = dataHolder;
        this.zzgfm = z;
        this.zzgfn = i;
    }

    public final int getType() {
        return 3;
    }

    public final void zzaj(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzfkz, i, false);
        zzd.zza(parcel, 3, this.zzgfm);
        zzd.zzc(parcel, 4, this.zzgfn);
        zzd.zzai(parcel, zze);
    }

    public final DataHolder zzanf() {
        return this.zzfkz;
    }

    public final boolean zzang() {
        return this.zzgfm;
    }

    public final int zzanh() {
        return this.zzgfn;
    }
}
