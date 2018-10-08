package com.google.android.gms.drive.events;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.internal.zzbhe;
import java.util.Arrays;
import java.util.List;

public final class zzr extends zza implements DriveEvent {
    public static final Creator<zzr> CREATOR = new zzs();
    private String zzdxg;
    private List<zzbhe> zzgfq;

    public zzr(String str, List<zzbhe> list) {
        this.zzdxg = str;
        this.zzgfq = list;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzr zzr = (zzr) obj;
        return zzbf.equal(this.zzdxg, zzr.zzdxg) && zzbf.equal(this.zzgfq, zzr.zzgfq);
    }

    public final int getType() {
        return 7;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzdxg, this.zzgfq});
    }

    public final String toString() {
        return String.format("TransferStateEvent[%s]", new Object[]{TextUtils.join("','", this.zzgfq)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzdxg, false);
        zzd.zzc(parcel, 3, this.zzgfq, false);
        zzd.zzai(parcel, zze);
    }
}
