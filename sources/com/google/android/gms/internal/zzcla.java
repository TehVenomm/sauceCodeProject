package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.nearby.connection.AdvertisingOptions;
import java.util.Arrays;

public final class zzcla extends zza {
    public static final Creator<zzcla> CREATOR = new zzclb();
    private final long durationMillis;
    private final String name;
    private final String zzjan;
    @Nullable
    private final zzciv zzjdd;
    @Nullable
    private final zzcjo zzjdg;
    @Nullable
    private final zzcip zzjdh;
    private final AdvertisingOptions zzjdi;

    public zzcla(@Nullable IBinder iBinder, @Nullable IBinder iBinder2, String str, String str2, long j, AdvertisingOptions advertisingOptions, @Nullable IBinder iBinder3) {
        zzcjo zzcjo;
        zzcip zzcip;
        zzciv zzciv;
        if (iBinder == null) {
            zzcjo = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IStartAdvertisingResultListener");
            zzcjo = queryLocalInterface instanceof zzcjo ? (zzcjo) queryLocalInterface : new zzcjq(iBinder);
        }
        if (iBinder2 == null) {
            zzcip = null;
        } else {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IAdvertisingCallback");
            zzcip = queryLocalInterface instanceof zzcip ? (zzcip) queryLocalInterface : new zzcir(iBinder2);
        }
        if (iBinder3 == null) {
            zzciv = null;
        } else {
            queryLocalInterface = iBinder3.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener");
            zzciv = queryLocalInterface instanceof zzciv ? (zzciv) queryLocalInterface : new zzcix(iBinder3);
        }
        this(zzcjo, zzcip, str, str2, j, advertisingOptions, zzciv);
    }

    private zzcla(@Nullable zzcjo zzcjo, @Nullable zzcip zzcip, String str, String str2, long j, AdvertisingOptions advertisingOptions, @Nullable zzciv zzciv) {
        this.zzjdg = zzcjo;
        this.zzjdh = zzcip;
        this.name = str;
        this.zzjan = str2;
        this.durationMillis = j;
        this.zzjdi = advertisingOptions;
        this.zzjdd = zzciv;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcla)) {
            return false;
        }
        zzcla zzcla = (zzcla) obj;
        return zzbf.equal(this.zzjdg, zzcla.zzjdg) && zzbf.equal(this.zzjdh, zzcla.zzjdh) && zzbf.equal(this.name, zzcla.name) && zzbf.equal(this.zzjan, zzcla.zzjan) && zzbf.equal(Long.valueOf(this.durationMillis), Long.valueOf(zzcla.durationMillis)) && zzbf.equal(this.zzjdi, zzcla.zzjdi) && zzbf.equal(this.zzjdd, zzcla.zzjdd);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjdg, this.zzjdh, this.name, this.zzjan, Long.valueOf(this.durationMillis), this.zzjdi, this.zzjdd});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjdg == null ? null : this.zzjdg.asBinder(), false);
        zzd.zza(parcel, 2, this.zzjdh == null ? null : this.zzjdh.asBinder(), false);
        zzd.zza(parcel, 3, this.name, false);
        zzd.zza(parcel, 4, this.zzjan, false);
        zzd.zza(parcel, 5, this.durationMillis);
        zzd.zza(parcel, 6, this.zzjdi, i, false);
        if (this.zzjdd != null) {
            iBinder = this.zzjdd.asBinder();
        }
        zzd.zza(parcel, 7, iBinder, false);
        zzd.zzai(parcel, zze);
    }
}
