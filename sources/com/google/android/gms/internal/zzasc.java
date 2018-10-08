package com.google.android.gms.internal;

import android.app.PendingIntent;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzasc extends zza {
    public static final Creator<zzasc> CREATOR = new zzasd();
    private String accountType;
    private int zzdxt;
    private PendingIntent zzeaa;

    zzasc(int i, String str, PendingIntent pendingIntent) {
        this.zzdxt = 1;
        this.accountType = (String) zzbp.zzu(str);
        this.zzeaa = (PendingIntent) zzbp.zzu(pendingIntent);
    }

    public zzasc(String str, PendingIntent pendingIntent) {
        this(1, str, pendingIntent);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.accountType, false);
        zzd.zza(parcel, 3, this.zzeaa, i, false);
        zzd.zzai(parcel, zze);
    }
}
