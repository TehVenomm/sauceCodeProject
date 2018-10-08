package com.google.android.gms.drive.events;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;
import java.util.Locale;

public final class zzb extends zza implements DriveEvent {
    public static final Creator<zzb> CREATOR = new zzc();
    private String zzdxg;
    private zze zzget;

    public zzb(String str, zze zze) {
        this.zzdxg = str;
        this.zzget = zze;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzb zzb = (zzb) obj;
        return zzbf.equal(this.zzget, zzb.zzget) && zzbf.equal(this.zzdxg, zzb.zzdxg);
    }

    public final int getType() {
        return 4;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzget, this.zzdxg});
    }

    public final String toString() {
        return String.format(Locale.US, "ChangesAvailableEvent [changesAvailableOptions=%s]", new Object[]{this.zzget});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzdxg, false);
        zzd.zza(parcel, 3, this.zzget, i, false);
        zzd.zzai(parcel, zze);
    }
}
