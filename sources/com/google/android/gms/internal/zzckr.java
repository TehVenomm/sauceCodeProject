package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzckr extends zza {
    public static final Creator<zzckr> CREATOR = new zzcks();
    private final long id;
    private final int type;
    @Nullable
    private final byte[] zzjao;
    @Nullable
    private final ParcelFileDescriptor zzjcy;
    @Nullable
    private final String zzjcz;
    private final long zzjda;
    @Nullable
    private final ParcelFileDescriptor zzjdb;

    public zzckr(long j, int i, @Nullable byte[] bArr, @Nullable ParcelFileDescriptor parcelFileDescriptor, @Nullable String str, long j2, @Nullable ParcelFileDescriptor parcelFileDescriptor2) {
        this.id = j;
        this.type = i;
        this.zzjao = bArr;
        this.zzjcy = parcelFileDescriptor;
        this.zzjcz = str;
        this.zzjda = j2;
        this.zzjdb = parcelFileDescriptor2;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckr)) {
            return false;
        }
        zzckr zzckr = (zzckr) obj;
        return zzbf.equal(Long.valueOf(this.id), Long.valueOf(zzckr.id)) && zzbf.equal(Integer.valueOf(this.type), Integer.valueOf(zzckr.type)) && zzbf.equal(this.zzjao, zzckr.zzjao) && zzbf.equal(this.zzjcy, zzckr.zzjcy) && zzbf.equal(this.zzjcz, zzckr.zzjcz) && zzbf.equal(Long.valueOf(this.zzjda), Long.valueOf(zzckr.zzjda)) && zzbf.equal(this.zzjdb, zzckr.zzjdb);
    }

    @Nullable
    public final byte[] getBytes() {
        return this.zzjao;
    }

    public final long getId() {
        return this.id;
    }

    public final int getType() {
        return this.type;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Long.valueOf(this.id), Integer.valueOf(this.type), this.zzjao, this.zzjcy, this.zzjcz, Long.valueOf(this.zzjda), this.zzjdb});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.id);
        zzd.zzc(parcel, 2, this.type);
        zzd.zza(parcel, 3, this.zzjao, false);
        zzd.zza(parcel, 4, this.zzjcy, i, false);
        zzd.zza(parcel, 5, this.zzjcz, false);
        zzd.zza(parcel, 6, this.zzjda);
        zzd.zza(parcel, 7, this.zzjdb, i, false);
        zzd.zzai(parcel, zze);
    }

    @Nullable
    public final ParcelFileDescriptor zzbar() {
        return this.zzjcy;
    }

    @Nullable
    public final String zzbas() {
        return this.zzjcz;
    }

    public final long zzbat() {
        return this.zzjda;
    }
}
