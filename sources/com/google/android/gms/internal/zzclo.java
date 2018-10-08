package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.nearby.messages.internal.zzg;
import com.google.android.gms.nearby.messages.internal.zzl;
import java.util.UUID;

public final class zzclo extends zza {
    public static final Creator<zzclo> CREATOR = new zzclp();
    private int zzdxt;
    private int zzjfk;
    private byte[] zzjfl;
    private boolean zzjfm;

    zzclo(int i, int i2, byte[] bArr, boolean z) {
        this.zzdxt = i;
        this.zzjfk = i2;
        this.zzjfl = bArr;
        this.zzjfm = z;
    }

    private zzclo(int i, byte[] bArr) {
        this(1, i, bArr, false);
    }

    public static zzclo zza(UUID uuid, @Nullable Short sh, @Nullable Short sh2) {
        return new zzclo(3, new zzl(uuid, sh, sh2).getBytes());
    }

    public static zzclo zzaw(String str, @Nullable String str2) {
        Object obj;
        String valueOf = String.valueOf(str);
        if (str2 == null) {
            obj = "";
        }
        String valueOf2 = String.valueOf(obj);
        return new zzclo(2, new zzg(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf)).getBytes());
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzjfk);
        zzd.zza(parcel, 2, this.zzjfl, false);
        zzd.zza(parcel, 3, this.zzjfm);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }
}
