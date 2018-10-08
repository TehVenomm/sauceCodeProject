package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.nearby.connection.PayloadTransferUpdate;
import java.util.Arrays;

public final class zzckh extends zza {
    public static final Creator<zzckh> CREATOR = new zzcki();
    private final String zzjbb;
    private final PayloadTransferUpdate zzjco;

    public zzckh(String str, PayloadTransferUpdate payloadTransferUpdate) {
        this.zzjbb = str;
        this.zzjco = payloadTransferUpdate;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckh)) {
            return false;
        }
        zzckh zzckh = (zzckh) obj;
        return zzbf.equal(this.zzjbb, zzckh.zzjbb) && zzbf.equal(this.zzjco, zzckh.zzjco);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjbb, this.zzjco});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjbb, false);
        zzd.zza(parcel, 2, this.zzjco, i, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzbaj() {
        return this.zzjbb;
    }

    public final PayloadTransferUpdate zzbaq() {
        return this.zzjco;
    }
}
