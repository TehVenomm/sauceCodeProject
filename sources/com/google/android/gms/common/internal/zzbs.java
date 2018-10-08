package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbs extends zza {
    public static final Creator<zzbs> CREATOR = new zzbt();
    private int zzdxt;
    private ConnectionResult zzfit;
    private boolean zzflo;
    private IBinder zzfvt;
    private boolean zzfvu;

    zzbs(int i, IBinder iBinder, ConnectionResult connectionResult, boolean z, boolean z2) {
        this.zzdxt = i;
        this.zzfvt = iBinder;
        this.zzfit = connectionResult;
        this.zzflo = z;
        this.zzfvu = z2;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzbs)) {
                return false;
            }
            zzbs zzbs = (zzbs) obj;
            if (!this.zzfit.equals(zzbs.zzfit)) {
                return false;
            }
            if (!zzakl().equals(zzbs.zzakl())) {
                return false;
            }
        }
        return true;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzfvt, false);
        zzd.zza(parcel, 3, this.zzfit, i, false);
        zzd.zza(parcel, 4, this.zzflo);
        zzd.zza(parcel, 5, this.zzfvu);
        zzd.zzai(parcel, zze);
    }

    public final ConnectionResult zzagc() {
        return this.zzfit;
    }

    public final zzam zzakl() {
        IBinder iBinder = this.zzfvt;
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IAccountAccessor");
        return queryLocalInterface instanceof zzam ? (zzam) queryLocalInterface : new zzao(iBinder);
    }

    public final boolean zzakm() {
        return this.zzflo;
    }

    public final boolean zzakn() {
        return this.zzfvu;
    }
}
