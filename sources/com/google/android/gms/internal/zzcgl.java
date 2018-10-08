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

public final class zzcgl extends zza {
    public static final Creator<zzcgl> CREATOR = new zzcgm();
    @Nullable
    private final zzcjl zzjaz;
    @Nullable
    private final zzcis zzjba;
    private final String zzjbb;
    @Nullable
    private final byte[] zzjbc;
    @Nullable
    private final zzcji zzjbd;

    public zzcgl(@Nullable IBinder iBinder, @Nullable IBinder iBinder2, String str, @Nullable byte[] bArr, @Nullable IBinder iBinder3) {
        zzcjl zzcjl;
        zzcis zzcis;
        zzcji zzcji = null;
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
        if (iBinder3 != null) {
            queryLocalInterface = iBinder3.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IPayloadListener");
            zzcji = queryLocalInterface instanceof zzcji ? (zzcji) queryLocalInterface : new zzcjk(iBinder3);
        }
        this(zzcjl, zzcis, str, bArr, zzcji);
    }

    private zzcgl(@Nullable zzcjl zzcjl, @Nullable zzcis zzcis, String str, @Nullable byte[] bArr, @Nullable zzcji zzcji) {
        this.zzjaz = zzcjl;
        this.zzjba = zzcis;
        this.zzjbb = str;
        this.zzjbc = bArr;
        this.zzjbd = zzcji;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcgl)) {
            return false;
        }
        zzcgl zzcgl = (zzcgl) obj;
        return zzbf.equal(this.zzjaz, zzcgl.zzjaz) && zzbf.equal(this.zzjba, zzcgl.zzjba) && zzbf.equal(this.zzjbb, zzcgl.zzjbb) && zzbf.equal(this.zzjbc, zzcgl.zzjbc) && zzbf.equal(this.zzjbd, zzcgl.zzjbd);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjaz, this.zzjba, this.zzjbb, this.zzjbc, this.zzjbd});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjaz == null ? null : this.zzjaz.asBinder(), false);
        zzd.zza(parcel, 2, this.zzjba == null ? null : this.zzjba.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjbb, false);
        zzd.zza(parcel, 4, this.zzjbc, false);
        if (this.zzjbd != null) {
            iBinder = this.zzjbd.asBinder();
        }
        zzd.zza(parcel, 5, iBinder, false);
        zzd.zzai(parcel, zze);
    }
}
