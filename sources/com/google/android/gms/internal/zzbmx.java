package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.query.Query;

public final class zzbmx extends zza {
    public static final Creator<zzbmx> CREATOR = new zzbmy();
    private Query zzgjv;

    public zzbmx(Query query) {
        this.zzgjv = query;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjv, i, false);
        zzd.zzai(parcel, zze);
    }
}
