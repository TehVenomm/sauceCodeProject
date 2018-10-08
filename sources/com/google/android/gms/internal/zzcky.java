package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzcky extends zza {
    public static final Creator<zzcky> CREATOR = new zzckz();
    @Nullable
    private final zzcjl zzjaz;
    @Nullable
    private final zzckr zzjcm;
    private final String[] zzjde;
    private final boolean zzjdf;

    public zzcky(@Nullable IBinder iBinder, String[] strArr, @Nullable zzckr zzckr, boolean z) {
        zzcjl zzcjl;
        if (iBinder == null) {
            zzcjl = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzcjl = queryLocalInterface instanceof zzcjl ? (zzcjl) queryLocalInterface : new zzcjn(iBinder);
        }
        this(zzcjl, strArr, zzckr, z);
    }

    private zzcky(@Nullable zzcjl zzcjl, String[] strArr, @Nullable zzckr zzckr, boolean z) {
        this.zzjaz = zzcjl;
        this.zzjde = strArr;
        this.zzjcm = zzckr;
        this.zzjdf = z;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcky)) {
            return false;
        }
        zzcky zzcky = (zzcky) obj;
        return zzbf.equal(this.zzjaz, zzcky.zzjaz) && Arrays.equals(this.zzjde, zzcky.zzjde) && zzbf.equal(this.zzjcm, zzcky.zzjcm) && zzbf.equal(Boolean.valueOf(this.zzjdf), Boolean.valueOf(zzcky.zzjdf));
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjaz, this.zzjde, this.zzjcm, Boolean.valueOf(this.zzjdf)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjaz == null ? null : this.zzjaz.asBinder(), false);
        zzd.zza(parcel, 2, this.zzjde, false);
        zzd.zza(parcel, 3, this.zzjcm, i, false);
        zzd.zza(parcel, 4, this.zzjdf);
        zzd.zzai(parcel, zze);
    }
}
