package com.google.android.gms.drive.events;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.internal.zzbhe;
import java.util.Arrays;

public final class zzn extends zza implements DriveEvent {
    public static final Creator<zzn> CREATOR = new zzo();
    private zzbhe zzgfo;

    public zzn(zzbhe zzbhe) {
        this.zzgfo = zzbhe;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        return zzbf.equal(this.zzgfo, ((zzn) obj).zzgfo);
    }

    public final int getType() {
        return 8;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgfo});
    }

    public final String toString() {
        return String.format("TransferProgressEvent[%s]", new Object[]{this.zzgfo.toString()});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfo, i, false);
        zzd.zzai(parcel, zze);
    }

    public final zzbhe zzani() {
        return this.zzgfo;
    }
}
