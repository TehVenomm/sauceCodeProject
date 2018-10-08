package com.google.android.gms.games.internal.player;

import android.net.Uri;
import android.os.Parcel;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzc;

public final class zzd extends zzc implements zza {
    private final zze zzhdi;

    public zzd(DataHolder dataHolder, int i, zze zze) {
        super(dataHolder, i);
        this.zzhdi = zze;
    }

    public final int describeContents() {
        return 0;
    }

    public final boolean equals(Object obj) {
        return zzb.zza(this, obj);
    }

    public final /* synthetic */ Object freeze() {
        return new zzb(this);
    }

    public final int hashCode() {
        return zzb.zza(this);
    }

    public final String toString() {
        return zzb.zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((zzb) ((zza) freeze())).writeToParcel(parcel, i);
    }

    public final String zzari() {
        return getString(this.zzhdi.zzhkb);
    }

    public final String zzarj() {
        return getString(this.zzhdi.zzhkc);
    }

    public final long zzark() {
        return getLong(this.zzhdi.zzhkd);
    }

    public final Uri zzarl() {
        return zzfu(this.zzhdi.zzhke);
    }

    public final Uri zzarm() {
        return zzfu(this.zzhdi.zzhkf);
    }

    public final Uri zzarn() {
        return zzfu(this.zzhdi.zzhkg);
    }
}
