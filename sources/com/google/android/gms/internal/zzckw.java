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

public final class zzckw extends zza {
    public static final Creator<zzckw> CREATOR = new zzckx();
    @Nullable
    private final String name;
    @Nullable
    private final zzcjl zzjaz;
    @Nullable
    private final zzcis zzjba;
    private final String zzjbb;
    @Nullable
    private final byte[] zzjbc;
    @Nullable
    private final zzciy zzjdc;
    @Nullable
    private final zzciv zzjdd;

    public zzckw(@Nullable IBinder iBinder, @Nullable IBinder iBinder2, @Nullable IBinder iBinder3, @Nullable String str, String str2, @Nullable byte[] bArr, @Nullable IBinder iBinder4) {
        zzcjl zzcjl;
        zzcis zzcis;
        zzciy zzciy;
        zzciv zzciv = null;
        if (iBinder == null) {
            zzcjl = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzcjl = queryLocalInterface instanceof zzcjl ? (zzcjl) queryLocalInterface : new zzcjn(iBinder);
        }
        if (iBinder2 == null) {
            zzcis = null;
        } else {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IConnectionEventListener");
            zzcis = queryLocalInterface instanceof zzcis ? (zzcis) queryLocalInterface : new zzciu(iBinder2);
        }
        if (iBinder3 == null) {
            zzciy = null;
        } else {
            queryLocalInterface = iBinder3.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IConnectionResponseListener");
            zzciy = queryLocalInterface instanceof zzciy ? (zzciy) queryLocalInterface : new zzcja(iBinder3);
        }
        if (iBinder4 != null) {
            queryLocalInterface = iBinder4.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener");
            zzciv = queryLocalInterface instanceof zzciv ? (zzciv) queryLocalInterface : new zzcix(iBinder4);
        }
        this(zzcjl, zzcis, zzciy, str, str2, bArr, zzciv);
    }

    private zzckw(@Nullable zzcjl zzcjl, @Nullable zzcis zzcis, @Nullable zzciy zzciy, @Nullable String str, String str2, @Nullable byte[] bArr, @Nullable zzciv zzciv) {
        this.zzjaz = zzcjl;
        this.zzjba = zzcis;
        this.zzjdc = zzciy;
        this.name = str;
        this.zzjbb = str2;
        this.zzjbc = bArr;
        this.zzjdd = zzciv;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckw)) {
            return false;
        }
        zzckw zzckw = (zzckw) obj;
        return zzbf.equal(this.zzjaz, zzckw.zzjaz) && zzbf.equal(this.zzjba, zzckw.zzjba) && zzbf.equal(this.zzjdc, zzckw.zzjdc) && zzbf.equal(this.name, zzckw.name) && zzbf.equal(this.zzjbb, zzckw.zzjbb) && zzbf.equal(this.zzjbc, zzckw.zzjbc) && zzbf.equal(this.zzjdd, zzckw.zzjdd);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjaz, this.zzjba, this.zzjdc, this.name, this.zzjbb, this.zzjbc, this.zzjdd});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjaz == null ? null : this.zzjaz.asBinder(), false);
        zzd.zza(parcel, 2, this.zzjba == null ? null : this.zzjba.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjdc == null ? null : this.zzjdc.asBinder(), false);
        zzd.zza(parcel, 4, this.name, false);
        zzd.zza(parcel, 5, this.zzjbb, false);
        zzd.zza(parcel, 6, this.zzjbc, false);
        if (this.zzjdd != null) {
            iBinder = this.zzjdd.asBinder();
        }
        zzd.zza(parcel, 7, iBinder, false);
        zzd.zzai(parcel, zze);
    }
}
