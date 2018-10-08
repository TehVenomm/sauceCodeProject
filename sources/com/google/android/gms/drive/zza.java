package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.Base64;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzbno;
import com.google.android.gms.internal.zzego;

public final class zza extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Creator<zza> CREATOR = new zzb();
    private long zzgcr;
    private long zzgcs;
    private long zzgct;
    private volatile String zzgcu = null;

    public zza(long j, long j2, long j3) {
        boolean z = true;
        zzbp.zzbh(j != -1);
        zzbp.zzbh(j2 != -1);
        if (j3 == -1) {
            z = false;
        }
        zzbp.zzbh(z);
        this.zzgcr = j;
        this.zzgcs = j2;
        this.zzgct = j3;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zza)) {
            return false;
        }
        zza zza = (zza) obj;
        return zza.zzgcs == this.zzgcs && zza.zzgct == this.zzgct && zza.zzgcr == this.zzgcr;
    }

    public final int hashCode() {
        String valueOf = String.valueOf(this.zzgcr);
        String valueOf2 = String.valueOf(this.zzgcs);
        String valueOf3 = String.valueOf(this.zzgct);
        return new StringBuilder((String.valueOf(valueOf).length() + String.valueOf(valueOf2).length()) + String.valueOf(valueOf3).length()).append(valueOf).append(valueOf2).append(valueOf3).toString().hashCode();
    }

    public final String toString() {
        if (this.zzgcu == null) {
            zzego zzbno = new zzbno();
            zzbno.versionCode = 1;
            zzbno.sequenceNumber = this.zzgcr;
            zzbno.zzgjz = this.zzgcs;
            zzbno.zzgka = this.zzgct;
            String encodeToString = Base64.encodeToString(zzego.zzc(zzbno), 10);
            String valueOf = String.valueOf("ChangeSequenceNumber:");
            encodeToString = String.valueOf(encodeToString);
            this.zzgcu = encodeToString.length() != 0 ? valueOf.concat(encodeToString) : new String(valueOf);
        }
        return this.zzgcu;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgcr);
        zzd.zza(parcel, 3, this.zzgcs);
        zzd.zza(parcel, 4, this.zzgct);
        zzd.zzai(parcel, zze);
    }
}
