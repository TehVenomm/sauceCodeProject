package com.google.android.gms.drive.events;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;
import java.util.Locale;

public final class zzp extends zza {
    public static final Creator<zzp> CREATOR = new zzq();
    private int zzgfp;

    public zzp(int i) {
        this.zzgfp = i;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        return zzbf.equal(Integer.valueOf(this.zzgfp), Integer.valueOf(((zzp) obj).zzgfp));
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzgfp)});
    }

    public final String toString() {
        return String.format(Locale.US, "TransferProgressOptions[type=%d]", new Object[]{Integer.valueOf(this.zzgfp)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgfp);
        zzd.zzai(parcel, zze);
    }
}
