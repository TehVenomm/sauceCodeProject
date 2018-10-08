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

public final class zzcku extends zza {
    public static final Creator<zzcku> CREATOR = new zzckv();
    @Nullable
    private final zzcjl zzjaz;
    private final String zzjbb;

    public zzcku(@Nullable IBinder iBinder, String str) {
        zzcjl zzcjl;
        if (iBinder == null) {
            zzcjl = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzcjl = queryLocalInterface instanceof zzcjl ? (zzcjl) queryLocalInterface : new zzcjn(iBinder);
        }
        this(zzcjl, str);
    }

    private zzcku(@Nullable zzcjl zzcjl, String str) {
        this.zzjaz = zzcjl;
        this.zzjbb = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcku)) {
            return false;
        }
        zzcku zzcku = (zzcku) obj;
        return zzbf.equal(this.zzjaz, zzcku.zzjaz) && zzbf.equal(this.zzjbb, zzcku.zzjbb);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjaz, this.zzjbb});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjaz == null ? null : this.zzjaz.asBinder(), false);
        zzd.zza(parcel, 2, this.zzjbb, false);
        zzd.zzai(parcel, zze);
    }
}
