package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.nearby.connection.DiscoveryOptions;
import java.util.Arrays;

public final class zzclc extends zza {
    public static final Creator<zzclc> CREATOR = new zzcld();
    private final long durationMillis;
    private final String zzjan;
    @Nullable
    private final zzcjl zzjaz;
    @Nullable
    private final zzcjb zzjdj;
    private final DiscoveryOptions zzjdk;
    @Nullable
    private final zzcjd zzjdl;

    public zzclc(@Nullable IBinder iBinder, @Nullable IBinder iBinder2, String str, long j, DiscoveryOptions discoveryOptions, @Nullable IBinder iBinder3) {
        zzcjl zzcjl;
        zzcjb zzcjb;
        zzcjd zzcjd = null;
        if (iBinder == null) {
            zzcjl = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzcjl = queryLocalInterface instanceof zzcjl ? (zzcjl) queryLocalInterface : new zzcjn(iBinder);
        }
        if (iBinder2 == null) {
            zzcjb = null;
        } else {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IDiscoveryCallback");
            zzcjb = queryLocalInterface instanceof zzcjb ? (zzcjb) queryLocalInterface : new zzcjc(iBinder2);
        }
        if (iBinder3 != null) {
            queryLocalInterface = iBinder3.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IDiscoveryListener");
            zzcjd = queryLocalInterface instanceof zzcjd ? (zzcjd) queryLocalInterface : new zzcjf(iBinder3);
        }
        this(zzcjl, zzcjb, str, j, discoveryOptions, zzcjd);
    }

    private zzclc(@Nullable zzcjl zzcjl, @Nullable zzcjb zzcjb, String str, long j, DiscoveryOptions discoveryOptions, @Nullable zzcjd zzcjd) {
        this.zzjaz = zzcjl;
        this.zzjdj = zzcjb;
        this.zzjan = str;
        this.durationMillis = j;
        this.zzjdk = discoveryOptions;
        this.zzjdl = zzcjd;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzclc)) {
            return false;
        }
        zzclc zzclc = (zzclc) obj;
        return zzbf.equal(this.zzjaz, zzclc.zzjaz) && zzbf.equal(this.zzjdj, zzclc.zzjdj) && zzbf.equal(this.zzjan, zzclc.zzjan) && zzbf.equal(Long.valueOf(this.durationMillis), Long.valueOf(zzclc.durationMillis)) && zzbf.equal(this.zzjdk, zzclc.zzjdk) && zzbf.equal(this.zzjdl, zzclc.zzjdl);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjaz, this.zzjdj, this.zzjan, Long.valueOf(this.durationMillis), this.zzjdk, this.zzjdl});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjaz == null ? null : this.zzjaz.asBinder(), false);
        zzd.zza(parcel, 2, this.zzjdj == null ? null : this.zzjdj.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjan, false);
        zzd.zza(parcel, 4, this.durationMillis);
        zzd.zza(parcel, 5, this.zzjdk, i, false);
        if (this.zzjdl != null) {
            iBinder = this.zzjdl.asBinder();
        }
        zzd.zza(parcel, 6, iBinder, false);
        zzd.zzai(parcel, zze);
    }
}
