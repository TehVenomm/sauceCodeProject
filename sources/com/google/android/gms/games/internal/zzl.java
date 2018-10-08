package com.google.android.gms.games.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzl extends zzc {
    public static final Creator<zzl> CREATOR = new zzm();
    private final Bundle zzhgr;
    private final IBinder zzhgs;

    zzl(Bundle bundle, IBinder iBinder) {
        this.zzhgr = bundle;
        this.zzhgs = iBinder;
    }

    public zzl(zzp zzp) {
        this.zzhgr = zzp.zzaqz();
        this.zzhgs = zzp.zzhgv;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzhgr, false);
        zzd.zza(parcel, 2, this.zzhgs, false);
        zzd.zzai(parcel, zze);
    }
}
